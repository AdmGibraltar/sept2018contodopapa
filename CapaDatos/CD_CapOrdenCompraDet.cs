using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapOrdenCompraDet
    {
        public void spCapOrdCompraDetalle_Consulta_Entradas_Partida(OrdenCompraDet ordenCompraDet, string Conexion, ref int Ord_Cantidad)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ord", "@Id_Prd" };
                object[] Valores = { ordenCompraDet.Id_Emp, ordenCompraDet.Id_Cd, ordenCompraDet.Id_Ord, ordenCompraDet.Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Consulta_Entradas_Partida", ref dr, Parametros, Valores);

                Ord_Cantidad = 0;

                while (dr.Read())
                {

                    Ord_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ord_Cantidad"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_Cantidad")));

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCapOrdCompraDetalle_Consulta_Entradas(OrdenCompraDet ordenCompraDet, string Conexion, ref List<OrdenCompraDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ord" };
                object[] Valores = { ordenCompraDet.Id_Emp, ordenCompraDet.Id_Cd, ordenCompraDet.Id_Ord };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Consulta_Entradas", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ordenCompraDet = new OrdenCompraDet();
                    ordenCompraDet.Producto = new Producto();

                    ordenCompraDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ordenCompraDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ordenCompraDet.Id_Ord = ordenCompraDet.Id_Ord;
                    ordenCompraDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    ordenCompraDet.Ord_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ord_Cantidad"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_Cantidad")));

                    ordenCompraDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    ordenCompraDet.Producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    ordenCompraDet.Producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    ordenCompraDet.Producto.Prd_UniNe = dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString();
                    ordenCompraDet.ProductoPrecio = new ProductoPrecios();
                    ordenCompraDet.ProductoPrecio.Prd_Pesos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));

                    List.Add(ordenCompraDet);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaOrdenCompraDetalle_Lista(OrdenCompraDet ordenCompraDet, string Conexion, ref List<OrdenCompraDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ord" };
                object[] Valores = { ordenCompraDet.Id_Emp, ordenCompraDet.Id_Cd, ordenCompraDet.Id_Ord };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapOrdCompraDetalle_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    ordenCompraDet = new OrdenCompraDet();
                    ordenCompraDet.Producto = new Producto();

                    ordenCompraDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    ordenCompraDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    ordenCompraDet.Id_Ord = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ord")));
                    ordenCompraDet.Id_OrdDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_OrdDet")));
                    ordenCompraDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    ordenCompraDet.Ord_Cantidad = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ord_Cantidad"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_Cantidad")));
                    ordenCompraDet.Ord_CantidadGen = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ord_CantidadGen"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_CantidadGen")));
                    ordenCompraDet.Ord_CantidadCump = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Ord_CantidadCump"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ord_CantidadCump")));

                    ordenCompraDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    ordenCompraDet.Producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    ordenCompraDet.Producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    ordenCompraDet.Producto.Prd_UniNe = dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString();
                    ordenCompraDet.Producto.Prd_InvFinal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_InvFinal"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    ordenCompraDet.Producto.Prd_Transito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Transito"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Transito")));
                    ordenCompraDet.ProductoPrecio = new ProductoPrecios();
                    ordenCompraDet.ProductoPrecio.Prd_Pesos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));

                    List.Add(ordenCompraDet);
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
