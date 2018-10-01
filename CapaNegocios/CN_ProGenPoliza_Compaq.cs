using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Collections;

namespace CapaNegocios
{
    public class CN_ProGenPoliza_Compaq
    {
        public void FiltrarProGenPoliza(Sesion sesion, ProGenPoliza_Compaq proGenPoliza, ref ArrayList verificador)
        {
            try
            {
                CD_ProGenPoliza_Compaq claseCapaDatos = new CD_ProGenPoliza_Compaq();
                claseCapaDatos.FiltrarProGenPoliza(sesion, proGenPoliza, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
