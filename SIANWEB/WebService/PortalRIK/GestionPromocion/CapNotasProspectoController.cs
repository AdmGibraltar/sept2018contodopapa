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
    public class CapNotasProspectoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CapNotasProspecto capNotasProspecto)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                var resultado = cnCapNotasProspecto.Crear(Sesion, capNotasProspecto.Id_Cliente, capNotasProspecto.CatNotaSerializable.Texto);
                
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]CapNotasProspecto capNotasProspecto)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                cnCapNotasProspecto.Eliminar(Sesion, capNotasProspecto);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]CapNotasProspecto capNotasProspecto)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                capNotasProspecto.CatNota = capNotasProspecto.CatNotaSerializable;
                cnCapNotasProspecto.Actualizar(Sesion, capNotasProspecto);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int idCte)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                var result = cnCapNotasProspecto.ObtenerPorProspecto(Sesion, idCte);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
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