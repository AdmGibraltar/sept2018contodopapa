<%@ Page Title="Registro de pedidos" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_FacRegistroPedidos.aspx.cs" Inherits="SIANWEB.Rep_FacRegistro" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function refreshGrid() {

                }

                //Validaciones especiales
                function ValidacionesEspeciales(sender, args) {
                    //debugger;

                    var AA = true;

                    //obtener controles de formulario de inserión/edición de Grid
                    var datePickerFechaInicio = $find('<%= RadDatePicker1.ClientID %>');
                    var datePickerFechaFin = $find('<%= RadDatePicker2.ClientID %>');

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
                        AA = false
                    }

                    args.set_cancel(!AA);
                }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="CmbCentro">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="100%" />
                        <telerik:AjaxUpdatedControl ControlID="CmbCentro" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div runat="server" id="divPrincipal">
            <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick"
                OnClientButtonClicking="ValidacionesEspeciales">
                <Items>
                    <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                        ImageUrl="~/Imagenes/blank.png" />
                    <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
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
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged"
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
                                <td width="10">
                                </td>
                                <td width="60">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td width="10">
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="120px">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="RadDatePicker1" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Width="120px">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy" />
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                            <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                        </DateInput>
                                        <DatePopupButton ImageUrl="" HoverImageUrl="" ToolTip="Abrir el calendario"></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="RadDatePicker2" Display="Dynamic" ErrorMessage="*Requerido" 
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblPedidosA" runat="server" Text="Pedidos a "></asp:Label>
                                </td>
                                <td colspan="5">
                                    <telerik:RadComboBox ID="cmbPedidos" runat="server" Width="130px">
                                        <Items>
                                            <%--
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbPedidos" />
                                        <telerik:RadComboBoxItem runat="server" Text="Facturar" Value="Facturar" 
                                            Owner="cmbPedidos" />
                                        <telerik:RadComboBoxItem runat="server" Text="Remisionar" Value="Remisionar" 
                                            Owner="cmbPedidos" />--%>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus "></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                        <asp:ListItem Value="">Todos</asp:ListItem>
                                        <%--<asp:ListItem Value="A">Asignado</asp:ListItem>--%>
                                        <%--<asp:ListItem Value="U">Autorizado</asp:ListItem>--%>
                                        <%--<asp:ListItem Value="D">Baja por administración</asp:ListItem>--%>
                                        <asp:ListItem Value="B">Baja por cliente</asp:ListItem>
                                        <%--<asp:ListItem Value="C">Capturado</asp:ListItem>--%>
                                        <%--<asp:ListItem Value="O">Confirmado</asp:ListItem>--%>
                                        <asp:ListItem Value="E">Embarque</asp:ListItem>
                                        <%--<asp:ListItem Value="N">Entregado</asp:ListItem>--%>
                                        <asp:ListItem Value="F">Facturado</asp:ListItem>
                                        <%--<asp:ListItem Value="X">Facturado/Remisionado</asp:ListItem>--%>
                                        <%--<asp:ListItem Value="I">Impreso</asp:ListItem>--%>
                                        <asp:ListItem Value="R">Remisionado</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
</asp:Content>
