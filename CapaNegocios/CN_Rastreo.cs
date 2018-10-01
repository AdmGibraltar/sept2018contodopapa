using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_Rastreo
    {
        public void Lista(CapaEntidad.Rastreo rastreo, ref List<Rastreo> list, string Conexion, int tipoBusqueda)
        {
            try
            {
                CD_Rastreo claseCapaDatos = new CD_Rastreo();
                claseCapaDatos.Lista(rastreo, list, Conexion, tipoBusqueda);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void LogDocumento(CapaEntidad.Rastreo rastreo, ref List<Rastreo> list, string Conexion, int tipoBusqueda)
        {
            try
            {
                CD_Rastreo claseCapaDatos = new CD_Rastreo();
                claseCapaDatos.LogDocumento(rastreo, list, Conexion, tipoBusqueda);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
