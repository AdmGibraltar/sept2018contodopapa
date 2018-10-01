using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatMensaje
    {
        public CN_CatMensaje(Sesion sesion)
        {
            _sesion = sesion;
        }

        public string MensajePeticionAutorizacionValuacion
        {
            get
            {
                CD_CatMensaje cdCatMensaje = new CD_CatMensaje();
                var mensaje = cdCatMensaje.ConsultarPorLlave("valuacion_autorizacion", _sesion.Emp_Cnx_EF);
                return mensaje.CatMen_Mensaje;
            }
        }

        public string TituloMensajeValuacionAceptada
        {
            get
            {
                CD_CatMensaje cdCatMensaje = new CD_CatMensaje();
                var mensaje = cdCatMensaje.ConsultarPorLlave("valuacion_titulo_autorizada", _sesion.Emp_Cnx_EF);
                return mensaje.CatMen_Mensaje;
            }
        }

        public string CuerpoMensajeValuacionAceptada
        {
            get
            {
                CD_CatMensaje cdCatMensaje = new CD_CatMensaje();
                var mensaje = cdCatMensaje.ConsultarPorLlave("valuacion_cuerpo_autorizada", _sesion.Emp_Cnx_EF);
                return mensaje.CatMen_Mensaje;
            }
        }

        private Sesion _sesion = null;
    }
}
