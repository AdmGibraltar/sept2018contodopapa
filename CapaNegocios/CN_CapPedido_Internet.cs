using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapPedido_Internet
    {


        public void ConsultarPedidos(ref IList<Pedido_Internet> pedidos, string Conexion, Pedido_Internet pedido)
        {
            try
            {
                CD_CapPedido_Internet claseCapaDatos = new CD_CapPedido_Internet();
                claseCapaDatos.ConsultarPedidos(Conexion, ref pedidos, pedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPedido_Datos(String Conexion, ref Pedido_Internet pedido, ref ClienteDirEntrega dirEntrega, int num_Pedido)
        {
            try
            {
                CD_CapPedido_Internet claseCapaDatos = new CD_CapPedido_Internet();
                claseCapaDatos.ConsultarPedido_Datos(Conexion, ref pedido, ref dirEntrega,num_Pedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarPedido_Detalle(String Conexion,  int num_pedido, ref DataTable dt)
        {
            try
            {
                CD_CapPedido_Internet claseCapaDatos = new CD_CapPedido_Internet();
                claseCapaDatos.ConsultaPedido_Detalle(Conexion, num_pedido, ref dt);
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
                CD_CapPedido_Internet claseCapaDatos = new CD_CapPedido_Internet();
                claseCapaDatos.Rechazar_Pedido(Conexion, num_pedido, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }


}
