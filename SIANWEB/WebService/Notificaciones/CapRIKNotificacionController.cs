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
    public class CapRIKNotificacionController : BaseWebAPIController
    {
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                CN_CapRIKNotificacion cnCapRIKNotificacion = new CN_CapRIKNotificacion();
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    cnCapRIKNotificacion.Eliminar(Sesion, id, ibt);
                    ibt.Commit();
                }
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Logger.Error("CapRIKNotificacionController::Delete", ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}