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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Proyectos
{
    public class BusquedaOtrosProductosCatalogoUnicoController
        : BaseWebAPIController
    {
        /// <summary>
        /// Regresa el conjunto de productos del catálogo único coincidentes con el identificador idPrd en el mapa de oferta para ese producto
        /// </summary>
        /// <param name="idPrd">Identificador del producto</param>
        /// <returns></returns>
        [HttpGet]
        public Task<HttpResponseMessage> Get(int idPrd)
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
                            CN_CrmCatalogoUnico cnCrmCatalogoUnico = new CN_CrmCatalogoUnico();
                            CN_CapAplicacionProducto cnCapAplicacionProducto = new CN_CapAplicacionProducto();
                            var productosCatalogoUnico = cnCapAplicacionProducto.ObtenerPorProducto(Sesion, idPrd, ibt);
                            //var productosCatalogoUnico = cnCrmCatalogoUnico.ObtenerPorProducto(Sesion, idPrd, ibt);

                            var response = from p in productosCatalogoUnico
                                           select new
                                           {
                                               Id_Prd=p.Id_Prd,
                                               Id_Apl=p.Id_Apl,
                                               Id_Sol=p.CatAplicacion.Id_Sol,
                                               Id_Area = p.CatAplicacion.CatSolucion.Id_Area,
                                               Id_Seg = p.CatAplicacion.CatSolucion.CatArea.Id_Seg,
                                               Id_Uen = p.CatAplicacion.CatSolucion.CatArea.CatSegmento.Id_Uen,
                                               Id_SubFam=p.AplProd_SubFamilia,
                                               NombreProducto=p.CatProducto.Prd_Descripcion,
                                               Ruta = string.Format("{0}/{1}/{2}/{3}", p.CatAplicacion.CatSolucion.CatArea.CatSegmento.CatUEN.Uen_Descripcion, p.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_Descripcion, p.CatAplicacion.CatSolucion.CatArea.Area_Descripcion, p.CatAplicacion.CatSolucion.Sol_Descripcion, p.CatAplicacion.Apl_Descripcion)
                                           };
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, response.ToList());
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("BusquedaOtrosProductosCatalogoUnicoController::Get->inside task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("BusquedaOtrosProductosCatalogoUnicoController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
        }
    }
}