<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmPromocion_Cancelar.aspx.cs"
    Inherits="SIANWEB.CrmPromocion_Cancelar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlCausa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlCancelar" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnCancelarOportunidad">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlCancelar" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
        <asp:Panel ID="pnlCancelar" runat="server" BackColor="AliceBlue" BorderColor="SteelBlue"
            BorderWidth="1px" Width="400px">
            <div class="emergente">
                <div align="center" class="ehcontainer">
                    <div class="ehizq">
                    </div>
                    <div class="ehcentro" style="color: red">
                        CANCELAR OPORTUNIDAD</div>
                    <div class="ehder">
                    </div>
                </div>
                <div class="econtent">
                    <table>
                        <tr>
                            <td colspan="3" style="font-family: Arial; font-size: 12px;">
                                ¿Está usted seguro que desea cancelar la oportunidad? En caso de que así sea, especifique
                                la causa y opcionalmente comentarios generales.
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3" style="font-family: Arial; font-size: 12px;">
                                <asp:Panel ID="pnlCancelacion" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Label ID="lblMensaje" runat="server" ForeColor="red"></asp:Label>
                                            </td>
                                            <caption>
                                                &nbsp;
                                            </caption>
                                        </tr>
                                        <caption>
                                            &nbsp;
                                            <tr>
                                                <td align="left" style="width: 100px">
                                                    <asp:Label ID="LblCausa" runat="server" ForeColor="Red" Text="Causa:"></asp:Label>
                                                </td>
                                                <td align="left" width="150">
                                                    <telerik:RadComboBox ID="ddlCausa" runat="server" MaxHeight="150px" 
                                                        Width="150px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td align="left" width="95">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                        ControlToValidate="ddlCausa" Display="Dynamic" ErrorMessage="*Requerido" 
                                                        ForeColor="Red" InitialValue="-- Seleccionar --" SetFocusOnError="True" 
                                                        ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 100px">
                                                    <asp:Label ID="LblCompetidor" runat="server" ForeColor="Red" Text="Competidor:"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <telerik:RadTextBox ID="txtCompetidor" runat="server" MaxLength="100" 
                                                        Width="150px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="Comentarios:"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <telerik:RadTextBox ID="txtComentario" runat="server" Height="72px" 
                                                        MaxLength="70" TextMode="MultiLine" Width="240px">
                                                        <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </caption>
                                    </table>
                                    <span style="background-color: #ffffff"></span>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td align="center" style="width: 100px">
                                &nbsp;<asp:ImageButton ID="ibtnCancelarOportunidad" runat="server" ImageUrl="Imagenes/check2.png"
                                    ToolTip="Cancelar oportunidad" OnClick="ibtnCancelarOportunidad_Click" ValidationGroup="Guardar"
                                    ImageAlign="AbsMiddle" />
                                <asp:ImageButton ID="ibtnCerrarVentana" runat="server" ImageUrl="imagenes/salir.png"
                                    ToolTip="Cerrar ventana" ImageAlign="Middle" OnClick="ibtnCerrarVentana_Click" />
                            </td>
                            <td style="width: 81px">
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            //
            function returnToParent(tipo, comp, coment) {
                var oArg = new Object();
                oArg.Tipo = tipo;
                oArg.Comp = comp;
                oArg.Coment = coment;
                //get a reference to the current RadWindow
                var oWnd = GetRadWindow();
                oWnd.close(oArg);
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function SoloAlfanumerico(sender, eventArgs) {
                //debugger;
                var c = eventArgs.get_keyCode();
                if ((c < 32) || (c > 32 && c < 46) || (c > 46 && c < 48) || (c > 57 && c < 65) || (c > 90 && c < 97) || (c > 122 && c < 124) || (c > 124 && c < 193) || (c > 193 && c < 201) || (c > 201 && c < 205) || (c > 205 && c < 209) || (c > 209 && c < 211) || (c > 211 && c < 218) || (c > 218 && c < 225) || (c > 225 && c < 233) || (c > 233 && c < 237) || (c > 237 && c < 241) || (c > 241 && c < 243) || (c > 243 && c < 250) || (c > 250))
                    if (c != 95 && c != 124) //el guion bajo  y '|' tambien son permitidos
                        eventArgs.set_cancel(true);

        }
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
