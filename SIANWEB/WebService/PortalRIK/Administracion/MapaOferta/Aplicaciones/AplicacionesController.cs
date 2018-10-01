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
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;

namespace SIANWEB.WebService.PortalRIK.Administracion.MapaOferta.Aplicaciones
{
    public class AplicacionesController
        : BaseWebAPIController
    {
        [HttpGet]
        public Task<HttpResponseMessage> Get()
        {
            try
            {
                HttpContext current = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    HttpContext.Current = current;
                    try
                    {
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
                            var aplicaciones = cnCatAplicacion.Obtener(Sesion, ibt).Select(ap => new
                            {
                                IdUen=ap.CatSolucion.CatArea.CatSegmento.Id_Uen,
                                IdSegmento=ap.CatSolucion.CatArea.Id_Seg,
                                IdArea=ap.CatSolucion.Id_Area,
                                IdSolucion=ap.Id_Sol,
                                IdAplicacion=ap.Id_Apl,
                                Nombre=ap.Apl_Descripcion,
                                Ruta = ap.CatSolucion.CatArea.CatSegmento.CatUEN.Uen_Descripcion + "/" + ap.CatSolucion.CatArea.CatSegmento.Seg_Descripcion + "/" + ap.CatSolucion.CatArea.Area_Descripcion + "/" + ap.CatSolucion.Sol_Descripcion
                            });
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, aplicaciones.ToList());
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("AplicacionesController::Get->task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("AplicacionesController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
        }
    }
}