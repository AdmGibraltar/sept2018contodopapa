using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Collections;
using CapaDatos;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;


namespace SIANWEB
{
    public partial class CapRemisiones : System.Web.UI.Page
    {
        #region Variables
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public bool HabilitarGuardar
        {
            get
            {
                //DEVUELVE SI SE PUEDE O NO GUARDAR
                return RadToolBar1.Items[1].Visible;
            }
        }
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
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
        //private static bool Tm_ReqSpo;
        private bool Tm_ReqSpo
        {
            set { Session["Tm_ReqSpoREM" + Session.SessionID] = value; }
            get { return (bool)Session["Tm_ReqSpoREM" + Session.SessionID]; }
        }
        public string FechaEnable
        {
            get
            {
                return Convert.ToInt32(dpFecha.Enabled).ToString();// txtFecha.Enabled;
            }
        }
        public DataTable dt_detalles
        {
            get
            {
                return Session["dt_DetallesRem" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["dt_DetallesRem" + Session.SessionID] = value;
            }
        }
        public DataTable dt_cuenta
        {
            get
            {
                return Session["dt_cuentaRem" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["dt_cuentaRem" + Session.SessionID] = value;
            }
        }
        public DataTable dt_cuentaOriginal
        {
            get
            {
                return Session["dt_cuentaOriginalRem" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["dt_cuentaOriginalRem" + Session.SessionID] = value;
            }
        }
        public DataTable dt_cuentaPedido
        {
            get
            {
                return Session["dt_cuentaPedidoRem" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["dt_cuentaPedidoRem" + Session.SessionID] = value;
            }
        }

        //static int id_detalle = 0;
        private int id_detalle
        {
            set { Session["id_detalleREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["id_detalleREM" + Session.SessionID]; return st == null ? 0 : (int)st; }
        }
        //static int Id_RemDet_A = -1; //id de la partida que se va actualizar
        private int Id_RemDet_A
        {
            set { Session["Id_RemDet_AREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["Id_RemDet_AREM" + Session.SessionID]; return st == null ? -1 : (int)st; }
        }
        //static int cantidad_A = 0; //cantidad de la partida que se va actualizar
        private int cantidad_A
        {
            set { Session["cantidad_AREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["cantidad_AREM" + Session.SessionID]; return st == null ? 0 : (int)st; }
        }
        //static double costo_A = -1; //costo de la partida que se va actualizar
        private double costo_A
        {
            set { Session["costo_AREM" + Session.SessionID] = value; }
            get { double? st = (double?)Session["costo_AREM" + Session.SessionID]; return st == null ? -1 : (double)st; }
        }
        //static int Id_Prd_A;
        private int Id_Prd_A
        {
            set { Session["Id_Prd_AREM" + Session.SessionID] = value; }
            get { return (int)Session["Id_Prd_AREM" + Session.SessionID]; }
        }
        //static bool edicionRemisionDePedido;
        private bool edicionRemisionDePedido
        {
            set { Session["edicionRemisionDePedidoRem" + Session.SessionID] = value; }
            get { return (bool)Session["edicionRemisionDePedidoRem" + Session.SessionID]; }
        }
        /// <summary>
        /// 1 nuevo, 2 actualizacion, 3 RemisionDePedido    
        /// </summary>
        //static int tipoDeMovimiento = 0;
        private int tipoDeMovimiento
        {
            set { Session["tipoDeMovimientoREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["tipoDeMovimientoREM" + Session.SessionID]; return st == null ? 0 : (int)st; }
        }
        //static bool remisionDePedido;
        //static bool hayProductosNoSpo;
        private bool hayProductosNoSpo
        {
            set { Session["hayProductosNoSpoREM" + Session.SessionID] = value; }
            get { return (bool)Session["hayProductosNoSpoREM" + Session.SessionID]; }
        }
        //static int Id_Rem_Actualiza;
        private int Id_Rem_Actualiza
        {
            set { Session["Id_Rem_ActualizaREM" + Session.SessionID] = value; }
            get { return (int)Session["Id_Rem_ActualizaREM" + Session.SessionID]; }
        }


        private int Id_CuentaNacional
        {
            set { Session["Id_CuentaNacional" + Session.SessionID] = value; }
            get { return (int)Session["Id_CuentaNacional" + Session.SessionID]; }
        }



        private int NumCuentaContNacional
        {
            set { Session["NumCuentaContNacional" + Session.SessionID] = value; }
            get { return (int)Session["NumCuentaContNacional" + Session.SessionID]; }
        }

        //Propiedad de lista de productos (partidas) de la Nota de cargo
        private List<RemisionDet> ListaProductosRemisionEspecial
        {
            get { return (List<RemisionDet>)Session["ListaProductosRemisionEspecial"]; }
            set { Session["ListaProductosRemisionEspecial"] = value; }
        }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                /*ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion.URL = HttpContext.Current.Request.Url.Host;
                if (sesion == null)
                {
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                }*/
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);

                    //Edsg 03072017
                    Application["dir_Remisiones"] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                    //Server.Transfer("Login.aspx");
                }


                else
                {
                    if (!Page.IsPostBack)
                    {
                        sesion.HoraInicio = DateTime.Now;
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        int Transferencia = 0;
                        if (HttpContext.Current.Request.Url.ToString().Contains("Trans"))
                        {
                            Transferencia = int.Parse(Page.Request.QueryString["Trans"].ToString());
                        }

                        //oculta campo si no es remision de pedido     
                        CN_Configuracion cn_configuracion = new CN_Configuracion();
                        ConfiguracionGlobal cg = new ConfiguracionGlobal();
                        cg.Id_Emp = sesion.Id_Emp;
                        cg.Id_Cd = sesion.Id_Cd_Ver;
                        cn_configuracion.Consulta(ref cg, sesion.Emp_Cnx);
                        HiddenField2.Value = cg.Mail_MinValuacion.ToString();

                        dpFecha.SelectedDate = DateTime.Now;
                        crearDT();
                        crear_dt_cuenta();
                        CargarTipoMovimiento();
                        cmbTipo.Items.Insert(0, new RadComboBoxItem("Remisiones", "0"));
                        Inicializar();
                        dpFecha.Focus();
                        rgDetalles.DataSource = dt_detalles;
                        rgDetalles.Rebind();

                        if (!sesion.Cu_Modif_Pass_Voluntario)
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");


                        if (!((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(divGenerales.Controls);
                            deshabilitarcontroles(formularioTotales.Controls);
                            GridCommandItem cmdItem = (GridCommandItem)rgDetalles.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;// remove the image button                             
                            rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 1].Display = false;
                            rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 2].Display = false;
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalles.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        //JMM:Si es transferencia solo se puede seleccionar el mov 54
                        if (Transferencia == 1)
                        {
                            this.txtTipoId.Value = 54;
                            this.txtTipoId.Enabled = false;
                            this.cmbTipoMovimiento.SelectedValue = "54";
                            this.cmbTipoMovimiento.Text = "TRANSFERENCIA DE ALMACEN";
                            this.cmbTipoMovimiento.Enabled = false;
                            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = false;
                        }
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
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ErrorManager();
            rgDetalles.DataSource = dt_detalles;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        Nuevo();
                        break;
                    case "save":
                        Guardar(true);
                        break;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
                //Alerta(ex.ToString());
                //cacharMsgBaseDatos(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                throw ex;
            }
        }
        protected void cmbTipoMovimiento_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();

                if (!ValidaSucursal())
                {
                    return;
                }

                if (!ValidaClienteCuentaNacional())
                {
                    return;
                }

                if (cmbTipoMovimiento.SelectedValue != "" && cmbTipoMovimiento.SelectedValue != "-1")
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    bool Tm_ReqSpo2 = false;
                    new CN_CatMovimientos().ConsultarTmovimientoReqSpo(sesion, int.Parse(cmbTipoMovimiento.SelectedValue), ref Tm_ReqSpo2);
                    Tm_ReqSpo = Tm_ReqSpo2;


                    if (tipoDeMovimiento == 3 && Tm_ReqSpo && hayProductosNoSpo)
                    {
                        Alerta("Este pedido contiene artículos que no son sistema de propietario, "
                                + "favor de seleccionar otro tipo de movimiento");
                        cmbTipoMovimiento.SelectedValue = "-1";
                        txtTipoId.Text = "";
                        return;
                    }
                    hf_spo.Value = Tm_ReqSpo.ToString();
                }

                if (tipoDeMovimiento == 1)
                {
                    crearDT();
                    crear_dt_cuenta();
                    rgDetalles.Rebind();
                    CalcularTotales();
                }
                int tipo = Convert.ToInt32(cmbTipoMovimiento.SelectedValue);
                if (tipo == 60)
                {
                    rgDetalles.Columns[rgDetalles.Columns.FindByUniqueName("TipoSalida").OrderIndex - 2].Display = true;
                    rgDetalles.Columns[rgDetalles.Columns.FindByUniqueName("ConceptoTipoSalida").OrderIndex - 2].Display = true;
                }
                else
                {
                    rgDetalles.Columns[rgDetalles.Columns.FindByUniqueName("TipoSalida").OrderIndex - 2].Display = false;
                    rgDetalles.Columns[rgDetalles.Columns.FindByUniqueName("ConceptoTipoSalida").OrderIndex - 2].Display = false;
                }

                if (tipo == 60 && txtTotal.Value >= Convert.ToDouble(HiddenField2.Value))
                {
                    valuacion.Visible = true;
                }
                else
                {
                    valuacion.Visible = false;
                }

                txtClienteId.Focus();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected bool ValidaClienteCuentaNacional()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            bool ret = true;

            Clientes cliente = new Clientes();
            cliente.Id_Emp = sesion.Id_Emp;
            cliente.Id_Cd = sesion.Id_Cd_Ver;
            cliente.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);

            int TieneCuentaNacional = 0;
            new CN_CatCliente().ConsultaClienteTieneCuentaNacional(ref cliente, ref TieneCuentaNacional, sesion.Emp_Cnx);
            if (int.Parse(cmbTipoMovimiento.SelectedValue) == 80)
            {

                if (TieneCuentaNacional == -1 && cliente.Id_Cte != -1)
                {
                    LimpiarCampos1();
                    txtClienteId.Text = "";
                    txtCliente.Text = "";
                    AlertaFocus("Este Cliente no Pertenece a cuenta Nacional", txtClienteId.ClientID);
                    ret = false;
                    return false;
                }
            }
            /*else if (int.Parse(cmbTipoMovimiento.SelectedValue) == 60)
            {

                if (TieneCuentaNacional > 0)
                {
                    LimpiarCampos1();
                    txtClienteId.Text = "";
                    txtCliente.Text = "";
                    AlertaFocus("Este Cliente  Pertenece a cuenta Nacional", txtClienteId.ClientID);
                    ret = false;
                    return false;
                }
            }*/
            return ret;

        }


        protected bool ValidaSucursal()
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Clientes cliente = new Clientes();
            cliente.Id_Emp = sesion.Id_Emp;
            cliente.Id_Cd = sesion.Id_Cd_Ver;
            cliente.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
            cliente.Id_Rik = sesion.Id_Rik;

            bool ret = true;

            if (cliente.Id_Cte != -1)
            {
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                }
                catch (Exception ex)
                {
                    txtClienteId.Text = "";
                    txtCliente.Text = "";
                    AlertaFocus(ex.Message, txtClienteId.ClientID);
                    ret = false;

                    return false;
                }

                if (int.Parse(cmbTipoMovimiento.SelectedValue) == 54 || int.Parse(cmbTipoMovimiento.SelectedValue) == 26)
                {

                    if (cliente.Cte_EsSucursal != null && !cliente.Cte_EsSucursal)
                    {
                        txtClienteId.Text = "";
                        txtCliente.Text = "";
                        LimpiarCampos1();
                        AlertaFocus("Este tipo de movimiento solo se puede aplicar a los clientes de tipo sucursal", txtClienteId.ClientID);
                        ret = false;
                        return false;
                    }

                }


                if (int.Parse(cmbTipoMovimiento.SelectedValue) != 54 && int.Parse(cmbTipoMovimiento.SelectedValue) != 65)
                {

                    if (cliente.Cte_EsSucursal != null && cliente.Cte_EsSucursal)
                    {
                        txtClienteId.Text = "";
                        txtCliente.Text = "";
                        LimpiarCampos1();
                        AlertaFocus("Este tipo de movimiento no se puede aplicar a clientes del tipo sucursal", txtClienteId.ClientID);
                        ret = false;
                        return false;
                    }

                }

            }

            return ret;

        }

        protected void txtClienteId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();

                cmbCliente_indiceCambia();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbTerritorio_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (cmbTerritorio.SelectedValue == "-1")
                {
                    txtRepresentante.Text = "";
                    txtRepresentanteStr.Text = "";
                    txtTerritorioId.Focus();
                }
                else
                {
                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = sesion.Id_Emp;
                    ter.Id_Cd = sesion.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.SelectedValue);
                    territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                    txtRepresentante.Text = ter.Id_Rik.ToString();
                    txtRepresentanteStr.Text = ter.Rik_Nombre;
                    txtCalle.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgDetalles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ErrorManager();

            if (e.CommandName == "InitInsert")
            {
                cantidad_A = 0;
                Id_RemDet_A = 0;
                Id_Prd_A = 0;
                costo_A = 0;

                if (!validarCamposDetalle())
                {
                    e.Canceled = true;
                    return;
                }
                if (!validarFecha())
                {
                    e.Canceled = true;
                    return;
                }
            }
            if (e.CommandName == "Edit")
            {
                Id_RemDet_A = int.Parse(rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_RemDet"].Text);
                Id_Prd_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                cantidad_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Cantidad"].FindControl("CantidadLabel") as Label).Text);
                costo_A = double.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Precio"].FindControl("PrecioLabel") as Label).Text);
                // RadNumericTextBox rad = (rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("RadNumericTextBox1") as RadNumericTextBox);               
                //rad.ClientEvents.OnLoad = "";
                //rad.ClientEvents.OnBlur = "";               
            }
        }
        protected void rgDetalles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            ErrorManager();
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                RadComboBox RadComboBoxTerr = editItem.FindControl("RadComboBoxTerr") as RadComboBox;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1), sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref RadComboBoxTerr);
                Label TerrIdDesc = (e.Item.FindControl("TerrIdDesc") as Label);




                RadComboBox RadComboBoxTipoSalida = e.Item.FindControl("RadComboBoxTipoSalida") as RadComboBox;
                new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1), sesion.Emp_Cnx, "sp_CatRemisionesTipoSalida", ref RadComboBoxTipoSalida);



                RadComboBox RadComboBoxConceptoTipoSalida = e.Item.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox;
                new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, -1, sesion.Emp_Cnx, "sp_CatRemisionesConceptoTipoSalida", ref RadComboBoxConceptoTipoSalida);

                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                { //se habilitan todos los controles
                    RadComboBoxTerr.Enabled = true;
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = true;
                    (editItem["importe"].Controls[0] as TextBox).Visible = false;
                }

                Control updatebtn = (Control)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    //se llenan y deshabilita cmb territorio y txtterritorio, cmb producto y txt producto.
                    //comboterritorio
                    (editItem.FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//
                    (editItem.FindControl("txtter1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//txtterr
                    (editItem.FindControl("RadComboBoxTerr") as RadComboBox).Enabled = false;
                    (editItem.FindControl("txtter1") as RadNumericTextBox).Enabled = false;
                    //producto
                    (editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();//txtproducto
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = false;//txtbox id del producto                    
                    editItem["importe"].Controls[1].Visible = false;
                }
                if (cmbTipoMovimiento.SelectedValue == "92")
                {
                    (editItem.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = "0.00";
                    (editItem.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Enabled = false;
                }
            }

            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["IdTerr"].FindControl("txtter1");
                if (!dataField.Enabled)
                {
                    dataField = (RadNumericTextBox)form["Cantidad"].FindControl("RadNumericTextBoxCantidad");
                }

                dataField.Focus();
            }
            //-----------------------------------------
        }
        protected void cmbTipoSalidaIdDesc_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RadComboBox combo = sender as RadComboBox;
            RadComboBox RadComboBoxConceptoTipoSalida = combo.Parent.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox;
            new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(e.Value), sesion.Emp_Cnx, "sp_CatESConceptoTipoSalida", ref RadComboBoxConceptoTipoSalida);
            RadComboBoxConceptoTipoSalida.SelectedValue = "1";
            RadComboBoxConceptoTipoSalida.Text = "";
        }
        protected void rgDetalles_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();

                GridEditableItem editedItem = e.Item as GridEditableItem;
                txtCantidad_TextChanged(editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox, e);
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                //RadComboBox comboboxProductos = (editedItem.FindControl("cmbProductosLista") as RadComboBox);
                RadNumericTextBox TxtProducto = editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                //comprobar campos vacios
                if (
                        ((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "" || (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "-1")
                        || !TxtProducto.Value.HasValue
                        || (editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text == ""
                        || (editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text == ""
                    )
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                int territorio = int.Parse((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue);
                string DescrTer = (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedItem.Text;
                int Id_Prd = Convert.ToInt32(TxtProducto.Value);
                string descripcion = (editedItem["Descripcion"].FindControl("DescripcionTextBox") as RadTextBox).Text;
                int cantidad = Convert.ToInt32((editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text);
                double precio = double.Parse((editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text);
                double importe = cantidad * precio;

                //No se puede ingresar el mismo producto varias veces a menos ke tenga territorio distinto
                //msg Producto ya capturado
                if (dt_detalles.Select("Id_Prd='" + Id_Prd + "' and Terr='" + territorio + "'").Length > 0)
                {
                    Alerta("El producto ya ha sido agregado a la remisión");
                    e.Canceled = true;
                    return;
                }
                if (cantidad == 0)
                {
                    Alerta("No puede ingresar una partida con cantidad 0");
                    e.Canceled = true;
                    return;
                }

                //cmbTipoMovimiento
                if (precio == 0 && cmbTipoMovimiento.SelectedValue != "92") //FRank: precio especial y precio publico se pueden modificar y no deben ser 0
                {
                    Alerta("No puede ingresar una partida con precio 0");
                    e.Canceled = true;
                    return;
                }


                if (Id_CuentaNacional > 0 && txtTipoId.Value == 80)
                {

                    WS_Producto.Service1 ws = new WS_Producto.Service1();
                    ws.Url = ConfigurationManager.AppSettings["WS_Producto"].ToString();

                    string envio = "" + Id_CuentaNacional + "|" + Id_Prd + "";
                    object respuesta = ws.TraeProductoCN(envio);
                    XmlDocument Xml = new XmlDocument();
                    Xml.LoadXml(respuesta.ToString());

                    XmlNode NodeError = Xml.SelectSingleNode("//Producto/MsgError/@Error");
                    XmlNode NodeValida = Xml.SelectSingleNode("//CuentaNacional/@ValidaCodEsp");
                    XmlNode NodeProductoID = Xml.SelectSingleNode("//Producto/@ProNum");
                    XmlNode NodeProductoDesc = Xml.SelectSingleNode("//Producto/@ProDesc");
                    XmlNode NodeProUM = Xml.SelectSingleNode("//Producto/@ProUM");
                    XmlNode NodeProPrecio = Xml.SelectSingleNode("//Producto/@ProPrecio");


                    if (!string.IsNullOrEmpty(NodeValida.InnerText))
                    {
                        if (NodeValida.InnerText != "N")
                        {

                            if (!string.IsNullOrEmpty(NodeError.InnerText))
                            {
                                Alerta(NodeError.InnerText);
                                e.Canceled = true;
                                return;
                            }
                        }

                    }


                }






                //int cantidadEnDt_cuentaOriginal = 0;
                ////---si es REMISION DE PEDIDO, contar lo que ya se tenìa
                //if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //{
                //    cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //    {
                //        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //        if ((cantidad + cuentaActual) > cantidadEnDt_cuentaOriginal)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (cantidad > cantidadEnDt_cuentaOriginal)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //}

                ////************* SI es edicion de una remision de pedido, verificar que la cantidad no supere lo del pedido***************
                //if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //{
                //    int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //    {
                //        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //        cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                //        if ((cantidad + cuentaActual - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (cantidad > cantidadEnPedido)
                //        {
                //            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //}
                //----------------------------------------------------------------------------------------------------------------------

                //int disponible = 0;
                //int invFinal = 0;
                //int asignado = 0;
                //new CN_CapEntradaSalida().ConsultarDisponible(session, Id_Prd, ref disponible, ref invFinal, ref asignado);

                ////cuenta articulos
                //DataRow[] row_cuenta;
                //if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                //{
                //    int original = tipoDeMovimiento == 2 ? int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString()) : 0;
                //    row_cuenta = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                //    if (int.Parse(row_cuenta[0]["Cantidad"].ToString()) - original + cantidad > disponible)
                //    {// MSG asignado por antiguo sian
                //        Alerta("Producto <b>" + Id_Prd.ToString() + "</b> inventario disponible insuficiente, <br>inventario final: " + invFinal.ToString() +
                //            ",<br>asignado: " + asignado.ToString() + ",<br>disponible: " + disponible.ToString());
                //        e.Canceled = true;
                //        return;
                //    }
                //}
                //else
                //{
                //    if (cantidad > disponible)
                //    {// MSG asignado por antiguo sian
                //        Alerta("Producto <b>" + Id_Prd.ToString() + "</b> inventario disponible insuficiente,</br>inventario final: " + invFinal.ToString() + ",</br>asignado: " + asignado.ToString() + ",</br>disponible: " + disponible.ToString());
                //        e.Canceled = true;
                //        return;
                //    }
                //}

                //agregar al dt
                DataRow[] editable_dr;
                if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                {
                    dt_cuenta.Rows.Add(new object[] { Id_Prd, cantidad });
                }

                double iva_cd = 0;
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                dt_detalles.Rows.Add(new object[] { ++id_detalle, territorio, Id_Prd, descripcion, cantidad, precio, importe, DescrTer });
                CalcularTotales();
                if (txtTipoId.Value == 60)
                {
                    double total = !string.IsNullOrEmpty(txtTotal.Text) ? Convert.ToDouble(txtTotal.Text) : 0;
                    if (total >= Convert.ToDouble(HiddenField2.Value))
                    {
                        valuacion.Visible = true;
                    }
                    else
                    {
                        valuacion.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            { //Alerta("No se pudieron guardar los datos. " + msgerror(ex)); //cambiar esto
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgDetalles_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                txtCantidad_TextChanged(sender, e);
                RadNumericTextBox TxtProducto = editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                //comprobar campos vacios
                if (
                        ((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "" || (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue == "-1")
                        || !TxtProducto.Value.HasValue
                        || (editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text == ""
                        || (editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text == ""
                    )
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                int Id_RemDet = int.Parse((editedItem["Id_RemDet"].Controls[0] as TextBox).Text);
                int territorio = int.Parse((editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue);
                string DescrTer = (editedItem["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedItem.Text;
                int Id_Prd = Convert.ToInt32(TxtProducto.Value);
                string descripcion = (editedItem["Descripcion"].FindControl("DescripcionTextBox") as RadTextBox).Text;
                int cantidad = Convert.ToInt32((editedItem["Cantidad"].FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text);
                double precio = double.Parse((editedItem["Precio"].FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text);
                double importe = cantidad * precio;

                if (cantidad == 0)
                {
                    Alerta("No puede ingresar una partida con cantidad 0");
                    e.Canceled = true;
                    return;
                }

                if (precio == 0 && cmbTipoMovimiento.SelectedValue != "92") //FRank: precio especial y precio publico se pueden modificar y no deben ser 0
                {
                    Alerta("No puede ingresar una partida con precio 0");
                    e.Canceled = true;
                    return;
                }

                int dif = 0;
                if (cantidad > cantidad_A)
                {
                    dif = cantidad - cantidad_A;
                    //si es actualizacion, contar lo que ya se tenìa
                    int cantidadEnDt_cuentaOriginal = 0;
                    if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    {
                        cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                        if ((dif + cuentaActual) > cantidadEnDt_cuentaOriginal)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            e.Canceled = true;
                            return;
                        }
                    }

                    //**************verificar que no pase lo que se ordenó ene l pedido*********
                    //if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    //{
                    //    int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                    //    int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + Id_Prd + "'")[0]["Cantidad"].ToString());
                    //    if ((dif + cuentaActual) > cantidadEnPedido)
                    //    {
                    //        Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                    //        e.Canceled = true;
                    //        return;
                    //    }
                    //}
                    //***************************************************************************
                    //sumar diferencia al dt_Cuenta
                    DataRow[] editable_drCuenta;
                    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    {
                        editable_drCuenta = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                        editable_drCuenta[0].BeginEdit();
                        editable_drCuenta[0]["Cantidad"] = int.Parse(editable_drCuenta[0]["Cantidad"].ToString()) + dif;
                        editable_drCuenta[0].AcceptChanges();
                    }
                }
                else
                {
                    dif = cantidad_A - cantidad;

                    DataRow[] editable_drCuenta;
                    if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                    {
                        editable_drCuenta = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                        editable_drCuenta[0].BeginEdit();
                        editable_drCuenta[0]["Cantidad"] = int.Parse(editable_drCuenta[0]["Cantidad"].ToString()) - dif;
                        editable_drCuenta[0].AcceptChanges();
                    }
                }

                //update dt_detalles
                DataRow[] editable_dr;
                editable_dr = null;

                if (dt_detalles.Select("Id_RemDet='" + Id_RemDet_A + "'").Length > 0)
                {
                    editable_dr = dt_detalles.Select("Id_RemDet='" + Id_RemDet_A + "'");

                    editable_dr[0].BeginEdit();
                    editable_dr[0]["cantidad"] = cantidad;
                    editable_dr[0]["Precio"] = precio;
                    editable_dr[0]["Importe"] = cantidad * precio;
                    editable_dr[0].AcceptChanges();
                }
                else
                {
                    throw new Exception("Error: No se pudo editar el detalle");
                }

                //se borran las cantidades anteriores del subtotal y se agregan las nuevas
                //subtotal -= costo_A * cantidad_A;
                CalcularTotales();
                //double iva_cd = 0;
                //new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                //subtotal += cantidad * precio;
                //IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                //total = subtotal + IVA;
                //txtSub.Text = subtotal.ToString();
                //txtIva.Text = IVA.ToString();
                //txtTotal.Text = total.ToString();

                if (txtTipoId.Value == 60)
                {
                    double total = !string.IsNullOrEmpty(txtTotal.Text) ? Convert.ToDouble(txtTotal.Text) : 0;
                    if (total >= Convert.ToDouble(HiddenField2.Value))
                    {
                        valuacion.Visible = true;
                    }
                    else
                    {
                        valuacion.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void rgDetalles_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                double iva_cd = 0;
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);

                string Id_RemDet = rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_RemDet"].Text;
                int cantidad = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Cantidad"].FindControl("CantidadLabel") as Label).Text);
                double precio = double.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Precio"].FindControl("PrecioLabel") as Label).Text);
                DataRow[] roww;
                roww = dt_detalles.Select("Id_RemDet='" + Id_RemDet + "'");
                if (roww.Length != 1)
                {
                    throw new Exception(" ");
                    //return;
                }
                dt_detalles.Rows.Remove(roww[0]);

                CalcularTotales();
                //subtotal -= cantidad * precio;
                //IVA = float.Parse((subtotal * (iva_cd / 100)).ToString());
                //total = subtotal + IVA;
                //txtSub.Text = subtotal.ToString();
                //txtIva.Text = IVA.ToString();
                //txtTotal.Text = total.ToString();

                ///*QUITAR productos a la lista (dt_cuenta)*/
                DataRow[] editable_dr;
                int Id_Prd = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) - cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                    throw new Exception(" ");
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                cacharMsgBaseDatos(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            finally
            {
                RadAjaxManager1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox estecombo = (sender as RadNumericTextBox);
                //if ((Telerik.Web.UI.GridDataInsertItem)estecombo.Parent.Parent != null)
                //{
                Telerik.Web.UI.GridDataInsertItem j = (Telerik.Web.UI.GridDataInsertItem)estecombo.Parent.Parent;

                int territorio = int.Parse((j["Terr"].FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue);

                if (dt_detalles.Select("Id_Prd='" + estecombo.Text + "' and Terr='" + territorio + "'").Length > 0)
                {
                    AlertaFocus("El producto ya ha sido agregado a la remisión", estecombo.ClientID);
                    estecombo.Text = "";
                    (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";

                    return;
                }

                float precioPublico = -1;
                ClienteProd clienteProd = new ClienteProd();
                clienteProd.Id_Emp = session.Id_Emp;
                clienteProd.Id_Cd = session.Id_Cd_Ver;
                clienteProd.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                clienteProd.Id_Prd = Convert.ToInt32(estecombo.Value.HasValue ? estecombo.Value : -1);

                RadNumericTextBox txtFac_Territorio = (RadNumericTextBox)estecombo.Parent.FindControl("txtter1");
                Producto prd = new Producto();
                //prd.Id_Ter = Convert.ToInt32(txtFac_Territorio.Value.HasValue ? txtFac_Territorio.Value : -1);
                prd.Id_Mov = (int?)txtTipoId.Value;
                prd.Id_Cd = clienteProd.Id_Cd;
                prd.Id_Cte = clienteProd.Id_Cte;


                try
                {
                    new CN_CatProducto().ConsultaProducto(ref prd, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt32(estecombo.Value.HasValue ? estecombo.Value : -1));
                }
                catch (Exception ex)
                {
                    (j.FindControl("DescripcionTextBox") as RadTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = "";
                    (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Text = "";
                    estecombo.Value = null;
                    AlertaFocus(ex.Message, estecombo.ClientID);
                    return;
                }
                new CN_CatClienteProd().ClienteProductoPrecioPublico_Consultar(ref clienteProd, session.Emp_Cnx, ref precioPublico);

                (j.FindControl("DescripcionTextBox") as RadTextBox).Text = prd.Prd_Descripcion;

                //FRank: precio especial y precio publico se pueden modificar y no deben ser 0
                if (precioPublico > 0)
                {//rm precio especial no es modificable
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = precioPublico.ToString();//verificar control

                }
                else
                {
                    //no tiene precio especial el cliente-producto
                    if (!string.IsNullOrEmpty(prd.Prd_Precio))
                        (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = prd.Prd_Precio /*precio_publico*/ == "-1" ? 0.ToString() : prd.Prd_Precio;//verificar control
                }
                (j.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Focus();
                if (cmbTipoMovimiento.SelectedValue == "92")
                {
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Text = "0.00";
                    (j.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Enabled = false;
                }
                // }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void dpFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                crearDT();
                crear_dt_cuenta();
                validarFecha();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {

        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox Txtcantidad = (sender as RadNumericTextBox);
                int cantidad = Txtcantidad.Value.HasValue ? (int)Txtcantidad.Value : 0;
                int prd = Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("RadNumericTextBox1") as RadNumericTextBox).Value);
                int ter = Convert.ToInt32((Txtcantidad.Parent.Parent.FindControl("txtter1") as RadNumericTextBox).Value);

                int disponible = 0;
                int invFinal = 0;
                int asignado = 0;
                int cantidad_B = 0;
                new CN_CapEntradaSalida().ConsultarDisponible(session, prd, ref disponible, ref invFinal, ref asignado);

                DataRow[] Dr = dt_detalles.Select("Id_Prd='" + prd + "' and Terr <> '" + ter + "'");

                Remision remision = new Remision();
                List<RemisionDet> detalle = new List<RemisionDet>();
                remision.Id_Emp = session.Id_Emp;
                remision.Id_Cd = session.Id_Cd_Ver;
                remision.Id_Rem = !string.IsNullOrEmpty(txtFolio.Text) ? Convert.ToInt32(txtFolio.Text) : -1;
                new CN_CapRemision().ConsultarRemisionesDetalle(session, remision, ref detalle);

                if (Dr.Length > 0)
                {
                    for (int i = 0; i < Dr.Length; i++)
                        cantidad_B += !string.IsNullOrEmpty(Dr[i]["Cantidad"].ToString()) ? Convert.ToInt32(Dr[i]["Cantidad"]) : 0;
                }
                int count = 0;
                foreach (RemisionDet rd in detalle)
                {
                    if (rd.Id_Prd == prd)
                        count += rd.Rem_Cant;
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

                //-----------------------------------------------------------
                int cantidadEnDt_cuentaOriginal = 0;
                //---si es REMISION DE PEDIDO, contar lo que ya se tenìa
                if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + prd + "'").Length > 0)
                {
                    cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                    if (dt_cuenta.Select("Id_Prd='" + prd + "'").Length > 0)
                    {
                        //int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                        if ((cantidad /*+ cuentaActual*/) > cantidadEnDt_cuentaOriginal)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                    else
                    {
                        if (cantidad > cantidadEnDt_cuentaOriginal)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                }

                //************* SI es edicion de una remision de pedido, verificar que la cantidad no supere lo del pedido***************
                if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + prd + "'").Length > 0)
                {
                    int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                    if (dt_cuenta.Select("Id_Prd='" + prd + "'").Length > 0)
                    {
                        int cuentaActual = int.Parse(dt_cuenta.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                        cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + prd + "'")[0]["Cantidad"].ToString());
                        if ((cantidad /*+ cuentaActual */ - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                    else
                    {
                        if ((cantidad - cantidadEnDt_cuentaOriginal) > cantidadEnPedido)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                }

                //---------------------------------------------
                if (cantidad + cantidad_B > disponible + count + disponible_pedido)
                {
                    AlertaFocus("Producto <b>" + prd.ToString() + "</b> inventario disponible insuficiente,</br>inventario final: " + invFinal.ToString() + ",</br>asignado: " + asignado.ToString() + ",</br>disponible: " + (disponible + count + disponible_pedido).ToString(), Txtcantidad.ClientID);
                    Txtcantidad.Text = "";
                    return;
                }
                else
                {
                    (Txtcantidad.Parent.Parent.FindControl("RadNumericTextBoxPrecio") as RadNumericTextBox).Focus();
                }

                if (tipoDeMovimiento == 2 && txtTipoId.Value == 60)
                {
                    int territorio = (int)((Txtcantidad.Parent.FindControl("txtter1") as RadNumericTextBox).Value.Value);
                    CN_CapEntradaSalida CNentrada = new CN_CapEntradaSalida();
                    int verificador = 0;
                    CNentrada.ConsultarSaldo(session.Id_Emp, session.Id_Cd_Ver, prd.ToString(), territorio.ToString(), txtClienteId.Text, session.Emp_Cnx, ref verificador, "60");

                    if (verificador - (cantidad_A - cantidad) < 0)
                    {
                        AlertaFocus("El producto " + prd.ToString() + " no cuenta con saldo suficiente", Txtcantidad.ClientID);
                        Txtcantidad.Text = "";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                ErrorManager();
                switch (e.Argument.ToString())
                {
                    case "cliente":
                        txtClienteId.Text = Session["Id_Buscar" + Session.SessionID].ToString();
                        txtClienteId_TextChanged(null, null);
                        break;
                    case "precio":
                        (producto as RadNumericTextBox).DbValue = Session["Id_Buscar" + Session.SessionID];
                        cmbProductoDet_TextChanged(producto, null);
                        if ((producto as RadNumericTextBox).Value.HasValue)
                        {
                            ((producto as RadNumericTextBox).Parent.FindControl("RadNumericTextBoxCantidad") as RadNumericTextBox).Focus();
                        }
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 180);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        break;
                    case "si":
                        Guardar(true);
                        break;
                    case "no":
                        Guardar(false);
                        break;
                    case "direccion":
                        CN_CatCliente clsCliente = new CN_CatCliente();
                        ClienteDirEntrega cliente = new ClienteDirEntrega();
                        Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        cliente.Id_Emp = Sesion.Id_Emp;
                        cliente.Id_Cd = Sesion.Id_Cd_Ver;

                        string[] id = Session["Id_Buscar" + Session.SessionID].ToString().Split('-');


                        cliente.Id_CteDirEntrega = Int32.Parse(id[0]) - 1;
                        cliente.Id_Cte = Int32.Parse(Session["Descripcion_Buscar" + Session.SessionID].ToString());
                        clsCliente.ConsultaClienteDirEntrega(cliente, Sesion.Emp_Cnx);
                        txtCalle.Text = cliente.Cte_Calle;
                        txtNumero2.Text = cliente.Cte_Numero;
                        txtCp.Text = cliente.Cte_Cp.Trim();
                        txtColonia.Text = cliente.Cte_Colonia;
                        txtMunicipio.Text = cliente.Cte_Municipio;
                        txtEstado.Text = cliente.Cte_Estado;
                        txtTelefono2.Text = cliente.Cte_Telefono;

                        break;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadAjaxManager1.ResponseScripts.Add("AbrirBuscarDireccionEntrega();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ErrorManager();
                RadAjaxManager1.ResponseScripts.Add("popup();");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void BtnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string mensaje = string.Empty;
                CN_RemisionesEntrega cn_remision = new CN_RemisionesEntrega();
                Remision Rem = new Remision();
                List<Remision> Remision = new List<CapaEntidad.Remision>();
                Rem.Id_Emp = Sesion.Id_Emp;
                Rem.Id_Cd = Sesion.Id_Cd_Ver;
                Rem.Id_Rem = Convert.ToInt32(Request.QueryString["Id_Rem"]);
                cn_remision.AutorizarRemision(ref Rem, Sesion.Emp_Cnx, "R");
                if (Rem.Id_Rem == 0)
                {
                    Alerta("Este documento ya ha sido Autorizado o Rechazado, favor de validar el estatus actual.");
                    return;
                }
                EnviarCorreoAutorizacion(Convert.ToInt32(Rem.Id_Rem), Rem.UCorreo, "Rechazado");
                RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));

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
                string mensaje = string.Empty;

                CN_RemisionesEntrega cn_remision = new CN_RemisionesEntrega();
                Remision Rem = new Remision();
                List<Remision> Remision = new List<CapaEntidad.Remision>();
                Rem.Id_Emp = Sesion.Id_Emp;
                Rem.Id_Cd = Sesion.Id_Cd_Ver;
                Rem.Id_Rem = Convert.ToInt32(Request.QueryString["Id_Rem"]);
                cn_remision.AutorizarRemision(ref Rem, Sesion.Emp_Cnx, "A");
                if (Rem.Id_Rem == 0)
                {
                    Alerta("Este documento ya ha sido Autorizado o Rechazado, favor de validar el estatus actual.");
                    return;
                }
                EnviarCorreoAutorizacion(Convert.ToInt32(Rem.Id_Rem), Rem.UCorreo, "Autorizado");
                Imprimir(Rem);
                RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "ImageButton1_Click");
            }
        }

        private void EnviarCorreoAutorizacion(int Id_Rem, string correo, string movimiento)
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
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha " + movimiento + " la remisión de transferencia de almacen con el folio  " + Id_Rem);
                cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "Capremisiones_Lista.aspx'" + ">");
                cuerpo_correo.Append("Solicitud de autorización de transferencia de almacen a Cedis Atendida</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(correo));
                m.Subject = "Autorización de transferencia de almacen a cedis #" + Id_Rem + " del centro " + session.Id_Cd_Ver;
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
                }
                catch (Exception)
                {
                    //CambiarEstatus(ordCompra, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                //Alerta("Solicitud enviada correctamente");
                rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Imprimir(Remision Rem)
        {
            try
            {
                ///Valida que el estatus esté en capturado, impreso, embarcado o entregado.
                ///Que sea diferente a baja
                ///Lo manda a imprimir, y se cambia el estatus a impreso.
                if (Rem.Rem_Estatus != "A")
                {
                    Alerta("El documento se encuentra en estatus no válido");
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_Emp = sesion.Id_Emp;
                int Id_Cd_Ver = sesion.Id_Cd_Ver;
                int Id_Rem = Rem.Id_Rem;

                GridItem gi = Session["Pedido" + Session.SessionID] as GridItem;

                Remision remision = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 0);
                ArrayList ALValorParametrosInternos = new ArrayList();

                // Validar si la Remisión es Válida o no en base a la suma de los montos en las partidas de la remisión y la remisión especial.
                bool bRemisionValida = false;
                new CN_CapRemision().ValidaMontosImpresion(remision, sesion.Id_Cd_Ver, sesion.Id_Emp, 1, sesion.Emp_Cnx, ref bRemisionValida);

                if (bRemisionValida)
                {
                    if (remision.Id_Tm == 54 || remision.Id_Tm == 80)
                    {
                        CN_CatCliente cn_catcliente = new CN_CatCliente();
                        Clientes cliente = new Clientes();
                        cliente.Id_Emp = sesion.Id_Emp;
                        cliente.Id_Cd = sesion.Id_Cd_Ver;
                        cliente.Id_Cte = remision.Id_Cte;
                        cn_catcliente.ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                        if (remision.Rem_CteCuentaNacional != 0 || cliente.Cte_RemisionElectronica > 0)
                        {
                            if (cliente.Cte_RemisionElectronica > 0 && remision.Rem_CteCuentaNacional == null || remision.Rem_CteCuentaNacional == 1)
                            {
                                // ImprimirRemisionElectronicaPIPES(remision);
                            }
                            else
                            {
                                ImprimirRemisionElectronica(remision);
                            }

                            return;
                        }
                    }

                    ////Consulta centro de distribución               
                    ALValorParametrosInternos.Add(remision.Id_Emp);
                    ALValorParametrosInternos.Add(remision.Rem_Calle);
                    if (!string.IsNullOrEmpty(remision.Rem_Numero))
                        remision.Rem_Numero = "# " + remision.Rem_Numero;
                    ALValorParametrosInternos.Add(remision.Rem_Numero);
                    ALValorParametrosInternos.Add(remision.Rem_Colonia);
                    ALValorParametrosInternos.Add(remision.Rem_Municipio);
                    ALValorParametrosInternos.Add(remision.Rem_Estado);
                    if (!string.IsNullOrEmpty(remision.Rem_Cp))
                        if (!remision.Rem_Cp.Contains("C.P."))
                            remision.Rem_Cp = "C.P. " + remision.Rem_Cp;
                    ALValorParametrosInternos.Add(remision.Rem_Cp);
                    ALValorParametrosInternos.Add(remision.Id_Rem);
                    ALValorParametrosInternos.Add(remision.Rem_Fecha.ToShortDateString());
                    ALValorParametrosInternos.Add((remision.Rem_FechaHr == null) ? "00:00" : remision.Rem_FechaHr.Value.ToShortTimeString());
                    ALValorParametrosInternos.Add(remision.Cte_NomComercial);
                    ALValorParametrosInternos.Add((remision.Id_Ped == 0 || remision.Id_Ped == -1) ? string.Empty : remision.Id_Ped.ToString());
                    ALValorParametrosInternos.Add(remision.Rem_Conducto);
                    ALValorParametrosInternos.Add(remision.Cte_CondPago);

                    ALValorParametrosInternos.Add(remision.Cte_Calle);
                    ALValorParametrosInternos.Add(remision.Cte_Numero);
                    ALValorParametrosInternos.Add(remision.Cte_Colonia);

                    ALValorParametrosInternos.Add(remision.Id_Cte);
                    ALValorParametrosInternos.Add(remision.Id_Tm);
                    ALValorParametrosInternos.Add(remision.Tm_Nombre);
                    ALValorParametrosInternos.Add(remision.Id_Cd);
                    ALValorParametrosInternos.Add((remision.Id_Ter == -1) ? string.Empty : remision.Id_Ter.ToString());
                    ALValorParametrosInternos.Add(remision.Id_Rik == -1 ? string.Empty : remision.Id_Rik.ToString());

                    ALValorParametrosInternos.Add(remision.Rik_Calle);
                    ALValorParametrosInternos.Add(remision.Rik_Numero);
                    ALValorParametrosInternos.Add(remision.Rik_Colonia);

                    ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", remision.Rem_Subtotal));
                    ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", remision.Rem_Iva));
                    ALValorParametrosInternos.Add(String.Format("{0:$#,##0.00}", remision.Rem_Total));
                    ALValorParametrosInternos.Add(remision.Rem_OrdenCompra);
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);


                    StringBuilder NotaProductosOriginales = new StringBuilder();

                    if (remision.Rem_Especial > 0)
                    {
                        List<RemisionDet> detalles = new List<RemisionDet>();
                        new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);

                        foreach (RemisionDet detalle in detalles)
                        {
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(detalle.Id_Prd.ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(Math.Round(detalle.Rem_Precio, 2).ToString());
                            NotaProductosOriginales.Append("/");
                            NotaProductosOriginales.Append(detalle.Rem_Cant.ToString());
                        }
                    }
                    ALValorParametrosInternos.Add(remision.Rem_Nota + NotaProductosOriginales.ToString());
                    ALValorParametrosInternos.Add("pp");
                    ALValorParametrosInternos.Add("jj");
                    ALValorParametrosInternos.Add("jj");
                    ALValorParametrosInternos.Add("jj");
                    ALValorParametrosInternos.Add("ks");
                    ALValorParametrosInternos.Add("sd");
                    ALValorParametrosInternos.Add("sd");
                    ALValorParametrosInternos.Add("sd");
                    ALValorParametrosInternos.Add("sds");
                    ALValorParametrosInternos.Add("sd");


                    Type instance = null;
                    instance = typeof(LibreriaReportes.RemisionImpresion);
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;

                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
                else
                {
                    RadAjaxManager1.ResponseScripts.Add("return OpenWindow('" + 2 + "','" + Id_Rem + "','" + true + "','" + "Los montos de la Remisión y la Remisión Especial no coinciden" + "')");
                }
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

        private string ObtenerNombreTerritorio(int Id_Emp, int Id_Cd, int? Id_ter, string Conexion)
        {
            CN_CatTerritorios cn_catterritorio = new CN_CatTerritorios();
            Territorios terr = new Territorios();
            terr.Id_Emp = Id_Emp;
            terr.Id_Cd = Id_Cd;
            terr.Id_Ter = (int)Id_ter;
            cn_catterritorio.ConsultaTerritorio(ref terr, Conexion);
            return terr.Descripcion;
        }

        private void ImprimirRemisionElectronica(Remision remision)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<RemisionDet> listaProdFacturaEspecialFinal = new List<RemisionDet>();

                if (remision.Rem_Estatus != "B")
                {
                    remision.Rem_Estatus = "I";
                }
                int verificador = 0;
                new CN_CapRemision().ModificarRemision_Estatus(remision, sesion.Emp_Cnx, ref verificador);

                new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdFacturaEspecialFinal, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, remision.Id_Rem, remision.Id_Cte);

                List<RemisionDet> detalles = new List<RemisionDet>();
                new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);


                CN_CatCliente cn_catcliente = new CN_CatCliente();
                Clientes clientes = new Clientes();
                clientes.Id_Emp = sesion.Id_Emp;
                clientes.Id_Cd = sesion.Id_Cd_Ver;
                clientes.Id_Cte = remision.Id_Cte;
                cn_catcliente.ConsultaClientes(ref clientes, sesion.Emp_Cnx);

                int Id_Rem = remision.Id_Rem;
                Remision remision2 = new Remision();
                new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision2, 0);

                CN_CatCentroDistribucion cn_cd = new CN_CatCentroDistribucion();
                CentroDistribucion cd = new CentroDistribucion();
                double iva = 0;
                cn_cd.ConsultarIva(sesion.Id_Emp, sesion.Id_Cd_Ver, ref iva, sesion.Emp_Cnx);

                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
                XML_Enviar.Append("<RemisionCuentaNacional");
                XML_Enviar.Append(" TipoDocumento=\"\">");

                XML_Enviar.Append(" <Sucursal");
                XML_Enviar.Append(" CDINum=\"\"");
                XML_Enviar.Append(" CDINom=\"\"");
                XML_Enviar.Append(" CDIIVA=\"\"/>");

                XML_Enviar.Append(" <Documento");
                XML_Enviar.Append(" Folio=\"\"");
                XML_Enviar.Append(" Status=\"\"");
                XML_Enviar.Append(" Fecha=\"\"");
                XML_Enviar.Append(" Conducto=\"\"");
                XML_Enviar.Append(" Total=\"\"/>");

                XML_Enviar.Append(" <Cliente");
                XML_Enviar.Append(" NoCliente=\"\"");
                XML_Enviar.Append(" Nombre=\"\"");
                XML_Enviar.Append(" CuentaNacional=\"\">");

                XML_Enviar.Append(" <DatosFiscales");
                XML_Enviar.Append(" Calle=\"\"");
                XML_Enviar.Append(" Numero=\"\"");
                XML_Enviar.Append(" Colonia=\"\"");
                XML_Enviar.Append(" Municipio=\"\"");
                XML_Enviar.Append(" Estado=\"\"");
                XML_Enviar.Append(" C.P.=\"\"/>");

                XML_Enviar.Append(" <DatosConsignacion");
                XML_Enviar.Append(" Calle=\"\"");
                XML_Enviar.Append(" Numero=\"\"");
                XML_Enviar.Append(" Colonia=\"\"");
                XML_Enviar.Append(" Municipio=\"\"");
                XML_Enviar.Append(" Estado=\"\"");
                XML_Enviar.Append(" C.P.=\"\"/>");
                XML_Enviar.Append(" </Cliente>");

                XML_Enviar.Append(" <DetalleKey>");
                if (detalles.Count() > 0)
                {
                    foreach (RemisionDet d in detalles)
                    {
                        XML_Enviar.Append(" <Producto");
                        XML_Enviar.Append(" Codigo=\"" + d.Producto.Id_Prd + "\"");
                        XML_Enviar.Append(" Descipcion=\"" + d.Producto.Prd_Descripcion.ToString().Replace("\"", "").Replace("'", "").Replace("&", "") +
                                          "\"");
                        XML_Enviar.Append(" Cantidad=\"" + d.Rem_Cant + "\"");
                        XML_Enviar.Append(" Unidad=\"" + d.Prd_UniNe + "\"");
                        XML_Enviar.Append(" Presentacion=\"" + d.Prd_Presentacion + "\"");
                        XML_Enviar.Append(" Precio=\"" + d.Rem_Precio + "\"");
                        XML_Enviar.Append(" Territorio=\"" + d.Producto.Id_Ter + "\"");
                        XML_Enviar.Append(" Nombre=\"" + ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx) + "\"");

                        XML_Enviar.Append(" />");
                    }
                }

                XML_Enviar.Append(" </DetalleKey>");

                if (remision.Rem_CteCuentaNacional == 6 || remision.Rem_CteCuentaNacional == 7)
                {
                    XML_Enviar.Append(" <DetalleEspecial>");
                    if (listaProdFacturaEspecialFinal.Count() > 0)
                    {
                        foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                        {
                            XML_Enviar.Append(" <ProductoEspecial");
                            XML_Enviar.Append(" Codigo=\"" + d.Producto.Id_PrdEsp + "\"");
                            XML_Enviar.Append(" Descipcion=\"" + d.Producto.Prd_DescripcionEspecial.ToString().Replace("\"", "").Replace("'", "").Replace("&", "") +
                                              "\"");
                            XML_Enviar.Append(" Cantidad=\"" + d.Rem_Cant + "\"");
                            XML_Enviar.Append(" Unidad=\"" + d.Producto.Prd_UniNe + "\"");
                            XML_Enviar.Append(" Presentacion=\"" + d.Producto.Prd_Presentacion + "\"");
                            XML_Enviar.Append(" Precio=\"" + d.Rem_Precio + "\"");
                            XML_Enviar.Append(" Territorio=\"" + d.Producto.Id_Ter + "\"");
                            XML_Enviar.Append(" Nombre=\"" + ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx) + "\"");
                            XML_Enviar.Append(" CodigoOriginal=\"" + d.Producto.Id_Prd + "\"");
                            XML_Enviar.Append(" Release=\"" + d.Clp_Release + "\"");

                            XML_Enviar.Append(" />");
                        }
                    }
                    XML_Enviar.Append(" </DetalleEspecial>");
                }
                XML_Enviar.Append(" </RemisionCuentaNacional>");

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(XML_Enviar.ToString());

                XmlNode RemisionCuentaNacional = xml.SelectSingleNode("RemisionCuentaNacional");
                RemisionCuentaNacional.Attributes["TipoDocumento"].Value = "Remision";

                XmlNode Sucursal = RemisionCuentaNacional.SelectSingleNode("Sucursal");
                Sucursal.Attributes["CDINum"].Value = remision.Id_Cd.ToString();
                Sucursal.Attributes["CDINom"].Value = "Remision";
                Sucursal.Attributes["CDIIVA"].Value = iva.ToString();

                XmlNode Documento = RemisionCuentaNacional.SelectSingleNode("Documento");
                Documento.Attributes["Folio"].Value = remision.Id_Rem.ToString();
                Documento.Attributes["Status"].Value = remision.Rem_Estatus.ToString();
                Documento.Attributes["Fecha"].Value = remision.Rem_Fecha.ToShortDateString();
                Documento.Attributes["Conducto"].Value = remision.Rem_Conducto.ToString();
                Documento.Attributes["Total"].Value = remision.Rem_Total.ToString();

                XmlNode Cliente = RemisionCuentaNacional.SelectSingleNode("Cliente");
                Cliente.Attributes["NoCliente"].Value = remision.Id_Cte.ToString();
                Cliente.Attributes["Nombre"].Value = remision.Cte_NomComercial.ToString();
                Cliente.Attributes["CuentaNacional"].Value = remision.Rem_CteCuentaNacional.ToString();

                XmlNode DatosFiscales = Cliente.SelectSingleNode("DatosFiscales");
                DatosFiscales.Attributes["Calle"].Value = clientes.Cte_FacCalle.ToString();
                DatosFiscales.Attributes["Numero"].Value = clientes.Cte_FacNumero.ToString();
                DatosFiscales.Attributes["Colonia"].Value = clientes.Cte_FacColonia.ToString();
                DatosFiscales.Attributes["Municipio"].Value = clientes.Cte_FacMunicipio.ToString();
                DatosFiscales.Attributes["Estado"].Value = clientes.Cte_FacEstado.ToString();
                DatosFiscales.Attributes["C.P."].Value = clientes.Cte_FacCp.ToString();

                XmlNode DatosConsignacion = Cliente.SelectSingleNode("DatosConsignacion");
                DatosConsignacion.Attributes["Calle"].Value = remision2.Rem_Calle.ToString();
                DatosConsignacion.Attributes["Numero"].Value = remision2.Rem_Numero.ToString();
                DatosConsignacion.Attributes["Colonia"].Value = remision2.Rem_Colonia.ToString();
                DatosConsignacion.Attributes["Municipio"].Value = remision.Rem_Municipio.ToString();
                DatosConsignacion.Attributes["Estado"].Value = remision.Rem_Estado.ToString();
                DatosConsignacion.Attributes["C.P."].Value = remision.Rem_Cp.ToString();

                /*
                StringBuilder cadena = new StringBuilder();
                int contador = 0;
                foreach (RemisionDet d in listaProdFacturaEspecialFinal)
                {
                if (contador > 0)
                cadena.Append("|");
                cadena.Append(remision.Id_Cd);
                cadena.Append("|");
                cadena.Append(d.Clp_Release.Replace("|",""));
                cadena.Append("|");
                cadena.Append(remision.Id_Rem);
                cadena.Append("|");
                cadena.Append(remision.Rem_EstatusStr.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Id_Ter);
                cadena.Append("|");
                cadena.Append(ObtenerNombreTerritorio(sesion.Id_Emp, sesion.Id_Cd_Ver, d.Producto.Id_Ter, sesion.Emp_Cnx));
                cadena.Append("|");
                cadena.Append(remision.Id_Cte);
                cadena.Append("|");
                cadena.Append(remision.Cte_NomComercial.Replace("|", ""));
                cadena.Append("|");
                //DATOS FISCALES
                cadena.Append(clientes.Cte_FacCalle.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacNumero.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacColonia.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacMunicipio.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacEstado.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(clientes.Cte_FacCp.Replace("|", ""));
                cadena.Append("|");
                //DATOS DE CONSIGNACION
                cadena.Append(remision2.Rem_Calle.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Numero.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Colonia.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Municipio.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Estado.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(remision2.Rem_Cp.Replace("|", ""));
                cadena.Append("|");
                //PRODUCTOS
                cadena.Append(d.Producto.Id_PrdEsp);
                cadena.Append("|");
                cadena.Append(d.Producto.Id_Prd);
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_DescripcionEspecial.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_Presentacion.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Producto.Prd_UniNs.Replace("|", ""));
                cadena.Append("|");
                cadena.Append(d.Rem_Cant);
                cadena.Append("|");
                cadena.Append(d.Rem_Precio);
                cadena.Append("|");
                cadena.Append(iva);
                cadena.Append("|");
                cadena.Append(remision.Rem_Total);
                if (remision.Rem_FechaHr == null)
                {
                cadena.Append("|");
                cadena.Append(remision.Rem_Fecha);
                }
                else
                {
                cadena.Append("|");
                cadena.Append(remision.Rem_FechaHr);
                }
                contador = 1;
                }
                string cadena_Final = cadena.ToString();*/

                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xml.WriteTo(tx);
                string xmlString = sw.ToString();

                WS_RemElectronicaCtaNacional.Service1 remelectronica = new WS_RemElectronicaCtaNacional.Service1();

                string sianRemisionElectronicaResult = remelectronica.Imprime(xmlString).ToString();

                byte[] PDFRemision = this.Base64ToByte(sianRemisionElectronicaResult);
                remision.PDF = PDFRemision;
                verificador = 0;
                new CN_CapRemision().ModificarRemision_PDF(remision, sesion.Emp_Cnx, ref verificador);

                string tempPDFname = string.Concat("REMISION_", remision.Id_Emp.ToString(), "_", remision.Id_Cd.ToString(), "_", remision.Id_U.ToString(), ".pdf");
                string URLtempPDF = Server.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                this.ByteToTempPDF(URLtempPDF, PDFRemision);
                //RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Funciones
        private bool validarFecha()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!((dpFecha.SelectedDate >= Sesion.CalendarioIni) && (dpFecha.SelectedDate <= Sesion.CalendarioFin)))
                {
                    Alerta("Fecha se encuentra fuera del periodo, favor de teclear la fecha correcta al periodo que se encuentra configurado el sistema");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapRemision", "Id_Rem", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void llenar_cmbTipo()
        {
            cmbTipo.Items.Insert(1, new RadComboBoxItem("Remisiones", "0"));
        }
        private void CargarTipoMovimiento()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatMovimiento_ComboParaRemisiones", ref cmbTipoMovimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cmbCliente_indiceCambia()
        {
            try
            {
                LimpiarCampos1();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                cliente.Id_Rik = sesion.Id_Rik;
                if (!ValidaSucursal())
                {
                    return;
                }



                if (!ValidaClienteCuentaNacional())
                {
                    return;
                }
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                }
                catch (Exception ex)
                {
                    txtClienteId.Text = "";
                    txtCliente.Text = "";
                    AlertaFocus(ex.Message, txtClienteId.ClientID);
                    return;
                }

                if (cliente.Cte_CreditoSuspendido)
                {
                    txtClienteId.Text = "";
                    txtCliente.Text = "";
                    AlertaFocus("Este cliente tiene el crédito suspendido", txtClienteId.ClientID);
                    return;
                }

                txtCliente.Text = cliente.Cte_NomComercial;
                NombreCliente_Amortizacion_Remision.Text = cliente.Cte_NomComercial;

                txtCalle.Text = cliente.Cte_Calle;
                txtNumero2.Text = cliente.Cte_Numero;
                txtCp.DbValue = (string.IsNullOrEmpty(cliente.Cte_Cp)) ? "" : cliente.Cte_Cp.ToString().Trim();
                txtColonia.Text = cliente.Cte_Colonia;
                txtMunicipio.Text = cliente.Cte_Municipio;
                txtEstado.Text = cliente.Cte_Estado;
                txtRfc.Text = cliente.Cte_DRfc;
                txtTelefono2.Text = cliente.Cte_Telefono;
                txtContacto.Text = cliente.Cte_Contacto;
                HiddenCteCuentaNacional.Value = cliente.Cte_RemisionElectronica.ToString();
                Id_CuentaNacional = cliente.Cte_RemisionElectronica;
                HiddenNumCuentaContNacional.Value = cliente.Cte_NumCuentaContNacional.ToString();
                NumCuentaContNacional = cliente.Cte_NumCuentaContNacional == null ? 0 : cliente.Cte_NumCuentaContNacional;

                //List<Territorios> territorios = new List<Territorios>();
                //new CN_CatCliente().ConsultaTerritoriosDelCliente(cliente.Id_Cte.Value, sesion, ref territorios);
                cmbTerritorio_indiceCambiado();
                //CargarComboTerritorios(territorios);
                //new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, cliente.Id_Cte.Value, sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref cmbTerritorio);
                if (cmbTerritorio.Items.Count < 2)
                    Alerta("El cliente no tiene territorios asociados");//verificarmensaje   
                if (IsPostBack)
                {
                    txtTerritorioId.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LimpiarCampos1()
        {
            txtCalle.Text = "";
            txtHora.Text = "";
            txtNumero2.Text = "";
            txtCp.Text = "";
            txtColonia.Text = "";
            txtMunicipio.Text = "";
            txtEstado.Text = "";
            txtRfc.Text = "";
            txtTelefono2.Text = "";
            txtContacto.Text = "";
            txtPedido.Text = "";
            txtConducto.Text = "";
            txtGuia2.Text = "";
            dtpFechaHora.SelectedDate = null;
            txtNota.Text = "";
            txtTerritorioId.Text = "";
            cmbTerritorio.Items.Clear();
            cmbTerritorio.Text = "";
            txtRepresentante.Text = "";
            txtRepresentanteStr.Text = "";
        }
        private void crearDT()
        {
            dt_detalles = new DataTable();
            dt_detalles.Columns.Add("Id_RemDet");
            dt_detalles.Columns.Add("Terr");
            dt_detalles.Columns.Add("Id_Prd");
            dt_detalles.Columns.Add("Descripcion");
            dt_detalles.Columns.Add("Cantidad", typeof(double));
            dt_detalles.Columns.Add("Precio", typeof(double));
            dt_detalles.Columns.Add("Importe", typeof(double));
            dt_detalles.Columns.Add("DescrTer");
            dt_detalles.Columns.Add("DescTipoSalida");
            dt_detalles.Columns.Add("DescConceptoTipoSalida");
        }
        private void CargarComboProducto(string Terr, ref RadComboBox cmbPrd, int Spo)
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Terr == "" ? -1 : Convert.ToInt32(Terr), Spo, Sesion.Emp_Cnx, "spCatProductoTerr_Combo", ref cmbPrd);
        }
        private void Guardar(bool gen_contrato)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!validarFecha())
                {
                    RadTabStrip1.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                    dpFecha.Focus();
                    return;
                }
                if (!validarCamposDetalle())
                {
                    RadTabStrip1.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                    return;
                }

                if (rgDetalles.Items.Count == 0)
                {
                    Alerta("Aún no se han capturado partidas");
                    return;
                }
                else
                {
                    if (rgDetalles.Items.Count > 0 && dt_detalles == null || dt_detalles.Rows.Count == 0)
                    {
                        Alerta("La sesión ha caducado, favor de ingresar nuevamente");
                        Response.Redirect("inicio.aspx");
                        return;
                    }
                }


                if (string.IsNullOrEmpty(txtSub.Text))
                {
                    Alerta("El total de la remisión no puede ser cero");
                    return;
                }
                foreach (DataRow row in dt_cuenta.Rows)
                { //solo se checa inventario si NO es Pedido
                    if (
                        tipoDeMovimiento == 1 ||
                        (tipoDeMovimiento == 2 && edicionRemisionDePedido == false) ||
                        (tipoDeMovimiento == 2 && edicionRemisionDePedido == true && dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length == 0) ||
                        (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length == 0)
                       )
                    {
                        int disponible = -1;
                        int invFinal = -1;
                        int asignado = -1;
                        new CN_CapEntradaSalida().ConsultarDisponible(sesion, int.Parse(row["Id_Prd"].ToString()), ref disponible, ref invFinal, ref asignado);
                        // En caso que de sea una edicion y sea un producto que no era parte de un 
                        //pedido (no estaba asignado) verificar el disponible
                        int original = 0;
                        if (tipoDeMovimiento == 2 && dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                            original = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'")[0]["Cantidad"].ToString());

                        if (int.Parse(row["Cantidad"].ToString()) - original > disponible)
                        {// MSG asignado por antiguo sian
                            Alerta("Producto " + row["Id_Prd"].ToString() +
                                " inventario disponible insuficiente, inventario final: " + invFinal.ToString() +
                                ", asignado: " + asignado.ToString() + ", disponible: " + disponible.ToString());
                            return;
                        }
                    }
                    //************* SI es edicion de una remision de pedido, verificar que la cantidad no supere lo del pedido***************
                    if (edicionRemisionDePedido && dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                    {
                        int cantidadEnPedido = int.Parse(dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'")[0]["Cantidad"].ToString());
                        int cantidadEnDt_cuentaOriginal = int.Parse(dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'")[0]["Cantidad"].ToString());
                        if (int.Parse(row["Cantidad"].ToString()) - cantidadEnDt_cuentaOriginal > cantidadEnPedido)
                        {
                            Alerta("No se puede asignar más cantidad de la que se ordenó en el pedido");
                            return;
                        }
                    }
                    //----------------------------------------------------------------------------------------------------------------------
                }
                if (txtTipoId.Value == 60 && valuacion.Visible)
                {
                    if (txtValuacion.Value.HasValue)
                    {
                        List<ValuacionProyectoDetalle> list = new List<ValuacionProyectoDetalle>();
                        ValuacionProyecto valuacionProyecto = new ValuacionProyecto();
                        valuacionProyecto.Id_Emp = sesion.Id_Emp;
                        valuacionProyecto.Id_Cd = sesion.Id_Cd_Ver;
                        valuacionProyecto.Id_Vap = (int)txtValuacion.Value.Value;

                        new CN_CapValuacionProyecto().ConsultarValuacionProyecto(ref valuacionProyecto, sesion.Emp_Cnx);
                        list = valuacionProyecto.ListaProductosValuacionProyecto;

                        double total_valuacion = 0;
                        foreach (DataRow row in dt_detalles.Rows)
                        {
                            foreach (ValuacionProyectoDetalle vpd in list)
                            {
                                total_valuacion += vpd.Vap_Cantidad * vpd.Vap_Costo;
                                if (row["Id_Prd"].ToString() == vpd.Id_Prd.ToString())
                                {
                                    if (vpd.Estatus.ToLower() != "autorizado")
                                    {
                                        RadTabStrip1.Tabs[0].Selected = true;
                                        RadMultiPage1.SelectedIndex = 0;
                                        AlertaFocus("El producto " + row["Id_Prd"] + " no esta autorizado", txtValuacion.ClientID);
                                        return;
                                    }
                                }
                                else
                                {
                                    RadTabStrip1.Tabs[0].Selected = true;
                                    RadMultiPage1.SelectedIndex = 0;
                                    AlertaFocus("El producto no se encontro en la valuación de proyectos capturada", txtValuacion.ClientID);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        RadTabStrip1.Tabs[0].Selected = true;
                        RadMultiPage1.SelectedIndex = 0;
                        AlertaFocus("Por favor capture el código de valuación de proyecto autorizado", txtValuacion.ClientID);
                        return;
                    }
                }

                Remision remision = new Remision();
                Funciones funcion = new Funciones();
                remision.Id_Emp = sesion.Id_Emp;
                remision.Id_Cd = sesion.Id_Cd_Ver;
                remision.Id_Rem = tipoDeMovimiento == 2 ? Id_Rem_Actualiza : -1;
                remision.Rem_ManAut = 1; // manual
                remision.Rem_Tipo = "3"; // 3=remision
                remision.Rem_Fecha = DateTime.Parse(dpFecha.SelectedDate.Value.ToString("dd/MM/yyyy") + " " + funcion.GetLocalDateTime(sesion.Minutos).ToString("HH:mm:ss"));
                remision.Id_Tm = int.Parse(cmbTipoMovimiento.SelectedValue);
                if (tipoDeMovimiento == 3 || (tipoDeMovimiento == 2 && edicionRemisionDePedido))
                    remision.Id_Ped = int.Parse(txtPedido.Text);
                else
                    remision.Id_Ped = -1;
                remision.Id_Cte = Convert.ToInt32(txtClienteId.Value.HasValue ? txtClienteId.Value.Value : -1);
                remision.Id_Ter = int.Parse(cmbTerritorio.SelectedValue);
                remision.Id_Rik = int.Parse(txtRepresentante.Text);
                remision.Id_U = sesion.Id_U;
                remision.Rem_Calle = txtCalle.Text;
                remision.Rem_Numero = txtNumero2.Text;
                remision.Rem_Cp = txtCp.Text;
                remision.Rem_Colonia = txtColonia.Text;
                remision.Rem_Municipio = txtMunicipio.Text;
                remision.Rem_Estado = txtEstado.Text;
                remision.Rem_Rfc = txtRfc.Text;
                remision.Rem_Telefono = txtTelefono2.Text;
                remision.Rem_Contacto = txtContacto.Text;
                remision.Rem_Conducto = txtConducto.Text;
                remision.Rem_Guia = txtGuia2.Text;
                remision.Rem_FechaEntrega = dtpFechaHora.SelectedDate;
                remision.Rem_Nota = txtNota.Text;
                remision.Rem_Subtotal = Convert.ToDouble(txtSub.Text);// subtotal;
                remision.Rem_Iva = Convert.ToDouble(txtIva.Text);// IVA;
                remision.Rem_Total = Convert.ToDouble(txtTotal.Text);// total;
                remision.Rem_Estatus = "C";
                remision.Rem_OrdenCompra = txtOrdenCompra.Text;
                remision.Id_Vap = (int?)txtValuacion.Value;
                remision.Rem_CteCuentaNacional = Id_CuentaNacional;
                remision.Rem_CteCuentaContNacional = NumCuentaContNacional;
                List<RemisionDet> detalles = new List<RemisionDet>();
                RemisionDet remdetalle = new RemisionDet();
                foreach (DataRow row in dt_detalles.Rows)
                {
                    remdetalle = new RemisionDet();
                    remdetalle.Id_Emp = sesion.Id_Emp;
                    remdetalle.Id_Cd = sesion.Id_Cd_Ver;
                    remdetalle.Id_RemDet = int.Parse(row["Id_RemDet"].ToString());
                    remdetalle.Id_Ter = int.Parse(row["Terr"].ToString());
                    remdetalle.Id_Prd = int.Parse(row["Id_Prd"].ToString());
                    remdetalle.Rem_Cant = int.Parse(row["Cantidad"].ToString());
                    remdetalle.Rem_Precio = double.Parse(row["Precio"].ToString());
                    //si es edicion de remision de pedido
                    if (tipoDeMovimiento == 2 && edicionRemisionDePedido == true && dt_cuentaPedido.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                        remdetalle.Ped_Pertenece = true;

                    //si es remision de pedido
                    if (tipoDeMovimiento == 3 && dt_cuentaOriginal.Select("Id_Prd='" + row["Id_Prd"].ToString() + "'").Length > 0)
                        remdetalle.Ped_Pertenece = true;
                    detalles.Add(remdetalle);
                }

                // Evita que se guarde el documento si los totales no coinciden
                if (Session["ListaProductosRemisionEspecial" + Session.SessionID] != null)
                {
                    //if (Session["RemEspecialGuardada" + Session.SessionID].ToString() == "1")
                    //{
                    double totalEspecial = 0;
                    foreach (RemisionDet ncd in (List<RemisionDet>)Session["ListaProductosRemisionEspecial" + Session.SessionID])
                    {
                        totalEspecial += ncd.Rem_Importe;
                    }

                    bool bEncontrados = true;

                    //Buscar que todos los Id´s de productos de la remisión especial estén también en la remisión detalle.
                    foreach (RemisionDet f in (List<RemisionDet>)Session["ListaProductosRemisionEspecial" + Session.SessionID])
                    {
                        bEncontrados = false;
                        for (int m = 0; m < dt_detalles.Rows.Count; m++)
                        {
                            if (dt_detalles.Rows[m]["Id_Prd"].ToString() == f.Id_Prd.ToString())
                            {
                                bEncontrados = true;
                                break;
                            }
                        }
                        if (bEncontrados == false)
                            break;
                    }
                    if (bEncontrados == false)
                    {
                        Alerta("La remisión especial contiene partidas con productos distintos a la remisión original.");
                        return;
                    }


                    //Datos del centro de distribución (Para obtener la tolerancia o diferencia permitida entre totales de fac y fact especial)
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                    // Se indico que solo podía haber diferecia de 90 centavos
                    double TE1 = (Math.Round(txtSub.Value.Value, 2) + Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // Math.Round(totalEspecial, 2) + .90; // se suman 70 centavos al total especial -- modificar si se desea disminuir o aumentar el rango
                    double TE2 = (Math.Round(txtSub.Value.Value, 2) - Math.Round((double)cd.Cd_MargenDiferenciaDocs, 2)); // se restan 70 centavos al total especia


                    if (TE1 < Math.Round(totalEspecial, 2) || TE2 > Math.Round(totalEspecial, 2))
                    {
                        Alerta("Los montos de la remision y la remision especial tienen una diferencia considerable, favor de revisarlos.");
                        return;
                    }
                    //}
                }

                int verificador = -1;
                int Id_Rem = 0;
                bool tipoMovimento = false;
                string mensaje = "";
                if (txtTipoId.Value == 54 && txtClienteId.Value == 100)
                {
                    try
                    {
                        new CN_CapRemision().GuardarRemision(remision, detalles, sesion, ref verificador, tipoDeMovimiento == 2, gen_contrato, ref Id_Rem, ref tipoMovimento, ref mensaje);
                        EnviarCorreo(Id_Rem);
                        //Alerta("Se genero el movimiento de transferencia #" + Id_Rem + " de almacen a cedis, pendiente de autorización.");
                        //return;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    try
                    {
                        new CN_CapRemision().GuardarRemision(remision, detalles, sesion, ref verificador, tipoDeMovimiento == 2, gen_contrato, ref Id_Rem, ref tipoMovimento, ref mensaje);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("saldo_insuficiente") || ex.Message.Contains("error"))
                        {
                            Alerta(ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            return;
                        }
                        else
                        {
                            if (ex.Message.Contains("no cuenta con inventario suficiente"))
                            {
                                Alerta(ex.ToString());
                            }
                            else
                            {
                                throw ex;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensaje))
                {
                    Alerta(mensaje);
                    return;
                }
                else
                {//SI GUARDÓ BIEN LA REMISION:
                    //Guardar los datos de los productos de remision especial
                    if (Session["ListaProductosRemisionEspecial" + Session.SessionID] != null)
                    {
                        if (Session["RemEspecialGuardada" + Session.SessionID].ToString() == "1") //guarda solo si hizo clic en guardar en pantalla de RemisionEspecial.
                        {
                            FacturaEspecial facturaEsp = new FacturaEspecial();
                            facturaEsp.Id_Emp = remision.Id_Emp;
                            facturaEsp.Id_Cd = remision.Id_Cd;
                            facturaEsp.Id_Fac = verificador;
                            facturaEsp.Id_Ter = Convert.ToInt32(remision.Id_Ter);
                            facturaEsp.FacEsp_Fecha = remision.Rem_Fecha;
                            facturaEsp.FacEsp_Importe = Convert.ToDouble(remision.Rem_Total);
                            facturaEsp.FacEsp_SubTotal = Convert.ToDouble(remision.Rem_Subtotal);
                            facturaEsp.FacEsp_ImporteIva = Convert.ToDouble(remision.Rem_Iva);
                            facturaEsp.FacEsp_Total = Convert.ToDouble(remision.Rem_Total);
                            List<RemisionDet> listaProductosRemisionEspecial = (List<RemisionDet>)Session["ListaProductosRemisionEspecial" + Session.SessionID];
                            new CN_CatClienteProd().ModificarRemisionEspecial(ref facturaEsp, ref listaProductosRemisionEspecial, sesion.Emp_Cnx, ref verificador, string.IsNullOrEmpty(this.hiddenId.Value) ? 0 : 1);
                        }
                    }
                    //EL sp de insertar remision cambia el estatus del pedido en caso de que sea remision de pedido
                    if (tipoMovimento)
                    {
                        Session["PreguntarImpresion" + Session.SessionID] = Id_Rem;
                    }
                    if (HF_VI.Value == "true")
                    {
                        Session["PreguntarImpresionVIRem" + Session.SessionID] = Id_Rem;
                    }
                    new CN_Rendimientos().InsertarRendimientosRemisiones(sesion, sesion.Emp_Cnx, Session.SessionID, ref Id_Rem, ref verificador);
                    RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));
                    Nuevo();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("deadlocked"))
                {
                    Alerta("El servidor esta tardando en responder, por favor de click en guardar nuevamente");
                }
                else
                {
                    throw ex;
                }
            }
        }
        private void EnviarCorreo(int Id_Rem)
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

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de una remisión de transferencia de almacen con el folio  " + Id_Rem);
                cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Centro de distribución:  " + session.Id_Cd_Ver + " - " + session.Cd_Nombre);
                cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Solicitó : " + session.Id_U + " - " + session.U_Nombre);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "CapRemisiones.aspx?tdm=" + 2 + "&Id_Rem=" + Id_Rem + "&Accion=2&email=1&PermisoGuardar=false&PermisoModificar=false&PermisoEliminar=false&PermisoImprimir=false'" + ">");
                cuerpo_correo.Append("Solicitud de autorización de transferencia de almacen a Cedis</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                //sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                m.To.Add(new MailAddress(configuracion.Mail_TransferenciasCedis));
                m.Subject = "Solicitud de autorización de transferencia de almacen a cedis #" + Id_Rem + " del centro " + session.Id_Cd_Ver;
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
                }
                catch (Exception)
                {
                    //CambiarEstatus(ordCompra, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Los datos se guardaron correctamente", "')"));
                rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cmbTerritorio_indiceCambiado()
        {
            try
            { //consultar representante del territorio seleccionado
                //if (int.Parse(cmbTerritorio.SelectedValue) > 0)
                //{
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Territorios> listaTerritorios = new List<Territorios>();
                new CN_CatCliente().ConsultaTodosTerritoriosDelCliente(Convert.ToInt32(txtClienteId.Value == null ? 0 : txtClienteId.Value.Value), sesion, ref listaTerritorios);

                cmbTerritorio.DataTextField = "Descripcion";
                cmbTerritorio.DataValueField = "Id_Ter";
                cmbTerritorio.DataSource = listaTerritorios;
                cmbTerritorio.DataBind();
                if (cmbTerritorio.Items.Count > 1)
                {

                    cmbTerritorio.SelectedIndex = 1;
                    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    txtTerritorioId.Text = cmbTerritorio.Items[1].Value;

                    CN_CatTerritorios territorio = new CN_CatTerritorios();
                    Territorios ter = new Territorios();
                    ter.Id_Emp = sesion.Id_Emp;
                    ter.Id_Cd = sesion.Id_Cd_Ver;
                    ter.Id_Ter = Convert.ToInt32(cmbTerritorio.Items[1].Value);
                    territorio.ConsultaTerritoriosCombo(ref ter, sesion.Emp_Cnx);
                    txtRepresentante.Text = ter.Id_Rik.ToString();
                    txtRepresentanteStr.Text = ter.Rik_Nombre;
                }
                if (cmbTerritorio.Items.Count < 2)
                    Alerta("El cliente no tiene territorios asociados");//verificarmensaje   
                if (IsPostBack)
                {
                    txtTerritorioId.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void crear_dt_cuenta()
        {
            dt_cuenta = new DataTable();
            dt_cuenta.Columns.Add("Id_Prd");
            dt_cuenta.Columns.Add("Cantidad");
        }
        private void crear_dt_cuentaPedido()
        {
            dt_cuentaPedido = new DataTable();
            dt_cuentaPedido.Columns.Add("Id_Detalle");
            dt_cuentaPedido.Columns.Add("Id_Prd");
            dt_cuentaPedido.Columns.Add("Cantidad");
        }
        private void Nuevo()
        {
            LimpiarCampos1();
            ReiniciarVariables();
            txtTipoId.Text = "";
            cmbTipoMovimiento.SelectedValue = "-1";
            txtClienteId.Text = "";
            txtCliente.Text = "";
            rgDetalles.Rebind();
            txtFolio.Text = MaximoId();
            CalcularTotales();
            //txtSub.Text = subtotal.ToString();
            //txtIva.Text = IVA.ToString();
            //txtTotal.Text = total.ToString();
            RadTabStrip1.SelectedIndex = 0;
            RadMultiPage1.SelectedIndex = 0;
        }
        private bool validarCamposDetalle()
        {

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            session = (Sesion)Session["Sesion" + Session.SessionID];

            if (cmbTipoMovimiento.SelectedValue == "" || cmbTipoMovimiento.SelectedValue == "-1")
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
                return false;
            }
            if (!txtClienteId.Value.HasValue)
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
                return false;
            }
            if (cmbTerritorio.SelectedValue == "" || cmbTerritorio.SelectedValue == "-1")
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
                return false;
            }
            if (tipoDeMovimiento == 3 && txtPedido.Text == "")
            {
                Alerta("Favor de capturar todos los campos de la pestaña datos generales");
            }
            return true;
        }
        private void ReiniciarVariables()
        {
            crearDT();
            crear_dt_cuenta();
            //subtotal = 0;
            //IVA = 0;
            //total = 0;
            Tm_ReqSpo = false;
            id_detalle = 0;
            Id_Rem_Actualiza = -1;
            edicionRemisionDePedido = false;
        }
        private void cargarProductosSpo(ref RadComboBox combo_a_llenar)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatProducto_ConsultaSpoLista", ref combo_a_llenar);
                combo_a_llenar.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cacharMsgBaseDatos(Exception exception, string metodoCachaExcepcion)
        {
            string[] Msgs =
            {     //spCapRemisionDet_Insertar
                    "No puede asignar una cantidad 0"
                    ,"No puede asignar precio 0"
                    ,"inventario disponible insuficiente, inventario final:"
                    ,"Se sobrepasa la cantidad disponible a remisionar para este pedido - producto"
                };
            bool msgConosido = false;
            foreach (string men in Msgs)
            {
                if (exception.Message.Contains(men))
                    msgConosido = true;
            }

            if (msgConosido)
            {
                Alerta(exception.Message);
            }
            else
            {
                ErrorManager(exception, metodoCachaExcepcion);
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Session["ListaProductosRemisionEspecial" + Session.SessionID] = null;
            Session["RemEspecialGuardada" + Session.SessionID] = "0";

            _Accion = Convert.ToInt32(Request.QueryString["Accion"]);

            if (_Accion == 2)
            {
                BtnAutorizar.Visible = true;
                BtnRechazar.Visible = true;
                ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("RemEspecial")).Visible = false;
            }
            Nuevo();
            switch (Request.QueryString["tdm"])
            {
                case "1":
                    //NUEVO remision
                    tipoDeMovimiento = 1;
                    txtFolio.Text = MaximoId();
                    if (Session["PedidoVI" + Session.SessionID] != null)
                    {
                        Session["PedidoVI" + Session.SessionID] = null;
                        HF_VI.Value = "true";
                    }
                    break;
                #region EDICION de remision
                case "2":
                    //EDICION de remision
                    tipoDeMovimiento = 2;
                    int Id_Rem = -1;
                    int.TryParse(Request.QueryString["Id_Rem"], out Id_Rem);
                    Remision remision = new Remision();
                    new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 0);

                    if (remision.Id_Tm == 60)
                    {
                        if (remision.Rem_Total >= Convert.ToDouble(HiddenField2.Value))
                            valuacion.Visible = true;
                        else
                            valuacion.Visible = false;
                    }
                    Id_Rem_Actualiza = remision.Id_Rem;
                    dpFecha.Enabled = false;
                    cmbTipoMovimiento.Enabled = false;
                    txtTipoId.Enabled = false;
                    txtCliente.Enabled = false;
                    txtClienteId.Enabled = false;
                    cmbTerritorio.Enabled = false;
                    txtTerritorioId.Enabled = false;

                    txtFolio.Text = remision.Id_Rem.ToString();
                    cmbTipoMovimiento.SelectedValue = remision.Id_Tm.ToString();
                    cmbTipoMovimiento.Text = cmbTipoMovimiento.FindItemByValue(remision.Id_Tm.ToString()).Text;
                    txtTipoId.Text = remision.Id_Tm.ToString();
                    txtCliente.Text = "";
                    txtClienteId.Text = remision.Id_Cte.ToString();
                    HiddenCteCuentaNacional.Value = remision.Rem_CteCuentaNacional.ToString();
                    cmbCliente_indiceCambia();
                    txtHora.Text = remision.Rem_FechaHr.Value.ToString("H:mm:ss");



                    /*Información de Amortización*/

                    Fol_Amortizacion_Remision.Text = remision.Id_Rem.ToString();
                    NCliente_Amortizacion_Remision.Text = remision.Id_Cte.ToString();
                    NTerritorio_Amortizacion_Remision.Text = remision.Id_Ter.ToString();

                    inversion.InnerHtml = remision.Rem_ResumenInversion;
                    tablaamortizacion.InnerHtml = remision.Rem_TablaAmortizacion;
                    kardexmovimiento.InnerHtml = remision.Rem_KardexAmortizacion;
                    /*Información de Amortización*/


                    cmbTerritorio_indiceCambiado();

                    cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(remision.Id_Ter.ToString());
                    cmbTerritorio.Text = cmbTerritorio.FindItemByValue(remision.Id_Ter.ToString()).Text;
                    string DescrTer = cmbTerritorio.Text;
                    txtTerritorioId.Text = remision.Id_Ter.ToString();

                    txtRepresentanteStr.Text = remision.Rik_Nombre;
                    txtRepresentante.Text = remision.Id_Rik.ToString();
                    txtValuacion.DbValue = remision.Id_Vap;
                    dpFecha.SelectedDate = remision.Rem_Fecha;
                    txtPedido.Text = remision.Id_Ped == -1 ? "" : remision.Id_Ped.ToString();
                    if (remision.Id_Ped > 0)
                    {
                        edicionRemisionDePedido = true;
                        crear_dt_cuentaPedido();
                        DataTable tabla = new DataTable();
                        tabla.Columns.Add("Id_PedDet");
                        tabla.Columns.Add("Id_Ter");
                        tabla.Columns.Add("Ter_Nombre");
                        tabla.Columns.Add("Id_Prd");
                        tabla.Columns.Add("Prd_Descripcion");
                        tabla.Columns.Add("Prd_Presentacion");
                        tabla.Columns.Add("Prd_Unidad");
                        tabla.Columns.Add("Prd_Precio");
                        tabla.Columns.Add("Prd_Cantidad");
                        tabla.Columns.Add("Prd_Importe");
                        tabla.Columns.Add("Id_Rem");
                        tabla.DefaultView.Sort = "Id_PedDet";
                        Pedido ped = new Pedido();
                        ped.Id_Emp = sesion.Id_Emp;
                        ped.Id_Cd = sesion.Id_Cd_Ver;
                        ped.Id_Ped = remision.Id_Ped;
                        new CN_CapPedido().ConsultaPedidoDetDisp(ped, ref tabla, null, sesion.Emp_Cnx);
                        ArrayList lista = new ArrayList();
                        foreach (DataRow row in tabla.Rows)
                        {
                            meterPedido_aDT_cuentaPedido(int.Parse(row["Id_PedDet"].ToString()), int.Parse(row["Id_Ter"].ToString()), int.Parse(row["Id_Prd"].ToString()),
                                                row["Prd_Descripcion"].ToString(), int.Parse(row["Prd_Cantidad"].ToString()), double.Parse(row["Prd_Precio"].ToString()),
                                                double.Parse(row["Prd_Importe"].ToString()));
                        }
                    }
                    txtCalle.Text = remision.Rem_Calle;
                    txtNumero2.Text = remision.Rem_Numero;
                    txtCp.Text = remision.Rem_Cp;
                    txtColonia.Text = remision.Rem_Colonia;
                    txtMunicipio.Text = remision.Rem_Municipio;
                    txtEstado.Text = remision.Rem_Estado;
                    txtRfc.Text = remision.Rem_Rfc;
                    txtTelefono2.Text = remision.Rem_Telefono;
                    txtContacto.Text = remision.Rem_Contacto;
                    txtConducto.Text = remision.Rem_Conducto;
                    txtGuia2.Text = remision.Rem_Guia;
                    dtpFechaHora.SelectedDate = remision.Rem_FechaEntrega;
                    txtNota.Text = remision.Rem_Nota;
                    txtOrdenCompra.Text = remision.Rem_OrdenCompra.ToString();

                    List<RemisionDet> detalles = new List<RemisionDet>();
                    new CN_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);
                    foreach (RemisionDet detalle in detalles)
                    {
                        meterPedido_aDTs(detalle.Id_RemDet, detalle.Id_Ter == null ? -1 : int.Parse(detalle.Id_Ter.ToString()),
                            detalle.Id_Prd, detalle.Prd_Descripcion, detalle.Rem_Cant, detalle.Rem_Precio == null ? 0 : double.Parse(detalle.Rem_Precio.ToString()),
                                        detalle.Rem_Cant * detalle.Rem_Precio, detalle.Ter_Nombre);
                    }
                    CalcularTotales();
                    //Sacar copia de los DT originales para compararlo y no pasar de esa cantidad                    
                    dt_cuentaOriginal = dt_cuenta.Clone();
                    dt_cuentaOriginal.Merge(dt_cuenta);

                    CargarEspecial(Id_Rem, sesion, remision.Id_Cte);
                    break;
                #endregion
                #region REMISION DE PEDIDO
                case "3":
                    //REMISION DE PEDIDO
                    tipoDeMovimiento = 3;
                    int PedRem = -1;
                    int.TryParse(Request.QueryString["PedRem"], out PedRem);
                    Pedido pedido = new Pedido();
                    pedido.Id_Emp = sesion.Id_Emp;
                    pedido.Id_Cd = sesion.Id_Cd_Ver;
                    pedido.Id_Ped = PedRem;
                    pedido.Filtro_Doc = "R";
                    new CN_CapPedido().ConsultaPedido(ref pedido, sesion.Emp_Cnx); //new CN_CapPedido().ConsultaPedidoDet(pedido, ref tabla, sesion.Emp_Cnx);
                    if (pedido.Id_Cte == 0 && pedido.Ped_Importe == 0 && pedido.Id_Ter == 0)
                    {  //cerrar ventana
                        RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "No existe el pedido a remisionar", "')"));
                        return;
                    }
                    else
                    {

                        if (Session["PedidoVI" + Session.SessionID] != null)
                        {
                            Session["PedidoVI" + Session.SessionID] = null;
                            HF_VI.Value = "true";
                        }

                        List<PedidoDet> List = GetList(PedRem, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                        bool bTieneAsignado = false;

                        foreach (var PedidoDet in List)
                            if (PedidoDet.Prd_Asig > 0)
                                bTieneAsignado = true;


                        if (bTieneAsignado)
                        {
                            //si existe pedido
                            txtCliente.Enabled = false;
                            txtClienteId.Enabled = false;
                            cmbTerritorio.Enabled = false;
                            txtTerritorioId.Enabled = false;
                            dtpFechaHora.Enabled = false;

                            txtFolio.Text = MaximoId();
                            txtCliente.Text = pedido.Cte_NomComercial;
                            txtClienteId.Text = pedido.Id_Cte.ToString();
                            cmbCliente_indiceCambia();
                            txtOrdenCompra.Text = pedido.Ped_OrdenCompra == "" ? pedido.ReqAcys : pedido.Ped_OrdenCompra;


                            cmbTerritorio_indiceCambiado();
                            try
                            {
                                cmbTerritorio.SelectedIndex = cmbTerritorio.FindItemIndexByValue(pedido.Id_Ter.ToString());
                                cmbTerritorio.Text = cmbTerritorio.FindItemByValue(pedido.Id_Ter.ToString()).Text;
                                txtTerritorioId.Text = pedido.Id_Ter.ToString();
                            }
                            catch
                            {


                            }

                            txtRepresentanteStr.Text = pedido.Rik_Nombre;
                            txtRepresentante.Text = pedido.Id_Rik.ToString();
                            txtConducto.Text = pedido.Ped_Solicito;
                            txtPedido.Text = PedRem.ToString();

                            txtCalle.Text = pedido.Ped_ConsignadoCalle;
                            txtNumero2.Text = pedido.Ped_ConsignadoNo;
                            txtCp.Text = pedido.Ped_ConsignadoCp;
                            txtMunicipio.Text = pedido.Ped_ConsignadoMunicipio;
                            txtEstado.Text = pedido.Ped_ConsignadoEstado;
                            txtColonia.Text = pedido.Ped_ConsignadoColonia;
                            txtNota.Text = pedido.Ped_Comentarios;




                            //cargar detalles
                            DataTable tabla = new DataTable();
                            tabla.Columns.Add("Id_PedDet");
                            tabla.Columns.Add("Id_Ter");
                            tabla.Columns.Add("Ter_Nombre");
                            tabla.Columns.Add("Id_Prd");
                            tabla.Columns.Add("Prd_Descripcion");
                            tabla.Columns.Add("Prd_Presentacion");
                            tabla.Columns.Add("Prd_Unidad");
                            tabla.Columns.Add("Prd_Precio");
                            tabla.Columns.Add("Prd_Cantidad");//en lugar de la cantidad, cargar "disponible de remision"
                            tabla.Columns.Add("Prd_Importe");
                            tabla.Columns.Add("Id_Rem");
                            tabla.DefaultView.Sort = "Id_PedDet";
                            new CN_CapPedido().ConsultaPedidoDetDisp(pedido, ref tabla, null, sesion.Emp_Cnx);

                            foreach (DataRow row in tabla.Rows)
                            {
                                meterPedido_aDTs(int.Parse(row["Id_PedDet"].ToString()), int.Parse(row["Id_Ter"].ToString()), int.Parse(row["Id_Prd"].ToString()),
                                                    row["Prd_Descripcion"].ToString(), int.Parse(row["Prd_Cantidad"].ToString()), double.Parse(row["Prd_Precio"].ToString()),
                                                    double.Parse(row["Prd_Importe"].ToString()), row["Ter_Nombre"].ToString());
                            }
                            //Sacar copia de los DT originales para compararlo y no pasar de esa cantidad                        
                            dt_cuentaOriginal = dt_cuenta.Clone();
                            dt_cuentaOriginal.Merge(dt_cuenta);
                        }
                        else
                        {
                            //cerrar ventana
                            RadAjaxManager1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "No es posible remisionar el pedido ya que no tiene asignación en ninguna partida", "')"));
                            return;
                        }


                    }
                    CalcularTotales();
                    break;
                #endregion
                default:
                    break;
            }
            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            botones_radtoolbar();//esconde o muestra los botones grabar , nuevo , imprimir , etc segun los permisos           
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

        private void botones_radtoolbar()
        {
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("save")).Visible = _PermisoGuardar;//actualizacionDocumento ? _PermisoModificar : true;
            ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("new")).Visible = tipoDeMovimiento == 1;
        }
        private void meterPedido_aDTs(int Id_Detalle, int territorio, int Id_Prd, string descripcion, int cantidad, double precio, double importe, string Ter_Nombre)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                //verificar si el articulo no es sistema de propietarios
                Producto producto = new Producto();
                producto.Id_Cte = txtClienteId.Value.HasValue ? Convert.ToInt32(txtClienteId.Value.Value) : 0;
                new CN_CatProducto().ConsultaProducto(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Id_Prd);

                if (!((bool)producto.Prd_AparatoSisProp))
                {//este articulo no es spo y no se puede seleccionar movimiento 60
                    hayProductosNoSpo = true;
                }

                //agregar al dt
                DataRow[] editable_dr;
                if (dt_cuenta.Select("Id_Prd='" + Id_Prd + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                {
                    dt_cuenta.Rows.Add(new object[] { Id_Prd, cantidad });
                }

                if (cantidad > 0)
                {
                    double iva_cd = 0;
                    new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);
                    dt_detalles.Rows.Add(new object[] { Id_Detalle, territorio, Id_Prd, descripcion, cantidad, precio, importe, Ter_Nombre });
                    id_detalle = ++Id_Detalle;
                    //CalcularTotales();
                    //subtotal += cantidad * precio;
                    //IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                    //total = subtotal + IVA;
                    //txtSub.Text = subtotal.ToString();
                    //txtIva.Text = IVA.ToString();
                    //txtTotal.Text = total.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CalcularTotales()
        {
            try
            {
                double importeTotal = 0;
                double iva_cd = 0;
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                new CN_CatCentroDistribucion().ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);

                if (dt_detalles.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_detalles.Rows.Count; i++)
                    {
                        importeTotal += Convert.ToDouble(dt_detalles.Rows[i]["Importe"].ToString());
                    }
                }

                double subtotal = importeTotal;
                double IVA = double.Parse((subtotal * (iva_cd / 100)).ToString());
                double total = subtotal + IVA;
                txtSub.Text = subtotal.ToString();
                txtIva.Text = IVA.ToString();
                txtTotal.Text = total.ToString();
                Session["fTotal" + Session.SessionID] = total.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void meterPedido_aDT_cuentaPedido(int Id_Detalle, int territorio, int Id_Prd, string descripcion, int cantidad, double precio, double importe)
        {
            try
            { //agregar al dt
                DataRow[] editable_dr;
                if (dt_cuentaPedido.Select("Id_Prd='" + Id_Prd + "' and Id_Detalle='" + Id_Detalle + "'").Length > 0)
                {
                    editable_dr = dt_cuenta.Select("Id_Prd='" + Id_Prd + "'");
                    editable_dr[0].BeginEdit();
                    editable_dr[0]["Cantidad"] = int.Parse(editable_dr[0]["Cantidad"].ToString()) + cantidad;
                    editable_dr[0].AcceptChanges();
                }
                else
                    dt_cuentaPedido.Rows.Add(new object[] { Id_Detalle, Id_Prd, cantidad });
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CerrarVentana()
        {
            try
            {
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
        private void CargarEspecial(int Id_Ncr, Sesion sesion, int Id_Cte)
        {
            //- Especial
            List<RemisionDet> listaProdRemisionEspecialFinal = new List<RemisionDet>();
            new CN_CapRemision().ConsultaRemisionEspecialDetalle(ref listaProdRemisionEspecialFinal
                , sesion.Emp_Cnx
                , sesion.Id_Emp
                , sesion.Id_Cd_Ver
                , Id_Ncr
                , Id_Cte);

            if (listaProdRemisionEspecialFinal.Count > 0)
            {
                if (listaProdRemisionEspecialFinal[0].bTieneEspecial == 1)
                {
                    Session["ListaProductosRemisionEspecial" + Session.SessionID] = listaProdRemisionEspecialFinal;
                    Session["RemEspecialGuardada" + Session.SessionID] = 1;
                }
            }
            //-
        }
        #endregion
        #region ErrorManager
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 340, 150);");
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
        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }
    }
}