using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapDevolucion
    {
        public void ConsultarCantidadDevolucionesCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapDevolucion().ConsultarCantidadDevolucionesCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
