using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
   public class CN_CatGasto
    {
       public void InsertarGasto(Gasto gasto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatGasto claseCapaDatos = new CD_CatGasto();
                claseCapaDatos.InsertarGasto(gasto, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void ConsultaGasto(ref Gasto Gasto, string Conexion)
       {
           try
           {
               CD_CatGasto claseCapaDatos = new CD_CatGasto();
               claseCapaDatos.ConsultarGasto(ref Gasto, Conexion);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

      
    }
}
