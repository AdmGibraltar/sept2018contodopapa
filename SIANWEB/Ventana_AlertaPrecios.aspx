<%@ Page Title="Alerta" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_AlertaPrecios.aspx.cs"
    Inherits="SIANWEB.Ventana_AlertaPrecios" %>

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
    <asp:Panel ID="PnlLogin" runat="server" >
        <table style="font-family: Verdana; font-size: 8pt">

            <tr>
            <td colspan="4">
             &nbsp; &nbsp;
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </td>
            </tr>
    
       
    
      <tr>
                <td align="center">
                
                </td>
                <td align="left" colspan="3">
                     <asp:Label ID="LblMensaje1" runat="server" Text="El precio a facturar de los productos [Productos]  es diferente al precio de venta autorizado según los convenios [Convenios] si se factura a otro precio se afectará la utilidad del cliente. Favor de corregir. " Width="500px" Font-Bold="True"></asp:Label>
                </td>
            </tr>
                <tr>
            <td>
            &nbsp;
            </td>
            </tr>
            <tr>
                <td align="center">
                  
                </td>
                <td align="left" colspan="3">
                     <asp:Label ID="LblMensaje2" runat="server" Text="Si se requiere seguir con esta operación favor de especificar el motivo en el siguiente recuadro, considerando que se cancelaran los precios AAA especiales en esta operación." Width="500px" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
            <td>
            &nbsp;
            </td>
            </tr>
            <tr>
                 <td align="center">
                  
                </td>
                <td align="left" colspan="3">
                     <asp:Label ID="Label1" runat="server" Text="Comentarios:" Width="500px" Font-Bold="True"></asp:Label>
                </td>
            </tr>
              
                       <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="left" colspan="3">
                  <telerik:RadTextBox runat ="Server" ID="TxtMensaje" Width ="500px" 
                        MaxLength="400" TextMode="MultiLine" Height="126px">
                  </telerik:RadTextBox>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
                <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center" colspan="3">
                    <asp:Label ID="Label5" runat="server" Text ="Nota: Esta información sera enviada al área de precios."></asp:Label>
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
                <td align="right">
                    &nbsp;
                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" 
                        Text="Aceptar" />
                        &nbsp;&nbsp;

  <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" 
                        Text="Cancelar" />

                </td>
            </tr>
        </table>
      
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

            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind(param) {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.ClienteSeleccionado(param);
            }


        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
