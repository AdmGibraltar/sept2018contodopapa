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

namespace SIANWEB.WebService
{
    public class CatTerritorioController 
        : BaseWebAPIController
    {
        // Este metodo reemplaza al de abajo 
        // ya no utiliza EF
        // 26 Jun 2018 RFH
        [HttpGet]
        public Task<HttpResponseMessage> Get(int X, int Y)
        {

            List<eTerritorio> lst = new List<eTerritorio>();

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
                            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                            lst = cnCatTerritorios.ObtenerTerritorios_PorRik(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, Sesion);
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, lst);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CatTerritorioController::Get", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }

        }

        /// <summary>
        /// 
        /// Consulta los territorios asociados al RIK. Utiliza el pase de sesión para determinar la empresa, 
        /// el centro de distribución y el representante.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<HttpResponseMessage> Get()
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
                            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                            var territorios = cnCatTerritorios.ObtenerTerritoriosPorRik(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, Sesion);
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, territorios.ToList());
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CatTerritorioController::Get", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
            
        }

        /// <summary>
        /// Devuelve la colección de territorios asociados a un prospecto.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idProspecto"></param>
        /// <returns>Colección de territorios asociados a un prospecto</returns>
        [HttpGet]
        public HttpResponseMessage Get(int idCliente)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                var resutado = cnCatClienteDet.ObtenerPorCliente(Sesion, idCliente);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resutado);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public IEnumerable<CatTerritorio> Get(int idEmp, int idCd, int idRik)
        {
            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
            return cnCatTerritorios.ObtenerTerritoriosPorRik(idEmp, idCd, idRik, Sesion);
        }

        [HttpGet]
        public IEnumerable<CatTerritorio> Get(int idEmp, int idCd, int idRik, int idSeg)
        {
            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
            return cnCatTerritorios.ObtenerTerritoriosPorRik(idEmp, idCd, idRik, Sesion);
        }
    }
}