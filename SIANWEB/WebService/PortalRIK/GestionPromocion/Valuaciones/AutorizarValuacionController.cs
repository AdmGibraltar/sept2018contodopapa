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
using CapaNegocios.FlujosDeEstado.CRM;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Valuaciones
{
    public class AutorizarValuacionController
        : BaseWebAPIController
    {
        [HttpGet]
        public HttpResponseMessage Get(int idVal)
        {
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
                    cnCapValProyecto.Autorizar(Sesion, idVal, ibt);
                    //var valuacion = cnCapValProyecto.ObtenerPorId(Sesion, idVal, ibt);
                    //var valOp = valuacion.CrmValuacionOportunidades.Single();

                    //ProyectoStateMachine proyectoStateMachine = new ProyectoStateMachine(valOp.CrmOportunidade, Sesion);
                    //proyectoStateMachine.Transaction = ibt;

                    //CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
                    //var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(Sesion.Id_Emp, Sesion.Id_Cd, valOp.CrmOportunidade.Id_Op, valuacion.Id_Cte, ibt.DataContext);
                    ////NOTA TECNICA: puesto que el contexto de conexión a la fuente de datos sigue activo, no tiene caso recargar la lista de productos asociados al proyecto (ya que internamente la máquina de estados la utiliza en sus validaciones)
                    ////proyecto.CrmOportunidadesProducto = productos;

                    //proyectoStateMachine.Update();
                    ibt.Commit();
                }
                
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (CN_CapValProyecto.ValuacionInhabilitadaParaAutorizarException vipae)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, vipae);
            }
            catch (Exception ex)
            {
                //Excepción no esperada
                Logger.Error("Error no esperado al autorizar una valuación", ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}