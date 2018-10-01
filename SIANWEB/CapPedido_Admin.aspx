<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapPedido_Admin.aspx.cs" Inherits="SIANWEB.CapPedido_Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls> 
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
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
            <telerik:AjaxSetting AjaxControlID="btnBuscar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgPedido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
       </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
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
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
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
                            </td>
                            <td colspan="9">
                                <telerik:RadTextBox ID="txtNombre" runat="server" Width="380px">
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
                                Tipo
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
                                <asp:Label ID="Label4" runat="server" Text="Fecha inicial" />
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
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Fecha final" />
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
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Estatus" />
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
                                <asp:Label ID="Label8" runat="server" Text="Pedido inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPedido1" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Pedido final" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPedido2" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar1" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
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
                        <tr runat="server" id="drUsuario">
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Usuario" />
                            </td>
                            <td colspan="4">
                                <telerik:RadComboBox ID="cmbUsuario" runat="server" Width="300px" MarkFirstMatch="true"
                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" Height="350px">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 50px; text-align: Right">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                <asp:HiddenField ID="HF_ClvPag" runat="server" />
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
                    <asp:Panel ID="Panel1" runat="server" Width="900px" ScrollBars="Horizontal">
                        <telerik:RadGrid ID="rgPedido" runat="server" AutoGenerateColumns="False" GridLines="None"
                            OnNeedDataSource="rg_NeedDataSource" EnableLinqExpressions="False" PageSize="10"
                            AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnPageIndexChanged="rg_PageIndexChanged" OnItemCommand="rgPedido_ItemCommand"
                            OnItemDataBound="rg_ItemDataBound">
                            <MasterTableView ClientDataKeyNames="Id_Ped,Ped_Tipo">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Ped_TipoStr" HeaderText="Tipo" UniqueName="Ped_TipoStr">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ped_Tipo" HeaderText="Tipo" UniqueName="Ped_Tipo"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="U_Nombre" HeaderText="Usuario" UniqueName="U_Nombre">
                                        <HeaderStyle Width="200px" />
                                      <%--  <ItemStyle HorizontalAlign="Right" />--%>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EstatusStr" HeaderText="Estatus" UniqueName="Estatus">
                                        <HeaderStyle Width="110px" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Ped" HeaderText="Pedido" UniqueName="Id_Ped">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ped_Fecha" HeaderText="Fecha" UniqueName="Ped_Fecha"
                                        DataFormatString="{0:dd/MM/yy}">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte">
                                        <HeaderStyle Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                        <HeaderStyle Width="400px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ped_Subtotal" HeaderText="Subtotal" UniqueName="Ped_Subtotal"
                                        DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ped_Iva" HeaderText="I.V.A." UniqueName="Ped_Iva"
                                        DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ped_Total" HeaderText="Total" UniqueName="Ped_Total"
                                        DataFormatString="{0:N2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Facturacion" HeaderText="Permitir Facturar" UniqueName="Cte_Facturacion"
                                        Visible="false">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="BajaAdmin" HeaderText="Baja por administración"
                                        ConfirmDialogType="RadWindow" ConfirmText="¿Está seguro de dar de baja el pedido <b>#[[ID]]</b> por parte de administración?</br></br>"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Baja por administración"
                                        UniqueName="BajaAdmin" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="baja">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="90px"></HeaderStyle>
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn CommandName="BajaCliente" HeaderText="Baja por cliente"
                                        ConfirmDialogType="RadWindow" ConfirmText="¿Está seguro de dar de baja el pedido <b>#[[ID]]</b> por parte del cliente?</br></br>"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Baja por cliente"
                                        UniqueName="BajaCliente" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="baja">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="70px"></HeaderStyle>
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                        ConfirmText="Se imprimirá el pedido, tenga listo el formato en la impresora</br></br>"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                        Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="70px"></HeaderStyle>
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>                          
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" ShowPagerText="True" PageButtonCount="3" 
                                PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >." />
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_PED" runat="server" Value="" />
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function AbrirVentana_OrdenCompra_Edicion(Id) {
                AbrirVentana_Pedidos(Id);
                return false;
            }
            function AbrirVentana_Pedidos(Id, guardar, modificar, eliminar, imprimir) {
                //debugger;
                var oWnd = radopen("CapPedido.aspx?Id=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir, "AbrirVentana_Pedido");
                oWnd.center();
                oWnd.Maximize();
                return false;
            }
            function AbrirVentana_ProPedidoVI(Id, guardar, modificar, eliminar, imprimir, Anio, semana) {
                //debugger;
                var oWnd = radopen("ProPedidoVI.aspx?IdP=" + Id + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar + "&PermisoEliminar=" + eliminar + "&PermisoImprimir=" + imprimir + "&Anio=" + Anio + "&Semana=" + semana, "AbrirVentana_PedidoVI");
                oWnd.center();
                oWnd.Maximize();
            }
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
