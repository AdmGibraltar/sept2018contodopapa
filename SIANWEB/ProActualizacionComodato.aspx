<%@ Page Title="Actualización de contrato de comodato" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="ProActualizacionComodato.aspx.cs" Inherits="SIANWEB.Pro_ActualizacionComodato" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //Validaciones especiales
            function ValidacionesEspeciales(sender, args) {
                //debugger;
                //obtener controles de formulario de inserión/edición de Grid
                var datePickerFechaInicio = $find('<%= txtFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= txtFecha2.ClientID %>');
                //realizar validaciones
                var fechaInicio = null;
                var fechaFin = null;

                if (datePickerFechaInicio != null && datePickerFechaFin != null) {
                    fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                    fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                    //validar rango correcto de fechas.
                    if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                        var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                        var alerta = radalert(mensage, 330, 150, tituloMensajes);

                        alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                        return false
                    }
                }
                var isPageValid = Page_ClientValidate('buscar');
                return isPageValid;
            }

            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                var continuarAccion = true;
                switch (button.get_value()) {
                    case 'print':
                        AbrirVentana_Contrato();
                        continuarAccion = false;
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            //********************************
            //refrescar grid
            //********************************
            function CerrarWindow() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            //cuando el campo de texto pirde el foco
            function txtCliente_OnBlur(sender, args) {
            }

            //cuando se selecciona un Item del combo
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }
            //cuando el campo de texto pirde el foco
            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }

            //cuando se selecciona un Item del combo
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('ImprimirContrato');
            }
            function AbrirVentana_Contrato() {
                var oWnd = radopen("ProActualizacionComodato_Ventana.aspx", "AbrirVentana_Contrato");
                oWnd.center();
            }
            function popup() {
                var oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                oWnd.center();
            }
            function ClienteSeleccionado(param) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(param);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBase" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
        OnClientButtonClicking="ToolBar_ClientClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
            <telerik:RadToolBarButton runat="server" CommandName="print" Value="print" CssClass="print"
                ToolTip="Imprimir" ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton runat="server" CommandName="save" Value="save" ToolTip="Guardar"
                CssClass="save" ImageUrl="~/Imagenes/blank.png" />
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
    <div runat="server" id="divPrincipal">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <div id="filtros" runat="server">
                        <table>
                            <tr>
                                <td width="125">
                                </td>
                                <td width="70">
                                    &nbsp;
                                </td>
                                <td width="15">
                                    &nbsp;
                                </td>
                                <td width="30">
                                </td>
                                <td width="70">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="125">
                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="350px" ReadOnly="True">
                                    </telerik:RadTextBox>
                                    <%--    <telerik:RadNumericTextBox ID="txtCliente" runat="server" Width="70px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnBlur="txtCliente_OnBlur" OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbCliente" runat="server" Width="350px" Filter="Contains"
                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" LoadingMessage="Cargando..."
                                    HighlightTemplatedItems="true" DataTextField="Descripcion" DataValueField="Id"
                                    OnClientSelectedIndexChanged="cmbCliente_ClientSelectedIndexChanged" OnClientBlur="Combo_ClientBlur"
                                    MaxHeight="250px">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 50px; text-align: right">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                </td>
                                                <td style="width: 250px; text-align: left">
                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>--%>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="val_txtCliente" runat="server" ControlToValidate="txtCliente"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="buscar"> </asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="imgAceptar_Click"
                                        ToolTip="Buscar" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTerritorio" runat="server" Text="Territorio"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" Width="70px" MaxLength="9"
                                        MinValue="1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnBlur="txtTerritorio_OnBlur" OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbTerritorio" runat="server" Width="350px" Filter="Contains"
                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" LoadingMessage="Cargando..."
                                        HighlightTemplatedItems="true" OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged"
                                        OnClientBlur="Combo_ClientBlur" MaxHeight="250px">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 50px; text-align: right">
                                                        <%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? string.Empty : DataBinder.Eval(Container.DataItem, "Id").ToString() %>
                                                    </td>
                                                    <td style="width: 250px; text-align: left">
                                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbltxtFecha1" runat="server" Text="Fecha de venc. inicio"></asp:Label>
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
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbltxtFecha2" runat="server" Text="Fecha de venc. final"></asp:Label>
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
                                <td colspan="2">
                                    <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="btnBuscar_Click"
                                        ValidationGroup="buscar" ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
                                </td>
                            </tr>
                            <tr>
                                <td width="125">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp; &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%-- <asp:Panel ID="aspPanel1" runat="server" ScrollBars="Horizontal" Width="950px">--%>
                    <telerik:RadGrid ID="rgBase" runat="server" AutoGenerateColumns="False" GridLines="None"
                        PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                        AllowMultiRowSelection="True" OnNeedDataSource="rgBase_NeedDataSource" OnPageIndexChanged="rgBase_PageIndexChanged"
                        OnDetailTableDataBind="rgBase_DetailTableDataBind">
                        <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                            SortToolTip="Click para reordenar" />
                        <MasterTableView Name="Master" DataKeyNames="Id_Emp,Id_Cd,Id_Cco,Id_Rem,Rem_Estatus, Id_Prd"
                            CommandItemDisplay="none" PageSize="10">
                            <%-- <DetailTables>
                                    <telerik:GridTableView runat="server" AllowFilteringByColumn="False" Name="DetalleCco"
                                        AllowSorting="False" DataKeyNames="Id_Prd" DataMember="listaContratoComDet" Width="360px">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Prd" HeaderText="Clave" UniqueName="Id_Prd">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Prd_Descripcion" HeaderText="Descripción" UniqueName="Prd_Descripcion">
                                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Rem_Cant" HeaderText="Cantidad" UniqueName="Rem_Cant">
                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                        </EditFormSettings>
                                    </telerik:GridTableView>
                                </DetailTables>--%>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridClientSelectColumn UniqueName="selectColumn">
                                    <ItemStyle Width="30px" HorizontalAlign="Right" />
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Cco" HeaderText="Clave" Display="false" UniqueName="Id_Cco">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn DataField="Cco_Fecha" HeaderText="Fecha" UniqueName="Cco_Fecha"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="center" />
                                    </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Cte" UniqueName="Id_Cte"
                                    Display="false">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCliente" runat="server" Text='<%# Bind("Id_Cte") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Cliente" DataField="Cte_NomComercial" UniqueName="Cte_NomComercial"
                                    Display="false">
                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblClienteNombre" runat="server" Text='<%# Bind("Cte_NomComercial") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Ter" UniqueName="Id_Ter"
                                    Display="false">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritorioPartida" runat="server" Text='<%#  Bind("Id_Ter")  %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Territorio" DataField="Ter_Nombre" UniqueName="Ter_Nombre"
                                    Display="false">
                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerritorioPartidaNombre" runat="server" Text='<%#  Bind("Ter_Nombre")  %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Rik" UniqueName="Id_Rik"
                                    Display="false">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRepresentante" runat="server" Text='<%# Bind("Id_Rik") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Representante" DataField="Rik_Nombre" UniqueName="Rik_Nombre"
                                    Display="false">
                                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRepresentanteNombre" runat="server" Text='<%# Bind("Rik_Nombre") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Núm." DataField="Id_Rik" UniqueName="Id_Prd">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProducto" runat="server" Text='<%# Bind("Id_Prd") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Producto" DataField="Rik_Nombre" UniqueName="Prd_Descripcion">
                                    <HeaderStyle Width="400px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductoNombre" runat="server" Text='<%# Bind("Prd_Descripcion") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Cantidad en contrato" DataField="Contrato"
                                    UniqueName="Contrato">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContrato" runat="server" Text='<%# Bind("Contrato") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Saldo actual" DataField="Saldo" UniqueName="Saldo">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaldo" runat="server" Text='<%# Bind("Saldo") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Cantidad a renovar" DataField="Cantidad"
                                    UniqueName="Cantidad">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad") %>'>
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Fechaini" DataField="Fechaini" UniqueName="Fechaini"
                                    Display="false">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <telerik:RadDatePicker ID="rdpFechaIni" runat="server" DbSelectedDate='<%# Eval("Cco_FechaIni") %>'>
                                        </telerik:RadDatePicker>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="fechafin" DataField="fechafin" UniqueName="fechafin"
                                    Display="false">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <telerik:RadDatePicker ID="rdpFechaFin" runat="server" DbSelectedDate='<%# Eval("Cco_FechaFin") %>'>
                                        </telerik:RadDatePicker>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%-- <telerik:GridTemplateColumn HeaderText="Comodato" UniqueName="Cco_Comodato">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" ForeColor="#FF0000" />
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkComodato" runat="server" Checked="true" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Id_Rem" HeaderText="Remisión" UniqueName="Id_Rem">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Rem_Estatus" HeaderText="Estatus" Display="false"
                                        UniqueName="Rem_Estatus">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Rem_EstatusStr" HeaderText="Estatus" UniqueName="Rem_EstatusStr">
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>--%>
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
                    <%-- </asp:Panel>--%>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HF_ClvPag" runat="server" Value="" />
</asp:Content>
