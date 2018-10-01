<%@ Page Title="Reclamaciones" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="CapReclamaciones_Lista.aspx.cs" Inherits="SIANWEB.CapReclamaciones_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //debugger;
            // ---------------------
            // Variables de permiso
            // ---------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'new':
                        AbrirVentana(-1,'<%= _PermisoGuardar %>','<%= _PermisoModificar %>');
                        args.set_cancel(true);
                        break;
                }
            }
            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                //obtener controles de formulario de inserión/edición de Grid
                var txtCliente1 = $find('<%= txtCliente1.ClientID %>');
                var txtCliente2 = $find('<%= txtCliente2.ClientID %>');
                var datePickerFechaInicio = $find('<%= dpFecha1.ClientID %>');
                var datePickerFechaFin = $find('<%= dpFecha2.ClientID %>');
                var txtRec1 = $find('<%= txtRec1.ClientID %>');
                var txtRec2 = $find('<%= txtRec2.ClientID %>');

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

                var recInicio = 0;
                if (txtRec1.get_textBoxValue() != '') {
                    recInicio = parseFloat(txtRec1.get_textBoxValue());
                }

                var recFin = 0;
                if (txtRec2.get_textBoxValue() != '') {
                    recFin = parseFloat(txtRec2.get_textBoxValue());
                }

                if (recInicio > 0 && recFin > 0 && (recInicio > recFin)) {
                    var alertaMsg = radalert('La reclamación inicial no debe ser mayor a la reclamación final', 330, 150, tituloMensajes);
                    alertaMsg.add_close(
                    function () {
                        txtRec1.focus();
                    });
                    return false;
                }
                return true;
            }

            function AbrirVentana_Edicion(Id, guardar, modificar) {
                //debugger;
                AbrirVentana(Id, guardar, modificar);
                return false;
            }

            function AbrirVentana(Id, guardar, modificar) {
                //debugger;
                var oWnd = radopen("CapReclamaciones.aspx?Id=" + Id
                  + "&PermisoGuardar=" + guardar + "&PermisoModificar=" + modificar
                  + "&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>"
                  , "AbrirVentana_Reclamaciones");
                oWnd.center();
                oWnd.Maximize();
            }

            function OpenAlert(mensaje, Id_Ncr_Editar, notaGuardar, notaModificable) {
                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana(Id_Ncr_Editar, notaGuardar, notaModificable);
                    });
            }            
            
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function LimpiarBanderaRebind() {
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
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro"  />
                    <telerik:AjaxUpdatedControl ControlID="rgReclamaciones" 
                    LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="rgReclamaciones" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="rgReclamaciones" 
                    LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgReclamaciones" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgReclamaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="rgReclamaciones" LoadingPanelID="RadAjaxLoadingPanel1"
                       />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton Enabled="false" Visible="False" CommandName="print" Value="print"
                    CssClass="print" ToolTip="Imprimir" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton Enabled="false" Visible="False" CommandName="delete" Value="delete"
                    CssClass="delete" ToolTip="Cancelación" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
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
                    &nbsp;
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
                            <td width="10">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="10">
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
                                Nombre de cliente
                            </td>
                            <td colspan="7">
                                <telerik:RadTextBox ID="txtNombre" runat="server" Width="300px">
                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="HD_GridRebind" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cliente inicial
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente1" runat="server" Width="125px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Cliente final
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCliente2" runat="server" Width="125px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
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
                                Fecha inicial
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="155px">
                                    <DatePopupButton ToolTip="Abrir calendario" />
                                    <Calendar ID="caldpFecha1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Fecha final
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="155px">
                                    <DatePopupButton ToolTip="Abrir calendario" />
                                    <Calendar ID="caldpFecha2" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Estatus
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Sort="Ascending">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Reclamación inicial
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtRec1" runat="server" Width="125px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Reclamación final
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtRec2" runat="server" Width="125px" MaxLength="9"
                                    MinValue="1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Tipo
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Sort="Ascending">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" OnClientClick="return ValidacionesEspeciales()" />
                            </td>
                        </tr>
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="aspPanel1" runat="server" Width="1000px">
                        <telerik:RadGrid ID="rgReclamaciones" runat="server" AutoGenerateColumns="False"
                            GridLines="None" PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            AllowPaging="True" AllowSorting="False" HeaderStyle-HorizontalAlign="Center"
                            OnNeedDataSource="rgReclamaciones_NeedDataSource" OnPageIndexChanged="rg_PageIndexChanged"
                            EnableLinqExpressions="False" OnItemCommand="rg_ItemCommand" OnItemDataBound="rgReclamaciones_ItemDataBound">
                            <MasterTableView ClientDataKeyNames="Id_Rec">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" Display="false" UniqueName="Id_Cd">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Rec" HeaderText="Número" UniqueName="Id_Rec">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cte_NomComercial" HeaderText="Cliente" UniqueName="Cte_NomComercial">
                                        <HeaderStyle Width="300px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Rec_Fecha" HeaderText="Capturado" UniqueName="Rec_Fecha"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Rec_FecAccion" HeaderText="Acción" UniqueName="Rec_FecAccion"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Rec_FecConformidad" HeaderText="Conformidad"
                                        UniqueName="Rec_FecConformidad" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Estatus" HeaderText="Estatus" UniqueName="Estatus">
                                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Nco_Descripcion" HeaderText="No conformidad"
                                        UniqueName="Nco_Descripcion">
                                        <HeaderStyle Width="200" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Rec_Estatus" HeaderText="Rec_Estatus" Display="false"
                                        UniqueName="Rec_Estatus">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Editar" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                        ConfirmText="Se imprimirá la reclamación, tenga listo el formato en la impresora</br></br>"
                                        ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                        Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" Value="" />
    </div>
</asp:Content>
