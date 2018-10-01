<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmCatCliente.aspx.cs"
    Inherits="SIANWEB.CrmCatCliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title></title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbUEN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbSegmento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnFiltro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
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
                    <img src="img/btn_inicio.jpg" /><img src="img/btn_cerrar_sesion.jpg" /></div>
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
                                            <td>
                                                <strong>UEN:</strong>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbUEN" runat="server" AutoPostBack="True" DataTextField="Descripcion"
                                                    DataValueField="Id" EnableLoadOnDemand="True" Filter="Contains" HighlightTemplatedItems="True"
                                                    LoadingMessage="Cargando..." MarkFirstMatch="True" OnSelectedIndexChanged="cmbUEN_SelectedIndexChanged"
                                                    Width="250px">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Segmento:</strong>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbSegmento" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                    Width="250px" AutoPostBack="True" OnSelectedIndexChanged="cmbSegmento_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="LabelID0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
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
                                                <telerik:RadComboBox ID="cmbTerritorios" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" Filter="Contains"
                                                    HighlightTemplatedItems="true" LoadingMessage="Cargando..." MarkFirstMatch="true"
                                                    Width="250px" AutoPostBack="True" OnSelectedIndexChanged="cmbSegmento_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="LabelID0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC0" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>No. de Cliente:</strong>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtNoCliente" runat="server">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtnFiltro" runat="server" ImageUrl="img/btn_filtro.gif" OnClick="ibtnFiltro_Click" />
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
                                    LISTADO DE CLIENTES</div>
                                <div class="tabla1">
                                    <telerik:RadGrid ID="rg1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        GridLines="None" OnNeedDataSource="rg1_NeedDataSource" PageSize="15" Width="892px"
                                        AllowSorting="True" Height="248px" OnItemDataBound="rg1_ItemDataBound">
                                        <SortingSettings SortToolTip="Click aqui para ordenar" />
                                        <MasterTableView NoMasterRecordsText="No se encontraron registros." ClientDataKeyNames="Id_Cte">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Clave" UniqueName="Id_Cte">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="Cte_NomComercial" HeaderText="Nombre cliente"
                                                    UniqueName="Cte_NomComercial">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkNombre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Cte_NomComercial") %>'>LinkButton</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="150px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="UnidadDimension" HeaderText="Unidad dimensión"
                                                    UniqueName="UnidadDimension">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Dimension" HeaderText="Dimension" UniqueName="Dimension">
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VPTeorico" DataType="System.Decimal" DataFormatString="{0:C}"
                                                    HeaderText="Valor potencial estimado" UniqueName="VPTeorico">
                                                    <HeaderStyle Width="75px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VPObservado" HeaderText="Valor potencial observado"
                                                    UniqueName="VPObservado" DataFormatString="{0:C}">
                                                    <HeaderStyle Width="75px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </MasterTableView>
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                            PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                            PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                    </telerik:RadGrid>
                                    <clientsettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="248px">
                                                </Scrolling>
                                            </clientsettings>
                                </div>
                            </div>
                            &nbsp;&nbsp;<br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:LinkButton ID="lnkDescargar" runat="server" OnClick="lnkDescargar_Click">Descargar Formato Alta Clientes</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                &nbsp; &nbsp;<br />
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
                                Derechos Reservados Key Qu&iacute;mica S.A de C.V.
                            </td>
                            <td>
                                <table border="0" align="right" cellpadding="0" cellspacing="0">
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
                                </table>
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
