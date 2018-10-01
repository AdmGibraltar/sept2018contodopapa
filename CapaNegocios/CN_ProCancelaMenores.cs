using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ProCancelaMenores
    {
        public void Cancelar(CentroDistribucion cd, DateTime dateTime1, DateTime dateTime2, string Conexion, ref int verificadorFact, ref int verificadorNcar)
        {
            try
            {
                CD_ProCancelaMenores claseCapaDatos = new CD_ProCancelaMenores();
                claseCapaDatos.Cancelar(cd, dateTime1, dateTime2, Conexion, ref verificadorFact, ref verificadorNcar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
