using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;

namespace SIANWEB.PortalRIK.GestionPromocion.Valuaciones
{
    public partial class ListadoValuaciones : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            SIANWEB.MasterPage.PortalRIK mp = Master as SIANWEB.MasterPage.PortalRIK;
            mp.CurrentPath = new List<string>() { "Gestion de la Promoción", "Valuaciones" }.ToArray();

            if (!IsPostBack)
            {
                Session["activeMenu"] = 5;
                try
                {
                    
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}