using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using CapaModelo;
using CapaEntidad;
using System.IO;

namespace CapaDatos
{
    public class CD_CapRemision
    {

        public bool ConsultaRemisionFacturacion(ref Remision remision, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                bool existe = false;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rem" };
                object[] Valores = { remision.Id_Emp, remision.Id_Cd, remision.Id_Rem };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionFacturacion_Consultar", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    remision.Rem_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rem_Fecha")));
                    remision.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    remision.NombreCliente = dr.GetValue(dr.GetOrdinal("NombreCliente")).ToString();
                    remision.Rem_Estatus = dr.GetValue(dr.GetOrdinal("Rem_Estatus")).ToString();
                    switch (remision.Rem_Estatus.ToLower())
                    {
                        case "c":
                            remision.Rem_EstatusStr = "Capturado";
                            break;
                        case "b":
                            remision.Rem_EstatusStr = "Baja";
                            break;
                        case "i":
                            remision.Rem_EstatusStr = "Impreso";
                            break;
                        case "n":
                            remision.Rem_EstatusStr = "Entregado";
                            break;
                        case "e":
                            remision.Rem_EstatusStr = "Embarque";
                            break;
                    }
                    existe = true;
                }

                dr.Close();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDetalleFacturacion_Consultar", ref dr, Parametros, Valores);
                remision.ListRemisionDetalle = new List<RemisionDet>();
                while (dr.Read())
                {
                    RemisionDet remisionDet = new RemisionDet();
                    remisionDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    remisionDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    remisionDet.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    remisionDet.Id_RemDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_RemDet")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter")))) remisionDet.Id_Ter = null; else remisionDet.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    remisionDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    remisionDet.Rem_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Asignar")))) remisionDet.Rem_Asignar = null; else remisionDet.Rem_Asignar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Asignar")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_CantE")))) remisionDet.Rem_CantE = null; else remisionDet.Rem_CantE = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_CantE")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_CantF")))) remisionDet.Rem_CantF = null; else remisionDet.Rem_CantF = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_CantF")));
                    remisionDet.Rem_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Precio")));

                    //si la cantidad facturada es nula o igual a la cantidad, se agrega el producto porque tiene producto pendiente a facturar
                    if (remisionDet.Rem_CantF == null)
                    {
                        remision.ListRemisionDetalle.Add(remisionDet);
                    }
                    else
                    {
                        if (Convert.ToInt32(remisionDet.Rem_CantF) < remisionDet.Rem_Cant)
                        {
                            remision.ListRemisionDetalle.Add(remisionDet);
                        }
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return existe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionesVencidas(Sesion session, Remision pRem, ref List<RemisionesVencidas> pRemList)
        {
            try
            {
                SqlDataReader dr = null;
                Funciones vFuncs = new Funciones();
                CapaDatos.CD_Datos CapaDatos = new CD_Datos(session.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@Id_Cte", "@FechaIni", "@FechaFin", "@TipoRemision" };
                object[] Valores = { pRem.Id_Emp, pRem.Id_Cd, pRem.Id_Ter, pRem.Id_Cte, pRem.Rem_Fecha, pRem.Rem_Fecha, pRem.Id_Tm };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_RemisionesVencidas", ref dr, Parametros, Valores);

                pRemList = vFuncs.GetEntityList<RemisionesVencidas>(dr);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ConsultaCantidadRemision(Sesion sesion, int prd, string remisiones)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd", "@Id_Rem" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd, prd, remisiones };
                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacRem_ConsultaCantidad", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionDetalleContratoComodato(ref Remision remision, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rem" };
                object[] Valores = { remision.Id_Emp, remision.Id_Cd, remision.Id_Rem };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDetalleContratoCom_Consultar", ref dr, Parametros, Valores);
                remision.ListRemisionDetalle = new List<RemisionDet>();
                while (dr.Read())
                {
                    RemisionDet remisionDet = new RemisionDet();
                    remisionDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    remisionDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    remisionDet.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    remisionDet.Id_RemDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_RemDet")));
                    remisionDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    remisionDet.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    remisionDet.Rem_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    remision.ListRemisionDetalle.Add(remisionDet);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionDetalleFacturacion(ref Remision remision, ref DataTable listaFacrutaRemision, string Id_Rem_Lista, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rem_Lista" };
                object[] Valores = { remision.Id_Emp, remision.Id_Cd, Id_Rem_Lista };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturacionRemision_Consultar", ref dr, Parametros, Valores);
                int count = 0;
                while (dr.Read())
                {
                    //listaFacrutaRemision.Rows.Add(
                    listaFacrutaRemision.Rows.Add(new object[] {
                    null, count,
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm"))),
                    0,
                    Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd"))),
                    dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString(),
                    dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString(),
                    dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString(),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant"))),
                    Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Rem_Precio"))),
                    (Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Rem_Cant"))) * Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Rem_Precio")))),
                    Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ter_Nombre"))) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString(),
                    string.Empty,
                    0,
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")))
                    });
                    count++;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDevolucionRemisionDetalleFacturacionAgrupado(ref Remision remision, ref DataTable listaFacrutaRemision, string Id_Rem_Lista, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Ter", "@Id_Tm", "@Id_Rem_Lista", "@Id_TG" };
                object[] Valores = { remision.Id_Emp, remision.Id_Cd, remision.Id_Cte, remision.Id_Ter, remision.Id_Tm, Id_Rem_Lista, remision.IdTg };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturacionRemisionDevolucionAgrupado_Consultar", ref dr, Parametros, Valores);
                int count = 0;
                while (dr.Read())
                {
                    //listaFacrutaRemision.Rows.Add(

                    Double precio = 0;
                    Double Total = 0;

                    if (remision.Id_Tm == 92)
                    {
                        precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PrecioAAA")));
                        Total = precio * Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    }
                    else
                    {
                        Total = (Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Cant"))) * Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Rem_Precio"))));
                        precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Precio")));

                    }


                    listaFacrutaRemision.Rows.Add(new object[] {
                    null, count,
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm"))),
                    0,
                    Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd"))),
                    dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString(),
                    dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString(),
                    dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString(),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant"))),
                    precio,
                    Total,
                    Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ter_Nombre"))) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString(),
                    string.Empty,
                    0,
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd"))),
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    dr.GetValue(dr.GetOrdinal("Remisiones")).ToString(),
                    null,
                    null,
                    null,
                    null,
                    precio* Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")))
                    });
                    count++;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaDevolucionRemisionDetalleFacturacion(ref Remision remision, ref DataTable listaFacrutaRemision, string Id_Rem_Lista, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Ter", "@Id_Tm", "@Id_Rem_Lista" };
                object[] Valores = { remision.Id_Emp, remision.Id_Cd, remision.Id_Cte, remision.Id_Ter, remision.Id_Tm, Id_Rem_Lista };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturacionRemisionDevolucion_Consultar", ref dr, Parametros, Valores);
                int count = 0;
                while (dr.Read())
                {
                    //listaFacrutaRemision.Rows.Add(
                    listaFacrutaRemision.Rows.Add(new object[] {
                    null, count,
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tm"))),
                    0,
                    Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd"))),
                    dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString(),
                    dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString(),
                    dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString(),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant"))),
                    Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Rem_Precio"))),
                    (Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Rem_Cant"))) * Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Rem_Precio")))),
                    Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ter_Nombre"))) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString(),
                    string.Empty,
                    0,
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")))
                    });
                    count++;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCantidadRemisionesCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spCapRemisionCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta un numero de referencia(Id_Rem o Id_Fac) y regresa la cantidad de resultados encontrados que 
        /// no sea estatus cancelado (can) ni capturado (cap). 1 remision, 2 factura
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="referencia"></param>
        /// <param name="verificador"></param>
        /// <param name="tipo_documento">1 remision, 2 factura</param>
        public void ConsultarReferencia(Sesion sesion, int referencia, int Id_Tm, ref string verificador, int cliente)
        {
            //RM
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Referencia",
                                        "@Cliente",
                                        "@Id_Tm"
                                      };
                object[] Valores = { sesion.Id_Emp,
                                     sesion.Id_Cd_Ver,
                                     referencia,
                                     cliente,
                                     Id_Tm
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_ConsultaReferencia", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// consulta la cantidad de partidas de una remision, que tienen saldo disponible para devolucion
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="id_remision"></param>
        /// <param name="partidasConSaldo"></param>
        public void ConsultarPartidasConSaldo(Sesion sesion, int id_remision, ref int partidasConSaldo)
        {//RM
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem", 
                                      };

                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_remision.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_ConsultarPartidasConSaldo", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    partidasConSaldo = dr.GetInt32(dr.GetOrdinal("PartidasConSaldo"));
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DepuraPartidasEliminadas(Sesion sesion, int id_remision, List<RemisionDet> detalles, int Id_Ped)
        {
            try
            {
                SqlDataReader dr = null;


                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem", 
                                      };

                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_remision.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_Consultar", ref dr, parametros, Valores);

                string Existe = "N";
                int verificador = 0;

                Remision detalleCarga = new Remision();
                List<Remision> listaRemision = new List<Remision>();
                while (dr.Read())
                {
                    detalleCarga = new Remision();
                    detalleCarga.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    detalleCarga.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    detalleCarga.Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    listaRemision.Add(detalleCarga);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                foreach (Remision lista in listaRemision)
                {
                    Existe = "N";
                    foreach (RemisionDet detalle in detalles)
                    {
                        if (detalle.Id_Ter == lista.Id_Ter && detalle.Id_Prd == lista.Id_Prd)
                        {
                            Existe = "S";
                        }
                    }

                    if (Existe == "N")
                    {
                        string[] parametros1 = { 
                                    "@Id_Emp",
                                    "@Id_Cd",
                                    "@Id_Rem", 
                                    "@Id_Ter", 
                                    "@Id_Prd", 
                                    "@Id_Ped", 
                                    "@Rem_Cant"
                                };

                        string[] Valores1 = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_remision.ToString(),
                                       lista.Id_Ter.ToString(),
                                       lista.Id_Prd.ToString(),
                                       Id_Ped.ToString(),
                                       lista.Cant.ToString()
                                   };
                        CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                        SqlCommand sqlcmd1 = CapaDatos1.GenerarSqlCommand("spCapRemision_Eliminar_Prd", ref verificador, parametros1, Valores1);
                        CapaDatos1.LimpiarSqlcommand(ref sqlcmd1);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Recibe el id de un documento (remision o factura) y obtiene una lista de los productos de ese documento, ordenados por agrupador
        /// </summary>
        /// <param name="Id_Documento"></param>
        /// <param name="Tipo_Documento">1 Remision, 2 Factura</param>
        /// <param name="sesion"></param>
        /// <param name="datatable"></param>
        public void ConsultarTotalProductoDocumento(int Id_Documento, int Tipo_Documento, Sesion sesion, ref DataTable datatable)
        {//RM
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Documento", //antes @Id_Rem
                                          "@tipoDocumento" //1 Remision, 2 Factura
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       Id_Documento.ToString(),
                                       Tipo_Documento.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDet_ConsultarTotalProducto", ref dr, parametros, Valores);

                datatable = new DataTable();
                datatable.Columns.Add("Prd_AgrupadoSpo");
                datatable.Columns.Add("Id_Ter");
                datatable.Columns.Add("Cantidad");
                while (dr.Read())
                {
                    int a = dr.GetInt32(dr.GetOrdinal("Prd_AgrupadoSpo"));
                    int b = dr.GetInt32(dr.GetOrdinal("Id_Ter"));
                    int c = dr.GetInt32(dr.GetOrdinal("Cantidad"));
                    datatable.Rows.Add(new object[] { a, b, c });
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarRemision(Remision remision, List<RemisionDet> detalles, Sesion sesion,
                                ref int verificador, bool actualizacionDocumento, bool Gen_Contrato, ref int Id_Rem, ref bool tipoMovimento, ref string mensaje)
        {// Ped_CantR se afecta en el SP
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                int verificador2 = 0;
                //si es actualizacion, dara de baja el documento
                #region "Actualizacion"
                List<RemisionDet> detalles_borrar = new List<RemisionDet>();
                if (actualizacionDocumento)
                {

                    ConsultarRemisionesDetalle(sesion, remision, ref detalles_borrar);
                    string[] Parametros2 = {
                                                "@Id_Emp"
                                                ,"@Id_Cd"
                                                ,"@Id_Rem"  
                                                ,"@Id_RemDet"   
		                                    };

                    //para cada detalle de la lista
                    foreach (RemisionDet detalle in detalles_borrar)
                    {
                        object[] Valores2 = {
                                                detalle.Id_Emp
                                                ,detalle.Id_Cd
                                                ,detalle.Id_Rem
                                                ,detalle.Id_RemDet
                                            };

                        if (remision.Id_Tm == 60)
                        {
                            int cantidad_B = 0;
                            foreach (RemisionDet det in detalles)
                            {
                                if (det.Id_Prd == detalle.Id_Prd && det.Id_Ter == detalle.Id_Ter)
                                {
                                    cantidad_B = det.Rem_Cant;
                                    break;
                                }
                            }

                            verificador = 0;
                            string[] ParametrosP = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd", "@Fecha", "@Id_Ter" };
                            object[] ValoresP = { detalle.Id_Emp, detalle.Id_Cd, remision.Id_Cte, detalle.Id_Prd, remision.Rem_Fecha, detalle.Id_Ter };
                            sqlcmd = CapaDatos.GenerarSqlCommand("BiComodato", ref verificador, ParametrosP, ValoresP);
                            if (verificador - detalle.Rem_Cant + cantidad_B < 0)
                            {
                                throw new System.ArgumentException("El producto " + detalle.Id_Prd + " no cuenta con saldo suficiente|", "saldo_insuficiente");
                            }
                        }

                        verificador = 0;
                        //sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDet_baja", ref verificador, Parametros2, Valores2);
                    }

                    //borradosFisicoPartidas(remision, ref verificador, sesion.Emp_Cnx, ref CapaDatos, ref sqlcmd);

                }
                #endregion
                if (actualizacionDocumento)
                {
                    #region parametros
                    string[] ParametrosModRem = {
                                              "@Id_Emp"
                                              ,"@Id_Cd"
                                              ,"@Id_Rem"
                                              ,"@Rem_tipo"
                                              ,"@Rem_Fecha"
                                              ,"@Id_Tm"
                                              ,"@Id_Ped"
                                              ,"@Id_Cte"
                                              ,"@Id_Ter"
                                              ,"@Id_Rik"
                                              ,"@Id_U"
                                              ,"@Rem_Calle"
                                              ,"@Rem_Numero"
                                              ,"@Rem_Cp"
                                              ,"@Rem_Colonia"
                                              ,"@Rem_Municipio"
                                              ,"@Rem_Estado"
                                              ,"@Rem_Rfc"
                                              ,"@Rem_Telefono"
                                              ,"@Rem_Contacto"
                                              ,"@Rem_Conducto"
                                              ,"@Rem_Guia"
                                              ,"@Rem_FechaEntrega"
                                              ,"@Rem_HoraEntrega"
                                              ,"@Rem_Nota"
                                              ,"@Rem_Subtotal"
                                              ,"@Rem_Iva"
                                              ,"@Rem_Total"
                                              ,"@Rem_Estatus" 
                                              ,"@Rem_ManAut"
                                              ,"@Id_Vap"
                                              ,"@Rem_OrdCompra"
                                              ,"@Rem_CteCuentaNacional"
                                              ,"@Rem_CteCuentaContNacional"
                                      };
                    object[] ValoresModRem = {                                                                                      
                                              remision.Id_Emp
                                              ,remision.Id_Cd
                                              ,remision.Id_Rem
                                              ,remision.Rem_Tipo
                                              ,remision.Rem_Fecha
                                              ,remision.Id_Tm
                                              ,remision.Id_Ped==-1?(object)null:remision.Id_Ped
                                              ,remision.Id_Cte
                                              ,remision.Id_Ter
                                              ,remision.Id_Rik
                                              ,remision.Id_U
                                              ,remision.Rem_Calle
                                              ,remision.Rem_Numero
                                              ,remision.Rem_Cp
                                              ,remision.Rem_Colonia
                                              ,remision.Rem_Municipio
                                              ,remision.Rem_Estado
                                              ,remision.Rem_Rfc
                                              ,remision.Rem_Telefono
                                              ,remision.Rem_Contacto
                                              ,remision.Rem_Conducto
                                              ,remision.Rem_Guia
                                              ,remision.Rem_FechaEntrega
                                              ,remision.Rem_HoraEntrega
                                              ,remision.Rem_Nota
                                              ,remision.Rem_Subtotal
                                              ,remision.Rem_Iva
                                              ,remision.Rem_Total
                                              ,remision.Rem_Estatus
                                              ,remision.Rem_ManAut
                                              ,remision.Id_Vap
                                              ,remision.Rem_OrdenCompra
                                              ,remision.Rem_CteCuentaNacional
                                              ,remision.Rem_CteCuentaContNacional
                                   };
                    #endregion
                    //Modifica una remision
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_Modificar", ref Id_Rem, ParametrosModRem, ValoresModRem);
                    Id_Rem = remision.Id_Rem;
                }
                else
                {
                    #region parametros
                    string[] Parametros = {
                                              "@Id_Emp"
                                              ,"@Id_Cd"
                                              ,"@Rem_tipo"
                                              ,"@Rem_Fecha"
                                              ,"@Id_Tm"
                                              ,"@Id_Ped"
                                              ,"@Id_Cte"
                                              ,"@Id_Ter"
                                              ,"@Id_Rik"
                                              ,"@Id_U"
                                              ,"@Rem_Calle"
                                              ,"@Rem_Numero"
                                              ,"@Rem_Cp"
                                              ,"@Rem_Colonia"
                                              ,"@Rem_Municipio"
                                              ,"@Rem_Estado"
                                              ,"@Rem_Rfc"
                                              ,"@Rem_Telefono"
                                              ,"@Rem_Contacto"
                                              ,"@Rem_Conducto"
                                              ,"@Rem_Guia"
                                              ,"@Rem_FechaEntrega"
                                              ,"@Rem_HoraEntrega"
                                              ,"@Rem_Nota"
                                              ,"@Rem_Subtotal"
                                              ,"@Rem_Iva"
                                              ,"@Rem_Total"
                                              ,"@Rem_Estatus" 
                                              ,"@Rem_ManAut"
                                              ,"@Id_Vap"
                                              ,"@Rem_OrdCompra"
                                              ,"@Rem_CteCuentaNacional"
                                              ,"@Rem_CteCuentaContNacional"
                                              ,"@Id_TG"
                                      };
                    object[] Valores = {                                                                                      
                                              remision.Id_Emp
                                              ,remision.Id_Cd
                                              ,remision.Rem_Tipo
                                              ,remision.Rem_Fecha
                                              ,remision.Id_Tm
                                              ,remision.Id_Ped==-1?(object)null:remision.Id_Ped
                                              ,remision.Id_Cte
                                              ,remision.Id_Ter
                                              ,remision.Id_Rik
                                              ,remision.Id_U
                                              ,remision.Rem_Calle
                                              ,remision.Rem_Numero
                                              ,remision.Rem_Cp
                                              ,remision.Rem_Colonia
                                              ,remision.Rem_Municipio
                                              ,remision.Rem_Estado
                                              ,remision.Rem_Rfc
                                              ,remision.Rem_Telefono
                                              ,remision.Rem_Contacto
                                              ,remision.Rem_Conducto
                                              ,remision.Rem_Guia
                                              ,remision.Rem_FechaEntrega
                                              ,remision.Rem_HoraEntrega
                                              ,remision.Rem_Nota
                                              ,remision.Rem_Subtotal
                                              ,remision.Rem_Iva
                                              ,remision.Rem_Total
                                              ,remision.Rem_Estatus
                                              ,remision.Rem_ManAut
                                              ,remision.Id_Vap
                                              ,remision.Rem_OrdenCompra
                                              ,remision.Rem_CteCuentaNacional
                                              ,remision.Rem_CteCuentaContNacional
                                              ,remision.IdTg
                                   };
                    #endregion
                    //inserta una remision
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_Insertar", ref Id_Rem, Parametros, Valores);
                    if (remision.Id_Tm == 60)
                        tipoMovimento = true;
                }
                //parametros del detalle
                string[] ParametrosDetalle = {
                                                 "@Id_Emp"
	                                            ,"@Id_Cd"	  	 
	                                            ,"@Id_Rem"
	                                            ,"@Id_RemDet"
	                                            ,"@Id_Ter"
	                                            ,"@Id_Prd"
                                                ,"@Id_Ped"
	                                            ,"@Rem_Cant"
                                                ,"@Ped_Pertenece"
	                                            ,"@Rem_Precio"
	                                            ,"@Entrada"
                                                ,"@Rem_CantOriginal"

                                        };

                if (Id_Rem != 0 && Id_Rem != -1)
                {


                    //Aqui eliminar partidas DepuraPartidasEliminadas

                    DepuraPartidasEliminadas(sesion, Id_Rem, detalles, remision.Id_Ped);

                    //para cada detalle de la lista
                    foreach (RemisionDet detalle in detalles)
                    {
                        int iCantidad = 0;
                        for (int i = 0; i < detalles_borrar.Count; i++)
                        {
                            if (detalles_borrar[i].Id_Prd == detalle.Id_Prd)
                            {
                                iCantidad = detalles_borrar[i].Rem_Cant;
                            }
                        }

                        object[] ValoresDetalle = {
                                                     detalle.Id_Emp	 	 
	                                                ,detalle.Id_Cd	  	 
	                                                ,Id_Rem	 	 
	                                                ,detalle.Id_RemDet	 	 
	                                                ,detalle.Id_Ter	 	 
	                                                ,detalle.Id_Prd	 
	                                                ,remision.Id_Ped==-1 ? (object)null:remision.Id_Ped 
	                                                ,detalle.Rem_Cant	 
	                                                ,detalle.Ped_Pertenece	 	 
	                                                ,detalle.Rem_Precio	 
	                                                ,0 //la remision es una salida 
                                                    ,iCantidad
                                            };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDet_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    }
                    string[] parametroFinal = {
                                                   "@Id_Emp", "@Id_Cd", "@Id_Rem" 
                                               };
                    object[] ValoresFinales = { 
                                                    remision.Id_Emp, remision.Id_Cd, Id_Rem 
                                               };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spValidaInventarioRemision", ref verificador2, parametroFinal, ValoresFinales);
                    if (verificador2 != 0)
                    {
                        CapaDatos.RollBackTrans();
                        mensaje = "El producto " + verificador2.ToString() + " no cuenta con inventario suficiente";
                    }
                }
                else
                {
                    throw new Exception("Problema al insertar en CapEntSal. Regresa ID Invalido");
                }
                if (verificador2 == 0)
                {
                    verificador = Id_Rem;
                    CapaDatos.CommitTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        private void borradosFisicoPartidas(Remision remision, ref int verificador, string conexion, ref CD_Datos CapaDatos, ref SqlCommand sqlcmd)
        {
            try
            {
                string[] Parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem"
                                      };

                string[] Valores = {
                                       remision.Id_Emp.ToString(),
                                       remision.Id_Cd.ToString(),
                                       remision.Id_Rem.ToString()
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDet_BorradoFisico", ref verificador, Parametros, Valores);

                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta las remisiones de acuerdo a los filtros de busqueda de la pantalla de remisiones_lista
        /// </summary>
        /// <param name="remisiones"></param>
        /// <param name="remision"></param>
        /// <param name="sesion"></param>
        /// <param name="NombreCliente"></param>
        /// <param name="ClienteIni"></param>
        /// <param name="ClienteFin"></param>
        /// <param name="ManAut"></param>
        /// <param name="FechaIni"></param>
        /// <param name="FechaFin"></param>
        /// <param name="Estatus"></param>
        /// <param name="NumeroIni"></param>
        /// <param name="NumeroFin"></param>
        /// <param name="Pedido"></param>
        public void ConsultarRemisiones(ref List<Remision> remisiones, ref Remision remision, CapaEntidad.Sesion sesion,
            string NombreCliente, int ClienteIni, int ClienteFin, int ManAut,
            DateTime? FechaIni, DateTime? FechaFin, string Estatus, int NumeroIni, int NumeroFin, int Pedido, string OrdenDeCompra, int IdTm)
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
                                          "@FechaIni",
                                          "@FechaFin",
                                          "@Estatus",
                                          "@NumeroIni",
                                          "@NumeroFin",
                                          "@Id_Ped",
                                          "@Rem_OrdenCompra",
                                          "@Id_Tm"

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
                                       FechaIni, 
                                       FechaFin,
                                       Estatus == "" ? (object)null : Estatus,
                                       NumeroIni == -1 ? (object)null : NumeroIni,
                                       NumeroFin == -1 ? (object)null : NumeroFin,
                                       Pedido == -1 ? (object)null : Pedido,
                                       OrdenDeCompra ==""?(object)null : OrdenDeCompra,//Se agrega para consultar por OC
                                       IdTm == -1 ? (object)null:IdTm
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_ListaConsulta", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    remision = new Remision();
                    remision.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    remision.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    remision.Id_U = dr.GetInt32(dr.GetOrdinal("Id_U"));
                    remision.UsuNom = dr.GetString(dr.GetOrdinal("U_Nombre"));
                    remision.Id_Tm = dr.GetInt32(dr.GetOrdinal("Id_Tm"));
                    remision.Rem_TipoStr = dr.GetString(dr.GetOrdinal("Tm_Nombre"));
                    // se comenta para mostrar la descripcion de movimento
                    //remision.Rem_Tipo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Tipo"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Tipo"));
                    //switch (remision.Rem_Tipo)
                    //{
                    //    case "3":
                    //        remision.Rem_TipoStr = "Remisión";
                    //        break;
                    //    default:
                    //        break;
                    //}
                    remision.Rem_ManAut = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_ManAut"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Rem_ManAut"));
                    switch (remision.Rem_ManAut)
                    {
                        case 1:
                            remision.Rem_ManAutStr = "Manual";
                            break;
                        case 0:
                            remision.Rem_ManAutStr = "Automático";
                            break;
                        default:
                            break;
                    }
                    remision.Rem_Estatus = dr.GetString(dr.GetOrdinal("Rem_Estatus"));
                    switch (remision.Rem_Estatus.ToLower())
                    {
                        case "c":
                            remision.Rem_EstatusStr = "Capturado";
                            break;
                        case "b":
                            remision.Rem_EstatusStr = "Baja";
                            break;
                        case "i":
                            remision.Rem_EstatusStr = "Impreso";
                            break;
                        case "n":
                            remision.Rem_EstatusStr = "Entregado";
                            break;
                        case "e":
                            remision.Rem_EstatusStr = "Embarque";
                            break;
                        case "s":
                            remision.Rem_EstatusStr = "Solicitado";
                            break;
                        case "a":
                            remision.Rem_EstatusStr = "Autorizado";
                            break;
                        case "r":
                            remision.Rem_EstatusStr = "Rechazado";
                            break;
                        default:
                            break;
                    }
                    remision.Id_Rem = dr.GetInt32(dr.GetOrdinal("Id_Rem"));
                    remision.Rem_Fecha = dr.GetDateTime(dr.GetOrdinal("Rem_Fecha"));
                    remision.Id_Cte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                    remision.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));//dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    remision.Rem_Subtotal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Subtotal"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Rem_Subtotal"));
                    remision.Rem_Iva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Iva"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Rem_Iva"));
                    remision.Rem_Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Total"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Rem_Total"));
                    remision.Id_Ped = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ped"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Ped"));
                    remision.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_Ter"));

                    remisiones.Add(remision);
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
        /// Consulta los datos para el encabezado del reporte a imprimir
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd_Ver"></param>
        /// <param name="Id_Rem"></param>
        /// <param name="remision"></param>
        public void ConsultarEncabezadoImprimir(Sesion sesion/*, int Id_Emp, int Id_Cd_Ver*/, int Id_Rem, ref Remision remision, int grabarContrato)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                //CapaDatos.StartTrans();
                SqlCommand sqlcmd = new SqlCommand();
                SqlDataReader dr = null;
                // CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);             
                // SqlCommand sqlcmd = new SqlCommand();
                if (grabarContrato == 1)
                {//hace el insert del contrato comodato.. sino existe, para despues mostrar los datos en el reporte
                    int verificador = 0;
                    string[] ParametrosContrato = { "@Id_Emp", "@Id_Cd", "@Id_Rem" };
                    object[] ValoresContrato = { sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Rem };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spContratoComodato_Insertar", ref verificador, ParametrosContrato, ValoresContrato);
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
                CapaDatos.CD_Datos CapaDatos1 = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem"
                                      };

                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       Id_Rem.ToString()
                                   };
                sqlcmd = CapaDatos1.GenerarSqlCommand("spCapRemision_Consulta", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    remision.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    remision.Id_Ped = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ped"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ped")));
                    remision.Emp_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Emp_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Emp_Nombre"));
                    remision.Rem_EstatusStr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_EstatusStr"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_EstatusStr"));
                    remision.Rem_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Calle"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Calle"));
                    remision.Rem_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Numero"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Numero"));
                    remision.Rem_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Colonia"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Colonia"));
                    remision.Rem_Municipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Municipio"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Municipio"));
                    remision.Rem_Estado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Estado"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Estado"));
                    remision.Rem_Cp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Cp"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Cp"));
                    remision.Id_Rem = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rem"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Rem")));
                    remision.Rem_Fecha = dr.GetDateTime(dr.GetOrdinal("Rem_Fecha"));
                    remision.Rem_FechaHr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_FechaHr"))) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("Rem_FechaHr"));

                    remision.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    remision.Cte_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Calle"))) ? "" : dr.GetString(dr.GetOrdinal("Cte_Calle"));
                    remision.Cte_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Numero"))) ? "" : dr.GetString(dr.GetOrdinal("Cte_Numero"));
                    remision.Cte_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Colonia"))) ? "" : dr.GetString(dr.GetOrdinal("Cte_Colonia"));

                    remision.Id_Cte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cte")));
                    remision.Id_Tm = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Tm"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Tm")));
                    remision.Tm_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Tm_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Tm_Nombre"));
                    remision.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cd")));
                    remision.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ter")));
                    remision.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Rik"))); //dr.GetInt32(dr.GetOrdinal("Id_Rik"));
                    remision.Rem_Rfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Rfc"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Rfc"));
                    remision.Rem_Telefono = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Telefono"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Telefono"));
                    remision.Rem_Contacto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Contacto"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Contacto"));
                    remision.Rem_Conducto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Conducto"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Conducto"));
                    remision.Rem_Guia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Guia"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Guia"));
                    remision.Rem_FechaEntrega = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_FechaEntrega"))) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("Rem_FechaEntrega"));
                    remision.Rem_Nota = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Nota"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_Nota"));
                    remision.Cte_CondPago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CondPago"))) ? 0 : dr.GetInt32(dr.GetOrdinal("Cte_CondPago"));
                    // remision.RemCtto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("RemCtto"))) ? "" : dr.GetString(dr.GetOrdinal("RemCtto"));

                    remision.Rik_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Rik_Nombre"));
                    remision.Rik_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Calle"))) ? "" : dr.GetString(dr.GetOrdinal("Rik_Calle"));
                    remision.Rik_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Numero"))) ? "" : dr.GetString(dr.GetOrdinal("Rik_Numero"));
                    remision.Rik_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Colonia"))) ? "" : dr.GetString(dr.GetOrdinal("Rik_Colonia"));

                    remision.Rem_Subtotal = Convert.IsDBNull(dr.GetDouble(dr.GetOrdinal("Rem_Subtotal"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Rem_Subtotal"));
                    remision.Rem_Iva = Convert.IsDBNull(dr.GetDouble(dr.GetOrdinal("Rem_Iva"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Rem_Iva"));
                    remision.Rem_Total = Convert.IsDBNull(dr.GetDouble(dr.GetOrdinal("Rem_Total"))) ? 0 : dr.GetDouble(dr.GetOrdinal("Rem_Total"));
                    remision.Id_Cco = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cco"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cco")));
                    remision.NumContratoImpresion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("NumContratoImpresion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("NumContratoImpresion")).ToString();
                    remision.Id_Vap = dr.IsDBNull(dr.GetOrdinal("Id_Vap")) ? (int?)null : dr.GetInt32(dr.GetOrdinal("Id_Vap"));
                    remision.Rem_OrdenCompra = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_OrdCompra"))) ? "" : dr.GetString(dr.GetOrdinal("Rem_OrdCompra"));
                    remision.Rem_Especial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_especial"))) ? 0 : dr.GetInt32((dr.GetOrdinal("Rem_especial")));
                    remision.Rem_CteCuentaNacional = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_CteCuentaNacional"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Rem_CteCuentaNacional")));
                    remision.Rem_CteCuentaContNacional = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_CteCuentaContNacional"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Rem_CteCuentaContNacional")));

                    try
                    {
                        remision.IdTg = dr.GetInt32(dr.GetOrdinal("Id_TG"));
                    }
                    catch (Exception ex)
                    {
                        remision.IdTg = null;
                    }

                    break;
                }
                dr.Close();
                CapaDatos1.LimpiarSqlcommand(ref sqlcmd);

                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                sqlcmd = CapaDatos2.GenerarSqlCommand("spCapRemision_Resumen_Inversion", ref dr, parametros, Valores);
                if (dr.Read())
                {
                    //System.IO.StreamWriter sw = new System.IO.StreamWriter("C:/files/key/sianwebcambio/_Fuentes/sianweb/ResumenInversion.html");

                    //System.IO.StreamWriter sw = new System.IO.StreamWriter("F:/APLICACIONES_IIS/sianwebmtyamortizacion/ResumenInversion.html");
                    // sw.WriteLine(Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CadenaHTML"))) ? "" : dr.GetString(dr.GetOrdinal("CadenaHTML")));
                    // sw.Close();

                    remision.Rem_ResumenInversion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IFrame"))) ? "" : dr.GetString(dr.GetOrdinal("IFrame"));
                }
                dr.Close();


                sqlcmd = CapaDatos2.GenerarSqlCommand("spCapRemision_Tabla_Amortizacion", ref dr, parametros, Valores);
                if (dr.Read())
                {

                    //System.IO.StreamWriter sw = new System.IO.StreamWriter("C:/files/key/sianwebcambio/_Fuentes/sianweb/TablaAmortizacion.html");
                    //System.IO.StreamWriter sw = new System.IO.StreamWriter("F:/APLICACIONES_IIS/sianwebmtyamortizacion/TablaAmortizacion.html");
                    //sw.WriteLine(Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CadenaHTML"))) ? "" : dr.GetString(dr.GetOrdinal("CadenaHTML")));
                    //sw.Close();

                    remision.Rem_TablaAmortizacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IFrame"))) ? "" : dr.GetString(dr.GetOrdinal("IFrame"));
                }
                dr.Close();


                sqlcmd = CapaDatos2.GenerarSqlCommand("spCapRemision_Kardex_Movimientos_Inversion", ref dr, parametros, Valores);
                if (dr.Read())
                {

                    //System.IO.StreamWriter sw = new System.IO.StreamWriter("C:/files/key/sianwebcambio/_Fuentes/sianweb/KardexMovimientosInversion.html");
                    //System.IO.StreamWriter sw = new System.IO.StreamWriter("F:/APLICACIONES_IIS/sianwebmtyamortizacion/kardexMovimientosInversion.html");
                    //sw.WriteLine(Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CadenaHTML"))) ? "" : dr.GetString(dr.GetOrdinal("CadenaHTML")));
                    //sw.Close();

                    remision.Rem_KardexAmortizacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("IFrame"))) ? "" : dr.GetString(dr.GetOrdinal("IFrame"));
                }
                dr.Close();


                CapaDatos2.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemision_Estatus(Remision remision, string conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Rem"                                        
                                        ,"@Rem_Estatus"
                                      };
                object[] Valores = { 
                                        remision.Id_Emp
                                        ,remision.Id_Cd
                                        ,remision.Id_Rem
                                        ,remision.Rem_Estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_ModificarEstatus", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemisiones_Estatus(int Id_Emp, int Id_Cd, string remisiones, string estatus, string conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_RemStr"                                        
                                        ,"@Rem_Estatus"
                                      };
                object[] Valores = { 
                                        Id_Emp
                                        ,Id_Cd
                                        ,remisiones
                                        ,estatus
                                   };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisiones_ModificarEstatus", ref verificador, Parametros, Valores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesDetalle(Sesion sesion, Remision remision, ref List<RemisionDet> remisiones)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem"
                                      };

                string[] Valores = {
                                       remision.Id_Emp.ToString(),
                                       remision.Id_Cd.ToString(),
                                       remision.Id_Rem.ToString()
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDet_Consulta", ref dr, parametros, Valores);

                RemisionDet detalle = new RemisionDet();
                while (dr.Read())
                {
                    detalle = new RemisionDet();
                    detalle.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    detalle.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cd")));
                    detalle.Id_Rem = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rem"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Rem")));
                    detalle.Id_RemDet = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_RemDet"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_RemDet")));
                    detalle.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ter")));
                    detalle.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    detalle.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Prd")));
                    detalle.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) ? "" : dr.GetString((dr.GetOrdinal("Prd_Descripcion")));
                    detalle.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? "" : dr.GetString((dr.GetOrdinal("Prd_Presentacion")));
                    detalle.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? "" : dr.GetString((dr.GetOrdinal("Prd_UniNe")));
                    detalle.Rem_Cant = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Cant"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Rem_Cant")));
                    detalle.Rem_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Precio"))) ? -1 : dr.GetDouble((dr.GetOrdinal("Rem_Precio")));

                    detalle.Producto = new Producto();
                    detalle.Producto.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Prd")));
                    detalle.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) ? "" : dr.GetString((dr.GetOrdinal("Prd_Descripcion")));
                    detalle.Producto.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ter")));

                    remisiones.Add(detalle);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesDetalle(Sesion sesion, Remision remision, ref List<RemisionDet> remisiones, ref CD_Datos CapaDatos, ref SqlCommand sqlcmd)
        {
            try
            {
                SqlDataReader dr = null;

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem"
                                      };
                string[] Valores = {
                                       remision.Id_Emp.ToString(),
                                       remision.Id_Cd.ToString(),
                                       remision.Id_Rem.ToString()
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDet_Consulta", ref dr, parametros, Valores);

                RemisionDet detalle = new RemisionDet();
                while (dr.Read())
                {
                    detalle = new RemisionDet();
                    detalle.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    detalle.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cd")));
                    detalle.Id_Rem = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rem"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Rem")));
                    detalle.Id_RemDet = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_RemDet"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_RemDet")));
                    detalle.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ter")));
                    detalle.Id_Prd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Prd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Prd")));
                    detalle.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Descripcion"))) ? "" : dr.GetString((dr.GetOrdinal("Prd_Descripcion")));
                    detalle.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Presentacion"))) ? "" : dr.GetString((dr.GetOrdinal("Prd_Presentacion")));
                    detalle.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_UniNe"))) ? "" : dr.GetString((dr.GetOrdinal("Prd_UniNe")));
                    detalle.Rem_Cant = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Cant"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Rem_Cant")));
                    detalle.Rem_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Precio"))) ? -1 : dr.GetDouble((dr.GetOrdinal("Rem_Precio")));

                    remisiones.Add(detalle);
                }
                dr.Close();

                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaRemision(ref Remision remision, ref List<RemisionDet> detalles, Sesion sesion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                //Se afecta la remision, cambiando el estatus a baja
                remision.Rem_Estatus = "B";

                string[] ParametrosEst = { "@Id_Emp", "@Id_Cd", "@Id_Rem", "@Rem_Estatus", "@Id_UCancelo" };
                object[] ValoresEst = { remision.Id_Emp, remision.Id_Cd, remision.Id_Rem, remision.Rem_Estatus, remision.Id_U };

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_ModificarEstatus", ref verificador, ParametrosEst, ValoresEst);

                //parametros del detalle
                string[] Parametros2 = { "@Id_Emp", "@Id_Cd", "@Id_Rem" };
                object[] Valores2 = { remision.Id_Emp, remision.Id_Cd, remision.Id_Rem };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_Eliminar", ref verificador, Parametros2, Valores2);

                //para cada detalle de la lista
                foreach (RemisionDet detalle in detalles)
                {
                    verificador = 0;
                    if (remision.Id_Tm == 60)
                    {
                        string[] ParametrosP = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Prd", "@Fecha", "@Id_Ter" };
                        object[] ValoresP = { detalle.Id_Emp, detalle.Id_Cd, remision.Id_Cte, detalle.Id_Prd, remision.Rem_Fecha, detalle.Id_Ter };
                        sqlcmd = CapaDatos.GenerarSqlCommand("BiComodato", ref verificador, ParametrosP, ValoresP);
                        if (verificador < 0)
                        {
                            throw new System.ArgumentException("El producto " + detalle.Id_Prd + " no cuenta con saldo suficiente");
                        }
                    }
                }

                if (remision.Id_Ped > 0)
                {
                    //Modificar estatus del pedido
                    string[] ParametrosPed = {
                                            "@Id_Emp"
                                            ,"@Id_Cd"	
                                            ,"@Id_Ped"
                                            ,"@Ped_Estatus"
		                                };
                    object[] ValoresPed = {
                                                remision.Id_Emp
                                                ,remision.Id_Cd
                                                ,remision.Id_Ped
                                                ,""
		                                };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_ModificarEstatus", ref verificador, ParametrosPed, ValoresPed);
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

        public List<Remision> ConsultaProductosRemision(ref Remision remision, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Rem"                                          
                                      };
                object[] Valores = { 
                                       remision.Id_Emp
                                       ,remision.Id_Cd
                                       ,remision.Id_Rem
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_ProductosEspecial", ref dr, Parametros, Valores);

                List<Remision> listaRemision = new List<Remision>();
                while (dr.Read())
                {//Id_Clp, c.Id_Prd, c.Clp_descripcion
                    remision = new Remision();
                    remision.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    listaRemision.Add(remision);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return listaRemision;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionEspecialDetalle(ref List<RemisionDet> listaRemisionProductos, string Conexion, int id_Emp, int id_Cd, int id_Rem, int id_Cte)
        {
            try
            {
                RemisionDet remisionDet = null;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rem" };
                object[] Valores = { id_Emp, id_Cd, id_Rem };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionEspecialDetalle_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    remisionDet = new RemisionDet();

                    remisionDet.Id_Emp = id_Emp;
                    remisionDet.Id_Cd = id_Cd;
                    remisionDet.Id_Rem = 0;
                    remisionDet.Id_RemDet = 0;
                    remisionDet.Id_CteExt = id_Cte;
                    remisionDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    remisionDet.Rem_Cant = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Cant"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    remisionDet.Rem_Precio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rem_Precio"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rem_Precio")));
                    remisionDet.Rem_Importe = remisionDet.Rem_Cant * remisionDet.Rem_Precio;
                    //datos del producto de la orden de compra
                    remisionDet.Producto = new Producto();
                    remisionDet.Producto.Id_PrdEsp = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_PrdEsp")));
                    remisionDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    remisionDet.Producto.Id_Emp = id_Emp;
                    remisionDet.Producto.Id_Cd = id_Cd;
                    remisionDet.Producto.Prd_Descripcion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("RemEsp_Descripcion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("RemEsp_Descripcion")).ToString();
                    remisionDet.Producto.Prd_DescripcionEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("RemEsp_DescripcionEspecial"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("RemEsp_DescripcionEspecial")).ToString();
                    remisionDet.Producto.Prd_Presentacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("RemEsp_Presentacion"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("RemEsp_Presentacion")).ToString();
                    remisionDet.Producto.Prd_UniNe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("RemEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("RemEsp_Unidades")).ToString();
                    remisionDet.Producto.Prd_UniNs = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("RemEsp_Unidades"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("RemEsp_Unidades")).ToString();
                    remisionDet.Clp_Release = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("RemEsp_Release"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("RemEsp_Release")).ToString();
                    remisionDet.Producto.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    remisionDet.bTieneEspecial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("bTieneEspecial"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("bTieneEspecial")));
                    listaRemisionProductos.Add(remisionDet);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesCantidad(int Id_Emp, int Id_Cd, string refe, string folio, int Prd_AgrupadoSpo, ref int cantidad, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rem", "@Prd_AgrupadoSpo", "@Id_Es" };
                object[] Valores = { Id_Emp, Id_Cd, refe, Prd_AgrupadoSpo, folio };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcapRemisionES", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    cantidad = dr.IsDBNull(dr.GetOrdinal("Cantidad")) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Cantidad"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesCantidadRem(int Id_Emp, int Id_Cd, string refe, int id_prd, ref int cantidadES, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rem", "@Id_Prd" };
                object[] Valores = { Id_Emp, Id_Cd, refe, id_prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcapRemisionES2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    cantidadES = (int)dr.GetValue(dr.GetOrdinal("total"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesCantidadRemCantidad(int Id_Emp, int Id_Cd, int id_ES, int id_prd, string Nat, ref int cantidadES2, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Es", "@Prd_AgrupadoSpo", "@Nat" };
                object[] Valores = { Id_Emp, Id_Cd, id_ES, id_prd, Nat };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcapRemisionES_Cantidad", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    cantidadES2 = (int)dr.GetValue(dr.GetOrdinal("total"));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionesxFactura(Sesion sesion, int Factura, ref List<Remision> remision)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, Factura };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaRemision_Consultar", ref dr, Parametros, Valores);

                Remision rem;
                while (dr.Read())
                {
                    rem = new Remision();
                    rem.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remision.Add(rem);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionesxPedido(Sesion sesion, int Pedido, ref List<Remision> remision)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, Pedido };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRedidoRemision_Consultar", ref dr, Parametros, Valores);

                Remision rem;
                while (dr.Read())
                {
                    rem = new Remision();
                    rem.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remision.Add(rem);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPermitirModificar(int Id_Rem, int Id_Emp, int Id_Cd, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem"
                                      };
                object[] Valores = {
                                      Id_Emp, Id_Cd,Id_Rem
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_ValidaEdicion", ref verificador, parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemision_PDF(Remision remision, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Rem",
                                          "@Rem_Pdf",
                                      };
                object[] Valores = {
                                      remision.Id_Emp, remision.Id_Cd,remision.Id_Rem,remision.PDF
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemision_PDF", ref verificador, parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaMontosImpresion(Remision remision, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool bVerificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] parametros = { 
                                          "@Id_Doc",
                                          "@Id_Cd",
                                          "@Id_Emp",
                                          "@iTipoDocumento"
                                      };
                object[] Valores = {
                                      remision.Id_Rem,
                                      Id_Cd,
                                      Id_Emp,
                                      iTipoDocumento
                                   };

                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapDocumentoValido_Impresion", ref verificador, parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                if (verificador == 0)
                    bVerificador = false;
                else
                    bVerificador = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calcula y devuelve la utilidad prima real
        /// </summary>
        /// <param name="id_Emp"></param>
        /// <param name="id_Cd"></param>
        /// <param name="id_Ter"></param>
        /// <param name="id_Cte"></param>
        /// <param name="id_TG"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="candenaConexionEF"></param>
        /// <returns>Decimal. La utilidad prima real.</returns>
        public Decimal CalcularUtilidadPrimaReal(int? id_Emp, int? id_Cd, int? id_Ter, int? id_Cte, int? id_TG, DateTime? fechaInicial, DateTime? fechaFinal, string candenaConexionEF)
        {
            Decimal ret = 0.0M;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(candenaConexionEF))
            {
                var res = ctx.spCapRemision_CalcularUtilidadPrimaReal(id_Emp, id_Cd, id_Ter, id_Cte, id_TG, fechaInicial, fechaFinal).ToList();
                if (res.Count > 0)
                {
                    ret = res[0].Column1.Value;
                }
            }
            return ret;
        }


        public void ValidarRemisionesPendientes(Sesion sesion, int Pedido, ref List<Remision> remision)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Ped" };
                object[] Valores = { Pedido };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValidaRemisionesPendientes", ref dr, Parametros, Valores);

                Remision rem;
                while (dr.Read())
                {
                    rem = new Remision();
                    rem.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remision.Add(rem);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ValidarRemisionesPendientesCliente(Sesion sesion, Remision remision, ref List<Remision> remisiones)
        {
            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_TG", "@Id_Ter" };
                object[] Valores = { remision.Id_Emp, remision.Id_Cd, remision.Id_Cte, remision.IdTg, remision.Id_Ter };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spValidaRemisionesPendientesCliente", ref dr, Parametros, Valores);

                Remision rem;
                while (dr.Read())
                {
                    rem = new Remision();
                    rem.Id_Rem = (int)dr.GetValue(dr.GetOrdinal("Id_Rem"));
                    remisiones.Add(rem);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
