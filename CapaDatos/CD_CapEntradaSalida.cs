using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;
using System.Collections;

namespace CapaDatos
{
    public class CD_CapEntradaSalida
    {
        /// <summary>
        /// Guarda documento entrada salida. "Afecta" 1 remision, 2 orden de compra, 0 nada
        /// </summary>
        /// <param name="entradaSalida"></param>
        /// <param name="detalles"></param>
        /// <param name="sesion"></param>
        /// <param name="verificador"></param>
        /// <param name="tipo_movimiento"></param>
        /// <param name="grupoMovimientosActivo"></param>
        /// <param name="afecta">1 remision, 2 orden de compra, 0 nada</param>
        /// <param name="entrada"></param>
        /// <param name="actualizacionDeDocumento"></param>
        ///      


        private void borrarFisicamentePartidas(int Id_Emp, int Id_Cd_Ver, int Id_Es, int EsDet_Naturaleza, ref int verificador, string conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Es"
                                        ,"@EsDet_Naturaleza"
                                        
                                      };
                object[] Valores = { 
                                        Id_Emp
                                        ,Id_Cd_Ver
                                        ,Id_Es
                                        ,EsDet_Naturaleza                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("capEntSalDet_BorradoFisico", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarConsecutivo(Sesion sesion, int Es_Naturaleza, ref int consecutivo)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Es_Naturaleza" };
                object[] Valores = { sesion.Id_Emp,
                                     sesion.Id_Cd_Ver,
                                     Es_Naturaleza
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_ConsultaConsecutivo", ref consecutivo, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta el saldo de BiComodato(1), BiFacturado(2), EstArrendado(3), EstConsignacion(4), EstNoConforme(5), EstPendFacturar(6), EstProdPrueba(7)
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Cliente"></param>
        /// <param name="Id_Prd"></param>
        /// <param name="fecha"></param>
        /// <param name="resultado"></param>
        /// <param name="tipo_movimiento">BiComodato(1), BiFacturado(2), EstArrendado(3), EstConsignacion(4), EstNoConforme(5), EstPendFacturar(6), EstProdPrueba(7)</param>
        public void ConsultarSaldo(Sesion sesion, int Id_Cliente, int Id_Prd, DateTime fecha, int Id_Ter, ref int resultado,
                                    int tipo_movimiento, ref CapaDatos.CD_Datos CapaDatos, ref SqlCommand sqlcmd)
        {
            try
            {
                //CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Prd",
                                        "@Fecha"
                                        ,"@Id_Ter"
                                      };
                object[] Valores = { sesion.Id_Emp,
                                     sesion.Id_Cd_Ver,
                                     Id_Cliente,
                                     Id_Prd,
                                     fecha
                                     ,Id_Ter==-1?(object)null:Id_Ter
                                   };
                string sp = "";
                switch (tipo_movimiento)
                {
                    case 6:
                    case 15:
                    case 16:
                        sp = "BiComodato";
                        break;
                    case 14:
                        sp = "BiFacturado";
                        break;

                    default:
                        throw new Exception("Opcion no encontrada");
                    //break;
                }
                //SqlCommand 
                sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref resultado, Parametros, Valores);

                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEntradaSalida(Sesion sesion, int Id_Emp, int Id_Cd_Ver, int Id_Es, int Es_Naturaleza, ref EntradaSalida entradaSalida)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Es",
                                          "@Es_Naturaleza"
                                      };

                string[] Valores = {
                                       Id_Emp.ToString(),
                                       Id_Cd_Ver.ToString(),
                                       Id_Es.ToString(),
                                       Es_Naturaleza.ToString()
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Consulta", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    entradaSalida.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    entradaSalida.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    entradaSalida.Id_Es = dr.GetInt32(dr.GetOrdinal("Id_Es"));
                    entradaSalida.Id_U = dr.GetInt32(dr.GetOrdinal("Id_U"));
                    entradaSalida.Es_Naturaleza = dr.GetInt32(dr.GetOrdinal("Es_Naturaleza"));
                    switch (dr.GetInt32(dr.GetOrdinal("Es_Naturaleza")))
                    {
                        case 0:
                            entradaSalida.Es_NaturalezaStr = "Entrada";
                            break;
                        case 1:
                            entradaSalida.Es_NaturalezaStr = "Salida";
                            break;
                        default:
                            break;
                    }
                    entradaSalida.Es_FechaHr = dr.GetDateTime(dr.GetOrdinal("Es_FechaHr"));
                    entradaSalida.Es_Fecha = dr.GetDateTime(dr.GetOrdinal("Es_Fecha"));
                    entradaSalida.Id_Tm = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Tm"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Tm")));
                    entradaSalida.Id_Cte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cte")));
                    entradaSalida.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Pvd")));
                    entradaSalida.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ter")));
                    entradaSalida.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    entradaSalida.Es_Referencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Referencia"))) ? "" : dr.GetString(dr.GetOrdinal("Es_Referencia"));
                    entradaSalida.Es_Notas = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Notas"))) ? "" : dr.GetString(dr.GetOrdinal("Es_Notas"));
                    entradaSalida.Es_SubTotal = dr.GetDouble(dr.GetOrdinal("Es_SubTotal"));
                    entradaSalida.Es_Iva = dr.GetDouble(dr.GetOrdinal("Es_Iva"));
                    entradaSalida.Es_Total = dr.GetDouble(dr.GetOrdinal("Es_Total"));
                    entradaSalida.Es_Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Estatus"))) ? "" : dr.GetString(dr.GetOrdinal("Es_Estatus"));
                    entradaSalida.ManAut = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ManAut"))) ? false : dr.GetBoolean(dr.GetOrdinal("ManAut"));
                    entradaSalida.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "" : (string)(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));//Cte_NomComercial
                    entradaSalida.Es_CteCuentaNacional = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_CteCuentaNacional"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Es_CteCuentaNacional")));
                    entradaSalida.Es_CteCuentaContNacional = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_CteCuentaContNacional"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Es_CteCuentaContNacional")));
                    if (!Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_FechaReferencia"))))
                    {
                        entradaSalida.Es_FechaReferencia = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Es_FechaReferencia")));
                    }
                    break;
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEntradasSalidas(ref List<EntradaSalida> EntradasSalidas, ref EntradaSalida entsal, CapaEntidad.Sesion sesion,
            string NombreCliente, int ClienteIni, int ClienteFin, int ManAut, int ProveedorIni, int ProveedorFin,
            string Es_Referencia, DateTime? FechaIni, DateTime? FechaFin, string Estatus, int NumeroIni,
            int NumeroFin)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                            
                                          "@Id_U",
                                          "@propia",
                                          "@Cte_NomComercial",//NOMBRE CLIENTE
                                          "@clienteIni",
                                          "@clienteFin",
                                          "@ManAut",
                                          "@ProveedorIni",
                                          "@ProveedorFin",
                                          "@Es_Referencia",
                                          "@FechaIni",
                                          "@FechaFin",
                                          "@Estatus",
                                          "@NumeroIni",
                                          "@NumeroFin"
                                      };

                object[] Valores = {
                                       sesion.Id_Emp,
                                       sesion.Id_Cd_Ver,

                                       sesion.Id_U,
                                       sesion.Propia,
                                       NombreCliente==""?(object)null : NombreCliente,//NOMBRE CLIENTE @Cte_NomComercial
                                       ClienteIni == -1 ? (object)null : ClienteIni,
                                       ClienteFin == -1 ? (object)null : ClienteFin,
                                       ManAut == -1 ? (object)null : ManAut,
                                       ProveedorIni  == -1 ? (object)null : ProveedorIni,
                                       ProveedorFin == -1 ? (object)null : ProveedorFin,
                                       Es_Referencia == "" ? (object)null : Es_Referencia,
                                       FechaIni, 
                                       FechaFin,
                                       Estatus == "" ? (object)null : Estatus,
                                       NumeroIni == -1 ? (object)null : NumeroIni,
                                       NumeroFin == -1 ? (object)null : NumeroFin
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_ConsultaLista", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    entsal = new EntradaSalida();
                    entsal.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    entsal.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    entsal.Id_U = dr.GetInt32(dr.GetOrdinal("Id_U"));
                    entsal.Id_Es = dr.GetInt32(dr.GetOrdinal("Id_Es"));
                    entsal.Es_Naturaleza = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Naturaleza"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Es_Naturaleza"));
                    switch (entsal.Es_Naturaleza)
                    {
                        case 0:
                            entsal.Es_NaturalezaStr = "Entrada";
                            break;
                        case 1:
                            entsal.Es_NaturalezaStr = "Salida";
                            break;
                        default:
                            break;
                    }
                    entsal.Es_Fecha = dr.GetDateTime(dr.GetOrdinal("Es_Fecha"));
                    entsal.Id_Tm = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Tm"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Tm"));
                    entsal.Id_Cte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                    entsal.Id_CteStr = entsal.Id_Cte == -1 ? "N/A" : entsal.Id_Cte.ToString();
                    entsal.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "N/A" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));//dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    entsal.Id_Pvd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Pvd"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Pvd"));
                    entsal.Id_PvdStr = entsal.Id_Pvd == -1 ? "N/A" : entsal.Id_Pvd.ToString();
                    entsal.Pvd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Pvd_Descripcion"))) ? "N/A" : dr.GetString(dr.GetOrdinal("Pvd_Descripcion"));
                    entsal.Es_Referencia = dr.GetString(dr.GetOrdinal("Es_Referencia"));
                    entsal.Es_Notas = dr.GetString(dr.GetOrdinal("Es_Notas"));
                    entsal.Es_SubTotal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_SubTotal"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Es_SubTotal"));
                    entsal.Es_Iva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Iva"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Es_Iva"));
                    entsal.Es_Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Total"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Es_Total"));
                    entsal.Es_Estatus = dr.GetString(dr.GetOrdinal("Es_Estatus"));
                    switch (entsal.Es_Estatus.ToUpper())
                    {
                        case "C":
                            entsal.Es_EstatusStr = "Capturado";
                            break;
                        case "B":
                            entsal.Es_EstatusStr = "Baja";
                            break;
                        case "I":
                            entsal.Es_EstatusStr = "Impreso";
                            break;
                        case "1":
                            entsal.Es_EstatusStr = "Capturado";
                            break;
                        default:
                            break;
                    }
                    entsal.ManAut = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ManAut"))) ? false : dr.GetBoolean(dr.GetOrdinal("ManAut"));
                    switch (entsal.ManAut)
                    {
                        case true:
                            entsal.ManAutStr = "Manual";
                            break;
                        case false:
                            entsal.ManAutStr = "Automático";
                            break;
                        default:
                            break;
                    }
                    EntradasSalidas.Add(entsal);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEncabezadoImprimir(Sesion sesion, int Id_Emp, int Id_Cd_Ver, int Id_Es, int Es_Naturaleza, ref EntradaSalida entradaSalida)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Es",
                                          "@Es_Naturaleza"
                                      };

                string[] Valores = {
                                       Id_Emp.ToString(),
                                       Id_Cd_Ver.ToString(),
                                       Id_Es.ToString(),
                                       Es_Naturaleza.ToString()
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Impresion", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    entradaSalida.Es_Naturaleza = dr.GetInt32(dr.GetOrdinal("Es_Naturaleza"));
                    switch (dr.GetInt32(dr.GetOrdinal("Es_Naturaleza")))
                    {
                        case 0:
                            entradaSalida.Es_NaturalezaStr = "Entrada";
                            break;
                        case 1:
                            entradaSalida.Es_NaturalezaStr = "Salida";
                            break;
                        default:
                            break;
                    }
                    entradaSalida.Es_Fecha = dr.GetDateTime(dr.GetOrdinal("Es_Fecha"));
                    entradaSalida.Es_FechaHr = dr.GetDateTime(dr.GetOrdinal("Es_FechaHr"));
                    entradaSalida.Nombre_Cliente = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Nombre"));
                    entradaSalida.Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Calle"))) ? "" : dr.GetString(dr.GetOrdinal("Calle"));
                    entradaSalida.Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Numero"))) ? "" : dr.GetString(dr.GetOrdinal("Numero"));
                    entradaSalida.Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Colonia"))) ? "" : dr.GetString(dr.GetOrdinal("Colonia"));
                    entradaSalida.Municipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Municipio"))) ? "" : dr.GetString(dr.GetOrdinal("Municipio"));
                    entradaSalida.Estado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Estado"))) ? "" : dr.GetString(dr.GetOrdinal("Estado"));
                    entradaSalida.Id_Cte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cte")));
                    entradaSalida.Id_Tm = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Tm"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Tm")));
                    entradaSalida.Tm_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Tm_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Tm_Nombre"));
                    entradaSalida.Es_Referencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Referencia"))) ? "" : dr.GetString(dr.GetOrdinal("Es_Referencia"));
                    entradaSalida.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cd")));
                    entradaSalida.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ter")));
                    entradaSalida.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Rik"))); //dr.GetInt32(dr.GetOrdinal("Id_Rik"));
                    entradaSalida.Es_SubTotal = dr.GetDouble(dr.GetOrdinal("Es_SubTotal"));
                    entradaSalida.Es_Iva = dr.GetDouble(dr.GetOrdinal("Es_Iva"));
                    entradaSalida.Es_Total = dr.GetDouble(dr.GetOrdinal("Es_Total"));
                    entradaSalida.Es_Notas = dr.GetValue(dr.GetOrdinal("Es_Notas")).ToString();
                    entradaSalida.Es_CteCuentaNacional = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_CteCuentaNacional"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Es_CteCuentaNacional")));
                    entradaSalida.Es_CteCuentaContNacional = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_CteCuentaContNacional"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Es_CteCuentaContNacional")));
                    break;
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarEntradaSalida_Estatus(EntradaSalida entradaSalida, string conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Es"
                                        ,"@Es_Naturaleza"
                                        ,"@Es_Estatus"
                                      };
                object[] Valores = { 
                                        entradaSalida.Id_Emp
                                        ,entradaSalida.Id_Cd
                                        ,entradaSalida.Id_Es
                                        ,entradaSalida.Es_Naturaleza
                                        ,entradaSalida.Es_Estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_ModificarEstatus", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarDisponible(Sesion sesion, int Id_Prd, ref int Disponible, ref int invFinal, ref int asignado)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       Id_Prd.ToString()
                                       
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_consultardisponible", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    asignado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Asignado"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Asignado"));
                    invFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Inventario final"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Inventario final"));
                    Disponible = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Disponible"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Disponible"));
                    break;
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Recibe un objeto EntradaSalida y consulta sus detalles
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="entsal"></param>
        /// <param name="detalles"></param>
        public void ConsultarEntradaSalidaDetalles(Sesion sesion, EntradaSalida entsal, ref List<EntradaSalidaDetalle> detalles)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Es",
                                          "@EsDet_Naturaleza"
                                      };
                string[] Valores = {
                                       entsal.Id_Emp.ToString(),
                                       entsal.Id_Cd.ToString(),
                                       entsal.Id_Es.ToString(),
                                       entsal.Es_Naturaleza.ToString()                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Consulta", ref dr, parametros, Valores);
                EntradaSalidaDetalle EntSalDetalle = new EntradaSalidaDetalle();
                //tabla();
                while (dr.Read())
                {
                    EntSalDetalle = new EntradaSalidaDetalle();
                    EntSalDetalle.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    EntSalDetalle.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    EntSalDetalle.Id_Es = dr.GetInt32(dr.GetOrdinal("Id_Es"));
                    EntSalDetalle.Id_EsDet = dr.GetInt32(dr.GetOrdinal("Id_EsDet"));
                    EntSalDetalle.Id_EsDetStr = Guid.NewGuid().ToString();
                    EntSalDetalle.EsDet_Naturaleza = dr.GetInt32(dr.GetOrdinal("EsDet_Naturaleza"));
                    EntSalDetalle.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Ter"));
                    EntSalDetalle.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    EntSalDetalle.Id_Prd = dr.GetInt32(dr.GetOrdinal("Id_Prd"));
                    EntSalDetalle.Prd_AgrupadoSpo = dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    EntSalDetalle.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) ? "" : dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    EntSalDetalle.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? "" : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                    EntSalDetalle.Prd_Unidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Unine"))) ? "" : dr.GetString(dr.GetOrdinal("Prd_Unine"));
                    EntSalDetalle.Presentacion = EntSalDetalle.Prd_Presentacion;
                    EntSalDetalle.Es_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Cantidad"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Es_Cantidad"));
                    EntSalDetalle.Es_Costo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Es_Costo"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Es_Costo"));
                    EntSalDetalle.Es_BuenEstado = dr.GetBoolean(dr.GetOrdinal("Es_BuenEstado"));
                    EntSalDetalle.Afct_OrdCompra = dr.GetBoolean(dr.GetOrdinal("EsDet_AfcOrdCom"));
                    EntSalDetalle.Importe = EntSalDetalle.Es_Cantidad * EntSalDetalle.Es_Costo;
                    EntSalDetalle.TipoSalida = dr.GetInt32(dr.GetOrdinal("EsDet_TipoSalida"));
                    EntSalDetalle.ConceptoTipoSalida = dr.GetInt32(dr.GetOrdinal("EsDet_ConceptoTipoSalida"));
                    // dt.Rows.Add(EntSalDetalle);
                    detalles.Add(EntSalDetalle);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BajaEntradaSalida(ref EntradaSalida entradaSalida, ref List<EntradaSalidaDetalle> detalles, Sesion sesion, ref int verificador,
                                       int afecta, bool entrada, bool actualizacionDocumento/*si es true , no hay control de transacciones*/, ref CapaDatos.CD_Datos CapaDatos)
        {
            try
            {
                if (!actualizacionDocumento)
                {
                    CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                    CapaDatos.StartTrans();
                }
                //Se afecta la entrada salida, cambiando el estatus a baja
                SqlCommand sqlcmd = new SqlCommand();
                string[] Parametros = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Es"
                                        ,"@Es_Naturaleza"
                                        ,"@Es_Estatus"
                                        ,"@Id_UCancelo"
                                      };
                object[] Valores = { 
                                        entradaSalida.Id_Emp
                                        ,entradaSalida.Id_Cd
                                        ,entradaSalida.Id_Es
                                        ,entradaSalida.Es_Naturaleza
                                        ,entradaSalida.Es_Estatus
                                        ,entradaSalida.Id_U
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_ModificarEstatus", ref verificador, Parametros, Valores);
                verificador = -1;
                //parametros del detalle
                string[] Parametros2 = {
                                            "@Id_Emp",
                                            "@Id_Cd",	
                                            "@Id_Es",
                                            "@Id_EsDet",
                                            "@EsDet_Naturaleza",
                                            "@Id_Prd",
                                            "@Es_Cantidad",
                                            "@Afct_OrdCompra",
                                            "@Id_Rem",
                                            "@afectaRemision",
                                            "@Entrada" 
                                       };
                //para cada detalle de la lista
                foreach (EntradaSalidaDetalle detalle in detalles)
                {
                    object[] Valores2 = {
                                                    detalle.Id_Emp,
                                                    detalle.Id_Cd,
                                                    detalle.Id_Es,///
                                                    detalle.Id_EsDet,
                                                    entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                    detalle.Id_Prd,
                                                    detalle.Es_Cantidad,
                                                    detalle.Afct_OrdCompra,
                                                    entradaSalida.Es_Referencia,
                                                    afecta,
                                                    entrada
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_baja", ref verificador, Parametros2, Valores2);
                    #region comentarios
                    //switch (grupoMovimientosActivo)
                    //{
                    //    case 1:
                    //    case 2:
                    //    case 3:
                    //        saldo = 0;
                    //        //se consulta el saldo
                    //        ConsultarSaldo(sesion, entradaSalida.Id_Cte, detalle.Id_Prd, entradaSalida.Es_Fecha, ref saldo, tipo_movimiento);
                    //        if (saldo > 0)
                    //        {
                    //            //se inserta el detalle
                    //            sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_eliminar", ref verificador, Parametros2, Valores2);
                    //        }
                    //        else
                    //        {
                    //            string msg = "";
                    //            switch (tipo_movimiento)
                    //            {
                    //                case 6:
                    //                case 15:
                    //                case 16:
                    //                    msg = "Base instalada comodato";
                    //                    break;
                    //                case 14:
                    //                    msg = "Base instalada facturado";
                    //                    break;
                    //                case 18:
                    //                    msg = "Estadistica arrendado";
                    //                    break;
                    //                case 7:
                    //                    msg = "Estadistica consignacion";
                    //                    break;
                    //                case 13:
                    //                    msg = "Estadistica no conforme";
                    //                    break;
                    //                case 11:
                    //                    msg = "Estadistica pendiente por facturar";
                    //                    break;
                    //                case 12:
                    //                    msg = "Estadistica productos a prueba";
                    //                    break;
                    //                default:
                    //                    throw new Exception("Opcion no encontrada");
                    //                    break;
                    //            }
                    //            throw new Exception("El saldo de " + msg + " es insuficiente");
                    //        }
                    //        break;
                    //    case 4:
                    //    case 0:
                    //        //se inserta el detalle
                    //        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificador, Parametros2, Valores2);                            
                    //        break;        
                    //    default:
                    //        break;
                    //}
                    #endregion
                }
                if (!actualizacionDocumento)
                {
                    CapaDatos.CommitTrans();
                    //CapaDatos.RollBackTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                if (!actualizacionDocumento)
                {
                    CapaDatos.RollBackTrans();
                }
                throw ex;
            }
        }

        public void Bitacora_BajaRemisionesSIAN(ref EntradaSalida entSal, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                // ---------------------------
                // Insertar detalle
                // ---------------------------
                string[] ParametrosDetalleEntSalRem = {
                                    "@Id_Emp",
                                    "@Id_Cd",
                                    "@Id_Es",
                                    "@Id_EsDet",
                                    "@EsDet_Naturaleza",
                                    "@Id_Ter",
                                    "@Id_Prd",
                                    "@Es_Cantidad",
                                    "@Es_Costo",
                                    "@Es_BuenEstado",
                                    "@EsDet_AfcOrdCom",
                                    "@Es_Referencia",    //OGC
                                    "@Es_CantidadTotal", //OGC
                                    "@Es_Secuencia"      //OGC
                                    };
                int cont = 0;
                int secuencia = 1;
                foreach (EntradaSalidaDetalle entSalRemDetalle in entSal.ListaDetalle)
                {
                    // ----------------------------------------
                    // Insertar detalle de Entrada-Salida
                    // ----------------------------------------
                    if (entSalRemDetalle.Es_Cantidad > 0)
                    {
                        object[] ValoresDetalleEntSalRem = {    
                                        entSalRemDetalle.Id_Emp
                                        ,entSalRemDetalle.Id_Cd
                                        ,entSal.Id_Es
                                        ,cont
                                        ,entSalRemDetalle.EsDet_Naturaleza
                                        ,(object)null
                                        ,entSalRemDetalle.Id_Prd
                                        ,entSalRemDetalle.Es_Cantidad
                                        ,entSalRemDetalle.Es_Costo
                                        ,entSalRemDetalle.Es_BuenEstado
                                        ,entSalRemDetalle.Afct_OrdCompra
                                        ,entSal.Es_Referencia//OGC
                                        ,entSalRemDetalle.Es_CantidadRem//OGC
                                        ,secuencia//OGC
                                        };

                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_BajaRemisionSIAN_Bitacora", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                        cont++;
                    }
                    secuencia++;
                }
                verificador = 0;
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarEntradaSalida_BajaRemisionesSIAN(ref EntradaSalida entSal, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] ParametrosEntSalRem = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_U"
                                        ,"@Es_Naturaleza"
                                        ,"@Es_Fecha"
                                        ,"@Id_Tm"
                                        ,"@Id_Cte"
                                        ,"@Id_Pvd"
                                        ,"@Es_Referencia"
                                        ,"@Es_Notas"
                                        ,"@Es_Subtotal"
                                        ,"@Es_Iva"
                                        ,"@Es_Total"
                                        ,"@Es_Estatus"
                                        ,"@ManAut"
                                    };
                object[] ValoresEntSalRem = {    
                                        entSal.Id_Emp
                                        ,entSal.Id_Cd
                                        ,entSal.Id_U
                                        ,entSal.Es_Naturaleza
                                        ,entSal.Es_Fecha
                                        ,entSal.Id_Tm
                                        ,(object)null //entSal.Id_Cte
                                        ,entSal.Id_Pvd == -1 ? (object)null : entSal.Id_Pvd
                                        ,entSal.Es_Referencia //referencia factura
                                        ,entSal.Es_Notas
                                        ,entSal.Es_SubTotal
                                        ,entSal.Es_Iva
                                        ,entSal.Es_Total
                                        ,entSal.Es_Estatus
                                        ,0
                                        };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_BajaRemisionSIAN_Insertar", ref verificador, ParametrosEntSalRem, ValoresEntSalRem);
                entSal.Id_Es = verificador; //clave (folio) de entrada-salida generado
                if (entSal.Id_Es != -1)
                {
                    // ---------------------------
                    // Insertar detalle
                    // ---------------------------
                    string[] ParametrosDetalleEntSalRem = {
                                    "@Id_Emp",
                                    "@Id_Cd",
                                    "@Id_Es",
                                    "@Id_EsDet",
                                    "@EsDet_Naturaleza",
                                    "@Id_Ter",
                                    "@Id_Prd",
                                    "@Es_Cantidad",
                                    "@Es_Costo",
                                    "@Es_BuenEstado",
                                    "@EsDet_AfcOrdCom",
                                    "@Es_Referencia",//OGC
                                    "@Es_CantidadTotal",//OGC
                                    "@Es_Secuencia"//OGC
                                    };
                    int cont = 0;
                    int secuencia = 1;
                    foreach (EntradaSalidaDetalle entSalRemDetalle in entSal.ListaDetalle)
                    {
                        // ----------------------------------------
                        // Insertar detalle de Entrada-Salida
                        // ----------------------------------------
                        if (entSalRemDetalle.Es_Cantidad > 0)
                        {
                            object[] ValoresDetalleEntSalRem = {    
                                        entSalRemDetalle.Id_Emp
                                        ,entSalRemDetalle.Id_Cd
                                        ,entSal.Id_Es
                                        ,cont
                                        ,entSalRemDetalle.EsDet_Naturaleza
                                        ,(object)null
                                        ,entSalRemDetalle.Id_Prd
                                        ,entSalRemDetalle.Es_Cantidad
                                        ,entSalRemDetalle.Es_Costo
                                        ,entSalRemDetalle.Es_BuenEstado
                                        ,entSalRemDetalle.Afct_OrdCompra
                                        ,entSal.Es_Referencia//OGC
                                        ,entSalRemDetalle.Es_CantidadRem//OGC
                                        ,secuencia//OGC
                                        };

                            sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_BajaRemisionSIAN_InsertarPrevio", ref verificador, ParametrosDetalleEntSalRem, ValoresDetalleEntSalRem);
                            cont++;
                        }
                        secuencia++;
                    }
                    if (entSal.ListaDetalle.Count == cont)
                    {
                        CapaDatos.CommitTrans();
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    }
                    else
                    {
                        CapaDatos.RollBackTrans();
                        verificador = 2;
                        return;
                    }

                }
                else
                {
                    CapaDatos.RollBackTrans();
                    return;
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ConsultarDisponible(EntradaSalida entsal, string Conexion, int producto, int cantidad, ref string verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Es_Cantidad", 
                                          "@Id_Prd"
                                      };
                object[] Valores = { 
                                       entsal.Id_Emp, 
                                       entsal.Id_Cd,
                                       cantidad,
                                       producto
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Disponible", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarSaldo(int Id_Emp, int Id_Cd, string Id_PRd, string Id_Ter, string Id_Cte, string Conexion, ref int verificador, string Id_Tm)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Cte", 
                                          "@Id_Prd",
                                          "@Fecha",
                                          "@Id_Ter"
                                      };
                object[] Valores = { 
                                       Id_Emp, 
                                       Id_Cd,
                                       Id_Cte,
                                       Id_PRd,
                                       DateTime.Now,
                                       Id_Ter
                                   };
                SqlCommand sqlcmd;
                if (Id_Tm == "14")
                {
                    sqlcmd = CapaDatos.GenerarSqlCommand("BiFacturado", ref verificador, Parametros, Valores);


                    //NO DEBE CHECAR ESTE SP CUANDO EL PRODUCTO NO SEA SISTEMA PROPIETARIO, O AS QUE EEL SP REVUELVA UN NUMERO MUY GRANDE 999999999
                }
                else
                {
                    sqlcmd = CapaDatos.GenerarSqlCommand("BiComodato", ref verificador, Parametros, Valores);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarEntradaSalida(ref EntradaSalida entradaSalida, ref List<EntradaSalidaDetalle> detalles, Sesion sesion,
                                            ref int verificador, int tipo_movimiento, int grupoMovimientosActivo, int afecta, bool entrada,
                                            DataTable preciosModificar, ref string verificadorStr, int VGEmpresa)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();


                int Id_Es = 0;

                string[] Parametros = { 
                                          "@Id_Emp",
	                                      "@Id_Cd",	
                                          "@Id_U",
	                                      "@Es_Naturaleza", 
	                                      "@Es_Fecha",
	                                      "@Id_Tm",
	                                      "@Id_Cte", 
	                                      "@Id_Pvd",
	                                      "@Es_Referencia",
	                                      "@Es_Notas",
	                                      "@Es_Subtotal",
	                                      "@Es_Iva",
	                                      "@Es_Total",
	                                      "@Es_Estatus"
                                          ,"@ManAut"
                                          ,"@Es_CteCuentaNacional"
                                          ,"@Es_CteCuentaContNacional"
                                      };
                object[] Valores = { 
                                        sesion.Id_Emp,
                                        sesion.Id_Cd_Ver,
                                        sesion.Id_U,
                                        entradaSalida.Es_Naturaleza,
                                        entradaSalida.Es_Fecha,
                                        entradaSalida.Id_Tm,
                                        ((entradaSalida.Id_Cte==-1)?(object)null:entradaSalida.Id_Cte),
                                        ((entradaSalida.Id_Pvd==-1)?(object)null:entradaSalida.Id_Pvd), 
                                        entradaSalida.Es_Referencia,
                                        entradaSalida.Es_Notas,
                                        entradaSalida.Es_SubTotal,
                                        entradaSalida.Es_Iva,
                                        entradaSalida.Es_Total,
                                        entradaSalida.Es_Estatus
                                        ,1 //MANUAL
                                        , entradaSalida.Es_CteCuentaNacional
                                        , entradaSalida.Es_CteCuentaContNacional
                                   };
                //inserta una entradaSalida
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref Id_Es, Parametros, Valores);
                // // }
                //parametros del detalle
                string[] Parametros2 = {
                                            "@Id_Emp",
                                            "@Id_Cd",	
                                            "@Id_Es",
                                            "@Id_EsDet",
                                            "@EsDet_Naturaleza",
                                            "@Id_Ter",
                                            "@Es_BuenEstado",
                                            "@Id_Prd",
                                            "@Es_Cantidad",
                                            "@Es_Costo",
                                            "@Afct_OrdCompra",
                                            "@Prd_AgrupadoSpo",
                                            "@Id_Rem",
                                            "@afectaRemision",
                                            "@Entrada"
                                            ,"@Id_Pvd"
                                            ,"@EsDet_TipoSalida"
                                            ,"@EsDet_ConceptoTipoSalida"
		                                };
                if (Id_Es != 0)
                {
                    int saldo = 0;
                    //para cada detalle de la lista
                    foreach (EntradaSalidaDetalle detalle in detalles)
                    {
                        object[] Valores2 = {
                                                detalle.Id_Emp,
                                                detalle.Id_Cd,
                                                Id_Es,
                                                detalle.Id_EsDet,
                                                entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                detalle.Id_Ter==-1?(object)null:detalle.Id_Ter,
                                                detalle.Es_BuenEstado,
                                                detalle.Id_Prd,
                                                detalle.Es_Cantidad,
                                                detalle.Es_Costo,
                                                detalle.Afct_OrdCompra,
                                                detalle.Prd_AgrupadoSpo,
                                                entradaSalida.Es_Referencia
                                                ,afecta
                                                ,entrada
                                                ,entradaSalida.Id_Pvd
                                                ,detalle.TipoSalida
                                                ,detalle.ConceptoTipoSalida

		                                    };

                        switch (grupoMovimientosActivo)
                        {
                            case 1:
                            case 2:
                                saldo = 0;
                                //se consulta el saldo
                                ConsultarSaldo(sesion, entradaSalida.Id_Cte, detalle.Id_Prd, entradaSalida.Es_Fecha, detalle.Id_Ter,
                                                ref saldo, tipo_movimiento, ref CapaDatos, ref sqlcmd);
                                if (detalle.Es_Cantidad > saldo)
                                {
                                    throw new Exception("El producto " + detalle.Id_Prd.ToString() + " no tiene saldo suficiente");
                                }
                                //se inserta el detalle
                                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificador, Parametros2, Valores2);
                                break;
                            case 3:
                                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificador, Parametros2, Valores2);
                                break;
                            case 4:
                                //se inserta el detalle
                                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificadorStr, Parametros2, Valores2);
                                if (VGEmpresa != sesion.Id_Emp)
                                {//validacion bennetts
                                    if (preciosModificar != null && preciosModificar.Rows.Count > 0)
                                    {
                                        foreach (DataRow row in preciosModificar.Rows)
                                        {
                                            string[] Parametros1 = { 
	                                                        "@Id_Emp", 
                                                            "@Id_Cd", 
	                                                        "@Id_Prd", 
                                                            "@Id_Pre",
                                                            "@Prd_Actual",
                                                            "@Prd_FechaInicio",
                                                            "@Prd_FechaFin",
                                                            "@Prd_PreDescripcion",
                                                            "@Prd_Pesos"
                                          };
                                            object[] Valores1 = { 
                                                            row["Id_Emp"]//productoPrecios.Id_Emp
                                                            ,row["Id_Cd"]//,productoPrecios.Id_Cd
                                                            ,row["Id_Prd"]//,productoPrecios.Id_Prd
                                                            ,row["Id_Pre"]//,productoPrecios.Id_Pre
                                                            ,row["Prd_Actual"]//,productoPrecios.Prd_Actual
                                                            ,DateTime.Parse(row["Prd_FechaInicio"].ToString())//,productoPrecios.Prd_FechaInicio
                                                            ,DateTime.Parse(row["Prd_FechaFin"].ToString())//,productoPrecios.Prd_FechaFin
                                                            ,row["Prd_PreDescripcion"]//,productoPrecios.Prd_PreDescripcion
                                                            ,row["Prd_Pesos"]//,productoPrecios.Prd_Pesos
                                            };
                                            sqlcmd = CapaDatos.GenerarSqlCommand("spProductoPrecios_Modificar2", ref verificador, Parametros1, Valores1);
                                        }
                                    }
                                }
                                break;
                            case 0:
                                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificador, Parametros2, Valores2);
                                break;
                            default:
                                break;
                        }
                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void GuardarEntradaSalida(EntradaSalida entradaSalida, List<EntradaSalidaDetalle> listaDetalle, ref string verificadorStr, int strEmp, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(Conexion);
            try
            {
                //CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();

                int Id_Es = 0;
                int Id_Es_Dispara_Entrada = 0;
                int Id_Es_Dispara_Salida = 0;

                string[] Parametros = { 
                                          "@Id_Emp",
	                                      "@Id_Cd",	
                                          "@Id_U",
	                                      "@Es_Naturaleza", 
	                                      "@Es_Fecha",
	                                      "@Id_Tm",
	                                      "@Id_Cte", 
	                                      "@Id_Pvd",
	                                      "@Es_Referencia",
	                                      "@Es_Notas",
	                                      "@Es_Subtotal",
	                                      "@Es_Iva",
	                                      "@Es_Total",
	                                      "@Es_Estatus",
                                          "@ManAut",
                                          "@Id_Ter"
                                          ,"@Es_CteCuentaNacional"
                                          ,"@Es_CteCuentaContNacional"
                                          ,"@Es_FechaReferencia"
                                          ,"@Id_Ord"
                                      };
                //CapaDatos.StartTrans();
                object[] Valores = { 
                                        entradaSalida.Id_Emp,
                                        entradaSalida.Id_Cd,
                                        entradaSalida.Id_U,
                                        entradaSalida.Es_Naturaleza,
                                        entradaSalida.Es_Fecha,
                                        entradaSalida.Id_Tm,
                                        ((entradaSalida.Id_Cte==-1) ? (object)null: entradaSalida.Id_Cte),
                                        ((entradaSalida.Id_Pvd==-1) ? (object)null: entradaSalida.Id_Pvd), 
                                        entradaSalida.Es_Referencia,
                                        entradaSalida.Es_Notas,
                                        entradaSalida.Es_SubTotal,
                                        entradaSalida.Es_Iva,
                                        entradaSalida.Es_Total,
                                        entradaSalida.Es_Estatus
                                        ,1 //MANUAL
                                        ,entradaSalida.Id_Ter == -1 ? (object)null: entradaSalida.Id_Ter
                                        ,entradaSalida.Es_CteCuentaNacional
                                        ,entradaSalida.Es_CteCuentaContNacional
                                        ,entradaSalida.Es_FechaReferencia
                                        ,entradaSalida.Id_Ord
                                   };
                //inserta una entradaSalida
                SqlDataReader dr = null;
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Insertar", ref dr, Parametros, Valores);


                if (dr.HasRows)
                {
                    dr.Read();
                    Id_Es = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Es")));
                    Id_Es_Dispara_Entrada = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Es_Dispara_Entrada")));
                    Id_Es_Dispara_Salida = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Es_Dispara_Salida")));
                }

                dr.Close();
                //CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);


                entradaSalida.Id_Es = Id_Es;

                Parametros = new string[]{
                                            "@Id_Emp",  
                                            "@Id_Cd",  
                                            "@Id_Es",  
                                            "@Id_EsDet",  
                                            "@EsDet_Naturaleza",  
                                            "@Id_Tm",
                                            "@Id_Ter",  
                                            "@Es_BuenEstado",  
                                            "@Id_Prd",  
                                            "@Es_Cantidad",  
                                            "@Es_Costo",  
                                            "@Afct_OrdCompra", 
                                            "@Afct_Precios", 
                                            "@Prd_AgrupadoSpo",  
                                            "@Id_Ref", 
                                            "@Id_Pvd", 
                                            "@Id_Cte", 
                                            "@Fecha",
                                            "@Id_Es_Dispara_Entrada",
                                            "@Id_Es_Dispara_Salida",
                                            "@EsDet_TipoSalida",
                                            "@EsDet_ConceptoTipoSalida"

		                                };
                int Id_EsDet = 0;

                SqlCommand sqlcmd1 = new SqlCommand();

                CapaDatos1.StartTrans();

                foreach (EntradaSalidaDetalle detalle in listaDetalle)
                {
                    Valores = new object[]{
                                                detalle.Id_Emp,
                                                detalle.Id_Cd,
                                                entradaSalida.Id_Es,
                                                Id_EsDet++,
                                                entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                entradaSalida.Id_Tm,
                                                detalle.Id_Ter==-1?(object)null:detalle.Id_Ter,
                                                detalle.Es_BuenEstado,
                                                detalle.Id_Prd,
                                                detalle.Es_Cantidad,
                                                detalle.Es_Costo,
                                                detalle.Afecta,
                                                strEmp != detalle.Id_Emp,
                                                detalle.Prd_AgrupadoSpo,
                                                entradaSalida.Es_Referencia
                                                ,entradaSalida.Id_Pvd
                                                ,entradaSalida.Id_Cte
                                                ,entradaSalida.Es_Fecha
                                                ,Id_Es_Dispara_Entrada
                                                ,Id_Es_Dispara_Salida
                                                ,detalle.TipoSalida
                                                ,detalle.ConceptoTipoSalida
		                                    };
                    sqlcmd1 = CapaDatos1.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificadorStr, Parametros, Valores);


                }

                CapaDatos1.CommitTrans();
                CapaDatos1.LimpiarSqlcommand(ref sqlcmd1);

            }

            catch (Exception ex)
            {
                //CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void EditarEntradaSalida(ref EntradaSalida entradaSalida, ref List<EntradaSalidaDetalle> detalles, Sesion sesion, ref int verificador, int tipo_movimiento, int grupoMovimientosActivo, int afecta, bool entrada, DataTable preciosModificar, ref string verificadorStr, int VGEmpresa)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                int Id_Es = 0;
                SqlCommand sqlcmd = new SqlCommand();
                List<EntradaSalidaDetalle> detalles_modificar = new List<EntradaSalidaDetalle>();
                ConsultarEntradaSalidaDetalles(sesion, entradaSalida, ref detalles_modificar);
                string[] Parametros_Modificar = {
                                            "@Id_Emp",
                                            "@Id_Cd",	
                                            "@Id_Es",
                                            "@Id_EsDet",
                                            "@EsDet_Naturaleza",
                                            "@Id_Prd",
                                            "@Es_Cantidad",
                                            "@Afct_OrdCompra",
                                            "@Id_Rem",
                                            "@afectaRemision",
                                            "@Entrada"
                                            ,"@grupo"
                                            ,"@CantidadAdd"
                                            ,"@Id_Pvd"
                                            ,"@Id_Ter"
                                            ,"@BuenEstado"
                                            ,"@Costo"
                                            
                                        };
                // tabla();

                foreach (EntradaSalidaDetalle detalle in detalles_modificar)
                {//modificado                   
                    int existe = 1;
                    int CantidadAdd = 0;
                    foreach (EntradaSalidaDetalle detalle2 in detalles)
                    {//guardado en tabla                           
                        if (entradaSalida.Es_Naturaleza == 0)
                        {
                            if (detalle2.Id_Prd == detalle.Id_Prd)
                            {
                                CantidadAdd += detalle2.Es_Cantidad;
                                existe = 0;
                            }
                        }
                    }

                    if (existe == 0)
                    {
                        object[] Valores_Modificar = {
                                                    detalle.Id_Emp,
                                                    detalle.Id_Cd,
                                                    entradaSalida.Id_Es,///
                                                    detalle.Id_EsDet,
                                                    entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida                                                   
                                                    detalle.Id_Prd,
                                                    detalle.Es_Cantidad,
                                                    detalle.Afct_OrdCompra,
                                                    entradaSalida.Es_Referencia,
                                                    afecta,
                                                    entrada
                                                    ,grupoMovimientosActivo
                                                    ,CantidadAdd
                                                    ,((entradaSalida.Id_Pvd==-1)?(object)null:entradaSalida.Id_Pvd)
                                                    ,detalle.Id_Ter==-1?(object)null:detalle.Id_Ter
                                                    ,detalle.Es_BuenEstado
                                                    ,detalle.Es_Costo
                                                    ,detalle.Prd_AgrupadoSpo                                                  
                                            };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Modificar", ref verificador, Parametros_Modificar, Valores_Modificar);
                    }
                    else
                    {
                        string[] Parametros_Eliminar = {
                                            "@Id_Emp",
                                            "@Id_Cd",	
                                            "@Id_Es",
                                            "@Id_EsDet",
                                            "@EsDet_Naturaleza",
                                            "@Id_Prd",
                                            "@Es_Cantidad",
                                            "@Afct_OrdCompra",
                                            "@Id_Rem",
                                            "@afectaRemision",
                                            "@Entrada"
                                            ,"@grupo"
                                            ,"@CantidadAdd"                                           
                                        };
                        object[] Valores_Eliminar = {
                                                            detalle.Id_Emp,
                                                            detalle.Id_Cd,
                                                            detalle.Id_Es,///
                                                            detalle.Id_EsDet,
                                                            entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                            detalle.Id_Prd,
                                                            detalle.Es_Cantidad,
                                                            detalle.Afct_OrdCompra,
                                                            entradaSalida.Es_Referencia,
                                                            afecta,
                                                            entrada
                                                            ,grupoMovimientosActivo
                                                            ,CantidadAdd                                                    
                                                    };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_baja", ref verificador, Parametros_Eliminar, Valores_Eliminar);

                        string[] Parametros_borrar = { 
                                                                        "@Id_Emp"
                                                                        ,"@Id_Cd"
                                                                        ,"@Id_Es"
                                                                        ,"@Id_EsDet"
                                                                        ,"@EsDet_Naturaleza"                                        
                                                                      };
                        object[] Valores_borrar = { 
                                                                        entradaSalida.Id_Emp
                                                                        ,entradaSalida.Id_Cd
                                                                        ,entradaSalida.Id_Es
                                                                        ,detalle.Id_EsDet
                                                                        ,entradaSalida.Es_Naturaleza                                        
                                                                   };
                        sqlcmd = CapaDatos.GenerarSqlCommand("capEntSalDet_BorradoFisico", ref verificador, Parametros_borrar, Valores_borrar);

                    }

                }

                foreach (EntradaSalidaDetalle detalle in detalles)
                {//nuevo   
                    int nuevo = 0;
                    int CantidadAdd = 0;
                    foreach (EntradaSalidaDetalle detalle2 in detalles_modificar)
                    {//guardado en tabla              
                        CantidadAdd = 0;
                        if (entradaSalida.Es_Naturaleza == 0)
                        {
                            CantidadAdd += detalle.Es_Cantidad;
                            if (detalle2.Id_Prd == detalle.Id_Prd)
                            {
                                nuevo = 1;
                            }
                        }
                    }
                    if (nuevo == 0)
                    {

                        string[] Parametros2 = {
                                            "@Id_Emp",
                                            "@Id_Cd",	
                                            "@Id_Es",
                                            "@Id_EsDet",
                                            "@EsDet_Naturaleza",
                                            "@Id_Ter",
                                            "@Es_BuenEstado",
                                            "@Id_Prd",
                                            "@Es_Cantidad",
                                            "@Es_Costo",
                                            "@Afct_OrdCompra",
                                            "@Prd_AgrupadoSpo",
                                            "@Id_Rem",
                                            "@afectaRemision",
                                            "@Entrada"
                                            ,"@Id_Pvd"
                                            ,"@EsDet_TipoSalida"
                                            ,"@EsDet_ConceptoTipoSalida"
		                                };

                        object[] Valores2 = {
                                                detalle.Id_Emp,
                                                detalle.Id_Cd,
                                                entradaSalida.Id_Es,
                                                detalle.Id_EsDet,
                                                entradaSalida.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                detalle.Id_Ter==-1?(object)null:detalle.Id_Ter,
                                                detalle.Es_BuenEstado,
                                                detalle.Id_Prd,
                                                detalle.Es_Cantidad,
                                                detalle.Es_Costo,
                                                detalle.Afct_OrdCompra,
                                                detalle.Prd_AgrupadoSpo,
                                                entradaSalida.Es_Referencia
                                                ,afecta
                                                ,entrada
                                                ,entradaSalida.Id_Pvd
                                                ,detalle.TipoSalida
                                                ,detalle.ConceptoTipoSalida
		                                    };

                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificador, Parametros2, Valores2);
                    }
                }

                Id_Es = entradaSalida.Id_Es;
                String[] Param = { 
                                         "@Id_Emp", 
                                         "@Id_Cd", 
                                         "@Id_Es", 
                                         "@Es_Naturaleza", 
                                         "@Es_Notas",
                                         "@Es_Subtotal",
                                         "@Es_Iva",
                                         "@Es_Total"
                                     };
                object[] val = {  
                                       sesion.Id_Emp,
                                        sesion.Id_Cd_Ver,
                                        Id_Es,
                                        entradaSalida.Es_Naturaleza,
                                        entradaSalida.Es_Notas,
                                        entradaSalida.Es_SubTotal,
                                        entradaSalida.Es_Iva,
                                        entradaSalida.Es_Total
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Modificar", ref verificador, Param, val);

                String[] Param1 = { 
                                         "@Id_Emp", 
                                         "@Id_Cd", 
                                         "@Id_Es", 
                                         "@Es_Naturaleza"
                                     };
                object[] val1 = {  
                                       sesion.Id_Emp,
                                        sesion.Id_Cd_Ver,
                                        Id_Es,
                                        entradaSalida.Es_Naturaleza
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Orden", ref verificador, Param1, val1);

                verificador = 0;
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Inventario", ref verificador, Param1, val1);
                if (verificador != 0)
                {
                    throw new Exception(" El producto # " + verificador + ", no se puede modificar o eliminar no cuenta con inventario suficiente.");
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void ConsultaTMov(ref Movimientos mov, string Conexion, string bdCentral)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                mov.Id = -1;

                string[] Parametros = { "@Empresa", "@Id_TProv", "@Tm_NatMov", "bdCentral", "@Id_Cd" };
                object[] Valores = { mov.Id_Emp, mov.Tipo, mov.NatMov, bdCentral, mov.Id_Cd };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spMovimientosxProveedor_Consulta", ref dr, Parametros, Valores);



                if (dr.HasRows)
                {
                    dr.Read();

                    mov.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm")));
                    mov.Afecta = 2;
                    mov.Estatus = true;
                    mov.EstatusStr = "Activo";

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void ConsultaTProveedor(ref Movimientos mov, string Conexion, string bdCentral)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Empresa", "@Id_Pvd", "@Id_Cd", "bdCentral" };
                object[] Valores = { mov.Id_Emp, mov.Id, mov.Id_Cd, bdCentral };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spTipoProveedor_Consulta", ref dr, Parametros, Valores);


                if (dr.HasRows)
                {
                    dr.Read();
                    mov.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tpvd")));

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void EdicionEntradaSalida(EntradaSalida entsal, List<EntradaSalidaDetalle> listaDetalle, string verificadorStr, int strEmp, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();

                int verificador = 0;
                String[] Parametros = { 
                                         "@Id_Emp", 
                                         "@Id_Cd", 
                                         "@Id_Es", 
                                         "@Es_Naturaleza", 
                                         "@Es_Notas",
                                         "@Es_Subtotal",
                                         "@Es_Iva",
                                         "@Es_Total"
                                         ,"@Es_CteCuentaNacional"
                                         ,"@Es_CteCuentaContNacional"
                                         ,"@Es_FechaReferencia"
                                     };
                object[] Valores = {  
                                       entsal.Id_Emp,
                                        entsal.Id_Cd,
                                        entsal.Id_Es,
                                        entsal.Es_Naturaleza,
                                        entsal.Es_Notas,
                                        entsal.Es_SubTotal,
                                        entsal.Es_Iva,
                                        entsal.Es_Total
                                        ,entsal.Es_CteCuentaNacional
                                        , entsal.Es_CteCuentaContNacional
                                        ,entsal.Es_FechaReferencia
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSal_Modificar", ref verificador, Parametros, Valores);

                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Es" };
                Valores = new object[] { entsal.Id_Emp, entsal.Id_Cd, entsal.Id_Es };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_BajaTemporal", ref verificador, Parametros, Valores);

                Parametros = new string[]{
                                            "@Id_Emp",  
                                            "@Id_Cd",  
                                            "@Id_Es",  
                                            "@Id_EsDet",  
                                            "@EsDet_Naturaleza",  
                                            "@Id_Tm",
                                            "@Id_Ter",  
                                            "@Es_BuenEstado",  
                                            "@Id_Prd",  
                                            "@Es_Cantidad",  
                                            "@Es_Costo",  
                                            "@Afct_OrdCompra", 
                                            "@Afct_Precios", 
                                            "@Prd_AgrupadoSpo",  
                                            "@Id_Ref", 
                                            "@Id_Pvd", 
                                            "@Id_Cte", 
                                            "@Fecha",
                                            "@EsDet_TipoSalida",
                                            "@EsDet_ConceptoTipoSalida"
		                                };
                int Id_EsDet = 0;
                foreach (EntradaSalidaDetalle detalle in listaDetalle)
                {
                    Valores = new object[]{
                                                detalle.Id_Emp,
                                                detalle.Id_Cd,
                                                entsal.Id_Es,
                                                Id_EsDet++,
                                                entsal.Es_Naturaleza,//<-- naturaleza del movimiento EntradaSalida
                                                entsal.Id_Tm,
                                                detalle.Id_Ter==-1?(object)null:detalle.Id_Ter,
                                                detalle.Es_BuenEstado,
                                                detalle.Id_Prd,
                                                detalle.Es_Cantidad,
                                                detalle.Es_Costo,
                                                detalle.Afecta,
                                                strEmp != detalle.Id_Emp,
                                                detalle.Prd_AgrupadoSpo,
                                                entsal.Es_Referencia
                                                ,entsal.Id_Pvd
                                                ,entsal.Id_Cte
                                                ,entsal.Es_Fecha
                                                ,detalle.TipoSalida
                                                ,detalle.ConceptoTipoSalida
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapEntSalDet_Insertar", ref verificadorStr, Parametros, Valores);
                }

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }

        }
    }
}
