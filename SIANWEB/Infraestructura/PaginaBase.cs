using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
using System.Reflection;
using System.Collections;

namespace SIANWEB
{
    public class PaginaBase : System.Web.UI.Page
    {
        protected Sesion gSession;

        private RadAjaxManager gRAM;

        public RadAjaxManager GlobalRAM
        {
            get { return gRAM; }
            set { gRAM = value; }
        }

        private Label lMensaje;

        public Label LabelMensaje
        {
            get { return lMensaje; }
            set { lMensaje = value; }
        }

        protected override void OnPreInit(EventArgs e)
        {
            gSession = (Sesion)Session["Sesion" + Session.SessionID];
            base.OnPreInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {                
                gSession = (Sesion)Session["Sesion" + Session.SessionID];

                if (gSession == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {                    
                    base.OnLoad(e);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }            
        }
                
        #region ErrorManager
        protected void Alerta(string mensaje)
        {
            try
            {
                gRAM.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        protected void Alerta(string mensaje, string controlID)
        {
            try
            {
                string vScript = string.Format("radalert('{0}', 330, 150); $find('{1}').focus();", mensaje, controlID);
                gRAM.ResponseScripts.Add(vScript);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        protected void Alerta(string mensaje, string funcion, string msjTitulo)
        {
            try
            {
                 string vScript = string.Format("radalert('{0}', 330, 150, '{1}', '{2}', '');", mensaje, msjTitulo, funcion);
                gRAM.ResponseScripts.Add(vScript);
            }
            catch (Exception ex)
            {
               ErrorManager(ex, "Alerta");
            }
        }

        protected void ErrorManager()
        {
            try
            {
                this.lMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ErrorManager(string Message)
        {
            try
            {
                this.lMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        #endregion
    }
}