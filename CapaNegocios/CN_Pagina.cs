using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
   public class CN_Pagina
    {
       public void PaginaConsultar(ref Pagina pagina, string Conexion)
       {
           CD_Pagina CapaDatos = new CD_Pagina();
           CapaDatos.PaginaConsultar(ref pagina, Conexion);
       }

        
    }
}
