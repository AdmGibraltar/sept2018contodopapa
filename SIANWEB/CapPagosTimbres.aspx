<%@ Page Title="Pagos Timbres" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="CapPagosTimbres.aspx.cs" Inherits="SIANWEB.CapPagosTimbres" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1"  runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
             <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnTimbrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
     
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
                <td align="right">
                <%--<telerik:RadButton ID="btnTimbrar" runat="server" Text="Timbrar">
                </telerik:RadButton>--%>
               <asp:ImageButton ID="btnTimbrar" runat="server" ImageUrl="~/images/check.png" OnClick="Timbrar_Click"
                                    ToolTip="Timbrar" />
                </td>
            <tr>
                <td>
                </td>
                
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Timbres" AccessKey="T" PageViewID="RadPageViewDetalles">
                            </telerik:RadTab>

                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <%--height="370px" width="820px">--%>
                        
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
                                                    PageSize="20" OnItemCreated="RgDet_ItemCreated" OnItemDataBound="RgDet_ItemDataBound" >
                                                    <MasterTableView >
                                                        <Columns>

                                                         <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" Text="Enviar" UniqueName="Enviar"
                                                            Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                        </telerik:GridButtonColumn>


                                                        <%--<telerik:GridButtonColumn CommandName="Timbrar" HeaderText="Timbrar" ConfirmDialogType="RadWindow"
                                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                                            Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                        </telerik:GridButtonColumn>--%>

                                                        <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                            UniqueName="PDF">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </telerik:GridTemplateColumn>
                                                        
                                                        <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                            UniqueName="XML">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn UniqueName="TimbrarVarios">
                                                            <HeaderTemplate>
                                                                <input onclick="CheckAllTimbrar(this);" type="checkbox" id="ChkTimbrarHeader" runat="server" />
                                                                Timbrar
                                                            
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkTimbrar" runat="server" Style="cursor: hand" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridTemplateColumn HeaderText="Id" UniqueName="rgDetlId2" Display="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDetlId1" runat="server" Text='<%# Bind("RgDId") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="20px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Doc_Referencia" HeaderText="Doc_Referencia" Display="false" UniqueName="Id_Emp">
                                                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Cdi" HeaderText="Cdi" Display="false" UniqueName="Id_Emp">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id_Cte" Display="false" UniqueName="Id_Emp">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Id_PagDet" HeaderText="Id_PagDet" Display="false" UniqueName="Id_PagDet">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Cte_Rfc" HeaderText="Cte_Rfc" Display="false" UniqueName="Cte_Rfc">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Serie" HeaderText="Serie2" Display="false" UniqueName="Serie2">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridTemplateColumn DataField="Pag_Movimiento" HeaderText="Movimiento" UniqueName="Pag_Movimiento">
                                       
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMov" runat="server" Text='<%# Bind("Pag_MovimientoStr") %>'></asp:Label>
                                                                <asp:Label ID="lblMov1" runat="server" Text='<%# Bind("Pag_Movimiento") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="120px" />
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridTemplateColumn DataField="Serie" HeaderText="Serie" UniqueName="Serie">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerie" runat="server" Text='<%# Bind("Serie") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Pag_Referencia" HeaderText="Ref." UniqueName="Pag_Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReferencia" runat="server" Text='<%# Bind("Doc_Referencia") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Fac_FolioFiscal" HeaderText="Folio Fiscal" UniqueName="Fac_FolioFiscal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblFolioFiscal" runat="server" Text='<%# Bind("Fac_FolioFiscal") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="260px" />
                                                        </telerik:GridTemplateColumn>

                                                         <telerik:GridTemplateColumn DataField="Pag_Importe" HeaderText="Importe" UniqueName="Pag_Importe">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblImportePag" runat="server" Text='<%# Bind("Pag_Importe", "{0:N2}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Cdi" HeaderText="Cdi" UniqueName="Cdi">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCdi" runat="server" Text='<%# Bind("Cdi") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Terr" HeaderText="Terr." UniqueName="v">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTerr" runat="server" Text='<%# Bind("Id_Terr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                       
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Pag_Fecha" HeaderText="Fecha" UniqueName="Pag_Fecha">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Doc_Fecha","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="110px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCte" runat="server" Text='<%# Bind("Id_Cte") %>'></asp:Label>
                                                            </ItemTemplate>
                                                          
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Cte_Nombre" HeaderText="Cliente" UniqueName="Cte_Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCteNombre" runat="server" Text='<%# Bind("Cte_Nombre") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Width="180px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Cte_Rfc" HeaderText="RFC" UniqueName="Cte_Rfc2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCteRfc" runat="server" Text='<%# Bind("Cte_Rfc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Width="105px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Pag_Numero" HeaderText="Núm." UniqueName="Pag_Numero">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("Pag_Numero") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Pag_Cheque" HeaderText="Cheque" UniqueName="Pag_Cheque">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCheque" runat="server" Text='<%# Bind("Pag_Cheque") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridTemplateColumn>
                                                       
                                                        <telerik:GridTemplateColumn DataField="Doc_Estatus" HeaderText="Estatus" UniqueName="Doc_Estatus"
                                                            Display="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstatus" runat="server" Text='<%# Bind("Doc_Estatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                         
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridTemplateColumn DataField="Doc_Importe" HeaderText="Importe" UniqueName="Doc_Importe"
                                                            Display="false">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblImporteDoc" runat="server" Text='<%# Bind("Doc_Importe") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Doc_Pagado" HeaderText="Pagado" UniqueName="Doc_Pagado"
                                                            Display="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPagado" runat="server" Text='<%# Bind("Doc_Pagado") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>
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
                                                <%--    <ClientSettings>
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
                        
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
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
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenRebind" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_Timbrado" runat="server" />
        <asp:HiddenField ID="HiddenHeight" runat="server" />
        <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

            function Ficha_Load(sender, args) {
                //debugger;
                var valor = sender.get_value();
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
                        if (mov.get_value() == '') {
                            radTabStrip.get_allTabs()[0].select();
                            continuarAccion = false;
                        }
                        else {
                            radTabStrip.get_allTabs()[0].select();

                        }
                        if (Page_ClientValidate()) {
                            button.set_enabled(false);
                        }
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            function txt_OnBlur(sender, args) {

            }

            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
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
                return txtFecha._dateInput;
            }
            function ValidarFichas(sender, args) {
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

            }
            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }

            function abrirArchivoCN(pagina, paginaCN) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, 'XML', opciones);
                window.open(paginaCN, 'XML Cuenta Nacional', opciones);
            }
            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }
            function AbrirFacturaPDFVarias(WebURL) {
                window.open(WebURL, "_blank");
            }
            function AbrirVentana_EnviarDocumentos(Id_Emp, Id_Cd, Id_Fac) {
                var oWnd = radopen("Ventana_EnviarDocumentos.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Doc=" + Id_Fac
                    + "&Tipo=FACTURA"
                    , "AbrirVentana_EnviarDocumentos");
                oWnd.center();
            }

            function AbrirVentana_EnviarPagos(Id_Emp, Id_Cd, Id_Cte, Id_Pag, Id_Fac, Id_PagDet, serie) {
                var oWnd = radopen("Ventana_EnviarPagos.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Cte=" + Id_Cte
                    + "&Id_Pag=" + Id_Pag
                    + "&Id_Fac=" + Id_Fac
                    + "&Id_PagDet=" + Id_PagDet
                    + "&Serie=" + serie
                    + "&Tipo=PAGO"
                    , "AbrirVentana_EnviarPagos");
                oWnd.center();
            }
            function CheckAllTimbrar(sender) {
                //debugger;
                var grid = $find('<%=RgDet.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;
                var importeTotal = 0;
                var importe;
                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    var chk = row.findElement("ChkTimbrar");
                    var lblImporte = row.findElement("lblImportePag");
                    if (chk != null) {
                        chk.checked = sender.checked;
                        if (chk.checked) {
                            importe = lblImporte.outerText.replace(',', '');
                            importeTotal = parseFloat(importeTotal) + parseFloat(importe);
                        }
                    }
                }
                document.getElementById('ctl00_CPH_txtImporte_text').value = importeTotal.toFixed(2);
            }

            function CheckTimbrar(sender) {
                //debugger;
                var grid = $find('<%=RgDet.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;
                var importeTotal = 0;
                var importe;
                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    var chk = row.findElement("ChkTimbrar");
                    var lblImporte = row.findElement("lblImportePag");
                    if (chk != null) {
                        if (chk.checked) {
                            importe = lblImporte.outerText.replace(',', '');
                            importeTotal = parseFloat(importeTotal) + parseFloat(importe);
                        }
                    }
                }
                document.getElementById('ctl00_CPH_txtImporte_text').value = importeTotal.toFixed(2);
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
