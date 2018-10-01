<%@ Page Title="Solicitud de entradas y salidas" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapEntSalAutorizacion.aspx.cs" Inherits="SIANWEB.CapEntSalAutorizacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .cssOcultar
        {
            display: none;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbNaturaleza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>           
            <telerik:AjaxSetting AjaxControlID="cmbTipoMovimento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbProveedor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtOCId">   
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Generales" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="divtotales" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEntradaSalida">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEntradaSalida" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divtotales" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
            OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                  <telerik:RadToolBarButton CommandName="rechazar" Value="rechazar" ToolTip="Rechazar" CssClass="delete"
                    ImageUrl="Imagenes/blank.png"  />
                 <telerik:RadToolBarButton CommandName="autorizar" Value="autorizar" ToolTip="Autorizar" CssClass="aceptarToolbar"
                    ImageUrl="Imagenes/blank.png"  />
                 <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="pestaniaDetalles" />
         
            <%--    <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="Imagenes/blank.png" />--%>
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
                        SelectedIndex="0" OnClientTabSelecting="Rts_TabSelecting" CausesValidation="False">
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
                                    <div runat="server" id="Generales">
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
                                                    &#160;&#160;
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
                                                    &#160;&#160;
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
                                                    <telerik:RadDatePicker ID="dpFecha" runat="server" Width="90px" Enabled="false">
                                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                        </Calendar>
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput><DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario">
                                                        </DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="LabelHora" runat="server" Text="Hora"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtHora" runat="server" MaxLength="50" Width="70px" Enabled="False">                                                        
                                                    </telerik:RadTextBox>
                                                </td>
                                                  <td>
                                                  <asp:Button ID="BtnAutorizar" runat="server" Text="Autorizar" 
                                                    ToolTip="Autorizar" OnClick="BtnAutorizar_Click" Visible="true" />
                              </td>
                                                <td>
                                                    <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" 
                                                    ToolTip="Rechazar" OnClick="BtnRechazar_Click" Visible="true" />
                                                    </td>
                                              

                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Aplica a" Visible="true"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadComboBox ID="cmbAfecta" runat="server" Width="150px" AutoPostBack="True"
                                                        Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                        DataTextField="Descripcion" DataValueField="Id" OnSelectedIndexChanged="cmbAfecta_SelectedIndexChanged"
                                                        LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" CausesValidation="False" visible="true">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbAfecta"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &#160;&#160;
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trProveedor" visible="false">
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelProveedor" runat="server" Text="Proveedor"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtProveedorId" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtProveedorId_OnBlur" OnKeyPress="handleClickEvent" OnFocus="_ValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbProveedor" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        Width="362px" MaxHeight="200px" OnClientSelectedIndexChanged="cmbProveedor_ClientSelectedIndexChanged"
                                                        OnSelectedIndexChanged="cmbProveedor_SelectedIndexChanged" AutopostBack="True"
                                                        OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
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
                                                <td>
                                                    &#160;&nbsp;
                                                </td>
                                            </tr>

                                      <tr runat="server" id="trProveedorF" visible="false">
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblProveedorF" runat="server" Text="Proveedor"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtProveedorFId" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtProveedorFId_OnBlur" OnKeyPress="handleClickEvent" OnFocus="_ValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="CmbProveedorF" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        Width="362px" MaxHeight="200px" OnClientSelectedIndexChanged="CmbProveedorF_ClientSelectedIndexChanged"
                                                        OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbProveedorF"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &#160;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="120">
                                                    <asp:Label ID="LabelTipoMovimiento" runat="server" Text="Tipo de movimiento"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtTipoId" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="1" Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtTipoId_OnBlur" OnKeyPress="handleClickEvent" OnFocus="_ValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbTipoMovimento" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        OnSelectedIndexChanged="cmbTipoMovimento_SelectedIndexChanged" Width="362px"
                                                        MaxHeight="250px" OnClientSelectedIndexChanged="cmbTipoMovimento_ClientSelectedIndexChanged"
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
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbTipoMovimento"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="display: none">
                                                    <telerik:RadNumericTextBox ID="txtRequerido" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="0" Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trCliente" visible="false">
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelCliente" runat="server" Text="Cliente"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtClienteId" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnBlur="txtClienteId_OnBlur" OnKeyPress="handleClickEvent" OnFocus="_ValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="358px" ReadOnly="True"
                                                        Enabled="False">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtClienteNombre"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="" ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &#160;&nbsp;
                               
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
                                                    <telerik:RadTextBox ID="txtReferencia" runat="server" MaxLength="15" Width="50px"
                                                        ClientEvents-OnBlur="txtReferencia_Onblur">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" OnFocus="_ValidarFechaEnPeriodo" />
                                                    </telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                        ControlToValidate="txtReferencia" Display="Dynamic" ErrorMessage="*Requerido"
                                                        ForeColor="Red" ValidationGroup="nn"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LabelTerritorio" runat="server" Text="Territorio" Visible="False"></asp:Label>

                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtTerritorioNombre" runat="server" ReadOnly="True" Width="301px"
                                                        Visible="False" Enabled="False">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td style="display: none">
                                                    <telerik:RadNumericTextBox ID="txtterritorio" runat="server" Enabled="false" MaxLength="9"
                                                        MinValue="0" Width="50px">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>

                                            <tr id="trOCID" runat="server" visible="false">
                                            <td></td>
                                                <td width="120"><asp:Label ID="LabelOC" runat="server" Text="Folio OC"  Title="Folio de Orden de Compra"></asp:Label>
                                                </td>
                                                <td style="margin-left: 40px">
                                                    <telerik:RadNumericTextBox ID="txtOCId" runat="server" MaxLength="9" MinValue="1"
                                                        Width="50px" Title="Folio de Orden de Compra"  AutoPostBack="True" OnTextChanged="txtOC_TextChanged">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    </telerik:RadNumericTextBox>   
                                                </td>
                                                <td></td>
                                            </tr>


                                            <tr id="trFechaOC" runat="server" visible="false">
                                            <td></td>
                                                <td width="120">
                                                    <asp:Label ID="LabelFechaOC" runat="server" Text="Fecha"  Title="Fecha de Orden de Compra"></asp:Label>
                                                </td>
                                                <td style="margin-left: 40px">
                                                    <telerik:RadTextBox ID="txtFechaOC" runat="server" ReadOnly="True" Width="90px"
                                                         Title="Fecha de Orden de Compra" CssClass="AlignCenter">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td></td>
                                            </tr>


                                            <tr id="trTotalOC" runat="server" visible="false">
                                            <td></td>
                                                <td width="120">
                                                    <asp:Label ID="LabelTotalOC" runat="server" Text="Total"  Title="Total de Orden de Compra"></asp:Label>
                                                </td>
                                                <td style="margin-left: 40px">
                                                    <telerik:RadNumericTextBox ID="txtTotalOC" runat="server" ReadOnly="True" Width="90px"
                                                         Title="Total de Orden de Compra" Value="0" CssClass="AlignRight">
                                                    </telerik:RadNumericTextBox>                                                                                                                                                


                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr id="trFechaReferencia" runat="server" visible="false">
                                               
                                                 <td>
                                                </td>
                                                <td width="120">
                                                    <asp:Label ID="Label2" runat="server" Text="Fecha referencia"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadDatePicker ID="dpFechaReferencia" runat="server" Width="90px">
                                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy" />
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                        </Calendar>
                                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                        </DateInput><DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario">
                                                        </DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFechaReferencia" runat="server"
                                                        ControlToValidate="dpFechaReferencia" Display="Dynamic" ErrorMessage="*Requerido"
                                                        ForeColor="Red" ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                </td>
                                                <td></td>
                                            </tr>


                                              <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Solicitar a:"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadComboBox ID="CmbId_UEnviar" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        Width="362px" MaxHeight="200px"
                                                        OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="CmbId_UEnviar"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &#160;&nbsp;
                                                </td>
                                            </tr>
                                                   <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Text="CC:" Visible= "false"></asp:Label>
                                                </td>
                                                <td colspan="2" ">
                                                    <telerik:RadComboBox ID="CmbId_UCC" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                        HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                        Width="362px" MaxHeight="200px"
                                                        OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo" Visible= "false">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 50px; text-align: center">
                                                                        <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="width: 200px; text-align: left">
                                                                        <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                       
                                                </td>
                                                <td>
                                                    &#160;&nbsp;
                                                </td>
                                            </tr>

                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                                <td width="120">
                                                    <asp:Label ID="LabelNotas" runat="server" ErrorMessage="*Requerido" Text="Notas"></asp:Label>
                                                </td>
                                                <td rowspan="6">
                                                    <telerik:RadTextBox ID="txtNotas" runat="server" CausesValidation="True" Height="90px"
                                                        MaxLength="256" TextMode="MultiLine" Width="440px">
                                                        <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td> <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNotas"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue=""
                                                        ValidationGroup="pestaniaDetalles"></asp:RequiredFieldValidator></td>
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
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                            </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="270px">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="270px" OnClientResized="onResize"
                                    BorderStyle="None">
                                    <telerik:RadGrid ID="rgEntradaSalida" runat="server" OnNeedDataSource="rgEntradaSalida_NeedDataSource"
                                        AutoGenerateColumns="False" GridLines="None" OnItemDataBound="rgEntradaSalida_ItemDataBound"
                                        OnItemCommand="rgEntradaSalida_ItemCommand" OnInsertCommand="rgEntradaSalida_InsertCommand"
                                        OnUpdateCommand="rgEntradaSalida_UpdateCommand" OnDeleteCommand="rgEntradaSalida_DeleteCommand" >
                                        <ClientSettings>
                                            <ClientEvents />
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros."
                                            EditMode="InPlace" DataKeyNames="Id_Ter">
                                            <CommandItemSettings AddNewRecordText="Agregar" ExportToPdfText="Export to Pdf" RefreshText="Actualizar"
                                                ShowRefreshButton="false" />
                                            <Columns>
                                                <telerik:GridTemplateColumn DataField="Id_EsDetStr" HeaderText="Id_EsDetStr" UniqueName="Id_EsDetStr"
                                                    Display="false">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblDet_Edit" runat="server" Text='<%# Eval("Id_EsDetStr") %>' /></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDet_Item" runat="server" Text='<%# Eval("Id_EsDetStr") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Id_Ter" HeaderText="Núm." UniqueName="Id_Ter"
                                                    Display="false">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" Width="50px"
                                                            MinValue="1" Text='<%# Eval("Id_Ter") %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtTerritorioPartida_OnLoad" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="TerLabel" runat="server" Text='<%# Eval("Id_Ter") %>'></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="territorio" HeaderText="Territorio" UniqueName="territorio">
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox ID="cmbTerritorio" runat="server" LoadingMessage="Cargando..."
                                                            OnClientBlur="Combo_ClientBlur" OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged"
                                                            Width="100%" HighlightTemplatedItems="true" Filter="Contains" MarkFirstMatch="true">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                                            <asp:Label ID="LabelID2" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:Label ID="LabelDESC2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Ter_Nombre") %>'></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="350px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Id_Prd" HeaderText="Núm." UniqueName="Id_Prd">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtId_Prd" runat="server" MaxLength="9" Width="50px"
                                                            MinValue="1" Text='<%# Eval("Id_Prd") %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtProductoPartida_OnLoad" OnBlur="txtProducto_Onblur" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="ProdLabel" runat="server" Text='<%# Eval("Id_Prd") %>'></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Prd_Descripcion" HeaderText="Producto" UniqueName="Descripcion">
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="DescripcionTextBox" runat="server" Enabled="False" Width="100%"
                                                            Text='<%# Eval("Prd_Descripcion") %>'>
                                                            <ClientEvents OnLoad="txtDescripcionPartida_OnLoad" />
                                                        </telerik:RadTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Prd_Descripcion") %>'></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Presentacion" HeaderText="Presen." UniqueName="Presen">
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="PresenTextBox" runat="server" Enabled="False" Text='<%# Bind("Presentacion") %>'
                                                            Width="50px">
                                                            <ClientEvents OnLoad="txtPresentacionPartida_OnLoad" />
                                                        </telerik:RadTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="PresenLabel" runat="server" Text='<%# Eval("Presentacion") %>'></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Es_Cantidad" HeaderText="Cantid." UniqueName="Cantidad">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxCantidad" runat="server" MinValue="1"
                                                            MaxLength="9" Width="50px" Text='<%# Eval("ESol_Cantidad") %>'>
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" OnLoad="txtCantidadPartida_OnLoad" OnBlur="txtCantidad_Onblur" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="CantidadLabel" runat="server" Text='<%# Eval("ESol_Cantidad") %>'></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Es_Costo" HeaderText="Costo" UniqueName="Costo">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxCosto" runat="server" MinValue="0"
                                                            MaxLength="9" Width="50px" Text='<%# Eval("ESol_EsCosto") %>'>
                                                            <ClientEvents OnLoad="txtPrecioPartida_OnLoad" OnBlur="txtPrecio_Onblur" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="CostoLabel" runat="server" Text='<%# Convert.ToDouble(Eval("ESol_EsCosto")).ToString("N") %>' /></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Importe" HeaderText="Importe" UniqueName="Importe">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txtImporte" runat="server" MinValue="0" MaxLength="9"
                                                            Width="50px" Text='<%# Bind("Importe") %>' BackColor="Transparent" ReadOnly="true"
                                                            CssClass="AlignRight">
                                                            <ClientEvents OnLoad="txtImportePartida_OnLoad" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblImporte_Item" runat="server" Text='<%# Convert.ToDouble(Eval("Importe")).ToString("N") %>' /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridCheckBoxColumn DataField="Afecta" DataType="System.Boolean" HeaderText="Afecta orden de compra"
                                                    SortExpression="Afecta" UniqueName="Afecta">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                </telerik:GridCheckBoxColumn>
                                                <telerik:GridCheckBoxColumn DataField="Es_BuenEstado" DataType="System.Boolean" HeaderText="Buen estado"
                                                    UniqueName="buenEstado">
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
                                                <telerik:GridTemplateColumn DataField="Prd_AgrupadoSpo" HeaderText="Prd_AgrupadoSpo"
                                                    UniqueName="Prd_AgrupadoSpo" Display="false">
                                                    <EditItemTemplate>
                                                        <telerik:RadNumericTextBox ID="AgrupadorTextBox" runat="server" Enabled="False" Width="100%"
                                                            Text='<%# Eval("Prd_AgrupadoSpo") %>'>
                                                            <ClientEvents OnLoad="txtAgrupadorPartida_OnLoad" />
                                                        </telerik:RadNumericTextBox></EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="AgrupadorLabel" runat="server" Text='<%# Eval("Prd_AgrupadoSpo") %>'></asp:Label></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings>
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <table width="99%" runat="server" id="divtotales">
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
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                                <asp:HiddenField ID="HiddenHeight" runat="server" />
                                 <asp:HiddenField ID="HFTipoOp" runat="server" />
                                <asp:HiddenField ID="HiddenCteCuentaNacional" runat="server" Value="-1" />
                                <asp:HiddenField ID="HiddenNumCuentaContNacional" runat="server" Value="0" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function initDialog() {

            }

            function Rts_TabSelecting(sender, eventArgs) {
                Page_ClientValidate("pestaniaDetalles")
                if (!Page_IsValid) {
                    eventArgs.set_cancel(true);
                    __doPostBack();
                }

                return false;
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
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), _ter);
            }
            function txtClienteId_OnBlur(sender, args) {
                var cte = $find("<%= txtClienteId.ClientID %>");
                var cte_nom = $find("<%= txtClienteNombre.ClientID %>");

                if (cte.get_value() == '')
                { return; }

                var nombre = ObtenerCliente(cte.get_value());

                var mySplitResult = nombre.split("@@");
                var Resultado = mySplitResult[0];

                if (Resultado == -1) {
                    cte_nom.set_value("");
                    cte.set_value("");
                    AlertaFocus_Mostrado(mySplitResult[1], cte._clientID);
                }
                else {
                    cte_nom.set_value(nombre);
                    ($find("<%= txtReferencia.ClientID %>")).focus();
                }
            }

            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                //debugger;
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtClienteId.ClientID %>'));
            }

            function txtProveedorId_OnBlur(sender, args) {
                var combo = $find('<%= cmbProveedor.ClientID %>');
                //debugger;
                OnBlur(sender, combo);
            }

            function txtProveedorFId_OnBlur(sender, args) {
                var combo = $find('<%= CmbProveedorF.ClientID %>');
                //debugger;
                OnBlur(sender, combo);
            }

            function cmbProveedor_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProveedorId.ClientID %>'));

            }
            function CmbProveedorF_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProveedorFId.ClientID %>'));

            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

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
                        button.set_enabled(false);
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
                                GetRadWindow().Close();
                            });
            }

            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
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

            function ObtenerCliente(cte) {
                var urlArchivo = 'ObtenerNombreCliente.aspx';
                parametros = "cte=" + cte;
                return obtenerrequest(urlArchivo, parametros);
            }

            var _prd;
            var _ter;
            var _prd_descripcion;
            var _prd_presentacion;
            var _prd_precio;
            var _prd_cantidad;
            var _prd_agrupador;
            var _importe;
            var _revisa_precio = true;
            var ultimo_precio;
            var ultima_cantidad;
            var ultimo_producto;


            function txtAgrupadorPartida_OnLoad(sender, args) {
                _prd_agrupador = sender;
            }
            function txtTerritorioPartida_OnLoad(sender, args) {
                _ter = sender;
            }
            function txtProductoPartida_OnLoad(sender, args) {
                _prd = sender;
                ultimo_producto = sender.get_value();
            }
            function txtDescripcionPartida_OnLoad(sender, args) {
                _prd_descripcion = sender;
            }
            function txtPresentacionPartida_OnLoad(sender, args) {
                _prd_presentacion = sender;
            }
            function txtPrecioPartida_OnLoad(sender, args) {
                _prd_precio = sender;
                ultimo_precio = sender.get_value();
            }
            function txtCantidadPartida_OnLoad(sender, args) {
                _prd_cantidad = sender;
                ultima_cantidad = sender.get_value();
            }
            function txtImportePartida_OnLoad(sender, args) {
                _importe = sender;
            }
            function txtPrecio_Onblur() {

                if (!_revisa_precio) {
                    _revisa_precio = true;
                }

                if (_prd.get_value() == '') {
                    return;
                }

                if (_prd_precio.get_value() == '') {
                    _prd_precio.set_value('0');
                }

                if (ultimo_precio == _prd_precio.get_value() && _prd_precio.get_value() != '0') {
                    return;
                }
                else {
                    ultimo_precio = _prd_precio.get_value();
                }

                ObtenerProductoPrecio(_prd, _prd_precio);
                CalcularImporte(_importe, _prd_cantidad, _prd_precio);

            }

            function ObtenerProductoPrecio(_prd, _pre) {

                var urlArchivo = 'ObtenerProducto_ESol.aspx';
                parametros = "prd=" + _prd.get_value();
                parametros = parametros + "&pre=" + _pre.get_value();
                parametros = parametros + "&acc=" + "cos";

                var a = obtenerrequest(urlArchivo, parametros);

                var mySplitResult = a.split("@@");
                var Resultado = mySplitResult[0];

                if (Resultado == -1) {
                    _pre.set_value('');
                    ultimo_precio = '';
                    AlertaFocus_Mostrado(mySplitResult[1], _pre._clientID);

                }

            }
            function CalcularImporte(lblImporte, txtCantidad, txtPrecio) {
                lblImporte.set_value(txtCantidad.get_value() * txtPrecio.get_value());
            }
            function txtCantidad_Onblur() {

                if (_prd.get_value() == '' || _prd_cantidad.get_value() == '') {
                    return;
                }
                if (ultima_cantidad == _prd_cantidad.get_value()) {
                    return;
                }
                else {
                    ultima_cantidad = _prd_cantidad.get_value();
                }
                var txtReferencia = $find("<%= txtReferencia.ClientID %>");
                var txtTipoId = $find("<%= txtTipoId.ClientID %>");
                var txtClienteId = $find("<%= txtClienteId.ClientID %>");
                var txtNaturaleza = $find("<%= cmbNaturaleza.ClientID %>");
                var Cve_pagina = document.getElementById("<%= HF_ClvPag.ClientID %>").value;
                var txtfolio = $find("<%= txtFolio.ClientID %>");
                var txtfoliooc = $find("<%= txtOCId.ClientID %>");
                ObtenerProductoCantidad(txtReferencia, txtTipoId, _prd, _ter, txtClienteId, Cve_pagina, txtNaturaleza, _prd_cantidad, txtfolio, txtfoliooc);
                CalcularImporte(_importe, _prd_cantidad, _prd_precio);
            }
            function ObtenerProductoCantidad(_doc, _tip, _prd, _ter, _cte, _clv, _nat, _can, _es, _foliooc) {

                var _gpo;
                switch (_tip.get_value()) {
                    case 2:
                    case 4:
                        _gpo = 1;
                        break;
                    case 6:
                    case 15:
                    case 16:
                        _gpo = 2;
                        break;
                    case 7:
                    case 11:
                    case 12:
                    case 13:
                        _gpo = 3;
                        break;
                    case 14:
                        _gpo = 4;
                        break;
                    default:
                        _gpo = 0;
                        break;
                }


                if (_can.get_value() == '') {
                    _can.set_value('0');
                }


                if (_tip.get_value() == 59) {
                    _es = null

                }


                var var_foliooc = _foliooc == null ? -1 : _foliooc.get_value();
                if (var_foliooc == null) { var_foliooc = ''; }

                var var_doc = _doc == null ? -1 : _doc.get_value();
                var var_ter = _ter == null ? -1 : _ter.get_value();
                var var_prd = _prd == null ? -1 : _prd.get_value();
                var var_nat = _nat == null ? -1 : _nat.get_value();
                var var_tip = _tip == null ? -1 : _tip.get_value();
                var var_can = _can == null ? -1 : _can.get_value();
                var var_cte = _cte == null ? -1 : _cte.get_value();
                var var_es = _es == null ? -1 : _es.get_value();

                if (_gpo == null) { _gpo = -1; }
                if (var_doc == null || var_doc == '') { var_doc = -1; }
                if (var_es == null || var_es == '') { var_es = -1; }
                if (var_ter == null || var_ter == '') { var_ter = -1; }
                if (var_prd == null || var_prd == '') { var_prd = -1; }
                if (var_nat == null || var_nat == '') { var_nat = -1; }
                if (var_tip == null || var_tip == '') { var_tip = -1; }
                if (var_can == null || var_can == '') { var_can = -1; }
                if (var_cte == null || var_cte == '') { var_cte = -1; }


                var urlArchivo = 'ObtenerProducto_ESol.aspx';
                parametros = "gpo=" + _gpo;
                parametros = parametros + "&ref=" + var_doc;
                parametros = parametros + "&ter=" + var_ter;
                parametros = parametros + "&prd=" + var_prd;

                parametros = parametros + "&nat=" + var_nat;
                parametros = parametros + "&mov=" + var_tip;
                parametros = parametros + "&cte=" + var_cte;
                parametros = parametros + "&can=" + var_can;
                parametros = parametros + "&clv=" + _clv;
                parametros = parametros + "&es=" + var_es;
                parametros = parametros + "&acc=" + "can";
                if (var_foliooc != '') {
                    parametros = parametros + "&Id_Ord=" + var_foliooc;
                    parametros = parametros + "&validatransitoordencompra=1";
                } else {
                    parametros = parametros + "&Id_Ord=0";
                    parametros = parametros + "&validatransitoordencompra=0";
                }
                var a = obtenerrequest(urlArchivo, parametros);

                var mySplitResult = a.split("@@");
                var Resultado = mySplitResult[0];

                if (Resultado == -1) {
                    _can.set_value('');
                    _revisa_precio = false;
                    ultima_cantidad = '';
                    AlertaFocus_Mostrado(mySplitResult[1], _can._clientID);
                }
                else if (Resultado == 1) {
                    _revisa_precio = true;
                    _prd_precio.focus();
                }
            }
            function txtProducto_Onblur() {
                if (_prd.get_value() == '') {
                    return;
                }

                if (ultimo_producto == _prd.get_value()) {
                    return;
                }
                else {
                    ultimo_producto = _prd.get_value();
                }

                var txtReferencia = $find("<%= txtReferencia.ClientID %>");
                var txtTipoId = $find("<%= txtTipoId.ClientID %>");
                var Cve_pagina = document.getElementById("<%= HF_ClvPag.ClientID %>").value;
                ObtenerProducto(txtReferencia, txtTipoId, _prd, _ter, Cve_pagina);
            }
            function ObtenerProducto(_doc, _tip, _prd, _ter, _clv) {

                _prd_precio.set_value('');
                _prd_presentacion.set_value('');
                _prd_descripcion.set_value('');
                _prd_cantidad.set_value('');
                _prd_agrupador.set_value('');

                var _gpo;
                switch (_tip.get_value()) {
                    case 2:
                    case 4:
                        _gpo = 1;
                        break;
                    case 6:
                    case 15:
                    case 16:
                        _gpo = 2;
                        break;
                    case 7:
                    case 11:
                    case 12:
                    case 13:
                        _gpo = 3;
                        break;
                    case 14:
                        _gpo = 4;
                        break;
                    default:
                        _gpo = 0;
                        break;
                }

                var urlArchivo = 'ObtenerProducto_ESol.aspx';
                parametros = "gpo=" + _gpo;
                parametros = parametros + "&ref=" + _doc.get_value();
                parametros = parametros + "&ter=" + _ter.get_value();
                parametros = parametros + "&prd=" + _prd.get_value();
                parametros = parametros + "&acc=" + "val";
                parametros = parametros + "&clv=" + _clv;
                var a = obtenerrequest(urlArchivo, parametros);


                var mySplitResult = a.split("@@");
                var Resultado = mySplitResult[0];


                if (Resultado == -2) {
                    _prd_precio.set_value(mySplitResult[1]);
                    _prd_presentacion.set_value(mySplitResult[2]);
                    _prd_descripcion.set_value(mySplitResult[3]);
                    _prd_agrupador.set_value(mySplitResult[4]);
                    AlertaFocus_Mostrado(mySplitResult[5], _prd_cantidad._clientID);
                }

                if (Resultado == 1) {
                    _prd_precio.set_value(mySplitResult[1]);
                    _prd_presentacion.set_value(mySplitResult[2]);
                    _prd_descripcion.set_value(mySplitResult[3]);
                    _prd_agrupador.set_value(mySplitResult[4]);
                    _prd_cantidad.focus();
                }

                if (Resultado == -1) {
                    _prd.set_value('');
                    ultimo_producto = '';
                    AlertaFocus_Mostrado(mySplitResult[1], _prd._clientID);
                }

            }

            function txtReferencia_Onblur() {
                var txtReferencia = $find("<%= txtReferencia.ClientID %>");
                var txtRequerido = $find("<%= txtRequerido.ClientID %>");
                var txtTerritorio = $find("<%= txtterritorio.ClientID %>");
                var txtTerritorioNombre = $find('<%= txtTerritorioNombre.ClientID %>');
                var txtCliente = $find("<%= txtClienteId.ClientID %>");
                ObtenerReferencia(txtReferencia, txtRequerido, txtTerritorio, txtTerritorioNombre, txtCliente);
            }

            function ObtenerReferencia(doc, req, ter, cmbTer, cte) {
                var id_Ter = '';
                var Ter_Nom = ''
                var Tm = $find("<%= txtTipoId.ClientID %>");

                if (req.get_value() != 0 && doc.get_value() != '') {

                    var urlArchivo = 'ObtenerReferencia.aspx';
                    parametros = "doc=" + doc.get_value();
                    parametros = parametros + "&cte=" + cte.get_value()
                    parametros = parametros + "&tm=" + Tm.get_value();
                    var a = obtenerrequest(urlArchivo, parametros);

                    switch (a) {
                        case '-1':
                            doc.set_value('');
                            AlertaFocus_Mostrado("El número de referencia no existe", doc._clientID);
                            break;
                        case '-2':
                            doc.set_value('');
                            AlertaFocus_Mostrado("El número de referencia está en estatus no valido", doc._clientID);
                            break;
                        case '-3':
                            doc.set_value('');
                            AlertaFocus_Mostrado("El cliente no corresponde al del documento que se hace referencia", doc._clientID);
                            break;
                        case '-4':
                            doc.set_value('');
                            AlertaFocus_Mostrado("Remisión con saldo insuficiente", doc._clientID);
                            break;
                        case '-5':
                            doc.set_value('');
                            AlertaFocus_Mostrado("El tipo de movimiento de la Remisión no coincide con el tipo de la Entrada/Salida actual", doc._clientID);
                            break;
                        default:
                            var mySplitResult = a.split("@");
                            id_Ter = mySplitResult[0];
                            Ter_Nom = mySplitResult[1];
                            $find("<%= txtClienteId.ClientID %>").set_value(mySplitResult[2]);
                            $find("<%= txtClienteNombre.ClientID %>").set_value(mySplitResult[3]);
                            document.getElementById("<%= HiddenCteCuentaNacional.ClientID %>").value = (mySplitResult[4] == null) ? -1 : mySplitResult[4];
                            document.getElementById("<%= HiddenNumCuentaContNacional.ClientID %>").value = (mySplitResult[5] == null) ? -1 : mySplitResult[5];

                            break;
                    }
                }

                ter.set_value(id_Ter);
                if (cmbTer != null) {
                    cmbTer.set_value(Ter_Nom);
                }
            }

            function IniciarPaginasAuxiliares() {
                parametros = "ini=1";
                var urlArchivo = 'ObtenerNombreCliente.aspx';
                obtenerrequest(urlArchivo, parametros);

                urlArchivo = 'ObtenerProducto_ESol.aspx';
                obtenerrequest(urlArchivo, parametros);

                urlArchivo = 'ObtenerReferencia.aspx';
                obtenerrequest(urlArchivo, parametros);
            }

            var mostrando_mensaje = false;
            function AlertaFocus_Mostrado(mensaje, control) {
                if (mostrando_mensaje) {
                    return;
                }
                var oWnd = radalert(mensaje, 340, 150);
                mostrando_mensaje = true;

                oWnd.add_close(function () {
                    mostrando_mensaje = false;
                    var target = $find(control);
                    if (target != null && (target.enabled || target._enabled)) {
                        target.focus();
                    }
                });
            }



            function AbrirVentana_RechazarSolicitud(Id_Emp, Id_Cd, Id_Fac) {
                //debugger;
                var oWnd = radopen("ventana_RechazaSolicitud.aspx?Id_Fac=" + Id_Fac
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    , "AbrirVentana_RechazarSolicitud");
                
                //oWnd.set_showOnTopWhenMaximized(false);
                oWnd.maximize();
                oWnd.center();

            }


        </script>


    </telerik:RadCodeBlock>
</asp:Content>
