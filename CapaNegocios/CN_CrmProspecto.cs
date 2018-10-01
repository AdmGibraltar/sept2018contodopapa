using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CrmProspecto
    {
        public IEnumerable<CrmProspecto> ObtenerProspectos(int idEmp, int idCd, int idRik, Sesion sesion)
        {
            IEnumerable<CrmProspecto> res = null;
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            res = cdCrmProspecto.ObtenerProspectos(idEmp, idCd, idRik, sesion.Emp_Cnx_EF);
            return res;
        }

        public IEnumerable<CrmProspecto> ObtenerTodosProspectos(int idEmp, int idCd, Sesion sesion)
        {
            IEnumerable<CrmProspecto> res = null;
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            res = cdCrmProspecto.ObtenerTodosProspectos(idEmp, idCd, sesion.Emp_Cnx_EF);
            return res;
        }

        /// <summary>
        /// Devuelve el conjunto de todos los prospectos.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable<CrmProspecto></returns>
        public IEnumerable<CrmProspecto> ObtenerTodosProspectos(int idEmp, int idCd, Sesion sesion, IBusinessTransaction ibt)
        {
            IEnumerable<CrmProspecto> res = null;
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            res = cdCrmProspecto.ObtenerTodosProspectos(idEmp, idCd, ibt.DataContext);
            return res;
        }

        public IEnumerable<CrmProspecto> ObtenerComoClientes(Sesion s, string terminoDeBusqueda)
        {
            CN_CatCliente cnCatCliente = new CN_CatCliente();
            var clientes = cnCatCliente.Obtener(s, terminoDeBusqueda);
            var clientesComoProspecto = (from c in clientes
                                         select new CrmProspecto()
                                         {
                                             CatCliente = c,
                                             Cte_Calle = c.Cte_Calle,
                                             Cte_Contacto = c.Cte_Contacto,
                                             Cte_Email = c.Cte_Email,
                                             Cte_NomComercial = c.Cte_NomComercial,
                                             Cte_Rfc = c.Cte_Rfc,
                                             Cte_Telefono = c.Cte_Telefono,
                                             Id_Cd = c.Id_Cd,
                                             Id_CrmProspecto = 0,
                                             Id_CrmTipoCliente = 2,
                                             Id_Cte = c.Id_Cte,
                                             Id_Emp = c.Id_Emp,
                                             Id_Rik = c.Id_Rik.Value
                                         }).ToList();
            return clientesComoProspecto;
        }

        /// <summary>
        /// Obtiene el conjunto de prospectos desde la entidad de clientes(puesto que los clientes pueden participar en el proceso de CRM)
        /// </summary>
        /// <param name="s">Sesión de l usuario en operación</param>
        /// <param name="terminoDeBusqueda">Término de búsqueda</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable[CrmProspecto]</returns>
        public IEnumerable<CrmProspecto> ObtenerComoClientes(Sesion s, string terminoDeBusqueda, IBusinessTransaction ibt)
        {
            CN_CatCliente cnCatCliente = new CN_CatCliente();
            var clientes = cnCatCliente.Obtener(s, terminoDeBusqueda, ibt);
            var clientesComoProspecto = (from c in clientes
                                         select new CrmProspecto()
                                         {
                                             CatCliente = c,
                                             Cte_Calle = c.Cte_Calle,
                                             Cte_Contacto = c.Cte_Contacto,
                                             Cte_Email = c.Cte_Email,
                                             Cte_NomComercial = c.Cte_NomComercial,
                                             Cte_Rfc = c.Cte_Rfc,
                                             Cte_Telefono = c.Cte_Telefono,
                                             Id_Cd = c.Id_Cd,
                                             Id_CrmProspecto = 0,
                                             Id_CrmTipoCliente = 2,
                                             Id_Cte = c.Id_Cte,
                                             Id_Emp = c.Id_Emp,
                                             Id_Rik = c.Id_Rik.Value
                                         }).ToList();
            return clientesComoProspecto;
        }

        /// <summary>
        /// Crea un nuevo prospecto. La información del cliente asociado a este prospecto también es creada y asociada a la instancia del prospecto que se pasa como argumento.
        /// </summary>
        /// <param name="prospecto">Información del prospecto a crear</param>
        /// <param name="sesion">Sesión de la petición</param>
        public CrmProspecto CrearProspecto(CrmProspecto prospecto, Sesion sesion)
        {
            if (string.IsNullOrEmpty(prospecto.Cte_Rfc))
            {
                //throw new RFCRequeridoException();
            }

            CD_CatCliente cdCatCliente = new CD_CatCliente();
            Clientes clientes=new Clientes();
            clientes.Id_Emp=sesion.Id_Emp;   
            clientes.Id_Cd=sesion.Id_Cd;
            clientes.Id_Cte=prospecto.Id_Cte;
            clientes.Id_U = sesion.Id_U;
            clientes.Id_UCd = sesion.Id_Cd;
            clientes.Id_UMod = sesion.Id_U;
            clientes.Id_Rik = sesion.Id_Rik;
            clientes.Id_Cfe = -1;
            clientes.Id_Corp = -1;
            clientes.Cte_NomComercial=prospecto.Cte_NomComercial;
            clientes.Cte_NomCorto=prospecto.Cte_NomComercial;
            clientes.Cte_FacCalle=prospecto.Cte_Calle;
            clientes.Cte_Contacto = prospecto.Cte_Contacto;
            clientes.Cte_FacNumero="";
            clientes.Cte_FacNumeroInterior=""; 
            clientes.Cte_FacCp="";
            clientes.Cte_FacColonia=""; 
            clientes.Cte_FacMunicipio="";
            clientes.Cte_FacTel="";
            clientes.Cte_FacRfc="";
            clientes.Cte_FacEstado=""; 
            clientes.Cte_Calle=prospecto.Cte_Calle;
            clientes.Cte_Numero="";
            clientes.Cte_NumeroInterior = "";
            clientes.Cte_Cp="";
            clientes.Cte_Colonia=""; 
            clientes.Cte_Municipio="";
            clientes.Cte_Estado="";
            clientes.Cte_Telefono=prospecto.Cte_Telefono;
            clientes.Cte_Fax="";
            clientes.Cte_DRfc = prospecto.Cte_Rfc;
            clientes.Cte_Tipo=0;
            clientes.Cte_Email=prospecto.Cte_Email; 
            clientes.Cte_Credito=false;
            clientes.Cte_Facturacion=false; 
                                    
            clientes.Id_Mon = -1;
            clientes.Cte_LimCobr=0.0; 
            clientes.Cte_RHoraam1="";
            clientes.Cte_RHoraam2=""; 
            clientes.Cte_RHorapm1=""; 
            clientes.Cte_RHorapm2=""; 
            clientes.Cte_RLunes=false; 
            clientes.Cte_RMartes=false;
            clientes.Cte_RMiercoles=false;
            clientes.Cte_RJueves=false;
            clientes.Cte_RViernes=false;
            clientes.Cte_RSabado=false;
            clientes.Cte_RDomingo=false;
            clientes.Cte_CondPago=0;
            clientes.Cte_CPLunes=false;
            clientes.Cte_CPMartes=false;
            clientes.Cte_CPMiercoles=false;
            clientes.Cte_CPJueves=false;
            clientes.Cte_CPViernes=false;
            clientes.Cte_CPSabado=false;
            clientes.Cte_CPDomingo=false;
            clientes.Cte_Comisiones=false;
            clientes.Cte_DesgIva=false;
            clientes.Cte_RetIva=false;
            clientes.Cte_AsignacionPed = -1;
            clientes.Id_Ade = -1;
            clientes.Cte_SerieNCre = -1;
            clientes.Cte_SerieNCa = -1;
            clientes.Estatus=false;

            clientes.Cte_CreditoSuspendido=true;
            clientes.Cte_PHoraam1="";
            clientes.Cte_PHoraam2="";
            clientes.Cte_PHorapm1="";
            clientes.Cte_PHorapm2="";
            clientes.Cte_SemRec=0;
            clientes.Cte_RecLunes=false;
            clientes.Cte_RecMartes=false;
            clientes.Cte_RecMiercoles=false;
            clientes.Cte_RecJueves=false;
            clientes.Cte_RecViernes=false;
            clientes.Cte_RecSabado=false;
            clientes.Cte_RecDomingo=false;
            clientes.Cte_Efectivo=false;
            clientes.Cte_Factoraje=false;
            clientes.Cte_Cheque=false;
            clientes.Cte_Transferencia=false;
            clientes.Cte_ReqOrdenCompra=false;
            clientes.Cte_Documentos="";
            clientes.Cte_TelCobranza1="";
            clientes.Cte_TelCobranza2="";
            clientes.Cte_RemisionElectronica=0;
            clientes.BPorcNotaCredito=false;
            clientes.PorcientoNotaCredito=0.0;
            clientes.PorcientoRetencion=0.0;
            clientes.BPorcientoIVA=false;
            clientes.PorcientoIVA=0;
            clientes.Cte_UDigitos="";
            clientes.Cte_Referencia="";
            clientes.Cte_AutorizaPlazo_IdU=null;
            clientes.Cte_AutorizaPlazo_IdCd=null;
            clientes.Cte_CorreoEdoCuenta1="";
            clientes.Cte_CorreoEdoCuenta2="";
            clientes.Cte_CorreoEdoCuenta3="";
            clientes.Cte_NumCuentaContNacional=0;
            clientes.Cte_SemRev=0;
            clientes.Cte_SemRev2=0;
            clientes.Cte_SemCob=0;
            clientes.Id_TCte=0;
            clientes.Cte_NumeroCuenta="";
            clientes.Cte_ReferenciaTecleada="";
            clientes.Cte_Portal="";
            clientes.Id_Ban=0;
            clientes.Id_UMod=0;

            if (RFCExistente(sesion.Id_Emp, sesion.Id_Cd, null, prospecto.Cte_Rfc, sesion))
            {
                throw new NuevoProspectoRFCExistenteException();
            }

            //Se determina un identificador disponible para el cliente
            int idCliente = 0;
            CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            try
            {
                idCliente = int.Parse(CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CatCliente", "Id_Cte", sesion.Emp_Cnx, "spCatLocal_Maximo"));
                clientes.Id_Cte = idCliente;

                try
                {
                    int verificador = 0;
                    cdCatCliente.InsertarClientes(clientes, sesion.Emp_Cnx, ref verificador);
                    if (verificador == 0)
                    {
                        //arrojar excepción de violación de llave primaria
                    }
                    try
                    {
                        prospecto.Id_Cte = idCliente;
                        prospecto.Id_CrmTipoCliente = 1;
                        prospecto.Id_Rik = sesion.Id_Rik;
                        prospecto.Id_Emp = sesion.Id_Emp;
                        prospecto.Id_Cd = sesion.Id_Cd;

                        CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
                        prospecto = cdCrmProspecto.InsertarProspecto(prospecto, sesion.Emp_Cnx_EF);

                        CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
                        
                        CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
                        var catNotificacion=cdCatNotificacion.Insertar(sesion.Id_Emp, sesion.Id_Cd, 4, String.Format("Se ha creado el prospecto '{0}'", prospecto.Cte_NomComercial), false, null);
                        cdCapRIKNotificacion.Insertar(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, catNotificacion.Id_Notificacion, null);

                        if (prospecto.Territorios != null)
                        {
                            try
                            {
                                CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx);
                                cdCatClienteDet.InsertarBasico(sesion.Id_Emp, sesion.Id_Cd, idCliente, sesion.Id_Rik, prospecto.Territorios, sesion.Emp_Cnx_EF);
                            }
                            catch (Exception ex)
                            {
                                throw new ErrorAsociarClienteTerritorioException(ex);
                            }
                        }

                        if (prospecto.TerritoriosAsociados != null)
                        {
                            try
                            {
                                CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx);
                                cdCatClienteDet.InsertarBasico(sesion.Id_Emp, sesion.Id_Cd, idCliente, sesion.Id_Rik, prospecto.TerritoriosAsociados, sesion.Emp_Cnx_EF);
                            }
                            catch (Exception ex)
                            {
                                throw new ErrorAsociarClienteTerritorioException(ex);
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        //Manejar: pérdida de conexión a la base de datos, error de base de datos y condición inesperada
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    //Manejar: pérdida de conexión a la base de datos, error de base de datos y condición inesperada
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                //Manejar: pérdida de conexión a la base de datos, error de base de datos, error de parseo y condición inesperada
                throw ex;
            }
            return prospecto;
        }

        /// <summary>
        /// Crea un nuevo prospecto. La información del cliente asociado a este prospecto también es creada y asociada a la instancia del prospecto que se pasa como argumento.
        /// </summary>
        /// <param name="prospecto">Información del prospecto a crear</param>
        /// <param name="sesion">Sesión de la petición</param>
        public CrmProspecto CrearProspecto(CrmProspecto prospecto, Sesion sesion, IBusinessTransaction ibt)
        {
            if (string.IsNullOrEmpty(prospecto.Cte_Rfc))
            {
                //throw new RFCRequeridoException();
            }

            CD_CatCliente cdCatCliente = new CD_CatCliente();
            Clientes clientes = new Clientes();
            clientes.Id_Emp = sesion.Id_Emp;
            clientes.Id_Cd = sesion.Id_Cd;
            clientes.Id_Cte = prospecto.Id_Cte;
            clientes.Id_U = sesion.Id_U;
            clientes.Id_UCd = sesion.Id_Cd;
            clientes.Id_UMod = sesion.Id_U;
            clientes.Id_Rik = sesion.Id_Rik;
            clientes.Id_Cfe = -1;
            clientes.Id_Corp = -1;
            clientes.Cte_NomComercial = prospecto.Cte_NomComercial;
            clientes.Cte_NomCorto = prospecto.Cte_NomComercial;
            clientes.Cte_FacCalle = prospecto.Cte_Calle;
            clientes.Cte_Contacto = prospecto.Cte_Contacto;
            clientes.Cte_FacNumero = "";
            clientes.Cte_FacNumeroInterior = "";
            clientes.Cte_FacCp = "";
            clientes.Cte_FacColonia = "";
            clientes.Cte_FacMunicipio = "";
            clientes.Cte_FacTel = "";
            clientes.Cte_FacRfc = "";
            clientes.Cte_FacEstado = "";
            clientes.Cte_Calle = prospecto.Cte_Calle;
            clientes.Cte_Numero = "";
            clientes.Cte_NumeroInterior = "";
            clientes.Cte_Cp = "";
            clientes.Cte_Colonia = "";
            clientes.Cte_Municipio = "";
            clientes.Cte_Estado = "";
            clientes.Cte_Telefono = prospecto.Cte_Telefono;
            clientes.Cte_Fax = "";
            clientes.Cte_DRfc = prospecto.Cte_Rfc;
            clientes.Cte_Tipo = 0;
            clientes.Cte_Email = prospecto.Cte_Email;
            clientes.Cte_Credito = false;
            clientes.Cte_Facturacion = false;

            clientes.Id_Mon = -1;
            clientes.Cte_LimCobr = 0.0;
            clientes.Cte_RHoraam1 = "";
            clientes.Cte_RHoraam2 = "";
            clientes.Cte_RHorapm1 = "";
            clientes.Cte_RHorapm2 = "";
            clientes.Cte_RLunes = false;
            clientes.Cte_RMartes = false;
            clientes.Cte_RMiercoles = false;
            clientes.Cte_RJueves = false;
            clientes.Cte_RViernes = false;
            clientes.Cte_RSabado = false;
            clientes.Cte_RDomingo = false;
            clientes.Cte_CondPago = 0;
            clientes.Cte_CPLunes = false;
            clientes.Cte_CPMartes = false;
            clientes.Cte_CPMiercoles = false;
            clientes.Cte_CPJueves = false;
            clientes.Cte_CPViernes = false;
            clientes.Cte_CPSabado = false;
            clientes.Cte_CPDomingo = false;
            clientes.Cte_Comisiones = false;
            clientes.Cte_DesgIva = false;
            clientes.Cte_RetIva = false;
            clientes.Cte_AsignacionPed = -1;
            clientes.Id_Ade = -1;
            clientes.Cte_SerieNCre = -1;
            clientes.Cte_SerieNCa = -1;
            clientes.Estatus = false;

            clientes.Cte_CreditoSuspendido = true;
            clientes.Cte_PHoraam1 = "";
            clientes.Cte_PHoraam2 = "";
            clientes.Cte_PHorapm1 = "";
            clientes.Cte_PHorapm2 = "";
            clientes.Cte_SemRec = 0;
            clientes.Cte_RecLunes = false;
            clientes.Cte_RecMartes = false;
            clientes.Cte_RecMiercoles = false;
            clientes.Cte_RecJueves = false;
            clientes.Cte_RecViernes = false;
            clientes.Cte_RecSabado = false;
            clientes.Cte_RecDomingo = false;
            clientes.Cte_Efectivo = false;
            clientes.Cte_Factoraje = false;
            clientes.Cte_Cheque = false;
            clientes.Cte_Transferencia = false;
            clientes.Cte_ReqOrdenCompra = false;
            clientes.Cte_Documentos = "";
            clientes.Cte_TelCobranza1 = "";
            clientes.Cte_TelCobranza2 = "";
            clientes.Cte_RemisionElectronica = 0;
            clientes.BPorcNotaCredito = false;
            clientes.PorcientoNotaCredito = 0.0;
            clientes.PorcientoRetencion = 0.0;
            clientes.BPorcientoIVA = false;
            clientes.PorcientoIVA = 0;
            clientes.Cte_UDigitos = "";
            clientes.Cte_Referencia = "";
            clientes.Cte_AutorizaPlazo_IdU = null;
            clientes.Cte_AutorizaPlazo_IdCd = null;
            clientes.Cte_CorreoEdoCuenta1 = "";
            clientes.Cte_CorreoEdoCuenta2 = "";
            clientes.Cte_CorreoEdoCuenta3 = "";
            clientes.Cte_NumCuentaContNacional = 0;
            clientes.Cte_SemRev = 0;
            clientes.Cte_SemRev2 = 0;
            clientes.Cte_SemCob = 0;
            clientes.Id_TCte = 0;
            clientes.Cte_NumeroCuenta = "";
            clientes.Cte_ReferenciaTecleada = "";
            clientes.Cte_Portal = "";
            clientes.Id_Ban = 0;
            clientes.Id_UMod = 0;

            //
            // Verifica si el RFC existe.            
            // Se comenta ya que no aplica el id_Cte viene desde la pagina.  
            //
            /*if (!string.IsNullOrEmpty(prospecto.Cte_Rfc))
            {
                //throw new RFCRequeridoException();
                if (RFCExistente(sesion.Id_Emp, sesion.Id_Cd, null, prospecto.Cte_Rfc, ibt))
                {
                    throw new NuevoProspectoRFCExistenteException();
                }
            }
            */
            
            int idCliente = 0;
            if (clientes.Id_Cte > 0)
            {
                // Si el Id_Cte es cero
                idCliente =Convert.ToInt32(clientes.Id_Cte);            
            }

            CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            try
            {
                bool AgregarCliente = false;
                if (idCliente <= 0)
                {   
                    // si es 0 es que es nuevo. 
                    idCliente = int.Parse(CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CatCliente", "Id_Cte", ibt.DataContext, "spCatLocal_Maximo"));
                    clientes.Id_Cte = idCliente;
                    AgregarCliente = true;
                }

                try
                {
                    int verificador = 0;
                    if (AgregarCliente)
                    {
                        // IdCliente no esta definido.
                        cdCatCliente.InsertarClientes(clientes, sesion.Emp_Cnx, ref verificador, ibt.DataContext);
                    }
                    else
                    {
                        // Ponemos verificado a 1 ya que el idCliente es un numero valido.
                        verificador = 1;
                    }                    

                    if (verificador == 0)
                    {
                        //arrojar excepción de violación de llave primaria
                    }
                    try
                    {
                        prospecto.Id_Cte = idCliente;
                        if (AgregarCliente)
                        {
                            prospecto.Id_CrmTipoCliente = 1; //1 Prospecto 
                        }
                        else
                        {
                            prospecto.Id_CrmTipoCliente = 2; // 2 Ya es cliente
                        }                        

                        prospecto.Id_Rik = sesion.Id_Rik;
                        prospecto.Id_Emp = sesion.Id_Emp;
                        prospecto.Id_Cd = sesion.Id_Cd;                        

                        CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
                        prospecto = cdCrmProspecto.InsertarProspecto(prospecto, ibt.DataContext);

                        CN_CapValuacionGlobalCliente cnCapValuacionGlobalCliente = new CN_CapValuacionGlobalCliente();
                        cnCapValuacionGlobalCliente.CrearParaCliente(sesion, idCliente, ibt);

                        //CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
                        //CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
                        //var catNotificacion = cdCatNotificacion.Insertar(sesion.Id_Emp, sesion.Id_Cd, 4, String.Format("Se ha creado el prospecto '{0}'", prospecto.Cte_NomComercial), false, ibt.DataContext);
                        //cdCapRIKNotificacion.Insertar(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, catNotificacion.Id_Notificacion, ibt.DataContext);

                        if (prospecto.Territorios != null)
                        {
                            try
                            {
                                CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx);
                                cdCatClienteDet.InsertarBasico(sesion.Id_Emp, sesion.Id_Cd, idCliente, sesion.Id_Rik, prospecto.Territorios, ibt.DataContext);
                            }
                            catch (Exception ex)
                            {
                                throw new ErrorAsociarClienteTerritorioException(ex);
                            }
                        }

                        if (prospecto.TerritoriosAsociados != null)
                        {
                            try
                            {
                                CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx);
                                cdCatClienteDet.InsertarBasico(sesion.Id_Emp, sesion.Id_Cd, idCliente, sesion.Id_Rik, prospecto.TerritoriosAsociados, ibt.DataContext);
                            }
                            catch (Exception ex)
                            {
                                throw new ErrorAsociarClienteTerritorioException(ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Manejar: pérdida de conexión a la base de datos, error de base de datos y condición inesperada
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    //Manejar: pérdida de conexión a la base de datos, error de base de datos y condición inesperada
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                //Manejar: pérdida de conexión a la base de datos, error de base de datos, error de parseo y condición inesperada
                throw ex;
            }
            return prospecto;
        }

        /// <summary>
        /// Crea un nuevo prospecto
        ///  Este metodo es un copia del de arrib pero en este se eliminan los try catch
        /// </summary>
        /// <param name="prospecto">Información del prospecto a crear</param>
        /// <param name="sesion">Sesión de la petición</param>
        public CrmProspecto Crear_Prospecto(CrmProspecto prospecto, Sesion sesion, IBusinessTransaction ibt)
        {
            if (string.IsNullOrEmpty(prospecto.Cte_Rfc))
            {
                //throw new RFCRequeridoException();
            }

            CD_CatCliente cdCatCliente = new CD_CatCliente();
            Clientes clientes = new Clientes();
            clientes.Id_Emp = sesion.Id_Emp;
            clientes.Id_Cd = sesion.Id_Cd;
            clientes.Id_Cte = prospecto.Id_Cte;
            clientes.Id_U = sesion.Id_U;
            clientes.Id_UCd = sesion.Id_Cd;
            clientes.Id_UMod = sesion.Id_U;
            clientes.Id_Rik = sesion.Id_Rik;
            clientes.Id_Cfe = -1;
            clientes.Id_Corp = -1;
            clientes.Cte_NomComercial = prospecto.Cte_NomComercial;
            clientes.Cte_NomCorto = prospecto.Cte_NomComercial;
            clientes.Cte_FacCalle = prospecto.Cte_Calle;
            clientes.Cte_Contacto = prospecto.Cte_Contacto;
            clientes.Cte_FacNumero = "";
            clientes.Cte_FacNumeroInterior = "";
            clientes.Cte_FacCp = "";
            clientes.Cte_FacColonia = "";
            clientes.Cte_FacMunicipio = "";
            clientes.Cte_FacTel = "";
            clientes.Cte_FacRfc = "";
            clientes.Cte_FacEstado = "";
            clientes.Cte_Calle = prospecto.Cte_Calle;
            clientes.Cte_Numero = "";
            clientes.Cte_NumeroInterior = "";
            clientes.Cte_Cp = "";
            clientes.Cte_Colonia = "";
            clientes.Cte_Municipio = "";
            clientes.Cte_Estado = "";
            clientes.Cte_Telefono = prospecto.Cte_Telefono;
            clientes.Cte_Fax = "";
            clientes.Cte_DRfc = prospecto.Cte_Rfc;
            clientes.Cte_Tipo = 0;
            clientes.Cte_Email = prospecto.Cte_Email;
            clientes.Cte_Credito = false;
            clientes.Cte_Facturacion = false;

            clientes.Id_Mon = -1;
            clientes.Cte_LimCobr = 0.0;
            clientes.Cte_RHoraam1 = "";
            clientes.Cte_RHoraam2 = "";
            clientes.Cte_RHorapm1 = "";
            clientes.Cte_RHorapm2 = "";
            clientes.Cte_RLunes = false;
            clientes.Cte_RMartes = false;
            clientes.Cte_RMiercoles = false;
            clientes.Cte_RJueves = false;
            clientes.Cte_RViernes = false;
            clientes.Cte_RSabado = false;
            clientes.Cte_RDomingo = false;
            clientes.Cte_CondPago = 0;
            clientes.Cte_CPLunes = false;
            clientes.Cte_CPMartes = false;
            clientes.Cte_CPMiercoles = false;
            clientes.Cte_CPJueves = false;
            clientes.Cte_CPViernes = false;
            clientes.Cte_CPSabado = false;
            clientes.Cte_CPDomingo = false;
            clientes.Cte_Comisiones = false;
            clientes.Cte_DesgIva = false;
            clientes.Cte_RetIva = false;
            clientes.Cte_AsignacionPed = -1;
            clientes.Id_Ade = -1;
            clientes.Cte_SerieNCre = -1;
            clientes.Cte_SerieNCa = -1;
            clientes.Estatus = false;

            clientes.Cte_CreditoSuspendido = true;
            clientes.Cte_PHoraam1 = "";
            clientes.Cte_PHoraam2 = "";
            clientes.Cte_PHorapm1 = "";
            clientes.Cte_PHorapm2 = "";
            clientes.Cte_SemRec = 0;
            clientes.Cte_RecLunes = false;
            clientes.Cte_RecMartes = false;
            clientes.Cte_RecMiercoles = false;
            clientes.Cte_RecJueves = false;
            clientes.Cte_RecViernes = false;
            clientes.Cte_RecSabado = false;
            clientes.Cte_RecDomingo = false;
            clientes.Cte_Efectivo = false;
            clientes.Cte_Factoraje = false;
            clientes.Cte_Cheque = false;
            clientes.Cte_Transferencia = false;
            clientes.Cte_ReqOrdenCompra = false;
            clientes.Cte_Documentos = "";
            clientes.Cte_TelCobranza1 = "";
            clientes.Cte_TelCobranza2 = "";
            clientes.Cte_RemisionElectronica = 0;
            clientes.BPorcNotaCredito = false;
            clientes.PorcientoNotaCredito = 0.0;
            clientes.PorcientoRetencion = 0.0;
            clientes.BPorcientoIVA = false;
            clientes.PorcientoIVA = 0;
            clientes.Cte_UDigitos = "";
            clientes.Cte_Referencia = "";
            clientes.Cte_AutorizaPlazo_IdU = null;
            clientes.Cte_AutorizaPlazo_IdCd = null;
            clientes.Cte_CorreoEdoCuenta1 = "";
            clientes.Cte_CorreoEdoCuenta2 = "";
            clientes.Cte_CorreoEdoCuenta3 = "";
            clientes.Cte_NumCuentaContNacional = 0;
            clientes.Cte_SemRev = 0;
            clientes.Cte_SemRev2 = 0;
            clientes.Cte_SemCob = 0;
            clientes.Id_TCte = 0;
            clientes.Cte_NumeroCuenta = "";
            clientes.Cte_ReferenciaTecleada = "";
            clientes.Cte_Portal = "";
            clientes.Id_Ban = 0;
            clientes.Id_UMod = 0;

            //
            // Verifica si el RFC existe.            
            // Se comenta ya que no aplica el id_Cte viene desde la pagina.  
            //
            /*if (!string.IsNullOrEmpty(prospecto.Cte_Rfc))
            {
                //throw new RFCRequeridoException();
                if (RFCExistente(sesion.Id_Emp, sesion.Id_Cd, null, prospecto.Cte_Rfc, ibt))
                {
                    throw new NuevoProspectoRFCExistenteException();
                }
            }
            */

            int idCliente = 0;
            if (clientes.Id_Cte > 0)
            {
                // Si el Id_Cte es cero
                idCliente = Convert.ToInt32(clientes.Id_Cte);
            }

            CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            
            bool AgregarCliente = false;
            if (idCliente <= 0)
            {
                // si es 0 es que es nuevo. 
                idCliente = int.Parse(CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CatCliente", "Id_Cte", ibt.DataContext, "spCatLocal_Maximo"));
                clientes.Id_Cte = idCliente;
                AgregarCliente = true;
            }

            int verificador = 0;
            if (AgregarCliente)
            {
                // IdCliente no esta definido.
                cdCatCliente.InsertarClientes(clientes, sesion.Emp_Cnx, ref verificador, ibt.DataContext);
            }
            else
            {
                // Ponemos verificado a 1 ya que el idCliente es un numero valido.
                verificador = 1;
            }

            if (verificador == 0)
            {
                //arrojar excepción de violación de llave primaria
            }
                    
            prospecto.Id_Cte = idCliente;
            if (AgregarCliente)
            {
                prospecto.Id_CrmTipoCliente = 1; //1 Prospecto 
            }
            else
            {
                prospecto.Id_CrmTipoCliente = 2; // 2 Ya es cliente
            }

            prospecto.Id_Rik = sesion.Id_Rik;
            prospecto.Id_Emp = sesion.Id_Emp;
            prospecto.Id_Cd = sesion.Id_Cd;

            ICD_Contexto icdCtx = ibt.DataContext;

            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            prospecto = cdCrmProspecto.InsertarProspecto(prospecto, ibt.DataContext);

            CN_CapValuacionGlobalCliente cnCapValuacionGlobalCliente = new CN_CapValuacionGlobalCliente();
            cnCapValuacionGlobalCliente.CrearParaCliente(sesion, idCliente, ibt);

            //CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
            //CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
            //var catNotificacion = cdCatNotificacion.Insertar(sesion.Id_Emp, sesion.Id_Cd, 4, String.Format("Se ha creado el prospecto '{0}'", prospecto.Cte_NomComercial), false, ibt.DataContext);
            //cdCapRIKNotificacion.Insertar(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, catNotificacion.Id_Notificacion, ibt.DataContext);

                if (prospecto.Territorios != null)
                {
                    try
                    {
                        CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx);
                        cdCatClienteDet.InsertarBasico(sesion.Id_Emp, sesion.Id_Cd, idCliente, sesion.Id_Rik, prospecto.Territorios, ibt.DataContext);
                    }
                    catch (Exception ex)
                    {
                        throw new ErrorAsociarClienteTerritorioException(ex);
                    }
                }

                if (prospecto.TerritoriosAsociados != null)
                {
                    try
                    {
                        CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx);
                        cdCatClienteDet.InsertarBasico(sesion.Id_Emp, sesion.Id_Cd, idCliente, sesion.Id_Rik, prospecto.TerritoriosAsociados, ibt.DataContext);
                    }
                    catch (Exception ex)
                    {
                        throw new ErrorAsociarClienteTerritorioException(ex);
                    }
                }
                      
            return prospecto;
        }


        /// <summary>
        /// Actualiza la información base de un proyecto.
        /// Asocia los nuevos territorios encontrados en la propiedad [Territorios]. Los territorios que no aparecen en la propiedad [Territorios] no son afectados.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="prospecto">Identificador del prospecto</param>
        /// <param name="sesion">Pase de inicio de sesión del llamador.</param>
        public void ActualizarProspecto(int idEmp, int idCd, CrmProspecto prospecto, Sesion sesion)
        {
            if (string.IsNullOrEmpty(prospecto.Cte_Rfc))
            {
                //throw new RFCRequeridoException();
            }

            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            //Si el RFC ha sido registrado con otro cliente, generar excepción
            if (RFCExistente(idEmp, idCd, prospecto.Id_Cte, prospecto.Cte_Rfc, sesion))
            {
                throw new NuevoProspectoRFCExistenteException();
            }
            //Se obtiene el prospecto actual en la base de datos con la finalidad de comparar la clave del cliente.
            var p = cdCrmProspecto.ObtenerProspecto(idEmp, idCd, prospecto.Id_Rik, prospecto.Id_CrmProspecto, sesion.Emp_Cnx_EF);
            if (p.Id_Cte == prospecto.Id_Cte)
            {
                //Prospecto encontrado
                cdCrmProspecto.ActualizarProspecto(idEmp, idCd, prospecto, sesion.Emp_Cnx_EF);

                //Se agregan los nuevos territorios
                CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx);
                if (prospecto.Territorios != null)
                {
                    var detalles = cdCatClienteDet.Consultar(idEmp, idCd, prospecto.Id_Cte, sesion.Emp_Cnx_EF);
                    var detallesExistentes = (from t in prospecto.Territorios
                                              join d in detalles
                                              on t equals d.Id_Ter
                                              select t).ToList();
                    var detallesNuevos = (from t in prospecto.Territorios
                                          where !detallesExistentes.Contains(t)
                                          select t).ToArray();
                    try
                    {
                        cdCatClienteDet.InsertarBasico(idEmp, idCd, prospecto.Id_Cte, prospecto.Id_Rik, detallesNuevos, sesion.Emp_Cnx_EF);
                    }
                    catch (Exception ex)
                    {
                        throw new ErrorAsociarClienteTerritorioException(ex);
                    }
                }
            }
            else
            {
                //La clave del cliente que se envía no coincide con la clave del prospecto original
                throw new ClienteProspectoInvalidoException();
            }
        }

        public CrmProspecto ObtenerProspecto(int idEmp, int idCd, int idRik, int idCrmProspecto, Sesion sesion)
        {
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            return cdCrmProspecto.ObtenerProspecto(idEmp, idCd, idRik, idCrmProspecto, sesion.Emp_Cnx_EF);
        }

        /*
        public void EliminarProspecto(ref iEliminado, int idCrmProspecto, int idCte, Sesion sesion)
        {
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            //Validar que no sea un cliente existente. ¿qué pasa al prospecto? ¿Solo se desactiva?
            //Validar que no existan proyectos asociados al prospecto: ¿qué pasa si tiene proyectos asociados? ¿Qué les pasa a los proyectos?
            cdCrmProspecto.EliminarProspecto(ref iEliminado,idCrmProspecto, sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, idCte, sesion.Emp_Cnx_EF, sesion.Emp_Cnx);
        }
        */

        public int EliminarProspecto(int idCrmProspecto, int idCte, Sesion sesion)
        {
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            //Validar que no sea un cliente existente. ¿qué pasa al prospecto? ¿Solo se desactiva?
            //Validar que no existan proyectos asociados al prospecto: ¿qué pasa si tiene proyectos asociados? ¿Qué les pasa a los proyectos?
            int iEstatus = 0;
            iEstatus= cdCrmProspecto.Eliminar_Prospecto(idCrmProspecto, sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, idCte, sesion.Emp_Cnx_EF, sesion.Emp_Cnx);
            return iEstatus;
        }


        /// <summary>
        /// Indica si un rfc ha sido asignado a un cliente.
        /// </summary>
        /// <param name="prospecto">CrmProspecto. Información del prospecto</param>
        /// <param name="sesion">Sesion. Pase de inicio de sesion del usuario que llama el servicio</param>
        /// <returns>bool. True en caso de haber coincidencias. False en caso contrario</returns>
        public bool RFCExistente(int idEmp, int idCd, int? idCte, string rfc, Sesion sesion)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            var clientesConRFC = cdCatCliente.ConsultarPorRFC(idEmp, idCd, rfc, sesion.Emp_Cnx_EF).ToList();
            if (idCte != null)
            {
                var clienteConRFC = clientesConRFC.Where(cc => cc.Id_Cte == idCte).ToList();
                return clienteConRFC.Count > 1;
            }
            return clientesConRFC.Count > 0;
            
        }

        /// <summary>
        /// Indica si un rfc ha sido asignado a un cliente.
        /// </summary>
        /// <param name="prospecto">CrmProspecto. Información del prospecto</param>
        /// <param name="sesion">Sesion. Pase de inicio de sesion del usuario que llama el servicio</param>
        /// <returns>bool. True en caso de haber coincidencias. False en caso contrario</returns>
        public bool RFCExistente(int idEmp, int idCd, int? idCte, string rfc, IBusinessTransaction ibt)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            var clientesConRFC = cdCatCliente.ConsultarPorRFC(idEmp, idCd, rfc, ibt.DataContext).ToList();
            if (idCte != null)
            {
                var clienteConRFC = clientesConRFC.Where(cc => cc.Id_Cte == idCte).ToList();
                return clienteConRFC.Count > 1;
            }
            return clientesConRFC.Count > 0;

        }

        public int ObtenerTipoCliente(Sesion s, int idCte)
        {
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            return cdCrmProspecto.ConsultarTipoCliente(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Devuelve el código del tipo de cliente
        /// </summary>
        /// <param name="s">Sesión del usuario en operación.</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>int</returns>
        public int ObtenerTipoCliente(Sesion s, int idCte, IBusinessTransaction ibt)
        {
            CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
            return cdCrmProspecto.ConsultarTipoCliente(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, ibt.DataContext);
        }

        /// <summary>
        /// Activa al prospecto como un cliente del sistema
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente como prospecto</param>
        public void ConvertirACliente(Sesion s, int idCte)
        {
            //Revisar si primero el identificador de cliente se encuentra en modo de prospecto, esto es, revisando si en la entidad de prospecto se encuentra aún marcado.
            int tipoCliente=ObtenerTipoCliente(s, idCte);
            if (tipoCliente == 1) //Prospecto
            {
                //Marcar [CRMProspecto].[Id_CrmTipoCliente]=2
                //Marcar [CatCliente].[Cte_Activo]=true
                CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
                cdCrmProspecto.ActualizarCampoTipoCliente(s.Id_Emp, s.Id_Cd, idCte, 2, s.Emp_Cnx_EF);
                CN_CatCliente cnCatCliente = new CN_CatCliente();
                cnCatCliente.Activar(s, idCte);
            }
            else //Cliente
            {
                //Arrojar excepción indicando que el prospecto ya es cliente.
                throw new ConvertirAClienteClienteYaEsProspectoException();
            }
        }

        /// <summary>
        /// Activa al prospecto como un cliente del sistema
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente como prospecto</param>
        /// <param name="ibt">Transacción de negocios</param>
        public void ConvertirACliente(Sesion s, int idCte, IBusinessTransaction ibt)
        {
            //Revisar si primero el identificador de cliente se encuentra en modo de prospecto, esto es, revisando si en la entidad de prospecto se encuentra aún marcado.
            int tipoCliente = ObtenerTipoCliente(s, idCte);
            if (tipoCliente == 1) //Prospecto
            {
                //Marcar [CRMProspecto].[Id_CrmTipoCliente]=2
                //Marcar [CatCliente].[Cte_Activo]=true
                CD_CrmProspecto cdCrmProspecto = new CD_CrmProspecto();
                cdCrmProspecto.ActualizarCampoTipoCliente(s.Id_Emp, s.Id_Cd, idCte, 2, ibt.DataContext);
                CN_CatCliente cnCatCliente = new CN_CatCliente();
                cnCatCliente.Activar(s, idCte, ibt);
            }
            else //Cliente
            {
                //Arrojar excepción indicando que el prospecto ya es cliente.
                throw new ConvertirAClienteClienteYaEsProspectoException();
            }
        }

        public class ClienteProspectoInvalidoException : Exception
        {
            public ClienteProspectoInvalidoException() : base("Cliente inválido para el prospecto")
            {

            }
        }

        public class NuevoProspectoRFCExistenteException : Exception
        {
            public NuevoProspectoRFCExistenteException() : base("El RFC ya se encuentra registrado por otro cliente.")
            {
            }
        }

        public class ErrorAsociarClienteTerritorioException : Exception
        {
            public ErrorAsociarClienteTerritorioException() : base("Ocurrió una complicación al asociar Territorios al prospecto")
            {
            }

            public ErrorAsociarClienteTerritorioException(Exception inner)
                : base("Ocurrió una complicación al asociar Territorios al prospecto", inner)
            {
            }
        }

        public class RFCRequeridoException : Exception
        {
            public RFCRequeridoException() : base("El campo RFC es requerido")
            {
            }
        }

        public class ConvertirAClienteClienteYaEsProspectoException
            : Exception
        {
            public ConvertirAClienteClienteYaEsProspectoException()
                : base("Conversión a cliente cancelada. Ya se encontraba como cliente")
            {
            }
        }
    }
}
