using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB
{
    public partial class Ventana_ModContrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                Salir();
            }
            else
            {
                if (Page.IsPostBack == false)
                {
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];

                    if (session2.Cu_Modif_Pass_Voluntario == false)
                    {
                        this.btnCancelar.Visible = false;
                    }
                    else
                    {
                        this.btnCancelar.Visible = true;
                    }

                     
                    CargarLongitud(session2.Id_Cd, session2.Emp_Cnx);
                }
            }

        }
        
        private void CargarLongitud(Int32 Id_Ofi, string Conexion)
        {
            try
            {
                CapaNegocios.CN_Contraseña clscontraseña = new CapaNegocios.CN_Contraseña();
                ConfiguracionGlobal conf = new ConfiguracionGlobal();
                conf.Id_Cd = Id_Ofi;
                System.Collections.Generic.List<ConfiguracionGlobal> list = new System.Collections.Generic.List<ConfiguracionGlobal>();
                clscontraseña.ConsultaLongitudPass(conf, Conexion,ref list);
                this.Hidden1.Value = list[0].Contraseña_Long_Min;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Alerta(string mensaje)
        {
            string radalertscript = "<script language='javascript'>function f(){radalert('" + mensaje + "', 310, 70); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        private bool ValidaCampos()
        {
            bool functionReturnValue = false;
            try
            {
                Sesion session2 = new Sesion();
                string ContraseñaAnt = null;
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                ContraseñaAnt = session2.Cu_Pass;

                functionReturnValue = true;
                if (this.txtContAnt.Text != ContraseñaAnt)
                {
                    functionReturnValue = false;
                    SetFocus(this.txtContAnt);
                    Alerta("La contraseña anterior no es correcta");
                }
                else if (this.txtContNueva.Text.Length < Convert.ToInt32(this.Hidden1.Value))
                {
                    functionReturnValue = false;
                    SetFocus(this.txtContNueva);
                    Alerta("La nueva contraseña debe tener al menos " + this.Hidden1.Value + " caracteres");
                }
                else if (this.txtContNueva.Text == this.txtContAnt.Text)
                {
                    functionReturnValue = false;
                    SetFocus(this.txtContNueva);
                    Alerta("La nueva contraseña no puede ser igual a la anterior");
                }
                else if (this.txtContNueva.Text != txtContConf.Text)
                {
                    functionReturnValue = false;
                    SetFocus(this.txtContConf);
                    Alerta("La confirmación no coincide con la nueva contraseña");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return functionReturnValue;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                if (ValidaCampos())
                {
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];

                    CapaNegocios.CN_CatUsuario clsUsuario = new CapaNegocios.CN_CatUsuario();

                    Usuario usuario = new Usuario();
                    usuario.Cu_pass = this.txtContNueva.Text;
                    usuario.Id_U = session2.Id_U;
                    usuario.Id_Cd = session2.Id_Cd;
                    Int32 verificador = default(Int32);
                    clsUsuario.ModificarContraseñaUsuario(ref usuario, session2.Emp_Cnx,ref verificador);

                    session2.Cu_Pass = this.txtContNueva.Text;
                    session2.Cu_Modif_Pass_Voluntario = true;

                    Session["Sesion" + Session.SessionID] = session2;

                    string script = "<script>RefreshParentPage()</" + "script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RefreshParentPage()", script, false);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex.Message);
            }

        }
        private void Salir()
        {

            try
            {
                string funcion = null;
                //If Me.HiddenRebind.Value = 0 Then
                funcion = "CloseWindow()";
                //Else
                //funcion = "CloseAndRebind()"
                //End If
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string script = "<script>CloseWindow()</" + "script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow()", script, false);
        }

        #region ErrorManager
         
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