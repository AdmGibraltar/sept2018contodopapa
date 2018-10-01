<%@ Page Title="Reclamaciones" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.master"
    AutoEventWireup="true" CodeBehind="CapReclamaciones.aspx.cs" Inherits="SIANWEB.CapReclamaciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" 
        onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divDescripcion" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divDescripcion" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divDescripcion"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCodigo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divDescripcion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dpFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicked="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="175px">
                    &nbsp;
                </td>
                <td width="175px">
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
                        SelectedIndex="0" OnClientTabSelecting="TabSelected">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="D&lt;u&gt;e&lt;/u&gt;scripción" PageViewID="RadPageViewDescripcion"
                                Selected="True" AccessKey="E">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="&lt;u&gt;A&lt;/u&gt;cción" AccessKey="A" PageViewID="RadPageViewAccion">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Con&lt;u&gt;f&lt;/u&gt;ormidad" PageViewID="RadPageViewConformidad"
                                AccessKey="F">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="&lt;u&gt;C&lt;/u&gt;omentarios" PageViewID="RadPageViewComentarios"
                                AccessKey="C">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px"><%-- Height="265px" Width="650px">--%>
                        <telerik:RadPageView ID="RadPageViewDescripcion" runat="server">
                           <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="265px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="265px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                            <div runat="server" id="divDescripcion">
                                <table>
                                    <tr>
                                        <td width="10">
                                            &nbsp;
                                        </td>
                                        <td width="120">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="right" width="50">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10">
                                        </td>
                                        <td width="120">
                                            <asp:Label ID="Label1" runat="server" Text="Reclamación"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtReclamacion" runat="server" Enabled="false" MaxLength="9"
                                                MinValue="1" Width="70px">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td align="right" width="50">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label20" runat="server" Text="Fecha"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="rdFecha" runat="server" Width="100px">
                                                <DatePopupButton ToolTip="Abrir calendario" />
                                                <Calendar ID="Calendar1" runat="server">
                                                    <ClientEvents OnDateClick="Calendar_Click" />
                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                        TodayButtonCaption="Hoy">
                                                    </FastNavigationSettings>
                                                </Calendar>
                                                <DateInput runat="server">
                                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                </DateInput>
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="10">
                                            &nbsp;
                                        </td>
                                        <td width="160">
                                            <asp:Label ID="Label4" runat="server" Text="Cliente"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtNumCliente" runat="server" MaxLength="9" MinValue="1"
                                                Width="70px" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txt_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td colspan="1">
                                            <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="300px" ReadOnly="True">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClienteNombre"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                            <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" 
                                                OnClick="imgAceptar_Click" ToolTip="Buscar" ValidationGroup="buscar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Usuario"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadTextBox ID="txtUsuario" runat="server" MaxLength="50" Width="250px">
                                                <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsuario"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Teléfono"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtTelefono" runat="server" MaxLength="20" MinValue="0" Width="100px">
                                                <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloNumerico" />
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTelefono"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Territorio"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtTerritorioId" runat="server" MaxLength="9" MinValue="1"
                                                Width="70px">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txt1_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td colspan="1">
                                            <telerik:RadComboBox ID="cmbTerritorio" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccionar cliente"
                                                EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                MarkFirstMatch="true" MaxHeight="150px" OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo"
                                                OnClientSelectedIndexChanged="cmb1_ClientSelectedIndexChanged" Width="300px">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 25px; text-align: center; vertical-align: top">
                                                                <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" 
                                                                ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' Width="50px" />
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTerritorioId"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="10">
                                            &nbsp;
                                        </td>
                                        <td width="160">
                                            <asp:Label ID="Label8" runat="server" Text="Tipo de reclamación"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbTipo" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                OnClientFocus="_ValidarFechaEnPeriodo" OnSelectedIndexChanged="cmbTipo_SelectedIndexChanged"
                                                Width="175px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbTipo"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="10">
                                            &nbsp;
                                        </td>
                                        <td width="160">
                                            <asp:Label ID="Label9" runat="server" Text="Código de no conformidad"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtCodigo" runat="server" MaxLength="9" MinValue="1"
                                                Width="70px">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                <ClientEvents OnBlur="txt2_OnBlur" OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td colspan="1">
                                            <telerik:RadComboBox ID="cmbCodigo" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                DataTextField="Descripcion" DataValueField="Id" EmptyMessage="Seleccionar tipo de reclamación"
                                                EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur" OnClientFocus="_ValidarFechaEnPeriodo"
                                                OnClientSelectedIndexChanged="cmb2_ClientSelectedIndexChanged" Width="300px"
                                                MaxHeight="150px">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 25px; text-align: center; vertical-align: top">
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" 
                                                                ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' Width="50px" />
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCodigo"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="Descripción"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadTextBox ID="txtDescripcion" runat="server" MaxLength="250" Width="370px">
                                                <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDescripcion"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Causa raíz"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <telerik:RadTextBox ID="txtCausa" runat="server" MaxLength="250" Width="370px">
                                                <ClientEvents OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCausa"
                                                Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                              </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewAccion" runat="server">
                           <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="265px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="265px" OnClientResized="onResize" BorderStyle="None">
                            <div runat="server" id="divAccion">
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td height="10">
                                        </td>
                                        <td width="70">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="10">
                                            <asp:Label ID="Label13" runat="server" Width="110px" Text="Fecha de acción"></asp:Label>
                                        </td>
                                        <td width="70">
                                            <telerik:RadDatePicker ID="dpFecha" runat="server" AutoPostBack="True" OnSelectedDateChanged="dpFecha_SelectedDateChanged">
                                                <DatePopupButton ToolTip="Abrir calendario" />
                                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                    ViewSelectorText="x">
                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                        TodayButtonCaption="Hoy" />
                                                </Calendar>
                                                <DateInput ID="DateInput1" runat="server">
                                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                </DateInput>
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="10" valign="top">
                                            <asp:Label ID="Label14" runat="server" Text="Acción correctiva" Width="110px"></asp:Label>
                                        </td>
                                        <td colspan="5" style="width: 120px">
                                            <telerik:RadTextBox ID="txtAccion1" runat="server" MaxLength="250" Rows="3" 
                                                TextMode="MultiLine" Width="450px" ClientEvents-OnBlur="accion1Blur">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="10" valign="top">
                                            <asp:Label ID="Label15" runat="server" Text="Acción preventiva" Width="110px"></asp:Label>
                                        </td>
                                        <td colspan="5" style="width: 120px" width="70">
                                            <telerik:RadTextBox ID="txtAccion2" runat="server" MaxLength="250" Rows="3" 
                                                TextMode="MultiLine" Width="450px">
                                                <ClientEvents OnBlur="accion2Blur" />
                                            </telerik:RadTextBox>
                                        </td>
                                         <td>
                                             <asp:Label ID="Label22" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="10">
                                            <asp:Label ID="Label16" runat="server" Text="Responsable" Width="110px"></asp:Label>
                                        </td>
                                        <td colspan="5" style="width: 120px" width="70">
                                            <telerik:RadTextBox ID="txtResponsable" runat="server" MaxLength="50" 
                                                Width="450px">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" OnBlur="ResponsableBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                         <td>
                                             <asp:Label ID="Label23" runat="server" ForeColor="Red"></asp:Label>
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
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                              </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewConformidad" runat="server">
                         <telerik:RadSplitter ID="RadSplitter3" runat="server" Height="265px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                              <telerik:RadPane ID="RadPane3" runat="server" Height="265px" OnClientResized="onResize" BorderStyle="None">
                            <div runat="server" id="divConformidad">
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td height="10">
                                        </td>
                                        <td width="70">
                                        </td>
                                        <td width="250">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="10">
                                            <asp:Label ID="Label17" runat="server" Width="130px" Text="Fecha de conformidad"></asp:Label>
                                        </td>
                                        <td colspan="2" width="70">
                                            <telerik:RadDatePicker ID="dpFecha1" runat="server" AutoPostBack="True" OnSelectedDateChanged="dpFecha1_SelectedDateChanged">
                                                <DatePopupButton ToolTip="Abrir calendario" />
                                                <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                    ViewSelectorText="x">
                                                    <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                        TodayButtonCaption="Hoy" />
                                                </Calendar>
                                                <DateInput ID="DateInput2" runat="server">
                                                    <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                </DateInput>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td width="15">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="10">
                                            <asp:Label ID="Label18" runat="server" Width="130px" Text="Nombre"></asp:Label>
                                        </td>
                                        <td colspan="2" style="width: 85px">
                                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="450px" MaxLength="50" ClientEvents-OnBlur="NombreBlur">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td height="10">
                                            <asp:Label ID="Label19" runat="server" Width="130px" Text="Departamento"></asp:Label>
                                        </td>
                                        <td colspan="2" style="width: 85px">
                                            <telerik:RadTextBox ID="txtDepartamento" runat="server" Width="450px" MaxLength="50">
                                                <ClientEvents OnKeyPress="SoloAlfanumerico" OnBlur="DepartamentoBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td width="120">
                                            <asp:HiddenField ID="HF_ID" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="HiddenRebind" runat="server" Value="0" />
                                        </td>
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
                        <telerik:RadPageView ID="RadPageViewComentarios" runat="server">
                         <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="265px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                              <telerik:RadPane ID="RadPane4" runat="server" Height="265px" OnClientResized="onResize" BorderStyle="None">
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtComentario" runat="server" Rows="9" TextMode="MultiLine"
                                                Height="231" Width="600px" MaxLength="500">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                             </telerik:RadPane>
                           </telerik:RadSplitter>
                         </telerik:RadPageView>                        
                    </telerik:RadMultiPage>
                </td>
                <td>
                    <asp:HiddenField ID="HiddenHeight" runat="server" />
                    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function LimpiarBanderaRebind() {
            }

            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;              
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                    ajaxManager.ajaxRequest('panel');
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
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save') {
                    habilitaValidacion = true;
                }
                else {
                    habilitaValidacion = false;
                }

                for (i = 0; i < Page_Validators.length; i++) {
                    ValidatorEnable(Page_Validators[i], habilitaValidacion);
                }

                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        LimpiarControles();
                        //registro nuevo -> se limpia bandera de actualización
                        var txtId = $find('<%= txtReclamacion.ClientID %>');                       
                        var urlArchivo = 'ObtenerMaximo.aspx';
                        parametros = "Catalogo=CapReclamaciones";
                        parametros = parametros + "&sp=spCatLocal_Maximo";
                        parametros = parametros + "&columna=Id_Pag";
                        var resultado = obtenerrequest(urlArchivo, parametros);
                        txtId.set_value(resultado);
                        continuarAccion = false;
                        break;
                }
            }
            function txt2_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbCodigo.ClientID %>'));
            }

            function cmb2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCodigo.ClientID %>'));
            }

            function txt1_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            function cmb1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorioId.ClientID %>'));
            }

            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtNumCliente.ClientID %>'));
            }

            function txt_OnBlur(sender, args) {
            }

            function ObtenerControlFecha() {
                var txtFecha = $find('<%= rdFecha.ClientID %>');
                return txtFecha._dateInput;
            }

            function TabSelected(sender, args) {
                var txtCteVar = $find('<%= txtNumCliente.ClientID %>');
                var txtUsuVar = $find('<%= txtUsuario.ClientID %>');
                var txtTelVar = $find('<%= txtTelefono.ClientID %>');
                var txtTerVar = $find('<%= txtTerritorioId.ClientID %>');
                var cmbTipVar = $find('<%= cmbTipo.ClientID %>');
                var txtCodVar = $find('<%= txtCodigo.ClientID %>');
                var txtDesVar = $find('<%= txtDescripcion.ClientID %>');
                var txtCRaVar = $find('<%= txtCausa.ClientID %>');

                if (txtCodVar.get_value() == '' ||
                    txtUsuVar.get_value() == '' ||
                    txtTelVar.get_value() == '' ||
                    txtTerVar.get_value() == '' ||
                    cmbTipVar.get_value() == '' ||
                    txtCodVar.get_value() == '' ||
                    txtDesVar.get_value() == '' ||
                    txtCRaVar.get_value() == '') {
                    radalert('Todos los campos de la sección de descripción son obligatorios</br></br>', 330, 150);
                    args.set_cancel(true);
                }
            }

            /*CERRAR VENTANA*/
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                //debugger;
                                //GetRadWindow().Close();
                                CloseAndRebind();
                            });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                //debugger;
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function accion1Blur() {
                var accion = $find('<%= txtAccion1.ClientID %>');
                var lblaccion = document.getElementById('<%= Label21.ClientID %>');

                if (accion.get_value() != "") {
                    lblaccion.innerHTML = "";
                }
            }

            function accion2Blur() {
                var accion = $find('<%= txtAccion2.ClientID %>');
                var lblaccion = document.getElementById('<%= Label22.ClientID %>');

                if (accion.get_value() != "") {
                    lblaccion.innerHTML = "";
                }
            }

            function ResponsableBlur() {
                var accion = $find('<%= txtResponsable.ClientID %>');
                var lblaccion = document.getElementById('<%= Label23.ClientID %>');

                if (accion.get_value() != "") {
                    lblaccion.innerHTML = "";
                }
            }

            function NombreBlur() {
                var accion = $find('<%= txtNombre.ClientID %>');
                var lblaccion = document.getElementById('<%= Label24.ClientID %>');

                if (accion.get_value() != "") {
                    lblaccion.innerHTML = "";
                }
            }

            function DepartamentoBlur() {
                var accion = $find('<%= txtDepartamento.ClientID %>');
                var lblaccion = document.getElementById('<%= Label25.ClientID %>');

                if (accion.get_value() != "") {
                    lblaccion.innerHTML = "";
                }
            }

            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }

            function ClienteSeleccionado() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('cliente');
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
