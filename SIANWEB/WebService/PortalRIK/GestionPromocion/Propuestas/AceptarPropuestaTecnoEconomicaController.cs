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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Propuestas
{
    public class AceptarPropuestaTecnoEconomicaController : ApiController
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

        /// <summary>
        /// Como todo método en este controlador, acepta la propuesta tecnoeconómica asociada a la valuación especificada en los parámetros de cada acción.
        /// </summary>
        /// <param name="idVal">Identificador de la valuación asociada a la propuesta que desea aceptarse.</param>
        /// <returns>En caso de éxito, regresa OK; InternalServerFailure en caso contrario con una descripción en el cuerpo.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int idVal)
        {
            try
            {
                CN_CrmPropuestaTecnoEconomica cnCrmPropuestaTecnoEconomica = new CN_CrmPropuestaTecnoEconomica();
                var acys = cnCrmPropuestaTecnoEconomica.Aceptar(Sesion, idVal);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, acys.Id_Acs);
            }
            catch (Exception ex)
            {
                Logger.Debug("Generación de propuesta tecno-economica", ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        protected log4net.ILog Logger
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }
    }
}