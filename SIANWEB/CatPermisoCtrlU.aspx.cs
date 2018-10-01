using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CatPermisoCtrlU : System.Web.UI.Page
    {
        #region "Variables"
        private static bool _PermisoGuardar;
        private static bool _PermisoModificar;
        private static bool _PermisoEliminar;
        private static bool _PermisoImprimir;


        #endregion
        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    Response.Redirect("Login.aspx");
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
                ErrorManager(ex, "Page_Load");
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
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
                ErrorManager(ex, "cboUsuario_SelectedIndexChanged");
            }
        }

        protected void ChkAccesarHeader_CheckedChanged(object sender, EventArgs e)
        {
            ErrorManager();
            try
            {
                bool Estatus = ((CheckBox)sender).Checked;
                foreach (GridDataItem dataItem in RadGridPermisos.MasterTableView.Items)
                {
                    ((CheckBox)dataItem.FindControl("ChkAccesar")).Checked = Estatus;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ChkAccesarHeader_CheckedChanged");
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.RadGridPermisos.DataSource = null;
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
        #region "Funciones"
        private void Inicializar()
        {
            CargarUsuarios();
            CargarPaginas();
            //RadGridPermisos.Rebind();
        }
        private void CargarPaginas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spSysMenu_Combo", ref cmbPantalla);
                //this.CmbOficina.Items.Remove(0);
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

                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, 1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUsuario_Combo", ref this.cmbUsuario);
                this.cmbUsuario.SelectedValue = "0";
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

                Session["Head" + Session.SessionID] = pagina.Descripcion;
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
                    {
                        this.rtb1.Items[6].Enabled = false;
                    }
                    if (Permiso.PGrabar == false & Permiso.PModificar == false)
                    {
                        this.rtb1.Items[5].Enabled = false;
                    }
                    //if (Permiso.PEliminar == false)
                    //{
                    //    this.RadToolBar1.Items[3].Enabled = false;
                    //}
                    //if(Permiso.PImprimir == false)
                    //{
                    //    this.RadToolBar1.Items[2].Enabled = false;
                    //}

                    //Nuevo
                    //Me.RadToolBar1.Items(6).Enabled = False
                    //Guardar
                    //Me.RadToolBar1.Items(5).Enabled = False
                    //Regresar
                    this.rtb1.Items[4].Enabled = false;
                    //Eliminar
                    this.rtb1.Items[3].Enabled = false;
                    //Imprimir
                    this.rtb1.Items[2].Enabled = false;
                    //Correo
                    this.rtb1.Items[1].Enabled = false;
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
                ValidarCtrl(Sesion, pagina.Clave);
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidarCtrl(Sesion Sesion, int sm_cve)
        {
            List<PermisoControl> list = new List<PermisoControl>();
            Permiso permiso = new Permiso();
            permiso.Id_Emp = Sesion.Id_Emp;
            permiso.Id_Cd = Sesion.Id_Cd_Ver;
            permiso.Id_U = Sesion.Id_U;
            permiso.Sm_cve = sm_cve;
            CN_PermisosU clsPermisosU = new CN_PermisosU();
            clsPermisosU.ConsultaPermisosCtrlU_Pagina(permiso, Sesion.Emp_Cnx, ref list);
            foreach (PermisoControl p in list)
            {
                switch (p.Tipo)
                {
                    case "System.Web.UI.WebControls.CheckBox":
                        CheckBox ch = (CheckBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        ch.Enabled = false;
                        break;
                    case "Telerik.Web.UI.RadTextBox":
                        RadTextBox rtb = (RadTextBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        rtb.Enabled = false;
                        break;
                    case "Telerik.Web.UI.RadNumericTextBox":
                        RadNumericTextBox rntb = (RadNumericTextBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        rntb.Enabled = false;
                        break;
                    case "Telerik.Web.UI.RadComboBox":
                        RadComboBox rcb = (RadComboBox)FindControlRecursive(divPrincipal, p.Id_Ctrl);
                        rcb.Enabled = false;
                        break;
                }
            }
        }
        private object FindControlRecursive(Control control, string id)
        {
            Control returnControl = control.FindControl(id);
            if (returnControl == null)
            {
                foreach (Control child in control.Controls)
                {
                    returnControl = child.FindControl(id);
                    if (returnControl != null && returnControl.ID == id)
                    {
                        return returnControl;
                    }
                }
            }
            return returnControl;
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Territorios> GetList()
        {
            try
            {
                List<Territorios> List = new List<Territorios>();
                CN_CatTerritorios clsCatTerritorios = new CN_CatTerritorios();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Territorios territorio = new Territorios();
                territorio.Id_Emp = session2.Id_Emp;
                territorio.Id_Cd = session2.Id_Cd_Ver;
                clsCatTerritorios.ConsultaTerritorios(territorio, session2.Emp_Cnx, ref List);
                return List;
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
                this.RadGridPermisos.DataSource = null;
                this.RadGridPermisos.DataBind();

                // cargarComboOficina()
                //Me.CboOficina.SelectedValue = 0
                this.cmbUsuario.SelectedValue = "0";
                //LimpiarChecks()
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

                CN_PermisosU clsPermisosTU = new CN_PermisosU();
                Int32 Verificador = default(Int32);
                //bool PAccesar = false;
                //bool PGrabar = false;
                //bool PModificar = false;
                //bool PEliminar = false;
                //bool PImprimir = false;

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];


                for (int cont = 0; cont <= this.RadGridPermisos.Items.Count - 1; cont++)
                {
                    Permiso permiso = new Permiso();
                    permiso.Id_Emp = session2.Id_Emp;
                    permiso.Id_Cd = session2.Id_Cd_Ver;
                    permiso.Id_U = Convert.ToInt32(this.cmbUsuario.SelectedValue);
                    permiso.Sm_cve = Convert.ToInt32(this.cmbPantalla.SelectedValue);
                    permiso.Id_Ctrl = this.RadGridPermisos.Items[cont]["MenuCve"].Text;
                    permiso.PAccesar = ((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkAccesar")).Checked;

                    //if (((CheckBox)this.RadGridPermisos.Items[cont].FindControl("ChkAccesar")).Checked != Convert.ToBoolean(this.RadGridPermisos.Items[cont]["SpTu_PAccesar"].Text))
                    //{
                    //    PAccesar = true;
                    //}
                    //permiso.PAccesar = PAccesar;

                    clsPermisosTU.ModificarPermisosU(permiso, session2.Emp_Cnx, ref Verificador);


                }

                Alerta("Los cambios se guardaron correctamente");

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
                CN_PermisosU clsPermisosTU = new CN_PermisosU();
                Permiso permiso = new Permiso();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                permiso.Id_U = Convert.ToInt32(this.cmbUsuario.SelectedValue);
                permiso.Id_Emp = session2.Id_Emp;
                permiso.Id_Cd = session2.Id_Cd;
                permiso.Sm_cve = Convert.ToInt32(this.cmbPantalla.SelectedValue);
                clsPermisosTU.ConsultaPermisosCtrlU(permiso, session2.Emp_Cnx, ref RadGridPermisos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region "ErrorManager"
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