using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatBanco
    {
        public void ConsultaBanco(Banco banco, string Conexion, ref List<Banco> List)
        {
            try
            {
                CD_CatBanco claseCapaDatos = new CD_CatBanco();
                claseCapaDatos.ConsultaBanco(banco, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarBanco(Banco banco, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatBanco claseCapaDatos = new CD_CatBanco();
                claseCapaDatos.InsertarBanco(banco, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarBanco(Banco banco, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatBanco claseCapaDatos = new CD_CatBanco();
                claseCapaDatos.ModificarBanco(banco, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         
    }
}
