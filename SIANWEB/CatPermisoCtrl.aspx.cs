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
    public partial class CatPermisoCtrl : System.Web.UI.Page
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
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    
                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();
                        Inicializar();
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
                if (sesion == null)                
                {                  
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);             
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];    
                    Response.Redirect("login.aspx" , false);            
                }               
                CN__Comun comun = new CN__Comun();    
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Nuevo();
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    //regresar()
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }        
        protected void ChkDeshabilitarHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkDeshabilitar")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ChkOcultarHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkOcultar")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                this.RadGridPermisos.DataSource = dt;               
                this.RadGridPermisos.DataBind();
                ErrorManager();
                CargarPermisos();
                RadGridPermisos.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }
        #endregion
        #region Funciones
        private void Inicializar()
        {
            CargarUsuarios();
            CargarPaginas();            
        }
        private void CargarPaginas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spSysMenu_Combo", ref cmbPantalla);             
                cmbPantalla.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarUsuarios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTUsuario_Combo", ref cmbTipoU);                
                cmbTipoU.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbTipoU.Sort = RadComboBoxSort.Ascending;
                cmbTipoU.SortItems();
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
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

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
                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.rtb1.Items[5].Visible = false;
                    //Regresar
                    this.rtb1.Items[4].Visible = false;
                    //Eliminar
                    this.rtb1.Items[3].Visible = false;
                    //Imprimir
                    this.rtb1.Items[2].Visible = false;
                    //Correo
                    this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
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
        private void Nuevo()
        {
            try
            {
                DataTable dt = new DataTable();
                this.RadGridPermisos.DataSource = dt;
                this.RadGridPermisos.DataBind();
                RadGridPermisos.Visible = false;
                this.cmbTipoU.SelectedIndex = 0;
                cmbPantalla.SelectedIndex = 0;             
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
                if (!RadGridPermisos.Visible)
                {
                    Alerta("No ha asignado permisos");
                    return;
                }
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }

                CN_PermisosTU clsPermisosTU = new CN_PermisosTU();
                Int32 Verificador = default(Int32);

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];

                for (int cont = 0; cont <= this.RadGridPermisos.Items.Count - 1; cont++)
                {
                    Permiso permiso = new Permiso();
                    permiso.Id_Emp = session2.Id_Emp;
                    permiso.Id_Cd = session2.Id_Cd_Ver;
                    permiso.Id_TU = Convert.ToInt32(this.cmbTipoU.SelectedValue);
                    permiso.Sm_cve = Convert.ToInt32(this.cmbPantalla.SelectedValue);
                    permiso.Id_Ctrl = this.RadGridPermisos.Items[cont]["MenuCve"].Text;
                    permiso.PDeshabilitar = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkDeshabilitar")).Checked;
                    permiso.POcultar = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkOcultar")).Checked; 
                    clsPermisosTU.ModificarPermisosTU(permiso, session2.Emp_Cnx, ref Verificador);
                }
                Alerta("Los permisos se guardaron correctamente");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarPermisos()
        {
            try
            {
                CN_PermisosTU clsPermisosTU = new CN_PermisosTU();
                Permiso permiso = new Permiso();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                permiso.Id_TU = Convert.ToInt32(this.cmbTipoU.SelectedValue);
                permiso.Id_Emp = session2.Id_Emp;
                permiso.Id_Cd = session2.Id_Cd;
                permiso.Sm_cve = Convert.ToInt32(this.cmbPantalla.SelectedValue);
                clsPermisosTU.ConsultaPermisosCtrlTU(permiso, session2.Emp_Cnx, ref RadGridPermisos);
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