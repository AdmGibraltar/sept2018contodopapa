<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" CodeBehind="RemisionPedido.aspx.cs" Inherits="SIANWEB.RemisionPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RadAjaxManager1" runat="server">
        <AjaxSettings>           
                
        </AjaxSettings>
    </telerik:radajaxmanager>
<%--    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager>--%>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
            function GetRadWindow_ventanaRemision() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow_ventanaRemisionPedido(mensaje) {
                //debugger;
                GetRadWindow_ventanaRemision().BrowserWindow.ActivarBanderaRebind_FacturaPedido(mensaje);
                CloseAndRebind_FacturaPedido();

                //var cerrarWindow = radalert(mensaje, 600, 10, tituloMensajes);
                //                cerrarWindow.add_close(
                //                    function () {
                //                        //debugger;
                //                        //GetRadWindow().Close();
                //                        CloseAndRebind_FacturaPedido();
                //                    });
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind_FacturaPedido() {
                //debugger;
                GetRadWindow_ventanaRemision().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }
                        
        </script>
    </telerik:radcodeblock>
    

    <div>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    
                </td>
                <td width="150px">
                </td>
            </tr>
        </table>
        <table style="width: 100%;" align="center">
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style1" align="center">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Names="Verdana" 
                        Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style1" align="center" width="300">
                    &nbsp;<asp:Label ID="LabelPedido" runat="server" Text="Pedido" Font-Names="Verdana" 
                        Font-Size="8pt"></asp:Label>
                &nbsp;
                    <telerik:RadNumericTextBox ID="RadNumericTextBoxPedido" Runat="server" 
                        MaxLength="9" MinValue="1">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="RadNumericTextBoxPedido" Display="Dynamic"
                        ErrorMessage="* Requerido" ForeColor="red" Font-Size="Small"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="imgAceptar" runat="server" CssClass="aceptar" 
                                    ImageUrl="~/Imagenes/blank.png" onclick="Button1_Click" ToolTip="Remisionar" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style1" align="center">
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>