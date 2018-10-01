using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
   public class CN_CatSisPropietarios
    {
       public void ConsultaSisPropietarios(int Id_Emp,String Conexion,ref List<SistemasPropietarios> List)
       {
           try
           {
               CD_CatSisPropietarios claseCapaDatos = new CD_CatSisPropietarios();
               claseCapaDatos.ConsultaSisPropietarios(Id_Emp, Conexion, ref List);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void InsertarSisPropietarios(SistemasPropietarios sp, string Conexion, ref int verificador)
       {
           try
           {
               CD_CatSisPropietarios claseCapaDatos = new CD_CatSisPropietarios();
               claseCapaDatos.InsertarSisPropietarios(sp, Conexion, ref verificador);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void ModificarSisPropietarios(SistemasPropietarios sp, string Conexion, ref int verificador)
       {
           try
           {
               CD_CatSisPropietarios claseCapaDatos = new CD_CatSisPropietarios();
               claseCapaDatos.ModificarSisPropietarios(sp, Conexion, ref verificador);

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
