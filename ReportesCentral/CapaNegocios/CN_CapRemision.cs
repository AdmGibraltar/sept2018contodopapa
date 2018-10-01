using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapRemision
    {
        public void ConsultaEstadistica(Remision rem, string Conexion, ref List<Remision> listaRemision)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                 claseCapaDatos.ConsultaEstadistica(rem, Conexion, ref listaRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

       
    }
}
