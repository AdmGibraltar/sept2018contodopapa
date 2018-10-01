using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;
using System.Net.Http;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmPropuestasController : ApiController
    {
        [HttpPut]
        public HttpResponseMessage Put([FromBody]CrmPropuestasController_PutData datos)
        {
            try
            {
                CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
                cnCrmPropuestaTecnica.ActualizarEdicionPropuesta(Sesion, datos.DatosPropuestaTecnica.Detalle);

                //Se necesita un contexto de control de persistencia para mantener la operación transaccional.
                CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
                cnCrmOportunidadesProductos.ActualizarEdicionPropuesta(Sesion, datos.DatosPropuestaEconomica.Detalle);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, new Exception("Se presentó una complicación al actualizar la información de la propuesta. Por favor, contacte al soporte del sistema."));
            }
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

        public class CrmPropuestasController_PutData
        {
            public CrmPropuestasController_PropuestaEconomicaPutData DatosPropuestaEconomica
            {
                get;
                set;
            }

            public CrmPropuestasController_PropuestaTecnicaPutData DatosPropuestaTecnica
            {
                get;
                set;
            }
        }

        public class CrmPropuestasController_PropuestaEconomicaPutData
        {
            public List<CrmOportunidadesProducto> Detalle
            {
                get;
                set;
            }
        }

        public class CrmPropuestasController_PropuestaTecnicaPutData
        {
            public List<CrmPropuestaTecnica> Detalle
            {
                get;
                set;
            }
        }
    }
}