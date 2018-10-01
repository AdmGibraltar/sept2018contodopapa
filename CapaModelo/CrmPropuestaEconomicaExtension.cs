using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaModelo
{
    public partial class CrmPropuestaEconomica
    {
        public CatProducto ProductoSerializable
        {
            get;
            set;
        }

        public string Prd_Descripcion
        {
            get
            {
                return CatProducto.Prd_Descripcion;
            }
        }

        public string Prd_Pres
        {
            get
            {
                return CatProducto.Prd_Presentacion;
            }
        }

        public float Prd_Precio
        {
            get;
            set;
        }

        public ProductoPrecio ProductoPrecio
        {
            get;
            set;
        }
    }
}
