using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;

namespace SIANWEB.Core.UI
{
    /// <summary>
    /// Representa la base de las páginas visualizadas en SIANWEB. Esta clase contiene funcionalidad común utilizada en el dominio de la aplicación.
    /// </summary>
    public class BaseServerPage : Page
    {
        public BaseServerPage() : base()
        {
            
        }

        public IBusinessTransaction BusinessTransaction
        {
            get
            {
                return _ibt;
            }
        }

        /// <summary>
        /// Determina si el cliente tiene capacidades para Form Data.
        /// </summary>
        public bool SoportaFormData
        {
            get
            {
                return true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (EntidadSesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = Page.Request.Url.PathAndQuery; //pag[pag.Length - 1];
                Response.Redirect("~/login.aspx", true);
            }
            _ibt = CN_FabricaTransaccionNegocios.Default(EntidadSesion);
            _ibt.Begin();
            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if (_ibt != null)
            {
                _ibt.Dispose();
            }
        }

        /// <summary>
        /// Regresa la ubicación del recurso uniforme (URL) base de la aplicación.
        /// </summary>
        public String ApplicationUrl
        {
            get
            {
                return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Request.ApplicationPath.TrimEnd('/'));
            }
        }

        /// <summary>
        /// Regresa la sesión sensitiva al usuario en operación.
        /// </summary>
        protected Sesion EntidadSesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }

        /// <summary>
        /// Devuelve el tipo de usuario basado en la sesión del usuario en operación
        /// NOTA: este miembro debería estar definido en Sesion, pero debido a que Sesion se encuentra en una capa debajo de la capa de modelo no es posible agregar la referencia a la entidad TipoUsuario.
        /// </summary>
        protected SysTipoUsuario TipoUsuario
        {
            get
            {
                if (_TipoUsuario == null)
                {
                    CN_SysTipoUsuario cnSysTipoUsuario = new CN_SysTipoUsuario();
                    _TipoUsuario = cnSysTipoUsuario.ObtenerPorSesion(EntidadSesion);
                }
                return _TipoUsuario;
            }
        }

        /// <summary>
        /// Devuelve la entidad representativa del tipo de usuario "Gerente de Sucursal"
        /// </summary>
        protected SysTipoUsuario TipoUsuarioGerenteDeSucursal
        {
            get
            {
                if (_tipoUsuarioGerenteDeSucursal == null)
                {
                    CN_SysTipoUsuario cnSysTipoUsuario = new CN_SysTipoUsuario();
                    _tipoUsuarioGerenteDeSucursal = cnSysTipoUsuario.ObtenerPorDescripcion(EntidadSesion, "Gerente de Sucursal", _ibt);
                }
                return _tipoUsuarioGerenteDeSucursal;
            }
        }

        /// <summary>
        /// Devuelve la entidad representativa del usuario "Gerente de Sucursal"
        /// </summary>
        protected CapaModelo.CatUsuario GerenteDeSucursal
        {
            get
            {
                if (_gerenteDeSucursal == null)
                {
                    CN_CatUsuario cnCatUsuario = new CN_CatUsuario();
                    //La siguiente llamada devuelve un conjunto de usuarios basados en su tipo. En la práctica solo deberá haber un usuario con dicho tipo por empresa, así que se toma el primer elemento del conjunto.
                    var usuariosGerentesSucursal = cnCatUsuario.ObtenerPorTipo(TipoUsuarioGerenteDeSucursal, _ibt);
                    if (usuariosGerentesSucursal.Count() > 0)
                    {
                        _gerenteDeSucursal = usuariosGerentesSucursal.First();
                    }
                }
                return _gerenteDeSucursal;
            }
        }

        protected log4net.ILog Logger
        {
            get
            {
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }

        }

        protected IBusinessTransaction _ibt = null;

        private SysTipoUsuario _TipoUsuario = null;

        private SysTipoUsuario _tipoUsuarioGerenteDeSucursal = null;

        private CapaModelo.CatUsuario _gerenteDeSucursal = null;
    }
}