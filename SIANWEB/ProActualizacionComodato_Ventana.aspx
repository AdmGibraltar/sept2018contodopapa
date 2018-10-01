<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProActualizacionComodato_Ventana.aspx.cs"
    Inherits="SIANWEB.ProActualizacionComodato_Ventana" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetRadWindow_FacturaPedido() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow_FacturaPedido() {
                //debugger;
                CloseAndRebind_FacturaPedido();
            }
            function CloseAndRebind_FacturaPedido() {
                //debugger;
                GetRadWindow_FacturaPedido().Close();
                //GetRadWindow().BrowserWindow.refreshGrid();
            }
        </script>
    </telerik:RadCodeBlock>
    <div style="font-family: Verdana; font-size: 8pt">
        <telerik:RadAjaxManager ID="RAM1" runat="server">
        </telerik:RadAjaxManager>
        <table>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Folio del contrato"></asp:Label>
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="txtFolio" runat="server" MaxLength="9" 
                        MinValue="0" Width="100px">
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFolio"
                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar"
                        ValidationGroup="aceptar" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
