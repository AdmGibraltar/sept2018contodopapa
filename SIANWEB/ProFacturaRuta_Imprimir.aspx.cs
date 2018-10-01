using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;

namespace SIANWEB
{
    public partial class ProFacturaRuta_Imprimir : System.Web.UI.Page
    {
        #region Variables
        private Sesion sesion
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
        #endregion Variables
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.txtIdEmb.Text = Page.Request.QueryString["Id_Emb"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "print")
                {
                    if (Page.IsValid)
                    {
                        this.Imprimir();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion Eventos
        #region Metodos
        private void Imprimir()
        {
            try
            {

                CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                CapaDatos.Funciones fecha = new CapaDatos.Funciones();
                ArrayList ALValorParametrosInternos = new ArrayList();
                CapaDatos.Funciones CD = new CapaDatos.Funciones();
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Convert.ToInt32(this.txtIdEmb.Text));

                Type instance = null;

                switch (this.optOpciones.SelectedItem.Value)
                {
                    case "rdFacturaCte":
                        instance = typeof(LibreriaReportes.ProFacturaRuta_Cte);
                        break;
                    case "rdFacturaPro":
                        instance = typeof(LibreriaReportes.ProFacturaRuta_Pro);
                        break;
                    case "rdFacturaFacPro":
                        instance = typeof(LibreriaReportes.ProFacturaRuta_FacPro);
                        break;
                }

                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReportePadre()");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Metodos
        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {

                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "', confirmCallBackFn);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
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