<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmAceptarAvance.aspx.cs" Inherits="SIANWEB.CrmAceptarAvance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div id="Panel">
            <asp:Panel ID="pnlAvanzar" runat="server" BackColor="AliceBlue" 
                BorderColor="SteelBlue" BorderWidth="1px" Width="420px">
                <div class="emergente">
                    <div align="center">
                        <div class="ehizq">
                        </div>
                        <div class="ehcentro">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="AVANZAR ETAPA"></asp:Label>
                            &nbsp;</div>
                        <div class="ehder">
                        </div>
                    </div>
                    <div style="font-family:Arial; font-size:12px" >
                        <p>
                            ¿Está usted seguro que desea avanzar el proyecto a la siguiente etapa? 
                            Una vez registrado el cambio no podrá regresar a la etapa previa.
                        </p>
                        <p>
                            De clic en &quot;Aceptar&quot; para avanzar de etapa, de lo contrario cierre la ventana 
                            para permanecer en la etapa actual.
                        </p>
                        <div id="efooter" align="center">
                            <asp:ImageButton ID="btnAvanzar" runat="server" AlternateText="Aceptar" 
                                ImageUrl="Imagenes\check2.png" ToolTip="Aceptar" ImageAlign="AbsMiddle"
                                onclick="btnAvanzar_Click" />
                            <asp:ImageButton ID="ibtnCerrar" runat="server" AlternateText="Cerrar Ventana" 
                                ImageUrl="Imagenes\salir.png" ToolTip="Cerrar ventana" ImageAlign="Middle" 
                                onclick="ibtnCerrar_Click" />
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
     function returnToParent(id, fecha, valido) {//create the argument that will be returned to the parent page
         var oArg = new Object();
         oArg.Id = id;
         oArg.fecha = fecha;
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
