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
    public class CatClienteDetPostArrayController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CatClienteDetPostAsArray data)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);

                var result = cnCatClienteDet.CrearNuevos(Sesion, data.Data);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

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

        /// <summary>
        /// Tipo utilizado para encapsular la lectura de un solo lote
        /// </summary>
        public class CatClienteDetPostAsArray
        {
            public CapaModelo.CatClienteDet[] Data
            {
                get;
                set;
            }
        }
    }
}