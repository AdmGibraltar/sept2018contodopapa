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
    public class CancelarProyectoController
        : BaseWebAPIController
    {
        [HttpPut]
        public Task<HttpResponseMessage> Put([FromBody] CancelarProyectoPutModel model)
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
                            cnCrmOportunidad.Cancelar(Sesion, model.IdCte, model.IdOp, model.IdCausa, ibt);
                            ibt.Commit();
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CancelarProyectoController::Get->inside task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("CancelarProyectoController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {

                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
        }
    }

    public class CancelarProyectoPutModel
    {
        public int IdCte
        {
            get;
            set;
        }

        public int IdOp
        {
            get;
            set;
        }

        public int IdCausa
        {
            get;
            set;
        }
    }
}