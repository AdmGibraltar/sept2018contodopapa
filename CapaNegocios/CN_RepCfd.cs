using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
   public class CN_RepCfd
    {


       public void Consultar(CapaEntidad.Sesion session, CapaEntidad.Cfd cfd, ref System.Collections.ArrayList verificador)
       {
           try
           {
               CD_RepCfd claseCapaDatos = new CD_RepCfd();
               claseCapaDatos.Consultar(session, cfd, ref verificador);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
