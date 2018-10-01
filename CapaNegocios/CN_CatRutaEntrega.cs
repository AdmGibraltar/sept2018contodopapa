using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatRutaEntrega
    {
        public void ConsultaRutaEntrega(RutaEntrega ruta, string Conexion, ref List<RutaEntrega> List)
        {
            try
            {
                CD_CatRutaEntrega claseCapaDatos = new CD_CatRutaEntrega();
                claseCapaDatos.ConsultaRutaEntrega(ruta, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRutaEntrega(RutaEntrega ruta, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRutaEntrega claseCapaDatos = new CD_CatRutaEntrega();
                claseCapaDatos.InsertarRutaEntrega(ruta, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRutaEntrega(RutaEntrega ruta, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRutaEntrega claseCapaDatos = new CD_CatRutaEntrega();
                claseCapaDatos.ModificarRutaEntrega(ruta, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
