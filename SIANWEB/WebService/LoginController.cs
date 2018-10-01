using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using CapaEntidad;
using System.Configuration;
using System.Net.Mail;
using CapaNegocios;
using System.Text;
using System.Net;
using System.Net.Mime;
using SIANWEB.WebAPI.Models;

namespace SIANWEB.WebService
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]LoginModel loginInfo)
        {
            Usuario usuario = new Usuario();
            usuario.Cu_User = loginInfo.Username;
            usuario.Cu_pass = loginInfo.Password;
            string StrCnx = ConfigurationManager.AppSettings.Get("strConnection");
            string StrCnxEF = ConfigurationManager.AppSettings.Get("strConnectionEF");
            Int32 Id = default(Int32);
            Empresa Empresa = new Empresa();
            bool Dependientes = false;
            CapaNegocios.CN_Login CN_Login = new CapaNegocios.CN_Login();
            Empresa.Emp_Cnx = StrCnx + ";Connect Timeout=600";
            Int32 Minutos = default(Int32);

            //Aqui se debe llamar a una clase que en caso de que encuentre el usuario y contraseña regrese información del usuario así como el uso horario, información de bloqueo, y caducidad del password
            CN_Login.Login(ref usuario, out Id, out Minutos, out Dependientes, Empresa.Emp_Cnx);
            //Datos correctos
            if (Id == 1)
            {
                //La cuenta no está bloqueada
                if (usuario.Cu_Estatus == true)
                {
                    if (!usuario.Cu_Activo)
                    {
                        //Cuenta inactiva
                        return Request.CreateErrorResponse((HttpStatusCode)506, "La cuanta está inactiva");
                    }
                    //Asignar las variables de sesión que sean necesarias
                    Sesion sesion = new Sesion();

                    sesion.URL = HttpContext.Current.Request.Url.Host;
                    sesion.HoraInicio = DateTime.Now;
                    int verificador = 0;


                    //Datos de la empresa------------------------------------
                    sesion.Id_Emp = usuario.Id_Emp;
                    sesion.Emp_Cnx = Empresa.Emp_Cnx;
                    //Datos de la oficina------------------------------------
                    sesion.Id_Cd = usuario.Id_Cd;
                    sesion.Id_Cd_Ver = usuario.Id_Cd;
                    //Datos de la cuenta-------------------------------------
                    sesion.Cu_User = usuario.Cu_User;
                    sesion.Cu_Pass = usuario.Cu_pass;
                    sesion.Cu_Modif_Pass_Voluntario = !usuario.Cu_Caducada;
                    //Datos de configuración---------------------------------
                    sesion.Minutos = Minutos;
                    sesion.U_VerTodo = usuario.U_VerTodo;
                    sesion.U_MultiOfi = usuario.U_MultiCentro;
                    //Datos del usuario--------------------------------------
                    sesion.Id_U = usuario.Id_U;
                    sesion.Id_TU = usuario.Id_TU;
                    sesion.U_Nombre = usuario.U_Nombre;
                    sesion.U_Correo = usuario.U_Correo;
                    sesion.Dependientes = Dependientes;
                    sesion.CalendarioIni = usuario.CalendarioIni;
                    sesion.CalendarioFin = usuario.CalendarioFin;
                    sesion.Propia = usuario.cc_Propia;
                    sesion.Id_Rik = usuario.Id_Rik;
                    sesion.ProcSvtasAlm = usuario.ProcSvtasAlm;
                    sesion.ProcEmbAlm = usuario.ProcEmbAlm;
                    sesion.ProcEntAlm = usuario.ProcEntAlm;
                    sesion.ProcAlmCob = usuario.ProcAlmCob;
                    sesion.ProcRevCob = usuario.ProcRevCob;

                    sesion.Emp_Cnx_EF = StrCnxEF;

                    //Así se va a llamar en las pantalla
                    //Dim Sesion As New Sesion
                    CN_Empresa clsCatEmpresa = new CN_Empresa();
                    Empresa.Id_Emp = usuario.Id_Emp;
                    clsCatEmpresa.ConsultaEmpresas(ref Empresa, sesion.Emp_Cnx);
                    sesion.Emp_Nombre = Empresa.Emp_Nombre;

                    CN_CatCentroDistribucion centro = new CN_CatCentroDistribucion();
                    CentroDistribucion cd = new CentroDistribucion();
                    centro.ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    sesion.Cd_Nombre = cd.Cd_Descripcion;

                    CapaNegocios.CN_Menu ClsMenu = new CapaNegocios.CN_Menu();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ClsMenu.LlenarMenu(sesion.Emp_Cnx, ref dt, sesion.Id_Cd, sesion.Id_U);
                    HttpContext.Current.Session["DtMenu" + HttpContext.Current.Session.SessionID] = dt;

                    HttpContext.Current.Session.Add("Sesion" + HttpContext.Current.Session.SessionID, sesion);
                    //
                    HttpContext.Current.Session["FechaAgenda" + HttpContext.Current.Session.SessionID] = DateTime.Today.Date;

                    new CN_Rendimientos().InsertarRendimientosLogin(sesion, sesion.Emp_Cnx, HttpContext.Current.Session.SessionID, "LOGIN", ref verificador);

                }
                else
                {
                    return Request.CreateErrorResponse((HttpStatusCode)507, "La cuenta está bloqueada");
                }
            }
            else if (Id == 2)
            {
                return Request.CreateErrorResponse((HttpStatusCode)508, "Excedió el número de intentos para acceder al portal, la cuenta ha sido bloqueada");
            }
            else if (Id == 3)
            {
                return Request.CreateErrorResponse((HttpStatusCode)509, "El usuario o contraseña son incorrectos");
            }
            else
            {
                return Request.CreateErrorResponse((HttpStatusCode)510, "No se regresó información de la base de datos");
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}