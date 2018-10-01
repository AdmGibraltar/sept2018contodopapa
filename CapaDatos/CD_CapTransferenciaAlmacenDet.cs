using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapTransferenciaAlmacenDet
    {
        public void ConsultaTransferenciaAlmacenDetalle_Lista(TransferenciaAlmacen transferenciaAlmacen, string Conexion, ref List<TransferenciaAlmacenDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Trans" };
                object[] Valores = { transferenciaAlmacen.Id_Emp, transferenciaAlmacen.Id_Cd, transferenciaAlmacen.Id_Trans };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapTransferenciaDetalle_Consulta", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    TransferenciaAlmacenDet transferenciaAlmacenDet = new TransferenciaAlmacenDet();
                    transferenciaAlmacenDet.Producto = new Producto();

                    transferenciaAlmacenDet.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    transferenciaAlmacenDet.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    transferenciaAlmacenDet.Id_Trans = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Trans")));
                    transferenciaAlmacenDet.Id_TransDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TransDet")));
                    transferenciaAlmacenDet.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    transferenciaAlmacenDet.Trans_Cant = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Trans_Cantidad"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Trans_Cantidad")));
                    transferenciaAlmacenDet.Producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    transferenciaAlmacenDet.Producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    transferenciaAlmacenDet.Producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    transferenciaAlmacenDet.Producto.Prd_UniNe = dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString();                   
                    transferenciaAlmacenDet.ProductoPrecio = new ProductoPrecios();
                    transferenciaAlmacenDet.ProductoPrecio.Prd_Pesos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Prd_Pesos"))) ? 0 : Convert.ToSingle(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));

                    List.Add(transferenciaAlmacenDet);
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
