using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using CapaDatos;
using System.Globalization;
using System.Threading;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Diagnostics;
using CapaModelo_CC.CuentasCoorporativas;


namespace SIANWEB
{
    public partial class CapFactura : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        public string CambioTerritorio;
        public bool HabilitarGuardar
        {
            get
            {
                //DEVUELVE SI SE PUEDE O NO GUARDAR
                return RadToolBar1.Items[5].Visible;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        public List<AdendaDet> ListCab
        {
            get
            {
                return Session["CabeceraFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["CabeceraFacturacion" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListCabNacional
        {
            get
            {
                return Session["CabeceraFacturacionNacional" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["CabeceraFacturacionNacional" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListCabRF
        {
            get
            {
                return Session["CabeceraReFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["CabeceraReFacturacion" + Session.SessionID] = value;
            }
        }
        public List<AdendaDet> ListDetNacional
        {
            get
            {
                return Session["DetalleFacturacionNacional" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["DetalleFacturacionNacional" + Session.SessionID] = value;
            }
        }

        public List<AdendaDet> ListDet
        {
            get
            {
                return Session["DetalleFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["DetalleFacturacion" + Session.SessionID] = value;
            }
        }

        public List<AdendaDet> ListDetRF
        {
            get
            {
                return Session["DetalleReFacturacion" + Session.SessionID] as List<AdendaDet>;
            }
            set
            {
                Session["DetalleReFacturacion" + Session.SessionID] = value;
            }
        }

        public string ClienteSIAN
        {
            get
            {
                return Session["ClienteSIAN" + Session.SessionID] as string;
            }
            set
            {
                Session["ClienteSIAN" + Session.SessionID] = value;
            }
        }

        public List<Intra_CFD_CuentaNacional> ListCuentaNacional
        {
            get
            {
                return Session["ListCuentaNacional" + Session.SessionID] as List<Intra_CFD_CuentaNacional>;
            }
            set
            {
                Session["ListCuentaNacional" + Session.SessionID] = value;
            }
        }


        public DataTable objdtLista { get; set; }
        protected DataTable objdtTabla { get { if (ViewState["objdtTabla"] != null) { return (DataTable)ViewState["objdtTabla"]; } else { return objdtLista; } } set { ViewState["objdtTabla"] = value; } }


        //public DataTable listaFacturaDet = new DataTable();//
        public string arrayRemisiones = string.Empty;
        public string ActualAnio { get { return DateTime.Now.Year.ToString(); } }
        public string ActualMes { get { return (DateTime.Now.Month - 1).ToString(); } }
        public string ActualDia { get { return DateTime.Now.Day.ToString(); } }

        //Variable de lista de productos para el combo del grid Editable
        private List<Producto> _listaProductos;
        public List<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }

        //Variable de lista de territorios para el combo del grid Editable
        private List<Territorios> _listaTerritorios;
        public List<Territorios> ListaTerritorios
        {
            get { return _listaTerritorios; }
            set { _listaTerritorios = value; }
        }

        //Propiedad de lista de productos (partidas) de la factura
        public DataTable ListaProductosFactura
        {
            get
            {
                return (Session["ListaProductosFactura" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosFactura" + Session.SessionID] = value;
            }
        }

        public DataTable ListaProductosFacturaAdenda
        {
            get
            {
                return (Session["ListaProductosFacturaAdenda" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosFacturaAdenda" + Session.SessionID] = value;
            }
        }

        public DataTable ListaProductosFacturaAdendaNacional
        {
            get
            {
                return (Session["ListaProductosFacturaAdendaNacional" + Session.SessionID] as DataTable);
            }
            set
            {
                Session["ListaProductosFacturaAdendaNacional" + Session.SessionID] = value;
            }
        }

        //Propiedad de lista de productos con amortizacion del cliente
        private List<Amortizacion> ListaProductosAmortizacion
        {
            get { return (List<Amortizacion>)Session["ListaAmortizaciones" + Session.SessionID]; }
            set { Session["ListaAmortizaciones" + Session.SessionID] = value; }
        }
        private int cantidad_A = 0;
        public double porcRetencion;
        private int Id_Rem_A = 0;
        private int Rem_Cant_A = 0;
        private string _Editable;
        public string FechaEnable
        {
            get
            {
                return _Editable;// txtFecha.Enabled;
            }
        }
        private string _reFactura;
        public string ReFactura
        {
            get
            {
                return _reFactura;
            }
        }
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public bool EsRefactura
        {
            get
            {
                if (Page.Request.QueryString["reFactura"] == "0" || Page.Request.QueryString["reFactura"] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        public bool Guardar_FacEspecial = false;

        private FacturaEspecial FacturaEspecial
        {
            get { return (FacturaEspecial)Session["FacturaEspecial" + Session.SessionID]; }
            set { Session["FacturaEspecial" + Session.SessionID] = value; }
        }
        private int _Accion
        {
            get
            {
                return (int)Session["SesionAccion" + Session.SessionID];
            }
            set
            {
                Session["SesionAccion" + Session.SessionID] = value;
            }

        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                //Edsg 29052018

                //this.chkFacturarCuentaNacional.Enabled = false;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //sesion.URL = HttpContext.Current.Request.Url.Host;
                Dictionary<string, object> vPars = new Dictionary<string, object>();
                int Id_Emp, Id_Cd, Id_Fac;
                string Fac_Estatus;
                string facModificable;
                if (sesion == null)
                {
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    txtFecha.Enabled = false;
                    //txtPedido.Text = "";
                    if (!Page.IsPostBack)
                    { //obtener valores desde la URL
                        _Accion = Convert.ToInt32(Request.QueryString["Accion"]);

                        if (_Accion == 2)
                        {
                            CN_CapFactura cn_CapFactura = new CN_CapFactura();
                            Id_Fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                            Id_Cd = sesion.Id_Cd_Ver;
                            Id_Emp = sesion.Id_Emp;
                            facModificable = "0";
                            _reFactura = "0";

                            _PermisoGuardar = false;
                            _PermisoModificar = false;
                            _PermisoEliminar = false;
                            _PermisoImprimir = false;

                            BtnAutorizar.Visible = true;
                            BtnRechazar.Visible = true;
                        }
                        else
                        {
                            Id_Fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                            Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                            Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                            facModificable = Page.Request.QueryString["facModificable"].ToString();
                            _Editable = facModificable;
                            if (Page.Request.QueryString["reFactura"] != null)
                            {
                                _reFactura = Page.Request.QueryString["reFactura"].ToString();
                            }
                            _PermisoGuardar = Convert.ToInt32(Page.Request.QueryString["permisoGuardar"]) == 1 ? true : false;
                            _PermisoModificar = Convert.ToInt32(Page.Request.QueryString["permisoModificar"]) == 1 ? true : false;
                            _PermisoEliminar = Convert.ToInt32(Page.Request.QueryString["permisoEliminar"]) == 1 ? true : false;
                            _PermisoImprimir = Convert.ToInt32(Page.Request.QueryString["permisoImprimir"]) == 1 ? true : false;
                        }

                        this.Inicializar(Id_Emp, Id_Cd, Id_Fac, facModificable);

                        sesion.HoraInicio = DateTime.Now;
                        txtCausaRef.Enabled = false;
                        cmbCausaRef.Enabled = false;

                        vPars = (Dictionary<string, object>)Session["Parametros" + Session.SessionID];

                        if (vPars != null)
                        {
                            if (Convert.ToBoolean(vPars.FirstOrDefault(p => p.Key == "FacDev").Value))
                            {
                                DeshabilitaControles(RadPane1);
                                DeshabilitaControles(RadPane2);
                                DeshabilitaControles(RadPane3);
                                DeshabilitaControles(RadPane4);

                                foreach (RadToolBarItem item in RadToolBar1.Items)
                                {
                                    if (item.Index != 7)
                                        item.Enabled = false;
                                }
                            }
                        }

                        Session["Parametros" + Session.SessionID] = null;

                        if (Page.Request.QueryString["tipo"] != null)
                        {
                            rgFacturaDet.Columns.FindByUniqueName("Id_Ter").Display = false;
                            rgFacturaDet.Columns.FindByUniqueName("Id_TerN").HeaderText = "Ter.";
                            double width_Prd = rgFacturaDet.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width.Value;
                            double width_Ter = rgFacturaDet.Columns.FindByUniqueName("Id_Ter").HeaderStyle.Width.Value;

                            rgFacturaDet.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width = (Unit)(width_Prd + width_Ter - 42);
                        }

                        //rgFacturaDetAde
                        if (Page.Request.QueryString["tipo"] != null)
                        {
                            double width_Prd = rgFacturaDetAde.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width.Value; //DataField="Id_Prd"    PRODUCTO                            
                            rgFacturaDetAde.Columns.FindByUniqueName("Id_Prd").HeaderStyle.Width = (Unit)(width_Prd);

                        }
                        //rgFacturaDetAde

                        if (_reFactura == "0")
                            _reFactura = null;

                        if (facModificable == "0" || !((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible || !string.IsNullOrEmpty(_reFactura))
                        {
                            deshabilitarcontroles(formularioDatosGenerales.Controls, _reFactura);
                            deshabilitarcontroles(formularioTotales.Controls, _reFactura);
                            if (!string.IsNullOrEmpty(_reFactura))
                                HabilitarColumnas(true);
                            else
                            {
                                if (facModificable == "0")
                                    HabilitarColumnas(true);
                                else
                                    HabilitarColumnas(false);
                            }
                            txtClienteNombre.Enabled = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgFacturaDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaDet.Rebind();


                        //rgFacturaDetAde
                        double ancho2 = 0;
                        foreach (GridColumn gc in rgFacturaDetAde.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDetAde.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                        rgFacturaDetAde.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                        rgFacturaDetAde.Rebind();

                        //rgFacturaDetAde
                        double ancho3 = 0;
                        foreach (GridColumn gc in rgFacturaDetAdeNacional.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho3 = ancho3 + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDetAdeNacional.Width = Unit.Pixel(Convert.ToInt32(ancho3));
                        rgFacturaDetAdeNacional.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho3));
                        rgFacturaDetAdeNacional.Rebind();

                        if (facModificable == "0")
                        {
                            ((GridCommandItem)rgFacturaDetAde.MasterTableView.GetItems(GridItemType.CommandItem)[0]).FindControl("AddNewRecordButton").Parent.Visible = false;
                        }

                    }

                    EvaluarAsignacionTipoMovimientoParaGarantia();
                    if (EsRefactura == true)
                    {
                        ChkRefacturaparcial.Enabled = true;
                        ChkRefacturatotal.Enabled = true;
                    }
                    else
                    {
                        ChkRefacturaparcial.Enabled = false;
                        ChkRefacturatotal.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos, string _reFactura)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls, _reFactura);
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
                        if ((controles_contenidos[x] as RadDatePicker).ID == txtFecha.ID && !string.IsNullOrEmpty(_reFactura))
                        {
                            (controles_contenidos[x] as RadDatePicker).Enabled = true;
                            _Editable = "1";
                        }
                        break;
                }

                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = true;
                }
            }

            //ChkRefacturatotal.Enabled = true;
            //ChkRefacturaparcial.Enabled = true;
        }

        protected void cmbCausaRef_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbCausaRef.SelectedValue == "3" || cmbCausaRef.SelectedValue == "9" || cmbCausaRef.SelectedValue == "10")
            {
                txtId.Enabled = true;
                txtCliente.Enabled = true;
                txtReq.Enabled = true;
                txtClienteNombre.Enabled = true;
                txtTerritorio.Enabled = true;
                cmbTerritorio.Enabled = true;
            }
            else
            {
                txtId.Enabled = false;
                txtCliente.Enabled = false;
                txtReq.Enabled = false;
                txtClienteNombre.Enabled = false;
                txtTerritorio.Enabled = false;
                cmbTerritorio.Enabled = false;
            }
        }

        protected void cmbTerritorio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if ((txtCliente.Text != string.Empty) && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                        && (cmbTerritorio.SelectedValue != "-1" && cmbTerritorio.SelectedValue != string.Empty))
                {
                    this.rgFacturaDet.Enabled = true;
                    this.btnFacturaEspecial.Enabled = true;
                }
                else
                {
                    this.rgFacturaDet.Enabled = false;
                    this.btnFacturaEspecial.Enabled = false;
                }

                CN_CatTerritorios territorio = new CN_CatTerritorios();
                Territorios ter = new Territorios();
                ter.Id_Emp = sesion.Id_Emp;
                ter.Id_Cd = sesion.Id_Cd_Ver;
                ter.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                txtRepresentante.Text = ter.Id_Rik.ToString();
                txtRepresentanteStr.Text = ter.Rik_Nombre;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message));
            }
        }
        protected void cmbMov_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (Session["PedidoFacturacion" + Session.SessionID] != null)
                {
                    if (e.Value != "51" && e.Value != "52")
                    {
                        this.DisplayMensajeAlerta("MovFacturacionPedidoNoValido");
                        txtMov.Text = string.Empty;
                        cmbMov.SelectedIndex = cmbMov.FindItemIndexByValue("-1");
                        return;
                    }
                }

                if ((e.Value != "-1" && e.Value != string.Empty) && (txtCliente.Value.HasValue))
                {
                    this.rgFacturaDet.Enabled = true;
                    this.btnFacturaEspecial.Enabled = true;
                }
                else
                {
                    this.rgFacturaDet.Enabled = false;
                    this.btnFacturaEspecial.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void cmbConsFacEle_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                int valor = cmbConsFacEle.SelectedValue == "" ? -1 : Convert.ToInt32(cmbConsFacEle.SelectedValue);
                if (!this.ObtenerConsecutivoFactElectronica(valor))
                    Alerta("No hay consecutivo de facturación electrónica disponible para la serie seleccionada");
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbConsFacEle_ObtenerConsFacElectFallo"));
            }
        }
        protected void cmbProducto_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            try
            {
                ErrorManager();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProductosLista_ItemsDataBound"));
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        mensajeError = hiddenId.Value == string.Empty ? "CapFactura_insert_error" : "CapFactura_update_error";
                        //====================================================================================================
                        // se agrega el guardar de la factura especial 
                        // Raúl Bórquez Martínez 
                        // 29-05-2015 
                        //Inicia codigo Factura especial=======================================================================
                        this.Guardar();
                        //Fin de codigo factura especial======================================================================= 
                        this.Guardar(false);
                        break;
                    //====================================================================================================
                    // se agrega codigo para inicializar factura especial o cancelar una factura especial antes de guardar
                    // Raúl Bórquez Martínez 
                    // 29-05-2015 
                    //Inicia codigo Factura especial=======================================================================
                    case "facEspecial":
                        this.Inicializar();
                        break;
                    case "CanfacEspecial":
                        this.EliminarFacturaEspecial();
                        break;
                    //Fin de codigo factura especial=======================================================================
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No hay cantidad suficiente en el inventario para facturar el producto"))
                {
                    Alerta(ex.Message);
                    return;
                }
                else if (ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                {
                    Alerta(ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    return;
                }

                else
                {
                    string mensaje = string.Concat(ex.Message, mensajeError);
                    this.DisplayMensajeAlerta(mensaje);
                    return;
                }
            }
            finally
            {
                RAM1.ResponseScripts.Add("HabilitarGuardar();");
            }

        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgFacturaDet.Rebind();
                        //rgFacturaDetAde
                        rgFacturaDetAde.Rebind();
                        //rgFacturaDetAde
                        break;
                    case "FacturaEspecial":
                        ListaProductosFactura = objdtTabla;
                        break;
                    case "cliente":
                        txtCliente.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtCliente_TextChanged(null, null);
                        break;
                    case "cn":
                        txtClienteNacional.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtClienteNacionalNombre.Text = Session["Descripcion_Buscar" + Session.SessionID].ToString();
                        txtClienteNacional_TextChanged(null, null);
                        break;
                    case "precio":
                        if ((producto as RadNumericTextBox).Enabled)
                        {
                            (producto as RadNumericTextBox).DbValue = Session["Id_Buscar" + Session.SessionID];
                            txtProducto_TextChanged(producto, null);
                            if ((producto as RadNumericTextBox).Value.HasValue)
                            {
                                ((producto as RadNumericTextBox).Parent.FindControl("txtFac_Cantidad") as RadNumericTextBox).Focus();
                            }
                        }
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 150);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                    case "AjustarCentavos":
                        txtSubTotal.DbValue = FacturaEspecial.FacEsp_SubTotal;
                        txtIVA.DbValue = FacturaEspecial.FacEsp_ImporteIva;
                        txtTotal.DbValue = FacturaEspecial.FacEsp_Total;
                        break;
                    case "CancelarAlertaPrecios":

                        RadTabStrip1.SelectedIndex = 1;
                        RadMultiPage1.SelectedIndex = 1;
                        Alerta("Modifique los precios en los productos de acuerdo a los convenios para poder guardar la factura.");
                        break;
                    case "AceptarPrecios":
                        Guardar(true);
                        break;
                    case "direccion":
                        CN_CatCliente clsCliente = new CN_CatCliente();
                        ClienteDirEntrega cliente = new ClienteDirEntrega();
                        Sesion session2 = new Sesion();
                        session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        cliente.Id_Emp = session2.Id_Emp;
                        cliente.Id_Cd = session2.Id_Cd_Ver;

                        string[] id = Session["Id_Buscar" + Session.SessionID].ToString().Split('-');


                        cliente.Id_CteDirEntrega = Int32.Parse(id[0]) - 1;
                        cliente.Id_Cte = Int32.Parse(Session["Descripcion_Buscar" + Session.SessionID].ToString());
                        clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx);
                        txtCalle.Text = cliente.Cte_Calle;
                        txtCalleNumero.Text = cliente.Cte_Numero;
                        txtCP.Text = cliente.Cte_Cp.Trim();
                        txtColonia.Text = cliente.Cte_Colonia;
                        txtMunicipio.Text = cliente.Cte_Municipio;
                        txtEstado.Text = cliente.Cte_Estado;
                        txtTelefono.Text = cliente.Cte_Telefono;

                        break;

                    case "direccion_CC":
                        string dirFiscal = "";
                        int idcliente = 0;
                        if (Session["Descripcion_Buscar" + Session.SessionID] != null)
                        {
                            dirFiscal = Session["Descripcion_Buscar" + Session.SessionID].ToString();
                            idcliente = Int32.Parse(Session["Id_Buscar" + Session.SessionID].ToString());

                            var dirArray = dirFiscal.Split(',');

                            this.txtClienteNacional.Text = idcliente.ToString();
                            this.txtClienteNacionalNombre.Text = dirArray[0].Replace("<b>", "").Replace("</b>", "");
                            this.TxtClienteNacionalCalle.Text = dirArray[1];
                            this.TxtClienteNacionalNoExterior.Text = "";
                            this.TxtClienteNacionalColonia.Text = dirArray[2];
                            this.TxtClienteNacionalCp.Text = dirArray[3];
                            this.TxtClienteNacionalMunicipio.Text = dirArray[4];
                            this.TxtClienteNacionalEstado.Text = dirArray[5];

                            this.TxtClienteNacionalRfc.Text = dirArray[6];

                            Session.Remove("Descripcion_Buscar" + Session.SessionID);
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "RAM1_AjaxRequest"));
            }
        }

        protected void rgFacturaDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Session["CantidadEditar" + Session.SessionID] = 0;
                Session["CantidadRemision" + Session.SessionID] = 0;
                Session["Remision" + Session.SessionID] = 0;
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFacturaDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                    case "Edit":
                        cantidad_A = int.Parse((rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Fac_Cant"].FindControl("lblOrd_Cantidad") as Label).Text);
                        Id_Rem_A = !string.IsNullOrEmpty(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text.Replace("&nbsp;", "")) ? int.Parse(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Id_Rem"].Text) : 0;
                        Rem_Cant_A = !string.IsNullOrEmpty(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Rem_Cant"].Text) ? Int32.TryParse(rgFacturaDet.MasterTableView.Items[e.Item.ItemIndex]["Rem_Cant"].Text, out Rem_Cant_A) ? Rem_Cant_A : 0 : 0;
                        Session["CantidadEditar" + Session.SessionID] = cantidad_A;//(rgFacturaDet.MasterTableView.Items[e.Item.RowIndex]["Fac_Cant"].FindControl("lblOrd_Cantidad") as Label).Text;
                        Session["CantidadRemision" + Session.SessionID] = Rem_Cant_A;
                        Session["Remision" + Session.SessionID] = Id_Rem_A;



                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgFacturaDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgFacturaDet.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgFacturaDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                { //crear dataSource del combo de productos de cada registro del Grid
                    this.EstablecerDataSourceProductosLista(string.Empty);
                    //Llenar Grid
                    rgFacturaDet.DataSource = this.objdtTabla;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_NeedDataSource"));
            }
        }
        protected void rgFacturaDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            bool EsDevolucion = false;
            try
            {
                ErrorManager();
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtFac_Cantidad");
                    string lblFac_Cantidad = ((Label)editItem.FindControl("lblVal_txtFac_Cantidad")).ClientID.ToString();
                    string txtFac_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    string lbltxtTerritorioPartida = ((Label)editItem.FindControl("lbltxtTerritorioPartida")).ClientID.ToString();
                    string txtTerritorioPartida = ((RadNumericTextBox)editItem.FindControl("txtTerritorioPartida")).ClientID.ToString();
                    string lblTxtClienteExterno = ((Label)editItem.FindControl("lblTxtClienteExterno")).ClientID.ToString();
                    string txtClienteExterno = ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).ClientID.ToString();

                    ////Llenar combo de productos
                    //RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    ////comboProductoItem.DataSource = this.ListaProductos;
                    ////comboProductoItem.DataBind();
                    ////
                    //CargarProductos(comboProductoItem);
                    //comboProductoItem.SelectedIndex = 0;

                    //Llenar combo de clientes, solo si es movimiento 70
                    //si no, el combo se oculta
                    //RadComboBox comboClientesItem = (RadComboBox)editItem.FindControl("cmbClienteExterno");
                    RadNumericTextBox txtClienteExternoItem = (RadNumericTextBox)editItem.FindControl("txtClienteExterno");
                    RadTextBox txtClienteExternoStr = (RadTextBox)editItem.FindControl("txtNombreCliente");
                    if (cmbMov.SelectedValue == "70")
                    {
                        //((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).ReadOnly = false;
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = true;
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = true;

                        //CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                        //CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatCliente_Combo", ref comboClientesItem);
                    }
                    else
                    {
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = false;
                        ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = false;

                        //editItem["Id_CteExt"].Controls.Clear();
                        //editItem["Id_CteExt"].Style.Add("display", "false");
                        //txtClienteExternoItem.Visible = false;
                        //comboClientesItem.Visible = false;

                        //txtClienteExternoItem.Parent.Visible = false;
                    }

                    //Llenar combo de territorios
                    RadComboBox comboTerritorioPartidaItem = (RadComboBox)editItem.FindControl("cmbTerritorioPartida");
                    List<Territorios> listaTerritorios = new List<Territorios>();
                    new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), sesion, ref listaTerritorios);
                    comboTerritorioPartidaItem.DataTextField = "Descripcion";
                    comboTerritorioPartidaItem.DataValueField = "Id_Ter";
                    comboTerritorioPartidaItem.DataSource = listaTerritorios;
                    comboTerritorioPartidaItem.DataBind();
                    //comboTerritorioPartidaItem.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    string jsControles = string.Concat(
                        "lblFac_CantidadClientId='", lblFac_Cantidad, "';"
                        , "txtFac_CantidadClientId='", txtFac_Cantidad, "';"
                        , "lbl_cmbProductoClientId='", lbl_cmbProducto, "';"
                        , "txtId_PrdClientId='", txtId_Prd, "';"
                        , "lbltxtTerritorioPartidaClientId='", lbltxtTerritorioPartida, "';"
                        , "txtTerritorioPartidaClientId='", txtTerritorioPartida, "';"
                        , "lblTxtClienteExternoClientId='", lblTxtClienteExterno, "';"
                        , "txtClienteExternoClientId='", txtClienteExterno, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //cuando la edición se usa para inserción, se habilita el combo de producto
                        //((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = true;
                        //es registro nuevo y se inhabilita el campo de cantidad (se habilita hasta ke elija un producto del combo)
                        Ctrl_txtOrd_Cantidad.Enabled = false;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                        if (txtClienteExternoItem.Parent.Visible == true)
                        {
                            ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).Focus();
                        }
                        else
                        {
                            ((RadNumericTextBox)editItem.FindControl("txtTerritorioPartida")).Focus();
                        }
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //////establecer unidades de empaque
                        ////int claveOrden = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Ord"].ToString());
                        ////int claveProducto = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString());
                        ////Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        ////Producto producto = null;
                        ////new CN_CatProducto().ConsultaProducto_OrdenCompra(ref producto, sesion.Emp_Cnx, claveOrden, claveProducto, sesion.Id_Emp, sesion.Id_Cd_Ver);
                        ////((HiddenField)editItem.FindControl("HD_Prd_UniEmp")).Value = producto.Prd_UniEmp.ToString();
                        //////-------------------------------


                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).Enabled = true;
                        ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).Enabled = true;
                        ((RadTextBox)editItem.FindControl("txtNombreCliente")).Enabled = true;
                        ((RadTextBox)editItem.FindControl("txtProductoNombre")).Enabled = true;
                        //((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = false;
                        //es registro de edición, se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                        Ctrl_txtOrd_Cantidad.Enabled = true;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);

                        //cuando es actualización se selecciona el producto del combo
                        //comboProductoItem.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                        //if (txtClienteExternoItem.Parent.Visible == true)
                        //{
                        //    if (editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_CteExt"] != null)
                        //        txtClienteExternoStr.Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_CteExtN"].ToString();
                        //}
                        comboTerritorioPartidaItem.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Ter"].ToString();
                        Ctrl_txtOrd_Cantidad.Focus();
                    }
                }



                //TODO: AGREGAR PARA PONER EL FOCUS
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem form = (GridEditableItem)e.Item;
                    List<Remision> listaRemisionesCan = (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID];

                    if (listaRemisionesCan != null)
                    {
                        EsDevolucion = !(e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem);
                    }

                    RadNumericTextBox dataField = (RadNumericTextBox)form["Id_TerN"].FindControl("txtTerritorioPartida");
                    if ((!dataField.Enabled) || EsDevolucion)
                    {
                        dataField = (RadNumericTextBox)form["Fac_Cant"].FindControl("txtFac_Cantidad");
                    }

                    dataField.Focus();

                    //Sesion Sesion = new Sesion();
                    Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];


                    int VGEmpresa = 0;
                    Int32.TryParse(strEmp, out VGEmpresa);
                    if (VGEmpresa == Sesion.Id_Emp)
                    {
                        ((RadNumericTextBox)form.FindControl("txtFac_Precio")).Enabled = false;
                    }



                    //((RadNumericTextBox)form.FindControl("txtTerritorioPartida")).Enabled = !(EsRefactura || EsDevolucion);
                    //((RadComboBox)form.FindControl("cmbTerritorioPartida")).Enabled = !(EsRefactura || EsDevolucion);
                    //((RadTextBox)form.FindControl("txtPrd_Presentacion")).Enabled = !(EsRefactura || EsDevolucion);
                    //((RadTextBox)form.FindControl("txtPrd_UniNe")).Enabled = !(EsRefactura || EsDevolucion);
                    //((RadNumericTextBox)form.FindControl("txtFac_Cantidad")).Enabled = !EsRefactura;
                    //((RadNumericTextBox)form.FindControl("txtFac_Precio")).Enabled = !(EsRefactura || EsDevolucion);

                    ((RadNumericTextBox)form.FindControl("txtTerritorioPartida")).Enabled = !(EsDevolucion);
                    ((RadComboBox)form.FindControl("cmbTerritorioPartida")).Enabled = !(EsDevolucion);
                    ((RadTextBox)form.FindControl("txtPrd_Presentacion")).Enabled = !(EsDevolucion);
                    ((RadTextBox)form.FindControl("txtPrd_UniNe")).Enabled = !(EsDevolucion);
                    //((RadNumericTextBox)form.FindControl("txtFac_Cantidad")).Enabled = !EsRefactura;
                    ((RadNumericTextBox)form.FindControl("txtFac_Precio")).Enabled = !(EsDevolucion);

                    if (cmbMov.SelectedValue == "91") ((RadNumericTextBox)form.FindControl("txtFac_Precio")).Enabled = true;
                }




                //-----------------------------------------
            }
            catch (Exception ex)
            {
                //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_ItemDataBound"));
            }
        }
        protected void rgFacturaDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            FacturaDet facturaDet = new FacturaDet();
            GridEditableItem insertedItem = (GridEditableItem)e.Item;
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];



                facturaDet.Id_Emp = sesion.Id_Emp;
                facturaDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Id_Fac = Convert.ToInt32(txtId.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                facturaDet.Id_FacDet = 0;
                facturaDet.Id_Ter = Convert.ToInt32((insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).SelectedValue);
                facturaDet.Id_TerStr = (insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).Text;
                facturaDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value); //Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                if (((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display == true)
                {
                    facturaDet.Id_CteExt = Convert.ToInt32((insertedItem["Id_CteExtN"].FindControl("txtClienteExterno") as RadNumericTextBox).Value);
                    facturaDet.Id_CteExtStr = (insertedItem["Id_CteExt"].FindControl("txtNombreCliente") as RadTextBox).Text;
                }
                else
                {
                    facturaDet.Id_CteExt = 0;
                    facturaDet.Id_CteExtStr = string.Empty;
                }
                facturaDet.Fac_Cant = Convert.ToInt32((insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).Text);
                double precioPartida = Convert.ToDouble((insertedItem["Fac_Precio"].FindControl("txtFac_Precio") as RadNumericTextBox).Text.Replace("$", string.Empty));
                double importe = precioPartida * Convert.ToDouble(facturaDet.Fac_Cant);
                if (facturaDet.Fac_Cant == 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadCero");
                }
                if (importe <= 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadImporteCero");
                }

                (insertedItem["Fac_Importe"].FindControl("lblFac_ImporteEdit") as Label).Text = importe.ToString();
                facturaDet.Fac_Precio = precioPartida;

                //datos del producto de la orden de compra
                facturaDet.Producto = new Producto();
                facturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value); //Convert.ToInt32((insertedItem.FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Producto.Id_Emp = sesion.Id_Emp;
                facturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Producto.Prd_Descripcion = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                facturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                facturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                if (objdtTabla.Select("Id_Prd='" + facturaDet.Id_Prd.ToString() + "' and Id_Ter='" + facturaDet.Id_Ter.ToString() + "'").Length > 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_insert_repetida");
                }


                if (ValidaPartidas(Convert.ToInt32((insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).SelectedValue), Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value), Convert.ToInt32((insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).Text)) == false)
                {
                    e.Canceled = true;
                    return;
                }

                ArrayList al = new ArrayList();
                al.Add(facturaDet.Id_Fac);
                al.Add(facturaDet.Id_FacDet);
                al.Add(facturaDet.Id_Rem);
                al.Add(0);//id_tm_Rem
                al.Add(facturaDet.Id_CteExt);
                al.Add(facturaDet.Id_Ter);
                al.Add(facturaDet.Id_Prd);
                al.Add(facturaDet.Producto.Prd_Descripcion);
                al.Add(facturaDet.Producto.Prd_Presentacion);
                al.Add(facturaDet.Producto.Prd_UniNe);
                al.Add(facturaDet.Fac_Cant);
                al.Add(facturaDet.Rem_Cant);
                al.Add(facturaDet.Fac_Precio);
                al.Add(importe);
                al.Add(facturaDet.Id_TerStr);
                al.Add(facturaDet.Id_CteExtStr);
                al.Add(facturaDet.AmortizacionProducto);
                al.Add(facturaDet.Id_Emp);
                al.Add(facturaDet.Id_Cd);
                al.Add(facturaDet.Fac_Asignar);
                al.Add(facturaDet.Fac_Devolucion);
                al.Add(facturaDet.Producto.Prd_UniNs);

                //GUARDA LA LISTA DE PRODUCTOS AL ARREGLO
                objdtTabla.Rows.Add(al.ToArray());
                this.CalcularTotales();
                //}
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                if (ex.Message.Contains("rgFacturaDet_InvFinalInsuficiente"))
                {
                    this.AlertaFocus(string.Concat("Producto "
                            , facturaDet.Id_Prd.ToString()
                            , ", inventario disponible insuficiente.<br/>Inventario final: "
                            , HD_Prd_InvFinal.Value
                            , "<br/>Asignado: "
                            , HD_Prd_Asignado.Value
                            , "<br/>Disponible: "
                            , HD_Prd_Disponible.Value), (insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).ClientID);
                    return;
                }
                else
                {
                    if (ex.Message.Contains("rgFacturaDet_cantidadCero"))
                    {
                        this.AlertaFocus("La cantidad del producto ingresado no puede ser cero", (insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).ClientID);
                        return;
                    }
                    else
                        if (ex.Message.Contains("rgFacturaDet_cantidadImporteCero"))
                        {
                            this.AlertaFocus("El importe del producto ingresado no puede ser cero", (insertedItem["Fac_Precio"].FindControl("txtFac_Precio") as RadNumericTextBox).ClientID);
                            return;
                        }
                        else
                        {
                            Alerta(ex.Message);
                            return;
                        }
                }
            }
        }
        protected void rgFacturaDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            float cantidadResmisionada = 0;
            double disponible = 0;
            int vRemCant = 0;

            FacturaDet facturaDet = new FacturaDet();
            try
            {
                ErrorManager();
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                //siempre ke se edita un producto se calcula el dsponible porque puede ser un producto de facturacion pedido o remision
                this.ConsultaInventarioProducto(Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value));

                facturaDet.Id_Emp = sesion.Id_Emp;
                facturaDet.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Id_Fac = Convert.ToInt32(txtId.Text); //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual

                if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Rem_Cant"]))
                {
                    vRemCant = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Rem_Cant"]);
                }
                else
                {
                    insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Rem_Cant"] = insertedItem["Rem_Cant"].Text;
                }

                insertedItem["Rem_Cant"].Text = vRemCant.ToString();

                if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_FacDet"]))
                    facturaDet.Id_FacDet = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_FacDet"]);
                else
                    facturaDet.Id_FacDet = 0;

                if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"]))
                    facturaDet.Id_Rem = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"]);

                facturaDet.Id_Ter = Convert.ToInt32((insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).SelectedValue);
                facturaDet.Id_TerStr = (insertedItem["Id_Ter"].FindControl("cmbTerritorioPartida") as RadComboBox).Text;
                facturaDet.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value);
                if (((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display == true)
                {
                    facturaDet.Id_CteExt = Convert.ToInt32((insertedItem["Id_CteExtN"].FindControl("txtClienteExterno") as RadNumericTextBox).Value);
                    facturaDet.Id_CteExtStr = (insertedItem["Id_CteExt"].FindControl("txtNombreCliente") as RadTextBox).Text;
                    //facturaDet.Id_CteExt = Convert.ToInt32((insertedItem["Id_CteExt"].FindControl("cmbClienteExterno") as RadComboBox).SelectedValue);
                    //facturaDet.Id_CteExtStr = (insertedItem["Id_CteExt"].FindControl("cmbClienteExterno") as RadComboBox).Text;
                }
                else
                {
                    facturaDet.Id_CteExt = 0;
                    facturaDet.Id_CteExtStr = string.Empty;
                }
                facturaDet.Fac_Cant = Convert.ToInt32((insertedItem["Fac_Cant"].FindControl("txtFac_Cantidad") as RadNumericTextBox).Text);
                double precioPartida = Convert.ToDouble((insertedItem["Fac_Precio"].FindControl("txtFac_Precio") as RadNumericTextBox).Text.Replace("$", string.Empty));
                double importe = precioPartida * Convert.ToDouble(facturaDet.Fac_Cant);
                (insertedItem["Fac_Importe"].FindControl("lblFac_ImporteEdit") as Label).Text = importe.ToString();
                facturaDet.Fac_Precio = precioPartida;

                if (facturaDet.Fac_Cant < vRemCant)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidaRemision");
                }

                if (facturaDet.Fac_Cant == 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadCero");
                }

                if (importe <= 0)
                {
                    e.Canceled = true;
                    throw new Exception("rgFacturaDet_cantidadImporteCero");
                }

                //datos del producto de la orden de compra
                facturaDet.Producto = new Producto();
                facturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value);  //Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                facturaDet.Producto.Id_Emp = sesion.Id_Emp;
                facturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                facturaDet.Producto.Prd_Descripcion = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                facturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                facturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe") as RadTextBox).Text;

                string mensajeInventarioExcepcion = string.Empty;
                //validar cantidad de producto de partida contra Disponible
                //if (facturaDet.Fac_Cant > Convert.ToSingle(HD_Prd_Disponible.Value))
                //    mensajeInventarioExcepcion = "rgFacturaDet_InvFinalInsuficiente";
                //validar cantidad de producto de partida contra cantidad de remisión si es que la factura es de remisiones

                //if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"])) //es facturacion de resmision
                //{
                //    if (!Convert.IsDBNull(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Rem_Cant"]))
                //        cantidadResmisionada = Convert.ToSingle(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Rem_Cant"]);

                //    facturaDet.Id_Rem = Convert.ToInt32(insertedItem.OwnerTableView.DataKeyValues[insertedItem.ItemIndex]["Id_Rem"]);

                //    facturaDet.Rem_Cant = cantidadResmisionada;
                //    disponible = !string.IsNullOrEmpty(HD_Prd_Disponible.Value) ? Convert.ToInt32(HD_Prd_Disponible.Value) : 0;
                //    disponible += cantidadResmisionada;
                //    if (facturaDet.Id_Rem > 0)
                //        if (facturaDet.Fac_Cant > disponible)
                //            mensajeInventarioExcepcion = "rgFacturaDet_InvFinalRemisionInsuficiente";
                //}

                if (mensajeInventarioExcepcion != string.Empty)
                    throw new Exception(mensajeInventarioExcepcion);
                else //agregar producto de orden de compra a la lista
                {
                    DataRow[] Ar_dr;

                    Ar_dr = objdtTabla.Select("Id_Ter='" + facturaDet.Id_Ter + "' and Id_Prd='" + facturaDet.Id_Prd + "'");
                    if (Ar_dr.Length > 0)
                    {
                        Ar_dr[0].BeginEdit();
                        Ar_dr[0]["Id_Fac"] = facturaDet.Id_Fac;
                        Ar_dr[0]["Id_FacDet"] = facturaDet.Id_FacDet;
                        Ar_dr[0]["Id_Rem"] = facturaDet.Id_Rem;
                        Ar_dr[0]["Id_CteExt"] = facturaDet.Id_CteExt;
                        Ar_dr[0]["Id_Ter"] = facturaDet.Id_Ter;
                        Ar_dr[0]["Id_Prd"] = facturaDet.Id_Prd;
                        Ar_dr[0]["Prd_Descripcion"] = facturaDet.Producto.Prd_Descripcion;
                        Ar_dr[0]["Prd_Presentacion"] = facturaDet.Producto.Prd_Presentacion;
                        Ar_dr[0]["Prd_UniNe"] = facturaDet.Producto.Prd_UniNe;
                        Ar_dr[0]["Fac_Cant"] = facturaDet.Fac_Cant;
                        Ar_dr[0]["Rem_Cant"] = facturaDet.Rem_Cant;
                        Ar_dr[0]["Fac_Precio"] = facturaDet.Fac_Precio;

                        Ar_dr[0]["Fac_Importe"] = importe;
                        Ar_dr[0]["Id_TerStr"] = facturaDet.Id_TerStr;
                        Ar_dr[0]["Id_CteExtStr"] = facturaDet.Id_CteExtStr;
                        Ar_dr[0]["AmortizacionProducto"] = facturaDet.AmortizacionProducto;
                        Ar_dr[0]["Id_Cd"] = facturaDet.Id_Cd;
                        Ar_dr[0]["Fac_Asignar"] = facturaDet.Fac_Asignar;
                        Ar_dr[0]["Fac_Devolucion"] = facturaDet.Fac_Devolucion;
                        Ar_dr[0]["Prd_UniNs"] = facturaDet.Producto.Prd_UniNs;

                        Ar_dr[0].AcceptChanges();
                    }
                    CalcularTotales();
                }

                //GridSortExpression expression = new GridSortExpression();
                //expression.FieldName = "Id_Prd";
                //expression.SetSortOrder("Ascending");
                //this.rgFacturaDet.MasterTableView.SortExpressions.AddSortExpression(expression);
                //this.rgFacturaDet.MasterTableView.Rebind();
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                if (ex.Message.Contains("rgFacturaDet_InvFinalInsuficiente"))
                {
                    this.Alerta(string.Concat("Producto "
                        , facturaDet.Id_Prd.ToString()
                        , ", inventario disponible insuficiente.<br/>Inventario final: ", HD_Prd_InvFinal.Value
                        , "<br/>Asignado: ", HD_Prd_Asignado.Value
                        , "<br/>Disponible: ", HD_Prd_Disponible.Value));
                    return;
                }
                else
                {
                    if (ex.Message.Contains("rgFacturaDet_InvFinalRemisionInsuficiente"))
                    {
                        this.Alerta(string.Concat("Producto "
                        , facturaDet.Id_Prd.ToString()
                        , ", inventario disponible insuficiente.<br/>Remisionado: ", cantidadResmisionada.ToString()
                        , "<br/>Disponible: ", disponible.ToString()));
                        return;
                    }
                    if (ex.Message.Contains("rgFacturaDet_cantidadCero"))
                    {
                        this.Alerta("La cantidad del producto ingresado no puede ser cero");
                        return;
                    }
                    else
                        if (ex.Message.Contains("rgFacturaDet_cantidadImporteCero"))
                        {
                            this.Alerta("El importe del producto ingresado no puede ser cero");
                            return;
                        }
                        else
                        {
                            if (ex.Message.Contains("rgFacturaDet_cantidadImporteCero"))
                            {
                                Alerta("La cantidad debe ser menor a la de la remisión.");
                                return;
                            }
                            else
                            {
                                Alerta(ex.Message.Replace("'", ""));
                                return;
                            }

                        }
                }
            }
        }
        protected void rgFacturaDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();

                if (rgFacturaDet.EditItems.Count > 0)
                {
                    Alerta("Ya está editando un registro");
                    e.Canceled = true;
                }
                else
                {
                    GridDataItem itemAde = (GridDataItem)e.Item;
                    int Id_PrdFac = Convert.ToInt32(itemAde.OwnerTableView.DataKeyValues[itemAde.ItemIndex]["Id_Prd"]);
                    string eliminar = "SI";
                    if (rgFacturaDetAde.Items.Count > 1)
                    {
                        foreach (GridDataItem item2 in rgFacturaDetAde.Items)
                        {
                            //int IdProducto = Convert.ToInt32( item["Id_Prod"].Text);
                            int IdProducto = Convert.ToInt32(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Id_Prd"]);

                            if (IdProducto == Id_PrdFac)
                            {
                                eliminar = "NO";
                            }
                        }
                    }

                    if (eliminar == "NO")
                    {
                        Alerta("No se Puede Eliminar este Producto, Existen Adendas Capturadas");
                    }
                    else
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        int id_Fac = Convert.ToInt32(txtId.Text);
                        int Id_FacDet = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_FacDet"]);
                        int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                        int Id_Ter = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Ter"]);
                        //actualizar producto de orden de compra a la lista
                        this.ListaProductosFactura_EliminarProducto(Id_Prd, Id_Ter, id_Fac);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFacturaDet_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void rgFacturaDetAde_ItemCommand(object source, GridCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "InitInsert":
                    if (objdtTabla.Rows.Count == 0)
                    {
                        Alerta("Debe agregar al menos un producto para llenar la Adenda");
                        e.Canceled = true;
                    }
                    else
                        if (rgFacturaDetAde.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        else
                        {
                            if (e.CommandName == RadGrid.InitInsertCommandName)
                            {
                                //Add new" button clicked
                                e.Canceled = true;

                                //Prepare an IDictionary with the predefined values
                                System.Collections.Specialized.ListDictionary newValues = new
                                System.Collections.Specialized.ListDictionary();
                                newValues["Id_Cons_AdeDet"] = generarGUI_IdAdeDet();
                                newValues["Id_Prd"] = string.Empty;
                                newValues["Prd_Descripcion"] = string.Empty;
                                //Insert the item and rebind
                                e.Item.OwnerTableView.InsertItem(newValues);
                            }
                        }
                    break;
            }

        }
        protected void rgFacturaDetAde_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgFacturaDetAde.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgFacturaDetAde_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgFacturaDetAde.DataSource = this.ListaProductosFacturaAdenda;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDetAde_NeedDataSource"));
            }
        }
        protected void rgFacturaDetAde_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //Llenar combo de Productos de Adenda                                     
                    RadComboBox comboproducto = (RadComboBox)editItem.FindControl("cmbProductoAde");

                    comboproducto.DataValueField = "Id_Prd";
                    comboproducto.DataTextField = "Prd_Descripcion";
                    comboproducto.DataSource = objdtTabla;
                    comboproducto.DataBind();
                    comboproducto.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        ((RadComboBox)editItem.FindControl("cmbProductoAde")).Enabled = true;
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto



                        //((RadComboBox)editItem.FindControl("cmbProductoAde")).Enabled = false;
                        //((RadNumericTextBox)editItem.FindControl("txtId_PrdAde")).Enabled = false;
                        comboproducto.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDetAde_ItemDataBound"));
            }
        }
        protected void rgFacturaDetAde_InsertCommand(object sender, GridCommandEventArgs e)
        {

            GridEditableItem insertedItem = (GridEditableItem)e.Item;
            try
            {

                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string Id_Cons_AdeDet = "";
                int Id_Prd = 0;
                string Prd_Descripcion = "";

                Id_Cons_AdeDet = generarGUI_IdAdeDet();
                Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_PrdAde") as RadNumericTextBox).Value);
                Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProductoAde") as RadComboBox).Text;

                if (Id_Prd == 0)
                {
                    Alerta("Elija un Producto Para poder Guardar");
                    e.Canceled = true;
                    return;
                }


                ArrayList al = new ArrayList();
                al.Add(Id_Cons_AdeDet);
                al.Add(Id_Prd);
                al.Add(Prd_Descripcion);

                bool falta_adenda = false;
                TextBox Txtadenda = new TextBox();
                string valor_adenda = "";
                ArrayList ok = new ArrayList();

                string adenda_faltante = "";
                foreach (AdendaDet det in ListDet)
                {
                    if (!ok.Contains(det.Id_AdeDet.ToString()))
                    {
                        ok.Add(det.Id_AdeDet.ToString());
                        Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                        valor_adenda = Txtadenda.Text.Replace("'", "");

                        if (valor_adenda == "" && det.Requerido)
                        {
                            adenda_faltante = det.Campo;
                            falta_adenda = true;
                            break;
                        }
                        else
                        {
                            al.Add(valor_adenda);

                        }
                    }
                }

                if (ListDetRF != null)
                {
                    foreach (AdendaDet det in ListDetRF)
                    {
                        if (!ok.Contains(det.Id_AdeDet.ToString()))
                        {
                            ok.Add(det.Id_AdeDet.ToString());
                            Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                            valor_adenda = Txtadenda.Text;

                            if (valor_adenda == "" && det.Requerido)
                            {
                                adenda_faltante = det.Campo;
                                falta_adenda = true;
                                break;
                            }
                            else
                            {
                                al.Add(valor_adenda);
                            }
                        }
                    }
                }

                if (falta_adenda)
                {
                    AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                    e.Canceled = true;
                }
                else
                {
                    ListaProductosFacturaAdenda.Rows.Add(al.ToArray());
                }
            }
            catch (Exception ex)
            {

                e.Canceled = true;
                Alerta(ex.Message);
                return;

            }
        }
        protected void rgFacturaDetAde_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string Id_Cons_AdeDet = "";
                int Id_Prd = 0;
                string Prd_Descripcion = "";


                Id_Cons_AdeDet = (insertedItem.FindControl("txtId_Cons_AdeDet") as RadTextBox).Text;
                Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_PrdAde") as RadNumericTextBox).Value);
                Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProductoAde") as RadComboBox).Text;


                DataRow[] Ar_dr;
                //Ar_dr = ListaProductosFacturaAdenda.Select("Id_Prd='" + Id_Prd + "'");
                Ar_dr = ListaProductosFacturaAdenda.Select("Id_Cons_AdeDet='" + Id_Cons_AdeDet + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Cons_AdeDet"] = Id_Cons_AdeDet;
                    Ar_dr[0]["Id_Prd"] = Id_Prd;
                    Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;


                    bool falta_adenda = false;
                    string valor_adenda = "";
                    TextBox Txtadenda = new TextBox();
                    //ADENDA FACTURACION
                    ArrayList ok = new ArrayList();
                    string adenda_faltante = "";

                    foreach (AdendaDet det in ListDet)
                    {
                        if (!ok.Contains(det.Id_AdeDet.ToString()))
                        {
                            ok.Add(det.Id_AdeDet.ToString());
                            Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                            valor_adenda = Txtadenda.Text.Replace("'", "");
                            bool addenda_Requerida = det.Requerido;//listDetT.Where(AdendaDet => AdendaDet.Id_AdeDet == det.Id_AdeDet).ToList()[0].Requerido;
                            if (valor_adenda == "" && addenda_Requerida)
                            {
                                adenda_faltante = det.Campo;
                                falta_adenda = true;
                                (insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox).Focus();
                                break;
                            }
                            else
                                Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                        }
                    }

                    //ADENDA REFACTURACION
                    if (ListDetRF != null && !falta_adenda)
                    {
                        foreach (AdendaDet det in ListDetRF)
                        {
                            if (!ok.Contains(det.Id_AdeDet.ToString()))
                            {
                                ok.Add(det.Id_AdeDet.ToString());
                                Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                bool addenda_Requerida = det.Requerido;//listDetT.Where(AdendaDet => AdendaDet.Id_AdeDet == det.Id_AdeDet).ToList()[0].Requerido;
                                valor_adenda = Txtadenda.Text;
                                if (valor_adenda == "" && addenda_Requerida)
                                {
                                    adenda_faltante = det.Campo;
                                    falta_adenda = true;
                                    (insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox).Focus();
                                    break;
                                }
                                else
                                    Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                            }
                        }
                    }
                    if (falta_adenda)
                    {
                        AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                        e.Canceled = true;
                    }
                    else
                        Ar_dr[0].AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                Alerta(ex.Message.Replace("'", ""));
                return;

            }
        }
        protected void rgFacturaDetAde_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();

                if (rgFacturaDetAde.EditItems.Count > 0)
                {
                    Alerta("Ya está editando un registro");
                    e.Canceled = true;
                }
                else
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string Id_Cons_AdeDet = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Cons_AdeDet"]);
                    //int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);                    
                    //actualizar producto de orden de compra a la lista
                    this.ListaProductosFacturaAdenda_EliminarProducto(Id_Cons_AdeDet);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFacturaDetAde_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {
                    GridDataItem editItem = (GridDataItem)e.Item;
                    TextBox txt;
                    try
                    {
                        if (ListDet != null)
                        {
                            foreach (AdendaDet det in ListDet)
                            {
                                if (editItem[det.Id_AdeDet.ToString()] != null)
                                {
                                    if (editItem[det.Id_AdeDet.ToString()].Controls.Count > 0)
                                    {
                                        txt = editItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                        txt.Attributes.Add("onkeypress", "return SoloAlfanumerico(this)");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        if (ListDetRF != null)
                        {
                            foreach (AdendaDet det in ListDetRF)
                            {
                                if (editItem[det.Id_AdeDet.ToString()] != null)
                                {
                                    if (editItem[det.Id_AdeDet.ToString()].Controls.Count > 0)
                                    {
                                        txt = editItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                        txt.Attributes.Add("onkeypress", "return SoloAlfanumerico(this)");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected bool ValidaPartidas(int ter, int prd, int cantidad)
        {
            try
            {
                ErrorManager();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                int disponible = 0;
                int invFinal = 0;
                int asignado = 0;
                int cantidad_B = 0;
                new CN_CapEntradaSalida().ConsultarDisponible(session, prd, ref disponible, ref invFinal, ref asignado);


                if (rgFacturaDet.Items.Count > 0)
                {

                    foreach (GridDataItem item in rgFacturaDet.Items) // loops through each rows in RadGrid
                    {

                        if (Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Ter"]) != ter
                            && Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]) == prd
                            )
                        {
                            cantidad_B += int.Parse((rgFacturaDet.MasterTableView.Items[item.ItemIndex]["Fac_Cant"].FindControl("lblOrd_Cantidad") as Label).Text);

                        }


                    }

                }



                int disponible_pedido = 0;
                #region pedido
                if (txtPedido.Text != "")
                {
                    CN_CapPedido cappedido = new CN_CapPedido();
                    Pedido pedido = new Pedido();
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Id_PedDet");
                    dt2.Columns.Add("Id_Ter");
                    dt2.Columns.Add("Ter_Nombre");
                    dt2.Columns.Add("Id_Prd");
                    dt2.Columns.Add("Prd_Descripcion");
                    dt2.Columns.Add("Prd_Presentacion"); dt2.Columns.Add("Prd_Unidad");
                    dt2.Columns.Add("Prd_Precio");
                    dt2.Columns.Add("Disponible");
                    dt2.Columns.Add("Prd_Importe");
                    dt2.Columns.Add("Id_Rem");
                    cappedido.ConsultaPedidoDetDisp(pedido, ref dt2, null, session.Emp_Cnx);

                    DataRow[] dr = dt2.Select("Id_Prd='" + prd + "'");
                    if (dr.Length > 0)
                    {
                        for (int i = 0; i < dr.Length; i++)
                            disponible_pedido += !string.IsNullOrEmpty(dr[i]["Disponible"].ToString()) ? Convert.ToInt32(dr[i]["Disponible"]) : 0;
                    }
                    if (disponible_pedido < 0)
                        disponible_pedido = 0;
                }
                #endregion


                //Cambio de Factura
                CN_CapFactura cn_factura = new CN_CapFactura();
                Factura fac = new Factura();
                fac.Id_Emp = session.Id_Emp;
                fac.Id_Cd = session.Id_Cd_Ver;
                fac.Id_Fac = (int)txtId.Value;
                List<FacturaDet> list = new List<FacturaDet>();
                cn_factura.ConsultaFactura(ref fac, ref list, session.Emp_Cnx);
                int count = 0;
                foreach (FacturaDet f in list)
                {
                    if (f.Id_Prd == prd)
                    {
                        count += f.Fac_Cant;
                    }
                }


                //Refactura
                int CantidadRefacturada = 0;
                if (Page.Request.QueryString["reFactura"] != null)
                {
                    if (Page.Request.QueryString["reFactura"].ToString() == "1")
                    {
                        CN_CapFactura cn_facturaref = new CN_CapFactura();
                        Factura facref = new Factura();
                        facref.Id_Emp = session.Id_Emp;
                        facref.Id_Cd = session.Id_Cd_Ver;
                        facref.Id_Fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                        List<FacturaDet> listref = new List<FacturaDet>();
                        cn_facturaref.ConsultaFactura(ref facref, ref listref, session.Emp_Cnx);
                        foreach (FacturaDet f in listref)
                        {
                            if (f.Id_Prd == prd)
                            {
                                CantidadRefacturada += f.Fac_Cant;
                            }
                        }
                    }
                }


                int canRem = 0;

                List<Remision> listaRemisionesCan = (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID];

                if (listaRemisionesCan != null)
                {
                    listaRemisionesCan = listaRemisionesCan.GroupBy(grp => new { /*grp.Id_Ter,*/ grp.Id_Prd })
                                                 .Select(g => new Remision
                                                 {
                                                     Id_Prd = g.Key.Id_Prd,
                                                     /*   Id_Ter = g.Key.Id_Ter,*/
                                                     Cant = g.Sum(a => a.Cant)
                                                 }).ToList();

                    if (listaRemisionesCan != null)
                    {
                        Remision enRemision = listaRemisionesCan.FirstOrDefault(x => x.Id_Prd == prd /*&& x.Id_Ter == ter*/);
                        if (enRemision != null)
                        {
                            canRem = enRemision.Cant;
                        }
                    }
                }



                //---si es REMISION DE PEDIDO
                //---Solo se puede Remisionar el dispobible del Pedido + disponible del producto
                if (txtPedido.Text != "")
                {
                    if ((cantidad + cantidad_B) > ((disponible_pedido + disponible) + count + CantidadRefacturada))
                    {
                        Alerta("En el Producto " + prd.ToString().Trim() + " Cantidad Disponible en el Pedido : " + disponible_pedido.ToString().Trim() + " Cantidad Disponible en el Producto : " + disponible.ToString().Trim() + " Cantidad Total Dispobible : " + (disponible_pedido + disponible).ToString().Trim() + "; Imposible Facturar más de la Cantidad Total Disponible.");
                        return false;
                    }
                }
                else
                {
                    if ((cantidad + cantidad_B) > (disponible + count + canRem + CantidadRefacturada))
                    {
                        if (canRem == 0)
                        {
                            Alerta("En el Producto " + prd.ToString().Trim() + " Cantidad Disponible en el Producto : " + (disponible + count).ToString().Trim() + "; Imposible Facturar más de la Cantidad Total en el Producto.");
                        }
                        else
                        {
                            Alerta("En el Producto " + prd.ToString().Trim() + " Cantidad Disponible en el Producto : " + (disponible + count).ToString().Trim() + " Cantidad de Remisión : " + canRem.ToString().Trim() + "; Imposible Facturar más de la Cantidad Total en el Producto.");
                        }
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                return false;
            }

        }


        //Refacturación Dic-2016
        //Raúl Bórquez Martínez
        //Inicio

        protected void ChkRefacturatotal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkRefacturatotal.Checked == true)
                {
                    ChkRefacturaparcial.Checked = false;
                    //RadTabStrip1.Tabs[1].Selected = true;
                    //RadPageViewDetalles.Selected = true;
                    rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn").Display = false;
                    rgFacturaDet.Columns.FindByUniqueName("DeleteColumn").Display = false;
                    txtCausaRef.Enabled = true;
                    cmbCausaRef.Enabled = true;
                    cmbCausaRef.SelectedIndex = -1;
                    RadToolBar1.Enabled = true;
                    txtCausaRef.Text = "";
                    this.CargarComboCausaRafactura(0);
                    this.txtCausaRef.Focus();
                }
                else
                {
                    txtCausaRef.Text = "";
                    cmbCausaRef.SelectedValue = "";
                    txtCausaRef.Enabled = false;
                    cmbCausaRef.Enabled = false;
                }
            }
            catch { }
        }
        protected void HabilitarcamposRefacturaParcial()
        {
            ChkRefacturatotal.Checked = false;
            txtCausaRef.Enabled = true;
            cmbCausaRef.Enabled = true;
            cmbCausaRef.SelectedIndex = -1;
            cmbTerritorio.Enabled = true;
            txtTerritorio.Enabled = true;
            this.rgFacturaDet.Enabled = true;
            RadToolBar1.Enabled = true;
            this.CargarComboCausaRafactura(1);
            this.txtCausaRef.Focus();
            txtCausaRef.Text = "";
            rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn").Display = true;
            rgFacturaDet.Columns.FindByUniqueName("DeleteColumn").Display = true;
        }
        protected void ChkRefacturaparcial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkRefacturaparcial.Checked == true)
                {
                    HabilitarcamposRefacturaParcial();
                }
                else
                {
                    txtCausaRef.Text = "";
                    cmbCausaRef.SelectedValue = "";
                    txtCausaRef.Enabled = false;
                    cmbCausaRef.Enabled = false;
                    cmbTerritorio.Enabled = false;
                    txtTerritorio.Enabled = false;
                }
            }
            catch { }

        }

        //Refacturación Dic-2016
        //Raúl Bórquez Martínez
        //Fin


        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            int prd = 0;
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox Txtcantidad = (sender as RadNumericTextBox);
                int canRem = 0;
                int cantidad = (Txtcantidad.Parent.Parent.FindControl("txtFac_Cantidad") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtFac_Cantidad") as RadNumericTextBox).Value.Value) : 0;
                prd = (Txtcantidad.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Value.Value) : 0;
                int ter = (Txtcantidad.Parent.Parent.FindControl("txtTerritorioPartida") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtTerritorioPartida") as RadNumericTextBox).Value.Value) : 0;
                GridDataItem di = Txtcantidad.Parent.Parent as GridDataItem;

                List<Remision> listaRemisionesCan = (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID];

                if (listaRemisionesCan != null)
                {
                    listaRemisionesCan = listaRemisionesCan.GroupBy(grp => new { grp.Id_Ter, grp.Id_Prd })
                                                 .Select(g => new Remision
                                                 {
                                                     Id_Prd = g.Key.Id_Prd,
                                                     Id_Ter = g.Key.Id_Ter,
                                                     Cant = g.Sum(a => a.Cant)
                                                 }).ToList();

                    if (listaRemisionesCan != null)
                    {
                        Remision enRemision = listaRemisionesCan.FirstOrDefault(x => x.Id_Prd == prd && x.Id_Ter == ter);
                        if (enRemision != null)
                        {
                            canRem = enRemision.Cant;
                            di["Rem_Cant"].Text = canRem.ToString();
                        }
                    }
                }

                if (cantidad < canRem)
                {
                    Alerta("La cantidad no puede ser menor a la de la remisión.");
                    Txtcantidad.Text = canRem.ToString();
                    return;
                }


                /*              
                              int disponible_pedido = 0;
                              #region pedido
                              if (txtPedido.Text != "")
                              {
                                  CN_CapPedido cappedido = new CN_CapPedido();
                                  Pedido pedido = new Pedido();
                                  pedido.Id_Emp = sesion.Id_Emp;
                                  pedido.Id_Cd = sesion.Id_Cd_Ver;
                                  pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);

                                  DataTable dt2 = new DataTable();
                                  dt2.Columns.Add("Id_PedDet");
                                  dt2.Columns.Add("Id_Ter");
                                  dt2.Columns.Add("Ter_Nombre");
                                  dt2.Columns.Add("Id_Prd");
                                  dt2.Columns.Add("Prd_Descripcion");
                                  dt2.Columns.Add("Prd_Presentacion");
                                  dt2.Columns.Add("Prd_Unidad");
                                  dt2.Columns.Add("Prd_Precio");
                                  dt2.Columns.Add("Disponible");
                                  dt2.Columns.Add("Prd_Importe");
                                  dt2.Columns.Add("Id_Rem");
                                  //cappedido.ConsultaPedidoDetDisp(pedido, ref dt2, 1, sesion.Emp_Cnx);

                                  //DataRow[] dr = dt2.Select("Id_Prd='" + prd + "'");//"Id_Ter='" + ter + "' and 

                                  //if (dr.Length > 0)
                                  //{
                                  //    for (int i = 0; i < dr.Length; i++)
                                  //        disponible_pedido += !string.IsNullOrEmpty(dr[i]["Disponible"].ToString()) ? Convert.ToInt32(dr[i]["Disponible"]) : 0;
                                  //}

                                  cappedido.ConsultaPedidoDisp(pedido, prd, sesion.Emp_Cnx, ref disponible_pedido);

                                  if (disponible_pedido < 0)
                                      disponible_pedido = 0;
                              }
                              #endregion
                              cantidad_A = Convert.ToInt32(Session["CantidadEditar" + Session.SessionID]);
                              Id_Rem_A = Convert.ToInt32(Session["Remision" + Session.SessionID]);

                              CN_CatProducto cn_producto = new CN_CatProducto();
                              List<int> actuales = new List<int>();
                              cn_producto.ConsultaProducto_Disponible(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ref actuales, sesion.Emp_Cnx);

                              int cantidad_B = 0;
                              DataRow[] Dr2 = objdtTabla.Select("Id_Prd='" + prd + "' and Id_Ter <> '" + ter + "'");
                              if (Dr2.Length > 0)
                              {
                                  for (int i = 0; i < Dr2.Length; i++)
                                      cantidad_B += !string.IsNullOrEmpty(Dr2[i]["Fac_Cant"].ToString()) ? Convert.ToInt32(Dr2[i]["Fac_Cant"]) : 0;
                              }
                

                              int cantRemision = 0;
                              List<Remision> listaRemisiones;
                              CN_CapRemision remision = new CN_CapRemision();
                              listaRemisiones = new List<Remision>();
                              remision.ConsultaRemisionesxFactura(sesion, Convert.ToInt32(txtId.Text), ref listaRemisiones);
                              if (listaRemisiones.Count != 0)
                              {
                                  CN_CapFactura cn_fact = new CN_CapFactura();
                                  int disponibleFacturar = 0;
                                  Factura fac2 = new Factura();
                                  fac2.Id_Fac = (int)txtId.Value;
                                  fac2.Id_Rem = Id_Rem_A;
                                  cn_fact.DisponibleFacturar(sesion, fac2, prd, ref disponibleFacturar);
                                  cantRemision += disponibleFacturar;
                              }
                              else
                                  if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                                  {

                                      listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                                      foreach (Remision rem in listaRemisiones)
                                      {
                                          arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                                      }
                                      if (arrayRemisiones.Length > 1)
                                          arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);

                                      CN_CapRemision cr = new CN_CapRemision();
                                      cantRemision = cr.ConsultaCantidadRemision(sesion, prd, arrayRemisiones);
                                      if (cantRemision == 0)
                                      {
                                          CN_CapFactura cn_factura = new CN_CapFactura();
                                          Factura fac = new Factura();
                                          fac.Id_Emp = sesion.Id_Emp;
                                          fac.Id_Cd = sesion.Id_Cd_Ver;
                                          fac.Id_Fac = (int)txtId.Value;
                                          List<FacturaDet> list = new List<FacturaDet>();
                                          cn_factura.ConsultaFactura(ref fac, ref list, sesion.Emp_Cnx);
                                          int count = 0;
                                          foreach (FacturaDet f in list)
                                          {
                                              if (f.Id_Prd == prd)
                                              {
                                                  count += f.Fac_Cant;
                                              }
                                          }
                                          cantRemision = count - cantidad_A;
                                      }
                                  }
                                  else if(Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null)
                                  {                        
                                      listaRemisiones = (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID];

                                      Remision vRemCan = listaRemisiones.GroupBy(grp => grp.Id_Prd)
                                                                        .Select(g => new Remision
                                                                        {
                                                                            Id_Prd = g.Key,
                                                                            Cant = g.Sum(a => a.Cant)
                                                                        })
                                                                        .Where(x => x.Id_Prd == prd)
                                                                        .FirstOrDefault();

                                      if (vRemCan != null)
                                      {
                                          cantRemision = vRemCan.Cant;
                                      }
                                  }
                                  else
                                  {
                                      int fac1 = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                                      if (fac1 != -1)
                                      {
                                          CN_CapFactura cn_factura = new CN_CapFactura();
                                          Factura fac = new Factura();
                                          fac.Id_Emp = sesion.Id_Emp;
                                          fac.Id_Cd = sesion.Id_Cd_Ver;
                                          fac.Id_Fac = (int)txtId.Value;
                                          List<FacturaDet> list = new List<FacturaDet>();
                                          cn_factura.ConsultaFactura(ref fac, ref list, sesion.Emp_Cnx);
                                          int count = 0;
                                          foreach (FacturaDet f in list)
                                          {
                                              if (f.Id_Prd == prd)
                                              {
                                                  count += f.Fac_Cant;
                                              }
                                          }
                                          cantRemision = count;
                                      }
                                  }
                              if (Convert.ToInt32(txtMov.Text) != 70)
                              {
                                  if (cantidad + cantidad_B > actuales[2] + disponible_pedido + cantRemision)//cantidad_A)
                                  {
                                      this.AlertaFocus(string.Concat("Producto "
                                              , prd.ToString()
                                              , ", inventario disponible insuficiente.<br/>Inventario final: "
                                              , actuales[0].ToString()
                                              , "<br/>Asignado: "
                                              , actuales[1].ToString()
                                              , "<br/>Disponible: "
                                              , (actuales[2] + disponible_pedido + cantRemision).ToString()), Txtcantidad.ClientID);//cantidad_A)
                                      Txtcantidad.Text = "";
                                      return;
                                  }
                                  else
                                  {
                                      (Txtcantidad.Parent.Parent.FindControl("txtFac_Precio") as RadNumericTextBox).Focus();
                                  }
                              }
                              else
                              {
                                  int cte_ext = (Txtcantidad.Parent.Parent.FindControl("txtClienteExterno") as RadNumericTextBox).Value.HasValue ? Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtClienteExterno") as RadNumericTextBox).Value.Value) : 0;


                                  CN_CapEntradaSalida cn_entsal = new CN_CapEntradaSalida();
                                  int verificador = 0;
                                  int disponible = 0;
                                  cn_entsal.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ter.ToString(), cte_ext.ToString(), sesion.Emp_Cnx, ref verificador, "14");
                                  disponible = verificador;
                                  verificador = 0;
                                  cn_entsal.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ter.ToString(), cte_ext.ToString(), sesion.Emp_Cnx, ref verificador, "");
                                  disponible += verificador;

                                  if (cantidad + cantidad_B > disponible)//cantidad_A)
                                  {
                                      this.AlertaFocus(string.Concat("Producto "
                                              , prd.ToString()
                                              , ", Saldo disponible: "
                                              , (disponible).ToString()), Txtcantidad.ClientID);//cantidad_A)
                                      Txtcantidad.Text = "";
                                      return;
                                  }
                                  else
                                  {
                                      (Txtcantidad.Parent.Parent.FindControl("txtFac_Precio") as RadNumericTextBox).Focus();
                                  }
                              }
                              string mensajeInventarioExcepcion = string.Empty;

                              //validar cantidad de producto de partida contra cantidad de remisión si es que la factura es de remisiones               
                              if (Id_Rem_A != 0) //es facturacion de remision
                              {
                                  if (cantidad > actuales[2] + disponible_pedido + Rem_Cant_A + cantRemision)// Rem_Cant_A)
                                      mensajeInventarioExcepcion = "rgFacturaDet_InvFinalRemisionInsuficiente";
                              }
                              if (mensajeInventarioExcepcion != string.Empty)
                                  throw new Exception(mensajeInventarioExcepcion);
                
                              if (txtMov.Text == "51" && actuales[2] + disponible_pedido + Rem_Cant_A  + cantRemision > cantidad)
                              {
                                  int agrupado = -1;
                                  int CantT = 0;

                                  Producto prod = new Producto();
                                  prod.Id_Cte = Convert.ToInt32(txtCliente.Value);
                                  cn_producto.ConsultaProducto(ref prod, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, prd);
                                  agrupado = prod.Prd_AgrupadoSpo;

                                  if (agrupado != -1)
                                  {
                                      foreach (DataRow dr in objdtTabla.Rows)
                                      {
                                          prod = new Producto();
                                          cn_producto.ConsultaProducto(ref prod, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(dr["Id_Prd"]));

                                          if (prod.Prd_AgrupadoSpo == agrupado)
                                              CantT += Convert.ToInt32(dr["Fac_Cant"]);
                                      }
                                  }
                                  CantT -= cantidad_A;
                                  CantT += cantidad;
                                  CN_CapFactura cn_factura = new CN_CapFactura();
                                  Factura fac = new Factura();
                                  fac.Id_Emp = sesion.Id_Emp;
                                  fac.Id_Cd = sesion.Id_Cd_Ver;
                                  fac.Id_Fac = (int)txtId.Value;
                                  List<FacturaDet> list = new List<FacturaDet>();
                                  cn_factura.ConsultaFactura(ref fac, ref list, sesion.Emp_Cnx);

                                  int CantOriginal = 0;
                                  foreach (FacturaDet fd in list)
                                  {
                                      if (fd.Prd_Agrupador == agrupado)
                                          CantOriginal += fd.Fac_Cant;
                                  }
                                  CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                                  int saldo = 0;
                                  CNentrada.ConsultarSaldo(sesion.Id_Emp, sesion.Id_Cd_Ver, prd.ToString(), ter.ToString(), txtCliente.Text, sesion.Emp_Cnx, ref saldo, "14");
                              }

              */

                if (ValidaPartidas(ter, prd, cantidad) == false)
                {
                    Txtcantidad.Text = "0";
                    Txtcantidad.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("rgFacturaDet_InvFinalRemisionInsuficiente"))
                {
                    this.Alerta(string.Concat("Producto "
                    , prd.ToString()
                    , ", inventario disponible insuficiente.<br/>Remisionado: ", Rem_Cant_A.ToString()
                    , "<br/>Disponible: ", Rem_Cant_A.ToString()));
                    return;
                }
                else
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (this.ConsultarDatosCliente(txtCliente.Text, false))
                {

                    //aqui

                    if (Page.Request.QueryString["reFactura"] == "0" || Page.Request.QueryString["reFactura"] == null)
                    {
                        CargarComboTerritorios();
                        rgFacturaDet.Rebind();
                        rgFacturaDetAde.Rebind();
                        rgAdendaFacturacion.Rebind();
                        Consultar_IVA_Cliente();
                        txtTerritorio.Focus();
                    }
                    else
                    {
                        CargarComboTerritorios();
                        txtTerritorio.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void txtClienteNacional_TextChanged(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CentroDistribucion cd = new CentroDistribucion();
            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
            SIANWEB.EnviaCuentaNacional.EnviaCuentaNacional servicio = new EnviaCuentaNacional.EnviaCuentaNacional();
            string resultado = servicio.CtaNacional(cd.Cd_Rfc, sesion.Id_Cd_Ver, Int32.Parse(txtClienteNacional.Text));

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(resultado);

            foreach (XmlNode xmlClientes in xml.ChildNodes)
            {
                if (xmlClientes.Name == "CuentasNacionales")
                {
                    foreach (XmlNode xmlCliente in xmlClientes.ChildNodes)
                    {
                        if (xmlCliente.Name == "CuentaNacional")
                        {
                            txtClienteNacional.Text = xmlCliente.Attributes["CliNum"].Value;
                            txtClienteNacionalNombre.Text = xmlCliente.Attributes["CliNom"].Value;
                            TxtClienteNacionalCalle.Text = xmlCliente.Attributes["Calle"].Value;
                            TxtClienteNacionalNoExterior.Text = xmlCliente.Attributes["NoExterior"].Value;
                            TxtClienteNacionalColonia.Text = xmlCliente.Attributes["Colonia"].Value;
                            TxtClienteNacionalMunicipio.Text = xmlCliente.Attributes["Municipio"].Value;
                            TxtClienteNacionalEstado.Text = xmlCliente.Attributes["Estado"].Value;// 
                            TxtClienteNacionalCp.Text = xmlCliente.Attributes["CodPost"].Value;
                            TxtClienteNacionalRfc.Text = xmlCliente.Attributes["RFC"].Value;
                            TxtClienteNacionalAdenda.Text = xmlCliente.Attributes["NombreAddenda"].Value;

                            ListDetNacional = new List<AdendaDet>();
                            InicializarTablaDetallesAdendaNacional();

                            if (TxtClienteNacionalAdenda.Text.Trim() != string.Empty)
                            {
                                RadTabStrip1.Tabs[5].Visible = true;

                                ConsultarDatosAdenda(TxtClienteNacionalAdenda.Text.Trim());
                                rgAdendaFacturacionNacional.Rebind();
                                rgFacturaDetAdeNacional.Rebind();
                            }

                            break;
                        }
                    }
                }
            }
        }
        protected void txtDescuento1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                this.CalcularTotales();
                txtDescPorc1.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtDescuento2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                this.CalcularTotales();
                txtDescPorc2.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;
                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)tabla.FindControl("txtTerritorioPartida");
                RadTextBox txtPrdDescripcion = (RadTextBox)tabla.FindControl("txtProductoNombre");
                RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion");
                RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion");
                RadTextBox txtPrd_UniNe = (RadTextBox)tabla.FindControl("txtPrd_UniNe");
                RadNumericTextBox txtFac_Cantidad = (RadNumericTextBox)tabla.FindControl("txtFac_Cantidad");
                RadNumericTextBox txtFac_Precio = (RadNumericTextBox)tabla.FindControl("txtFac_Precio");

                if (objdtTabla.Select("Id_Prd='" + combo.Value.ToString() + "' and Id_Ter='" + txtFac_Territorio.Value.ToString() + "'").Length > 0)
                {
                    AlertaFocus("El producto ya ha sido agregado a la factura", combo.ClientID);
                    combo.Text = "";
                    txtFac_Territorio.Text = "";
                    txtPrdDescripcion.Text = "";
                    txtPrd_Descripcion.Text = "";
                    txtPrd_Presentacion.Text = "";
                    txtPrd_UniNe.Text = "";
                    txtFac_Cantidad.Text = "";
                    txtFac_Precio.Text = "";
                    return;
                }

                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int id_Cd_Prod = sesion.Id_Cd_Ver;

                Producto producto = new Producto();
                if (combo.Value.HasValue)
                {
                    //esta variable guardará el precio de producto aceptado para la partida
                    //puede ser el precio publico del catalogo de producto
                    //o el precio publico del catalogo de cliente-producto
                    //o el precio AAA de una solicitud de precios especiales
                    double precioProductoAceptado = 0;

                    //obtener datos de producto
                    CN_CatProducto clsProducto = new CN_CatProducto();
                    producto.Id_Ter = Convert.ToInt32(txtFac_Territorio.Value.HasValue ? txtFac_Territorio.Value : -1);
                    try
                    {
                        producto.Id_Cte = Convert.ToInt32(txtCliente.Value);
                        producto.EmpBen = strEmp == "" ? (int?)null : Convert.ToInt32(strEmp);
                        clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(combo.Value), 1);
                    }
                    catch (Exception ex)
                    {
                        combo.Text = "";
                        txtPrdDescripcion.Text = "";
                        txtPrd_Descripcion.Text = "";
                        txtPrd_Presentacion.Text = "";
                        txtPrd_UniNe.Text = "";
                        txtFac_Cantidad.Text = "";
                        txtFac_Precio.Text = "";
                        AlertaFocus(ex.Message, combo.ClientID);
                        return;
                    }
                    //obtener precio de producto
                    float precioPublico = 0;
                    new CN_ProductoPrecios().ConsultaListaProductoPrecioPUBLICO(ref precioPublico, sesion, Convert.ToInt32(combo.Value));

                    //obtener precio especial de producto
                    //desde el catálogo CAT_CLIENTEPRODUCTO
                    float precioPublicoCAT_CLIENTEPRODUCTO = 0;
                    ClienteProd clienteProd = new ClienteProd();
                    clienteProd.Id_Emp = sesion.Id_Emp;
                    clienteProd.Id_Cd = sesion.Id_Cd_Ver;
                    clienteProd.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                    clienteProd.Id_Prd = Convert.ToInt32(combo.Value);
                    new CN_CatClienteProd().ClienteProductoPrecioPublico_Consultar(ref clienteProd, sesion.Emp_Cnx, ref precioPublicoCAT_CLIENTEPRODUCTO);

                    precioProductoAceptado = precioPublicoCAT_CLIENTEPRODUCTO > 0 ? precioPublicoCAT_CLIENTEPRODUCTO : precioPublico;


                    //JMM: Se modifica ahora toma los convenios de precios especiales
                    #region anteriorprecios
                    //////obtener SOLICITUDES DE PRECIOS ESPECIALES vencidas
                    ////List<VentanaPrecioEspecialPro> listaPrecioEspecial = new List<VentanaPrecioEspecialPro>();
                    ////new CN_PrecioEspecial().PrecioEspecialSolicitudesVencidas_Consulta(ref listaPrecioEspecial, sesion.Emp_Cnx, sesion.Id_Emp
                    ////    , sesion.Id_Cd_Ver
                    ////    , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                    ////    , Convert.ToInt32(combo.Value)
                    ////    /*, Convert.ToInt32(cmbMoneda.SelectedValue)*/);

                    //obtener precio especial del producto 
                    //para el cliente actual de la factura
                    //desde la CAPTURA de SOLICITUDES DE PRECIOS ESPECIALES
                    //VentanaPrecioEspecialPro precioEspecialPro = null;
                    //new CN_PrecioEspecial().PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, sesion.Emp_Cnx, sesion.Id_Emp
                    //    , sesion.Id_Cd_Ver
                    //    , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                    //    , Convert.ToInt32(combo.Value)
                    //    /* ,Convert.ToInt32(cmbMoneda.SelectedValue) */);
                    //if (precioEspecialPro != null && precioEspecialPro.Ape_PreEsp > 0)
                    //{ //mensaje de vencimiento de solicitud de precio especial
                    //    string mensajePreciEspecialVencimiento = string.Concat("Faltan solo "
                    //        , ((TimeSpan)precioEspecialPro.Ape_FecFin.Subtract(precioEspecialPro.Ape_FecInicio)).Days.ToString()
                    //        , " día(s) para que venzan producto(s) con precio especial de la solicitud "
                    //        , precioEspecialPro.Id_Ape.ToString()
                    //        , " de precios especiales.<br/><br/>");
                    //    if (listaPrecioEspecial.Count > 0)
                    //    {
                    //        for (int i = 0; i < listaPrecioEspecial.Count; i++)
                    //        {
                    //            mensajePreciEspecialVencimiento = string.Concat(mensajePreciEspecialVencimiento
                    //            , "La solicitud de precios especiales ", listaPrecioEspecial[i].Id_Ape.ToString()
                    //            , " tiene productos con "
                    //            , ((TimeSpan)new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Subtract(precioEspecialPro.Ape_FecInicio)).Days.ToString()
                    //            , " días vencidos.<br/>");
                    //        }
                    //    }
                    //    mensajePreciEspecialVencimiento = string.Concat(mensajePreciEspecialVencimiento
                    //        , "<br/>Los productos sin actualizar el Precio AAA Especial, impactan directamente en los cálculos de utilidad del CDI"
                    //        , " y por ende, en los sistemas de compensación de todo el personal.");

                    //    //validar precio de venta (de catClienteProducto) es diferente al precio especial de la solicitud de precios especiales
                    //    //se toma como precio aceptado al precio de catClienteProducto y  se manda mensaje
                    //    if (precioProductoAceptado != precioEspecialPro.Ape_PreVta)
                    //    {
                    //        mensajePreciEspecialVencimiento = string.Concat(mensajePreciEspecialVencimiento
                    //            , "<br/><br/>El precio especial autorizado para este producto en la solicitud "
                    //            , precioEspecialPro.Id_Ape
                    //            , " no se tomará en cuenta, ya que el precio de venta no es el mismo al convenido que es de "
                    //            , precioEspecialPro.Ape_PreVta);
                    //    }
                    //    else
                    //    {   /*
                    //         * NOTA: si el precio está en dólares u otro tipo de moneda, 
                    //         * se hace la conversión al tipo de moneda de la factura que se está capturando.
                    //         */
                    //        if (precioEspecialPro.Id_Mon != Convert.ToInt32(cmbMoneda.SelectedValue))
                    //        { //Consultar tipo de cambio
                    //            TipoMoneda tipoMoneda = new TipoMoneda();
                    //            List<TipoMoneda> listaTipoMoneda = new List<TipoMoneda>();
                    //            double tipoCambioFactura = 0, tipoCambioPrecioEspecial = 0;
                    //            new CN_CatTipoMoneda().ConsultaTipoMoneda(tipoMoneda, sesion.Id_Emp, sesion.Emp_Cnx, ref listaTipoMoneda);
                    //            foreach (TipoMoneda tm in listaTipoMoneda)
                    //            {
                    //                if (tm.Id_Mon == Convert.ToInt32(cmbMoneda.SelectedValue))
                    //                    tipoCambioFactura = tm.Mon_TipCambio;
                    //                if (tm.Id_Mon == precioEspecialPro.Id_Mon)
                    //                    tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                    //            }
                    //            precioProductoAceptado = (precioEspecialPro.Ape_PreEsp * tipoCambioPrecioEspecial) / tipoCambioFactura;
                    //        }
                    //        else
                    //            precioProductoAceptado = precioEspecialPro.Ape_PreEsp;
                    //    }
                    //    this.AlertaFocus2(mensajePreciEspecialVencimiento, txtFac_Cantidad.ClientID);
                    //}
                    //Finalmente introduce el precio de producto aceptado para la partida
                    //    txtFac_Precio.Text = precioProductoAceptado.ToString();
                    #endregion

                    string mensajePreciEspecialVencimiento = string.Empty;

                    CN_Convenio cn_conv = new CN_Convenio();
                    ConvenioDet conv = new ConvenioDet();
                    ConvenioDet convdet = new ConvenioDet();
                    string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                    conv.Id_Emp = sesion.Id_Emp;
                    conv.Id_Cd = sesion.Id_Cd_Ver;
                    conv.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                    conv.Id_Prd = Convert.ToInt32(combo.Value);


                    cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                    if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                    {
                        //JMM: si el minimo y el maximo son diferentes a 0 quiere decir que hay unprecio convenido
                        if (convdet.PCD_PrecioVtaMin != 0 && convdet.PCD_PrecioVtaMax != 0)
                        {
                            //if (precioProductoAceptado < convdet.PCD_PrecioVtaMin || precioProductoAceptado > convdet.PCD_PrecioVtaMax)
                            //{
                            //      mensajePreciEspecialVencimiento = string.Concat(mensajePreciEspecialVencimiento
                            //        , "El precio especial autorizado para este producto en el convenio <b>"
                            //        , convdet.PC_NoConvenio + "-" + convdet.PC_Nombre 
                            //        , "</b> no se tomará en cuenta, ya que el precio de venta no esta dentro del minimo y máximo convenido que es:  "
                            //        , "<b>Min. ", convdet.PCD_PrecioVtaMin , " Max. " , convdet.PCD_PrecioVtaMax  + ".</b>");

                            //      this.AlertaFocus2(mensajePreciEspecialVencimiento, txtFac_Cantidad.ClientID);
                        }
                        //else
                        //{   /*
                        //     * NOTA: si el precio está en dólares u otro tipo de moneda, 
                        //     * se hace la conversión al tipo de moneda de la factura que se está capturando.
                        //     */
                        //    if (convdet.Id_Moneda != Convert.ToInt32(cmbMoneda.SelectedValue))
                        //    { //Consultar tipo de cambio
                        //        TipoMoneda tipoMoneda = new TipoMoneda();
                        //        List<TipoMoneda> listaTipoMoneda = new List<TipoMoneda>();
                        //        double tipoCambioFactura = 0, tipoCambioPrecioEspecial = 0;
                        //        new CN_CatTipoMoneda().ConsultaTipoMoneda(tipoMoneda, sesion.Id_Emp, sesion.Emp_Cnx, ref listaTipoMoneda);
                        //        foreach (TipoMoneda tm in listaTipoMoneda)
                        //        {
                        //            if (tm.Id_Mon == Convert.ToInt32(cmbMoneda.SelectedValue))
                        //                tipoCambioFactura = tm.Mon_TipCambio;
                        //            if (tm.Id_Mon == convdet.Id_Moneda)
                        //                tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                        //        }
                        //        precioProductoAceptado = (double.Parse(convdet.PCD_PrecioAAAEsp.ToString()) * tipoCambioPrecioEspecial) / tipoCambioFactura;
                        //    }
                        //    else
                        //    {
                        //        precioProductoAceptado = double.Parse(convdet.PCD_PrecioAAAEsp.ToString());
                        //    }
                        //}
                    }
                    //else
                    //{   /*
                    //         * NOTA: si el precio está en dólares u otro tipo de moneda, 
                    //         * se hace la conversión al tipo de moneda de la factura que se está capturando.
                    //         */
                    //    if (convdet.Id_Moneda != Convert.ToInt32(cmbMoneda.SelectedValue))
                    //    { //Consultar tipo de cambio
                    //        TipoMoneda tipoMoneda = new TipoMoneda();
                    //        List<TipoMoneda> listaTipoMoneda = new List<TipoMoneda>();
                    //        double tipoCambioFactura = 0, tipoCambioPrecioEspecial = 0;
                    //        new CN_CatTipoMoneda().ConsultaTipoMoneda(tipoMoneda, sesion.Id_Emp, sesion.Emp_Cnx, ref listaTipoMoneda);
                    //        foreach (TipoMoneda tm in listaTipoMoneda)
                    //        {
                    //            if (tm.Id_Mon == Convert.ToInt32(cmbMoneda.SelectedValue))
                    //                tipoCambioFactura = tm.Mon_TipCambio;
                    //            if (tm.Id_Mon == convdet.Id_Moneda)
                    //                tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                    //        }
                    //        precioProductoAceptado = (double.Parse(convdet.PCD_PrecioAAAEsp.ToString())  * tipoCambioPrecioEspecial) / tipoCambioFactura;
                    //    }
                    //    else
                    //    {
                    //        precioProductoAceptado = double.Parse(convdet.PCD_PrecioAAAEsp.ToString());
                    //    }
                    //}

                    //this.AlertaFocus2(mensajePreciEspecialVencimiento, txtFac_Cantidad.ClientID);
                    //}
                    //Finalmente introduce el precio de producto aceptado para la partida
                    txtFac_Precio.Text = Math.Round(precioProductoAceptado, 3).ToString();
                }

                txtPrdDescripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                txtPrd_Descripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                txtPrd_Presentacion.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                txtPrd_UniNe.Text = producto == null ? string.Empty : producto.Prd_UniNe;
                //--------controles auxiliares--------
                //establecer unidades de empaque
                HD_Prd_UniEmp.Value = producto == null ? string.Empty : producto.Prd_UniEmp.ToString();
                HD_Prd_InvFinal.Value = producto == null ? string.Empty : producto.Prd_InvFinal.ToString();
                HD_Prd_Asignado.Value = producto == null ? string.Empty : producto.Prd_Asignado.ToString();
                HD_Prd_Disponible.Value = producto == null ? string.Empty : (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
                //    HD_Prd_Disponible.Value = producto == null ? string.Empty : ((producto.Prd_InvFinal - producto.Prd_Asignado) + producto.Prd_InvFinal).ToString();

                //este evento es porque se elige producto, por lo que 
                //se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                txtFac_Cantidad.Enabled = true;
                txtFac_Cantidad.Text = string.Empty;
                if (combo.Value.HasValue)
                    txtFac_Cantidad.Focus();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtTerritorio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                if (txtMov.Text == "70")
                {
                    if (objdtTabla.Select("Id_Ter<>'" + Convert.ToInt32(combo.Value.ToString()) + "'").Length > 0)
                    {
                        AlertaFocus("El territorio no puede ser diferente al ya capturado en el detalle", combo.ClientID);
                        combo.Text = "";
                        ((RadComboBox)combo.Parent.FindControl("cmbTerritorioPartida")).SelectedIndex = 0;
                        return;
                    }

                }
                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)combo.Parent.FindControl("txtId_Prd");
                if (combo.Value.HasValue)
                    txtFac_Territorio.Focus();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtClienteExterno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                RadNumericTextBox combo = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                RadTextBox nombre = combo.Parent.FindControl("txtNombreCliente") as RadTextBox;

                if (objdtTabla.Select("Id_CteExt<>'" + Convert.ToInt32(combo.Value.ToString()) + "'").Length > 0)
                {
                    AlertaFocus("El cliente externo no puede ser diferente al ya capturado en el detalle", combo.ClientID);
                    combo.Text = "";
                    nombre.Text = "";
                    return;
                }

                if (combo.Value.ToString() == txtCliente.Text)
                {
                    AlertaFocus("El cliente externo no puede ser igual al capturado en los datos generales", combo.ClientID);
                    combo.Text = "";
                    nombre.Text = "";
                    return;
                }
                //obtiene la tabla contenedora de los controles de edición de registro del Grid
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Clientes cliente = new Clientes();
                if (combo.Value.HasValue)
                {
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Rik = sesion.Id_Rik;
                    cliente.Id_Cte = Convert.ToInt32(combo.Value.ToString());
                    try
                    {
                        new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    }
                    catch (Exception ex)
                    {
                        AlertaFocus(ex.Message, combo.ClientID);
                        combo.Text = "";
                        return;
                    }

                    RadTextBox txtNombreCliente = (RadTextBox)tabla.FindControl("txtNombreCliente");

                    txtNombreCliente.Text = cliente.Cte_NomComercial;
                    RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)tabla.FindControl("txtTerritorioPartida");
                    if (combo.Value.HasValue)
                        txtFac_Territorio.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void chkDesgloceIva_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                IVA.Visible = chkDesgloce.Checked;

                if (chkDesgloce.Checked)
                {
                    Consultar_IVA_Cliente();
                }
                else
                {
                    HD_IVAfacturacion.Value = "0";
                    txtIVA.Text = "0";
                }
                CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void chkFacturarCuentaNacional_CheckedChanged(object sender, EventArgs e)
        {
            RadTabStrip1.Tabs[4].Visible = chkFacturarCuentaNacional.Checked;
        }

        protected void ImgBuscarClienteNacional_Click(object sender, EventArgs e)
        {
            if (ClienteSIAN == "")
                RAM1.ResponseScripts.Add("popup(true);");
            else
                RAM1.ResponseScripts.Add("popup_CC('" + ClienteSIAN + "');");
        }


        private void Consultar_IVA_Cliente()
        {
            string IVA_Cliente = "NO";
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtCliente.Text != string.Empty && txtCliente.Text != "-1")
            {
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Text);
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                if (cliente.BPorcientoIVA == true)
                {
                    if (cliente.PorcientoIVA == 0 || cliente.PorcientoIVA == null)
                    {
                        Alerta("El porcentaje de IVA no está establecido, debe ser Mayor a Cero");
                        return;
                    }
                    else
                    {
                        HD_IVAfacturacion.Value = cliente.PorcientoIVA.ToString();
                        IVA_Cliente = "SI";
                    }
                }
            }

            if (IVA_Cliente == "NO")
            {
                // Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                HD_IVAfacturacion.Value = cd.Cd_IvaPedidosFacturacion.ToString();
            }
        }
        protected void consultarRetencion()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtCliente.Text != string.Empty && txtCliente.Text != "-1")
            { //Consultar clientes
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Text);
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();

                    if (cliente.PorcientoRetencion == 0 || cliente.PorcientoRetencion == null)
                    {
                        Alerta("El porcentaje de Retencion no está establecido, no se guardará el importe de Retención");
                        chkRetencion.Checked = false;
                        txtPorcRetencion.Visible = false;
                    }
                    else
                    {
                        txtPorcRetencion.Visible = true;
                        txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }
        protected void chkRetencion_CheckedChanged(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtCliente.Text != string.Empty && txtCliente.Text != "-1")
            {
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Text);
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();
                    if (chkRetencion.Checked == true)
                    {
                        if (cliente.PorcientoRetencion == 0 || cliente.PorcientoRetencion == null)
                        {
                            Alerta("El porcentaje de Retencion no está establecido, debe ser Mayor a Cero");
                            chkRetencion.Checked = false;
                            txtPorcRetencion.Visible = false;
                        }
                        else
                        {
                            consultarRetencion();
                        }
                    }
                    else
                    {
                        txtPorcRetencion.Visible = false;
                    }
                }
                catch (Exception ex)
                {

                    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("popup(false);");
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

        protected void BtnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapFactura cn_CapFactura = new CN_CapFactura();
                string mensaje = string.Empty;
                string FacEstatus = "";
                Factura fac = new Factura();
                fac.Id_Fac = Convert.ToInt32(Request.QueryString["Id_Fac"]);
                fac.Id_Cd = Sesion.Id_Cd_Ver;
                fac.Id_Emp = Sesion.Id_Emp;

                cn_CapFactura.ConsultaEstatus(fac.Id_Fac, Sesion, ref FacEstatus);
                if (FacEstatus == "S")
                {
                    CambiarEstatus(Sesion, fac.Id_Fac, "I");
                    EnviarCorreoAutorizacion(Convert.ToInt32(fac.Id_Fac), "No Autorizada");
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "La factura cambio a estatus valido en el sistema.", "')"));
                }
                else
                    Alerta("Este documento ya ha sido Autorizado o Rechazado, favor de validar el estatus actual.");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }

        protected void BtnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapFactura cn_CapFactura = new CN_CapFactura();
                string mensaje = string.Empty;
                string FacEstatus = "";
                CN_FacturasEntrega cn_factura = new CN_FacturasEntrega();
                Factura fac = new Factura();
                fac.Id_Fac = Convert.ToInt32(Request.QueryString["Id_Fac"]);
                fac.Id_Cd = Sesion.Id_Cd_Ver;
                fac.Id_Emp = Sesion.Id_Emp;

                cn_CapFactura.ConsultaEstatus(fac.Id_Fac, Sesion, ref FacEstatus);
                if (FacEstatus != "S")
                {
                    Alerta("Este documento ya ha sido Autorizado o Rechazado, favor de validar el estatus actual.");
                    return;
                }
                else
                {
                    CancelarFactura(fac.Id_Emp, fac.Id_Cd, fac.Id_Fac);
                    if (fac.Id_Fac == 0)
                    {
                        Alerta("Este documento ya ha sido Autorizado o Rechazado, favor de validar el estatus actual.");
                        return;
                    }
                    EnviarCorreoAutorizacion(Convert.ToInt32(fac.Id_Fac), "Autorizada");
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "La factura se dio de baja correctamente", "')"));
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }

        #region CancelarFacturas
        public void CancelarFactura(int Id_Emp, int Id_Cd, int Id_Fac)
        {
            try
            {
                this.Inicializar();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Factura factura = new Factura();
                factura.Id_Emp = Id_Emp;
                factura.Id_Cd = Id_Cd;
                factura.Id_Fac = Id_Fac;
                factura.Id_U = sesion.Id_U;
                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                EntradaSalida entSal = new EntradaSalida();
                List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                double importeTotalFactura = 0;
                double importeTotalFacturaIVA = 0;
                double importeTotalFactura_ProdNoDevolucion = 0;
                double importeTotalFacturaIVA_ProdNoDevolucion = 0;

                //Consultar factura
                new CN_CapFactura().ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                factura.Id_U = sesion.Id_U;
                string RFC = string.Empty;
                string UUID = string.Empty;


                XmlDocument xmlBD = new XmlDocument();
                int TSATCANCELACION = 1;
                /*
                if (factura.Fac_Xml != null)
                {
                    xmlBD.LoadXml(factura.Fac_Xml.ToString());

                    foreach (XmlNode nodo in xmlBD.ChildNodes)
                    {
                        if (nodo.Name == "Comprobante")
                        {
                            TSATCANCELACION = 1;
                        }
                        else if (nodo.Name == "cfdi:Comprobante")
                        {
                            TSATCANCELACION = 2;
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    XmlNode Nodo_nivel3;
                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                    UUID = Nodo_nivel3.Attributes["UUID"].Value;
                                }

                                if (Nodo_nivel2.Name == "cfdi:Emisor")
                                {
                                    RFC = Nodo_nivel2.Attributes["rfc"].Value;
                                }

                            }
                        }
                    }
                }
                */


                if (factura.Fac_Estatus != "B")
                {

                    /*
                     * Calcular cantidad de Iva en base al porcentaje que representa el importe de la factura a 
                     * cancelar calculado de los productos a los que no se ha aplicado una devolución
                     */
                    importeTotalFactura = factura.Fac_SubTotal != null ? Convert.ToSingle(factura.Fac_SubTotal) : 0;
                    importeTotalFacturaIVA = factura.Fac_ImporteIva != null ? Convert.ToSingle(factura.Fac_ImporteIva) : 0;
                    double porcentaje = 0;
                    if (importeTotalFactura > 0)
                        porcentaje = importeTotalFactura_ProdNoDevolucion / importeTotalFactura;
                    if (porcentaje > 0 && importeTotalFacturaIVA > 0)
                        importeTotalFacturaIVA_ProdNoDevolucion = importeTotalFacturaIVA * porcentaje;

                    CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                    //llenar objeto de entrada-salida, movimiento 7 (cancelación de factura)
                    entSal.Id_Emp = Id_Emp;
                    entSal.Id_Cd = Id_Cd;
                    entSal.Id_U = sesion.Id_U;
                    entSal.Id_Tm = 8; //mov. de entrada por cancelacion de factura, el prod. vuvlve al almacén de la sucursal
                    entSal.Id_Cte = factura.Id_Cte;
                    entSal.Id_Pvd = -1;
                    entSal.Es_Naturaleza = 0;//entrada
                    entSal.Es_Fecha = funciones.GetLocalDateTime(sesion.Minutos);
                    entSal.Es_Referencia = string.Concat("Canc. F-", factura.Id_Fac.ToString());
                    entSal.Es_Notas = string.Concat("Movimiento automático generado por cancelación de factura ", factura.Id_Fac.ToString());
                    entSal.Es_SubTotal = importeTotalFactura_ProdNoDevolucion;
                    entSal.Es_Iva = importeTotalFacturaIVA_ProdNoDevolucion;
                    entSal.Es_Total = importeTotalFactura_ProdNoDevolucion + importeTotalFacturaIVA_ProdNoDevolucion;
                    entSal.Es_Estatus = "I";
                    int verificador = 0;
                    factura.Id_U = sesion.Id_U;
                    new CN_CapFactura().EliminarFactura(ref factura, sesion.Emp_Cnx, ref verificador, ref entSal, ref listaEntSal);//, ref notaCredito, ref listaNotaCreditoDetalle);
                }

                if (TSATCANCELACION == 2)
                {
                    /* string valorResultadoCancelacion = "0";
                     WS_CFDICancelacion.Service1 ws = new WS_CFDICancelacion.Service1();
                     ws.Url = ConfigurationManager.AppSettings["WS_CFDICancelacion"].ToString();
                     String respuestaCancelacion = ws.CancelacionWS("" + RFC + "," + UUID + "");
                     XmlDocument XmlCancelacion = new XmlDocument();
                     XmlCancelacion.LoadXml(respuestaCancelacion);


                     foreach (XmlNode nodo in XmlCancelacion.ChildNodes)
                     {
                         if (nodo.Name == "Acuse")
                         {
                             foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                             {
                                 if (Nodo_nivel2.Name == "Folios")
                                 {
                                     foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                     {
                                         if (Nodo_nivel3.Name == "EstatusUUID")
                                         {
                                             valorResultadoCancelacion = Nodo_nivel3.InnerText;
                                         }

                                     }

                                 }
                             }

                         }
                     }
                     string valorResultadoCancelacionTexto = string.Empty;
                     switch (valorResultadoCancelacion)
                     {
                         case "202":
                             valorResultadoCancelacionTexto = "Documento Previamente Cancelado";
                             break;
                         case "203":
                             valorResultadoCancelacionTexto = "Documento No corresponda al emisor";
                             break;
                         case "204":
                             valorResultadoCancelacionTexto = "Documento No Aplicable para cancelación";
                             break;
                         case "205":
                             valorResultadoCancelacionTexto = "Documento No Existe emisión";
                             break;
                         default:
                             valorResultadoCancelacionTexto = "No se hizo conexión con el servicio de cancelación";
                             break;
                     }

                     if (valorResultadoCancelacion != "201")
                     {
                         this.Alerta(valorResultadoCancelacionTexto);
                         return;
                     }
                     */
                }


                ImprimirFactura(sesion.Id_Emp, sesion.Id_Cd, factura.Id_Fac, "CANCELACION", string.Concat("Canc. F-", factura.Id_Fac.ToString()), false);
                if (factura.Fac_Estatus != "B")
                { }
                //  rgFactura.Rebind();
                Alerta("Factura cancelada exitosamente");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        private void ShowTempPDF(string tempPath_archivoPDF)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(tempPath_archivoPDF);
            //new CN_CapFactura().LogError_Insertar("17.1", "carga info de proceso a ejecutar, iniciando proc.Start", sesion.Emp_Cnx);
            proc.Start();
            //proc.Start("IExplore.exe", "C:\\myPath\\myFile.asp");


            //new CN_CapFactura().LogError_Insertar("17.2", "proc.Start iniciado correctamente", sesion.Emp_Cnx);
            while (!proc.HasExited)
            {
                System.Threading.Thread.Sleep(200);
            }
            //new CN_CapFactura().LogError_Insertar("17.3", "finalizó proceso para mostrar impresion", sesion.Emp_Cnx);
            //File.Delete(tempPath_archivoPDF);
            //new CN_CapFactura().LogError_Insertar("17.3", "Borró archivo temporal", sesion.Emp_Cnx);
        }

        private void ImprimirFactura(int Id_Emp, int Id_Cd, int Id_Fac, string movimiento, string agregado_nota_cancelacion, bool tienePDF = false)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                int verificador = 0;

                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                CN_CapFactura cn_factura = new CN_CapFactura();
                Factura factura = new Factura();
                Factura facturaNacional = new Factura();
                factura.Id_Emp = sesion.Id_Emp;
                factura.Id_Cd = sesion.Id_Cd_Ver;
                factura.Id_Fac = Id_Fac;

                facturaNacional.Id_Emp = sesion.Id_Emp;
                facturaNacional.Id_Cd = sesion.Id_Cd_Ver;
                facturaNacional.Id_Fac = Id_Fac;

                cn_factura.ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                cn_factura.ConsultaFacturaNacional(ref facturaNacional, sesion.Emp_Cnx);

                // Validar si la Remisión es Válida o no en base a la suma de los montos en las partidas de la remisión y la remisión especial.
                bool bDocumentoValido = false;
                new CN_CapFactura().ValidaMontosImpresion(factura, sesion.Id_Cd_Ver, sesion.Id_Emp, 2, sesion.Emp_Cnx, ref bDocumentoValido);

                if (bDocumentoValido)
                {
                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    List<AdendaDet> listCabR = new List<AdendaDet>();
                    List<AdendaDet> listDetR = new List<AdendaDet>();
                    List<AdendaDet> listCabNacionalT = new List<AdendaDet>();
                    List<AdendaDet> listDetNacionalT = new List<AdendaDet>();
                    new CN_CapFactura().ConsultarAdenda(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                    new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);
                    new CN_CapFactura().ConsultarAdendaNacional(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabNacionalT, ref listDetNacionalT, sesion.Emp_Cnx);

                    // -------------------------------------------------------------------------------------------
                    // Consulta productos de factura especial de la tabla 'CapFacturaEspecialDet' si esque la factura especial existe
                    // esto es si es una actualización de factura --> si el parametro Folio trae un Id de factura
                    // -------------------------------------------------------------------------------------------
                    List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();

                    new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                        , sesion.Emp_Cnx
                        , Id_Emp
                        , Id_Cd
                        , Id_Fac
                        , factura.Id_Cte);
                    // -------------------------------------------------------------------------------------------

                    #region variable XML a enviar
                    StringBuilder XML_Enviar = new StringBuilder();
                    XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    XML_Enviar.Append("<Comprobante");
                    XML_Enviar.Append(" serie=\"\"");
                    XML_Enviar.Append(" folio=\"\"");
                    XML_Enviar.Append(" fecha=\"\"");
                    XML_Enviar.Append(" formaDePago=\"\"");
                    XML_Enviar.Append(" subTotal=\"\"");
                    XML_Enviar.Append(" total=\"\"");

                    XML_Enviar.Append(" tipoDeComprobante=\"\"");
                    XML_Enviar.Append(" Sustituye=\"\"");
                    XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                    XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                    XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                    XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                    XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                    XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                    XML_Enviar.Append(" TasaDescuento1=\"\"");
                    XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                    XML_Enviar.Append(" TasaDescuento2=\"\"");
                    XML_Enviar.Append(" Notas=\"\"");
                    XML_Enviar.Append(" Correo=\"\"");
                    XML_Enviar.Append(" CliNum=\"\"");

                    XML_Enviar.Append(" MetodoPago=\"\"");
                    XML_Enviar.Append(" CuentaBancaria=\"\"");
                    XML_Enviar.Append(" Referencia=\"\"");
                    XML_Enviar.Append(" ComprobanteVersion=\"\"");
                    XML_Enviar.Append(">");
                    XML_Enviar.Append(" <Emisor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" numero=\"\" />");
                    XML_Enviar.Append(" <Receptor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" nombre=\"\"");
                    XML_Enviar.Append(" UsoCFDI=\"\">");
                    XML_Enviar.Append(" <Domicilio");
                    XML_Enviar.Append(" calle=\"\"");
                    XML_Enviar.Append(" noExterior=\"\"");
                    XML_Enviar.Append(" noInterior=\"\"");
                    XML_Enviar.Append(" colonia=\"\"");
                    XML_Enviar.Append(" municipio=\"\"");
                    XML_Enviar.Append(" estado=\"\"");
                    XML_Enviar.Append(" pais=\"\"");
                    XML_Enviar.Append(" codigoPostal=\"\" />");
                    XML_Enviar.Append(" </Receptor>");
                    XML_Enviar.Append(" <Conceptos>");
                    XML_Enviar.Append(" <Concepto");
                    XML_Enviar.Append(" ClaveProdServ=\"\"");
                    XML_Enviar.Append(" ClaveUnidad=\"\"");
                    XML_Enviar.Append(" cantidad=\"\"");
                    XML_Enviar.Append(" noIdentificacion=\"\"");
                    XML_Enviar.Append(" descripcion=\"\"");
                    XML_Enviar.Append(" valorUnitario=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Conceptos>");
                    XML_Enviar.Append(" <Impuestos");
                    XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                    XML_Enviar.Append(" <Traslados>");
                    XML_Enviar.Append(" <Traslado");
                    XML_Enviar.Append(" impuesto=\"\"");
                    XML_Enviar.Append(" tasa=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Traslados>");

                    if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                    {
                        XML_Enviar.Append(" <Retenidos>");
                        XML_Enviar.Append(" <Retenido");
                        XML_Enviar.Append(" importe=\"\"");
                        XML_Enviar.Append(" impuesto=\"\" />");
                        XML_Enviar.Append(" </Retenidos>");
                    }
                    XML_Enviar.Append(" </Impuestos>");

                    XML_Enviar.Append(" <Addenda>");

                    //ADENDA CABECERA
                    XML_Enviar.Append(" <cabecera");
                    XML_Enviar.Append(" Pedido=\"\"");
                    XML_Enviar.Append(" Requisicion=\"\"");
                    XML_Enviar.Append(" consignarRenglon1=\"\"");
                    XML_Enviar.Append(" consignarRenglon2=\"\"");
                    XML_Enviar.Append(" consignarRenglon3=\"\"");
                    XML_Enviar.Append(" consignarRenglon4=\"\"");
                    XML_Enviar.Append(" consignarRenglon5=\"\"");
                    XML_Enviar.Append(" Conducto=\"\"");
                    XML_Enviar.Append(" CondicionesPago=\"\"");
                    XML_Enviar.Append(" NumeroGuia=\"\"");
                    XML_Enviar.Append(" ControlPedido=\"\"");
                    XML_Enviar.Append(" OrdenEmbarque=\"\"");
                    XML_Enviar.Append(" Zona=\"\"");
                    XML_Enviar.Append(" Territorio=\"\"");
                    XML_Enviar.Append(" Agente=\"\"");
                    XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                    XML_Enviar.Append(" Formulo=\"\"");
                    XML_Enviar.Append(" Autorizo=\"\"");

                    XML_Enviar.Append(" NombreAddenda=\"\"");
                    foreach (AdendaDet det in listCabT)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }
                    foreach (AdendaDet det in listCabR)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }
                    XML_Enviar.Append("/>");




                    //ADENDA DETALLE
                    if (listaProdFacturaEspecialFinal.Count > 0)
                    {
                        foreach (FacturaDet fd in listaProdFacturaEspecialFinal)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                            string primerNodo = "";
                            int primerfila = 0;
                            foreach (AdendaDet det in listDetT)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            primerNodo = "";
                            primerfila = 0;
                            foreach (AdendaDet det in listDetR)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            XML_Enviar.Append("/>");
                        }
                    }
                    else
                    {
                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                            string primerNodo = "";
                            int primerfila = 0;
                            foreach (AdendaDet det in listDetT)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            primerNodo = "";
                            primerfila = 0;
                            foreach (AdendaDet det in listDetR)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            XML_Enviar.Append("/>");
                        }

                    }
                    XML_Enviar.Append(" </Addenda>");
                    if (facturaNacional != null)
                    {
                        if (movimiento != "CANCELACION")
                        {
                            //COMPROBANTE NACIONAL
                            XML_Enviar.Append(" <ComprobanteCN");
                            XML_Enviar.Append(" CliNum=\"\"");
                            XML_Enviar.Append(">");
                            XML_Enviar.Append(" <Conceptos>");
                            XML_Enviar.Append(" <Concepto");
                            XML_Enviar.Append(" cantidad=\"\"");
                            XML_Enviar.Append(" noIdentificacion=\"\"");
                            XML_Enviar.Append(" descripcion=\"\"");
                            XML_Enviar.Append(" valorUnitario=\"\"");
                            XML_Enviar.Append(" importe=\"\" />");
                            XML_Enviar.Append(" </Conceptos>");

                            //ADENDA NACIONAL
                            XML_Enviar.Append(" <AddendaCN>");

                            //ADENDA NACIONAL CABECERA
                            XML_Enviar.Append(" <CabeceraCN");
                            XML_Enviar.Append(" Pedido=\"\"");
                            XML_Enviar.Append(" Requisicion=\"\"");
                            XML_Enviar.Append(" consignarRenglon1=\"\"");
                            XML_Enviar.Append(" consignarRenglon2=\"\"");
                            XML_Enviar.Append(" consignarRenglon3=\"\"");
                            XML_Enviar.Append(" consignarRenglon4=\"\"");
                            XML_Enviar.Append(" consignarRenglon5=\"\"");
                            XML_Enviar.Append(" Conducto=\"\"");
                            XML_Enviar.Append(" CondicionesPago=\"\"");
                            XML_Enviar.Append(" NumeroGuia=\"\"");
                            XML_Enviar.Append(" ControlPedido=\"\"");
                            XML_Enviar.Append(" OrdenEmbarque=\"\"");
                            XML_Enviar.Append(" Zona=\"\"");
                            XML_Enviar.Append(" Territorio=\"\"");
                            XML_Enviar.Append(" Agente=\"\"");
                            XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                            XML_Enviar.Append(" Formulo=\"\"");
                            XML_Enviar.Append(" Autorizo=\"\"");

                            XML_Enviar.Append(" NombreAddenda=\"\"");
                            foreach (AdendaDet det in listCabNacionalT)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                            XML_Enviar.Append("/>");


                            //ADENDA NACIONAL DETALLE

                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            foreach (FacturaDet fd in listaFacturaDet)
                            {
                                XML_Enviar.Append(" <Detalle");
                                XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                                string primerNodo = "";
                                int primerfila = 0;
                                foreach (AdendaDet det in listDetNacionalT)
                                {

                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                            // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }

                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }

                                XML_Enviar.Append("/>");
                            }

                            XML_Enviar.Append(" </AddendaCN>");

                            XML_Enviar.Append(" </ComprobanteCN>");
                        }
                        else
                        {
                            XML_Enviar.Append("<ComprobanteCN UUID=\"" + factura.Fac_FolioFiscalCN + "\" Folio=\"" + factura.Fac_FolioCN.ToString() + "\" Serie=\"" + factura.Serie + "\" />");
                            facturaNacional = null;
                        }
                    }
                    XML_Enviar.Append(" </Comprobante>");

                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA

                    //foreach (FacturaDet fd in listaFacturaDet)
                    //{
                    //    XML_Enviar.Append(" <Detalle");
                    //    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                    //    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                    //    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\""); 
                    //    foreach (AdendaDet det in listDetT)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    foreach (AdendaDet det in listDetR)
                    //    {
                    //        if (fd.Id_Prd == det.Id_Prd)
                    //        {
                    //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    //        }
                    //    }
                    //    XML_Enviar.Append("/>");
                    //}






                    #endregion


                    // --------------------------------------
                    // Consulta centro de distribución
                    // --------------------------------------
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    // --------------------------------------------------------------------
                    // Consulta detalle de factura para generar lista de productos
                    // --------------------------------------------------------------------
                    //if (factura.Fac_Sello != "" && factura.Fac_Sello != null && movimiento == "FACTURA")
                    //{
                    //    //Abre el XML y carga el PDF de la factura
                    //    object resultado = null;
                    //    cn_factura.ConsultarFacturaSAT(ref factura, sesion.Emp_Cnx, ref resultado);
                    //    byte[] archivoPdf = (byte[])resultado;
                    //    if (archivoPdf.Length > 0)
                    //    {
                    //        string tempPDFname = string.Concat("FACTURA_"
                    //                 , factura.Id_Emp.ToString()
                    //                 , "_", factura.Id_Cd.ToString()
                    //                 , "_", factura.Id_U.ToString()
                    //                 , ".pdf");
                    //        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    //        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                    //        this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    //        // ------------------------------------------------------------------------------------------------
                    //        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    //        // ------------------------------------------------------------------------------------------------

                    //        RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                    //    }
                    //    else
                    //        this.DisplayMensajeAlerta("TempPDFNoData");
                    //}
                    //else
                    //{
                    // --------------------------------------
                    // cargar xml de factura que se envia a SAT
                    // --------------------------------------
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XML_Enviar.ToString());

                    // --------------------------------------//
                    // --------------------------------------//
                    //         LLENAR DATOS DEL XML          //
                    // --------------------------------------//
                    // --------------------------------------//
                    #region Llenar datos factura a Enviar
                    //encabezado
                    XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = factura.Id_Emp;
                    cliente.Id_Cd = factura.Id_Cd;
                    cliente.Id_Cte = factura.Id_Cte;
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    Comprobante.Attributes["serie"].Value = factura.Serie;
                    Comprobante.Attributes["folio"].Value = factura.Folio_cancelacion > 0 ? factura.Folio_cancelacion.ToString() : factura.Id_Fac.ToString();
                    //Comprobante.Attributes["fecha"].Value = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", factura.Fac_FechaHr);
                    Comprobante.Attributes["fecha"].Value = string.Format("{0:s}", factura.Fac_Fecha);

                    Comprobante.Attributes["formaDePago"].Value = cliente.Cte_MetodoPago;/*"PAGO EN UNA SOLA EXHIBICION"*/;
                    Comprobante.Attributes["subTotal"].Value = factura.Fac_SubTotal == null ? "0" : Math.Round((double)factura.Fac_SubTotal, 2).ToString();
                    Comprobante.Attributes["total"].Value = (Math.Round((double)factura.Fac_SubTotal, 2) + Math.Round((double)factura.Fac_ImporteIva, 2)).ToString();
                    Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                    Comprobante.Attributes["Sustituye"].Value = factura.Fac_Refactura;
                    Comprobante.Attributes["tipoMovimiento"].Value = movimiento;
                    Comprobante.Attributes["tipoMoneda"].Value = factura.Mon_Unidad;
                    Comprobante.Attributes["tipoCambio"].Value = factura.Mon_TipCambio.ToString();
                    Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                    Comprobante.Attributes["movimientoacancelar"].Value = ""; //

                    Comprobante.Attributes["ConceptoDescuento1"].Value = factura.Fac_Desc1;
                    Comprobante.Attributes["TasaDescuento1"].Value = factura.Fac_DescPorcen1 == null ? string.Empty : factura.Fac_DescPorcen1.ToString();
                    Comprobante.Attributes["ConceptoDescuento2"].Value = factura.Fac_Desc2;
                    Comprobante.Attributes["TasaDescuento2"].Value = factura.Fac_DescPorcen2 == null ? string.Empty : factura.Fac_DescPorcen2.ToString();
                    Comprobante.Attributes["Correo"].Value = factura.Cte_Email;
                    Comprobante.Attributes["CliNum"].Value = factura.Id_Cte.ToString();
                    Comprobante.Attributes["MetodoPago"].Value = "00".Substring(1, 2 - factura.Fac_FPago.Trim().Length) + factura.Fac_FPago.Trim();


                    //Comprobante.Attributes["MetodoPago"].Value = FormaPagoNombre(factura.Fac_FPago);
                    Comprobante.Attributes["CuentaBancaria"].Value = factura.Fac_UDigitos.ToString();
                    Comprobante.Attributes["Referencia"].Value = cliente.Cte_Referencia;
                    Comprobante.Attributes["ComprobanteVersion"].Value = "3.3";


                    XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                    Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                    Emisor.Attributes["numero"].Value = Cd.Id_Cd.ToString();

                    //receptor
                    XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                    Receptor.Attributes["rfc"].Value = factura.Fac_CteRfc;
                    Receptor.Attributes["nombre"].Value = factura.Cte_NomComercial;
                    Receptor.Attributes["UsoCFDI"].Value = cliente.Cte_UsoCFDI;

                    //Domicilio
                    XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                    Domicilio.Attributes["calle"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacCalle); // factura.Fac_CteCalle.Replace("\"", "");
                    Domicilio.Attributes["noExterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumero);// factura.Fac_CteNumero;
                    Domicilio.Attributes["noInterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumeroInterior);// factura.Fac_CteNumero;
                    Domicilio.Attributes["colonia"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacColonia);// factura.Fac_CteColonia;
                    Domicilio.Attributes["municipio"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacMunicipio);// factura.Fac_CteMunicipio;
                    Domicilio.Attributes["estado"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacEstado);// factura.Fac_CteEstado;
                    Domicilio.Attributes["pais"].Value = "México";
                    Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;// factura.Fac_CteCp;
                    // ---------------------
                    // Conceptos --> partidas = producto
                    // Detalle --> productoDetalle
                    // ---------------------         
                    XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                    XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                    XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");

                    XmlNode ComprobanteCN = Comprobante.SelectNodes("ComprobanteCN").Count > 0 ? Comprobante.SelectSingleNode("ComprobanteCN") : null;
                    XmlNode AddendaCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("AddendaCN") : null;
                    XmlNode ConceptosCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("Conceptos") : null;
                    XmlNode productoCN = ConceptosCN != null ? ConceptosCN.SelectSingleNode("Concepto") : null;

                    if (facturaNacional != null)
                    {
                        ComprobanteCN.Attributes["CliNum"].Value = facturaNacional != null ? facturaNacional.Id_Cte.ToString() : "0";
                    }


                    //Si existe una factura especial, en los nodos de conceptos del producto se pone
                    //los productos de la factura especial
                    //si no, se pone los datos de productos de la factura original
                    StringBuilder NotaProductosOriginales = new StringBuilder();
                    if (listaProdFacturaEspecialFinal.Count > 0)
                    {
                        foreach (FacturaDet facturaDet in listaProdFacturaEspecialFinal)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Producto.Id_PrdEsp;
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_CantE.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_ImporteE, 2).ToString();
                            prd.Attributes["ClaveProdServ"].Value = "01010101";
                            prd.Attributes["ClaveUnidad"].Value = "H87";
                            producto.ParentNode.AppendChild(prd);
                        }

                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Id_Prd.ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(Math.Round(facturaDet.Fac_Precio, 2).ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(facturaDet.Fac_Cant.ToString());
                        }
                    }
                    else
                    {
                        foreach (FacturaDet facturaDet in listaFacturaDet)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                            prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                            prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                            prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                            // prd.Attributes["ClaveProdServ"].Value = facturaDet.Fac_ClaveProdServ.ToString();
                            // prd.Attributes["ClaveUnidad"].Value = facturaDet.Fac_ClaveUnidad.ToString();
                            producto.ParentNode.AppendChild(prd);


                            if (facturaNacional != null)
                            {
                                XmlNode prdCN = productoCN.Clone();
                                prdCN.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                                prdCN.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                                prdCN.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                                prdCN.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                                prdCN.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                                prd.Attributes["ClaveProdServ"].Value = "01010101";
                                prd.Attributes["ClaveUnidad"].Value = "H87";
                                productoCN.ParentNode.AppendChild(prdCN);
                            }
                        }
                    }
                    producto.ParentNode.RemoveChild(xml.SelectNodes("//Concepto").Item(0));

                    if (facturaNacional != null)
                    {
                        productoCN.ParentNode.RemoveChild(xml.SelectNodes("//ComprobanteCN//Conceptos//Concepto").Item(0));
                    }


                    //Impuestos
                    XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                    Impuestos.Attributes["totalImpuestosTrasladados"].Value = factura.Fac_ImporteIva == null ? "0" : factura.Fac_ImporteIva.ToString();

                    //Traslado (impuestos desgloce)
                    XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                    XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                    Traslado.Attributes["impuesto"].Value = "IVA";
                    if (cliente.BPorcientoIVA == true)
                        Traslado.Attributes["tasa"].Value = cliente.PorcientoIVA.ToString();
                    else
                        Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                    Traslado.Attributes["importe"].Value = factura.Fac_ImporteIva == null ? "0" : Math.Round((double)factura.Fac_ImporteIva, 2).ToString();

                    if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                    {
                        XmlNode Retenidos = Impuestos.SelectSingleNode("Retenidos");
                        XmlNode Retenido = Retenidos.SelectSingleNode("Retenido");
                        Retenido.Attributes["importe"].Value = factura.Fac_ImporteRetencion == null ? "0" : Math.Round((double)factura.Fac_ImporteRetencion, 2).ToString();
                        Retenido.Attributes["impuesto"].Value = "IVA";
                    }

                    //Addenda
                    XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                    cabecera.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    //consulta datos cliente                 
                    cabecera.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                    cabecera.Attributes["consignarRenglon2"].Value = string.Concat(factura.Fac_CteCalle.Replace("\"", ""), " ", factura.Fac_CteNumero);
                    cabecera.Attributes["consignarRenglon3"].Value = factura.Fac_CteColonia;
                    cabecera.Attributes["consignarRenglon4"].Value = string.Concat(factura.Fac_CteMunicipio, " ", factura.Fac_CteEstado, " ", factura.Fac_CteCp);
                    cabecera.Attributes["consignarRenglon5"].Value = "México";
                    cabecera.Attributes["Conducto"].Value = factura.Fac_Conducto;
                    cabecera.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                    cabecera.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                    cabecera.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabecera.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                    cabecera.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                    cabecera.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                    cabecera.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                    cabecera.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                    cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                    cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;


                    //Addenda Nacional
                    if (facturaNacional != null)
                    {
                        XmlNode cabeceraCN = AddendaCN.SelectSingleNode("CabeceraCN");
                        cabeceraCN.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabeceraCN.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        //consulta datos cliente                 
                        cabeceraCN.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                        cabeceraCN.Attributes["consignarRenglon2"].Value = string.Concat(facturaNacional.Fac_CteCalle.Replace("\"", ""), " ", facturaNacional.Fac_CteNumero);
                        cabeceraCN.Attributes["consignarRenglon3"].Value = facturaNacional.Fac_CteColonia;
                        cabeceraCN.Attributes["consignarRenglon4"].Value = string.Concat(facturaNacional.Fac_CteMunicipio, " ", facturaNacional.Fac_CteEstado, " ", facturaNacional.Fac_CteCp).Replace('É', 'E');
                        cabeceraCN.Attributes["consignarRenglon5"].Value = "Mexico";
                        cabeceraCN.Attributes["Conducto"].Value = factura.Fac_Conducto;
                        cabeceraCN.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                        cabeceraCN.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                        cabeceraCN.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabeceraCN.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                        cabeceraCN.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                        cabeceraCN.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                        cabeceraCN.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                        cabeceraCN.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        cabeceraCN.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                        cabeceraCN.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                        cabeceraCN.Attributes["NombreAddenda"].Value = facturaNacional.Fac_CteAdeNombre;//cliente.Ade_Nombre;
                    }


                    Factura factura_remision = new Factura();
                    factura_remision.Id_Emp = factura.Id_Emp;
                    factura_remision.Id_Cd = factura.Id_Cd;
                    factura_remision.Id_Fac = factura.Id_Fac;
                    string agregado_nota = "";
                    cn_factura.FacturaRemision_Nota(factura_remision, sesion.Emp_Cnx, ref agregado_nota);
                    StringBuilder NotaCompleta = new StringBuilder();

                    NotaCompleta.Append(agregado_nota + "//");
                    NotaCompleta.Append(NotaProductosOriginales.ToString() + "//");
                    NotaCompleta.Append(factura.Fac_Notas + "//");
                    NotaCompleta.Append(agregado_nota_cancelacion);
                    Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();

                    /*
                    if (!ValidaImpresionFactura(xml)) 
                    {
                        Alerta("No se puede Imprimir Documento: Detalle de factura no coincide con total, Revise factura");
                        return;
                    
                    }*/

                    #endregion
                    // --------------------------------------
                    // convertir XML a string
                    // --------------------------------------
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xml.WriteTo(tx);
                    string xmlString = sw.ToString();
                    // ------------------------------------------------------   
                    // ENVIAR XML al servicio de la aplicacion de KEY
                    // -------- ----------------------------------------------
                    XmlDocument xmlSAT = new XmlDocument();

                    int TSAT = 1;

                    XmlDocument xmlBD = new XmlDocument();

                    if (factura.Fac_Xml != null && factura.Fac_Xml != "")
                    {
                        xmlBD.LoadXml(factura.Fac_Xml.ToString());

                        foreach (XmlNode nodo in xmlBD.ChildNodes)
                        {
                            if (nodo.Name == "Comprobante")
                            {
                                TSAT = 1;
                            }
                            else if (nodo.Name == "cfdi:Comprobante")
                            {
                                TSAT = 2;

                            }
                        }
                    }


                    //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();

                    if (TSAT == 2 && tienePDF)
                    {
                        descargarPDF(Id_Fac);
                        return;
                    }

                    WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                    sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                    object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                    if (movimiento == "CANCELACION")
                    {
                        string XmLRegex = string.Empty;
                        XmLRegex = Regex.Replace(sianFacturacionElectronicaResult.ToString(), @"(?s)(?<=<cfdi:Addenda>).+?(?=</cfdi:Addenda>)", "");
                        XmLRegex = XmLRegex.Replace("<cfdi:Addenda>", "");
                        XmLRegex = XmLRegex.Replace("</cfdi:Addenda>", "");
                        xmlSAT.LoadXml(XmLRegex);
                    }
                    else
                    {
                        xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                    }





                    //*********************************************//
                    //* Procesar XML recibido de servicio de SAT  *//
                    //*********************************************//
                    string stringPDF = string.Empty;
                    string stringPDFCN = string.Empty;
                    string selloSAT = string.Empty;
                    string selloSATCN = string.Empty;
                    string folioFiscal = string.Empty;
                    string folioFiscalCN = string.Empty;
                    string errorNum = string.Empty;
                    string errorText = string.Empty;
                    string errorNumCN = string.Empty;
                    string errorTextCN = string.Empty;
                    string VersionCFDI = string.Empty;


                    TSAT = 1;

                    foreach (XmlNode nodoSistemaFacturacion in xmlSAT.ChildNodes)
                    {
                        if (nodoSistemaFacturacion.Name == "Comprobante")
                        {
                            TSAT = 1;
                            selloSAT = nodoSistemaFacturacion.Attributes["sello"].Value;
                            VersionCFDI = nodoSistemaFacturacion.Attributes["Version"].Value;
                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }


                            }
                        }
                        else if (nodoSistemaFacturacion.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;
                            VersionCFDI = nodoSistemaFacturacion.Attributes["Version"].Value;
                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "tfd:TimbreFiscalDigital")
                                        {
                                            //selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;


                                            if (VersionCFDI == "3.2")
                                            {
                                                selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                            }
                                            else
                                            {
                                                selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                            }


                                            folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                        }
                                    }

                                }

                            }

                        }
                        if (nodoSistemaFacturacion.Name == "SistemaFacturacion")
                        {
                            foreach (XmlNode nodoXmlSAT in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (nodoXmlSAT.Name == "ComprobanteCDIK")
                                {
                                    foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                    {
                                        if (nodo.Name == "Comprobante")
                                        {

                                            VersionCFDI = nodo.Attributes["Version"].Value;
                                            TSAT = 1;
                                            selloSAT = nodo.Attributes["sello"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }
                                            }
                                        }
                                        else if (nodo.Name == "cfdi:Comprobante")
                                        {
                                            TSAT = 2;
                                            VersionCFDI = nodo.Attributes["Version"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }

                                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                {
                                                    XmlNode Nodo_nivel3;
                                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                    //selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;


                                                    if (VersionCFDI == "3.2")
                                                    {
                                                        selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                                    }
                                                    else
                                                    {
                                                        selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                                    }
                                                    folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                                }

                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    if (nodoXmlSAT.Name == "ComprobanteKSL")
                                    {



                                        foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                        {
                                            if (nodo.Name == "Comprobante")
                                            {
                                                TSAT = 1;
                                                selloSATCN = nodo.Attributes["sello"].Value;
                                                VersionCFDI = nodo.Attributes["Version"].Value;

                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }


                                                }
                                            }
                                            else if (nodo.Name == "cfdi:Comprobante")
                                            {
                                                VersionCFDI = nodo.Attributes["Version"].Value;
                                                TSAT = 2;
                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }

                                                    if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                    {
                                                        XmlNode Nodo_nivel3;
                                                        Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                        //selloSATCN = Nodo_nivel3.Attributes["SelloSAT"].Value;


                                                        if (VersionCFDI == "3.2")
                                                        {
                                                            selloSATCN = Nodo_nivel3.Attributes["selloSAT"].Value;
                                                        }
                                                        else
                                                        {
                                                            selloSATCN = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                                        }
                                                        folioFiscalCN = Nodo_nivel3.Attributes["UUID"].Value;
                                                    }

                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }



                    if (errorNum != "0")
                    {
                        this.Alerta(string.Concat(errorText.Replace("'", "\"")));

                        /* factura.Fac_Sello = selloSAT;
                         System.Data.SqlTypes.SqlXml sqlXml
                             = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                         factura.Fac_Xml = sqlXml;
                         factura.Fac_Pdf = this.Base64ToByte(stringPDF);

                         verificador = 0;

                         new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);*/
                    }
                    else
                    {
                        //ComprobanteSAT.RemoveChild(AddendaSAT);

                        if ((facturaNacional != null) && (errorNumCN != "0"))
                        {
                            this.Alerta(string.Concat(errorTextCN.Replace("'", "\"")));
                        }
                        else
                        {
                            factura.Fac_Sello = selloSAT;
                            factura.Fac_SelloCN = selloSATCN;

                            System.Data.SqlTypes.SqlXml sqlXml;
                            System.Data.SqlTypes.SqlXml sqlXmlCN;

                            if (xmlSAT.SelectNodes("SistemaFacturacion").Count > 0)
                            {
                                //sqlXml = sqlXml.Value.Replace("ComprobanteCDIK", "").;
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteCDIK").OuterXml.Replace("<ComprobanteCDIK>", "").Replace("</ComprobanteCDIK>", ""), XmlNodeType.Document, null));
                                sqlXmlCN = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").OuterXml.Replace("<ComprobanteKSL>", "").Replace("</ComprobanteKSL>", ""), XmlNodeType.Document, null));
                                factura.Fac_FolioCN = int.Parse(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["Folio"].Value == string.Empty ? "0" : xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["Folio"].Value);
                            }
                            else
                            {
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                                sqlXmlCN = null;
                                factura.Fac_FolioCN = null;
                            }


                            if (movimiento != "CANCELACION")
                            {

                                factura.Fac_Xml = sqlXml;
                                factura.Fac_XmlCN = sqlXmlCN;
                                factura.Fac_FolioFiscal = folioFiscal;
                                factura.Fac_FolioFiscalCN = folioFiscalCN;
                            }

                            factura.Fac_Pdf = this.Base64ToByte(stringPDF);
                            factura.Fac_PdfCN = this.Base64ToByte(stringPDFCN);

                            #region reporte factura


                            #endregion

                            // ---------------------------------------------------------------------------------------------
                            // Se actualiza el estatus de la factura a Impreso (I)
                            // ---------------------------------------------------------------------------------------------
                            if (movimiento != "CANCELACION")
                            {
                                factura.Fac_Estatus = "I";
                                new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);
                            }
                            else
                            {
                                factura.Fac_Estatus = "B";
                            }
                            verificador = 0;


                            // -----------------------
                            // Abrir PDF de factura
                            // -----------------------
                            string tempPDFname = string.Concat("FACTURA_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                            string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                            string tempPDFCNname = string.Concat("FACTURACN_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                            string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                            string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                            this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                            if (facturaNacional != null)
                            {
                                this.ByteToTempPDF(URLtempPDFCN, this.Base64ToByte(stringPDFCN));
                                // ------------------------------------------------------------------------------------------------
                                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                                // ------------------------------------------------------------------------------------------------
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','", WebURLtempPDFCN, "')"));
                            }
                            else
                            {
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','')"));
                            }
                        }
                        //}
                    }
                }
                else
                {
                    RAM1.ResponseScripts.Add("OpenAlert('Los montos de la Factura y la Factura Especial no coinciden','" + Id_Emp + "','" + Id_Cd + "','" + Id_Fac + "','" + 1 + "')");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void descargarPDF(int Id_Fac)
        {
            object resultado = null;
            object resultadoCN = null;
            Factura fac = new Factura();
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            fac.Id_Emp = Sesion.Id_Emp;
            fac.Id_Cd = Sesion.Id_Cd_Ver;
            fac.Id_Fac = Id_Fac;
            CN_CapFactura factura = new CN_CapFactura();
            factura.ConsultarFacturaSAT(ref fac, Sesion.Emp_Cnx, ref resultado, ref resultadoCN);
            byte[] archivoPdf = (byte[])resultado;
            byte[] archivoPdfCN = resultadoCN != System.DBNull.Value ? (byte[])resultadoCN : new byte[0];
            if (archivoPdf.Length > 0)
            {
                string tempPDFname = string.Concat("FACTURA_"
                         , Sesion.Id_Emp.ToString()
                         , "_", Sesion.Id_Cd.ToString()
                         , "_", Id_Fac.ToString()
                         , ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, archivoPdf);

                if (archivoPdfCN.Length > 0)
                {
                    string tempPDFCNname = string.Concat("FACTURACN_", Sesion.Id_Emp.ToString(), "_", Sesion.Id_Cd.ToString(), "_", Id_Fac.ToString(), ".pdf");
                    string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                    string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                    this.ByteToTempPDF(URLtempPDFCN, archivoPdfCN);

                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','" + WebURLtempPDFCN + "')"));
                }
                else
                {
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFCN('", WebURLtempPDF, "','')"));
                }
            }
        }

        #endregion


        private void EnviarCorreoAutorizacion(int Id_Fac, string Estatus)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                string Mensaje;
                if (Estatus == "Autorizada")
                    Mensaje = "La baja de la factura #" + Id_Fac + ", ha sido autorizada y el estatus de la factura fue actualizado a Baja.";
                else
                    Mensaje = "La baja de la factura #" + Id_Fac + ", ha sido denegada y el estatus de la factura fue actualizado a Impreso";

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='3'>");
                cuerpo_correo.Append(Mensaje);
                cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='3'>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(ConsultarEmail(Id_Fac)));
                m.Subject = "solicitud de baja de factura #" + Id_Fac;
                // +" del centro " + session.Id_Cd_Ver + "fue " + Estatus;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                    //Alerta("La autorización se realizo correctamente y se envio correo de confirmación.");
                }
                catch (Exception)
                {
                    //CambiarEstatus(ordCompra, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                //rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Funcion para cambiar estatus de la factura al ser cancelada
        private void CambiarEstatus(Sesion sesion, int Id_Fac, string Fac_Estatus)
        {
            try
            {
                CN_FacturasEntrega Factura = new CN_FacturasEntrega();
                Factura.CambiarEstatus(sesion, Id_Fac, Fac_Estatus);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Se consulta el correo del usuario que creo la factura y al solicitar la baja se envia
        //correo de autorizacion o no autorizado al correo.
        private string ConsultarEmail(int Id_Fac)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            CN_CapFactura cn_capfactura = new CN_CapFactura();
            Factura factura = new Factura();
            factura.Id_Emp = session.Id_Emp;
            factura.Id_Cd = session.Id_Cd_Ver;
            factura.Id_Fac = Id_Fac;
            string correo = "";
            cn_capfactura.ConsultaCorreoUsuarioAutoriza(factura, session.Emp_Cnx, ref correo);
            return correo;
        }

        #endregion
        #region Funciones
        private void Inicializar(int Id_Emp, int Id_Cd, int Id_Fac, string facModificable)
        {
            try
            {
                if (_reFactura == "0" || _reFactura == null)
                {
                    txtCausaRef.Enabled = false;
                    cmbCausaRef.Enabled = false;
                    ChkRefacturaparcial.Enabled = false;
                    ChkRefacturatotal.Enabled = false;
                }
                else
                {

                    txtCausaRef.Enabled = true;
                    cmbCausaRef.Enabled = true;
                    ChkRefacturaparcial.Enabled = true;
                    ChkRefacturatotal.Enabled = true;
                }
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Session["FacEspecialGuardada" + Session.SessionID] = "0";
                //Session["ListaProductosFacturaEspecial" + Session.SessionID] = null;
                ListaProductosFacturaEspecial = null;
                InicializarTablaProductos();


                chkDesgloce.Attributes.Add("onfocus", "return _ValidarFechaEnPeriodo()");
                chkRetencion.Attributes.Add("onfocus", "return _ValidarFechaEnPeriodo()");
                Consultar_IVA_Cliente();
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                //HD_IVAfacturacion.Value   = cd.Cd_IvaPedidosFacturacion.ToString();
                HF_VI.Value = "true";

                //llenar combos
                this.CargarConsFactElectronica();
                this.CargarComboTipoMovimientos();
                this.CargarComboTerritorios();

                this.CargarComboTipoModeda();
                this.HabilitaBotonesToolBar(false, true, false, false, false, false);

                //Variable de sesion para los productos con amortizaciones de un cliente
                Session["ListaAmortizaciones" + Session.SessionID] = new List<Amortizacion>();

                //establece valores de controles de inicio
                if (Id_Emp > 0 && Id_Cd > 0 && Id_Fac > 0)
                {
                    this.hiddenId.Value = Id_Fac.ToString();
                    this.LLenarFormFactura(Id_Emp, Id_Cd, Id_Fac); //EDICION de factura
                    this.rgFacturaDet.Enabled = true;
                    //rgFacturaDetAde
                    this.rgFacturaDetAde.Enabled = true;
                    this.rgAdendaFacturacion.Enabled = true;

                    //rgFacturaDetAde
                    this.btnFacturaEspecial.Enabled = false;

                    if (facModificable == "0")
                        this.HabilitaBotonesToolBar(false, false, false, false, false, false);
                    if (_reFactura == "1")
                    {
                        this.HabilitaBotonesToolBar(false, true, false, false, false, false);
                        this.txtFecha.SelectedDate = DateTime.Now;

                    }
                    this.txtFecha.Focus();
                }
                else //FACTURA Nueva
                {

                    this.hiddenId.Value = string.Empty;
                    this.txtFecha.SelectedDate = DateTime.Now;

                    if (cmbConsFacEle.Items.Count > 1)
                    {
                        cmbConsFacEle.SelectedIndex = 1;
                        cmbConsFacEle.Text = cmbConsFacEle.Items[1].Text;
                        cmbConsFacEle_SelectedIndexChanged(null, null);
                    }

                    if (Session["PedidoFacturacion" + Session.SessionID] != null) //nueva factura con pedido previo                   
                    {
                        this.ConsultarDatosPedido();
                        // Se destruye sesion de pedido para evitar que se queden los datos en el proceso de new
                        Session["PedidoFacturacion" + Session.SessionID] = null;

                    }
                    else if (Session["ListaRemisionesFactura" + Session.SessionID] != null) //nueva factura de remisiones
                    {
                        this.ConsultarDatosRemisiones();
                    }
                    else if (Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null) //nueva factura devolución de remisiones
                    {
                        this.ConsultarDatosDevolucionRemisiones();
                    }
                    else
                    {
                        Session["ListaRemisionesFactura" + Session.SessionID] = null;
                        Session["ListaDevolucionRemisionesFactura" + Session.SessionID] = null;
                        this.Nuevo();
                        this.rgFacturaDet.Enabled = false;
                        //rgFacturaDetAde
                        this.rgFacturaDetAde.Enabled = false;
                        //rgFacturaDetAde
                        this.btnFacturaEspecial.Enabled = false;
                    }
                    this.txtFecha.Focus();
                }
                rgAdendaFacturacion.Rebind();
                rgFacturaDet.Rebind();
                rgFacturaDetAde.Rebind();

                if (this.cmbMov.SelectedValue == "91")
                {
                    this.lblUnidadesGarantia.Visible = true;
                    this.txtUnidadesGarantia.Visible = true;

                    rgFacturaDet.Columns[16].Visible = true;
                    rgFacturaDet.Columns[17].Visible = true;
                    rgFacturaDet.Columns[18].Visible = true;
                }
                else
                {
                    this.lblUnidadesGarantia.Visible = false;
                    this.txtUnidadesGarantia.Visible = false;

                    rgFacturaDet.Columns[16].Visible = false;
                    rgFacturaDet.Columns[17].Visible = false;
                    rgFacturaDet.Columns[18].Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CambiarTerritorio()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
            Factura factura = new Factura();
            new CN_CapFactura().ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
        }

        private void LLenarFormFactura(int Id_Emp, int Id_Cd, int Id_Fac)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Factura factura = new Factura();
                factura.Id_Emp = Id_Emp;
                factura.Id_Cd = Id_Cd;
                factura.Id_Fac = Id_Fac;
                Factura facturaNacional = new Factura();
                facturaNacional.Id_Emp = Id_Emp;
                facturaNacional.Id_Cd = Id_Cd;
                facturaNacional.Id_Fac = Id_Fac;
                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                List<FacturaDet> listaProductosFacturaEspecial = new List<FacturaDet>();
                //Consultar factura
                new CN_CapFactura().ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                new CN_CapFactura().ConsultaFacturaNacional(ref facturaNacional, sesion.Emp_Cnx);
                //Agregar Adendas
                InicializarTablaDetallesAdenda();
                InicializarTablaDetallesAdendaNacional();
                List<AdendaDet> listCabT = new List<AdendaDet>();
                List<AdendaDet> listDetT = new List<AdendaDet>();
                List<AdendaDet> listCabR = new List<AdendaDet>();
                List<AdendaDet> listDetR = new List<AdendaDet>();
                List<AdendaDet> listCabTNacional = new List<AdendaDet>();
                List<AdendaDet> listDetTNacional = new List<AdendaDet>();

                if (rgFacturaDetAde.Columns.Count > 17)
                    for (int i = rgFacturaDetAde.Columns.Count; i > 17; i--)
                        rgFacturaDetAde.Columns.RemoveAt(rgFacturaDetAde.Columns.Count - 1);

                //if ((factura.Fac_Refactura != null && factura.Fac_Refactura != "") || EsRefactura)
                //{                    
                //    new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);
                //    if (listCabR.Count > 0)
                //    {
                //        RadTabStrip1.Tabs[2].Visible = true;
                //        RadTabStrip1.Tabs[3].Visible = true;
                //        ListCabRF = listCabR;
                //        rgAdendaReFacturacion.Rebind();                        
                //    }
                //    ListCab = new List<AdendaDet>();
                //}
                //else               
                {
                    new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                    if (listCabT.Count > 0)
                    {
                        //RadTabStrip1.Tabs[1].Visible = true;
                        RadTabStrip1.Tabs[2].Visible = true;
                        ListCab = listCabT;
                        rgAdendaFacturacion.Rebind();

                    }
                    listCabR = new List<AdendaDet>();

                    new CN_CapFactura().ConsultarAdendaNacional(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "1", "2", ref listCabTNacional, ref listDetTNacional, sesion.Emp_Cnx);
                    if (listCabTNacional.Count > 0)
                    {
                        //RadTabStrip1.Tabs[1].Visible = true;
                        RadTabStrip1.Tabs[5].Visible = true;
                        ListCabNacional = listCabTNacional;
                        rgAdendaFacturacionNacional.Rebind();

                    }

                }

                if (Page.Request.QueryString["facModificable"].ToString() == "2")
                {
                    if (listCabR.Count == 0)
                    {
                        new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(factura.Id_Cte), "7,8", ref listCabR, ref listDetR, ref listCabR, sesion.Emp_Cnx);
                        if (listCabR.Count > 0)
                        {
                            RadTabStrip1.Tabs[3].Visible = true;
                            ListCabRF = listCabR;
                            rgAdendaReFacturacion.Rebind();

                        }
                    }
                    HiddenIdRF.Value = hiddenId.Value;
                    hiddenId.Value = "";
                    if (EsRefactura == true)
                    {
                        ChkRefacturaparcial.Enabled = true;
                        ChkRefacturatotal.Enabled = true;
                    }
                    else
                    {
                        ChkRefacturaparcial.Enabled = false;
                        ChkRefacturatotal.Enabled = false;
                    }
                }

                //if ((factura.Fac_Refactura != null && factura.Fac_Refactura != "") || EsRefactura)
                //{                    
                //    GridBoundColumn boundColumn2;

                //    foreach (AdendaDet ad in listDetR)
                //    {
                //        if (!ListaProductosFacturaAdenda.Columns.Contains(ad.Id_AdeDet.ToString()))
                //        {
                //            boundColumn2 = new GridBoundColumn();
                //            rgFacturaDetAde.MasterTableView.Columns.Add(boundColumn2);
                //            boundColumn2.DataField = ad.Id_AdeDet.ToString();
                //            boundColumn2.UniqueName = ad.Id_AdeDet.ToString();
                //            boundColumn2.HeaderText = ad.Campo;
                //            boundColumn2.HeaderStyle.Width = Unit.Pixel(150);
                //            boundColumn2.MaxLength = ad.Longitud;
                //            boundColumn2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                //            ListaProductosFacturaAdenda.Columns.Add(ad.Id_AdeDet.ToString());
                //        }
                //    }
                //    ListDetRF = listDetR;
                //    ListDet = new List<AdendaDet>();
                //}
                //else
                {
                    GridBoundColumn boundColumn3;

                    foreach (AdendaDet ad in listDetT)
                    {
                        if (!ListaProductosFacturaAdenda.Columns.Contains(ad.Id_AdeDet.ToString()))
                        {
                            boundColumn3 = new GridBoundColumn();
                            rgFacturaDetAde.MasterTableView.Columns.Add(boundColumn3);
                            boundColumn3.DataField = ad.Id_AdeDet.ToString();
                            boundColumn3.UniqueName = ad.Id_AdeDet.ToString();
                            boundColumn3.HeaderText = ad.Campo;
                            boundColumn3.HeaderStyle.Width = Unit.Pixel(150);
                            boundColumn3.MaxLength = ad.Longitud;
                            boundColumn3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                            ListaProductosFacturaAdenda.Columns.Add(ad.Id_AdeDet.ToString());

                        }
                    }
                    ListDet = listDetT;
                    ListDetRF = new List<AdendaDet>();

                    GridBoundColumn boundColumn4;

                    foreach (AdendaDet ad in listDetTNacional)
                    {
                        if (!ListaProductosFacturaAdendaNacional.Columns.Contains(ad.Id_AdeDet.ToString()))
                        {
                            boundColumn4 = new GridBoundColumn();
                            rgFacturaDetAdeNacional.MasterTableView.Columns.Add(boundColumn4);
                            boundColumn4.DataField = ad.Id_AdeDet.ToString();
                            boundColumn4.UniqueName = ad.Id_AdeDet.ToString();
                            boundColumn4.HeaderText = ad.Campo;
                            boundColumn4.HeaderStyle.Width = Unit.Pixel(150);
                            boundColumn4.MaxLength = ad.Longitud;
                            boundColumn4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                            ListaProductosFacturaAdendaNacional.Columns.Add(ad.Id_AdeDet.ToString());

                        }
                    }
                    ListDetNacional = listDetTNacional;
                }

                //CREA BOTON DE EDITAR
                GridEditCommandColumn commandColumnAde = new GridEditCommandColumn();
                rgFacturaDetAde.MasterTableView.Columns.Add(commandColumnAde);

                commandColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                commandColumnAde.UniqueName = "EditCommandColumn";
                commandColumnAde.EditText = "Editar";
                commandColumnAde.CancelText = "Cancelar";
                commandColumnAde.InsertText = "Aceptar";
                commandColumnAde.UpdateText = "Actualizar";
                commandColumnAde.HeaderStyle.Width = Unit.Pixel(60);
                commandColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                commandColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                //CREA BOTON ELIMINAR     
                GridButtonColumn deleteColumnAde = new GridButtonColumn();
                rgFacturaDetAde.MasterTableView.Columns.Add(deleteColumnAde);

                deleteColumnAde.ConfirmText = "¿Desea quitar este producto de la lista?";
                deleteColumnAde.ConfirmDialogHeight = Unit.Pixel(150);
                deleteColumnAde.ConfirmDialogWidth = Unit.Pixel(350);
                deleteColumnAde.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                deleteColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                deleteColumnAde.CommandName = "Delete";
                deleteColumnAde.Text = "Eliminar";
                deleteColumnAde.UniqueName = "DeleteColumn";
                deleteColumnAde.HeaderStyle.Width = Unit.Pixel(50);
                deleteColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                deleteColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                double ancho2 = 0;
                foreach (GridColumn gc in rgFacturaDetAde.Columns)
                {
                    if (gc.Display)
                    {
                        ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFacturaDetAde.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                rgFacturaDetAde.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho2));






                //CREA BOTON DE EDITAR
                GridEditCommandColumn commandColumnAdeNacional = new GridEditCommandColumn();
                rgFacturaDetAdeNacional.MasterTableView.Columns.Add(commandColumnAde);

                commandColumnAdeNacional.ButtonType = GridButtonColumnType.ImageButton;
                commandColumnAdeNacional.UniqueName = "EditCommandColumn";
                commandColumnAdeNacional.EditText = "Editar";
                commandColumnAdeNacional.CancelText = "Cancelar";
                commandColumnAdeNacional.InsertText = "Aceptar";
                commandColumnAdeNacional.UpdateText = "Actualizar";
                commandColumnAdeNacional.HeaderStyle.Width = Unit.Pixel(60);
                commandColumnAdeNacional.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                commandColumnAdeNacional.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                //CREA BOTON ELIMINAR     
                GridButtonColumn deleteColumnAdeNacional = new GridButtonColumn();
                rgFacturaDetAdeNacional.MasterTableView.Columns.Add(deleteColumnAde);

                deleteColumnAdeNacional.ConfirmText = "¿Desea quitar este producto de la lista?";
                deleteColumnAdeNacional.ConfirmDialogHeight = Unit.Pixel(150);
                deleteColumnAdeNacional.ConfirmDialogWidth = Unit.Pixel(350);
                deleteColumnAdeNacional.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                deleteColumnAdeNacional.ButtonType = GridButtonColumnType.ImageButton;
                deleteColumnAdeNacional.CommandName = "Delete";
                deleteColumnAdeNacional.Text = "Eliminar";
                deleteColumnAdeNacional.UniqueName = "DeleteColumn";
                deleteColumnAdeNacional.HeaderStyle.Width = Unit.Pixel(50);
                deleteColumnAdeNacional.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                deleteColumnAdeNacional.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                double ancho3 = 0;
                foreach (GridColumn gc in rgFacturaDetAdeNacional.Columns)
                {
                    if (gc.Display)
                    {
                        ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFacturaDetAdeNacional.Width = Unit.Pixel(Convert.ToInt32(ancho3));
                rgFacturaDetAdeNacional.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho3));

                ////rgFacturaDetAde 
                ////rgFacturaDetAde                


                //CREA BOTON DE EDITAR
                GridEditCommandColumn commandColumn = new GridEditCommandColumn();
                rgFacturaDet.MasterTableView.Columns.Add(commandColumn);
                commandColumn.ButtonType = GridButtonColumnType.ImageButton;
                commandColumn.UniqueName = "EditCommandColumn";
                commandColumn.EditText = "Editar";
                commandColumn.CancelText = "Cancelar";
                commandColumn.InsertText = "Aceptar";
                commandColumn.UpdateText = "Actualizar";
                commandColumn.HeaderStyle.Width = Unit.Pixel(60);
                commandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                commandColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                //CREA BOTON ELIMINAR
                GridButtonColumn deleteColumn = new GridButtonColumn();
                rgFacturaDet.MasterTableView.Columns.Add(deleteColumn);
                deleteColumn.ConfirmText = "¿Desea quitar este producto de la lista?";
                deleteColumn.ConfirmDialogHeight = Unit.Pixel(150);
                deleteColumn.ConfirmDialogWidth = Unit.Pixel(350);
                deleteColumn.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                deleteColumn.ButtonType = GridButtonColumnType.ImageButton;
                deleteColumn.CommandName = "Delete";
                deleteColumn.Text = "Eliminar";
                deleteColumn.UniqueName = "DeleteColumn";
                deleteColumn.HeaderStyle.Width = Unit.Pixel(50);
                deleteColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                deleteColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                double ancho = 0;
                foreach (GridColumn gc in rgFacturaDet.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFacturaDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgFacturaDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                //agregar remisiones de factura a variable de sesion, silas trae
                List<Remision> listaRemisiones = new List<Remision>();
                //foreach (FacturaDet fd in listaFacturaDet)
                foreach (FacturaDet dr in listaFacturaDet)
                {
                    if (dr.Id_Rem != null && Convert.ToInt32(dr.Id_Rem) > 0)
                    {
                        Remision rem = new Remision();
                        rem.Id_Rem = Convert.ToInt32(dr.Id_Rem);
                        listaRemisiones.Add(rem);
                    }
                    //columan especial de cliente que no acepta null, por eso si cliente externo es null, se pasa a 0.
                    if (dr.Id_CteExt == null)
                    {
                        dr.Id_CteExt = 0;
                        dr.Id_CteExtStr = string.Empty;
                    }
                }
                if (listaRemisiones.Count > 0)
                    Session["ListaRemisionesFactura" + Session.SessionID] = listaRemisiones;

                txtId.Text = factura.Id_Fac.ToString();
                if (factura.Id_Cfe == null)
                    cmbConsFacEle.SelectedIndex = 0;
                else
                    cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue(factura.Id_Cfe.ToString());

                if (Page.Request.QueryString["facModificable"].ToString() == "2")
                    ObtenerConsecutivoFactElectronica(Convert.ToInt32(cmbConsFacEle.SelectedValue));

                txtMov.Text = factura.Id_Tm.ToString();
                cmbMov.SelectedIndex = cmbMov.FindItemIndexByValue(factura.Id_Tm.ToString());
                //si es factura de aparatos inproductivos (mov. 70), se visualiza columna de Cliente Externo del grid
                if (factura.Id_Tm == 70)
                {
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = true;
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = true;
                }
                else
                {
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = false;
                    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = false;
                }
                txtFecha.SelectedDate = factura.Fac_Fecha;
                if (factura.Fac_PedNum == null) txtPedido.Text = string.Empty; else txtPedido.Text = factura.Fac_PedNum.ToString();
                if (factura.Fac_PedDesc == null) txtPedidoDesc.Text = string.Empty; else txtPedidoDesc.Text = factura.Fac_PedDesc.ToString();
                if (factura.Fac_Req == null) txtReq.Text = string.Empty; else txtReq.Text = factura.Fac_Req.ToString();

                txtCliente.Text = factura.Id_Cte.ToString();
                txtClienteNombre.Text = factura.Cte_NomComercial;

                CargarComboTerritorios();
                txtTerritorio.Text = factura.Id_Ter.ToString();
                cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(factura.Id_Ter.ToString());

                //no se permite modificar al cliente en modificacion de factura
                txtCliente.Enabled = false;
                txtClienteNombre.Enabled = false;

                txtContacto.Text = factura.Fac_Contacto;

                CargarFormaPago();
                cmbFormaPago.SelectedIndex = cmbFormaPago.FindItemIndexByValue(factura.Fac_FPago);
                txtUDigitos.Text = factura.Fac_UDigitos;
                txtHora.Text = factura.Fac_FechaHr.ToString("H:mm:ss");



                //-----------------------------
                if (factura.Fac_DesgIva == null) chkDesgloce.Checked = false; else chkDesgloce.Checked = Convert.ToBoolean(factura.Fac_DesgIva);
                if (factura.Fac_RetIva == null) chkRetencion.Checked = false; else chkRetencion.Checked = Convert.ToBoolean(factura.Fac_RetIva);
                if (factura.Id_Mon == null) txtMoneda.Text = string.Empty; else txtMoneda.Text = factura.Id_Mon.ToString();
                if (factura.Id_Mon == null) cmbMoneda.SelectedIndex = -1; else cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue(factura.Id_Mon.ToString());

                if (factura.Fac_CteCalle == null) txtCalle.Text = string.Empty; else txtCalle.Text = factura.Fac_CteCalle.ToString();
                if (factura.Fac_CteNumero == null) txtCalleNumero.Text = string.Empty; else txtCalleNumero.Text = factura.Fac_CteNumero.ToString();
                if (factura.Fac_CteNumeroInterior == null) txtCalleNumeroInterior.Text = string.Empty; else txtCalleNumeroInterior.Text = factura.Fac_CteNumeroInterior.ToString();
                if (factura.Fac_CteCp == null) txtCP.Text = string.Empty; else txtCP.Text = factura.Fac_CteCp.ToString();
                if (factura.Fac_CteColonia == null) txtColonia.Text = string.Empty; else txtColonia.Text = factura.Fac_CteColonia.ToString();
                if (factura.Fac_CteMunicipio == null) txtMunicipio.Text = string.Empty; else txtMunicipio.Text = factura.Fac_CteMunicipio.ToString();
                if (factura.Fac_CteEstado == null) txtEstado.Text = string.Empty; else txtEstado.Text = factura.Fac_CteEstado.ToString();
                if (factura.Fac_CteRfc == null) txtRFC.Text = string.Empty; else txtRFC.Text = factura.Fac_CteRfc.ToString();
                if (factura.Fac_CteTel == null) txtTelefono.Text = string.Empty; else txtTelefono.Text = factura.Fac_CteTel.ToString();

                if (factura.Fac_CondEntrega == null) txtCondiciones.Text = string.Empty; else txtCondiciones.Text = factura.Fac_CondEntrega.ToString();
                if (factura.Fac_Conducto == null) txtConducto.Text = string.Empty; else txtConducto.Text = factura.Fac_Conducto.ToString();
                if (factura.Fac_NumEntrega == null) txtNumeroEntrega.Text = string.Empty; else txtNumeroEntrega.Text = factura.Fac_NumEntrega.ToString();
                if (factura.Fac_OrdEntrega == null) txtOrden.Text = string.Empty; else txtOrden.Text = factura.Fac_OrdEntrega.ToString();
                if (factura.Fac_NumeroGuia == null) txtNumeroGuia.Text = string.Empty; else txtNumeroGuia.Text = factura.Fac_NumeroGuia.ToString();
                if (factura.Fac_Notas == null) txtNotas.Text = string.Empty; else txtNotas.Text = factura.Fac_Notas.ToString();
                if (factura.Fac_DescPorcen1 == null) txtDescuento1.Text = string.Empty; else txtDescuento1.Text = factura.Fac_DescPorcen1.ToString();
                if (factura.Fac_DescPorcen2 == null) txtDescuento2.Text = string.Empty; else txtDescuento2.Text = factura.Fac_DescPorcen2.ToString();
                if (factura.Fac_Desc1 == null) txtDescPorc1.Text = string.Empty; else txtDescPorc1.Text = factura.Fac_Desc1.ToString();
                if (factura.Fac_Desc2 == null) txtDescPorc2.Text = string.Empty; else txtDescPorc2.Text = factura.Fac_Desc2.ToString();
                if ((factura.Fac_ImporteRetencion > 0) && (factura.Fac_RetIva == true)) { chkRetencion.Checked = true; consultarRetencion(); } else { chkRetencion.Checked = false; txtPorcRetencion.Visible = false; }

                if (txtCondiciones.Text.Trim() == "")
                {
                    CN_CatCliente Cn_cte = new CN_CatCliente();
                    Clientes cte = new Clientes();
                    cte.Id_Emp = sesion.Id_Emp;
                    cte.Id_Cd = sesion.Id_Cd_Ver;
                    cte.Id_Cte = factura.Id_Cte;
                    Cn_cte.ConsultaClientes(ref cte, sesion.Emp_Cnx);

                    txtCondiciones.Text = cte.Cte_CondPago.ToString();
                }

                if (factura.Id_Es == null)
                    this.hiddenId_Es.Value = string.Empty;
                else
                    this.hiddenId_Es.Value = factura.Id_Es.ToString();

                ConvertiraDt(listaFacturaDet, listDetR, listDetTNacional, factura.Fac_Refactura);
                //this.CalcularTotales();

                txtSubTotal.DbValue = factura.Fac_SubTotal;
                txtIVA.DbValue = factura.Fac_ImporteIva;
                txtImporte.DbValue = factura.Fac_Importe;
                txtTotal.DbValue = factura.Fac_SubTotal + factura.Fac_ImporteIva;

                if ((factura.Id_Cte.ToString() != "-1" && factura.Id_Cte.ToString() != string.Empty)
                    && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                    && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                {
                    this.rgFacturaDet.Enabled = true;
                    this.rgFacturaDetAde.Enabled = true;
                    this.rgAdendaFacturacion.Enabled = true;
                    this.btnFacturaEspecial.Enabled = true;
                }
                else
                {
                    this.rgFacturaDet.Enabled = false;
                    this.rgAdendaFacturacion.Enabled = false;
                    this.rgFacturaDetAde.Enabled = false;
                    this.btnFacturaEspecial.Enabled = false;
                }

                CargarEspecial(Id_Fac, sesion, factura.Id_Cte);

                if (facturaNacional != null)
                {
                    RadTabStrip1.Tabs[4].Visible = true;
                    txtClienteNacionalNombre.Text = facturaNacional.Cte_NomComercial;
                    txtClienteNacional.Text = facturaNacional.Id_Cte.ToString();
                    TxtClienteNacionalCalle.Text = facturaNacional.Fac_CteCalle;
                    TxtClienteNacionalNoExterior.Text = facturaNacional.Fac_CteNumero;
                    TxtClienteNacionalColonia.Text = facturaNacional.Fac_CteColonia;
                    TxtClienteNacionalMunicipio.Text = facturaNacional.Fac_CteMunicipio;
                    TxtClienteNacionalEstado.Text = facturaNacional.Fac_CteEstado;
                    TxtClienteNacionalCp.Text = facturaNacional.Fac_CteCp;
                    TxtClienteNacionalRfc.Text = facturaNacional.Fac_CteRfc;
                    TxtClienteNacionalAdenda.Text = facturaNacional.Fac_CteAdeNombre;

                }

                new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProductosFacturaEspecial, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, factura.Id_Fac, factura.Id_Cte);
                if (listaProductosFacturaEspecial.Count > 0)
                {
                    Inicializar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarEspecial(int Id_Fac, Sesion sesion, int Id_Cte)
        {
            //- Especial
            List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
            new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                , sesion.Emp_Cnx
                , sesion.Id_Emp
                , sesion.Id_Cd_Ver
                , Id_Fac
                , Id_Cte);

            if (listaProdFacturaEspecialFinal.Count > 0)
            {
                Session["FacEspecialGuardada" + Session.SessionID] = 1;
            }
        }
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                ErrorManager();

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProducto_Combo", ref sender);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConvertiraDt(List<FacturaDet> listaFacturaDet, List<AdendaDet> listaReFacturaDet, object refactura)
        {

            try
            {
                ArrayList al;
                foreach (FacturaDet fd in listaFacturaDet)
                {
                    al = new ArrayList();
                    al.Add(fd.Id_Fac);
                    al.Add(fd.Id_FacDet);
                    al.Add(fd.Id_Rem);
                    al.Add(0);//Id_Tm_Rem
                    al.Add(fd.Id_CteExt);
                    al.Add(fd.Id_Ter);
                    al.Add(fd.Id_Prd);
                    al.Add(fd.Producto.Prd_Descripcion);
                    al.Add(fd.Producto.Prd_Presentacion);
                    al.Add(fd.Producto.Prd_UniNe);
                    al.Add(fd.Fac_Cant);
                    al.Add(fd.Rem_Cant);
                    al.Add(fd.Fac_Precio);
                    al.Add(fd.Fac_Importe);
                    al.Add(fd.Id_TerStr);
                    al.Add(fd.Id_CteExtStr);
                    al.Add(fd.AmortizacionProducto);
                    al.Add(fd.Id_Emp);
                    al.Add(fd.Id_Cd);
                    al.Add(fd.Fac_Asignar);
                    al.Add(fd.Fac_Devolucion);
                    al.Add(fd.Producto.Prd_UniNs);

                    objdtTabla.Rows.Add(al.ToArray());
                }

                //NUEVO METODO PARA OBTENER LOS VALORES DE LAS ADENDAS
                //NUEVO METODO PARA OBTENER LOS VALORES DE LAS ADENDAS

                ArrayList alAde;
                ArrayList nombreCampos;
                nombreCampos = new ArrayList();
                alAde = new ArrayList();
                string primercampo = "";
                int primerfila = 0;

                foreach (AdendaDet ad1 in ListDet)
                {
                    if (primerfila == 0)
                    {
                        primercampo = ad1.Campo.ToString();
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            if (fd.Id_Prd == ad1.Id_Prd)
                            {
                                alAde.Add(generarGUI_IdAdeDet());
                                alAde.Add(fd.Id_Prd);
                                alAde.Add(fd.Producto.Prd_Descripcion);
                            }
                        }
                    }
                    nombreCampos.Add(ad1.Campo.ToString());

                    if (nombreCampos.Count > 1 && ad1.Campo == primercampo)
                    {
                        ListaProductosFacturaAdenda.Rows.Add(alAde.ToArray());
                        alAde = new ArrayList();
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            if (fd.Id_Prd == ad1.Id_Prd)
                            {
                                alAde.Add(generarGUI_IdAdeDet());
                                alAde.Add(fd.Id_Prd);
                                alAde.Add(fd.Producto.Prd_Descripcion);
                            }
                        }
                    }

                    alAde.Add(ad1.Valor);

                    primerfila++;
                }
                ListaProductosFacturaAdenda.Rows.Add(alAde.ToArray());


            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConvertiraDt(List<FacturaDet> listaFacturaDet, List<AdendaDet> listaReFacturaDet, List<AdendaDet> listaReFacturaDetNacional, object refactura)
        {

            try
            {
                InicializarTablaProductos();
                ArrayList al;
                foreach (FacturaDet fd in listaFacturaDet)
                {
                    al = new ArrayList();
                    al.Add(fd.Id_Fac);
                    al.Add(fd.Id_FacDet);
                    al.Add(fd.Id_Rem);
                    al.Add(0);//Id_Tm_Rem
                    al.Add(fd.Id_CteExt);
                    al.Add(fd.Id_Ter);
                    al.Add(fd.Id_Prd);
                    al.Add(fd.Producto.Prd_Descripcion);
                    al.Add(fd.Producto.Prd_Presentacion);
                    al.Add(fd.Producto.Prd_UniNe);
                    al.Add(fd.Fac_Cant);
                    al.Add(fd.Rem_Cant);
                    al.Add(fd.Fac_Precio);
                    al.Add(fd.Fac_Importe);
                    al.Add(fd.Id_TerStr);
                    al.Add(fd.Id_CteExtStr);
                    al.Add(fd.AmortizacionProducto);
                    al.Add(fd.Id_Emp);
                    al.Add(fd.Id_Cd);
                    al.Add(fd.Fac_Asignar);
                    al.Add(fd.Fac_Devolucion);
                    al.Add(fd.Producto.Prd_UniNs);
                    al.Add(null);
                    al.Add(null);

                    al.Add(fd.Multiplicador);
                    al.Add(fd.Precio_Venta);
                    al.Add(fd.Totales);

                    objdtTabla.Rows.Add(al.ToArray());
                }

                //NUEVO METODO PARA OBTENER LOS VALORES DE LAS ADENDAS
                //NUEVO METODO PARA OBTENER LOS VALORES DE LAS ADENDAS

                ArrayList alAde;
                ArrayList nombreCampos;
                nombreCampos = new ArrayList();
                alAde = new ArrayList();
                string primercampo = "";
                int primerfila = 0;

                foreach (AdendaDet ad1 in ListDet)
                {
                    if (primerfila == 0)
                    {
                        primercampo = ad1.Campo.ToString();
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            if (fd.Id_Prd == ad1.Id_Prd)
                            {
                                alAde.Add(generarGUI_IdAdeDet());
                                alAde.Add(fd.Id_Prd);
                                alAde.Add(fd.Producto.Prd_Descripcion);
                            }
                        }
                    }
                    nombreCampos.Add(ad1.Campo.ToString());

                    if (nombreCampos.Count > 1 && ad1.Campo == primercampo)
                    {
                        ListaProductosFacturaAdenda.Rows.Add(alAde.ToArray());
                        alAde = new ArrayList();
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            if (fd.Id_Prd == ad1.Id_Prd)
                            {
                                alAde.Add(generarGUI_IdAdeDet());
                                alAde.Add(fd.Id_Prd);
                                alAde.Add(fd.Producto.Prd_Descripcion);
                            }
                        }
                    }

                    alAde.Add(ad1.Valor);

                    primerfila++;
                }
                ListaProductosFacturaAdenda.Rows.Add(alAde.ToArray());

                alAde = new ArrayList();
                foreach (AdendaDet ad1 in ListDetNacional)
                {
                    if (primerfila == 0)
                    {
                        primercampo = ad1.Campo.ToString();
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            if (fd.Id_Prd == ad1.Id_Prd)
                            {
                                alAde.Add(generarGUI_IdAdeDet());
                                alAde.Add(fd.Id_Prd);
                                alAde.Add(fd.Producto.Prd_Descripcion);
                            }
                        }
                    }
                    nombreCampos.Add(ad1.Campo.ToString());

                    if (nombreCampos.Count > 1 && ad1.Campo == primercampo)
                    {
                        ListaProductosFacturaAdendaNacional.Rows.Add(alAde.ToArray());
                        alAde = new ArrayList();
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            if (fd.Id_Prd == ad1.Id_Prd)
                            {
                                alAde.Add(generarGUI_IdAdeDet());
                                alAde.Add(fd.Id_Prd);
                                alAde.Add(fd.Producto.Prd_Descripcion);
                            }
                        }
                    }

                    alAde.Add(ad1.Valor);

                    primerfila++;
                }
                ListaProductosFacturaAdendaNacional.Rows.Add(alAde.ToArray());

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularAdenda(out bool calcularAdendaTrue, out bool subtotalesIguales)
        {
            calcularAdendaTrue = false;
            subtotalesIguales = false;
            try
            {
                ErrorManager();

                string cantidadExiste = "NO", precioExiste = "NO";
                double subtotales = 0, subtotal = 0;
                string campoCant = "", campoPre = "";

                //SE BUSCA SI EXISTE CAMPO DE CANTIDAD Y DE PRECIO
                foreach (AdendaDet ad1 in ListDet)
                {
                    string campoTemp = ad1.Id_AdeDet.ToString();
                    if (ad1.Nodo.ToString() == "AddendaCantidad")
                    {
                        cantidadExiste = "SI";
                        campoCant = campoTemp;
                    }
                    if (ad1.Nodo.ToString() == "AddendaVU")
                    {
                        precioExiste = "SI";
                        campoPre = campoTemp;
                    }
                }

                if (cantidadExiste == "SI" && precioExiste == "SI") //VERIFICAMOS QUE EXISTAN LAS 2 COLUMNAS DE PRECIO Y CANTIDAD
                {
                    //CALCULAMOS EL SUBTOTAL
                    foreach (DataRow myRow in ListaProductosFacturaAdenda.Rows)
                    {
                        subtotales = Convert.ToDouble(myRow[campoCant]) * Convert.ToDouble(myRow[campoPre]);
                        subtotal += subtotales;
                    }
                    calcularAdendaTrue = true;
                }

                if (Math.Round(subtotal, 2) == Math.Round(Convert.ToDouble(txtSubTotal.Text), 2))
                {
                    subtotalesIguales = true;
                }
            }
            catch (Exception ex)
            {
                Alerta("Los Valores de CANTIDAD y PRECIO de la ADDENDA no se han llenado correctamente");
                throw ex;


            }
        }
        private void CalcularAdendaNacional(out bool calcularAdendaTrue, out bool subtotalesIguales)
        {
            calcularAdendaTrue = false;
            subtotalesIguales = false;
            try
            {
                ErrorManager();

                string cantidadExiste = "NO", precioExiste = "NO";
                double subtotales = 0, subtotal = 0;
                string campoCant = "", campoPre = "";

                //SE BUSCA SI EXISTE CAMPO DE CANTIDAD Y DE PRECIO
                foreach (AdendaDet ad1 in ListDetNacional)
                {
                    string campoTemp = ad1.Id_AdeDet.ToString();
                    if (ad1.Nodo.ToString() == "AddendaCantidad")
                    {
                        cantidadExiste = "SI";
                        campoCant = campoTemp;
                    }
                    if (ad1.Nodo.ToString() == "AddendaVU")
                    {
                        precioExiste = "SI";
                        campoPre = campoTemp;
                    }
                }

                if (cantidadExiste == "SI" && precioExiste == "SI") //VERIFICAMOS QUE EXISTAN LAS 2 COLUMNAS DE PRECIO Y CANTIDAD
                {
                    //CALCULAMOS EL SUBTOTAL
                    foreach (DataRow myRow in ListaProductosFacturaAdendaNacional.Rows)
                    {
                        subtotales = Convert.ToDouble(myRow[campoCant]) * Convert.ToDouble(myRow[campoPre]);
                        subtotal += subtotales;
                    }
                    calcularAdendaTrue = true;
                }

                if (Math.Round(subtotal, 2) == Math.Round(Convert.ToDouble(txtSubTotal.Text), 2))
                {
                    subtotalesIguales = true;
                }
            }
            catch (Exception ex)
            {
                Alerta("Los Valores de CANTIDAD y PRECIO de la ADDENDA no se han llenado correctamente");
                throw ex;


            }
        }
        private void LlenarFactura(DataTable ListaProductosFactura, ref Factura factura)
        {
            try
            {
                Funciones func = new Funciones();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                if (objdtTabla.Rows.Count > 0)
                {
                    factura.Id_Tm_Rem = !string.IsNullOrEmpty(objdtTabla.Rows[0]["Id_Tm"].ToString()) ? Convert.ToInt32(objdtTabla.Rows[0]["Id_Tm"].ToString()) : 0;
                }
                factura.Id_Emp = session.Id_Emp;
                factura.Id_Cd = session.Id_Cd_Ver;
                factura.Id_Fac = Convert.ToInt32(txtId.Text); //cambia cuando se inserta la factura si es nueva, permanece si se modifica
                if (cmbConsFacEle.SelectedValue == "-1") factura.Id_Cfe = null; else factura.Id_Cfe = Convert.ToInt32(cmbConsFacEle.SelectedValue);
                if (cmbConsFacEle.SelectedValue == "-1") factura.Id_FacSerie = null; else factura.Id_FacSerie = string.Concat(cmbConsFacEle.Text);

                factura.Id_U = session.Id_U;
                factura.Id_Tm = Convert.ToInt32(cmbMov.SelectedValue);
                if (txtPedido.Text == string.Empty) factura.Fac_PedNum = null; else factura.Fac_PedNum = Convert.ToInt32(txtPedido.Text);
                if (txtPedidoDesc.Text == string.Empty) factura.Fac_PedDesc = null; else factura.Fac_PedDesc = txtPedidoDesc.Text;
                if (txtReq.Text == string.Empty) factura.Fac_Req = null; else factura.Fac_Req = txtReq.Text;
                factura.Fac_Fecha = Convert.ToDateTime(txtFecha.SelectedDate.Value.ToString("dd/MM/yyyy") + " " + func.GetLocalDateTime(session.Minutos).ToString("HH:mm:ss"));
                factura.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                factura.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                factura.Id_Rik = Convert.ToInt32(txtRepresentante.Text);
                factura.Id_Mon = Convert.ToInt32(cmbMoneda.SelectedValue);
                factura.Fac_DesgIva = chkDesgloce.Checked;
                factura.Fac_RetIva = chkRetencion.Checked;
                factura.Fac_Contacto = txtContacto.Text;
                factura.Fac_CteCalle = txtCalle.Text;
                factura.Fac_CteNumero = txtCalleNumero.Text;
                factura.Fac_CteNumeroInterior = txtCalleNumeroInterior.Text;
                factura.Fac_CteNombre = txtClienteNombre.Text;
                factura.Fac_CteCp = txtCP.Text;
                factura.Fac_CteColonia = txtColonia.Text;
                factura.Fac_CteMunicipio = txtMunicipio.Text;
                factura.Fac_CteEstado = txtEstado.Text;
                factura.Fac_CteRfc = txtRFC.Text;
                factura.Fac_CteTel = txtTelefono.Text;
                //  if (txtOrden.Text == string.Empty) factura.Fac_OrdEntrega = null; else factura.Fac_OrdEntrega = txtOrden.Text;
                factura.Fac_OrdEntrega = txtOrden.Text;
                factura.Fac_NumeroGuia = txtNumeroGuia.Text;
                factura.Fac_CondEntrega = txtCondiciones.Text;
                if (txtNumeroEntrega.Text == string.Empty) factura.Fac_NumEntrega = null; else factura.Fac_NumEntrega = Convert.ToInt32(txtNumeroEntrega.Text);
                factura.Fac_Notas = txtNotas.Text;
                if (txtDescuento1.Text == string.Empty) factura.Fac_DescPorcen1 = null; else factura.Fac_DescPorcen1 = Convert.ToDouble(txtDescuento1.Text);
                if (txtDescuento2.Text == string.Empty) factura.Fac_DescPorcen2 = null; else factura.Fac_DescPorcen2 = Convert.ToDouble(txtDescuento2.Text);
                factura.Fac_Desc1 = txtDescPorc1.Text;
                factura.Fac_Desc2 = txtDescPorc2.Text;
                factura.Fac_Tipo = "VN";
                factura.Fac_Conducto = txtConducto.Text;
                factura.Fac_CanNum = null; //????
                factura.Fac_FecCan = null;
                factura.Fac_Pagado = 0;
                factura.Fac_FecPag = DateTime.Now;
                factura.Fac_Importe = Convert.ToDouble(txtImporte.Value.Value);
                factura.Fac_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                factura.Fac_ImporteIva = Convert.ToDouble(txtIVA.Text);
                if (factura.Fac_RetIva == true)
                {
                    factura.Fac_ImporteRetencion = Convert.ToDouble(txtPorcRetencion.Text) != 0 ? ((Convert.ToDouble(txtSubTotal.Text)) * (Convert.ToDouble(txtPorcRetencion.Text) / 100)) : 0;
                }
                else
                {
                    factura.Fac_ImporteRetencion = 0;
                }
                factura.Fac_Estatus = "C";

                factura.Fac_FPago = cmbFormaPago.SelectedValue;
                factura.Fac_UDigitos = txtUDigitos.Text;

                //Estos campos se agregan para identificar las refacturaciones
                //14/12/2016 Raul Borquez Martinez
                //Inicio
                if (EsRefactura == true)
                {
                    factura.Fac_IdUsuRef = session.Id_U.ToString();
                    factura.Fac_FechaRef = DateTime.Now;
                    factura.Fac_EsRefactura = Page.Request.QueryString["reFactura"].ToString();
                    if (ChkRefacturatotal.Checked == true) factura.Fac_TipoRef = "1"; else factura.Fac_TipoRef = "0";
                    if (cmbCausaRef.SelectedValue == "-1") factura.Fac_IdCausaRef = null; else factura.Fac_IdCausaRef = cmbCausaRef.SelectedIndex.ToString();
                }
                //Fin

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
                this.HabilitaControles(true);
                txtNotas.Text = string.Empty;
                //COMENTARIADO POR OSCAR PARA CAMBIO DE LISTA A DT
                //rgFacturaDet.DataSource = this.ListaProductosFactura = new List<FacturaDet>();
                //rgFacturaDet.DataBind();
                txtFecha.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar(bool AceptacionPrecios)
        {
            try
            {
                RadToolBar1.Enabled = false;
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];


                if (!ChkRefacturaparcial.Checked && !ChkRefacturatotal.Checked && EsRefactura == true)
                {
                    Alerta("Favor de seleccionar si sera Refacturación Parcial o total, antes de continuar");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return;
                }

                if (ChkRefacturaparcial.Checked && txtCausaRef.Text == "" && EsRefactura == true)
                {
                    Alerta("Favor de seleccionar una causa de Refacturación antes de seguir");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return;
                }

                if (rgFacturaDet.Items.Count == 0)
                {
                    Alerta("Capture al menos un producto para guardar la factura");
                    RadToolBar1.Enabled = true;
                    return;
                }


                if (string.IsNullOrEmpty(cmbTerritorio.SelectedValue) || cmbTerritorio.SelectedValue == "-1")
                {
                    Alerta("Seleccione un territorio válido");
                    RadToolBar1.Enabled = true;
                    return;
                }




                if (HF_VI.Value == "true")
                { //validacion para facturaVI campo requisicion
                    bool requisicion = false;
                    Factura fac2 = new Factura();
                    fac2.Id_Cte = Convert.ToInt32(txtCliente.Text);
                    fac2.Id_Ter = Convert.ToInt32(txtTerritorio.Text);

                    new CN_CapFactura().FacturaVI_ValidadorRequisicion(session, fac2, ref requisicion);
                    if (requisicion)
                    {
                        if (string.IsNullOrEmpty(txtReq.Text))
                        {
                            AlertaFocus("El campo Requisición es obligatorio", txtReq.ClientID);
                            RadToolBar1.Enabled = true;
                            return;
                        }
                    }

                }

                EntradaSalida entSal = new EntradaSalida();
                List<EntradaSalidaDetalle> listaEntSal = new List<EntradaSalidaDetalle>();
                List<EntradaSalida> listaEntSalRemisiones = new List<EntradaSalida>();
                string productosFactura = string.Empty; //para las notas de la entrada-salida
                float montoAmortizacionTotal = 0;

                //llenar datos de factura
                Factura factura = new Factura();
                this.LlenarFactura(ListaProductosFactura, ref factura);


                //EDSG18032016
                int id_TG = 0;
                if (Session["Id_TG_Fac" + Session.SessionID] != null) id_TG = Convert.ToInt32(Session["Id_TG_Fac" + Session.SessionID]);
                if (factura.Id_Tm == 91 && id_TG != 0 && !Guardar_FacEspecial)
                {
                    Alerta("La Factura de garantia debe ser especial");
                    return;
                }





                int error = 0;
                for (int m = 0; m < rgFacturaDet.Items.Count; m++)
                {
                    if ((rgFacturaDet.Items[m].FindControl("lblid_prdnum") as Label) == null)
                    {
                        Alerta("Capture al menos un producto para guardar la factura");
                        error = 1;
                        RadToolBar1.Enabled = true;
                        break;
                    }
                }
                if (error == 1)
                    return;

                for (int k = 0; k < rgFacturaDet.Items.Count; k++)
                {
                    int prd = Convert.ToInt32((rgFacturaDet.Items[k].FindControl("lblid_prdnum") as Label).Text);//rgFacturaDet.Items[k]["Id_Prd"].ToString());
                    int ter = Convert.ToInt32((rgFacturaDet.Items[k].FindControl("LblTerritorioPartidaNum") as Label).Text);//rgFacturaDet.Items[k]["Id_Ter"].ToString());
                    int cantidad = Convert.ToInt32((rgFacturaDet.Items[k].FindControl("lblord_cantidad") as Label).Text);//rgFacturaDet.Items[k]["Cantidad"].ToString());


                    if (ValidaPartidas(ter
                        , prd
                        , cantidad
                        ) == false)
                    {
                        RadToolBar1.Enabled = true;
                        return;
                    }
                    /*
                                            List<int> actuales = new List<int>();
                                            CN_CatProducto cn_producto = new CN_CatProducto();
                                            cn_producto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, prd.ToString(), ref actuales, session.Emp_Cnx);

                        

                                            int cantidad_B = 0;
                                            int disponible_pedido = 0;
                                            #region pedido
                                            if (txtPedido.Text != "")
                                            {
                                                CN_CapPedido cappedido = new CN_CapPedido();
                                                Pedido pedido = new Pedido();
                                                pedido.Id_Emp = session.Id_Emp;
                                                pedido.Id_Cd = session.Id_Cd_Ver;
                                                pedido.Id_Ped = Convert.ToInt32(txtPedido.Text);

                                                DataTable dt2 = new DataTable();
                                                dt2.Columns.Add("Id_PedDet");
                                                dt2.Columns.Add("Id_Ter");
                                                dt2.Columns.Add("Ter_Nombre");
                                                dt2.Columns.Add("Id_Prd");
                                                dt2.Columns.Add("Prd_Descripcion");
                                                dt2.Columns.Add("Prd_Presentacion");
                                                dt2.Columns.Add("Prd_Unidad");
                                                dt2.Columns.Add("Prd_Precio");
                                                dt2.Columns.Add("Disponible");
                                                dt2.Columns.Add("Prd_Importe");
                                                dt2.Columns.Add("Id_Rem");
                                                cappedido.ConsultaPedidoDetDisp(pedido, ref dt2, 1, session.Emp_Cnx);

                                                DataRow[] dr = dt2.Select("Id_Prd='" + prd + "'");

                                                if (dr.Length > 0)
                                                {
                                                    for (int i = 0; i < dr.Length; i++)
                                                        disponible_pedido += !string.IsNullOrEmpty(dr[i]["Disponible"].ToString()) ? Convert.ToInt32(dr[i]["Disponible"]) : 0;
                                                }
                                                if (disponible_pedido < 0)
                                                    disponible_pedido = 0;
                                            }
                                            #endregion

                                            DataRow[] Dr2 = objdtTabla.Select("Id_Prd='" + prd + "' and Id_Ter <> '" + ter + "'");
                                            if (Dr2.Length > 0)
                                            {
                                                for (int i = 0; i < Dr2.Length; i++)
                                                    cantidad_B += !string.IsNullOrEmpty(Dr2[i]["Fac_Cant"].ToString()) ? Convert.ToInt32(Dr2[i]["Fac_Cant"]) : 0;
                                            }

                                            int cantRemision = 0;


                                            if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                                            {
                                                List<Remision> listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];

                                                arrayRemisiones = "";
                                                foreach (Remision rem in listaRemisiones)
                                                {
                                                    arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                                                }
                                                if (arrayRemisiones.Length > 1)
                                                    arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);

                                                CN_CapRemision cr = new CN_CapRemision();
                                                cantRemision = cr.ConsultaCantidadRemision(session, prd, arrayRemisiones);
                                            }



                                            if (Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null)
                                            {
                                                List<Remision> listaRemisiones = new List<Remision>();

                                                foreach (DataRow facturaDet in objdtTabla.Rows)
                                                {
                                                    foreach (string idRem in facturaDet["Remisiones"].ToString().Split('|')
                                                                                                                .Select(rem => rem.Replace("|", ""))
                                                                                                                .Where(rem => !string.IsNullOrEmpty(rem)))
                                                    {
                                                        Remision remision = new Remision();
                                                        remision.Id_Rem = Convert.ToInt32(idRem);
                                                        remision.Id_Tm = Convert.ToInt32(facturaDet["Id_Tm"]);
                                                        listaRemisiones.Add(remision);
                                                    }
                                                }

                                                arrayRemisiones = "";
                                                foreach (Remision rem in listaRemisiones)
                                                {
                                                    arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                                                }
                                                if (arrayRemisiones.Length > 1)
                                                    arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);

                                                CN_CapRemision cr = new CN_CapRemision();
                                                cantRemision = cantidad;
                                            }
                                            else
                                            {
                                                int fac = Convert.ToInt32(Page.Request.QueryString["Id_Fac"]);
                                                if (fac != -1)
                                                    if (string.IsNullOrEmpty(txtPedido.Text))
                                                    {
                                                        CN_CapFactura fact = new CN_CapFactura();
                                                        fact.ConsultarCantidadProdFactura(session, prd, fac, ter, ref cantRemision);
                                                    }
                                                    else
                                                        cantRemision = cantidad_A;
                                            }*/

                }

                //ValidaPreciosEspeciales(ListaProductosFactura);
                //JMM:Se agrega ventana de precios especiales
                #region ValidarPreciosEsp
                //JMM: Si viene de enviar el mensaje de aceptacion de precios diferentes a los especiales lo ignora
                if (!AceptacionPrecios)
                {
                    //Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                    string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString();
                    ConvenioDet conv;
                    ConvenioDet convdet;
                    CN_Convenio cn_conv;
                    List<string> Productos = new List<string>();
                    List<string> Convenios = new List<string>();

                    foreach (DataRow dr in objdtTabla.Rows)
                    {
                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                        conv.Id_Prd = Convert.ToInt32(dr["Id_Prd"].ToString());

                        double PrecioIngresado = Convert.ToDouble(dr["Fac_Precio"]);

                        cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);


                        if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                        {
                            if (convdet.PCD_PrecioVtaMin != 0 && convdet.PCD_PrecioVtaMax != 0)
                            {
                                if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin || Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                {
                                    Productos.Add(convdet.Id_Prd.ToString());
                                    if (!Convenios.Contains(convdet.PC_NoConvenio + '-' + convdet.PC_Nombre))
                                    {
                                        Convenios.Add(convdet.PC_NoConvenio + '-' + convdet.PC_Nombre);
                                    }

                                }
                            }

                        }

                    }

                    Session["ProdsConv" + Session.SessionID] = null;
                    Session["ConvPrecios" + Session.SessionID] = null;
                    Session["Id_FacPrec" + Session.SessionID] = null;

                    if (Productos.Count > 0 && Convenios.Count > 0)
                    {
                        Session["ProdsConv" + Session.SessionID] = Productos;
                        Session["ConvPrecios" + Session.SessionID] = Convenios;
                        Session["Id_FacPrec" + Session.SessionID] = txtId.Text;


                        RAM1.ResponseScripts.Add("AbrirVentana_AlertaPrecios(); return false;");
                        //return;
                    }
                }
                #endregion

                /*
                * Si el tipo de movimiento fue 70 (caso de aparatos inproductivo):
                * Calcula la amortización . (Checar formulas para el cálculo de la amortización). 
                * Se genera un movimiento en el almacén (CapEntSal antes entsal,  CapEntSalDet antes entsal1). 
                * Se genera un movimiento 16 de tipo automático, en las referencia se captura el número de factura y hace referencia a la factura.
                */
                #region txtMov = 70
                if (txtMov.Text == "70")
                {
                    Amortizacion amortizacion = new Amortizacion();
                    amortizacion.Id_Emp = session.Id_Emp;
                    amortizacion.Id_Cd = session.Id_Cd_Ver;
                    amortizacion.Id_Cte = Convert.ToInt32(txtCliente.Text);
                    List<Amortizacion> listAmortizacion = new List<Amortizacion>();

                    //obtener productos con amortización del cliente
                    new CN_Amortizacion().ConsultaAmortizacionCliente(amortizacion, session.Emp_Cnx, ref listAmortizacion);
                    this.ListaProductosAmortizacion = listAmortizacion;

                    //calcula amortizacion de cada producto
                    int anioActual = DateTime.Now.Year;
                    int mesActual = DateTime.Now.Month;
                    //foreach (FacturaDet facturaDet in this.ListaProductosFactura)
                    foreach (DataRow facturaDet in objdtTabla.Rows)
                    {
                        //validar que todas las partidas tengan capturado el cliente externo
                        if (facturaDet["Id_CteExt"] == null)
                        {
                            RadMultiPage1.SelectedIndex = 1;
                            RadTabStrip1.SelectedIndex = RadTabStrip1.FindTabByValue("Detalles").Index;
                            throw new Exception("FacturacionClienteExtNoEnPartida");
                        }
                        productosFactura = string.Concat(productosFactura, "Prod ", facturaDet["Id_Prd"], ": ", facturaDet["Fac_Cant"], ", ");
                        float montoAmortizacion = 0;
                        foreach (Amortizacion amor in this.ListaProductosAmortizacion)
                        {
                            if (Convert.ToInt32(facturaDet["Id_Prd"]) == amor.Id_Prd)
                            {
                                //si el año y mes actual es mayor al año y mes de la amortizacion del producto
                                //la amortizacion se queda en 0
                                DateTime fechaFinAmortizacion = new DateTime(amor.Amo_AnioFin, amor.Amo_MesFin, 1);
                                DateTime fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                int mesesAmortizacion = 0;
                                if (((TimeSpan)(fechaFinAmortizacion.Subtract(fechaActual))).Ticks > 0)
                                {
                                    //calcula meses de amortizacion
                                    //al final al mes actual se le resta 1 porque aun no se acaba el mes actual
                                    mesesAmortizacion = (((anioActual - amor.Amo_AnioInicio) * 12) - amor.Amo_MesInicio) + (mesActual - 1);
                                }
                                float importeTotalAmortizacion = amor.Amo_Cant * amor.Amo_Costo;
                                montoAmortizacion = importeTotalAmortizacion / mesesAmortizacion;

                                facturaDet["AmortizacionProducto"] = montoAmortizacion;
                                montoAmortizacionTotal += montoAmortizacion;
                                break;
                            }
                        }

                        //Crear item de lista de entrada-salida mov. 16
                        EntradaSalidaDetalle entSalDetalle = new EntradaSalidaDetalle();
                        entSalDetalle.Id_Emp = session.Id_Emp;
                        entSalDetalle.Id_Cd = session.Id_Cd_Ver;
                        entSalDetalle.Id_Es = 0;//se reasigna al insertar la entSal de encabezado
                        entSalDetalle.Id_EsDet = 0;//se reasigna al insertar la entSalDetalle
                        entSalDetalle.Id_Ter = Convert.ToInt32(facturaDet["Id_Ter"]);//txtTerritorio.Text);
                        entSalDetalle.Id_Prd = Convert.ToInt32(facturaDet["Id_Prd"]);
                        entSalDetalle.EsDet_Naturaleza = 0; //entrada
                        entSalDetalle.Es_Cantidad = Convert.ToInt32(facturaDet["Fac_Cant"]);
                        entSalDetalle.Es_Costo = Convert.ToInt32(facturaDet["Fac_Precio"]);
                        entSalDetalle.Es_BuenEstado = true;
                        entSalDetalle.Afct_OrdCompra = false;
                        listaEntSal.Add(entSalDetalle);
                    }
                    productosFactura = (productosFactura.Length > 0) ? (productosFactura.Substring(0, productosFactura.Length - 2)) : productosFactura;

                    //llenar objeto de entrada-salida
                    entSal.Id_Emp = session.Id_Emp;
                    entSal.Id_Cd = session.Id_Cd_Ver;
                    entSal.Id_U = session.Id_U;
                    entSal.Id_Tm = 16; //mov. de entrada por aparatos inproductivos

                    try
                    {
                        entSal.Id_Cte = Convert.ToInt32(objdtTabla.Rows[0]["Id_CteExt"].ToString()); //Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                    }
                    catch
                    {

                    }
                    entSal.Id_Pvd = -1;
                    try
                    {
                        entSal.Id_Ter = Convert.ToInt32(objdtTabla.Rows[0]["Id_Ter"].ToString()); //Convert.ToInt32(txtTerritorio.Text);
                    }
                    catch
                    {

                    }


                    entSal.Es_Naturaleza = 0;//entrada
                    entSal.Es_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    entSal.Es_Referencia = "0"; //ref. de factura o remision
                    entSal.Es_Notas = string.Concat("Factura: #Id_Fac# Aparatos improductivos/Ter: ", entSal.Id_Ter, ", Cte: ", entSal.Id_Cte, " ", productosFactura);
                    entSal.Es_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                    entSal.Es_Iva = Convert.ToDouble(txtIVA.Text);
                    entSal.Es_Total = Convert.ToDouble(txtTotal.Text);
                    entSal.Es_Estatus = "C";
                }
                #endregion
                factura.Fac_SubTotal += montoAmortizacionTotal;
                // ------------------------------------------------------------------------------------------
                // checar si se esta facturando remisiones y obtener contrapartidas (ENTRADAS) de remisiones 
                // ------------------------------------------------------------------------------------------
                #region remisiones
                //if (Session["PedidoRemisionado" + Session.SessionID] != null)
                //{
                //    if (txtPedido.Text != "")
                //    {
                //        CN_CapRemision remision = new CN_CapRemision();
                //        List<Remision> listaRemisiones = new List<Remision>();
                //        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRemisiones);
                //        foreach (Remision rem in listaRemisiones)
                //        {
                //            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                //        }
                //        if (arrayRemisiones.Length > 1)
                //        {
                //            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                //            Session["ListaRemisionesFactura" + Session.SessionID] = listaRemisiones;
                //        }
                //    }
                //}
                List<Remision> listaRem = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                if (listaRem == null)
                {
                    if (!string.IsNullOrEmpty(txtPedido.Text))
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRem = new List<Remision>();
                        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRem);
                    }
                    else if (Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null)
                    {
                        List<Remision> listaRemisionesOrg = (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID];
                        listaRem = new List<Remision>();
                        XElement xe;

                        foreach (DataRow facturaDet in objdtTabla.Rows)
                        {
                            int vIdTer = Convert.ToInt32(facturaDet["Id_Ter"]);
                            int vIdPrd = Convert.ToInt32(facturaDet["Id_Prd"]);

                            xe = new XElement("Remisiones", listaRemisionesOrg.Where(x => x.Id_Ter == vIdTer && x.Id_Prd == vIdPrd)
                                                                              .Select(s => new XElement("Remision",
                                                                                    new XElement("Id_Rem", s.Id_Rem),
                                                                                    new XElement("Id_Ter", s.Id_Ter),
                                                                                    new XElement("Id_Prd", s.Id_Prd),
                                                                                    new XElement("Cant", s.Cant)
                                                                                                )
                                                                            ));

                            facturaDet["RemisionesXML"] = xe.ToString();

                            foreach (string idRem in facturaDet["Remisiones"].ToString().Split('|')
                                                                                        .Select(rem => rem.Replace("|", ""))
                                                                                        .Where(rem => !string.IsNullOrEmpty(rem)))
                            {
                                Remision remision = new Remision();
                                remision.Id_Rem = Convert.ToInt32(idRem);
                                remision.Id_Tm = Convert.ToInt32(facturaDet["Id_Tm"]);

                                if (!listaRem.Any(x => x.Id_Rem == remision.Id_Rem))
                                {
                                    listaRem.Add(remision);
                                }
                            }
                        }
                    }
                    else
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRem = new List<Remision>();
                        remision.ConsultaRemisionesxFactura(session, Convert.ToInt32(txtId.Text), ref listaRem);
                    }
                }




                //agregado para facturar un pedido-remision

                //agregado para facturar un pedido-remision
                if (listaRem == null || Session["PedidoRemisionado" + Session.SessionID] != null || listaRem.Count == 0)
                {
                    if (!string.IsNullOrEmpty(txtPedido.Text))
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRem = new List<Remision>();
                        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRem);
                    }
                }

                if (listaRem != null) //nueva factura de remisiones
                {
                    foreach (Remision rem in listaRem)
                    {
                        EntradaSalida entRem = new EntradaSalida();
                        //llenar objeto de entrada-salida
                        entRem.Id_Emp = session.Id_Emp;
                        entRem.Id_Cd = session.Id_Cd_Ver;
                        entRem.Id_U = session.Id_U;
                        entRem.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                        entRem.Id_Tm = rem.Id_Tm; //mov. de entrada por mov. inverso de facturacion de remision
                        entRem.Id_Cte = Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1);
                        entRem.Id_Pvd = -1;
                        entRem.Es_Naturaleza = 0;//entrada
                        entRem.Es_Fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        entRem.Es_Referencia = rem.Id_Rem.ToString(); //ref. de factura o remision
                        entRem.Es_Notas = string.Empty;//string.Concat("Remisión ", rem.Id_Rem.ToString());//, ", #producto-cantidad#");
                        entRem.Es_SubTotal = 0;
                        entRem.Es_Iva = 0;
                        entRem.Es_Total = 0;
                        entRem.Es_Estatus = "C";
                        entRem.Id_Rem = rem.Id_Rem;
                        entRem.ListaDetalle = new List<EntradaSalidaDetalle>();
                        listaEntSalRemisiones.Add(entRem);
                    }
                }
                #endregion
                //foreach (FacturaDet facturaDet in this.ListaProductosFactura)
                #region ListaProductosFactura
                foreach (DataRow facturaDet in objdtTabla.Rows)
                {
                    //esta validacion es por la columna especial de cliente externo (que en el Grid no puede ser null se maneja como 0), 
                    // --> si es 0 se guarda como null en base de datos
                    //if (facturaDet.Id_CteExt == 0) COMENTARIADO POR OSCAR
                    int? id_cteext = !Convert.IsDBNull(facturaDet["Id_CteExt"]) ? Convert.ToInt32(facturaDet["Id_CteExt"]) == 0 ? (int?)null : Convert.ToInt32(facturaDet["Id_CteExt"]) : null;

                    facturaDet["Id_CteExt"] = id_cteext;
                    //validar si es producto de remisión
                    //if (facturaDet.Id_Rem != null && facturaDet.Id_Rem > 0) COMENTARIADO POR OSCAR
                    if (!Convert.IsDBNull(facturaDet["Remisiones"]))
                        foreach (string idRem in facturaDet["Remisiones"].ToString().Split('|')
                                                                                    .Select(rem => rem.Replace("|", ""))
                                                                                    .Where(rem => !string.IsNullOrEmpty(rem)))
                        {
                            if (Convert.ToInt32(idRem) > 0)
                            {//Crear item de lista de entrada-salida mov. 16
                                EntradaSalidaDetalle entSalDetalleRem = new EntradaSalidaDetalle();
                                entSalDetalleRem.Id_Emp = session.Id_Emp;
                                entSalDetalleRem.Id_Cd = session.Id_Cd_Ver;
                                entSalDetalleRem.Id_Es = 0;//se reasigna al insertar la entSal de encabezado
                                entSalDetalleRem.Id_EsDet = 0;//se reasigna al insertar la entSalDetalle
                                entSalDetalleRem.Id_Ter = Convert.ToInt32(facturaDet["Id_Ter"]);//txtTerritorio.Text);
                                entSalDetalleRem.Id_Prd = Convert.ToInt32(facturaDet["Id_Prd"]); //facturaDet.Id_Prd;
                                entSalDetalleRem.EsDet_Naturaleza = 0; //entrada
                                entSalDetalleRem.Es_Cantidad = Convert.ToInt32(facturaDet["Fac_Cant"]); //facturaDet.Fac_Cant;
                                entSalDetalleRem.Es_Costo = Convert.ToDouble(facturaDet["Fac_Precio"]); //facturaDet.Fac_Precio;
                                entSalDetalleRem.Es_BuenEstado = true;
                                entSalDetalleRem.Afct_OrdCompra = false;
                                entSalDetalleRem.Id_Rem = Convert.ToInt32(idRem);
                                entSalDetalleRem.Es_CantidadRem = 0;// Convert.ToInt32(facturaDet["Rem_Cant"]);

                                EntradaSalida es = listaEntSalRemisiones.FirstOrDefault(x => x.Id_Rem == entSalDetalleRem.Id_Rem);
                                es.ListaDetalle.Add(entSalDetalleRem);

                                //foreach (EntradaSalida entRem in listaEntSalRemisiones)
                                //{
                                //    if (entRem.Es_Notas.Contains(string.Concat("Remisión ", facturaDet["Id_Rem"].ToString()))) //si las notas tienen la referencia a la remisión
                                //    {
                                //        //entRem.Es_Notas = entRem.Es_Notas.Replace("#producto-cantidad#",
                                //        //    string.Concat("producto ", facturaDet["Id_Prd"].ToString(), " cantidad ", facturaDet["Fac_Cant"], ", #producto-cantidad#"));
                                //        entRem.ListaDetalle.Add(entSalDetalleRem);
                                //        break;
                                //    }
                                //}
                            }
                        }
                }
                #endregion

                //calcular totales de movimientos de ENTRADA inversos por facturacion de remisiones
                #region totales de movimientos
                //if (listaEntSalRemisiones.Count > 0)
                //    for (int i = 0; i < listaEntSalRemisiones.Count; i++)
                //    {
                //        EntradaSalida entRem = listaEntSalRemisiones[i];
                //        this.CalcularTotalesEntradaRemision(ref entRem);
                //        entRem.Es_Notas = entRem.Es_Notas.Replace(", #producto-cantidad#", string.Empty);
                //    }
                #endregion

                #region Adenda
                List<AdendaDet> listAdendaCabecera = new List<AdendaDet>();
                List<AdendaDet> listAdendaNacionalCabecera = new List<AdendaDet>();
                AdendaDet ad;
                RadTextBox txtAdenda = new RadTextBox();

                List<AdendaDet> listCabT = new List<AdendaDet>();
                List<AdendaDet> listDetT = new List<AdendaDet>();
                List<AdendaDet> listCabR = new List<AdendaDet>();

                for (int i = 0; i < rgAdendaFacturacion.MasterTableView.Items.Count; i++)
                {
                    ad = new AdendaDet();
                    ad.Tipo = 1;
                    ad.Id_AdeDet = Convert.ToInt32(rgAdendaFacturacion.MasterTableView.Items[i]["Id_AdeDet"].Text);
                    ad.Campo = rgAdendaFacturacion.MasterTableView.Items[i]["campo"].Text;
                    ad.Nodo = rgAdendaFacturacion.MasterTableView.Items[i]["nodo"].Text;
                    txtAdenda = rgAdendaFacturacion.MasterTableView.Items[i]["valor"].FindControl("RadTextBox1") as RadTextBox;
                    ad.Valor = txtAdenda.Text.Replace("'", "").Trim();
                    bool addenda_Requerida = ListCab.Where(AdendaDet => AdendaDet.Id_AdeDet == ad.Id_AdeDet).ToList()[0].Requerido;
                    if (ad.Valor == "" && addenda_Requerida)
                    {
                        AlertaFocus("El campo <b>" + ad.Campo + "</b> de la addenda es requerido", txtAdenda.ClientID);
                        //RadTabStrip1.Tabs[1].Selected = true;
                        RadTabStrip1.Tabs[2].Selected = true;
                        rpvAdendaFacturacion.Selected = true;
                        RadToolBar1.Enabled = true;
                        return;
                    }
                    else
                        listAdendaCabecera.Add(ad);
                }


                for (int i = 0; i < rgAdendaFacturacionNacional.MasterTableView.Items.Count; i++)
                {
                    ad = new AdendaDet();
                    ad.Tipo = 1;
                    ad.Id_AdeDet = Convert.ToInt32(rgAdendaFacturacionNacional.MasterTableView.Items[i]["Id_AdeDet"].Text);
                    ad.Campo = rgAdendaFacturacionNacional.MasterTableView.Items[i]["campo"].Text;
                    ad.Nodo = rgAdendaFacturacionNacional.MasterTableView.Items[i]["nodo"].Text;
                    txtAdenda = rgAdendaFacturacionNacional.MasterTableView.Items[i]["valor"].FindControl("RadTextBox11") as RadTextBox;
                    ad.Valor = txtAdenda.Text.Replace("'", "").Trim();
                    bool addenda_Requerida = ListCabNacional.Where(AdendaDet => AdendaDet.Id_AdeDet == ad.Id_AdeDet).ToList()[0].Requerido;
                    if (ad.Valor == "" && addenda_Requerida)
                    {
                        AlertaFocus("El campo <b>" + ad.Campo + "</b> de la addenda nacional es requerido", txtAdenda.ClientID);
                        //RadTabStrip1.Tabs[1].Selected = true;
                        RadTabStrip1.Tabs[5].Selected = true;
                        rpvAdendaCuentaNacional.Selected = true;
                        RadToolBar1.Enabled = true;
                        return;
                    }
                    else
                        listAdendaNacionalCabecera.Add(ad);
                }

                for (int i = 0; i < rgAdendaReFacturacion.MasterTableView.Items.Count; i++)
                {
                    ad = new AdendaDet();
                    ad.Tipo = 7;
                    ad.Id_AdeDet = Convert.ToInt32(rgAdendaReFacturacion.MasterTableView.Items[i]["Id_AdeDet"].Text);
                    ad.Campo = rgAdendaReFacturacion.MasterTableView.Items[i]["campo"].Text;
                    ad.Nodo = rgAdendaReFacturacion.MasterTableView.Items[i]["nodo"].Text;
                    txtAdenda = rgAdendaReFacturacion.MasterTableView.Items[i]["valor"].FindControl("RadTextBox1") as RadTextBox;
                    ad.Valor = txtAdenda.Text.Replace("'", "").Trim();
                    bool addenda_Requerida = ListCabRF.Where(AdendaDet => AdendaDet.Id_AdeDet == ad.Id_AdeDet).ToList()[0].Requerido;
                    if (ad.Valor == "" && addenda_Requerida)
                    {
                        AlertaFocus("El campo <b>" + ad.Campo + "</b> de la addenda es requerido", txtAdenda.ClientID);
                        //RadTabStrip1.Tabs[2].Selected = true;
                        RadTabStrip1.Tabs[3].Selected = true;
                        rpvAdendaRefacturacion.Selected = true;
                        RadToolBar1.Enabled = true;
                        return;
                    }
                    else
                        listAdendaCabecera.Add(ad);
                }

                //VALIDA SI HAY CAMPOS REQUERIDOS DE DETALLES GUARDAR AL MENOS UN DETALLE DE ADENDA
                string requeridodetalle = "NO";
                if (ListDet.Count > 0 || ListDetRF.Count > 0)
                {
                    foreach (AdendaDet det in ListDet)
                    {
                        if (det.Requerido)
                        { requeridodetalle = "SI"; }
                    }
                    if (ListDetRF != null)
                    {
                        foreach (AdendaDet det in ListDetRF)
                        {
                            if (det.Requerido)
                            { requeridodetalle = "SI"; }
                        }
                    }

                    if (requeridodetalle == "SI")
                    {
                        if (rgFacturaDetAde.Items.Count == 0)
                        {
                            Alerta("Capture al menos un Detalle de Adenda para guardar la factura");
                            RadTabStrip1.Tabs[2].Selected = true;
                            rpvAdendaFacturacion.Selected = true;
                            RadToolBar1.Enabled = true;
                            return;
                        }
                    }
                }


                requeridodetalle = "NO";
                if (ListDetNacional.Count > 0)
                {
                    foreach (AdendaDet det in ListDetNacional)
                    {
                        if (det.Requerido)
                        { requeridodetalle = "SI"; }
                    }

                    if (requeridodetalle == "SI")
                    {
                        if (rgFacturaDetAdeNacional.Items.Count == 0)
                        {
                            Alerta("Capture al menos un Detalle de Adenda Nacional para guardar la factura");
                            RadTabStrip1.Tabs[5].Selected = true;
                            rpvAdendaCuentaNacional.Selected = true;
                            RadToolBar1.Enabled = true;
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ListaProductosFacturaAdenda.Rows)
                {
                    foreach (AdendaDet det in ListDet)
                    {
                        if (dr[det.Id_AdeDet.ToString()] != null && dr[det.Id_AdeDet.ToString()].ToString().Trim() == "" && det.Requerido)
                        {
                            Alerta("El campo <b>" + det.Campo + "</b> de la addenda es requerido");
                            //RadTabStrip1.Tabs[3].Selected = true;
                            RadTabStrip1.Tabs[2].Selected = true;
                            RadPageViewDetalles.Selected = true;
                            RadToolBar1.Enabled = true;
                            return;
                        }
                    }
                    if (ListDetRF != null)
                    {
                        foreach (AdendaDet det in ListDetRF)
                        {
                            if (dr[det.Id_AdeDet.ToString()] != null && dr[det.Id_AdeDet.ToString()].ToString().Trim() == "" && det.Requerido)
                            {
                                Alerta("El campo <b>" + det.Campo + "</b> de la addenda es requerido");
                                //RadTabStrip1.Tabs[3].Selected = true;
                                RadTabStrip1.Tabs[2].Selected = true;
                                RadPageViewDetalles.Selected = true;
                                RadToolBar1.Enabled = true;
                                return;
                            }
                        }
                    }
                }

                foreach (DataRow dr in ListaProductosFacturaAdendaNacional.Rows)
                {
                    foreach (AdendaDet det in ListDetNacional)
                    {
                        if (dr[det.Id_AdeDet.ToString()] != null && dr[det.Id_AdeDet.ToString()].ToString().Trim() == "" && det.Requerido)
                        {
                            Alerta("El campo <b>" + det.Campo + "</b> de la addenda es requerido");
                            //RadTabStrip1.Tabs[3].Selected = true;
                            RadTabStrip1.Tabs[5].Selected = true;
                            rpvAdendaCuentaNacional.Selected = true;
                            RadToolBar1.Enabled = true;
                            return;
                        }
                    }
                }
                //VALIDAMOS EL SUBTOTAL DE LA FACTURA CON LA ADENDA
                bool calcularAdendaTrue2 = false;
                bool subtotalesIguales2 = false;
                CalcularAdenda(out calcularAdendaTrue2, out subtotalesIguales2);

                if (calcularAdendaTrue2 == true)
                {
                    if (subtotalesIguales2 != true)
                    {
                        Alerta("El Subtotal de la Addenda no coincide con el subtotal de la Factura!");
                        RadTabStrip1.Tabs[2].Selected = true;
                        rpvAdendaFacturacion.Selected = true;
                        RadToolBar1.Enabled = true;
                        return;
                    }
                }


                calcularAdendaTrue2 = false;
                subtotalesIguales2 = false;
                CalcularAdendaNacional(out calcularAdendaTrue2, out subtotalesIguales2);

                if (calcularAdendaTrue2 == true)
                {
                    if (subtotalesIguales2 != true)
                    {
                        Alerta("El Subtotal de la Addenda Nacional no coincide con el subtotal de la Factura!");
                        RadTabStrip1.Tabs[2].Selected = true;
                        rpvAdendaCuentaNacional.Selected = true;
                        RadToolBar1.Enabled = true;
                        return;
                    }
                }

                //VALIDAMOS EL SUBTOTAL DE LA FACTURA CON LA ADENDA NACIONAL
                //calcularAdendaTrue2 = false;
                //subtotalesIguales2 = false;
                //CalcularAdenda(out calcularAdendaTrue2, out subtotalesIguales2);

                //if (calcularAdendaTrue2 == true)
                //{
                //    if (subtotalesIguales2 != true)
                //    {
                //        Alerta("El Subtotal de la Addenda Nacional no coincide con el subtotal de la Factura!");
                //        RadTabStrip1.Tabs[5].Selected = true;
                //        rpvAdendaCuentaNacional.Selected = true;
                //        return;
                //    }
                //}      


                #endregion
                // Evita que se guarde el documento si los totales no coinciden
                #region validacion
                if (ListaProductosFacturaEspecial != null)
                {
                    double totalEspecial = 0;
                    foreach (FacturaDet ncd in ListaProductosFacturaEspecial)
                    {
                        totalEspecial += ncd.Fac_ImporteE;
                    }

                    //Datos del centro de distribución (Para obtener la tolerancia o diferencia permitida entre totales de fac y fact especial)
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);

                    // Se indico que solo podía haber diferecia de 90 centavos
                    double TE1 = (Math.Round(txtImporte.Value.Value, 2) + Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // Math.Round(totalEspecial, 2) + .90; // se suman 70 centavos al total especial -- modificar si se desea disminuir o aumentar el rango
                    double TE2 = (Math.Round(txtImporte.Value.Value, 2) - Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // se restan 70 centavos al total especia


                    // se cambia la validacion ahora solo se valida que el total de la factura este dentro del rango permitido

                    if (!(Math.Round(totalEspecial, 2) <= TE1 && Math.Round(totalEspecial, 2) >= TE2) && this.cmbMov.SelectedValue != "91")
                    {
                        Alerta("El total del documento especial no es igual al total del documento original");// si la diferencia es de mas menos 70 centavos muestra el mensaje
                        RadToolBar1.Enabled = true;
                        return;
                    }
                }

                #endregion

                #region arrayRemisiones
                if (string.IsNullOrEmpty(arrayRemisiones))
                {
                    List<Remision> listaRemisiones;
                    if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                    {
                        listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                    }
                    else
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        listaRemisiones = new List<Remision>();
                        remision.ConsultaRemisionesxFactura(session, Convert.ToInt32(txtId.Text), ref listaRemisiones);
                    }
                    foreach (Remision rem in listaRemisiones)
                    {
                        arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                    }
                    if (arrayRemisiones.Length > 1)
                        arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                    Session["ListaRemisionesFactura" + Session.SessionID] = null;
                }

                //-- facturar pedido que tiene remision auto
                if (string.IsNullOrEmpty(arrayRemisiones))
                {
                    if (txtPedido.Text != "")
                    {
                        CN_CapRemision remision = new CN_CapRemision();
                        List<Remision> listaRemisiones = new List<Remision>();
                        remision.ConsultaRemisionesxPedido(session, Convert.ToInt32(txtPedido.Text), ref listaRemisiones);
                        foreach (Remision rem in listaRemisiones)
                        {
                            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                        }
                        if (arrayRemisiones.Length > 1)
                            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                    }
                }
                #endregion

                //***--------------------------------------***
                //***          GUARDAR FACTURA             ***
                //***--------------------------------------***
                int verificador = 0;
                string mensaje = string.Empty;
                //List<FacturaDet> listaFacturaDetalle = this.ListaProductosFactura;
                DataTable listaFacturaDetalle = objdtTabla;
                DataTable listaFacturaDetalleAdenda = ListaProductosFacturaAdenda;
                DataTable listaFacturaDetalleAdendaNacional = ListaProductosFacturaAdendaNacional;
                //----checar si se esta facturando un pedido-----
                int? pedidoPrevio = null;
                if (Session["PedidoRemisionado" + Session.SessionID] != null)
                    Session["PedidoRemisionado" + Session.SessionID] = null;
                else
                {
                    if (txtPedido.Text != string.Empty)
                        pedidoPrevio = Convert.ToInt32(txtPedido.Text);
                }
                //int columnas_RF = (ListDetRF != null ? ListDetRF.Count : 0) / (ListaProductosFactura != null ? objdtTabla.Rows.Count : 1);

                int dividendo = 0;
                int divisor = 0;

                if (ListDetRF != null)
                {
                    dividendo = ListDetRF.Count;
                }

                if (objdtTabla != null)
                {
                    divisor = objdtTabla.Rows.Count;
                }
                else
                {
                    divisor = 1;
                }

                int columnas_RF = divisor == 0 ? 0 : dividendo / divisor;
                bool bVerificador = true;

                if (_PermisoGuardar)
                {// NUEVA FACTURA    



                    #region Factura Especial
                    //Guardar los datos de los productos de factura especial
                    //en catálogo de Cli ente-Producto                    

                    List<FacturaDet> listaProductosFacturaEspecial = new List<FacturaDet>();
                    FacturaEspecial facturaEsp = null;

                    if (ListaProductosFacturaEspecial != null)
                    {
                        if (Guardar_FacEspecial == true) //guarda solo si hizo clic en guardar en pantalla de facturaEspecial.
                        {
                            facturaEsp = new FacturaEspecial();
                            facturaEsp.Id_Emp = factura.Id_Emp;
                            facturaEsp.Id_Cd = factura.Id_Cd;
                            facturaEsp.Id_Fac = factura.Id_Fac;
                            facturaEsp.Id_Ter = Convert.ToInt32(factura.Id_Ter);
                            facturaEsp.FacEsp_Fecha = factura.Fac_Fecha;
                            facturaEsp.FacEsp_Importe = Convert.ToDouble(factura.Fac_Importe);
                            facturaEsp.FacEsp_SubTotal = Convert.ToDouble(factura.Fac_SubTotal);
                            facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(factura.Fac_ImporteIva);
                            facturaEsp.FacEsp_Total = Convert.ToDouble(factura.Fac_Importe);

                            listaProductosFacturaEspecial = ListaProductosFacturaEspecial;
                            // new CN_CapFactura().ModificarFacturaEspecial(ref facturaEsp, ref listaProductosFacturaEspecial, session.Emp_Cnx, ref verificador, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);
                        }
                    }
                    #endregion


                    double fTotal = 0.0;
                    double fTotalEsp = 0.0;
                    bool bEncontrados = true;

                    // Obtener Total de factura
                    foreach (DataRow dr in listaFacturaDetalle.Rows)
                        fTotal += Convert.ToDouble(dr["Fac_Importe"]);


                    // EDSG 08122015



                    if (factura.Id_Tm == 91 && id_TG != 0)
                    {
                        foreach (DataRow dr in listaFacturaDetalle.Rows)
                        {
                            dr["Fac_Precio_Original"] = Convert.ToDouble(dr["Fac_Precio"]);
                            dr["Fac_Precio"] = Convert.ToDouble(dr["Precio_Venta"]);
                        }
                    }


                    // Obtener Total de factura Especial (si la hay)
                    if (listaProductosFacturaEspecial.Count > 0)
                    {
                        //Calcular el monto total de las partidas para comparar el de la factura especial con la de detalle.
                        foreach (FacturaDet f in listaProductosFacturaEspecial)
                            fTotalEsp += Convert.ToDouble(f.Fac_ImporteE);

                        //Buscar que todos los Id´s de productos de la factura especial estén también en la factura detalle.
                        foreach (FacturaDet f in listaProductosFacturaEspecial)
                        {
                            bEncontrados = false;
                            for (int m = 0; m < rgFacturaDet.Items.Count; m++)
                            {
                                if ((rgFacturaDet.Items[m].FindControl("lblId_PrdNum") as Label).Text == f.Id_Prd.ToString())
                                {
                                    bEncontrados = true;
                                    break;
                                }
                            }
                            if (bEncontrados == false)
                                break;
                        }
                    }


                    #region nueva factura

                    if (this.hiddenId.Value == string.Empty)
                    {

                        if (bEncontrados == false && this.txtMov.Text != "91")
                        {
                            mensaje = "La factura especial contiene partidas con productos distintos a la factura original.";
                            bVerificador = false;
                            this.DisplayMensajeAlerta(mensaje);
                        }
                        else
                        {
                            // Si es refactura llena los campos.
                            // RFH 01032018
                            if (EsRefactura == true)
                            {
                                int iTipoRef = 0;
                                if (ChkRefacturatotal.Checked)
                                {
                                    iTipoRef = 1;
                                }
                                if (ChkRefacturaparcial.Checked)
                                {
                                    iTipoRef = 2;
                                }
                                factura.Fac_IdUsuRef = session.Id_U.ToString();
                                factura.Fac_IdCausaRef = cmbCausaRef.SelectedValue.ToString();
                                factura.Fac_TipoRef = iTipoRef.ToString();
                                factura.Fac_EsRefactura = "1";
                            }
                            if (RadTabStrip1.Tabs[4].Visible)
                            {
                                Factura facturaNacional = new Factura();
                                facturaNacional.Id_Emp = session.Id_Emp;
                                facturaNacional.Id_Cd = session.Id_Cd_Ver;

                                //EDSG 29052018
                                if (ListCuentaNacional == null)
                                    facturaNacional.Id_Cte = Int32.Parse(txtClienteNacional.Value.ToString());
                                else
                                    facturaNacional.Id_Cte = ListCuentaNacional.FirstOrDefault().CFD_CuentaNacional_Id;

                                facturaNacional.Cte_NomComercial = txtClienteNacionalNombre.Text;
                                facturaNacional.Fac_CteCalle = TxtClienteNacionalCalle.Text;
                                facturaNacional.Fac_CteNumero = TxtClienteNacionalNoExterior.Text;
                                facturaNacional.Fac_CteColonia = TxtClienteNacionalColonia.Text;
                                facturaNacional.Fac_CteMunicipio = TxtClienteNacionalMunicipio.Text;
                                facturaNacional.Fac_CteEstado = TxtClienteNacionalEstado.Text;
                                facturaNacional.Fac_CteRfc = TxtClienteNacionalRfc.Text;
                                facturaNacional.Fac_CteCp = TxtClienteNacionalCp.Text;
                                facturaNacional.Fac_CteAdeNombre = TxtClienteNacionalAdenda.Text;
                                facturaNacional.Fac_CteAdeId = 9;

                                //CAMBIO EN EL INSERT GP
                                //new CN_CapFactura().InsertarFactura(session, ref factura, ref listaFacturaDetalle, ref listaFacturaDetalleAdenda, columnas_RF, session.Emp_Cnx, ref verificador, ref pedidoPrevio, ref listaEntSalRemisiones, listAdendaCabecera, HiddenIdRF.Value, arrayRemisiones, ref entSal, ref listaEntSal, ref facturaEsp, ref listaProductosFacturaEspecial, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);
                                new CN_CapFactura().InsertarFactura(session, ref factura, ref facturaNacional, ref listaFacturaDetalle, ref listaFacturaDetalleAdenda, ref listaFacturaDetalleAdendaNacional, columnas_RF, session.Emp_Cnx, ref verificador, ref pedidoPrevio, ref listaEntSalRemisiones, listAdendaCabecera, listAdendaNacionalCabecera, HiddenIdRF.Value, arrayRemisiones, ref entSal, ref listaEntSal, ref facturaEsp, ref listaProductosFacturaEspecial, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);
                            }
                            else
                            {
                                new CN_CapFactura().InsertarFactura(session, ref factura, ref listaFacturaDetalle, ref listaFacturaDetalleAdenda, columnas_RF, session.Emp_Cnx, ref verificador, ref pedidoPrevio, ref listaEntSalRemisiones, listAdendaCabecera, HiddenIdRF.Value, arrayRemisiones, ref entSal, ref listaEntSal, ref facturaEsp, ref listaProductosFacturaEspecial, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);
                            }

                            if (verificador == -2)
                            {
                                Alerta("No se pudo insertar la factura, favor de revisar el tipo de remisión a facturar porque no tiene tipo de movimiento inverso");
                                RadToolBar1.Enabled = true;
                                return;
                            }
                            new CN_Rendimientos().InsertarRendimientos(session, session.Emp_Cnx, Session.SessionID, ref factura, ref verificador);
                            mensaje = "Se creó correctamente la factura <b>#" + factura.Id_Fac.ToString() + "</b>";
                            bVerificador = true;
                        }
                    }
                    #endregion
                    // ACTUALIZAR FACTURA                   
                    #region Actualizar factura
                    else
                    {
                        //Datos del centro de distribución (Para obtener la tolerancia o diferencia permitida entre totales de fac y fact especial)
                        CentroDistribucion cd = new CentroDistribucion();
                        new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);

                        //if (bEncontrados )
                        //{
                        //     string sListaProductos = string.Empty;
                        //    for (int i = 0; i < listaFacturaDetalle.Rows.Count-1; i++)
                        //        sListaProductos = sListaProductos + listaFacturaDetalle.Rows[i]["Id_Prd"] + ",";

                        //    sListaProductos = sListaProductos.Substring(0, sListaProductos.Length - 1);
                        //    bEncontrados = new CN_CapFactura().ValidaProductos(factura, 2, sListaProductos, session.Emp_Cnx);
                        //}

                        if (bEncontrados == false)
                        {
                            mensaje = "La factura especial contiene partidas con productos distintos a la factura original.";
                            bVerificador = false;
                            this.DisplayMensajeAlerta(mensaje);
                        }
                        else
                        {

                            if (listaProductosFacturaEspecial.Count > 0 && ((Math.Round((fTotal + (double)cd.Cd_MargenDiferenciaDocs), 2) < Math.Round(fTotalEsp, 2)) || (Math.Round((fTotal - (double)cd.Cd_MargenDiferenciaDocs), 2) > Math.Round(fTotalEsp, 2))) && cmbMov.SelectedValue != "91")
                            {
                                mensaje = "Los montos de la factura y la factura especial tienen una diferencia considerable, favor de revisarlos.";
                                bVerificador = false;
                                this.DisplayMensajeAlerta(mensaje);
                            }
                            else
                            {
                                if (txtMov.Text == "70")
                                {
                                    entSal.Es_Referencia = factura.Id_Fac.ToString(); //referencia a la factura
                                    entSal.Es_Notas = entSal.Es_Notas.Replace("#Id_Fac#", factura.Id_Fac.ToString());
                                    if (this.hiddenId_Es.Value != string.Empty)
                                        entSal.Id_Es = Convert.ToInt32(this.hiddenId_Es.Value);
                                    else
                                        throw new Exception("CapFactura_Id_Es_NoEncontrado");
                                }

                                if (RadTabStrip1.Tabs[4].Visible)
                                {
                                    Factura facturaNacional = new Factura();
                                    facturaNacional.Id_Emp = session.Id_Emp;
                                    facturaNacional.Id_Cd = session.Id_Cd_Ver;
                                    facturaNacional.Id_Cte = Int32.Parse(txtClienteNacional.Value.ToString());
                                    facturaNacional.Cte_NomComercial = txtClienteNacionalNombre.Text;
                                    facturaNacional.Fac_CteCalle = TxtClienteNacionalCalle.Text;
                                    facturaNacional.Fac_CteNumero = TxtClienteNacionalNoExterior.Text;
                                    facturaNacional.Fac_CteColonia = TxtClienteNacionalColonia.Text;
                                    facturaNacional.Fac_CteMunicipio = TxtClienteNacionalMunicipio.Text;
                                    facturaNacional.Fac_CteEstado = TxtClienteNacionalEstado.Text;
                                    facturaNacional.Fac_CteRfc = TxtClienteNacionalRfc.Text;
                                    facturaNacional.Fac_CteCp = TxtClienteNacionalCp.Text;
                                    facturaNacional.Fac_CteAdeNombre = TxtClienteNacionalAdenda.Text;
                                    facturaNacional.Fac_CteAdeId = 9;

                                    new CN_CapFactura().ModificarFactura(session, ref factura
                                     , ref facturaNacional
                                     , ref listaFacturaDetalle
                                     , ref listaFacturaDetalleAdenda
                                     , ref listaFacturaDetalleAdendaNacional
                                     , columnas_RF
                                     , session.Emp_Cnx
                                     , ref verificador
                                     , ref pedidoPrevio
                                     , ref listaEntSalRemisiones,
                                     listAdendaCabecera,
                                     listAdendaNacionalCabecera,
                                     arrayRemisiones, ref entSal, ref listaEntSal, ref facturaEsp, ref listaProductosFacturaEspecial, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);

                                }
                                else
                                {

                                    new CN_CapFactura().ModificarFactura(session, ref factura
                                        , ref listaFacturaDetalle
                                        , ref listaFacturaDetalleAdenda
                                        , columnas_RF
                                        , session.Emp_Cnx
                                        , ref verificador
                                        , ref pedidoPrevio
                                        , ref listaEntSalRemisiones,
                                        listAdendaCabecera,
                                        arrayRemisiones, ref entSal, ref listaEntSal, ref facturaEsp, ref listaProductosFacturaEspecial, string.IsNullOrEmpty(this.hiddenId.Value) ? false : true);

                                }



                                new CN_Rendimientos().InsertarRendimientos(session, session.Emp_Cnx, Session.SessionID, ref factura, ref verificador);

                                mensaje = "Los datos se modificaron correctamente";
                                bVerificador = true;
                            }
                        }
                    }

                    #endregion
                }
                //SI GUARDÓ BIEN LA FACTURA:

                if (HF_VI.Value == "true")
                {
                    Session["PreguntarImpresionVI" + Session.SessionID] = factura.Id_Fac;
                }
                if (bVerificador)
                {
                    Session["ListaRemisionesFactura" + Session.SessionID] = null;
                    Session["ListaDevolucionRemisionesFactura" + Session.SessionID] = null;
                    Session["PedidoFacturacion" + Session.SessionID] = null;
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                }
                RadToolBar1.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void validarDetalleInventario(DataTable listaFacturaDetalle)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                foreach (DataRow dr in listaFacturaDetalle.Rows)
                {
                    if (txtPedido.Text != "")
                    {
                        int validador = 0;
                        int cantidadDetalle = Convert.ToInt32(dr["Fac_Cant"]);
                        int producto = Convert.ToInt32(dr["Id_Prd"].ToString());
                        int pedido = !string.IsNullOrEmpty(txtPedido.Text) ? Convert.ToInt32(txtPedido.Text) : -1;
                        new CN_CapFactura().ValidarDisponibilidadInventario(session, cantidadDetalle, producto, pedido, ref validador);

                        if (validador == 0)
                        {
                            throw new Exception("No hay cantidad suficiente en el inventario para facturar el producto # " + producto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConsultarDatosDevolucionRemisiones()
        {
            try
            {
                if (Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null)
                {
                    Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                    List<Remision> listaRemisiones = (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID];
                    //----------------------------
                    // llenar datos de factura
                    //----------------------------              
                    if (listaRemisiones.Count > 0)
                    {
                        if (this.ConsultarDatosCliente(listaRemisiones[0].Id_Cte.ToString(), false)) //si la consulta de datos del cliente es correcta
                        {
                            txtCliente.Text = listaRemisiones[0].Id_Cte.ToString();
                            if (!string.IsNullOrEmpty(listaRemisiones[0].Cte_NomComercial))
                                txtClienteNombre.Text = listaRemisiones[0].Cte_NomComercial;
                            else
                                txtClienteNombre.Text = listaRemisiones[0].NombreCliente;
                        }
                        //---------------------------------------------------------------------------------------
                        // Consulta partidas de la remisones y las pasa a partidas de detalle de factura
                        //---------------------------------------------------------------------------------------
                        DataTable listaFacturaDet = new DataTable();
                        InicializarTablaProductosRemisiones(ref listaFacturaDet);
                        //List<FacturaDet> listaFactura = new List<FacturaDet>();
                        Remision remision = new Remision();

                        this.objdtTabla.Clear();
                        //obtener string de remisiones                        
                        XElement xe = new XElement("Remisiones", listaRemisiones.Select(s => new XElement("Remision",
                                                                                                new XElement("Id_Rem", s.Id_Rem),
                                                                                                new XElement("Id_Ter", s.Id_Ter),
                                                                                                new XElement("Id_Prd", s.Id_Prd),
                                                                                                new XElement("Cant", s.Cant)
                                                                                                          )
                                                                                        ));
                        arrayRemisiones = xe.ToString();

                        remision.Id_Emp = session.Id_Emp;
                        remision.Id_Cd = session.Id_Cd_Ver;
                        remision.Id_Tm = listaRemisiones[0].Id_Tm;
                        remision.Id_Cte = listaRemisiones[0].Id_Cte;
                        remision.Id_Ter = listaRemisiones[0].Id_Ter;
                        remision.IdTg = Convert.ToInt32(Session["Id_TG_Fac" + Session.SessionID]);

                        new CN_CapRemision().ConsultaDevolucionRemisionDetalleFacturacionAgrupado(ref remision, ref listaFacturaDet, arrayRemisiones, session.Emp_Cnx);

                        Double factorGarantia = 0;

                        CN_CapAcys cnAcys = new CN_CapAcys();
                        // AcysDatosGarantia datosGar = cnAcys.DatosGarantia_Consulta_Remision(session.Emp_Cnx, listaRemisiones[0].Id_Rem).FirstOrDefault();
                        AcysDatosGarantia datosGar = cnAcys.DatosGarantia_Consulta_Remision(session.Emp_Cnx, listaRemisiones[0].Id_Rem, session.Id_Emp, session.Id_Cd, int.Parse(txtCliente.Text), int.Parse(txtTerritorio.Text)).FirstOrDefault();

                        if (datosGar != null)
                            factorGarantia = datosGar.FactorGarantia;

                        if (datosGar != null && remision.Id_Tm == 92 && datosGar.Id_TG > 0)
                        {
                            Prorrateo(listaFacturaDet, factorGarantia);
                            this.rgFacturaDet.Columns[16].Visible = true;
                            this.rgFacturaDet.Columns[17].Visible = true;
                            this.rgFacturaDet.Columns[18].Visible = true;
                            this.rgFacturaDet.Columns[19].Visible = false;
                            this.rgFacturaDet.Columns[20].Visible = false;

                            this.rgFacturaEspecialDet.Columns[12].Visible = false;
                            this.rgFacturaEspecialDet.Columns[13].Visible = false;

                        }
                        else
                        {
                            this.rgFacturaDet.Columns[16].Visible = false;
                            this.rgFacturaDet.Columns[17].Visible = false;
                            this.rgFacturaDet.Columns[18].Visible = false;
                            this.rgFacturaDet.Columns[19].Visible = true;
                            this.rgFacturaDet.Columns[20].Visible = true;

                            this.rgFacturaEspecialDet.Columns[12].Visible = true;
                            this.rgFacturaEspecialDet.Columns[13].Visible = true;

                            this.txtUnidadesGarantia.Visible = false;
                            this.lblUnidadesGarantia.Visible = false;
                        }



                        this.objdtTabla = listaFacturaDet;

                        rgFacturaDet.Rebind();
                        this.CalcularTotales(remision.Id_Tm);

                        if (remision.Id_Tm == 92)
                        {
                            this.txtMov.Text = "91";
                            this.cmbMov.SelectedValue = "91";
                            this.cmbMov.Text = (this.cmbMov.FindItemByValue("91")).Text;

                            // this.Inicializar();
                        }

                        // se cambian estas lineas de codigo para agregar validacion y habilitar adenda al llegar 
                        // de las devoluciones de remisiones con mov 63

                        //this.rgFacturaDet.Enabled = false;
                        //this.rgFacturaDetAde.Enabled = false;
                        //this.rgAdendaFacturacion.Enabled = false;
                        //this.btnFacturaEspecial.Enabled = false;

                        if ((txtCliente.Text != string.Empty)
                            && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                            && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                        {
                            this.rgFacturaDet.Enabled = true;
                            this.rgFacturaDetAde.Enabled = true;
                            this.btnFacturaEspecial.Enabled = true;
                            this.rgAdendaFacturacion.Enabled = true;
                        }
                        else
                        {
                            this.rgFacturaDet.Enabled = false;
                            this.rgFacturaDetAde.Enabled = false;
                            this.rgAdendaFacturacion.Enabled = false;
                            this.btnFacturaEspecial.Enabled = false;
                        }

                        txtCliente.Enabled = false;
                        txtClienteNombre.Enabled = false;
                        cmbConsFacEle.Enabled = false;
                        txtTerritorio.Enabled = false;
                        txtMov.Enabled = false;
                        cmbMov.Enabled = false;
                        imgBuscar.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void ConsultarDatosRemisiones()
        {
            try
            {
                if (Session["ListaRemisionesFactura" + Session.SessionID] != null)
                {
                    Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                    List<Remision> listaRemisiones = (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID];
                    //----------------------------
                    // llenar datos de factura
                    //----------------------------              
                    if (listaRemisiones.Count > 0)
                    {
                        if (this.ConsultarDatosCliente(listaRemisiones[0].Id_Cte.ToString(), false)) //si la consulta de datos del cliente es correcta
                        {
                            txtCliente.Text = listaRemisiones[0].Id_Cte.ToString();
                            if (!string.IsNullOrEmpty(listaRemisiones[0].Cte_NomComercial))
                                txtClienteNombre.Text = listaRemisiones[0].Cte_NomComercial;
                            else
                                txtClienteNombre.Text = listaRemisiones[0].NombreCliente;
                        }
                        //---------------------------------------------------------------------------------------
                        // Consulta partidas de la remisones y las pasa a partidas de detalle de factura
                        //---------------------------------------------------------------------------------------
                        DataTable listaFacturaDet = new DataTable();
                        InicializarTablaProductosRemisiones(ref listaFacturaDet);
                        //List<FacturaDet> listaFactura = new List<FacturaDet>();
                        Remision remision = new Remision();

                        this.objdtTabla.Clear();
                        //obtener string de remisiones
                        foreach (Remision rem in listaRemisiones)
                        {
                            arrayRemisiones = string.Concat(arrayRemisiones, rem.Id_Rem.ToString(), "|");
                        }
                        if (arrayRemisiones.Length > 1)
                            arrayRemisiones = arrayRemisiones.Substring(0, arrayRemisiones.Length - 1);
                        remision.Id_Emp = session.Id_Emp;
                        remision.Id_Cd = session.Id_Cd_Ver;

                        new CN_CapRemision().ConsultaRemisionDetalleFacturacion(ref remision, ref listaFacturaDet, arrayRemisiones, session.Emp_Cnx);
                        this.objdtTabla = listaFacturaDet;
                        rgFacturaDet.Rebind();
                        this.CalcularTotales();

                        if ((txtCliente.Text != string.Empty)
                            && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                            && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                        {
                            this.rgFacturaDet.Enabled = true;
                            this.rgFacturaDetAde.Enabled = true;
                            this.btnFacturaEspecial.Enabled = true;
                            this.rgAdendaFacturacion.Enabled = true;
                        }
                        else
                        {
                            this.rgFacturaDet.Enabled = false;
                            this.rgFacturaDetAde.Enabled = false;
                            this.rgAdendaFacturacion.Enabled = false;
                            this.btnFacturaEspecial.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConsultarDatosPedido()
        {
            try
            {
                if (Session["PedidoFacturacion" + Session.SessionID] != null)
                {
                    if (Session["PedidoVI" + Session.SessionID] != null)
                    {
                        Session["PedidoVI" + Session.SessionID] = null;
                        HF_VI.Value = "true";
                    }
                    Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                    DataTable dtPedido = new DataTable();
                    Pedido pedido = new Pedido();
                    List<Pedido> listaPedido = new List<Pedido>();
                    int pedidoFacturacion = Convert.ToInt32(Session["PedidoFacturacion" + Session.SessionID]);
                    pedido.Filtro_Doc = "F";
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Ped = pedidoFacturacion;
                    new CN_CapPedido().ConsultaPedido(ref pedido, session.Emp_Cnx);

                    // Solo es posible remisionar pedidos que no sean de Venta Instalada
                    //if (pedido.Ped_Tipo != 4)
                    //{

                    List<PedidoDet> List = GetList(pedidoFacturacion, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);
                    bool bTieneAsignado = false;

                    foreach (var PedidoDet in List)
                        if (PedidoDet.Prd_Asig > 0)
                            bTieneAsignado = true;


                    if (bTieneAsignado)
                    {
                        txtPedido.Text = pedido.Id_Ped.ToString();
                        if (pedido.Ped_Tipo == 3 || pedido.Ped_Tipo == 4)//si es pedido captado
                            if (!string.IsNullOrEmpty(pedido.Requisicion))
                                txtReq.Text = pedido.Requisicion;
                            else
                                txtReq.Text = pedido.Ped_OrdenCompra.ToString();
                        else//si pedido es normal, va en orden de entrega
                        {
                            txtOrden.Text = pedido.Ped_OrdenEntrega;
                            txtReq.Text = pedido.Requisicion;
                        }
                        txtNotas.Text = pedido.Ped_Comentarios;
                        //txtReq.Text = pedido.Ped_OrdenCompra.ToString();
                        txtDescuento1.Text = pedido.Ped_DescPorcen1.ToString();
                        txtDescuento2.Text = pedido.Ped_DescPorcen2.ToString();
                        txtDescPorc1.Text = pedido.Ped_Desc1 == string.Empty ? "descto" : pedido.Ped_Desc1.ToString();
                        txtDescPorc2.Text = pedido.Ped_Desc2 == string.Empty ? "descto" : pedido.Ped_Desc2.ToString();
                        txtCondiciones.Text = pedido.Ped_CondEntrega.ToString();
                        txtConducto.Text = pedido.Ped_Solicito;


                        if (this.ConsultarDatosCliente(pedido.Id_Cte.ToString(), false))
                        {//si la consulta de datos del cliente es correcta
                            txtCliente.Text = pedido.Id_Cte.ToString();
                            txtClienteNombre.Text = pedido.Cte_NomComercial;
                        }

                        if (pedido.Ped_Tipo == 3 || pedido.Ped_Tipo == 4)
                        {
                            txtCalle.Text = pedido.Ped_ConsignadoCalle;
                            txtCalleNumero.Text = pedido.Ped_ConsignadoNo;
                            txtCP.Text = pedido.Ped_ConsignadoCp;
                            txtColonia.Text = pedido.Ped_ConsignadoColonia;
                            txtMunicipio.Text = pedido.Ped_ConsignadoMunicipio;
                            txtEstado.Text = pedido.Ped_ConsignadoEstado;
                        }

                        txtNotas.Text = pedido.Ped_Comentarios;
                        cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(pedido.Id_Ter.ToString());

                        txtTerritorio.Text = pedido.Id_Ter.ToString();
                        cmbTerritorio_SelectedIndexChanged(cmbTerritorio, null);
                        //-------------------------------
                        // Consulta partidas del pedido
                        //-------------------------------
                        dtPedido.Columns.Add("Id_PedDet", typeof(int));
                        dtPedido.Columns.Add("Id_Ter", typeof(int));
                        dtPedido.Columns.Add("Ter_Nombre", typeof(string));
                        dtPedido.Columns.Add("Id_Prd", typeof(int));
                        dtPedido.Columns.Add("Prd_Descripcion", typeof(string));
                        dtPedido.Columns.Add("Prd_Presentacion", typeof(string));
                        dtPedido.Columns.Add("Prd_Unidad", typeof(string));
                        dtPedido.Columns.Add("Prd_Precio", typeof(double));
                        dtPedido.Columns.Add("Disponible", typeof(int));
                        dtPedido.Columns.Add("Prd_Importe", typeof(double));
                        dtPedido.Columns.Add("Id_Rem", typeof(int));
                        new CN_CapPedido().ConsultaPedidoDetDisp(pedido, ref dtPedido, 1, session.Emp_Cnx);

                        DataRowCollection colRow = dtPedido.Rows;
                        int z = 0;
                        foreach (DataRow row in colRow)
                        {

                            int Id_Ter = 0;
                            string Id_TerStr = string.Empty;
                            int Id_Prd = 0;
                            string Prd_Descripcion = string.Empty;
                            string Prd_Presentacion = string.Empty;
                            string Prd_UniNe = string.Empty;
                            int Fac_Cant = 0;
                            int Rem_Cant = 0;
                            int id_Rem = 0;
                            double Fac_Precio = 0;
                            int Id_CteExt = 0;
                            string Id_CteExtStr = string.Empty;

                            if (row["Id_Ter"] != null)
                                Id_Ter = row["Id_Ter"].ToString() == string.Empty ? -1 : Convert.ToInt32(row["Id_Ter"]);

                            Id_TerStr = Convert.IsDBNull(row["Ter_Nombre"]) ? string.Empty : row["Ter_Nombre"].ToString();

                            if (row["Id_Prd"] != null)
                                Id_Prd = row["Id_Prd"].ToString() == string.Empty ? -1 : Convert.ToInt32(row["Id_Prd"]);

                            if (row["Prd_Descripcion"] != null)
                                Prd_Descripcion = row["Prd_Descripcion"].ToString() == string.Empty ? string.Empty : row["Prd_Descripcion"].ToString();

                            if (row["Prd_Presentacion"] != null)
                                Prd_Presentacion = row["Prd_Presentacion"].ToString() == string.Empty ? string.Empty : row["Prd_Presentacion"].ToString();

                            if (row["Prd_Unidad"] != null)
                                Prd_UniNe = row["Prd_Unidad"].ToString() == string.Empty ? string.Empty : row["Prd_Unidad"].ToString();

                            if (row["Prd_Precio"] != null)
                                Fac_Precio = row["Prd_Precio"].ToString() == string.Empty ? 0 : Convert.ToDouble(row["Prd_Precio"]);

                            if (row["Disponible"] != null)
                                Fac_Cant = row["Disponible"].ToString() == string.Empty ? 0 : Convert.ToInt32(row["Disponible"]);

                            if (row["Id_Rem"] != null)
                                id_Rem = row["Id_Rem"].ToString() == string.Empty ? 0 : Convert.ToInt32(row["Id_Rem"]);

                            Id_CteExt = 0;
                            Id_CteExtStr = string.Empty;
                            objdtTabla.Rows.Add(new object[] { null, z, id_Rem, null, Id_CteExt, Id_Ter, Id_Prd, Prd_Descripcion, Prd_Presentacion, Prd_UniNe, Fac_Cant, Rem_Cant, Fac_Precio, Fac_Precio * (Fac_Cant + Rem_Cant), Id_TerStr, Id_CteExtStr, null, session.Id_Emp, session.Id_Cd_Ver });
                            z++;
                        }
                        this.rgFacturaDet.Enabled = true;

                        this.CalcularTotales();
                        //deshabilitar campos que no se deben de editar en una facturación de pedido
                        this.HabilitarCamposPedido(false);

                        if ((txtCliente.Text != string.Empty)
                                && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                                && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                        {
                            this.rgFacturaDet.Enabled = true;
                            this.btnFacturaEspecial.Enabled = true;
                            this.rgFacturaDet.Enabled = true;
                            this.rgAdendaFacturacion.Enabled = true;
                        }
                        else
                        {
                            this.rgFacturaDet.Enabled = false;
                            this.btnFacturaEspecial.Enabled = false;
                            this.rgFacturaDetAde.Enabled = false;
                            this.rgAdendaFacturacion.Enabled = false;
                        }
                    }
                    else
                    {
                        //cerrar ventana
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "No es posible remisionar el pedido ya que no tiene asignación en ninguna partida", "')"));
                        return;
                    }

                    //}
                    //else
                    //{
                    //    //cerrar ventana
                    //    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('No es posible Facturar pedidos de Venta Instalada')"));
                    //    return;
                    //}

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<PedidoDet> GetList(int IdPedido, int IdCd, int IdEmp, string Conexion)
        {
            try
            {
                try
                {
                    List<PedidoDet> List = new List<PedidoDet>();
                    CN_CapPedido cn_CapPedido = new CN_CapPedido();
                    Pedido pedido = new Pedido();
                    pedido.Id_Emp = IdEmp;
                    pedido.Id_Cd = IdCd;
                    pedido.Id_Ped = IdPedido;

                    cn_CapPedido.ConsultaPedidoAsig(pedido, Conexion, ref List);
                    return List;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ObtenerConsecutivoFactElectronica(int id_Cfe)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int consecutivo = 0;

                txtId.Text = string.Empty;
                if (cmbConsFacEle.SelectedValue != "-1")
                {
                    new CN__Comun().ConsultaFactura_ConsecutivoFacElectronica(
                        sesion.Id_Emp
                        , sesion.Id_Cd_Ver
                        , id_Cfe
                        , 1 // 1 = factura, 2 = nota de cargo, 3 = nota de credito
                        , ref consecutivo
                        , sesion.Emp_Cnx);
                    txtId.Text = consecutivo.ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                txtId.Text = string.Empty;
                cmbConsFacEle.SelectedIndex = 0;
                cmbConsFacEle.SelectedValue = "-1";
                cmbConsFacEle.Text = "-- Seleccionar --";
                return false;
                throw ex;
            }
        }
        private void InicializarTablaProductos()
        {
            try
            {
                objdtLista = new DataTable();
                objdtLista.Columns.Add("Id_Fac");
                objdtLista.Columns.Add("Id_FacDet");
                objdtLista.Columns.Add("Id_Rem");
                objdtLista.Columns.Add("Id_Tm");
                objdtLista.Columns.Add("Id_CteExt");
                objdtLista.Columns.Add("Id_Ter");
                objdtLista.Columns.Add("Id_Prd", typeof(System.Int32));
                objdtLista.Columns.Add("Prd_Descripcion");
                objdtLista.Columns.Add("Prd_Presentacion");
                objdtLista.Columns.Add("Prd_UniNe");
                objdtLista.Columns.Add("Fac_Cant");
                objdtLista.Columns.Add("Rem_Cant");
                objdtLista.Columns.Add("Fac_Precio", typeof(System.Double));
                objdtLista.Columns.Add("Fac_Importe", typeof(System.Double));
                objdtLista.Columns.Add("Id_TerStr");
                objdtLista.Columns.Add("Id_CteExtStr");
                objdtLista.Columns.Add("AmortizacionProducto");
                objdtLista.Columns.Add("Id_Emp");
                objdtLista.Columns.Add("Id_Cd");
                objdtLista.Columns.Add("Fac_Asignar");
                objdtLista.Columns.Add("Fac_Devolucion");
                objdtLista.Columns.Add("Prd_UniNs");
                objdtLista.Columns.Add("Remisiones", typeof(System.String));
                objdtLista.Columns.Add("RemisionesXML", typeof(System.String));

                // Edsg 09112015
                objdtLista.Columns.Add("Multiplicador", typeof(System.Double));
                objdtLista.Columns.Add("Precio_Venta", typeof(System.Double));
                objdtLista.Columns.Add("Totales", typeof(System.Double));
                objdtLista.Columns.Add("TotalesAAA", typeof(System.Double));

                objdtLista.Columns.Add("Fac_Precio_Original", typeof(System.Double));

                objdtTabla = objdtLista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InicializarTablaDetallesAdenda()
        {
            try
            {
                ListaProductosFacturaAdenda = new DataTable();
                ListaProductosFacturaAdenda.Columns.Add("Id_Cons_AdeDet");
                ListaProductosFacturaAdenda.Columns.Add("Id_Prd");
                ListaProductosFacturaAdenda.Columns.Add("Prd_Descripcion");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InicializarTablaDetallesAdendaNacional()
        {
            try
            {
                ListaProductosFacturaAdendaNacional = new DataTable();
                ListaProductosFacturaAdendaNacional.Columns.Add("Id_Cons_AdeDet");
                ListaProductosFacturaAdendaNacional.Columns.Add("Id_Prd");
                ListaProductosFacturaAdendaNacional.Columns.Add("Prd_Descripcion");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InicializarTablaProductosRemisiones(ref DataTable remisiones)
        {
            try
            {
                remisiones = new DataTable();
                remisiones.Columns.Add("Id_Fac");
                remisiones.Columns.Add("Id_FacDet");
                remisiones.Columns.Add("Id_Rem");
                remisiones.Columns.Add("Id_Tm");
                remisiones.Columns.Add("Id_CteExt");
                remisiones.Columns.Add("Id_Ter");
                remisiones.Columns.Add("Id_Prd");
                remisiones.Columns.Add("Prd_Descripcion");
                remisiones.Columns.Add("Prd_Presentacion");
                remisiones.Columns.Add("Prd_UniNe");
                remisiones.Columns.Add("Fac_Cant");
                remisiones.Columns.Add("Rem_Cant");
                remisiones.Columns.Add("Fac_Precio", typeof(System.Double));
                remisiones.Columns.Add("Fac_Importe", typeof(System.Double));
                remisiones.Columns.Add("Id_TerStr");
                remisiones.Columns.Add("Id_CteExtStr");
                remisiones.Columns.Add("AmortizacionProducto");
                remisiones.Columns.Add("Id_Emp");
                remisiones.Columns.Add("Id_Cd");
                remisiones.Columns.Add("Fac_Asignar");
                remisiones.Columns.Add("Fac_Devolucion");
                remisiones.Columns.Add("Prd_UniNs");
                remisiones.Columns.Add("Remisiones", typeof(System.String));
                remisiones.Columns.Add("RemisionesXML", typeof(System.String));

                // Edsg 09112015
                remisiones.Columns.Add("Multiplicador", typeof(System.Double));
                remisiones.Columns.Add("Precio_Venta", typeof(System.Double));
                remisiones.Columns.Add("Totales", typeof(System.Double));
                remisiones.Columns.Add("TotalesAAA", typeof(System.Double));

                remisiones.Columns.Add("Fac_Precio_Original", typeof(System.Double));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConsultarDatosAdenda(string ade_descripcion)
        {
            InicializarTablaDetallesAdendaNacional();
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<AdendaDet> listCabT = new List<AdendaDet>();
            List<AdendaDet> listDetT = new List<AdendaDet>();
            List<AdendaDet> listCabR = new List<AdendaDet>();

            new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, ade_descripcion, ref listCabT, ref listDetT, ref listCabR, sesion.Emp_Cnx);

            GridBoundColumn boundColumn1;
            foreach (AdendaDet ad in listDetT)
            {
                boundColumn1 = new GridBoundColumn();
                //rgFacturaDetAde.MasterTableView.Columns.Add(boundColumn1);
                //AKI
                rgFacturaDetAdeNacional.MasterTableView.Columns.Add(boundColumn1);
                boundColumn1.DataField = ad.Id_AdeDet.ToString();
                boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                boundColumn1.HeaderText = ad.Campo;
                boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn1.MaxLength = ad.Longitud;
                ListaProductosFacturaAdendaNacional.Columns.Add(ad.Id_AdeDet.ToString());
            }
            ListDetNacional = listDetT;
            ListCabNacional = listCabT;

            try
            {
                rgFacturaDetAdeNacional.Columns.Remove(rgFacturaDetAde.Columns.FindByUniqueName("EditCommandColumn"));
            }
            catch (Exception)
            {

            }

            GridEditCommandColumn commandColumnAde = new GridEditCommandColumn();
            rgFacturaDetAdeNacional.MasterTableView.Columns.Add(commandColumnAde);

            commandColumnAde.ButtonType = GridButtonColumnType.ImageButton;
            commandColumnAde.UniqueName = "EditCommandColumn";
            commandColumnAde.EditText = "Editar";
            commandColumnAde.CancelText = "Cancelar";
            commandColumnAde.InsertText = "Aceptar";
            commandColumnAde.UpdateText = "Actualizar";
            commandColumnAde.HeaderStyle.Width = Unit.Pixel(60);
            commandColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            commandColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            //CREA BOTON ELIMINAR     
            //CREA BOTON ELIMINAR
            try
            {
                rgFacturaDetAdeNacional.Columns.Remove(rgFacturaDetAde.Columns.FindByUniqueName("DeleteColumn"));
            }
            catch (Exception)
            {

            }
            GridButtonColumn deleteColumnAde = new GridButtonColumn();
            rgFacturaDetAdeNacional.MasterTableView.Columns.Add(deleteColumnAde);

            deleteColumnAde.ConfirmText = "¿Desea quitar este producto de la lista?";
            deleteColumnAde.ConfirmDialogHeight = Unit.Pixel(150);
            deleteColumnAde.ConfirmDialogWidth = Unit.Pixel(350);
            deleteColumnAde.ConfirmDialogType = GridConfirmDialogType.RadWindow;
            deleteColumnAde.ButtonType = GridButtonColumnType.ImageButton;
            deleteColumnAde.CommandName = "Delete";
            deleteColumnAde.Text = "Eliminar";
            deleteColumnAde.UniqueName = "DeleteColumn";
            deleteColumnAde.HeaderStyle.Width = Unit.Pixel(50);
            deleteColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            deleteColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


            double ancho2 = 0;
            foreach (GridColumn gc in rgFacturaDetAdeNacional.Columns)
            {
                if (gc.Display)
                {
                    ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                }
            }
            rgFacturaDetAdeNacional.Width = Unit.Pixel(Convert.ToInt32(ancho2));
            rgFacturaDetAdeNacional.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho2));

        }

        private bool ConsultarDatosCliente(string idCliente, bool modificar)
        {
            try
            {
                bool datosClienteEstablecidos = false;
                bool proveedorNoSeleccionado = false;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (idCliente != string.Empty && idCliente != "-1")
                { //Consultar clientes
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Rik = sesion.Id_Rik;
                    cliente.Id_Cte = Convert.ToInt32(idCliente);
                    try
                    {
                        bool facVI = false;
                        facVI = !string.IsNullOrEmpty(HF_VI.Value) ? Convert.ToBoolean(HF_VI.Value) : false;
                        new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                        CN_PrecioEspecial clsCN_CapPrecioEspecial = new CN_PrecioEspecial();
                        PrecioEspecial PrecioEspecial = new PrecioEspecial();
                        PrecioEspecial.Id_Emp = sesion.Id_Emp;
                        PrecioEspecial.Id_Cd = sesion.Id_Cd_Ver;
                        PrecioEspecial.Id_Cte = cliente.Id_Cte;
                        int valor = 0;
                        int result = 0;
                        clsCN_CapPrecioEspecial.ConsultaProveedorSeleccionado(PrecioEspecial, sesion.Emp_Cnx, ref valor, ref proveedorNoSeleccionado);

                    }
                    catch (Exception ex)
                    {
                        InicializarTablaProductos();
                        AlertaFocus(ex.Message, txtCliente.ClientID);
                        txtCliente.Text = "";
                        txtClienteNombre.Text = "";
                        txtRepresentante.Text = "";
                        CargarComboTerritorios();
                        txtTerritorio.Text = "";
                        txtContacto.Text = string.Empty;
                        txtCalle.Text = string.Empty;
                        txtCalleNumero.Text = string.Empty;
                        txtCalleNumeroInterior.Text = string.Empty;
                        txtCP.Text = string.Empty;
                        txtColonia.Text = string.Empty;
                        txtMunicipio.Text = string.Empty;
                        txtEstado.Text = string.Empty;
                        txtRFC.Text = string.Empty;
                        txtTelefono.Text = string.Empty;
                        chkDesgloce.Checked = false;
                        chkRetencion.Checked = false;
                        txtCondiciones.Text = string.Empty;
                        txtMoneda.Text = string.Empty;
                        txtPorcRetencion.Text = string.Empty;
                        txtUDigitos.Text = "";
                        cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");
                        return false;
                    }

                    if (cliente.Cte_CreditoSuspendido || proveedorNoSeleccionado)
                    {
                        InicializarTablaProductos();

                        txtCliente.Text = "";
                        txtClienteNombre.Text = "";
                        txtRepresentante.Text = "";
                        CargarComboTerritorios();
                        txtTerritorio.Text = "";
                        txtContacto.Text = string.Empty;
                        txtCalle.Text = string.Empty;
                        txtCalleNumero.Text = string.Empty;
                        txtCalleNumeroInterior.Text = string.Empty;
                        txtCP.Text = string.Empty;
                        txtColonia.Text = string.Empty;
                        txtMunicipio.Text = string.Empty;
                        txtEstado.Text = string.Empty;
                        txtRFC.Text = string.Empty;
                        txtTelefono.Text = string.Empty;
                        chkDesgloce.Checked = false;
                        chkRetencion.Checked = false;
                        txtCondiciones.Text = string.Empty;
                        txtMoneda.Text = string.Empty;
                        txtPorcRetencion.Text = string.Empty;
                        txtUDigitos.Text = "";
                        cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");

                        if (!proveedorNoSeleccionado)
                        {
                            if (cliente.Cte_CreditoSuspendido)
                            {
                                AlertaFocus(generar_motivo(cliente.Cte_MotCreditoSuspendido), txtCliente.ClientID);
                            }
                        }
                        else
                        {
                            AlertaFocus("Existen Solicitudes de Precios Especiales para este cliente sin un Proveedor seleccionado ó No se ha registrado el Número de Usuario para el proveedor Georgia Pacific, favor ir al módulo de solicitud de precios especiales  seleccionar un proveedor y registrar numero de usuario según sea el caso para cada solicitud autorizada no vencida", txtCliente.ClientID);
                            // Response.Redirect("PrecioEspecial_Admin.aspx?&Id_Cte=" + cliente.Id_Cte );
                        }
                        return false;
                    }

                    txtUDigitos.Text = cliente.Cte_UDigitos;
                    txtClienteNombre.Text = cliente.Cte_NomComercial;
                    txtCliente.Value = cliente.Id_Cte == null ? 0 : cliente.Id_Cte;


                    //Consultar territorios relacionados con el cliente
                    List<Territorios> listaTerritorios = new List<Territorios>();
                    this.CargarComboTerritorios();
                    CargarFormaPago();
                    if (!modificar)
                    {
                        if (HF_VI.Value != "true" && Session["PedidoFacturacion" + Session.SessionID] != null)
                        {
                            txtCalle.Text = txtCalle.Text == "" ? cliente.Cte_Calle : txtCalle.Text;
                            txtCalleNumero.Text = txtCalleNumero.Text == "" ? cliente.Cte_Numero : txtCalleNumero.Text;
                            txtCalleNumeroInterior.Text = txtCalleNumeroInterior.Text == "" ? cliente.Cte_NumeroInterior : txtCalleNumeroInterior.Text;
                            txtCP.Text = txtCP.Text == "" ? cliente.Cte_Cp : txtCP.Text;
                            txtColonia.Text = txtColonia.Text == "" ? cliente.Cte_Colonia : txtColonia.Text;
                            txtMunicipio.Text = txtMunicipio.Text == "" ? cliente.Cte_Municipio : txtMunicipio.Text;
                            txtEstado.Text = txtEstado.Text == "" ? cliente.Cte_Estado : txtEstado.Text;
                        }
                        if (Page.Request.QueryString["reFactura"] == "0" || Page.Request.QueryString["reFactura"] == null)
                        {
                            InicializarTablaProductos();
                            InicializarTablaDetallesAdenda();
                            InicializarTablaDetallesAdendaNacional();
                        }
                        List<AdendaDet> listCabT = new List<AdendaDet>();
                        List<AdendaDet> listDetT = new List<AdendaDet>();
                        List<AdendaDet> listCabR = new List<AdendaDet>();
                        listCabR = new List<AdendaDet>();
                        ListCabRF = new List<AdendaDet>();
                        ListDet = new List<AdendaDet>();
                        ListDetRF = new List<AdendaDet>();
                        ListDetNacional = new List<AdendaDet>();

                        //if (rgFacturaDetAde.Columns.Count > 17)
                        //    for (int i = rgFacturaDetAde.Columns.Count; i > 17; i--)
                        //        rgFacturaDetAde.Columns.RemoveAt(rgFacturaDetAde.Columns.Count - 1);

                        new CN_CatCliente().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(idCliente), "1,2", ref listCabT, ref listDetT, ref listCabR, sesion.Emp_Cnx);


                        //new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);

                        if (listCabT.Count > 0)
                        {
                            RadTabStrip1.Tabs[2].Visible = true;
                            ListCab = listCabT;
                            rgAdendaFacturacion.Rebind();
                            //rgFacturaDetAde.Rebind();
                        }
                        else
                        {
                            RadTabStrip1.Tabs[2].Visible = false;
                            ListCab = listCabT;
                            rgAdendaFacturacion.Rebind();
                            //rgFacturaDetAde.Rebind();
                        }

                        if (listCabR.Count > 0)
                        {
                            RadTabStrip1.Tabs[3].Visible = true;
                            ListCabRF = listCabR;
                            rgAdendaReFacturacion.Rebind();

                        }
                        else
                        {
                            RadTabStrip1.Tabs[3].Visible = false;
                            rgAdendaReFacturacion.Rebind();
                            //rgFacturaDetAde.Rebind();
                            //rgFacturaDet.Rebind();
                        }

                        GridBoundColumn boundColumn1;

                        foreach (AdendaDet ad in listDetT)
                        {
                            boundColumn1 = new GridBoundColumn();
                            rgFacturaDetAde.MasterTableView.Columns.Add(boundColumn1);
                            //rgFacturaDetAdeNacional.MasterTableView.Columns.Add(boundColumn1);
                            boundColumn1.DataField = ad.Id_AdeDet.ToString();
                            boundColumn1.UniqueName = ad.Id_AdeDet.ToString();
                            boundColumn1.HeaderText = ad.Campo;
                            boundColumn1.HeaderStyle.Width = Unit.Pixel(150);
                            boundColumn1.MaxLength = ad.Longitud;
                            ListaProductosFacturaAdenda.Columns.Add(ad.Id_AdeDet.ToString());
                        }
                        ListDet = listDetT;


                        //CREA BOTON DE EDITAR

                        //CREA BOTON DE EDITAR
                        try
                        {
                            rgFacturaDetAde.Columns.Remove(rgFacturaDetAde.Columns.FindByUniqueName("EditCommandColumn"));
                        }
                        catch (Exception)
                        {

                        }

                        GridEditCommandColumn commandColumnAde = new GridEditCommandColumn();
                        rgFacturaDetAde.MasterTableView.Columns.Add(commandColumnAde);

                        commandColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                        commandColumnAde.UniqueName = "EditCommandColumn";
                        commandColumnAde.EditText = "Editar";
                        commandColumnAde.CancelText = "Cancelar";
                        commandColumnAde.InsertText = "Aceptar";
                        commandColumnAde.UpdateText = "Actualizar";
                        commandColumnAde.HeaderStyle.Width = Unit.Pixel(60);
                        commandColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        commandColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                        //CREA BOTON ELIMINAR     
                        //CREA BOTON ELIMINAR
                        try
                        {
                            rgFacturaDetAde.Columns.Remove(rgFacturaDetAde.Columns.FindByUniqueName("DeleteColumn"));
                        }
                        catch (Exception)
                        {

                        }
                        GridButtonColumn deleteColumnAde = new GridButtonColumn();
                        rgFacturaDetAde.MasterTableView.Columns.Add(deleteColumnAde);

                        deleteColumnAde.ConfirmText = "¿Desea quitar este producto de la lista?";
                        deleteColumnAde.ConfirmDialogHeight = Unit.Pixel(150);
                        deleteColumnAde.ConfirmDialogWidth = Unit.Pixel(350);
                        deleteColumnAde.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                        deleteColumnAde.ButtonType = GridButtonColumnType.ImageButton;
                        deleteColumnAde.CommandName = "Delete";
                        deleteColumnAde.Text = "Eliminar";
                        deleteColumnAde.UniqueName = "DeleteColumn";
                        deleteColumnAde.HeaderStyle.Width = Unit.Pixel(50);
                        deleteColumnAde.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        deleteColumnAde.ItemStyle.HorizontalAlign = HorizontalAlign.Center;


                        double ancho2 = 0;
                        foreach (GridColumn gc in rgFacturaDetAde.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho2 = ancho2 + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDetAde.Width = Unit.Pixel(Convert.ToInt32(ancho2));
                        rgFacturaDetAde.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho2));


                        //CREA BOTON DE EDITAR
                        try
                        {
                            rgFacturaDet.Columns.Remove(rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn"));
                        }
                        catch (Exception)
                        {

                        }


                        GridEditCommandColumn commandColumn = new GridEditCommandColumn();
                        rgFacturaDet.MasterTableView.Columns.Add(commandColumn);
                        commandColumn.ButtonType = GridButtonColumnType.ImageButton;
                        commandColumn.UniqueName = "EditCommandColumn";
                        commandColumn.EditText = "Editar";
                        commandColumn.CancelText = "Cancelar";
                        commandColumn.InsertText = "Aceptar";
                        commandColumn.UpdateText = "Actualizar";
                        commandColumn.HeaderStyle.Width = Unit.Pixel(60);
                        commandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        commandColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                        //CREA BOTON ELIMINAR
                        try
                        {
                            rgFacturaDet.Columns.Remove(rgFacturaDet.Columns.FindByUniqueName("DeleteColumn"));
                        }
                        catch (Exception)
                        {

                        }


                        GridButtonColumn deleteColumn = new GridButtonColumn();
                        rgFacturaDet.MasterTableView.Columns.Add(deleteColumn);
                        deleteColumn.ConfirmText = "¿Desea quitar este producto de la lista?";
                        deleteColumn.ConfirmDialogHeight = Unit.Pixel(150);
                        deleteColumn.ConfirmDialogWidth = Unit.Pixel(350);
                        deleteColumn.ConfirmDialogType = GridConfirmDialogType.RadWindow;
                        deleteColumn.ButtonType = GridButtonColumnType.ImageButton;
                        deleteColumn.CommandName = "Delete";
                        deleteColumn.Text = "Eliminar";
                        deleteColumn.UniqueName = "DeleteColumn";
                        deleteColumn.HeaderStyle.Width = Unit.Pixel(50);
                        deleteColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        deleteColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                        double ancho = 0;
                        foreach (GridColumn gc in rgFacturaDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgFacturaDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgFacturaDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                    //Si el cliente no se le permite facturacion, manda mensaje y no permite continuar
                    if (!cliente.Cte_Facturacion)
                    {
                        this.DisplayMensajeAlerta("Cliente_NoPermiteFacturacion");
                        txtId.Text = string.Empty;
                        txtCliente.Text = string.Empty;
                        txtClienteNombre.Text = string.Empty;
                        cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue("-1");
                        txtRepresentante.Text = string.Empty;
                        txtRepresentanteStr.Text = string.Empty;
                        txtTerritorio.Text = string.Empty;
                        cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue("-1");

                        txtContacto.Text = string.Empty;
                        txtCalle.Text = string.Empty;
                        txtCalleNumero.Text = string.Empty;
                        txtCalleNumeroInterior.Text = string.Empty;
                        txtCP.Text = string.Empty;
                        txtColonia.Text = string.Empty;
                        txtMunicipio.Text = string.Empty;
                        txtEstado.Text = string.Empty;
                        txtRFC.Text = string.Empty;
                        txtTelefono.Text = string.Empty;
                        chkDesgloce.Checked = false;
                        chkRetencion.Checked = false;
                        txtCondiciones.Text = string.Empty;
                        txtMoneda.Text = string.Empty;
                        txtPorcRetencion.Text = string.Empty;
                        cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");
                    }
                    else
                    {//muestra datos del cliente, los de CONSIGNACION, si no existen, muestra los de FACTURACION
                        if (cliente.Id_Cfe != -1)
                        {
                            cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue(cliente.Id_Cfe.ToString());
                            cmbConsFacEle.Text = cmbConsFacEle.FindItemByValue(cliente.Id_Cfe.ToString()).Text;
                            if (!modificar) //Trae el cosecutivo si no es una modificación de documento                            
                            {
                                if (!ObtenerConsecutivoFactElectronica(cliente.Id_Cfe))
                                {
                                    Alerta("No hay consecutivo de facturación electrónica disponible para la serie seleccionada");
                                }
                            }
                        }
                        else
                        { //se limpia solo si es nueva factura, si no le pone el "ConsFacEle" que trae el cliente en la Factura
                            if (this.hiddenId.Value != string.Empty)
                            {
                                txtId.Text = string.Empty;
                                cmbConsFacEle.SelectedIndex = cmbConsFacEle.FindItemIndexByValue("-1");
                            }
                        }
                        txtContacto.Text = cliente.Cte_Contacto;

                        if (string.IsNullOrEmpty(cliente.Cte_Calle) && string.IsNullOrEmpty(cliente.Cte_Numero) &&
                            string.IsNullOrEmpty(cliente.Cte_Colonia) && string.IsNullOrEmpty(cliente.Cte_Municipio) &&
                            string.IsNullOrEmpty(cliente.Cte_Estado) && string.IsNullOrEmpty(cliente.Cte_Cp) &&
                            string.IsNullOrEmpty(cliente.Cte_NumeroInterior))
                        {
                            txtCalle.Text = cliente.Cte_FacCalle;
                            txtCalleNumero.Text = cliente.Cte_FacNumero;
                            txtCalleNumeroInterior.Text = cliente.Cte_FacNumeroInterior;
                            txtCP.Text = cliente.Cte_FacCp;
                            txtColonia.Text = cliente.Cte_FacColonia;
                            txtMunicipio.Text = cliente.Cte_FacMunicipio;
                            txtEstado.Text = cliente.Cte_FacEstado;
                        }
                        else
                        {

                            //if (txtCalle.Text == "" && txtCalleNumero.Text == "" && txtCP.Text == "" && txtColonia.Text == "" && txtMunicipio.Text == "" && txtEstado.Text == "")
                            //{
                            txtCalle.Text = cliente.Cte_Calle;
                            txtCalleNumero.Text = cliente.Cte_Numero;
                            txtCalleNumeroInterior.Text = cliente.Cte_NumeroInterior;
                            txtCP.Text = cliente.Cte_Cp;
                            txtColonia.Text = cliente.Cte_Colonia;
                            txtMunicipio.Text = cliente.Cte_Municipio;
                            txtEstado.Text = cliente.Cte_Estado;

                            //}
                        }

                        txtRFC.Text = cliente.Cte_FacRfc;
                        txtTelefono.Text = cliente.Cte_Telefono;
                        chkDesgloce.Checked = cliente.Cte_DesgIva;
                        chkRetencion.Checked = cliente.Cte_RetIva;
                        txtPorcRetencion.Text = cliente.PorcientoRetencion.ToString();
                        txtCondiciones.Text = cliente.Cte_CondPago.ToString();
                        if ((chkRetencion.Checked == true))
                        {
                            txtPorcRetencion.Visible = true;
                        }
                        else
                        {
                            txtPorcRetencion.Visible = false;
                        }
                        if (cliente.Id_Mon != -1)
                        {
                            txtMoneda.Text = cliente.Id_Mon.ToString();
                            cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue(cliente.Id_Mon.ToString());
                        }
                        else
                        {
                            txtMoneda.Text = string.Empty;
                            cmbMoneda.SelectedIndex = cmbMoneda.FindItemIndexByValue("-1");
                        }

                        if ((idCliente != "-1" && idCliente != string.Empty)
                            && (this.cmbMov.SelectedValue != "-1" && this.cmbMov.SelectedValue != string.Empty)
                            && (this.cmbTerritorio.SelectedValue != "-1" && this.cmbTerritorio.SelectedValue != string.Empty))
                        {
                            this.rgFacturaDet.Enabled = true;
                            this.rgFacturaDetAde.Enabled = true;
                            this.rgAdendaFacturacion.Enabled = true;
                            this.btnFacturaEspecial.Enabled = true;
                        }
                        else
                        {
                            this.rgFacturaDet.Enabled = false;
                            this.rgFacturaDetAde.Enabled = false;
                            this.btnFacturaEspecial.Enabled = false;
                        }

                        ClienteSIAN = cliente.ClienteSIAN;
                        if (cliente.ClienteSIAN != "")
                        {
                            chkFacturarCuentaNacional.Checked = true;
                            chkFacturarCuentaNacional.Enabled = false;

                            // EDSG Llena los datos de la franquicia
                            string idClienteSIAN = cliente.ClienteSIAN;
                            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

                            var diFisc = cn.ConsultaDireccionesFiscales_SP(idClienteSIAN).FirstOrDefault();

                            this.txtClienteNacional.Text = diFisc.FranqConsecionada.ToString();
                            this.txtClienteNacionalNombre.Text = diFisc.ClienteDirFis;
                            this.TxtClienteNacionalCalle.Text = diFisc.DireccionDirFis;
                            this.TxtClienteNacionalNoExterior.Text = diFisc.NumeroDirFis;
                            this.TxtClienteNacionalColonia.Text = diFisc.ColoniaDirFis;
                            this.TxtClienteNacionalCp.Text = diFisc.CPDirFis;
                            this.TxtClienteNacionalMunicipio.Text = diFisc.MunicipioDirFis;
                            this.TxtClienteNacionalEstado.Text = diFisc.EstadoDirFis;

                            this.TxtClienteNacionalRfc.Text = diFisc.RFCDirFis;
                            this.ImgBuscarClienteNacional.Enabled = false;

                            // Busca Cliente Intranet
                            var cIntra = cn.ConsultaIntranetCuentaNacional(diFisc.id_ClienteMatriz.Value, diFisc.Id);
                            ListCuentaNacional = cIntra;
                        }
                        else
                        {

                            chkFacturarCuentaNacional.Checked = false;
                            chkFacturarCuentaNacional.Enabled = true;

                            this.txtClienteNacional.Text = "";
                            this.txtClienteNacionalNombre.Text = "";
                            this.TxtClienteNacionalCalle.Text = "";
                            this.TxtClienteNacionalNoExterior.Text = "";
                            this.TxtClienteNacionalColonia.Text = "";
                            this.TxtClienteNacionalCp.Text = "";
                            this.TxtClienteNacionalMunicipio.Text = "";
                            this.TxtClienteNacionalEstado.Text = "";
                            this.TxtClienteNacionalRfc.Text = "";

                            this.ImgBuscarClienteNacional.Enabled = true;
                        }
                        RadTabStrip1.Tabs[4].Visible = chkFacturarCuentaNacional.Checked;


                        datosClienteEstablecidos = true;
                    }
                }
                return datosClienteEstablecidos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string generar_motivo(string motivo)
        {
            string motivo_texto = "El cliente tiene el crédito suspendido";

            if (motivo != "")
            {
                motivo = "<table>" + motivo + "</table>";
                motivo = motivo.Replace("LIM", "<tr><td>-&nbsp;</td><td>excedió el límite de crédito</tr></td>");
                motivo = motivo.Replace("ACC", "<tr><td>-&nbsp;</td><td>tiene acciones pendientes</tr></td>");
                motivo = motivo.Replace("VEN", "<tr><td>-&nbsp;</td><td>tiene facturas vencidas</tr></td>");
                motivo = motivo.Replace("REC", "<tr><td>-&nbsp;</td><td>faltan días de recepción</tr></td>");
                motivo = motivo.Replace("REV", "<tr><td>-&nbsp;</td><td>faltan días de revisión</tr></td>");
                motivo = motivo.Replace("PAG", "<tr><td>-&nbsp;</td><td>faltan días de pago</tr></td>");
                motivo = motivo.Replace("CON", "<tr><td>-&nbsp;</td><td>faltan condiciones de pago</tr></td>");
                motivo = motivo.Replace("NR", "<tr><td>-&nbsp;</td><td>tiene facturas que no existén en ninguna relación de cobranza</tr></td>");
                motivo = motivo.Replace(",", "");

                motivo = motivo[0].ToString().ToUpper() + motivo.Substring(1, motivo.Length - 1);

                motivo_texto = motivo_texto + ", causas:" + motivo;
            }
            return motivo_texto;
        }


        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirBuscarDireccionEntrega();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void ConsultaInventarioProducto(int Id_Prd)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = null;
                //obtener datos de producto
                CN_CatProducto clsProducto = new CN_CatProducto();
                clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, 0, sesion.Id_Cd_Ver, Id_Prd, 1);

                HD_Prd_UniEmp.Value = producto == null ? string.Empty : producto.Prd_UniEmp.ToString();
                HD_Prd_InvFinal.Value = producto == null ? string.Empty : producto.Prd_InvFinal.ToString();
                HD_Prd_Asignado.Value = producto == null ? string.Empty : producto.Prd_Asignado.ToString();
                HD_Prd_Disponible.Value = producto == null ? string.Empty : (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HabilitarCamposPedido(bool habilitar)
        {
            try
            {
                txtCondiciones.Enabled = habilitar;
                txtMoneda.Enabled = habilitar;
                cmbMoneda.Enabled = habilitar;
                txtConducto.Enabled = habilitar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HabilitaBotonesToolBar(bool nuevo, bool guardar, bool regresar, bool eliminar, bool imprimir, bool correo)
        {
            try
            {
                this.RadToolBar1.Items[6].Visible = nuevo;
                this.RadToolBar1.Items[5].Visible = guardar;
                if (guardar)
                    if (_PermisoGuardar == false & _PermisoModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;
                //Regresar
                this.RadToolBar1.Items[4].Visible = regresar;
                //Eliminar
                this.RadToolBar1.Items[3].Visible = eliminar;
                //Imprimir
                this.RadToolBar1.Items[2].Visible = imprimir;
                //Correo
                this.RadToolBar1.Items[1].Visible = correo;

                if (EsRefactura == true)
                {
                    ChkRefacturatotal.Enabled = true;
                    ChkRefacturaparcial.Enabled = true;
                }
                else
                {
                    ChkRefacturatotal.Enabled = false;
                    ChkRefacturaparcial.Enabled = false;
                }

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
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "CapFactura", "Id_Fac", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string generarGUI_IdAdeDet()
        {
            string guidIdentifier = System.Guid.NewGuid().ToString();
            guidIdentifier = guidIdentifier.Replace("-", string.Empty);
            guidIdentifier.ToUpper();
            return guidIdentifier;
        }
        private void HabilitaControles(bool habilitar)
        {
            try
            {
                txtNotas.Enabled = habilitar;
                rgFacturaDet.Enabled = habilitar;
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = habilitar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EstablecerDataSourceProductosLista(string filtro)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Producto producto = new Producto();

                List<Producto> listaProducto = new List<Producto>();
                if (cmbTerritorio.SelectedValue != string.Empty)
                    new CN_CatProducto().ConsultaListaProductoFacturacion(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(cmbTerritorio.SelectedValue), filtro, ref listaProducto, 1);

                producto = new Producto();
                producto.Id_Prd = -1;
                producto.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Insert(0, producto);

                this.ListaProductos = listaProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EstablecerDataSourceTerritoriosClienteLista()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<Territorios> listaTerritorios = new List<Territorios>();
                //if (cmbCliente.SelectedValue != "-1")
                if (txtCliente.Value.HasValue)
                {
                    new CN_CatCliente().ConsultaTerritoriosDelCliente(Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1), sesion, ref listaTerritorios);
                }
                this.ListaTerritorios = listaTerritorios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarConsFactElectronica()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 1, sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbConsFacEle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTipoMovimientos()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, 1, sesion.Id_Emp, 2, sesion.Emp_Cnx, "spCatMovimiento_Combo", ref cmbMov);
                this.cmbMov.SelectedValue = "51";
                this.cmbMov.Text = (this.cmbMov.FindItemByValue("51")).Text;
                this.txtMov.Text = this.cmbMov.SelectedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarComboTerritorios()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                int cliente = !string.IsNullOrEmpty(txtCliente.Value.ToString()) ? Convert.ToInt32(txtCliente.Value.ToString()) : -1;

                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTerritoriosDelCliente(cliente, sesion, ref listaTerritorios);
                cmbTerritorio.DataTextField = "Descripcion";
                cmbTerritorio.DataValueField = "Id_Ter";
                cmbTerritorio.DataSource = listaTerritorios;
                cmbTerritorio.DataBind();

                if (cmbTerritorio.Items.Count > 1)
                {
                    cmbTerritorio.SelectedIndex = 1;
                    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    txtTerritorio.Text = cmbTerritorio.Items[1].Value;

                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = sesion.Id_Emp;
                    ter.Id_Cd = sesion.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.Items[1].Value);
                    territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                    txtRepresentante.Text = ter.Id_Rik.ToString();
                    txtRepresentanteStr.Text = ter.Rik_Nombre;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarFormaPago()
        {
            try
            {
                int? cliente;
                if (!_PermisoGuardar)
                {
                    cliente = null;
                }
                else
                {
                    cliente = txtCliente.Value.HasValue ? (int)txtCliente.Value.Value : -1;
                }
                cmbFormaPago.Items.Clear();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, cliente, sesion.Emp_Cnx, "spCatClienteFormaPago_Combo", ref cmbFormaPago);
                //this.cmbFormaPago.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                if (cmbFormaPago.Items.Count > 0)
                {
                    cmbFormaPago.SelectedIndex = 0;
                }
                else
                {
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "El cliente no tiene configurado un método de pago, favor de configurarlo en el catalogo de clientes.", "')"));
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboCausaRafactura(int Id_causa)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Id_causa, sesion.Emp_Cnx, "spCatCausaRefactura_combo", ref cmbCausaRef);
                //this.cmbCausaRef.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        private void CargarComboTipoModeda()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatTipoMoneda_Combo", ref cmbMoneda);
                this.cmbMoneda.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rgAdendaFacturacion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendaFacturacion.DataSource = ListCab;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgAdendaReFacturacion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendaReFacturacion.DataSource = ListCabRF;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("ConsFacElectronica_ExcedeRango"))
                Alerta("El consecutivo de facturación electrónica no está en el rango de consecutivos (folio inicial y folio final) de la serie seleccionada");
            else
                if (mensaje.Contains("cmbConsFacEle_ObtenerConsFacElectFallo"))
                    Alerta("Error al momento de obtener el consecutivo de facturación electrónica");
                else
                    if (mensaje.Contains("MovFacturacionPedidoNoValido"))
                        Alerta("El tipo de movimiento no es válido");
                    else
                        if (mensaje.Contains("FacturacionClienteExtNoEnPartida"))
                            Alerta("Favor de capturar el cliente externo en todas las partidas");
                        else
                            if (mensaje.Contains("FacturacionPedidoNoReferenciaFac"))
                                Alerta("El pedido no hace referencia a ninguna factura");
                            else
                                if (mensaje.Contains("rgFacturaDet_delete_error_cancelacion"))
                                    Alerta("Esta orden de compra ya esta cancelada");
                                else
                                    if (mensaje.Contains("CapOrdCompra_print_error"))
                                        Alerta("Error al momento de generar el reporte de impresi&oacute;n de orden de compra");
                                    else
                                        if (mensaje.Contains("CapOrdCompra_delete_ok"))
                                            Alerta("La orden de compra se ha dado de baja (estatus 'B') correctamente");
                                        else
                                            if (mensaje.Contains("CapOrdCompra_delete_error"))
                                                Alerta("Error al momento de dar de baja la orden de compra");
                                            else
                                                if (mensaje.Contains("rgFacturaDet_insert_repetida"))
                                                    Alerta("Este producto ya ha sido capturado");
                                                else
                                                    if (mensaje.Contains("rgFacturaDet_delete_item_error"))
                                                        Alerta("Error al momento de eliminar el producto a la lista de productos de la factura");
                                                    else
                                                        if (mensaje.Contains("rgFacturaDet_insert_error"))
                                                            Alerta("Error al momento de agregar el producto a la lista de productos de la factura");
                                                        else
                                                            if (mensaje.Contains("Cliente_NoPermiteFacturacion"))
                                                                Alerta("CUIDADO. Este cliente se encuentra bloqueado por parte de cobranza, favor de aclarar su situación de créditos");
                                                            else
                                                                if (mensaje.Contains("cmbCliente_IndexChanging_error"))
                                                                    Alerta("Error al consultar los datos del cliente");
                                                                else
                                                                    if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                                                        Alerta("Error al consultar los datos de producto");
                                                                    else
                                                                        if (mensaje.Contains("rgFacturaDet_ItemDataBound"))
                                                                            Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                                                        else
                                                                            if (mensaje.Contains("rgFacturaDetAde_ItemDataBound"))
                                                                                Alerta("Error al momento de preparar un registro para edici&oacute;n");
                                                                            else
                                                                                if (mensaje.Contains("CapOrdCompraDetalle_consulta_error"))
                                                                                    Alerta("Error al consultar el detalle de la orden de compra");
                                                                                else
                                                                                    if (mensaje.Contains("CapOrdCompraDetalle_insert_error"))
                                                                                        Alerta("Error al guardar el detalle de la orden de compra");
                                                                                    else
                                                                                        if (mensaje.Contains("rgFacturaDet_NeedDataSource"))
                                                                                            Alerta("Error al cargar el grid de detalle de factura");
                                                                                        else
                                                                                            if (mensaje.Contains("rgFacturaDetAde_NeedDataSource"))
                                                                                                Alerta("Error al cargar el grid de detalle de factura");
                                                                                            else
                                                                                                if (mensaje.Contains("rgFacturaDet_ItemCommand"))
                                                                                                    Alerta("Error al ejecutar un evento (rgOrdCompra_ItemCommand) al cargar el grid de detalle de factura");
                                                                                                else
                                                                                                    if (mensaje.Contains("rgFacturaDet_Actualizar_ok"))
                                                                                                        Alerta("El producto de la orden de compra fue actualizado correctamente");
                                                                                                    else
                                                                                                        if (mensaje.Contains("rgFacturaDet_Actualizar_error"))
                                                                                                            Alerta("Error al actualizar el producto de la orden de compra");
                                                                                                        else
                                                                                                            if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                                                                                                                Alerta("Error al cambiar de centro de distribución");
                                                                                                            else
                                                                                                                if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                                                                                    Alerta("Error al cambiar de página");
                                                                                                                else
                                                                                                                    if (mensaje.Contains("CapFactura_LlenarForm_error"))
                                                                                                                        Alerta("Error al momento de consultar los datos de la factura");
                                                                                                                    else
                                                                                                                        if (mensaje.Contains("PermisoGuardarNo"))
                                                                                                                            Alerta("No tiene permisos para grabar");
                                                                                                                        else
                                                                                                                            if (mensaje.Contains("PermisoModificarNo"))
                                                                                                                                Alerta("No tiene permisos para actualizar");
                                                                                                                            else
                                                                                                                                if (mensaje.Contains("CapOrdCompraDetalle_delete_error"))
                                                                                                                                    Alerta("Error al momento de eliminar el detalle de la orden de compra");
                                                                                                                                else
                                                                                                                                    if (mensaje.Contains("cmbProducto_IndexChanging_error"))
                                                                                                                                        Alerta("Error al consultar los datos del producto");
                                                                                                                                    else
                                                                                                                                        if (mensaje.Contains("CapFactura_Id_Es_NoEncontrado"))
                                                                                                                                            Alerta("No se pudo actualizar los datos de la factura. No se encontró el movimiento de entrada para esta factura hecha a partir de aparatos inproductivos. (tipo de movimiento 16)");
                                                                                                                                        else
                                                                                                                                            if (mensaje.Contains("CapFactura_insert_ok"))
                                                                                                                                                Alerta("Los datos se guardaron correctamente");
                                                                                                                                            else
                                                                                                                                                if (mensaje.Contains("CapFactura_insert_error"))
                                                                                                                                                    Alerta(string.Concat("No se pudo guardar la factura. ", mensaje.Replace("'", "\"")));
                                                                                                                                                else
                                                                                                                                                    if (mensaje.Contains("CapFactura_update_ok"))
                                                                                                                                                        Alerta("Los datos se modificaron correctamente");
                                                                                                                                                    else
                                                                                                                                                        if (mensaje.Contains("CapFactura_update_error"))
                                                                                                                                                            Alerta(string.Concat("No se pudo actualizar los datos de la factura. ", mensaje.Replace("'", "\"")));
                                                                                                                                                        else
                                                                                                                                                            if (mensaje.Contains("Page_Load_error"))
                                                                                                                                                                Alerta("Error al cargar la página");
                                                                                                                                                            else
                                                                                                                                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        private void CerrarVentana()
        {
            try
            {
                Session["ListaRemisionesFactura" + Session.SessionID] = null;
                Session["ListaDevolucionRemisionesFactura" + Session.SessionID] = null;
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region "Métodos para obtrener desde objetos los valores para los controles durante la inserción/actualización de un Grid editable"

        protected string ObtenerDescripcion(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_Descripcion; } else { return string.Empty; }
        }

        protected string ObtenerDescripcionTerritorio(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Id_TerStr; } else { return string.Empty; }
        }

        protected string ObtenerDescripcionCliente(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Id_CteExtStr; } else { return string.Empty; }
        }

        protected string ObtenerPresentacion(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_Presentacion; } else { return string.Empty; }
        }

        protected string ObtenerUnidades(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_UniNe; } else { return string.Empty; }
        }

        protected int ObtenerInvFinal(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_InvFinal; } else { return 0; }
        }

        #endregion
        private void HabilitarColumnas(bool habilitar)
        {
            GridCommandItem cmdItem = (GridCommandItem)rgFacturaDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            cmdItem.FindControl("AddNewRecordButton").Parent.Visible = !habilitar;

            GridCommandItem cmdItem2 = (GridCommandItem)rgFacturaDetAde.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            cmdItem2.FindControl("AddNewRecordButton").Parent.Visible = !habilitar;

            try
            {
                rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn").Display = !habilitar;
                rgFacturaDetAde.Columns.FindByUniqueName("EditCommandColumn").Display = !habilitar;
            }
            catch
            { }
            try
            {
                rgFacturaDet.Columns.FindByUniqueName("DeleteColumn").Display = !habilitar;
                rgFacturaDetAde.Columns.FindByUniqueName("DeleteColumn").Display = !habilitar;
            }
            catch
            {
            }


            if (Page.Request.QueryString["reFactura"] != null)
            {
                cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                cmdItem2.FindControl("AddNewRecordButton").Parent.Visible = false;
                rgAdendaReFacturacion.Rebind();
                try
                {

                    rgFacturaDet.Columns.FindByUniqueName("EditCommandColumn").Display = true;
                    rgFacturaDet.Columns.FindByUniqueName("DeleteColumn").Display = true;
                    rgFacturaDetAde.Columns.FindByUniqueName("EditCommandColumn").Display = true;
                    rgFacturaDetAde.Columns.FindByUniqueName("DeleteColumn").Display = true;
                }
                catch
                { }
            }
        }
        #region "Métodos para manejar la lista dinámica de Productos de la factura"
        //protected void ListaProductosFactura_AgregarProducto(FacturaDet factura_prod)
        //{
        //List<FacturaDet> lista = this.ListaProductosFactura;


        ////buscar producto de factura en la lista para ver si ya existe
        //for (int i = 0; i < lista.Count; i++)
        //{
        //    FacturaDet factura = lista[i];
        //    if (factura.Id_Prd == factura_prod.Id_Prd)//si el producto es el mismo
        //    {
        //        if (factura.Id_Ter == factura_prod.Id_Ter)//y si el territorio es el mismo
        //        {
        //            throw new Exception("rgFacturaDet_insert_repetida");
        //        }
        //    }
        //}
        //lista.Add(factura_prod);

        //}

        //protected void ListaProductosFactura_ModificarProducto(FacturaDet factura_prod)
        //{
        //    //List<FacturaDet> lista = this.ListaProductosFactura;

        //    ////buscar producto de factura en la lista
        //    //for (int i = 0; i < lista.Count; i++)
        //    //{
        //    //    FacturaDet factura = lista[i];
        //    //    if (factura.Id_Prd == factura_prod.Id_Prd)
        //    //    {
        //    //        lista[i] = factura_prod;
        //    //        break;
        //    //    }
        //    //}
        //    //this.ListaProductosFactura = lista;
        //    //this.CalcularTotales();


        //    try
        //    {
        //        int Id_Fac = factura_prod.Id_Fac;
        //        int Id_FacDet = factura_prod.Id_FacDet;
        //        int? Id_Rem = factura_prod.Id_Rem;
        //        int? Id_CteExt = factura_prod.Id_CteExt;
        //        int Id_Ter = factura_prod.Id_Ter;
        //        int? Id_Prd = factura_prod.Id_Prd;
        //        string Prd_Descripcion = factura_prod.Producto.Prd_Descripcion;
        //        string Prd_Presentacion = factura_prod.Producto.Prd_Presentacion;
        //        string Prd_UniNe = factura_prod.Producto.Prd_UniNe;
        //        int Fac_Cant = factura_prod.Fac_Cant;
        //        float? Rem_Cant = factura_prod.Rem_Cant;
        //        float Fac_Precio = factura_prod.Fac_Precio;
        //        float Fac_Importe = factura_prod.Fac_Importe;

        //        DataRow[] Ar_dr;

        //        Ar_dr = objdtTabla.Select("Id_Prd='" + Id_Prd + "'");
        //        if (Ar_dr.Length > 0)
        //        {
        //            Ar_dr[0].BeginEdit();
        //            Ar_dr[0]["Id_Fac"] = Id_Fac;
        //            Ar_dr[0]["Id_FacDet"] = Id_FacDet;
        //            Ar_dr[0]["Id_CteExt"] = Id_CteExt;
        //            Ar_dr[0]["Id_Ter"] = Id_Ter;
        //            Ar_dr[0]["Id_Prd"] = Id_Prd;
        //            Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
        //            Ar_dr[0]["Prd_Presentacion"] = Prd_Presentacion;
        //            Ar_dr[0]["Prd_UniNe"] = Prd_UniNe;
        //            Ar_dr[0]["Fac_Cant"] = Fac_Cant;
        //            Ar_dr[0]["Rem_Cant"] = Rem_Cant;
        //            Ar_dr[0]["Fac_Precio"] = Fac_Precio;
        //            Ar_dr[0]["Fac_Importe"] = Fac_Importe;
        //            Ar_dr[0].AcceptChanges();
        //        }

        //        CalcularTotales();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        protected void ListaProductosFactura_EliminarProducto(int Id_Prd, int Id_Ter, int id_Fac)
        {
            try
            {
                DataRow[] Ar_dr;

                Ar_dr = objdtTabla.Select("Id_Prd='" + Id_Prd + "' and Id_Ter='" + Id_Ter + "'");
                if (Ar_dr.Length > 0)
                {
                    if (this.hiddenId.Value != string.Empty)
                    {
                        int verificador = 0;
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        CN_CapFactura factura = new CN_CapFactura();
                        factura.revisionEspeciales(sesion, Id_Prd, Id_Ter, id_Fac, ref verificador);
                        if (verificador != 0)
                        {
                            Ar_dr[0].Delete();
                            objdtTabla.AcceptChanges();
                        }
                        else
                        {
                            Alerta("no se puede eliminar el producto " + Id_Prd + " por que tiene relación con facturas especiales");
                        }
                    }
                    else
                    {
                        Ar_dr[0].Delete();
                        objdtTabla.AcceptChanges();
                    }
                }
                this.CalcularTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ListaProductosFacturaAdenda_EliminarProducto(string Id_Cons_AdeDet)
        {
            try
            {
                DataRow[] Ar_dr;

                //                Ar_dr = ListaProductosFacturaAdenda.Select("Id_Prd='" + Id_Prd +  "'");
                Ar_dr = ListaProductosFacturaAdenda.Select("Id_Cons_AdeDet='" + Id_Cons_AdeDet + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    ListaProductosFacturaAdenda.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ListaProductosFacturaAdendaNacional_EliminarProducto(string Id_Cons_AdeDet)
        {
            try
            {
                DataRow[] Ar_dr;

                //                Ar_dr = ListaProductosFacturaAdenda.Select("Id_Prd='" + Id_Prd +  "'");
                Ar_dr = ListaProductosFacturaAdendaNacional.Select("Id_Cons_AdeDet='" + Id_Cons_AdeDet + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    ListaProductosFacturaAdendaNacional.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CalcularTotales(int Id_Tm = 0)
        {
            try
            {
                double importeTotal = 0;
                double porcDescuento1 = txtDescuento1.Text != string.Empty ? Convert.ToDouble(txtDescuento1.Text) : 0;
                double porcDescuento2 = txtDescuento2.Text != string.Empty ? Convert.ToDouble(txtDescuento2.Text) : 0;

                for (int i = 0; i < objdtTabla.Rows.Count; i++)
                {

                    if (Id_Tm == 92)
                        importeTotal += Convert.ToDouble(objdtTabla.Rows[i]["Totales"]);
                    else
                        importeTotal += Convert.ToDouble(objdtTabla.Rows[i]["Fac_Importe"]);
                }
                txtImporte.Text = importeTotal.ToString();
                importeTotal = porcDescuento1 > 0 ? (importeTotal - (importeTotal * (porcDescuento1 / 100))) : importeTotal;
                importeTotal = porcDescuento2 > 0 ? (importeTotal - (importeTotal * (porcDescuento2 / 100))) : importeTotal;
                txtSubTotal.Text = importeTotal.ToString();
                txtIVA.Text = HD_IVAfacturacion.Value.Trim() != string.Empty ? (importeTotal * (Convert.ToDouble(HD_IVAfacturacion.Value.Trim()) / 100)).ToString() : "0";
                txtTotal.Text = (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtIVA.Text)).ToString();
                Session["fTotalFactura" + Session.SessionID] = txtTotal.Text;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularTotalesEntradaRemision(ref EntradaSalida entrada)
        {
            try
            {
                double importeTotal = 0;
                double porcDescuento1 = txtDescuento1.Text != string.Empty ? Convert.ToInt32(txtDescuento1.Text) : 0;
                double porcDescuento2 = txtDescuento2.Text != string.Empty ? Convert.ToInt32(txtDescuento2.Text) : 0;

                foreach (EntradaSalidaDetalle entradaDet in entrada.ListaDetalle)
                {
                    importeTotal += (entradaDet.Es_Cantidad * entradaDet.Es_Costo);
                }
                importeTotal = porcDescuento1 > 0 ? (importeTotal - (importeTotal * (porcDescuento1 / 100))) : importeTotal;
                importeTotal = porcDescuento2 > 0 ? (importeTotal - (importeTotal * (porcDescuento2 / 100))) : importeTotal;
                entrada.Es_SubTotal = importeTotal;
                entrada.Es_Iva = HD_IVAfacturacion.Value.Trim() != string.Empty ? (importeTotal * (Convert.ToDouble(HD_IVAfacturacion.Value.Trim()) / 100)) : 0;
                entrada.Es_Total = entrada.Es_SubTotal + entrada.Es_Iva;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HabilitarControlesTotales(bool habilitar)
        {
            try
            {
                txtImporte.Enabled = habilitar;
                txtSubTotal.Enabled = habilitar;
                txtIVA.Enabled = habilitar;
                txtTotal.Enabled = habilitar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
        #region AdendaNacional
        protected void rgFacturaDetAdeNacional_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgFacturaDetAdeNacional.DataSource = this.ListaProductosFacturaAdendaNacional;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDetAde_NeedDataSource"));
            }
        }
        protected void rgAdendaFacturacionNacional_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgAdendaFacturacionNacional.DataSource = ListCabNacional;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgFacturaDetAdeNacional_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.Item is GridDataItem)
                {
                    GridDataItem editItem = (GridDataItem)e.Item;
                    TextBox txt;
                    try
                    {
                        if (ListDet != null)
                        {
                            foreach (AdendaDet det in ListDet)
                            {
                                if (editItem[det.Id_AdeDet.ToString()] != null)
                                {
                                    if (editItem[det.Id_AdeDet.ToString()].Controls.Count > 0)
                                    {
                                        txt = editItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                        txt.Attributes.Add("onkeypress", "return SoloAlfanumerico(this)");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        if (ListDetRF != null)
                        {
                            foreach (AdendaDet det in ListDetRF)
                            {
                                if (editItem[det.Id_AdeDet.ToString()] != null)
                                {
                                    if (editItem[det.Id_AdeDet.ToString()].Controls.Count > 0)
                                    {
                                        txt = editItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                        txt.Attributes.Add("onkeypress", "return SoloAlfanumerico(this)");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaDetAdeNacional_InsertCommand(object sender, GridCommandEventArgs e)
        {

            GridEditableItem insertedItem = (GridEditableItem)e.Item;
            try
            {

                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string Id_Cons_AdeDet = "";
                int Id_Prd = 0;
                string Prd_Descripcion = "";

                Id_Cons_AdeDet = generarGUI_IdAdeDet();
                Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_PrdAdeNacional") as RadNumericTextBox).Value);
                Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProductoAdeNacional") as RadComboBox).Text;

                if (Id_Prd == 0)
                {
                    Alerta("Elija un Producto Para poder Guardar");
                    e.Canceled = true;
                    return;
                }


                ArrayList al = new ArrayList();
                al.Add(Id_Cons_AdeDet);
                al.Add(Id_Prd);
                al.Add(Prd_Descripcion);

                bool falta_adenda = false;
                TextBox Txtadenda = new TextBox();
                string valor_adenda = "";
                ArrayList ok = new ArrayList();

                string adenda_faltante = "";
                foreach (AdendaDet det in ListDetNacional)
                {
                    if (!ok.Contains(det.Id_AdeDet.ToString()))
                    {
                        ok.Add(det.Id_AdeDet.ToString());
                        Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                        valor_adenda = Txtadenda.Text.Replace("'", "");

                        if (valor_adenda == "" && det.Requerido)
                        {
                            adenda_faltante = det.Campo;
                            falta_adenda = true;
                            break;
                        }
                        else
                        {
                            al.Add(valor_adenda);

                        }
                    }
                }

                if (ListDetRF != null)
                {
                    foreach (AdendaDet det in ListDetRF)
                    {
                        if (!ok.Contains(det.Id_AdeDet.ToString()))
                        {
                            ok.Add(det.Id_AdeDet.ToString());
                            Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                            valor_adenda = Txtadenda.Text;

                            if (valor_adenda == "" && det.Requerido)
                            {
                                adenda_faltante = det.Campo;
                                falta_adenda = true;
                                break;
                            }
                            else
                            {
                                al.Add(valor_adenda);
                            }
                        }
                    }
                }

                if (falta_adenda)
                {
                    AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                    e.Canceled = true;
                }
                else
                {
                    ListaProductosFacturaAdendaNacional.Rows.Add(al.ToArray());
                }
            }
            catch (Exception ex)
            {

                e.Canceled = true;
                Alerta(ex.Message);
                return;

            }
        }

        protected void rgFacturaDetAdeNacional_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //Llenar combo de Productos de Adenda                                     
                    RadComboBox comboproducto = (RadComboBox)editItem.FindControl("cmbProductoAdeNacional");

                    comboproducto.DataValueField = "Id_Prd";
                    comboproducto.DataTextField = "Prd_Descripcion";
                    comboproducto.DataSource = objdtTabla;
                    comboproducto.DataBind();
                    comboproducto.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        ((RadComboBox)editItem.FindControl("cmbProductoAdeNacional")).Enabled = true;
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto



                        //((RadComboBox)editItem.FindControl("cmbProductoAde")).Enabled = false;
                        //((RadNumericTextBox)editItem.FindControl("txtId_PrdAde")).Enabled = false;
                        comboproducto.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDetAdeNacional_ItemDataBound"));
            }
        }

        protected void rgFacturaDetAdeNacional_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                string Id_Cons_AdeDet = "";
                int Id_Prd = 0;
                string Prd_Descripcion = "";


                Id_Cons_AdeDet = (insertedItem.FindControl("txtId_Cons_AdeDetNacional") as RadTextBox).Text;
                Id_Prd = Convert.ToInt32((insertedItem.FindControl("txtId_PrdAdeNacional") as RadNumericTextBox).Value);
                Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProductoAdeNacional") as RadComboBox).Text;


                DataRow[] Ar_dr;
                //Ar_dr = ListaProductosFacturaAdenda.Select("Id_Prd='" + Id_Prd + "'");
                Ar_dr = ListaProductosFacturaAdendaNacional.Select("Id_Cons_AdeDet='" + Id_Cons_AdeDet + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Cons_AdeDet"] = Id_Cons_AdeDet;
                    Ar_dr[0]["Id_Prd"] = Id_Prd;
                    Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;


                    bool falta_adenda = false;
                    string valor_adenda = "";
                    TextBox Txtadenda = new TextBox();
                    //ADENDA FACTURACION
                    ArrayList ok = new ArrayList();
                    string adenda_faltante = "";

                    foreach (AdendaDet det in ListDetNacional)
                    {
                        if (!ok.Contains(det.Id_AdeDet.ToString()))
                        {
                            ok.Add(det.Id_AdeDet.ToString());
                            Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                            valor_adenda = Txtadenda.Text.Replace("'", "");
                            bool addenda_Requerida = det.Requerido;//listDetT.Where(AdendaDet => AdendaDet.Id_AdeDet == det.Id_AdeDet).ToList()[0].Requerido;
                            if (valor_adenda == "" && addenda_Requerida)
                            {
                                adenda_faltante = det.Campo;
                                falta_adenda = true;
                                (insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox).Focus();
                                break;
                            }
                            else
                                Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                        }
                    }

                    //ADENDA REFACTURACION
                    if (ListDetRF != null && !falta_adenda)
                    {
                        foreach (AdendaDet det in ListDetRF)
                        {
                            if (!ok.Contains(det.Id_AdeDet.ToString()))
                            {
                                ok.Add(det.Id_AdeDet.ToString());
                                Txtadenda = insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox;
                                bool addenda_Requerida = det.Requerido;//listDetT.Where(AdendaDet => AdendaDet.Id_AdeDet == det.Id_AdeDet).ToList()[0].Requerido;
                                valor_adenda = Txtadenda.Text;
                                if (valor_adenda == "" && addenda_Requerida)
                                {
                                    adenda_faltante = det.Campo;
                                    falta_adenda = true;
                                    (insertedItem[det.Id_AdeDet.ToString()].Controls[0] as TextBox).Focus();
                                    break;
                                }
                                else
                                    Ar_dr[0][det.Id_AdeDet.ToString()] = valor_adenda;
                            }
                        }
                    }
                    if (falta_adenda)
                    {
                        AlertaFocus("El campo <b>" + adenda_faltante + "</b> de la addenda es requerido", Txtadenda.ClientID);
                        e.Canceled = true;
                    }
                    else
                        Ar_dr[0].AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                Alerta(ex.Message.Replace("'", ""));
                return;

            }
        }

        protected void rgFacturaDetAdeNacional_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();

                if (rgFacturaDetAdeNacional.EditItems.Count > 0)
                {
                    Alerta("Ya está editando un registro");
                    e.Canceled = true;
                }
                else
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string Id_Cons_AdeDet = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Cons_AdeDet"]);
                    //int Id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);                    
                    //actualizar producto de orden de compra a la lista
                    this.ListaProductosFacturaAdendaNacional_EliminarProducto(Id_Cons_AdeDet);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgFacturaDetAdeNacional_ItemCommand(object source, GridCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "InitInsert":
                    if (objdtTabla.Rows.Count == 0)
                    {
                        Alerta("Debe agregar al menos un producto para llenar la Adenda");
                        e.Canceled = true;
                    }
                    else
                        if (rgFacturaDetAdeNacional.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        else
                        {
                            if (e.CommandName == RadGrid.InitInsertCommandName)
                            {
                                //Add new" button clicked
                                e.Canceled = true;

                                //Prepare an IDictionary with the predefined values
                                System.Collections.Specialized.ListDictionary newValues = new
                                System.Collections.Specialized.ListDictionary();
                                newValues["Id_Cons_AdeDet"] = generarGUI_IdAdeDet();
                                newValues["Id_Prd"] = string.Empty;
                                newValues["Prd_Descripcion"] = string.Empty;
                                //Insert the item and rebind
                                e.Item.OwnerTableView.InsertItem(newValues);
                            }
                        }
                    break;
            }

        }
        #endregion
        #region Manejo de Controles
        public void DeshabilitaControles(WebControl pContenedor)
        {
            List<Type> vCommonTypes = new List<Type>() { typeof(RadNumericTextBox),
                                                     typeof(RadTextBox),
                                                     typeof(RadDatePicker),
                                                     typeof(RadComboBox),
                                                     typeof(CheckBox),
                                                     typeof(RadGrid),
                                                     typeof(ImageButton),
                                                     typeof(Button)
            };

            ObtieneControles(pContenedor).Where(ctrl => vCommonTypes.Contains(ctrl.GetType()))
                                         .ToList()
                                         .ForEach(ctrl => ((WebControl)ctrl).Enabled = false);
        }

        public IEnumerable<Control> ObtieneControles(Control pContenedor)
        {
            foreach (Control ctrl in pContenedor.Controls)
            {
                yield return ctrl;

                foreach (Control childCtrl in ObtieneControles(ctrl))
                {
                    yield return childCtrl;
                }
            }
        }
        #endregion
        #region ErrorManager
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
        private void AlertaFocus2(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus2('" + mensaje + "','" + rtb + "');");
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
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

        #region Facturas Especiales
        #region Eventos
        protected void cmbProducto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //verificar que el producto este el segmento
                if (false)
                {

                }
                else
                {
                    RadComboBox combo = (RadComboBox)sender;
                    //obtiene la tabla contenedora de los controles de edición de registro del Grid
                    Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                    RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion_Esp");
                    RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion_Esp");
                    RadTextBox txtPrd_UniNe = (RadTextBox)tabla.FindControl("txtPrd_UniNe_Esp");
                    RadNumericTextBox txtRem_Cantidad = (RadNumericTextBox)tabla.FindControl("txtRem_Cantidad_Esp");
                    RadNumericTextBox txtRem_Precio = (RadNumericTextBox)tabla.FindControl("txtRem_Precio_Esp");


                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    RadComboBoxItem item = ((RadComboBox)sender).FindItemByValue(e.Value);
                    int id_Cd_Prod = sesion.Id_Cd_Ver;

                    Producto producto = new Producto();
                    if (e.Value != string.Empty && e.Value != "-1")
                    {
                        //obtener datos de producto
                        CN_CatProducto clsProducto = new CN_CatProducto();
                        clsProducto.ConsultaProducto(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value), 1);
                    }
                    txtRem_Precio.Text = "";
                    txtPrd_Descripcion.Text = producto == null ? string.Empty : producto.Prd_Descripcion;
                    txtPrd_Presentacion.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                    txtPrd_UniNe.Text = producto == null ? string.Empty : producto.Prd_UniNe;


                    //este evento es porque se elige producto, por lo que 
                    //se habilita el campo de cantidad porque ya eligió producto y se estableció las unidades de empaque
                    txtRem_Cantidad.Enabled = true;
                    txtRem_Cantidad.Text = string.Empty;

                    txtPrd_Descripcion.Focus();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "cmbProducto_IndexChanging_error"));
            }
        }
        protected void rgFacturaEspecialDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgFacturaEspecialDet.DataSource = this.ListaProductosFacturaEspecial;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaEspecialDet_NeedDataSource"));
            }
        }
        protected void rgFacturaEspecialDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;


                    RadComboBox cmbcmbProdServ = (RadComboBox)editItem.FindControl("cmbProdServ");
                    cmbcmbProdServ.SelectedValue = ((Label)editItem.FindControl("lblProdServ")).Text;


                    RadComboBox cmbcmbSATUnidad = (RadComboBox)editItem.FindControl("cmbSATUnidad");
                    cmbcmbSATUnidad.SelectedValue = ((Label)editItem.FindControl("Label12")).Text;

                    //txt_cmbProdServ.Text = ((Label)editItem.FindControl("lblProdServ")).Text;
                    //txt_cmbProdServ.Text = txt_cmbProdServ.FindItemByValue(((Label)editItem.FindControl("lblProdServ")).ClientID.ToString()).Text;


                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtRem_Cantidad_Esp");
                    string lblRem_Cantidad = ((Label)editItem.FindControl("lblVal_txtRem_Cantidad_Esp")).ClientID.ToString();
                    string txtRem_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto_Esp")).ClientID.ToString();
                    string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd_Esp")).ClientID.ToString();
                    string lblVal_txtPrd_Descripcion = ((Label)editItem.FindControl("lblVal_txtPrd_Descripcion_Esp")).ClientID.ToString();
                    string txtPrd_Descripcion = ((RadTextBox)editItem.FindControl("txtPrd_Descripcion_Esp")).ClientID.ToString();
                    string lblVal_txtPrd_Presentacion = ((Label)editItem.FindControl("lblVal_txtPrd_Presentacion")).ClientID.ToString();
                    string txtPrd_Presentacion = ((RadTextBox)editItem.FindControl("txtPrd_Presentacion_Esp")).ClientID.ToString();
                    string lblVal_txtPrd_UniNe = ((Label)editItem.FindControl("lblVal_txtPrd_UniNe_Esp")).ClientID.ToString();
                    string txtPrd_UniNe = ((RadTextBox)editItem.FindControl("txtPrd_UniNe_Esp")).ClientID.ToString();
                    string lblVal_txtFac_Precio = ((Label)editItem.FindControl("lblVal_txtRem_Precio_Esp")).ClientID.ToString();
                    string txtRem_Precio = ((RadNumericTextBox)editItem.FindControl("txtRem_Precio_Esp")).ClientID.ToString();




                    //Llenar combo de productos
                    RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    CargarProductosEspeciales(comboProductoItem);

                    string jsControles = string.Concat(
                        "lblRem_CantidadClientId_Esp='", lblRem_Cantidad, "';"
                        , "txtRem_CantidadClientId_Esp='", txtRem_Cantidad, "';"
                        , "lbl_cmbProductoClientId_Esp='", lbl_cmbProducto, "';"
                        , "txtId_PrdClientId_Esp='", txtId_Prd, "';"
                        , "lblVal_txtPrd_DescripcionClientId_Esp='", lblVal_txtPrd_Descripcion, "';"
                        , "txtPrd_DescripcionClientId_Esp='", txtPrd_Descripcion, "';"
                        , "lblVal_txtPrd_PresentacionClientId_Esp='", lblVal_txtPrd_Presentacion, "';"
                        , "txtPrd_PresentacionClientId_Esp='", txtPrd_Presentacion, "';"
                        , "lblVal_txtPrd_UniNeClientId_Esp='", lblVal_txtPrd_UniNe, "';"
                        , "txtPrd_UniNeClientId_Esp='", txtPrd_UniNe, "';"
                        , "lblVal_txtRem_PrecioClientId_Esp='", lblVal_txtFac_Precio, "';"
                        , "txtRem_PrecioClientId_Esp='", txtRem_Precio, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //cuando la edición se usa para inserción, se habilita el combo de producto
                        ((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = true;
                        //es registro nuevo y se inhabilita el campo de cantidad (se habilita hasta ke elija un producto del combo)
                        Ctrl_txtOrd_Cantidad.Enabled = false;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEditEspecial(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd_Esp")).Focus();
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {

                        //cuando la edición se usa para actualización, se deshabilita el campo de texto y el combo de producto
                        ((RadNumericTextBox)editItem.FindControl("txtId_Prd_Esp")).Enabled = false;
                        ((RadComboBox)editItem.FindControl("cmbProducto")).Enabled = false;
                        //es registro de edición, se habilita el campo de cantidad porque ya eligió producto
                        Ctrl_txtOrd_Cantidad.Enabled = true;

                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEditEspecial(\"actualizar\");");
                        updatebtn.Attributes.Add("onclick", jsControles);

                        //cuando es actualización se selecciona el producto del combo
                        comboProductoItem.SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();
                        Ctrl_txtOrd_Cantidad.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaEspecialDet_ItemDataBound"));
            }
        }
        protected void rgFacturaEspecialDet_InsertCommand(object sender, GridCommandEventArgs e)
        {
            FacturaDet FacturaDet = new FacturaDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                FacturaDet.Id_Emp = sesion.Id_Emp;
                FacturaDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Id_Fac = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaDet.Id_FacDet = 0;
                FacturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                FacturaDet.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                FacturaDet.Fac_CantE = (insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad_Esp") as RadNumericTextBox).Value.HasValue ? float.Parse((insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad_Esp") as RadNumericTextBox).Value.Value.ToString()) : 0;
                FacturaDet.Clp_Release = (insertedItem["Clp_Release"].FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                double precio = (insertedItem["Fac_Precio"].FindControl("txtRem_Precio_Esp") as RadNumericTextBox).Value.HasValue ? (double)(insertedItem["Fac_Precio"].FindControl("txtRem_Precio_Esp") as RadNumericTextBox).Value.Value : 0;
                (insertedItem["Fac_Importe"].FindControl("lblRem_ImporteEdit") as Label).Text = FacturaDet.Fac_ImporteE.ToString();
                FacturaDet.Fac_Precio = precio;
                FacturaDet.Fac_ClaveProdServ = Convert.ToString((insertedItem["ProdServ"].FindControl("cmbProdServ") as RadComboBox).SelectedValue);
                FacturaDet.Fac_ClaveUnidad = Convert.ToString((insertedItem["SATUnidad"].FindControl("cmbSATUnidad") as RadComboBox).SelectedValue);


                //datos del producto de la orden de compra
                FacturaDet.Producto = new Producto();
                FacturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                FacturaDet.Producto.Id_Emp = sesion.Id_Emp;
                FacturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text; ///<------
                FacturaDet.Producto.Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).Text;
                FacturaDet.Producto.Prd_DescripcionEspecial = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion_Esp") as RadTextBox).Text;
                FacturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion_Esp") as RadTextBox).Text;
                FacturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe_Esp") as RadTextBox).Text;

                //agregar producto de orden de compra a la lista
                this.ListaProductosFacturaEspecial_AgregarProducto(FacturaDet);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgFacturaEspecial_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgFacturaEspecialDet_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            FacturaDet FacturaDet = new FacturaDet();
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                FacturaDet.Id_Emp = sesion.Id_Emp;
                FacturaDet.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Id_Fac = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                FacturaDet.Id_FacDet = 0;
                FacturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//cliente de datos generales de la factura
                FacturaDet.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);


                FacturaDet.Fac_CantE = (insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad_Esp") as RadNumericTextBox).Value.HasValue ? float.Parse((insertedItem["Fac_CantE"].FindControl("txtRem_Cantidad_Esp") as RadNumericTextBox).Value.Value.ToString()) : 0;
                FacturaDet.Clp_Release = (insertedItem["Clp_Release"].FindControl("txtClp_ReleaseEdit") as RadTextBox).Text;
                double precioPartida = (insertedItem["Fac_Precio"].FindControl("txtRem_Precio_Esp") as RadNumericTextBox).Value.HasValue ? Convert.ToDouble((insertedItem["Fac_Precio"].FindControl("txtRem_Precio_Esp") as RadNumericTextBox).Value.Value) : 0;


                (insertedItem["Fac_Importe"].FindControl("lblRem_ImporteEdit") as Label).Text = FacturaDet.Fac_ImporteE.ToString();
                FacturaDet.Fac_Precio = precioPartida;
                FacturaDet.Fac_ClaveProdServ = Convert.ToString((insertedItem["ProdServ"].FindControl("cmbProdServ") as RadComboBox).SelectedValue);
                FacturaDet.Fac_ClaveUnidad = Convert.ToString((insertedItem["SATUnidad"].FindControl("cmbSATUnidad") as RadComboBox).SelectedValue);

                //datos del producto de la orden de compra
                FacturaDet.Producto = new Producto();
                FacturaDet.Producto.Id_Prd = Convert.ToInt32((insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).SelectedValue);
                FacturaDet.Producto.Id_Emp = sesion.Id_Emp;
                FacturaDet.Producto.Id_Cd = sesion.Id_Cd_Ver;
                FacturaDet.Producto.Id_PrdEsp = (insertedItem.FindControl("txtId_PrdEsp") as RadTextBox).Text;
                FacturaDet.Producto.Prd_Descripcion = (insertedItem["Id_Prd"].FindControl("cmbProducto") as RadComboBox).Text;
                FacturaDet.Producto.Prd_DescripcionEspecial = (insertedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion_Esp") as RadTextBox).Text;
                FacturaDet.Producto.Prd_Presentacion = (insertedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion_Esp") as RadTextBox).Text;
                FacturaDet.Producto.Prd_UniNe = (insertedItem["Prd_UniNe"].FindControl("txtPrd_UniNe_Esp") as RadTextBox).Text;

                //agregar producto de orden de compra a la lista
                this.ListaProductosFacturaEspecial_ModificarProducto(FacturaDet, e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgFacturaEspecial_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgFacturaEspecialDet_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id_Prd = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                string descripcion = ((Label)item["Prd_Descripcion"].FindControl("lblId_PrdStr")).Text;
                //actualizar producto de orden de compra a la lista
                ListaProductosFacturaEspecial_EliminarProducto(e.Item.ItemIndex);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgOrdCompra_delete_item_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }
        protected void rgFacturaEspecialDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgFacturaEspecialDet.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgFacturaEspecialDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgFacturaEspecialDet.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        #endregion
        #region Funciones
        protected string ObtenerIdEspecial(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Id_PrdEsp; } else { return string.Empty; }
        }
        protected string ObtenerDescripcionEspecial(object oc)
        {
            if (oc is FacturaDet) { return ((FacturaDet)oc).Producto.Prd_DescripcionEspecial; } else { return string.Empty; }
        }
        private List<FacturaDet> listaProdFactEsp { get; set; }
        private List<FacturaDet> ListaProductosFacturaEspecial
        {
            get { if (ViewState["objdtTablaEspecial"] != null) { return (List<FacturaDet>)ViewState["objdtTablaEspecial"]; } else { return listaProdFactEsp; } }
            set { ViewState["objdtTablaEspecial"] = value; }
        }
        private void CargarProductosEspeciales(RadComboBox cmb)
        {
            try
            {
                List<Producto> listaProducto = new List<Producto>();
                Producto prd = new Producto();
                prd.Id_Prd = -1;
                prd.Prd_Descripcion = "-- Seleccionar --";
                listaProducto.Add(prd);

                foreach (DataRow item in objdtTabla.Rows)
                {
                    prd = new Producto();
                    prd.Id_Prd = Convert.ToInt32(item["Id_Prd"]);
                    prd.Prd_Descripcion = item["Prd_Descripcion"].ToString();
                    listaProducto.Add(prd);
                }
                cmb.DataSource = listaProducto;
                cmb.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaProductosFacturaEspecial_AgregarProducto(FacturaDet remision_prod)
        {
            try
            {
                List<FacturaDet> lista = this.ListaProductosFacturaEspecial;

                lista.Add(remision_prod);
                this.ListaProductosFacturaEspecial = lista;
                this.CalcularTotalesEspecial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaProductosFacturaEspecial_ModificarProducto(FacturaDet remision_prod, int index)
        {
            try
            {
                List<FacturaDet> lista = this.ListaProductosFacturaEspecial;
                //buscar producto de factura en la lista           
                FacturaDet remision = lista[index];
                if (remision.Id_Prd == remision_prod.Id_Prd)
                    lista[index] = remision_prod;

                this.ListaProductosFacturaEspecial = lista;
                this.CalcularTotalesEspecial();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ListaProductosFacturaEspecial_EliminarProducto(int index)
        {
            try
            {
                List<FacturaDet> lista = this.ListaProductosFacturaEspecial;
                lista.RemoveAt(index);

                this.ListaProductosFacturaEspecial = lista;
                this.CalcularTotalesEspecial();
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
                // CalcularTotalesEspecial();
                if (!Convert.ToBoolean(Request.QueryString["Modificar"]))
                    RadToolBar1.Items[1].Visible = false;

                RadToolBar1.Items[6].Visible = false;

                if (ReFactura == "1")
                {
                    RadTabStrip1.Tabs[6].Visible = true;
                    RadTabStrip1.Tabs[0].Visible = true;
                    RadTabStrip1.Tabs[0].Selected = true;
                }
                else
                {
                    // Se muestra la pestaña y grid de facturas especiales 
                    RadTabStrip1.Tabs[6].Visible = true;
                    RadTabStrip1.Tabs[6].Selected = true;
                    rpvFacturasEspeciales.Selected = true;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                this.HD_Cliente.Value = this.txtCliente.Text;
                this.HD_Moneda.Value = this.txtMoneda.Text;
                this.HD_ImporteTotal.Value = Convert.ToDouble(txtTotal.Text).ToString();
                this.HD_IVARemision.Value = HD_IVAfacturacion.Value.ToString();


                if (Session["FacEspecialGuardada" + Session.SessionID] != null)
                    if (Session["FacEspecialGuardada" + Session.SessionID].ToString() != "1")
                        ListaProductosFacturaEspecial = new List<FacturaDet>();

                if (ListaProductosFacturaEspecial != null && ListaProductosFacturaEspecial.Count > 0)
                {
                    if (ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial == null)
                        ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial = "";

                    ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial = (ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)).Length > 0 ? (ListaProductosFacturaEspecial[0].Producto.Prd_DescripcionEspecial.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))[0] : "";
                }
                else
                {
                    ListaProductosFacturaEspecial = new List<FacturaDet>();

                    string folio = hiddenId.Value;
                    Factura remision = new Factura();

                    remision.Id_Emp = sesion.Id_Emp;
                    remision.Id_Cd = sesion.Id_Cd_Ver;
                    remision.Id_Fac = !string.IsNullOrEmpty(folio) ? Convert.ToInt32(folio) : 0;

                    List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
                    if (!string.IsNullOrEmpty(folio))
                    {
                        new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                            , sesion.Emp_Cnx
                            , sesion.Id_Emp
                            , sesion.Id_Cd_Ver
                            , Convert.ToInt32(folio)
                            , Convert.ToInt32(this.HD_Cliente.Value));
                    }

                    if (listaProdFacturaEspecialFinal.Count != 0)
                        ListaProductosFacturaEspecial = listaProdFacturaEspecialFinal;
                    else
                    {
                        //// -------------------------------------------------------------------------------------------
                        //// obtener claves de productos de Factura original
                        //// -------------------------------------------------------------------------------------------
                        string clavesProducto = string.Empty;
                        List<FacturaDet> listProdFact = new List<FacturaDet>();

                        foreach (DataRow dr in objdtTabla.Rows)
                        {
                            //if (!clavesProducto.Contains(string.Format("{0}|", dr["Id_Prd"])))
                            clavesProducto = clavesProducto + dr["Id_Prd"] + "|";

                            FacturaDet fDet = new FacturaDet();
                            fDet.Id_Prd = Convert.ToInt32(dr["Id_Prd"]);
                            fDet.Id_Ter = Convert.ToInt32(dr["Id_Ter"]);
                            fDet.Fac_CantE = Convert.ToInt32(dr["Fac_Cant"]);
                            fDet.Fac_Precio = Convert.ToDouble(dr["Fac_Precio"]);
                            listProdFact.Add(fDet);
                        }
                        // -------------------------------------------------------------------------------------------
                        // consulta productos de facturacion especial en el catalogo de Cliente-Producto en base a los productos de la Remision original
                        // -------------------------------------------------------------------------------------------
                        List<FacturaDet> listaProdFacturaEspecial = new List<FacturaDet>();

                        if (!string.IsNullOrEmpty(this.HD_Cliente.Value))
                            new CN_CatClienteProd().ConsultaClienteProd_FacturaEspecial(ref listaProdFacturaEspecial
                                , sesion.Emp_Cnx
                                , sesion.Id_Emp
                                , sesion.Id_Cd_Ver
                                , Convert.ToInt32(this.HD_Cliente.Value)
                                , clavesProducto);

                        IEnumerable<FacturaDet> grupedDT = from dt in listProdFact
                                                           group dt by dt.Id_Prd into grpDT
                                                           select new FacturaDet
                                                           {
                                                               Id_Prd = grpDT.Key,
                                                               Fac_CantE = grpDT.Sum(a => a.Fac_CantE),
                                                               Fac_Precio = grpDT.Max(a => a.Fac_Precio)
                                                           };

                        var joinedList = from dt in grupedDT
                                         join lt in listaProdFacturaEspecial
                                            on dt.Id_Prd equals lt.Id_Prd into jProd
                                         from subQry in jProd.DefaultIfEmpty()
                                         select new
                                         {
                                             lProds = dt,
                                             lProdsE = subQry
                                         };

                        // -------------------------------------------------------------------------------------------
                        // Crear partidas adicionales de remision si es que la descripción viene con separadores "|"
                        // -------------------------------------------------------------------------------------------
                        foreach (var jItem in joinedList)
                        {
                            FacturaDet cFacturaDet = jItem.lProdsE;
                            cFacturaDet.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);//actualiza el cliente de la partida que es el cliente de la fact. original
                            string[] descripcion = cFacturaDet.Producto.Prd_DescripcionEspecial.Split(new char[] { '|' });

                            for (int j = 0; j < descripcion.Length; j++)
                            {
                                FacturaDet remisionCopia = new FacturaDet();
                                remisionCopia.Producto = new Producto();
                                remisionCopia.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);
                                remisionCopia.Id_Prd = cFacturaDet.Id_Prd;
                                remisionCopia.Producto.Id_PrdEsp = cFacturaDet.Producto.Id_PrdEsp.ToString();
                                remisionCopia.Producto.Id_Prd = cFacturaDet.Producto.Id_Prd;
                                remisionCopia.Producto.Prd_Descripcion = cFacturaDet.Producto.Prd_Descripcion;
                                remisionCopia.Producto.Prd_Presentacion = cFacturaDet.Producto.Prd_Presentacion;
                                remisionCopia.Producto.Prd_UniNe = cFacturaDet.Producto.Prd_UniNe;
                                remisionCopia.Producto.Prd_InvFinal = cFacturaDet.Producto.Prd_InvFinal;
                                remisionCopia.Producto.Prd_DescripcionEspecial = descripcion[j];


                                remisionCopia.Fac_ClaveProdServ = cFacturaDet.Producto.Prd_ClaveProdServ;
                                remisionCopia.Fac_ClaveUnidad = cFacturaDet.Producto.Prd_ClaveUnidad;

                                try
                                {
                                    remisionCopia.Fac_Precio = jItem.lProds.Fac_Precio;
                                }
                                catch (Exception)
                                {
                                }
                                remisionCopia.Id_Emp = sesion.Id_Emp;
                                remisionCopia.Id_Cd = sesion.Id_Cd_Ver;
                                if (j == 0)
                                {
                                    try
                                    {
                                        remisionCopia.Fac_CantE = jItem.lProds.Fac_CantE;
                                    }
                                    catch
                                    {
                                        remisionCopia.Fac_CantE = 0;
                                    }
                                }
                                else
                                {
                                    remisionCopia.Fac_CantE = 0;
                                }
                                ListaProductosFacturaEspecial.Add(remisionCopia);
                            }
                        }
                    }
                }

                //Edsg 28092015
                //if (cmbMov.SelectedValue == "91")
                //{
                //    ListaProductosFacturaEspecial.Clear();

                //    //FacturaDet rem = new FacturaDet();
                //    //rem.Producto = new Producto();
                //    //rem.Id_CteExt = Convert.ToInt32(this.HD_Cliente.Value);
                //    //rem.Id_Prd = 0;
                //    //rem.Producto.Id_PrdEsp = "";
                //    //rem.Producto.Id_Prd = 0;
                //    //rem.Producto.Prd_Descripcion = "";
                //    //rem.Producto.Prd_Presentacion = "";
                //    //rem.Producto.Prd_UniNe = "";
                //    //rem.Producto.Prd_InvFinal = 0;
                //    //rem.Producto.Prd_DescripcionEspecial = "Capture el Concepto para la factura de Garantía";

                //    //ListaProductosFacturaEspecial.Add(rem);
                //}

                double ancho = 0;
                foreach (GridColumn gc in rgFacturaEspecialDet.Columns)
                {
                    if (gc.Display)
                    {
                        ancho = ancho + gc.HeaderStyle.Width.Value;
                    }
                }
                rgFacturaEspecialDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                rgFacturaEspecialDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                this.CalcularTotalesEspecial();
                rgFacturaEspecialDet.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EliminarFacturaEspecial()
        {

            RadTabStrip1.Tabs[6].Visible = false;
            this.ListaProductosFacturaEspecial = null;
            rpvFacturasEspeciales.Selected = false;
            RadPageViewDetalles.Selected = true;

        }
        private bool Guardar()
        {
            //Guardar_FacEspecial = false;
            try
            {
                RadToolBar1.Enabled = false;
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = 0;

                if (!ChkRefacturaparcial.Checked && !ChkRefacturatotal.Checked && EsRefactura == true)
                {
                    Alerta("Favor de seleccionar si sera Refacturación Parcial o total, antes de continuar");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return false;
                }

                if (ChkRefacturaparcial.Checked && txtCausaRef.Text == "" && EsRefactura == true)
                {
                    Alerta("Favor de seleccionar una causa de Refacturación antes de seguir");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return false;
                }

                if (rgFacturaDet.Items.Count == 0)
                {
                    Alerta("Capture al menos un producto para guardar la factura");
                    RadToolBar1.Enabled = true;
                    return false;
                }


                if (string.IsNullOrEmpty(cmbTerritorio.SelectedValue) || cmbTerritorio.SelectedValue == "-1")
                {
                    Alerta("Seleccione un territorio válido");
                    RadToolBar1.Enabled = true;
                    return false;
                }

                //Guardar los datos de los productos de factura especial
                //en catálogo de Cliente-Producto
                List<FacturaDet> ListaPrdRemEspecial = new List<FacturaDet>();
                if (this.ListaProductosFacturaEspecial != null)
                {
                    for (int i = 0; i < this.ListaProductosFacturaEspecial.Count; i++)
                        ListaPrdRemEspecial.Add((FacturaDet)this.ListaProductosFacturaEspecial[i]);


                    //Datos del centro de distribución
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);


                    if (Session["fTotalFactura" + Session.SessionID] != null)
                    {
                        if (txtTotal.Text.Length > 0)
                        {
                            decimal fTotalFacturaOriginal = decimal.Round(decimal.Parse(Session["fTotalFactura" + Session.SessionID].ToString()), 2);

                            if (((fTotalFacturaOriginal + (decimal)cd.Cd_MargenDiferenciaDocs) < decimal.Parse(txtTotal.Text)) || ((fTotalFacturaOriginal - (decimal)cd.Cd_MargenDiferenciaDocs) > decimal.Parse(txtTotal.Text)))
                            {
                                Alerta("El monto Total de la Factura especial tiene una diferencia considerable con respecto a la Factura original.");
                                Guardar_FacEspecial = false;
                                RadToolBar1.Enabled = true;
                            }
                        }
                    }

                    new CN_CatClienteProd().ModificarClienteProdFacturaEspecial(ListaPrdRemEspecial, session.Emp_Cnx, ref verificador);

                    //SET variable de encabezado de factura especial
                    FacturaEspecial facturaEsp = new FacturaEspecial();
                    facturaEsp.Id_Emp = session.Id_Emp;
                    facturaEsp.Id_Cd = session.Id_Cd_Ver;
                    facturaEsp.FacEsp_Importe = Convert.ToDouble(txtImporte.Text);
                    facturaEsp.FacEsp_SubTotal = Convert.ToDouble(txtSubTotal.Text);
                    facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(txtIVA.Text);
                    facturaEsp.FacEsp_Total = Convert.ToDouble(txtTotal.Text);
                    this.FacturaEspecial = facturaEsp;

                    Session["FacEspecialGuardada" + Session.SessionID] = "1";
                    Guardar_FacEspecial = true;
                    string mensaje = "Los datos se guardaron correctamente";
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow_FacturaEspecial('", mensaje, "')")); //cerrar ventana radWindow de factura especial
                }
                RadToolBar1.Enabled = true;
            }
            catch (Exception ex)
            {
                Guardar_FacEspecial = false;
                throw ex;
            }
            return Guardar_FacEspecial;
        }

        private void CalcularTotalesEspecial()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<FacturaDet> lista = this.ListaProductosFacturaEspecial;
            double importeTotalEsp = 0;
            //if ( Convert.ToDouble((Request.QueryString["Descuento1"]).ToString())  )

            double porcDescuento1 = (Request.QueryString["Descuento1"]) == null ? 0 : Convert.ToDouble((Request.QueryString["Descuento1"]).ToString());
            double porcDescuento2 = (Request.QueryString["Descuento2"]) == null ? 0 : Convert.ToDouble((Request.QueryString["Descuento2"]).ToString());

            for (int i = 0; i < lista.Count; i++)
            {
                FacturaDet rem = lista[i];
                importeTotalEsp += rem.Fac_ImporteE;
            }
            CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
            double iva = 0;
            cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

            txtImporte_Esp.Text = importeTotalEsp.ToString();
            importeTotalEsp = porcDescuento1 > 0 ? (importeTotalEsp - (importeTotalEsp * (porcDescuento1 / 100))) : importeTotalEsp;
            importeTotalEsp = porcDescuento2 > 0 ? (importeTotalEsp - (importeTotalEsp * (porcDescuento2 / 100))) : importeTotalEsp;
            txtSubTotal_Esp.Text = importeTotalEsp.ToString();
            txtIVA_Esp.Text = HD_IVARemision.Value.Trim() != string.Empty ? (importeTotalEsp * iva / 100).ToString() : "0";
            txtTotal_Esp.Text = (Convert.ToSingle(txtSubTotal_Esp.Text) + Convert.ToSingle(txtIVA_Esp.Text)).ToString();
            //Session["FacEspecialGuardada" + Session.SessionID] = 2;
        }

        public void Prorrateo(DataTable listaFacturaDet, Double factorGarantia)
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];


            int unidadesGarantia = Convert.ToInt32(this.txtUnidadesGarantia.Value);

            Double cantCobrar = factorGarantia * unidadesGarantia;
            Double costoAAA = Convert.ToDouble(listaFacturaDet.Compute("Sum(TotalesAAA)", ""));

            Double multiplicador = cantCobrar / costoAAA;
            foreach (DataRow row in listaFacturaDet.Rows)
            {
                row["Multiplicador"] = multiplicador;
                row["Precio_Venta"] = Convert.ToDouble(row["Fac_Precio"]) * multiplicador;
                row["Totales"] = Convert.ToDouble(row["Precio_Venta"]) * Convert.ToDouble(row["Fac_Cant"]);
            }

            CalcularTotales();
        }

        #endregion
        #endregion Facturas Especiales

        protected void cmbTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void EvaluarAsignacionTipoMovimientoParaGarantia()
        {
            if (Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null)
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapAcys cnAcys = new CN_CapAcys();
                List<Remision> listaRemisiones = (List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID];
                AcysDatosGarantia datosGar = cnAcys.DatosGarantia_Consulta_Remision(session.Emp_Cnx, listaRemisiones[0].Id_Rem, session.Id_Emp, session.Id_Cd, int.Parse(txtCliente.Text), int.Parse(txtTerritorio.Text)).FirstOrDefault();

                Remision remision = new Remision();
                remision.Id_Emp = session.Id_Emp;
                remision.Id_Cd = session.Id_Cd_Ver;
                remision.Id_Tm = listaRemisiones[0].Id_Tm;
                remision.Id_Cte = listaRemisiones[0].Id_Cte;
                remision.Id_Ter = listaRemisiones[0].Id_Ter;
                remision.IdTg = Convert.ToInt32(Session["Id_TG_Fac" + Session.SessionID]);

                if (remision.Id_Tm == 92)
                {
                    this.txtMov.Text = "91";
                    this.cmbMov.SelectedValue = "91";
                    this.cmbMov.Text = (this.cmbMov.FindItemByValue("91")).Text;

                    // this.Inicializar();
                }
            }
        }

        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (Page.Request.QueryString["reFactura"] != null)
            {
                if (Request.QueryString["reFactura"].ToString() == "1")
                {
                    if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "FacEspeciales")
                    {
                        if (ChkRefacturaparcial.Checked && txtCausaRef.Text == "" && EsRefactura == true)
                        {
                            Alerta("Favor de seleccionar una causa de Refacturación antes de seguir");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                        if (!ChkRefacturaparcial.Checked && !ChkRefacturatotal.Checked && EsRefactura == true)
                        {
                            Alerta("Favor de seleccionar si sera Refacturación Parcial o total, antes de continuar");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                    }
                    if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "AdendaRef")
                    {
                        if (ChkRefacturaparcial.Checked && txtCausaRef.Text == "" && EsRefactura == true)
                        {
                            Alerta("Favor de seleccionar una causa de Refacturación antes de seguir");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                        if (!ChkRefacturaparcial.Checked && !ChkRefacturatotal.Checked && EsRefactura == true)
                        {
                            Alerta("Favor de seleccionar si sera Refacturación Parcial o total, antes de continuar");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                    }

                    if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "AdendaCuentaNac")
                    {
                        if (ChkRefacturaparcial.Checked && txtCausaRef.Text == "")
                        {
                            Alerta("Favor de seleccionar una causa de Refacturación antes de seguir");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                        if (!ChkRefacturaparcial.Checked && !ChkRefacturatotal.Checked && EsRefactura == true)
                        {
                            Alerta("Favor de seleccionar si sera Refacturación Parcial o total, antes de continuar");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                    }

                    if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "Detalles")
                    {
                        foreach (GridDataItem Item in rgFacturaDet.Items)
                        {
                            CambioTerritorio = Item.GetDataKeyValue("Id_Ter").ToString();
                        }

                        if ((ChkRefacturaparcial.Checked && txtCausaRef.Text == "") || (ChkRefacturatotal.Checked && txtCausaRef.Text == ""))
                        {
                            Alerta("Favor de seleccionar una causa de Refacturación antes de seguir");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                        if (!ChkRefacturaparcial.Checked && !ChkRefacturatotal.Checked && EsRefactura == true)
                        {
                            Alerta("Favor de seleccionar si sera Refacturación Parcial o total, antes de continuar");
                            RadTabStrip1.Tabs[0].Selected = true;
                            RadPageViewDGenerales.Selected = true;
                            return;
                        }
                        else
                        {
                            if (CambioTerritorio != txtTerritorio.Text)
                            {
                                DataTable table = new DataTable();
                                foreach (DataRow row in objdtTabla.Rows)
                                {
                                    row["Id_Ter"] = txtTerritorio.Text;
                                }

                                this.EstablecerDataSourceProductosLista(string.Empty);
                                //Llenar Grid
                                rgFacturaDet.DataSource = this.objdtTabla;
                                rgFacturaDet.Rebind();
                            }
                            else
                            {
                                rgFacturaDet.Rebind();
                            }
                        }
                    }
                }
            }
            if (this.txtMov.Value == 91)
            {

                //if (txtUnidadesGarantia.Text == "")
                //{
                //    RadTabStrip1.Tabs[0].Selected = true;
                //    RadTabStrip1.Tabs[0].Visible = true;
                //    return;
                //}


                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapAcys cnAcys = new CN_CapAcys();

                int id_rem = 0;

                if (Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null)
                    id_rem = ((List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID])[0].Id_Rem;

                AcysDatosGarantia datosGar = cnAcys.DatosGarantia_Consulta_Remision(session.Emp_Cnx, id_rem, session.Id_Emp, session.Id_Cd, int.Parse(txtCliente.Text), int.Parse(txtTerritorio.Text)).FirstOrDefault();
                if (datosGar == null) return;

                ConsultarDatosDevolucionRemisiones();
                RadTabStrip1.Tabs[6].Visible = true;
                //Inicializar();

                //rgFacturaEspecialDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                //rgFacturaEspecialDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                ListaProductosFacturaEspecial = new List<FacturaDet>();

                Int32 id_tg = Convert.ToInt32(Session["Id_TG_Fac" + Session.SessionID]);
                String mensProdEspecial = "";

                switch (id_tg)
                {
                    case 1: mensProdEspecial = "Costo garantía por kilo lavado"; break;
                    case 2: mensProdEspecial = "Costo garantía por comensal"; break;
                    case 3: mensProdEspecial = "Costo garantía por habitación"; break;
                    case 4: mensProdEspecial = "Costo garantía por iguala"; break;
                }


                FacturaDet rem = new FacturaDet();
                rem.Producto = new Producto();
                rem.Id_CteExt = 0;

                if (Session["ListaDevolucionRemisionesFactura" + Session.SessionID] != null)
                {
                    rem.Id_Prd = ((List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID])[0].Id_Prd;
                    rem.Id_Emp = ((List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID])[0].Id_Emp;
                    rem.Id_Cd = ((List<Remision>)Session["ListaDevolucionRemisionesFactura" + Session.SessionID])[0].Id_Cd;
                }

                rem.Producto.Prd_UniNe = "1 UD"; // unidades
                rem.Producto.Prd_Presentacion = ""; // Presentacion
                rem.Fac_CantE = Convert.ToSingle(this.txtUnidadesGarantia.Value); // Cantidad Especial
                rem.Fac_Precio = datosGar.FactorGarantia; // Precio

                rem.Producto.Prd_DescripcionEspecial = mensProdEspecial;
                rem.Producto.Id_PrdEsp = "NA";

                //ListaProductosFacturaEspecial.Add(rem);
                ListaProductosFacturaEspecial.Add(rem);

                this.HD_Cliente.Value = this.txtCliente.Text;
                rgFacturaEspecialDet.Rebind();
                CalcularTotalesEspecial();

            }

        }

    }

}
