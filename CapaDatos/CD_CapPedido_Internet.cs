using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CapPedido_Internet
    {

        public void ConsultarPedidos(string Conexion, ref IList<Pedido_Internet> pedidosLista, Pedido_Internet pedido)
        {
            try
            {

                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cte_Inicio", "@Id_Cte_Final", "@Id_Ter_Inicio", "@Id_Ter_Final", "@Anio_inicio", "@Anio_Final", "@Estatus_id", "@NombreCliente"};
                object[] Valores = {pedido.P_Cliente_Inicio,pedido.P_Cliente_Final,pedido.P_Terr_Inicio,pedido.P_Terr_Final,pedido.P_Anio_Inicio,pedido.P_Anio_Final,pedido.P_Estatus,pedido.P_Nom_Cliente };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidosInternet_Consulta", ref dr, Parametros, Valores);

                Pedido_Internet p;
                while (dr.Read())
                {

                    p = new Pedido_Internet();
                    p.Num_Pedido = Convert.ToInt32(dr["Num_Pedido"]);
                    p.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    p.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    p.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    p.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr["Id_Ter"]);
                    p.Observaciones=dr["Observaciones"].ToString();
                    p.Nombre = dr["Nombre"].ToString();
                    p.Telefono = dr["Telefono"].ToString();
                    p.Ext = dr["Ext"].ToString();
                    p.Fecha_Requisicion = DateTime.Parse(dr["Fecha_Requisicion"].ToString());

                    p.Cte_NomComercial = dr["Cte_NomComercial"].ToString();

                    p.Subtotal = Convert.ToDouble(dr["Subtotal"]);
                    p.IVA = Convert.ToDouble(dr["IVA"]);
                    p.Total = Convert.ToDouble(dr["Total"]);

                    p.Cuenta_Usuario = dr["Cuenta_Usuario"].ToString();
                    p.Nombre_Usuario = dr["Nombre_Usuario"].ToString();
                    p.Estatus_Id = dr["Estatus_Id"].ToString();
                    p.Estatus_Nombre = dr["Estatus_Nombre"].ToString();

                    p.Cte_CreditoLetra = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) == true ? "NO" : "SI";
                    p.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido")));

                    p.UnidadNegocio_Id = Convert.ToInt32(dr["UnidadNegocio_Id"]);
                    p.UnidadNegocio_Nombre = dr["UnidadNegocio_Nombre"].ToString();

                    pedidosLista.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarPedido_Datos(string Conexion, ref Pedido_Internet pedido,ref ClienteDirEntrega dirEntrega, int num_Pedido)
        {
            try
            {

                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Num_Pedido" };
                object[] Valores = { num_Pedido };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidosInternet_ConsultaPedido", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();

                    var p = new Pedido_Internet();
                    var d = new ClienteDirEntrega();

                    p.Num_Pedido = Convert.ToInt32(dr["Num_Pedido"]);
                    p.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    p.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    p.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    p.Id_Ter = null;

                    p.Nom_Cliente = dr["Nom_Cliente"].ToString();

                    p.Observaciones = dr["Observaciones"].ToString();
                    p.Nombre = dr["Nombre"].ToString();
                    p.Telefono = dr["Telefono"].ToString();
                    p.Ext = dr["Ext"].ToString();
                    p.Correo = dr["Correo"].ToString();
                    p.Fecha_Requisicion = DateTime.Parse(dr["Fecha_Requisicion"].ToString());

                    p.Subtotal = Convert.ToDouble(dr["Subtotal"]);
                    p.IVA = Convert.ToDouble(dr["IVA"]);
                    p.Total = Convert.ToDouble(dr["Total"]);

                    p.Cuenta_Usuario = dr["Cuenta_Usuario"].ToString();
                    p.Nombre_Usuario = dr["Nombre_Usuario"].ToString();
                    p.Telefono_Usuario = dr["Nombre_Usuario"].ToString();
                    p.Estatus_Id = dr["Estatus_Id"].ToString();


                    d.Cte_Calle = dr["Cte_Calle"].ToString();
                    d.Cte_Numero = dr["Cte_Numero"].ToString();
                    d.Cte_Cp = dr["Cte_Cp"].ToString();
                    d.Cte_Colonia = dr["Cte_Colonia"].ToString();
                    d.Cte_Municipio = dr["Cte_Municipio"].ToString();
                    d.Cte_Estado = dr["Cte_Estado"].ToString();

                    d.Cte_HoraAm1 = dr["Cte_HoraAm1"].ToString();
                    d.Cte_HoraAm2 = dr["Cte_HoraAm2"].ToString();
                    d.Cte_HoraPm1 = dr["Cte_HoraPm1"].ToString();
                    d.Cte_HoraPm2 = dr["Cte_HoraPm2"].ToString();


                    dirEntrega = d;
                    pedido = p;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedido_Detalle(string Conexion, Int32 num_pedido,  ref DataTable dt)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Num_Pedido" };
                object[] Valores = { num_pedido };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidosInternet_Detalle_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
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

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Rechazar_Pedido(string Conexion, Int32 num_pedido, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Num_Pedido" };
                object[] Valores = { num_pedido };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidosInternet_Rechazar", ref verificador, Parametros, Valores);
        
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

    }
}