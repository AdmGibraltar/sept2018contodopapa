using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using CapaModelo;
namespace CapaNegocios
{
    /// <summary>
    /// Clase de reglas de negocio para la entidad de configuración global del sistema.
    /// </summary>
    public class CN_CapConfiguracionGlobal
    {
        public CN_CapConfiguracionGlobal(Sesion sesion)
        {
            _sesion = sesion;
        }

        /// <summary>
        /// Obtiene la entrada de configuración global asociada a la llave indicada.
        /// </summary>
        /// <param name="llave">Llave de la configuración de interés</param>
        /// <returns>Entrada asociada a la llave de interés indicada</returns>
        public CapConfiguracionGlobal ObtenerPorLlave(string llave)
        {
            CapConfiguracionGlobal ret = null;
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            ret = cdCapConfiguracionGlobal.ObtenerPorLlave(llave);
            return ret;
        }

        /// <summary>
        /// Obtiene la entrada de configuración global asociada a la llave del perfil de correo para el envio de notificaciones de meta excedida.
        /// </summary>
        /// <returns>Entrada asociada a la llave del perfil de correo para el envio de notificaciones de meta excedida</returns>
        public String ObtenerValorPerfilCorreo()
        {
            CapConfiguracionGlobal ret = null;
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            ret = cdCapConfiguracionGlobal.ObtenerPorLlave("PerfilCorreo");
            return ret.Conf_valor;
        }

        /// <summary>
        /// Guarda el valor que se desea asociar a la llave del perfil de correo para el envio de notificaciones de meta excedida
        /// </summary>
        /// <param name="valor">Nuevo valor de interés que se desea asociar a la llave del perfil de correo para el envio de notificaciones de meta excedida</param>
        public void GuardarValorPerfilCorreo(string valor)
        {
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            cdCapConfiguracionGlobal.GuardarValor("PerfilCorreo", valor);
        }

        /// <summary>
        /// Obtiene la entrada de configuración global asociada a la llave del valor del directorio que contiene a la utilidad BCP de SQL para el envio de notificaciones de meta excedida.
        /// </summary>
        /// <returns>Entrada asociada a la llave del valor del directorio que contiene a la utilidad BCP de SQL para el envio de notificaciones de meta excedida.</returns>
        public String ObtenerValorRutaBCP()
        {
            CapConfiguracionGlobal ret = null;
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            ret = cdCapConfiguracionGlobal.ObtenerPorLlave("RutaBCP");
            return ret.Conf_valor;
        }

        /// <summary>
        /// Guarda el valor que se desea asociar a la llave del valor del directorio que contiene a la utilidad BCP de SQL para el envio de notificaciones de meta excedida
        /// </summary>
        /// <param name="valor">Nuevo valor de interés que se desea asociar a la llave del valor del directorio que contiene a la utilidad BCP de SQL para el envio de notificaciones de meta excedida</param>
        public void GuardarValorRutaBCP(string valor)
        {
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            cdCapConfiguracionGlobal.GuardarValor("RutaBCP", valor);
        }

        /// <summary>
        /// Obtiene la entrada de configuración global asociada a la llave del valor del nombre de la base de datos de la aplicación, para el envio de notificaciones de meta excedida.
        /// </summary>
        /// <returns>Entrada asociada a la llave del valor del nombre de la base de datos de la aplicación, para el envio de notificaciones de meta excedida</returns>
        public String ObtenerValorBaseDatosSIAM()
        {
            CapConfiguracionGlobal ret = null;
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            ret = cdCapConfiguracionGlobal.ObtenerPorLlave("BaseDeDatosSIAM");
            return ret.Conf_valor;
        }

        /// <summary>
        /// Guarda el valor que se desea asociar a la llave del valor del nombre de la base de datos de la aplicación, para el envio de notificaciones de meta excedida
        /// </summary>
        /// <param name="valor">Nuevo valor de interés que se desea asociar a la llave del valor del nombre de la base de datos de la aplicación, para el envio de notificaciones de meta excedida</param>
        public void GuardarValorBaseDatosSIAM(string valor)
        {
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            cdCapConfiguracionGlobal.GuardarValor("BaseDeDatosSIAM", valor);
        }

        /// <summary>
        /// Obtiene la entrada de configuración global asociada a la llave del valor del directorio que aloja a los archivos temporales creados por el proceso del envío de notificaciones de meta excedida
        /// </summary>
        /// <returns>Entrada asociada a la llave del valor del directorio que aloja a los archivos temporales creados por el proceso del envío de notificaciones de meta excedida</returns>
        public String ObtenerValorArchivosTemporalesNotificacion()
        {
            CapConfiguracionGlobal ret = null;
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            ret = cdCapConfiguracionGlobal.ObtenerPorLlave("ArchivosTemporalesNotificacion");
            return ret.Conf_valor;
        }

        /// <summary>
        /// Guarda el valor que se desea asociar a la llave del valor del directorio que aloja a los archivos temporales creados por el proceso del envío de notificaciones de meta excedida
        /// </summary>
        /// <param name="valor">Nuevo valor de interés que se desea asociar a la llave del valor del directorio que aloja a los archivos temporales creados por el proceso del envío de notificaciones de meta excedida</param>
        public void GuardarValorArchivosTemporalesNotificacion(string valor)
        {
            CD_CapConfiguracionGlobal cdCapConfiguracionGlobal = new CD_CapConfiguracionGlobal(_sesion.Emp_Cnx_EF);
            cdCapConfiguracionGlobal.GuardarValor("ArchivosTemporalesNotificacion", valor);
        }

        private Sesion _sesion;
    }
}
