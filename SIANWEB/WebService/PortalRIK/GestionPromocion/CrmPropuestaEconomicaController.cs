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
    public class CrmPropuestaEconomicaController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int idCte, int idVal)
        {
            CN_CrmPropuestaEconomica cnCrmPropuestaEconomica = new CN_CrmPropuestaEconomica();
            try
            {
                var result = cnCrmPropuestaEconomica.ObtenerPorValuacion(Sesion, idCte, idVal);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

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
    }
}