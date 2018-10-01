<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="Pruebas.aspx.cs" Inherits="SIANWEB.Pruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div style="width: 900px">
        <%=GeneraGraficaDistribucion()%>
    </div>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function myJS(var1,var2) {
                window.alert(var1);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
