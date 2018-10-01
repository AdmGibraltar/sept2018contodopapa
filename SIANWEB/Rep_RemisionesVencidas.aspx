<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_RemisionesVencidas.aspx.cs" Inherits="SIANWEB.Rep_RemisionesVencidas" %>

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
                        var txtCliente = $find("<%= txtClientes.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
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
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="Mostrar" />
            </Items>
        </telerik:RadToolBar>  <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
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
                            <td>
                                <asp:Label ID="LblClientes" runat="server" Text="Clientes"></asp:Label>
                            </td>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txtClientes" runat="server" MaxLength="19" 
                                    onpaste="return false" Width="300px">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblFechaini" runat="server" Text="Fecha inicial"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechaini" runat="server" Width="155px">
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
                            <td>
                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="dpFechaini" ValidationGroup="Mostrar"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="LblFechafin" runat="server" Text="Fecha final"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dpFechafin" runat="server" Width="155px">
                                    <Calendar ID="Calendar2" runat="server">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput2" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="dpFechafin" ValidationGroup="Mostrar"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblTipoR" runat="server" Text="Territorios"></asp:Label>
                            </td>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txtTerritorio" runat="server" MaxLength="19" 
                                    onpaste="return false" Width="300px">
                                    <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblEstatus" runat="server" Text="Estatus"></asp:Label>
                            </td>
                            <td colspan="5">
                                <telerik:RadComboBox ID="cmbTipo" runat="server" Width="150px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Selected="True" Text="-- Todos --" 
                                            Value="-1" />
                                        <telerik:RadComboBoxItem runat="server" Text="No vencidos" Value="0" />
                                        <telerik:RadComboBoxItem runat="server" Text="Vencidos" Value="1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        </table>
                    <table>
                        <tr>
                            <td style="width: 150px">
                                <asp:HiddenField ID="HF_ClvPag" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
