<%@ Page Title="Excesos de inventarios" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_InvExcesoInventario.aspx.cs" Inherits="SIANWEB.Rep_InvExcesoInventario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var button = args.get_item();
                switch (button.get_value()) {
                    case 'Mostrar':
                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        if (txtProducto != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function refreshGrid() {
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="Mostrar" ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="1000px">
                    <asp:Label ID="Label1" runat="server" Text="Centro de distribución"></asp:Label>
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
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblProducto" runat="server" Text="Producto" />
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtProducto" runat="server" MaxLength="19" onpaste="return false">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblTipo" runat="server" Text="Tipo" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Width="130px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblTipoP" runat="server" Text="Tipo proveedor" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbTipoProveedor" runat="server" Width="130px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Todos" Value="0" Owner="cmbTipoProveedor"
                                            Selected="true" />
                                        <telerik:RadComboBoxItem runat="server" Text="CYAN manufactura" Value="1" Owner="cmbTipoProveedor" />
                                        <telerik:RadComboBoxItem runat="server" Text="Compra local" Value="2" Owner="cmbTipoProveedor" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblMostrar" runat="server" Text="Mostrar" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbEstadistica" runat="server" Text="Estadística" GroupName="mostrar"
                                    OnCheckedChanged="rbEstadistica_CheckedChanged" AutoPostBack="true" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbFecha" runat="server" Text="Fecha" GroupName="mostrar" OnCheckedChanged="rbFecha_CheckedChanged"
                                    AutoPostBack="true" />
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFecha" runat="server" Width="155px" Enabled="false">
                                    <Calendar ID="Calendar1" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="LblExcesos" runat="server" Text="Excesos de inventario" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbInventario" runat="server" Text="Excesos de inventario " SkinID="excesos"
                                    GroupName="excesos" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbInventariorota" runat="server" Text="Excesos de inventario que rota "
                                    SkinID="excesos" GroupName="excesos" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbInventarionorota" runat="server" Text="Excesos de inventario que no rota "
                                    SkinID="excesos" GroupName="excesos" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>
