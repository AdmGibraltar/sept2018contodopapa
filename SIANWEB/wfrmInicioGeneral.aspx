<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmInicioGeneral.aspx.cs"
    Inherits="SIANWEB.wfrmInicioGeneral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
     
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
    <script type="text/javascript">
        function printpage() {
            window.print();
        }  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div id="centrador">
        <!--Inicia header-->
        <div class="header">
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="ddlCDS">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1"
                                UpdatePanelHeight="" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="ddl">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1"
                                UpdatePanelHeight="" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>
            <div class="menu_sesion">
                <asp:HyperLink ID="hpInicio" runat="server" ImageUrl="img/btn_inicio.jpg" NavigateUrl="wfrmInicioGeneral.aspx"></asp:HyperLink><a
                    href="inicio.aspx"><asp:Image ID="ibtnCerrarSesion" runat="server" ImageUrl="img/btn_cerrar_sesion.jpg" /></a>
            </div>
            <div class="logo">
                <img src="img/key_logo.jpg" />
            </div>
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
        <div class="contenido" runat="server" id="contenido">
            <table id="tblPrin" align="center">
                <tr>
                    <td align="right">
                        <asp:Panel ID="pnlComercial" runat="server">
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Núm. proyectos"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label3" runat="server" Text="Monto proyectos"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label4" runat="server" Text="Avances mes"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label6" runat="server" Text="Cantidad cerrados"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label5" runat="server" Text="Monto cerrados"></asp:Label>
                                    </td>
                                    <td width="100">
                                        &nbsp;
                                    </td>
                                    <td width="100">
                                        <asp:Label ID="lblcd" runat="server" Text="CD:"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="ddlCDS" runat="server" AutoPostBack="True" CssClass="inp1"
                                            OnSelectedIndexChanged="ddlCDS_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px" align="center">
                                        <telerik:RadNumericTextBox ID="NumProyectos" runat="server" ReadOnly="true" size="5"
                                            Style="text-align: center" Width="80px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td style="width: 100px" align="center">
                                        <telerik:RadNumericTextBox ID="MontoProyectos" runat="server" ReadOnly="true" size="5"
                                            Style="text-align: center" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td style="width: 100px" align="center">
                                        <telerik:RadNumericTextBox ID="AvanceMes" runat="server" ReadOnly="true" size="5"
                                            Style="text-align: center" Width="80px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td style="width: 100px" align="center">
                                        <telerik:RadNumericTextBox ID="CantidadCerrados" runat="server" ReadOnly="true" size="5"
                                            Style="text-align: center" Width="80px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td style="width: 100px" align="center">
                                        <telerik:RadNumericTextBox ID="MontoCerrados" runat="server" ReadOnly="true" size="5"
                                            Style="text-align: center" Width="80px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkMetas" runat="server" PostBackUrl="wfrmMetasTerritorio.aspx">Establecer metas y cuotas</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlGeneral" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        Vista de gráficas:&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="ddl" runat="server" AutoPostBack="True">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Importe de proyectos" Value="1" />
                                                <telerik:RadComboBoxItem Text="Número de proyectos" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="ddlMeses" runat="server" AutoPostBack="True" Visible="False">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 81px">
                        <div id="Label1">
                        </div>
                        <div>
                            <table align="center">
                                <tr>
                                    <td>
                                        <%=GeneraGraficaDistribucion()%>
                                    </td>
                                    <td>
                                        <%=GeneraGraficaActividad()%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <div runat="server" id="divImprimir" visible="false">
                                            &nbsp;<asp:LinkButton ID="ibtnImprimir" runat="server" CssClass="btn_imprimir">IMPRIMIR</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--Termina contenido-->
        <!--Inicia footer-->
        <div style="padding-bottom: 30px;">
            <div class="footer">
                <table width="910" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="274" height="27">
                            Derechos Reservados Key Qu&iacute;mica S.A de C.V.
                        </td>
                        <td>
                            <%--<table border="0" align="right" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <a href="#">&gt; URL M&oacute;dulo CRM</a>
                                    </td>
                                    <td>
                                        <a href="#">&gt; URL M&oacute;dulo ERP</a>
                                    </td>
                                    <td>
                                        <a href="#">&gt; URL keyquimica.com</a>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!--Termina footer-->
    </div>
    </form>
</body>
</html>
