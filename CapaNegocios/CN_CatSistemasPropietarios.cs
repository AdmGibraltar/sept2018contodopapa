using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
   public class CN_CatSistemasPropietarios
    {
       public void ConsultaSistemasPropietarios(String Conexion,ref List<SistemasPropietarios> List)
       {
           try
           {
               CD_CatSistemasPropietarios claseCapaDatos = new CD_CatSistemasPropietarios();
               claseCapaDatos.ConsultaBanco(Conexion, ref List);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
