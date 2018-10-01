<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapFactura.aspx.cs" Inherits="SIANWEB.CapFactura" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------

            function _PreValidarFechaEnPeriodo() {
                //debugger;
                if ('<%= FechaEnable %>' == '1') {
                    _ValidarFechaEnPeriodo();
                }
            }

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

                    var Ser = $find('<%= txtId.ClientID %>');
                    var Mov = $find('<%= txtMov.ClientID %>');
                    var Cte = $find('<%= txtCliente.ClientID %>');
                    var Ter = $find('<%= txtTerritorio.ClientID %>');
                    var Ter = $find('<%= txtTerritorio.ClientID %>');
                    var ReParcial = $find('<%= ChkRefacturaparcial.ClientID %>');
                    var Causa = $find('<%= txtCausaRef.ClientID %>');

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
                    //                    else if (Causa.get_value() == "" && ReParcial.Checked == true) {
                    //                        radalert('Por favor seleccione una causa de re facturación  antes de continuar', 330, 150, '');
                    //                        args.set_cancel(true);
                    //                     } 

                }
            }

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Orden de Compra
            //--------------------------------------------------------------------------------------------------

            function LimpiarControlesOrdenCompra() {
                //debugger;
                var txtId = $find('<%= txtId.ClientID %>');
                LimpiarTextBox(txtId);
            }

            //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
            //y selecciona la Tab donde esta el control
            function ValidaObjetoRequerido(textBox, label, indiceTab) {
                //debugger;
                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                if (textBox.get_textBoxValue() == '') {
                    label.innerHTML = '*Requerido';
                    radTabStrip.get_allTabs()[indiceTab].select();
                    return false;
                }
                return true;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lblFac_CantidadClientId = '';
            var txtFac_CantidadClientId = '';
            var lbl_cmbProductoClientId = '';
            var txtId_PrdClientId = '';
            var lbltxtTerritorioPartidaClientId = '';
            var txtTerritorioPartidaClientId = '';
            var txtId_PrdAdeClientId = '';
            var lblTxtClienteExternoClientId = '';
            var txtClienteExternoClientId = '';
            //Validación del formulario de insercion/edición de registro en un RadGrid.
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            function ValidaFormEdit(accion) {
                //debugger;
                var continuarAccion = true;
                //obtener controles de formulario de inserción/edición de Grid
                var lblFac_Cantidad = document.getElementById(lblFac_CantidadClientId);
                var txtFac_Cantidad = $find(txtFac_CantidadClientId);
                var lbl_cmbProducto = document.getElementById(lbl_cmbProductoClientId);
                var txtId_Prd = $find(txtId_PrdClientId);
                var lbltxtTerritorioPartida = document.getElementById(lbltxtTerritorioPartidaClientId);
                var txtTerritorioPartida = $find(txtTerritorioPartidaClientId);
                var lblTxtClienteExterno = document.getElementById(lblTxtClienteExternoClientId);
                var txtClienteExterno = $find(txtClienteExternoClientId);
                var txtId_PrdAde = $find(txtId_PrdAdeClientId);

                try {
                    //Limpiar contenedores de mensaje de validación
                    lblFac_Cantidad.innerHTML = '';
                    lbl_cmbProducto.innerHTML = '';
                    lbltxtTerritorioPartida.innerHTML = '';
                    if (lblTxtClienteExterno != null) {
                        lblTxtClienteExterno.innerHTML = '';
                    }

                }
                catch (e) {

                }

                //---------------------------------
                //inicia validaciones de formulario
                //---------------------------------
                //validar cliente
                //si el movimiento es 70

                var cmbMov = $find('<%= cmbMov.ClientID %>');
                if (cmbMov.get_value() == '70') {
                    if (txtClienteExterno != null) {
                        if (txtClienteExterno.get_textBoxValue() == '') {
                            lblTxtClienteExterno.innerHTML = '*Requerido';
                            continuarAccion = false
                        }
                        else {
                            //validar que no sea el mismo cliente que el de los datos generales de la factura
                            var txtCliente = $find('<%= txtCliente.ClientID %>');
                            if (txtCliente.get_textBoxValue() == txtClienteExterno.get_textBoxValue()) {
                                var alertCliente = radalert('El cliente no debe ser el mismo que el capturado en la pestaña de datos generales', 330, 150, tituloMensajes);
                                alertCliente.add_close(
                                    function () {
                                        txtClienteExterno.focus();
                                    });
                                continuarAccion = false
                            }
                        }
                    }
                    else
                        continuarAccion = false
                }

                //validar territorio
                if (txtTerritorioPartida != null) {
                    if ((txtTerritorioPartida.get_textBoxValue()) == '' && (txtId_Prd.get_textBoxValue() == '')) {
                        //                     lbltxtTerritorioPartida.innerHTML = '*Requerido';
                        alert("EL CAMPO TERRITORIO ES REQUERIDO");
                        continuarAccion = false
                    }
                    else
                        if (txtId_Prd.get_textBoxValue() == '') {
                            //                       lbl_cmbProducto.innerHTML = '*Requerido';
                            alert("EL CAMPO PRODUCTO ES REQUERIDO");
                            continuarAccion = false
                        }
                }
                else
                    continuarAccion = false



                //validar cantidad
                if (txtFac_Cantidad != null) {
                    if (txtFac_Cantidad.get_textBoxValue() == '') {
                        lblFac_Cantidad.innerHTML = '*Requerido';
                        continuarAccion = false
                    }
                    else
                        if (parseInt(txtFac_Cantidad.get_textBoxValue()) <= 0) {
                            lblFac_Cantidad.innerHTML = '*Requerido. El valor debe ser mayor a 0';
                            continuarAccion = false
                        }
                }
                else
                    continuarAccion = false
                return continuarAccion
            }
            //====================================================================================================
            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid. 
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            // Raúl Bórquez Martínez 
            // 29-05-2015 
            //Inicia codigo Factura especial=======================================================================
            var lblRem_CantidadClientId_Esp = '';
            var txtRem_CantidadClientId_Esp = '';
            var lbl_cmbProductoClientId_Esp = '';
            var txtId_PrdClientId_Esp = '';
            var lblVal_txtPrd_DescripcionClientId_Esp = '';
            var txtPrd_DescripcionClientId_Esp = '';
            var lblVal_txtPrd_PresentacionClientId_Esp = '';
            var txtPrd_PresentacionClientId_Esp = '';
            var lblVal_txtPrd_UniNeClientId_Esp = '';
            var txtPrd_UniNeClientId_Esp = '';
            var lblVal_txtRem_PrecioClientId_Esp = '';
            var txtRem_PrecioClientId_Esp = '';

            //Validación del formulario de insercion/edición de registro en un RadGrid. Para facturas especiales
            function ValidaFormEditEspecial(accion) {
                var continuarAccion = true;
                debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var lblRem_Cantidad_Esp = document.getElementById(lblRem_CantidadClientId_Esp);
                var txtRem_Cantidad_Esp = $find(txtRem_CantidadClientId_Esp);
                var lbl_cmbProducto_Esp = document.getElementById(lbl_cmbProductoClientId_Esp);
                var txtId_Prd_Esp = $find(txtId_PrdClientId_Esp);
                var lblVal_txtPrd_Descripcion_Esp = document.getElementById(lblVal_txtPrd_DescripcionClientId_Esp);
                var txtPrd_Descripcion_Esp = $find(txtPrd_DescripcionClientId_Esp);
                var lblVal_txtPrd_Presentacion_Esp = document.getElementById(lblVal_txtPrd_PresentacionClientId_Esp);
                var txtPrd_Presentacion_Esp = $find(txtPrd_PresentacionClientId_Esp);
                var lblVal_txtPrd_UniNe_Esp = document.getElementById(lblVal_txtPrd_UniNeClientId_Esp);
                var txtPrd_UniNe_Esp = $find(txtPrd_UniNeClientId_Esp);
                var lblVal_txtRem_Precio_Esp = document.getElementById(lblVal_txtRem_PrecioClientId_Esp);
                var txtRem_Precio_Esp = $find(txtRem_PrecioClientId_Esp);

                //Limpiar contenedores de mensaje de validación
                lblRem_Cantidad_Esp.innerHTML = '';
                lbl_cmbProducto_Esp.innerHTML = '';
                lblVal_txtPrd_Descripcion_Esp.innerHTML = '';
                lblVal_txtPrd_Presentacion_Esp.innerHTML = '';
                lblVal_txtRem_Precio_Esp.innerHTML = '';
                return continuarAccion
            }
            //Termina código Factura especial=======================================================================

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
                        continuarAccion = true;
                        break;

                    case 'save':
                        if (Page_ClientValidate()) {
                            button.set_enabled(false);
                        }

                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        continuarAccion = _ValidarFechaEnPeriodo();
                        //                    if (continuarAccion == false) {
                        radTabStrip.get_allTabs()[0].select();
                        //                    }
                        break;
                }

                var HF = document.getElementById('<%= HF_VI.ClientID %>');
                if (continuarAccion == true && HF.value == 'false') {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                }
                args.set_cancel(!continuarAccion);
            }

            function HabilitarGuardar() {
                var toolBar = $find("<%=RadToolBar1.ClientID %>");
                var button = toolBar.findItemByValue("save");
                button.set_enabled(true);
            }


            function ObtenerControlFecha() {
                var txtFecha = $find('<%= txtFecha.ClientID %>');
                return txtFecha._dateInput;
            }
            //Oculta o visualiza la columna de cliente del Grid
            function rgFacturaDet_HiddeColumn(ocultar) {
                var radGrid = $find('<%= rgFacturaDet.ClientID %>');
                var table = radGrid.get_masterTableView();
                var column = table.getColumnByUniqueName("Id_CteExt");

                if (ocultar)
                    table.hideColumn(column.get_element().cellIndex);
                else
                    table.showColumn(column.get_element().cellIndex);
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
                ////debugger;
                var cerrarWindow = radalert(mensaje, 350, 150, tituloMensajes);
                cerrarWindow.add_close(
                    function () {
                        ////debugger;
                        CloseAndRebind();
                        RefreshParentPage();
                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                ////debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid_FacturaEspecial() {
                ////debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura especial se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent_FacturaEspecial(sender, eventArgs) {
            }

            function LimpiarBanderaRebind_FacturaEspecial(sender, eventArgs) {
                ////debugger;

                ModificaBanderaRebind_FacturaEspecial('0');
            }

            function ActivarBanderaRebind_FacturaEspecial() {
                ////debugger;
                ModificaBanderaRebind_FacturaEspecial('1');
            }
            function AjustarCentavos() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('AjustarCentavos');
            }

            function OnClientshow() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('FacturaEspecial');
            }

            function ModificaBanderaRebind_FacturaEspecial(valor) {
                ////debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind_FacturaEspecial.ClientID %>');
                HD_GridRebind.value = valor;
            }

            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de Precios dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgFacturaDet_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //cuando el campo de texto pirde el foco
            function txtMov_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbMov.ClientID %>'));
            }


            function txtCausaRef_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbCausaRef.ClientID %>'));
            }


            //cuando se selecciona un Item del combo
            function cmbMov_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtMov.ClientID %>'));

                //si es movimiento 70, hace visible la columna 70 del grid
                var itemCombo = eventArgs.get_item();
                if (itemCombo != null)
                    if (itemCombo.get_value() == '70')
                        rgFacturaDet_HiddeColumn(false);
                    else
                        rgFacturaDet_HiddeColumn(true);
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

            //cuando el campo de texto pirde el foco
            function txtRepresentante_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbRepresentante_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRepresentante.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtMoneda_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbMoneda.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbMoneda_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtMoneda.ClientID %>'));
            }

            function txtMoneda_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbCausaRef.ClientID %>'));
            }

            function cmbCausaRef_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCausaRef.ClientID %>'));
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
                ////debugger; 
                OnBlur(sender, cmbTerritorioPartida);
            }


            //cuando el combo de edición del Grid de TerritorioPartida cambia de indice
            function cmbTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtTerritorioPartida);
            }

            //------------------------------------------------------------------------------------//
            //--PARA COMBOS DE ADENDA - EVITAR ESCRITURA------------------------------------------//            
            //------------------------------------------------------------------------------------//

            //Para el combo de ProductosAdenda dentro del Grid
            var txtId_PrdAde;
            var cmbProductoAde;

            function txtId_PrdAde_OnLoad(sender, args) {
                txtId_PrdAde = sender;
            }

            function cmbProductoAde_OnLoad(sender, args) {
                cmbProductoAde = sender;
                var input = cmbProductoAde.get_inputDomElement();
                input.onkeydown = onKeyDownHandler;
            }

            function onKeyDownHandler(e) {
                if (!e)
                    e = window.event;
                var code = e.keyCode || e.which;
                //do not allow any of these chars to be entered: !@#$%^&*()    
                if (code != 9) {
                    e.returnValue = false;
                    if (e.preventDefault) {
                        e.preventDefault();
                    }
                }
            }

            //cuando el campo de texto de ediciÃ³n del Grid de Adenda pierde el foco
            function txtId_PrdAde_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbProductoAde);
            }

            //cuando el combo de ediciÃ³n del Grid de ProductoAdenda cambia de indice
            function cmbProductoAde_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtId_PrdAde);
            }

            //Para el combo de Productos dentro del Grid
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
                ////debugger; 
                OnBlur(sender, cmbProducto);
            }

            //cuando el combo de edición del Grid de producto cambia de indice
            function cmbProducto_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtId_Prd);
            }

            //Para el combo de Cliente Externo dentro del Grid
            var txtClienteExterno;
            var cmbClienteExterno;

            function txtClienteExterno_OnLoad(sender, args) {
                txtClienteExterno = sender;
            }

            function cmbClienteExterno_OnLoad(sender, args) {
                cmbClienteExterno = sender;
            }

            //cuando el campo de texto de edición del Grid de ClienteExterno pirde el foco
            function txtClienteExterno_OnBlur(sender, args) {
                ////debugger; 
                OnBlur(sender, cmbClienteExterno);
            }

            //cuando el combo de edición del Grid de ClienteExterno cambia de indice
            function cmbClienteExterno_ClientSelectedIndexChanged(sender, eventArgs) {
                ////debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtClienteExterno);
            }

            function Cantidad_Blur() {
            }

            function abrirBuscar() {
                // debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtCliente.ClientID%>');
                            var oWnd = radopen("Ventana_Buscar.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarPrecio");
                            oWnd.center();
                        }
                    }
                }
            }

            function abrirEstadistica() {
                // debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtCliente.ClientID%>');
                            var cteNom = $find('<%=txtClienteNombre.ClientID%>');
                            var oWnd = radopen("Ventana_Estadisticas.aspx?cte=" + cte.get_value() + "&cteNom=" + cteNom.get_value(), "AbrirVentana_BuscarPrecio");
                            oWnd.center();
                        }
                    }
                }
            }
            function abrirIndicadores() {
                // debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtCliente.ClientID%>');
                            var oWnd = radopen("Ventana_Indicadores.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarIndicadores");
                            oWnd.center();
                        }
                    }
                }
            }

            //            function popup() {
            //                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
            //                oWnd.center();
            //            }

            function popup(cn) {
                var oWnd;
                if (cn) {
                    oWnd = radopen("Ventana_Buscar.aspx?cn=true", "AbrirVentana_Buscar");
                }
                else {
                    oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                }
                oWnd.center();
            }


            
            function popup_CC(clienteSIAN) {
                var oWnd;
                if (clienteSIAN) {
                    oWnd = radopen("Ventana_Buscar.aspx?ClienteSIAN=" + clienteSIAN  , "AbrirVentana_Buscar");
                }
                else {
                    oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                }
                oWnd.center();
            }

            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }


            function OnClientItemsRequestedHandler(sender, eventArgs) {
                debugger;
                //set the max allowed height of the combo  
                var MAX_ALLOWED_HEIGHT = 220;
                //this is the single item's height  
                var SINGLE_ITEM_HEIGHT = 22;

                var calculatedHeight = sender.get_items().get_count() * SINGLE_ITEM_HEIGHT;

                var dropDownDiv = sender.get_dropDownElement();

                if (calculatedHeight > MAX_ALLOWED_HEIGHT) {
                    setTimeout(function () { dropDownDiv.firstChild.style.height = MAX_ALLOWED_HEIGHT + "px"; }, 20);
                }
                else {
                    setTimeout(function () { dropDownDiv.firstChild.style.height = calculatedHeight + "px"; }, 20);
                }
            }


            function AbrirVentana_AlertaPrecios() {
                var oWnd = radopen("Ventana_AlertaPrecios.aspx", "AbrirVentana_AlertaPrecios");
                oWnd.center();
            }

            function AbrirBuscarDireccionEntrega() {
                var cte = $find('<%=txtCliente.ClientID%>');
                var oWnd = radopen("Ventana_Buscar.aspx?DirEnt=true&cte=" + cte.get_value(), "AbrirVentana_BuscarDireccionEntrega");
                oWnd.setSize(600, 400);
                oWnd.center();
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$rgFactura$ctl00$ctl02$ctl00$ImgExportar") != -1)
                    args.set_enableAjax(false);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_Buscar" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="600px" Height="415px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Buscar" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_BuscarPrecio" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="750px" Height="415px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Buscar" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_BuscarIndicadores" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="False" Width="750px" Height="515px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Buscar" Modal="True">
            </telerik:RadWindow>
                   <telerik:RadWindow ID="AbrirVentana_AlertaPrecios" runat="server"  Behaviors="None"
                Opacity="100" VisibleStatusbar="False" Width="600px" Height="415px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Alerta" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbConsFacEle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMov">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkDesgloce">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="formularioTotales" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRetencion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtDescuento1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtDescuento2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturaDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturaEspecialDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFacturaEspecial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
             </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelaFacturaEspecial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Mail"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Cancelar"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="facEspecial" Value="facEspecial" CssClass="facEspecial"
                ToolTip="Capturar factura especial" ImageUrl="Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="CanfacEspecial" Value="facEspecial" CssClass="undo"
                ToolTip="Cancelar factura especial" ImageUrl="Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            <td> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                <td>
                        <asp:Button ID="BtnAutorizar" runat="server" Text="Autorizar" ToolTip="Autorizar" OnClick="BtnAutorizar_Click" Visible="false" />
                </td>
                <td>
                        <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="false" />
                </td>
            </tr>
        </table>
        <table style="font-family: verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting" OnTabClick="RadTabStrip1_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True" Value="DatosGenerales">
                            </telerik:RadTab>

                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="T" PageViewID="RadPageViewDetalles"
                                Value="Detalles">
                            </telerik:RadTab>
                            
                            <telerik:RadTab runat="server" AccessKey="F" Text="Addenda de &lt;u&gt;f&lt;/u&gt;acturación"
                                PageViewID="rpvAdendaFacturacion" Visible="false" Value="Adenda">
                            </telerik:RadTab>

                            <telerik:RadTab runat="server" AccessKey="R" Text="Addenda de &lt;u&gt;r&lt;/u&gt;efacturación"
                                PageViewID="rpvAdendaRefacturacion" Visible="false"  Value="AdendaRef">
                            </telerik:RadTab>

                            <telerik:RadTab runat="server" AccessKey="N" Text="Facturación de C&lt;u&gt;N&lt;/u&gt;"
                                PageViewID="rpvCuentaNacional" Visible="false" Value="CuentaNacional">
                            </telerik:RadTab>

                            <telerik:RadTab runat="server" AccessKey="D" Text="Addenda &lt;u&gt;C&lt;/u&gt;uenta Nacional"
                                PageViewID="rpvAdendaCuentaNacional" Visible="false" Value="AdendaCuentaNac">
                            </telerik:RadTab>
                             <%-- 
                                Cambio para facturas especiales 
                                26-05-2015
                                Raul Bórquez Martínez
                            --%>
                            <telerik:RadTab runat="server" AccessKey="E" Text="Facturas Especiales"
                                PageViewID="rpvFacturasEspeciales" Value="FacEspeciales" visible = "false">
                            </telerik:RadTab>
                            <%-- Fin  de codigo de Facturas especiales --%>

                        </Tabs>
                    </telerik:RadTabStrip>

                    <%--SE CREAN LAS PESTAÑAS DE LA PAGINA, CAB FACTURA, DETALLES, ADENDA Y REF_ADENDA --%>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="500px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                                    <%--<asp:Panel ID="Panel1" runat="server">--%>
                                    <div id="formularioDatosGenerales" runat="server">
                                        <table border="0">
                                            <tr>
                                                <td width="120">
                                                    <asp:Label ID="lblNumero" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtId" runat="server" Width="70px" MaxLength="9" MinValue="1"
                                                        Enabled="false">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                    <br />
                                                </td>
                                                <td width="129">
                                                    <asp:Label ID="lblConsFacEle" runat="server" Text="Serie del consecutivo"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbConsFacEle" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" Width="167px" DataValueField="Id" Filter="Contains"
                                                        MarkFirstMatch="true" AutoPostBack="True" OnClientBlur="Combo_ClientBlur" OnSelectedIndexChanged="cmbConsFacEle_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_cmbConsFacEle" runat="server" ControlToValidate="cmbConsFacEle"
                                                        ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px">
                                                        <DatePopupButton ToolTip="Abrir calendario" />
                                                        <Calendar ID="cal_txtFecha" runat="server">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput MaxLength="10" runat="server">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="val_txtFecha" runat="server" ControlToValidate="txtFecha"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelHora" runat="server" Text="Hora"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtHora" runat="server" MaxLength="50" Width="70px" Enabled="False">                                                        
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTMov" runat="server" Text="T. mov."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMov" runat="server" Width="70px" MaxLength="9"
                                                        MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnBlur="txtMov_OnBlur" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadComboBox ID="cmbMov" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        Filter="Contains" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo"
                                                        OnClientSelectedIndexChanged="cmbMov_ClientSelectedIndexChanged" OnSelectedIndexChanged="cmbMov_SelectedIndexChanged"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                        Width="300px" LoadingMessage="Cargando...">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
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
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblReq" runat="server" Text="Req."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtReq" runat="server" MaxLength="50" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                </tr>

                                                                      
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="120">
                                                    <asp:Label ID="lblNumeroPedido" runat="server" Text="Número de pedido"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPedido" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPedido" runat="server" Text="Pedido"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtPedidoDesc" runat="server" MaxLength="50" Width="250px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
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
                                                <td>
                                                      &nbsp;
                                                </td>
                                                                                                <td>
                                                    &nbsp;
                                                </td>
                                               <td>
                                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                                </td>
                                              <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUnidadesGarantia" runat="server" Text="Unidades de Garantia:"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtUnidadesGarantia" runat="server" Width="70px" MaxLength="9"
                                                                 MinValue="1">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="120">
                                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtCliente" runat="server" AutoPostBack="true" MaxLength="9"
                                                        MinValue="1" OnTextChanged="txtCliente_TextChanged" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" ReadOnly="True" Width="295px">
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
                                                    <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTerritorio_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="true" MaxHeight="300px" OnClientBlur="Combo_ClientBlur"
                                                        OnClientFocus="_PreValidarFechaEnPeriodo" OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged" Width="300px">
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
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRepresentante" runat="server" Text="Representante"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtRepresentanteStr" runat="server" Enabled="false" Width="295px">
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
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblContacto" runat="server" Text="Contacto"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadTextBox ID="txtContacto" runat="server" MaxLength="40" Width="325px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                             <tr>
                                                <td style="height: 50px">
                                                    <asp:CheckBox ID="chkFacturarCuentaNacional" runat="server" Text="Facturar Cuenta Nacional" OnCheckedChanged="chkFacturarCuentaNacional_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </td>
                                                <td style="height: 50px">
                                                    <asp:CheckBox ID="ChkRefacturatotal"   runat="server" Text="Re Facturar Totalidades" OnCheckedChanged="ChkRefacturatotal_CheckedChanged"    ValidationGroup = "Refactura" AutoPostBack="true" CausesValidation="True" />
                                                </td>
                                                <td style="height: 50px">
                                                    <asp:CheckBox ID="ChkRefacturaparcial" runat="server" Text="Re Factura Parcialidades" OnCheckedChanged="ChkRefacturaparcial_CheckedChanged" ValidationGroup = "Refactura" AutoPostBack="true" />
                                                </td>
                                                     <td>
                                                     <table>
                                                       <tr> 
                                                         <td>
                                                             <asp:Label ID="lblCausaRefactura" runat="server" Text="Causa"></asp:Label>
                                                         </td>
                                                       
                                                         <td>
                                                            <telerik:RadNumericTextBox ID="txtCausaRef" runat="server" MaxLength="9" MinValue="1" Width="40px">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                            <ClientEvents OnBlur="txtCausaRef_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                            </telerik:RadNumericTextBox>
                                                         </td>
                                                         <td colspan = "2">
                                                            <telerik:RadComboBox ID="cmbCausaRef" runat="server" ChangeTextOnKeyBoardNavigation="true" AutoPostBack="True"
                                                                            DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true" MarkFirstMatch="true"
                                                                            OnClientFocus="_PreValidarFechaEnPeriodo" OnClientBlur="Combo_ClientBlur"
                                                                            OnClientSelectedIndexChanged="cmbCausaRef_ClientSelectedIndexChanged" 
                                                                            OnSelectedIndexChanged="cmbCausaRef_SelectedIndexChanged"                                                                       
                                                                            Width="260px">
                                                            </telerik:RadComboBox>
                                                         
                                                      <%--  <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando..." MarkFirstMatch="true" MaxHeight="300px" OnClientBlur="Combo_ClientBlur"
                                                        OnClientFocus="_PreValidarFechaEnPeriodo" OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged" Width="300px">--%>
                                                         
                                                         
                                                         </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                             </tr>    
                                            <tr>
                                                <td style="height: 35px">
                                                    <asp:CheckBox ID="chkDesgloce" runat="server" Text="Desglose del I.V.A." OnCheckedChanged="chkDesgloceIva_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </td>
                                                <td style="height: 35px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                            <asp:CheckBox ID="chkRetencion" runat="server" Text="Retención del I.V.A. " OnCheckedChanged="chkRetencion_CheckedChanged"
                                                            AutoPostBack="true" />
                                                            <telerik:RadNumericTextBox ID="txtPorcRetencion" runat="server" MinValue="0" Width="40px"
                                                            MaxLength="5" MaxValue="100" ReadOnly="True" > 
                                                            <NumberFormat DecimalDigits="2" />                                                            
                                                            </telerik:RadNumericTextBox> %
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTipoMoneda" runat="server" Text="Tipo de moneda"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtMoneda" runat="server" MaxLength="9" MinValue="1"
                                                                    Width="70px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnBlur="txtMoneda_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="cmbMoneda" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                    OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo" OnClientSelectedIndexChanged="cmbMoneda_ClientSelectedIndexChanged"
                                                                    Width="150px">
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="val_txtMoneda" runat="server" ControlToValidate="txtMoneda"
                                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="45">
                                                    <asp:Label ID="lblCalle" runat="server" Text="Calle"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCalle" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo"  />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalleNumero" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCalleNumero" runat="server" MaxLength="15" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                 <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Número Interior"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCalleNumeroInterior" runat="server" MaxLength="15" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCP" runat="server" Text="C.P."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCP" runat="server" MaxLength="10" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox>
                                                </td>
                                           
                                                <td>
                                                    <asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImgBuscarDireccionEntrega_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="45">
                                                    <asp:Label ID="lblColonia" runat="server" Text="Colonia"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtColonia" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo"  />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMunicipio" runat="server" Text="Municipio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtMunicipio" runat="server" MaxLength="40" Width="150px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtEstado" runat="server" MaxLength="20" Width="150px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="45">
                                                    <asp:Label ID="lblRFC" runat="server" Text="R.F.C."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtRFC" runat="server" MaxLength="13" Width="200px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtRFC" runat="server" ControlToValidate="txtRFC"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTel" runat="server" Text="Tel."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtTelefono" runat="server" MaxLength="20" Width="120px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCondiciones" runat="server" Text="Condiciones de pago"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCondiciones" runat="server" MaxLength="70" Width="70px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="val_txtCondiciones" runat="server" ControlToValidate="txtCondiciones"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCondiciones0" runat="server" Text="Forma de pago"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbFormaPago" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                        OnClientBlur="Combo_ClientBlur" OnClientFocus="_PreValidarFechaEnPeriodo" OnSelectedIndexChanged="cmbConsFacEle_SelectedIndexChanged"
                                                        Width="167px" EmptyMessage="Seleccione cliente" LoadingMessage="Cargando...">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCondiciones1" runat="server" Text="Últimos 4 dígitos de la cuenta"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtUDigitos" runat="server" MaxLength="50" Width="80px">   
                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />                                                                                                                                   
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td width="110">
                                                    <asp:Label ID="lblOrdenEntrega" runat="server" Text="Orden de entrega"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtOrden" runat="server" Width="70px" MaxLength="10">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloNumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNumeroGuia" runat="server" Text="Número de guía"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtNumeroGuia" runat="server" MaxLength="9" MinValue="0"
                                                                    Width="70px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 55px;">
                                                                <asp:Label ID="lblConducto" runat="server" Text="Conducto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtConducto" runat="server" MaxLength="50" Width="120px">
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                                </telerik:RadTextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblNumEntrega" runat="server" Text="Núm. de entrega"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtNumeroEntrega" runat="server" MaxLength="9" MinValue="0"
                                                                    Width="70px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                                <br />
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescuento1" runat="server" Text="Descuento 1"></asp:Label>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtDescuento1" runat="server" AutoPostBack="true"
                                                                    MaxLength="9" MinValue="0" MaxValue="100" OnTextChanged="txtDescuento1_TextChanged"
                                                                    Width="70px" Value="0.00">
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSigno1" runat="server" Text="%"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtDescPorc1" runat="server" MaxLength="50" Width="200px">
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNotas" runat="server" Text="Notas"></asp:Label>
                                                </td>
                                                <td rowspan="2" valign="top">
                                                    <telerik:RadTextBox ID="txtNotas" runat="server" Height="45px" MaxLength="500" TextMode="MultiLine"
                                                        Width="300px">
                                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescuento2" runat="server" Text="Descuento 2"></asp:Label>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtDescuento2" runat="server" AutoPostBack="true"
                                                                    MaxLength="9" MinValue="0" MaxValue="100" OnTextChanged="txtDescuento2_TextChanged"
                                                                    Width="70px" Value="0.00">
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSigno2" runat="server" Text="%"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtDescPorc2" runat="server" MaxLength="50" Width="200px">
                                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%--</asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>

                        <%--CREAMOS LA PESTAÑA 2 DETALLES DE LA FACTURA--%>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <asp:HiddenField ID="HD_Prd_UniEmp" runat="server" />
                            <asp:HiddenField ID="HD_Prd_InvFinal" runat="server" />
                            <asp:HiddenField ID="HD_Prd_Asignado" runat="server" />
                            <asp:HiddenField ID="HD_Prd_Disponible" runat="server" />
                            <asp:HiddenField ID="HD_Amortizacion" runat="server" />
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="455px" OnClientResized="onResize"
                                    BorderStyle="None">
                                    <%--<asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="810px">--%>
                                    <telerik:RadGrid ID="rgFacturaDet" runat="server" GridLines="None" AllowPaging="false"
                                        AutoGenerateColumns="False" OnNeedDataSource="rgFacturaDet_NeedDataSource" OnInsertCommand="rgFacturaDet_InsertCommand"
                                        OnUpdateCommand="rgFacturaDet_UpdateCommand" OnDeleteCommand="rgFacturaDet_DeleteCommand"
                                        OnItemDataBound="rgFacturaDet_ItemDataBound" OnItemCommand="rgFacturaDet_ItemCommand"
                                        OnPageIndexChanged="rgFacturaDet_PageIndexChanged" BorderStyle="None" DataMember="listaOrdCompraDet"
                                        OnItemCreated="rgFacturaDet_ItemCreated">
                                        <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Fac,Id_FacDet,Id_Prd,Id_CteExt,Id_Ter,Id_Rem,Rem_Cant"
                                            EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                            NoMasterRecordsText="No se encontraron registros." PageSize="9">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Fac" HeaderText="Id_Fac" UniqueName="Id_Fac"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_FacDet" HeaderText="Id_FacDet" UniqueName="Id_FacDet"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Id_Rem" UniqueName="Id_Rem"
                                                    ReadOnly="true" Display="false">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_CteExtN" UniqueName="Id_CteExtN"
                                                    Display="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClienteExternoNum" runat="server" Text='<%# Eval("Id_CteExt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtClienteExterno" runat="server" Width="50px" MaxLength="9"
                                                            Text='<%# Eval("Id_CteExt") %>' OnTextChanged="txtClienteExterno_TextChanged"
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtClienteExterno_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cliente externo" DataField="Id_CteExt" UniqueName="Id_CteExt"
                                                    Display="false">
                                                    <HeaderStyle Width="280px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClienteExterno" runat="server" Text='<%# Eval("Id_CteExtStr") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="border-bottom-color: Transparent">
                                                                    <asp:Label ID="lblTxtClienteExterno" runat="server" ForeColor="#FF0000"></asp:Label>
                                                                </td>
                                                                <td style="border-bottom-color: Transparent">
                                                                    <telerik:RadTextBox ID="txtNombreCliente" runat="server" ReadOnly="true" Width="280px"
                                                                        Text='<%# Bind("Id_CteExtStr") %>'>
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Ter" UniqueName="Id_TerN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblTerritorioPartidaNum" runat="server" Text='<%# Eval("Id_Ter")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtTerritorioPartida" runat="server" Width="50px"
                                                            MaxLength="9" Text='<%# Eval("Id_Ter") %>' AutoPostBack="true" OnTextChanged="txtTerritorio_TextChanged">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtTerritorioPartida_OnBlur" OnLoad="txtTerritorioPartida_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Territorio" DataField="Id_Ter" UniqueName="Id_Ter">
                                                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritorioPartida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_TerStr") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbltxtTerritorioPartida" runat="server" ForeColor="#FF0000" Visible="false" />
                                                        <telerik:RadComboBox ID="cmbTerritorioPartida" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                            MarkFirstMatch="true" LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbTerritorioPartida_ClientSelectedIndexChanged"
                                                            OnClientLoad="cmbTerritorioPartida_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px"
                                                            Width="100%" EnableLoadOnDemand="true" OnClientBlur="Combo_ClientBlur"  >
                                                            <ExpandAnimation Type="none" />
                                                            <CollapseAnimation Type="none" />
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() %>' />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN"
                                                    DataType="System.Int32">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="1" Text='<%# Eval("Id_Prd") %>' OnTextChanged="txtProducto_TextChanged"
                                                            AutoPostBack="true" OnLoad="txtProducto_Load">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnLoad="txtId_Prd_OnLoad" OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                    <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId_Prd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000" Visible="false"></asp:Label>
                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="100%"
                                                            Text='<%# Bind("Prd_Descripcion") %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Prd_Descripcion"
                                                    UniqueName="Prd_Descripcion" Display="false">
                                                    <HeaderStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# Eval("Prd_Descripcion").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox Width="0px" ID="txtPrd_Descripcion" runat="server" ReadOnly="true"
                                                            Text='<%# Eval("Prd_Descripcion").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Pres." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# Eval("Prd_Presentacion").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox Width="100%" ID="txtPrd_Presentacion" runat="server" ReadOnly="true"
                                                            Text='<%# Eval("Prd_Presentacion").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Unidades" DataField="Prd_UniNe" UniqueName="Prd_UniNe">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrd_UniNe" runat="server" Text='<%# Eval("Prd_UniNe").ToString() %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtPrd_UniNe" runat="server" Width="100%" ReadOnly="true"
                                                            Text='<%# Eval("Prd_UniNe").ToString() %>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cantidad" DataField="Fac_Cant" UniqueName="Fac_Cant">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrd_Cantidad" runat="server" Text='<%# Eval("Fac_Cant") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtFac_Cantidad" runat="server" Width="100%" MaxLength="9"
                                                            Text='<%# Eval("Fac_Cant") %>' OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:Label ID="lblVal_txtFac_Cantidad" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Rem_Cant" HeaderText="Rem_Cant" UniqueName="Rem_Cant"
                                                    Display="false" ReadOnly="true">
                                                    <HeaderStyle Width="0px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="Fac_Precio" HeaderText="Precio" UniqueName="Fac_Precio">
                                                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFac_Precio" runat="server" Text='<%# Bind("Fac_Precio", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtFac_Precio" runat="server" Width="100%" MaxLength="9"
                                                            MinValue="0" Text='<%# Eval("Fac_Precio") %>'>
                                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Fac_Importe" HeaderText="Importe" UniqueName="Fac_Importe">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFac_Importe" runat="server" Text='<%# Bind("Fac_Importe", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblFac_ImporteEdit" runat="server" Text='<%# Bind("Fac_Importe", "{0:N2}") %>' />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>

                                               <telerik:GridTemplateColumn  HeaderText="Multiplicador" UniqueName="Multiplicador">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Multiplicador", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                              <telerik:GridTemplateColumn  HeaderText="Precio de venta" UniqueName="Precio_venta">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Precio_Venta", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                               <telerik:GridTemplateColumn  HeaderText="Totales" UniqueName="Totales">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Totales", "{0:N2}") %>' />
                                                    </ItemTemplate>
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

                                    <div id="botonFacturaEspecial" runat="server" style="text-align: right; margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="btnFacturaEspecial" runat="server" Visible="false" Text="Capturar factura especial"
                                            OnClientClick="AbrirVentana_FacturaEspecial()" />
                                    </div>
                                    <div id="BotonCancelaEspecial" runat="server" style="text-align: right; margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="btnCancelaFacturaEspecial" runat="server" Visible="false" Text="Eliminar factura especial"
                                            OnClientClick="CerrarVentana_FacturaEspecial()" />
                                    </div>
                                    <div id="formularioTotales" runat="server">
                        <table width="90%">
                            <tr>
                                <td>
                                </td>
                                <td width="70%">
                                </td>
                                <td>
                                    <asp:Label ID="lblImporte" runat="server" Text="Importe"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtImporte" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblSubtotal" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtSubTotal" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="IVA">
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblIVA" runat="server" Text="I.V.A."></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIVA" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                                    <%--</asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <%--CREAMOS LA PESTAÑA 3 ADENDA--%>
                        <telerik:RadPageView ID="rpvAdendaFacturacion" runat="server" Height="450px">
                            <telerik:RadGrid ID="rgAdendaFacturacion" runat="server" AutoGenerateColumns="False"
                                GridLines="None" OnNeedDataSource="rgAdendaFacturacion_NeedDataSource" Width="100%"
                                BorderStyle="None" Height="35%">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" ScrollHeight="100px" SaveScrollPosition="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
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
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <%--INSERTAMOS NUEVO GRID PARA DETALLES DE ADENDA--%>
                            <telerik:RadSplitter ID="RadSplitter3" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane3" runat="server" Height="455px" OnClientResized="onResize"
                                    BorderStyle="None">   
                                    <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Auto" HorizontalAlign="Justify">                              
                                        <telerik:RadGrid ID="rgFacturaDetAde" runat="server" GridLines="None" AllowPaging="false" Width="100%" 
                                        AutoGenerateColumns="False" OnNeedDataSource="rgFacturaDetAde_NeedDataSource" OnInsertCommand="rgFacturaDetAde_InsertCommand"
                                        OnUpdateCommand="rgFacturaDetAde_UpdateCommand" OnDeleteCommand="rgFacturaDetAde_DeleteCommand"
                                        OnItemDataBound="rgFacturaDetAde_ItemDataBound" OnItemCommand="rgFacturaDetAde_ItemCommand" 
                                        OnPageIndexChanged="rgFacturaDetAde_PageIndexChanged" BorderStyle="None"
                                        OnItemCreated="rgFacturaDetAde_ItemCreated" >   
                                                                                                                                                        
                                            <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Cons_AdeDet,Id_Prd"
                                            EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                            NoMasterRecordsText="No se encontraron registros." PageSize="9">                                            
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <Columns>  
                                              <telerik:GridTemplateColumn HeaderText="Consecutivo" DataField="Id_Cons_AdeDet"  UniqueName="Id_Cons_AdeDet" Visible="false" >
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Id_Cons_AdeDet" runat="server" Text='<%# Eval("Id_Cons_AdeDet")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                       
                                                        <telerik:RadTextBox ID="txtId_Cons_AdeDet" runat="server" Width="50px" 
                                                           Text='<%# Eval("Id_Cons_AdeDet").ToString() %>'  >                                                                                                                                                                               
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                                                                                                                                                                                                                                                                                                                                              
                                            									
													<telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblIdProducto" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_PrdAde" runat="server" Width="50px"
                                                            MaxLength="9" Text='<%# Eval("Id_Prd") %>'  >
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtId_PrdAde_OnBlur" OnLoad="txtId_PrdAde_OnLoad" />                                                            
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                               

                                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcionProducto" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                       
                                                        <telerik:RadComboBox ID="cmbProductoAde" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                             LoadingMessage="Cargando..."    OnClientSelectedIndexChanged="cmbProductoAde_ClientSelectedIndexChanged" 
                                                            OnClientLoad="cmbProductoAde_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px" Width="100%" 
                                                             MarkFirstMatch="true"  EnableLoadOnDemand="true"  OnClientBlur="Combo_ClientBlur">
                                                            <ExpandAnimation Type="none" />
                                                            <CollapseAnimation Type="none" />
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>                                                                    
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
                                                                            <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() %>' />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
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
                                              <Scrolling AllowScroll="True" ScrollHeight="180px" SaveScrollPosition="true" UseStaticHeaders="true" 
                                               />
                                        </ClientSettings>                                        
                                    </telerik:RadGrid>
                                    <div id="Div1" runat="server" style="text-align: right; margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="Button1" runat="server" Visible="false" Text="Capturar factura especial"
                                            OnClientClick="AbrirVentana_FacturaEspecial()" />
                                    </div>
                            <%--TERMINAMOS NUEVO GRID PARA DETALLES DE ADENDA--%>
                            </asp:Panel> 
                            </telerik:RadPane>
                            </telerik:RadSplitter>
                            </telerik:RadPageView>
                            <%--CREAMOS LA PESTAÑA 4 REFACT_ADENDA--%>                        
                        <telerik:RadPageView ID="rpvAdendaRefacturacion" runat="server" Height="450px">
                            <telerik:RadGrid ID="rgAdendaReFacturacion" runat="server" AutoGenerateColumns="False"
                                GridLines="None" OnNeedDataSource="rgAdendaReFacturacion_NeedDataSource" Width="100%"
                                BorderStyle="None" Height="100%">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" ScrollHeight="100px" SaveScrollPosition="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
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
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpvCuentaNacional" runat="server">
                            <telerik:RadSplitter ID="RadSplitter10" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane runat="server" ID="RCuentaNacional">
                                    <table>
                                        <tr>
                                            <td width="120">
                                                <asp:Label ID="LblClienteNacional" runat="server" Text="Cliente"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtClienteNacional" runat="server" AutoPostBack="true" MaxLength="9"
                                                    MinValue="0" OnTextChanged="txtClienteNacional_TextChanged">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtClienteNacionalNombre" runat="server" ReadOnly="True" Width="295px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
<%--                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClienteNacional"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                </asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImgBuscarClienteNacional" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImgBuscarClienteNacional_Click"
                                                    ToolTip="Buscar" ValidationGroup="buscar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" colspan="8">
                                                <table>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalCalle" runat="server" Text="Calle"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalCalle" runat="server" ReadOnly="true"></telerik:RadTextBox>
                                                            <telerik:RadTextBox ID="txtClienteNacionalFiscal" runat="server" Visible="false"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalNoExterior" runat="server" Text="No. Exterior"></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalNoExterior" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalColonia" runat="server" Text="Colonia"></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalColonia" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalMunicipio" runat="server" Text="Municipio"></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalMunicipio" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalEstado" runat="server" Text="Estado"></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalEstado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalCp" runat="server" Text="C.P."></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalCp" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalRfc" runat="server" Text="RFC"></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalRfc" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionalAdenda" runat="server" Text="Addenda"></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalAdenda" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <table>
                                                    <tr>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalConsignar1" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalConsignar2" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalConsignar3" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalConsignar4" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalConsignar5" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td><asp:Label ID="LblClienteNacionEmail" runat="server" Text="Email"></asp:Label></td>
                                                        <td><telerik:RadTextBox ID="TxtClienteNacionalEmail" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvAdendaCuentaNacional" runat="server" Height="450px">
                            <telerik:RadGrid ID="rgAdendaFacturacionNacional" runat="server" AutoGenerateColumns="False"
                                GridLines="None" OnNeedDataSource="rgAdendaFacturacionNacional_NeedDataSource" Width="100%"
                                BorderStyle="None" Height="35%">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" ScrollHeight="100px" SaveScrollPosition="true" UseStaticHeaders="true" />
                                </ClientSettings>
                                <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
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
                                                <telerik:RadTextBox ID="RadTextBox11" runat="server" Width="200px" Text='<%# Bind("valor") %>'
                                                    MaxLength='<%# Bind("Longitud") %>'>
                                                    <ClientEvents OnKeyPress="SinComillas" />
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <%--INSERTAMOS NUEVO GRID PARA DETALLES DE ADENDA--%>
                            <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane4" runat="server" Height="455px" OnClientResized="onResize"
                                    BorderStyle="None">   
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" HorizontalAlign="Justify">                              
                                        <telerik:RadGrid ID="rgFacturaDetAdeNacional" runat="server" GridLines="None" AllowPaging="false" Width="100%" 
                                        AutoGenerateColumns="False" OnNeedDataSource="rgFacturaDetAdeNacional_NeedDataSource" OnInsertCommand="rgFacturaDetAdeNacional_InsertCommand"
                                        OnUpdateCommand="rgFacturaDetAdeNacional_UpdateCommand" OnDeleteCommand="rgFacturaDetAdeNacional_DeleteCommand"
                                        OnItemDataBound="rgFacturaDetAdeNacional_ItemDataBound" OnItemCommand="rgFacturaDetAdeNacional_ItemCommand" 
                                        OnPageIndexChanged="rgFacturaDetAde_PageIndexChanged" BorderStyle="None"
                                        OnItemCreated="rgFacturaDetAdeNacional_ItemCreated" >   
                                                                                                                                                        
                                            <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Cons_AdeDet,Id_Prd"
                                            EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                            NoMasterRecordsText="No se encontraron registros." PageSize="9">                                            
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                            <Columns>  
                                              <telerik:GridTemplateColumn HeaderText="Consecutivo" DataField="Id_Cons_AdeDet"  UniqueName="Id_Cons_AdeDet" Visible="false" >
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_Id_Cons_AdeDetNacional" runat="server" Text='<%# Eval("Id_Cons_AdeDet")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                       
                                                        <telerik:RadTextBox ID="txtId_Cons_AdeDetNacional" runat="server" Width="50px" 
                                                           Text='<%# Eval("Id_Cons_AdeDet").ToString() %>'  >                                                                                                                                                                               
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                                                                                                                                                                                                                                                                                                                                              
                                            									
													<telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblIdProductoNacional" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_PrdAdeNacional" runat="server" Width="50px"
                                                            MaxLength="9" Text='<%# Eval("Id_Prd") %>'  >
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtId_PrdAde_OnBlur" OnLoad="txtId_PrdAde_OnLoad" />                                                            
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>                                               

                                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcionProductoNacional" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>                                                       
                                                        <telerik:RadComboBox ID="cmbProductoAdeNacional" runat="server" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                                             LoadingMessage="Cargando..."    OnClientSelectedIndexChanged="cmbProductoAde_ClientSelectedIndexChanged" 
                                                            OnClientLoad="cmbProductoAde_OnLoad" HighlightTemplatedItems="true" MaxHeight="300px" Width="100%" 
                                                             MarkFirstMatch="true"  EnableLoadOnDemand="true"  OnClientBlur="Combo_ClientBlur">
                                                            <ExpandAnimation Type="none" />
                                                            <CollapseAnimation Type="none" />
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>                                                                    
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() %>' />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
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
                                              <Scrolling AllowScroll="True" ScrollHeight="180px" SaveScrollPosition="true" UseStaticHeaders="true" 
                                               />
                                        </ClientSettings>                                        
                                    </telerik:RadGrid>
                                    <div id="Div2" runat="server" style="text-align: right; margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="Button2" runat="server" Visible="false" Text="Capturar factura especial"
                                            OnClientClick="AbrirVentana_FacturaEspecial()" />
                                    </div>
                            <%--TERMINAMOS NUEVO GRID PARA DETALLES DE ADENDA--%>
                            </asp:Panel> 
                            </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                         <%--COMIENZA LA PESTAÑA DE FACTURAS ESPECIALES --%>
                        <telerik:RadPageView ID="rpvFacturasEspeciales" runat="Server">
                            <telerik:RadSplitter ID="RadSplitter7" runat="server" Height="600px" ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane7" runat="server" Height="600px" BorderStyle="None" OnClientResized="onResize" ScrollBars="Auto">
                                    <telerik:RadGrid ID="rgFacturaEspecialDet" runat="server" GridLines="None" AllowPaging="false" AutoGenerateColumns="False" BorderStyle="None"  
                                    OnNeedDataSource="rgFacturaEspecialDet_NeedDataSource" OnInsertCommand="rgFacturaEspecialDet_InsertCommand" OnUpdateCommand="rgFacturaEspecialDet_UpdateCommand" OnDeleteCommand="rgFacturaEspecialDet_DeleteCommand" OnItemDataBound="rgFacturaEspecialDet_ItemDataBound"
                                    OnItemCommand="rgFacturaEspecialDet_ItemCommand" OnPageIndexChanged="rgFacturaEspecialDet_PageIndexChanged">
                                        <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Fac,Id_FacDet,Id_Prd"
                                        EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" AutoGenerateColumns="False"
                                        NoMasterRecordsText="No se encontraron registros." PageSize="15">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Id_Rem" UniqueName="Id_Rem"
                                                ReadOnly="true" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_RemDet" HeaderText="Id_RemDet" UniqueName="Id_RemDet"
                                                ReadOnly="true" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Prod." DataField="Id_Prd" UniqueName="Id_Prd">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_Prd_Esp" runat="server" Text='<%# Eval("Id_Prd").ToString() %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtId_Prd_Esp" runat="server" Width="100%" MaxLength="9"
                                                        Text='<%# Eval("Id_Prd") %>'>
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtId_Prd_OnBlur" OnLoad="txtId_Prd_OnLoad" />
                                                    </telerik:RadNumericTextBox>
                                                    <asp:Label ID="lbl_cmbProducto_Esp" runat="server" ForeColor="#FF0000" Width="0px"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                <HeaderStyle Width="220px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_PrdStr" runat="server" Text='<%# ObtenerDescripcion(Container.DataItem)  %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="cmbProducto" runat="server" Width="100%" Filter="Contains"
                                                        AutoPostBack="true" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                        OnClientBlur="Combo_ClientBlur" DataTextField="Prd_Descripcion" DataValueField="Id_Prd"
                                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" OnClientSelectedIndexChanged="cmbProducto_ClientSelectedIndexChanged"
                                                        OnClientLoad="cmbProducto_OnLoad">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="LblID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Prd").ToString() %>' />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="LblDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Clave especial" DataField="Id_PrdEsp" UniqueName="Id_PrdEsp">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_PrdEsp_Esp" runat="server" Text='<%# ObtenerIdEspecial(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtId_PrdEsp" runat="server" Width="100%" MaxLength="50"
                                                        Text='<%# ObtenerIdEspecial(Container.DataItem) %>'>
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                    <asp:Label ID="lblId_PrdEsp" runat="server" ForeColor="#FF0000" Width="0px"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Prd_Descripcion"
                                                UniqueName="Prd_Descripcion">
                                                <HeaderStyle Width="170px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# ObtenerDescripcionEspecial(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtPrd_Descripcion_Esp" runat="server" 
                                                        Text='<%# ObtenerDescripcionEspecial(Container.DataItem)  %>' Width="100%">
                                                        <ClientEvents OnKeyPress="SinComillas" />
                                                    </telerik:RadTextBox>
                                                    <asp:Label ID="lblVal_txtPrd_Descripcion_Esp" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Pres." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# ObtenerPresentacion(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtPrd_Presentacion_Esp" runat="server" onpaste="return false"
                                                        Text='<%# ObtenerPresentacion(Container.DataItem) %>' Width="100%">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                    <asp:Label ID="lblVal_txtPrd_Presentacion" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Unidades" DataField="Prd_UniNe" UniqueName="Prd_UniNe">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# ObtenerUnidades(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtPrd_UniNe_Esp" runat="server" onpaste="return false" Text='<%# ObtenerUnidades(Container.DataItem) %>'
                                                        Width="100%">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                    <asp:Label ID="lblVal_txtPrd_UniNe_Esp" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Cant." DataField="Fac_CantE" UniqueName="Fac_CantE">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("Fac_CantE") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtRem_Cantidad_Esp" runat="server" Width="100%" MaxLength="9"
                                                        MinValue="0" Text='<%# Eval("Fac_CantE") %>'>                                                        
                                                    </telerik:RadNumericTextBox>
                                                    <asp:Label ID="lblVal_txtRem_Cantidad_Esp" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="Rem_Precio" HeaderText="Precio" UniqueName="Fac_Precio">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRem_Precio" runat="server" Text='<%# Eval("Fac_Precio", "{0:N2}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtRem_Precio_Esp" runat="server" Width="100%" MaxLength="9"
                                                        MinValue="0" Text='<%# Eval("Fac_Precio") %>'>
                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                    <asp:Label ID="lblVal_txtRem_Precio_Esp" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="Fac_Importe" HeaderText="Importe" UniqueName="Fac_Importe">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRem_Importe" runat="server" Text='<%# Eval("Fac_ImporteE", "{0:N2}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblRem_ImporteEdit" runat="server" Text='<%# Eval("Fac_ImporteE", "{0:N2}") %>' />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn DataField="Clp_Release" HeaderText="Release" UniqueName="Clp_Release">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClp_Release" runat="server" Text='<%# Eval("Clp_Release") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtClp_ReleaseEdit" runat="server" onpaste="return false"
                                                        MaxLength="100" Text='<%# Eval("Clp_Release") %>' Width="100%">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>



                                            <telerik:GridTemplateColumn HeaderText="SAT - Productos y Servicios" DataField="ProdServ" UniqueName="ProdServ" GroupByExpression="ProdServ">
                                                <HeaderStyle Width="140px" HorizontalAlign="Center" />
                                                <ItemStyle Width="140px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("Fac_ClaveProdServ") %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="cmbProdServ" runat="server" Width="120px" Filter="Contains"
                                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnSelectedIndexChanged="cmbTipo_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <Items>
<telerik:RadComboBoxItem Text="01010101 - No existe en el catálogo" Value= "01010101" />
<telerik:RadComboBoxItem Text="10191500 - Pesticidas o repelentes de plagas" Value= "10191500" />
<telerik:RadComboBoxItem Text="10191700 - Dispositivos para control de plagas" Value= "10191700" />
<telerik:RadComboBoxItem Text="12161803 - Aerosoles" Value= "12161803" />
<telerik:RadComboBoxItem Text="14111701 - Pañuelos faciales" Value= "14111701" />
<telerik:RadComboBoxItem Text="14111702 - Cubiertos de asientos de sanitario" Value= "14111702" />
<telerik:RadComboBoxItem Text="14111703 - Toallas de papel" Value= "14111703" />
<telerik:RadComboBoxItem Text="14111704 - Papel higiénico" Value= "14111704" />
<telerik:RadComboBoxItem Text="14111705 - Servilletas de papel" Value= "14111705" />
<telerik:RadComboBoxItem Text="15121500 - Preparados lubricantes" Value= "15121500" />
<telerik:RadComboBoxItem Text="15121514 - Lubricantes espray" Value= "15121514" />
<telerik:RadComboBoxItem Text="15121800 - Anticorrosivos" Value= "15121800" />
<telerik:RadComboBoxItem Text="15121807 - Anticongelante" Value= "15121807" />
<telerik:RadComboBoxItem Text="23181506 - Maquinas de lavado" Value= "23181506" />
<telerik:RadComboBoxItem Text="24122000 - Botellas" Value= "24122000" />
<telerik:RadComboBoxItem Text="26111702 - Pilas alcalinas" Value= "26111702" />
<telerik:RadComboBoxItem Text="27112913 - Lubricador de aceite" Value= "27112913" />
<telerik:RadComboBoxItem Text="31132100 - Forjas de acero" Value= "31132100" />
<telerik:RadComboBoxItem Text="31191500 - Abrasivos y medios de abrasivo" Value= "31191500" />
<telerik:RadComboBoxItem Text="40101601 - Secadores" Value= "40101601" />
<telerik:RadComboBoxItem Text="40141742 - Atomizadores" Value= "40141742" />
<telerik:RadComboBoxItem Text="40151505 - Bombas dosificadoras" Value= "40151505" />
<telerik:RadComboBoxItem Text="40151724 - Partes de repuesto para bombas dosificadoras" Value= "40151724" />
<telerik:RadComboBoxItem Text="41104210 - Disolventes" Value= "41104210" />
<telerik:RadComboBoxItem Text="41112200 - Instrumentos de medida de temperatura y calor" Value= "41112200" />
<telerik:RadComboBoxItem Text="41113035 - Tiras o papeles para pruebas químicas" Value= "41113035" />
<telerik:RadComboBoxItem Text="42281700 - Soluciones y equipo de limpieza pre- esterilización" Value= "42281700" />
<telerik:RadComboBoxItem Text="42281711 - Desincrustadores de esterilización" Value= "42281711" />
<telerik:RadComboBoxItem Text="44121626 - Removedor de adhesivo" Value= "44121626" />
<telerik:RadComboBoxItem Text="46181500 - Ropa de seguridad" Value= "46181500" />
<telerik:RadComboBoxItem Text="46181504 - Guantes de protección" Value= "46181504" />
<telerik:RadComboBoxItem Text="46182400 - Equipo de limpieza de seguridad y materiales de descontaminación" Value= "46182400" />
<telerik:RadComboBoxItem Text="47101530 - Equipo de control de olores" Value= "47101530" />
<telerik:RadComboBoxItem Text="47101600 - Consumibles para el tratamiento de agua" Value= "47101600" />
<telerik:RadComboBoxItem Text="47101606 - Químicos de control de corrosión" Value= "47101606" />
<telerik:RadComboBoxItem Text="47101607 - Químicos de control de olor" Value= "47101607" />
<telerik:RadComboBoxItem Text="47121500 - Carritos y accesorios para limpieza" Value= "47121500" />
<telerik:RadComboBoxItem Text="47121600 - Máquinas y accesorios para pisos" Value= "47121600" />
<telerik:RadComboBoxItem Text="47121608 - Almohadillas de máquinas de piso" Value= "47121608" />
<telerik:RadComboBoxItem Text="47121612 - Barredoras para pisos" Value= "47121612" />
<telerik:RadComboBoxItem Text="47121613 - Accesorios para brilladoras de pisos" Value= "47121613" />
<telerik:RadComboBoxItem Text="47121700 - Envases y accesorios para residuos" Value= "47121700" />
<telerik:RadComboBoxItem Text="47121701 - Bolsas de basura" Value= "47121701" />
<telerik:RadComboBoxItem Text="47121800 - Equipo de limpieza" Value= "47121800" />
<telerik:RadComboBoxItem Text="47121804 - Baldes para limpieza" Value= "47121804" />
<telerik:RadComboBoxItem Text="47131500 - Trapos y paños de limpieza" Value= "47131500" />
<telerik:RadComboBoxItem Text="47131502 - Pañitos o toallas para limpiar" Value= "47131502" />
<telerik:RadComboBoxItem Text="47131600 - Escobas, traperos, cepillos y accesorios" Value= "47131600" />
<telerik:RadComboBoxItem Text="47131600 - ESCOBAS,TRAPEROS,CEPILLOS Y ACCESORIOS" Value= "47131600" />
<telerik:RadComboBoxItem Text="47131604 - Escobas" Value= "47131604" />
<telerik:RadComboBoxItem Text="47131605 - Cepillos de limpieza" Value= "47131605" />
<telerik:RadComboBoxItem Text="47131611 - Recogedor de basura" Value= "47131611" />
<telerik:RadComboBoxItem Text="47131617 - Traperos para polvo" Value= "47131617" />
<telerik:RadComboBoxItem Text="47131701 - Dispensadores de toallas de papel" Value= "47131701" />
<telerik:RadComboBoxItem Text="47131702 - Dispensadores de productos sanitarios" Value= "47131702" />
<telerik:RadComboBoxItem Text="47131704 - Dispensadores institucionales de jabón o loción" Value= "47131704" />
<telerik:RadComboBoxItem Text="47131705 - Accesorios para urinales o inodoros" Value= "47131705" />
<telerik:RadComboBoxItem Text="47131707 - Secadores de manos institucionales" Value= "47131707" />
<telerik:RadComboBoxItem Text="47131710 - Dispensadores de papel higiénico" Value= "47131710" />
<telerik:RadComboBoxItem Text="47131800 - Soluciones de limpieza y desinfección" Value= "47131800" />
<telerik:RadComboBoxItem Text="47131801 - Limpiadores de pisos" Value= "47131801" />
<telerik:RadComboBoxItem Text="47131802 - Terminados o ceras para pisos" Value= "47131802" />
<telerik:RadComboBoxItem Text="47131805 - Limpiadores de propósito general" Value= "47131805" />
<telerik:RadComboBoxItem Text="47131807 - Blanqueadores" Value= "47131807" />
<telerik:RadComboBoxItem Text="47131810 - Productos para el lavaplatos" Value= "47131810" />
<telerik:RadComboBoxItem Text="47131811 - Productos de lavandería" Value= "47131811" />
<telerik:RadComboBoxItem Text="47131812 - Refrescador de aire" Value= "47131812" />
<telerik:RadComboBoxItem Text="47131821 - Compuestos desengrasantes" Value= "47131821" />
<telerik:RadComboBoxItem Text="47131824 - Limpiadores de vidrio o ventanas" Value= "47131824" />
<telerik:RadComboBoxItem Text="47131826 - Limpiadores de alfombras o tapizados" Value= "47131826" />
<telerik:RadComboBoxItem Text="47131828 - Limpiadores de automotores" Value= "47131828" />
<telerik:RadComboBoxItem Text="47131829 - Limpiadores de baños" Value= "47131829" />
<telerik:RadComboBoxItem Text="47131830 - Limpiadores de muebles" Value= "47131830" />
<telerik:RadComboBoxItem Text="47131833 - Antisépticos para uso en alimentos" Value= "47131833" />
<telerik:RadComboBoxItem Text="47131900 - Absorbentes" Value= "47131900" />
<telerik:RadComboBoxItem Text="47131902 - Absorbentes granulares" Value= "47131902" />
<telerik:RadComboBoxItem Text="47132100 - Kits de limpieza" Value= "47132100" />
<telerik:RadComboBoxItem Text="48101600 - Equipos para preparado de alimentos" Value= "48101600" />
<telerik:RadComboBoxItem Text="48101615 - Lavadoras de platos para uso comercial" Value= "48101615" />
<telerik:RadComboBoxItem Text="48101618 - Partes de máquinas para lavar platos para uso comercial" Value= "48101618" />
<telerik:RadComboBoxItem Text="48101711 - Dispensadores de agua embotellada o accesorios" Value= "48101711" />
<telerik:RadComboBoxItem Text="48101916 - Dispensadores de servilletas para servicio de comidas" Value= "48101916" />
<telerik:RadComboBoxItem Text="52101505 - Alfombras sintéticas" Value= "52101505" />
<telerik:RadComboBoxItem Text="52101507 - Tapetes de baño" Value= "52101507" />
<telerik:RadComboBoxItem Text="52101510 - Tapetes anti fatiga" Value= "52101510" />
<telerik:RadComboBoxItem Text="52101511 - Tapetes de caucho o vinilo" Value= "52101511" />
<telerik:RadComboBoxItem Text="52151500 - Utensilios de cocina desechables domésticos" Value= "52151500" />
<telerik:RadComboBoxItem Text="52152000 - Platos, utensilios para servir y recipientes para almacenar" Value= "52152000" />
<telerik:RadComboBoxItem Text="53131608 - Jabones" Value= "53131608" />
<telerik:RadComboBoxItem Text="53131625 - Redecillas para el cabello o la barba" Value= "53131625" />
<telerik:RadComboBoxItem Text="53131626 - Desinfectante de manos" Value= "53131626" />
<telerik:RadComboBoxItem Text="53131627 - Limpiador de manos" Value= "53131627" />
<telerik:RadComboBoxItem Text="55101516 - Manuales operativos o de instrucciones" Value= "55101516" />
<telerik:RadComboBoxItem Text="55101520 - Hojas o folletos de instrucciones" Value= "55101520" />
<telerik:RadComboBoxItem Text="55121600 - Etiquetas" Value= "55121600" />
<telerik:RadComboBoxItem Text="55121704 - Señales de seguridad" Value= "55121704" />
<telerik:RadComboBoxItem Text="76121501 - Recolección o destrucción o transformación o eliminación de basuras" Value= "76121501" />
<telerik:RadComboBoxItem Text="80141605 - Mercancía promocional" Value= "80141605" />
<telerik:RadComboBoxItem Text="82121500 - Impresión" Value= "82121500" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:Label ID="lblProdServ" runat="server" ForeColor="#FF0000" Text='<%# Eval("Fac_ClaveProdServ") %>'></asp:Label></EditItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <telerik:GridTemplateColumn HeaderText="SAT - Unidad" DataField="SATUnidad" UniqueName="SATUnidad" GroupByExpression="SATUnidad">
                                                <HeaderStyle Width="140px" HorizontalAlign="Center" />
                                                <ItemStyle Width="140px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("Fac_ClaveUnidad") %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="cmbSATUnidad" runat="server" Width="120px" Filter="Contains"
                                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnSelectedIndexChanged="cmbTipo_SelectedIndexChanged"
                                                        AutoPostBack="true" Text='<%# Eval("Fac_ClaveProdServ") %>'>
                                                        <Items>

<telerik:RadComboBoxItem Text="A76 - Galon" Value= "A76" />
<telerik:RadComboBoxItem Text="E48 - Unidad de servicio" Value= "E48" />
<telerik:RadComboBoxItem Text="EA - Elemento" Value= "EA" />
<telerik:RadComboBoxItem Text="H87 - Pieza" Value= "H87" />
<telerik:RadComboBoxItem Text="KGM - Kilogramo" Value= "KGM" />
<telerik:RadComboBoxItem Text="LTR - Litro" Value= "LTR" />
<telerik:RadComboBoxItem Text="MLT - Mililitro" Value= "MLT" />
<telerik:RadComboBoxItem Text="PR - Par" Value= "PR" />
<telerik:RadComboBoxItem Text="XBX - Caja" Value= "XBX" />
<telerik:RadComboBoxItem Text="XKI - Kit (Conjunto de piezas)" Value= "XKI" />
<telerik:RadComboBoxItem Text="XPK - Paquete" Value= "XPK" />
<telerik:RadComboBoxItem Text="XRO - Rollo" Value= "XRO" />
<telerik:RadComboBoxItem Text="XUN - Unidad" Value= "XUN" />

                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:Label ID="Label12" runat="server" ForeColor="#FF0000" Text='<%# Eval("Fac_ClaveUnidad") %>'></asp:Label></EditItemTemplate>
                                            </telerik:GridTemplateColumn>


                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                                ButtonType="ImageButton" CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn">
                                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
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
                      <div id="formularioTotales_Esp" runat="server">
                        <table width="90%">
                            <tr>
                                <td>
                                </td>
                                <td width="70%">
                                </td>
                                <td>
                                    <asp:Label ID="lblImporte_Esp" runat="server" Text="Importe"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtImporte_Esp" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblSubtotal_Esp" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtSubTotal_Esp" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="IVA_Esp">
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblIVA_Esp" runat="server" Text="I.V.A."></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIVA_Esp" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblTotal_Esp" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotal_Esp" runat="server" Width="100px" MaxLength="9"
                                        Value="0" MinValue="0" Enabled="false" CssClass="AlignRight">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <%--TERMINA LA PESTAÑA DE FACTURAS ESPECIALES --%>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <asp:HiddenField ID="HD_IVAfacturacion" runat="server" Value="0" />
                <asp:HiddenField ID="hiddenId" runat="server" />
                <asp:HiddenField ID="HiddenIdRF" runat="server" />
                <asp:HiddenField ID="hiddenId_Es" runat="server" Value="" />
                <asp:HiddenField ID="HD_GridRebind_FacturaEspecial" runat="server" Value="0" />
                <asp:HiddenField ID="HF_VI" runat="server" Value="false" />
                <asp:HiddenField ID="HiddenHeight" runat="server" />
                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />

                <asp:HiddenField ID="HD_Cliente" runat="server" />
                <asp:HiddenField ID="HD_Moneda" runat="server" />
                <asp:HiddenField ID="HD_ImporteTotal" runat="server" />
                <asp:HiddenField ID="HD_IVARemision" runat="server" />
                <asp:HiddenField ID="HD_Descuento1" runat="server" />
                <asp:HiddenField ID="HD_Descuento2" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />

            </tr>
        </table>
    </div>
</asp:Content>
