using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaModelo;
using CapaNegocios;
using SIANWEB.Core.UI;
using System.Data;


namespace SIANWEB.PortalRIK.GestionPromocion.Valuaciones
{
    public partial class UCAutorizacionValuaciones : BaseServerControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptrValuacionesAAprobar.DataSource = ValuacionesAAutorizar;
                rptrValuacionesAAprobar.DataBind();
            }
            else
            {
                rptrValuacionesAAprobar.DataSource = ValuacionesAAutorizar;
                rptrValuacionesAAprobar.DataBind();
            }

            BusinessTransaction.Commit();
        }

        /*
         * 11 Sep 2018 se Reemplaza RFH
        protected void CargarValuacionesAAutorizar()
        {
            CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
            _ValuacionesAAutorizar = cnCapValProyecto.AAutorizar(EntidadSesion, BusinessTransaction);
        }
        */

        protected void CargarValuacionesAAutorizar()
        {
            CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
            _ValuacionesAAutorizar = cnCapValProyecto.CRM2_ConsultarValuacionesAAutorizar(EntidadSesion);
        }

        protected List<CapaEntidad.eCapValProyecto> ValuacionesAAutorizar
        //protected IEnumerable<CapValProyecto> ValuacionesAAutorizar
        {
            get
            {
                if (_ValuacionesAAutorizar == null)
                {
                    CargarValuacionesAAutorizar();
                }
                return _ValuacionesAAutorizar;
            }
        }

        protected int ConteoValuacionesAAutorizar
        {
            get
            {
                try
                {
                    return _ValuacionesAAutorizar.Count();
                }
                catch
                {
                    return 0; //RFH
                }
            }
        }

        //protected IEnumerable<CapValProyecto> _ValuacionesAAutorizar = null;
        protected List<CapaEntidad.eCapValProyecto> _ValuacionesAAutorizar = null;

    }
}