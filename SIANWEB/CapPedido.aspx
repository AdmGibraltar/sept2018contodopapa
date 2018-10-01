<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapPedido.aspx.cs" Inherits="SIANWEB.CapPedido" %>

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
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting> 
            <telerik:AjaxSetting AjaxControlID="rdFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgDetalles">
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
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
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
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                </td>
                <td width="150px">
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" ValidationGroup="guardar" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;talles" AccessKey="E" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <%--Width="815px" Height="300px">--%>
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" Height="300px">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="300px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                                    <div runat="server" id="divGenerales">
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="120">
                                                </td>
                                                <td width="75">
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td width="130">
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td>
                                                </td>
                                                <td width="10">
                                                </td>
                                                <td colspan="2" width="200">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="Folio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" Enabled="False"
                                                        MaxLength="9" MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
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
                                                    <asp:Label ID="Label3" runat="server" Text="Fecha de pedido"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="rdFecha" runat="server" Width="100px" OnSelectedDateChanged="rdFecha_SelectedDateChanged"
                                                        AutoPostBack="true">
                                                        <Calendar ID="Calendar1" runat="server">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy">
                                                            </FastNavigationSettings>
                                                        </Calendar>
                                                        <DateInput runat="server">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="120">
                                                    <asp:Label ID="Label2" runat="server" Text="Cliente"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtNumCliente" runat="server" MinValue="1" Width="70px"
                                                        MaxLength="9" OnTextChanged="txtNumCliente_TextChanged" AutoPostBack="True">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtCliente" runat="server" Width="296px" ReadOnly="True">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCliente"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                        ToolTip="Buscar" ValidationGroup="buscar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Territorio"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MinValue="0" Width="70px"
                                                        MaxLength="9">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txt2_OnBlur" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" EmptyMessage="Seleccionar territorio"
                                                        OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged" OnTextChanged="cmbTerritorio_TextChanged"
                                                        Width="300px" Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        OnClientBlur="Combo_ClientBlur">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: right; vertical-align: top">
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
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbTerritorio"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Representante"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtRepresentanteID" runat="server" MinValue="1" Width="70px"
                                                        MaxLength="9">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txt3_OnBlur" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbRik" runat="server" EmptyMessage="Seleccionar representante"
                                                        OnClientSelectedIndexChanged="cmb3_ClientSelectedIndexChanged" Width="300px"
                                                        Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                        OnClientBlur="Combo_ClientBlur" Enabled="False">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: right; vertical-align: top">
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
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbRik"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="Pedido cliente"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadTextBox ID="txtPedidodel" runat="server" Width="180px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Text="Requisición"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadTextBox ID="txtRequisicion" runat="server" Width="180px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Solicitó"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadTextBox ID="txtSolicito" runat="server" Width="180px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="Flete"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadTextBox ID="txtFlete" runat="server" Width="180px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Orden compra"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadTextBox ID="txtOrden" runat="server" Width="90px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
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
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Text="Condiciones de pago"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtCondiciones" runat="server" MinValue="0" Width="70px"
                                                        MaxLength="9">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="Label8" runat="server" Text="días"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Fecha de entrega"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadDatePicker ID="dpFecha2" runat="server" onpaste="return false" Culture="es-MX"
                                                        Width="100px">
                                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
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
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Text="Observaciones"></asp:Label>
                                                </td>
                                                <td colspan="8">
                                                    <telerik:RadTextBox ID="txtObservaciones" runat="server" Width="470px" MaxLength="250">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text="Descuento 1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtDescuento" runat="server" EmptyMessage="0.00" MinValue="0"
                                                        Value="0" Width="70px" MaxLength="9">
                                                        <ClientEvents OnBlur="CalcularTotales" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server" Text="%"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label18" runat="server" Text="Concepto 1"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadTextBox ID="txtConcepto" runat="server" Width="275px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" Text="Descuento 2"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtDescuento2" runat="server" EmptyMessage="0.00"
                                                        MinValue="0" Value="0" Width="70px" MaxLength="9">
                                                        <ClientEvents OnBlur="CalcularTotales" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label20" runat="server" Text="%"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label21" runat="server" Text="Concepto 2"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadTextBox ID="txtConcepto2" runat="server" Width="275px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="120">
                                                    <asp:HiddenField ID="HF_ID" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="HiddenRebind" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server">
                            <%-- <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="815px">--%>
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="300px" OnClientResized="onResize"
                                    BorderStyle="None">
                                    <telerik:RadGrid ID="rgDetalles" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                        OnItemCommand="rgDetalles_ItemCommand" OnNeedDataSource="rgDetalles_NeedDataSource"
                                        OnPageIndexChanged="rgDetalles_PageIndexChanged" PageSize="15" OnItemDataBound="rgDetalles_ItemDataBound">
                                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace">
                                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Id_PedDet" UniqueName="Prd_Presentacion"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDet" runat="server" Text='<%# Bind("Id_PedDet") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblDet2" runat="server" Text='<%# Bind("Id_PedDet") %>' />
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Ter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTer1" runat="server" Text='<%# Bind("Id_Ter") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtTerritorioPartida" runat="server" MaxLength="9"
                                                            Text='<%# Eval("Id_Ter") %>' Width="100%">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="txtTerritorioPartida_OnBlur" OnKeyPress="handleClickEvent"
                                                                OnLoad="txtTerritorioPartida_OnLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Territorio" UniqueName="Ter_Nombre">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTer" runat="server" Text='<%# Bind("Ter_Nombre") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblTer2" runat="server" Text='<%# Bind("Id_Ter") %>' Visible="false"></asp:Label>
                                                        <telerik:RadComboBox ID="cmbTer" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                            EmptyMessage="Seleccionar cliente" EnableLoadOnDemand="true" Filter="Contains"
                                                            HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                            MaxHeight="300px" OnClientBlur="Combo_ClientBlur" OnClientLoad="cmbTerritorioPartida_OnLoad"
                                                            OnClientSelectedIndexChanged="cmbTerritorioPartida_ClientSelectedIndexChanged"
                                                            OnDataBinding="cmbTerritorio_DataBinding" OnDataBound="cmbTerritorio_DataBound"
                                                            Width="250px">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: right; vertical-align: top">
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
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="270px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Núm." UniqueName="Id_Prod">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProd" runat="server" Text='<%# Bind("Id_Prd") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblProdEdit" runat="server" Text='<%# Bind("Id_Prd") %>' Visible="false" />
                                                        <telerik:RadNumericTextBox ID="txtProd" runat="server" AutoPostBack="true" MaxLength="9"
                                                            MinValue="1" OnTextChanged="cmbProductoDet_TextChanged" Text='<%# Bind("Id_Prd") %>'
                                                            Width="100%" OnLoad="txtProducto_Load">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnBlur="IdPrd_OnBlur" OnLoad="OnIdPrdLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Producto" UniqueName="Prd_Nombre">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescr" runat="server" Text='<%# Bind("Prd_Descripcion") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtProductoNombre" runat="server" ReadOnly="true" Text='<%# Bind("Prd_Descripcion") %>'
                                                            Width="100%">
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="220px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Pres." UniqueName="Prd_Presentacion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPres" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblPres2" runat="server" Text='<%# Bind("Prd_Presentacion") %>' />
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Unidad" UniqueName="Prd_Unidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnidad" runat="server" Text='<%# Bind("Prd_Unidad") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblUnidad2" runat="server" Text='<%# Bind("Prd_Unidad") %>' />
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Precio" UniqueName="Id_Prod">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Prd_Precio", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtPrecio" runat="server" MaxLength="9" MinValue="0"
                                                            Text='<%# Bind("Prd_Precio") %>' Width="100%">
                                                            <ClientEvents OnBlur="OnBlurImporte" OnFocus="OnFocus" OnLoad="OnPrecioLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cantidad" UniqueName="Prd_Cantidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Prd_Cantidad") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" AutoPostBack="true" MaxLength="9"
                                                            MinValue="1" OnTextChanged="txtCantidad_TextChanged" Text='<%# Bind("Prd_Cantidad") %>'
                                                            Width="100%">
                                                            <ClientEvents OnBlur="OnBlurImporte" OnFocus="OnFocus" OnLoad="OnCantidadLoad" />
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Importe" UniqueName="Id_Prod">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("Prd_Importe", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtImporte" runat="server" MaxLength="9" MinValue="0"
                                                            ReadOnly="true" Text='<%# Bind("Prd_Importe") %>' Width="100%">
                                                            <ClientEvents OnFocus="OnFocus" OnLoad="OnImporteLoad" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                    InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px"
                                                    ConfirmDialogWidth="350px" ConfirmDialogType="RadWindow" ConfirmText="¿Borrar este detalle?&lt;/br&gt;&lt;/br&gt;"
                                                    Text="Borrar" UniqueName="DeleteColumn">
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <%-- <ClientSettings>
                                        <Scrolling AllowScroll="True" SaveScrollPosition="True" ScrollHeight="260px" UseStaticHeaders="True" />
                                    </ClientSettings>--%>
                                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPagesToolTip="Páginas siguientes"
                                            NextPageToolTip="Página siguiente" PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                            PageSizeLabelText="Cantidad de registros" PrevPagesToolTip="Páginas anteriores"
                                            PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                    </telerik:RadGrid>
                                    <%-- </asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <div runat="server" id="formularioTotales">
                        <table width="99%">
                            <tr>
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Comentarios"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtComentarios" runat="server" Width="500px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="99%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label23" runat="server" Text="Importe neto"></asp:Label>
                                </td>
                                <td align="right" width="100">
                                    <telerik:RadNumericTextBox ID="txtImporte" runat="server" Width="100px" MinValue="0"
                                        MaxLength="9" Value="0" ReadOnly="True" CssClass="AlignRight">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td align="right" width="80">
                                    <asp:Label ID="Label22" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td width="100">
                                    <telerik:RadNumericTextBox ID="txtSub" runat="server" Width="100px" MinValue="0"
                                        MaxLength="9" Value="0" ReadOnly="True" CssClass="AlignRight">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="right">
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label24" runat="server" Text="I.V.A."></asp:Label>
                                </td>
                                <td width="100">
                                    <telerik:RadNumericTextBox ID="txtIVA" runat="server" Width="100px" MinValue="0"
                                        MaxLength="9" Value="0" ReadOnly="True" CssClass="AlignRight">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="right">
                                </td>
                                <td align="right">
                                    Total
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MinValue="0"
                                        MaxLength="9" Value="0" ReadOnly="True" CssClass="AlignRight">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                //  debugger;
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }
            function txtTerritorioPartida_OnBlur(sender, args) {
                //  debugger; 
                OnBlur(sender, cmbTer);
            }
            function txtTerritorioPartida_OnLoad(sender, args) {
                //  debugger;
                txtTerritorioPartida = sender;
            }
            function cmbTerritorioPartida_OnLoad(sender, args) {
                // debugger;
                cmbTer = sender;
            } //cuando el combo de edición del Grid de TerritorioPartida cambia de indice

            function cmbTerritorioPartida_ClientSelectedIndexChanged(sender, eventArgs) {
                // debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), txtTerritorioPartida);
            }

            function ClientTabSelecting(sender, args) {
                //debugger;
                var Mov = $find('<%= txtRepresentanteID.ClientID %>');
                var Cte = $find('<%= txtNumCliente.ClientID %>');
                var Ter = $find('<%= txtTerritorio.ClientID %>');

                if (Mov.get_value() == "") {
                    radalert('Por favor capture el representante antes de continuar', 330, 150, '');
                    args.set_cancel(true);
                }
                else if (Cte.get_value() == "") {
                    radalert('Por favor capture el cliente antes de continuar', 330, 150, '');
                    args.set_cancel(true);
                }
                else if (Ter.get_value() == "") {
                    radalert('Por favor capture el territorio antes de continuar', 330, 150, '');
                    args.set_cancel(true);
                }
            }
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                // debugger;
                var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();     //habilitar/deshabilitar validators

                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
                //debugger;
                switch (button.get_value()) {
                    case 'save':
                        radTabStrip.get_allTabs()[0].select()
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function rdFecha_OnBlur(sender, args) {
                // debugger;  // Get the current date
                var txtFecha = $find('<%= rdFecha.ClientID %>');
                var dia = txtFecha._dateInput.get_selectedDate();
            }

            function txt1_OnBlur(sender, args) {
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                // debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtNumCliente.ClientID %>'));
            }

            function txt2_OnBlur(sender, args) {
                //debugger;
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                // debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function txt3_OnBlur(sender, args) {
                // debugger;
                OnBlur(sender, $find('<%= cmbRik.ClientID %>'));
            }

            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                // debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRepresentanteID.ClientID %>'));
            }

            function ObtenerControlFecha() {
                // debugger;
                var txtFecha = $find('<%= rdFecha.ClientID %>');
                return txtFecha._dateInput;
            }

            /**/
            var IdPrd;
            var txtId_Prd;
            var DescPrd;
            function OnIdPrdLoad(sender, args) {
                 //debugger;
                IdPrd = sender;
                txtId_Prd = sender;
            }
            function OnDescripcionPrdLoad(sender, args) {
                // debugger;
                DescPrd = sender;
            }
            function IdPrd_OnBlur(sender, eventArgs) {
                //debugger;
                //OnBlur(sender, DescPrd);
            }
            function DescPrd_ClientSelectedIndexChanged(sender, eventArgs) {
                // debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), IdPrd);
            }

            /*TOTALES*/
            var txtPrecio;
            var txtCantidad;
            var txtImporte;
            function OnPrecioLoad(sender, args) {
                //debugger;
                txtPrecio = sender;
            }
            function OnCantidadLoad(sender, args) {
                // debugger;
                txtCantidad = sender;
            }
            function OnImporteLoad(sender, args) {
                //debugger;
                txtImporte = sender;
            }

            function OnBlurImporte() {
                // debugger;
                if (txtPrecio.get_value() != "") {
                    var value1 = txtPrecio.get_value();
                    var value2 = txtCantidad.get_value();
                    if (value2 == 0) {
                        txtCantidad.set_value(1);
                        value2 = 1
                    }
                    txtImporte.set_value(value1 * value2);
                }
                else {
                    return false;
                }
            }

            function CalcularTotales() {
                // debugger;
                var ctr_desc1 = $find('<%= txtDescuento.ClientID %>');
                var ctr_desc2 = $find('<%= txtDescuento2.ClientID %>');
                var ctr_importe = $find('<%= txtImporte.ClientID %>');
                var ctr_subtotal = $find('<%= txtSub.ClientID %>');
                var ctr_iva = $find('<%= txtIVA.ClientID %>');
                var ctr_total = $find('<%= txtTotal.ClientID %>');

                //var SumDesc = ctr_desc1.get_value() + ctr_desc2.get_value();
                var Subtotal = ctr_importe.get_value() * (1 - ctr_desc1.get_value() / 100);
                Subtotal = Subtotal * (1 - ctr_desc2.get_value() / 100);
                var Iva = Subtotal * ('<%= Iva_cd %>' / 100);

                ctr_subtotal.set_value(Subtotal);
                ctr_iva.set_value(Iva);
                ctr_total.set_value(Subtotal + Iva);
            }

            /*CERRAR VENTANA*/
            function GetRadWindow() {
                // debugger;
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                // debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }
            function CloseAlert(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }
            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                // debugger;
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                //debugger;
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function OnFocus() {
                //debugger;
                if (IdPrd.get_value() == '') {
                    AlertaFocus('Capture el producto para continuar', IdPrd._clientID);
                }
            }
            function abrirBuscar() {
                //debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtNumCliente.ClientID%>');
                            var oWnd = radopen("Ventana_Buscar.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarPrecio");
                            oWnd.center();
                        }
                    }
                }
            }
            function abrirEstadistica() {
                //debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtNumCliente.ClientID%>');
                            var cteNom = $find('<%=txtCliente.ClientID%>');
                            var oWnd = radopen("Ventana_Estadisticas.aspx?cte=" + cte.get_value() + "&cteNom=" + cteNom.get_value(), "AbrirVentana_BuscarPrecio");
                            oWnd.center();
                        }
                    }
                }
            }
            function abrirIndicadores() {
                //debugger;
                if (txtId_Prd != null) {
                    if (txtId_Prd._focused == true) {
                        if (txtId_Prd._enabled) {
                            var cte = $find('<%=txtNumCliente.ClientID%>');
                            var oWnd = radopen("Ventana_Indicadores.aspx?Precio=true&cte=" + cte.get_value(), "AbrirVentana_BuscarIndicadores");
                            oWnd.center();
                        }
                    }
                }
            }
            function popup() {
                // debugger;
                //if (txtId_Prd._enabled || txtId_Prd == null) {
                    var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                    oWnd.center();
               // }
            }

            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            } 
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
