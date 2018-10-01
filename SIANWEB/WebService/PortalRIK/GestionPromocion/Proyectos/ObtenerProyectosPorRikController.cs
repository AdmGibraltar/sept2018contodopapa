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
using CapaEntidad;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Proyectos
{
    public class ObtenerProyectosPorRikController
        : BaseWebAPIController
    {
        [HttpGet]
        public Task<HttpResponseMessage> Get(int IdRik)
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
                            CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                            CN_CrmValuacionOportunidades cnCrmValuacionOportunidades = new CN_CrmValuacionOportunidades();
                            var proyectos = cnCrmOportunidad.ObtenerSoloCRMII_PorRik(IdRik, Sesion, ibt).Select(p =>
                            {
                                var promocion = new CrmPromociones()
                                {
                                    Ids = p.Id_Op,
                                    Id = p.Id_Op,
                                    Id_Cte = p.Id_Cte.Value,
                                    Cds = p.Id_Cd,
                                    Id_Apl = p.Id_Apl.Value, // RFH 
                                    Id_Area = p.ID_Area.Value,
                                    Id_Sol = p.Id_Sol.Value,
                                    Id_Seg = p.Id_Seg.Value,
                                    IdUen = p.Id_Uen.Value,
                                    Representante = p.Id_Usu.Value,
                                    NombreCte = p.CatCliente.Cte_NomComercial,
                                    Id_Ter = p.Id_Ter.Value,
                                    Segmento = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Id_Seg.Value : p.Id_Seg.Value,
                                    Cli_VPObservado = Convert.ToDouble(p.MontoProyecto.Value),
                                    Descripcion = p.CrmOportunidadesAplicacion != null ? string.Format("{0}/{1}/{2}/{3}/{4}", p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.CatUEN.Uen_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Area_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.Sol_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.Apl_Descripcion) : string.Empty,
                                    Analisis = p.Analisis != null ? p.Analisis.Value.ToShortDateString() : string.Empty,
                                    Presentacion = p.Presentacion != null ? p.Presentacion.Value.ToShortDateString() : string.Empty,
                                    Negociacion = p.Negociacion != null ? p.Negociacion.Value.ToShortDateString() : string.Empty,
                                    Cierre = p.Cierre != null ? p.Cierre.Value.ToShortDateString() : string.Empty,
                                    Cancelacion = p.Cancelacion,
                                    FechaCancelacion = p.FechaCancelacion != null ? p.FechaCancelacion.Value.ToShortDateString() : string.Empty,
                                    Avances = p.Avances != null ? p.Avances.Value : 0,
                                    Estatus = p.Estatus != null ? p.Estatus.Value : 0,
                                    VentaMensual = p.VentaMensual != null ? Convert.ToDouble(p.VentaMensual.Value) : 0.0D,
                                    Id_CrmProspecto = p.Id_CrmProspecto != null ? p.Id_CrmProspecto.Value : 0,
                                    VentaNoRepetitiva = p.VentaNoRepetitiva,
                                    Id_Uen = p.Id_Uen.Value,
                                    EnValuacion = p.CrmOp_EnValuacion,
                                    Cliente = p.Id_Cte.Value,
                                    Area = p.ID_Area.Value,
                                    Solucion = p.Id_Sol.Value,
                                    Seg_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_Descripcion : string.Empty,
                                    Area_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Area_Descripcion : string.Empty,
                                    Sol_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.Sol_Descripcion : string.Empty,
                                    Dim_Id_Uen = p.Dim_Id_Uen,
                                    Dim_Cantidad = p.Dim_Cantidad,
                                    Dim_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_Unidades : string.Empty,
                                    ValorPotencialTeorico = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_ValUniDim.Value * Convert.ToDouble(p.Dim_Cantidad != null ? p.Dim_Cantidad.Value : 0) : 0,
                                    VentaPromedioMensualEsperada = p.CrmOp_VPM != null ? p.CrmOp_VPM.Value : 0,
                                    Uen_Descrip = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.CatUEN.Uen_Descripcion : string.Empty,
                                    Ter_Nombre = p.CatTerritorio != null ? p.CatTerritorio.Ter_Nombre : string.Empty,
                                    CrmValuacionOportunidades = cnCrmValuacionOportunidades.ObtenerPorProyecto(Sesion, p.Id_Cte.Value, p.Id_Op, ibt)

                                };

                                // RFH 14 May 2018
                                // Este cambio por lo pronto no ya que 
                                // el calculo se hace sobre las listas en memoria 	
                                // ya que se verifique como va a funcionar se aplicara el SP.
                                // 0.- Solo consulta
                                // 1.- Metodo Default 
                                // 2.- Metodo SP
                                // 

                                switch (1)
                                {
                                    case 0:
                                        CN_CrmOportunidad CD_co = new CN_CrmOportunidad();
                                        CapaEntidad.eCapValProyecto Obj = new CapaEntidad.eCapValProyecto();

                                        //Obj = CD_co.Consulta_ResultadoValuacion(promocion.Id_Cte, promocion.Id, Sesion, ibt);
                                        //promocion.ValorPresenteNeto = Obj.Vap_ValorPresenteNeto;
                                        //promocion.UtilidadRemanente = Obj.Vap_UtilidadRemanente;

                                        //promocion.UtilidadRemanente = promocion.

                                        break;
                                    case 1:
                                        CN_CrmOportunidad.ResultadosValuacion resultadosValuacion = null;
                                        try
                                        {
                                            resultadosValuacion = cnCrmOportunidad.CalcularResultadoValuacion(promocion, Sesion, ibt);
                                            promocion.ValorPresenteNeto = resultadosValuacion.ValorPresenteNeto;
                                            promocion.UtilidadRemanente = resultadosValuacion.UtilidadRemanente;
                                        }
                                        catch (CN_CrmOportunidad.CapValProyecto_ParametrosIndefinidosException ex1)
                                        {
                                            promocion.ValorPresenteNeto = null;
                                            promocion.UtilidadRemanente = null;
                                        }
                                        catch (CN_CrmOportunidad.CapValProyecto_ParamsIndefinidosException ex2)
                                        {
                                            promocion.ValorPresenteNeto = null;
                                            promocion.UtilidadRemanente = null;
                                        }
                                        catch (CN_CrmOportunidad.ProyectoNoAsociadoAValuacionException ex)
                                        {
                                            promocion.ValorPresenteNeto = null;
                                            promocion.UtilidadRemanente = null;
                                        }
                                        break;
                                    case 2:
                                        CapaEntidad.eResultadoValuacion eRV = new CapaEntidad.eResultadoValuacion();
                                        CN_CrmOportunidad cnCRMO = new CN_CrmOportunidad();
                                        eRV = cnCRMO.Calcular_ResultadoValuacion(Sesion.Id_Emp, Sesion.Id_Cd, promocion.Id, Sesion);
                                        promocion.ValorPresenteNeto = (decimal)eRV.ValorPresenteNeto;
                                        promocion.UtilidadRemanente = (decimal)eRV.UtilidadRemanente;
                                        break;
                                }

                                return promocion;
                            }).ToList();
                            CN_CapValuacionGlobalCliente cnCapValuacionGlobalCliente = new CN_CapValuacionGlobalCliente();
                            var agrupacion = (from cp in proyectos
                                              group cp by new { Id_Cte = cp.Id_Cte, Cte_NomComercial = cp.NombreCte } into ctes
                                              select new
                                              {
                                                  Id_Cte = ctes.Key.Id_Cte,
                                                  NombreCliente = ctes.Key.Cte_NomComercial,
                                                  Proyectos = ctes.Select(cp => cp).ToList(),
                                                  Id_ValGlobal = 0,
                                                  ValuacionGlobal = cnCapValuacionGlobalCliente.ObtenerPorCliente(Sesion, ctes.Key.Id_Cte, ibt).First().CapValProyecto, //siempre existe....siempre
                                              }).ToList().Select(a =>
                                              {
                                                  return new
                                                  {
                                                      Id_Cte = a.Id_Cte,
                                                      NombreCliente = a.NombreCliente,
                                                      Proyectos = a.Proyectos,
                                                      Id_ValGlobal = 0,
                                                      ValuacionGlobal = a.ValuacionGlobal,
                                                      ProyectosEnValuacion = cnCapValuacionGlobalCliente.ObtenerProyectosAsociados(Sesion, a.ValuacionGlobal.Id_Vap, a.ValuacionGlobal.Id_Cte, ibt).ToList()
                                                  };
                                              }).ToList();
                            //Se envían los cambios al servidor en caso de que haya habido alguno.
                            ibt.Commit();

                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, agrupacion);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("ObtenerProyectosPorRikController::Get->inside task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("ObtenerProyectosPorRikController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
        }

        /// <summary>
        /// Obtiene los proyectos asociados a un RIK mediante la sesión de ASP.Net.
        /// </summary>
        /// <returns>Proyectos asociados al RIK</returns>
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
                            CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                            CN_CrmValuacionOportunidades cnCrmValuacionOportunidades = new CN_CrmValuacionOportunidades();
                            var proyectos = cnCrmOportunidad.ObtenerSoloCRMIIPorRik(Sesion, ibt).Select(p => {
                                var promocion = new CrmPromociones()
                                {
                                    Ids = p.Id_Op,
                                    Id = p.Id_Op,
                                    Id_Cte = p.Id_Cte.Value,
                                    Cds = p.Id_Cd,                                    
                                    Id_Apl = p.Id_Apl.Value , // RFH 
                                    Id_Area= p.ID_Area.Value ,
                                    Id_Sol=p.Id_Sol.Value ,
                                    Id_Seg=p.Id_Seg.Value ,
                                    IdUen=p.Id_Uen.Value ,
                                    Representante = p.Id_Usu.Value,
                                    NombreCte = p.CatCliente.Cte_NomComercial,
                                    Id_Ter = p.Id_Ter.Value,
                                    Segmento = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Id_Seg.Value : p.Id_Seg.Value ,
                                    Cli_VPObservado = Convert.ToDouble(p.MontoProyecto.Value),
                                    Descripcion = p.CrmOportunidadesAplicacion != null ? string.Format("{0}/{1}/{2}/{3}/{4}", p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.CatUEN.Uen_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Area_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.Sol_Descripcion, p.CrmOportunidadesAplicacion.CatAplicacion.Apl_Descripcion) : string.Empty,
                                    Analisis = p.Analisis != null ? p.Analisis.Value.ToShortDateString() : string.Empty,
                                    Presentacion = p.Presentacion != null ? p.Presentacion.Value.ToShortDateString() : string.Empty,
                                    Negociacion = p.Negociacion != null ? p.Negociacion.Value.ToShortDateString() : string.Empty,
                                    Cierre = p.Cierre != null ? p.Cierre.Value.ToShortDateString() : string.Empty,
                                    Cancelacion = p.Cancelacion,
                                    FechaCancelacion = p.FechaCancelacion != null ? p.FechaCancelacion.Value.ToShortDateString() : string.Empty,
                                    Avances = p.Avances != null ? p.Avances.Value : 0,
                                    Estatus = p.Estatus != null ? p.Estatus.Value : 0,
                                    VentaMensual = p.VentaMensual != null ? Convert.ToDouble(p.VentaMensual.Value) : 0.0D,
                                    Id_CrmProspecto = p.Id_CrmProspecto != null ? p.Id_CrmProspecto.Value : 0,
                                    VentaNoRepetitiva = p.VentaNoRepetitiva,
                                    Id_Uen = p.Id_Uen.Value,
                                    EnValuacion = p.CrmOp_EnValuacion,
                                    Cliente = p.Id_Cte.Value,
                                    Area = p.ID_Area.Value,
                                    Solucion = p.Id_Sol.Value,
                                    Seg_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_Descripcion : string.Empty,
                                    Area_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Area_Descripcion : string.Empty,
                                    Sol_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.Sol_Descripcion : string.Empty,
                                    Dim_Id_Uen = p.Dim_Id_Uen,
                                    Dim_Cantidad = p.Dim_Cantidad,
                                    Dim_Descripcion = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_Unidades : string.Empty,
                                    ValorPotencialTeorico = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.Seg_ValUniDim.Value * Convert.ToDouble(p.Dim_Cantidad!=null ? p.Dim_Cantidad.Value : 0) : 0,
                                    VentaPromedioMensualEsperada = p.CrmOp_VPM != null ? p.CrmOp_VPM.Value : 0,
                                    Uen_Descrip = p.CrmOportunidadesAplicacion != null ? p.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.CatSegmento.CatUEN.Uen_Descripcion : string.Empty,
                                    Ter_Nombre = p.CatTerritorio != null ? p.CatTerritorio.Ter_Nombre : string.Empty,
                                    CrmValuacionOportunidades = cnCrmValuacionOportunidades.ObtenerPorProyecto(Sesion, p.Id_Cte.Value, p.Id_Op, ibt)
                                    
                                };

								// RFH 14 May 2018
                                // Este cambio por lo pronto no ya que 
								// el calculo se hace sobre las listas en memoria 	
								// ya que se verifique como va a funcionar se aplicara el SP.
                                // 0.- Solo consulta
                                // 1.- Metodo Default 
                                // 2.- Metodo SP
                                // 

                                switch (1)
                                {
                                    case 0:
                                        CN_CrmOportunidad CD_co = new CN_CrmOportunidad();
                                        CapaEntidad.eCapValProyecto Obj = new CapaEntidad.eCapValProyecto();

                                        //Obj = CD_co.Consulta_ResultadoValuacion(promocion.Id_Cte, promocion.Id, Sesion, ibt);
                                        //promocion.ValorPresenteNeto = Obj.Vap_ValorPresenteNeto;
                                        //promocion.UtilidadRemanente = Obj.Vap_UtilidadRemanente;

                                        //promocion.UtilidadRemanente = promocion.
                                        
                                        break;
                                    case 1:                                        
                                        CN_CrmOportunidad.ResultadosValuacion resultadosValuacion = null;
                                        try
                                        {
                                            resultadosValuacion = cnCrmOportunidad.CalcularResultadoValuacion(promocion, Sesion, ibt);
                                            promocion.ValorPresenteNeto = resultadosValuacion.ValorPresenteNeto;
                                            promocion.UtilidadRemanente = resultadosValuacion.UtilidadRemanente;
                                        }
                                        catch (CN_CrmOportunidad.CapValProyecto_ParametrosIndefinidosException ex1)
                                        {
                                            promocion.ValorPresenteNeto = null;
                                            promocion.UtilidadRemanente = null;
                                        }
                                        catch (CN_CrmOportunidad.CapValProyecto_ParamsIndefinidosException ex2)
                                        {
                                            promocion.ValorPresenteNeto = null;
                                            promocion.UtilidadRemanente = null;
                                        }
                                        catch (CN_CrmOportunidad.ProyectoNoAsociadoAValuacionException ex)
                                        {
                                            promocion.ValorPresenteNeto = null;
                                            promocion.UtilidadRemanente = null;
                                        }
                                        break;
                                    case 2:
                                        CapaEntidad.eResultadoValuacion eRV = new CapaEntidad.eResultadoValuacion();
                                        CN_CrmOportunidad cnCRMO = new CN_CrmOportunidad ();
                                        eRV = cnCRMO.Calcular_ResultadoValuacion(Sesion.Id_Emp,Sesion.Id_Cd,promocion.Id,Sesion);
                                        promocion.ValorPresenteNeto = (decimal)eRV.ValorPresenteNeto;
                                        promocion.UtilidadRemanente = (decimal)eRV.UtilidadRemanente;                                        
                                        break;                                
                                }

                                return promocion;
                            }).ToList();
                            CN_CapValuacionGlobalCliente cnCapValuacionGlobalCliente = new CN_CapValuacionGlobalCliente();
                            var agrupacion = (from cp in proyectos
                                              group cp by new { Id_Cte = cp.Id_Cte, Cte_NomComercial = cp.NombreCte } into ctes
                                              select new
                                              {
                                                  Id_Cte = ctes.Key.Id_Cte,
                                                  NombreCliente = ctes.Key.Cte_NomComercial,
                                                  Proyectos = ctes.Select(cp => cp).ToList(),
                                                  Id_ValGlobal = 0,
                                                  ValuacionGlobal = cnCapValuacionGlobalCliente.ObtenerPorCliente(Sesion, ctes.Key.Id_Cte, ibt).First().CapValProyecto, //siempre existe....siempre
                                              }).ToList().Select(a =>
                                              {
                                                  return new
                                                  {
                                                      Id_Cte = a.Id_Cte,
                                                      NombreCliente = a.NombreCliente,
                                                      Proyectos = a.Proyectos,
                                                      Id_ValGlobal = 0,
                                                      ValuacionGlobal = a.ValuacionGlobal,
                                                      ProyectosEnValuacion = cnCapValuacionGlobalCliente.ObtenerProyectosAsociados(Sesion, a.ValuacionGlobal.Id_Vap, a.ValuacionGlobal.Id_Cte, ibt).ToList()
                                                  };
                                              }).ToList();
                            //Se envían los cambios al servidor en caso de que haya habido alguno.
                            ibt.Commit();

                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, agrupacion);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("ObtenerProyectosPorRikController::Get->inside task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("ObtenerProyectosPorRikController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
        }

        //
    }
}