using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Data;

namespace SIANWEB
{
    public partial class CrmPromocion_Cancelar : System.Web.UI.Page
    {
        public Sesion session
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
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
                {
                    CerrarVentana();
                }
                else
                {                  
                        if (!Page.IsPostBack)
                        {
                            if (session.Cu_Modif_Pass_Voluntario == false)
                                return;
                            CargarTipos();                            
                        }
                }               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }            
        }
        protected void ibtnCancelarOportunidad_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int tipo = !string.IsNullOrEmpty(ddlCausa.SelectedValue) ? Convert.ToInt32(ddlCausa.SelectedValue) : 0;
                string competidor = txtCompetidor.Text;
                string comentario = txtComentario.Text;
                if (tipo > 0)
                {
                    Session["NumCausa"] = tipo.ToString();
                    Session["NumCancela"] = comentario;
                    Session["NumCompetencia"] = competidor; 
                    string funcion = "returnToParent(" + tipo + ",'" + competidor + "','" + comentario + "')";
                    string script = "<script>" + funcion + "</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                }
                else
                    Alerta("Seleccione una causa para la cancelación");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ibtnCerrarVentana_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string funcion = "returnToParent(0, '0', '0')";//"CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private void CargarTipos()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Emp_Cnx, "spCrmCausasCancelacion_Combo", ref ddlCausa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("alert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
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