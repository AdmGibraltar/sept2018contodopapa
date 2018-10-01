<%@ Page Title="Relación de facturas enviadas a revisión o cobro" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapFacturaSvtas_Lista.aspx.cs" Inherits="SIANWEB.CapFacturaSvtas_Lista" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            // --------------------- 
            // Variables de permiso
            // ---------------------
            var permisoGuardar = '<%= PermisoGuardar %>'
            var permisoModificar = '<%= PermisoModificar %>'
            var permisoEliminar = '<%= PermisoEliminar %>'
            var permisoImprimir = '<%= PermisoImprimir %>'
         

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;

                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');
                var txtFolio1 = $find('<%= txtFolio1.ClientID %>');
                var txtFolio2 = $find('<%= txtFolio2.ClientID %>');

                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;

                fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                //validar rango correcto de fechas.
                if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                    var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                    var alerta = radalert(mensage, 330, 150, tituloMensajes);
                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }

                //valida rango Folio
                var notaInicio = 0;
                if (txtFolio1.get_textBoxValue() != '') {
                    notaInicio = parseFloat(txtFolio1.get_textBoxValue());
                }

                var notaFin = 0;
                if (txtFolio2.get_textBoxValue() != '') {
                    notaFin = parseFloat(txtFolio2.get_textBoxValue());
                }

                if (notaInicio > 0 && notaFin > 0 && (notaInicio > notaFin)) {
                    var alertaMsg = radalert('El folio inicial no debe ser mayor al folio final', 330, 150, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtFolio1.focus();
                    });
                    return false;
                }

                return true;
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                var ProcSvtasAlm ='<%= ProcSvtasAlm %>'
                if (ProcSvtasAlm == 'True') {
                    switch (button.get_value()) {
                        case 'new':
                            continuarAccion = false;
                            AbrirVentana_FacturaRevision(0, 0, 0, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                            break;
                    }
                }
                else {
                    var alertaMsg = radalert('El proceso no esta activado para el CDI', 330, 150, tituloMensajes);
                    args.set_cancel(!continuarAccion);
                }
               
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de nota de credito, cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_FacturaSvtaAlmacen_Edicion(Id_Emp, Id_Cd, Id_Fva_Editar, estatus) {
                AbrirVentana_FacturaRevision(Id_Emp, Id_Cd, Id_Fva_Editar, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, estatus);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de nota de credito
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_FacturaRevision(Id_Emp, Id_Cd, Id_Fva, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, estatus) {
                //debugger;
                var oWnd = radopen("CapFacturaSvtas.aspx?Id_Fva=" + Id_Fva
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    + "&Estatus=" + estatus
                    , "AbrirVentana_FacturaRevision");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.maximize();
                oWnd.center();
            }

            //********************************
            //refrescar grid
            //********************************
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid_Frc(accion) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(accion);
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                //debugger;
//                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
//                if (HD_GridRebind.value == '1') {
//                    refreshGrid_Frc('RebindGrid');
//                }
            }

            function LimpiarBanderaRebind(sender, eventArgs) {
                //debugger;
                ModificaBanderaRebind('0');
            }

            function ActivarBanderaRebind() {
                //debugger;
                ModificaBanderaRebind('1');
            }

            function ModificaBanderaRebind(valor) {
                //debugger;
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                HD_GridRebind.value = valor;
            }

            //cuando el campo de texto pierde el foco
            function txtCliente_OnBlur(sender, args) {
                
            }

            //cuando se selecciona un Item del combo
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaSvtaAlmacen" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="formulario" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaSvtaAlmacen" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgFacturaSvtaAlmacen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFacturaSvtaAlmacen" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:RadToolBar>
    <table id="TblEncabezado" runat="server" width="99%" style="font-family: verdana;
        font-size: 8pt">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
            </td>
            <td style="text-align: right" width="150px">
                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
            </td>
            <td width="150px" style="font-weight: bold">
                <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
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
                <div id="formulario" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadDatePicker ID="txtFecha1" runat="server" Width="100px">
                                    <DatePopupButton ToolTip="Abrir calendario" />
                                    <Calendar ID="calTxtFecha1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha2" runat="server" Width="100px">
                                    <DatePopupButton ToolTip="Abrir calendario" />
                                    <Calendar ID="calTxtFecha2" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:Label ID="Label41" runat="server" Text="Estatus"></asp:Label>&nbsp;&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="120px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Impreso" Value="I" />
                                        <telerik:RadComboBoxItem Text="Capturado" Value="C" />
                                        <telerik:RadComboBoxItem Text="Baja" Value="B" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEtiqueta" runat="server" Text="Folio inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFolio1" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="25">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Folio final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtFolio2" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click"
                                    ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        </table>
                </div>
                <div style="margin-top: 10px">
                    <telerik:RadGrid ID="rgFacturaSvtaAlmacen" runat="server" AutoGenerateColumns="False" GridLines="None"
                        PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                        OnNeedDataSource="rgFacturaSvtaAlmacen_NeedDataSource" OnPageIndexChanged="rgFacturaSvtaAlmacen_PageIndexChanged"
                        OnItemCommand="rgFacturaSvtaAlmacen_ItemCommand" OnItemDataBound="rgFacturaSvtaAlmacen_ItemDataBound">
                        <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                            SortToolTip="Clic para reordenar" />
                        <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Lista_Facturas_Svta_Almacen"
                            HideStructureColumns="true" ExportOnlyData="true">
                            <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Lista_Facturas_Svta" Title="Lista_Facturas_Svta" />
                        </ExportSettings>
                        <MasterTableView DataKeyNames="Id_Emp,Id_Cd,Id_Fva" ClientDataKeyNames="Id_Fva" CommandItemDisplay="none"
                            DataMember="listNotaCredito" PageSize="10">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <CommandItemSettings ShowExportToPdfButton="true" ExportToPdfText="Exportar a Pdf"
                                ShowExportToExcelButton="true" ExportToExcelText="Exportar a Excel" ShowExportToWordButton="true"
                                ExportToWordText="Exportar a Word" ShowExportToCsvButton="false" ExportToCsvText="Exportar a Csv"
                                AddNewRecordText="Agregar"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Fva" HeaderText="Folio" UniqueName="Id_Fva">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fva_Estatus" HeaderText="Fva_Estatus" UniqueName="Fva_Estatus"
                                    Display="false">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fva_EstatusStr" HeaderText="Estatus" UniqueName="Fva_EstatusStr">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fva_Fecha" HeaderText="Fecha" UniqueName="Fva_Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fva_Entrego" HeaderText="Entregó" UniqueName="Fva_Entrego">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fva_Recibio" HeaderText="Recibió" UniqueName="Fva_Recibio">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                    UniqueName="Editar" ItemStyle-HorizontalAlign="Center" AllowFiltering="false"
                                    ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="¿Está seguro de dar de baja la relación de facturas enviadas de Servicio de Ventas a Almacen?"
                                    Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                    ButtonCssClass="baja">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="Se imprimirá la relación de facturas enviadas de Servicio de Ventas a Almacen, tenga listo el formato en la impresora"
                                    Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                    ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                            PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                            PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HF_PED" runat="server" Value="" />
    <asp:HiddenField ID="HF_ClvPag" runat="server" Value="" />
</asp:Content>
