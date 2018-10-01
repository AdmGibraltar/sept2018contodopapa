<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="CapPagos.aspx.cs" Inherits="SIANWEB.CapPagos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RgGral">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RgGral" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtImporte" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RgDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RgDet" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
        OnButtonClick="rtb1_ButtonClick">
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
                    &nbsp;
                </td>
                <td width="150px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="T" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>
                             <telerik:RadTab runat="server" Text="C&lt;u&gt;a&lt;/u&gt;rga Archivo" AccessKey="A" PageViewID="RadPageViewCargaArchivo"> 
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <%--height="370px" width="820px">--%>
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" Height="370px">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="370px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100.5%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="370px" OnClientResized="onResize"
                                    Scrolling="None">
                                    <div runat="server" id="Generales">
                                        <table>
                                            <tr>
                                                <td width="120">
                                                    &nbsp;
                                                </td>
                                                <td width="30">
                                                    &nbsp;
                                                </td>
                                                <td width="100">
                                                    &nbsp;
                                                </td>
                                                <td width="10">
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
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80">
                                                    Tipo
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadComboBox ID="cmbTipo" runat="server" Width="130px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Folio" />
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtClave" runat="server" Width="70px" Enabled="false"
                                                        MaxLength="9" MinValue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Fecha
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="rdFechaPago" runat="server" Width="100px" Culture="es-MX">
                                                        <Calendar ID="Calendar1" runat="server">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy">
                                                            </FastNavigationSettings>
                                                        </Calendar>
                                                        <DateInput runat="server">
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td width="120">
                                                    <asp:Label ID="Label4" runat="server" Text="Tipo de movimiento"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMovimiento" runat="server" Width="50px" MinValue="1"
                                                        MaxLength="9">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" OnBlur="txt_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbMovimiento" runat="server" Width="350px" Filter="Contains"
                                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                        DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                        LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmb_ClientSelectedIndexChanged"
                                                        OnClientFocus="_PreValidarFechaEnPeriodo">
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
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMovimiento"
                                                        ErrorMessage="*Requerido" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <%--<asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" Width="790px">--%>
                                                    <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                                        ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                                        <telerik:RadPane ID="RadPane4" runat="server" Height="270px" BorderStyle="None" OnClientResized="onResize">
                                                            <telerik:RadGrid ID="RgGral" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                                OnItemCommand="RgGral_ItemCommand" OnNeedDataSource="RgGral_NeedDataSource" OnPageIndexChanged="RgGral_PageIndexChanged"
                                                                PageSize="6" OnItemCreated="RgGral_ItemCreated">
                                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="Top"
                                                                    EditMode="InPlace">
                                                                    <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn DataField="rgGralId" HeaderText="Id" UniqueName="rgGralId"
                                                                            Display="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGralId1" runat="server" Text='<%# Bind("rgGralId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="lblGralId2" runat="server" Text='<%# Bind("rgGralId") %>'></asp:Label>
                                                                            </EditItemTemplate>
                                                                            <HeaderStyle Width="10px" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_ficha" HeaderText="Ficha" UniqueName="Pag_ficha">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFicha" runat="server" Text='<%# Bind("Pag_ficha") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadNumericTextBox ID="rgtxtFicha" runat="server" Width="50px" Text='<%# Bind("Pag_ficha") %>'
                                                                                    MaxLength="9" ReadOnly="true" MinValue="0">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnLoad="Ficha_Load" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </EditItemTemplate>
                                                                            <HeaderStyle Width="70px" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_Fecha" HeaderText="Fecha" UniqueName="Pag_Fecha">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Pag_Fecha","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadDatePicker ID="rdFecha" runat="server" Width="100px" DbSelectedDate='<%# Bind("Pag_Fecha") %>'>
                                                                                    <Calendar ID="Calendar1" runat="server">
                                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                            TodayButtonCaption="Hoy">
                                                                                        </FastNavigationSettings>
                                                                                    </Calendar>
                                                                                    <DateInput runat="server">
                                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                    </DateInput>
                                                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                                </telerik:RadDatePicker>
                                                                            </EditItemTemplate>
                                                                            <HeaderStyle Width="120px" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_Banco" HeaderText="Banco" UniqueName="Pag_Banco">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBanco" runat="server" Text='<%# Bind("Pag_BancoStr") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadNumericTextBox ID="rgtxtIdBanco" runat="server" Width="50px" MaxLength="9"
                                                                                    DbValue='<%# Bind("Pag_Banco") %>' MinValue="1" OnTextChanged="txtBanco_TextChanged"
                                                                                    AutoPostBack="true">
                                                                                    <ClientEvents OnLoad="IdBanco_Load" OnBlur="txtBanco_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <telerik:RadComboBox ID="cmbBancos" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                                                    LoadingMessage="Cargando..." OnDataBinding="cmbBancos_DataBinding" Text='<%# Bind("Pag_BancoStr") %>'
                                                                                    OnClientLoad="Banco_Load" OnClientSelectedIndexChanged="cmbBanco_ClientSelectedIndexChanged"
                                                                                    MaxHeight="250px">
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
                                                                            <HeaderStyle Width="280px" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Cuenta" DataField="Ban_Cuenta" UniqueName="Ban_Cuenta">
                                                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBanCuenta" runat="server" Text='<%# Eval("Ban_Cuenta").ToString() %>' />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadTextBox Width="100px" ID="txtBanCuenta" runat="server" ReadOnly="true"
                                                                                    Text='<%# Eval("Ban_Cuenta").ToString() %>'>
                                                                                </telerik:RadTextBox>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_Importe" HeaderText="Importe" UniqueName="Pag_Importe">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("Pag_Importe", "{0:N2}" ) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadNumericTextBox ID="rgtxtImporte" runat="server" Width="100px" Text='<%# Bind("Pag_Importe") %>'
                                                                                    MaxLength="9" MinValue="0">
                                                                                    <NumberFormat DecimalDigits="2" AllowRounding="true" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <HeaderStyle Width="120px" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                            InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                                            <HeaderStyle Width="70px" />
                                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </telerik:GridEditCommandColumn>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                                            UniqueName="DeleteColumn">
                                                                            <HeaderStyle Width="50px" />
                                                                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </telerik:GridButtonColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                                <%--<ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="215px">
                                                                    </Scrolling>
                                                                </ClientSettings>--%>
                                                            </telerik:RadGrid>
                                                            <%--</asp:Panel>--%>
                                                        </telerik:RadPane>
                                                    </telerik:RadSplitter>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="370px">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="370px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="370px" OnClientResized="onResize"
                                    BorderStyle="None" Scrolling="None">
                                    <div runat="server" id="Detalles">
                                        <%-- <table>
                                            <tr>
                                                <td>--%>
                                        <%--<asp:Panel ID="Panel1" runat="server" Width="810px"  Height="340px" ScrollBars="Horizontal">--%>
                                        <telerik:RadSplitter ID="RadSplitter3" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                            ResizeWithBrowserWindow="true" BorderSize="0" Width="98%">
                                            <telerik:RadPane ID="RadPane3" runat="server" Width="99%" Height="250px" OnClientResized="onResize"
                                                BorderStyle="None">
                                                <telerik:RadGrid ID="RgDet" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                    GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                    OnItemCommand="RgDet_ItemCommand" OnNeedDataSource="RgDet_NeedDataSource" OnPageIndexChanged="RgDet_PageIndexChanged"
                                                    PageSize="7" OnItemCreated="RgDet_ItemCreated" OnItemDataBound="RgDet_ItemDataBound">
                                                    <MasterTableView CommandItemDisplay="Top" EditMode="InPlace">
                                                        <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Id" UniqueName="rgDetlId2" Display="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDetlId1" runat="server" Text='<%# Bind("RgDId") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblDetlId2" runat="server" Text='<%# Bind("RgDId") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="20px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Pag_Movimiento" HeaderText="Movimiento" UniqueName="Pag_Movimiento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMov" runat="server" Text='<%# Bind("Pag_MovimientoStr") %>'></asp:Label>
                                                                    <asp:Label ID="lblMov1" runat="server" Text='<%# Bind("Pag_Movimiento") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadComboBox ID="rgcmbMov" runat="server" Width="100px" SelectedValue='<%# Bind("Pag_Movimiento") %>'
                                                                        autopostback ="true" onSelectedIndexChanged="cmbDocumento_SelectedIndexChanged">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Factura" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Nota de cargo" Value="2" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="120px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Serie" HeaderText="Serie" UniqueName="Serie">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerie" runat="server" Text='<%# Bind("Serie") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="rgSerie" runat="server" Text='<%# Bind("Serie") %>' Width="40px"
                                                                        MaxLength="10" OnTextChanged="rgTxtSerie_TextChanged" AutoPostBack="true">
                                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Pag_Referencia" HeaderText="Ref." UniqueName="Pag_Referencia">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReferencia" runat="server" Text='<%# Bind("Doc_Referencia") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="rgReferencia" runat="server" Width="40px" OnTextChanged="rgTxtReferencia_TextChanged"
                                                                        AutoPostBack="true" Text='<%# Bind("Doc_Referencia") %>' MaxLength="40">
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Fac_FolioFiscal" HeaderText="Folio Fiscal" UniqueName="Fac_FolioFiscal">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblFolioFiscal" runat="server" Text='<%# Bind("Fac_FolioFiscal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="rgFolioFiscal" runat="server" Width="280px" 
                                                                        AutoPostBack="true" Text='<%# Bind("Fac_FolioFiscal") %>' MaxLength="40">
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="260px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Cdi" HeaderText="Cdi" UniqueName="Cdi">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCdi" runat="server" Text='<%# Bind("Cdi") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="rgCdi" runat="server" Text='<%# Bind("Cdi") %>' Width="40px"
                                                                        MinValue="0" MaxLength="9" ReadOnly="true">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Id_Terr" HeaderText="Terr." UniqueName="v">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTerr" runat="server" Text='<%# Bind("Id_Terr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="rgTerr" runat="server" Text='<%# Bind("Id_Terr") %>'
                                                                        Width="40px" ReadOnly="true" MinValue="1">
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Pag_Fecha" HeaderText="Fecha" UniqueName="Pag_Fecha">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Doc_Fecha","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadDatePicker ID="rdFecha" runat="server" DbSelectedDate='<%# Bind("Doc_Fecha") %>'
                                                                        Width="90px" MinDate="01/01/0001">
                                                                        <Calendar ID="Calendar1" runat="server">
                                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                TodayButtonCaption="Hoy">
                                                                            </FastNavigationSettings>
                                                                        </Calendar>
                                                                        <DateInput runat="server" ReadOnly="true">
                                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        </DateInput>
                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" Enabled="false" />
                                                                    </telerik:RadDatePicker>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="110px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCte" runat="server" Text='<%# Bind("Id_Cte") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="rgCte" runat="server" Text='<%# Bind("Id_Cte") %>'
                                                                        OnTextChanged="rgTxtCliente_TextChanged" AutoPostBack="true" Width="40px" ReadOnly="true"
                                                                        MinValue="1" MaxLength="9">
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Cte_Nombre" HeaderText="Cliente" UniqueName="Cte_Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCteNombre" runat="server" Text='<%# Bind("Cte_Nombre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="rgCteNombre" runat="server" Text='<%# Bind("Cte_Nombre") %>'
                                                                        Width="160px" ReadOnly="true">
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <HeaderStyle Width="180px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Pag_Numero" HeaderText="Núm." UniqueName="Pag_Numero">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("Pag_Numero") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadComboBox ID="rgcmbNum" runat="server" Width="40px" OnDataBinding="rgcmbMov_DataBinding"
                                                                        SelectedValue='<%# Bind("Pag_Numero") %>'>
                                                                    </telerik:RadComboBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="60px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Pag_Cheque" HeaderText="Cheque" UniqueName="Pag_Cheque">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCheque" runat="server" Text='<%# Bind("Pag_Cheque") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="rgCheque" runat="server" Text='<%# Bind("Pag_Cheque") %>'
                                                                        Width="50px" MaxLength="100">
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="70px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Pag_Importe" HeaderText="Importe" UniqueName="Pag_Importe">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblImportePag" runat="server" Text='<%# Bind("Pag_Importe", "{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="rgtxtImporte" runat="server" Text='<%# Bind("Pag_Importe") %>'
                                                                        Width="50px" MinValue="0" MaxLength="9">
                                                                        <NumberFormat DecimalDigits="2" AllowRounding="true" />
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="70px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Doc_Estatus" HeaderText="Estatus" UniqueName="Doc_Estatus"
                                                                Display="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstatus" runat="server" Text='<%# Bind("Doc_Estatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTextBox ID="rgEstatus" runat="server" Text='<%# Bind("Doc_Estatus") %>'
                                                                        Width="70px">
                                                                    </telerik:RadTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Doc_Importe" HeaderText="Importe" UniqueName="Doc_Importe"
                                                                Display="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblImporteDoc" runat="server" Text='<%# Bind("Doc_Importe") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="rgImporte" runat="server" Text='<%# Bind("Doc_Importe") %>'
                                                                        Width="70px" MaxLength="9" MinValue="0">
                                                                        <NumberFormat DecimalDigits="2" AllowRounding="true" />
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn DataField="Doc_Pagado" HeaderText="Pagado" UniqueName="Doc_Pagado"
                                                                Display="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPagado" runat="server" Text='<%# Bind("Doc_Pagado") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="rgPagado" runat="server" Text='<%# Bind("Doc_Pagado") %>'
                                                                        Width="70px" MaxLength="9" MinValue="0">
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" CancelText="Cancelar" EditText="Editar"
                                                                InsertText="Aceptar" UniqueName="EditCommandColumn" UpdateText="Actualizar">
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </telerik:GridEditCommandColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                                UniqueName="DeleteColumn">
                                                                <HeaderStyle Width="29px" />
                                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top"
                                                                    Width="29px" />
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <EditFormSettings>
                                                            <EditColumn UniqueName="EditCommandColumn1">
                                                            </EditColumn>
                                                        </EditFormSettings>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </MasterTableView>
                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                    <%--<ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">                                                           
                                                                    </Scrolling>
                                                                </ClientSettings>--%>
                                                </telerik:RadGrid>
                                            </telerik:RadPane>
                                        </telerik:RadSplitter>
                                        <%--  </asp:Panel>--%>
                                        <%--   </td>
                                            </tr>
                                        </table>--%>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewCargaArchivo" runat="server" heigth="370px">
                            <telerik:RadSplitter ID="RadSplitter5" runat="server" Height="370px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                                <telerik:RadPane ID="RadPane5" runat="server" Height="370px" OnClientResized="onResize"
                                    BorderStyle="None" Scrolling="None">
                                    <div runat="server" id="CargaArchivo">
                                <table>
                                    <tr>
                                    <td colspan="3">
                                        <telerik:RadAsyncUpload runat="server" ID="RadUpload1" AllowedFileExtensions="xls,xlsx"
                                            Height="25px" Width="400px" OnFileUploaded="RadAsyncUpload1_FileUploaded" ControlObjectsVisibility="None"
                                            ToolTip="Seleccione archivo a subir" MaxFileInputsCount="1" InputSize="30">
                                            <Localization Remove="Quitar" Select="Seleccionar" />
                                        </telerik:RadAsyncUpload>
                                        <asp:Panel ID="ValidFiles" runat="server">
                                        </asp:Panel>&nbsp;<a href="PlantillaPagos.xls">Descarga Plantilla de Excel</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="buttonSubmit" runat="server" CssClass="RadUploadSubmit" Text="Subir Archivo"
                                            Style="margin-top: 6px" OnClick="btnImportar_Click" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>


                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label2" runat="server" Text="Importe de fichas"></asp:Label>
                            </td>
                            <td width="100">
                                <telerik:RadNumericTextBox ID="txtImporte" runat="server" Width="100px" MinValue="0"
                                    ReadOnly="True" Value="0" MaxLength="9">
                                    <NumberFormat DecimalDigits="2" AllowRounding="true" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label3" runat="server" Text="Total en detalle"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MinValue="0"
                                    ReadOnly="True" Value="0" MaxLength="9">
                                    <NumberFormat DecimalDigits="2" AllowRounding="true" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenRebind" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HiddenHeight" runat="server" />
        <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

            function Ficha_Load(sender, args) {
                //debugger;
                var valor = sender.get_value();
                var rgGrid = $find("<%= RgGral.ClientID %>");
                var MasterTable = rgGrid.get_masterTableView();
                var length = MasterTable.get_dataItems().length;
                if (valor == '') {
                    sender.set_value(length + 1);
                }
            }
            function AbrirVentana_PagoDif() {
                var oWnd = radopen("CapPago_Dif.aspx", "AbrirVentana_DifPago");
                oWnd.center();
                //return true;
                //GetRadWindow().BrowserWindow.AbrirVentana_PagoDif();
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
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

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }

                switch (button.get_value()) {
                    case 'save':
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        var mov = $find('<%= txtMovimiento.ClientID %>');
                        if (mov.get_value() == '') {
                            radTabStrip.get_allTabs()[0].select();
                            continuarAccion = false;
                        }
                        else {
                            radTabStrip.get_allTabs()[0].select();
                            if ('<%= FechaEnable %>' == '1') {
                                continuarAccion = _ValidarFechaEnPeriodo();
                            }
                        }
                        if (Page_ClientValidate()) {
                            button.set_enabled(false);
                        }
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function HabilitarGuardar() {
                var toolBar = $find("<%=rtb1.ClientID %>");
                var button = toolBar.findItemByValue("save");
                button.set_enabled(true);
            }
            function txt_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbMovimiento.ClientID %>'));
            }

            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtMovimiento.ClientID %>'));
            }

            var IdBanco;
            var NomBanco;
            function IdBanco_Load(sender, args) {
                IdBanco = sender;
            }
            function Banco_Load(sender, args) {
                NomBanco = sender;
            }
            function txtBanco_OnBlur(sender, args) {
                OnBlur(sender, NomBanco);
            }
            function cmbBanco_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), IdBanco);
            }
            function ObtenerControlFecha() {
                var txtFecha = $find('<%= rdFechaPago.ClientID %>');
                return txtFecha._dateInput;
            }
            function ValidarFichas(sender, args) {
                var rgGrid = $find("<%= RgGral.ClientID %>");
                var MasterTable = rgGrid.get_masterTableView();
                if (MasterTable != null) {
                    var length = MasterTable.get_dataItems().length;
                    if (length < 1) {
                        radalert('Favor de capturar al menos un pago', 330, 150);
                        return true;
                    }
                }
                else {
                    radalert('Favor de capturar al menos un pago', 330, 150);
                    //args.set_cancel(true);
                    return true;
                }
                return false;
            }

            function ClientTabSelecting(sender, args) {

                continuarAccion = ValidarFichas();
                args.set_cancel(continuarAccion);
            }

            function _PreValidarFechaEnPeriodo() {
                //debugger;
                if ('<%= FechaEnable %>' == '1') {
                    _ValidarFechaEnPeriodo();
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
