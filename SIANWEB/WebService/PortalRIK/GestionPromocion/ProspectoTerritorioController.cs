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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class ProspectoTerritorioController : ApiController
    {
        /// <summary>
        /// Devuelve el conjunto de territorios asociados con el prospecto idCrmProspecto.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idRik">Identificadr del representante</param>
        /// <param name="idCrmProspecto">Identificador del prospecto de interés</param>
        /// <returns>HttpResponseMessage, IEnumerable<CatTerritorio>. Conjunto de territorios asociados al prospecto idCrmProspecto.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int idEmp, int idCd, int idRik, int idCrmProspecto)
        {
            try
            {
                CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                var territorios = cnCatTerritorios.ObtenerTerritoriosPorProspecto(Sesion, idCrmProspecto);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, territorios);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Sesión de usuario del que llama.
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
    }
}