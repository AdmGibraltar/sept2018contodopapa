<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmPrincipalCampanias.aspx.cs"
    Inherits="SIANWEB.wfrmPrincipalCampanias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Pragma" content="no-cache"> 
<meta http-equiv="no-cache"> 
<meta http-equiv="Expires" content="-1"> 
<meta http-equiv="Cache-Control" content="no-cache"> 
<meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Key Química CRM</title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgCampanas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCampanas" LoadingPanelID="RadAjaxLoadingPanel1" />
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
            </div>
            <!--Termina header-->
            <!--Inicia contenido-->
            <div id="contenido" class="contenido" runat="server">
                <br />
                <table align="center">
                    <tr>
                        <td colspan="3" align="right">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="botones_tabla">
                                <asp:LinkButton class="btn_add" ID="ibtnNuevaOportunidad" runat="server" OnClick="ibtnNuevaOportunidad_Click">REGISTRAR NUEVA CAMPAÑA
                                </asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="tit_azul">
                                CAMPAÑAS DE PROMOCIÓN</div>
                            <telerik:RadGrid ID="rgCampanas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                PageSize="15" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                AllowPaging="True" HeaderStyle-HorizontalAlign="Center"
                                OnNeedDataSource="rgCampanas_NeedDataSource" OnPageIndexChanged="rgCampanas_PageIndexChanged"
                                OnItemCommand="rgCampanas_ItemCommand">
                                <SortingSettings SortedAscToolTip="Orden acendente" SortedDescToolTip="Orden decendente"
                                    SortToolTip="Click para reordenar" />
                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Lista" HideStructureColumns="true"
                                    ExportOnlyData="true">
                                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Lista" Title="Lista" />
                                </ExportSettings>
                                <MasterTableView DataKeyNames="Id_Emp,Id_Cam" CommandItemDisplay="none" PageSize="15">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <CommandItemSettings ExportToPdfText="Exportar a Pdf" AddNewRecordText="Agregar">
                                    </CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Id_Cam" HeaderText="ID" UniqueName="Id_Cam">
                                            <HeaderStyle Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="Cam_Nombre" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditarCampana" runat="server" ToolTip="Modificar" CommandName="Editar"
                                                    Text='<%# Eval("Cam_Nombre") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" UniqueName="DeleteColumn"
                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="img\x.gif" />
                                                <asp:LinkButton ID="lnkEliminarCampana" runat="server" CausesValidation="false" CommandName="Eliminar"
                                                    Text="Eliminar campaña"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Siguiente página"
                                    PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PrevPageToolTip="Página anterior" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                    ShowPagerText="True" PageButtonCount="3" />
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                            &nbsp;
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
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
        </div>
    </div>
    </form>
</body>
</html>
