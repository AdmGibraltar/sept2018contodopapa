using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.Threading;
using System.IO;
using System.Text;

namespace SIANWEB
{
    public partial class RepEmbudoClientes : System.Web.UI.Page
    {
        public string strTotales = "";
        public string strPlaneados = "";
        public string strCalendario = "";
        public string strCumplidos = "";

        protected void Page_Load(object sender, EventArgs e)
        {
             try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);                      
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];       
                    Response.Redirect("Login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "Page_Load");
            }
            
        }
    #region Eventos

        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                //  comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                string Evento = Request.Form["__EVENTTARGET"].ToString();

                //if (!_PermisoImprimir)
                //{
                //    this.Alerta("No tiene permisos para ver el reporte");
                //    return;
                //}
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                //if (Page.IsValid)
                //{
                //    if (btn.CommandName == "print")
                //    {
                //        Imprimir();
                //    }

                //}
            }
            catch (Exception ex)
            {
                Alerta("Imposible generar el reporte; aún no se han generado los respaldos del mes y año seleccionados");
            }
        }

    #endregion
       
    #region Funciones

    #endregion

    #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

    #endregion
    }
}