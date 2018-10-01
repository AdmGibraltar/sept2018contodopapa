<%@ Page Title="Rotación de inventarios" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_InvRotacionInventario.aspx.cs" Inherits="SIANWEB.Rep_InvRotacionInventario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Imprimir" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="Imprimir" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
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
                            <td width="70">
                            </td>
                            <td valign="middle" width="101">
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
                                &nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Width="200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cmbTipo_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbTipo" />
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbTipo" Text="General" Value="General" />
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbTipo" Text="Papel " Value="Papel " />
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbTipo" Text="Producto " Value="Producto " />
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbTipo" Text="Producto " Value="Producto " />
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbTipo" Text="Sistemas propietarios "
                                            Value="Sistemas propietarios " />
                                        <telerik:RadComboBoxItem runat="server" Owner="cmbTipo" Text="Producto en consignación "
                                            Value="Producto en consignación " />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbTipo"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" InitialValue="-- Seleccionar --"
                                    ValidationGroup="Imprimir"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFecha" runat="server" Width="100px" Culture="es-MX"
                                    Enabled="False">
                                    <Calendar ID="Calendar1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton ToolTip="Abrir el calendario" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dpFecha"
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Imprimir"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="RowProducto" visible="false">
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Producto"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtProducto" runat="server">
                                    <ClientEvents OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" />
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="RowCliente" visible="false">
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtCliente" runat="server">
                                    <ClientEvents OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" />
                                </telerik:RadTextBox>
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
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDetalle" runat="server" Text="Detalle" Visible="false" />
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
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">



            function refreshGrid()
            { }


            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();

                switch (button.get_value()) {
                    case 'print':

                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);


                        break;
                }

                args.set_cancel(!continuarAccion);
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
