<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmPrincipalClientes.aspx.cs"
    Inherits="SIANWEB.CrmCatCliente" %>

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
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbUEN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbSegmento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnFiltro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <div class="centrador">
        <div id="centrador">
            <!--Inicia header-->
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
            <!--Inicia contenido-->
            <div id="contenido" class="contenido" runat="server">
                <br />
                <div class="detalle_cliente">
                    <div class="filtro_t1">
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" height="130">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" Text="UEN:" Font-Bold="true" />
                                            </td>
                                            <td align="right" width="5">
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbUEN" runat="server" TabIndex="1" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cmbUEN_SelectedIndexChanged" controltovalidate="ddlUEN"
                                                    Width="180px" MaxHeight="250px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Segmento:" Font-Bold="true" />
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbSegmento" runat="server" TabIndex="2" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cmbSegmento_SelectedIndexChanged" Width="180px" MaxHeight="250px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td>
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
                                            <td align="right">
                                            </td>
                                            <td align="right" width="5">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Territorio:" Font-Bold="true" />
                                            </td>
                                            <td align="right" width="5">
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox ID="cmbTerritorios" runat="server" TabIndex="3" Width="180px"
                                                    MaxHeight="250px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="No. de cliente:" Font-Bold="true" />
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                                <telerik:RadNumericTextBox ID="txtNoCliente" runat="server" MaxLength="9" MinValue="1"
                                                    TabIndex="4" Width="70px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" Text="Nombre del cliente:" Font-Bold="True" />
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                                <telerik:RadTextBox ID="txtNombre" runat="server" Width="200px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtnFiltro" runat="server" ImageUrl="img/btn_filtro.gif" OnClick="ibtnFiltro_Click"
                                                    TabIndex="5" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table>
                    <tr>
                        <td style="font-weight: bold" align="center">
                            <asp:Label ID="lblMensajes" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tabla_detalles">
                                <div class="tit_azul">
                                    <asp:Label ID="Label5" runat="server" Text="LISTADO DE CLIENTES"></asp:Label></div>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="tabla1">
                                        <telerik:RadGrid ID="rg1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            OnNeedDataSource="rg1_NeedDataSource" PageSize="15" Width="872px" OnItemDataBound="rg1_ItemDataBound"
                                            OnSortCommand="rg1_SortCommand" CssClass="tr_1" TabIndex="6">
                                            <SortingSettings SortToolTip="Click aqui para ordenar" />
                                            <MasterTableView NoMasterRecordsText="No se encontraron registros." ClientDataKeyNames="Id_Cte,Id_Uen,Id_Seg,Id_Terr">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Número de cliente" UniqueName="Id_Cte">
                                                        <HeaderStyle Width="50px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn DataField="Cte_NomComercial" HeaderText="Nombre cliente"
                                                        UniqueName="Cte_NomComercial">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkNombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Cte_NomComercial") %>'>LinkButton</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle Width="250px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="UnidadDimension" HeaderText="Unidad dimensión"
                                                        UniqueName="UnidadDimension">
                                                        <HeaderStyle Width="200px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Dimension" HeaderText="Dimensión" UniqueName="Dimension">
                                                        <HeaderStyle Width="70px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="VPTeorico" DataType="System.Decimal" DataFormatString="{0:C}"
                                                        HeaderText="Valor potencial estimado" UniqueName="VPTeorico">
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="VPObservado" HeaderText="Valor potencial observado"
                                                        UniqueName="VPObservado" DataFormatString="{0:C}">
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Id_Uen" HeaderText="Id_Uen" UniqueName="Id_Uen"
                                                        Display="false">
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Id_Seg" HeaderText="Id_Seg" UniqueName="Id_Seg"
                                                        Display="false">
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Id_Terr" HeaderText="Id_Terr" UniqueName="Id_Terr"
                                                        Display="false">
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </MasterTableView>
                                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                        </telerik:RadGrid>
                                    </div>
                                </asp:Panel>
                            </div>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:LinkButton ID="lnkDescargar" runat="server" OnClick="lnkDescargar_Click" TabIndex="7"
                                Visible="False">Descargar formato alta clientes</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
                <br />
            </div>
            <!--Inicia footer-->
            <div style="padding-bottom: 30px;">
                <div class="footer">
                    <table width="910" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="274" height="27">
                                <asp:Label ID="Label6" runat="server" Text="Derechos Reservados Key Qu&iacute;mica S.A de C.V."></asp:Label>
                            </td>
                            <td>
                                <%--  <table border="0" align="right" cellpadding="0" cellspacing="0">
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
        </div>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function txt1_OnBlur(sender, args) {
                    OnBlur(sender, $find('<%= cmbUEN.ClientID %>'));
                }
                function txt2_OnBlur(sender, args) {
                    OnBlur(sender, $find('<%= cmbSegmento.ClientID %>'));
                }
                function txt3_OnBlur(sender, args) {
                    OnBlur(sender, $find('<%= cmbTerritorios.ClientID %>'));
                }
                function OnBlur(textbox, combo) {
                    //debugger;
                    try {
                        if (combo.get_items().getItem(0) == null) {
                            return;
                        }

                        var itemSelect = false;


                        for (var i = 0; i < combo.get_items().get_count(); i++) {
                            var item = combo.get_items().getItem(i);
                            if (textbox.get_value() == item.get_value()) {
                                itemSelect = true;
                                if (textbox.get_value() != combo.get_value()) {
                                    combo.get_items().getItem(i).select();
                                }
                                break;
                            }
                        }

                        if (!itemSelect) {
                            if (combo.get_value() != '-1') {
                                combo.get_items().getItem(0).select()
                            }
                            else {
                                textbox.set_value('');
                            }
                        }
                    }
                    catch (error) {
                    }

                }
                function refreshGrid() {
                    //debugger;

                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    ajaxManager.ajaxRequest('RebindGrid');

                }
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>
