using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_FabricaTransaccionNegocios
    {
        /// <summary>
        /// Regresa la implementación por defecto para el manejo de transacciones de reglas de negocio utilizando un contexto basado en Entity Framework
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <returns>IBusinessTransaction en una llamada satisfactoria; null en caso contrario</returns>
        public static IBusinessTransaction Default(Sesion s)
        {
            return new EFBusinessTransaction(s);
        }

        /// <summary>
        /// Regresa una transacción de capa de negocio para la fuente de datos designada de SIANCentral.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <returns>IBusinessTransaction</returns>
        public static IBusinessTransaction ParaSIANCentral(Sesion s)
        {
            Sesion copia = new Sesion();
            copia.Emp_Cnx_EF = s.SIANCentralEF;
            return EFBusinessTransaction.ParaSIANCentral(s);
        }

        /// <summary>
        /// Crea una transacción dado el nombre clave del sistema SIANWeb al que se desea conectar
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="nombreSIANWeb">Nombre clave del sistema web</param>
        /// <returns>IBusinessTransaction</returns>
        public static IBusinessTransaction Obtener(Sesion s, string nombreSIANWeb)
        {
            return new EFBusinessTransaction(s, nombreSIANWeb);
        }

        /// <summary>
        /// Crea transacciones a partir de las conexiones configuradas en la sección SianCentral del archivo de configuración de la aplicación.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación del sistema</param>
        /// <returns>IEnumerable[IBusinessTransaction]</returns>
        public static IEnumerable<IBusinessTransaction> ObtenerParaSIANCentral(Sesion s)
        {
            List<IBusinessTransaction> resultado = new List<IBusinessTransaction>();
            foreach (var e in s.ConexionesSIANWeb)
            {
                resultado.Add(new EFBusinessTransaction(s, e.Key));
            }
            return resultado;
        }
    }
}
