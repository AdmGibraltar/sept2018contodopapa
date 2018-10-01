using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;
using System.Net.Http;
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService.PortalRIK
{
    /// <summary>
    /// Controlador web del api para el catálogo CatProducto de SianWeb.
    /// </summary>
    public class CatProductoController 
        : BaseWebAPIController
    {
        /// <summary>
        /// Obtiene la entrada del producto con Id_Prd igual a id.
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>CatProducto</returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            CatProducto catProducto = null;
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CatProducto cnCatProducto = new CN_CatProducto();
                    catProducto = cnCatProducto.ObtenerPorId(Sesion, id, ibt);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, catProducto);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Se ha producido un error al obtener el producto {0}", id), ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, string.Format("Se ha producido un error al obtener el producto {0}", id),  ex);
            }
        }
    }
}