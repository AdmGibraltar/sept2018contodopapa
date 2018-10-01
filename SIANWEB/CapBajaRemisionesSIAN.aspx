<%@ Page Title="Baja de remisiones a SIAN" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="CapBajaRemisionesSIAN.aspx.cs" Inherits="SIANWEB.CapBajaRemisionesSIAN" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
        </script>
    </telerik:radcodeblock>
    
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

    <telerik:radajaxmanager id="RAM1" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:radajaxmanager>

    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
</asp:Content>
