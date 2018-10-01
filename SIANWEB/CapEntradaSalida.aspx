<%@ Page Title="Entradas y Salidas" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapEntradaSalida.aspx.cs" Inherits="SIANWEB.CapEntradaSalida" %>

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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbNaturaleza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxSubTotal" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxIVA" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dpFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxSubTotal" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxIVA" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipoMovimento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxSubTotal" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxIVA" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtClienteId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbProveedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxSubTotal" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxIVA" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtReferencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtReferencia2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxSubTotal" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxIVA" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEntradaSalida">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxSubTotal" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxIVA" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="RadNumericTextBoxTotal" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                    ImageUrl="Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                    ImageUrl="Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                    ImageUrl="Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="pestaniaDetalles" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="Rts_TabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De<u>t</u>alles" AccessKey="T" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <%--width="820px"  height="270px">--%>
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" heigth="270px">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="270px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td width="120">
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                            </td>
                                            <td>
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="10">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td width="70">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelNaturaleza" runat="server" Text="Naturaleza"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadComboBox ID="cmbNaturaleza" runat="server" Width="150px" AutoPostBack="True"
                                                    Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                    DataTextField="Descripcion" DataValueField="Id" OnSelectedIndexChanged="cmbNaturaleza_SelectedIndexChanged"
                                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" CausesValidation="False">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td colspan="2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbNaturaleza"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                    ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelFolio" runat="server" Text="Folio"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtFolio" runat="server" Width="50px" Enabled="false"
                                                    MinValue="0" MaxLength="9">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelFecha" runat="server" Text="Fecha"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadDatePicker ID="dpFecha" runat="server" Width="90px">
                                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                        ViewSelectorText="x">
                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                            TodayButtonCaption="Hoy" />
                                                        <ClientEvents OnDateClick="Calendar_Click" />
                                                    </Calendar>
                                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                    </DateInput>
                                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td width="120">
                                                <asp:Label ID="LabelTipoMovimiento" runat="server" Text="Tipo de movimiento"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtTipoId" runat="server" Enabled="false" MaxLength="9"
                                                    MinValue="1" Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtTipoId_OnBlur" OnKeyPress="handleClickEvent" OnFocus="_ValidarFechaEnPeriodo" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbTipoMovimento" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                    OnSelectedIndexChanged="cmbTipoMovimento_SelectedIndexChanged" Width="300px"
                                                    MaxHeight="250px" OnClientSelectedIndexChanged="cmbTipoMovimento_ClientSelectedIndexChanged"
                                                    OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbTipoMovimento"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                    ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelCliente" runat="server" Text="Cliente"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtClienteId" runat="server" Enabled="false" MaxLength="9"
                                                    MinValue="1" Width="70px" OnTextChanged="txtClienteId_TextChanged" AutoPostBack="true">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtClienteId_OnBlur" OnKeyPress="handleClickEvent" OnFocus="_ValidarFechaEnPeriodo" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtClienteNombre"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="" ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelProveedor" runat="server" Text="Proveedor"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtProveedorId" runat="server" Enabled="false" MaxLength="9"
                                                    MinValue="1" Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtProveedorId_OnBlur" OnKeyPress="handleClickEvent" OnFocus="_ValidarFechaEnPeriodo" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbProveedor" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                    OnSelectedIndexChanged="cmbProveedor_SelectedIndexChanged" Width="300px" MaxHeight="200px"
                                                    OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged" OnClientBlur="Combo_ClientBlur"
                                                    OnClientFocus="_ValidarFechaEnPeriodo">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cmbProveedor"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                    ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td width="120">
                                                <asp:Label ID="LabelReferencia" runat="server" Text="Referencia"></asp:Label>
                                            </td>
                                            <td style="margin-left: 40px">
                                                <telerik:RadTextBox ID="txtReferencia" runat="server" MaxLength="15" OnTextChanged="txtReferencia_TextChanged"
                                                    Width="70px" AutoPostBack="True">
                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" OnFocus="_ValidarFechaEnPeriodo" />
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtReferencia"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                <telerik:RadNumericTextBox ID="txtReferencia2" runat="server" AutoPostBack="True"
                                                    CausesValidation="True" MaxLength="9" MinValue="1" OnTextChanged="txtReferencia2_TextChanged"
                                                    Visible="false" Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnFocus="_ValidarFechaEnPeriodo" />
                                                </telerik:RadNumericTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReferencia2"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelTerritorio" runat="server" Text="Territorio" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxTerritorio" runat="server" AutoPostBack="True"
                                                    ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                                                    EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                    MarkFirstMatch="true" Width="200px" MaxHeight="250px" OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged"
                                                    OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo" Enabled="False">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td width="120">
                                                <asp:Label ID="LabelNotas" runat="server" Text="Notas"></asp:Label>
                                            </td>
                                            <td rowspan="6">
                                                <telerik:RadTextBox ID="txtNotas" runat="server" CausesValidation="True" Height="90px"
                                                    MaxLength="256" TextMode="MultiLine" Width="440px">
                                                    <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="HF_TipoDoc" runat="server" />
                                    <asp:HiddenField ID="HF_SoloVer" runat="server" />
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="270px">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="270px" OnClientResized="onResize"
                                    BorderStyle="None">
                                    <%--GRID--%>
                                    <%--<asp:Panel runat="server" ID="pnl1" Width="820px" ScrollBars="Horizontal">--%>
                                    <telerik:RadGrid ID="rgEntradaSalida" runat="server" OnNeedDataSource="rgEntradaSalida_NeedDataSource"
                                        AutoGenerateColumns="False" GridLines="None" OnItemCommand="rgEntradaSalida_ItemCommand"
                                        OnInsertCommand="rgEntradaSalida_InsertCommand" OnUpdateCommand="rgEntradaSalida_UpdateCommand"
                                        OnDeleteCommand="rgEntradaSalida_DeleteCommand" OnItemDataBound="rgEntradaSalida_ItemDataBound">
                                        <ClientSettings>
                                            <ClientEvents OnCommand="onCommand" />
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id_Prd, Cantidad, Costo, importe, territorio"
                                            NoMasterRecordsText="No se encontraron registros." EditMode="InPlace">
                                            <CommandItemSettings AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf" RefreshText="Actualizar"
                                                ShowRefreshButton="false" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_EsDet" HeaderText="Id_EsDet" UniqueName="Id_EsDet"
                                                    Display="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="territorio" HeaderText="Territorio" UniqueName="territorio"
                                                    ReadOnly="True" Visible="false">
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="RadComboBox1" runat="server" LoadingMessage="Cargando..."
                                                            OnClientBlur="Combo_ClientBlur" Width="200px" HighlightTemplatedItems="true"
                                                            Filter="Contains" MarkFirstMatch="true">
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
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("territorio") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBox1" runat="server" MaxLength="9" Width="50px"
                                                            OnTextChanged="txtProducto_TextChanged" MinValue="1" AutoPostBack="true" Text='<%# Eval("Id_Prd") %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="ProdLabel" runat="server" Text='<%# Eval("Id_Prd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Descripcion" HeaderText="Producto" UniqueName="Descripcion">
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="DescripcionTextBox" runat="server" ReadOnly="true" Text='<%# Bind("Descripcion") %>'
                                                            Width="100%">
                                                        </telerik:RadTextBox>
                                                        <li class="col5" runat="server" id="LiPrd_AgrupadoSpo" visible="false">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Prd_AgrupadoSpo") %>'
                                                                Visible="false"></asp:Label>
                                                        </li>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Presen" HeaderText="Presen." UniqueName="Presen">
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="PresenTextBox" runat="server" Enabled="False" Text='<%# Bind("Presen") %>'
                                                            Width="50px">
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="PresenLabel" runat="server" Text='<%# Eval("Presen") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Cantidad" HeaderText="Cantid." UniqueName="Cantidad">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxCantidad" runat="server" MinValue="1"
                                                            MaxLength="9" Width="50px" OnTextChanged="Cantidad_TextChanged" AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="CantidadLabel" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Costo" HeaderText="Costo" UniqueName="Costo">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxCosto" runat="server" MinValue="0"
                                                            MaxLength="9" Width="50px" OnTextChanged="Costo_TextChanged" AutoPostBack="true">
                                                        </telerik:RadNumericTextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="CostoLabel" runat="server" Text='<%# Convert.ToDouble(Eval("Costo")).ToString("N") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="importe" HeaderText="Importe" UniqueName="importe"
                                                    ReadOnly="True" DataFormatString="{0:N2}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridCheckBoxColumn DataField="afecta" DataType="System.Boolean" HeaderText="Afecta orden de compra"
                                                    SortExpression="afecta" UniqueName="afecta" ReadOnly="True" Visible="False">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                </telerik:GridCheckBoxColumn>
                                                <telerik:GridCheckBoxColumn DataField="buenEstado" DataType="System.Boolean" HeaderText="Buen estado"
                                                    UniqueName="buenEstado" Visible="False" ReadOnly="True">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </telerik:GridCheckBoxColumn>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                                    EditText="Editar" CancelText="Cancelar" InsertText="Aceptar" HeaderText="" UpdateText="Actualizar">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                    
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn ConfirmText="¿Desea quitar este producto de la lista?"
                                                    ConfirmDialogType="RadWindow" ConfirmTitle="" ButtonType="ImageButton" CommandName="Delete"
                                                    Text="Borrar" HeaderText="" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                                    ConfirmDialogWidth="350px">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="Prd_AgrupadoSpo" HeaderText="agrupado" UniqueName="Prd_AgrupadoSpo"
                                                    Display="False">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <%--</asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <table width="99%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelSubtotal" runat="server" Text="Subtotal"></asp:Label>
                            </td>
                            <td width="125">
                                <telerik:RadNumericTextBox ID="RadNumericTextBoxSubTotal" runat="server" Enabled="false"
                                    Value="0" CssClass="AlignRight">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelIVA" runat="server" Text="I.V.A."></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBoxIVA" runat="server" Enabled="false"
                                    Value="0" CssClass="AlignRight">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LabelTotal" runat="server" Text="Total"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBoxTotal" runat="server" Enabled="false"
                                    Value="0" CssClass="AlignRight">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenHeight" runat="server" />
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Rts_TabSelecting(sender, args) {
                //debugger;
                var txtTipo = $find('<%= txtTipoId.ClientID %>');
                var txtCliente = $find('<%= txtClienteId.ClientID %>');
                var txtProveedor = $find('<%= txtProveedorId.ClientID %>');
                var txtRef = $find('<%= txtReferencia.ClientID %>');
                var txtRef2 = $find('<%= txtReferencia2.ClientID %>');
                var txtNotas = $find('<%= txtNotas.ClientID %>');

                var cmb = $find('<%= RadComboBoxTerritorio.ClientID %>');

                var HF_SoloVer = document.getElementById('<%= HF_SoloVer.ClientID %>');
                if (HF_SoloVer.value != '1') {
                    continuarAccion = _ValidarFechaEnPeriodo();
                    args.set_cancel(!continuarAccion);

                    if (txtTipo.get_value() == '' || (txtCliente.get_value() == '' && txtProveedor.get_value() == '')) {

                        radalert('Todos los campos son requeridos', 330, 150, '');
                        args.set_cancel(true);
                    }
                    else

                        if (txtRef != null && cmb != null) {
                            if (txtRef.get_value() == '') {
                                radalert('Todos los campos son requeridos', 330, 150, '');
                                args.set_cancel(true);
                            }
                        }
                        else
                            if (txtRef2 != null && cmb != null) {
                                if (txtRef2.get_value() == '') {
                                    radalert('Todos los campos son requeridos', 330, 150, '');
                                    args.set_cancel(true);
                                }
                            }
                }
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= dpFecha.ClientID %>');
                return txtFecha._dateInput;
            }
            function txtTipoId_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoMovimento.ClientID %>'));
            }

            function cmbTipoMovimento_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoId.ClientID %>'));
            }

            function txtClienteId_OnBlur(sender, args) {

            }

            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtClienteId.ClientID %>'));
            }

            function txtProveedorId_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbProveedor.ClientID %>'));
            }

            function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProveedorId.ClientID %>'));
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                // debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        break;
                    case 'delete':
                        continuarAccion = Confirma();

                        break;
                    case 'save':
                        continuarAccion = _ValidarFechaEnPeriodo();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else {
                    if (window.frameElement != null) {
                        if (window.frameElement.radWindow)
                            oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                    }
                    else
                        window.open("login.aspx");
                }
                return oWindow;
            }

            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                //debugger;
                                //GetRadWindow().Close();
                                CloseAndRebind();
                            });
            }

            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            function CloseWindow3() {
                var cerrarWindow = radalert('El campo de referencia se encuentra vacío', 330, 150, '');
                cerrarWindow.add_close(
                function () {
                    //debugger;                        
                    var txt = $find('<%= txtReferencia2.ClientID %>');
                    txt.focus();
                });
            }
            function CloseWindow2(msg) {
                var cerrarWindow = radalert(msg, 330, 150, '');
                cerrarWindow.add_close(
                    function () {
                        //debugger;                        
                        var txt = $find('<%= txtReferencia2.ClientID %>');
                        txt.focus();
                    });
            }
            function CloseWindow() {
                var cerrarWindow = radalert('El campo de referencia se encuentra vacío', 330, 150, '');
                cerrarWindow.add_close(
                function () {
                    //debugger;                        
                    var txt = $find('<%= txtReferencia.ClientID %>');
                    txt.focus();
                });
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //funcion oscar
            var txtProductoClientID;
            var cmbProductoClientID;

            function IdPrd_OnBlur(sender, eventArgs) {
                //debugger;
                OnBlur(sender, $find(cmbProductoClientID));
            }
            function cmbProductosLista_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), $find(txtProductoClientID));
            }

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

            function onCommand(sender, eventargs) {
                if (eventargs.get_commandName() == "PerformInsert" || eventargs.get_commandName() == "Update" || eventargs.get_commandName() == "Delete") {
                    var radGrid = $find('<%= rgEntradaSalida.ClientID %>');
                    var table = radGrid.get_masterTableView();
                    var column = table.getColumnByUniqueName("EditCommandColumn");
                    table.hideColumn(column.get_element().cellIndex);

                    column = table.getColumnByUniqueName("DeleteColumn");
                    table.hideColumn(column.get_element().cellIndex);
                }
            }


            function showcolum() {
                var radGrid = $find('<%= rgEntradaSalida.ClientID %>');
                var table = radGrid.get_masterTableView();
                var column = table.getColumnByUniqueName("EditCommandColumn");
                table.showColumn(column.get_element().cellIndex)

                column = table.getColumnByUniqueName("DeleteColumn");
                table.showColumn(column.get_element().cellIndex);
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
