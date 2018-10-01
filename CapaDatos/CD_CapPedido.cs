using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapPedido
    {
        public void ConsultarTotalPedidosCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoCantidadEnCd_Consultar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPedido(ref Pedido pedido, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_ConsultarV2", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    string entrega = dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString();
                    if (!string.IsNullOrEmpty(entrega))
                        pedido.Ped_CondEntrega = Convert.ToInt32(entrega);
                    else
                        pedido.Ped_CondEntrega = 0;
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Rik_Nombre = dr.IsDBNull(dr.GetOrdinal("Rik_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Ped_Fecha = dr.IsDBNull(dr.GetOrdinal("Ped_Fecha")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    pedido.Ped_FechaEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_FechaEntrega")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    pedido.Ped_SolicitoTel = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoTel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoTel")).ToString();
                    pedido.Ped_SolicitoEmail = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoEmail")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoEmail")).ToString();
                    pedido.Ped_SolicitoPuesto = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoPuesto")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoPuesto")).ToString();
                    pedido.Ped_ConsignadoCalle = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCalle")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCalle")).ToString();
                    pedido.Ped_ConsignadoNo = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoNo")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoNo")).ToString();
                    pedido.Ped_ConsignadoCp = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCp")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCp")).ToString();
                    pedido.Ped_ConsignadoMunicipio = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoMunicipio")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoMunicipio")).ToString();
                    pedido.Ped_ConsignadoEstado = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoEstado")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoEstado")).ToString();
                    pedido.Ped_ConsignadoColonia = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoColonia")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoColonia")).ToString();
                    pedido.Ped_ReqOrden = dr.IsDBNull(dr.GetOrdinal("Ped_ReqOrden")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ped_ReqOrden")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();
                    pedido.Ped_AcysSemana = dr.IsDBNull(dr.GetOrdinal("Ped_AcysSemana")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysSemana")));
                    pedido.Ped_AcysAnio = dr.IsDBNull(dr.GetOrdinal("Ped_AcysAnio")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysAnio")));
                    pedido.Id_Acs = dr.IsDBNull(dr.GetOrdinal("Id_Acs")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.ReqAcys = dr.IsDBNull(dr.GetOrdinal("Ped_ReqAcys")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ReqAcys")).ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                    {
                        pedido.Id_TG = (int)dr.GetValue(dr.GetOrdinal("Id_TG"));
                    }

                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ConsultaPedidoFacturacion(ref Pedido pedido, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                bool pedidoEncontrado = false;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pedidoEncontrado = true;
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    pedido.Ped_CondEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_CondEntrega")) ? 0 : (string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString()) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CondEntrega"))));
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Estatus = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.Cant_Facturada = dr.IsDBNull(dr.GetOrdinal("cant_Facturada")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cant_Facturada")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();

                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return pedidoEncontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ConsultaPedidoTieneUnidadesRemisionadas(ref Pedido pedido, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoTieneUnidRemisionadas_Consultar", ref dr, Parametros, Valores);
                bool unidadesRem = false;
                if (dr.HasRows)
                {
                    dr.Read();
                    unidadesRem = dr.IsDBNull(dr.GetOrdinal("UniRem")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("UniRem")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return unidadesRem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPedidoDet(Pedido pedido, string Conexion, ref DataTable dt)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (pedido.Ped_Captacion == true)
                    {
                        dt.Rows.Add(new object[] { 
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            dr.GetValue(dr.GetOrdinal("Prd_RemFact")),
                            dr.GetValue(dr.GetOrdinal("Ped_Asignar"))
                       });
                    }
                    else
                    {
                        dt.Rows.Add(new object[] { 
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")) 
                    });
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
        public void ConsultaPedidoDetDisp(Pedido pedido, string Conexion, int? facturando, ref DataTable dt)
        {//rm
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "@Ped_Captacion", "@Filtro_Doc", "@ConMinimo", "@Facturando" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, null, pedido.Filtro_Doc, null, facturando };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Disponible"))) > 0)
                    {
                        dt.Rows.Add(new object[] { 
                                               dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                                               dr.GetValue(dr.GetOrdinal("Id_Ter")),
                                               dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                                               dr.GetValue(dr.GetOrdinal("Id_Prd")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                                               dr.GetValue(dr.GetOrdinal("Disponible")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Importe")), 
                                               dr.GetValue(dr.GetOrdinal("Id_Rem"))   
                        });
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
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
        public void InsertarPedido(Pedido pedido, DataTable dt, string Conexion, ref int verificador)
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
                                        "@Ped_Tipo"
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
                                        pedido.Ped_Tipo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Insertar", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;
                if (verificador > -1)
                {
                    verificador = -1;
                    ModificarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);
                    CapaDatos.CommitTrans();
                }
                else
                    CapaDatos.RollBackTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ModificarPedido(Pedido pedido, DataTable dt, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = null;
                object[] Valores = null;
                SqlCommand sqlcmd = default(SqlCommand);
                Parametros = new string[] { 
                                        	"@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Ped",
                                            "@Ped_Fecha",
                                            "@Id_Cte",
                                            "@Id_Rik",
                                            "@Id_Ter",
                                            "@Pedido_del",
                                            "@Ped_Solicito",
                                            "@Requisicion",
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
	                                        "@Id_U" 
                                      };
                Valores = new object[]{                                        
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Cte,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Ped_Solicito,
                                        pedido.Requisicion,
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
                                        pedido.Id_U
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Modificar", ref verificador, Parametros, Valores);
                if (verificador == 1)
                {
                    ModificarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        private static void ModificarDet(Pedido pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {
            if (dt.Rows.Count == 0) return;

            Parametros = new string[]{ 
	                                    "@Id_Emp", 
		                                "@Id_Cd", 
		                                "@Id_Ped", 
		                                "@Id_PedDet", 
		                                "@Id_Ter", 
		                                "@Id_Prd", 
		                                "@Ped_Precio", 
		                                "@Ped_Cantidad",
                                        "@Accion"
                                      };
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                Valores = new object[] { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        dt.Rows[x]["Id_Ter"],
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);
            }
        }
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
        private string Estatus(string p)
        {
            switch (p)
            {
                case "O": return "Confirmado";
                case "C": return "Capturado";
                case "I": return "Impreso";
                case "U": return "Autorizado";
                case "A": return "Asignado";
                case "F": return "Facturado";
                case "R": return "Remisionado";
                case "X": return "Facturado/Remisionado";
                case "E": return "Embarque";
                case "N": return "Entregado";
                case "D": return "Baja por administración";
                case "B": return "Baja por cliente";
                default: return "";
            }
        }
        public void Baja(Pedido ped, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ped",
                                        "@Estatus"
                                      };
                object[] Valores = { 
                                        ped.Id_Emp,
                                        ped.Id_Cd,
                                        ped.Id_Ped,
                                        ped.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Baja", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Imprimir(Pedido ped, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ped",
                                        "@Estatus"
                                      };
                object[] Valores = { 
                                        ped.Id_Emp,
                                        ped.Id_Cd,
                                        ped.Id_Ped,
                                        ped.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Imprimir", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ConsultaPedido(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                          "@Filtro_Tipo",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_Estatus",
                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",
                                          "@Filtro_usuario",
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre == "" ? (object)null : pedido.Filtro_Nombre,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_Tipo,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin ,
                                       pedido.Filtro_Estatus,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_usuario == "" || pedido.Filtro_usuario == "-1" ? (object)null : pedido.Filtro_usuario 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Lista", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    p.Ped_TipoStr = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? "" : Tipo(dr.GetValue(dr.GetOrdinal("Ped_Tipo")).ToString());
                    p.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    p.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    p.Estatus = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    p.EstatusStr = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString());
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    p.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    p.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    p.Facturacion = dr.IsDBNull(dr.GetOrdinal("Cte_Facturacion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Facturacion")));
                    List.Add(p);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPedidoAsig_Admin(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                         
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                         
                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",
                                        
                                          "@Filtro_RutaIni",
                                          "@Filtro_RutaFin",

                                          "@Filtro_SectorIni",
                                          "@Filtro_SectorFin",

                                          "@Filtro_Credito"

                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre  ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin== ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_RutaInicial,
                                       pedido.Filtro_RutaFinal,
                                       pedido.Filtro_SectorInicial,
                                       pedido.Filtro_SectorFinal,
                                       pedido.Filtro_Credito
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_Cantidad = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Ped_CantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_CantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantidadDisponible")));
                    p.Ped_ImporteOrdenado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteOrdenado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteOrdenado")));
                    p.Ped_ImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteDisponible")));
                    p.Ped_Asignado = dr.IsDBNull(dr.GetOrdinal("Ped_Asignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignado")));
                    p.Ped_ImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteAsignado")));
                    p.Sector = dr["Sector"].ToString();
                    p.Ruta = dr["Ruta"].ToString();
                    p.Secuencia = dr.IsDBNull(dr.GetOrdinal("Secuencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Secuencia")));
                    p.Ped_PorcentajeCantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")));
                    p.Ped_PorcentajeImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")));
                    p.Ped_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeAsignado")));
                    p.Ped_PorcentajeImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPedidoAsig(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {   
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ped"
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Consultar", ref dr, Parametros, Valores);

                PedidoDet p;
                while (dr.Read())
                {
                    p = new PedidoDet();
                    p.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    p.Prd_Desc = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    p.Prd_Ord = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Prd_OrdDisp = dr.IsDBNull(dr.GetOrdinal("Prd_OrdDisp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_OrdDisp")));

                    p.Ped_CantF = dr.IsDBNull(dr.GetOrdinal("Ped_CantF")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantF")));
                    p.Ped_CantR = dr.IsDBNull(dr.GetOrdinal("Ped_CantR")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantR")));
                    p.Prd_Asig = dr.IsDBNull(dr.GetOrdinal("Ped_Asignar")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignar")));
                    p.Prd_Faltante = dr.IsDBNull(dr.GetOrdinal("Prd_Faltante")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Faltante")));
                    p.Prd_Existencia = dr.IsDBNull(dr.GetOrdinal("Prd_InvFinal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    p.Prd_Disponible = dr.IsDBNull(dr.GetOrdinal("Prd_Disponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disponible")));
                    p.Prd_NoConf = dr.IsDBNull(dr.GetOrdinal("Prd_NoConf")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoConf")));
                    p.Prd_NoEnc = dr.IsDBNull(dr.GetOrdinal("Prd_NoEnc")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoEnc")));
                    p.Id_PedDet = dr.IsDBNull(dr.GetOrdinal("Id_PedDet")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PedDet")));
                    p.Prd_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Prd_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_PorcentajeAsignado")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AsignarPrdXPed(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();


                string[] Parametros = {   
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd",
                                          "@Id_Asig",
                                          "@FecAsig",
                                          "@UsrAsig",
                                          "@Id_PedDet"
                                          ,"@Prd_NoConf"
                                          ,"@Prd_NoEnc"
                                      };
                object[] Valores = null;

                for (int x = 0; x < list.Count; x++)
                {
                    Valores = new object[] { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        list[x].Id_Prd,
                                        list[x].Prd_Asig,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        list[x].Id_PedDet
                                        ,list[x].Prd_NoConf
                                        ,list[x].Prd_NoEnc
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed", ref verificador, Parametros, Valores);
                    if (verificador == 2 || verificador == 3)
                    {
                        CapaDatos.RollBackTrans();
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                        return;
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
        public void AsignarRuta(int id_Ped, string sector, string ruta, int secuencia, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();


                string[] Parametros = {   
                                          "@Id_Ped", 
                                          "@Sector",
                                          "@Ruta",
                                          "@Secuencia"
                                      };
                object[] Valores = new object[] { 
                                        id_Ped,
                                        sector,
                                        ruta,
                                        secuencia
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignRuta", ref verificador, Parametros, Valores);
                if (verificador == 2 || verificador == 3)
                {
                    CapaDatos.RollBackTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    return;

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
        public void ConsultaPedidoAutorizacion_Lista(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_usuario",
                                      };
                object[] Valores = { 
                                       pedido.Id_Emp, 
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre == ""? (object)null: pedido.Filtro_Nombre,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_usuario == ""? (object)null: pedido.Filtro_usuario
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoAutorizacion_Lista", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    p.Ped_TipoStr = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? "" : Tipo(dr.GetValue(dr.GetOrdinal("Ped_Tipo")).ToString());
                    p.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    p.Estatus = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    p.EstatusStr = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString());
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    p.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    p.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    p.Facturacion = dr.IsDBNull(dr.GetOrdinal("Cte_Facturacion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Facturacion")));
                    List.Add(p);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Autorizar(Pedido pedido, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Ped",
                                        "@Fecha",
                                        "@Id_U"
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        pedido.Ped_FechaAut,
                                        pedido.Id_U
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Autorizar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEncabezado_RepFacPedidosPendientes(Sesion sesion, ref string Emp_Nombre, ref string Cd_Nombre, ref string U_Nombre)
        {//rm
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_U",
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       sesion.Id_U.ToString(),
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepFacPedidosPendientes_Encabezado", ref dr, parametros, Valores);
                while (dr.Read())
                {
                    Emp_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Emp_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Emp_Nombre"));
                    Cd_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    U_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("U_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
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

        public void ConsultaPedidoDisp(Pedido pedido, int prd, string Conexion, ref int disponible_pedido)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd"
                                      };
                object[] Valores = {
                                      pedido.Id_Emp,
                                      pedido.Id_Cd,
                                      pedido.Id_Ped,
                                      prd
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDisponible_Consultar", ref disponible_pedido, parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoCancelacion(PedidoDet pedido, string Conexion, ref List<PedidoDet> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] parametros = { 
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Ped,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_BajaParcialConsulta", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoDet();
                    pedido.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    pedido.Prd_Desc = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    pedido.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    pedido.Original = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Original")));
                    pedido.Cancelado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cancelado")));
                    pedido.Pendiente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pendiente")));
                    pedido.Final = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Final")));
                    list.Add(pedido);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaParcial(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();


                string[] Parametros = {   
                                          "@Id_Emp", 
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd",
                                          "@Id_Ter",
                                          "@Cant_Cancelar"
                                      };
                object[] Valores = null;

                for (int x = 0; x < list.Count; x++)
                {
                    Valores = new object[] { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        list[x].Id_Prd,
                                        list[x].Id_Ter,
                                        list[x].Cancelado
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_BajaParcial", ref verificador, Parametros, Valores);

                }
                int verificador2 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_BajaParcialTotales", ref verificador2, Parametros, Valores);

                verificador2 = 0;
                if (verificador == 1)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador2, Parametros, Valores);
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

        public void PedidosPendientes_ConsultaReporte(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                DataSet ds = null;
                string[] Parametros = {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Territorios",
                            "@Clientes",
                            "@Productos",
                            "@Tipo",
                            "@FechaIni",
                            "@FechaFin",
                            "@Pedido"
                        };

                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Territorios,
                                       pedido.Clientes,
                                       pedido.Productos,
                                       1,
                                       pedido.FechaIni == null ? (object) null: pedido.FechaIni,
                                       pedido.FechaFin  == null ? (object) null: pedido.FechaFin,
                                       pedido.Pedidos
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spRepFacPedidosPendientes_Detalle", ref ds, Parametros, Valores);
                dt = ds.Tables[0];

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
