<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="SIANWEB.Ayuda.Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onNodeClicking(sender, args) {
                try {
                    //  alert(window.parent.frames[1].location);
                    //  alert("OnClientNodeClicking: " + args.get_node().get_toolTip());
                    window.parent.frames[1].location = args.get_node().get_toolTip();
                }
                catch (err) {
                    alert(err);
                }
            }

            function Serach() {
                var busca = document.getElementById("<%=  txtJelp.ClientID %>");
                document.getElementById("<%=  txtJelp.ClientID %>").value = busca.value.replace(/</g, '&lt;').replace(/>/g, '&gt;') ;
                window.parent.frames[1].location = "Busqueda.aspx?Palabra=" + busca.value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tvMenu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
    <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <table style='font-family: verdana; font-size:10px; color: #3366FF'>
                    <tr>
                        <td colspan="2">Buscar en Ayuda:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtJelp" runat="server" Width="170px" TextMode="Search"></asp:TextBox>
                            
                        </td>
                        <td><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="img/ifind.png" width="20px" height="20px" OnClientClick="Serach();" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style=" border-top-style:inset; border-top-color:#3366FF; border-top-width:2px">&nbsp;</td></tr>
        <tr>
            <td>    
                <!-- OnNodeClick="tvMenu_NodeClick" -->
                <telerik:RadTreeView ID="tvMenu" runat="server" Width="300px"  OnClientNodeClicking="onNodeClicking"
                    OnNodeDrop="tvMenu_NodeDrop" EnableDragAndDrop="True" EnableDragAndDropBetweenNodes="True"
                    RenderMode="Lightweight" 
                    AllowNodeEditing="false" 
                    Skin="Vista" 
                    >
                </telerik:RadTreeView>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
