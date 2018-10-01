<%@ Page Title="Cedula de Visita" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_CedulaVisita.aspx.cs" Inherits="SIANWEB.Rep_CedulaVisita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">    
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {

            }

            function UpdateItemCountField(sender, args) {
                //Set the footer text.
                sender.get_dropDownElement().lastChild.innerHTML = "Un total de " + sender.get_items().get_count() + " direcciones";
            }

            function UpdateItemCountFieldTer(sender, args) {
                //Set the footer text.
                sender.get_dropDownElement().lastChild.innerHTML = "Un total de " + sender.get_items().get_count() + " territorios";
            }

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
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
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                //debugger;
                GetRadWindow().Close();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }

            function TabSelected(sender, args) {

            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                //GetRadWindow().BrowserWindow.location.reload();
            }

            function AbrirReportePadre() {
                GetRadWindow().BrowserWindow.AbrirReporte();
            }

            function refreshGrid() {

            }

            function popup(cn) {
                var oWnd;
                if (cn) {
                    oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                }
                else {
                    var txtAse = $find('<%= txtAsesor.ClientID %>');
                    oWnd = radopen("Ventana_Buscar.aspx?TerAse=" + txtAse.get_value(), "AbrirVentana_Buscar");
                }

                oWnd.center();
            }

            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }

            function alertCallBack(arg) {

                if (arg) {

                    ClienteSeleccionado('cliente');
                    
                }
            }

            function cmbAsesor_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtAsesor.ClientID %>'));
            }

            function txtAsesor_ClientBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbAsesor.ClientID %>'));
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtNumCliente"/>
                    <telerik:AjaxUpdatedControl ControlID="cmbAcys"/>
                    <telerik:AjaxUpdatedControl ControlID="cmbDireccion"/>                    
                </UpdatedControls>
            </telerik:AjaxSetting>                   
            <telerik:AjaxSetting AjaxControlID="cmbAsesor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtAsesor"/>
                    <telerik:AjaxUpdatedControl ControlID="txtNumCliente"/>
                    <telerik:AjaxUpdatedControl ControlID="cmbAcys"/>
                    <telerik:AjaxUpdatedControl ControlID="cmbDireccion"/>                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtNumCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="cmbAcys"/>
                        <telerik:AjaxUpdatedControl ControlID="cmbDireccion"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAgregar" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>                   
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <div>
            <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
                <Items>
                    <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
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
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>     
                            <tr>
                                <td>
                                    Asesor
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAsesor" runat="server" MaxLength="9" MinValue="1" Width="70px">
                                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                <ClientEvents OnKeyPress="handleClickEvent" OnBlur="txtAsesor_ClientBlur"/>
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>                                
                                                &nbsp;                                
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbAsesor" runat="server" AutoPostBack="True" Width="300px" MaxHeight="400px" ChangeTextOnKeyBoardNavigation="true"
                                                                    DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                                    EnableLoadOnDemand="True" Filter="Contains" OnClientSelectedIndexChanged="cmbAsesor_ClientSelectedIndexChanged" OnSelectedIndexChanged="cmbAsesor_SelectedIndexChanged">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 50px; text-align: center">
                                                                            <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString()%>
                                                                        </td>
                                                                        <td style="width: 200px; text-align: left">
                                                                            <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>                                                                    
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>                            
                            </tr>                   
                            <tr>
                                <td>
                                    <asp:Label ID="lblCliente" Text="Cliente:" runat="server">
                                    </asp:Label>&nbsp;
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtNumCliente" runat="server" AutoPostBack="true" MaxLength="9"
                                                            MinValue="1" OnTextChanged="txtCliente_TextChanged" Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>                                
                                                &nbsp;                                
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="295px" enabled="false">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>                                
                                                &nbsp;                                
                                            </td>
                                            <td>                                
                                                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                                ToolTip="Buscar" ValidationGroup="buscar"/>                                
                                            </td>
                                        </tr>
                                    </table>
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
                            <tr>
                                <td>
                                    <asp:Label ID="lblAcys" Text="Territorio:" runat="server">
                                    </asp:Label>&nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAcys" runat="server" Width="550px" MarkFirstMatch="true"
                                                         EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnClientItemsRequested="UpdateItemCountFieldTer"
                                                         OnDataBound="cmbAcys_DataBound" OnItemDataBound="cmbAcys_ItemDataBound">
                                        <HeaderTemplate>
                                            <ul>
                                                <li class="col1">Id Acys</li>
                                                <li class="col2">Id Territorio Asesor</li>   
                                                <li class="col3">Territorio Asesor</li>
                                                <li class="col4">Id Territorio Rep</li>   
                                                <li class="col4">Territorio Rep</li>
                                            </ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <ul>
                                                <li class="col1">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id_Acs") %></li>
                                                <li class="col2">
                                                    <%# DataBinder.Eval(Container.DataItem, "Acs_RscIdTerr")%></li>
                                                <li class="col3">
                                                    <%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Acs_RscTerritorio").ToString()) ? " " : DataBinder.Eval(Container.DataItem, "Acs_RscTerritorio"))%></li>
                                                <li class="col4">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id_Ter")%></li>
                                                <li class="col4">
                                                    <%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Acs_Territorio").ToString()) ? " " : DataBinder.Eval(Container.DataItem, "Acs_Territorio"))%></li> 
                                            </ul>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Un total de 
                                            <asp:Literal runat="server" ID="RadComboItemsCountAcys" />
                                            Territorios.
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDirEntrega" Text="Dirección de Entrega:" runat="server">
                                    </asp:Label>&nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbDireccion" runat="server" Width="550px" MarkFirstMatch="true"
                                                         EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnClientItemsRequested="UpdateItemCountField"
                                                         OnDataBound="cmbDireccion_DataBound" OnItemDataBound="cmbDireccion_ItemDataBound">    
                                        <HeaderTemplate>
                                            <ul>
                                                <li class="col1">Calle</li>
                                                <li class="col2">Número</li>
                                                <li class="col3">Código Postal</li>
                                                <li class="col4">Colonia</li>
                                            </ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <ul>
                                                <li class="col1">
                                                    <%# DataBinder.Eval(Container.DataItem, "Cte_Calle") %></li>
                                                <li class="col2">
                                                    <%# DataBinder.Eval(Container.DataItem, "Cte_Numero") %></li>
                                                <li class="col3">
                                                    <%# DataBinder.Eval(Container.DataItem, "Cte_Cp") %></li>
                                                <li class="col4">
                                                    <%# DataBinder.Eval(Container.DataItem, "Cte_Colonia") %></li>
                                            </ul>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Un total de 
                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            direcciones.
                                        </FooterTemplate>                                        
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:HiddenField ID="HF_Cve" runat="server" />
                    </td>
                </tr>            
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <telerik:RadGrid ID="rgAgregar" runat="server" GridLines="None" PageSize="15"
                                        MasterTableView-NoMasterRecordsText="No se encontraron registros." AutoGenerateColumns="False"
                                        AllowPaging="false" HeaderStyle-HorizontalAlign="Center" OnNeedDataSource="rgAgregar_NeedDataSource"                                   
                                        Enabled="true" OnItemCommand="rgAgregar_ItemCommand">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Núm. Cliente" UniqueName="Id_Cte">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Acs" HeaderText="Núm. Acys" UniqueName="Id_Acs" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Acs_NomComercial" HeaderText="Nombre Comercial" UniqueName="Acs_NomComercial">
                                        <HeaderStyle HorizontalAlign="Center" Width="270px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IdCte_DirEntrega" HeaderText="Núm. Direccion Entrega" UniqueName="IdCte_DirEntrega" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="Acs_RscIdTerr" HeaderText="Núm. Territorio Asesor" UniqueName="Acs_RscIdTerr">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Acs_RscTerritorio" HeaderText="Territorio Asesor" UniqueName="Acs_RscTerritorio">
                                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Núm. Territorio Rep" UniqueName="Id_Ter">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Acs_Territorio" HeaderText="Territorio Rep" UniqueName="Acs_Territorio">
                                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="DireccionEntrega" HeaderText="Direccion Entrega" UniqueName="DireccionEntrega">
                                        <HeaderStyle HorizontalAlign="Center" Width="500px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                                        ConfirmText="¿Deseas eliminar el registro?" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                                        Text="Cancelar" UniqueName="Cancelar" Visible="True" ButtonType="ImageButton"
                                                        ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                            ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>    
    
</asp:Content>
