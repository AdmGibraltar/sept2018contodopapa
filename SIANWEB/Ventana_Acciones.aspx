<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="Ventana_Acciones.aspx.cs" Inherits="SIANWEB.Ventana_Acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="div" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           
              
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="div">
        <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick1">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                </td>
                <td width="150px">
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt;">
            <tr>
                <td width="10">
                </td>
                <td>
                    <asp:Label ID="lblTip" runat="server" Text="Etapa" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTipo" runat="server"></asp:Label>
                </td>
                <td width="10">
                </td>
                <td>
                    <asp:Label ID="lblDia" runat="server" Text="Días" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDias" runat="server"></asp:Label>
                </td>
                <td width="10">
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td width="70">
                    <asp:Label ID="lblCli" runat="server" Text="Cliente" Font-Bold="True"></asp:Label>
                </td>
                <td colspan="7">
                    <asp:Label ID="lblCliente" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td width="70">
                    <asp:Label ID="lblDoc" runat="server" Text="Documento" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDocumento" runat="server"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblImp" runat="server" Text="Importe" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblImporte" runat="server"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblSal" runat="server" Text="Saldo" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSaldo" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td>
                    &nbsp;
                </td>
                <td width="70">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td width="70">
                </td>
                <td width="150">
                </td>
                <td width="10">
                </td>
                <td width="50">
                </td>
                <td>
                </td>
                <td width="10">
                </td>
                <td width="50">
                </td>
                <td>
                </td>
            </tr>
        </table>
        <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="76%" ResizeMode="AdjacentPane"
            ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
            <telerik:RadPane ID="divPrincipal" runat="server" ScrollBars="Vertical" Height="100%">
                <div style="float:left;width:20%;display:none" runat="server" id="divFacturas" >
                    <table  id="tbfacturas">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFacturas" runat="server" Font-Bold="True" Text="Facturas" Width="350px"></asp:Label>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadListBox ID="listFacturas" runat="server" CheckBoxes="True" Width="120px" 
                                    Height="250px" SelectionMode="Single" >
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float:left;width:75%;">
                <table style="font-family: Verdana; font-size: 8pt;" width="100%">
                    <tr>
                        <td colspan="2" align="center" style="background-color:#EFEFEF">
                            <b>Acciones</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" style="height: 100%; width: 100%; border: solid 1px grey">
                                <tr>
                                    <td width="10px">
                                    <asp:HiddenField ID="HiddenIdCliente" runat="server" value="0" />
                                    </td>
                                    <td>
                                        <div runat="server" id="divPreguntas">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                 </div>
                 <div style="float:left;width:95%;" >
                    <table style="font-family: Verdana; font-size: 8pt;" width="100%">
                        <tr>
                            <td colspan="2" align="center" style="background-color:#EFEFEF">
                                <b>Bitácora</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" style="height: 100%; width: 100%; border: solid 1px grey">
                                    <tr>
                                        <td width="10px">
                                        </td>
                                        <td>
                                            <div runat="server" id="divBitacora">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                 </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <asp:HiddenField ID="HiddenRebind" runat="server" />
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

                GetRadWindow().BrowserWindow.refresh();
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refresh();
            }

            function OnItemChecked(sender, eventArgs) {
                var urlArchivo = 'VentanaAcciones.aspx';
                parametros = "gpo=" + _gpo;
                
                parametros = parametros + "&clv=" + _clv;
                var a = obtenerrequest(urlArchivo, parametros);
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
