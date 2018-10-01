<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMAceptarCambios.aspx.cs" Inherits="SIANWEB.CRMAceptarCambios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
        EnablePageHeadUpdate="False">
    </telerik:RadAjaxManager>
    <div>
       <div id="Panel">
            <asp:Panel ID="pnlAvanzar" runat="server" BackColor="AliceBlue" 
                BorderColor="SteelBlue" BorderWidth="1px" Width="420px">
                <div class="emergente">
                    <div align="center">                      
                    </div>
                    <div style="font-family:Arial; font-size:12px" >                        
                        <p id="Proyecto" runat="server">                            
                        </p>
                        <p>
                          Es importante que este proyecto se va a dividir en varios proyectos dependiendo las aplicaciones de la solución.
                        </p>
                        <p>
                          Estas seguro de continuar?
                        </p>
                        <div id="efooter" align="right">
                            <asp:Button ID="btnAvanzar" runat="server" 
                                ToolTip="Aceptar" Text="Si" Width="100px" 
                                OnClick="btnAvanzar_Click" />&nbsp;&nbsp;
                            <asp:Button ID="ibtnCerrar" runat="server" 
                                ToolTip="Cerrar ventana" Text="No" Width="100px" 
                                onClick="ibtnCerrar_Click" />
                        </div>
                    </div>                   
                </div>
            </asp:Panel>
        </div>    
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">
     //Cierra la venata actual y regresa el foco a la ventana padre
     function CloseWindow() {
         GetRadWindow().Close();
     }
     //
     function returnToParent(valido) {//create the argument that will be returned to the parent page
         var oArg = new Object();       
         oArg.Valido = valido;
         var oWnd = GetRadWindow();
         oWnd.close(oArg);
     }
     function GetRadWindow() {
         var oWindow = null;
         if (window.radWindow) oWindow = window.radWindow;
         else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
         return oWindow;
     }
   </script>
    </telerik:radcodeblock>
  </form>
</body>
</html>