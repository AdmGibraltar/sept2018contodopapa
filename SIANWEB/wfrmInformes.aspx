<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmInformes.aspx.cs" Inherits="SIANWEB.wfrmInformes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radControl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radControlAplicacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="radControlEntrada">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radDII">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radDIINumero">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radCierreMes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkProyectoNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlZonas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="radCampania">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnl" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div id="centrador">
        <!--Inicia header-->
        <div class="header">
            <div class="menu_sesion">
                <asp:HyperLink ID="hpInicio" runat="server" ImageUrl="img/btn_inicio.jpg" NavigateUrl="wfrmInicioGeneral.aspx"></asp:HyperLink><a
                    href="inicio.aspx"><asp:Image ID="ibtnCerrarSesion" runat="server" ImageUrl="img/btn_cerrar_sesion.jpg" /></a>
            </div>
            <div class="logo">
                <img src="img/key_logo.jpg" /></div>
            <div class="menu">
                <script type="text/javascript">
                    AC_FL_RunContent('codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0', 'width', '472', 'height', '86', 'src', 'swf/menu', 'quality', 'high', 'pluginspage', 'http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash', 'movie', 'swf/menu'); //end AC code
                </script>
                <noscript>
                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
                        width="472" height="86">
                        <param name="movie" value="swf/menu.swf" />
                        <param name="quality" value="high" />
                        <embed src="swf/menu.swf" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash"
                            type="application/x-shockwave-flash" width="472" height="86"></embed>
                    </object>
                </noscript>
            </div>
        </div>
        <!--Termina header-->
        <div id="divPrincipal" runat="server">
            <div class="contenido">
                <div id="mainContent" align="center">
                    <div id="barra">
                        &nbsp;</div>
                    <asp:Panel ID="pnl" runat="server" HorizontalAlign="Left">
                        <table>
                            <tr>
                                <td style="width: 250px">
                                    <div id="divRadioButtons" runat="server">
                                        <asp:RadioButton ID="radControl" runat="server" Text="Control de la promoción nivel solución"
                                            AutoPostBack="True" ValidationGroup="Opciones" GroupName="opcion" OnCheckedChanged="radControl_CheckedChanged" /><br />
                                        <br />
                                        <br />                                      
                                        <asp:RadioButton ID="radControlAplicacion" runat="server" Text="Control de la promoción nivel aplicación"
                                            ValidationGroup="Opciones" GroupName="opcion" AutoPostBack="True" OnCheckedChanged="radControlAplicacion_CheckedChanged" /><br />
                                        <br />
                                        <br />
                                        <br />
                                         <asp:RadioButton ID="radControlEntrada" runat="server" Text="Control de Entradas"
                                            ValidationGroup="Opciones" GroupName="opcion" AutoPostBack="True" OnCheckedChanged="radControlEntrada_CheckedChanged" /><br />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:RadioButton ID="radDII" runat="server" Text="DII en número de proyectos" AutoPostBack="True"
                                            GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radDII_CheckedChanged" /><br />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:RadioButton ID="radDIINumero" runat="server" Text="DII en importe de proyectos"
                                            AutoPostBack="True" GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radDIINumero_CheckedChanged" /><br />
                                          <br />
                                        <br />
                                        <br />
                                        <asp:RadioButton ID="radCampania" runat="server" Text="Campañas"
                                            AutoPostBack="True" GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radCampania_CheckedChanged" /><br />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:RadioButton ID="radCierreMes" runat="server" Text="Cierre de Mes"
                                            AutoPostBack="True" GroupName="opcion" ValidationGroup="Opciones" OnCheckedChanged="radCierreMes_CheckedChanged" /><br />
                                    </div>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlRepresentante" runat="server" Height="50px" Width="125px">
                                                    <table>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Label ID="Label3" runat="server" Width="300px" Text="Establezca el rango de monto del proyecto para filtrar los resultados de la búsqueda"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px">
                                                                <asp:Label ID="Label10" runat="server" Text="De:"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <telerik:RadNumericTextBox ID="txtDe" runat="server" Width="90px" MaxLength="9" MinValue="0">
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    <ClientEvents />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Label ID="Label2" runat="server" Text="A:"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px">
                                                                <telerik:RadNumericTextBox ID="txtA" runat="server" Width="90px" MaxLength="9" MinValue="0">
                                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                                    <ClientEvents />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                      
                                    </table>
                                    &nbsp;
                                    <asp:Panel ID="pnlComercial" runat="server">
                                        <table id="tablaComercial" runat="server" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="height: 21px">
                                                    <asp:Label ID="lblZona" runat="server" Text="CD:"></asp:Label>
                                                </td>
                                                <td style="width: 5px; height: 21px;">
                                                </td>
                                                <td style="height: 21px">
                                                    <telerik:RadComboBox ID="ddlZonas" runat="server" Width="224px" Filter="Contains"
                                                        ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" DataTextField="Descripcion"
                                                        DataValueField="Id" LoadingMessage="Cargando..." AutoPostBack="True" OnClientBlur="Combo_ClientBlur2"
                                                        OnSelectedIndexChanged="ddlZonas_SelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <td style="height: 21px">
                                                    <asp:Label ID="Label12" runat="server" Text="Representante:"></asp:Label>
                                                </td>
                                                <td style="width: 5px; height: 21px;">
                                                </td>
                                                <td style="height: 21px">
                                                    <telerik:RadComboBox ID="ddlRepresentantesComercial" runat="server" Width="224px"
                                                        Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                                        DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..."
                                                        OnClientBlur="Combo_ClientBlur">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 21px; width: 80px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Periodo:"></asp:Label>
                                                </td>
                                                <td style="width: 5px; height: 21px;">
                                                </td>
                                                <td style="height: 21px">
                                                    <telerik:RadComboBox ID="ddPeriodo" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                        Filter="Contains" LoadingMessage="Cargando..." MarkFirstMatch="true" HighlightTemplatedItems="true"
                                                        OnClientBlur="Combo_ClientBlur" Width="224px" MaxHeight="250">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="LabelID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                            Width="50px" />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 21px">
                                                    <asp:Label ID="lNuevo" runat="server" Text="Proyectos Nuevos:"></asp:Label>
                                                </td>
                                                <td style="width: 5px; height: 21px;">
                                                </td>
                                                <td style="height: 21px">
                                                  <asp:CheckBox ID="chkProyectoNuevo" runat="server" Text="" AutoPostBack="true"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td align="right">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 16px">
                                                    <asp:Label ID="lblRepComercial" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 230px">
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="ibtnExcelSolucion" runat="server" AlternateText="Generar reporte"
                                        ImageUrl="Imagenes\excel.gif" OnClick="ibtnExcel_Click" ToolTip="Generar reporte " />
                                    <asp:ImageButton ID="ibtnExcelAplicacion" runat="server" AlternateText="Generar reporte"
                                        ImageUrl="Imagenes\excel.gif" OnClick="ibtnExcelAplicacion_Click" ToolTip="Generar reporte"
                                        Visible="False" />
                                    <asp:ImageButton ID="ibtnExcelControlEntrada" runat="server" AlternateText="Generar reporte"
                                        ImageUrl="Imagenes\excel.gif" OnClick="ibtnExcelControlEntrada_Click" ToolTip="Generar reporte"
                                        Visible="False" />
                                    <asp:ImageButton ID="ibtnExcelNumero" runat="server" AlternateText="Generar reporte"
                                        ImageUrl="Imagenes\excel.gif" OnClick="ibtnExcelNumero_Click" ToolTip="Generar reporte N"
                                        Visible="False" />
                                    <asp:ImageButton ID="ibtnExcelImporte" runat="server" AlternateText="Generar reporte"
                                        ImageUrl="Imagenes\excel.gif" OnClick="ibtnExcelImporte_Click" ToolTip="Generar reporte I"
                                        Visible="False" />
                                    <asp:ImageButton ID="ibtnExcelCampania" runat="server" AlternateText="Generar reporte"
                                        ImageUrl="Imagenes\excel.gif" OnClick="ibtnExcelCampania_Click" ToolTip="Generar reporte"
                                        Visible="False" />
                                    <asp:ImageButton ID="IbtnExcelCierreMes" runat="server" AlternateText="Generar reporte Cierre Mes"
                                        ImageUrl="Imagenes\excel.gif" OnClick="IbtnExcelCierreMes_Click" ToolTip="Generar reporte"
                                        Visible="False" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblMensajes2" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        &nbsp; &nbsp;&nbsp;
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                    &nbsp;
                </div>
            </div>
        </div>
        <!--Inicia footer-->
        <div style="padding-bottom: 30px;">
            <div class="footer">
                <table width="910" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="274" height="27">
                            Derechos Reservados Key Qu&iacute;mica S.A de C.V.
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!--Termina footer-->
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                //--------------------------------------------------------------------------------------------------
                //Cuando el combo pierde el foco
                //Si el usuario escribió y no eligió un item correcto limpia el combo
                //--------------------------------------------------------------------------------------------------
                function Combo_ClientBlur(sender, args) {
                    //debugger;
                    var itemSelected = sender.findItemByText(sender.get_text())
                    if (itemSelected == null) {
                        LimpiarComboSelectIndex0(sender, '-- Seleccionar --');
                    }
                }
                function Combo_ClientBlur2(sender, args) {
                    //debugger;
                    var itemSelected = sender.findItemByText(sender.get_text())
                    if (itemSelected == null) {
                        LimpiarComboSelectIndex0(sender, '-- Todos --');
                    }
                }
                function Combo_ClientBlur3(sender, args) {
                    //debugger;
                    var itemSelected = sender.findItemByText(sender.get_text())
                    if (itemSelected == null) {
                        LimpiarComboSelectIndex0(sender, '-- Todas --');
                    }
                }
                function refreshGrid() {

                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    ajaxManager.ajaxRequest('RebindGrid');
                }


                //Limpiar radComboBox
                //param sender --> objeto a limpiar
                //NOTA: selecciona el primer item del combo que debe contener el texto '-- seleccionar --'
                function LimpiarComboSelectIndex0(sender, texto) {
                    //debugger;
                    if (sender.get_items().get_count() > 0) {
                        sender.get_items().getItem(0).select();
                        sender.set_value(texto);
                    }
                    else {
                        sender.set_value('');
                    }
                }
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>
