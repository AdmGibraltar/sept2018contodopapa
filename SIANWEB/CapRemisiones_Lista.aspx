<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapRemisiones_Lista.aspx.cs" Inherits="SIANWEB.CapRemisiones_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server"  >
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function confirmCallBackFnVI(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {

                    ajaxManager.ajaxRequest('ok');
                }
                else {
                    ajaxManager.ajaxRequest('no');
                }
            }


            function Confirma() {
                radconfirm("¿Desea imprimir el contrato comodato?", confirmCallBackFnVI, 400, 160)
                return false;
            }
            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //*******************************
            function ToolBar_ClientClick2(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'new':
                        AbrirVentana_Pagos(1, -1, true, true, true, true);
                        args.set_cancel(true);
                        break;
                    case 'remisionPedido':
                        var oWnd2 = radopen("RemisionPedido.aspx", "AbrirVentana_RemisionPedido");
                        oWnd2.center();
                        break;
                }
            }
            function OpenWindow(tdm, Id_Rem, PermisoGuardar, mensaje) {

                if (mensaje != "") {
                    var cerrarWindow = radalert(mensaje, 350, 150, tituloMensajes);
                    cerrarWindow.add_close(
                    function () {
                        AbrirVentana_Pagos(tdm, Id_Rem, PermisoGuardar);
                    });
                }
                else {
                    AbrirVentana_Pagos(tdm, Id_Rem, PermisoGuardar);
                }
            }
            function AbrirVentana_Pagos(tdm, Id_Rem, PermisoGuardar) {
                //debugger;
                var oWnd = radopen("CapRemisiones.aspx?tdm=" + tdm + "&Id_Rem=" + Id_Rem + "&PermisoGuardar=" + PermisoGuardar, "AbrirVentana_Remision");
                oWnd.center();
                oWnd.Maximize();
            }

            function promptCallBackFn(arg) {
                //debugger;
                if (/^[0-9]+$/.test(arg)) {
                    var oWnd2 = radopen("CapRemisiones.aspx?tdm=3&PedRem=" + arg, "AbrirVentana_EntSal");
                    oWnd2.center();
                    oWnd.Maximize();
                }
            }

            function ActivarBanderaRebind_FacturaPedido(mensaje) {
                //debugger;
                if (mensaje != "" && mensaje != null) {
                    var oWnd3 = radopen("CapRemisiones.aspx?tdm=3&PedRem=" + mensaje + "&PermisoGuardar=true", "AbrirVentana_Remision");
                    oWnd3.center();
                    oWnd3.Maximize();
                }
            }
            //*******************************************************
            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                //obtener controles de formulario de inserión/edición de Grid
                var txtCliente1 = $find('<%= txtCliente1.ClientID %>');
                var txtCliente2 = $find('<%= txtCliente2.ClientID %>');
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');
                var txtNotaCredito1 = $find('<%= txtPedido1.ClientID %>');
                var txtNotaCredito2 = $find('<%= txtPedido2.ClientID %>');

                var clienteInicio = 0;
                if (txtCliente1.get_textBoxValue() != '') {
                    clienteInicio = parseFloat(txtCliente1.get_textBoxValue());
                }

                var clienteFin = 0;
                if (txtCliente2.get_textBoxValue() != '') {
                    clienteFin = parseFloat(txtCliente2.get_textBoxValue());
                }

                if (clienteInicio > 0 && clienteFin > 0 && (clienteInicio > clienteFin)) {
                    var alertaCli = radalert('El cliente inicial no debe ser mayor al cliente final', 600, 10, tituloMensajes);
                    alertaCli.add_close(
                    function () {
                        txtCliente1.focus();
                    });
                    return false;
                }

                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;
                fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                fechaFin = datePickerFechaFin._dateInput.get_selectedDate();
                //validar rango correcto de fechas.
                if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                    var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }

                var notaInicio = 0;
                if (txtNotaCredito1.get_textBoxValue() != '') {
                    notaInicio = parseFloat(txtNotaCredito1.get_textBoxValue());
                }
                var notaFin = 0;
                if (txtNotaCredito2.get_textBoxValue() != '') {
                    notaFin = parseFloat(txtNotaCredito2.get_textBoxValue());
                }
                if (notaInicio > 0 && notaFin > 0 && (notaInicio > notaFin)) {
                    var alertaMsg = radalert('La remisión inicial no debe ser mayor a la remisión final', 600, 10, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtNotaCredito1.focus();
                    });
                    return false;
                }
                return true;
            }
            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }

            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }
            function txtTipoId_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoMovimiento.ClientID %>'));
            }
            function cmbTipoMovimiento_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoId.ClientID %>'));
            }

            function ModificaBanderaRebind(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }

            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFRemision");
                oWnd.center();
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$rgPedido$ctl00$ctl02$ctl00$ImgExportar") != -1)
                    args.set_enableAjax(false);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EventName="RAM1_AjaxRequest" 
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgPedido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick2">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="remisionPedido" Value="remisionPedido" ToolTip="Remisiona pedido"
                CssClass="remPedido" ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <div>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                    <asp:HiddenField ID="HF_ClvPag" runat="server" />
                      <asp:HiddenField ID="HD_GridRebind_RemisionEspecial" runat="server" Value="0" />
                    <asp:HiddenField ID="hiddenId" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="hf_spo" runat="server" />
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" runat="server" MaxHeight="300px" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <div id="divPrincipal" runat="server">
            <table style="font-family: Verdana; font-size: 8pt; width: 99%;">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td width="110">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td width="80">
                                </td>
                                <td width="100">
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                </td>
                                <td width="10">
                                </td>
                                <td width="45">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNombreDel" runat="server" Text="Nombre del cliente"></asp:Label>
                                </td>
                                <td colspan="9">
                                    <telerik:RadTextBox ID="txtNombre" runat="server" Width="380px" MaxLength="70">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                 <asp:Label ID="Label1" runat="server" Text="Tipo de movimiento"></asp:Label>
                                </td>
                                <td>
                               <telerik:RadNumericTextBox ID="txtTipoId" runat="server" MaxLength="9" MinValue="0"
                                                       Width="50px">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                <ClientEvents OnBlur="txtTipoId_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                                </td>  
                                <td>
                                </td>
                                <td colspan ="9">
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
                                              
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblClienteInicial" runat="server" Text="Cliente inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblClienteFinal" runat="server" Text="Cliente final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblManAut" runat="server" Text="Man/Aut"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Width="150px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px">
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px">
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="150px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRemisionInicial" runat="server" Text="Remisión inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPedido1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9" ontextchanged="txtPedido1_TextChanged">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblRemisionFinal" runat="server" Text="Remisión final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPedido2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblPedido" runat="server" Text="Pedido"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadNumericTextBox ID="txtPedido3" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9" >
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                 <td>
                                </td>
                                 <td>
                                 <asp:Label ID="LblRem_OrdCompra" runat="server" Text="Orden compra"></asp:Label>
                                </td>
                                <td>
                                <telerik:RadTextBox ID="TxtRem_OrdCompra" runat="server" Width="80px" 
                                        MaxLength="20">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                        ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
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
                                <td>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="900px">
                            <telerik:RadGrid ID="rgPedido" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnNeedDataSource="rg_NeedDataSource" EnableLinqExpressions="False" PageSize="15"
                                AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                OnItemCreated="rg_ItemCreated" OnPageIndexChanged="rg_PageIndexChanged" OnItemCommand="rgPedido_ItemCommand"
                                Width="1100px" OnItemDataBound="rgPago_ItemDataBound">
                                <MasterTableView ClientDataKeyNames="Id_Rem" CommandItemDisplay="Top">
                                
                               <CommandItemTemplate> 
                                             <asp:ImageButton ID="ImgExportar" runat="server" ImageUrl="Imagenes/icono_excel.png"
                                     AlternateText="Esportar facturas" ToolTip="Exportar excel"        onclick="ImgExportar_Click" Width="24px" Height="24px" />                         
                                        </CommandItemTemplate> 
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Id_Ped" UniqueName="Id_Ped"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Id_Ter" UniqueName="Id_Ter"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Tm" HeaderText="Id_Tm" UniqueName="Id_Tm"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Id_Rem" UniqueName="Id_Rem"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_U" HeaderText="Usuario" UniqueName="Id_U"
                                            Display="false">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UsuNom" HeaderText="Usuario" UniqueName="UsuNom">
                                            <HeaderStyle Width="250px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_ManAutStr" HeaderText="Man/Aut" UniqueName="Rem_ManAutStr">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="left" Width="50px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_TipoStr" HeaderText="Tipo" UniqueName="Rem_TipoStr">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Tipo" HeaderText="Tipo" UniqueName="Ped_Tipo"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Estatus" HeaderText="Rem_Estatus" UniqueName="Rem_Estatus"
                                            Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_EstatusStr" HeaderText="Estatus" UniqueName="Rem_EstatusStr">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Número" UniqueName="Id_Rem">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Fecha" HeaderText="Fecha" UniqueName="Rem_Fecha"
                                            DataFormatString="{0:dd/MM/yy}">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                            <HeaderStyle Width="400px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Subtotal" HeaderText="Subtotal" UniqueName="Rem_Subtotal"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Iva" HeaderText="I.V.A." UniqueName="Rem_Iva"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Rem_Total" HeaderText="Total" UniqueName="Rem_Total"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Width="90px" />
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                    </telerik:GridTemplateColumn>--%>
                                        <telerik:GridButtonColumn CommandName="Editar" HeaderText="Editar" Text="Editar"
                                            UniqueName="Editar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                            ButtonCssClass="edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="Se imprimirá la remisión, tenga listo el formato en la impresora<br /><br />"
                                            ConfirmTitle="" Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="Baja" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="¿Estás seguro de dar de baja la remisión <b>#[[ID]]</b>?<br /><br />"
                                            ConfirmTitle="" Text="Baja" UniqueName="Baja" Visible="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn CommandName="ImprimirContrato" HeaderText="Imprimir Contrato"
                                            ConfirmDialogType="RadWindow" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                            ConfirmText="Se imprimirá el contrato de comodato y servicio de la remisión, tenga listo el formato en la impresora<br /><br />"
                                            ConfirmTitle="" Text="Imprimir contrato" UniqueName="ImprimirContrato" Visible="True"
                                            ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                    PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                    ShowPagerText="True" PageButtonCount="3" />
                            </telerik:RadGrid>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
