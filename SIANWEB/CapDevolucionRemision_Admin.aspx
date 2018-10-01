<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPage01.master" CodeBehind="CapDevolucionRemision_Admin.aspx.cs" Inherits="SIANWEB.CapDevolucionRemision_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                var RowFolio;


                function RowSelected(sender, eventArgs) {
                    var grid = sender;
                    var MasterTable = grid.get_masterTableView();
                    var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
                    var cell = MasterTable.getCellByColumnUniqueName(row, "Id_Devrem");
                    //here cell.innerHTML holds the value of the cell

                    RowFolio = cell.innerHTML;
                }


                function ToolBar_ClientClick(sender, args) {

                    var button = args.get_item();

                    switch (button.get_value()) {

                        case 'new':
                            AbrirVentana_DevolucionRemision(-1);
                            args.set_cancel(true);
                            break;


                    }
                }
                                
                function AbrirVentana_DevolucionRemision(RowFolio) {
                    //debugger;

                    if (RowFolio == null) {
                        radalert("Se debe seleccionar una solicitud del grid antes de presionar este botón.", 330, 150);
                        return false;
                    }

                    var permisoGuardar = 'false'
                    var permisoModificar = 'false'
                    var permisoEliminar = 'false'
                    var permisoImprimir = 'false'

                    var oWnd = radopen("CapDevolucionRemision.aspx?Id_Folio=" + RowFolio +  "&PermisoGuardar=" +
                permisoGuardar + "&PermisoModificar=" + permisoModificar + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir="
                + permisoImprimir, "AbrirVentana_DevolucionRemision");
                    oWnd.Maximize();
                    oWnd.center();
                }

                function AbrirFacturaPDF(oWnd, eventArgs) {
                    var oWnd1 = radopen(oWnd.argument, "AbrirVentana_ImpresionPDFFactura");
                    oWnd1.set_showOnTopWhenMaximized(false);
                    oWnd1.center();
                    oWnd.remove_close(AbrirFacturaPDF);
                }
                function AbrirFacturaPDFCN(WebURL, WebURLCN) {
                    var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                    oWnd.set_showOnTopWhenMaximized(false);
                    if (WebURLCN != '') {
                        oWnd.argument = WebURLCN
                        oWnd.add_close(AbrirFacturaPDF);
                    }
                    oWnd.center();
                }

                function refreshGrid() {
                    //debugger;
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    ajaxManager.ajaxRequest('RebindGrid');
                }

                function CerrarWindow_Event(sender, eventArgs) {
                    //debugger;
                    refreshGrid();
                }

                function CerrarWindow_ClientEvent(sender, eventArgs) {
                    var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                    if (HD_GridRebind.value == '1') {
                        refreshGrid_Fac('RebindGrid');
                    }
                    else {
                        refreshGrid_Fac('FacturacionVarialesSesionDestruir');
                    }
                }


                function refreshGrid_Fac(accion) {
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    ajaxManager.ajaxRequest(accion);
                }


                function LimpiarBanderaRebind(sender, eventArgs) {
                    ModificaBanderaRebind('0');
                }

                function ActivarBanderaRebind() {
                    ModificaBanderaRebind('1');
                }

                function ModificaBanderaRebind(valor) {
                    var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                    HD_GridRebind.value = valor;
                }

                function txtTipoId_OnBlur(sender, args) {
                    OnBlur(sender, $find('<%= cmbTipoMovimiento.ClientID %>'));
                }
                function cmbTipoMovimiento_ClientSelectedIndexChanged(sender, eventArgs) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoId.ClientID %>'));
                }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RAM1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
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
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rg1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>       
            <%-- Factura (Impresion PDF) --%>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>     
            <telerik:RadWindow ID="AbrirVentana_DevolucionRemision" runat="server" Behaviors="Move, Close, Maximize"
                Opacity="100" VisibleStatusbar="False" Width="940px" Height="645px" Animation="Fade"
                ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Devolución de Remisiones"
                Modal="True" OnClientClose="CerrarWindow_ClientEvent" OnClientPageLoad="LimpiarBanderaRebind"
                Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
                InitialBehaviors="Maximize">
            </telerik:RadWindow>
          </Windows>
        </telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div runat="server" id="divPrincipal">
            <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick">
                <Items>
                    <telerik:RadToolBarButton Width="20px" Enabled="False" />
                    <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                        ImageUrl="~/Imagenes/blank.png" />
                </Items>
            </telerik:RadToolBar>
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                        <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_spo" runat="server" />
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="lblCentro" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td width="90">
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
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="middle">
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
                                    <asp:Label ID="lblFolioIni" runat="server" Text="Folio inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtFolioIni" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblFolioFin" runat="server" Text="Folio final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtFolioFin" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
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
                                <td>
                                    <asp:Label ID="lblFechaIni" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dpFechaIni" runat="server" Width="100px" Culture="es-MX">
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaFin" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dpFechaFin" runat="server" Width="100px" Culture="es-MX">
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="153px" LoadingMessage="Cargando..."
                                        OnClientBlur="Combo_ClientBlur">
                                    </telerik:RadComboBox>
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
                                    <asp:Label ID="LblCte" runat="server" Text="Cliente inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtClienteIni" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Lblcte2" runat="server" Text="Cliente final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtClienteFin" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp; <%--<asp:Label ID="LblSolicitud" runat="server" Text="No. Solicitud"></asp:Label>--%>
                                </td>
                                <td>
                                 &nbsp;
                                    <%-- <telerik:RadNumericTextBox ID="txtSolicitud" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>--%>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                        ToolTip="Buscar" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" OnNeedDataSource="rg1_NeedDataSource" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnItemCommand="rg1_ItemCommand" OnItemDataBound="rg1_ItemDataBound" OnPageIndexChanged="rg1_PageIndexChanged">
                            <MasterTableView ClientDataKeyNames="Id_DevRem">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Empresa Id" UniqueName="Id_Emp" Display="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="CDI" UniqueName="Id_Cd" Display="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_DevRem" HeaderText="Folio" UniqueName="Id_DevRem">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="DevRem_Fecha" HeaderText="Fecha" UniqueName="DevRem_Fecha"  DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_U" HeaderText="# Usuario" UniqueName="Id_U">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="U_Nombre" HeaderText="Usuario" UniqueName="U_Nombre">
                                    <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="# Cliente" UniqueName="Id_Cte">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DevRem_CteNombre" HeaderText="Nombre" UniqueName="DevRem_CteNombre">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Tm" HeaderText="# Num Mov." UniqueName="Id_Tm">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>       
                                     <telerik:GridBoundColumn DataField="Tm_nombre" HeaderText="Movimiento" UniqueName="Tm_nombre">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>                                         
                                     <telerik:GridBoundColumn DataField="Id_Fac" HeaderText="Folio de Factura" UniqueName="Id_Fac">
                                        <HeaderStyle Width="80" />
                                    </telerik:GridBoundColumn> 
                                    <telerik:GridBoundColumn DataField="NumEntradas" HeaderText="# Entradas" UniqueName="NumEntradas">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DevRem_EstatusStr" HeaderText="Estatus" UniqueName="DevRem_EstatusStr">
                                        <HeaderStyle Width="80" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DevRem_Estatus" HeaderText="Estatus" UniqueName="DevRem_Estatus" Display=false>
                                        <HeaderStyle Width="80" />
                                    </telerik:GridBoundColumn>                             
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="editar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                        ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir" ConfirmDialogHeight="150px"
                                        ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>     
                                    <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Cancelar" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Está seguro de cancelar la solicitud?" ConfirmDialogHeight="150px"
                                        ConfirmDialogWidth="350px" Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton"
                                        ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
