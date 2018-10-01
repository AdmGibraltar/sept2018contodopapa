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
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    /// <summary>
    /// Controlador de la entidad Prospecto
    /// </summary>
    public class CrmProspectoController
        : BaseWebAPIController
    {
        [HttpGet]
        public IEnumerable<CrmProspecto> Get(int idEmp, int idCd, int idRik)
        {
            CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
            /*var resultado = cnCrmProspecto.ObtenerProspectos(idEmp, idCd, idRik, Sesion);
            if (resultado == null)
                return new List<CrmProspecto>();
            return resultado;*/
            List<CrmProspecto> resultado = new List<CrmProspecto>();

            try
            {
                resultado = cnCrmProspecto.ObtenerProspectos(idEmp, idCd, idRik, Sesion).ToList();
                if (resultado == null)
                {
                    resultado = new List<CrmProspecto>();           
                }
            }
            catch (Exception ex)
            {
                //Manejar los casos adecuadamente
                resultado = new List<CrmProspecto>();           
                Logger.Error("CrmProspectoController::Get", ex);                
            }
            return resultado;                
        }

        /// <summary>
        /// Consulta un listado de prospectos basada en un término de búsqueda
        /// </summary>
        /// <param name="terminoDeBusqueda">String. Término de búsqueda.</param>
        /// <returns>List<CrmProspecto> - Prospectos asociados en la búsqueda por el término.</returns>
        [HttpGet]
        public Task<HttpResponseMessage> Get(string terminoDeBusqueda, bool incluirClientes=false)
        {
            try
            {
                var httpContext = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    HttpContext.Current = httpContext;
                    try
                    {
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            IEnumerable<CrmProspecto> resultado = null;
                            ibt.Begin();
                            CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
                            resultado = cnCrmProspecto.ObtenerTodosProspectos(Sesion.Id_Emp, Sesion.Id_Cd, Sesion, ibt);
                            if (resultado == null)
                                resultado = new List<CrmProspecto>();
                            terminoDeBusqueda = terminoDeBusqueda.ToLower();
                            var resultadoFiltrado = (from p in resultado
                                                     where p.Cte_NomComercial.ToLower().Contains(terminoDeBusqueda) || p.Id_CrmProspecto.ToString().Contains(terminoDeBusqueda)
                                                     select p).ToList();
                            resultadoFiltrado = resultadoFiltrado.Select(p =>
                            {
                                p.TerritoriosDeProspectoSerializable = p.CatCliente.CatClienteDets.Select(ccd => ccd.CatTerritorio).ToList();
                                return p;
                            }).ToList();
                            resultado = resultadoFiltrado;

                            if (incluirClientes)
                            {
                                var clientes = cnCrmProspecto.ObtenerComoClientes(Sesion, terminoDeBusqueda, ibt);
                                var resultadoComoLista = new List<CrmProspecto>(resultado);
                                resultadoComoLista.AddRange(clientes);
                                resultado = resultadoComoLista;
                            }
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CrmProspectoController::Get", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });

                return t;
            }
            catch (Exception ex)
            {
                //Manejar los casos adecuadamente
                Logger.Error("CrmProspectoController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() => Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex));
            }
        }

        [HttpGet]
        public CrmProspecto Get(int idEmp, int idCd, int idRik, int idCrmProspecto)
        {
            CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
            var resultado = cnCrmProspecto.ObtenerProspecto(idEmp, idCd, idRik, idCrmProspecto, Sesion);
            return resultado;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]NuevoProspectoModel prospecto)
        {
            
            CrmProspecto p = new CrmProspecto();
            p.Cte_Calle = prospecto.txtCalle==null ? "" : prospecto.txtCalle;
            p.Cte_Contacto = prospecto.txtContacto==null ? "" : prospecto.txtContacto;
            p.Cte_Email = prospecto.txtEmail==null ? "" : prospecto.txtEmail;
            p.Cte_NomComercial = prospecto.txtNombre;
            p.Cte_Telefono = prospecto.txtTelefono==null ? "" : prospecto.txtTelefono;
            //p.Cte_Rfc = prospecto.RFC;
            p.Cte_Rfc = prospecto.RFC == null ? "" : prospecto.RFC;
            p.Territorios = prospecto.Territorios;
            p.Id_Ter_Temporal = prospecto.TerritorioTemporal;
            p.TerritoriosAsociados = prospecto.TerritoriosAsociados;

            //
            // Pasa el id de cliente, si es un cliente existente.
            //
            int Id_Cte = 0;

            if (prospecto.hdnId_Cte != null)
            {
                Int32.TryParse(prospecto.hdnId_Cte.ToString(), out Id_Cte);
                p.Id_Cte = Id_Cte;
            }
            else
            {
                p.Id_Cte = 0;
            }
            
            CatNotificacion notificacion=null;

            CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();          

            try
            {
                //manejar el caso para prospecto no existente
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {

                    ibt.Begin();

                    //p = cnCrmProspecto.CrearProspecto(p, Sesion, ibt);

                    p = cnCrmProspecto.Crear_Prospecto(p, Sesion, ibt);
                    ibt.Commit();

                    //Se crea una notificación.
                    //ibt.Begin();
                    //CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
                    //CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
                    //notificacion = cdCatNotificacion.Insertar(Sesion.Id_Emp, Sesion.Id_Cd, 4, String.Format("Se ha creado el prospecto '{0}'", p.Cte_NomComercial), false, ibt.DataContext);
                    //cdCapRIKNotificacion.Insertar(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, notificacion.Id_Notificacion, ibt.DataContext);
                    //ibt.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al crear un prospecto", ex);
                return Request.CreateErrorResponse((System.Net.HttpStatusCode)520, ex);
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, new { Prospecto = p, Notificacion = notificacion });
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]ActualizarProspectoModel prospecto)
        {
            CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
            CrmProspecto p=new CrmProspecto();
            p.Id_Emp = Sesion.Id_Emp;
            p.Id_Cd = Sesion.Id_Cd;
            p.Id_Rik = Sesion.Id_Rik;
            try
            {
                p.Id_CrmTipoCliente = int.Parse(prospecto.hdnId_CrmTipoCliente);
            }
            catch (Exception ex)
            {
                //Tipo de cliente inválido a causa de parseo
                Request.CreateErrorResponse((System.Net.HttpStatusCode)512, "El tipo de cliente no es válido.");
            }
            
            try
            {
                p.Id_Cte = int.Parse(prospecto.hdnId_Cte);
            }
            catch (Exception ex)
            {
                //Cliente inválido a causa de parseo
                Request.CreateErrorResponse((System.Net.HttpStatusCode)511, "La información del prospecto se encuentra corrompida.");
            }
            
            try
            {
                p.Id_CrmProspecto = int.Parse(prospecto.idCrmProspecto);
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse((System.Net.HttpStatusCode)510, "La clave del prospecto en edición no es válida.");
            }
            p.Cte_Calle=prospecto.txtCalle;
            p.Cte_Contacto=prospecto.txtContacto;
            p.Cte_Email=prospecto.txtEmail;
            p.Cte_NomComercial=prospecto.txtNombre;
            p.Cte_Telefono=prospecto.txtTelefono;
            p.Cte_Rfc = prospecto.RFC;
            p.Territorios = prospecto.Territorios;
            p.Id_Ter_Temporal = prospecto.TerritorioTemporal;
            try
            {
                //manejar el caso para prospecto no existente
                cnCrmProspecto.ActualizarProspecto(Sesion.Id_Emp, Sesion.Id_Cd, p, Sesion);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse((System.Net.HttpStatusCode)520, ex);
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }

       /* [HttpDelete]
        public HttpResponseMessage Delete([FromBody]CrmProspecto prospecto)
        {
            CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
            try
            {
                
                cnCrmProspecto.EliminarProspecto(prospecto.Id_CrmProspecto  , prospecto.Id_Cte, Sesion);
            }
            catch (Exception ex)
            {
                //Manejar: pérdida de conexión a la fuente de datos, condición inesperada, error de base de datos
                Request.CreateErrorResponse((System.Net.HttpStatusCode)520, ex); //Manejo general
            }
            
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }*/

        [HttpDelete]
        public HttpResponseMessage Delete(int IdCrmProspecto, int IdCte)
        {            
            CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
            int iEliminado = 0;
            try
            {
                iEliminado = cnCrmProspecto.EliminarProspecto(IdCrmProspecto, IdCte, Sesion);
                //cnCrmProspecto.EliminarProspecto(prospecto.Id_CrmProspecto, prospecto.Id_Cte, Sesion);
            }
            catch (Exception ex)
            {
                //Manejar: pérdida de conexión a la fuente de datos, condición inesperada, error de base de datos
                return Request.CreateErrorResponse((System.Net.HttpStatusCode)521, ex); //Error                
            }

            if (iEliminado <= 0)
            {
                return Request.CreateErrorResponse((System.Net.HttpStatusCode)512, "El prospecto tiene proyectos en seguimiento, No se puede eliminar.");                
            }
            
            return Request.CreateResponse(System.Net.HttpStatusCode.OK,"Ejecución completa.");            
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