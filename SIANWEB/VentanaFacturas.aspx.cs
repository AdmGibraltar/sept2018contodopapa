using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class VentanaFacturas : System.Web.UI.Page
    {
        public static string param { get; set; }

        #region Metodos
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            param = "Eliminar";
            func_cerrarventana(param);
        }

        protected void btnRefacturar_Click(object sender, EventArgs e)
        {
            param = "Refacturar";
            func_cerrarventana(param);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                func_cerrarventana(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void func_cerrarventana(string param)
        {
            string funcion = "CloseAndRebind('" + param + "')";
            string script = "<script>" + funcion + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

       
    }
}