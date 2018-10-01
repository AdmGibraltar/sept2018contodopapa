using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class CRMAceptarCambios : System.Web.UI.Page
    {
        public int idRegreso = 0;
        public string Cliente = string.Empty;
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
            if (Request.QueryString["Cliente"] != null)
                Cliente = Request.QueryString["Cliente"].ToString();

            if (idRegreso != 0 && !string.IsNullOrEmpty(Cliente))
            {
                string label = "En el proyecto " + idRegreso.ToString() + " del cliente " + Cliente;
                label += " Se ha detectado que una o más aplicaciones NO están SELECCIONADAS.";
                this.Proyecto.InnerHtml = label;
            }
            else
            {
                string funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
        }       

        protected void btnAvanzar_Click(object sender, EventArgs e)
        {
            try
            {
                Session["NumValido"] = "1";
                //string funcion = "returnToParent(1)";
                //string script = "<script>" + funcion + "</script>";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                RadAjaxManager1.ResponseScripts.Add("return returnToParent(1);");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ibtnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                Session["NumValido"] = "0";
                string funcion = "returnToParent(0)";
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