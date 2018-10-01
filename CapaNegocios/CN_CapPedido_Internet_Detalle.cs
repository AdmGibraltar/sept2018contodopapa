using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapPedido_Internet_Detalle
    {
        public void ConsultarPedidosDetalle(ref IList<Pedido_Internet_Detalle> pedidos, int num_pedido, string Conexion)
        {
            try
            {
                CD_CapPedido_Internet_Detalle claseCapaDatos = new CD_CapPedido_Internet_Detalle();
                claseCapaDatos.Consultar_Detalle_Pedido(Conexion,num_pedido, ref pedidos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
