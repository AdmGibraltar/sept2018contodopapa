<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmMetasTerritorio.aspx.cs"
    Inherits="SIANWEB.CrmMetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <!--Inicia header-->
    <div id="centrador">
        <div class="header">
            <div class="menu_sesion">
                <asp:HyperLink ID="hpInicio" runat="server" ImageUrl="img/btn_inicio.jpg" NavigateUrl="wfrmInicioGeneral.aspx"></asp:HyperLink><a
                    href="inicio.aspx"><asp:Image ID="ibtnCerrarSesion" runat="server" ImageUrl="img/btn_cerrar_sesion.jpg" /></a>
            </div>
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
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RAM1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlCDS">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ibtnAplicar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="lnkGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="dgMetas">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div runat="server" id="divPrincipal" class="contenido">
            <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right" width="150px">
                    </td>
                    <td width="150px">
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="3" align="right">
                        &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="wfrmPrincipalCampanias.aspx">Campañas de promoción</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <table style="border-right: #33cc00 thin solid; border-top: #33cc00 thin solid; border-left: #33cc00 thin solid;
                            border-bottom: #33cc00 thin solid" cellpadding="0" cellspacing="0" class="tr_1"
                            width="100%">
                            <tr>
                                <td style="width: 188px">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 188px">
                                    <asp:Label ID="Label1" runat="server" Text="CD:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Cantidad" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Monto" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Avances" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Cantidad cerrados" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Monto cerrados" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="ddlCDS" runat="server" AutoPostBack="True" MaxHeight="250px"
                                        OnSelectedIndexChanged="ddlCDS_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtACantidad" runat="server" Width="70px" MaxLength="9"
                                        MinValue="0" onKeyPress="SoloNumerico">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    $<telerik:RadNumericTextBox ID="txtAMonto" runat="server" Width="70px" MaxLength="9"
                                        MinValue="0" onKeyPress="SoloNumerico">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtAAvances" runat="server" Width="70px" MaxLength="9"
                                        MinValue="0" CssClass="SoloNumerico">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtACantidadCerrados" runat="server" Width="70px"
                                        MaxLength="9" MinValue="0" CssClass="SoloNumerico">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    $<telerik:RadNumericTextBox ID="txtAMontoCerrados" runat="server" Width="70px" MaxLength="9"
                                        MinValue="0" onKeyPress="SoloNumerico">
                                        <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:ImageButton ID="ibtnAplicar" runat="server" AlternateText="Aplicar a todos"
                            ToolTip="Aplicar a todos" ImageUrl="~/img/check2.png" OnClick="ibtnAplicar_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="tit_azul">
                            METAS Y CUOTAS DE REPRESENTANTES</div>
                        <telerik:RadGrid ID="dgMetas" runat="server" AutoGenerateColumns="False" GridLines="None"
                            CssClass="tr_1" OnNeedDataSource="rg1_NeedDataSource" DataKeyField="UsuarioID"
                            MasterTableView-NoMasterRecordsText="No se encontraron registros." PageSize="15"
                            AllowPaging="True" OnPageIndexChanged="dgMetas_PageIndexChanged" Width="872px">
                            <MasterTableView>
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Id_Met" DataField="Id_Met" UniqueName="Id_Met"
                                        Display="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="CDS" DataField="Id_Cd" UniqueName="Id_Cd" Display="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="CDS" DataField="Cd_Nombre" UniqueName="Cd_Nombre">
                                        <HeaderStyle Width="100px" CssClass="tr_tit" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Id rik" DataField="Id_Rik" UniqueName="Id_Rik"
                                        Display="false">
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Representante" DataField="Rik_Nombre" UniqueName="Rik_Nombre">
                                        <HeaderStyle Width="200px" CssClass="tr_tit" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Núm. proyectos" DataField="Met_Proyectos"
                                        UniqueName="Met_Proyectos">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" DbValue='<%# Bind("Met_Proyectos") %>'
                                                Width="70px" OnKeyPress="SoloNumerico" MaxLength="9" MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" AllowRounding="false" />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Monto proyectos" DataField="Met_MontoProyecto"
                                        UniqueName="Met_MontoProyecto">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtMonto" runat="server" DbValue='<%# Bind("Met_MontoProyecto") %>'
                                                Width="90px" MinValue="0" onKeyPress="SoloNumerico" MaxLength="9">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Avances mes" DataField="Met_Avances" UniqueName="Met_Avances">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtAvances" runat="server" DbValue='<%# Bind("Met_Avances") %>'
                                                Width="70px" CssClass="SoloNumerico" MaxLength="9" MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" AllowRounding="false" />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Cantidad cerrados" DataField="Met_CantCerrado"
                                        UniqueName="Met_CantCerrado">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidadCerrados" runat="server" DbValue='<%# Bind("Met_CantCerrado") %>'
                                                Width="70px" CssClass="SoloNumerico" MaxLength="9" MinValue="0">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" AllowRounding="false" />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Monto cerrados" DataField="Met_MontCerrado"
                                        UniqueName="Met_MontCerrado">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtMontoCerrados" runat="server" DbValue='<%# Bind("Met_MontCerrado") %>'
                                                Width="90px" MinValue="0" onKeyPress="SoloNumerico" MaxLength="9">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <AlternatingItemStyle CssClass="tr_2" />
                            </MasterTableView>
                            <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                NextPagesToolTip="Páginas siguientes" PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente"
                                PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:LinkButton ID="lnkGuardar" runat="server" class="btn_guardar" OnClick="lnkGuardar_Click">GUARDAR</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <telerik:RadWindowManager ID="RadWindowManagerMaster" runat="server" Style="z-index: 7001">
                <Windows>
                   
                </Windows>
            </telerik:RadWindowManager>
            <!--Inicia footer-->
            <div style="padding-bottom: 30px;">
                <div class="footer">
                    <table width="910" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="274" height="27">
                                Derechos Reservados Key Qu&iacute;mica S.A de C.V.
                            </td>
                            <td>
                             <%--   <table border="0" align="right" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <a href="#">&gt; URL M&oacute;dulo CRM</a>
                                        </td>
                                        <td>
                                            <a href="#">&gt; URL M&oacute;dulo RP</a>
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
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
                    function SoloNumerico(sender, eventArgs) {
                        var c = eventArgs.get_keyCode();
                        if (c < 48 || c > 57) //si no es numero
                            eventArgs.set_cancel(true);
                    }            
                </script>
            </telerik:RadCodeBlock>
        </div>
    </form>
</body>
</html>
