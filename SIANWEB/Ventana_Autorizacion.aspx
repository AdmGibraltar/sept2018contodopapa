<%@ Page Title="Autorización" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_Autorizacion.aspx.cs"
    Inherits="SIANWEB.Ventana_Autorizacion" %>

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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
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
    <asp:Panel ID="PnlLogin" runat="server" DefaultButton="btnEntrar">
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td align="center" width="15">
                    &nbsp;
                </td>
                <td align="center" width="110">
                    &nbsp;
                </td>
                <td align="left" width="180">
                    &nbsp;
                </td>
                <td align="center" width="150">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Usuario:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtUserName" runat="server" MaxLength="20" onpaste="return false"
                        TabIndex="1" Width="180px">
                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                    </telerik:RadTextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserNameLogin" runat="server"
                        ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red"
                        SetFocusOnError="true" ValidationGroup="Login" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Contraseña: "></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtPassword" runat="server" AutoCompleteType="Disabled" MaxLength="15"
                        onpaste="return false" TabIndex="2" TextMode="Password" Width="180px">
                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                    </telerik:RadTextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass" runat="server" ControlToValidate="txtPassword"
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" SetFocusOnError="true"
                        ValidationGroup="Login" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center" colspan="3">
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label>
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
                    <asp:Button ID="btnEntrar" runat="server" Text="Aceptar" OnClick="btnEntrar_Click" />
                </td>
                <td align="center">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HF_IdTU"></asp:HiddenField>
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            function SoloAlfabetico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if ((c < 32) || (c > 32 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 193) || (c > 193 && c < 201) || (c > 201 && c < 205) || (c > 205 && c < 209) || (c > 209 && c < 211) || (c > 211 && c < 218) || (c > 218 && c < 225) || (c > 225 && c < 233) || (c > 233 && c < 237) || (c > 237 && c < 241) || (c > 241 && c < 243) || (c > 243 && c < 250) || (c > 250))
                    eventArgs.set_cancel(true);
            }

            function SoloAlfanumerico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (event.keyCode == 13) {
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    ajaxManager.ajaxRequest(null);
                } else
                    if ((c < 32) || (c > 32 && c < 46) || (c > 46 && c < 48) || (c > 57 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 193) || (c > 193 && c < 201) || (c > 201 && c < 205) || (c > 205 && c < 209) || (c > 209 && c < 211) || (c > 211 && c < 218) || (c > 218 && c < 225) || (c > 225 && c < 233) || (c > 233 && c < 237) || (c > 237 && c < 241) || (c > 241 && c < 243) || (c > 243 && c < 250) || (c > 250))
                        eventArgs.set_cancel(true);

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
            function CloseWindow(id_u, id_cd, nombre) {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.autorizar(id_u, id_cd, nombre);
            }
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
