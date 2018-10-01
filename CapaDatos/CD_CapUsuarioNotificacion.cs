using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    /// <summary>
    /// Representa la implementación de la interface para ejecutar operaciones de datos sobre el repositorio de la entidad CapUsuarioNotificacion
    /// </summary>
    public class CD_CapUsuarioNotificacion
    {
        /// <summary>
        /// Crea un registro en el repositorio de la entidad CapUsuarioNotificacion
        /// </summary>
        /// <param name="entidad">Instancia de datos de la entidad CapUsuarioNotificacion</param>
        /// <param name="icdCtx">Contexto de conexión al repositorio CapUsuarioNotificacion</param>
        /// <returns></returns>
        public CapUsuarioNotificacion Insertar(CapUsuarioNotificacion entidad, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            ctx.CapUsuarioNotificacions.Add(entidad);
            return entidad;
        }

        /// <summary>
        /// Regresa el resultado de la consulta a la entidad CapUsuarioNotificacion condicionada por el usuario idU
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro e distribucion</param>
        /// <param name="idU">Identificador del usuario</param>
        /// <param name="icdCtx">Contexto de conexión al repositorio CapUsuarioNotificacion</param>
        /// <returns>IQueryable[CapUsuarioNotificacion]</returns>
        public IQueryable<CapUsuarioNotificacion> ConsultarPorUsuario(int idEmp, int idCd, int idU, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var notificaciones = from un in ctx.CapUsuarioNotificacions
                                 where un.Id_Emp==idEmp && un.Id_Cd==idCd && un.Id_U==idU
                                 select un;
            return notificaciones;
        }
    }
}
