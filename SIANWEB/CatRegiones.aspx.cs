using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using CapaNegocios;

namespace SIANWEB
{
    public partial class CatRegiones : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        static bool actualiza;

        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }

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
                    Context.Items.Add("href", pag[pag.Length - 1]);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                }
                else
                {
                    txtRegion2.Focus();
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        CargarCentros();
                        txtRegion2.Text = Valor;
                        rgRegion.Rebind();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RadAjaxManager1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                        }


                        //Session["Head" + Session.SessionID] = "Region";

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgRegion.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgTipoCosto_DataBound(object source, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editItem = (GridEditFormItem)e.Item;
                //Telerik.Web.UI.GridLinkButton
                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                {
                    (editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = true;
                    CapaEntidad.Region region = new Region();
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN_CatRegion cn_region = new CapaNegocios.CN_CatRegion();
                    cn_region.ConsultaRegionConsecutivo(ref region, session);
                    (editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = region.Id_Reg.ToString();
                }
                else
                    (editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = false;
            }

            if ((e.Item is GridDataItem) && (e.Item.IsDataBound))
            {
                GridDataItem item = (GridDataItem)e.Item;
                switch (item["Estatus"].Text)
                {
                    case "False":
                        item["Estatus"].Text = "Inactivo";
                        break;
                    case "True":
                        item["Estatus"].Text = "Activo";
                        break;
                    default:
                        break;
                }

            }
        }

        protected void rgRegion_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
        }

        protected void rgRegion_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                CapaNegocios.CN_CatRegion cn_catRegion = new CapaNegocios.CN_CatRegion();
                Region region = new Region();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                region.Id_Emp = session.Id_Emp;
                region.Id_Reg = Convert.ToInt32((editedItem["id_reg"].FindControl("RadNumericTextBox1") as RadNumericTextBox).Text);
                region.Reg_Descripcion = Convert.ToString((editedItem["Reg_Descripcion"].FindControl("RadTextBox2") as RadTextBox).Text);
                region.Reg_Activo = Convert.ToBoolean((editedItem["Reg_Activo"].Controls[0] as CheckBox).Checked);
                int verificador = 0;

                cn_catRegion.GuardarRegion(ref region, ref region, session, ref verificador, false);
                Alerta("Los datos se guardaron correctamente");

            }
            catch (Exception)
            {
                Alerta("La clave ya existe");
                e.Canceled = true;
            }
        }

        protected void rgRegion_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                CapaNegocios.CN_CatRegion cn_catRegion = new CapaNegocios.CN_CatRegion();
                Region region_nueva = new Region();
                Region region_vieja = new Region();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                region_nueva.Id_Emp = session.Id_Emp;
                region_nueva.Id_Reg = Convert.ToInt32((editedItem["id_reg"].FindControl("RadNumericTextBox1") as RadNumericTextBox).Text);
                region_nueva.Reg_Descripcion = Convert.ToString((editedItem["Reg_Descripcion"].FindControl("RadTextBox2") as RadTextBox).Text);
                region_nueva.Reg_Activo = Convert.ToBoolean((editedItem["Reg_Activo"].Controls[0] as CheckBox).Checked);
                region_vieja.Id_Emp = session.Id_Emp;
                region_vieja.Id_Reg = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id_reg"]);
                region_vieja.Reg_Descripcion = Convert.ToString((editedItem["Reg_Descripcion"].FindControl("RadTextBox2") as RadTextBox).Text);
                region_vieja.Reg_Activo = Convert.ToBoolean((editedItem["Reg_Activo"].Controls[0] as CheckBox).Checked);
                int verificador = 0;

                cn_catRegion.GuardarRegion(ref region_nueva, ref region_vieja, session, ref verificador, true);
                Alerta("Los datos se guardaron correctamente");
            }
            catch (Exception)
            {
                Alerta("La clave ya existe");
                e.Canceled = true;
            }
        }

        protected void rgRegion_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                CN__Comun.RemoverValidadores(Validators);
                actualiza = true;
                txtRegion2.Enabled = false;
                GridEditableItem editedItem = e.Item as GridEditableItem;
                hiddenActualiza.Value = (editedItem["Id_Reg"].FindControl("id_regLabel") as Label).Text;
                txtRegion2.Text = (editedItem["Id_Reg"].FindControl("id_regLabel") as Label).Text;
                txtDescripcion2.Text = (editedItem["Reg_Descripcion"].FindControl("Reg_DescripcionLabel") as Label).Text;
                CheckBox1.Checked = (editedItem["Reg_Activo"].Controls[0] as CheckBox).Checked;
            }
        }

        protected void txtRegion2_TextChanged(object sender, EventArgs e)
        {
            rgRegion.Rebind();
        }

        protected void txtDescripcion2_TextChanged(object sender, EventArgs e)
        {
            rgRegion.Rebind();
        }

        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        txtRegion2.Enabled = true;
                        txtRegion2.Focus();
                        actualiza = false;
                        limpiarControles();
                        txtRegion2.Text = Valor;
                        break;

                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta("Ha ocurrido un problema: " + ex.Message);
            }
        }

        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && hiddenActualiza.Value != "")
            {
                if (!Deshabilitar())
                {
                    Alerta("El registro está siendo utilizado por otro componente");
                    ((CheckBox)sender).Checked = true;
                }
            }
        }
        #endregion

        #region Funciones
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
                        ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = _PermisoGuardar;                  
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                         ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;
                  
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("mail")).Visible = false;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = false;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("delete")).Visible = false;
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("undo")).Visible = false;                   
                }
                else               
                    Response.Redirect("Inicio.aspx");               
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
                        return returnControl;                   
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
                if (!actualiza)
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                }
                Region region = new Region();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                region.Id_Emp = session.Id_Emp;
                region.Id_Reg = txtRegion2.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtRegion2.Text);
                region.Reg_Descripcion = txtDescripcion2.Text.Trim();
                region.Reg_Activo = CheckBox1.Checked;
                int verificador = 0;

                CapaNegocios.CN_CatRegion cn_catRegion = new CapaNegocios.CN_CatRegion();
                cn_catRegion.GuardarRegion(ref region, ref region, session, ref verificador, actualiza);
                Alerta("Los datos se " + (actualiza ? "modificaron" : "guardaron") + " correctamente");
                limpiarControles();
                txtRegion2.Text = Valor;
                rgRegion.Rebind();
                actualiza = false;
            }
            catch (Exception)
            {
                Alerta("La clave ya existe");
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, "catregion", "id_reg", sesion.Emp_Cnx, "spCatCentral_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string msgerror(Exception exception)
        {
            switch (exception.Message)
            {
                case "msg04":
                    { return "Ya existe una región con este nombre"; }

                case "msg05":
                    { return "La clave ya existe"; } //"El id seleccionado ya existe" cambiado para testing
                default:
                    { return exception.Message; }
            }
        }

        private void limpiarControles()
        {
            txtRegion2.Enabled = true;
            txtRegion2.Text = "";
            txtDescripcion2.Text = "";
            CheckBox1.Checked = true;
        }

        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (hiddenActualiza.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = 0;
                    ct.IdStr = hiddenActualiza.Value;
                    ct.IsStr = true;
                    ct.Tabla = "CatRegiones";
                    ct.Columna = "Id_Reg";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Region> GetList()
        {
            int id_region = 0;
            try
            {
                List<Region> List = new List<Region>();
                CapaNegocios.CN_CatRegion _catRegion = new CapaNegocios.CN_CatRegion();
                Region _Region = new Region();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                _catRegion.ConsultaRegion(ref _Region, id_region, txtDescripcion2.Text, session, ref List);

                return List;
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 350, 150);");
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