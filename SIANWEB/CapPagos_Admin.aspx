<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="CapPagos_Admin.aspx.cs" Inherits="SIANWEB.CapPagos_Lista" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="rgPago" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rtb1">
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
                    <telerik:AjaxUpdatedControl ControlID="rgPago" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgPago">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPago" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="divPrincipal" runat="server">
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
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="newExt" Value="newExt" ToolTip="Pago extemporaneo"
                    CssClass="new" ImageUrl="~/Imagenes/blank.png" />
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
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="80">
                            </td>
                            <td width="100">
                            </td>
                            <td width="10">
                            </td>
                            <td width="80">
                            </td>
                            <td width="100">
                            </td>
                            <td width="10">
                            </td>
                            <td width="45">
                            </td>
                            <td width="45">
                            </td>
                            <td width="100">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha inicial" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha1" runat="server" Culture="es-MX" Width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Fecha final" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha2" runat="server" Culture="es-MX" Width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Estatus" />
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbEstatus" runat="server" Width="150px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Pago inicial" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPedido1" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Pago final" />
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPedido2" runat="server" Width="70px" MinValue="1"
                                    MaxLength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label10" runat="server" Text="Extemporáneo" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbExtemporaneo" runat="server" Width="100px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="-- Todos --" Value="-1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Si" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="No" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                    ToolTip="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rgPago" runat="server" AutoGenerateColumns="False" GridLines="None"
                        EnableLinqExpressions="False" PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnNeedDataSource="rg_NeedDataSource" OnPageIndexChanged="rg_PageIndexChanged"
                        OnItemCommand="rg_ItemCommand" OnItemDataBound="rgPago_ItemDataBound">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="Id_Pag">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Id_Pag" HeaderText="Folio" UniqueName="Id_Pag">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_PagExt" HeaderText="Folio ejecutor" UniqueName="Id_PagExt">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_CdOrigenStr" HeaderText="Centro ejecutor"
                                    UniqueName="Id_Cd">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_U" HeaderText="Usuario" UniqueName="Id_U">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Filtro_usuario" HeaderText="Nombre" UniqueName="U_Nombre">
                                    <HeaderStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pag_TipoStr" HeaderText="Tipo" UniqueName="Pag_TipoStr">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pag_Estatus" HeaderText="Estatus" UniqueName="Pag_Estatus"
                                    Display="false">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pag_EstatusStr" HeaderText="Estatus" UniqueName="Pag_EstatusStr">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pag_Extemporaneo" HeaderText="Extemporaneo" UniqueName="Pag_Extemporaneo"
                                    Display="false">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pag_ExtemporaneoStr" HeaderText="Extemporaneo"
                                    Display="false" UniqueName="Pag_ExtemporaneoStr">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pag_Fecha" HeaderText="Fecha" UniqueName="Pag_Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Pag_Total" HeaderText="Total" UniqueName="Pag_Total"
                                    DataFormatString="{0:N2}">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ejecutor" HeaderText="Ejecutor" UniqueName="Ejecutor"
                                    Display="false">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Editar" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Editar" CommandName="Editar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="Baja" HeaderText="Baja" ConfirmDialogType="RadWindow"
                                    ConfirmText="¿Está seguro de dar de baja el pago <b>#[[ID]]</b>?</br></br>" ConfirmDialogHeight="150px"
                                    ConfirmDialogWidth="350px" Text="Baja" UniqueName="Baja" Visible="True" ButtonType="ImageButton"
                                    ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn CommandName="Imprimir" HeaderText="Imprimir" ConfirmDialogType="RadWindow"
                                    ConfirmText="Se imprimirá el pago, tenga listo el formato en la impresora</br></br>"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Imprimir" UniqueName="Imprimir"
                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="imprimir">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </telerik:GridButtonColumn>
                                <telerik:GridButtonColumn CommandName="Timbre" HeaderText="Complemento de Pago" ConfirmDialogType="RadWindow"
                                    ConfirmDialogHeight="150px" ConfirmDialogWidth="350px" Text="Timbre" UniqueName="Timbre"
                                    Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                </telerik:GridButtonColumn>

                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_PED" runat="server" Value="" />
        <asp:HiddenField ID="HF_EXT" runat="server" Value="" />
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {

                    case 'newExt':
                        var HF = document.getElementById('<%= HF_EXT.ClientID %>');
                        if (HF.value != '1') {
                            radalert('Ya se ha efectuado el cierre extemporáneo de pagos, no es posible capturar pagos extemporáneos', 330, 150);
                            args.set_cancel(true);
                        }

                        break;
                }
            }

            function AbrirVentana_Pagos_Timbre(Id, notaGuardar, notaModificable) {
                AbrirVentana_Pagos_Timbre(Id, notaModificable, notaModificable);
                return false;
            }
            function AbrirVentana_Pagos_Edicion(Id, notaGuardar, notaModificable) {
                AbrirVentana_Pagos(Id, notaModificable, notaModificable);
                return false;
            }
            function AbrirVentana_PagosExp_Edicion(Id, notaGuardar, notaModificable) {
                AbrirVentana_PagosExt(Id, notaModificable, notaModificable);
                return false;
            }
            function AbrirVentana_Pagos_Timbre(Id, notaGuardar, notaModificable) {
                //debugger;
                var oWnd = radopen("CapPagosTimbres.aspx?Id=" + Id + "&PermisoGuardar=" + notaGuardar + "&PermisoModificar=" + notaModificable + "&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_Pago");
                oWnd.center();
                oWnd.Maximize();
            }
            function AbrirVentana_Pagos(Id, notaGuardar, notaModificable) {
                //debugger;
                var oWnd = radopen("CapPagos.aspx?Id=" + Id + "&PermisoGuardar=" + notaGuardar + "&PermisoModificar=" + notaModificable + "&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_Pago");
                oWnd.center();
                oWnd.Maximize();
            }
            function AbrirVentana_PagosExt(Id, notaGuardar, notaModificable) {
                //debugger;
                var oWnd = radopen("CapPagos.aspx?Ext=true&Id=" + Id + "&PermisoGuardar=" + notaGuardar + "&PermisoModificar=" + notaModificable + "&PermisoEliminar=<%= _PermisoEliminar %>&PermisoImprimir=<%= _PermisoImprimir %>", "AbrirVentana_Pago");
                oWnd.center();
                oWnd.Maximize();
            }
            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function OpenAlert(mensaje, Id_Emp, Id_Cd, Id_Ncr_Editar, notaGuardar, notaModificable) {

                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_Pagos(Id_Ncr_Editar, notaGuardar, notaModificable);
                    });
            }
            function OpenAlertExp(mensaje, Id_Emp, Id_Cd, Id_Ncr_Editar, notaGuardar, notaModificable) {

                var abrirWindow = radalert(mensaje, 330, 150);
                abrirWindow.add_close(
                    function () {
                        AbrirVentana_PagosExt(Id_Ncr_Editar, notaGuardar, notaModificable);
                    });
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
