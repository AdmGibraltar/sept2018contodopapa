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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Proyectos
{
    public class OfertaPromocionController
        : BaseWebAPIController
    {
        [HttpGet]
        public HttpResponseMessage Get(int idCte, int idOp)
        {
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
                    var opAps = cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(Sesion, idCte, idOp, ibt);
                    CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                    var op = cnCrmOportunidad.ObtenerPorId(Sesion, idOp, ibt);
                    //Se valida si el proyecto tiene asociado otros productos
                    if (op.Id_Apl == -1)
                    {
                        var oferta = new { 
                               Id_SubFam = -1, 
                               Id_Apl = -1, 
                               Id_Sol = -1, 
                               Id_Area = -1, 
                               Id_Seg = op.CatTerritorio.CatSegmento.Id_Seg, 
                               Id_Uen = op.CatTerritorio.CatSegmento.Id_Uen 
                        };
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, oferta);
                    }
                    else
                    {
                        var opAp = opAps.First();
                        var oferta = new { 
                            Id_SubFam = 0, 
                            Id_Apl = opAp.Id_Apl, 
                            Id_Sol = opAp.CatAplicacion.Id_Sol, 
                            Id_Area = opAp.CatAplicacion.CatSolucion.Id_Area, 
                            Id_Seg = opAp.CatAplicacion.CatSolucion.CatArea.Id_Seg, 
                            Id_Uen = opAp.CatAplicacion.CatSolucion.CatArea.CatSegmento.Id_Uen 
                        };
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, oferta);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Se generó un error al obtener la aplicación asociada con el proyecto", ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "Se generó un error al obtener la aplicación asociada con el proyecto", ex);
            }
        }
    }
}