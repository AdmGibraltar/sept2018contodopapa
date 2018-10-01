using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatAsesoria
    {
        public void ConsultaAsesoria(Asesoria asesoria, string Conexion, int id_Emp, ref List<Asesoria> List)
        {
            try
            {
                CD_CatAsesoria claseCapaDatos = new CD_CatAsesoria();
                claseCapaDatos.ConsultaAsesoria(asesoria, Conexion, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarAsesoria(Asesoria asesoria, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAsesoria claseCapaDatos = new CD_CatAsesoria();
                claseCapaDatos.InsertarAsesoria(asesoria, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAsesoria(Asesoria asesoria, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatAsesoria claseCapaDatos = new CD_CatAsesoria();
                claseCapaDatos.ModificarAsesoria(asesoria, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
