<%@ Page Title="Exceso de inventarios" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="Rep_InvExceso1.aspx.cs" Inherits="SIANWEB.Rep_InvExceso1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div runat="server" id="divPrincipal">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px" style="font-weight: bold">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt;">
            <tr>
                <td>
                </td>
                <td style="width: 900px">
                    <asp:Label ID="lblLeyenda" runat="server" Text="Centro:<b>[Sucursal]</b> Ultima actualización:<b>[Fecha]</b>"
                        Width="500px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 900px">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                        <%=GeneraGraficaDistribucion()%>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function myJS(var1, var2, var3, var4, var5, var6) {
                var window_dimensions = "toolbars=no,menubar=no,directories=no,location=no,scrollbars=auto,resizable=yes,status=no,width=530,height=500;left=200;top=200"
                window.open("Rep_InvExceso2.aspx?Proveedor=" + var1 + "&Centro=" + var2 + "&DiasVer=" + var4 + "&Tproducto=" + var3 + "&Indicador=" + var5 + "&Dias=" + var6, "_blank", window_dimensions);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
