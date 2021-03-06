﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapTransferenciaAlmacen_Lista.aspx.cs" Inherits="SIANWEB.CapTransferenciaAlmacen_Lista" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" eventname="RadAjaxManager1_AjaxRequest"
        OnAjaxRequest="RAM1_AjaxRequest" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1" >
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
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgTransferenciaAlmacen" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgTransferenciaAlmacen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgTransferenciaAlmacen" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div class="formulario" id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton Enabled="false" CommandName="mail" Value="mail" CssClass="mail"
                    ToolTip="Correo" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton Enabled="false" CommandName="print" Value="print" CssClass="print"
                    ToolTip="Imprimir" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton Enabled="false" CommandName="delete" Value="delete" CssClass="delete"
                    ToolTip="Eliminar" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton Enabled="false" CommandName="undo" Value="undo" CssClass="undo"
                    ToolTip="Regresar" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton Enabled="false" CommandName="save" Value="save" ToolTip="Guardar"
                    CssClass="save" ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
               
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" runat="server" width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px"  runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
                        Width="150px" AutoPostBack="True">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    Folio inicial
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="txtFolio1" runat="server" Width="70px" MinValue="1" MaxLength="9">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        <ClientEvents OnKeyPress="handleClickEvent" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                </td>
                <td>
                    Folio final
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="txtFolio2" runat="server" Width="70px" MinValue="1" MaxLength="9">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        <ClientEvents OnKeyPress="handleClickEvent" />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Fecha inicial
                </td>
                <td>
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
                </td>
                <td>
                    Fecha final
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
                </td>
                <td>
                    Estatus
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbEstatus" runat="server" ChangeTextOnKeyBoardNavigation="true"
                        MarkFirstMatch="false" AllowCustomText="false" LoadingMessage="Cargando...">
                        <Items>
                            <telerik:RadComboBoxItem Text="-- Todos --" Value="-1" />
                            <telerik:RadComboBoxItem Text="Capturado" Value="Capturado" />
                            <telerik:RadComboBoxItem Text="Baja" Value="Baja" />
                            <telerik:RadComboBoxItem Text="Impreso" Value="Impreso" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click"
                        OnClientClick="return ValidacionesEspeciales()" ToolTip="Buscar" />
                </td>
            </tr>
            <tr>
                <td>
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
        </table>     
        <table style="font-family: Verdana; font-size: 8pt">
            <!-- Tabla principal--->
            <tr>
                <td>
                </td>
                <td>
                 
                    <telerik:RadGrid ID="rgTransferenciaAlmacen" runat="server" AutoGenerateColumns="False" GridLines="None"
                        PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                        OnNeedDataSource="rgTransferenciaAlmacen_NeedDataSource" OnPageIndexChanged="rgTransferenciaAlmacen_PageIndexChanged"
                        OnItemCreated="rgTransferenciaAlmacen_ItemCreated" OnItemCommand="rgTransferenciaAlmacen_ItemCommand">
                        <SortingSettings SortedAscToolTip="Trasnferencia acendente" SortedDescToolTip="Trasnferencia decendente"
                            SortToolTip="Click para reordenar" />
                        <MasterTableView DataKeyNames="Id_Emp,Id_Cd,Id_Trans" ClientDataKeyNames="Id_Trans" DataMember="listTrasnferenciaAlmacen">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                         
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Id_Emp" UniqueName="Id_Emp" DataField="Id_Emp"
                                    Display="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Id_Cd" UniqueName="Id_Cd" DataField="Id_Cd"
                                    Display="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Folio" UniqueName="Id_Trans" DataField="Id_Trans">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Fecha" UniqueName="Trans_Fecha" DataField="Trans_Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ID Usuario Creador" UniqueName="Id_UOrigen" DataField="Id_UOrigen"  Display="true">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Usuario Creador" UniqueName="U_NombreOrigen" DataField="U_NombreOrigen">                                    
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Num Remisión origen" UniqueName="Id_RemOrigen" DataField="Id_RemOrigen">                                    
                                </telerik:GridBoundColumn>
                               
                                 <telerik:GridBoundColumn HeaderText="CDI" UniqueName="Id_CdOrigen" DataField="Id_CdOrigen">                                    
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="CDI Nombre" UniqueName="Id_CdOrigenStr" DataField="Id_CdOrigenStr"
                                    AutoPostBackOnFilter="true">
                                </telerik:GridBoundColumn>          
                                <telerik:GridBoundColumn HeaderText="Estatus" UniqueName="Trans_EstatusStr" DataField="Trans_EstatusStr"
                                    AutoPostBackOnFilter="true">
                                </telerik:GridBoundColumn>                                
                                <telerik:GridTemplateColumn HeaderText="Ver" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Descargar" HeaderText="Descargar" ConfirmDialogType="RadWindow"
                                    ConfirmText="¿Está seguro de dar de baja la orden de compra?" ConfirmDialogHeight="150px"
                                    ConfirmDialogWidth="350px" Text="Baja" UniqueName="Eliminar" Visible="True" ButtonType="ImageButton"
                                    ImageUrl="~/Img/descargar.png">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                    ConfirmText="Se imprimirá la orden de compra, tenga listo el formato en la impresora</br></br>"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="70px"></HeaderStyle>
                                </telerik:GridButtonColumn>                               
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" 
                                NextPageToolTip="Siguiente página" 
                                PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                PrevPageToolTip="Página anterior" 
                                PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>                 
                </td>
            </tr>
        </table>      
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            // ---------------------
            // Variables de permiso
            // ---------------------
            var permisoGuardar = '<%= PermisoGuardar %>'
            var permisoModificar = '<%= PermisoModificar %>'
            var permisoEliminar = '<%= PermisoEliminar %>'
            var permisoImprimir = '<%= PermisoImprimir %>'

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'new':
                        continuarAccion = false;
                        AbrirVentana_TransferenciaAlmacen(0, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            
            //Validaciones especiales
            function ValidacionesEspeciales() {
                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');
                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;
                fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                fechaFin = datePickerFechaFin._dateInput.get_selectedDate();
                //validar rango correcto de fechas.
                if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                    var mensage = 'La fecha inicial no debe ser mayor a la fecha final';
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }
                return true;
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando se edita una orden de compra haiendo click en el botón de edición de la columna del Grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_TransferenciaAlmacen_Edicion(index) {
                var grid = $find("<%= rgTransferenciaAlmacen.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var row = MasterTable.get_dataItems()[index];
                var Id_Trans = row.getDataKeyValue('Id_Trans');

                AbrirVentana_TransferenciaAlmacen(Id_Trans, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                return false;
            }
            function AbrirResultado(WebURL) {
                var oWnd = radopen(WebURL, "RWReporte");
                oWnd.center();
            }
            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de ordenes de compra
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_TransferenciaAlmacen(Id_Trans, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                //debugger;
                var oWnd = radopen("CapTransferenciaAlmacen.aspx?Id_Trans=" + Id_Trans
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    , "AbrirVentana_TransferenciaAlmacen");
                oWnd.center();
                oWnd.Maximize();
            }

            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //--------------------------------------------------------------------------------------------------
            // Se ejecuata cuando el radWindow del detalle de orden de compra se cierra,
            // Esta función es invocada por el evento 'radWindowClose'
            //--------------------------------------------------------------------------------------------------         

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
            function CerrarWindow_ClientEvent(sender, eventArgs) {
                //debugger;
                refreshGrid('RebindGrid');
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
