using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.IO;

namespace SIANWEB
{
    public partial class CatDesbloqueo : System.Web.UI.Page
    {
        #region Variables
                private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Context.Items.Add("href", pag[pag.Length-1]);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    if (Page.IsPostBack == false)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RadAjaxManager1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        }

                        CargarCentros();
                        this.txtUsuario.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Nuevo(1);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Nuevo(0);
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Usuario Usuario = new Usuario();
                Int32 Verificador = default(Int32);
                Usuario.Id_Cd = Sesion.Id_Cd_Ver;
                Usuario.Cu_User = this.txtUsuario.Text;

                if (this.txtUsuario.Text != string.Empty)
                {
                    CapaNegocios.CN_CatUsuario CN_Negocios = new CapaNegocios.CN_CatUsuario();
                    CN_Negocios.BloqueoConsulta(Usuario, Sesion.Emp_Cnx, ref Verificador);
                }
                else
                {
                    Verificador = 2;
                }

                if (Verificador == 1)
                {
                    this.HiddenId_U.Value = Usuario.Id_U.ToString();
                    this.chkBloqueado.Checked = !Usuario.Cu_Estatus;
                    this.TblDesbloquear.Rows[3].Cells[1].InnerText = Usuario.U_Nombre;
                    if (Usuario.Cu_FBloq.ToString("dd/MM/yyyy") == "01/01/1900")
                    {
                        this.TblDesbloquear.Rows[4].Cells[1].InnerText = "Nunca";
                    }
                    else
                    {
                        this.TblDesbloquear.Rows[4].Cells[1].InnerText = Usuario.Cu_FBloq.ToString("dd/MM/yyyy hh:mm tt");
                    }

                    if (this.chkBloqueado.Checked == false)
                    {
                        this.chkBloqueado.Enabled = false;
                    }
                }
                else if (Verificador == 2)
                {
                    Alerta("La cuenta no pertenece al centro de distribución o no existe");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Nuevo(0);
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Usuario Usuario = new Usuario();
                Int32 Verificador = default(Int32);
                Usuario.Id_Cd = Sesion.Id_Cd_Ver;
                Usuario.Cu_User = this.txtUsuario.Text;

                if (this.txtUsuario.Text != string.Empty)
                {
                    CapaNegocios.CN_CatUsuario CN_Negocios = new CapaNegocios.CN_CatUsuario();
                    CN_Negocios.BloqueoConsulta(Usuario, Sesion.Emp_Cnx, ref Verificador);
                }
                else
                {
                    Verificador = 2;
                }

                if (Verificador == 1)
                {
                    this.HiddenId_U.Value = Usuario.Id_U.ToString();
                    this.chkBloqueado.Checked = !Usuario.Cu_Estatus;
                    this.TblDesbloquear.Rows[3].Cells[1].InnerText = Usuario.U_Nombre;
                    if (Usuario.Cu_FBloq.ToString("dd/MM/yyyy") == "01/01/1900")
                    {
                        this.TblDesbloquear.Rows[4].Cells[1].InnerText = "Nunca";
                    }
                    else
                    {
                        this.TblDesbloquear.Rows[4].Cells[1].InnerText = Usuario.Cu_FBloq.ToString("dd/MM/yyyy hh:mm tt");
                    }

                    if (this.chkBloqueado.Checked == false)
                    {
                        this.chkBloqueado.Enabled = false;
                    }
                }
                else if (Verificador == 2)
                {
                    Alerta("La cuenta no pertenece al centro de distribución o no existe");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion

        #region Funciones
        private void Nuevo(Int32 Opcion)
        {
            try
            {
                if (Opcion > 0)
                {
                    this.txtUsuario.Text = string.Empty;
                }

                this.HiddenId_U.Value = string.Empty;
                this.chkBloqueado.Checked = true;
                this.chkBloqueado.Enabled = true;
                this.TblDesbloquear.Rows[3].Cells[1].InnerText = string.Empty;
                this.TblDesbloquear.Rows[4].Cells[1].InnerText = string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }
                if (this.HiddenId_U.Value != string.Empty)
                {
                    if (this.chkBloqueado.Checked == false)
                    {
                        Sesion Sesion = new Sesion();
                        Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                        Usuario Usuario = new Usuario();
                        Int32 Verificador = default(Int32);
                        Usuario.Id_Cd = Sesion.Id_Cd_Ver;
                        Usuario.Id_U = Convert.ToInt32(this.HiddenId_U.Value);
                        Usuario.Cu_Estatus = !this.chkBloqueado.Checked;

                        CapaNegocios.CN_CatUsuario CN_Negocios = new CapaNegocios.CN_CatUsuario();
                        CN_Negocios.BloqueoModificar(Usuario, Sesion.Emp_Cnx, ref Verificador);

                        Alerta("La cuenta ha sido desbloqueada");
                    }
                    else
                    {
                        Alerta("Para desbloquear la cuenta primero desmarque la casilla");
                    }
                }
                else
                {
                    Alerta("Realice la búsqueda de una cuenta");
                }

            }
            catch (Exception ex)
            {
                throw ex;
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
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    //If Permiso.PGrabar = False Then
                    //Me.RadToolBar1.Items(6).Enabled = False
                    //End If
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    {
                        this.RadToolBar1.Items[5].Visible = false;
                    }
                    //If Permiso.PEliminar = False Then
                    //    Me.RadToolBar1.Items(3).Enabled = False
                    //End If
                    //If Permiso.PImprimir = False Then
                    //    Me.RadToolBar1.Items(2).Enabled = False
                    //End If

                    //Nuevo
                    this.RadToolBar1.Items[6].Visible = false;
                    //Guardar
                    //Me.RadToolBar1.Items(5).Enabled = False
                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
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

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ErrorManager();
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        // Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadToolBar1_ButtonClick");
            }
        }
    }

}