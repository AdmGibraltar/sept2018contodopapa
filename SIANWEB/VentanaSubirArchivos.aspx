<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01.Master" AutoEventWireup="true" CodeBehind="VentanaSubirArchivos.aspx.cs" Inherits="SIANWEB.VentanaSubirArchivos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
   <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind(param) {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.FisicoTerminado(param);
            }

        </script>
      
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="buttonSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div id="wrapper" style = "background:red;">
        <div id="content">
            <h1>Subír Ordenes de Compra y de Instalación</h1>
            <p>Click en Choose File para buscar el archivo que se desea subir, depues de 
                seleccionarlo dar click en el simbolo de &quot; + &quot; para agregar o en el &quot; - &quot; para 
                eliminar.</p>
            <asp:UpdatePanel ID="pnUpdateFile" runat="server"  UpdateMode="Conditional" >
                <Triggers>
                    <asp:PostBackTrigger ControlID="cmdAddFile" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 450px;">
                                <asp:FileUpload ID="fUpload" runat="server" /></td>
                            <td style="width: 50px;">
                                <asp:Button ID="cmdAddFile" runat="server" Text="Agregar al listado" ToolTip="Añade el fichero a la lista"
                                    OnClick="cmdAddFile_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lstFiles" runat="server"></asp:ListBox>
                            </td>
                            <td>
                                <asp:Button ID="cmdDelFile" runat="server" Text="Eliminar del listado" ToolTip="Elimina el fichero seleccionado de la lista"
                                    OnClick="cmdDelFile_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="cmdSendMail" runat="server" Text="Enviar Archivos" OnClick="cmdSendMail_Click" />
            <h1>Resultado</h1>
            <asp:Literal ID="resultado" runat="server" />
        </div>
    </div>


</asp:Content>
