using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using CapaEntidad;

namespace SIANWEB.Core.Web.API
{
    public class BaseWebAPIController
        : ApiController
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

        protected log4net.ILog Logger
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }

        }

        /// <summary>
        /// Regresa la ubicación del recurso uniforme (URL) base de la aplicación.
        /// </summary>
        public String ApplicationUrl
        {
            get
            {
                return string.Format("{0}://{1}{2}", System.Web.HttpContext.Current.Request.Url.Scheme, System.Web.HttpContext.Current.Request.Url.Authority, System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/'));
            }
        }
    }
}