<%@ Page Title="Valuación de proyectos de Cuentas Marginales" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" 
AutoEventWireup="true" CodeBehind="CapValProyCtasMarginales.aspx.cs" Inherits="SIANWEB.CapValProyCtasMarginales" %>

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
        .style1
        {
            height: 19px;
        }
    </style>
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
            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }
            function ClienteSeleccionado() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('cliente');
            }
            //--------------------------------------------------------------------------------------------------
            //Variables de la forma
            //--------------------------------------------------------------------------------------------------
            var tabSeleccionada = '';

            function ClientTabSelecting(sender, args) {
                //debugger;             
                continuarAccion = true;
                if (continuarAccion) {

                    var Cte = $find('<%= txtCliente.ClientID %>');
                    if (Cte.get_value() == "") {
                        radalert('Por favor capture el cliente antes de continuar', 330, 150, '');
                        args.set_cancel(true);
                    }
                }
            }

            function _PreValidarFechaEnPeriodo() {
                continuarAccion = true;
            }

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de captura de Nota de cargo
            //--------------------------------------------------------------------------------------------------
            function LimpiarControlesOrdenCompra() {
                //debugger;
            }

            //variables para guardar los nombres de los controles de formulario de inserción/edición de Grid.
            var lblVal_cmbTipoClientID = '';
            var cmbTipoClientID = '';
            var lbl_cmbProductoClientID = '';
            var txtId_PrdClientID = '';
            var lblVal_txtVap_CantidadClientID = '';
            var txtVap_CantidadClientID = '';
            var lblVal_txtVap_PrecioClientID = '';
            var txtVap_PrecioClientID = '';
            var lblVap_CostoEditClientID = '';

            //Validación del formulario de insercion/edición de registro en un RadGrid.
            //param: accion --> indica que tipo de operación se esta realizando, puede traer los valores 'insertar' o 'actualizar'
            function ValidaFormEdit(accion) {
                var continuarAccion = true;
                //debugger;
                //obtener controles de formulario de inserión/edición de Grid
                var lblVal_cmbTipo = document.getElementById(lblVal_cmbTipoClientID);
                var cmbTipo = $find(cmbTipoClientID);
                var lbl_cmbProducto = document.getElementById(lbl_cmbProductoClientID);
                var txtId_Prd = $find(txtId_PrdClientID);
                var lblVal_txtVap_Cantidad = document.getElementById(lblVal_txtVap_CantidadClientID);
                var txtVap_Cantidad = $find(txtVap_CantidadClientID);
                var lblVal_txtVap_Precio = document.getElementById(lblVal_txtVap_PrecioClientID);
                var txtVap_Precio = $find(txtVap_PrecioClientID);

                var lblVap_CostoEdit = document.getElementById(lblVap_CostoEditClientID);

                //Limpiar contenedores de mensaje de validación
                lblVal_cmbTipo.innerHTML = '';
                lbl_cmbProducto.innerHTML = '';
                lblVal_txtVap_Cantidad.innerHTML = '';
                lblVal_txtVap_Precio.innerHTML = '';

                if (cmbTipo != null)
                    if (cmbTipo.get_value() == '-1') {
                        lblVal_cmbTipo.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (txtId_Prd != null)
                    if (txtId_Prd.get_textBoxValue() == '') {
                        lbl_cmbProducto.innerHTML = '*Requerido';
                        continuarAccion = false
                    }

                if (txtVap_Cantidad != null) {
                    if (txtVap_Cantidad.get_textBoxValue() == '') {
                        lblVal_txtVap_Cantidad.innerHTML = '*Requerido';
                        continuarAccion = false
                    }
                    else {
                        if (parseInt(txtVap_Cantidad.get_textBoxValue()) <= 0) {
                            lblVal_txtVap_Cantidad.innerHTML = '*Requerido, el valor debe ser mayor a 0';
                            continuarAccion = false
                        }
                    }
                }
                if (txtVap_Precio != null) {
                    if (txtVap_Precio.get_textBoxValue() == '') {
                        lblVal_txtVap_Precio.innerHTML = '*Requerido';
                        continuarAccion = false
                    }
                    else {
                        if (parseFloat(txtVap_Precio.get_textBoxValue()) <= 0 && cmbTipo.get_value() == 1) {
                            lblVal_txtVap_Precio.innerHTML = '*El valor debe ser mayor a 0';
                            continuarAccion = false
                        }
                        else {
                            var costoAAA = parseFloat(lblVap_CostoEdit.innerHTML)
                            if (costoAAA >= parseFloat(txtVap_Precio.get_textBoxValue()) && cmbTipo.get_value() == 1) {
                                var alertaCosto = radalert('Favor de capturar correctamente el precio de venta, tiene que ser mayor que el precio AAA', 330, 150, tituloMensajes);
                                continuarAccion = false
                            }
                        }
                    }
                }
                return continuarAccion
            }

            function ValidacionesEspeciales() {
                var conntinuar = true;
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

                    case 'save':
                        //select tab datos generales
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        radTabStrip.get_allTabs()[0].select();
                        continuarAccion = ValidacionesEspeciales();
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
                GetRadWindow().Close();
            }
            //cuando el campo de texto pirde el foco
            function txtCliente_OnBlur(sender, args) {

            }

            //cuando se selecciona un Item del combo
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
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
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="div1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hiddenId" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgDetalle" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="div1">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
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
                            <telerik:RadTab runat="server" Text="&lt;u&gt;P&lt;/u&gt;roductos" AccessKey="P"
                                PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RpvParametros" Text="&lt;u&gt;C&lt;/u&gt;ondiciones"
                                AccessKey="C">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden"> <%--Height="480px"--%>                       
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" heigth="480px">
                         <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="480px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%" >
                           <telerik:RadPane ID="RadPane2" runat="server" Height="480px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                            <div runat="server" id="divPrincipal">
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td width="80">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td width="70">
                                        </td>
                                        <td width="150">
                                        </td>
                                        <td width="40">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td width="110">
                                        </td>
                                        <td width="10">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Número"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFolio" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1" Enabled="false">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="val_txtFolio" runat="server" ControlToValidate="txtFolio"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Fecha"></asp:Label>
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
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Cliente"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MaxLength="9"
                                                MinValue="1" AutoPostBack="True" OnTextChanged="txtCliente_TextChanged">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="val_txtCliente" runat="server" ControlToValidate="txtCliente"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                            </asp:RequiredFieldValidator>
                                            <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                Style="width: 16px" ToolTip="Buscar" ValidationGroup="buscar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Label4" runat="server" Text="Nota"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <telerik:RadTextBox ID="txtNota" runat="server" Rows="4" TextMode="MultiLine" MaxLength="250"
                                                Width="377px">
                                                <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" />
                                            </telerik:RadTextBox>
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
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                           </telerik:RadPane>
                         </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="480px">
                         <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="480px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="480px" OnClientResized="onResize" BorderStyle="None"> 
                           <%-- <asp:Panel ID="Panel1" runat="server" Width="800px" ScrollBars="Vertical">--%>
                                <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" AllowPaging="False"
                                    AutoGenerateColumns="False" OnNeedDataSource="rgDetalle_NeedDataSource" OnInsertCommand="rgDetalle_InsertCommand"
                                    OnUpdateCommand="rgDetalle_UpdateCommand" OnDeleteCommand="rgDetalle_DeleteCommand"
                                    OnItemDataBound="rgDetalle_ItemDataBound" OnItemCommand="rgDetalle_ItemCommand"
                                    OnPageIndexChanged="rgDetalle_PageIndexChanged"> <%--Height="461px">--%>
                                    <MasterTableView Name="Master" CommandItemDisplay="Top" DataKeyNames="Id_Vap,Id_VapDet,Id_Prd"
                                        EditMode="InPlace" HorizontalAlign="NotSet" AutoGenerateColumns="False" NoMasterRecordsText="No se encontraron registros.">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" AddNewRecordText="Agregar" RefreshText="Actualizar" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Vap" HeaderText="Id_Vap" UniqueName="Id_Vap"
                                                ReadOnly="true" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_VapDet" HeaderText="Id_VapDet" UniqueName="Id_VapDet"
                                                ReadOnly="true" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vap_Tipo" HeaderText="Vap_Tipo" UniqueName="Vap_Tipo"
                                                ReadOnly="true" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Tipo" DataField="Vap_TipoStr" UniqueName="Vap_TipoStr">
                                                <HeaderStyle Width="140px" HorizontalAlign="Center" />
                                                <ItemStyle Width="140px" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("Vap_TipoStr") %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Width="120px" Filter="Contains"
                                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnSelectedIndexChanged="cmbTipo_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <Items>
                                                            <%--<telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-1" />--%>
                                                            <%--<telerik:RadComboBoxItem Text="Producto" Value="1" />--%>
                                                            <telerik:RadComboBoxItem Text="Comodato" Value="2" />
                                                            <telerik:RadComboBoxItem Text="Facturado" Value="1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:Label ID="lblVal_cmbTipo" runat="server" ForeColor="#FF0000"></asp:Label></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Producto" DataField="Id_Prd" UniqueName="Id_Prd">
                                                <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_Prd" runat="server" Text='<%# string.Concat(Eval("Id_Prd").ToString(), " - ", ObtenerDescripcionProducto(Container.DataItem)) %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:HiddenField ID="hdSisProp" runat="server" />
                                                    <table>
                                                        <tr>
                                                            <td style="border-bottom-color: transparent">
                                                                <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" Width="50px" MaxLength="9"
                                                                    MinValue="1" Text='<%# Eval("Id_Prd") %>' OnTextChanged="cmbProductoDet_TextChanged"
                                                                    AutoPostBack="true">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    <ClientEvents OnLoad="txtId_Prd_OnLoad" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td style="border-bottom-color: transparent">
                                                                <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Width="230px"
                                                                    Text='<%# ObtenerDescripcionProducto(Container.DataItem) %>'>
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="lbl_cmbProducto" runat="server" ForeColor="#FF0000"></asp:Label></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Presen." DataField="Prd_Presentacion" UniqueName="Prd_Presentacion">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_Presentacion" runat="server" Text='<%# ObtenerPresentacionProducto(Container.DataItem) %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblPrd_PresentacionEdit" runat="server" Text='<%# ObtenerPresentacionProducto(Container.DataItem) %>'
                                                        Width="50px" /></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Unid." DataField="Prd_UniNs" UniqueName="Prd_UniNs">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrd_UniNs" runat="server" Text='<%# ObtenerUnidadesProducto(Container.DataItem) %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblPrd_UniNsEdit" runat="server" Text='<%# ObtenerUnidadesProducto(Container.DataItem) %>'
                                                        Width="50px" /></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Cantidad" DataField="Vap_Cantidad" UniqueName="Vap_Cantidad">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVap_Cantidad" runat="server" Text='<%# Eval("Vap_Cantidad") %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtVap_Cantidad" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="0" Text='<%# Eval("Vap_Cantidad") %>'>
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox><asp:Label ID="lblVal_txtVap_Cantidad" runat="server"
                                                        ForeColor="#FF0000"></asp:Label></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Precio AAA" DataField="Vap_Costo" UniqueName="Vap_Costo">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVap_Costo" runat="server" Text='<%# Convert.IsDBNull(Eval("Vap_Costo")) ? string.Empty : Convert.ToDouble(Eval("Vap_Costo")).ToString("N") %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblVap_CostoEdit" runat="server" Text='<%# Convert.IsDBNull(Eval("Vap_Costo")) ? string.Empty :  Convert.ToDouble(Eval("Vap_Costo")).ToString("N") %>'
                                                        Width="50px" /></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Precio lista" DataField="Vap_PrecioEspecial"
                                                UniqueName="Vap_PrecioEspecial">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVap_PrecioEspecial" runat="server" Text='<%# Convert.IsDBNull(Eval("Vap_PrecioEspecial")) ? string.Empty : Convert.ToDouble(Eval("Vap_PrecioEspecial")).ToString("N") %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                   <%-- <telerik:RadNumericTextBox ID="txtVap_PrecioEspecialEdit" runat="server" Width="50px"
                                                        MaxLength="9" MinValue="0" Text='<%# Convert.IsDBNull(Eval("Vap_PrecioEspecial")) ? string.Empty : Convert.ToDouble(Eval("Vap_PrecioEspecial")).ToString("N") %>'>
                                                        <ClientEvents />
                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>--%>
                                                     <asp:Label ID="lblVap_ListaEdit" runat="server" Text='<%# Convert.IsDBNull(Eval("Vap_PrecioEspecial")) ? string.Empty :  Convert.ToDouble(Eval("Vap_PrecioEspecial")).ToString("N") %>'
                                                        Width="50px" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Precio venta" DataField="Vap_Precio" UniqueName="Vap_Precio">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVap_Precio" runat="server" Text='<%# Convert.IsDBNull(Eval("Vap_Precio")) ? string.Empty : Convert.ToDouble(Eval("Vap_Precio")).ToString("N") %>' /></ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txtVap_Precio" runat="server" Width="50px" MaxLength="9"
                                                        MinValue="0" Text='<%# Eval("Vap_Precio") %>'>
                                                        <ClientEvents />
                                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox><asp:Label ID="lblVal_txtVap_Precio" runat="server" ForeColor="#FF0000"></asp:Label>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                                ReadOnly="true" Display="false">
                                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" UpdateText="Actualizar">
                                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?"
                                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow"
                                                ButtonType="ImageButton" CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn">
                                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                   <%-- <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                                        </Scrolling>
                                    </ClientSettings>--%>
                                </telerik:RadGrid>
                               <%-- </asp:Panel>--%>
                             </telerik:RadPane>
                         </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RpvParametros" runat="server">
                            <%--<asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="300">--%>
                             <telerik:RadSplitter ID="RadSplitter3" runat="server" Height="480px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%" >
                           <telerik:RadPane ID="RadPane3" runat="server" Height="480px" OnClientResized="onResize"
                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                            <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
                                <tr>
                                    <td align="center" colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>

                                <%--INICIO DE PARAMETROS  INICIO DE PARAMETROS  --%>

                                <tr>
                                    <td align="center" colspan="5">                                                              
                                        <asp:Label ID="Label67" runat="server" Font-Size="Large" Font-Bold="True" Visible="True" Text="EVALUACION DEL PROYECTO DE VENTA - CUENTAS MARGINALES"></asp:Label>
                                    </td>
                                </tr>

                                 <tr>
                                    <td align="center" class="style1" colspan="3">
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label70" runat="server" Text="Costo de Servicio a Equipos"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtManoObra" runat="server" MaxLength="9" MinValue="0"
                                            Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                    
                                <tr>
                                    <td>
                                        <asp:Label ID="Label69" runat="server" Text="Fletes (% sobre la venta)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFleteLocales" runat="server" Enabled="True" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" class="style1" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>                                                                       
                                    <td >
                                        <asp:Label ID="Label95" runat="server" Text="Gastos de Administración y Venta (% sobre la CM)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGAdmitivos" runat="server" Enabled="True" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                               
                                <tr>

                                    <td >
                                        <asp:Label ID="Label1" runat="server" Text="Otros Gastos Variables (% Sobre venta)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtOtrosGastos" runat="server" Enabled="True" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <asp:Label ID="Label89" runat="server" Text="Contribución a los costos fijos (%  sobre venta)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCostosFijosNoPapel" runat="server" Enabled="True"
                                            MaxLength="9" MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" class="style1" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label80" runat="server" Text="ISR y PTU (%)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIsr" runat="server" Enabled="True" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="center" class="style1" colspan="4">
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                
                                <tr>

                                <td>
                                        <asp:Label ID="Label82" runat="server" Text="Cuentas Por Cobrar (días promedio s/ingreso)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCuentasporCobrar" runat="server" MaxLength="4" MinValue="0"
                                            Width="80px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label77" runat="server" Text="Inventarios (Días en promedio al costo)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtInventarioKey" runat="server" Enabled="True" MaxLength="4"
                                            MinValue="0" Width="80px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                     </td>
                                  </tr>

                                  <tr>
                                  <td>
                                        <asp:Label ID="Label92" runat="server" Text="Inversión en activos fijos (días de venta en promedio)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtInversionActivosFijos" runat="server" Enabled="True"
                                            MaxLength="4" MinValue="0" Width="80px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    </tr>

                                    <tr>
                                    <td align="center" class="style1" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>

                                  <tr>
                                        <td>
                                            <asp:Label ID="Label79" runat="server" Text="Financiamiento de Prov. Químicos (en días)"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCreditoProveedor" runat="server" Enabled="True"
                                                MaxLength="4" MinValue="0" Width="80px">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="SoloNumerico" />
                                            </telerik:RadNumericTextBox>
                                      </td>
                                  </tr>

                                  <tr>
                                    <td align="center" class="style1" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>

                                    <tr>
                                    <td>
                                        <asp:Label ID="Label81" runat="server" Text="Tasa de Cetes (%)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCetes" runat="server" Enabled="True" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    </tr>

                                 <tr>
                                    <td>
                                        <asp:Label ID="Label86" runat="server" Text="Rendimiento sobre cetes (%)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAdicionalCetes" runat="server" Enabled="True"
                                            MaxLength="9" MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>                                    
                                </tr>

                                 <tr>                                    
                                    <td>
                                        <asp:Label ID="Label76" runat="server" Text="Tasa de IVA (%)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtIva" runat="server" Enabled="False" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>                                   
                                </tr>



                                <tr>
                                    <td align="center" class="style1" colspan="5">
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style1" colspan="5">
                                        <asp:Label ID="Label68" runat="server" Visible="false" Font-Bold="True" Text="GASTOS EXTRAS PARA PODER SERVIR AL CLIENTE"></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                <td>
                                        <asp:Label ID="Label65" runat="server" visible="false" Text="Proporción de la participación de utilidades reconocida al RIK (%)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtComision" runat="server" MaxLength="9" MinValue="0" Visible="false" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label66" runat="server" Visible="false" Text="Vigencia del ACYS (en años)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtVigencia" runat="server" MaxLength="1" MaxValue="2" Visible="false"
                                            MinValue="1" Width="80px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label93" runat="server" Visible="false" Text="Número de entregas"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtNumEntregas" Visible="false" runat="server" MaxLength="9" MinValue="0"
                                            Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label94" runat="server" Visible="false" Text="Costo de entregas"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCostoEntregas" Visible="false" runat="server" MaxLength="9" MinValue="0"
                                            Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                <td style="margin-left: 40px">
                                        <asp:Label ID="Label71" runat="server" Visible="false" Text="Amortización de equipos arrendados"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAmortizacion" Visible="false" runat="server" MaxLength="9" MinValue="0"
                                            Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label72" runat="server" Visible="false" Text="% de comisión por factoraje"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtComisionFactoraje" Visible="false" runat="server" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
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
                                <tr>
                                    <td align="center" colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        <asp:Label ID="Label75" runat="server" Visible="false" Font-Bold="True" Text="INFORMACIÓN PARA DETERMINAR ACTIVOS NETOS DE OP'N."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    

                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label87" runat="server" Visible="false" Text="Inventarios a consignación KEY (en días)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtInventarioKeyConsignacion" Visible="false" runat="server" MaxLength="4"
                                            MinValue="0" Width="80px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="6">
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="6">
                                        <asp:Label ID="Label96" runat="server" Font-Bold="True" Text="CONDICIONES GENERALES"></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    
                                   
                                    <td>
                                        <asp:Label ID="Label90" runat="server" Visible="false" Text="Contribución a los costos fijos (%  sobre venta de PAPEL)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCostosFijosPapel" runat="server" Visible="false" Enabled="False"
                                            MaxLength="9" MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label85" runat="server" Visible="false" Text="UCS's (%)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtUcs" runat="server" Visible="false" Enabled="False" MaxLength="9"
                                            MinValue="0" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                               
                                
                                <tr>
                                    <td>
                                        <asp:Label ID="Label73" runat="server" Visible="false" Text="% de comisión por cruce de andén (por surtir a través de un CEDIS)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtComisionCruce" runat="server" Visible="false" MaxLength="9" MinValue="0"
                                            Width="80px" MaxValue="100">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    
                                    
                                </tr>
                                <tr>                                    
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label84" runat="server" Visible="false" Text="Crédito del proveedor de PAPEL (en días)"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCreditoProveedorPapel" runat="server" Visible="false" Enabled="False"
                                            MaxLength="4" MinValue="0" Width="80px">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents OnKeyPress="SoloNumerico" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="310">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td width="310">
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
                            <%--</asp:Panel>--%>
                              </telerik:RadPane>
                         </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="hiddenId" runat="server" />
                    <asp:HiddenField ID="HD_IVAfacturacion" runat="server" Value="0" />
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
