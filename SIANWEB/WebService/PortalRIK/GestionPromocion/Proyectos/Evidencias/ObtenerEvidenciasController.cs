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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Proyectos.Evidencias
{
    public class ObtenerEvidenciasController
        : BaseWebAPIController
    {
        [HttpGet]
        public Task<HttpResponseMessage> Get(int idCte, int idOp)
        {
            try
            {
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("ObtenerEvidenciasController::Get", ex);
                return null;
            }
        }
    }
}