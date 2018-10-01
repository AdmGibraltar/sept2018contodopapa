using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;

namespace SIANWEB.js.ListControl
{
    public partial class UCListControlToolbar_js : System.Web.UI.UserControl
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

        }
    }
}