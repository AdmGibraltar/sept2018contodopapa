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
    public class CrmPropuestaTecnicaController : ApiController
    {
        [HttpPut]
        public HttpResponseMessage Put([FromBody]CrmPropuestaTecnicaController_PutData detalle)
        {
            try
            {
                CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
                cnCrmPropuestaTecnica.ActualizarEdicionPropuesta(Sesion, detalle.Detalle);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
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

        /// <summary>
        /// Clase que encapsula los datos de entrada del método Put con el fin de que el framework arme la entrada en una sola lectura
        /// </summary>
        public class CrmPropuestaTecnicaController_PutData
        {
            public List<CrmPropuestaTecnica> Detalle
            {
                get;
                set;
            }
        }
    }
}