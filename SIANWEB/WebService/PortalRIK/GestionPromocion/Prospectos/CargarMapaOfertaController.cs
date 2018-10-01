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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Prospectos
{
    public class CargarMapaOfertaController
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
                            CN_CatUen cnCatUen = new CN_CatUen();
                            var uens=cnCatUen.ObtenerUEnsDeEmpresa(Sesion, ibt);
                            var mapaOferta = from uen in uens
                                             select new EntradaMapaOferta()
                                             {
                                                 text=uen.Uen_Descripcion,
                                                 icon = "fa fa-industry",
                                                 nodes = ObtenerMapaOfertaUen(uen)
                                             };
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, new
                            {
                                succeeded=true,
                                data = mapaOferta.ToList()
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CargarMapaOfertaController::Get->inside task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }catch(Exception ex)
            {
                Logger.Error("CargarMapaOfertaController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
        }

        public IEnumerable<EntradaMapaOferta> ObtenerMapaOfertaUen(CatUEN cuen)
        {
            //List<EntradaMapaOferta> entradasSegmentos = new List<EntradaMapaOferta>();
            var entradasSegmentos = from seg in cuen.CatSegmentos
                                    select new EntradaMapaOferta()
                                    {
                                        text=seg.Seg_Descripcion,
                                        icon = "fa fa-database",
                                        nodes = ObtenerMapaOfertaSegmento(seg)
                                    };
            return entradasSegmentos.ToList();
        }

        public IEnumerable<EntradaMapaOferta> ObtenerMapaOfertaSegmento(CatSegmento cs)
        {
            var entradasAreas = from a in cs.CatAreas
                                select new EntradaMapaOferta()
                                {
                                    text=a.Area_Descripcion,
                                    icon = "fa fa-language",
                                    nodes = ObtenerMapaOfertaArea(a)
                                };
            return entradasAreas.ToList();
        }

        public IEnumerable<EntradaMapaOferta> ObtenerMapaOfertaArea(CatArea ca)
        {
            var entradasSoluciones = from sol in ca.CatSolucions
                                     select new EntradaMapaOferta()
                                     {
                                         text=sol.Sol_Descripcion,
                                         icon = "fa fa-refresh",
                                         nodes = ObtenerMapaOfertaSolucion(sol)
                                     };
            return entradasSoluciones.ToList();
        }

        public IEnumerable<EntradaMapaOferta> ObtenerMapaOfertaSolucion(CapaModelo.CatSolucion cs)
        {
            var entradasAplicaciones = from ap in cs.CatAplicacions
                                       select new EntradaAplicacionMapaOferta()
                                       {
                                           text=ap.Apl_Descripcion,
                                           icon = "fa fa-bolt",
                                           aplicacion=ap
                                       };
            return entradasAplicaciones.ToList();
        }

        public class EntradaMapaOferta
        {
            public string text
            {
                get;
                set;
            }

            public IEnumerable<EntradaMapaOferta> nodes
            {
                get;
                set;
            }

            public string icon
            {
                get;
                set;
            }
        }

        public class EntradaAplicacionMapaOferta
            : EntradaMapaOferta
        {
            public CapaModelo.CatAplicacion aplicacion
            {
                get;
                set;
            }
        }
    }
}