<%@ Page Title="Estadistica de ventas" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="Rep_VenEstadisticaVentas.aspx.cs" Inherits="SIANWEB.Rep_VenEstadisticaVentas" %>

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
                        var txtTerritorio = $find("<%= txtTerritorio.ClientID %>");
                        if (txtTerritorio != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtTerritorio);
                        var txtCliente = $find("<%= txtCliente.ClientID %>");
                        if (txtCliente != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtCliente);
                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        if (txtProducto != null)
                            continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);
                        //Opcional, validaciones extras
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" ClientEvents-OnRequestStart="onRequestStart"
        onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
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
            <telerik:AjaxSetting AjaxControlID="rbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbProducto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAño">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="Mostrar" Value="Mostrar" ToolTip="Imprimir"
                    ValidationGroup="Mostrar" CssClass="print" ImageUrl="~/Imagenes/blank.png">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                        width="99%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMensaje" runat="server" />
                            </td>
                            <td style="text-align: right" width="1000px">
                                <asp:Label ID="Label2" runat="server" Text="Centro de distribución"></asp:Label>
                            </td>
                            <td width="150px" style="font-weight: bold">
                                <telerik:RadComboBox ID="CmbCentro" MaxHeight="250px" runat="server" OnSelectedIndexChanged="cmbCentrosDist_SelectedIndexChanged"
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
                                <asp:Label ID="Label3" runat="server" Text="Order por"></asp:Label>
                            </td>
                            <td width="100">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbCliente" runat="server" Text="Cliente" GroupName="por" AutoPostBack="true"
                                                OnCheckedChanged="rbCliente_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbProducto" runat="server" Text="Producto" GroupName="por" AutoPostBack="true"
                                                OnCheckedChanged="rbProducto_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbTerritorio" runat="server" Text="Territorio" AutoPostBack="true"
                                                GroupName="por" OnCheckedChanged="rbTerritorio_CheckedChanged" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbRepresentante" runat="server" Text="Representante" AutoPostBack="true"
                                                GroupName="por" OnCheckedChanged="rbRepresentante_CheckedChanged" Checked="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr runat="server" id="Filtro_Territorio">
                                        <td width="60">
                                            <asp:Label ID="LblTerritorio" runat="server" Text="Territorio"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtTerritorio" runat="server" MaxLength="100" onpaste="return false"
                                                Width="300px">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Filtro_Cliente" visible="false">
                                        <td width="60">
                                            <asp:Label ID="LblCliente" runat="server" Text="Cliente"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtCliente" runat="server" MaxLength="100" onpaste="return false"
                                                Width="300px">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Filtro_Producto">
                                        <td width="60">
                                            <asp:Label ID="LblProducto" runat="server" Text="Producto"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtProducto" runat="server" MaxLength="100" onpaste="return false"
                                                Width="300px">
                                                <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="60">
                                            <asp:Label ID="LblAnio" runat="server" Text="Año"></asp:Label>
                                        </td>
                                        <td width="130">
                                            <telerik:RadComboBox ID="cmbAño" runat="server" Width="130px" MaxHeight="250px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="100">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                ErrorMessage="*Requerido" ForeColor="Red" SetFocusOnError="True" ControlToValidate="cmbAño"
                                                ValidationGroup="Mostrar"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="TrMes">
                                        <td width="60">
                                            <asp:Label ID="LblAnio0" runat="server" Text="Mes"></asp:Label>
                                        </td>
                                        <td width="130">
                                            <telerik:RadComboBox ID="cmbMes" runat="server" Width="130px" MaxHeight="250px">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="-- Todos --" Value="0" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" Owner="cmbMes" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" Owner="cmbMes" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="100">
                                            &nbsp;
                                        </td>
                                        </tr>
                                        <tr runat="server" id="Semanal">
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <asp:CheckBox ID="ckbSemanal" runat="server" Text="Venta Semanal" AutoPostBack="true"
                                                OnCheckedChanged="CkbSemanal_CheckedChanged" />
                                            </td>
                                        </tr>
                                         <tr runat="server" id="Filtro_Años">
                                        <td width="60">
                                            <asp:Label ID="Label1" runat="server" Text="Años"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <telerik:radcombobox runat="server" ID= "cmbAnios" onpaste="return false"></telerik:radcombobox>
                                        </td>
                                    </tr>
                                    <tr  runat ="server" id="Mov80">
                                        <td></td>
                                        <td colspan="2">
                                                <asp:CheckBox ID="ChkMov80" runat="server" Text="Considerar ventas con remisiones de tipo Mov 80" AutoPostBack="true"
                                                OnCheckedChanged="ChkMov80_CheckedChanged" />
                                        </td>
                                    </tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblMostrar" runat="server" Text="Mostrar en"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbPesos" runat="server" Text="Pesos" Checked="true" GroupName="pesos" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbUnidades" runat="server" Text="Unidades" GroupName="pesos" />
                            </td>
                        </tr>
                        <tr runat="server" id="Ambos">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:RadioButton ID="rbAmbos" runat="server" Text="Ambos" GroupName="pesos" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr runat="server" id="Nivel">
                            <td colspan="2">
                                <asp:Label ID="LblNivel" runat="server" Text="Nivel"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr runat="server" id="Nivel_Cliente">
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="cbCliente" runat="server" Text="Cliente" />
                            </td>
                        </tr>
                        <tr runat="server" id="Nivel_Producto">
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="cbProducto" runat="server" Text="Producto" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HF_ClvPag" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
