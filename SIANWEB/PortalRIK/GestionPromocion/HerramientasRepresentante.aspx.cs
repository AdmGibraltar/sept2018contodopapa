using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB.PortalRIK.GestionPromocion
{
    public partial class HerramientasRepresentante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SIANWEB.MasterPage.PortalRIK mp = Master as SIANWEB.MasterPage.PortalRIK;
            mp.CurrentPath = new List<string>() { "Gestion de la Promoción", "Herramientas de Representante"}.ToArray();

            if (!IsPostBack)
            {
                Session["activeMenu"] = 3;
            }
        }
    }
}