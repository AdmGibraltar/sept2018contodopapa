using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using CapaModelo;
using SIANWEB.WebAPI.Models;
using System.Net.Http;

namespace SIANWEB.WebService.PortalRIK
{
    public class CatClienteDetContactoController : ApiController
    {
        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }

         /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]WebAPI.Models.Post.CatClienteDetContacto data)
        {
            try
            {
                //Sesion session = new Sesion();
                //session = (Sesion)Session["Sesion" + Session.SessionID];               
                //CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                //var result = cnCatClienteDet.CrearNuevo(Sesion, data.IdCte, data.IdRik, data.IdTer, data.IdSeg, data.VPO);

                CapaEntidad.ClienteDetContacto Contacto = new CapaEntidad.ClienteDetContacto();

                Contacto.Id_Emp= data.Id_Emp;
                Contacto.Id_Cd= data.Id_Cd ;
                Contacto.Id_Cte = data.Id_Cte;
                Contacto.Id_CteDet = data.Id_CteDet;
                Contacto.Id_Ter = data.Id_Ter;
                Contacto.Id_Seg = data.Id_Seg;
                Contacto.Nombre = data.Nombre;
                Contacto.Puesto = data.Puesto;
                data.Cumpleanios = data.Cumpleanios.Replace("-", "/");
                Contacto.Cumpleanios = data.Cumpleanios;
                Contacto.Correo = data.Correo;
                Contacto.Direccion1 = data.Direccion1;
                Contacto.Direccion2 = data.Direccion2;
                Contacto.TelNegocio = data.TelNegocio;
                Contacto.TelCasa = data.TelCasa;
                Contacto.Id_Consecutivo = data.Id_Consecutivo;                                
                int iRes =0;
                int verificador = -1;

                CN_CatClienteDetContacto cnCatClienteDetContacto = new CN_CatClienteDetContacto(Sesion);
                cnCatClienteDetContacto.InsertarCR(Contacto, Sesion.Emp_Cnx, ref verificador);
                                                
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, iRes);
                if (verificador == 1)
                {
                    iRes = 1;                     
                    //Alerta("Los datos se guardaron correctamente");                    
                }
                else
                {
                    iRes = 0;                     
                    //Alerta("La clave ya existe");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
        
        /// <summary>
        /// Obtiene el lista do de 
        /// </summary>
        /// <param name="">String. Término de búsqueda.</param>
        /// <returns>List<Clientes> - Listado.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int IdEmp,int IdCd, int IdCte, int IdTer)
        {
            try
            {
                CN_CatClienteDetContacto cnCatClienteDetContacto = new CN_CatClienteDetContacto(Sesion);
                CapaEntidad.ClienteDetContacto pars = new CapaEntidad.ClienteDetContacto();

                List<CapaEntidad.ClienteDetContacto> result = new List<CapaEntidad.ClienteDetContacto>();

                pars.Id_Emp = IdEmp;
                pars.Id_Cd = IdCd;
                pars.Id_Cte = IdCte;
                pars.Id_Ter = IdTer;

                cnCatClienteDetContacto.Consulta(pars , Sesion.Emp_Cnx, ref result);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Manejar los casos adecuadamente
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEmp"></param>
        /// <param name="IdCd"></param>
        /// <param name="IdCte"></param>
        /// <param name="IdTer"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(int IdEmp, int IdCd, int IdCte, int IdTer, int IdConsecutivo)
        {
            try
            {
                CN_CatClienteDetContacto cnCatClienteDetContacto = new CN_CatClienteDetContacto(Sesion);
                CapaEntidad.ClienteDetContacto pars = new CapaEntidad.ClienteDetContacto();

                CapaEntidad.ClienteDetContacto result = new CapaEntidad.ClienteDetContacto();

                pars.Id_Emp = IdEmp;
                pars.Id_Cd = IdCd;
                pars.Id_Cte = IdCte;
                pars.Id_Ter = IdTer;
                pars.Id_Consecutivo = IdConsecutivo;

                cnCatClienteDetContacto.ConsultaPorId(pars, Sesion.Emp_Cnx, ref result);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Manejar los casos adecuadamente
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

           
    }
}