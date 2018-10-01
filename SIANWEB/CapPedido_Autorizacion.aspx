<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapPedido_Autorizacion.aspx.cs" Inherits="SIANWEB.CapPedido_Autorizacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EventName="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
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
    <div id="divPrincipal" runat="server">
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
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="80">
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
                                <asp:Label ID="Label1" runat="server" Text="Nombre" />
                                &nbsp;del cliente
                            </td>
                            <td colspan="9">
                                <telerik:RadTextBox ID="txtNombre" runat="server" Width="380px" MaxLength="200">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Cliente inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Cliente final" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha inicial" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px" Culture="es-MX">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Fecha final" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px" Culture="es-MX">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
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
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgPedido" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="rg_NeedDataSource" EnableLinqExpressions="False" PageSize="15"
                        AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnPageIndexChanged="rg_PageIndexChanged" OnItemCommand="rgPedido_ItemCommand"
                        OnItemDataBound="rgPedido_ItemDataBound">
                        <MasterTableView ClientDataKeyNames="Id_Ped">
                            <Columns>
                                <telerik:GridBoundColumn DataField="Ped_TipoStr" HeaderText="Tipo" UniqueName="Ped_TipoStr"
                                    Display="false">
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle HorizontalAlign="left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Tipo" HeaderText="Tipo" UniqueName="Ped_Tipo"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_U" HeaderText="Usuario" UniqueName="Id_U"
                                    Display="false">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="Estatus">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Pedido" UniqueName="Id_Ped">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Fecha" HeaderText="Fecha" UniqueName="Ped_Fecha"
                                    DataFormatString="{0:dd/MM/yy}">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. cte." UniqueName="Id_Cte">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Subtotal" HeaderText="Subtotal" UniqueName="Ped_Subtotal"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Iva" HeaderText="I.V.A." UniqueName="Ped_Iva"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ped_Total" HeaderText="Total" UniqueName="Ped_Total"
                                    DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Facturacion" HeaderText="Permitir Facturar" UniqueName="Cte_Facturacion"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" ConfirmDialogType="RadWindow"
                                    ConfirmText="¿Desea autorizar el pedido <b>#[[ID]]</b>?</br></br>" Text="Autorizar"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" UniqueName="Autorizar"
                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="aceptar">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_PED" runat="server" Value="" />
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('ok');
                }
                else {
                    ajaxManager.ajaxRequest('cancel');
                }
            }

            function ToolBar_ClientClick(sender, args) {
                AbrirVentana_Pedidos(-1);
                args.set_cancel(true);
            }

            function AbrirVentana_OrdenCompra_Edicion(Id) {
                AbrirVentana_Pedidos(Id);
                return false;
            }

            function AbrirVentana_Pedidos(Id) {
                //debugger;
                var oWnd = radopen("CapPedido.aspx?Id=" + Id + "&PermisoGuardar=<%= _PermisoGuardar %>&PermisoModificar=<%= _PermisoModificar %>&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_Pedido");
                oWnd.center();
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
