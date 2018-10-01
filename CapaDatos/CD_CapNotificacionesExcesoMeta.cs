using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapNotificacionesExcesoMeta
    {
        public CD_CapNotificacionesExcesoMeta(String cadenaDeConexionEF)
        {
            _cadenaDeConxionEF = cadenaDeConexionEF;
        }

        public CapNotificacionExcesoMeta Consultar(int? idEmp, int? idCd)
        {
            CapNotificacionExcesoMeta ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConxionEF))
            {
                var res = ctx.spCapNotificacionesExcesoMeta_Consultar(idEmp, idCd).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }

        /// <summary>
        /// Inserta un registro en la tabla CapNotificacionExcesoMeta.
        /// </summary>
        /// <param name="id_Emp">Identificador de la empresa</param>
        /// <param name="id_Cd">Identificador del centro de distribución</param>
        /// <param name="nem_Asunto">Texto de asunto del mensaje</param>
        /// <param name="nem_FechaEnvio">Sin uso.</param>
        /// <param name="nem_Cuerpo">Cadena de texto del cuerpo del mensaje</param>
        public void Insertar(int? id_Emp, int? id_Cd, string nem_Asunto, DateTime? nem_FechaEnvio, string nem_Cuerpo, byte[] contenidoArchivoAdjunto)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConxionEF))
            {
                ctx.spCapNotificacionesExcesoMeta_Insertar(id_Emp, id_Cd, nem_Asunto, nem_FechaEnvio, nem_Cuerpo, contenidoArchivoAdjunto);
            }
        }

        public string _cadenaDeConxionEF = string.Empty;
    }
}
