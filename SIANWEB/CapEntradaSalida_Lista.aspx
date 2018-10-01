<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapEntradaSalida_Lista.aspx.cs" Inherits="SIANWEB.CapEntradaSalida_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators

                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function OpenAlert(mensaje, Id, Nat) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Pagos(Id, Nat, false, false, false, false, 1);
                    });
            }
            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //*******************************
            function AbrirVentana_Pagos_Edicion(Id, Es_Naturaleza) {
                AbrirVentana_Pagos(Id, Es_Naturaleza);
                return false;
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                refreshGrid_Nca('RebindGrid');
            }

            function refreshGrid() {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function AbrirVentana_Pagos(Id, Es_Naturaleza, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, soloVer) {
                //debugger;
                var oWnd = radopen("CapEntSal.aspx?Id=" + Id + "&Es_Naturaleza=" + Es_Naturaleza + "&PermisoGuardar=" + permisoGuardar + "&PermisoModificar=" + permisoModificar + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir=" + permisoImprimir + "&soloVer=" + soloVer, "AbrirVentana_EntSal");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.maximize();
                oWnd.center();
                
            }
            //**********************************************************************
            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var txtCliente1 = $find('<%= txtCliente1.ClientID %>');
                var txtCliente2 = $find('<%= txtCliente2.ClientID %>');
                var txtProveedor1 = $find('<%= txtProveedor1.ClientID %>');
                var txtProveedor2 = $find('<%= txtProveedor2.ClientID %>');
                var datePickerFechaInicio = $find('<%= dpFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= dpFecha2.ClientID %>');
                var txtNotaCredito1 = $find('<%= txtNumero1.ClientID %>');
                var txtNotaCredito2 = $find('<%= txtCliente3.ClientID %>');

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

                //proveedor
                var proveedorinicio = 0;
                if (txtProveedor1.get_textBoxValue() != '') {
                    proveedorinicio = parseFloat(txtProveedor1.get_textBoxValue());
                }

                var proveedorfin = 0;
                if (txtProveedor2.get_textBoxValue() != '') {
                    proveedorfin = parseFloat(txtProveedor2.get_textBoxValue());
                }

                if (proveedorinicio > 0 && proveedorfin > 0 && (proveedorinicio > proveedorfin)) {
                    var alertaProv = radalert('El proveedor inicial no debe ser mayor al proveedor final', 600, 10, tituloMensajes);
                    alertaProv.add_close(
                    function () {
                        txtProveedor1.focus();
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
                    var alertaMsg = radalert('El número inicial no debe ser mayor al número final', 600, 10, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtNotaCredito1.focus();
                    });
                    return false;
                }

                return true;
            }
            //**********************************************************************

            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFRemision");
                oWnd.center();
            }

            function IniciarPaginasAuxiliares() {
                parametros = "ini=1";
                var urlArchivo = 'ObtenerNombreCliente.aspx';
                obtenerrequest(urlArchivo, parametros);

                urlArchivo = 'ObtenerProducto_ES.aspx';
                obtenerrequest(urlArchivo, parametros);

                urlArchivo = 'ObtenerReferencia.aspx';
                obtenerrequest(urlArchivo, parametros);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEntSal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
        OnClientButtonClicking="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                <asp:HiddenField ID="HF_ClvPag" runat="server" />
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label13" runat="server" Text="Centro de distribución" />
            </td>
            <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                    Width="150px" AutoPostBack="True" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div id="divPrincipal" runat="server">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td style="margin-left: 40px">
                    <table>
                        <tr>
                            <td width="120">
                            </td>
                            <td>
                            </td>
                            <td width="10">
                            </td>
                            <td>
                            </td>
                            <td width="50">
                            </td>
                            <td width="10">
                            </td>
                            <td width="80">
                            </td>
                            <td width="50">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Nombre de cliente"></asp:Label>
                            </td>
                            <td colspan="7">
                                <telerik:RadTextBox ID="txtNombre2" runat="server" Width="390px" MaxLength="70">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Cliente inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Cliente final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Man/Auto"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbManAuto" runat="server" Width="130px" LoadingMessage="Cargando..."
                                    OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Proveedor inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProveedor1" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Proveedor final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtProveedor2" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Referencia"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtReferencia2" runat="server" MaxLength="20">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="155px">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="155px">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Estatus"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="130px" LoadingMessage="Cargando..."
                                    OnClientBlur="Combo_ClientBlur">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Número inicial"></asp:Label>
                            </td>
                            <%--numero inicial (copia del sian viejo)--%>
                            <td>
                                <telerik:RadNumericTextBox ID="txtNumero1" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="Número final"></asp:Label>
                            </td>
                            <%--numero final copia del sian viejo--%>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente3" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <%--<asp:Button ID="Button1" runat="server" Text="Buscar" onclick="Button1_Click"/>--%>
                                <asp:ImageButton ID="btnBuscar1" runat="server" ImageUrl="~/Img/find16.png" OnClick="Button1_Click"
                                    ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
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
                    <br />
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="950px">
                        <telerik:RadGrid ID="rgEntSal" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="rgEntSal_ItemDataBound"
                            OnItemCommand="rgEntSal_ItemCommand">
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView>
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Es" HeaderText="Id_Es" UniqueName="Id_Es1"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_U" HeaderText="Usuario" UniqueName="Id_U">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_Estatus" HeaderText="Es_Estatus" UniqueName="Es_Estatus"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_EstatusStr" HeaderText="Estatus" UniqueName="Es_EstatusStr">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_Naturaleza" HeaderText="Es_Naturaleza" UniqueName="Es_Naturaleza"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_NaturalezaStr" HeaderText="Tipo" UniqueName="Es_NaturalezaStr">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ManAutStr" HeaderText="Man/Auto" UniqueName="ManAutStr">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Es" HeaderText="Número" UniqueName="Id_Es">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_Fecha" HeaderText="Fecha" UniqueName="Es_Fecha"
                                        DataFormatString="{0:dd/MM/yy}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Tm" HeaderText="Id_Tm" UniqueName="Id_Tm"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_CteStr" HeaderText="Núm." UniqueName="Id_Cte">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                        <HeaderStyle Width="250px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_PvdStr" HeaderText="Núm." UniqueName="Id_Pvd">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Pvd_Descripcion" HeaderText="Proveedor" UniqueName="Pvd_Descripcion">
                                        <HeaderStyle Width="250px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_SubTotal" HeaderText="Subtotal" UniqueName="Es_SubTotal"
                                        DataFormatString="{0:N2}">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_Iva" HeaderText="IVA" UniqueName="Es_Iva"
                                        DataFormatString="{0:N2}">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_Total" HeaderText="Total" UniqueName="Es_Total"
                                        DataFormatString="{0:N2}">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="Editar" HeaderText="Editar" Text="Editar"
                                        UniqueName="Editar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="edit" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn CommandName="Baja" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Está seguro de eliminar el documento?" ConfirmTitle="" Text="Baja"
                                        UniqueName="Baja" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="baja" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                        ConfirmText="Se imprimirá el documento, tenga listo el formato en la impresora"
                                        ConfirmTitle="" Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                        ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir" ConfirmDialogHeight="150px"
                                        ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
