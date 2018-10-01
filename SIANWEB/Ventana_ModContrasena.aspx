<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ventana_ModContrasena.aspx.cs" Inherits="SIANWEB.Ventana_ModContrasena" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language ="javascript" type ="text/javascript" >
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
            return oWindow;
        }

        function CloseWindow() {
            GetRadWindow().Close();
        }
        function RefreshParentPage() {
            GetRadWindow().BrowserWindow.location.reload();
        }
       
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All"/>
    <telerik:RadSkinManager runat="server" ID="RadSkinManager1" ShowChooser="False"  PersistenceKey="Skin" PersistenceMode="Session"></telerik:RadSkinManager>
    <div>
        <table style="font-family: verdana; font-size: 8pt">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                    </tr>
                </table>    
            </td>
        </tr>
        <tr>
            <td>
                <table>
                     <tr>
                        <td>&nbsp;</td>
                        <td width="140px">Contraseña anterior</td>
                        <td><asp:TextBox ID="txtContAnt" runat="server" TextMode="Password" MaxLength="15" Width="115px" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtContAnt" Display="Dynamic" ErrorMessage="*Requerido" 
                                ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                     <tr>
                        <td>&nbsp;</td>
                        <td width="140px">Contraseña nueva</td>
                        <td><asp:TextBox ID="txtContNueva" runat="server" TextMode="Password" MaxLength="15" Width="115px" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtContNueva" Display="Dynamic" ErrorMessage="*Requerido" 
                                ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                     <tr>
                        <td>&nbsp;</td>
                        <td width="140px">Confirmar contraseña</td>
                        <td><asp:TextBox ID="txtContConf" runat="server" TextMode="Password" MaxLength="15" Width="115px" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtContConf" Display="Dynamic" ErrorMessage="*Requerido" 
                                ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table align="right">
                    <tr>
                        <td><input id="Hidden1" type="hidden" runat ="server" visible ="false" /></td>
                        <td><asp:Button ID="btnGuardar" Text="Guardar" runat="server" Width ="65px" 
                                ValidationGroup ="guardar" onclick="btnGuardar_Click" /></td>
                        <td width="1px">&nbsp;</td>
                        <td><asp:Button ID="btnCancelar" Text="Cancelar" runat="server" Width ="65px" 
                                onclick="btnCancelar_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>   
        </table>
        
    <telerik:RadInputManager ID="RadInputManager1" runat="server">
     <telerik:TextBoxSetting Validation-IsRequired="True" Validation-ValidationGroup="guardar" Validation-ValidateOnEvent="Submit">
        <TargetControls >
        <telerik:TargetInput ControlID ="txtContAnt" />
        <telerik:TargetInput ControlID ="txtContNueva" />
        <telerik:TargetInput ControlID ="txtContConf" />
        </TargetControls>
     </telerik:TextBoxSetting>
   </telerik:RadInputManager>
   
    </div>
    </form>
</body>
</html>
