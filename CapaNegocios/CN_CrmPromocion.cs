using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;
using System.Diagnostics;

namespace CapaNegocios
{
    public class CN_CrmPromocion
    {
        public void ComboCds(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboCds(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboRik(Sesion sesion, int cds, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboRik(sesion, cds, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboUen(Sesion sesion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboUen(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboSegmento(Sesion sesion, int cds, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboSegmento(sesion, cds, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ComboArea(sesion, segmento, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarSolucion(Sesion sesion, int area, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaSolucion(sesion, area, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAplicacion(Sesion sesion, int solucion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaAplicacion(sesion, solucion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatPromocion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaCatPromocion(sesion, promocion, ref list);
                CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
                foreach (var p in list)
                {
                    p.CrmTipoCliente = cnCrmProspecto.ObtenerTipoCliente(sesion, p.Id_Cte);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelarPromocion(Sesion sesion, int cd, int promocion, ref int validador)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.CancelarPromocion(sesion, cd, promocion, ref validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatClientes(Sesion sesion, int Id_Ter, int Id_UEN, int Id_Rik, int id_Seg, int idCliente, string nombreCliente, ref List<CrmPromociones> List)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaCatClientes(sesion, Id_Ter, Id_UEN, Id_Rik, id_Seg, idCliente, nombreCliente, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarOportunidad(Sesion sesion, CRMRegistroProyectos promocion, ref int validador, string aplicaciones)
        {
            List<int> faultyIdApls = new List<int>();
            //Se registra la oportunidad
            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(sesion))
            {
                ibt.Begin();
                #region "Registro de oportunidad"
                try
                {
                    CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                    claseCRM.InsertarOportunidad(sesion, promocion, ref validador, aplicaciones, ibt.DataContext);
                    promocion.Id_Op = promocion.IdMax;
                }
                catch (Exception ex)
                {
                    Logger.Debug("CD_CrmPromocion::InsertarOportunidad", ex);
                    throw ex;
                }
                #endregion

                #region "Asociación de aplicaciones al proyecto"
                //Se registran las aplicaciones en caso de que el valor de la propiedad [Aplicaciones] de [promocion] no sea nulo.

                if (promocion.AplicacionesV2 != null)
                {
                    //Las aplicaciones se registran una por una para dar oportunidad a su registro exitoso aún y cuando emerja una condición de error en el proceso debido a una regla de base de datos.
                    CD_CrmOportunidadesAplicacion cdCrmOpApl = new CD_CrmOportunidadesAplicacion();
                    CrmOportunidades registros = new CrmOportunidades();

                    registros.Id_Emp = sesion.Id_Emp;
                    registros.Id_Cd = sesion.Id_Cd_Ver;
                    registros.Id_Ter = promocion.Territorio;
                    registros.Id_Cte = promocion.Cliente;
                    registros.ID_Area = promocion.Area;
                    registros.Id_Seg = promocion.Segmento;
                    registros.Id_Uen = promocion.Uen;
                    registros.Id_Sol = promocion.Solucion;
                    registros.Id_Op = promocion.Id_Op.Value;

                    CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
                    var aplicacionesAsociadasActualmente = cnCatAplicacion.ObtenerAplicacionesEnProyecto(sesion, promocion.Cliente, promocion.Id_Op.Value, ibt).Select(ap => ap.Id_Apl);
                    foreach (var idApl in promocion.AplicacionesV2)
                    {
                        //validar que la aplicación no se encuentre actualmente asociada.
                        var aplicacionAsociada = aplicacionesAsociadasActualmente.Contains(idApl.Id_Aplicacion);
                        if (!aplicacionAsociada) //La aplicación no se encuentra asociada
                        {
                            try
                            {
                                cdCrmOpApl.Insertar(sesion.Id_Emp, sesion.Id_Cd, promocion.IdMax, idApl.Id_Aplicacion, idApl.VPO, ibt.DataContext);

                                #region Estructura
                                try
                                {
                                    registros.Id_Apl = idApl.Id_Aplicacion;
                                    registros.Porcentaje = 0;
                                    registros.Activo = true;
                                    ActualizaDimension(registros, sesion.Emp_Cnx, ref validador, ibt);
                                }
                                catch (Exception innerEx)
                                {
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                //Se rastrea el identificador de la aplicación que fallo al tratar de asociarse al proyecto.
                                Logger.Debug("CD_CrmOportunidadesAplicacion::Insertar", ex);
                                faultyIdApls.Add(idApl.Id_Aplicacion);
                            }
                        }
                    }

                }
                #endregion

                //#region "Asociación de Territorio al prospecto"
                ////Se determina si existe una relación actual
                //if (promocion.Id_CrmProspecto != 0)
                //{
                //    CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(sesion);
                //    var catClienteDet = cnCatClienteDet.Obtener(promocion.Cliente, promocion.Territorio);
                //    if (catClienteDet == null) //relacion no existe
                //    {
                //        CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(sesion.Emp_Cnx_EF);
                //        try
                //        {
                //            cdCatClienteDet.InsertarBasico(sesion.Id_Emp, sesion.Id_Cd, promocion.Cliente, sesion.Id_Rik, new int[] { promocion.Territorio }, ibt.DataContext);
                //        }
                //        catch (Exception ex)
                //        {
                //            Logger.Debug("CD_CatClienteDet::InsertarBasico", ex);
                //            throw ex;
                //        }

                //    }
                //}
                //#endregion

                ibt.Commit();
            }


            //Arrojar excepción de AplicacionesNoAsociadasException.
            if (faultyIdApls.Count > 0)
            {
                throw new AplicacionesNoAsociadasException(faultyIdApls.ToArray());
            }
            //Si se llega hasta este punto, todo se encuentra bien.
        }

        /// <summary>
        /// Inicializa la estructura del repositorio de recursos por fase de proyecto
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="promocion">Instancia de datos del proyecto</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void InicializarLibreriaEvidencias(Sesion sesion, CRMRegistroProyectos promocion, IBusinessTransaction ibt)
        {

            //Obtener el nodo "Proyectos"
            CN_CatBiblioteca cnCatBiblioteca = new CN_CatBiblioteca();
            var nodoProyectos = cnCatBiblioteca.ObtenerRepositorioDeProyectos(sesion, ibt);
            //Se registra un nuevo nodo bajo "Proyectos" y se crea una nueva entrada en la entidad CapProyectoBiblioNodo con el identificador del nuevo nodo registrado.
            CD_CapBibliotecaNodo cdCapBibliotecaNodo = new CD_CapBibliotecaNodo();
            CapBibliotecaNodo entradaNodoProyectoId = new CapBibliotecaNodo()
            {
                Id_Emp = sesion.Id_Emp,
                Id_Cd = sesion.Id_Cd,
                Id_BiblioNodo_Padre = nodoProyectos.Id_BiblioNodo,
                Id_Biblioteca = nodoProyectos.Id_Biblioteca,
                Id_Recurso = null,
                BiblioNodo_Nombre = string.Format("{0}", promocion.Id_Op)
            };
            entradaNodoProyectoId = cdCapBibliotecaNodo.Insertar(entradaNodoProyectoId, ibt.DataContext);
            ibt.Save();
            CD_CapProyectoBiblioNodo cdCapProyectoBiblioNodo = new CD_CapProyectoBiblioNodo();
            CapProyectoBiblioNodo capProyectoBiblioNodo = new CapProyectoBiblioNodo()
            {
                Id_Emp = sesion.Id_Emp,
                Id_Cd = sesion.Id_Cd,
                Id_BiblioNodo = entradaNodoProyectoId.Id_BiblioNodo,
                Id_Biblioteca = entradaNodoProyectoId.Id_Biblioteca,
                Id_Op = promocion.Id_Op.Value,
                Id_U = sesion.Id_U
            };
            capProyectoBiblioNodo = cdCapProyectoBiblioNodo.Insertar(capProyectoBiblioNodo, ibt.DataContext);
            //Obtener las fases del flujo de un proyecto: por lo pronto se codifican estáticamente
            //Registrar las 4 entradas de las fases del nodo de instancia de proyecto en la entidad CapProyectoFasesBiblioNodo.
            CD_CapProyectoFasesBiblioNodo cdCapProyectoFasesBiblioNodo = new CD_CapProyectoFasesBiblioNodo();
            for (int i = 1; i < 5; i++)
            {
                CapProyectoFaseBiblioNodo capProyectoFaseBiblioNodo = new CapProyectoFaseBiblioNodo()
                {
                    Id_Emp = sesion.Id_Emp,
                    Id_Cd = sesion.Id_Cd,
                    Id_Biblioteca = entradaNodoProyectoId.Id_Biblioteca,
                    Id_Fase = i,
                    Id_U = sesion.Id_U,
                    Id_Op = promocion.Id_Op.Value,
                    Id_BiblioNodo = entradaNodoProyectoId.Id_BiblioNodo
                };
                cdCapProyectoFasesBiblioNodo.Insertar(capProyectoFaseBiblioNodo, ibt.DataContext);
            }

        }

        /// <summary>
        /// Inicializa la estructura del repositorio de recursos por fase de proyecto
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="idOp">Identificador del proyecto</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void InicializarLibreriaEvidencias(Sesion sesion, int idOp, IBusinessTransaction ibt)
        {
            //Obtener el nodo "Proyectos"
            CN_CatBiblioteca cnCatBiblioteca = new CN_CatBiblioteca();
            var nodoProyectos = cnCatBiblioteca.ObtenerRepositorioDeProyectos(sesion, ibt);
            //Se registra un nuevo nodo bajo "Proyectos" y se crea una nueva entrada en la entidad CapProyectoBiblioNodo con el identificador del nuevo nodo registrado.
            CD_CapBibliotecaNodo cdCapBibliotecaNodo = new CD_CapBibliotecaNodo();
            CapBibliotecaNodo entradaNodoProyectoId = new CapBibliotecaNodo()
            {
                Id_Emp = sesion.Id_Emp,
                Id_Cd = sesion.Id_Cd,
                Id_BiblioNodo_Padre = nodoProyectos.Id_BiblioNodo,
                Id_Biblioteca = nodoProyectos.Id_Biblioteca,
                Id_Recurso = null,
                BiblioNodo_Nombre = string.Format("{0}", idOp)
            };
            entradaNodoProyectoId = cdCapBibliotecaNodo.Insertar(entradaNodoProyectoId, ibt.DataContext);
            ibt.Save();
            CD_CapProyectoBiblioNodo cdCapProyectoBiblioNodo = new CD_CapProyectoBiblioNodo();
            CapProyectoBiblioNodo capProyectoBiblioNodo = new CapProyectoBiblioNodo()
            {
                Id_Emp = sesion.Id_Emp,
                Id_Cd = sesion.Id_Cd,
                Id_BiblioNodo = entradaNodoProyectoId.Id_BiblioNodo,
                Id_Biblioteca = entradaNodoProyectoId.Id_Biblioteca,
                Id_Op = idOp,
                Id_U = sesion.Id_U
            };
            capProyectoBiblioNodo = cdCapProyectoBiblioNodo.Insertar(capProyectoBiblioNodo, ibt.DataContext);
            //Obtener las fases del flujo de un proyecto: por lo pronto se codifican estáticamente
            //Registrar las 4 entradas de las fases del nodo de instancia de proyecto en la entidad CapProyectoFasesBiblioNodo.
            CD_CapProyectoFasesBiblioNodo cdCapProyectoFasesBiblioNodo = new CD_CapProyectoFasesBiblioNodo();
            for (int i = 1; i < 5; i++)
            {
                CapProyectoFaseBiblioNodo capProyectoFaseBiblioNodo = new CapProyectoFaseBiblioNodo()
                {
                    Id_Emp = sesion.Id_Emp,
                    Id_Cd = sesion.Id_Cd,
                    Id_Biblioteca = entradaNodoProyectoId.Id_Biblioteca,
                    Id_Fase = i,
                    Id_U = sesion.Id_U,
                    Id_Op = idOp,
                    Id_BiblioNodo = entradaNodoProyectoId.Id_BiblioNodo
                };
                cdCapProyectoFasesBiblioNodo.Insertar(capProyectoFaseBiblioNodo, ibt.DataContext);
            }

        }

        //
        // Libreria de Evidencias SP 
        // RFH 12 Abr 2018
        // 
        public List<eCapBibliotecaNodo> InicializarLibreriaEvidencias_(Sesion sesion, int id_Op, string _conexion)
        {
            CD_CapBibliotecaNodo cdCBN = new CD_CapBibliotecaNodo();
            return cdCBN.Inicializa_LibreriaEvidencias(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, id_Op, _conexion);
        }

        /// <summary>
        /// Crea un proyecto.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="promocion">Información del proyecto a crear</param>
        /// <param name="validador">Señalador de éxito de la operación</param>
        /// <param name="aplicaciones">Conjunto de aplicaciones asociadas al proyecto</param>
        /// <param name="ibt">Transacción de la capa de negocios</param>
        public void InsertarOportunidad(Sesion sesion, CRMRegistroProyectos promocion, ref int validador, string aplicaciones, IBusinessTransaction ibt)
        {
            List<int> faultyIdApls = new List<int>();
            //Se registra la oportunidad
            #region "Registro de oportunidad"

            CD_CrmPromocion claseCRM = new CD_CrmPromocion();
            //Se asigna la ruta de la aplicación elegida a la ruta declarada en el proyecto.
            if (promocion.AplicacionesV2 != null)
            {
                if (promocion.AplicacionesV2.Length > 0)
                {
                    var idApl = promocion.AplicacionesV2[0];
                    CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
                    var aplicacion = cnCatAplicacion.Consultar(sesion, idApl.Id_Aplicacion, ibt);
                    promocion.Solucion = aplicacion.Id_Sol.Value;
                    promocion.Area = aplicacion.CatSolucion.Id_Area.Value;
                    promocion.Aplicacion = idApl.Id_Aplicacion;
                    promocion.ValorPotencialO = (double)idApl.VPO;
                }
            }
            claseCRM.InsertarOportunidad(sesion, promocion, ref validador, aplicaciones, ibt.DataContext);
            promocion.Id_Op = promocion.IdMax;

            //Crear la librería del proyecto para alojar los archivos de evidencia en sus fases.
            try
            {

                InicializarLibreriaEvidencias(sesion, promocion, ibt);
            }
            catch (Exception ex)
            {
                //Se rastrea el identificador de la aplicación que fallo al tratar de asociarse al proyecto.
                Logger.Debug("No se puede iniciar la libreria de Evidencias", ex);
            }

            #endregion

            #region "Asociación de aplicaciones al proyecto"
            //Se registran las aplicaciones en caso de que el valor de la propiedad 
            //[Aplicaciones] de [promocion] no sea nulo.

            if (promocion.AplicacionesV2 != null)
            {
                //Las aplicaciones se registran una por una para dar oportunidad a su registro exitoso aún y 
                //cuando emerja una condición de error en el proceso debido a una regla de base de datos.

                CD_CrmOportunidadesAplicacion cdCrmOpApl = new CD_CrmOportunidadesAplicacion();
                CrmOportunidades registros = new CrmOportunidades();

                registros.Id_Emp = sesion.Id_Emp;
                registros.Id_Cd = sesion.Id_Cd_Ver;
                registros.Id_Ter = promocion.Territorio;
                registros.Id_Cte = promocion.Cliente;
                registros.ID_Area = promocion.Area;
                registros.Id_Seg = promocion.Segmento;
                registros.Id_Uen = promocion.Uen;
                registros.Id_Sol = promocion.Solucion;
                registros.Id_Op = promocion.Id_Op.Value;

                CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
                var aplicacionesAsociadasActualmente = cnCatAplicacion.ObtenerAplicacionesEnProyecto(sesion, promocion.Cliente, promocion.Id_Op.Value, ibt).Select(ap => ap.Id_Apl);
                foreach (var idApl in promocion.AplicacionesV2)
                {
                    //validar que la aplicación no se encuentre actualmente asociada.
                    var aplicacionAsociada = aplicacionesAsociadasActualmente.Contains(idApl.Id_Aplicacion);
                    if (!aplicacionAsociada) //La aplicación no se encuentra asociada
                    {
                        try
                        {

                            //cdCrmOpApl.Insertar(sesion.Id_Emp, sesion.Id_Cd, promocion.IdMax, idApl.Id_Aplicacion, idApl.VPO, ibt.DataContext);

                            cdCrmOpApl.Insertar(sesion, sesion.Id_Emp, sesion.Id_Cd, promocion.IdMax, idApl.Id_Aplicacion, idApl.VPO, ref validador, ibt.DataContext);


                            #region Estructura
                            try
                            {
                                registros.Id_Apl = idApl.Id_Aplicacion;
                                registros.Porcentaje = 0;
                                registros.Activo = true;
                                ActualizaDimension(registros, sesion.Emp_Cnx, ref validador, ibt);
                            }
                            catch (Exception innerEx)
                            {
                            }
                            #endregion
                            ibt.Save();
                        }
                        catch (Exception ex)
                        {
                            //Se rastrea el identificador de la aplicación que fallo al tratar de asociarse al proyecto.
                            Logger.Debug("CD_CrmOportunidadesAplicacion::Insertar", ex);
                            faultyIdApls.Add(idApl.Id_Aplicacion);
                        }
                    }
                }

            }
            #endregion

            //Arrojar excepción de AplicacionesNoAsociadasException.
            if (faultyIdApls.Count > 0)
            {
                throw new AplicacionesNoAsociadasException(faultyIdApls.ToArray());
            }
            //Si se llega hasta este punto, todo se encuentra bien.
        }
        public void EstructuraSegmento(ref DataSet dsEstructuraSegmento, CrmOportunidades registros, string Conexion)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.EstructuraSegmento(ref dsEstructuraSegmento, registros, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizaDimension(CrmOportunidades registros, string Cnx, ref int verificador)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ActualizaDimension(registros, Cnx, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión transaccional.
        /// </summary>
        /// <param name="registros"></param>
        /// <param name="Cnx"></param>
        /// <param name="verificador"></param>
        /// <param name="ibt">Transaccional a nivel de capa de negocio</param>
        public void ActualizaDimension(CrmOportunidades registros, string Cnx, ref int verificador, IBusinessTransaction ibt)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ActualizaDimension(registros, Cnx, ref verificador, ibt.DataContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CrmOportunidade ConsultarDetalle(int idEmp, int idCd, int idOp, Sesion sesion)
        {
            CrmOportunidade result = null;
            CD_CrmPromocion cdCrmPromocion = new CD_CrmPromocion();
            result = cdCrmPromocion.ConsultarDetalle(idEmp, idCd, idOp, sesion.Emp_Cnx_EF);
            return result;
        }

        /// <summary>
        /// Actualiza un proyecto, así como las asociaciones a las aplicaciones.
        /// </summary>
        /// <param name="sesion">Sesion. Pase de inicio de sesión asociada a la cuenta del usuario actual.</param>
        /// <param name="promocion">CRMRegistroProyectos. Información del registro a actualizar.</param>
        public void Actualizar(Sesion sesion, CRMRegistroProyectos promocion)
        {
            using (ICD_Contexto contexto = CD_FabricaContexto.CrearDefault(sesion.Emp_Cnx_EF))
            {
                //Se actualiza la información base
                CD_CrmPromocion cdCrmPromocion = new CD_CrmPromocion();
                try
                {
                    cdCrmPromocion.ActualizarPromocion(sesion.Id_Emp, sesion.Id_Cd, promocion, contexto);
                }
                catch (Exception ex)
                {
                    throw new ActualizarProyectoException();
                }

                //Se actualizan las asociaciones con las aplicaciones
                //Primero se eliminan las aplicaciones y después se recrean los registros
                //Se considera un esquema de ejecución transaccional
                #region "Asociación de aplicaciones al proyecto"

                //Se registran las aplicaciones en caso de que el valor de la propiedad [Aplicaciones] de [promocion] no sea nulo.
                if (promocion.Aplicaciones != null)
                {
                    CD_CrmOportunidadesAplicacion cdCrmOpApl = new CD_CrmOportunidadesAplicacion();


                    #region "Desasociación de aplicaciones"
                    var aplicacionesRegistradas = cdCrmOpApl.ConsultarPorOportunidad(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, promocion.Id_Op.Value, sesion.Emp_Cnx_EF).ToList();
                    var aplicacionesAEliminar = (from o in aplicacionesRegistradas
                                                 where !promocion.Aplicaciones.Contains(o.Id_Apl)
                                                 select o).ToList();
                    var aplicacionesAEliminarIds = aplicacionesAEliminar.Select(a => a.Id_Apl).ToList();
                    var aplicacionesRegistradasIds = (from ar in aplicacionesRegistradas
                                                      select ar.Id_Apl).ToList();
                    var aplicacionesAInsertarIds = (from a in promocion.Aplicaciones
                                                    where !aplicacionesRegistradasIds.Contains(a)
                                                    select a).ToList();
                    if (aplicacionesAEliminar.Count > 0)
                    {
                        try
                        {
                            cdCrmOpApl.Eliminar(sesion.Id_Emp, sesion.Id_Cd, promocion.Id_Op.Value, aplicacionesAEliminar.Select(oa => oa.Id_Apl).ToArray(), contexto);
                        }
                        catch (Exception ex)
                        {
                            throw new ActualizarAplicacionesException();
                        }
                    }
                    #endregion

                    //Las aplicaciones se registran una por una para dar oportunidad a su registro exitoso aún y cuando emerja una condición de error en el proceso debido a una regla de base de datos.
                    List<int> faultyIdApls = new List<int>();

                    CrmOportunidades registros = new CrmOportunidades();

                    registros.Id_Emp = sesion.Id_Emp;
                    registros.Id_Cd = sesion.Id_Cd_Ver;
                    registros.Id_Ter = promocion.Territorio;
                    registros.Id_Cte = promocion.Cliente;
                    registros.ID_Area = promocion.Area;
                    registros.Id_Seg = promocion.Segmento;
                    registros.Id_Uen = promocion.Uen;
                    registros.Id_Sol = promocion.Solucion;
                    registros.Id_Op = promocion.Id_Op.Value;

                    int validador = 0;
                    foreach (var idApl in aplicacionesAInsertarIds)
                    {
                        try
                        {
                            cdCrmOpApl.Insertar(sesion.Id_Emp, sesion.Id_Cd, promocion.Id_Op.Value, idApl, contexto);

                            #region Estructura
                            try
                            {
                                registros.Id_Apl = idApl;
                                registros.Porcentaje = 0;
                                registros.Activo = true;
                                //Esta llamada debe de encontrarse dentro del contexto de la transacción.
                                ActualizaDimension(registros, sesion.Emp_Cnx, ref validador);
                            }
                            catch (Exception innerEx)
                            {

                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw new AplicacionNoAsociadaException(idApl);
                            //Se rastrea el identificador de la aplicación que fallo al tratar de asociarse al proyecto.
                            //faultyIdApls.Add(idApl);
                        }
                    }

                    contexto.Commit();

                    //Arrojar excepción de AplicacionesNoAsociadasException.
                    if (faultyIdApls.Count > 0)
                    {
                        throw new AplicacionesNoAsociadasException(faultyIdApls.ToArray());
                    }
                }
                else
                {
                    CD_CrmOportunidadesAplicacion cdCrmOpApl = new CD_CrmOportunidadesAplicacion();
                    var aplicacionesRegistradas = cdCrmOpApl.ConsultarPorOportunidad(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, promocion.Id_Op.Value, sesion.Emp_Cnx_EF).ToList();
                    try
                    {
                        cdCrmOpApl.Eliminar(sesion.Id_Emp, sesion.Id_Cd, promocion.Id_Op.Value, aplicacionesRegistradas.Select(oa => oa.Id_Apl).ToArray(), contexto);
                    }
                    catch (Exception ex)
                    {
                        throw new ActualizarAplicacionesException();
                    }
                }
                #endregion
            }
        }

        public void Actualizar(Sesion sesion, CrmPromociones promocion)
        {
            using (ICD_Contexto contexto = CD_FabricaContexto.CrearDefault(sesion.Emp_Cnx_EF))
            {
                //Se actualiza la información base
                CD_CrmPromocion cdCrmPromocion = new CD_CrmPromocion();
                try
                {

                    cdCrmPromocion.ActualizarPromocion(sesion.Id_Emp, sesion.Id_Cd, promocion, contexto);
                }
                catch (Exception ex)
                {
                    throw new ActualizarProyectoException();
                }

                //Se actualizan las asociaciones con las aplicaciones
                //Primero se eliminan las aplicaciones y después se recrean los registros
                //Se considera un esquema de ejecución transaccional
                #region "Asociación de aplicaciones al proyecto"

                //Se registran las aplicaciones en caso de que el valor de la propiedad [Aplicaciones] de [promocion] no sea nulo.
                if (promocion.Aplicaciones != null)
                {
                    CD_CrmOportunidadesAplicacion cdCrmOpApl = new CD_CrmOportunidadesAplicacion();


                    #region "Desasociación de aplicaciones"
                    var aplicacionesRegistradas = cdCrmOpApl.ConsultarPorOportunidad(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, promocion.Id, sesion.Emp_Cnx_EF).ToList();
                    var aplicacionesAEliminar = (from o in aplicacionesRegistradas
                                                 where promocion.Aplicaciones.Contains(o.Id_Apl)
                                                 select o).ToList();
                    var aplicacionesAEliminarIds = aplicacionesAEliminar.Select(a => a.Id_Apl).ToList();
                    var aplicacionesAInsertarIds = (from a in aplicacionesRegistradas
                                                    where aplicacionesAEliminarIds.Contains(a.Id_Apl)
                                                    select a.Id_Apl).ToList();
                    if (aplicacionesAEliminar.Count > 0)
                    {
                        try
                        {
                            cdCrmOpApl.Eliminar(sesion.Id_Emp, sesion.Id_Cd, promocion.Id, aplicacionesAEliminar.Select(oa => oa.Id_Apl).ToArray(), contexto);
                        }
                        catch (Exception ex)
                        {
                            throw new ActualizarAplicacionesException();
                        }
                    }
                    #endregion

                    //Las aplicaciones se registran una por una para dar oportunidad a su registro exitoso aún y cuando emerja una condición de error en el proceso debido a una regla de base de datos.
                    List<int> faultyIdApls = new List<int>();

                    CrmOportunidades registros = new CrmOportunidades();

                    registros.Id_Emp = sesion.Id_Emp;
                    registros.Id_Cd = sesion.Id_Cd_Ver;
                    registros.Id_Ter = promocion.Territorio;
                    registros.Id_Cte = promocion.Cliente;
                    registros.ID_Area = promocion.Area;
                    registros.Id_Seg = promocion.Segmento;
                    registros.Id_Uen = promocion.Uen;
                    registros.Id_Sol = promocion.Solucion;
                    registros.Id_Op = promocion.Id;

                    int validador = 0;
                    foreach (var idApl in aplicacionesAInsertarIds)
                    {
                        try
                        {
                            cdCrmOpApl.Insertar(sesion.Id_Emp, sesion.Id_Cd, promocion.Id, idApl, contexto);

                            #region Estructura
                            try
                            {
                                registros.Id_Apl = idApl;
                                registros.Porcentaje = 0;
                                registros.Activo = true;
                                //Esta llamada debe de encontrarse dentro del contexto de la transacción.
                                ActualizaDimension(registros, sesion.Emp_Cnx, ref validador);
                            }
                            catch (Exception innerEx)
                            {

                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            throw new AplicacionNoAsociadaException(idApl);
                            //Se rastrea el identificador de la aplicación que fallo al tratar de asociarse al proyecto.
                            //faultyIdApls.Add(idApl);
                        }
                    }

                    //Arrojar excepción de AplicacionesNoAsociadasException.
                    if (faultyIdApls.Count > 0)
                    {
                        throw new AplicacionesNoAsociadasException(faultyIdApls.ToArray());
                    }
                }
                #endregion
                contexto.Commit();
            }
        }

        public IEnumerable<CrmOportunidade> ObtenerPorClienteTerritorio(Sesion s, int idCte, int idTer)
        {
            CD_CrmPromocion cdCrmPromocion = new CD_CrmPromocion();
            return cdCrmPromocion.ConsultarPorClienteTerritorio(s.Id_Emp, s.Id_Cd, idCte, idTer, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Esta versión determina los proyectos disponibles para una valuación de un cliente en específico.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="promocion"></param>
        /// <param name="list"></param>
        public void ProyectosDisponiblesParaValuacion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> list)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaCatPromocion(sesion, promocion, ref list);
                CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                var proyectosEnValuacion = cnCrmOportunidad.ObtenerProyectosEnValuaciones(sesion, promocion.Cliente).ToList();
                list = list.Where(crmOp => proyectosEnValuacion.Where(op => op.Id_Op == crmOp.Id).Count() == 0).ToList();
                CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
                foreach (var p in list)
                {
                    p.CrmTipoCliente = cnCrmProspecto.ObtenerTipoCliente(sesion, p.Id_Cte);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Esta versión determina los proyectos disponibles para una valuación de un cliente en específico.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="promocion">Datos de la consulta</param>
        /// <param name="list">Lista de resultados</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void ProyectosDisponiblesParaValuacion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> list, IBusinessTransaction ibt)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaCatPromocion(sesion, promocion, ref list, ibt.DataContext);
                CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                var proyectosEnValuacion = cnCrmOportunidad.ObtenerProyectosEnValuaciones(sesion, promocion.Cliente, ibt);
                list = list.Where(crmOp => proyectosEnValuacion.Where(op => op.Id_Op == crmOp.Id).Count() == 0).ToList();
                CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
                foreach (var p in list)
                {
                    p.CrmTipoCliente = cnCrmProspecto.ObtenerTipoCliente(sesion, p.Id_Cte, ibt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Devuelve los proyectos que tienen asociado una valuación.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="promocion">Datos de la consulta</param>
        /// <param name="list">Lista de resultados</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void ProyectosConValuacion(Sesion sesion, CrmPromociones promocion, ref List<CrmPromociones> list, IBusinessTransaction ibt)
        {
            try
            {
                CD_CrmPromocion claseCRM = new CD_CrmPromocion();
                claseCRM.ConsultaCatPromocion(sesion, promocion, ref list, ibt.DataContext);
                CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                var proyectosEnValuacion = cnCrmOportunidad.ObtenerProyectosEnValuaciones(sesion, promocion.Cliente, ibt);
                list = list.Where(crmOp => proyectosEnValuacion.Where(op => op.Id_Op == crmOp.Id).Count() > 0 && !crmOp.Cancelado).ToList();
                CN_CrmProspecto cnCrmProspecto = new CN_CrmProspecto();
                foreach (var p in list)
                {
                    p.CrmTipoCliente = cnCrmProspecto.ObtenerTipoCliente(sesion, p.Id_Cte, ibt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected log4net.ILog Logger
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }

        }
    }

    public class AplicacionesNoAsociadasException : Exception
    {
        public AplicacionesNoAsociadasException(int[] idApls)
            : base(string.Format("Aplicaciones no asociadas al proyecto: {0}", string.Join(",", idApls)))
        {
            IdApls = idApls;
        }

        public AplicacionesNoAsociadasException(int[] idApls, Exception innerException)
            : base(string.Format("Aplicaciones no asociadas al proyecto: {0}", string.Join(",", idApls)), innerException)
        {
            IdApls = idApls;
        }

        public int[] IdApls
        {
            get;
            private set;
        }
    }

    public class AplicacionNoAsociadaException : Exception
    {
        public AplicacionNoAsociadaException(int idApl)
            : base(string.Format("Error al asociar la aplicación {0}", idApl))
        {
            IdApl = idApl;
        }

        public AplicacionNoAsociadaException(int idApl, Exception innerException)
            : base(string.Format("Error al asociar la aplicación {0}", idApl), innerException)
        {
            IdApl = idApl;
        }

        public int IdApl
        {
            get;
            private set;
        }
    }

    public class ActualizarAplicacionesException : Exception
    {
        public ActualizarAplicacionesException()
            : base("Error al actualizar las aplicaciones")
        {

        }
    }

    public class ActualizarProyectoException : Exception
    {
        public ActualizarProyectoException()
            : base("Error al actualizar la información del proyecto")
        {

        }
    }
}
