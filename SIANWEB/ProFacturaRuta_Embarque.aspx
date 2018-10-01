<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProFacturaRuta_Embarque.aspx.cs" Inherits="SIANWEB.ProFacturaRuta_Embarque" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            // ---------------------
            // Variables de permiso
            // ---------------------
            function ToolBar_ClientClick(sender, args) {
                debugger;
                var button = args.get_item();
                var ProcEmbAlm = '<%= ProcEmbAlm %>'
                if (ProcEmbAlm == 'True') {
                    switch (button.get_value()) {
                        case 'new':
                            continuarAccion = false;
                            AbrirVentana(0, 0, 0, 1);
                            break;
                    }
                }
                else {
                    var alertaMsg = radalert('El proceso no esta activado para el CDI', 330, 150, tituloMensajes);
                    args.set_cancel(!continuarAccion);
                }

            }

            function AbrirVentana_Edicion(Id) {
                AbrirVentana(Id);
                return false;
            }

            function LimpiarBanderaRebind() {
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de seleccion de impresion
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Impresion(Id_Emp, Id_Cd, Id_Emb) {
                var oWnd = radopen("ProFacturaRuta_Imprimir.aspx?Id_Emb=" + Id_Emb
                  + "&Id_Cd=" + Id_Cd + "&Id_Emp=" + Id_Emp
                  + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>"
                  + "&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>"
                  , "AbrirVentana_EmbarqueImpresion");
                oWnd.center();
            }

            function AbrirVentana(Id_Emp, Id_Cd, Id, embModificable) {
                var oWnd = radopen("ProFacturaRuta.aspx?Id=" + Id
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&embModificable=" + embModificable
                    + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>"
                    + "&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>"
                    , "AbrirVentana_FacturasXRuta");
               // oWnd.center();
                oWnd.Maximize();
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura, cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Embarque_Edicion(Id_Emp, Id_Cd, Id, embModificable) {
                AbrirVentana(Id_Emp, Id_Cd, Id, embModificable);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de factura
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_Embarque(Id_Emp, Id_Cd, Id_Emb, embModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                var oWnd = radopen("ProFacturaRuta.aspx?Id_Emb=" + Id_Emb
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&embModificable=" + embModificable
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    , "AbrirVentana_FacturasXRuta");
                //oWnd.center();
                oWnd.Maximize();
            }

            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaRuta" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaRuta" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturaRuta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaRuta" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtNumero1" />
                    <telerik:AjaxUpdatedControl ControlID="txtNumero2" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaRuta" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaRuta" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
              <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged1"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <table>
                        <tr>
                            <td width="100">
                            </td>
                            <td width="90">
                            </td>
                            <td width="10">
                            </td>
                            <td width="50">
                            </td>
                            <td width="90">
                            </td>
                            <td width="50">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha inicial
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="120px" DateInput-MaxLength="10">
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
                            <td width="65">
                                Fecha final
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="120px" DateInput-MaxLength="10">
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
                                Embarque
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtEmbarque" runat="server" Width="90px" MaxLength="9" 
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
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
                    <telerik:RadGrid ID="rgFacturaRuta" runat="server" AutoGenerateColumns="False" GridLines="None"
                        PageSize="10" Widht="900px" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                        OnNeedDataSource="RadGrid1_NeedDataSource" OnPageIndexChanged="rg_PageIndexChanged"
                        EnableLinqExpressions="False" OnItemCommand="rg_ItemCommand" OnItemDataBound="rgFacturaRuta_ItemDataBound">
                        <MasterTableView ClientDataKeyNames="Id_Emb">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Empresa" UniqueName="Id_Emp"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Ciudad" UniqueName="Id_Cd"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Emb_EstatusStr" HeaderText="Estatus" UniqueName="Emb_EstatusStr">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="U_Nombre" HeaderText="Usuario" UniqueName="U_Nombre">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Emb" HeaderText="Embarque" UniqueName="Id_Emb">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Emb_Fec" HeaderText="Fecha" UniqueName="Emb_Fec"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Emb_Dia" HeaderText="Día" UniqueName="Emb_Dia"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Emb_Chofer" HeaderText="Chofer" UniqueName="Emb_Chofer">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Emb_Camioneta" HeaderText="Camioneta" UniqueName="Emb_Camioneta">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                    ConfirmText="¿Está seguro de dar de baja el registro?" ConfirmDialogHeight="150px"
                                    ConfirmDialogWidth="350px" Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton"
                                    ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                            ShowPagerText="True" PageButtonCount="3" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
