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
    public partial class CatContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //imprimir();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
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
                        ValidarPermisos();

                        CargarLongitud(session2.Id_Cd, session2.Emp_Cnx);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                clscontraseña.ConsultaLongitudPass(conf, Conexion, ref list);
                this.Hidden1.Value = list[0].Contraseña_Long_Min;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    clsUsuario.ModificarContraseñaUsuario(ref usuario, session2.Emp_Cnx, ref verificador);

                    session2.Cu_Pass = this.txtContNueva.Text;
                    session2.Cu_Modif_Pass_Voluntario = true;

                    Session["Sesion" + Session.SessionID] = session2;

                    txtContAnt.Text = "";
                    txtContConf.Text = "";
                    txtContNueva.Text = "";

                    Alerta("Se modificó la contraseña correctamente");
                    //string script = "<script>RefreshParentPage()</" + "script>";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "RefreshParentPage()", script, false);
                    Response.Redirect("login.aspx");//se agrego para que cargue la nueva sesion
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex.Message);
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                //Permiso Permiso = new Permiso();
                //Permiso.Id_U = Sesion.Id_U;
                //Permiso.Id_Cd = Sesion.Id_Cd;
                //Permiso.Sm_cve = pagina.Clave;
                ////Esta clave depende de la pantalla

                //CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                //CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                //if (Permiso.PAccesar == true)
                //{
                //_PermisoGuardar = Permiso.PGrabar;
                //_PermisoModificar = Permiso.PModificar;
                //_PermisoEliminar = Permiso.PEliminar;
                //_PermisoImprimir = Permiso.PImprimir;

                //if (_PermisoGuardar == false)
                //{
                //    this.rtb1.Items[6].Visible = false;
                //}
                //if (_PermisoGuardar == false & _PermisoModificar == false)
                //{
                //    this.rtb1.Items[5].Visible = false;
                //}
                //if (Permiso.PEliminar == false)
                //{
                //    this.RadToolBar1.Items[3].Visible = false;
                //}
                //if(Permiso.PImprimir == false)
                //{
                //    this.RadToolBar1.Items[2].Visible = false;
                //}

                //Nuevo
                //Me.RadToolBar1.Items(6).Enabled = False
                //Guardar
                //Me.RadToolBar1.Items(5).Enabled = False
                //Regresar
                //this.rtb1.Items[4].Enabled = true;
                //Eliminar
                //this.rtb1.Items[3].Visible = false;
                //Imprimir
                //this.rtb1.Items[2].Visible = false;
                //Correo
                //this.rtb1.Items[1].Visible = false;
                //}
                //else
                //{
                //    Response.Redirect("Inicio.aspx");
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtContAnt.Text = "";
            txtContConf.Text = "";
            txtContNueva.Text = "";
        }
    }
}