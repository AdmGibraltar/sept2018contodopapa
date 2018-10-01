using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatUnidad
    {
        public void ConsultaUnidad(int Id_Emp, string Conexion, ref List<Unidad> List)
        {
            try
            {
                CD_CatUnidad claseCapaDatos = new CD_CatUnidad();
                claseCapaDatos.ConsultaUnidad(Id_Emp, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatUnidad claseCapaDatos = new CD_CatUnidad();
                claseCapaDatos.InsertarUnidad(unidad, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUnidad(Unidad unidad, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatUnidad claseCapaDatos = new CD_CatUnidad();
                claseCapaDatos.ModificarUnidad(unidad, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
