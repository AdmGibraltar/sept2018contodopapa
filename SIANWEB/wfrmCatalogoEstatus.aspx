<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmCatalogoEstatus.aspx.cs"
    Inherits="SIANWEB.CrmEstatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Pragma" content="no-cache"> 
<meta http-equiv="no-cache"> 
<meta http-equiv="Expires" content="-1"> 
<meta http-equiv="Cache-Control" content="no-cache"> 
<meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title></title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body class="twoColHybLtHdr">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDeshacer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnCrear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnl1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <!--Inicia header-->
    <div class="header">
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
    <div id="container" class="centrador">
        <div runat="server" id="mainContent" align="center" class="contenido">
            <table align="center" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="CATÁLOGO DE ESTATUS"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="pnl1" Visible="False">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" width="120">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td width="106">
                                    </td>
                                    <td width="80">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td width="10">
                                        &nbsp;
                                    </td>
                                    <td width="100">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblAreas" runat="server" Text="Estatus:"></asp:Label>
                                    </td>
                                    <td style="width: 5px">
                                        &nbsp;
                                    </td>
                                    <td align="left" colspan="2">
                                        <telerik:RadTextBox ID="txtPosicion" runat="server" TabIndex="3" Width="184px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPosicion"
                                            Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left" rowspan="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" rowspan="2">
                                        <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="Imagenes/disk_blue.png"
                                            OnClick="btnAgregar_Click" TabIndex="5" ToolTip="Agregar estatus" ValidationGroup="guardar" />
                                        <asp:ImageButton ID="btnDeshacer" runat="server" CausesValidation="False" ImageUrl="Imagenes/undo.png"
                                            OnClick="btnDeshacer_Click" TabIndex="6" ToolTip="Deshacer cambios " />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td style="width: 5px">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="Label12" runat="server" Text="Registrar nuevo estatus"></asp:Label>
                        <asp:ImageButton ID="ibtnCrear" runat="server" AlternateText="Registrar Oportunidad"
                            ImageUrl="Imagenes/document_new.png" OnClick="ibtnCrear_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="rg1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            GridLines="None" OnItemCommand="rg1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource"
                            OnPageIndexChanged="rg1_PageIndexChanged" PageSize="15" CssClass="tr_1">
                            <MasterTableView NoMasterRecordsText="No se encontraron registros.">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Estatus" HeaderText="Clave" UniqueName="Id_Estatus">
                                        <HeaderStyle Width="70px" CssClass="tr_tit" />
                                        <ItemStyle HorizontalAlign="center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Es_Descripcion" HeaderText="Descripción" UniqueName="Es_Descripcion">
                                        <HeaderStyle Width="300px" CssClass="tr_tit" />
                                        <ItemStyle HorizontalAlign="center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                        UniqueName="DeleteColumn" ImageUrl="img\ic_trash.jpg">
                                        <HeaderStyle Width="30px" CssClass="tr_tit" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                        </telerik:RadGrid>
                        <asp:Label ID="lblMensajes" runat="server" Font-Italic="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
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
                       <%-- <table border="0" align="right" cellpadding="0" cellspacing="0">
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
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
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
                "Cancel": "No",
                "Yes": "Si",
                "No": "No"
            };

           
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
