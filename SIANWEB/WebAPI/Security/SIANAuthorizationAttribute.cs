using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;

namespace SIANWEB.WebAPI.Security
{
    public class SIANAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.PathAndQuery.Contains("/api/Login"))
            {
                return true;
            }
            if (HttpContext.Current.Session != null)
            {
                return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID] != null;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            //string[] pag = actionContext.Request.RequestUri.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
            //HttpContext.Current.Session["dir" + HttpContext.Current.Session.SessionID] = pag[pag.Length - 1];
        }
    }
}