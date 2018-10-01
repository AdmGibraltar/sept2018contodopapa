using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;

namespace CapaNegocios
{
   public class CN_Contraseña
    {
       public void ConsultaLongitudPass(ConfiguracionGlobal ConfiguracionGlobal, string conexion, ref System.Collections.Generic.List<ConfiguracionGlobal> list)
       {
           try
           {
               CapaDatos.CD_Contraseña claseCapaDatos = new CapaDatos.CD_Contraseña();
               claseCapaDatos.ConsultaLongitudPass(ConfiguracionGlobal, conexion,ref list);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
