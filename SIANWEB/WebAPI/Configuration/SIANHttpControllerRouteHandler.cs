using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;

namespace SIANWEB.WebAPI.Configuration
{
    public class SIANHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            return new SIANHttpControllerHandler(requestContext.RouteData);
        }
    }
}