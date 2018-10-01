using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaModelo;
using CapaDatos;
using CapaNegocios;

namespace SIANWEB.PortalRIK.GestionPromocion
{
    public partial class SelectorDimension : System.Web.UI.UserControl
    {
        public string AlSeleccionar
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CN_CatEmpresa cnCatEmpresa = new CN_CatEmpresa();
                rptSelectorDimension.DataSource = cnCatEmpresa.ObtenerUENs(Sesion);
                rptSelectorDimension.DataBind();
            }
        }

        private Sesion Sesion
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
    }
}