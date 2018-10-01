<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CapPedidoCaptado.aspx.cs" Inherits="SIANWEB.CapPedidoCaptado" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow_Pedido() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow_Pedido(mensaje) {
                //debugger;
                GetRadWindow_Pedido().BrowserWindow.ActivarBanderaRebind_Pedido();
               //debugger;
                if (mensaje != '' && mensaje != null) {
                    var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                    cerrarWindow.add_close(
                        function () {
                            CloseAndRebind_Pedido();
                        });
                }
                else {
                    CloseAndRebind_Pedido();
                }              
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind_Pedido() {
                //debugger;
                GetRadWindow_Pedido().Close();
            }

            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage_Pedido() {
                GetRadWindow_Pedido().BrowserWindow.location.reload();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
    </telerik:RadAjaxManager>
    <div class="formulario">
        <div runat="server" id="divPrincipal" style="margin-left: 10px; margin-right: 10px;
            margin-top: 10px;">
            <telerik:RadAjaxPanel ID="ajaxFormPanel" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                <table runat="server" visible="false">
                    <tr>
                        <td>
                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label>
                        </td>
                        <td height="25px" style="background-color: #F2F2F2;">
                            <asp:Label ID="lblEmpresaNombre" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRegion" runat="server" Text="Región"></asp:Label>
                        </td>
                        <td height="25px" style="background-color: #F2F2F2;">
                            <asp:Label ID="lblRegionNombre" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal"></asp:Label>
                        </td>
                        <td height="25px" style="background-color: #F2F2F2;">
                            <asp:Label ID="lblSucursalNombre" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAceptar">
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <br />
                                <br />
                            </td>
                            <td height="25px">
                                &nbsp;
                            </td>
                            <td height="25px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblPedido" runat="server" Text="Pedido"></asp:Label>
                                </td>
                                <td height="25px">
                                    <telerik:RadNumericTextBox ID="txtPedido" runat="server" MaxLength="9" MinValue="1"
                                        Width="100px">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td height="25px">
                                    <asp:RequiredFieldValidator ID="val_txtPedido" runat="server" ControlToValidate="txtPedido"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="aceptar">
                                    </asp:RequiredFieldValidator>
                                </td>
                        </tr>
                        <tr>
                                    <td width="60">
                                        &nbsp;
                                    </td>
                                    <td width="60">
                                    </td>
                                    <td style="text-align: right;" width="100">
                                        <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar"
                                            ValidationGroup="aceptar" />
                                    </td>
                                    <td style="text-align: right;">
                                        &nbsp;
                                    </td>
                         </tr>           
                    </table>
                </asp:Panel>
            </telerik:RadAjaxPanel>
        </div>
    </div>
</asp:Content>
