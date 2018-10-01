using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIANWEB.Core.UI;
using CapaModelo;
using CapaNegocios;

namespace SIANWEB.PortalRIK.Navegacion.Notificaciones
{
    public partial class UCNotificaciones : BaseServerControl
    {
        /// <summary>
        /// Total de notificaciones
        /// </summary>
        public int TotalNotificaciones
        {
            get
            {
                return _notificaciones.Count;
            }
        }

        /// <summary>
        /// Notificaciones no leidas
        /// </summary>
        public int TotalNotificacionesNuevas
        {
            get
            {
                return _notificaciones.Where(ben => !ben.NotificacionLeida).Count();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarNotificaciones();
        }

        protected IEnumerable<CapUsuarioNotificacion> _notificacionesUsuario = null;
        protected IEnumerable<CapRIKNotificacion> _notificacionesRIK = null;
        protected List<BaseElementoNotificacion> _notificaciones = new List<BaseElementoNotificacion>();

        protected void ProcesarNotificacionesRIK()
        {
            var notificacionesProcesadas = from n in _notificacionesRIK
                                           select new BaseElementoNotificacion()
                                           {
                                               NotificacionLeida = n.CatNotificacion.Notif_Leida,
                                               IdNotificacion = n.Id_Notificacion.ToString(),
                                               IdElemento = string.Format("notif_{0}_rik{1}", n.Id_Notificacion, n.Id_Rik),
                                               ClaseIcono = _clasesIconos[n.CatNotificacion.Id_TipoNotificacion],
                                               Contenido = n.CatNotificacion.Notif_Contenido,
                                               OperacionEliminar = string.Format("eliminarNotificacionRIK({0}, '{1}')", n.Id_Notificacion, string.Format("notif_{0}_rik{1}", n.Id_Notificacion, n.Id_Rik))
                                           };
            _notificaciones.AddRange(notificacionesProcesadas);
        }

        protected void ProcesarNotificacionesUsuario()
        {
            var notificacionesDeUsuario = from un in _notificacionesUsuario
                                          select new BaseElementoNotificacion()
                                          {
                                              NotificacionLeida = un.CatNotificacion.Notif_Leida,
                                              IdNotificacion = un.Id_Notificacion.ToString(),
                                              IdElemento = string.Format("notif_{0}_u{1}", un.Id_Notificacion, un.Id_U),
                                              ClaseIcono = _clasesIconos[un.CatNotificacion.Id_TipoNotificacion],
                                              Contenido = un.CatNotificacion.Notif_Contenido,
                                              OperacionEliminar = string.Format("eliminarNotificacionUsuario({0}, '{1}')", un.Id_Notificacion, string.Format("notif_{0}_u{1}", un.Id_Notificacion, un.Id_U))
                                          };
            _notificaciones.AddRange(notificacionesDeUsuario);
        }

        protected void CargarNotificaciones()
        {
            CargarNotificacionesRIK();
            if (_notificacionesRIK != null)
            {
                ProcesarNotificacionesRIK();
            }

            CargarNotificacionesDeUsuario();
            if (_notificacionesUsuario != null)
            {
                ProcesarNotificacionesUsuario();
            }

            rptrNotificaciones.DataSource = _notificaciones;
            rptrNotificaciones.DataBind();
        }

        protected void CargarNotificacionesRIK()
        {
            CN_CapRIKNotificacion cnCapRIKNotificacion = new CN_CapRIKNotificacion();
            _notificacionesRIK = cnCapRIKNotificacion.Obtener(EntidadSesion, BusinessTransaction);
        }

        protected void CargarNotificacionesDeUsuario()
        {
            CN_CapUsuarioNotificacion cnCapUsuarioNotificacion = new CN_CapUsuarioNotificacion();
            _notificacionesUsuario = cnCapUsuarioNotificacion.ObtenerPorUsuario(EntidadSesion, _ibt);
        }

        public class BaseElementoNotificacion
        {
            public string IdNotificacion
            {
                get;
                set;
            }

            public string IdElemento
            {
                get;
                set;
            }

            public string OperacionEliminar
            {
                get;
                set;
            }

            public string Contenido
            {
                get;
                set;
            }

            public string ClaseIcono
            {
                get;
                set;
            }

            public bool NotificacionLeida
            {
                get;
                set;
            }
        }

        public enum ClasesNotificacion
        {
            OK = 0,
            ERROR,
            INFO,
            ENVELOPE,
            USER,
            REBALANCE,
            MESSAGES,
            BLUEPRINT,
            BUNDLE
        }

        public string[] _clasesIconos =
        {
            "i pficon pficon-ok",
            "i pficon pficon-error-circle-o",
            "i pficon pficon-info",
            "fa-envelope",
            "i pficon pficon-user",
            "i pficon pficon-rebalance",
            "i pficon pficon-messages",
            "i pficon pficon-blueprint",
            "i pficon pficon-bundle"
        };
    }
}