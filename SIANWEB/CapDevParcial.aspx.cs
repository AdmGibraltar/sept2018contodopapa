using System;
using System.Collections.Generic;
using System.Web.UI;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class CapDevParcial : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private string edit;
        public string FacturaEnable
        {
            get
            {
                if (cmbFactura.Visible)
                    return "1";// txtFecha.Enabled;
                else
                    return "0";
            }
        }
        //
        #endregion
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    edit = Request.QueryString["editar"].ToString();
                    if (edit == "0" && !txtFactura.Value.HasValue)
                    {
                        CargarFacturas();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    RAM1.ResponseScripts.Add("RefreshParentPage()");
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
                        Inicializar();

                        if (!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(divPrincipal.Controls);
                        }
                        double ancho = 0;
                        foreach (GridColumn gc in rgDevParcial.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDevParcial.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDevParcial.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
                        break;
                }
                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }
                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }
        protected void rgDevParcial_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    int valor = Convert.ToInt32(item["Cantidad1"].Text); // Convert.ToInt32((item.FindControl("Cantidad1").FindControl("lblCantidad") as Label).Text);
                    (item.FindControl("NumCantDevuelta") as RadNumericTextBox).MaxValue = valor;

                    if (!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                    {
                        (item.FindControl("NumCantDevuelta") as RadNumericTextBox).Enabled = false;
                        (item.FindControl("ckDevuelto") as CheckBox).Enabled = false;
                    }
                }
                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    RadNumericTextBox dataField = (RadNumericTextBox)form["CantDevuelta"].FindControl("NumCantDevuelta");
                    if (!dataField.Enabled)
                    {
                        // dataField = (RadNumericTextBox)form["Acys_Cantidad"].FindControl("txtCantidad");
                    }
                    else
                        dataField.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDevParcial_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgDevParcial.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDevParcial_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridItem item = e.Item;
                List<string> statusPosibles = new List<string>();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDevParcial_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbFactura_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int factura = -1;
                Int32.TryParse(e.Value, out factura);
                CargarDatosdeFactura(factura, 0);
                rgDevParcial.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "new":
                        LimpiarDatos();
                        cmbFactura.SelectedIndex = 0;
                        cmbFactura.Text = cmbFactura.Items[0].Text;
                        txtFactura.Value = null;
                        GetList();
                        rgDevParcial.Rebind();
                        break;
                    case "save":
                        Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (this.RadPageViewDetalles.Selected)
            {
                if (string.IsNullOrEmpty(txtFactura.Text))
                {
                    if (_PermisoGuardar && _PermisoModificar)
                    {
                        if (cmbFactura.Visible)
                        {
                            Alerta("Seleccione una factura para ver sus detalles");
                            RadPageViewDGenerales.Selected = true;
                            RadTabStrip1.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtClienteNombre.Text = NombreCliente();
                this.ValidarHabilitacionGrid();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rdFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                if ((txtFecha.SelectedDate >= session.CalendarioIni) && (txtFecha.SelectedDate <= session.CalendarioFin))
                {
                }
                else
                {
                    Alerta("Fecha se encuentra fuera del periodo");
                    txtFecha.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void ValidarHabilitacionGrid()
        {
            this.rgDevParcial.Enabled = false;
            if (cmbTipoMovimento.SelectedValue != "-1" && !string.IsNullOrEmpty(txtCliente.Text) && cmbTerritorio.SelectedValue != "-1")
                this.rgDevParcial.Enabled = true;
        }
        protected void txtClave_TextChanged(object sender, EventArgs e)
        {
            if (txtFactura.Text == "")
            {
                return;
            }

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DevParcial_DetalleFactura detalle = new DevParcial_DetalleFactura();
            CN_DevParcialDetalle clsDevParcial = new CN_DevParcialDetalle();
            int factura = txtFactura.Value.HasValue ? Convert.ToInt32(txtFactura.Value.Value) : -1;
            clsDevParcial.ConsultaFacturas(Sesion, factura, ref detalle);

            if (detalle.Descripcion1 != null)
            {
                cmbFactura.Text = detalle.Descripcion1;
                CargarDatosdeFactura(factura, 0);
                rgDevParcial.Rebind();
            }
            else
            {
                txtFactura.Text = "";
                Alerta("La factura no existe o no esta en estatus válido para realizar la devolución");
                LimpiarDatos();
            }
        }
        protected void cmbCliente_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.CargarFacturas();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ckDevuelto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if ((sender as CheckBox).Checked)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    int i = Convert.ToInt32(((sender as CheckBox).Parent.FindControl("Label1") as Label).Text) - 1;
                    int territorio = Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["Id_Ter"].Text));
                    int prd = Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["Id_Prod"].Text));
                    string agrupador = rgDevParcial.MasterTableView.Items[i]["Prd_Agrupador"].Text;
                    int Cantidad_A = Convert.ToInt32(rgDevParcial.MasterTableView.Items[i]["CantDevueltaOld"].Text);
                    int fac = Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["Id_Fac"].Text));
                    int cantidad = 0;
                    if (agrupador != "-1")
                    {
                        for (int j = 0; j < rgDevParcial.MasterTableView.Items.Count; j++)
                        {
                            if (agrupador == rgDevParcial.MasterTableView.Items[j]["Prd_Agrupador"].Text && (rgDevParcial.MasterTableView.Items[j].FindControl("ckDevuelto") as CheckBox).Checked)
                                if (prd == Convert.ToInt32(rgDevParcial.MasterTableView.Items[j]["Id_Prod"].Text))
                                    cantidad += (rgDevParcial.MasterTableView.Items[j].FindControl("NumCantDevuelta") as RadNumericTextBox).Value.HasValue ? (int)(rgDevParcial.MasterTableView.Items[j].FindControl("NumCantDevuelta") as RadNumericTextBox).Value.Value : 0;
                        }
                    }
                    int cantidadFac = 0;
                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    new CN_CapFactura().ConsultarCantidadProdFactura(sesion, prd, fac, territorio, ref cantidadFac);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void NumCantDevuelta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (((sender as RadNumericTextBox).Parent.FindControl("ckDevuelto") as CheckBox).Checked)
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    int i = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("Label1") as Label).Text) - 1;
                    int territorio = Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["Id_Ter"].Text));
                    int prd = Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["Id_Prod"].Text));
                    int fac = Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["Id_Fac"].Text));
                    string agrupador = rgDevParcial.MasterTableView.Items[i]["Prd_Agrupador"].Text;
                    int Cantidad_A = Convert.ToInt32(rgDevParcial.MasterTableView.Items[i]["CantDevueltaOld"].Text);
                    int cantidad = 0;
                    if (agrupador != "-1")
                    {
                        for (int j = 0; j < rgDevParcial.MasterTableView.Items.Count; j++)
                        {
                            if (agrupador == rgDevParcial.MasterTableView.Items[j]["Prd_Agrupador"].Text && (rgDevParcial.MasterTableView.Items[j].FindControl("ckDevuelto") as CheckBox).Checked)
                                if (prd == Convert.ToInt32(rgDevParcial.MasterTableView.Items[j]["Id_Prod"].Text))
                                    cantidad += (rgDevParcial.MasterTableView.Items[j].FindControl("NumCantDevuelta") as RadNumericTextBox).Value.HasValue ? (int)(rgDevParcial.MasterTableView.Items[j].FindControl("NumCantDevuelta") as RadNumericTextBox).Value.Value : 0;
                        }
                    }
                    int verificador = 0;
                    //int cantidadFac = 0;
                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    //new CN_CapFactura().ConsultarCantidadProdFactura(sesion, prd, fac, territorio, ref cantidadFac);
                    //if (cantidadFac > 0)
                    //{
                    //    if (cantidadFac >= cantidad)
                    //    {
                    //        CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), territorio.ToString(), txtCliente.Text, sesion.Emp_Cnx, ref verificador, "14");
                    //        if (verificador + Cantidad_A - cantidad < 0)
                    //        {
                    //            AlertaFocus("El producto " + prd.ToString() + " no cuenta con saldo suficiente", (sender as RadNumericTextBox).ClientID);
                    //            (sender as RadNumericTextBox).Value = 0;
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        AlertaFocus("La cantidad del producto " + prd.ToString() + " en la factura es menor que la cantidad que desea devolver", (sender as RadNumericTextBox).ClientID);
                    //        return;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 180);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadPageViewDetalles_Load(object sender, EventArgs e)
        {
            object a = RadPageViewDetalles.Width;
        }
        #endregion
        #region Funciones
        private string NombreCliente()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string idCliente = txtCliente.Value.HasValue ? txtCliente.Value.Value.ToString() : "-1";
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(idCliente);
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                return cliente.Cte_NomComercial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetList()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Factura = 0;
                List<DevParcial_DetalleFactura> List2 = new List<DevParcial_DetalleFactura>();
                string facStr = txtFactura.Text;
                if (!string.IsNullOrEmpty(facStr))
                    Int32.TryParse(facStr, out Factura);
                if (Factura > 0)
                {
                    CN_DevParcialDetalle clsDevParcial = new CN_DevParcialDetalle();

                    rgDevParcial.DataSource = clsDevParcial.ConsultaDetalleFactura(sesion, Factura, 0);
                }
                else
                    rgDevParcial.DataSource = List2;
                //Totales();
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
                // this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                //if (Permiso.PAccesar == true)
                //{
                bool valor = false;
                Boolean.TryParse(Page.Request.QueryString["permisoGuardar"], out valor);// == 1 ? true : false;
                _PermisoGuardar = valor;
                valor = false;
                Boolean.TryParse(Page.Request.QueryString["permisoModificar"], out valor);// == 1 ? true : false;
                _PermisoModificar = valor;
                valor = false;
                Boolean.TryParse(Page.Request.QueryString["permisoEliminar"], out valor);// == 1 ? true : false;
                _PermisoEliminar = valor;
                valor = false;
                Boolean.TryParse(Page.Request.QueryString["permisoImprimir"], out valor);// == 1 ? true : false;
                _PermisoImprimir = valor;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                {
                    this.RadToolBar1.Items[0].Visible = false;
                    this.RadToolBar1.Items[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            try
            {
                edit = Request.QueryString["editar"].ToString();
                CargarTipo();
                CargarComboTipoMovimientos();
                CargarTerritorio();
                CargarRepresentante();
                CargarDatos();
                if (!string.IsNullOrEmpty(txtFactura.Text))
                    if (txtFactura.Text == "-1")
                        txtFactura.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void baja(ref GridCommandEventArgs e, ref List<string> statusPosibles, ref GridItem item)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fecha = DateTime.Now;
            //validar fecha dentro del periodo
            if (!(fecha >= Sesion.CalendarioIni && fecha <= Sesion.CalendarioFin))
            {
                if (!(txtFecha.SelectedDate >= Sesion.CalendarioIni && txtFecha.SelectedDate <= Sesion.CalendarioFin))
                {
                    Alerta("La fecha se encuentra fuera del período");
                    e.Canceled = true;
                    return;
                }
            }
            DevParcial_Detalle devparcial = new DevParcial_Detalle();
            devparcial.TipoMovimiento = Convert.ToInt32(txtTipoMov.Text);
            devparcial.Cliente1 = Convert.ToInt32(txtCliente.Text);
            devparcial.Territorio = Convert.ToInt32(txtTerritorio.Text);
            devparcial.Representante = Convert.ToInt32(txtRepresentante.Text);
            devparcial.Factura = Convert.ToInt32(rgDevParcial.MasterTableView.Items[e.Item.ItemIndex]["Id_Fac"].Text);
            devparcial.Notas = txtNotas.Text;
            devparcial.Descuento = Convert.ToDouble(txtDescuento.Text);
            devparcial.Descuento2 = Convert.ToDouble(txtDescuento2.Text);
            devparcial.Nota = Convert.ToInt32(txtNota.Text);
            if (devparcial.Nota == 0)
                devparcial.Nota = Convert.ToInt32(MaximoIdNota());
            devparcial.Fecha_Fac = txtFechafac.SelectedDate.Value;
            devparcial.Fecha_dev = txtFecha.SelectedDate.Value;
            devparcial.Importe = Convert.ToDouble(txtImporte.Text);
            devparcial.Subtotal = Convert.ToDouble(txtSubtotal.Text);
            devparcial.Iva = Convert.ToDouble(txtIVA.Text);
            devparcial.Total = Convert.ToDouble(txtTotal.Text);
            devparcial.Numero = Convert.ToInt32(txtNumero.Text);
            devparcial.TipoDev = Convert.ToInt32(cmbTipo.SelectedValue);
            devparcial.Desc = txtDesc.Text;
            devparcial.Desc2 = txtDesc2.Text;
            devparcial.IdProd = Convert.ToInt32(rgDevParcial.MasterTableView.Items[e.Item.ItemIndex]["Id_Prod"].Text);
            devparcial.Dev_Cant = Convert.ToInt32(rgDevParcial.MasterTableView.Items[e.Item.ItemIndex]["Cantidad1"].Text);
            devparcial.Dev_Precio = Convert.ToDouble(rgDevParcial.MasterTableView.Items[e.Item.ItemIndex]["Precio1"].Text);
            devparcial.Dev_Importe = Convert.ToDouble(rgDevParcial.MasterTableView.Items[e.Item.ItemIndex]["Importe1"].Text);
            //int verificador = 0;
            CN_DevParcialDetalle clsDevParcial = new CN_DevParcialDetalle();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        }
        private void CargarTipo()
        {
            cmbTipo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbTipo.Items.Insert(0, new RadComboBoxItem("Entradas de almacen", "0"));
            cmbTipo.Items.Insert(0, new RadComboBoxItem("Salidas de almacen", "1"));
            cmbTipo.Items.Insert(0, new RadComboBoxItem("Facturas", "2"));
            cmbTipo.Items.Insert(0, new RadComboBoxItem("Remisiones", "3"));
            cmbTipo.Items.Insert(0, new RadComboBoxItem("Devoluciones parciales", "4"));
            this.cmbTipo.SelectedValue = "4";
            this.cmbTipo.Text = (this.cmbTipo.FindItemByValue("4")).Text;
            cmbTipo.Enabled = false;
        }
        private void CargarTipoMovimiento(int tipo_movimiento)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, tipo_movimiento, 0, Convert.ToInt32(cmbTipo.SelectedValue), Sesion.Emp_Cnx, "spCatMovimientoFiltro_Combo", ref cmbTipoMovimento);
                cmbTipoMovimento.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoMovimientos()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(sesion.Id_Emp, 0, 0, Convert.ToInt32(cmbTipo.SelectedValue), sesion.Emp_Cnx, "spCatMovimientoFiltro_Combo", ref cmbTipoMovimento);
        }
        private void CargarRepresentante()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_TU == 2 ? Sesion.Id_U : (int?)null, Sesion.Emp_Cnx, "spCatRik_Combo", ref cmbRepresentante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTerritorio()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                new CapaNegocios.CN__Comun().LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatTerritorio_Combo", ref cmbTerritorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarFacturas()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                if (cmbFactura.Items.Count == 0)
                    CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, (txtFactura.Value.HasValue ? Convert.ToInt32(txtFactura.Value.Value) : -1), Sesion.Emp_Cnx, "spCatFactura_Combo", ref cmbFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarDatos()
        {
            try
            {
                txtFecha.SelectedDate = DateTime.Now;
                if (Request.QueryString["id"] != null)
                {
                    string IdStr = Request.QueryString["id"].ToString();
                    int Id = 0;
                    Int32.TryParse(IdStr, out Id);
                    txtNumero.Text = Id.ToString();
                    txtNota.Text = Id.ToString();
                    if (Id == 0)
                    {
                        txtNumero.Text = MaximoId();
                        txtNumero.Enabled = false;
                    }
                    if (Request.QueryString["fac"] != null)
                    {
                        string FacStr = Request.QueryString["fac"].ToString();
                        int Fac = -1;
                        Int32.TryParse(FacStr, out Fac);
                        if (Fac == 0)
                            Fac = -1;
                        if (Fac != -1)
                            txtFactura.Text = Fac.ToString();
                        if (edit == "0")
                        {
                            cmbFactura.SelectedIndex = cmbFactura.FindItemIndexByValue(Fac.ToString());
                            if (cmbFactura.SelectedIndex > 0)
                            {
                                cmbFactura.Text = cmbFactura.FindItemByValue(Fac.ToString()).Text;
                                if (!_PermisoGuardar && !_PermisoModificar)
                                {
                                    txtFactura.Text = Fac.ToString();
                                    txtFactura.Enabled = false;
                                    cmbFactura.Enabled = false;
                                    txtFecha.Enabled = false;
                                }
                            }
                            else
                            {
                                if (!_PermisoGuardar && !_PermisoModificar)
                                {
                                    cmbFactura.Visible = false;
                                    txtFactura2.Visible = true;
                                    txtFactura.Text = Fac.ToString();
                                    txtFactura.Enabled = false;
                                    cmbFactura.Enabled = false;
                                    txtFecha.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            cmbFactura.Visible = false;
                            txtFactura2.Visible = true;
                            txtFactura.Enabled = false;
                            if (!_PermisoGuardar && !_PermisoModificar)
                            {
                                txtFactura.Text = Fac.ToString();
                                txtFactura.Enabled = false;
                                cmbFactura.Enabled = false;
                                txtFecha.Enabled = false;
                            }
                        }
                        if (Fac > 0)
                            CargarDatosdeFactura(Fac, Id);
                    }
                }
                else
                {
                    txtNumero.Text = MaximoId();
                    txtFactura.Focus();
                }
                Deshabilitar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Deshabilitar()
        {
            txtNumero.Enabled = false;
            txtTipoMov.Enabled = false;
            cmbTipoMovimento.Enabled = false;
            txtCliente.Enabled = false;
            txtTerritorio.Enabled = false;
            cmbTerritorio.Enabled = false;
            txtRepresentante.Enabled = false;
            cmbRepresentante.Enabled = false;
            txtDescuento.Enabled = false;
            txtDescuento2.Enabled = false;
            txtDesc.Enabled = false;
            txtDesc2.Enabled = false;
            txtNotas.Enabled = false;
            txtNota.Enabled = false;
            txtFechafac.Enabled = false;
            txtImporte.Enabled = false;
            txtSubtotal.Enabled = false;
            txtIVA.Enabled = false;
            txtTotal.Enabled = false;
        }
        private void CargarDatosdeFactura(int Factura, int id)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<DevParcial_Detalle> List = new List<DevParcial_Detalle>();

                if (Factura > 0)
                {
                    CN_DevParcialDetalle clsDevParcial = new CN_DevParcialDetalle();
                    clsDevParcial.ConsultaDevParcialDetalleFactura(sesion, Factura, id, ref List);
                    CargarFacturasPantalla(List);
                    rgDevParcial.DataSource = clsDevParcial.ConsultaDetalleFactura(sesion, Factura, id);
                }
                else
                {
                    LimpiarDatos();
                    txtNumero.Text = MaximoId();
                    rgDevParcial.DataSource = List;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarFacturasPantalla(List<DevParcial_Detalle> devParcialDetalle)
        {
            try
            {
                LimpiarDatos();
                if (string.IsNullOrEmpty(txtNumero.Text))
                    if (devParcialDetalle[0].Numero == 0)
                        txtNumero.Text = MaximoId();
                    else
                        txtNumero.Text = devParcialDetalle[0].Numero.ToString();

                txtFactura.Text = devParcialDetalle[0].Factura.ToString();
                txtFactura2.Text = devParcialDetalle[0].Fac_Total2.ToString();
                txtTipoMov.Text = devParcialDetalle[0].TipoMovimiento.ToString();
                cmbTipoMovimento.SelectedIndex = cmbTipoMovimento.FindItemIndexByValue(devParcialDetalle[0].TipoMovimiento.ToString());
                cmbTipoMovimento.Text = cmbTipoMovimento.FindItemByValue(devParcialDetalle[0].TipoMovimiento.ToString()).Text;
                txtCliente.Text = devParcialDetalle[0].Cliente1.ToString();

                txtClienteNombre.Text = NombreCliente();
                txtTerritorio.Text = devParcialDetalle[0].Territorio.ToString();
                cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(devParcialDetalle[0].Territorio.ToString());
                cmbTerritorio.Text = cmbTerritorio.FindItemByValue(devParcialDetalle[0].Territorio.ToString()).Text;
                txtRepresentante.Text = devParcialDetalle[0].Representante.ToString();
                cmbRepresentante.SelectedIndex = cmbRepresentante.FindItemIndexByValue(devParcialDetalle[0].Representante.ToString());
                cmbRepresentante.Text = cmbRepresentante.FindItemByValue(devParcialDetalle[0].Representante.ToString()).Text;

                txtDescuento.Text = devParcialDetalle[0].Descuento.ToString("##0.00");
                txtDescuento2.Text = devParcialDetalle[0].Descuento2.ToString("##0.00");
                txtDesc.Text = devParcialDetalle[0].Desc;
                txtDesc2.Text = devParcialDetalle[0].Desc2;

                txtNotas.Text = devParcialDetalle[0].Notas;
                txtNota.Text = devParcialDetalle[0].Nota.ToString();
                if (FacturaEnable != "1")
                {
                    txtFecha.SelectedDate = devParcialDetalle[0].Fecha_dev;
                }
                txtFechafac.SelectedDate = devParcialDetalle[0].Fecha_Fac;
                txtImporte.Text = devParcialDetalle[0].Importe.ToString("##0.00");
                txtSubtotal.Text = devParcialDetalle[0].Subtotal.ToString("##0.00");
                txtIVA.Text = devParcialDetalle[0].Iva.ToString("##0.00");
                txtTotal.Text = devParcialDetalle[0].Total.ToString("##0.00");
                txtHora.Text = devParcialDetalle[0].Fecha_devHr.ToString("H:mm:ss");

                Deshabilitar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarDatos()
        {
            txtTipoMov.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtClienteNombre.Text = string.Empty;
            txtTerritorio.Text = string.Empty;
            txtRepresentante.Text = string.Empty;
            txtNotas.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtDesc2.Text = string.Empty;
            cmbTipoMovimento.ClearSelection();
            cmbTerritorio.ClearSelection();
            cmbRepresentante.ClearSelection();
            txtNota.Text = "0";
            txtFechafac.Clear();
            txtDescuento.Text = "0.00";
            txtDescuento2.Text = "0.00";
            txtImporte.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtIVA.Text = "0.00";
            txtTotal.Text = "0.00";
        }
        private void Totales()
        {
            double acumulado = 0;
            double importe = 0;
            double iva_cd = 0;
            double iva = 0;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            new CN_CatCentroDistribucion().ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva_cd, sesion.Emp_Cnx);

            double descuento1 = !string.IsNullOrEmpty(txtDescuento.Value.ToString()) ? Convert.ToDouble(txtDescuento.Value.Value) : 0;
            double descuento2 = !string.IsNullOrEmpty(txtDescuento2.Value.ToString()) ? Convert.ToDouble(txtDescuento2.Value.Value) : 0;

            for (int i = 0; i < this.rgDevParcial.Items.Count; i++)
            {
                bool devuelto = (rgDevParcial.MasterTableView.Items[i]["Devuelto"].FindControl("ckDevuelto") as CheckBox).Checked;
                if (devuelto)
                {
                    int cant = Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["CantDevuelta"].FindControl("NumCantDevuelta") as RadNumericTextBox).Text);
                    string label = (rgDevParcial.MasterTableView.Items[i]["Precio1"].FindControl("lblPrecio") as Label).Text;
                    double precio = !string.IsNullOrEmpty(label) ? Convert.ToDouble(label) : 0;
                    double cantidad = cant * precio;
                    importe += cantidad;

                    if (descuento1 != 0)
                    {//si tiene descuento 1
                        cantidad = cantidad - (cantidad * (descuento1 / 100));
                    }
                    if (descuento2 != 0)
                    {//si tiene descuento 2
                        cantidad = cantidad - (cantidad * (descuento2 / 100));
                    }

                    iva += (cantidad * (iva_cd / 100));
                    acumulado += cantidad;
                }
            }

            txtImporte.Text = importe.ToString();
            txtSubtotal.Text = acumulado.ToString();
            txtIVA.Text = iva.ToString();
            txtTotal.Text = (acumulado + iva).ToString();
        }
        private void Guardar()
        {
            if (Request.QueryString["fac"] != "-1")
            {// EDICION
                if (!_PermisoModificar)
                {
                    Alerta("No tiene permisos para modificar");
                    return;
                }
            }
            else
            {//NUEVO
                if (!_PermisoGuardar)
                {
                    Alerta("No tiene permisos para grabar");
                    return;
                }
            }

            int error = 0;
            for (int i = 0; i < this.rgDevParcial.Items.Count; i++)
            {
                int Dev_Cant = Convert.ToInt32(rgDevParcial.MasterTableView.Items[i]["Cantidad1"].Text);
                int CantDevuelta = !string.IsNullOrEmpty((rgDevParcial.MasterTableView.Items[i]["CantDevuelta"].FindControl("NumCantDevuelta") as RadNumericTextBox).Text) ? Convert.ToInt32((rgDevParcial.MasterTableView.Items[i]["CantDevuelta"].FindControl("NumCantDevuelta") as RadNumericTextBox).Text) : 0;
                if (Dev_Cant < CantDevuelta)
                    error = 1;
            }
            if (error == 1)
            {
                Alerta("Revise que las cantidades devueltas no sean mayores a las cantidades de la factura");
                return;
            }

            error = 0;
            for (int j = 0; j < this.rgDevParcial.Items.Count; j++)
            {
                bool Devuelto = (rgDevParcial.MasterTableView.Items[j]["Devuelto"].FindControl("ckDevuelto") as CheckBox).Checked;
                if (Devuelto)
                    error = 1;
            }
            if (error == 0)
            {
                Alerta("Seleccione por lo menos un registro para la devolución");
                return;
            }

            error = 0;
            for (int k = 0; k < this.rgDevParcial.Items.Count; k++)
            {
                bool Devuelto = (rgDevParcial.MasterTableView.Items[k]["Devuelto"].FindControl("ckDevuelto") as CheckBox).Checked;
                int CantDevuelta = !string.IsNullOrEmpty((rgDevParcial.MasterTableView.Items[k]["CantDevuelta"].FindControl("NumCantDevuelta") as RadNumericTextBox).Text) ? Convert.ToInt32((rgDevParcial.MasterTableView.Items[k]["CantDevuelta"].FindControl("NumCantDevuelta") as RadNumericTextBox).Text) : 0;
                if (Devuelto && CantDevuelta == 0)
                    error = 1;
            }
            if (error == 1)
            {
                Alerta("Ingrese una cantidad mayor a cero en el registro que se va a devolver");
                return;
            }

            DateTime fecha = DateTime.Now;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //validar fecha dentro del periodo
            if (!(fecha >= sesion.CalendarioIni && fecha <= sesion.CalendarioFin))
                if (!(txtFecha.SelectedDate >= sesion.CalendarioIni && txtFecha.SelectedDate <= sesion.CalendarioFin))
                {
                    Alerta("La fecha se encuentra fuera del período");
                    return;
                }
            this.Totales();

            DevParcial_Detalle devparcial = new DevParcial_Detalle();
            devparcial.TipoMovimiento = txtTipoMov.Value.HasValue ? Convert.ToInt32(txtTipoMov.Value.Value) : 0;
            devparcial.Cliente1 = txtCliente.Value.HasValue ? Convert.ToInt32(txtCliente.Value.Value) : 0;
            devparcial.Territorio = txtTerritorio.Value.HasValue ? Convert.ToInt32(txtTerritorio.Value.Value) : 0;
            devparcial.Representante = txtRepresentante.Value.HasValue ? Convert.ToInt32(txtRepresentante.Value.Value) : 0;
            devparcial.Factura = txtFactura.Value.HasValue ? Convert.ToInt32(txtFactura.Value.Value) : 0;
            devparcial.Notas = txtNotas.Text;
            devparcial.Descuento = txtDescuento.Value.HasValue ? Convert.ToDouble(txtDescuento.Value.Value) : 0;
            devparcial.Descuento2 = txtDescuento2.Value.HasValue ? Convert.ToDouble(txtDescuento2.Value.Value) : 0;
            devparcial.Nota = txtNota.Value.HasValue ? Convert.ToInt32(txtNota.Value.Value) : 0;
            devparcial.Fecha_Fac = txtFechafac.SelectedDate.Value;
            devparcial.Fecha_dev = txtFecha.SelectedDate.Value;
            devparcial.Importe = txtImporte.Value.HasValue ? Convert.ToDouble(txtImporte.Value.Value) : 0;
            devparcial.Subtotal = txtSubtotal.Value.HasValue ? Convert.ToDouble(txtSubtotal.Value.Value) : 0;
            devparcial.Iva = txtIVA.Value.HasValue ? Convert.ToDouble(txtIVA.Value.Value) : 0;
            devparcial.Total = txtTotal.Value.HasValue ? Convert.ToDouble(txtTotal.Value.Value) : 0;
            devparcial.Numero = txtNumero.Value.HasValue ? Convert.ToInt32(txtNumero.Value.Value) : 0;
            devparcial.TipoDev = !string.IsNullOrEmpty(cmbTipo.SelectedValue) ? Convert.ToInt32(cmbTipo.SelectedValue) : 0;
            devparcial.Desc = txtDesc.Text;
            devparcial.Desc2 = txtDesc2.Text;
            int verificador = 0;

            List<DevParcial_Detalle> devparcialList = new List<DevParcial_Detalle>();
            guardarGrid(ref devparcialList);
            CN_DevParcialDetalle clsDevParcial = new CN_DevParcialDetalle();
            clsDevParcial.InsertarDevParcial(sesion, devparcial, devparcialList, ref verificador);
            if (verificador >= 1)
            {//valido            
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Devolución grabada correctamente", "')"));
            }
            else//error
                Alerta("No se pudo agregar la devolución, Revise sus datos nuevamente");
        }
        private void guardarGrid(ref List<DevParcial_Detalle> devparcialList)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DevParcial_Detalle devparcial = new DevParcial_Detalle();

            for (int i = 0; i < this.rgDevParcial.Items.Count; i++)
            {
                devparcial = new DevParcial_Detalle();
                devparcial.TipoMovimiento = txtTipoMov.Value.HasValue ? Convert.ToInt32(txtTipoMov.Value.Value) : 0;
                devparcial.Cliente1 = txtCliente.Value.HasValue ? Convert.ToInt32(txtCliente.Value.Value) : 0;
                devparcial.Territorio = txtTerritorio.Value.HasValue ? Convert.ToInt32(txtTerritorio.Value.Value) : 0;
                devparcial.Representante = txtRepresentante.Value.HasValue ? Convert.ToInt32(txtRepresentante.Value.Value) : 0;
                devparcial.Factura = !string.IsNullOrEmpty(rgDevParcial.MasterTableView.Items[i]["Id_Fac"].Text) ? Convert.ToInt32(rgDevParcial.MasterTableView.Items[i]["Id_Fac"].Text) : 0;
                devparcial.Notas = txtNotas.Text;
                devparcial.Descuento = txtDescuento.Value.HasValue ? txtDescuento.Value.Value : 0;
                devparcial.Descuento2 = txtDescuento2.Value.HasValue ? txtDescuento2.Value.Value : 0;
                devparcial.Fecha_Fac = txtFechafac.SelectedDate.Value;
                devparcial.Fecha_dev = txtFecha.SelectedDate.Value;
                devparcial.Importe = txtImporte.Value.HasValue ? txtImporte.Value.Value : 0;
                devparcial.Subtotal = txtSubtotal.Value.HasValue ? txtSubtotal.Value.Value : 0;
                devparcial.Iva = txtIVA.Value.HasValue ? txtIVA.Value.Value : 0;
                devparcial.Total = txtTotal.Value.HasValue ? txtTotal.Value.Value : 0;
                devparcial.Numero = txtNumero.Value.HasValue ? Convert.ToInt32(txtNumero.Value.Value) : 0;
                devparcial.TipoDev = !string.IsNullOrEmpty(cmbTipo.SelectedValue) ? Convert.ToInt32(cmbTipo.SelectedValue) : 0;
                devparcial.Desc = txtDesc.Text;
                devparcial.Desc2 = txtDesc2.Text;
                devparcial.IdProd = !string.IsNullOrEmpty(rgDevParcial.MasterTableView.Items[i]["Id_Prod"].Text) ? Convert.ToInt32(rgDevParcial.MasterTableView.Items[i]["Id_Prod"].Text) : 0;
                string cantidad = (rgDevParcial.MasterTableView.Items[i]["CantDevuelta"].FindControl("NumCantDevuelta") as RadNumericTextBox).Text;
                devparcial.CantDevuelta = !string.IsNullOrEmpty(cantidad) ? Convert.ToInt32(cantidad) : 0;
                devparcial.Dev_Cant = devparcial.CantDevuelta;
                string label = (rgDevParcial.MasterTableView.Items[i]["Precio1"].FindControl("lblPrecio") as Label).Text;
                devparcial.Dev_Precio = !string.IsNullOrEmpty(label) ? Convert.ToDouble(label) : 0;
                devparcial.Dev_Importe = (devparcial.CantDevuelta * devparcial.Dev_Precio);
                if (devparcial.Descuento > 0)
                    devparcial.TotalImporte = devparcial.Dev_Importe - (devparcial.Dev_Importe * (devparcial.Descuento / 100));
                else
                    devparcial.TotalImporte = devparcial.Dev_Importe;
                if (devparcial.Descuento2 > 0)
                    devparcial.TotalImporte = devparcial.TotalImporte - (devparcial.TotalImporte * (devparcial.Descuento2 / 100));
                devparcial.Devuelto = (rgDevParcial.MasterTableView.Items[i]["Devuelto"].FindControl("ckDevuelto") as CheckBox).Checked;
                devparcialList.Add(devparcial);
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapDevParcial", "Id_Dev", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string MaximoIdNota()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapNotaCredito", "Id_Ncr", sesion.Emp_Cnx, "spCatLocal_Maximo");
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
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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