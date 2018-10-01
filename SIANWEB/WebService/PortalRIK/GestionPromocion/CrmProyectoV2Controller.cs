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
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmProyectoV2Controller
        : BaseWebAPIController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]FormaCRMRegistroProyectos proyecto)
        {
            List<int> aplicacionesNoAsociadas = new List<int>();
            CN_CrmPromocion cnCrmPromocion = new CN_CrmPromocion();
            List<CatNotificacion> notificaciones = new List<CatNotificacion>();
            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
            {
                ibt.Begin();
                if (proyecto.FormaAplicaciones != null)
                {
                    if (proyecto.FormaAplicaciones.Length > 0)
                    {
                        for (int i = 0; i < proyecto.FormaAplicaciones.Length; i++)
                        {
                            if (proyecto.FormaAplicaciones[i].Seleccionado != null)
                            {
                                CRMRegistroProyectos p = new CRMRegistroProyectos(proyecto);
                                /*Este bloque debe de encontrarse en la capa de negocio; para no provocar daño colateral, se sobrecargará el método de creación de proyecto a nivel de negocio con el fragmento siguiente*/
                                p.Analisis = DateTime.Now;
                                p.Estatus = 1;
                                p.FechaCotizacion = DateTime.Now; //??????
                                p.Productos = string.Empty;
                                p.Comentarios = string.Empty;
                                p.Crm_TipoVenta = proyecto.tipoVenta;
                                p.CrmOp_OrigenCRMII = true;

                                // RFH 13/10/2017
                                decimal ValorPotencial = 0;
                                if (proyecto.FormaAplicaciones[i].VPO!=null) {
                                    decimal.TryParse(proyecto.FormaAplicaciones[i].VPO.Value.ToString(), out ValorPotencial);
                                } else {
                                    ValorPotencial=0;
                                }
                                
                                //p.AplicacionesV2 = new CRMRegistroProyectos._Aplicacion[1] { new CRMRegistroProyectos._Aplicacion() { Id_Aplicacion = proyecto.FormaAplicaciones[i].Id_Aplicacion.Value, VPO = proyecto.FormaAplicaciones[i].VPO.Value } };
                                p.AplicacionesV2 = new CRMRegistroProyectos._Aplicacion[1] { 
                                    new CRMRegistroProyectos._Aplicacion() { 
                                        Id_Aplicacion = proyecto.FormaAplicaciones[i].Id_Aplicacion, 
                                        VPO = ValorPotencial } 
                                };

                                string valor = MaximoId(ibt.DataContext);
                                p.IdMax = !string.IsNullOrEmpty(valor) ? Convert.ToInt32(valor) : 0;

                                int validador = 0;
                                try
                                {
                                    cnCrmPromocion.InsertarOportunidad(Sesion, p, ref validador, "", ibt);
                                }
                                catch (CapaNegocios.AplicacionesNoAsociadasException aplicacionesNoAsociadasException)
                                {
                                    //Error 521: Error al asociar aplicaciones al proyecto.
                                    aplicacionesNoAsociadas.AddRange(aplicacionesNoAsociadasException.IdApls);
                                }
                                catch (Exception ex)
                                {
                                    //Manejar: error de base de datos, pérdida de comunicación a la base de datos, condición inesperada, cliente inválido
                                    //
                                    
                                    aplicacionesNoAsociadas.Add(proyecto.FormaAplicaciones[i].Id_Aplicacion);
                                }

                                // TODO: RFH terminar de depurar y activar.
                                // notificaciones se comenta debe estar activa.
                                //
                                /*
                                try
                                {
                                    CN_CapRIKNotificacion cnCapRIKNotificacion = new CN_CapRIKNotificacion();
                                    var capRikNotificacion = cnCapRIKNotificacion.CrearNotificacionNuevoProyecto(Sesion, String.Format("Se ha creado el proyecto {0}", p.Id_Op), ibt);
                                    ibt.Save();
                                    ibt.DataContext.ReloadEntity(capRikNotificacion, e => e.CatNotificacion);
                                    notificaciones.Add(capRikNotificacion.CatNotificacion);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error("CrmProyectoV2Controller::Post", ex);
                                }
                                */

                            }
                        }
                    }
                }
                else if (proyecto.Area == -1)
                {
                    CRMRegistroProyectos p = new CRMRegistroProyectos(proyecto);
                    p.Analisis = DateTime.Now;
                    p.Estatus = 1;
                    p.FechaCotizacion = DateTime.Now; //??????
                    p.Productos = string.Empty;
                    p.Comentarios = string.Empty;
                    p.Crm_TipoVenta = proyecto.tipoVenta;
                    p.CrmOp_OrigenCRMII = true;
                    p.Aplicacion = -1;

                    string valor = MaximoId(ibt.DataContext);
                    p.IdMax = !string.IsNullOrEmpty(valor) ? Convert.ToInt32(valor) : 0;

                    int validador = 0;
                    try
                    {
                        cnCrmPromocion.InsertarOportunidad(Sesion, p, ref validador, "", ibt);
                    }
                    catch (CapaNegocios.AplicacionesNoAsociadasException aplicacionesNoAsociadasException)
                    {
                        //Error 521: Error al asociar aplicaciones al proyecto.
                        aplicacionesNoAsociadas.AddRange(aplicacionesNoAsociadasException.IdApls);
                    }
                    catch (Exception ex)
                    {
                        //Manejar: error de base de datos, pérdida de comunicación a la base de datos, condición inesperada, cliente inválido
                    }

                    try
                    {
                        CN_CapRIKNotificacion cnCapRIKNotificacion = new CN_CapRIKNotificacion();
                        var capRikNotificacion = cnCapRIKNotificacion.CrearNotificacionNuevoProyecto(Sesion, String.Format("Se ha creado el proyecto {0}", p.Id_Op), ibt);
                        ibt.Save();
                        ibt.DataContext.ReloadEntity(capRikNotificacion, e => e.CatNotificacion);
                        notificaciones.Add(capRikNotificacion.CatNotificacion);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CrmProyectoV2Controller::Post", ex);
                    }
                }
                ibt.Commit();
            }
                
            
            if (aplicacionesNoAsociadas.Count > 0)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.OK, new Exception("Algunas aplicaciones no pudieron ser asociadas."));
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, new { Notificaciones = notificaciones });
            }

            /*Este bloque debe de encontrarse en la capa de negocio; para no provocar daño colateral, se sobrecargará el método de creación de proyecto a nivel de negocio con el fragmento siguiente*/
            //proyecto.Analisis = DateTime.Now;
            //proyecto.Estatus = 1;
            //proyecto.FechaCotizacion = DateTime.Now;
            //proyecto.Productos = string.Empty;
            //proyecto.Comentarios = string.Empty;

            //string valor = MaximoId();
            //proyecto.IdMax = !string.IsNullOrEmpty(valor) ? Convert.ToInt32(valor) : 0;
            ///*
            // *
            // */

            //int validador=0;
            //try
            //{
            //    cnCrmPromocion.InsertarOportunidad(Sesion, proyecto, ref validador, "");
            //    return Request.CreateResponse(System.Net.HttpStatusCode.OK, proyecto);
            //}
            //catch (CapaNegocios.AplicacionesNoAsociadasException aplicacionesNoAsociadasException)
            //{
            //    //Error 521: Error al asociar aplicaciones al proyecto.
            //    return Request.CreateErrorResponse((System.Net.HttpStatusCode)521, aplicacionesNoAsociadasException);
            //}
            //catch (Exception ex)
            //{
            //    //Manejar: error de base de datos, pérdida de comunicación a la base de datos, condición inesperada, cliente inválido
            //    return Request.CreateErrorResponse((System.Net.HttpStatusCode)520, ex);
            //}
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
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
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
                var res = List.Select((p) => 
                {
                    var aplicaciones = cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(Sesion, p.Id_Cte, p.Id).ToList();
                    if (aplicaciones.Count() > 0)
                    {
                        var apl = cnCatAplicacion.Consultar(Sesion, aplicaciones[0].Id_Apl);
                        p.Descripcion += apl.Apl_Descripcion;
                    }
                    return p; 
                }).ToList();
                
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, res);
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

        private string MaximoId(ICD_Contexto icdCtx)
        {
            try
            {
                Sesion sesion = Sesion;
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CrmOportunidades", "Id_Op", icdCtx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public enum Checkbox
    {
        on = 1,
        off = 0
    }

    public class FormaAplicacion
    {
        public int Id_Aplicacion
        {
            get;
            set;
        }

        public decimal? VPO
        {
            get;
            set;
        }

        public Checkbox? Seleccionado
        {
            get;
            set;
        }
    }

    public class FormaCRMRegistroProyectos : CRMRegistroProyectos
    {
        public int? tipoVenta
        {
            get;
            set;
        }

        public FormaAplicacion[] FormaAplicaciones
        {
            get;
            set;
        }
    }
}