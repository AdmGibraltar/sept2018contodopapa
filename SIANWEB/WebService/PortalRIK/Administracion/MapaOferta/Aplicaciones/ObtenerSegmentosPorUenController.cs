using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using System.Threading.Tasks;
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService.PortalRIK.Administracion.MapaOferta.Aplicaciones
{
    public class ObtenerSegmentosPorUenController
        : BaseWebAPIController
    {
        [HttpGet]
        public Task<HttpResponseMessage> Get(int idUen)
        {
            try
            {
                var currentContext = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    try
                    {
                        HttpContext.Current = currentContext;
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            CN_CatSegmentos cnCatSegmentos = new CN_CatSegmentos();
                            var result = cnCatSegmentos.ObtenerSegmentosPorUen(idUen, Sesion, ibt).Select(seg => new
                            {
                                IdSegmento = seg.Id_Seg,
                                Descripcion = seg.Seg_Descripcion
                            });
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, new
                            {
                                succeeded = true,
                                data = result.ToList()
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("ObtenerSolucionesPorAreaController::Get->task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("ObtenerSolucionesPorAreaController::Get->task", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }

        }
    }
}