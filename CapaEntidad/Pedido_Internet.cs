using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class Pedido_Internet
    {
       public int Num_Pedido { get; set; }
       public int Id_Emp { get; set; }
       public int Id_Cd { get; set; }
       public int Id_Cte { get; set; }
       public Nullable<int> Id_Ter { get; set; }
       public String Observaciones { get; set; }
       public String Nombre { get; set; }
       public String Telefono { get; set; }
       public String Ext { get; set; }
       public DateTime Fecha_Requisicion { get; set; }

       public Double Subtotal { get; set; }
       public Double IVA { get; set; }
       public Double Total { get; set; }

       public String Cuenta_Usuario { get; set; }
       public String Nombre_Usuario { get; set; }
       public String Estatus_Id { get; set; }
       public String Estatus_Nombre { get; set; }

       public bool Seleccionado { get; set; }

       public String Nom_Cliente { get; set; }
       public String Correo { get; set; }
       public String Telefono_Usuario { get; set; }

       public String Cte_NomComercial { get; set; }


       public Int32 P_Cliente_Inicio { get; set; }
       public Int32 P_Cliente_Final { get; set; }

       public Int32 P_Terr_Inicio { get; set; }
       public Int32 P_Terr_Final { get; set; }

       public Int32 P_Anio_Inicio { get; set; }
       public Int32 P_Anio_Final { get; set; }

       public String P_Estatus { get; set; }
       public String P_Nom_Cliente { get; set; }

       public String Cte_CreditoLetra { get; set; }
       public Boolean Cte_Credito { get; set; }

       public int UnidadNegocio_Id { get; set; }
       public String UnidadNegocio_Nombre { get; set; }

    }
}

