using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Collections;

namespace CapaDatos
{
    public class CD_CapPedidoVtaInst
    {
        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Estatus", 
                                          "@Vigencia",
                                          "@CteIni",
                                          "@CteFin",
                                          "@SemIni",
                                          "@SemFin",
                                          "@AnioIni",
                                          "@AnioFin",
                                          "@TerIni",
                                          "@TerFin",
                                          "@Id_U",
                                          "@Cte_Nombre",
                                          "@Credito"
                                      };
                object[] Valores = { 
                                    
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Estatus == "" ? (object)null: pedido.Estatus, 
                                       pedido.Filtro_Vigencia ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_SemIni  == ""? (object)null: pedido.Filtro_SemIni ,
                                       pedido.Filtro_SemFin  == ""? (object)null: pedido.Filtro_SemFin ,
                                       pedido.Filtro_AnioIni  == ""? (object)null: pedido.Filtro_AnioIni ,
                                       pedido.Filtro_AnioFin  == ""? (object)null: pedido.Filtro_AnioFin ,
                                       pedido.Filtro_TerIni ,
                                       pedido.Filtro_TerFin ,
                                       pedido.Id_U,
                                       pedido.Filtro_Nombre  == ""? (object)null: pedido.Filtro_Nombre,
                                       pedido.Filtro_Credito == ""? (object)null : pedido.Filtro_Credito
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_Lista", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    pedido.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    pedido.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.Acs_Vigencia = Convert.ToInt32(dr["Vigencia"]);
                    pedido.Acs_VigenciaStr = dr["VigenciaStr"].ToString();
                    pedido.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    if (dr["Id_TG"] != null)
                    {

                        if (dr["Id_TG"].GetType() != typeof(DBNull))
                        {
                            pedido.Id_TG = Convert.ToInt32(dr["Id_TG"]);
                        }
                    }

                    if (dr["ModalidadGarantia"] != null)
                    {
                        if (dr["ModalidadGarantia"].GetType() != typeof(DBNull))
                        {
                            pedido.ModalidadGarantia = (string)dr["ModalidadGarantia"];
                        }
                    }

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List, string modalidadVenta)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Estatus", 
                                          "@Vigencia",
                                          "@CteIni",
                                          "@CteFin",
                                          "@SemIni",
                                          "@SemFin",
                                          "@AnioIni",
                                          "@AnioFin",
                                          "@TerIni",
                                          "@TerFin",
                                          "@Id_U",
                                          "@Cte_Nombre",
                                          "@Credito",
                                          "@filtroTipoGarantia"
                                      };
                object[] Valores = { 
                                    
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Estatus == "" ? (object)null: pedido.Estatus, 
                                       pedido.Filtro_Vigencia ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_SemIni  == ""? (object)null: pedido.Filtro_SemIni ,
                                       pedido.Filtro_SemFin  == ""? (object)null: pedido.Filtro_SemFin ,
                                       pedido.Filtro_AnioIni  == ""? (object)null: pedido.Filtro_AnioIni ,
                                       pedido.Filtro_AnioFin  == ""? (object)null: pedido.Filtro_AnioFin ,
                                       pedido.Filtro_TerIni ,
                                       pedido.Filtro_TerFin ,
                                       pedido.Id_U,
                                       pedido.Filtro_Nombre  == ""? (object)null: pedido.Filtro_Nombre,
                                       pedido.Filtro_Credito == ""? (object)null : pedido.Filtro_Credito,
                                       modalidadVenta
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_Lista_ModalidadVenta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Seleccionado = false;
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    pedido.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    pedido.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));
                    pedido.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr["Acs_Estatus"].ToString();
                    pedido.Acs_EstatusStr = dr["Acs_EstatusStr"].ToString();
                    pedido.Acs_Vigencia = Convert.ToInt32(dr["Vigencia"]);
                    pedido.Acs_VigenciaStr = dr["VigenciaStr"].ToString();
                    pedido.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    if (dr["Id_TG"] != null)
                    {

                        if (dr["Id_TG"].GetType() != typeof(DBNull))
                        {
                            pedido.Id_TG = Convert.ToInt32(dr["Id_TG"]);
                        }
                    }

                    if (dr["ModalidadGarantia"] != null)
                    {
                        if (dr["ModalidadGarantia"].GetType() != typeof(DBNull))
                        {
                            pedido.ModalidadGarantia = (string)dr["ModalidadGarantia"];
                        }
                    }

                    List.Add(pedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancelar(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs",
                                          "@Acs_Anio",
                                          "@Acs_Semana"
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Acs_Semana
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Baja", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RechazarPedidoVI(PedidoVtaInst pedido, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs",
                                          "@Acs_Anio",
                                          "@Acs_Semana"
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Acs_Semana
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Rechazar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Consultar(ref PedidoVtaInst pedido, string Conexion, ref int verificador, ref Clientes cc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs"
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Id_Acs
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pedido = new PedidoVtaInst();

                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Id_AcsVersion = Convert.ToInt32(dr["Id_AcsVersion"]);
                    pedido.Cte_Nom = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                    pedido.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Acs_Contacto = dr.GetValue(dr.GetOrdinal("Acs_Contacto")).ToString();
                    pedido.Acs_Puesto = dr.GetValue(dr.GetOrdinal("Acs_Puesto")).ToString();
                    pedido.Acs_Telefono = dr.GetValue(dr.GetOrdinal("Acs_Telefono")).ToString();
                    pedido.Acs_email = dr.GetValue(dr.GetOrdinal("Acs_email")).ToString();
                    pedido.Acs_ReqOrden = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_ReqOrden")));
                    pedido.Acs_ReqDocReposicion = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocReposicion")));
                    pedido.Acs_ReqDocFolio = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Acs_RecDocFolio")));
                    pedido.Acs_ReqDocOtro = dr["Acs_RecDocOtro"].ToString();
                    pedido.Acs_Modalidad = dr["Acs_Modalidad"].ToString().Trim();
                    cc.Cte_Calle = dr.GetValue(dr.GetOrdinal("Cte_Calle")).ToString();
                    cc.Cte_Numero = dr.GetValue(dr.GetOrdinal("Cte_Numero")).ToString();
                    cc.Cte_Colonia = dr.GetValue(dr.GetOrdinal("Cte_Colonia")).ToString();
                    cc.Cte_Municipio = dr.GetValue(dr.GetOrdinal("Cte_Municipio")).ToString();
                    cc.Cte_Cp = dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cc.Cte_Estado = dr.GetValue(dr.GetOrdinal("Cte_Estado")).ToString();
                    pedido.Acs_PedidoEncargadoEnviar = dr["Acs_PedidoEncargadoEnviar"].ToString();
                    pedido.Acs_PedidoPuesto = dr["Acs_PedidoPuesto"].ToString();
                    pedido.Acs_PedidoTelefono = dr["Acs_PedidoTelefono"].ToString();
                    pedido.Acs_PedidoEmail = dr["Acs_PedidoEmail"].ToString();
                    pedido.Acs_Contacto3 = dr["Acs_Contacto3"].ToString();
                    pedido.Acs_Telefono3 = dr["Acs_Telefono3"].ToString();
                    pedido.Acs_Email3 = dr["Acs_Email3"].ToString();


                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarDet(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int? idTG)
        {
            try
            {
                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_TG"
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       idTG
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] { 
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                        dr.GetValue(dr.GetOrdinal("Id_Uni")),
                        -100,
                        -100,
                        -100,
                        dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                        dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                        dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                        Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                        0,
                        DBNull.Value,
                        dr.GetValue(dr.GetOrdinal("Id_TG")),
                        dr.GetValue(dr.GetOrdinal("Id_Acs"))
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

                string[] ParametrosFec = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio"
                                      };
                object[] ValoresFec = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio
                                   };

                SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, ParametrosFec, ValoresFec);
                PedidoVtaInst p;
                while (dr2.Read())
                {
                    if (dr2["Id_TG"] != null)
                    {
                        if (dr2["Id_TG"].ToString() == idTG.Value.ToString())
                        {
                            p = new PedidoVtaInst();
                            p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                            p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                            p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                            p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                            p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                            p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                            p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                            p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                            p.Acs_Documento = dr2["Acs_Documento"].ToString();

                            List.Add(p);
                        }
                    }
                    else if (idTG.Value == 0)
                    {
                        p = new PedidoVtaInst();
                        p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                        p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                        p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                        p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                        p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                        p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                        p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                        p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                        p.Acs_Documento = dr2["Acs_Documento"].ToString();

                        List.Add(p);
                    }

                }

                CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] { 
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Id_Prd")),
                        dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                        dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                        dr.GetValue(dr.GetOrdinal("Id_Uni")),
                        -100,
                        -100,
                        -100,
                        dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                        dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                        dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                        dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                        Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                        dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                        dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                        Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                        dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                        0,
                        DBNull.Value,
                        dr.GetValue(dr.GetOrdinal("Id_TG")),
                        dr.GetValue(dr.GetOrdinal("Id_Acs"))
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);
                //SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, Parametros, Valores);
                //PedidoVtaInst p;
                //while (dr2.Read())
                //{
                //    p = new PedidoVtaInst();
                //    p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                //    p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                //    p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                //    p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                //    p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                //    p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                //    p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                //    p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                //    p.Acs_Documento = dr2["Acs_Documento"].ToString();
                //    List.Add(p);

                //}

                //CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta el detalle del resto de productos de un pedido en captura de garantía.
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="List"></param>
        /// <param name="Conexion"></param>
        /// <param name="dt"></param>
        /// <param name="Id_TG">Identificador del tipo de garantía</param>
        public void ConsultarDet_Resto(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, string Conexion, ref System.Data.DataTable dt, int Id_TG)
        {
            try
            {
                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio",
                                          "@Id_Cte"
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoViDet_Consultar_Resto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    bool cargar = false;
                    if (Id_TG == 0)
                    {
                        if (dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                        {
                            //cargar
                            cargar = true;
                        }
                        else //no es nulo: validar que el tipo de garantía coincida
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    else //no es cero: validar que el tipo de garantía coincida
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("Id_TG"))) // primero se valida que no sea nulo
                        {
                            if (dr.GetValue(dr.GetOrdinal("Id_TG")).ToString() == Id_TG.ToString())
                            {
                                //cargar
                                cargar = true;
                            }
                        }
                    }
                    if (cargar == true)
                    {
                        dt.Rows.Add(new object[] { 
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            -100,
                            -100,
                            -100,
                            dr.GetValue(dr.GetOrdinal("Acs_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Acs_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            0,
                            DBNull.Value,
                            dr.GetValue(dr.GetOrdinal("Id_TG")),
                            dr.GetValue(dr.GetOrdinal("Id_Acs"))
                        });
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                //CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);
                //SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, Parametros, Valores);
                //PedidoVtaInst p;
                //while (dr2.Read())
                //{
                //    p = new PedidoVtaInst();
                //    p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                //    p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                //    p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                //    p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                //    p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                //    p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                //    p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                //    p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                //    p.Acs_Documento = dr2["Acs_Documento"].ToString();
                //    List.Add(p);

                //}

                //CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDetAcys(PedidoVtaInst pedido, ref  List<PedidoVtaInst> List, string Conexion)
        {
            try
            {
                SqlDataReader dr2 = null;

                CapaDatos.CD_Datos CapaDatos2 = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Acs",
                                          "@Semana",
                                          "@Anio"
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd, 
                                       pedido.Id_Acs,
                                       pedido.Acs_Semana,
                                       pedido.Acs_Anio
                                   };



                SqlCommand sqlcmd2 = CapaDatos2.GenerarSqlCommand("spCapPedidoVI_ConsultaFec", ref dr2, Parametros, Valores);
                PedidoVtaInst p;
                while (dr2.Read())
                {
                    p = new PedidoVtaInst();
                    p.Id_Prd = Convert.ToInt32(dr2["Id_Prd"]);
                    p.Prd_Descripcion = dr2["Prd_Descripcion"].ToString();
                    p.Acs_Lunes = Convert.ToBoolean(dr2["Acs_Lunes"]);
                    p.Acs_Martes = Convert.ToBoolean(dr2["Acs_Martes"]);
                    p.Acs_Miercoles = Convert.ToBoolean(dr2["Acs_Miercoles"]);
                    p.Acs_Jueves = Convert.ToBoolean(dr2["Acs_Jueves"]);
                    p.Acs_Viernes = Convert.ToBoolean(dr2["Acs_Viernes"]);
                    p.Acs_Sabado = Convert.ToBoolean(dr2["Acs_Sabado"]);
                    p.Acs_Documento = dr2["Acs_Documento"].ToString();
                    List.Add(p);

                }

                CapaDatos2.LimpiarSqlcommand(ref sqlcmd2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private object Str(object p)
        {
            switch (p.ToString().ToUpper())
            {
                case "F": return "Factura";
                case "R": return "Remisión";
                default: return "";
            }
        }

        private object Nombre(object p)
        {
            switch (p.ToString().ToLower())
            {
                case "l": return "Lunes";
                case "m": return "Martes";
                case "mi": return "Miercoles";
                case "j": return "Jueves";
                case "v": return "Viernes";
                case "s": return "Sabado";
                default: return "";
            }
        }
        public void ConsultarPedidoExistente(PedidoVtaInst pvi, int Id_Prd, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Prd",
                                          "@Semana",
                                          "@Id_Cte",
                                          "@Id_ter"
                                      };
                object[] Valores = { 
                                        pvi.Id_Emp,
                                        pvi.Id_Cd,
                                        Id_Prd,
                                        pvi.Acs_Semana,
                                        pvi.Id_Cte,
                                        pvi.Id_Ter
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Existente", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
/*
        public void Insertar(PedidoVtaInst pedido, System.Data.DataTable dt, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Cte", 
                                        "@Ped_Fecha",
	                                    "@Id_Rik", 
	                                    "@Id_Ter", 
	                                    "@Pedido_del", 
	                                    "@Requisicion", 
	                                    "@Ped_Solicito", 
                                        "@Ped_Flete", 
                                        "@Ped_OrdenEntrega", 
                                        "@Ped_CondEntrega", 
                                        "@Ped_FechaEntrega", 
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel", 
	                                    "@Ped_SolicitoEmail", 
	                                    "@Ped_SolicitoPuesto", 
	                                    "@Ped_ConsignadoCalle", 
	                                    "@Ped_ConsignadoNo", 
	                                    "@Ped_ConsignadoCp", 
	                                    "@Ped_ConsignadoMunicipio", 
	                                    "@Ped_ConsignadoEstado", 
	                                    "@Ped_ConsignadoColonia", 
	                                    "@Ped_ReqOrden", 
	                                    "@Ped_OrdenCompra", 
	                                    "@Ped_AcysSemana", 
	                                    "@Ped_AcysAnio", 
	                                    "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys", 
                                        "@OcAcys"

                //                                pedido.FechaFacAcys = rdFechaF.SelectedDate.Value;
                //pedido.PedAcys = this.TxtPed_PedAcys.Text.Trim();
                //pedido.ReqAcys = this.TxtPed_ReqAcys.Text.Trim();
                //pedido.OcAcys = this.TxtPed_OCAcys.Text.Trim();
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel, 
	                                    pedido.Ped_SolicitoEmail, 
	                                    pedido.Ped_SolicitoPuesto, 
	                                    pedido.Ped_ConsignadoCalle, 
	                                    pedido.Ped_ConsignadoNo, 
	                                    pedido.Ped_ConsignadoCp, 
	                                    pedido.Ped_ConsignadoMunicipio, 
	                                    pedido.Ped_ConsignadoEstado, 
	                                    pedido.Ped_ConsignadoColonia, 
	                                    pedido.Ped_ReqOrden, 
	                                    pedido.Ped_OrdenCompra, 
	                                    pedido.Ped_AcysSemana, 
	                                    pedido.Ped_AcysAnio, 
	                                    pedido.Id_Acs, 
                                        pedido.FechaFacAcys, 
                                        pedido.PedAcys, 
                                        pedido.ReqAcys, 
                                        pedido.OcAcys
                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    InsertarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);

                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
*/
        /**
         * Versión sobrecargada de [Insertar] con el fin de incluir el tipo de garantia asociada al pedido 
         **/
        public void Insertar(PedidoVtaInst pedido, System.Data.DataTable dt, string Conexion, ref int verificador, int? idTG, int? idAcsVersion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Cte", 
                                        "@Ped_Fecha",
	                                    "@Id_Rik", 
	                                    "@Id_Ter", 
	                                    "@Pedido_del", 
	                                    "@Requisicion", 
	                                    "@Ped_Solicito", 
                                        "@Ped_Flete", 
                                        "@Ped_OrdenEntrega", 
                                        "@Ped_CondEntrega", 
                                        "@Ped_FechaEntrega", 
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel", 
	                                    "@Ped_SolicitoEmail", 
	                                    "@Ped_SolicitoPuesto", 
	                                    "@Ped_ConsignadoCalle", 
	                                    "@Ped_ConsignadoNo", 
	                                    "@Ped_ConsignadoCp", 
	                                    "@Ped_ConsignadoMunicipio", 
	                                    "@Ped_ConsignadoEstado", 
	                                    "@Ped_ConsignadoColonia", 
	                                    "@Ped_ReqOrden", 
	                                    "@Ped_OrdenCompra", 
	                                    "@Ped_AcysSemana", 
	                                    "@Ped_AcysAnio", 
	                                    "@Id_Acs",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys", 
                                        "@OcAcys",
                                        "@Id_TG",
                                        "@Id_AcsVersion"

                //                                pedido.FechaFacAcys = rdFechaF.SelectedDate.Value;
                //pedido.PedAcys = this.TxtPed_PedAcys.Text.Trim();
                //pedido.ReqAcys = this.TxtPed_ReqAcys.Text.Trim();
                //pedido.OcAcys = this.TxtPed_OCAcys.Text.Trim();
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel, 
	                                    pedido.Ped_SolicitoEmail, 
	                                    pedido.Ped_SolicitoPuesto, 
	                                    pedido.Ped_ConsignadoCalle, 
	                                    pedido.Ped_ConsignadoNo, 
	                                    pedido.Ped_ConsignadoCp, 
	                                    pedido.Ped_ConsignadoMunicipio, 
	                                    pedido.Ped_ConsignadoEstado, 
	                                    pedido.Ped_ConsignadoColonia, 
	                                    pedido.Ped_ReqOrden, 
	                                    pedido.Ped_OrdenCompra, 
	                                    pedido.Ped_AcysSemana, 
	                                    pedido.Ped_AcysAnio, 
	                                    pedido.Id_Acs, 
                                        pedido.FechaFacAcys, 
                                        pedido.PedAcys, 
                                        pedido.ReqAcys, 
                                        pedido.OcAcys,
                                        idTG,
                                        idAcsVersion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Insertar_Garantias", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    InsertarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd, idTG.Value);

                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        private static void InsertarDet(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {
            if (dt.Rows.Count == 0) return;
            string[] ParametrosAcys;
            object[] ValoresAcys;

            Parametros = new string[]{ 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Ped", 
		                                "@Id_PedDet", 
		                                "@Id_Ter", 
		                                "@Id_Prd", 
		                                "@Ped_Precio", 
		                                "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToBoolean(dt.Rows[x]["Mod"])) //MODIFICA EL ACYS (DETALLE Y CALENDARIO)
                {
                    if (Convert.ToInt32(dt.Rows[x]["Id_PrdOld"]) == -1) //NUEVO
                    {
                        ParametrosAcys = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {                         
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
		                    0, 
		                    pedido.Id_Acs, 
		                    dt.Rows[x]["Id_Prd"], 
		                    dt.Rows[x]["Prd_Cantidad"], 
		                    dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1), 
		                    dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
		                    dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                    else//ACTUALIZACION
                    {
                        ParametrosAcys = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
		                    "@Id_Prd", 
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {                         
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
		                    0, 
		                    pedido.Id_Acs, 
		                    dt.Rows[x]["Id_Prd"], 
		                    dt.Rows[x]["Prd_Cantidad"], 
		                    dt.Rows[x]["Acs_Doc"], 
		                    dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
		                    dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Modificar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                }
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {


                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"]
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);

                }
            }

            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
        }

        private static void InsertarDet(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd, int idTg)
        {
            if (dt.Rows.Count == 0) return;
            string[] ParametrosAcys;
            object[] ValoresAcys;

            Parametros = new string[]{ 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Ped", 
		                                "@Id_PedDet", 
		                                "@Id_Ter", 
		                                "@Id_Prd", 
		                                "@Ped_Precio", 
		                                "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys",
                                        "@Id_TG"
                                      };

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToBoolean(dt.Rows[x]["Mod"])) //MODIFICA EL ACYS (DETALLE Y CALENDARIO)
                {
                    if (Convert.ToInt32(dt.Rows[x]["Id_PrdOld"]) == -1) //NUEVO
                    {
                        ParametrosAcys = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {                         
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
		                    0, 
		                    pedido.Id_Acs, 
		                    dt.Rows[x]["Id_Prd"], 
		                    dt.Rows[x]["Prd_Cantidad"], 
		                    dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1), 
		                    dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
		                    dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                    else//ACTUALIZACION
                    {
                        ParametrosAcys = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
		                    "@Id_Prd", 
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                        ValoresAcys = new object[] {                         
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
		                    0, 
		                    pedido.Id_Acs, 
		                    dt.Rows[x]["Id_Prd"], 
		                    dt.Rows[x]["Prd_Cantidad"], 
		                    dt.Rows[x]["Acs_Doc"], 
		                    dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
		                    dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Modificar", ref verificador, ParametrosAcys, ValoresAcys);
                    }
                }
                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {


                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"],
                                        idTg
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_InsertarV2", ref verificador, Parametros, Valores);

                }
            }

            Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
            Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
            sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
        }

        public void Modificar(PedidoVtaInst pedido, DataTable dt, string Conexion, int captado, ref int verificador, ArrayList eliminados)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                if (captado > 0)
                {
                    string[] Parametros2 = { 
                                           "@Id_Emp", 
	                                       "@Id_Cd",
	                                       "@Credito", 
	                                       "@Id_PedVI" 
                                       };
                    object[] Valores2 = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        2,
                                        captado
                                     };

                    SqlCommand sqlcmd2 = CapaDatos.GenerarSqlCommand("spProPedido_DesasignacionAutomatica", ref verificador, Parametros2, Valores2);
                }

                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Cte", 
                                        "@Ped_Fecha",
	                                    "@Id_Rik", 
	                                    "@Id_Ter", 
	                                    "@Pedido_del", 
	                                    "@Requisicion", 
	                                    "@Ped_Solicito", 
                                        "@Ped_Flete", 
                                        "@Ped_OrdenEntrega", 
                                        "@Ped_CondEntrega", 
                                        "@Ped_FechaEntrega", 
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo",
                                        "@Ped_SolicitoTel", 
	                                    "@Ped_SolicitoEmail", 
	                                    "@Ped_SolicitoPuesto", 
	                                    "@Ped_ConsignadoCalle", 
	                                    "@Ped_ConsignadoNo", 
	                                    "@Ped_ConsignadoCp", 
	                                    "@Ped_ConsignadoMunicipio", 
	                                    "@Ped_ConsignadoEstado", 
	                                    "@Ped_ConsignadoColonia", 
	                                    "@Ped_ReqOrden", 
	                                    "@Ped_OrdenCompra", 
	                                    "@Ped_AcysSemana", 
	                                    "@Ped_AcysAnio", 
	                                    "@Id_Acs",
                                        "@Id_Ped",
                                        "@FechaFacAcys",
                                        "@PedAcys",
                                        "@ReqAcys", 
                                        "@OcAcys"

                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo,
                                        pedido.Ped_SolicitoTel, 
	                                    pedido.Ped_SolicitoEmail, 
	                                    pedido.Ped_SolicitoPuesto, 
	                                    pedido.Ped_ConsignadoCalle, 
	                                    pedido.Ped_ConsignadoNo, 
	                                    pedido.Ped_ConsignadoCp, 
	                                    pedido.Ped_ConsignadoMunicipio, 
	                                    pedido.Ped_ConsignadoEstado, 
	                                    pedido.Ped_ConsignadoColonia, 
	                                    pedido.Ped_ReqOrden, 
	                                    pedido.Ped_OrdenCompra, 
	                                    pedido.Ped_AcysSemana, 
	                                    pedido.Ped_AcysAnio, 
	                                    pedido.Id_Acs,
                                        pedido.Id_Ped,
                                        pedido.FechaFacAcys, 
                                        pedido.PedAcys, 
                                        pedido.ReqAcys, 
                                        pedido.OcAcys
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Modificar", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;

                if (verificador > -1)
                {
                    verificador = -1;
                    ModificarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);

                    foreach (int i in eliminados)
                    {
                        Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped", "@Id_Prd" };
                        Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, i };
                        sqlcmd = CapaDatos.GenerarSqlCommand("spProPedidoVi_Eliminar", ref verificador, Parametros, Valores);
                    }

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_CorrigeDet", ref verificador, Parametros, Valores);

                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador, Parametros, Valores);
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = pedido.Id_Ped;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        private static void ModificarDet(PedidoVtaInst pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {
            if (dt.Rows.Count == 0) return;
            string[] ParametrosAcys;
            object[] ValoresAcys;

            Parametros = new string[]{ 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Ped", 
		                                "@Id_PedDet", 
		                                "@Id_Ter", 
		                                "@Id_Prd", 
		                                "@Ped_Precio", 
		                                "@Ped_Cantidad",
                                        "@Accion",
                                        "@Ped_AcysSemana",
                                        "@Id_Acys",
                                        "@Acs_Anio",
                                        "@FecAsig",
                                        "@UsrAsig",
                                        "@Ped_Doc",
                                        "@Ped_ModAcys"
                                      };


            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Convert.ToBoolean(dt.Rows[x]["Mod"])) //MODIFICA EL ACYS (DETALLE Y CALENDARIO)
                {
                    ParametrosAcys = new string[] { 
                    		"@Id_Emp", 
		                    "@Id_Cd",  
		                    "@Id_AcsDet",  
		                    "@Id_Acs",  
		                    "@Id_Prd",  
		                    "@Acs_Cantidad",  
		                    "@Acs_Documento",  
		                    "@Acs_Sabado",  
		                    "@Acs_Viernes",  
		                    "@Acs_Jueves",  
		                    "@Acs_Miercoles", 
		                    "@Acs_Martes",  
		                    "@Acs_Lunes",  
		                    "@Acs_Frecuencia",
                            "@Acs_Precio",
                            "@Semana"
                        };

                    ValoresAcys = new object[] {                         
                            pedido.Id_Emp,
                            pedido.Id_Cd ,
		                    0, 
		                    pedido.Id_Acs, 
		                    dt.Rows[x]["Id_Prd"], 
		                    dt.Rows[x]["Prd_Cantidad"], 
		                    dt.Rows[x]["Acs_Doc"], 
		                    dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
		                    dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
		                    dt.Rows[x]["Acs_Frecuencia"],
                            dt.Rows[x]["Prd_Precio"],
                            pedido.Acs_Semana
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Insertar", ref verificador, ParametrosAcys, ValoresAcys);

                    //}
                    //else//ACTUALIZACION
                    //{
                    //    ParametrosAcys = new string[] { 
                    //        "@Id_Emp", 
                    //        "@Id_Cd",  
                    //        "@Id_AcsDet",  
                    //        "@Id_Acs",  
                    //        "@Id_Prd", 
                    //        "@Acs_Cantidad",  
                    //        "@Acs_Documento",  
                    //        "@Acs_Sabado",  
                    //        "@Acs_Viernes",  
                    //        "@Acs_Jueves",  
                    //        "@Acs_Miercoles", 
                    //        "@Acs_Martes",  
                    //        "@Acs_Lunes",  
                    //        "@Acs_Frecuencia",
                    //        "@Acs_Precio",
                    //        "@Semana"
                    //    };

                    //    ValoresAcys = new object[] {                         
                    //        pedido.Id_Emp,
                    //        pedido.Id_Cd ,
                    //        0, 
                    //        pedido.Id_Acs, 
                    //        dt.Rows[x]["Id_Prd"], 
                    //        dt.Rows[x]["Prd_Cantidad"], 
                    //        dt.Rows[x]["Acs_Doc"], 
                    //        dt.Rows[x]["Acs_Dia"].ToString() == "S" ? true: false,
                    //        dt.Rows[x]["Acs_Dia"].ToString() == "V" ? true: false,
                    //        dt.Rows[x]["Acs_Dia"].ToString() == "J" ? true: false,
                    //        dt.Rows[x]["Acs_Dia"].ToString() == "Mi" ? true: false,
                    //        dt.Rows[x]["Acs_Dia"].ToString() == "M" ? true: false,
                    //        dt.Rows[x]["Acs_Dia"].ToString() == "L" ? true: false,
                    //        dt.Rows[x]["Acs_Frecuencia"],
                    //        dt.Rows[x]["Prd_Precio"],
                    //        pedido.Acs_Semana
                    //    };

                    //    sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_Modificar", ref verificador, ParametrosAcys, ValoresAcys);
                    //}
                }
                var prueba = dt.Rows[x]["Acs_Doc"].ToString();

                if (Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]) != 0)
                {
                    //INSERTA EL DETALLE EN EL PEDIDO Y ACTUALIZA EL ESTATUS EN EL CALENDARIO DE ACYS
                    Valores = new object[] { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        pedido.Id_Ter,
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x,
                                        pedido.Acs_Semana,
                                        pedido.Id_Acs,
                                        pedido.Acs_Anio,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        // dt.Rows[x]["Acs_Doc"].ToString() ==""?"":dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1)
                                        dt.Rows[x]["Acs_Doc"].ToString().Substring(0,1),
                                        dt.Rows[x]["Mod"]

                                   };
                    if (Convert.ToInt32(dt.Rows[x]["Id_PrdOld"]) == -1) //NUEVO
                    {
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);
                    }
                    else //ACTUALIZAR
                    {
                        sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Modificar", ref verificador, Parametros, Valores);
                    }
                }
            }
        }

        public void ListaFacturacion(PedidoVtaInst pedido, string Conexion, ref List<PedidoVtaInst> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Cte_Nombre",
                                          "@Id_CteIni",
                                          "@Id_CteFin",
                                          "@Ped_FechaIni",
                                          "@Ped_FechaFin",
                                          "@Ped_FechaFIni",
                                          "@Ped_FechaFFin",
                                          "@Estatus",
                                          "@Id_U",
                                          "@Ped_Doc"
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_FecFIni,
                                       pedido.Filtro_FecFFin,
                                       pedido.Filtro_Estatus,
                                       pedido.Filtro_usuario,
                                       pedido.Filtro_Documento
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoVi_Lista", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    pedido = new PedidoVtaInst();
                    pedido.Id_Ped = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    pedido.Ped_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Ped_Fecha"));
                    pedido.Acs_Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Ped_FechFactAcys"));
                    pedido.Ped_Comentarios = dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    pedido.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    pedido.Cte_Nom = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Id_Ter = (int)dr.GetValue(dr.GetOrdinal("Id_Ter"));
                    pedido.Ped_Total = (double)dr.GetValue(dr.GetOrdinal("Ped_Total"));
                    pedido.Ped_Asign = dr.GetValue(dr.GetOrdinal("Ped_Asign")).ToString();
                    pedido.Ped_AsignStr = AsignadoStr(dr.GetValue(dr.GetOrdinal("Ped_Asign")).ToString());
                    pedido.Rut_Descripcion = dr.GetValue(dr.GetOrdinal("Rut_Descripcion")).ToString();
                    pedido.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    pedido.TG_Nombre = dr.GetValue(dr.GetOrdinal("TG_Nombre")).ToString();
                    List.Add(pedido);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //EDSG 08042015
        private string Tipo(string p)
        {
            switch (p)
            {
                case "1":
                    return "Sin distribución";
                case "2":
                    return "Con distribución";
                case "3":
                    return "Venta instalada";
                case "4":
                    return "Venta nueva";
                case "5":
                    return "Internet";
                default:
                    return "";
            }
        }


        private string AsignadoStr(string p)
        {
            switch (p)
            {
                case "A": return "Si";
                case "N": return "No";
                case "P": return "Parcial";
                default: return "";
            }
        }

        public void ConsultarAAAEspecial(int Id_Emp, int Id_Cd, double Id_Cte, string Id_prd, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Cte",
                                          "@Id_Prd"
                                      };
                object[] Valores = { 
                                        Id_Emp,
                                        Id_Cd,
                                        Id_Cte,
                                        Id_prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPrecioAAAEspecial", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
