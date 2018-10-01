using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatRentabilidad
    {
        public void ConsultaRentabilidad(int Id_Emp, string Conexion, ref List<Rentabilidad> List)
        {
            try
            {
                CD_CatRentabilidad claseCapaDatos = new CD_CatRentabilidad();
                claseCapaDatos.ConsultaRentabilidad(Id_Emp, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRentabilidad(Rentabilidad segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRentabilidad claseCapaDatos = new CD_CatRentabilidad();
                claseCapaDatos.InsertarRentabilidad(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRentabilidad(Rentabilidad segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRentabilidad claseCapaDatos = new CD_CatRentabilidad();
                claseCapaDatos.ModificarRentabilidad(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
