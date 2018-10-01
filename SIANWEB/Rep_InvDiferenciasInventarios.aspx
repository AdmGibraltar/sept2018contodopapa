<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master"
    AutoEventWireup="true" CodeBehind="Rep_InvDiferenciasInventarios.aspx.cs" Inherits="SIANWEB.Rep_InvDiferenciasInventarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //Validaciones especiales
            function ValidacionesEspeciales() {
                //debugger;
                var conntinuar = true;

                return conntinuar;
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

                        var txtProducto = $find("<%= txtProducto.ClientID %>");
                        continuarAccion = RangoEnterosSeparacionGuionYComas_OnBlur(txtProducto);

                        if (continuarAccion == true) {
                            continuarAccion = ValidacionesEspeciales();
                        }
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
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="CmbCentro" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="filtros" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Imprimir" CssClass="print"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ValidationGroup="print" ImageUrl="~/Imagenes/blank.png" />
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
                        <asp:Label ID="lblProd" runat="server" Text="Producto"></asp:Label>
                    </td>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtProducto" onpaste="return false" runat="server" Width="350px"
                                        MaxLength="1000">
                                        <ClientEvents OnKeyPress="RangoEnterosSeparacionGuionYComas_OnKeyPress" OnBlur="RangoEnterosSeparacionGuionYComas_OnBlur" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Inventario"></asp:Label>
                    </td>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cmbInventario" runat="server" Width="130px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="-- Todos --" Value="-1" />
                                            <telerik:RadComboBoxItem runat="server" Text="Faltante" Value="FALTANTE" />
                                            <telerik:RadComboBoxItem runat="server" Text="Sobrante" Value="SOBRANTE" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Tipo de precio"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rbTipoPrecio" runat="server">
                            <asp:ListItem Text="AAA" Value="P_AAA" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Lista" Value="P_LISTA"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>
</asp:Content>
