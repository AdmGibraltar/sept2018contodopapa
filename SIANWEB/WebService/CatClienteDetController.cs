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

namespace SIANWEB.WebService.PortalRIK
{
    public class CatClienteDetController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]WebAPI.Models.Post.CatClienteDet data)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                var result = cnCatClienteDet.CrearNuevo(Sesion, data.IdCte, data.IdRik, data.IdTer, data.IdSeg, data.VPO);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Devuelve los territorios asociados a un cliente.
        /// </summary>
        /// <param name="idCte">Identificador del cliente</param>
        /// <returns>IEnumerable(CatClienteDet). Conjunto de territorios asociados a un cliente.</returns>
        [HttpGet]
        public HttpResponseMessage Get(int idCte)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                var resultado=cnCatClienteDet.ObtenerPorCliente(Sesion, idCte);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int idCte, int idTer)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                cnCatClienteDet.Remover(Sesion, idCte, idTer);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
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