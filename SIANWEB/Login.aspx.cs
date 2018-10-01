using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Configuration;
using System.Net.Mail;
using CapaNegocios;
using System.Text;
using System.Net;
using System.Net.Mime;
using SIANWEB.Configuracion;

namespace SIANWEB
{
    public partial class Login : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    if (Session["Sesion" + Session.SessionID] != null)
                    {
                        int salir = 0;
                        salir = Convert.ToInt32(Request.QueryString["id"]);
                        if (salir != 1 && salir != 2)
                        {
                            Response.Redirect("inicio.aspx");
                            return;
                        }
                        else
                        {
                            Session.Abandon();
                            Session.Clear();
                            Session.RemoveAll();
                        }
                        if (salir == 2)
                        {
                            this.Alerta("La sesión ha caducado");
                        }
                    }
                    else
                    {
                        Usuario usuario = null;
                        if (Session["SIANKEY"] != null)
                        {
                            usuario = (Usuario)Session["SIANKEY"];
                            Entrar(usuario);
                            return;
                        }
                    }
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtUserName.Text == "" || txtPassword.Text == "")
                {
                    return;
                }
                ErrorManager();
                Usuario usuario = new Usuario();
                usuario.Cu_User = this.txtUserName.Text;
                usuario.Cu_pass = this.txtPassword.Text;

                Entrar(usuario);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        private void Entrar(Usuario usuario)
        {
            string StrCnx = ConfigurationManager.AppSettings.Get("strConnection");
            string StrCnxEF = ConfigurationManager.AppSettings.Get("strConnectionEF");
            string StrCnxSIANCentralEF = ConfigurationManager.AppSettings.Get("strConnectionSIANCentralEF");
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
                        Alerta("La cuenta está inactiva");
                        return;
                    }
                    //Asignar las variables de sesión que sean necesarias
                    Sesion sesion = new Sesion();
                    foreach (ConexionSIANWeb c in SeccionSIANCentralConfig.Conexiones)
                    {
                        sesion.ConexionesSIANWeb.Add(c.Nombre, c.ConexionEF);
                    }
                    sesion.URL = HttpContext.Current.Request.Url.Host;
                    sesion.HoraInicio = DateTime.Now;
                    sesion.SIANCentralEF = StrCnxSIANCentralEF;
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
                    Session["DtMenu" + Session.SessionID] = dt;

                    Session.Add("Sesion" + Session.SessionID, sesion);
                    //
                    Session["FechaAgenda" + Session.SessionID] = DateTime.Today.Date;

                    string destino;
                    if (Session["dir" + Session.SessionID] != null)
                    {

                        destino = Session["dir" + Session.SessionID].ToString();
                    }
                    else
                    {
                        //Edsg03072017
                        if (Application["dir_Remisiones"] != null)
                            destino = Application["dir_Remisiones"].ToString();
                        else
                            destino = "inicio.aspx";
                    }

                    Session["dir" + Session.SessionID] = null;
                    //Edsg03072017
                    Application["dir_Remisiones"] = null;


                    Response.Redirect(destino, false);

                    new CN_Rendimientos().InsertarRendimientosLogin(sesion, sesion.Emp_Cnx, Session.SessionID, "LOGIN", ref verificador);

                }
                else
                {
                    Alerta("La cuenta está bloqueada");
                }
            }
            else if (Id == 2)
            {
                Alerta("Excedió el número de intentos para acceder al portal, la cuenta ha sido bloqueada");
            }
            else if (Id == 3)
            {
                AlertaFocus("El usuario o contraseña son incorrectos", txtPassword.ClientID);
            }
            else
            {
                Alerta("No se regresó información de la base de datos");
            }
        }
        protected void LinkRecuperaPassword_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (txtUserName.Text == "")
                {
                    RequiredFieldValidatorUserNameLogin.IsValid = false;
                    return;
                }
                RadConfirm("¿Desea recuperar su contraseña?");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            RequiredFieldValidatorUserNameLogin.IsValid = true;
            txtPassword.Focus();
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string StrCnx = ConfigurationManager.AppSettings.Get("strConnection");
                Int32 Id = default(Int32);

                ConfiguracionGlobal Configuracion = new ConfiguracionGlobal();
                Empresa Empresa = new Empresa();

                CentroDistribucion Cdis = new CentroDistribucion();
                Usuario Usuario = new Usuario();

                Usuario.Cu_User = this.txtUserName.Text;

                CapaNegocios.CN_Login CN_Login = new CapaNegocios.CN_Login();

                CN_Login.RecuperarContraseña(ref Usuario, ref Cdis, ref Configuracion, out Id, ConfigurationManager.AppSettings.Get("strConnection"));
                //Datos correctos
                if (Id == 1)
                {
                    if (Usuario.U_Correo != string.Empty)
                    {
                        EnviaEmail(Usuario, Cdis, Configuracion);
                        Alerta("La contraseña ha sido enviada por e-mail al correo que está registrado en la cuenta");
                    }
                    else
                    {
                        Alerta("No hay una cuenta de correo asociada para la cuenta, favor de comunicarse con el administrador");
                    }
                }
                else if (Id == 2)
                {
                    Alerta("La cuenta no existe");
                }
                else
                {
                    Alerta("No se regresó información de la base de datos");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void EnviaEmail(Usuario Usuario, CentroDistribucion Cdis, ConfiguracionGlobal Configuracion)
        {
            try
            {
                if (Usuario.U_Correo == string.Empty | Usuario.U_Correo == "&nbsp;")
                {
                    Alerta("Usted no posee un correo electrónico asociado a su usuario que pueda ser usado como remitente en el correo");
                    return;
                }

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append(" <table>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td><img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("   <td valign='middle' style='text-decoration: underline'><b><font face= 'Tahoma' size = '4'>Recuperación de contraseña</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><b><font face= 'Tahoma' size = '2'>Ha solicitado recordarle los datos de su cuenta de acceso, anexo se encuentra su contraseña</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align='right'><b><font face= 'Tahoma' size = '2'>Contraseña:</font></b></td>");
                cuerpo_correo.Append("   <td align='left'><b><font face= 'Tahoma' size = '2' color='#777777'>" + Usuario.Cu_pass);
                cuerpo_correo.Append("</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align ='center' colspan='2'><b><font face= 'Tahoma' size = '2'></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_Descripcion);
                cuerpo_correo.Append("</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_Tel);
                cuerpo_correo.Append("</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'></font></b>");
                cuerpo_correo.Append("   </td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td align ='Left' colspan='2'><b><font face= 'Tahoma' size = '2'>" + Cdis.Cd_CalleNo);
                cuerpo_correo.Append("</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append(" </table>");
                cuerpo_correo.Append("</div>");

                SmtpClient sm = new SmtpClient(Configuracion.Mail_Servidor, Convert.ToInt32(Configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(Configuracion.Mail_Usuario, Configuracion.Mail_Contraseña);
                MailMessage m = new MailMessage();
                m.From = new MailAddress(Configuracion.Mail_Remitente);

                string To = Usuario.U_Correo;
                m.To.Add(new MailAddress(To));
                m.Subject = "Contraseña recuperada";
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                m.AlternateViews.Add(vistaHtml);
                sm.Send(m);
            }
            catch (Exception)
            {
                Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
            }
        }
        #endregion
        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                this.LblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                this.LblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.LblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.LblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}
