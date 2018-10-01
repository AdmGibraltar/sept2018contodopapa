using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatRutas
    {
        public void ConsultaRuta(Ruta Ruta, string Conexion, ref List<Ruta> List)
        {
            try
            {
                CD_CatRutas claseCapaDatos = new CD_CatRutas();
                claseCapaDatos.ConsultaRuta(Ruta, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRuta(Ruta Ruta, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRutas claseCapaDatos = new CD_CatRutas();
                claseCapaDatos.InsertarRuta(Ruta, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRuta(Ruta Ruta, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRutas claseCapaDatos = new CD_CatRutas();
                claseCapaDatos.ModificarRuta(Ruta, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
