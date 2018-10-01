using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapRIKNotificacion
    {
        public CapRIKNotificacion Insertar(int idEmp, int idCd, int idRik, int idNotificacion, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            CapRIKNotificacion resultado = new CapRIKNotificacion() { Id_Emp = idEmp, Id_Cd = idCd, Id_Rik = idRik, Id_Notificacion = idNotificacion };
            ctx.CapRIKNotificacions.Add(resultado);
            return resultado;
        }

        /// <summary>
        /// Consulta el conjunto de entradas CapRIKNotificacion asociadas al RIK idRik.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución al que pertenece el RIK idRik</param>
        /// <param name="idRik">Identificador del RIK al cual se encuentran asociadas las entradas de interés</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IQueryable[CapRIKNotificacion]</returns>
        public IQueryable<CapRIKNotificacion> Obtener(int idEmp, int idCd, int idRik, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var notificaciones = (from n in ctx.CapRIKNotificacions
                                  where n.Id_Emp == idEmp && n.Id_Cd == idCd && n.Id_Rik == idRik
                                  select n);
            return notificaciones;
        }

        /// <summary>
        /// Elimina la instancia especificada del repositorio CapRIKNotificacion.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el centro de distribución idCd</param>
        /// <param name="idCd">Identificador del centro de distribución al que pertenece el RIK idRik</param>
        /// <param name="idRik">Identificador del RIK al que se encuentra asociado la notificación idNotificación</param>
        /// <param name="idNotificacion">Identificador de la notificación a elimianr</param>
        /// <param name="icdCtx">Contexto de conexión al repositorio de datos</param>
        public void Eliminar(int idEmp, int idCd, int idRik, int idNotificacion, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var instancias = from i in ctx.CapRIKNotificacions
                             where i.Id_Emp==idEmp && i.Id_Cd==idCd && i.Id_Rik==idRik && i.Id_Notificacion==idNotificacion
                             select i;
            if (instancias.Count() > 0)
            {
                var instancia = instancias.Single();
                ctx.CapRIKNotificacions.Remove(instancia);
            }
        }
    }
}
