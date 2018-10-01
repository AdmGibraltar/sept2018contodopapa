<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_ReportViewer.aspx.cs"
    Inherits="SIANWEB.Ventana_ReportViewer" %>

<html xmlns="http://www.w3.org/1999/xhtml" runat="server" id="html">
<head id="Head1" runat="server">
    <style type="text/css">
        html#html, body#body, form#form1, div#content
        {
            border: 0px solid black;
            padding: 0px;
            margin: 0px;
            height: 100%;
            width: 100%;
        }
    </style>
</head>
<body runat="server" id="body">
    <form id="form1" runat="server" style="width: 101%">
    <div runat="server" id="content">
        <telerik:ReportViewer ID="ReportViewer1" runat="server" ProgressText="Generando..."
            Width="100%" Height="100%">
            <Resources CurrentPageToolTip="Página actual" ExportButtonText="Exportar" ExportSelectFormatText="Exportar al formato seleccionado"
                ExportToolTip="Exportar" FirstPageToolTip="Primer página" LabelOf="de" LastPageToolTip="Última página"
                NextPageToolTip="Página siguiente" PreviousPageToolTip="Página anterior" PrintToolTip="Imprimir"
                ProcessingReportMessage="Generando reporte" RefreshToolTip="Actualizar"  />
        </telerik:ReportViewer>
    </div>
    </form>
</body>
</html>
