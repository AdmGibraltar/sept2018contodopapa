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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmOportunidadesAplicacionController 
        : BaseWebAPIController
    {
        [HttpPut]
        public HttpResponseMessage Put([FromBody]PUTArray oportunidadesAplicaciones)
        {
            try
            {
                CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
                cnCrmOportunidadesAplicacion.Actualizar(oportunidadesAplicaciones.IdOp, oportunidadesAplicaciones.OportunidadesAplicacion, Sesion);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Obtiene la aplicación asociada con el Proyecto
        /// </summary>
        /// <param name="idOp">Identificador del proyecto</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(int idCte, int idOp)
        {
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
                    var opAp = cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(Sesion, idCte, idOp, ibt);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, opAp);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Se generó un error al obtener la aplicación asociada con el proyecto", ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "Se generó un error al obtener la aplicación asociada con el proyecto", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmOportunidadesAplicacion"></param>
        /// <returns></returns>
        //[HttpPut]
        //public HttpResponseMessage Put([FromBody]CrmOportunidadesAplicacion crmOportunidadesAplicacion)
        //{
        //    try
        //    {
        //        CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
        //        cnCrmOportunidadesAplicacion.Actualizar(crmOportunidadesAplicacion, Sesion);
        //        return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        public class PUTArray
        {
            public int IdOp
            {
                get;
                set;
            }

            public CrmOportunidadesAplicacion[] OportunidadesAplicacion
            {
                get;
                set;
            }
        }
    }
}