using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class ProActualizacionComodato_Ventana : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFolio.Focus();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            Session["ComodatoVentana" + Session.SessionID] = txtFolio.Text;
            RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_FacturaPedido()"));

        }
    }
}