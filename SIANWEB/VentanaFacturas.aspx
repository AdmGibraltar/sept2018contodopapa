<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentanaFacturas.aspx.cs" Inherits="SIANWEB.VentanaFacturas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 410px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" />
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" PersistenceMode="Session"
            Skin="Outlook">
        </telerik:RadSkinManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnBuscar1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <table id="Table1" style="font-family: verdana; font-size: 8pt" runat="server">
            <tr  colspan = "3">               
                <td class="style1">
                    <asp:Label ID="Label1" runat="server" Text="¿Que deseas hacer con esta fatura?" 
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>                
            </tr>
            <tr>
             <td>&nbsp;</td>
            </tr>
             <tr>
             <td>&nbsp;</td>
            </tr>
             <tr>
             <td>&nbsp;</td>
            </tr>
             <tr>
                 <td class="style1">
                 <asp:Button ID="btnBaja" runat="server" Text="Baja Factura" onclick="btnBaja_Click" />
                 <asp:Button ID="btnRefacturar" runat="server" Text="Refacturar" onclick="btnRefacturar_Click"/>
                 <asp:Button ID="btnCancelar" runat="server" Text="Salir" onclick="btnCancelar_Click"/>
                 </td>
            </tr>
        </table>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                    return oWindow;
                }
                //Cierra la venata actual y regresa el foco a la ventana padre
                function CloseWindow() {
                    GetRadWindow().Close();
                }
                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind(param) {
                    debugger;
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.SeleccinarOpcion(param);
                }
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>
