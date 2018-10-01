using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatCalendarioControl
    {

        public void ConsultaCalendarioControl(ref CalendarioControl Cal, string conexion, ref List<CalendarioControl> list)
        {
            try
            {
                CD_CatCalendarioControl cal = new CD_CatCalendarioControl();
                cal.ConsultaCalendario(ref Cal, conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarCalendarioControl(ref List<CalendarioControl> itemsCalendario, string Conexion, ref int verificador, string estatus)
        {
            try
            {
                CD_CatCalendarioControl cal = new CD_CatCalendarioControl();
                cal.GuardarCalendario(ref itemsCalendario, ref verificador, Conexion, estatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}