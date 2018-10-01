<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="SIANWEB.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
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
    <telerik:RadWindowManager ID="RWM1" runat="server" Skin="Office2007">
    <%--JFCV VERSION--%>
    <windows>
                
            <telerik:RadWindow ID="AbrirVentana_Version" runat="server" Behaviors="Move,Close,Maximize"
                Opacity="100" VisibleStatusbar="False" Width="560px" Height="280px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Versión" Modal="True" ShowContentDuringLoad="False">
            </telerik:RadWindow>
            </windows>
            <%--JFCV VERSION--%>
    </telerik:RadWindowManager>
    <div style="width: 800px; margin: 100px auto">
        <asp:Panel ID="PnlLogin" runat="server" DefaultButton="btnEntrar" BackImageUrl="~/Imagenes/Encabezados/Fondo_Login.png"
            Height="350px">
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    <%--JFCV VERSION--%>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/key_logo_portada.jpg" OnClick="popup();"/>
                        &nbsp;
                    </td>
                    <td width="200">
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/tit_modulo.jpg" />
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td width="200">
                        &nbsp;
                    </td>
                    <td align="center" width="90">
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
                    </td>
                    <td>
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
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
                    </td>
                    <td>
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/flecha.jpg" />
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
                    <td>
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkRecuperaPassword" runat="server" OnClick="LinkRecuperaPassword_Click"
                            TabIndex="4" Text="Recuperar contraseña" ValidationGroup="Recuperar"></asp:LinkButton>
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="right" colspan="3" style="border-color: #00AEEF; border-style: solid;
                        border-width: 1px 0px 0px 0px;">
                        &nbsp; &nbsp;
                        <asp:ImageButton ID="btnEntrar" runat="server" ImageUrl="~/Imagenes/entrar2.gif"
                            OnClick="ImageButton1_Click" ValidationGroup="Login" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
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
                        __doPostBack('<%= btnEntrar.ClientID %>', '')
                    } else
                        if ((c < 32) || (c > 32 && c < 46) || (c > 46 && c < 48) || (c > 57 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 193) || (c > 193 && c < 201) || (c > 201 && c < 205) || (c > 205 && c < 209) || (c > 209 && c < 211) || (c > 211 && c < 218) || (c > 218 && c < 225) || (c > 225 && c < 233) || (c > 233 && c < 237) || (c > 237 && c < 241) || (c > 241 && c < 243) || (c > 243 && c < 250) || (c > 250))
                            eventArgs.set_cancel(true);

                }

                function confirmCallBackFn(arg) {
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    if (arg) {
                        ajaxManager.ajaxRequest(null);
                    }
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

                //JFCV Agregar un popup que muestre el numero de version de las dlls y fecha 15 jun 2016 
                function popup() {
                    var oWnd = radopen("VentanaVersion.aspx", "AbrirVentana_Version");
                    oWnd.center();
                }

            </script>
        </telerik:RadCodeBlock>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            Telerik.Web.UI.RadWindowUtils.Localization =
            {
                "Close": "Cerrar",
                "Minimize": "Minimizar",
                "Maximize": "Maximizar",
                "Reload": "Recargar",
                "PinOn": "Pin on",
                "PinOff": "Pin off",
                "Restore": "Restore",
                "OK": "Aceptar",
                "Cancel": "Cancelar",
                "Yes": "Si",
                "No": "No"
            };
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
