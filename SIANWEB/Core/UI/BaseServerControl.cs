using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.Core.UI
{
    /// <summary>
    /// Representa la base de todos los controles de servidor en SIANWEB. Esta clase contiene funcionalidad común utilizada en el dominio de la aplicación.
    /// </summary>
    public class BaseServerControl : UserControl
    {
        public BaseServerControl() : base()
        {
        }

        public IBusinessTransaction BusinessTransaction
        {
            get
            {
                return _ibt;
            }
        }

        private bool _managedBusinessTransaction = false;

        protected override void OnLoad(EventArgs e)
        {
            BaseServerPage possibleServerPage = Page as BaseServerPage;
            if (possibleServerPage != null)
            {
                _ibt = possibleServerPage.BusinessTransaction;
            }
            else
            {
                _ibt = CN_FabricaTransaccionNegocios.Default(EntidadSesion);
                _managedBusinessTransaction = true;
            }
            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if (_ibt != null && _managedBusinessTransaction)
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
                return string.Format("{0}://{1}{2}", Page.Request.Url.Scheme, Page.Request.Url.Authority, Page.Request.ApplicationPath.TrimEnd('/'));
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

        /// <summary>
        /// Regresa la sesión sensitiva al usuario en operación.
        /// </summary>
        protected Sesion EntidadSesion
        {
            get
            {
                return (Sesion)Page.Session["Sesion" + Page.Session.SessionID];
            }
            set
            {
                Page.Session["Sesion" + Page.Session.SessionID] = value;
            }
        }

        protected log4net.ILog Logger
        {
            get{
                return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
            
        }

        protected IBusinessTransaction _ibt = null;
    }
}