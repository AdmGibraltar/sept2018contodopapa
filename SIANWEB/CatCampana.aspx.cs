using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Globalization;

namespace SIANWEB
{
    public partial class CatCampana : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }

         private List<Producto> list_Producto
        {
            get { return (List<Producto>)Session["Sesion" + Session.SessionID + "list_producto"]; }
            set { Session["Sesion" + Session.SessionID + "list_producto"] = value; }
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

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
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
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                Nuevo();
                Inicializar();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
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
                        Nuevo();
                    }
                    else if (btn.CommandName == "undo")
                    {
                        //Regresar()
                    }
                }
                else
                {
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadMultiPage1.PageViews[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void rg1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rg1.DataSource = GetList();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_NeedDataSource");
            }
        }
        protected void rg1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Modificar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        CN__Comun.RemoverValidadores(Validators);

                        txtClave.Enabled = false;
                        HF_ID.Value = rg1.Items[item]["Id_Cam"].Text;
                        txtClave.Text = rg1.Items[item]["Id_Cam"].Text;
                        txtDescripcion.Text = rg1.Items[item]["Cam_Nombre"].Text;
                        txtUen.Text = rg1.Items[item]["Id_Uen"].Text;
                        if (cmbUen.FindItemIndexByValue(rg1.Items[item]["Id_Uen"].Text) > 0)
                        {
                            cmbUen.SelectedIndex = cmbUen.FindItemIndexByValue(rg1.Items[item]["Id_Uen"].Text);
                            cmbUen.Text = cmbUen.FindItemByValue(rg1.Items[item]["Id_Uen"].Text).Text;
                        }
                        else
                        {
                            cmbUen.SelectedIndex = 0;
                            cmbUen.Text = cmbUen.Items[0].Text;
                            txtUen.Text = "";
                        }
                       
                        CargarSeg();

                        txtSegmento.Text = rg1.Items[item]["Id_Seg"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Seg"].Text;
                        if (cmbSegmento.FindItemIndexByValue(rg1.Items[item]["Id_Seg"].Text) > 0)
                        {
                            cmbSegmento.SelectedIndex = cmbSegmento.FindItemIndexByValue(rg1.Items[item]["Id_Seg"].Text);
                            cmbSegmento.Text = cmbSegmento.FindItemByValue(rg1.Items[item]["Id_Seg"].Text).Text;
                        }
                        else
                        {
                            cmbSegmento.SelectedIndex = 0;
                            cmbSegmento.Text = cmbSegmento.Items[0].Text;
                            txtSegmento.Text = "";
                        }
                        CargarArea();                        
                        txtArea.Text = rg1.Items[item]["Id_Area"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Area"].Text;
                        if (cmbArea.FindItemIndexByValue(rg1.Items[item]["Id_Area"].Text) > 0)
                        {
                            cmbArea.SelectedIndex = cmbArea.FindItemIndexByValue(rg1.Items[item]["Id_Area"].Text);
                            cmbArea.Text = cmbArea.FindItemByValue(rg1.Items[item]["Id_Area"].Text).Text;
                        }
                        else
                        {
                            cmbArea.SelectedIndex = 0;
                            cmbArea.Text = cmbArea.Items[0].Text;
                            txtArea.Text = "";
                        }
                        CargarSolucion();

                        txtSolucion.Text = rg1.Items[item]["Id_Sol"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Sol"].Text;
                        if (cmbSolucion.FindItemIndexByValue(rg1.Items[item]["Id_Sol"].Text) > 0)
                        {
                            cmbSolucion.SelectedIndex = cmbSolucion.FindItemIndexByValue(rg1.Items[item]["Id_Sol"].Text);
                            cmbSolucion.Text = cmbSolucion.FindItemByValue(rg1.Items[item]["Id_Sol"].Text).Text;
                        }
                        else
                        {
                            cmbSolucion.SelectedIndex = 0;
                            cmbSolucion.Text = cmbSolucion.Items[0].Text;
                            txtSolucion.Text = "";
                        }
                        //CargarAplicacion();
                        txtAplicacion.Text = rg1.Items[item]["Id_Aplicacion"].Text == "-1" ? string.Empty : rg1.Items[item]["Id_Aplicacion"].Text;
                        if (cmbAplicacion.FindItemByText(rg1.Items[item]["Aplicacion"].Text).Text != null)
                        {
                            cmbAplicacion.SelectedIndex = cmbAplicacion.FindItemIndexByValue(rg1.Items[item]["Id_Aplicacion"].Text);
                            cmbAplicacion.Text = cmbAplicacion.FindItemByText(rg1.Items[item]["Aplicacion"].Text).Text;
                        }
                        else
                        {
                            cmbAplicacion.SelectedIndex = 0;
                            cmbAplicacion.Text = cmbSolucion.Items[0].Text;
                            txtAplicacion.Text = "";
                        }               


                        txtFechaInicio.DbSelectedDate = rg1.Items[item]["Cam_FechaInicio"].Text == "-1" ? string.Empty : rg1.Items[item]["Cam_FechaInicio"].Text;
                        txtFechaFin.DbSelectedDate = rg1.Items[item]["Cam_FechaFin"].Text == "-1" ? string.Empty : rg1.Items[item]["Cam_FechaFin"].Text;

                        chkActivo.Checked = Convert.ToBoolean(rg1.Items[item]["Cam_Activo"].Text);

                        list_Producto = GetListProducto(int.Parse(HF_ID.Value));
                        rgProductos.Rebind();
                       

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_ItemCommand");
            }
        }
        protected void rg1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rg1.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rg1_PageIndexChanged");
            }
        }
        
        protected void RadComboBox_DataBinding(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            comboBox.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
            CultureInfo cultura = CultureInfo.CurrentCulture;

            for (int x = 1; x < 13; x++)
            {
                comboBox.Items.Add(new RadComboBoxItem(cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(x)), x.ToString()));
            }
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && HF_ID.Value != "")
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
        private void Inicializar()
        {
            txtClave.Text = Valor;
            rg1.Rebind();
            CargarUen();            
            CargarSeg();
            CargarArea();
            CargarSolucion();
            CargarAplicacion();
            List<Producto> list_productoTemp = new List<Producto>();
            list_Producto = list_productoTemp;
        }

        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
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
        private void CargarUen() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatUen_Combo", ref cmbUen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void CargarSeg() //Central
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, cmbUen.SelectedValue == "" ? -1 : Convert.ToInt32(cmbUen.SelectedValue), Sesion.Emp_Cnx, "spCatSegmentos_Combo", ref cmbSegmento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarArea() //Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, cmbSegmento.SelectedValue == "" ? -1 : Convert.ToInt32(cmbSegmento.SelectedValue), Sesion.Emp_Cnx, "spCatAreaSegmento_Combo", ref cmbArea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarSolucion() //Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp,  cmbArea.SelectedValue == "" ? -1 : Convert.ToInt32(cmbArea.SelectedValue), Sesion.Emp_Cnx, "spCatSolucionArea_Combo", ref cmbSolucion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarAplicacion() //Local
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, cmbSolucion.SelectedValue == "" ? -1 : Convert.ToInt32(cmbSolucion.SelectedValue), Sesion.Emp_Cnx, "spCatAplicacionUEN_Combo", ref cmbAplicacion);
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
        private List<Campanas> GetList()
        {
            try
            {
                List<Campanas> List = new List<Campanas>();
                CN_CatCampanas clsCatCampanas = new CN_CatCampanas();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Campanas campanas = new Campanas();
                campanas.Id_Emp = session2.Id_Emp;
                campanas.Id_Cd = session2.Id_Cd;

                clsCatCampanas.ConsultaCampanas(campanas, session2.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Producto> GetListProducto(int idCam)
        {
            try
            {
                List<Producto> List = new List<Producto>();
                CN_CatCampanas clsCatCampanas = new CN_CatCampanas();
                Sesion session3 = new Sesion();
                session3 = (Sesion)Session["Sesion" + Session.SessionID];
                Campanas campanas = new Campanas();
                campanas.Id_Cam = idCam;

                clsCatCampanas.ConsultaCampanaProducto(campanas, session3.Emp_Cnx, ref List);
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtIdPrd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                CN_CatProducto clsProducto = new CN_CatProducto();
                Producto producto = new Producto();
                producto.Id_Prd =   Convert.ToInt32((sender as RadNumericTextBox).Value.HasValue ? (sender as RadNumericTextBox).Value : -1);
                clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, producto.Id_Prd, sesion.Id_Cd_Ver, Convert.ToInt32(combo.Value),0);

                if (producto.Prd_Descripcion != null)
                {
                    txtProducto.Text = producto.Prd_Descripcion;
                    txtProducto.Focus();
                }
                else
                {
                    txtProducto.Text = "";
                    Alerta("El Producto no existe o esta inactivo");
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }
       
        private void Nuevo()
        {

            txtClave.Text = Valor;
            txtClave.Enabled = true;
            txtDescripcion.Text = string.Empty;
            txtUen.Text = string.Empty;
            txtSegmento.Text = string.Empty;           
            txtArea.Text = string.Empty;            
            txtSolucion.Text = string.Empty;
            txtAplicacion.Text = string.Empty;
            cmbSegmento.SelectedIndex = 0;
            cmbSegmento.Text = cmbSegmento.Items[0].Text;
            cmbUen.SelectedIndex = 0;
            cmbUen.Text = cmbUen.Items[0].Text;
            cmbArea.SelectedIndex = 0;
            cmbArea.Text = cmbArea.Items[0].Text;
            cmbSolucion.SelectedIndex = 0;
            cmbSolucion.Text = cmbSolucion.Items[0].Text;
            cmbAplicacion.SelectedIndex = 0;
            cmbAplicacion.Text = cmbAplicacion.Items[0].Text;
            txtId_Prd.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;       

           
            chkActivo.Checked = true;
            HF_ID.Value = string.Empty;
            List<Producto> list_productoTemp = new List<Producto>();
            list_Producto = list_productoTemp;
            rgProductos.DataSource = list_Producto;
            rgProductos.Rebind();
          
            
           
        }
        private void Guardar()
        {
            try
            {
                CapaDatos.Funciones func = new CapaDatos.Funciones();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Campanas campanas = new Campanas();
                campanas.Id_Emp = session.Id_Emp;
                campanas.Id_Cd = session.Id_Cd;

                campanas.Id_Aplicacion = Convert.ToInt32(txtAplicacion.Text.ToString());
                campanas.Id_Uen = Convert.ToInt32(cmbUen.SelectedValue);
                campanas.Id_Seg = Convert.ToInt32(cmbSegmento.SelectedValue);
                campanas.Id_Area = Convert.ToInt32(cmbArea.SelectedValue);
                campanas.Id_Sol = Convert.ToInt32(cmbSolucion.SelectedValue);
                campanas.Aplicacion = cmbAplicacion.Text = cmbAplicacion.Text;
                

             
                   
                  
/*
                if (campanas.Id_Uen > 0)
                {

                    if (campanas.Id_Seg > 0)
                    {                        
                        RadTabStrip1.Tabs[1].Selected = true;
                        RPProductos.Selected = true;
                        Alerta("Seleccione Segmento");
                        return;
                    }


                    if (campanas.Id_Area > 0)
                    {
                        RadTabStrip1.Tabs[1].Selected = true;
                        RPProductos.Selected = true;
                        Alerta("Seleccione Area");
                        return;
                    }


                    if (campanas.Id_Sol > 0)
                    {
                        RadTabStrip1.Tabs[1].Selected = true;
                        RPProductos.Selected = true;
                        Alerta("Seleccione Solución");
                        return;
                    }
                }*/
          
                
                
                campanas.Cam_Nombre = txtDescripcion.Text;
                campanas.Cam_FechaInicio = Convert.ToDateTime(txtFechaInicio.SelectedDate.Value.ToString("dd/MM/yyyy"));
                campanas.Cam_FechaFin = Convert.ToDateTime(txtFechaFin.SelectedDate.Value.ToString("dd/MM/yyyy"));
                campanas.Cam_Activo = chkActivo.Checked;


                if (list_Producto.Count == 0)
                {
                    RadTabStrip1.Tabs[1].Selected = true;
                    RPProductos.Selected = true;
                    Alerta("Aún no se han capturado producto");
                    return;
                }
                else
                {
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                }

               // RadTabStrip1.Enabled = false;
               // RadMultiPage1.Enabled = false;


                CN_CatCampanas clsCatCampana = new CN_CatCampanas();
                int verificador = -1;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }
                    campanas.Id_Cam = Convert.ToInt32(txtClave.Text);
                    clsCatCampana.InsertarCampanas(campanas, list_Producto, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {                       
                        Nuevo();
                        Alerta("Los datos se guardaron correctamente");
                    }
                    else
                        Alerta("La clave ya existe");
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    campanas.Id_Cam= Convert.ToInt32(HF_ID.Value);
                    clsCatCampana.ModificarCampanas(campanas, list_Producto, session.Emp_Cnx, ref verificador);
                    Alerta("Los datos se modificaron correctamente");
                   
                }
                rg1.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (HF_ID.Value != "")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = Sesion.Id_Emp;
                    ct.Id_Cd = Sesion.Id_Cd_Ver;
                    ct.Id = Convert.ToInt32(HF_ID.Value);
                    ct.Tabla = "CatCamapana";
                    ct.Columna = "Id_Cam";
                    CN_Comun.Deshabilitar(ct, Sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();

                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                    return "";
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatCampana", "Id_Cam", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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

        protected void cmbUen_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                CN_CatCampanas clsCatCampanas = new CN_CatCampanas();
                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                Campanas campanas = new Campanas();
                campanas.Id_Emp = session2.Id_Emp;
                campanas.Id_Cd = session2.Id_Cd;
                campanas.Cam_Nombre = cmbAplicacion.Text;

                
                campanas.Id_Uen = int.Parse(cmbUen.SelectedValue.ToString());

                clsCatCampanas.ConsultaRuta(ref campanas, session2.Emp_Cnx);



                txtAplicacion.Text = campanas.Id_Aplicacion.ToString();

                CargarSeg();

                txtSegmento.Text = campanas.Id_Seg.ToString() == "-1" ? string.Empty : campanas.Id_Seg.ToString();
                if (cmbSegmento.FindItemIndexByValue(campanas.Id_Seg.ToString()) > 0)
                {
                    cmbSegmento.SelectedIndex = cmbSegmento.FindItemIndexByValue(campanas.Id_Seg.ToString());
                    cmbSegmento.Text = cmbSegmento.FindItemByValue(campanas.Id_Seg.ToString()).Text;
                }
                else
                {
                    cmbSegmento.SelectedIndex = 0;
                    cmbSegmento.Text = cmbSegmento.Items[0].Text;
                    txtSegmento.Text = "";
                }
                CargarArea();
                txtArea.Text = campanas.Id_Area.ToString() == "-1" ? string.Empty : campanas.Id_Area.ToString();
                if (cmbArea.FindItemIndexByValue(campanas.Id_Area.ToString()) > 0)
                {
                    cmbArea.SelectedIndex = cmbArea.FindItemIndexByValue(campanas.Id_Area.ToString());
                    cmbArea.Text = cmbArea.FindItemByValue(campanas.Id_Area.ToString()).Text;
                }
                else
                {
                    cmbArea.SelectedIndex = 0;
                    cmbArea.Text = cmbArea.Items[0].Text;
                    txtArea.Text = "";
                }
                CargarSolucion();

                txtSolucion.Text = campanas.Id_Sol.ToString() == "-1" ? string.Empty : campanas.Id_Sol.ToString();
                if (cmbSolucion.FindItemIndexByValue(campanas.Id_Sol.ToString()) > 0)
                {
                    cmbSolucion.SelectedIndex = cmbSolucion.FindItemIndexByValue(campanas.Id_Sol.ToString());
                    cmbSolucion.Text = cmbSolucion.FindItemByValue(campanas.Id_Sol.ToString()).Text;
                }
                else
                {
                    cmbSolucion.SelectedIndex = 0;
                    cmbSolucion.Text = cmbSolucion.Items[0].Text;
                    txtSolucion.Text = "";
                }
              
               
               
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbSegmento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                
                CargarArea();
                CargarSolucion();
                CargarAplicacion();
               
                txtArea.Text = string.Empty;
                txtSolucion.Text = string.Empty;
                txtAplicacion.Text = string.Empty;
                cmbArea.SelectedIndex = 0;
                cmbArea.Text = cmbArea.Items[0].Text;
                cmbSolucion.SelectedIndex = 0;
                cmbSolucion.Text = cmbSolucion.Items[0].Text;
                cmbAplicacion.SelectedIndex = 0;
                cmbAplicacion.Text = cmbAplicacion.Items[0].Text;
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void cmbArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                
                CargarSolucion();
                CargarAplicacion();

               
                txtSolucion.Text = string.Empty;
                txtAplicacion.Text = string.Empty;                
                cmbSolucion.SelectedIndex = 0;
                cmbSolucion.Text = cmbSolucion.Items[0].Text;
                cmbAplicacion.SelectedIndex = 0;
                cmbAplicacion.Text = cmbAplicacion.Items[0].Text;
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void CrearProducto(ref Producto producto)
        {
            try
            {
                producto.Id_Prd = Convert.ToInt32(txtId_Prd.Value);
                producto.Prd_Descripcion = txtProducto.Text.ToString();
                producto.Prd_Cuota = Convert.ToInt32(txtCantidad.Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void cmbSolucion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                              
                CargarAplicacion();

               
                txtAplicacion.Text = string.Empty;              
                cmbAplicacion.SelectedIndex = 0;
                cmbAplicacion.Text = cmbAplicacion.Items[0].Text;
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void Borrar_Producto(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                int columna_respuesta = rgProductos.Columns.FindByUniqueName("Id_Prd").OrderIndex;
                list_Producto.Remove(list_Producto.Where(Producto => Producto.Id_Prd.ToString() == gi.Cells[columna_respuesta].Text).ToList()[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgProductos_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        Borrar_Producto(e);
                        break;
                    case "Modificar":
                        Modificar_Producto(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgProductos_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
           
            rgProductos.DataSource = list_Producto;
        }
        protected void imgAgregarProducto_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Producto producto;
                if (txtId_Prd.Value != null)
                {
                    producto = new Producto();
                    producto.Id_Prd = int.Parse(txtId_Prd.Value.ToString());
                    if (list_Producto.Where(ProductoB => ProductoB.Id_Prd == producto.Id_Prd).ToList().Count == 0)
                    {
                        CrearProducto(ref producto);
                        list_Producto.Add(producto);
                    }
                    else
                    {
                        Alerta("Ya existe ese código de producto  " + producto.Id_Prd + " - " + txtProducto.ClientID);
                        return;
                    }
                }
               
                rgProductos.Rebind();
                LimpiarProducto();
                txtId_Prd.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void LimpiarProducto()
        {

            txtId_Prd.Value = null;
            txtProducto.Text = "";
            txtCantidad.Value = null;
            rgProductos.Rebind();
        }


        private void Modificar_Producto(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int columna_respuesta = rgProductos.Columns.FindByUniqueName("Id_Prd").OrderIndex;
            Producto p = list_Producto.Where(Producto => Producto.Id_Prd.ToString() == gi.Cells[columna_respuesta].Text).ToList()[0];

            txtId_Prd.Value = p.Id_Prd;
            txtProducto.Text = p.Prd_Descripcion;
            txtCantidad.DbValue = p.Prd_Fisico;

        }



        protected void fin_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                RadDatePicker dpFin = sender as RadDatePicker;
                RadDatePicker dpIni = (dpFin.Parent.FindControl("txtFechaFin") as RadDatePicker);

                if (dpIni.SelectedDate.HasValue && dpFin.SelectedDate.HasValue)
                {
                    if (dpIni.SelectedDate > dpFin.SelectedDate)
                    {
                        Alerta("La fecha de fin debe ser mayor a la fecha de inicio");
                        dpFin.DbSelectedDate = null;
                        return;
                    }
                    
                }
                else if (dpIni.SelectedDate.HasValue)
                {
                    dpFin.DateInput.Focus();
                }
                else
                {
                    dpIni.DateInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
    }
}
