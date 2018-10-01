using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
 

namespace CapaNegocios
{
    public class CN_CatNoConformidades
    {
        public void ConsultaNoConformidades(NoConformidades NoConformidades, string Conexion, ref List<NoConformidades> List)
        {
            try
            {
                CD_CatNoConformidades claseCapaDatos = new CD_CatNoConformidades();
                claseCapaDatos.ConsultaNoConformidades(NoConformidades, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarNoConformidades(NoConformidades NoConformidades, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatNoConformidades claseCapaDatos = new CD_CatNoConformidades();
                claseCapaDatos.InsertarNoConformidades(NoConformidades, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNoConformidades(NoConformidades NoConformidades, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatNoConformidades claseCapaDatos = new CD_CatNoConformidades();
                claseCapaDatos.ModificarNoConformidades(NoConformidades, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
