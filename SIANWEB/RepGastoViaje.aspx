<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="RepGastoViaje.aspx.cs" Inherits="SIANWEB.RepGastoViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
<style>
        .RadInput input[readonly]
        {
            background-color: #F7F7F7 !important;
        }
        #PopUpBackground
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: gray;
            filter: alpha(opacity=50);
            opacity: 0.5;
            z-index: 100000;
        }
        #PopUpProgress
        {
            position: fixed;
            font-size: 120%;
            top: 40%;
            left: 40%;
            height: 20%;
            width: 20%;
            z-index: 100001;
            background-color: #FFFFFF;
            border: 1px solid Gray;
            background-image: url('images/loading1.gif');
            background-repeat: no-repeat;
            background-position: center;
            border-radius: .6em;
        }
}
    </style>
    <telerik:radajaxmanager id="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest" onajaxrequest="RAM1_AjaxRequest" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPago" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_ComprobacionGastos" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="940px" Height="645px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Captura Gastos" Modal="True" Localization-Maximize="Maximizar"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <%-- Factura (Impresion PDF) --%>
            <telerik:RadWindow ID="AbrirVentana_ImpresionPDFFactura" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="840px" Height="540px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Factura" Modal="True"
                OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_LstComprobantes" runat="server" Opacity="100"
                Behaviors="Move, Close, Maximize" VisibleStatusbar="False" Width="900px" Height="600px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Listado de Comprobantes"
                Modal="True" OnClientClose="refreshGrid" ReloadOnShow="true">
            </telerik:RadWindow>
                <telerik:RadWindow ID="AbrirVentana_GastosConsultar" runat="server" Opacity="100"
                Behaviors="Resize, Close,Move, Maximize" VisibleStatusbar="False" Width="1120px" Height="690px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Consultar Gastos" Modal="True" Localization-Maximize="Maximizar"
                OnClientClose="refreshGrid" ReloadOnShow="true" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>

                <td>
                    <asp:Label ID="LblId_Cd" runat="server" Text="CDI" Visible = "false"></asp:Label>
                </td>
                
                <td colspan="9">
                <telerik:RadComboBox ID="CmbId_Cd" runat="server" AutoPostBack="true" CausesValidation="False"
                    ChangeTextOnKeyBoardNavigation="true" DataTextField="Descripcion" DataValueField="Id"
                    EnableLoadOnDemand="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                    MarkFirstMatch="true" MaxHeight="200px" Width="300px" Visible = "false">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width: 25px; text-align: center; vertical-align: top">
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
            </td>
                <td>
                    <asp:Label ID="lblid" runat="server" Text="Núm. Solicitud"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtidPagoElectronico" runat="server" Width="70px">
                    </telerik:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Núm. Gasto de Viaje" ></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtidGastoViaje" runat="server" Width="50px" ToolTip="Teclee el número del gasto de Viaje a Buscar">
                    </telerik:RadTextBox>
                </td>

                <td>
                    <asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="CmbTipo" runat="server" AutoPostBack="True">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="-1" />
                        </Items>
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Solicitud de Cheque" Value="1" />
                        </Items>
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Reposicion de Caja" Value="2" />
                        </Items>
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Gastos de Viaje" Value="3" />
                        </Items>
                    </telerik:RadComboBox>
                </td>


                <td><asp:Label ID="LblEstatus" runat="server" Text="Estatus"></asp:Label></td>
                <td>
                    <telerik:RadCombobox ID="CmbEstatus" runat="server">
                        <Items><telerik:RadComboBoxItem runat="server" Text="-- Seleccionar --" Value="0" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Por Comprobar" Value="1" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Solicitado" Value="2" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Autorizado" Value="3" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Rechazado" Value="4" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Cancelado" Value="5" /></Items>
                        <Items><telerik:RadComboBoxItem runat="server" Text="Comprobado" Value="6" /></Items>
                    </telerik:RadCombobox>
                </td>
                <td><asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" /></td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgPago" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="rgPago_NeedDataSource" OnItemCommand="rgPago_ItemCommand" OnPageIndexChanged="rgPago_PageIndexChanged"
                    PageSize="20" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros.">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" UniqueName="Id_Emp" Display="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="CD" UniqueName="Id_Cd" Display="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_PagElec" HeaderText="Núm. Solicitud" UniqueName="Id_PagElec"><HeaderStyle Width="100" /></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Id_GV" HeaderText="Núm. Gasto Viaje" UniqueName="Id_GV"><HeaderStyle Width="100" /></telerik:GridBoundColumn>
                           <%-- Id_PagElec--%>
                            <telerik:GridBoundColumn DataField="Id_GVEst" HeaderText="Tipo" UniqueName="Id_GVEst" Display="false"></telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="PagElecTipo_Descripcion" HeaderText="Tipo Comprobación" UniqueName="PagElecTipo_Descripcion"><HeaderStyle Width="100" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GVEst_Descripcion" HeaderText="Estatus" UniqueName="GVEst_Descripcion"><HeaderStyle Width="120" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Acr_Nombre" HeaderText="Acreedor" UniqueName="Acr_Nombre"><HeaderStyle Width="130" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Acr_NumeroGenerado" HeaderText="Cta. Pago" UniqueName="Acr_NumeroGenerado"><HeaderStyle Width="60" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_FechaElaboracion" HeaderText="Fecha Elaboración" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaElaboracion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Fecha_Comprobacion" HeaderText="Fecha Comprobación" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_Fecha_Comprobacion"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="GV_Saldo_Comprobar" HeaderText="Saldo" DataFormatString="{0:C}" UniqueName="GV_Saldo_Comprobar" ItemStyle-HorizontalAlign="Right" ><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn HeaderText="Comprobantes" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px" UniqueName="Comprobantes">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgComprobantes" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Comprobantes PDF y XML" CommandName="Comprobantes" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>

<%--                            <telerik:GridBoundColumn DataField="GV_Solicitante" HeaderText="Solicitante" UniqueName="GV_Solicitante"><HeaderStyle Width="150" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_Motivo" HeaderText="Motivo" UniqueName="GV_Motivo"><HeaderStyle Width="150" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_FechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaSalida"><HeaderStyle Width="80" /></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GV_FechaRegreso" HeaderText="Fecha Regreso" DataFormatString="{0:dd/MM/yy}" UniqueName="GV_FechaRegreso"><HeaderStyle Width="80" /></telerik:GridBoundColumn>--%>
                             <telerik:GridTemplateColumn HeaderText="Consultar" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/Imagenes/view.png"
                                         ToolTip="Consultar" CommandName="Consultar"    />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="35px" />
                                <HeaderStyle Width="35px" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                        PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                        ShowPagerText="True" PageButtonCount="3" />
                </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function AbrirVentana_ComprobacionGastos(id) {
                var oWnd = radopen("CapGastosViajeComprobacion.aspx?Id=" + id, "AbrirVentana_ComprobacionGastos");
                oWnd.center();
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

            //JFCV abrir pantalla de Gastos de Viaje listado 
            function AbrirVentana_LstComprobantesGV(Id, Id_Emp, Id_Cd) {
                var oWnd = radopen("CapGastosViajeComprobantes_Listado.aspx?Id=" + Id + "&Id_Emp=" + Id_Emp + "&Id_Cd=" + Id_Cd, "AbrirVentana_LstComprobantes");
                oWnd.center();
            }

            function AbrirVentana_GastosConsultar(id) {
                var oWnd = radopen("CapPagosElectronicosConsultar.aspx?Id=" + id, "AbrirVentana_GastosConsultar");
                oWnd.center();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
