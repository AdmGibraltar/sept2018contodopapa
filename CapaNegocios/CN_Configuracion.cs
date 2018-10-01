using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_Configuracion
    {
        public void Consulta(ref ConfiguracionGlobal Configuracion, string conexion)
        {
            try
            {
                CapaDatos.CD_Configuracion CD_Configuracion = new CapaDatos.CD_Configuracion();
                CD_Configuracion.Consulta(ref Configuracion, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Modificar(ref ConfiguracionGlobal Configuracion, string conexion)
        {
            try
            {
                CapaDatos.CD_Configuracion CD_Configuracion = new CapaDatos.CD_Configuracion();
                CD_Configuracion.Modificar(ref Configuracion, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la instancia de [SysConfiguracion] dado el identificador de la entidad.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idConf">Identificador de la instancia</param>
        /// <returns>SysConfiguracion</returns>
        public SysConfiguracion Obtener(Sesion s, int idConf)
        {
            CapaDatos.CD_Configuracion cdConfiguracion = new CapaDatos.CD_Configuracion();
            var config = cdConfiguracion.Consultar(s.Id_Emp, s.Id_Cd, idConf, s.Emp_Cnx_EF);
            return config;
        }

        /// <summary>
        /// Obtiene la instancia de [SysConfiguracion] dado el identificador de la entidad. Versión transaccional.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idConf">Identificador de la instancia</param>
        /// <param name="ibt">Transacción de negocio</param>
        /// <returns>SysConfiguracion</returns>
        public SysConfiguracion Obtener(Sesion s, int idConf, IBusinessTransaction ibt)
        {
            CapaDatos.CD_Configuracion cdConfiguracion = new CapaDatos.CD_Configuracion();
            var config = cdConfiguracion.Consultar(s.Id_Emp, s.Id_Cd, idConf, ibt.DataContext);
            return config;
        }
    }
}
