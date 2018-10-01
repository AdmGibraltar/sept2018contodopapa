<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="ProEntradaVirtual_Admin.aspx.cs" Inherits="SIANWEB.ProEntradaVirtual_Admin" %>

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
                    var cell = MasterTable.getCellByColumnUniqueName(row, "Id_Env");
                    //here cell.innerHTML holds the value of the cell

                    RowFolio = cell.innerHTML;
                }


                function ToolBar_ClientClick(sender, args) {

                    var button = args.get_item();

                    switch (button.get_value()) {

                        case 'new':
                            AbrirVentana_EntradaVirtual(-1, 4);
                            args.set_cancel(true);
                            break;

                        case 'mail':
                            enviarSolicitudPorCorreo();
                            break;
                           

                    }
                }

                function AbrirVentana_EntradaVirtual(RowFolio, tipoAccion) {
                    //debugger;

                    if (RowFolio == null) {
                        radalert("Se debe seleccionar una solicitud del grid antes de presionar este botón.", 330, 150);
                        return false;
                    }

                    var permisoGuardar = 'true'
                    var permisoModificar = 'true'
                    var permisoEliminar = 'true'
                    var permisoImprimir = 'true'

                    var oWnd = radopen("ProEntradaVirtual.aspx?Id_Folio=" + RowFolio + "&accion=" + tipoAccion + "&PermisoGuardar=" +
                permisoGuardar + "&PermisoModificar=" + permisoModificar + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir="
                + permisoImprimir, "AbrirVentana_EntradaVirtual");
                    oWnd.Maximize();
                    oWnd.center();
                    oWnd.add_close(function () {
                        refreshGrid();
                    });
                }

                //Abrir ventana precio especial autorizacion

                function AbrirVentana_EntradaVirtualAutorizacion(ApeUnique,idEmp,idCd, IdT) {
                    //debugger;


                    var oWnd = radopen("ProEntradaVirtual_Autorizacion.aspx?Id1=" + ApeUnique + "&Id2=" + idEmp + "&Id3=" + idCd + "&Id4=" + IdT, "AbrirVentana_EntradaVirtualAutorizacion");

                    oWnd.center();
                    oWnd.add_close(function () {
                        CloseAndRebind();
                    });
                }

                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                    return oWindow;
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

                function CloseAndRebind() {
                    //debugger;
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.refreshGrid(null);
                    return false;
                }


                function enviarSolicitudPorCorreo() {

                    if (RowFolio == null) {
                        radalert("Se debe seleccionar una solicitud del grid antes de presionar este botón.", 330, 150);
                        return false;
                    }

                    alert("(<enviar solicitud terminada por correo>)");

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

        <%-- Corrección MPS --%>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <%-- Prueba --%>
                <telerik:RadWindow ID="AbrirVentana_EntradaVirtual" runat="server" Behaviors="Move, Close, Maximize"
                    Opacity="100" VisibleStatusbar="False" Width="940px" Height="645px" Animation="Fade"
                    ShowContentDuringLoad="false" KeepInScreenBounds="True" Overlay="True" Title="Entrada Virtual"
                    Modal="True" OnClientClose="" OnClientPageLoad=""
                    Localization-Restore="Restaurar" Localization-Maximize="Maximizar" Localization-Close="Cerrar"
                    InitialBehaviors="Maximize">
                </telerik:RadWindow>      
            </Windows>
        </telerik:RadWindowManager>
        <%-- fin --%>

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
                            <MasterTableView ClientDataKeyNames="Id_Env">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>

                                    <telerik:GridBoundColumn DataField="Id_Env" HeaderText="Folio" UniqueName="Id_Env">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Env_Fecha" HeaderText="Fecha" UniqueName="Env_Fecha"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Env_Estatus" HeaderText="Estatus" UniqueName="Env_Estatus"
                                        Display="false">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Env_EstatusStr" HeaderText="Estatus" UniqueName="Env_EstatusStr">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Cliente" UniqueName="Id_Cte">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Env_CteNomComercial" HeaderText="Nombre" UniqueName="Env_CteNomComercial">
                                        <HeaderStyle Width="400px" />
                                    </telerik:GridBoundColumn>
                                       
                                     <telerik:GridBoundColumn DataField="Id_Es" HeaderText="Folio de Entrada" UniqueName="Id_Es">
                                        <HeaderStyle Width="80" />
                                    </telerik:GridBoundColumn>
                                                                
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                                                      
                                    <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Desea enviar la solicitud <b>#[[ID]]</b>?" Text="Enviar" UniqueName="Enviar"
                                        Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </telerik:GridButtonColumn>

                                      <telerik:GridTemplateColumn HeaderText="Autorizar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Autorizar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Autorizar" CommandName="autorizar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>

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
