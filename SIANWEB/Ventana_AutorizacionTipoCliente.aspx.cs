using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Configuration;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class Ventana_AutorizacionTipoCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //HF_IdTU.Value = Page.Request.QueryString["Id_Tu"].ToString();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtUserName.Text == "" || txtPassword.Text == "")
                {
                    return;
                }
                ErrorManager();
                Usuario usuario = new Usuario();
                usuario.Cu_User = this.txtUserName.Text;
                usuario.Cu_pass = this.txtPassword.Text;

                Entrar(usuario);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                btnEntrar_Click(btnEntrar, null);

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        private void Entrar(Usuario usuario)
        {
            try
            {
                string StrCnx = ConfigurationManager.AppSettings.Get("strConnection");
                Int32 Id = default(Int32);
                Empresa Empresa = new Empresa();
                bool Dependientes = false;
                CapaNegocios.CN_Login CN_Login = new CapaNegocios.CN_Login();
                Empresa.Emp_Cnx = StrCnx + ";Connect Timeout=600";
                Int32 Minutos = default(Int32);

                //Aqui se debe llamar a una clase que en caso de que encuentre el usuario y contraseña regrese información del usuario así como el uso horario, información de bloqueo, y caducidad del password
                CN_Login.Login(ref usuario, out Id, out Minutos, out Dependientes, Empresa.Emp_Cnx);



                List<int> CDIS = new List<int> { 110, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 310, 340, 350, 360, 370, 380, 390, 400, 430, 510, 620, 640 };

                //Datos correctos
                if (Id == 1)
                {
                    //La cuenta no está bloqueada
                    if (usuario.Cu_Estatus == true)
                    {
                        if (!usuario.Cu_Activo)
                        {
                            Alerta("La cuenta está inactiva");
                            return;
                        }

                        Sesion session = new Sesion();
                        session = (Sesion)Session["Sesion" + Session.SessionID];

                        int CDI = -1;

                        CDI = CDIS.Find(x => x == session.Id_Cd_Ver);

                        if (!Session["Sesion" + Session.SessionID + "TCte_Autorizadores"].ToString().Contains("," + usuario.Id_U.ToString() + ",") && CDI != 0)
                        {
                            Alerta("El usuario no cuenta con la autorización necesaria");
                        }
                        else
                        {
                            CerrarVentana(usuario.Id_U.ToString(), usuario.Id_Cd.ToString(), usuario.U_Nombre);
                        }
                    }
                    else
                    {
                        Alerta("La cuenta está bloqueada");
                    }
                }
                else
                {
                    AlertaFocus("El usuario o contraseña son incorrectos", txtPassword.ClientID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CerrarVentana(string id_u, string id_cd, string nombre)
        {
            try
            {
                string funcion = "CloseWindow(" + id_u + "," + id_cd + ",'" + nombre + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
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
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
                this.LblMensaje.Text = "";
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
                this.LblMensaje.Text = Message;
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
                this.LblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.LblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}