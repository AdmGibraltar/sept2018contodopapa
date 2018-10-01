using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatZonas
    {
        public void Consultar(int Id_Emp, int Id_Cd, ref List<CapaEntidad.CentroDistribucion> cd, string Conexion)
        {
            try
            {
                CD_CatZonas cd_zonas = new CD_CatZonas();
                cd_zonas.Consultar(Id_Emp, Id_Cd, ref cd, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
