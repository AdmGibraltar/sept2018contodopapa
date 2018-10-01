<%@ Page Title="Enviar documentos" Language="C#" AutoEventWireup="True" CodeBehind="Ventana_EnviarPagos.aspx.cs"
    Inherits="SIANWEB.Ventana_EnviarPagos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</head> 
<body>
    <form id="form1" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      </telerik:RadScriptManager>
      <telerik:RadWindowManager ID="RWM1" runat="server" Skin="Office2007">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PnlLogin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="PnlLogin" runat="server" DefaultButton="btnEnviar">
        <table style="font-family: Verdana; font-size: 8pt">

            <tr>
            <td colspan="4">
             &nbsp; &nbsp;
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Tipo "></asp:Label>
                </td>
                <td>
                 <asp:Label ID="LblTipo" runat="server" ></asp:Label>
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="No. Documento "></asp:Label>
                </td>
                <td>
                 <asp:Label ID="LblDocumento" runat="server" ></asp:Label>
                </td>
                <td>
                   
                </td>
            </tr>
                 <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Cliente "></asp:Label>
                </td>
                <td>
                 <asp:Label ID="LblId_CteStr" runat="server" ></asp:Label>
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td align="center">
                  <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td align="left" colspan="3">
                     <asp:Label ID="Label3" runat="server" Text="Correos "></asp:Label>
                </td>
            </tr>
              
                       <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="left" colspan="3">
                  <telerik:RadTextBox runat ="Server" ID="TxtCorreos" Width ="500px" MaxLength="400">
                  </telerik:RadTextBox>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
                <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center" colspan="3">
                    <asp:Label ID="Label5" runat="server" Text ="(Puede agregar varias direcciones de correo separandolos con <b>;</b>)"></asp:Label>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;</td>
                <td align="center">
                    &nbsp;
                    <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" 
                        Text="Enviar" />
                </td>
            </tr>
        </table>
         <asp:HiddenField runat="server" ID="HFId_Cte"></asp:HiddenField> 
         <asp:HiddenField runat="server" ID="HFId_Cd"></asp:HiddenField> 
         <asp:HiddenField runat="server" ID="HFId_Pag"></asp:HiddenField> 
          <asp:HiddenField runat="server" ID="HFId_Emp"></asp:HiddenField> 
           <asp:HiddenField runat="server" ID="HFId_Fac"></asp:HiddenField> 
        <asp:HiddenField runat="server" ID="HFId_PagDet"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="HFSerie"></asp:HiddenField> 
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">

            function CloseWindowA(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }


            function AlertaFocus(mensaje, control) {

                var oWnd = radalert(mensaje, 340, 150);
                //oWnd.add_close(foco(control));
                oWnd.add_close(function () {
                    var target = $find(control);
                    if (target != null) {
                        target.focus();
                    }
                });
            }
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
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
            }
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }


        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
