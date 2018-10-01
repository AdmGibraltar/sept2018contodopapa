using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    /// <summary>
    /// Clase de negocio de la entidad de relación CapRIKNotificacion.
    /// </summary>
    public class CN_CapRIKNotificacion
    {
        /// <summary>
        /// Crea una nueva notificación de proyecto.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="mensaje">Mensaje de la notificación</param>
        /// <param name="ibt">Instancia de transacción de capa de negocio</param>
        /// <returns>CapRIKNotificacion</returns>
        public CapRIKNotificacion CrearNotificacionNuevoProyecto(Sesion s, string mensaje, IBusinessTransaction ibt)
        {
            CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
            var catNotificacion = cdCatNotificacion.Insertar(s.Id_Emp, s.Id_Cd, 6, mensaje, false, ibt.DataContext);
            CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
            var capRikNotificacion = cdCapRIKNotificacion.Insertar(s.Id_Emp, s.Id_Cd, s.Id_Rik, catNotificacion.Id_Notificacion, ibt.DataContext);
            return capRikNotificacion;
        }

        /// <summary>
        /// Crea una nueva notificación de nueva valuación para el rik en sesión
        /// </summary>
        /// <param name="s">Sesión dek usuario en operación</param>
        /// <param name="mensaje">Mensaje de la notificación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CapRIKNotificacion</returns>
        public CapRIKNotificacion CrearNotificacionNuevaValuacion(Sesion s, string mensaje, IBusinessTransaction ibt)
        {
            CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
            var catNotificacion = cdCatNotificacion.Insertar(s.Id_Emp, s.Id_Cd, 5, mensaje, false, ibt.DataContext);
            CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
            var capRikNotificacion = cdCapRIKNotificacion.Insertar(s.Id_Emp, s.Id_Cd, s.Id_Rik, catNotificacion.Id_Notificacion, ibt.DataContext);
            return capRikNotificacion;
        }

        [System.Obsolete("Este método estaba destinado a registrar notificaciones para otro usuario....que pudiera no ser RIK :(. Utiliza mejor CN_CapUsuarioNotificacion::Crear", true)]
        public CapRIKNotificacion CrearNotificacionValuacionAAprobar(Sesion s, string mensaje, IBusinessTransaction ibt)
        {
            CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
            var catNotificacion = cdCatNotificacion.Insertar(s.Id_Emp, s.Id_Cd, 5, mensaje, false, ibt.DataContext);
            CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
            var capRikNotificacion = cdCapRIKNotificacion.Insertar(s.Id_Emp, s.Id_Cd, s.Id_Rik, catNotificacion.Id_Notificacion, ibt.DataContext);
            return capRikNotificacion;
        }

        /// <summary>
        /// Obtiene el conjunto de notificaciones asociadas al rik en sesión
        /// </summary>
        /// <param name="s">Sesión del rik en operacion</param>
        /// <param name="ibt">Instancia de transacción a nivel de capa de negocio</param>
        /// <returns>CapRIKNotificacion[]</returns>
        public IEnumerable<CapRIKNotificacion> Obtener(Sesion s, IBusinessTransaction ibt)
        {
            CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
            var resultado = cdCapRIKNotificacion.Obtener(s.Id_Emp, s.Id_Cd, s.Id_Rik, ibt.DataContext);
            return resultado;
        }

        /// <summary>
        /// Elimina la instancia CapRIKNotificacion asociada a la notificacion idNotificacion y al RIK en operación
        /// </summary>
        /// <param name="s">Sesión del RIK en operación</param>
        /// <param name="idNotificacion">Identificador de la notificación</param>
        /// <param name="ibt">Instancia de transacción a nivel capa de negocio</param>
        public void Eliminar(Sesion s, int idNotificacion, IBusinessTransaction ibt)
        {
            CD_CapRIKNotificacion cdCapRIKNotificacion = new CD_CapRIKNotificacion();
            cdCapRIKNotificacion.Eliminar(s.Id_Emp, s.Id_Cd, s.Id_Rik, idNotificacion, ibt.DataContext);
            CD_CatNotificacion cdCatNotificacion = new CD_CatNotificacion();
            cdCatNotificacion.Eliminar(s.Id_Emp, s.Id_Cd, idNotificacion, ibt.DataContext);
        }
    }
}
