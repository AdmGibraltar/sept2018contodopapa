<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainSyn.aspx.cs" Inherits="SIANWEB.MainSyn.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Sync SIANWeb</title>        
        <style type="text/css">
            body
            {
                color:#000000;
                background-color:#FFFFFF;
                background-image:url('Background Image');
                background-repeat:no-repeat;
                font-family:verdana;
                font-size:10pt;
                }
            a { color:#0000FF;}
            a:visited { color:#800080;}
            a:hover { color:#008000; }
            a:active { color:#FF0000; }
    
        </style>
    </head>
    <script type="text/javascript">
        //  alert("<%=urll%>");     AyudaGenerica.aspx
        //  document.getElementById["contenido"].src = "<%=urll%>";
    </script>
    <body>
    <table>
        <tr>
            <td>
                Se sincronizan los procesos de la pagina <%=urll%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="Imagen1" runat="server"  Width="440px" Height="260px" />
            </td>
        </tr>
    </table>
    </body>
</html>

