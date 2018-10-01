<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapFisico.aspx.cs" Inherits="SIANWEB.CapFisico" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Actualiza el número de registros en combo de productos.
            //--------------------------------------------------------------------------------------------------
            function handleClickEventFisico(sender, eventArgs) {
                var key = eventArgs.get_keyCode();
                if (key && key == 13) {
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    ajaxManager.ajaxRequest('guardar');
                }
            }
            function cmbProductosLista_UpdateItemCountField(sender, args) {
                //set the footer text
                sender.get_dropDownElement().lastChild.innerHTML = "Un total de " + sender.get_items().get_count() + " registros.";
            }
            function cmb3_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtProducto.ClientID %>'));
            }
            //cuando el campo de texto pierde el foco
            function txtProducto_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbProductosLista.ClientID %>'));
            }
            function test(sender, eventArgs) {

                var mySplitResult = eventArgs.get_item()._text.split("   ");
                var i = 1;
                var cadena = "";
                for (i = 1; i < mySplitResult.length; i++) {
                    cadena = cadena + mySplitResult[i] + " ";
                }

            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                var continuarAccion = true;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'new':

                        continuarAccion = Confirma();
                        break;
                    case 'delete':
                        //debugger;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function confirmCallBackFn(arg) {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                if (arg) {

                    ajaxManager.ajaxRequest('ok');
                }

            }
            function Confirma() {
                radconfirm("Por favor confirme que desea inicializar el inventario.\nSe borrarán todos los datos de físicos y consignados en la base de datos\n", confirmCallBackFn, 400, 160)
                return false;
            }


            var cmbCte;
            var txtCte;
            var cmbTerr;
            var txtTerr;

            function cmbCte_Load(sender, args) {
                cmbCte = sender;
            }
            function txtCte_Load(sender, args) {
                txtCte = sender;
            }
            function cmbTerr_Load(sender, args) {
                cmbTerr = sender;
            }
            function txtTerr_Load(sender, args) {
                txtTerr = sender;
            }

            function txtCte_OnBlur(sender, args) {
                OnBlur(sender, cmbCte);
            }

            function cmbCte_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), txtCte);
            }
            function txtTerr_OnBlur(sender, args) {
                OnBlur(sender, cmbTerr);
            }

            function cmbTerr_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), txtTerr);
            }

            function Producto_Focus() {
                //debugger;
                var combo = $find("<%= cmbProductosLista.ClientID %>");
                combo.clearSelection();
            }
            function popup(terr) {

                var oWnd = radopen("Ventana_Buscar.aspx?ter=" + terr, "AbrirVentana_Buscar");
                oWnd.center();
            }
            function UpExcel() {

                var oWnd = radopen("Ventana_Fisico.aspx", "AbrirVentana_vExcel");
                oWnd.center();
            }
            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
            function FisicoTerminado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbProductosLista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecorationZoneID="DecoratedControlsContainer"
        runat="server" />
    <div>
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick"
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
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Inicializar físicos"
                    CssClass="new" ImageUrl="Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="DwExcel" Value="DwExcel" Text="" CssClass="facPedido"
                    ToolTip="Descargar formato" ImageUrl="Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="UpExcel" Value="UpExcel" Text="" CssClass="descExcel"
                    ToolTip="Subir archivo Excel" ImageUrl="Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
        <div runat="server" id="divPrincipal1">
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="lblCentro" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <div runat="server" id="divPrincipal">
                <table style="font-family: Verdana; font-size: 8pt">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                                SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab runat="server" Text="&lt;u&gt;F&lt;/u&gt;ísico" PageViewID="RPVFisico"
                                        SelectedIndex="0" Selected="True" AccessKey="F">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" Text="&lt;u&gt;C&lt;/u&gt;onsignación" PageViewID="RPVConsignacion"
                                        AccessKey="U">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                                BorderWidth="1px">
                                <telerik:RadPageView ID="RPVFisico" runat="server">
                                    <table>
                                        <tr>
                                            <td width="80">
                                            </td>
                                            <td width="50">
                                            </td>
                                            <td width="80">
                                            </td>
                                            <td width="80">
                                                &nbsp;
                                            </td>
                                            <td width="80">
                                                &nbsp;
                                            </td>
                                            <td width="80">
                                            </td>
                                            <td width="150">
                                            </td>
                                            <td width="20">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblProducto" runat="server" Text="Producto"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtProducto" runat="server" MinValue="1" Width="70px"
                                                    AutoPostBack="True" OnTextChanged="txtProducto_TextChanged">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtProducto_OnBlur" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td colspan="5">
                                                <%--  <telerik:RadComboBox runat="server" ID="cmbProductosLista" Width="330px" HighlightTemplatedItems="true"
                                                EnableLoadOnDemand="true" AutoPostBack="true" DataTextField="Prd_Descripcion"
                                                DataValueField="Id_Prd" OnDataBound="cmbProductosLista_DataBound" OnItemDataBound="cmbProductosLista_ItemDataBound"
                                                OnSelectedIndexChanged="cmbProductosLista_SelectedIndexChanged" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" 
                                                Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientSelectedIndexChanged="cmbProductosLista_ClientSelectedIndexChanged">
                                                <HeaderTemplate>
                                                    <ul>
                                                        <li class="col200">Descripcion</li>
                                                        <li class="col3">Activo</li>
                                                    </ul>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <ul>
                                                        <li class="col2" runat="server" id="liClave" style="display: none">
                                                            <asp:Label ID="lblLiCd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Cd") %>'></asp:Label>
                                                            <asp:Label ID="lblLiClave" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id_Prd") %>'></asp:Label>
                                                          </li>
                                                        <li class="col200" runat="server" id="liDescripcion">
                                                            <%# DataBinder.Eval(Container.DataItem, "Prd_Descripcion")%></li>
                                                        <li class="col3" runat="server" id="liActivo">
                                                            <asp:CheckBox ID="chkProductoEsActivo" runat="server" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "Prd_Activo") %>' />
                                                        </li>
                                                    </ul>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Un total de
                                                    <asp:Literal runat="server" ID="cmbProductosCount" />
                                                    registros.
                                                </FooterTemplate>
                                            </telerik:RadComboBox>--%>
                                                <telerik:RadComboBox ID="cmbProductosLista" runat="server" Width="350px" Filter="Contains"
                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientSelectedIndexChanging="test"
                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                    LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cmbProductosLista_SelectedIndexChanged" MaxHeight="250px"
                                                    EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True" ItemsPerRequest="10"
                                                    ShowMoreResultsBox="True" OnClientDropDownOpening="Producto_Focus">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: left">
                                                                    <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                                        NoMatches="No hay coincidencias" />
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td width="80">
                                                <asp:Label ID="lblPresentacion" runat="server" Text="Presentación"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPresentacion" runat="server" ReadOnly="true" Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblInventario0" runat="server" Text="Unidades"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPresentacion0" runat="server" ReadOnly="true" Width="70px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblInventario" runat="server" Text="Inventario"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtInventario" runat="server" MaxLength="6" ReadOnly="true"
                                                    Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
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
                                                <asp:Label ID="lblFisico" runat="server" Text="Físico"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtFisico" runat="server" MaxLength="9" MinValue="0"
                                                    Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnKeyPress="handleClickEventFisico" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFisico"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
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
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td width="80">
                                                <asp:HiddenField ID="HF_ID" runat="server" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="HF_Contador" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RPVConsignacion" runat="server">
                                    <br />
                                    &nbsp; Producto:
                                    <asp:Label ID="lblProducto2" runat="server" Font-Bold="True" Text="Ninguno seleccionado"></asp:Label>
                                    <br />
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                    MasterTableView-NoMasterRecordsText="No se encontraron registros." OnItemCommand="rg1_ItemCommand"
                                                    OnNeedDataSource="rg1_NeedDataSource" OnPageIndexChanged="rg1_PageIndexChanged"
                                                    PageSize="6" onitemdatabound="rg1_ItemDataBound">
                                                    <MasterTableView CommandItemDisplay="Top" EditMode="InPlace">
                                                        <CommandItemSettings AddNewRecordText="Agregar" ShowRefreshButton="false" />
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Num." UniqueName="Id_Ter">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdTer" runat="server" Text='<%# Bind("Id_Ter") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtIdTer" runat="server" Text='<%# Bind("Id_Ter") %>'
                                                                        MaxLength="9" Width="60px" MinValue="1" OnTextChanged="txtTerritorio_TextChanged"
                                                                        AutoPostBack="true" OnLoad="txtTerritorio_Load">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        <ClientEvents OnLoad="txtTerr_Load" OnBlur="txtTerr_OnBlur" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="80px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Territorio" UniqueName="Id_Ter">
                                                                <%-- DataField="Id_Ter"--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCmbIdTer" runat="server" Text='<%# Bind("Id_TerStr") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadComboBox ID="Cmb_Id_Ter" runat="server" Width="300px" Filter="Contains"
                                                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                                        DataTextField="Descripcion" DataValueField="Id" HighlightTemplatedItems="true"
                                                                        LoadingMessage="Cargando..." OnClientSelectedIndexChanged="cmbTerr_ClientSelectedIndexChanged"
                                                                        OnClientLoad="cmbTerr_Load" Text='<%# Bind("Id_TerStr") %>' MaxHeight="250px"
                                                                        EnableLoadOnDemand="false" OnDataBinding="cmbTerr_DataBinding">
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
                                                                <HeaderStyle Width="320px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Num." UniqueName="Id_Cte">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdCte" runat="server" Text='<%# Bind("Id_Cte") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtIdCte" runat="server" Text='<%# Bind("Id_Cte") %>'
                                                                        MaxLength="9" Width="60px" MinValue="1" AutoPostBack="true" OnTextChanged="txtCliente_TextChanged"
                                                                        OnLoad="txtProducto_Load">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        <ClientEvents OnLoad="txtCte_Load" OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="80px" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Cliente" UniqueName="Id_Cte">
                                                                <%--DataField="Id_Cte"--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCmbIdCte" runat="server" Text='<%# Bind("Id_CteStr") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td style="border-bottom-style: none">
                                                                                <telerik:RadTextBox ID="txtCliente" runat="server" Width="250px" MaxLength="9" ReadOnly="true"
                                                                                    Text='<%# Bind("Id_CteStr") %>'>
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td style="border-bottom-style: none">
                                                                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                                                    ToolTip="Buscar" ValidationGroup="buscar" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="320px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Productos en consignación" UniqueName="Fis_Consignados">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFis_Consignados" runat="server" Text='<%# Bind("Fis_Consignados") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtFis_Consignados" runat="server" Text='<%# Bind("Fis_Consignados") %>'
                                                                        MaxLength="9" Width="60px" MinValue="0">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Editar" CancelText="Cancelar"
                                                                UpdateText="Actualizar" InsertText="Aceptar" UniqueName="EditCommandColumn">
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </telerik:GridEditCommandColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                                                ConfirmDialogHeight="130px" ConfirmText="¿Borrar este detalle?" Text="Borrar"
                                                                UniqueName="DeleteColumn">
                                                                <HeaderStyle Width="30px" />
                                                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </telerik:GridButtonColumn>
                                                            <telerik:GridTemplateColumn UniqueName="Id_FisCons" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId_FisCons" runat="server" Text='<%# Bind("Id_FisCons") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_FisCons" runat="server" Text='<%# Bind("Id_FisCons") %>'
                                                                        MinValue="0">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="Id_Emp" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId_Emp" runat="server" Text='<%# Bind("Id_Emp") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Emp" runat="server" Text='<%# Bind("Id_Emp") %>'>
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="Id_Cd" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId_Cd" runat="server" Text='<%# Bind("Id_Cd") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Cd" runat="server" Text='<%# Bind("Id_Cd") %>'>
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="Id_Fis" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId_Fis" runat="server" Text='<%# Bind("Id_Fis") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtId_Fis" runat="server" Text='<%# Bind("Id_Fis") %>'>
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
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
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
