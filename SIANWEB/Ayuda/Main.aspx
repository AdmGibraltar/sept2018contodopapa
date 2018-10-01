<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="SIANWEB.Ayuda.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Ayuda SIANWeb</title>
        <link rel="shortcut icon" href="../Ayuda/img/icon-question.ico" type="image/x-icon">
        <link rel="icon" href="../Ayuda/img/icon-question.ico" type="image/x-icon">
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
<FRAMESET cols="20%,80%" name="principal">
    <FRAME name="menu" SRC="Menu.aspx" ></FRAME>  
    <FRAME name="contenido" src=" <%=urll%>"></FRAME>
</FRAMESET>
    <script type="text/javascript">
        //  alert("<%=urll%>");     AyudaGenerica.aspx
        //  document.getElementById["contenido"].src = "<%=urll%>";
    </script>
</html>

