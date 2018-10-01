<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="ConfiguracionRelacionGestor.aspx.cs" Inherits="SIANWEB.ConfiguracionRelacionGestor" %>
     
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest"
        DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divFiltro" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divFiltro" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divFiltro" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgClientes" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgClientes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgClientes" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Aceptar" CssClass="aceptarToolbar"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
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
        <div runat="server" id="divFiltro">
            <table style="font-family: Verdana; font-size: 8pt; height: 100%">
                <tr>
                    <td width="10">
                    </td>
                    <td width="70">
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
                    </td>
                    <td>
                        <asp:Label ID="lblCentro" runat="server" Text="Centro"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbCentro" runat="server" DataTextField="Descripcion" DataValueField="Id"
                            AutoPostBack="True" OnSelectedIndexChanged="cmbCentro_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table style="font-family: Verdana; font-size: 8pt; height: 100%">
                <tr>
                    <td width="10">
                        &nbsp;
                    </td>
                    <td width="70">
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtCliente" runat="server" MaxLength="9" MinValue="1"
                            Width="70px" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            <ClientEvents OnKeyPress="handleClickEvent" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtClienteNombre" runat="server" ReadOnly="True" Width="295px">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="val_txtCliente" runat="server" ControlToValidate="txtCliente"
                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                            ToolTip="Buscar" ValidationGroup="buscar" />
                    </td>
                </tr>
            </table>
            <table style="font-family: Verdana; font-size: 8pt; height: 100%">
                <tr>
                    <td width="10">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="1"
                            Width="70px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            <ClientEvents OnBlur="txtTerritorio_OnBlur" OnKeyPress="handleClickEvent" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                            DataTextField="Descripcion" DataValueField="Id" Filter="Contains" HighlightTemplatedItems="true"
                            LoadingMessage="Cargando..." MarkFirstMatch="true" MaxHeight="300px" OnClientBlur="Combo_ClientBlur"
                            OnClientSelectedIndexChanged="cmbTerritorio_SelectedIndexChanged" Width="300px">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 50px; text-align: center">
                                            <%# DataBinder.Eval(Container.DataItem, "Id_Ter").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id_Ter").ToString()%>
                                        </td>
                                        <td style="width: 200px; text-align: left">
                                            <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                            <div style="display: none">
                                                <asp:Label ID="lbl_Id_Rik" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Rik") %>'></asp:Label>
                                                <asp:Label ID="lbl_Rik_Nombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rik_Nombre") %>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgAgregar" runat="server" CssClass="add16" ImageUrl="~/Imagenes/blank.png"
                            OnClick="imgAgregar_Click" ToolTip="Agregar" ValidationGroup="add" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td width="70">
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
            </table>
        </div>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadGrid ID="rgClientes" runat="server" AutoGenerateColumns="False" GridLines="None"
                        MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rgClientes_ItemCommand"
                        OnNeedDataSource="rgClientes_NeedDataSource" OnPageIndexChanged="rgClientes_PageIndexChanged"
                        PageSize="6">
                        <MasterTableView NoMasterRecordsText="No se encontraron registros." CommandItemDisplay="None">
                            <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="GUID" UniqueName="GUID" DataField="GUID" Display="false">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Centro" UniqueName="Cd_Nombre" DataField="Cd_Nombre">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Id_Cte" DataField="Id_Cte">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cliente" UniqueName="Cte_Nombre" DataField="Cte_Nombre">
                                    <HeaderStyle Width="275px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Clave" UniqueName="Id_Ter" DataField="Id_Ter">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Territorio" UniqueName="Ter_Nombre" DataField="Ter_Nombre">
                                    <HeaderStyle Width="275px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" UniqueName="DeleteColumn"
                                    Text="Eliminar">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                            PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenRebind" runat="server" />
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function txtTerritorio_OnBlur(sender, args) {
                    OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
                }

                function cmbTerritorio_SelectedIndexChanged(sender, eventArgs) {
                    ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
                }


                function popup(cd) {
                    var oWnd = radopen("Ventana_Buscar.aspx?cd=" + cd + "&Id='" + Math.random() + "'", "AbrirVentana_Buscar");
                    oWnd.center();
                    return false;
                }

                function ClienteSeleccionado(param) {
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    ajaxManager.ajaxRequest('cliente');
                }

                function ToolBar_ClientClick(sender, args) {
                    CloseWindow();

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
            </script>
        </telerik:RadCodeBlock>
    </div>
</asp:Content>
