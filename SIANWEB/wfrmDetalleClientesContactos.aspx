<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmDetalleClientesContactos.aspx.cs"
    Inherits="SIANWEB.wfrmDetalleClientesContactos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Pragma" content="no-cache"> 
<meta http-equiv="no-cache"> 
<meta http-equiv="Expires" content="-1"> 
<meta http-equiv="Cache-Control" content="no-cache"> 
 <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title></title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnNuevoContacto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rg2" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rg3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rg3" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
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
                    href="inicio.aspx"><asp:Image ID="ibtnCerrarSesion" runat="server"
                        ImageUrl="img/btn_cerrar_sesion.jpg" /></a>
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
        <div class="contenido" id="contenido" runat="server">
            <div class="tit_secc">
                <img src="img/tit_detalle_cliente.gif" /></div>
            <div id="DIV1">
                &nbsp;<br />
                <div class="detalle_cliente">
                    <div class="filtro_t1">
                        <table align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" height="130">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right">
                                                <strong>UEN:</strong>
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtUEN" runat="server" class="sel1" Width="170px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
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
                                        <tr>
                                            <td align="right">
                                                <strong>Segmento:</strong>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSegmento" runat="server" class="sel1" Width="170px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
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
                                            <td align="right">
                                                <strong>Territorio:</strong>
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtTerritorio" runat="server" class="sel1" Width="170px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTer" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
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
                                        <tr>
                                            <td align="right">
                                                <strong>Cliente:</strong>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCliente" runat="server" class="sel1" Width="170px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
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
                <table align="center">
                    <tr>
                        <td colspan="3" align="center" style="width: 958px">
                            &nbsp;
                            <div align="center">
                                <div class="detalle">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgContactos" runat="server" ImageUrl="img/div_stick_potencial.gif"
                                                    Width="289" Height="26" BorderWidth="0px" />
                                                <a href="#">
                                                    <img alt="" src="img/div_stick_contactos.gif" /></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="ibtnNuevoContacto" class="btn_add_conts" runat="server" CausesValidation="False"
                                                    OnClick="ibtnNuevoContacto_Click">REGISTRAR NUEVO CONTACTO</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <div class="tit_azul">
                                                                ESTRUCTURA GERENCIAL</div>
                                                            <telerik:RadGrid ID="rg1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                GridLines="None" PageSize="15" Width="892px" Height="248px" OnItemCommand="rg1_ItemCommand"
                                                                OnNeedDataSource="rg1_NeedDataSource" OnSortCommand="rg1_SortCommand">
                                                                <SortingSettings SortToolTip="Click aqui para ordenar" />
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                                </ClientSettings>
                                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." ClientDataKeyNames="ContactoID">
                                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Posicion" HeaderText="Posición" SortExpression="Posicion">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Correo" HeaderText="Correo" SortExpression="Correo">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Celular" HeaderText="Celular" SortExpression="Celular">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="PosicionID" HeaderText="PosicionID" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ContactoID" HeaderText="ContactoID" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" Text="Editar"
                                                                            ImageUrl="img\ic_edit.jpg" UniqueName="EditColumn">
                                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                                            ImageUrl="img\ic_trash.jpg" UniqueName="DeleteColumn">
                                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridBoundColumn DataField="ContactoID" HeaderText="ContactoID" SortExpression="ContactoID"
                                                                            Display="false">
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                            </telerik:RadGrid>
                                                            <br />
                                                            <div class="tit_azul">
                                                                ESTRUCTURA DE COMPRAS</div>
                                                            <telerik:RadGrid ID="rg2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                GridLines="None" PageSize="15" Width="892px" Height="248px" OnItemCommand="rg2_ItemCommand"
                                                                OnNeedDataSource="rg2_NeedDataSource" OnSortCommand="rg2_SortCommand">
                                                                <SortingSettings SortToolTip="Click aqui para ordenar" />
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                                </ClientSettings>
                                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." ClientDataKeyNames="ContactoID">
                                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Posicion" HeaderText="Posición" SortExpression="Posicion">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Correo" HeaderText="Correo" SortExpression="Correo">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Celular" HeaderText="Celular" SortExpression="Celular">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="PosicionID" HeaderText="PosicionID" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ContactoID" HeaderText="ContactoID" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" Text="Editar"
                                                                            ImageUrl="img\ic_edit.jpg" UniqueName="EditColumn">
                                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                                            ImageUrl="img\ic_trash.jpg" UniqueName="DeleteColumn">
                                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridBoundColumn DataField="ContactoID" HeaderText="ContactoID" SortExpression="ContactoID"
                                                                            Display="false">
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                            </telerik:RadGrid>
                                                            <br />
                                                            <div class="tit_azul">
                                                                ESTRUCTURA DE USUARIOS INTERNOS
                                                            </div>
                                                            <telerik:RadGrid ID="rg3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                GridLines="None" PageSize="15" Width="892px" Height="248px" OnItemCommand="rg3_ItemCommand"
                                                                OnNeedDataSource="rg3_NeedDataSource" OnSortCommand="rg3_SortCommand">
                                                                <SortingSettings SortToolTip="Click aqui para ordenar" />
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                                                </ClientSettings>
                                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." ClientDataKeyNames="ContactoID">
                                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Posicion" HeaderText="Posición" SortExpression="Posicion">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" SortExpression="Nombres">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Correo" HeaderText="Correo" SortExpression="Correo">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Celular" HeaderText="Celular" SortExpression="Celular">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="PosicionID" HeaderText="PosicionID" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="ContactoID" HeaderText="ContactoID" Visible="False">
                                                                            <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Editar" Text="Editar"
                                                                            ImageUrl="img\ic_edit.jpg" UniqueName="EditColumn">
                                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                                                            ImageUrl="img\ic_trash.jpg" UniqueName="DeleteColumn">
                                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" CssClass="tr_tit" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridBoundColumn DataField="ContactoID" HeaderText="ContactoID" SortExpression="ContactoID"
                                                                            Display="false">
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </MasterTableView>
                                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        &nbsp; &nbsp; </td> </tr>
        <tr>
            <td colspan="3" style="width: 958px">
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
    <telerik:RadWindowManager ID="RadWindowManagerMaster" runat="server" Style="z-index: 7001">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_Nuevos" runat="server" Behaviors="Move,Close"
                Opacity="100" VisibleStatusbar="False" Width="670px" Height="662px" Animation="Fade"
                KeepInScreenBounds="True" Overlay="True" Title="Detalle del contacto" Modal="True"
                ShowContentDuringLoad="False">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            Telerik.Web.UI.RadWindowUtils.Localization =
            {
                "Close": "Cerrar",
                "Minimize": "Minimize",
                "Maximize": "Maximize",
                "Reload": "Reload",
                "PinOn": "PinOn",
                "PinOff": "v",
                "Restore": "Restore",
                "OK": "OK",
                "Cancel": "Cancel",
                "Yes": "Yes",
                "No": "No"
            };
            function AbrirVentana_Nuevos(Mov, PosNombre, Cte, Seg, Con, Pos, Tipo, grid) {
                //debugger;
                var oWnd = radopen("wfrmContactosNuevos.aspx?" +
                "Mov=" + Mov +
                "&PosNombre=" + PosNombre +
                "&Cte=" + Cte +
                "&Seg=" + Seg +
                "&Con=" + Con +
                "&Pos=" + Pos +
                "&Grid=" + grid +
                "&Tipo=" + Tipo, "AbrirVentana_Nuevos");
                oWnd.center();
            }

            function refreshGrid(grid) {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest(grid);
            }
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
