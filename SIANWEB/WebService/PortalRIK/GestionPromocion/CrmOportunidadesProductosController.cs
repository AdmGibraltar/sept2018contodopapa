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
using System.Threading.Tasks;
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    /// <summary>
    /// Controlador del repositorio CrmOportunidadesProductos
    /// </summary>
    public class CrmOportunidadesProductosController
        : BaseWebAPIController
    {
        /// <summary>
        /// Obtiene el listado de productos de un proyecto
        /// </summary>
        /// <param name="Id_CrmOportunidad">Identificador del proyecto</param>
        /// <param name="Id_Cte">Identificador del cliente</param>
        /// <returns>Task[HttpResponseMessage]</returns>
        [HttpGet]
        public Task<HttpResponseMessage> Get(int Id_CrmOportunidad, int Id_Cte)
        {
            //Se guarda el contexto de la llamada para capturarlos por referencia en las expresiones lambda.
            var currentContext = HttpContext.Current;
            var request = Request;
            try
            {
                //Se crea la tarea
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    //Se reestablece el contexto
                    HttpContext.Current = currentContext;
                    Request = request;
                    CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
                    try
                    {
                        using (var ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            var result = cnCrmOportunidadesProductos.ObtenerProductosPorOportunidad(Sesion, Id_CrmOportunidad, Id_Cte, ibt);
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                //Se manda a bitácora el evento de la excepción
                Logger.Error("CrmOportunidadesProductosController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() => 
                {
                    Request = request;
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex); 
                });
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]CrmOportunidadesProducto model)
        {
            CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
            model.Id_Emp = Sesion.Id_Emp;
            model.Id_Cd = Sesion.Id_Cd;
            model.Id_Rik = Sesion.Id_Rik;

            if (model.Id_Prd == 0)
            {                
                throw new Exception("Falta la clave de producto");
            }

            CrmOportunidadesProducto result = null;
            try
            {
                int iResult = 0;
                result = cnCrmOportunidadesProductos.Crear(Sesion, model, ref iResult);

                if (iResult == 3)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
                }

                if (iResult == 2)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.Conflict, result);
                }

                if (iResult == 1)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, result);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]CrmOportunidadesProducto model)
        {
            CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
            model.Id_Emp = Sesion.Id_Emp;
            model.Id_Cd = Sesion.Id_Cd;
            model.Id_Rik = Sesion.Id_Rik;
            try
            {
                cnCrmOportunidadesProductos.Actualizar(Sesion, model);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int idCte, int idOp, int idPrd)
        {
            try
            {
                CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
                cnCrmOportunidadesProductos.Eliminar(Sesion, idCte, idOp, idPrd);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }
    }
}