<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" CodeBehind="CapGastoViajeRegistro.aspx.cs" Inherits="SIANWEB.CapGastoViajeRegistro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAsignacion" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbDesasingar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbProgramaReparto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbAsignar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbPickingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImbBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

    <div id="divPrincipal" runat="server">
        <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
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
        </telerik:radtoolbar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; border:1px solid #ccc">
            <tr>
                <td><asp:Label ID="LblSolicitanteViajero" runat="server" Text="Persona que Viaja"></asp:Label></td>
                <td colspan="2"><telerik:RadTextbox ID="TxtSolicitanteViajero" runat="server" width="200px" ReadOnly="true"></telerik:RadTextbox></td>
            </tr>
            <tr>
                <td><asp:Label ID="LblMotivo" runat="server" Text="Motivo del Viaje"></asp:Label></td>
                <td><telerik:RadTextbox ID="TxtMotivo" runat="server" ReadOnly="true"></telerik:RadTextbox></td>
                <td><asp:Label ID="LblFechaSalida" runat="server" Text="Fecha Salida"></asp:Label></td>
                <td><telerik:RadDatePicker ID="TxtFechaSalida" runat="server" Culture="es-MX" Width="100px" AutoPostBack="true" ReadOnly="true"></telerik:RadDatePicker></td>
                <td><asp:Label ID="LblFechaRegreso" runat="server" Text="Fecha Regreso"></asp:Label></td>
                <td><telerik:RadDatePicker ID="TxtFechaRegreso" runat="server" Culture="es-MX" Width="100px" AutoPostBack="true" ReadOnly="true"></telerik:RadDatePicker></td>
                <td><asp:Label ID="LblCantidadDias" runat="server" Text="Dias de Viaje"></asp:Label></td>
                <td><telerik:RadNumericTextBox ID="TxtCantidadDias" runat="server" ReadOnly="true"><NumberFormat DecimalDigits="0" GroupSeparator="" /></telerik:RadNumericTextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="LblImporteSolicitado" runat="server" Text="Importe Solicitado"></asp:Label></td>
                <td><telerik:RadNumericTextbox ID="TxtImporteSolicitado" runat="server" ReadOnly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:RadNumericTextbox></td>
                <td><asp:Label ID="LblImporteComprobado" runat="server" Text="Importe Comprobado"></asp:Label></td>
                <td><telerik:RadNumericTextbox ID="TxtImporteComprobado" runat="server" ReadOnly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:RadNumericTextbox></td>
                <td><asp:Label ID="LblSaldoFavor" runat="server" Text="A Mi Favor"></asp:Label></td>
                <td><telerik:RadNumericTextbox ID="TxtSaldoFavor" runat="server" ReadOnly="true"><NumberFormat DecimalDigits="2" GroupSeparator="" /></telerik:RadNumericTextbox></td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgPagoElectronico" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="rgPagoElectronico_NeedDataSource" OnItemCommand="rgPagoElectronico_ItemCommand"
                    PageSize="10" AllowPaging="false" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_GVComprobante" HeaderText="Núm. Solicitud" UniqueName="Id_GVComprobante" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_ConComprobanteDescripcion" HeaderText="Con/Sin" UniqueName="GVComprobante_ConComprobanteDescripcion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                           <%--//JFCV  la columna de descripción ya no se va a mostrar en el grid <telerik:GridBoundColumn DataField="GVComprobanteTipo_Descripcion" HeaderText="Concepto" UniqueName="GVComprobanteTipo_Descripcion"><HeaderStyle Width="180" /></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="GVComprobante_Importe" HeaderText="Importe" UniqueName="GVComprobante_Importe" DataFormatString="{0:N2}"><HeaderStyle Width="55" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_Fecha" HeaderText="Fecha" UniqueName="GVComprobante_Fecha" DataFormatString="{0:dd/MM/yy}"><HeaderStyle Width="55" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVComprobante_Observaciones" HeaderText="Observaciones" UniqueName="GVComprobante_Observaciones"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
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
<%--                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Desea eliminar el comprobante?" Text="Cancelar" UniqueName="DeleteColumn" ConfirmDialogHeight="150px"
                                ConfirmDialogWidth="350px">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                            </telerik:GridButtonColumn>
--%>                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
        <script type="text/javascript">

            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }
            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }

        </script>
    </telerik:radcodeblock>

</asp:Content>
