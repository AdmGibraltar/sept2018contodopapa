<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapEntSalAutorizacion_Lista.aspx.cs" Inherits="SIANWEB.CapEntSalAutorizacion_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators

                switch (button.get_value()) {
                    case 'new':
                        //debugger;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function OpenAlert(mensaje, Id) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana(Id, false, false, false, false, false, 0);
                    });
            }
            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //*******************************
            function AbrirVentana_Pagos_Edicion(Id, Es_Naturaleza) {
                AbrirVentana(Id, Es_Naturaleza);
                return false;
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                refreshGrid_Nca('RebindGrid');
            }

            function refreshGrid() {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function AbrirVentana(Id, Es_Naturaleza, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, TipoOp) {
                //debugger;
                var oWnd = radopen("CapEntSalAutorizacion.aspx?Id=" + Id + "&Es_Naturaleza=" + Es_Naturaleza + "&PermisoGuardar=" + permisoGuardar + "&PermisoModificar=" + permisoModificar + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir=" + permisoImprimir + "&TipoOp=" + TipoOp, "AbrirVentana_EntSal");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.maximize();
                oWnd.center();

            }

            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFRemision");
                oWnd.center();
            }

            function IniciarPaginasAuxiliares() {
                parametros = "ini=1";
                var urlArchivo = 'ObtenerNombreCliente.aspx';
                obtenerrequest(urlArchivo, parametros);

                urlArchivo = 'ObtenerProducto_ES.aspx';
                obtenerrequest(urlArchivo, parametros);

                urlArchivo = 'ObtenerReferencia.aspx';
                obtenerrequest(urlArchivo, parametros);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgEntSal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="Panel1" UpdatePanelHeight="" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
        OnClientButtonClicking="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                <asp:HiddenField ID="HF_ClvPag" runat="server" />
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label13" runat="server" Text="Centro de distribución" />
            </td>
            <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
                    Width="150px" AutoPostBack="True" LoadingMessage="Cargando..." OnClientBlur="Combo_ClientBlur">
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
    <div id="divPrincipal" runat="server">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td style="margin-left: 40px">
                    <table>
                        <tr>
                            <td width="120">
                            </td>
                            <td>
                            </td>
                            <td width="10">
                            </td>
                            <td>
                            </td>
                            <td width="50">
                            </td>
                            <td width="10">
                            </td>
                            <td width="80">
                            </td>
                            <td width="50">
                            </td>
                        </tr>
                
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Folio inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtIdESol_Ini" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Folio final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="TxtIdESol_Fin" runat="server" Width="125px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Estatus"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="130px" LoadingMessage="Cargando..."
                                    OnClientBlur="Combo_ClientBlur">
                                       <Items>
                                                 <telerik:RadComboBoxItem runat="server" Selected="True"  Text="-- Todos --" Value="" />
                                                 <telerik:RadComboBoxItem runat="server"  Text="Pendiente" Value="P" />
                                                 <telerik:RadComboBoxItem runat="server"  Text="Autorizado" Value="A" />
                                                 <telerik:RadComboBoxItem runat="server"  Text="Rechazado" Value="A" />
                                                 <telerik:RadComboBoxItem runat="server"  Text="Cancelado" Value="X" />
                                      </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="155px">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="155px">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar1" runat="server" ImageUrl="~/Img/find16.png" OnClick="Button1_Click"
                                    ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    
                    </table>
                    <br />
                    <asp:Panel ID="Panel1" runat="server"  Width="950px">
                        <telerik:RadGrid ID="rgEntSal" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnNeedDataSource="rgEntSal_NeedDataSource"  OnItemCommand="rgEntSal_ItemCommand"  OnPageIndexChanged="rgEntSal_PageIndexChanged">
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView>
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" UniqueName="Id_Cd"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="ESol_Unique" HeaderText="ESol_Unique" UniqueName="ESol_Unique"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_UEnviar" HeaderText="Id_UEnviar" UniqueName="Id_UEnviar"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="Id_UCC" HeaderText="Id_UCC" UniqueName="Id_UCC"
                                       Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_ESol" HeaderText="Folio" UniqueName="Id_ESol" >
                                     <ItemStyle HorizontalAlign="center" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="ESol_Fecha" HeaderText="Fecha" UniqueName="ESol_Fecha"
                                        DataFormatString="{0:dd/MM/yy}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Id_TmStr" HeaderText="Tipo Mov." UniqueName="Id_TmStr">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="ESol_EstatusStr" HeaderText="Estatus" UniqueName="ESol_EstatusStr">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="Id_UCreoStr" HeaderText="Solicita" UniqueName="Id_UCreoStr">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="ESol_Total" HeaderText="Importe" UniqueName="ESol_Total">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="Id_EsStr" HeaderText="Folio movimiento" UniqueName="Id_EsStr">
                                        <ItemStyle HorizontalAlign="center" />
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="Editar" HeaderText="Editar" Text="Editar"
                                        UniqueName="Editar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="edit" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Desea enviar la solicitud?" Text="Enviar" UniqueName="Enviar"
                                        Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </telerik:GridButtonColumn>
                                      <telerik:GridButtonColumn CommandName="Autorizar" HeaderText="Autorizar" Text="Autorizar"
                                        UniqueName="Autorizar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="edit" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn CommandName="Baja" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                        ConfirmText="¿Está seguro de cancelar la solicitud?" ConfirmTitle="" Text="Baja"
                                        UniqueName="Baja" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                        ButtonCssClass="baja" ConfirmDialogHeight="150px" ConfirmDialogWidth="350px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridButtonColumn>
               
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
