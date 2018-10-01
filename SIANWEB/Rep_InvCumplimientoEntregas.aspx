<%@ Page Title="Cumplimiento de entregas" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_InvCumplimientoEntregas.aspx.cs" Inherits="SIANWEB.Rep_InvCumplimientoEntregas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;

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
                    var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                    var alerta = radalert(mensage, 330, 150, tituloMensajes);

                    alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                    return false
                }


                var txtFolio1 = $find('<%= txtFolio1.ClientID %>');
                var txtFolio2 = $find('<%= txtFolio2.ClientID %>');


                var FolioInicio = 0;
                if (txtFolio1.get_textBoxValue() != '') {
                    FolioInicio = parseFloat(txtFolio1.get_textBoxValue());
                }

                var FolioFin = 0;
                if (txtFolio2.get_textBoxValue() != '') {
                    FolioFin = parseFloat(txtFolio2.get_textBoxValue());
                }

                if (FolioInicio > 0 && FolioFin > 0 && (FolioInicio > FolioFin)) {
                    var alertaCli = radalert('La orden de compra inicial no debe ser mayor a la orden de compra final', 600, 10, tituloMensajes);
                    alertaCli.add_close(
                    function () {
                        txtFolio1.focus();
                    });
                    return false;
                }

                return true;
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
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':
                        continuarAccion = ValidacionesEspeciales();
                        break;
                }

                args.set_cancel(!continuarAccion);
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="filtros" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" />
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" ValidationGroup="print"
                    CssClass="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="print" />
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
            <table border="0" style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table border="0">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Proveedor"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="cmbProveedor" runat="server" Width="170px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="-- Seleccionar --" Value="-- Seleccionar --" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Compras locales" Value="Compras locales" />
                                            <telerik:RadComboBoxItem Text="Proveedores de convenio" Value="Proveedores de convenio" />
                                            <telerik:RadComboBoxItem Text="Cedis" Value="Sian" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ID="val_cmbProveedor" ControlToValidate="cmbProveedor"
                                        ErrorMessage="*Requerido" InitialValue="-- Seleccionar --" ValidationGroup="print"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
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
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFecha1"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFecha2"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label51" runat="server" Text="Ord. compra inicial"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtFolio1" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFolio1"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="Label41" runat="server" Text="Ord. compra final"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtFolio2" runat="server" Width="70px" MinValue="1"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFolio2"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="print">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProd" runat="server" Text="Producto"></asp:Label>
                                </td>
                                <td colspan="4">
                                    <telerik:RadTextBox ID="txtProducto" onpaste="return false" runat="server" Width="350px"
                                        MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkDetalle" runat="server" Checked="false" Text="Detalle" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
    </div>
</asp:Content>
