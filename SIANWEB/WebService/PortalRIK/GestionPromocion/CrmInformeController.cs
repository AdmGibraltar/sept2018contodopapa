using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;
using CapaDatos;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmInformeController : ApiController
    {

        [HttpGet]
        public List<CapaEntidad.Informe1> Get(int TipoReporte,int Zona,int Representante, string Periodo, string Monto1, string Monto2)
        {
            List<CapaEntidad.Informe1> lst = new List<CapaEntidad.Informe1>();
            CN_Informe1 Inf = new CN_Informe1();
            Inf.spCRM_ControlPromocion_LimpiezaAplicacion2(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, Representante, Periodo, 0, Monto1, Monto2, false, ref lst, Sesion.Emp_Cnx);            

            return lst;
        }

        [HttpGet]
        public List<CapaEntidad.Informe2> Get(int TipoReporte, int Zona, int Representante, string Periodo)
        {
            List<CapaEntidad.Informe2> lst = new List<CapaEntidad.Informe2>();
            CN_Informe1 Inf = new CN_Informe1();
            Inf.spCRM_ControlEntrada(Sesion.Id_Emp, Sesion.Id_Cd, Representante, Periodo, ref lst, Sesion.Emp_Cnx);

            return lst;
        }

        [HttpGet]
        public HttpResponseMessage Get(int Zona, string Periodo)
        {
            List<CapaEntidad.Informe3> lst = new List<CapaEntidad.Informe3>();
            try
            {
                CN_Informe1 Inf = new CN_Informe1();
                Inf.spCRM_ControlPromocion_DIINumero(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, Periodo, ref lst, Sesion.Emp_Cnx);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, lst);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }            
            //return lst;
        }

        /// <summary>
        /// Sesión del usuario actual
        /// </summary>
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
        //
    }
}