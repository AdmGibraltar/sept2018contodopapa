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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmCatalogoUnicoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int idCte, int idOp, string terminoBusqueda)
        {
            CN_CrmCatalogoUnico cnCrmCatalogoUnico = new CN_CrmCatalogoUnico();
            try
            {
                var result = cnCrmCatalogoUnico.ObtenerPorConfiguracionDeProyecto(Sesion, idCte, idOp).ToList().Where(cu=>{
                    return cu.CatProducto.Prd_Descripcion.Contains(terminoBusqueda) || cu.CatProducto.Id_Prd.ToString().Contains(terminoBusqueda);
                });
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
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