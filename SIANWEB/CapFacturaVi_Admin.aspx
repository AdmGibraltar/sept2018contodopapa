<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapFacturaVi_Admin.aspx.cs" Inherits="SIANWEB.CapFacturaVi_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgPedido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />                
                <telerik:RadToolBarButton CommandName="generarR" Value="generar" ToolTip="Nueva Remisión"
                    CssClass="new" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
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
                                <asp:Label ID="Label7" runat="server" Text="Nombre del cliente"></asp:Label>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txtNombre" runat="server" Width="260px" MaxLength="50">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td width="10">
                            </td>
                            <td width="10">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Cliente inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Cliente final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Asignado"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Fecha Fact. inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechaFIni" runat="server" Culture="es-MX" Width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Fecha Fact. final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechaFFin" runat="server" Culture="es-MX" Width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>                           
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha Pedido inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha1" runat="server" Culture="es-MX" Width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Fecha Pedido final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha2" runat="server" Culture="es-MX" Width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rdFactura" runat="server" Text="Factura" Checked="True" GroupName="documento" />
                                <asp:RadioButton ID="rdRemision" runat="server" Text="Remisión" GroupName="documento" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" ToolTip="Buscar"
                                    OnClick="btnBuscar_Click" />
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
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" Width="1200px">
                                    <telerik:RadGrid ID="rgPedido" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        EnableLinqExpressions="False" PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                        OnNeedDataSource="rgPedido_NeedDataSource" OnItemCommand="rgPedido_ItemCommand"
                                        OnItemDataBound="rgPedido_ItemDataBound" OnPageIndexChanged="rgPedido_PageIndexChanged">
                                        <MasterTableView ClientDataKeyNames="Id_Ped, Cte_Nom">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="Seleccionar">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSeleccionar_All" runat="server" Text="" ToolTip="Seleccionar/deseleccionar todos"
                                                            AutoPostBack="true" OnCheckedChanged="SelTodo_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSeleccionar" runat="server" OnCheckedChanged="Sel_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Folio" UniqueName="Id_Ped">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Acs_Fecha" HeaderText="Fecha Fact." UniqueName="Acs_Fecha"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Ped_Fecha" HeaderText="Fecha Pedido" UniqueName="Ped_Fecha"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_U" HeaderText="Usuario" UniqueName="Id_U"
                                                    Display="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="U_Nombre" HeaderText="Usuario" UniqueName="Usuario">
                                                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cte_Nom" HeaderText="Cliente" UniqueName="Cte_Nom">
                                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Terr." UniqueName="Id_Ter">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Ped_Total" HeaderText="Vta. inst." UniqueName="Ped_Total"
                                                    DataFormatString="{0:N2}">
                                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Ped_Comentarios" HeaderText="Notas" UniqueName="Ped_Comentarios">
                                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Ped_Asign" HeaderText="Asignado" UniqueName="Ped_Asign"
                                                    Display="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Ped_AsignStr" HeaderText="Asignado" UniqueName="Ped_AsignStr">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Rut_Descripcion" HeaderText="Ruta de entrega"
                                                    UniqueName="Rut_Descripcion" Display="false">
                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Asignar" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgAsignar" runat="server" ImageUrl="~/Imagenes/blank.png" CssClass="aceptar"
                                                            ToolTip="Asignar" CommandName="Asignar" Visible='<%# DataBinder.Eval(Container.DataItem, "Ped_Asign").ToString() == "A" ? false : true  %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Desasignar" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                    UniqueName="Desasignar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDesasignar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                            CssClass="aceptar" ToolTip="Desasignar" CommandName="Desasignar" Visible='<%# DataBinder.Eval(Container.DataItem, "Ped_Asign").ToString() != "N" ? true : false  %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Facturar" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                    UniqueName="BtnFacturar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgFacturar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                            CssClass="aceptar" ToolTip="Facturar" CommandName="Facturar" Visible='<%# DataBinder.Eval(Container.DataItem, "Ped_Asign").ToString() != "N" ? true : false  %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Remisionar" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                    UniqueName="BtnRemisionar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgRemisionar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                            CssClass="aceptar" ToolTip="Remisionar" CommandName="Remisionar" Visible='<%# DataBinder.Eval(Container.DataItem, "Ped_Asign").ToString() != "N" ? true : false  %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Cte_Credito" HeaderText="Credito" UniqueName="Cte_Credito"
                                                    Display="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn CommandName="Baja" HeaderText="Cancelar" ConfirmDialogType="RadWindow"
                                                    ConfirmText="¿Está seguro de dar de baja el pedido [[ID]]?</br></br>" ConfirmDialogHeight="150px"
                                                    ConfirmDialogWidth="350px" Text="Cancelación de pedido de venta instalada" UniqueName="Baja"
                                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="70px"></HeaderStyle>
                                                </telerik:GridButtonColumn>
                                            </Columns>
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
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_PED" runat="server" Value="" />
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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

            function confirmCallBackFnVIRem(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {

                    ajaxManager.ajaxRequest('okRem');
                }
                else {
                    ajaxManager.ajaxRequest('no');
                }
            }

            function ToolBar_ClientClick2(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'generarR':
                        AbrirVentana_Pagos(1, -1, true, true, true, true);
                        args.set_cancel(true);
                        break;
                }
            }

            function Confirma() {
                radconfirm("¿Desea imprimir la factura?", confirmCallBackFnVI, 400, 160)
                return false;
            }

            function ConfirmaRem() {
                radconfirm("¿Desea imprimir la Remision?", confirmCallBackFnVIRem, 400, 160)
                return false;
            }


            function AbrirVentana_AsigPrdxPed(Id, Id_Cte, Nom_Cte) {
                var oWnd = radopen("ProAsignPrdxPed.aspx?Id=" + Id + "&Id_Cte=" + Id_Cte + "&Nom_Cte=" + Nom_Cte + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_ProAsignPrdxPed");
                oWnd.center();
            }

            function AbrirVentana_Remision(Id_Rem, PermisoGuardar) {
                //debugger;
                var oWnd = radopen("CapRemisiones.aspx?tdm=3&PedRem=" + Id_Rem + "&PermisoGuardar=" + PermisoGuardar, "AbrirVentana_Remision");
                oWnd.center();
                oWnd.Maximize();
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('AsAuto');
                }
                else {
                    ajaxManager.ajaxRequest('AsManual');
                }
            }
            function confirmCallBackFnDes(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('DesAuto');
                }
                else {
                    ajaxManager.ajaxRequest('DesManual');
                }
            }
            function AbrirVentana_Factura(Id_Emp, Id_Cd, Id_Fac, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                //debugger;
                var oWnd = radopen("CapFactura.aspx?Id_Fac=" + Id_Fac
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&facModificable=1"
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    + "&tipo=vi"
                    , "AbrirVentana_FacturacionVi");
                oWnd.center();
                oWnd.Maximize();
            }

            function AbrirFacturaPDF(WebURL) {
                //debugger;
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFacturaVI");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function AbrirFacturaPDFCN(WebURL, WebURLCN) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFacturaVI");
                oWnd.set_showOnTopWhenMaximized(false);
                if (WebURLCN != '') {
                    oWnd.argument = WebURLCN
                    oWnd.add_close(AbrirFacturaPDF);
                }
                oWnd.center();
            }
            function AbrirVentana_Pagos(tdm, Id_Rem, PermisoGuardar) {
                //debugger;
                var oWnd = radopen("CapRemisiones.aspx?tdm=" + tdm + "&Id_Rem=" + Id_Rem + "&PermisoGuardar=" + PermisoGuardar, "AbrirVentana_Remision");
                oWnd.center();
                oWnd.Maximize();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
