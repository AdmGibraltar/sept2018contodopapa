using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatNotificacion
    {
        public CatNotificacion Insertar(int idEmp, int idCd, int idTipoNotificacion, string contenido, bool leaida, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            CatNotificacion result = new CatNotificacion() { Id_Emp = idEmp, Id_Cd = idCd, Id_TipoNotificacion = idTipoNotificacion, Notif_Contenido = contenido, Notif_Leida = leaida };
            result = ctx.CatNotificacions.Add(result);
            return result;
        }

        /// <summary>
        /// Elimina la instancia CatNotificacion dado su identificador del repositorio de datos.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución al que pertenece el RIK idRik</param>
        /// <param name="idNotificacion">Identificador de la notificación de interés</param>
        /// <param name="icdCtx">Contexto de conexión al repositorio de datos</param>
        public void Eliminar(int idEmp, int idCd, int idNotificacion, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var instancias = from i in ctx.CatNotificacions
                             where i.Id_Emp==idEmp && i.Id_Cd==idCd && i.Id_Notificacion==idNotificacion
                             select i;
            if (instancias.Count() > 0)
            {
                var instancia = instancias.Single();
                ctx.CatNotificacions.Remove(instancia);
            }
        }
    }
}
