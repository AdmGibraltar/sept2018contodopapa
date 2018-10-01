using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class CrmAceptarAvance : System.Web.UI.Page
    {
        public int idRegreso = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            parametros();
        }

        protected void parametros()
        {
            if (Request.QueryString["id"] != null)
            {
                string strId = Request.QueryString["id"].ToString();                
                Int32.TryParse(strId, out idRegreso);
            }
        }

        protected void btnAvanzar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["NumTipo"] = idRegreso.ToString();
                Session["NumValido"] = "1"; 
                string fecha = DateTime.Now.ToShortDateString();
                string funcion = "returnToParent(" + idRegreso + ",'" + fecha + "', 1)";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ibtnCerrar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["NumTipo"] = idRegreso.ToString();                
                Session["NumValido"] = "0"; 
                string fecha = DateTime.Now.ToShortDateString();
                string funcion = "returnToParent(" + idRegreso + ",'" + fecha + "', 0)";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}