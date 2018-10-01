<%@ Page Title="Orden de compra" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapOrdCompra.aspx.cs" Inherits="SIANWEB.CapOrdCompra" %>

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
            //cuando el campo de texto pirde el foco
            function txtMoneda_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbMoneda.ClientID %>'));
            }
            //cuando se selecciona un Item del combo
            function cmbMoneda_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtMoneda.ClientID %>'));
            }
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Orden de Compra
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesOrdenCompra() {
                //debugger;
                var txtFolio = $find('<%= txtFolio.ClientID %>');
                var txtFecha = $find('<%= txtFecha.ClientID %>');
                var cmbTipo = $find('<%= cmbTipo.ClientID %>');
                var txtProveedor = $find('<%= txtProveedor.ClientID %>');
                var cmbProveedor = $find('<%= cmbProveedor.ClientID %>');
                var txtNotas = $find('<%= txtNotas.ClientID %>');

                LimpiarTextBox(txtFolio);
                LimpiarDatePicker(txtFecha);
                LimpiarComboSelectIndex0(cmbTipo);
                LimpiarTextBox(txtProveedor);
                LimpiarComboSelectIndex0(cmbProveedor);
                LimpiarTextBox(txtNotas);
            }

            //Valida una caja de texto que es un dato requerido al momento de insertar o actualizar un producto
            //y selecciona la Tab donde esta el control
            function ValidaObjetoRequerido(textBox, label, indiceTab) {
                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                if (textBox.get_textBoxValue() == '') {
                    label.innerHTML = '*Requerido';
                    radTabStrip.get_allTabs()[indiceTab].select();
                    return false;
                }
                return true;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lbl_cmbProductoClientId = '';
            var txtOrd_CantidadClientId = '';
            var lblVal_txtOrd_CantidadClientId = '';
            var HD_Prd_UniEmpClientId = '';

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

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


                        //establecer datos por default
                        var cmbTipo = $find('<%= cmbTipo.ClientID %>');
                        cmbTipo.get_items().getItem(1).select();

                        var fechaActual = new Date('<%= ActualAnio %>', '<%= ActualMes %>', '<%= ActualDia %>');
                        var txtFecha = $find('<%= txtFecha.ClientID %>');
                        txtFecha.set_selectedDate(fechaActual);


                        //establecer consecitivo de folio de proveedor
                        var txtFolio = $find('<%= txtFolio.ClientID %>');
                        txtFolio.set_value('<%= Valor %>');
                        txtFolio.disable();

                        //poner foco en txtProveedor
                        var txtProveedor = $find('<%= txtProveedor.ClientID %>');
                        txtProveedor.focus();

                        continuarAccion = true;
                        break;

                    case 'save':
                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        radTabStrip.get_allTabs()[0].select();

                        continuarAccion = _ValidarFechaEnPeriodo();
                        break;

                    case 'mail':
                        var grid = $find("<%= rgOrdCompra.ClientID %>");
                        var numProductosOrdCompra = grid.get_masterTableView().get_dataItems();
                        var cmbProveedor = $find('<%= cmbProveedor.ClientID %>');
                        var claveProveedor = cmbProveedor.get_value();

                        //La orden de compra no se envía si no hay productos capturados en el grid
                        if (numProductosOrdCompra == 0) {
                            continuarAccion = false;
                            var Alerta_NoProductos = radalert('No se puede enviar la orden de compra ya que no tiene productos capturados', 600, 10, tituloMensajes);
                        }

                        //solamente con el proveedor 100 (Almacen central) se puede enviar la Orden de Compra por internet
                        if (claveProveedor != '100') {
                            continuarAccion = false;
                            var Alerta_EnvioInternet = radalert('Operaci&oacute;n denegada<br />'
                                + 'Solo se pueden enviar ordenes de compra con el proveedor Almac&eacute;n central (clave 100)', 600, 10, tituloMensajes);
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
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //--------------------------------------------------------------------------------------------------
            //Setea variable de pestaña del TabStrip es clickeada
            //--------------------------------------------------------------------------------------------------
            function OnClientTabSelectingHandler(sender, args) {
                tabSeleccionada = args.get_tab().get_text();
            }

            //--------------------------------------------------------------------------------------------------
            //Doble click en un Row del Grid de Precios dispara evento de edición
            //--------------------------------------------------------------------------------------------------
            function rgOrdCompra_ClientRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //cuando el campo de texto pirde el foco
            function txtProveedor_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbProveedor.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProveedor.ClientID %>'));
            }

            //Para el combo de Productos dentro del Grid
            var txtId_Prd;
            var cmbProducto;

            function txtId_Prd_OnLoad(sender, args) {
                txtId_Prd = sender;
            }

            function txtPrd_Descripcion(sender, args) {//cmbProducto_OnLoad
                cmbProducto = sender;
            }

            //cuando el campo de texto de edición del Grid de clave de producto pierde el foco
            function txtId_Prd_OnBlur(sender, args) {
            }
            function popup(pvd) {

                var oWnd = radopen("Ventana_Buscar.aspx?pvd=" + pvd, "AbrirVentana_Buscar");
                oWnd.center();
            }

            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="hiddenId" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbProveedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgOrdCompra">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Enviar orden de compra por internet."
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
        </Items>
    </telerik:RadToolBar>
    <div class="formulario" id="divPrincipal" runat="server">
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="hiddenId" runat="server" />
                </td>
            </tr>
        </table>
        <table style="font-family: verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RPVGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="t" PageViewID="RPVDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden" Width="98%"><%-- Height="300px" Width="765px">--%>
                        <telerik:RadPageView ID="RPVGenerales" runat="server" Heigth="300px">
                         <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%" >
                            <telerik:RadPane ID="RadPane2" runat="server" Height="300px" OnClientResized="onResize"
                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                            <table><%-- width="500px">--%>
                                <tr>
                                    <td colspan="10">
                                        <asp:Literal ID="literal" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFolio" runat="server" Text="Folio"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFolio" runat="server" Width="70px" MaxLength="9"
                                            MinValue="1" Enabled="false">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="val_txtFolio" runat="server" ControlToValidate="txtFolio"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFecha" runat="server" Width="100px" Enabled="false">
                                            <DatePopupButton ToolTip="Abrir calendario" />
                                            <Calendar ID="cal_dpFecha" runat="server">
                                                <ClientEvents OnDateClick="Calendar_Click" />
                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                    TodayButtonCaption="Hoy" />
                                            </Calendar>
                                            <DateInput runat="server" MaxLength="10">
                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="val_txtFecha" runat="server" ControlToValidate="txtFecha"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbTipo" runat="server" Width="100%" Filter="Contains" ChangeTextOnKeyBoardNavigation="true"
                                            MarkFirstMatch="true" Enabled="false" OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />
                                                <telerik:RadComboBoxItem Text="Manual" Value="1" />
                                                <telerik:RadComboBoxItem Text="Automático" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProveedor" runat="server" Text="Proveedor"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtProveedor" runat="server" Width="70px" MaxLength="9"
                                            MinValue="1">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnBlur="txtProveedor_OnBlur" OnFocus="_ValidarFechaEnPeriodo" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td colspan="6">
                                        <telerik:RadComboBox ID="cmbProveedor" runat="server" Width="100%" Filter="Contains"
                                            LoadingMessage="Cargando..." ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            HighlightTemplatedItems="true" OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged"
                                            OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo" MaxHeight="200px">
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
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblTipoMoneda" runat="server" Text="Tipo de moneda"></asp:Label>
                                    </td>
                                    <td colspan="7">
                                       <telerik:RadNumericTextBox ID="txtMoneda" runat="server" MaxLength="9" MinValue="1"
                                                                    Width="70px">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnBlur="txtMoneda_OnBlur" OnKeyPress="handleClickEvent" />
                                                                </telerik:RadNumericTextBox>
                                        &nbsp;
                                        <telerik:RadComboBox ID="cmbMoneda" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                                    DataTextField="Descripcion" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                                                                    OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmbMoneda_ClientSelectedIndexChanged"
                                                                    Width="150px">
                                                                </telerik:RadComboBox>
                                    </td>
                                </tr>


<tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="Label3" runat="server" Text="Fecha de Entrega"></asp:Label>
                                    </td>
                                    <td colspan="7">
                                        <telerik:RadDatePicker ID="txtFechaEntrega" runat="server" Width="100px">
                                            <DatePopupButton ToolTip="Abrir calendario" />
                                            <Calendar ID="cal_dpFechaEntrega" runat="server">
                                                <ClientEvents OnDateClick="Calendar_Click" />
                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                    TodayButtonCaption="Hoy" />
                                            </Calendar>
                                            <DateInput runat="server" MaxLength="10">
                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>


                                <tr>
                                    <td colspan="10">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lblNotas" runat="server" Text="Notas"></asp:Label>
                                    </td>
                                    <td colspan="7">
                                        <telerik:RadTextBox ID="txtNotas" runat="server" Rows="4" TextMode="MultiLine" Width="99%">
                                            <ClientEvents OnKeyPress="SoloAlfanumerico" OnFocus="_ValidarFechaEnPeriodo" />
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <asp:HiddenField ID="HD_ordenCompraEstatus" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            </telerik:RadPane>
                         </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVDetalles" runat="server">
                          <%--  <asp:Panel ID="Panel" runat="server" Width="765px" ScrollBars="auto">--%>
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                               <telerik:RadPane ID="RadPane1" runat="server" Height="300px" OnClientResized="onResize" BorderStyle="None" Scrolling="Both"> 
                                <telerik:RadGrid ID="rgOrdCompra" runat="server" GridLines="None" DataMember="listaOrdCompraDet"
                                    AllowPaging="False" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                                    BorderColor="White" BorderStyle="Solid" OnNeedDataSource="rgOrdCompra_NeedDataSource" 
                                    OnDeleteCommand="rgOrdCompra_DeleteCommand"
                                    OnInsertCommand="rgOrdCompra_InsertCommand" OnUpdateCommand="rgOrdCompra_UpdateCommand"
                                    OnItemDataBound="rgOrdCompra_ItemDataBound" OnPageIndexChanged="rgOrdCompra_PageIndexChanged">
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="ListaOrdenesDeCompra"
                                        HideStructureColumns="true" ExportOnlyData="true">
                                    </ExportSettings>
                                    <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Ord, Id_OrdDet, Id_Prd"
                                        EditMode="InPlace" DataMember="listaOrdCompraDet" HorizontalAlign="NotSet" PageSize="6"
                                        AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros.">
                                        <ExpandCollapseColumn Visible="True">
                                        </ExpandCollapseColumn>
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Ord" HeaderText="Id_Ord" UniqueName="Id_Ord"
                                                ReadOnly="true" Display="false">
                                                <HeaderStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_OrdDet" HeaderText="Id_OrdDet" UniqueName="Id_OrdDet"
                                                ReadOnly="true" Display="false">
                                                <HeaderStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Prd" UniqueName="Id_PrdN">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_PrdNum" runat="server" Text='<%# Eval("Id_Prd")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="1" Text='<%# Eval("Id_Prd") %>' OnTextChanged="txtProducto_TextChanged"
                                                        OnLoad="txtProducto_Load" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtId_Prd_OnBlur" OnLoad="txtId_Prd_OnLoad" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd"
                                                Visible="false">
                                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_Prd" runat="server" Text='<%DataBinder.Eval(Container.DataItem, "Prd_Descripcion") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <table border="0">
                                                        <tr>
                                                            <td style="border-color: transparent">
                                                            </td>
                                                            <td style="border-color: transparent">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Descripción" DataField="Producto.Prd_Descripcion"
                                                UniqueName="Prd_Descripcion">
                                                <HeaderStyle Width="367px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_Descripcion" runat="server" Text='<%# ObtenerDescripcion(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="border-bottom-style: none">
                                                                <telerik:RadTextBox ID="txtPrd_Descripcion" runat="server" Width="320px" ReadOnly="true"
                                                                    Text='<%# ObtenerDescripcion(Container.DataItem) %>'>
                                                                </telerik:RadTextBox>
                                                            </td>
                                                            <td style="border-bottom-style: none">
                                                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                                    ToolTip="Buscar" ValidationGroup="buscar" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Presen." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# ObtenerPresentacion(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtPrd_Presentacion" runat="server" Width="50px" ReadOnly="true"
                                                        Text='<%# ObtenerPresentacion(Container.DataItem) %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Unid." DataField="Prd_UniNe" UniqueName="Prd_UniNe">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_UniNe" runat="server" Text='<%# ObtenerUnidades(Container.DataItem) %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadTextBox ID="txtPrd_UniNe" runat="server" Width="50px" ReadOnly="true"
                                                        Text='<%# ObtenerUnidades(Container.DataItem) %>'>
                                                    </telerik:RadTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Cantid. gen." DataField="Ord_CantidadGen"
                                                Display="false" UniqueName="Ord_CantidadGen">
                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_CantGen" runat="server" Text='<%# Eval("Ord_CantidadGen") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtPrd_CantGen" runat="server" Width="50px" ReadOnly="true"
                                                        Text='<%# Eval("Ord_CantidadGen") %>'>
                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Cantid. ord." DataField="Ord_Cantidad" UniqueName="Ord_Cantidad">
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrd_Cantidad" runat="server" Text='<%# Eval("Ord_Cantidad") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtOrd_Cantidad" runat="server" Width="80px" MaxLength="9"
                                                        Text='<%# Eval("Ord_Cantidad") %>' OnTextChanged="txtCantidad_TextChanged" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtId_Prd_OnBlur" OnLoad="txtId_Prd_OnLoad" />
                                                    </telerik:RadNumericTextBox>
                                                    <asp:Label ID="lblVal_txtOrd_Cantidad" runat="server" ForeColor="#FF0000"></asp:Label>
                                                    <asp:HiddenField ID="HD_Prd_UniEmp" runat="server" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                             <telerik:GridTemplateColumn HeaderText="Prd_precio" DataField="Prd_precio" UniqueName="Prd_precio" Display= 'False'>
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='' />
                                                </ItemTemplate>
                                                <EditItemTemplate>                                                   
                                                    <asp:HiddenField ID="HD_Prd_PrecioAAA" runat="server" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar">
                                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?"
                                                ConfirmDialogType="RadWindow" ButtonType="ImageButton" CommandName="Delete" Text="Eliminar"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="DeleteColumn">
                                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                                        PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                        PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                        ShowPagerText="True" PageButtonCount="3" />
                                    <GroupingSettings CaseSensitive="False" />
                                    <%--<ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="300px">
                                        </Scrolling>
                                    </ClientSettings>--%>
                                </telerik:RadGrid>
                          <%--  </asp:Panel>--%>
                          </telerik:RadPane>
                         </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTotal" runat="server" ReadOnly="true">
                                    <NumberFormat DecimalDigits="2" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                           <td>
                               <asp:HiddenField ID="HiddenHeight" runat="server" />
                               <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                           </td>
                       </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
