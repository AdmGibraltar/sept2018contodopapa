using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    /// <summary>
    /// Clase de reglas de negocio para la entidad CapUsuarioNotificacion
    /// </summary>
    public class CN_CapUsuarioNotificacion
    {
        /// <summary>
        /// Crea una nueva notificación dirijida al usuario idUsuario
        /// </summary>
        /// <param name="s">Sesión de trabajo del usuario en operación</param>
        /// <param name="idUsuario">Identificador del usuario de destino de la notificación</param>
        /// <param name="mensaje">Contenido del mensaje de la notificación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CapUsuarioNotificacion</returns>
        public CapUsuarioNotificacion CrearNotificacionParaUsuario(Sesion s, int idUsuario, string mensaje, IBusinessTransaction ibt)
        {
            CD_CapUsuarioNotificacion cdCapUsuarioNotificacion = new CD_CapUsuarioNotificacion();
            CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
            var notificacion = cdCatNotificacion.Insertar(s.Id_Emp, s.Id_Cd, 5, mensaje, false, ibt.DataContext);
            CapUsuarioNotificacion cun=new CapUsuarioNotificacion(){ Id_Emp=s.Id_Emp, Id_Cd=s.Id_Cd, Id_Notificacion=notificacion.Id_Notificacion, Id_U=idUsuario};
            cun = cdCapUsuarioNotificacion.Insertar(cun, ibt.DataContext);
            return cun;
        }

        /// <summary>
        /// Regresa el conjunto de notificaciones asociadas a un usuario.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción a nivel de base de datos</param>
        /// <returns>IEnumerable[CapUsuarioNotificacion]</returns>
        public IEnumerable<CapUsuarioNotificacion> ObtenerPorUsuario(Sesion s, IBusinessTransaction ibt)
        {
            CD_CapUsuarioNotificacion cdCapUsuarioNotificacion = new CD_CapUsuarioNotificacion();
            return cdCapUsuarioNotificacion.ConsultarPorUsuario(s.Id_Emp, s.Id_Cd, s.Id_U, ibt.DataContext);
        }
    }
}
