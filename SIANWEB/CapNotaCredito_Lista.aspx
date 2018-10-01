<%@ Page Title="Notas de crédito" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapNotaCredito_Lista.aspx.cs" Inherits="SIANWEB.CapNotaCredito_Lista" %>

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
                var txtCliente1 = $find('<%= txtCliente1.ClientID %>');
                var txtCliente2 = $find('<%= txtCliente2.ClientID %>');
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');
                var txtNotaCredito1 = $find('<%= txtNotaCredito1.ClientID %>');
                var txtNotaCredito2 = $find('<%= txtNotaCredito2.ClientID %>');

                var clienteInicio = 0;
                if (txtCliente1.get_textBoxValue() != '') {
                    clienteInicio = parseFloat(txtCliente1.get_textBoxValue());
                }

                var clienteFin = 0;
                if (txtCliente2.get_textBoxValue() != '') {
                    clienteFin = parseFloat(txtCliente2.get_textBoxValue());
                }

                if (clienteInicio > 0 && clienteFin > 0 && (clienteInicio > clienteFin)) {
                    var alertaCli = radalert('El cliente inicial no debe ser mayor al cliente final', 330, 150, tituloMensajes);
                    alertaCli.add_close(
                    function () {
                        txtCliente1.focus();
                    });
                    return false;
                }

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

                var notaInicio = 0;
                if (txtNotaCredito1.get_textBoxValue() != '') {
                    notaInicio = parseFloat(txtNotaCredito1.get_textBoxValue());
                }

                var notaFin = 0;
                if (txtNotaCredito2.get_textBoxValue() != '') {
                    notaFin = parseFloat(txtNotaCredito2.get_textBoxValue());
                }

                if (notaInicio > 0 && notaFin > 0 && (notaInicio > notaFin)) {
                    var alertaMsg = radalert('La nota de crédito inicial no debe ser mayor a la nota de crédito final', 330, 150, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtNotaCredito1.focus();
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

                switch (button.get_value()) {
                    case 'new':
                        continuarAccion = false;
                        AbrirVentana_NotaCredito(0, 0, 0,'0', '1', permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            function AbrirNotaCreditoPDF(WebURL) {
                var oWnd = window.radopen(WebURL, 'AbrirVentana_ImpresionPDFNotaCredito');
                oWnd.center();
            }

            function OpenAlert(mensaje, Id_Emp, Id_Cd, Id_Ncr_Editar, Id_NcrSerie,notaModificable) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_NotaCredito_Edicion(Id_Emp, Id_Cd, Id_Ncr_Editar,Id_NcrSerie, notaModificable);
                    });
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de nota de credito, cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_NotaCredito_Edicion(Id_Emp, Id_Cd, Id_Ncr_Editar,Id_NcrSerie, notaModificable) {
                AbrirVentana_NotaCredito(Id_Emp, Id_Cd, Id_Ncr_Editar,Id_NcrSerie, notaModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de nota de credito
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_NotaCredito(Id_Emp, Id_Cd, Id_Ncr,Id_NcrSerie, notaModificable, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                //debugger;
                var oWnd = radopen("CapNotaCredito.aspx?Id_Ncr=" + Id_Ncr
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&Id_NcrSerie=" + Id_NcrSerie
                    + "&notaModificable=" + notaModificable
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    , "AbrirVentana_NotaCredito");
                oWnd.center();
                oWnd.Maximize();
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToPdfButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                    args.set_enableAjax(false);
                }
                else {
                    args.set_enableAjax(true);
                }
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
            function refreshGrid_Ncr(accion) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(accion);
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de factura se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------
            function CerrarWindow_Event(sender, eventArgs) {               
                var HD_GridRebind = document.getElementById('<%= HD_GridRebind.ClientID %>');
                if (HD_GridRebind.value == '1') {
                    refreshGrid_Ncr('RebindGrid');
                }
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

            //cuando el campo de texto pirde el foco
            function txtCliente1_OnBlur(sender, args) {
            }
                      
            //cuando el campo de texto pirde el foco
            function txtCliente2_OnBlur(sender, args) {
            }
          
            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }


        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
    <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGrid" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgNotaCredito">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlGrid" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>  
    <div runat="server" id="divPrincipal">
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
        <div id="filtros" runat="server">
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table style="font-family: verdana; font-size: 8pt">
                            <tr>
                                <td width="110">
                                </td>
                                <td width="70">
                                </td>
                                <td width="25">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td width="100">
                                </td>
                                <td width="50">
                                </td>
                                <td width="150">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="label1" runat="server" Text="Nombre del cliente"></asp:Label>
                                </td>
                                <td colspan="6">
                                    <telerik:RadTextBox onpaste="return false" ID="txtNombreCliente" runat="server" Width="460px"
                                        MaxLength="70">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Cliente inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>                             
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Cliente final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>                             
                            </tr>
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
                                <td style="width: 200PX">
                                    <asp:Label ID="Label41" runat="server" Text="Estatus"></asp:Label>&nbsp;&nbsp;
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="120px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" />
                                            <telerik:RadComboBoxItem Text="Capturado" Value="C" />
                                            <telerik:RadComboBoxItem Text="Impreso" Value="I" />
                                            <telerik:RadComboBoxItem Text="Baja" Value="B" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEtiqueta" runat="server" Text="Nota inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtNotaCredito1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Nota final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtNotaCredito2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click"
                                        ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div style="margin: 0px 15px 0px 15px">
            <asp:Panel runat="server" ID="pnlGrid" ScrollBars="Horizontal" Width="950px">
                <telerik:RadGrid ID="rgNotaCredito" runat="server" AutoGenerateColumns="False" GridLines="None"
                    PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                    AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                    DataMember="listNotaCredito" OnNeedDataSource="rgNotaCredito_NeedDataSource"
                    OnPageIndexChanged="rgNotaCredito_PageIndexChanged" OnItemCommand="rgNotaCredito_ItemCommand"
                    OnItemDataBound="rgNotaCredito_ItemDataBound">
                    <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                        SortToolTip="Clic para reordenar" />
                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="ListaNotasCredito"
                        HideStructureColumns="true" ExportOnlyData="true">
                        <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Lista de notas de crédito" Title="Lista_Notas_Credito" />
                    </ExportSettings>
                    <MasterTableView DataKeyNames="Id_Emp,Id_Cd,Id_Ncr,Id_Tm" ClientDataKeyNames="Id_Ncr"
                        CommandItemDisplay="Top" DataMember="listNotaCredito" PageSize="10">
                        
                        <CommandItemSettings ShowExportToPdfButton="true" ExportToPdfText="Exportar a Pdf"
                            ShowExportToExcelButton="true" ExportToExcelText="Exportar a Excel" ShowExportToWordButton="true"
                            ExportToWordText="Exportar a Word" ShowExportToCsvButton="false" ExportToCsvText="Exportar a Csv"
                            AddNewRecordText="Agregar"></CommandItemSettings>
                        
                       <%-- <CommandItemTemplate>
                            <asp:ImageButton ID="ImgExportar" runat ="server" ImageUrl="Imagenes/icono_excel.png"
                            AlternateText="Exportar Notas de Credito" ToolTip="Exportar Excel" onclick="ImgExportar_Click" Width="24px" Height="24px">
                            
                            </asp:ImageButton>
                        </CommandItemTemplate>--%>

                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_NcrSerie" HeaderText="Id_NcrSerie" Display="false" UniqueName="Id_NcrSerie">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Ncr" HeaderText="Número" UniqueName="Id_Ncr">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Tm" HeaderText="Id_Tm" Display="false" UniqueName="Id_Tm">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_TipoStr" HeaderText="Tipo" UniqueName="Ncr_TipoStr">
                                <HeaderStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_EstatusStr" HeaderText="Estatus" UniqueName="Ncr_EstatusStr">
                                <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_Fecha" HeaderText="Fecha" UniqueName="Ncr_Fecha"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Cliente" UniqueName="Id_Cte">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Nombre" UniqueName="Cte_NomComercial">
                                <HeaderStyle Width="400px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_FolioFiscal" HeaderText="Folio Fiscal" UniqueName="Ncr_FolioFiscal">
                                <HeaderStyle Width="280px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_SubTotal" HeaderText="Subtotal" DataFormatString="{0:N2}"
                                UniqueName="Ncr_SubTotal">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_Iva" HeaderText="I.V.A." DataFormatString="{0:N2}"
                                UniqueName="Ncr_Iva">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_Total" HeaderText="Total" DataFormatString="{0:N2}"
                                UniqueName="Ncr_Total">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Ncr_Saldo" HeaderText="Saldo" DataFormatString="{0:N2}"
                                UniqueName="Ncr_Saldo" Display="false">
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                UniqueName="Editar" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridTemplateColumn>
                              <telerik:GridBoundColumn DataField="PDF" HeaderText="TienePDF" UniqueName="PDF"
                            Display="false"> 
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NcrXML" HeaderText="TieneXML" UniqueName="NcrXML"
                            Display="false"> 
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px" UniqueName="PDF">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                    CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"  UniqueName="NcrXML">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                    CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="Eliminar" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="¿Está seguro de dar de baja la nota de crédito?"
                                Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png"
                                ButtonCssClass="baja">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" ConfirmText="Se imprimirá la nota de crédito, tenga listo el formato en la impresora"
                                Text="Imprimir" UniqueName="Imprimir" Visible="True" ButtonType="ImageButton"
                                ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                        PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                        PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                        ShowPagerText="True" PageButtonCount="3" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
