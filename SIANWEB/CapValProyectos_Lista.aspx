<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapValProyectos_Lista.aspx.cs" Inherits="SIANWEB.CapValProyectos_Lista" %>

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

                //obtener controles de formulario de inserción/edición de Grid
                var txtCliente1 = $find('<%= txtCliente1.ClientID %>');
                var txtCliente2 = $find('<%= txtCliente2.ClientID %>');
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');

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
                    var alerta = radalert(mensage, 400, 10, tituloMensajes);
                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
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
                        AbrirVentana_ValProyecto(0, 0, 0, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de nota de credito, cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_ValProyecto_Edicion(Id_Emp, Id_Cd, Id_Vap_Editar, modificable) {
                AbrirVentana_ValProyecto(Id_Emp, Id_Cd, Id_Vap_Editar, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, modificable);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de valuacion de proyectos
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_ValProyecto(Id_Emp, Id_Cd, Id_Vap, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir, modificable) {
                //debugger;
                var oWnd = radopen("CapValProyectos.aspx?Id_Vap=" + Id_Vap
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    + "&modificable=" + modificable
                    , "AbrirVentana_ValProyecto");
                oWnd.center();
                oWnd.Maximize();
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de parametros de resntabilidad
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_CentroDisParamsRentabilidad(Id_Emp, Id_Cd, Id_Vap_Editar, Id_Cte, Cte_NomComercial) {
                AbrirVentana_CentroDisParamsRentabilidad_Open(Id_Emp, Id_Cd, Id_Vap_Editar, Id_Cte, Cte_NomComercial, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir);
            }

            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición de valuacion de proyectos
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana_CentroDisParamsRentabilidad_Open(Id_Emp, Id_Cd, Id_Vap, Id_Cte, Cte_NomComercial, permisoGuardar, permisoModificar, permisoEliminar, permisoImprimir) {
                //debugger;
                var oWnd = radopen("CatCentroDisParamsValuacion.aspx?Id_Vap=" + Id_Vap
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Emp=" + Id_Emp
                    + "&Id_Cte=" + Id_Cte
                    + "&Cte_NomComercial=" + Cte_NomComercial
                    + "&permisoGuardar=" + permisoGuardar
                    + "&permisoModificar=" + permisoModificar
                    + "&permisoEliminar=" + permisoEliminar
                    + "&permisoImprimir=" + permisoImprimir
                    , "AbrirVentana_CentroDistParametrosRentabilidad");
                oWnd.center();
                oWnd.Maximize();
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
            function refreshGrid_Vap(accion) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(accion);
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

            //cuando se selecciona un Item del combo
            function cmbCliente1_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente1.ClientID %>'));
            }

            //cuando el campo de texto pirde el foco
            function txtCliente2_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbCliente2_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente2.ClientID %>'));
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rgBase" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="rgBase" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="formulario" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rgBase" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBase" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgBase">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBase" LoadingPanelID="RadAjaxLoadingPanel1" />
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
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="divPrincipal" runat="server">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <div id="formulario" runat="server">
                        <table>
                            <tr>
                                <td width="115">
                                    &nbsp;
                                </td>
                                <td colspan="1" width="70">
                                    &nbsp;
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td width="150">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="label1" runat="server" Text="Nombre del cliente"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <telerik:RadTextBox onpaste="return false" ID="txtNombreCliente" runat="server" Width="460px"
                                        MaxLength="70">
                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td width="115">
                                    <asp:Label ID="Label7" runat="server" Text="Cliente inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtCliente1_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="33">
                                    &nbsp;
                                </td>
                                <td width="70">
                                    <asp:Label ID="Label3" runat="server" Text="Cliente final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtCliente2_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td width="115">
                                    <asp:Label ID="Label5" runat="server" Text="Fecha inicial"></asp:Label>
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
                                <td width="5">
                                    &nbsp;
                                </td>
                                <td width="70">
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
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td width="115">
                                    <asp:Label ID="Label8" runat="server" Text="Estatus"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEstatus" runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="-- Todos --" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Análisis" Value="1" />
                                            <telerik:RadComboBoxItem runat="server" Text="Autorizada" Value="2" />
                                            <telerik:RadComboBoxItem runat="server" Text="Parcialmente Autorizada" Value="4" />
                                            <telerik:RadComboBoxItem runat="server" Text="Rechazada" Value="3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="5">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click"
                                        ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgBase" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                    AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                                    OnNeedDataSource="rgBase_NeedDataSource" OnPageIndexChanged="rgBase_PageIndexChanged"
                                    OnItemCommand="rgBase_ItemCommand" OnItemDataBound="rgBase_ItemDataBound">
                                    <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                                        SortToolTip="Click para reordenar" />
                                    <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Lista" HideStructureColumns="true"
                                        ExportOnlyData="true">
                                        <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Lista" Title="Lista" />
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="Id_Emp,Id_Cd,Id_Vap" CommandItemDisplay="none" PageSize="15">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <CommandItemSettings ExportToPdfText="Exportar a Pdf" AddNewRecordText="Agregar">
                                        </CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Vap" HeaderText="Clave" UniqueName="Id_Vap">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vap_Fecha" HeaderText="Fecha" UniqueName="Vap_Fecha"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_U" HeaderText="Id_U" Display="false" UniqueName="Id_U">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vap_Usuario" HeaderText="Usuario" Display="true"
                                                UniqueName="Vap_Usuario">
                                                <HeaderStyle Width="180px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Cliente" UniqueName="Id_Cte">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Nombre" UniqueName="Cte_NomComercial">
                                                <HeaderStyle Width="400px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vap_Estatus" HeaderText="Estatus" Display="true"
                                                UniqueName="Vap_Estatus">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vap_Nota" HeaderText="Vap_Nota" Display="false"
                                                UniqueName="Vap_Nota">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                                UniqueName="Editar" ItemStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                ItemStyle-Width="35px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                        CssClass="edit" ToolTip="Editar" CommandName="Modificar" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>

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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" Value="" />
    </div>
</asp:Content>
