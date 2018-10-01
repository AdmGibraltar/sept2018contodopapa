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
using Newtonsoft.Json;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Valuaciones
{
    public class ValuacionesRIKController : ApiController
    {
        /// <summary>
        /// Sesión del usuario en operación
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

        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                CN_CapValuacionProyecto cnCapValuacionProyecto = new CN_CapValuacionProyecto();
                var resultado = cnCapValuacionProyecto.ObtenerPorRik(Sesion);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, new Exception("Ha ocurrido una complicación al obtener las valuaciones del representante. Por favor, comuníquese con el administrador del sitio para sobrellevar esta inconveniencia."));
            }
        }
    }
}