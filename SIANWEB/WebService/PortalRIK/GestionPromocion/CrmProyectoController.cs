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
using System.Diagnostics;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmProyectoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CRMRegistroProyectos proyecto)
        {
            CN_CrmPromocion cnCrmPromocion = new CN_CrmPromocion();
            /*Este bloque debe de encontrarse en la capa de negocio; para no provocar daño colateral, se sobrecargará el método de creación de proyecto a nivel de negocio con el fragmento siguiente*/
            proyecto.Analisis = DateTime.Now;
            proyecto.Estatus = 1;
            proyecto.FechaCotizacion = DateTime.Now; //??????
            proyecto.Productos = string.Empty;
            proyecto.Comentarios = string.Empty;

            string valor = MaximoId();
            proyecto.IdMax = !string.IsNullOrEmpty(valor) ? Convert.ToInt32(valor) : 0;

            int validador = 0;
            try
            {
                cnCrmPromocion.InsertarOportunidad(Sesion, proyecto, ref validador, "");
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, proyecto);
            }
            catch (CapaNegocios.AplicacionesNoAsociadasException aplicacionesNoAsociadasException)
            {
                //Error 521: Error al asociar aplicaciones al proyecto.
                Trace.TraceError(aplicacionesNoAsociadasException.ToString());
                return Request.CreateErrorResponse((System.Net.HttpStatusCode)521, aplicacionesNoAsociadasException);
            }
            catch (Exception ex)
            {
                //Manejar: error de base de datos, pérdida de comunicación a la base de datos, condición inesperada, cliente inválido
                return Request.CreateErrorResponse((System.Net.HttpStatusCode)520, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]CRMRegistroProyectos proyecto)
        {
            CN_CrmPromocion cnCrmPromocion = new CN_CrmPromocion();
            if (Request.Content.IsFormData())
            {
                try
                {
                    cnCrmPromocion.Actualizar(Sesion, proyecto);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, proyecto);
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
                    var task = Request.Content.ReadAsStreamAsync();
                    task.Wait();
                    var stream = task.Result;
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    System.IO.MemoryStream ms=new System.IO.MemoryStream();
                    stream.CopyTo(ms);
                    string requestBody = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    CrmPromociones objetoDeDatos = JsonConvert.DeserializeObject<CrmPromociones>(requestBody);
                    cnCrmPromocion.Actualizar(Sesion, objetoDeDatos);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, objetoDeDatos);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        /// <summary>
        /// Obtiene los proyectos asociados a un RIK mediante la sesión de ASP.Net
        /// </summary>
        /// <returns>Proyectos asociados al RIK</returns>
        [HttpGet]
        public virtual HttpResponseMessage Get()
        {
            CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
            try
            {
                //var result = cnCrmOportunidad.ObtenerPorRik(Sesion);
                CrmPromociones promocion = new CrmPromociones();
                promocion.Cds = Sesion.Id_Cd;
                promocion.Representante = Sesion.Id_U;

                promocion.Uen = -1;
                promocion.Segmento = -1;
                promocion.Territorio = -1;
                //filtro2
                promocion.Area = -1;
                promocion.Solucion = -1;
                promocion.Aplicacion = -1;
                promocion.Estatus = -1;
                promocion.Cliente = 0;
                promocion.Id_Rik = Sesion.Id_Rik.ToString();

                List<CrmPromociones> List = new List<CrmPromociones>();
                CN_CrmPromocion cls = new CN_CrmPromocion();
                cls.ConsultaCatPromocion(Sesion, promocion, ref List);
                CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
                CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
                CN_CrmValuacionOportunidades cnCrmValuacionOportunidades = new CN_CrmValuacionOportunidades();
                using (var ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    var res = List.Select((p) =>
                    {
                        p.CrmValuacionOportunidades = cnCrmValuacionOportunidades.ObtenerPorProyecto(Sesion, p.Id_Cte, p.Id, ibt);
                        CN_CrmOportunidad.ResultadosValuacion resultadosValuacion = null;

                        try
                        {
                            resultadosValuacion = cnCrmOportunidad.CalcularResultadoValuacion(p, Sesion, ibt);
                            p.ValorPresenteNeto = resultadosValuacion.ValorPresenteNeto;
                            p.UtilidadRemanente = resultadosValuacion.UtilidadRemanente;
                        }
                        catch (CN_CrmOportunidad.CapValProyecto_ParametrosIndefinidosException ex1)
                        {
                            p.ValorPresenteNeto = null;
                            p.UtilidadRemanente = null;
                        }
                        catch (CN_CrmOportunidad.CapValProyecto_ParamsIndefinidosException ex2)
                        {
                            p.ValorPresenteNeto = null;
                            p.UtilidadRemanente = null;
                        }
                        catch (CN_CrmOportunidad.ProyectoNoAsociadoAValuacionException ex)
                        {
                            p.ValorPresenteNeto = null;
                            p.UtilidadRemanente = null;
                        }
                        
                        var aplicaciones = cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(Sesion, p.Id_Cte, p.Id, ibt).ToList();
                        if (aplicaciones.Count() > 0)
                        {
                            p.Id_Apl = aplicaciones[0].Id_Apl;
                            p.Id_Area = aplicaciones[0].CatAplicacion.CatSolucion.Id_Area.Value;
                            p.Id_Sol = aplicaciones[0].CatAplicacion.Id_Sol.Value;
                            p.Id_Seg = aplicaciones[0].CatAplicacion.CatSolucion.CatArea.Id_Seg.Value;
                            p.IdUen = aplicaciones[0].CatAplicacion.CatSolucion.CatArea.CatSegmento.Id_Uen.Value;

                            var apl = cnCatAplicacion.Consultar(Sesion, aplicaciones[0].Id_Apl, ibt);
                            if (apl != null)
                            {
                                p.Descripcion = string.Format("{0}/{1}/{2}/{3}/{4}", apl.CatSolucion.CatArea.CatSegmento.CatUEN.Uen_Descripcion, apl.CatSolucion.CatArea.CatSegmento.Seg_Descripcion, apl.CatSolucion.CatArea.Area_Descripcion, apl.CatSolucion.Sol_Descripcion, apl.Apl_Descripcion);
                            }
                        }

                        return p;
                    }).ToList();

                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, res);
                }
            }
            catch (Exception ex)
            {
                //Manejar la pérdida de conexión a la base de datos. El cliente no debe de saber detalles específicos.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int opId, int idCte)
        {
            CN_CrmOportunidad cls = new CN_CrmOportunidad();
            List<CrmOportunidades> list = new List<CrmOportunidades>();
            try
            {
                cls.ConsultaOportunidad(Sesion, Sesion.Id_Cd, opId, ref list, true);
                if (list.Count > 0)
                {
                    //CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
                    //var aplicaciones = cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(Sesion, idCte, opId);
                    //list[0].Aplicaciones = (from a in aplicaciones
                    //                       select a.Id_Apl).ToArray();
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, list[0]);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, (CrmOportunidades)null);
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

        private string MaximoId()
        {
            try
            {
                Sesion sesion = Sesion;
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CrmOportunidades", "Id_Op", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}