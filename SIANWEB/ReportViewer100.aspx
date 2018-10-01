<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer100.aspx.cs" Inherits="ReportingSite.ReportViewer100" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        body, form { width: 100%; height: 100%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 100%">
        <telerik:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%"/>
    </div>
    </form>
</body>
</html>
