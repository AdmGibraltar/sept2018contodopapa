using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIANWEB.WebService;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http;
using CapaNegocios;
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService.Notificaciones
{
    public class CapRIKNotificacionProyectoController
         : BaseWebAPIController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string mensaje)
        {
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CapRIKNotificacion cnCapRIKNotificacion = new CN_CapRIKNotificacion();
                    var capRikNotificacion = cnCapRIKNotificacion.CrearNotificacionNuevoProyecto(Sesion, mensaje, ibt);
                    ibt.Commit();

                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, capRikNotificacion.CatNotificacion);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("CapRIKNotificacionController::Post", ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}