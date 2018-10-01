<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapRemisiones.aspx.cs" Inherits="SIANWEB.CapRemisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }
            function popup() {
                //  if (txtId_Prd._enabled) {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
                //  } 
            }
            function AbrirBuscarDireccionEntrega() {
                var cte = $find('<%=txtClienteId.ClientID%>');
                var oWnd = radopen("Ventana_Buscar.aspx?DirEnt=true&cte=" + cte.get_value(), "AbrirVentana_BuscarDireccionEntrega");
                oWnd.setSize(600, 400);
                oWnd.center();
            }

            function AbrirVentana_EnviarCorreo(Id_Acs) {
                var oWnd = radopen("capAcysEnviarCorreo.aspx?Id_Acs=" + Id_Acs, "AbrirVentana_capAcysEnviarCorreo");
                oWnd.setSize(750, 500);
                oWnd.center();

            }


            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest(param); //'cliente');
            }
            function txtTipoId_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoMovimiento.ClientID %>'));
            }
            function cmbTipoMovimiento_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoId.ClientID %>'));
            }

            function txtClienteId_OnBlur(sender, args) {
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], false);
                }
            }
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtClienteId.ClientID %>'));
            }

            function txtTerritorioId_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorioId.ClientID %>'));
            }
            //Territorio
            var RadNumericTextBox1;
            var cmbProductoClientID;

            function IdPrd_OnBlur(sender, eventArgs) {
                //debugger;
                // OnBlur(sender, cmbProductoClientID);
            }
            function cmbProductosLista_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), RadNumericTextBox1);
            }

            //Para el combo de Productos dentro del Grid            
            function txtId_Prd_OnLoad(sender, args) {
                RadNumericTextBox1 = sender;
            }
            function cmbProducto_OnLoad(sender, args) {
                cmbProductoClientID = sender;
            }

            //productos
            var txtter1;
            var RadComboBoxTerr;

            function txtter1_OnBlur(sender, eventArgs) {
                //debugger;
                OnBlur(sender, RadComboBoxTerr);
            }
            function RadComboBoxTerr_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtter1);
            }

            //Para el combo de Productos dentro del Grid            
            function txtter1_OnLoad(sender, args) {
                txtter1 = sender;
            }
            function RadComboBoxTerr_OnLoad(sender, args) {
                RadComboBoxTerr = sender;
            }



            var RadComboBoxTipoSalida;

            function RadComboBoxTipoSalida_OnLoad(sender, args) {
                RadComboBoxTipoSalida = sender;
            }

            function RadComboBoxTipoSalida_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtter1);
            }




            var RadComboBoxConceptoTipoSalida;

            function RadComboBoxConceptoTipoSalida_OnLoad(sender, args) {
                RadComboBoxConceptoTipoSalida = sender;
            }

            function RadComboBoxConceptoTipoSalida_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtter1);
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

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                // debugger;
                GetRadWindow().Close();
            }

            function CloseWindow(mensaje) {
                // debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                function () {
                    //debugger;
                    CloseAndRebind();
                });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null); //
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function OnFocus() {
                // debugger;
                if (IdPrd.get_value() == '') {
                    AlertaFocus('Capture el producto para continuar', IdPrd._clientID);
                }
            }

            function ObtenerControlFecha() {
                //debugger;
                var txtFecha = $find('<%= dpFecha.ClientID %>');
                return txtFecha._dateInput;
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

                args.set_cancel(!continuarAccion);
                if (continuarAccion) {
                    var Mov = $find('<%= txtTipoId.ClientID %>');
                    var Cte = $find('<%= txtClienteId.ClientID %>');
                    var Ter = $find('<%= txtTerritorioId.ClientID %>');

                    if (Mov.get_value() == "") {
                        radalert('Por favor capture el tipo de movimiento antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Cte.get_value() == "") {
                        //debugger;
                        radalert('Por favor capture el cliente antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else if (Ter.get_value() == "") {
                        radalert('Por favor capture el territorio antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                }
            }

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = false;
                else {
                    habilitaValidacion = false;
                }
                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }
                switch (button.get_value()) {
                    case 'RemEspecial':
                        var radGrid = $find('<%= rgDetalles.ClientID %>');
                        var MasterTable = radGrid.get_masterTableView();
                        var length = MasterTable.get_dataItems().length;

                        if (length != '' && length > 0) {
                            AbrirVentana_RemisionEspecial();
                            continuarAccion = false;
                        }
                        else {
                            var alertaFEsp = radalert('No se ha agregado ningún producto', 330, 150, tituloMensajes);
                        }
                        break;
                }
                try {
                    if (continuarAccion == true) {
                        GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                    }
                } catch (e) {
                }
                args.set_cancel(!continuarAccion);
            }

            function AbrirVentana_RemisionEspecial() {

                var txtClienteFE = $find('<%= txtClienteId.ClientID %>');
                var txtTotal = $find('<%= txtSub.ClientID %>');
                var txtSubtotal = $find('<%= txtSub.ClientID %>');
                var txtIva = $find('<%= txtIva.ClientID %>');
                var Folio = $find('<%= txtFolio.ClientID %>');
                var txtTmov = $find('<%= txtTipoId.ClientID %>');

                var oWnd = radopen("CapRemisiones_Especial.aspx?Id_Cte="
                  + txtClienteFE.get_value()
                  + "&Rem_ImporteTotal=" + txtTotal.get_value()
                  + "&IVARem=" + txtIva.get_value()
                  + "&Folio=" + Folio.get_value()
                  + "&Modificar=" + '<%= HabilitarGuardar %>'
                  + "&Id_Tmov=" + txtTmov.get_value()
                , "AbrirVentana_RemisionEspecial");
                oWnd.setSize(600, 400);
                oWnd.center();
                oWnd.Maximize();
            }

            function LimpiarBanderaRebind_RemisionEspecial(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind_RemisionEspecial('0');
            }

            function ActivarBanderaRebind_RemisionEspecial() {
                //debugger;
                ModificaBanderaRebind_RemisionEspecial('1');
            }

            function ModificaBanderaRebind_RemisionEspecial(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind_RemisionEspecial.ClientID %>');
                HD_GridRebind.value = valor;
            }

            function Confirma() {
                radconfirm("¿Desea generar el contrato?", confirmCallBackFn, 400, 160)
                return false;
            }

            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('si');
                }
                else {
                    ajaxManager.ajaxRequest('no');
                }
            }
            var IdPrd;
            var txtId_Prd;
            var DescPrd;
            function OnIdPrdLoad(sender, args) {
                IdPrd = sender;
                txtId_Prd = sender;
            }
            function OnDescripcionPrdLoad(sender, args) {
                DescPrd = sender;
            }
            function IdPrd_OnBlur(sender, eventArgs) {
                // debugger; 
                //OnBlur(sender, DescPrd);
            }

            function DescPrd_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), IdPrd);
            }

            function onCommand(sender, eventargs) {
                if (eventargs.get_commandName() == "PerformInsert" || eventargs.get_commandName() == "Update" || eventargs.get_commandName() == "Delete") {
                    var radGrid = $find('<%= rgDetalles.ClientID %>');
                    var table = radGrid.get_masterTableView();
                    var column = table.getColumnByUniqueName("EditCommandColumn");
                    table.hideColumn(column.get_element().cellIndex);

                    column = table.getColumnByUniqueName("DeleteColumn");
                    table.hideColumn(column.get_element().cellIndex);
                }
            }
            function showcolum() {
                var radGrid = $find('<%= rgDetalles.ClientID %>');
                var table = radGrid.get_masterTableView();
                var column = table.getColumnByUniqueName("EditCommandColumn");
                table.showColumn(column.get_element().cellIndex)

                column = table.getColumnByUniqueName("DeleteColumn");
                table.showColumn(column.get_element().cellIndex);
            }

            function abrirBuscar() {
                // debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtClienteId.ClientID%>');
                            var oWnd = radopen("Ventana_Buscar.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarPrecio");
                            oWnd.center();
                        }
                    }
                }
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$BtnAutorizar") != -1)
                    args.set_enableAjax(false);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False" 
        OnAjaxRequest="RadAjaxManager1_AjaxRequest"  ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipoMovimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtClienteId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divGenerales" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgDetalles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="divGenerales" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDetalles" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="btnFacturaEspecial" 
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtSub" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtIva" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="pestaniaDetalles" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="nuevo" />
                <telerik:RadToolBarButton CommandName="RemEspecial" Value="RemEspecial" CssClass="facEspecial"
                    ToolTip="Capturar remisión especial" ImageUrl="Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt; margin-left: 40px;"
            runat="server" width="99%">
            <tr>
                <td>
                     <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                </td>
                <td width="150px" style="font-weight: bold">
                </td>
                <td  width="140"></td>
                <td  width="140"></td>
                <td  width="140"></td>
                <td  width="140"></td>
                <td  width="140"></td>
                <td>
                        <asp:Button ID="BtnAutorizar" runat="server" Text="Autorizar" ToolTip="Autorizar" OnClick="BtnAutorizar_Click" Visible="false" />
                </td>
                <td>
                        <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="false" />
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;talles" AccessKey="E" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Análisis Inversión y Amortización" AccessKey="A" PageViewID="RadPageViewAmortizacion">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Identificación" AccessKey="A" PageViewID="RadPageViewIdentificacion">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <%-- Width="800px" Height="370px">--%>
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" heigth="370px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" BorderSize="0" Height="370px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" BorderColor="White" BorderStyle="Solid"
                                    BorderWidth="1px" Height="370px" OnClientResized="onResize" Scrolling="None">
                                    <div id="divGenerales" runat="server">
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td width="140">
                                                </td>
                                                <td width="50">
                                                </td>
                                                <td width="60">
                                                </td>
                                                <td width="25">
                                                </td>
                                                <td width="60">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td width="100">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelTipo" runat="server" Text="Tipo"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Enabled="False" Width="170px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFolio" runat="server" Text="Folio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtFolio" runat="server" Enabled="False" Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelFecha" runat="server" Text="Fecha"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadDatePicker ID="dpFecha" runat="server" Width="120px" Enabled="false">
                                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario">
                                                        </DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="LabelHora" runat="server" Text="Hora"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtHora" runat="server" MaxLength="50" Width="70px" Enabled="False">                                                        
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="140">
                                                    <asp:Label ID="LabelTipoMovimiento" runat="server" Text="Tipo de movimiento"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTipoId" runat="server" MaxLength="9" MinValue="0"
                                                        Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTipoId_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTipoMovimiento" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="200px" OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo"
                                                        OnClientSelectedIndexChanged="cmbTipoMovimiento_ClientSelectedIndexChanged" OnSelectedIndexChanged="cmbTipoMovimiento_SelectedIndexChanged"
                                                        Width="300px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTipoId"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="pestaniaDetalles">*Requerido</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelPedido" runat="server" Text="Pedido"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPedido" runat="server" Enabled="False" MaxLength="9"
                                                        MinValue="0" Width="100px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="140">
                                                    <asp:Label ID="LabelCliente" runat="server" Text="Cliente"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtClienteId" runat="server" AutoPostBack="true" MaxLength="9"
                                                        MinValue="1" OnTextChanged="txtClienteId_TextChanged" Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCliente" runat="server" ReadOnly="True" Width="295px">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtClienteId"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="" SetFocusOnError="True"
                                                        ValidationGroup="pestaniaDetalles">*Requerido</asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="bottom">
                                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                        ToolTip="Buscar" ValidationGroup="buscar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelTerritorio" runat="server" Text="Territorio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTerritorioId" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTerritorioId_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" CausesValidation="False"
                                                        ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                        EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        MarkFirstMatch="true" MaxHeight="250px" OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo"
                                                        OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged" OnSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged"
                                                        Width="300px">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="width: 250px; text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cmbTerritorio"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        SetFocusOnError="True" ValidationGroup="pestaniaDetalles">*Requerido</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelRepresentante" runat="server" Text="Representante"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="50px">
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
                                                </td>
                                                <td width="10">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="140">
                                                    <asp:Label ID="LabelCalle" runat="server" Text="Calle"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCalle" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td width="60">
                                                    <asp:Label ID="LabelNúmero" runat="server" Text="Número"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNumero2" runat="server" MaxLength="15" Width="50px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelCP" runat="server" Text="C.P."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtCp" runat="server" MaxLength="9" MinValue="0" Width="100px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImgBuscarDireccionEntrega_Click"
                                                                ToolTip="Buscar" ValidationGroup="buscar" Visible="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelColonia" runat="server" Text="Colonia"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtColonia" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico2" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelMunicipio" runat="server" Text="Municipio"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadTextBox ID="txtMunicipio" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelEstado" runat="server" Text="Estado"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtEstado" runat="server" MaxLength="40" Width="200px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td width="10">
                                                </td>
                                                <td width="140">
                                                    <asp:Label ID="LabelRFC" runat="server" Text="R.F.C."></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtRfc" runat="server" MaxLength="20" Width="200px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelTelefono" runat="server" Text="Teléfono"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtTelefono2" runat="server" MaxLength="15" MinValue="0"
                                                        Width="110px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelContacto" runat="server" Text="Contacto"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtContacto" runat="server" MaxLength="20" Width="114px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelConducto" runat="server" Text="Conducto"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtConducto" runat="server" MaxLength="30" Width="200px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td width="60">
                                                    <asp:Label ID="LabelGuia" runat="server" Text="Guía"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtGuia2" runat="server" MaxLength="15" Width="110px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="140">
                                                    <asp:Label ID="LabelFecha2" runat="server" Text="Fecha-Hora de entrega"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadDateTimePicker ID="dtpFechaHora" runat="server">
                                                        <TimeView runat="server" CellSpacing="-1" Culture="es-MX" HeaderText="Horario">
                                                        </TimeView>
                                                        <TimePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir horarios" />
                                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                        </Calendar>
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                        <ClientEvents OnPopupOpening="ClientTabSelecting" />
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelNota" runat="server" Text="Nota"></asp:Label>
                                                </td>
                                                <td colspan="9">
                                                    <telerik:RadTextBox ID="txtNota" runat="server" MaxLength="256" Width="360px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="140">
                                                    <asp:Label ID="lblOrdenCompra" runat="server" Text="Orden de Compra"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtOrdenCompra" runat="server" MaxLength="12" MinValue="0"
                                                        Width="110px">
                                                       <%-- <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />--%>
                                                    </telerik:RadTextBox>
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
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="valuacion" runat="server" visible="false">
                                                <td>
                                                </td>
                                                <td width="140">
                                                    Código de valuación de proyecto autorizado
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtValuacion" runat="server" MaxLength="9" MinValue="0"
                                                        Width="70px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
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
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>


                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="370px">
                            <%--  <asp:Panel ID="panelDetalles" runat="server" Width="800px" Height="370px" ScrollBars="Horizontal">--%>
                            <%--  <asp:Panel ID="panelDetalles" runat="server" Width="800px" Height="370px" ScrollBars="Horizontal">--%>
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" BorderSize="0" Height="370px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" Width="101%">
                                <telerik:RadPane ID="RadPane1" runat="server" BorderStyle="None" Height="370px" OnClientResized="onResize">
                                    <%--  </asp:Panel>--%>
                                    <%--  </asp:Panel>--%>
                                    <telerik:RadGrid ID="rgDetalles" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                        GridLines="None" OnDeleteCommand="rgDetalles_DeleteCommand" OnInsertCommand="rgDetalles_InsertCommand"
                                        OnItemCommand="rgDetalles_ItemCommand" OnItemDataBound="rgDetalles_ItemDataBound"
                                        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="rgDetalles_UpdateCommand">
                                        <%--  Width="1210px">--%>
                                        <ClientSettings>
                                            <ClientEvents OnCommand="onCommand" />
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Prd, Terr" EditMode="InPlace"
                                            NoMasterRecordsText="No se encontraron registros.">
                                            <CommandItemSettings AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf" RefreshText="Actualizar"
                                                ShowRefreshButton="false" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_RemDet" UniqueName="Id_RemDet" Visible="False">
                                                </telerik:GridBoundColumn>


                                                <telerik:GridTemplateColumn DataField="Terr" HeaderText="Núm." UniqueName="IdTerr">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtter1" runat="server" MaxLength="9" MinValue="0"
                                                            Width="50px">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtter1_OnBlur" OnKeyPress="handleClickEvent" OnLoad="txtter1_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="TerrLabel" runat="server" Text='<%# Eval("Terr") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Terr" HeaderText="Territorio" UniqueName="Terr">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="TerrIdDesc" runat="server" Text='<%# Eval("Terr") %>' Visible="false"></asp:Label>
                                                        <telerik:RadComboBox ID="RadComboBoxTerr" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            OnClientBlur="Combo_ClientBlur" OnClientLoad="RadComboBoxTerr_OnLoad" OnClientSelectedIndexChanged="RadComboBoxTerr_ClientSelectedIndexChanged"
                                                            Width="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                                Width="50px" />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="TerrLabelDesc" runat="server" Text='<%# Eval("Terr") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="270px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>



                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" AutoPostBack="True"
                                                            MaxLength="9" MinValue="0" OnLoad="txtProducto_Load" OnTextChanged="cmbProductoDet_TextChanged"
                                                            Width="50px">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="IdPrd_OnBlur" OnLoad="OnIdPrdLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <%-- txtId_Prd_OnLoad     --%>
                                                    <ItemTemplate>
                                                        <asp:Label ID="ProdLabel" runat="server" Text='<%# Eval("Id_Prd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Descripcion" HeaderText="Producto" UniqueName="Descripcion"
                                                    Visible="true">
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="DescripcionTextBox" runat="server" ReadOnly="true" Text='<%# Bind("Descripcion") %>'
                                                            Width="100%">
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="225px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Cantidad" HeaderText="Cantid." UniqueName="Cantidad">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxCantidad" runat="server" AutoPostBack="true"
                                                            MaxLength="9" MinValue="0" OnTextChanged="txtCantidad_TextChanged" Text='<%# Bind("Cantidad") %>'
                                                            Width="100%">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="CantidadLabel" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Precio" HeaderText="Precio" UniqueName="Precio">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxPrecio" runat="server" MaxLength="9"
                                                            MinValue="0" Text='<%# Bind("Precio") %>' Width="100%">
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="PrecioLabel" runat="server" Text='<%# Eval("Precio") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Importe" DataFormatString="{0:N2}" HeaderText="Importe"
                                                    ReadOnly="True" UniqueName="Importe">
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="DescTipoSalida" HeaderText="Tipo de Salida" UniqueName="TipoSalida">
                                            <ItemTemplate>  
                                                <asp:Label ID="TipoSalida"   runat="server" Text='<%# Eval("DescTipoSalida") %>'></asp:Label>
                                            </ItemTemplate>

                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="RadComboBoxTipoSalida"  runat="server" ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"  
                                                            Width="250px" OnSelectedIndexChanged="cmbTipoSalidaIdDesc_SelectedIndexChanged" AutoPostBack="true" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true">
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="270px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>




                                                <telerik:GridTemplateColumn DataField="DescConceptoTipoSalida" HeaderText="Concepto Tipo de Salida" UniqueName="ConceptoTipoSalida">

                                            <ItemTemplate>  
                                                <asp:Label ID="ConceptoTipoSalida"   runat="server" Text='<%# Eval("DescConceptoTipoSalida") %>'></asp:Label>
                                            </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="RadComboBoxConceptoTipoSalida"  runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            OnClientBlur="Combo_ClientBlur" OnClientLoad="RadComboBoxConceptoTipoSalida_OnLoad" OnClientSelectedIndexChanged="RadComboBoxConceptoTipoSalida_ClientSelectedIndexChanged"
                                                            Width="250px">
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="270px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                    HeaderText="" InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px"
                                                    ConfirmDialogType="RadWindow" ConfirmDialogWidth="350px" ConfirmText="¿Desea quitar este producto de la lista?"
                                                    ConfirmTitle="" HeaderText="" Text="Borrar" UniqueName="DeleteColumn">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>

                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <%-- <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="360px" />
                                    </ClientSettings>--%>
                                    </telerik:RadGrid>
                                    <div id="botonFacturaEspecial" runat="server" style="text-align: right; margin: 5px 5px 5px 0px;">
                                        <asp:Button ID="btnFacturaEspecial" runat="server" OnClientClick="AbrirVentana_RemisionEspecial()"
                                            Text="Capturar factura especial" Visible="false" />
                                    </div>
                                    <%--  </asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageViewAmortizacion" runat="server" heigth="370px">
                            <telerik:RadSplitter ID="RadSplitter3" runat="server" BorderSize="0" Height="370px"
                                ResizeMode="AdjacentPane" ResizeWithBrowserWindow="true" Width="100%">
                                <telerik:RadPane ID="RadPane3" runat="server" BorderColor="White" BorderStyle="Solid"
                                    BorderWidth="1px" Height="370px" OnClientResized="onResize" Scrolling="None">
                                    <div id="divAmortizacion" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" align="left" width="25%">
                                            <asp:Label ID="Label_Amortizacion_Remision" runat="server" Text="Remisión"></asp:Label>
                                            &nbsp;
                                                    <telerik:RadTextBox ID="Fol_Amortizacion_Remision" runat="server" ReadOnly="true" Width="50px">
                                                    </telerik:RadTextBox>                                            
                                            </td>
                                            <td rowspan="3" align="center" colspan="3" width="75%">
                                            <div id="inversion" runat="server">
                                            Inversión
                                            </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left" width="25%">
                                            <asp:Label ID="Label_Amortizacion_Cliente" runat="server" Text="Cliente"></asp:Label>
                                            &nbsp;<telerik:RadTextBox ID="NCliente_Amortizacion_Remision" runat="server" ReadOnly="true" Width="50px">
                                                  </telerik:RadTextBox>&nbsp;
                                                    <telerik:RadTextBox ID="NombreCliente_Amortizacion_Remision" runat="server" ReadOnly="true" Width="300px">
                                                    </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left" width="25%">
                                            <asp:Label ID="Label_Amortizacion_Territorio" runat="server" Text="Territorio"></asp:Label>
                                            &nbsp;<telerik:RadTextBox ID="NTerritorio_Amortizacion_Remision" runat="server" ReadOnly="true" Width="50px">
                                                  </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" width="40%">
                                            <div id="tablaamortizacion" runat="server">
                                            Tabla Amortización
                                            </div>
                                            </td>
                                            <td colspan="3" align="center" width="60%">
                                            <div id="kardexmovimiento" runat="server">
                                            Kardex de Movimientos
                                            </div>
                                            </td>
                                        </tr>

                                    </table>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>



                    </telerik:RadMultiPage>
                    <div id="formularioTotales" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td width="125">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LabelSubtotal" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td width="125">
                                    <telerik:RadNumericTextBox ID="txtSub" runat="server" Enabled="false" CssClass="AlignRight"
                                        Width="100px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LabelIVA2" runat="server" Text="I.V.A."></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtIva" runat="server" Enabled="false" CssClass="AlignRight"
                                        Width="100px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="margin-left: 80px">
                                    <asp:Label ID="LabelTotal" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotal" runat="server" Enabled="false" CssClass="AlignRight"
                                        Width="100px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="HF_ID" runat="server" /> 
                    <asp:HiddenField ID="HD_GridRebind_RemisionEspecial" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenId" runat="server" />
                    <asp:HiddenField ID="HiddenCteCuentaNacional" runat="server" Value="-1" />
                     <asp:HiddenField ID="HiddenNumCuentaContNacional" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="hf_spo" runat="server" />
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                    <asp:HiddenField ID="HF_ClvPag" runat="server" />
                    <asp:HiddenField ID="HF_VI" runat="server" Value="false" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
