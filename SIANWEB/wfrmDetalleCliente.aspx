<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmDetalleCliente.aspx.cs"
    Inherits="SIANWEB.wfrmDetalleCliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPotencial" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div class="centrador">
        <!--Inicia header-->
        <div class="header">
            <div class="menu_sesion">
                <asp:HyperLink ID="hpInicio" runat="server" ImageUrl="img/btn_inicio.jpg" NavigateUrl="wfrmInicioGeneral.aspx"></asp:HyperLink><a
                    href="inicio.aspx"><asp:Image ID="ibtnCerrarSesion" runat="server" ImageUrl="img/btn_cerrar_sesion.jpg" /></a></div>
            <div class="logo">
                <img alt="" src="img/key_logo.jpg" /></div>
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
        <!--Inicia contenido-->
        <div id="contenido" class="contenido" runat="server">
            <div class="tit_secc">
                <img alt="" src="img/tit_detalle_cliente.gif" />
            </div>
            <div id="DIV1">
                &nbsp;<br />
                <div class="detalle_cliente">
                    <div class="filtro_t1">
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" height="130">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <strong>UEN:</strong>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtUEN" runat="server" class="sel1" ReadOnly="true" TabIndex="0"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Segmento:</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSegmento" runat="server" class="sel1" ReadOnly="true" TabIndex="1"></asp:TextBox>
                                                <asp:Label ID="lblSeg" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="filtro_t1">
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" height="130">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <strong>Territorio:</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTerritorio" runat="server" class="sel1" ReadOnly="true" TabIndex="2"></asp:TextBox>
                                                <asp:Label ID="lblTer" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Cliente:</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCliente" runat="server" class="sel1" ReadOnly="true" TabIndex="3"></asp:TextBox>
                                                <asp:Label ID="lblCte" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblDim" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                            <div style="height: 31px; margin: 0px 0px 0px 55px;">
                                <a href="#">
                                    <img alt="" src="img/tit_btn_potencial.jpg" width="287" height="31" /></a>
                                <asp:ImageButton ID="imgContactos" runat="server" ImageUrl="img/tit_btn_contactos.jpg"
                                    Width="607" Height="31" BorderWidth="0px" />
                            </div>
                            <div style="background: url(img/Copia de bg_potencial.jpg) no-repeat; height: 51px;
                                margin: 0px 0px 0px 55px; padding: 5px 0px 30px 10px;">
                                <table width="870" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="right">
                                            Unidad de dimensi&oacute;n:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUnidadDimension" runat="server" class="inp4" ReadOnly="True"
                                                TabIndex="4" Width="248px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Factor:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFactor" runat="server" ReadOnly="True" MaxLength="6"
                                                TabIndex="5" MinValue="0">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Valor:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtValor" runat="server" MaxLength="6" MinValue="0"
                                                ReadOnly="true" TabIndex="6">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="," />
                                            </telerik:RadNumericTextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtValor"
                                                ErrorMessage="Valor incorrecto" ValidationExpression="[$]?\s*[-+]?([0-9]{0,3}(,[0-9]{3})*\.?[0-9]+)"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="tabla_detalles">
                                <asp:Panel ID="pnlPotencial" runat="server">
                                    <asp:Literal ID="ltlEstructura" runat="server"></asp:Literal><br />
                                    <asp:Label ID="lblEstruct" runat="server"></asp:Label>
                                    <table width="894" border="0" cellpadding="0" cellspacing="0">
                                    </table>
                                    <table width="900px">
                                        <tr>
                                            <td>
                                                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    CellSpacing="1" DataKeyField="AplicacionID" ShowFooter="true" Width="900" TabIndex="7">
                                                    <Columns>
                                                        <asp:BoundColumn DataField="UEN" HeaderText="UEN" SortExpression="UEN"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Segmento" HeaderText="Segmento" SortExpression="Segmento">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Area" HeaderText="Área"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Solucion" HeaderText="Solución"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Aplicacion" HeaderText="Aplicación"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Porcentaje" HeaderText="VPTeórico"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="VPObservado">
                                                            <ItemTemplate>
                                                                <telerik:RadNumericTextBox ID="txt" runat="server" AutoPostBack="True" OnTextChanged="txt_TextChanged"
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Porcentaje") %>' Width="80px"
                                                                    TabIndex="7" MinValue="0" MaxLength="9">
                                                                    <ClientEvents OnFocus="txt_OnFocus" />
                                                                </telerik:RadNumericTextBox>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VPObservado") %>'
                                                                    TabIndex="7"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="tr_tit" />
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Valor potencial teórico<asp:TextBox ID="txtValorPT" runat="server" ReadOnly="true"
                                                    TabIndex="8"></asp:TextBox>
                                                Valor potencial observado<asp:TextBox ID="txtValorPO" runat="server" ReadOnly="true"
                                                    TabIndex="9"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="width: 894px; margin: 30px 0px 30px 55px; height: 40px;">
                                        <asp:LinkButton ID="ibtnRegresar" runat="server" class="btn_regresar" CausesValidation="False"
                                            TabIndex="11" OnClick="ibtnRegresar_Click">REGRESAR</asp:LinkButton>
                                        <asp:LinkButton ID="ibtnGuardaPotencial" runat="server" class="btn_guardar" OnClick="ibtnGuardaPotencial_Click"
                                            TabIndex="10">GUARDAR</asp:LinkButton></div>
                                    <asp:DataGrid ID="dg2" runat="server" AutoGenerateColumns="False" CssClass="tablagrid"
                                        DataKeyField="AplicacionID" Visible="False">
                                        <Columns>
                                            <asp:BoundColumn DataField="UEN" HeaderText="UEN" SortExpression="UEN"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Segmento" HeaderText="Segmento" SortExpression="Segmento">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Area" HeaderText="Área"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Solucion" HeaderText="Solución"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Aplicacion" HeaderText="Aplicación"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Porcentaje" HeaderText="VPTeórico"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="VPObservado">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Porcentaje") %>'
                                                        Width="112px" MinValue="0" MaxLength="9">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VPObservado") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" Visible="False">Potencial</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server" Visible="False">Contactos</asp:LinkButton>&nbsp;
                        </td>
                    </tr>
                </table>
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
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var varfocus;
            function txt_OnFocus(sender) {
                varfocus = sender.get_id();
            }

            function setFocus() {
                var comboBox = $find(varfocus);
                comboBox.focus();
            }
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
