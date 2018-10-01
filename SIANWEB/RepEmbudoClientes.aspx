<%@ Page Title="Cumplimiento de Programacion de Actividades del RSC" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.master"
    AutoEventWireup="true" CodeBehind="RepEmbudoClientes.aspx.cs" Inherits="SIANWEB.RepEmbudoClientes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">    
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ctl00$CPH$RadToolBar1") != -1)
                    args.set_enableAjax(false);
            }
            
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
         <ClientEvents OnRequestStart="onRequestStart" />
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir" Display="false"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <br />
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td><asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                <td style="text-align: right" width="150px">&nbsp;</td>
                <td width="150px" style="font-weight: bold">&nbsp;</td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                    <table border=1 style="font-family: Verdana; font-size: 10pt; width:520px" >
                        <tr align="center">
                            <td style="width:200px">&nbsp;</td>
                            <td style="width:80px"><b>Tipo A</b></td>
                            <td style="width:80px"><b>Tipo B</b></td>
                            <td style="width:80px"><b>Tipo C</b></td>
                            <td style="width:80px"><b>Tipo D</b></td>
                        </tr>
                        <tr>
                            <td><b>Clientes Totales&nbsp;</b></td>
                            <%=strTotales%>
                        </tr>
                        <tr>
                            <td><b>Clientes con Planeacion&nbsp;</b></td>
                            <%=strPlaneados%>
                        </tr>
                        <tr>
                            <td><b>Clientes Calendarizados&nbsp;</b></td>
                            <%=strCalendario%>
                        </tr>
                        <tr>
                            <td><b>Citas Cumplidas&nbsp;</b></td>
                            <%=strCumplidos%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HF_Cve" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


