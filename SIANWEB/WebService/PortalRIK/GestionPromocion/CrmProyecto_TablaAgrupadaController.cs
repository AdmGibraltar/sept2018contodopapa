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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmProyecto_TablaAgrupadaController : CrmProyectoController
    {
        /// <summary>
        /// Obtiene los proyectos asociados a un RIK mediante la sesión de ASP.Net.
        /// Esta versión difiere de CrmProyectoController.Get en que el resultado lo envía agrupado por cliente, con la finalidad de acomodar el resultado en una vista que agrupa los proyectos por cliente.
        /// </summary>
        /// <returns>Proyectos asociados al RIK</returns>
        [HttpGet]
        public override HttpResponseMessage Get()
        {
            List<CrmPromociones> resultado=new List<CrmPromociones>();
            if (base.Get().TryGetContentValue(out resultado))
            {
                CN_CapValuacionGlobalCliente cnCapValuacionGlobalCliente=new CN_CapValuacionGlobalCliente();
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    var agrupacion = (from cp in resultado
                                      group cp by new { Id_Cte = cp.Id_Cte, Cte_NomComercial = cp.NombreCte } into ctes
                                      select new
                                      {
                                          Id_Cte = ctes.Key.Id_Cte,
                                          NombreCliente = ctes.Key.Cte_NomComercial,
                                          Proyectos = ctes.Select(cp => cp).ToList(),
                                          Id_ValGlobal = 0,
                                          ValuacionGlobal = cnCapValuacionGlobalCliente.ObtenerPorCliente(Sesion, ctes.Key.Id_Cte, ibt).First().CapValProyecto, //siempre existe....siempre
                                      }).ToList().Select(a=>
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
            else
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, new Exception("Error al obtener los proyectos"));
            }
        }
    }
}