<%@ Page Title="Notas de crédito" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapNotaCredito.aspx.cs" Inherits="SIANWEB.CapNotaCredito" %>

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
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';

            function _PreValidarFechaEnPeriodo() {
                //debugger;
                if ('<%= FechaEnable %>' == '1') {
                    _ValidarFechaEnPeriodo();
                }
            }

            function ClientTabSelecting(sender, args) {
                //debugger;
                if ('<%= FechaEnable %>' == '1') {
                    continuarAccion = _ValidarFechaEnPeriodo();
                    args.set_cancel(!continuarAccion);
                }
                else {
                    continuarAccion = true;
                }
                if (continuarAccion) {
                    var Mov = $find('<%= txtFolio.ClientID %>');
                    var Cte = $find('<%= txtMov.ClientID %>');
                    var Ter = $find('<%= txtReferencia.ClientID %>');

                    if (Mov.get_value() == "") {
                        radalert('Por favor seleccione la serie del consecutivo antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Cte.get_value() == "") {
                        radalert('Por favor seleccione el tipo de movimiento antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Ter.get_value() == "") {
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
            var txtNcr_ImporteClientID = '';
            var lbl_txtNcr_ImporteClientID = '';

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
                var txtId_Prd = $find(txtId_PrdClientID);
                var lbl_txtNcr_Importe = document.getElementById(lbl_txtNcr_ImporteClientID);
                var txtNcr_Importe = $find(txtNcr_ImporteClientID);

                //Limpiar contenedores de mensaje de validación
                lbltxtTerritorioPartida.innerHTML = '';
                lblTxtRepresentantePartida.innerHTML = '';
                lbl_cmbProducto.innerHTML = '';
                lbl_txtNcr_Importe.innerHTML = '';

                if (txtTerritorioPartida != null)
                    if (txtTerritorioPartida.get_textBoxValue() == '') {
                        //lbltxtTerritorioPartida.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (txtRepresentantePartida != null)
                    if (txtRepresentantePartida.get_textBoxValue() == '') {
                        //lblTxtRepresentantePartida.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (txtId_Prd != null)
                    if (txtId_Prd.get_textBoxValue() == '') {
                        //lbl_cmbProducto.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (txtNcr_Importe != null)
                    if (txtNcr_Importe.get_textBoxValue() == '') {
                        //lbl_txtNcr_Importe.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (continuarAccion == false) {
                    var alertaRequedridosGrig = radalert('Todos los datos son requeridos', 330, 150, tituloMensajes);
                }

                return continuarAccion
            }

            function ValidacionesEspeciales() {
                var conntinuar = true;

                var hiddenHD_PanelVisible = document.getElementById('<%= HD_PanelVisible.ClientID %>');
                if (hiddenHD_PanelVisible.value == 'e') {
                    var lblVal_txtEmpleado = document.getElementById('<%= lblVal_txtEmpleado.ClientID %>');
                    var lblVal_txtNombreEmpleado = document.getElementById('<%= lblVal_txtNombreEmpleado.ClientID %>');
                    var txtEmpleado = $find('<%= txtEmpleado.ClientID %>');
                    var txtNombreEmpleado = $find('<%= txtNombreEmpleado.ClientID %>');

                    if (txtEmpleado != null)
                        if (txtEmpleado.get_textBoxValue() == '') {
                            lblVal_txtEmpleado.innerHTML = '*Requerido';
                            conntinuar = false
                        }
                        else {
                            lblVal_txtEmpleado.innerHTML = ''
                        }
                    if (txtNombreEmpleado != null)
                        if (txtNombreEmpleado.get_textBoxValue() == '') {
                            lblVal_txtNombreEmpleado.innerHTML = '*Requerido';
                            conntinuar = false
                        }
                        else {
                            lblVal_txtNombreEmpleado.innerHTML = ''
                        }
                }
                else {
                    if (hiddenHD_PanelVisible.value == 'c') {
                        var lblVal_txtCuentaContable = document.getElementById('<%= lblVal_txtCuentaContable.ClientID %>');
                        var txtCuentaContable = $find('<%= txtCuentaContable.ClientID %>');

                        if (txtCuentaContable != null)
                            if (txtCuentaContable.get_textBoxValue() == '') {
                                lblVal_txtCuentaContable.innerHTML = '*Requerido';
                                conntinuar = false
                            }
                            else {
                                lblVal_txtCuentaContable.innerHTML = ''
                            }
                    }
                }
                return conntinuar
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

                //if (tabSeleccionada == 'Datos generales')
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

                        if (continuarAccion == true) {
                            //validar saldo de la captura contra saldo del documento
                            var txtTotal = $find('<%= txtTotal.ClientID %>');
                            var txtSaldo = $find('<%= txtSaldo.ClientID %>');
                            var saldoMovimiento = 0;
                            var saldoCaptura = 0;
                            if (txtTotal.get_textBoxValue() != '') {
                                saldoCaptura = parseFloat(txtTotal.get_textBoxValue());
                            }
                            if (txtSaldo.get_textBoxValue() != '') {
                                saldoMovimiento = parseFloat(txtSaldo.get_textBoxValue());
                            }

                            if (saldoMovimiento == 0 && saldoCaptura == 0) {
                                var alertNoSaldos = radalert('La nota de crédito no tiene saldo', 330, 150, tituloMensajes);
                                continuarAccion = false;
                            }
                            else {
                                if (saldoMovimiento - saldoCaptura < 0) {
                                    continuarAccion = RadConfirmSaldoMenor();
                                }
                                else if (saldoMovimiento != saldoCaptura) {
                                    continuarAccion = RadConfirmSaldosDiferentes();
                                }
                            }
                        }
                        break;

                    case 'NotaEspecial':
                        var radGrid = $find('<%= rgNotaCreditoDet.ClientID %>');
                        var MasterTable = radGrid.get_masterTableView();
                        var length = MasterTable.get_dataItems().length;

                        if (length != '' && length > 0) {
                            AbrirVentana_NotaCreditoEspecial();
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
            function AbrirVentana_NotaCreditoEspecial() {
                //debugger;                
                var txtSubtotal = $find('<%= txtSubtotal.ClientID %>');
                var txtIva = document.getElementById('<%= txtIva.ClientID %>');
                var txtTotal = $find('<%= txtTotal.ClientID %>');
                var txtCliente = $find('<%= txtCliente.ClientID %>');
                var Folio = $find('<%= txtFolio.ClientID %>');
                var Id_NcrSerie = document.getElementById('<%= HDId_NcrSerie.ClientID %>');

                txtIva = txtIva == null ? 0 : txtIva.value;
                

                var oWnd = radopen("CapNotaCredito_Especial.aspx?Id_Cte="
                  + txtCliente.get_value()
                  + "&Ncr_ImporteTotal=" + txtSubtotal.get_value()
                  + "&IVA_Ncr=" + txtIva
                  + "&Folio=" + Folio.get_value() //clave de la factura   
                  + "&Id_NcrSerie=" + Id_NcrSerie.value + Folio.get_value()
                  + "&Modificar=" + '<%= HabilitarGuardar %>'
                , "AbrirVentana_NotaCreditoEspecial");
                oWnd.center();
                oWnd.Maximize();
            }

            function RadConfirmSaldosDiferentes_CallBackFn(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                    ajaxManager.ajaxRequest('GuardarMovimiento');
                }
            }
            function RadConfirmSaldoMenor() {
                radconfirm("El saldo del documento se hará negativo. ¿Desea continuar?", RadConfirmSaldosDiferentes_CallBackFn, 330, 150)
                return false;
            }
            function RadConfirmSaldosDiferentes() {
                radconfirm("El saldo del documento es diferente al importe de la nota de crédito. ¿Desea continuar?", RadConfirmSaldosDiferentes_CallBackFn, 330, 150)
                return false;
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= txtFecha.ClientID %>');
                return txtFecha._dateInput;
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
                                Close();
                            });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function Close() {
                GetRadWindow().Close();
            }

            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid();
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

            //calcular totales
            function CalcularTotales(sender, args) {
                //debugger;
                var txtSubtotal = $find('<%= txtSubtotal.ClientID %>');
                var txtIva = $find('<%= txtIva.ClientID %>');
                var txtTotal = $find('<%= txtTotal.ClientID %>');
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

                if (txtNcr_Importe != null) {
                    txtNcr_Importe.focus();
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

            var txtNcr_Importe;
            function txtNcr_Importe_OnLoad(sender, args) {
                txtNcr_Importe = sender;
            }
            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }
            function ClienteSeleccionado() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('cliente');
            }
            function onResize(sender, eventArgs) {
                //debugger;
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>");
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }     
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbConsFacEle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMov">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMovimientoTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtReferencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="formulario" LoadingPanelID="RadAjaxLoadingPanel1"
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
                    <telerik:AjaxUpdatedControl ControlID="formularioTotales" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgNotaCreditoDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="NotaEspecial" Value="NotaEspecial" CssClass="facEspecial"
                    ToolTip="Capturar nota de crédito especial" ImageUrl="Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <br />
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="N" PageViewID="rpvAdendaNCredito" Text="Adenda de &lt;u&gt;n&lt;/u&gt;ota de crédito"
                                Visible="False">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="T" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" Width="100%">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="102.4%" Height="351px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0">
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%" Height="351px" OnClientResized="onResize">
                                    <div id="formulario" runat="server">
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td width="90">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td colspan="1">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                                                </td>
                                                <td colspan="1">
                                                    <telerik:RadComboBox ID="cmbTipo" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        Enabled="false" Filter="Contains" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        OnClientFocus="_PreValidarFechaEnPeriodo" Width="115px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Factura" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Nota de cargo" Value="2" />
                                                            <telerik:RadComboBoxItem Selected="true" Text="Nota de crédito" Value="3" />
                                                            <telerik:RadComboBoxItem Text="Devolución parcial" Value="4" />
                                                            <telerik:RadComboBoxItem Text="Pago" Value="5" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="5">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtFolio" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtFolio" runat="server" ControlToValidate="txtFolio"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td width="5">
                                                    &nbsp;&nbsp;
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
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td width="90">
                                                    <asp:Label ID="Label4" runat="server" Text="Movimiento"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMov" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtMov_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbMov" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Nombre" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        OnClientFocus="_PreValidarFechaEnPeriodo" OnClientSelectedIndexChanged="cmbMov_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbMov_SelectedIndexChanged" Width="300px" MaxHeight="250px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Nombre") %>
                                                                        <div style="display: none">
                                                                            <asp:Label ID="lbl_AfeVta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AfeVta") %>'></asp:Label></div>
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
                                                <td width="5">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Fecha"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px">
                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                        <Calendar ID="cal_dpFecha" runat="server">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput ID="DateInput1" runat="server" MaxLength="10">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput></telerik:RadDatePicker>
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
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td width="90">
                                                    <asp:Label ID="Label9" runat="server" Text="Documento"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbMovimientoTipo" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        Filter="Contains" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo"
                                                        OnSelectedIndexChanged="cmbMovimientoTipo_SelectedIndexChanged" Width="115px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Factura" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Nota de cargo" Value="2" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbMovimientoTipo"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td colspan="1" width="5">
                                                    &nbsp;&nbsp;
                                                </td>
                                                 <td>
                                                    Serie
                                                </td>
                                                  <td>
                                                    <telerik:RadComboBox ID="cmbSerie" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        Filter="Contains" MarkFirstMatch="true" OnClientFocus="_PreValidarFechaEnPeriodo"
                                                        Width="115px">                  
                                                    </telerik:RadComboBox>
                                                </td>
                                                   <td colspan="1" width="5">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    Referencia
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtReferencia" runat="server" AutoPostBack="true"
                                                        MaxLength="9" MinValue="1" OnTextChanged="txtReferencia_TextChanged" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtReferencia" runat="server" ControlToValidate="txtReferencia"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td width="5">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    Saldo
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtSaldo" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="0" Width="70px">
                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtSaldo" runat="server" ControlToValidate="txtSaldo"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td width="90">
                                                    <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtCliente" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true" Enabled="False">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True"
                                                        Enabled="False">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtCliente" runat="server" ControlToValidate="txtCliente"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Territorio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px" Enabled="False">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTerritorio_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo"
                                                        OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged" OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged"
                                                        Width="300px" Enabled="False">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() %>
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion")%>
                                                                        <div style="display: none">
                                                                            <asp:Label ID="lbl_Id_Rik" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label><asp:Label
                                                                                ID="lbl_Rik_Nombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label></div>
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
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Representante"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" Enabled="False" MaxLength="9"
                                                        MinValue="1" Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtRepresentanteStr" runat="server" Enabled="False" Width="300px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtRepresentante" runat="server" ControlToValidate="txtRepresentante"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDesgloceIva" runat="server" Text="Desglose I.V.A." AutoPostBack="True"
                                                        OnCheckedChanged="chkDesgloceIva_CheckedChanged" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDesglocePartidasFormato" runat="server" Text="Desglose de partidas en el formato" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Panel ID="panelEmpleado" runat="server">
                                                        <table style="background-color: #EEEEEE">
                                                            <tr>
                                                                <td style="height: 30px; width: 97px">
                                                                    <asp:Label ID="lblEmpleado" runat="server" Text="Núm. de nómina"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtEmpleado" runat="server" MaxLength="9" MinValue="1"
                                                                        Width="90px">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox><asp:Label ID="lblVal_txtEmpleado" runat="server" ForeColor="#FF0000"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNombreEmpleado" runat="server" Text="Nombre"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <div style="margin-left: 5px">
                                                                        <telerik:RadTextBox ID="txtNombreEmpleado" runat="server" MaxLength="70" Width="200px">
                                                                            <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfabetico" />
                                                                        </telerik:RadTextBox><asp:Label ID="lblVal_txtNombreEmpleado" runat="server" ForeColor="#FF0000"></asp:Label></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelCuentaContable" runat="server">
                                                        <table style="background-color: #EEEEEE">
                                                            <tr>
                                                                <td style="height: 30px; width: 97px">
                                                                    <asp:Label ID="Label17" runat="server" Text="Cuenta contable"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <telerik:RadTextBox ID="txtCuentaContable" runat="server" MaxLength="15" Width="261px">
                                                                            <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloNumerico" />
                                                                        </telerik:RadTextBox><asp:Label ID="lblVal_txtCuentaContable" runat="server" ForeColor="#FF0000"></asp:Label></div>
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
                                                </td>
                                                <td>
                                                    Notas
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNotas" runat="server" Height="45px" TextMode="MultiLine"
                                                        Width="510px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="351px" Width="95%">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="102.4%" Height="351px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0">
                                <telerik:RadPane ID="RadPane1" runat="server" Width="100%" Height="351px" OnClientResized="onResize">
                                    <%-- <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="800px">--%>
                                    <telerik:RadGrid ID="rgNotaCreditoDet" runat="server" GridLines="None" AllowPaging="False"
                                        AutoGenerateColumns="False" OnNeedDataSource="rgNotaCreditoDet_NeedDataSource"
                                        OnInsertCommand="rgNotaCreditoDet_InsertCommand" OnUpdateCommand="rgNotaCreditoDet_UpdateCommand"
                                        OnDeleteCommand="rgNotaCreditoDet_DeleteCommand" OnItemDataBound="rgNotaCreditoDet_ItemDataBound"
                                        OnItemCommand="rgNotaCreditoDet_ItemCommand" OnPageIndexChanged="rgNotaCreditoDet_PageIndexChanged"
                                        OnItemCreated="rgNotaCreditoDet_ItemCreated">
                                        <MasterTableView Name="Master" CommandItemDisplay="Top" EditMode="InPlace" DataMember="listaOrdCompraDet"
                                            HorizontalAlign="NotSet" AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros."
                                            PageSize="5" DataKeyNames="Id_Ncr,Id_NcrDet,Id_Prd,Id_Rik,Id_Ter">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Ncr" HeaderText="Id_Ncr" UniqueName="Id_Ncr"
                                                    ReadOnly="true" Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_NcrDet" HeaderText="Id_NcrDet" UniqueName="Id_NcrDet"
                                                    ReadOnly="true" Display="false">
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
                                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritorioPartida" runat="server" Text='<%# Eval("Ter_Nombre").ToString() %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbltxtTerritorioPartida" runat="server" ForeColor="#FF0000" Text='<%# Eval("Ter_Nombre").ToString() %>'></asp:Label>
                                                        <telerik:RadComboBox ID="cmbTerritorioPartida" runat="server" Width="180px" Filter="Contains"
                                                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" LoadingMessage="Cargando..."
                                                            HighlightTemplatedItems="true" OnClientSelectedIndexChanged="cmbTerritorioPartida_ClientSelectedIndexChanged"
                                                            MaxHeight="300px" OnClientBlur="Combo_ClientBlur" OnClientLoad="cmbTerritorioPartida_OnLoad"
                                                            OnSelectedIndexChanged="cmbTerritorioPartida_SelectedIndexChanged" AutoPostBack="true">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </tr>
                                                                </table>
                                                                <div style="display: none">
                                                                    <asp:Label ID="lbl_Id_Rik_Partida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label><asp:Label
                                                                        ID="lbl_Rik_Nombre_Partida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label></div>
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
                                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRepresentante" runat="server" Text='<%# Eval("Rik_Nombre").ToString() %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtRepresentantePartidaStr" runat="server" Width="180px"
                                                            Enabled="false">
                                                        </telerik:RadTextBox><br />
                                                        <asp:Label ID="lblTxtRepresentantePartida" runat="server" ForeColor="#FF0000"></asp:Label></EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="1" Text='<%# Eval("Id_Prd") %>' AutoPostBack="true" OnTextChanged="cmbProductoDet_TextChanged">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnLoad="txtId_Prd_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_Prd" runat="server" Text='<%# Eval("Prd_Nombre").ToString() %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="180px"
                                                            Text='<%# Bind("Prd_Nombre") %>'>
                                                        </telerik:RadTextBox>
                                                        <asp:Label ID="lbl_productoold" runat="server" Visible="false" Text='<%# Eval("Id_Prd") %>'></asp:Label>
                                                        <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Ncr_Importe" HeaderText="Importe" UniqueName="Ncr_Importe">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNcr_Importe" runat="server" Text='<%# Bind("Ncr_Importe", "{0:#,##0.00}") %>' /></ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtNcr_Importe" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="0" Text='<%# Eval("Ncr_Importe") %>'>
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            <ClientEvents OnLoad="txtNcr_Importe_OnLoad" />
                                                        </telerik:RadNumericTextBox><asp:Label ID="lbl_txtNcr_Importe" runat="server" ForeColor="#FF0000"></asp:Label></EditItemTemplate>
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
                                    </telerik:RadGrid><div id="botonFacturaEspecial" runat="server" style="text-align: right;
                                        margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="btnFacturaEspecial" runat="server" Visible="false" Text="Capturar factura especial"
                                            OnClientClick="AbrirVentana_NotaCreditoEspecial()" /></div>
                                    <%--   </asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvAdendaNCredito" runat="server">
                            <telerik:RadSplitter ID="RadSplitter3" runat="server" Width="102.4%" Height="351px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0">
                                <telerik:RadPane ID="RadPane3" runat="server" Width="100%" Height="351px" OnClientResized="onResize">
                                    <telerik:RadGrid ID="rgAdendaNotaCredito" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" OnNeedDataSource="rgAdendaNotaCredito_NeedDataSource" BorderStyle="None"
                                        Height="100%">
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
                                                            <ClientEvents OnKeyPress="SinComillas" />
                                                        </telerik:RadTextBox></ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView></telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <div id="formularioTotales" runat="server">
                        <table width="99%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label14" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td width="80">
                                    <telerik:RadNumericTextBox ID="txtSubtotal" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                        <ClientEvents OnBlur="CalcularTotales" OnFocus="_PreValidarFechaEnPeriodo" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="IVA">
                                <td align="right">
                                    <asp:Label ID="Label15" runat="server" Text="I.V.A."></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIva" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                        <ClientEvents OnBlur="CalcularTotales" OnFocus="_PreValidarFechaEnPeriodo" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label16" runat="server" Text="Total"></asp:Label>
                                    <asp:HiddenField ID="HD_IVAfacturacion" runat="server" Value="0" />
                                    <asp:HiddenField ID="HD_PanelVisible" runat="server" Value="" />
                                    <asp:HiddenField ID="hiddenId" runat="server" />
                                    <asp:HiddenField ID="HDId_NcrSerie" runat="server" />
                                    <asp:HiddenField ID="HD_GridRebind_notaEspecial" runat="server" Value="0" />
                                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
