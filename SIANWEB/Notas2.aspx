<%@ Page Title="Notas de cargo" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="Notas2.aspx.cs" Inherits="SIANWEB.Notas2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .MyImageButton
        {
            cursor: hand;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }
            function ClienteSeleccionado() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('cliente');
            }
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';
            function ClientTabSelecting(sender, args) {
                if ('<%= FechaEnable %>' == '1') {
                    continuarAccion = _ValidarFechaEnPeriodo();
                    args.set_cancel(!continuarAccion);
                }
                else {
                    continuarAccion = true;
                }

                args.set_cancel(!continuarAccion);
                if (continuarAccion) {
                    var Ser = $find('<%= txtFolio.ClientID %>');
                    var Mov = $find('<%= txtMov.ClientID %>');
                    var Cte = $find('<%= txtCliente.ClientID %>');
                    var Ter = $find('<%= txtTerritorio.ClientID %>');
                    var Ref = $find('<%= txtReferencia.ClientID %>');
                    if (Ser.get_value() == "") {
                        radalert('Por favor seleccione la serie del consecutivo antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Mov.get_value() == "") {
                        radalert('Por favor seleccione el tipo de movimiento antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Cte.get_value() == "") {
                        radalert('Por favor capture el cliente antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Ter.get_value() == "") {
                        radalert('Por favor capture el territorio antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Ref.get_value() == "") {
                        radalert('Por favor capture la referencia antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                }
            }
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Nota de cargo
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesOrdenCompra() {
                //debugger;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var txtTerritorioPartidaClientID = '';
            var lbltxtTerritorioPartidaClientID = '';
            var txtRepresentantePartidaClientID = '';
            var lblTxtRepresentantePartidaClientID = '';
            var txtId_PrdClientID = '';
            var lbl_cmbProductoClientID = '';
            var txtNca_ImporteClientID = '';
            var lbl_txtNca_ImporteClientID = '';

            //Validación del formulario de insercion/edición de registro en un RadGrid.
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            function ValidaFormEdit(accion) {
                var continuarAccion = true;
                //debugger;
                //obtener controles de formulario de inserión/edición de Grid
                var lbltxtTerritorioPartida = document.getElementById(lbltxtTerritorioPartidaClientID);
                var txtTerritorioPartida = $find(txtTerritorioPartidaClientID);
                var lblTxtRepresentantePartida = document.getElementById(lblTxtRepresentantePartidaClientID);
                var txtRepresentantePartida = $find(txtRepresentantePartidaClientID);
                var lbl_cmbProducto = document.getElementById(lbl_cmbProductoClientID);
                var txtId_Prd = $find();
                var lbl_txtNca_Importe = document.getElementById(lbl_txtNca_ImporteClientID);
                var txtNca_Importe = $find(txtNca_ImporteClientID);

                //Limpiar contenedores de mensaje de validación
                lbltxtTerritorioPartida.innerHTML = '';
                lblTxtRepresentantePartida.innerHTML = '';
                lbl_cmbProducto.innerHTML = '';
                lbl_txtNca_Importe.innerHTML = '';

                if (txtTerritorioPartida != null)
                    if (txtTerritorioPartida.get_textBoxValue() == '') {
                        continuarAccion = false;
                    }

                if (txtRepresentantePartida != null)
                    if (txtRepresentantePartida.get_textBoxValue() == '') {
                        continuarAccion = false;
                    }

                if (txtId_Prd != null)
                    if (txtId_Prd.get_textBoxValue() == '') {
                        continuarAccion = false;
                    }

                if (txtNca_Importe != null)
                    if (txtNca_Importe.get_textBoxValue() == '') {
                        continuarAccion = false;
                    }

                if (continuarAccion == false) {
                    var alertaRequedridosGrig = radalert('Todos los datos son requeridos', 330, 150, tituloMensajes);
                }
                return continuarAccion
            }

            function ValidacionesEspeciales() {
                var conntinuar = true;

                var hiddenHD_PanelVisible = document.getElementById('<%= HD_PanelVisible.ClientID %>');
                if (hiddenHD_PanelVisible.value == 'b') {
                    var lblVal_txtBanco = document.getElementById('<%= lblVal_txtBanco.ClientID %>');
                    var txtBanco = $find('<%= txtBanco.ClientID %>');

                    if (txtBanco != null)
                        if (txtBanco.get_textBoxValue() == '') {
                            lblVal_txtBanco.innerHTML = '*Requerido';
                            conntinuar = false;
                        }
                        else {
                            lblVal_txtBanco.innerHTML = '';
                        }
                }
                else {
                    if (hiddenHD_PanelVisible.value == 'c') {
                        var lblVal_txtCuentaContable = document.getElementById('<%= lblVal_txtCuentaContable.ClientID %>');
                        var txtCuentaContable = $find('<%= txtCuentaContable.ClientID %>');

                        if (txtCuentaContable != null)
                            if (txtCuentaContable.get_textBoxValue() == '') {
                                lblVal_txtCuentaContable.innerHTML = '*Requerido';
                                conntinuar = false;
                            }
                            else {
                                lblVal_txtCuentaContable.innerHTML = '';
                            }
                    }
                }
                return conntinuar;
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }
                switch (button.get_value()) {
                    case 'new':
                        LimpiarControlesOrdenCompra();

                        //select tab datos generales
                        var RadTabStripPrincipal = $find('<%= RadTabStrip1.ClientID %>');
                        RadTabStripPrincipal.get_allTabs()[0].select();

                        //registro nuevo -> se limpia bandera de actualización
                        var hiddenId = document.getElementById('<%= hiddenId.ClientID %>');
                        hiddenId.value = '';

                        var fechaActual = new Date('<%= ActualAnio %>', '<%= ActualMes %>', '<%= ActualDia %>');
                        var txtFecha = $find('<%= txtFecha.ClientID %>');
                        txtFecha.set_selectedDate(fechaActual);

                        //establecer consecitivo de folio de proveedor
                        var txtFolio = $find('<%= txtFolio.ClientID %>');
                        txtFolio.set_value('<%= Valor %>');
                        txtFolio.disable();

                        continuarAccion = true;
                        break;

                    case 'save':
                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        radTabStrip.get_allTabs()[0].select();

                        continuarAccion = ValidacionesEspeciales();

                        if (continuarAccion == true) {
                            continuarAccion = _ValidarFechaEnPeriodo();
                        }
                        break;

                    case 'NotaEspecial':
                        var radGrid = $find('<%= rgNotaCargoDet.ClientID %>');
                        var MasterTable = radGrid.get_masterTableView();
                        var length = MasterTable.get_dataItems().length;

                        if (length != '' && length > 0) {
                            AbrirVentana_NotaCargoEspecial();
                            continuarAccion = false;
                        }
                        else {
                            var alertaFEsp = radalert('No se ha agregado ningún producto', 330, 150, tituloMensajes);
                        }
                        break;
                }
                if (continuarAccion == true) {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                }
                args.set_cancel(!continuarAccion);
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= txtFecha.ClientID %>');
                return txtFecha._dateInput;
            }

            function LimpiarBanderaRebind_notaEspecial(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind_notaEspecial('0');
            }

            function ActivarBanderaRebind_notaEspecial() {
                //debugger;
                ModificaBanderaRebind_notaEspecial('1');
            }

            function ModificaBanderaRebind_notaEspecial(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind_notaEspecial.ClientID %>');
                HD_GridRebind.value = valor;
            }
            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de Nota de cargo especial
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_NotaCargoEspecial() {
                //debugger;               
                var txtSubtotal2 = $find('<%= txtSubtotal2.ClientID %>');
                var txtIva2 = $find('<%= txtIva2.ClientID %>');
                var txtTotal2 = $find('<%= txtTotal2.ClientID %>');
                var txtCliente = $find('<%= txtCliente.ClientID %>');
                var Folio = $find('<%= txtFolio.ClientID %>');

                var oWnd = radopen("CapNotaCargo_Especial.aspx?Id_Cte="
                  + txtCliente.get_value()
                  + "&Nca_ImporteTotal=" + txtTotal2.get_value()
                  + "&IVA_Nca=" + txtIva2.get_value()
                  + "&Folio=" + Folio.get_value() //clave de la factura
                  + "&Modificar=" + '<%= HabilitarGuardar %>'
                , "AbrirVentana_NotaCargoEspecial");
                oWnd.center();
            }
            //calcular totales 2
            function CalcularTotales(sender, args) {
                //debugger;
                var txtSubtotal = $find('<%= txtSubtotal2.ClientID %>');
                var txtIva = $find('<%= txtIva2.ClientID %>');
                var txtTotal = $find('<%= txtTotal2.ClientID %>');
                var HD_IVAfacturacion = document.getElementById('<%= HD_IVAfacturacion.ClientID %>');
                var HD_IVAfacturacionValue = HD_IVAfacturacion.value == '' ? 0 : parseFloat(HD_IVAfacturacion.value);
                var Val_txtSubtotal = 0;
                var Val_txtIva = 0;
                if (txtSubtotal.get_textBoxValue() != '') {
                    Val_txtSubtotal = parseFloat(txtSubtotal.get_textBoxValue());
                }
                if (txtIva != null) {
                    if (sender._clientID != txtIva._clientID) {

                        if (txtIva.get_textBoxValue() != '') {
                            if (HD_IVAfacturacionValue > 0) {
                                txtIva.set_value(parseFloat(txtSubtotal.get_textBoxValue()) * (HD_IVAfacturacionValue / 100));
                            }
                            Val_txtIva = parseFloat(txtIva.get_textBoxValue());
                        }
                    }
                }
                if (txtIva != null) {
                    if (txtIva.get_textBoxValue() != '') {
                        Val_txtIva = parseFloat(txtIva.get_textBoxValue());
                    }
                }
                txtTotal.set_value(Val_txtSubtotal + Val_txtIva);
            }
            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                    function () {
                        CloseAndRebind();
                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //cuando el campo de texto pirde el foco
            function txtMov_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbMov.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbMov_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtMov.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtBanco_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbBanco.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbBanco_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtBanco.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtCliente_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            var txtId_Prd;
            var cmbProducto;

            function txtId_Prd_OnLoad(sender, args) {
                txtId_Prd = sender;
            }

            function cmbProducto_OnLoad(sender, args) {
                cmbProducto = sender;
            }

            //cuando el campo de texto de edición del Grid de clave de producto pirde el foco
            function txtId_Prd_OnBlur(sender, args) {
                //debugger; 
                OnBlur(sender, cmbProducto);
            }

            //cuando el combo de edición del Grid de producto cambia de indice
            function cmbProducto_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtId_Prd);
                if (txtNca_Importe != null) {
                    txtNca_Importe.focus();
                }
            }

            //Para el combo de Territorios dentro del Grid
            var txtTerritorioPartida;
            var cmbTerritorioPartida;

            function txtTerritorioPartida_OnLoad(sender, args) {
                txtTerritorioPartida = sender;
            }

            function cmbTerritorioPartida_OnLoad(sender, args) {
                cmbTerritorioPartida = sender;
            }

            //cuando el campo de texto de edición del Grid de TerritorioPartida pirde el foco
            function txtTerritorioPartida_OnBlur(sender, args) {
                //debugger; 
                OnBlur(sender, cmbTerritorioPartida);
            }

            //cuando el combo de edición del Grid de TerritorioPartida cambia de indice
            function cmbTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtTerritorioPartida);
            }

            var txtNca_Importe;
            function txtNca_Importe_OnLoad(sender, args) {
                txtNca_Importe = sender;
            }

            function onResize(sender, eventArgs) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;             
                ajaxManager.ajaxRequest('panel');
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbConsFacEle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="formulario" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMov">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="aspPanel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkDesgloceIva">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgNotaCargoDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal" style="border: solid 1px green:">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" Height="100%" dir="rtl"
            OnClientButtonClicking="ToolBar_ClientClick" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="NotaEspecial" Value="NotaEspecial" CssClass="facEspecial"
                    ToolTip="Capturar nota de cargo especial" ImageUrl="Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: verdana; font-size: 8pt; height: 100%" border="1" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="N" Owner="RadTabStrip1" PageViewID="rpvAdendaNCargo"
                                Text="Addenda de &lt;u&gt;n&lt;/u&gt;ota de cargo" Visible="False">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="T" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" Width="100%">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="103.5%" Height="351px"
                                ResizeMode="AdjacentPane" BorderStyle="Solid" BorderColor="Red" ResizeWithBrowserWindow="true">
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="351px" BorderStyle="Solid"
                                    BorderColor="Pink" OnClientResized="onResize">
                                    <div id="formulario" runat="server">
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    987998
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td valign="middle">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td width="90" valign="middle">
                                                    <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                                                </td>
                                                <td style="vertical-align: middle">
                                                    <telerik:RadComboBox ID="cmbTipo" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        Enabled="false" Filter="Contains" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        OnClientFocus="_ValidarFechaEnPeriodo" Width="100px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Factura" Value="1" />
                                                            <telerik:RadComboBoxItem Selected="true" Text="Nota de cargo" Value="2" />
                                                            <telerik:RadComboBoxItem Text="Nota de crédito" Value="3" />
                                                            <telerik:RadComboBoxItem Text="Pago" Value="5" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td width="70">
                                                    <telerik:RadNumericTextBox ID="txtFolio" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelSerie" runat="server" Text="Serie del consecutivo"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbConsFacEle" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                        OnClientBlur="Combo_ClientBlur" OnSelectedIndexChanged="cmbConsFacEle_SelectedIndexChanged"
                                                        Width="120px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ValidatorComboEmp" runat="server" ControlToValidate="cmbConsFacEle"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td width="50">
                                                    <asp:Label ID="Label3" runat="server" Text="Fecha"></asp:Label>
                                                </td>
                                                <td width="70">
                                                    <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px">
                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                        <Calendar ID="cal_dpFecha" runat="server">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput MaxLength="10" runat="server">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtFecha" runat="server" ControlToValidate="txtFecha"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td width="90">
                                                    <asp:Label ID="Label4" runat="server" Text="Movimiento"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMov" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtMov_OnBlur" OnFocus="_ValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td width="25">
                                                    <telerik:RadComboBox ID="cmbMov" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Nombre" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        OnClientFocus="_ValidarFechaEnPeriodo" OnClientSelectedIndexChanged="cmbMov_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbMov_SelectedIndexChanged" Width="350px" MaxHeight="250px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                                    </td>
                                                                    <td style="width: 300px; text-align: left">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Nombre") %>
                                                                        <div style="display: none">
                                                                            <asp:Label ID="lbl_AfeVta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AfeVta") %>'></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtMov" runat="server" ControlToValidate="txtMov"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="panelBanco" runat="server">
                                                        <table style="background-color: #EEEEEE">
                                                            <tr>
                                                                <td width="100">
                                                                    <asp:Label ID="lblBanco" runat="server" Text="Banco"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtBanco" runat="server" MaxLength="9" MinValue="1"
                                                                        Width="70px">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        <ClientEvents OnBlur="txtBanco_OnBlur" OnFocus="_ValidarFechaEnPeriodo" />
                                                                    </telerik:RadNumericTextBox>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="cmbBanco" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                                        MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo"
                                                                        OnClientSelectedIndexChanged="cmbBanco_ClientSelectedIndexChanged" Width="250px"
                                                                        MaxHeight="250px">
                                                                        <ItemTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="width: 50px; text-align: center">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                                                    </td>
                                                                                    <td style="width: 200px; text-align: left">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblVal_txtBanco" runat="server" ForeColor="#FF0000"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelCuentaContable" runat="server">
                                                        <table style="background-color: #EEEEEE">
                                                            <tr>
                                                                <td width="100">
                                                                    <asp:Label ID="Label17" runat="server" Text="Cuenta contable"></asp:Label>
                                                                </td>
                                                                <td style="height: 30px">
                                                                    <telerik:RadTextBox ID="txtCuentaContable" runat="server" MaxLength="15" Width="150px">
                                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloNumerico" />
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                                <td style="height: 30px">
                                                                    <asp:Label ID="lblVal_txtCuentaContable" runat="server" ForeColor="#FF0000"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtCliente" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEventCliente" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="295px" ReadOnly="True">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtCliente" runat="server" ControlToValidate="txtCliente"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                        ToolTip="Buscar" ValidationGroup="buscar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Territorio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTerritorio_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo"
                                                        OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged" OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged"
                                                        Width="300px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                        <div style="display: none">
                                                                            <asp:Label ID="lbl_Id_Rik" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Rik_Nombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtTerritorio" runat="server" ControlToValidate="txtTerritorio"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td width="90">
                                                    <asp:Label ID="Label7" runat="server" Text="Representante"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtRepresentanteStr" runat="server" Enabled="false" Width="295px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtRepresentante" runat="server" ControlToValidate="txtRepresentante"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDesgloceIva" runat="server" Text="Desglose I.V.A." AutoPostBack="True"
                                                        OnCheckedChanged="chkDesgloceIva_CheckedChanged" />
                                                </td>
                                                <td width="10">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Text="Referencia"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtReferencia" runat="server" MaxLength="9" MinValue="0"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtReferencia" runat="server" ControlToValidate="txtReferencia"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td rowspan="4">
                                                    <telerik:RadTextBox ID="txtNotas" runat="server" Height="60px" TextMode="MultiLine"
                                                        Width="382px" MaxLength="250">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Notas"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="351px" Width="100%">                        
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="103.5%" Height="351px"
                                ResizeMode="AdjacentPane" BorderStyle="Solid" BorderColor="Red" ResizeWithBrowserWindow="true">
                                <telerik:RadPane ID="RadPane1" runat="server" Width="100%" Height="351px" BorderStyle="Solid"
                                    BorderColor="Pink" OnClientResized="onResize">                                  
                                    <telerik:RadGrid ID="rgNotaCargoDet" runat="server" GridLines="None" AllowPaging="False"
                                        AutoGenerateColumns="False" OnNeedDataSource="rgNotaCargoDet_NeedDataSource"
                                        OnInsertCommand="rgNotaCargoDet_InsertCommand" OnUpdateCommand="rgNotaCargoDet_UpdateCommand"
                                        OnDeleteCommand="rgNotaCargoDet_DeleteCommand" OnItemDataBound="rgNotaCargoDet_ItemDataBound"
                                        OnItemCommand="rgNotaCargoDet_ItemCommand" OnPageIndexChanged="rgNotaCargoDet_PageIndexChanged"
                                        OnItemCreated="rgNotaCargoDet_ItemCreated" OnDataBound="rgNotaCargoDet_DataBound">
                                        <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Nca,Id_NcaDet,Id_Prd,Id_Rik,Id_Ter"
                                            EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                            NoMasterRecordsText="No se encontraron registros." PageSize="5">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Nca" HeaderText="Id_Nca" UniqueName="Id_Nca"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_NcaDet" HeaderText="Id_NcaDet" UniqueName="Id_NcaDet"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Ter" UniqueName="Id_TerN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTerritorioPartidaNum" runat="server" Text='<%# Eval("Id_Ter")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtTerritorioPartida" runat="server" Width="50px"
                                                            MaxLength="9" MinValue="1" Text='<%# Eval("Id_Ter") %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtTerritorioPartida_OnBlur" OnLoad="txtTerritorioPartida_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Territorio" DataField="Id_Ter" UniqueName="Id_Ter">
                                                    <HeaderStyle Width="270px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritorioPartida" runat="server" Text='<%# Eval("Ter_Nombre").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbltxtTerritorioPartida" runat="server" ForeColor="#FF0000"></asp:Label>
                                                        <telerik:RadComboBox ID="cmbTerritorioPartida" runat="server" Width="250px" Filter="Contains"
                                                            MaxHeight="250px" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                            LoadingMessage="Cargando..." HighlightTemplatedItems="true" AutoPostBack="true"
                                                            OnClientSelectedIndexChanged="cmbTerritorioPartida_ClientSelectedIndexChanged"
                                                            OnClientBlur="Combo_ClientBlur" OnClientLoad="cmbTerritorioPartida_OnLoad" OnSelectedIndexChanged="cmbTerritorioPartida_SelectedIndexChanged">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() %>' />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="lblTerritorioPartidaEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>'></asp:Label>
                                                                            <div style="display: none">
                                                                                <asp:Label ID="lbl_Id_Rik_Partida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_Rik_Nombre_Partida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Rik" UniqueName="Id_RikN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRepresentanteNum" runat="server" Text='<%# Eval("Id_Rik")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtRepresentantePartida" runat="server" Width="50px"
                                                            MaxLength="9" Enabled="false" Text='<%# Eval("Id_Rik") %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Representante" DataField="Id_Rik" UniqueName="Id_Rik">
                                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRepresentante" runat="server" Text='<%# Eval("Rik_Nombre").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtRepresentantePartidaStr" runat="server" Width="280px"
                                                            Enabled="false">
                                                        </telerik:RadTextBox>
                                                        <br />
                                                        <asp:Label ID="lblTxtRepresentantePartida" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="1" Text='<%# Eval("Id_Prd") %>' OnTextChanged="cmbProductoDet_TextChanged"
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnLoad="txtId_Prd_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_Prd" runat="server" Text='<%# Eval("Prd_Nombre").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="280px"
                                                            Text='<%# Bind("Prd_Nombre") %>'>
                                                        </telerik:RadTextBox>
                                                        <asp:Label ID="lbl_productoold" runat="server" Visible="false" Text='<%# Eval("Id_Prd") %>'>></asp:Label>
                                                        <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Nca_Importe" HeaderText="Importe" UniqueName="Nca_Importe">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNca_Importe" runat="server" Text='<%# Bind("Nca_Importe", "{0:#,##0.00}")  %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtNca_Importe" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="0" Text='<%# Eval("Nca_Importe") %>'>
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            <ClientEvents OnLoad="txtNca_Importe_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:Label ID="lbl_txtNca_Importe" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                                            PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                            PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                            ShowPagerText="True" PageButtonCount="3" />
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </telerik:RadPane>                            
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvAdendaNCargo" runat="server" Height="350px">
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgAdendaNotaCargo" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" OnNeedDataSource="rgAdendaNotaCargo_NeedDataSource" Height="335px"
                                            Width="785px">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" ScrollHeight="100px" SaveScrollPosition="true" UseStaticHeaders="true" />
                                            </ClientSettings>
                                            <MasterTableView>
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Tipo" HeaderText="Tipo" UniqueName="Tipo" Display="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Id_AdeDet" HeaderText="Id_AdeDet" UniqueName="Id_AdeDet"
                                                        Display="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="nodo" HeaderText="Nodo" UniqueName="nodo" Display="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="campo" HeaderText="Campo" UniqueName="campo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn DataField="valor" HeaderText="Valor" UniqueName="valor">
                                                        <HeaderStyle Width="250px" />
                                                        <ItemTemplate>
                                                            <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="200px" Text='<%# Bind("valor") %>'
                                                                MaxLength='<%# Bind("Longitud") %>'>
                                                                <ClientEvents OnKeyPress="Email" />
                                                            </telerik:RadTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <div id="formularioTotales" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label14" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td width="80">
                                    <telerik:RadNumericTextBox ID="txtSubtotal2" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                        <ClientEvents OnBlur="CalcularTotales" OnFocus="_ValidarFechaEnPeriodo" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="IVA">
                                <td align="right">
                                    <asp:Label ID="Label15" runat="server" Text="I.V.A."></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIva2" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                        <ClientEvents OnBlur="CalcularTotales" OnFocus="_ValidarFechaEnPeriodo" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label16" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotal2" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:HiddenField ID="hiddenId" runat="server" />
                    <asp:HiddenField ID="HD_IVAfacturacion" runat="server" Value="0" />
                    <asp:HiddenField ID="HD_PanelVisible" runat="server" Value="" />
                    <asp:HiddenField ID="HD_GridRebind_notaEspecial" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
