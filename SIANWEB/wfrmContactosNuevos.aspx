<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmContactosNuevos.aspx.cs"
    Inherits="SIANWEB.wfrmContactosNuevos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=8" /> 
     <meta http-equiv="Pragma" content="no-cache"> 
<meta http-equiv="no-cache"> 
<meta http-equiv="Expires" content="-1"> 
<meta http-equiv="Cache-Control" content="no-cache"> 
 <title></title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlAltaContacto" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div align="center" width="550px">
        <asp:Panel ID="pnlBusqueda" runat="server" Width="650px">
            <div class="emergente">
                <div class="ehcontainer" align="center">
                    <div class="ehder">
                    </div>
                </div>
                <div class="econtent">
                    <asp:Panel ID="pnlAltaContacto" runat="server" BackColor="AliceBlue" BorderColor="SteelBlue"
                        BorderWidth="1px" Width="650px">
                        <table cellpadding="0" cellspacing="0">
                            <!--<tr>
                                <td colspan="4" style="font-weight: bold; color: white; background-color: steelblue;">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="DETALLE DE CONTACTO"></asp:Label>
                                </td>
                            </tr>-->
                            <tr>
                                <td align="left" colspan="4" style="font-weight: bold; background-color: white;">
                                    &nbsp;Información del contacto
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="4" style="font-weight: bold; background-color: white">
                                    <asp:Label ID="Label11" runat="server" Font-Size="X-Small" ForeColor="Red" Text="*Campos obligatorios"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label41" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" Text="Estructura:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblEstructura" runat="server" CssClass="inp1" Width="196px"></asp:Label>
                                    <telerik:RadComboBox ID="ddlEstructura" runat="server" CssClass="sel1" Visible="False" ValidationGroup="guardar" TabIndex="1">
                                        <Items>
                                          
                                        </Items>    
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 71px" align="right">
                                    <asp:Label ID="Label10" runat="server" Text="Posición:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblPosicion" runat="server" CssClass="inp1"></asp:Label>
                                    <telerik:RadTextBox ID="txtPosicion" runat="server" CssClass="inp1" TabIndex="2">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label23" runat="server" Text="Título:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtTitulo" runat="server" CssClass="inp1" Width="96px" TabIndex="3">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 71px" align="right">
                                    <asp:Label ID="Label27" runat="server" Text="Departamento:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtDepartamento" runat="server" CssClass="inp1" Width="208px" TabIndex="4">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:Label ID="Label7" runat="server" Text="Nombre(s):"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtNombres" runat="server" CssClass="inp1" Width="208px" TabIndex="5">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 71px" align="right">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:Label ID="Label12" runat="server" Text="Apellidos:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtApellidos" runat="server" CssClass="inp1" TabIndex="6"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 19px" align="right">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="Label8" runat="server" Text="Teléfono:"></asp:Label>
                                </td>
                                <td style="height: 19px" align="left">
                                    <telerik:RadTextBox ID="txtTelefono" runat="server" CssClass="inp1" MaxLength="15" TabIndex="7">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadTextBox>
                                    &nbsp;
                                </td>
                                <td style="width: 71px; height: 19px;" align="right">
                                    <asp:Label ID="Label24" runat="server" Text="Ext."></asp:Label>
                                </td>
                                <td align="left" style="height: 19px">
                                    <telerik:RadNumericTextBox ID="txtExt" runat="server" CssClass="inp1" MaxLength="15" TabIndex="8"
                                        Width="128px" MinValue="0">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label25" runat="server" Text="Otro teléfono:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtOtroTel" runat="server" CssClass="inp1" MaxLength="15" TabIndex="9">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 71px" align="right">
                                    <asp:Label ID="Label13" runat="server" Text="Celular:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtCelular" runat="server" CssClass="inp1" MaxLength="15" TabIndex="10"
                                        Width="128px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 37px" align="right">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:Label ID="Label9" runat="server" Text="Correo:"></asp:Label>
                                </td>
                                <td style="height: 37px" align="left">
                                    <telerik:RadTextBox ID="txtCorreo" runat="server" CssClass="inp1" TabIndex="11"/>
                                    <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo"
                                        ErrorMessage="Dirección de correo electrónico no válida" Font-Size="X-Small"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 71px; height: 37px;" align="right">
                                    <asp:Label ID="Label26" runat="server" Text="Jefe inmediato:"></asp:Label>
                                </td>
                                <td style="height: 37px" align="left">
                                    <telerik:RadTextBox ID="txtJefeInmediato" runat="server" CssClass="inp1" TabIndex="12">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="font-weight: bold; background-color: white">
                                    &nbsp;Información acerca de la dirección
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label18" runat="server" Text="Calle:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtCalle" runat="server" CssClass="inp1" TabIndex="13">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 71px" align="right">
                                    <asp:Label ID="Label19" runat="server" Text="Colonia:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtColonia" runat="server" CssClass="inp1" TabIndex="14">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label20" runat="server" Text="Municipio:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtMunicipio" runat="server" CssClass="inp1" TabIndex="15">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 71px" align="right">
                                    <asp:Label ID="Label21" runat="server" Text="Estado:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtEstado" runat="server" CssClass="inp1" TabIndex="16">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label22" runat="server" Text="Código postal:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadNumericTextBox ID="txtCodigoPostal" runat="server" CssClass="inp1" TabIndex="17" 
                                        MaxLength="5" MinValue="0">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td style="width: 71px">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="font-weight: bold; background-color: white">
                                    &nbsp;Información adicional
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label14" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="txtFechaNacimiento" runat="server" Width="100px" CssClass="inp1" TabIndex="18">
                                        <Calendar ID="Calendar1" runat="server">
                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                TodayButtonCaption="Hoy">
                                            </FastNavigationSettings>
                                        </Calendar>
                                        <DateInput ID="DateInput1" runat="server">
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 71px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label15" runat="server" Text="Asistente:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtAsistente" runat="server" CssClass="inp1" TabIndex="19">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 71px" align="right">
                                    <asp:Label ID="Label16" runat="server" Text="Teléfono del asistente:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="txtTelefonoAsistente" runat="server" CssClass="inp1" TabIndex="20">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label17" runat="server" Text="Comentarios generales:"></asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="txtComentariosGenerales" runat="server" CssClass="inp1" Width="384px" TabIndex="21"
                                        TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4" style="background-color: white">
                                    &nbsp;<asp:LinkButton ID="btnDeshacer" runat="server" CausesValidation="False" class="btn_regresar" TabIndex="23"
                                        OnClick="btnDeshacer_Click" >REGRESAR</asp:LinkButton>
                                    <asp:LinkButton ID="btnAgregar" runat="server" class="btn_guardar" ValidationGroup="guardar" TabIndex="22"
                                        OnClick="btnAgregar_Click" >GUARDAR</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HF_IdCte" runat="server" />
    <asp:HiddenField ID="HF_IdSeg" runat="server" />
    <asp:HiddenField ID="HF_IdCon" runat="server" />
    <asp:HiddenField ID="HF_IdPos" runat="server" />
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

            function CloseAndRebind(grid) {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(grid);
            }

            function SoloNumerico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (c && c == 13)
                    eventArgs.set_cancel(true);
                if (c < 48 || c > 57) //si no es numero
                    eventArgs.set_cancel(true);
            }
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
