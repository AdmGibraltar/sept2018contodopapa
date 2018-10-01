using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CapPedido_Internet_Detalle
    {

        public void Consultar_Detalle_Pedido(string Conexion, int num_pedido, ref IList<Pedido_Internet_Detalle> pedidosLista)
        {
            try
            {

                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Num_Pedido"};
                object[] Valores = { num_pedido };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidosInternet_Detalle_Consulta", ref dr, Parametros, Valores);

                Pedido_Internet_Detalle p;
                while (dr.Read())
                {

                    p = new Pedido_Internet_Detalle();
                    p.Num_Pedido = Convert.ToInt32(dr["Num_Pedido"]);
                    p.Cod_Producto = Convert.ToInt32(dr["Cod_Producto"]);
                    p.Prd_Descripcion = dr["Prd_Descripcion"].ToString();
                    p.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                    p.Precio = Convert.ToDouble(dr["Precio"]);

                    pedidosLista.Add(p);

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