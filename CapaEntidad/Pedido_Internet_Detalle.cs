using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Pedido_Internet_Detalle
    {

        public int Num_Pedido { get; set; }
        public int Cod_Producto { get; set; }
        public string Prd_Descripcion { get; set; }
        public int Inventario { get; set; }
        public int Cantidad { get; set; }

        public double Precio { get; set; }

    }
}
