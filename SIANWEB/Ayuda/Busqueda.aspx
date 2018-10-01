<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Busqueda.aspx.cs" Inherits="SIANWEB.Busqueda.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Ayuda</title>
        <style type="text/css">
            body
            {
                color:#000000;
                background-color:#FFFFFF;
                background-image:url('Background Image');
                background-repeat:no-repeat;
                }
            a { color:#0000FF;}
            a:visited { color:#800080;}
            a:hover { color:#008000; }
            a:active { color:#FF0000; }
    
    </style>
    </head>
    <body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="font-family: Verdana">
    <table id="TblEncabezado" style="font-family: verdana; font-size: 12pt" runat="server"
        width="99%">
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td> Resultados de la Busqueda "<%=strpalabra%>"</td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <%=strPagina%>
            </td>
        </tr>
    </table>
  </div>
  </form></body>
</html>

