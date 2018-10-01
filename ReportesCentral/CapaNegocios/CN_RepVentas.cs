using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_RepVentas
    {
        public void Consulta(VenEstadisticaVentas ven, string Conexion, ref List<VenEstadisticaVentas> List)
        {
            try
            {
                CD_RepVentas claseCapaDatos = new CD_RepVentas();
                claseCapaDatos.Consulta(ven, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
