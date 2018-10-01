<%@ Page Title="Poliza Contable de Amortización" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepPolizaContableAmortizacion.aspx.cs" Inherits="SIANWEB.RepPolizaContableAmortizacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" 
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
            <telerik:AjaxSetting AjaxControlID="rbRik">
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
                                <asp:Label ID="Label3" runat="server" Text="Orden por"></asp:Label>
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
                                            <asp:RadioButton ID="rbCdi" runat="server" Text="CDI" GroupName="por" AutoPostBack="true"
                                                OnCheckedChanged="rbCdi_CheckedChanged" Checked="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbRik" runat="server" Text="RIK" GroupName="por" AutoPostBack="true"
                                                OnCheckedChanged="rbRik_CheckedChanged"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbTerritorio" runat="server" Text="Territorio" GroupName="por" AutoPostBack="true"
                                                OnCheckedChanged="rbTerritorio_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbCliente" runat="server" Text="Cliente" AutoPostBack="true"
                                                GroupName="por" OnCheckedChanged="rbCliente_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td width="60">
                                            <asp:Label ID="Label1" runat="server" Text="Mes"></asp:Label>
                                        </td>
                                        <td width="130">
                                            <telerik:RadComboBox ID="cmbMes" runat="server" Width="130px" MaxHeight="250px">
                                    <Items>
                                          <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                          <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                          <telerik:RadComboBoxItem Text="Marzo" Value="3"/>
                                          <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                          <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                          <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                          <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                          <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                          <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                          <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                          <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                          <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                    </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="100">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                ErrorMessage="*Requerido" ForeColor="Red" SetFocusOnError="True" ControlToValidate="cmbMes"
                                                ValidationGroup="Mostrar"></asp:RequiredFieldValidator>
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
                        <tr runat="server" id="Nivel">
                            <td colspan="2">
                                <asp:Label ID="LblNivel" runat="server" Text="Nivel"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr runat="server" id="Nivel_RIK">
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="cbRIK" runat="server" Text="RIK" />
                            </td>
                        </tr>
                        <tr runat="server" id="Nivel_Territorio">
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="cbTerritorio" runat="server" Text="Territorio" />
                            </td>
                        </tr>
                        <tr runat="server" id="Nivel_Cliente">
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="cbCliente" runat="server" Text="Cliente" />
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
