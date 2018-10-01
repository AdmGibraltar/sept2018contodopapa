using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using SIANWEB.WebAPI.Security;
using System.Web.Routing;
using System.Web.Http;
using System.Reflection;
using System.Web.Http.Dispatcher;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SIANWEB
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciarse la aplicación
            GlobalConfiguration.Configuration.Filters.Add(new SIANAuthorizationAttribute());
            var route = RouteTable.Routes.MapHttpRoute("SIANWEBApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            route.RouteHandler = new SIANWEB.WebAPI.Configuration.SIANHttpControllerRouteHandler();
            CheckDependencies();

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }

        protected void CheckDependencies()
        {
            IAssembliesResolver assembliesResolver = GlobalConfiguration.Configuration.Services.GetAssembliesResolver();

            ICollection<Assembly> assemblies = assembliesResolver.GetAssemblies();

            StringBuilder errorsBuilder = new StringBuilder();

            foreach (Assembly assembly in assemblies)
            {
                Type[] exportedTypes = null;
                if (assembly == null || assembly.IsDynamic)
                {
                    // can't call GetExportedTypes on a dynamic assembly
                    continue;
                }

                try
                {
                    exportedTypes = assembly.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    exportedTypes = ex.Types;
                }
                catch (Exception ex)
                {
                    errorsBuilder.AppendLine(ex.ToString());
                }
            }

            if (errorsBuilder.Length > 0)
            {
                //Log errors into Event Log
                Trace.TraceError(errorsBuilder.ToString());
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que se ejecuta cuando se cierra la aplicación

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Código que se ejecuta al producirse un error no controlado
            Trace.TraceError(e.ToString());
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando se inicia una nueva sesión

        }

        void Session_End(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando finaliza una sesión.
            // Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
            // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
            // o SQLServer, el evento no se genera.
            
        }

    }
}
