using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CapNotificacionesExcesoMeta
    {
        public CN_CapNotificacionesExcesoMeta(Sesion sesion)
        {
            _sesion = sesion;
        }

        public CapNotificacionExcesoMeta NotificacionExistente()
        {
            CD_CapNotificacionesExcesoMeta cdCapNotificacionesExcesoMeta = new CD_CapNotificacionesExcesoMeta(_sesion.Emp_Cnx_EF);
            var ret = cdCapNotificacionesExcesoMeta.Consultar(_sesion.Id_Emp, _sesion.Id_Cd);
            return ret;
        }

        public void ProgramarNotificacion(String asunto, string cuerpo, byte[] contenidoArchivoAdjunto)
        {
            if (NotificacionExistente() == null)
            {
                CD_CapNotificacionesExcesoMeta cdCapNotificacionesExcesoMeta = new CD_CapNotificacionesExcesoMeta(_sesion.Emp_Cnx_EF);
                cdCapNotificacionesExcesoMeta.Insertar(_sesion.Id_Emp, _sesion.Id_Cd, asunto, null, cuerpo, contenidoArchivoAdjunto);
            }
        }

        private Sesion _sesion;
    }
}
