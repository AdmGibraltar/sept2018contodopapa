using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using System.Threading.Tasks;
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService
{
    public class CatAplicacionController 
        : BaseWebAPIController
    {
        [HttpGet]
        public HttpResponseMessage Get(int idEmp, int idSol)
        {
            CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
            try
            {
                var result = cnCatAplicacion.ObtenerPorEmpresaYSolucion(idEmp, idSol, Sesion);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int idSol, int idSeg, int idOp)
        {
            CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
            try
            {
                var result = cnCatAplicacion.ObtenerPorEmpresaSolucionSegmento(Sesion.Id_Emp, Sesion.Id_Cd, idSol, idSeg, idOp, Sesion);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
            
        }

        [HttpGet]
        public HttpResponseMessage Get(int idUen, int idSeg, int idArea, int idSol, int idOp, int idCte)
        {
            CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
            if (idOp == 0)
            {
                try
                {
                    var aplicacionesDisponibles = cnCatAplicacion.ObtenerTodasLasAplicacionesDisponibles(Sesion, idUen, idSeg, idArea, idSol, idCte);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, aplicacionesDisponibles);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                try
                {
                    var aplicacionesDisponibles = cnCatAplicacion.ObtenerTodasLasAplicacionesDisponibles(Sesion, idUen, idSeg, idArea, idSol, idOp, idCte);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, aplicacionesDisponibles);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
        }
    }
}