<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorReportesPropuestaTecnoEconomica.aspx.cs" 
Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.VisorReportesPropuestaTecnoEconomica" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

</head>

<body>

    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    <asp:Panel ID="pnlVisorDeReporte" runat="server">    
        <div id="divReporte" style="display:block;" style="width:100%;" >                        
            
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" style="width:100%;">
            </rsweb:ReportViewer>
        </div>
    </asp:Panel>

    </form>
    
</body>