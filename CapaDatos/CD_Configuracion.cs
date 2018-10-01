using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_Configuracion
    {
        public void Consulta(ref ConfiguracionGlobal Configuracion, string Conexion)
        {
            try
            {
                using (dbAccess oDB = new dbAccess(Conexion))
                {
                    DataSet DS = oDB.spExecDataSet(
                        "spSysConfiguracion_Consulta",
                        new SqlParameter("@Id_Cd", Configuracion.Id_Cd),
                        new SqlParameter("@Id_Emp", Configuracion.Id_Emp)
                    );

                    if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                    {
                        DataRow item = DS.Tables[0].Rows[0];
                        Configuracion.Solicitud_Prospecto = item["Solicitud_Prospecto"] == null ? false : item["Solicitud_Prospecto"] == "0" ? false : true;
                        Configuracion.Hora_Zona = (String)item["Hora_Zona"];
                        Configuracion.Hora_Verano = item["Hora_Verano"] == null ? false : item["Hora_Verano"] == "0" ? false : true;
                        Configuracion.Mail_Servidor = (String)item["Mail_Servidor"];
                        Configuracion.Mail_Usuario = (String)item["Mail_Usuario"];
                        Configuracion.Mail_Contraseña = (String)item["Mail_Contraseña"];
                        Configuracion.Mail_Puerto = (String)item["Mail_Puerto"];
                        Configuracion.Mail_Remitente = (String)item["Mail_Remitente"];
                        Configuracion.Login_Intentos = (String)item["Login_Intentos"];
                        Configuracion.Login_Tiempo_Bloqueo = (String)item["Login_Tiempo_Bloqueo"];
                        Configuracion.Contraseña_Tiempo_Vida = (String)item["Contraseña_Tiempo_Vida"];
                        Configuracion.Contraseña_Long_Min = (String)item["Contraseña_Long_Min"];
                        Configuracion.Mail_CompLocal = item["Mail_CompLocal"] == null ? "" : (String)item["Mail_CompLocal"];
                        Configuracion.Mail_PrecioEsp = item["Mail_PrecioEsp"] == null ? "" : (String)item["Mail_PrecioEsp"];
                        Configuracion.Mail_BaseInstalada = item["Mail_Bi"] == null ? "" : (String)item["Mail_Bi"];
                        Configuracion.Mail_Acys = item["Mail_Acys"] == null ? "" : (String)item["Mail_Acys"];
                        Configuracion.Mail_EVirtual = item["Mail_EVirtual"] == null ? "" : (String)item["Mail_EVirtual"];
                        Configuracion.Mail_Valuacion = item["Mail_CorreoValuacion"] == null ? "" : (String)item["Mail_CorreoValuacion"];
                        Configuracion.Mail_MinValuacion = item["Mail_MinValuacion"] == null ? 0 : Convert.ToDouble(item["Mail_MinValuacion"]);

                        Configuracion.Mail_GastosProveedores = item["Mail_GastosProveedores"] == DBNull.Value ? "" : (String)item["Mail_GastosProveedores"];
                        Configuracion.Mail_GastosAcreedores = item["Mail_GastosAcreedores"] == DBNull.Value ? "" : (String)item["Mail_GastosAcreedores"];
                        Configuracion.Mail_GastosComprasLocales = item["Mail_GastosComprasLocales"] == DBNull.Value ? "" : (String)item["Mail_GastosComprasLocales"];
                        Configuracion.Mail_GastosFletes = item["Mail_GastosFletes"] == DBNull.Value ? "" : (String)item["Mail_GastosFletes"];
                        Configuracion.Mail_GastosNoInventariables = item["Mail_GastosNoInventariables"] == DBNull.Value ? "" : (String)item["Mail_GastosNoInventariables"];
                        Configuracion.Mail_GastosPagoServicios = item["Mail_GastosPagoServicios"] == DBNull.Value ? "" : (String)item["Mail_GastosPagoServicios"];
                        Configuracion.Mail_GastosOtrosPagos = item["Mail_GastosOtrosPagos"] == DBNull.Value ? "" : (String)item["Mail_GastosOtrosPagos"];
                        Configuracion.Mail_GastosReposicionCaja = item["Mail_GastosReposicionCaja"] == DBNull.Value ? "" : (String)item["Mail_GastosReposicionCaja"];
                        Configuracion.Mail_GastosCuentaGastos = item["Mail_GastosCuentaGastos"] == DBNull.Value ? "" : (String)item["Mail_GastosCuentaGastos"];
                        Configuracion.Mail_GastosComprobacion = item["Mail_GastosComprobacion"] == DBNull.Value ? "" : (String)item["Mail_GastosComprobacion"];
                        Configuracion.Mail_GastosAvisoGerente = item["Mail_GastosAvisoGerente"] == DBNull.Value ? "" : (String)item["Mail_GastosAvisoGerente"];
                        Configuracion.Mail_GastosAvisoUsuario = item["Mail_GastosAvisoUsuario"] == DBNull.Value ? "" : (String)item["Mail_GastosAvisoUsuario"];
                        //jfcv 12 sep 2016
                        Configuracion.Mail_GastosHonorarios = item["Mail_GastosHonorarios"] == DBNull.Value ? "" : (String)item["Mail_GastosHonorarios"];
                        Configuracion.Mail_GastosArrendamientos = item["Mail_GastosArrendamientos"] == DBNull.Value ? "" : (String)item["Mail_GastosArrendamientos"];
                        Configuracion.Mail_OrdenCompra_sisprop = item["Mail_OrdenCompra_sisprop"] == DBNull.Value ? "" : (String)item["Mail_OrdenCompra_sisprop"];
                        Configuracion.Mail_TransferenciasCedis = item["Mail_TransferenciasCedis"] == DBNull.Value ? "" : (String)item["Mail_TransferenciasCedis"];

                        //jfcv 14 oct 2016
                        //Configuracion.Mail_AutorizaReFacturas = item["Mail_AutorizaReFacturas"] == DBNull.Value ? "" : (String)item["Mail_AutorizaReFacturas"];
                        //Configuracion.Mail_ResponsableReFacturas = item["Mail_ResponsableReFacturas"] == DBNull.Value ? "" : (String)item["Mail_ResponsableReFacturas"];
                        //jfcv 26oct2016 que se lea la configuración de remisiones especiales 
                        //Configuracion.Remisiones_Especiales = item["Remisiones_Especiales"] == null ? false : item["Remisiones_Especiales"].ToString() == "0" ? false : true;

                        Configuracion.Mail_GastosComprobacionCompras = item["Mail_GastosComprobacionCompras"] == DBNull.Value ? "" : (String)item["Mail_GastosComprobacionCompras"];

                        Configuracion.Cuenta_GastosComprobacion = item["Cuenta_GastosComprobacion"] == DBNull.Value ? "" : (String)item["Cuenta_GastosComprobacion"];
                        Configuracion.Cuenta_GastosComprobacionCompras = item["Cuenta_GastosComprobacionCompras"] == DBNull.Value ? "" : (String)item["Cuenta_GastosComprobacionCompras"];

                        //RBM se agrega correo para autorizar cancelacion de facturas de periodos pasados
                        Configuracion.Mail_AutorizaBajaFactura = item["Mail_AutorizaBajaFactura"] == DBNull.Value ? "" : (String)item["Mail_AutorizaBajaFactura"];
                        //RBM

                        // Hervin De La Cruz Betancourt - 05/04/18 - Correo de aprobadores de cambio de territorio
                        Configuracion.CorreAprobadoresCambiosTerriotrio = item["Mail_AprobadorCambioTerriotrio"] == DBNull.Value ? "" : (String)item["Mail_AprobadorCambioTerriotrio"];
                        //RBM se agregan correos para el modulo de quejas
                        Configuracion.Mail_PlaneacioyCompras = item["Mail_PlaneacioyCompras"] == DBNull.Value ? "" : (String)item["Mail_PlaneacioyCompras"];
                        Configuracion.Mail_AbastoyEntregas = item["Mail_AbastoyEntregas"] == DBNull.Value ? "" : (String)item["Mail_AbastoyEntregas"];
                        Configuracion.Mail_ServicioCliente = item["Mail_ServicioCliente"] == DBNull.Value ? "" : (String)item["Mail_ServicioCliente"];
                        Configuracion.Mail_OperacionesCEDIS = item["Mail_OperacionesCEDIS"] == DBNull.Value ? "" : (String)item["Mail_OperacionesCEDIS"];
                        Configuracion.Mail_CXCFranquicias = item["Mail_CXCFranquicias"] == DBNull.Value ? "" : (String)item["Mail_CXCFranquicias"];
                        Configuracion.Mail_ValidarCorreos = item["Mail_ValidarCorreos"] == DBNull.Value ? "" : (String)item["Mail_ValidarCorreos"];
                        //RBM

                        //RBM se agrega correos para el modulo de Clientes en territorios
                        Configuracion.Mail_Autorizaterritorios = item["Mail_Autorizaterritorios"] == DBNull.Value ? "" : (String)item["Mail_Autorizaterritorios"];
                        //RBM
                    }
                    else
                    {
                        Configuracion = new ConfiguracionGlobal();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            /*
            try
            {
                SqlDataReader SqlDr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd", "@Id_Emp" };
                object[] Valores = { Configuracion.Id_Cd, Configuracion.Id_Emp };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysConfiguracion_Consulta", ref SqlDr, Parametros, Valores);
                if (SqlDr.HasRows == true)
                {
                    SqlDr.Read();
                    Configuracion.Solicitud_Prospecto = Convert.ToBoolean(Convert.ToInt32(SqlDr.GetValue(SqlDr.GetOrdinal("Solicitud_Prospecto"))));
                    Configuracion.Hora_Zona = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Hora_Zona"));
                    Configuracion.Hora_Verano = Convert.ToBoolean(Convert.ToInt32(SqlDr.GetValue(SqlDr.GetOrdinal("Hora_Verano"))));
                    Configuracion.Mail_Servidor = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Servidor"));
                    Configuracion.Mail_Usuario = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Usuario"));
                    Configuracion.Mail_Contraseña = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Contraseña"));
                    Configuracion.Mail_Puerto = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Puerto"));
                    Configuracion.Mail_Remitente = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Remitente"));
                    Configuracion.Login_Intentos = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Login_Intentos"));
                    Configuracion.Login_Tiempo_Bloqueo = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Login_Tiempo_Bloqueo"));
                    Configuracion.Contraseña_Tiempo_Vida = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Contraseña_Tiempo_Vida"));
                    Configuracion.Contraseña_Long_Min = (String)SqlDr.GetValue(SqlDr.GetOrdinal("Contraseña_Long_Min"));
                    Configuracion.Mail_CompLocal = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_CompLocal")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_CompLocal"));
                    Configuracion.Mail_PrecioEsp = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_PrecioEsp")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_PrecioEsp"));
                    Configuracion.Mail_BaseInstalada = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_Bi")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Bi"));
                    Configuracion.Mail_Acys = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_Acys")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_Acys"));
                    Configuracion.Mail_EVirtual = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_EVirtual")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_EVirtual"));
                    Configuracion.Mail_Valuacion = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_CorreoValuacion")) ? "" : (String)SqlDr.GetValue(SqlDr.GetOrdinal("Mail_CorreoValuacion"));
                    Configuracion.Mail_MinValuacion = SqlDr.IsDBNull(SqlDr.GetOrdinal("Mail_MinValuacion")) ? 0 : Convert.ToDouble(SqlDr.GetValue(SqlDr.GetOrdinal("Mail_MinValuacion")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            */
        }

        public void Modificar(ref ConfiguracionGlobal Configuracion, string Conexion)
        {

            using (dbAccess oDB = new dbAccess(Conexion))
            {
                oDB.BeginTransaction();
                try
                {
                    oDB.spExecNonQuery(
                        "spSysConfiguracion_Modificar",
                        new SqlParameter("@Id_Cd", Configuracion.Id_Cd),
                        new SqlParameter("@Id_Emp", Configuracion.Id_Emp),
                        new SqlParameter("@Solicitud_Prosp", Configuracion.Solicitud_Prospecto),
                        new SqlParameter("@Hora_Zona", Configuracion.Hora_Zona),
                        new SqlParameter("@Hora_Verano", Configuracion.Hora_Verano),
                        new SqlParameter("@Mail_Servidor", Configuracion.Mail_Servidor),
                        new SqlParameter("@Mail_Usuario", Configuracion.Mail_Usuario),
                        new SqlParameter("@Mail_Contraseña", Configuracion.Mail_Contraseña),
                        new SqlParameter("@Mail_Puerto", Configuracion.Mail_Puerto),
                        new SqlParameter("@Mail_Remitente", Configuracion.Mail_Remitente),
                        new SqlParameter("@Login_Intentos", Configuracion.Login_Intentos),
                        new SqlParameter("@Login_Tiempo_Bloqueo", Configuracion.Login_Tiempo_Bloqueo),
                        new SqlParameter("@Contraseña_Tiempo_Vida", Configuracion.Contraseña_Tiempo_Vida),
                        new SqlParameter("@Contraseña_Long_Min", Configuracion.Contraseña_Long_Min),
                        new SqlParameter("@Mail_CompLocal", Configuracion.Mail_CompLocal),
                        new SqlParameter("@Mail_PrecioEsp", Configuracion.Mail_PrecioEsp),
                        new SqlParameter("@Mail_Bi", Configuracion.Mail_BaseInstalada),
                        new SqlParameter("@Mail_CorreoValuacion", Configuracion.Mail_Valuacion),
                        new SqlParameter("@Mail_MinValuacion", Configuracion.Mail_MinValuacion),
                        new SqlParameter("@Mail_Acys", Configuracion.Mail_Acys),
                        new SqlParameter("@Mail_EVirtual", Configuracion.Mail_EVirtual),
                        new SqlParameter("@Mail_GastosProveedores", Configuracion.Mail_GastosProveedores),
                        new SqlParameter("@Mail_GastosAcreedores", Configuracion.Mail_GastosAcreedores),
                        new SqlParameter("@Mail_GastosComprasLocales", Configuracion.Mail_GastosComprasLocales),
                        new SqlParameter("@Mail_GastosFletes", Configuracion.Mail_GastosFletes),
                        new SqlParameter("@Mail_GastosNoInventariables", Configuracion.Mail_GastosNoInventariables),
                        new SqlParameter("@Mail_GastosPagoServicios", Configuracion.Mail_GastosPagoServicios),
                        new SqlParameter("@Mail_GastosOtrosPagos", Configuracion.Mail_GastosOtrosPagos),
                        new SqlParameter("@Mail_GastosReposicionCaja", Configuracion.Mail_GastosReposicionCaja),
                        new SqlParameter("@Mail_GastosCuentaGastos", Configuracion.Mail_GastosCuentaGastos),
                        new SqlParameter("@Mail_GastosComprobacion", Configuracion.Mail_GastosComprobacion),
                        new SqlParameter("@Mail_GastosAvisoGerente", Configuracion.Mail_GastosAvisoGerente),
                        new SqlParameter("@Mail_GastosAvisoUsuario", Configuracion.Mail_GastosAvisoUsuario),
                        new SqlParameter("@Mail_GastosHonorarios", Configuracion.Mail_GastosHonorarios),
                        new SqlParameter("@Mail_GastosArrendamientos", Configuracion.Mail_GastosArrendamientos),
                        new SqlParameter("@Mail_OrdenCompra", Configuracion.Mail_OrdenCompra),
                        //jfcv 14 oct 2016
                        //new SqlParameter("@Mail_AutorizaReFacturas", Configuracion.Mail_AutorizaReFacturas),
                        //new SqlParameter("@Mail_ResponsableReFacturas", Configuracion.Mail_ResponsableReFacturas),
                        //jfcv 26oct2016 agregar configuración para remisiones especiales 
                        //new SqlParameter("@Remisiones_Especiales", Configuracion.Remisiones_Especiales),
                        new SqlParameter("@Mail_GastosComprobacionCompras", Configuracion.Mail_GastosComprobacionCompras),
                        new SqlParameter("@Cuenta_GastosComprobacion", Configuracion.Cuenta_GastosComprobacion),
                        new SqlParameter("@Cuenta_GastosComprobacionCompras", Configuracion.Cuenta_GastosComprobacionCompras),

                        //Hervin De La Cruz Betancourt - 05/04/18 - Cambio correo de aprobadores de cambio de territorios

                        new SqlParameter("@Mail_AprobadorCambioTerriotrio", Configuracion.CorreAprobadoresCambiosTerriotrio),

                        new SqlParameter("@Mail_AutorizaBajaFactura", Configuracion.Mail_AutorizaBajaFactura),

                        //RBM correos para modulo de quejas 
                        new SqlParameter("@Mail_PlaneacioyCompras",Configuracion.Mail_PlaneacioyCompras),
                        new SqlParameter("@Mail_AbastoyEntregas",Configuracion.Mail_AbastoyEntregas),
                        new SqlParameter("@Mail_ServicioCliente",Configuracion.Mail_ServicioCliente),
                        new SqlParameter("@Mail_OperacionesCEDIS",Configuracion.Mail_OperacionesCEDIS),
                        new SqlParameter("@Mail_CXCFranquicias",Configuracion.Mail_CXCFranquicias),
                        new SqlParameter("@Mail_ValidarCorreos",Configuracion.Mail_ValidarCorreos),

                        //RBM correo para modulo de clientes en el apartado de territorios
                        new SqlParameter("@Mail_Autorizaterritorios", Configuracion.Mail_Autorizaterritorios)




                    );

                    oDB.Commit();
                }
                catch (Exception ex)
                {
                    oDB.RollBack();
                    throw ex;
                }
                finally
                {
                    oDB.Dispose();
                }
            }

            //    try
            //    {
            //        //SqlDataReader SqlDr = null;
            //        CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            //        string[] Parametros = {
            //    "@Id_Cd",
            //    "@Id_Emp",
            //    "@Solicitud_Prosp",
            //    "@Hora_Zona",
            //    "@Hora_Verano",
            //    "@Mail_Servidor",
            //    "@Mail_Usuario",
            //    "@Mail_Contraseña",
            //    "@Mail_Puerto",
            //    "@Mail_Remitente",
            //    "@Login_Intentos",
            //    "@Login_Tiempo_Bloqueo",
            //    "@Contraseña_Tiempo_Vida",
            //    "@Contraseña_Long_Min",
            //    "@Mail_CompLocal",
            //    "@Mail_PrecioEsp",
            //    "@Mail_Bi",
            //    "@Mail_CorreoValuacion",
            //    "@Mail_MinValuacion",
            //    "@Mail_Acys",
            //    "@Mail_EVirtual"
            //};

            //        object[] Valores = {
            //    Configuracion.Id_Cd,
            //    Configuracion.Id_Emp,
            //    Configuracion.Solicitud_Prospecto,
            //    Configuracion.Hora_Zona,
            //    Configuracion.Hora_Verano,
            //    Configuracion.Mail_Servidor,
            //    Configuracion.Mail_Usuario,
            //    Configuracion.Mail_Contraseña,
            //    Configuracion.Mail_Puerto,
            //    Configuracion.Mail_Remitente,
            //    Configuracion.Login_Intentos,
            //    Configuracion.Login_Tiempo_Bloqueo,
            //    Configuracion.Contraseña_Tiempo_Vida,
            //    Configuracion.Contraseña_Long_Min,
            //    Configuracion.Mail_CompLocal,
            //    Configuracion.Mail_PrecioEsp,
            //    Configuracion.Mail_BaseInstalada,
            //    Configuracion.Mail_Valuacion,
            //    Configuracion.Mail_MinValuacion,
            //    Configuracion.Mail_Acys,
            //    Configuracion.Mail_EVirtual
            //};

            //        SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand_Nonquery("spSysConfiguracion_Modificar", Parametros, Valores);

            //        CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
        }

        /// <summary>
        /// Consulta y devuelve una instancia de la entidad [SysConfiguracion] asociada al identificador idConf.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idConf">Identificador de configuración</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato compatible con Entity Framework</param>
        /// <returns>SysConfiguracion</returns>
        public SysConfiguracion Consultar(int idEmp, int idCd, int idConf, string cadenaConexionEF)
        {
            SysConfiguracion resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var resultados = (from sc in ctx.SysConfiguracions
                                  where sc.Id_Emp == idEmp && sc.Id_Cd == idCd && sc.Id_Conf == idConf
                                  select sc).ToList();
                if (resultados.Count > 0)
                {
                    resultado = resultados[0];
                }
            }
            return resultado;
        }

        /// <summary>
        /// Consulta y devuelve una instancia de la entidad [SysConfiguracion] asociada al identificador idConf. Versión transaccional.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idConf">Identificador de configuración</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>SysConfiguracion</returns>
        public SysConfiguracion Consultar(int idEmp, int idCd, int idConf, ICD_Contexto icdCtx)
        {
            SysConfiguracion resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var resultados = (from sc in ctx.SysConfiguracions
                              where sc.Id_Emp == idEmp && sc.Id_Cd == idCd && sc.Id_Conf == idConf
                              select sc).ToList();
            if (resultados.Count > 0)
            {
                resultado = resultados[0];
            }
            return resultado;
        }
    }
}
