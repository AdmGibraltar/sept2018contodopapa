<%@ Page Title="Control de proyectos de promoción" Language="C#" AutoEventWireup="true"
    CodeBehind="wfrmPrincipaloportunidades.aspx.cs" Inherits="SIANWEB.wfrmPrincipaloportunidades" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlCDS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlRik">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlUENS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSegmento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSolucion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlAplicacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlEstatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnFiltro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnNuevaOportunidad">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dgControlPromocion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <%--divGrid--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
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
                    <div class="header">
                        <div class="menu_sesion">
                            <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="img/btn_inicio.jpg" NavigateUrl="wfrmInicioGeneral.aspx"></asp:HyperLink>
                            <a href="http://localhost:3270/Login.aspx?Id=1">
                                <asp:Image ID="Image1" runat="server" ImageUrl="img/btn_cerrar_sesion.jpg" /></a>
                        </div>
                        <div class="logo">
                            <img alt="" src="http://localhost:3270/img/key_logo.jpg" /></div>
                        <div class="menu">
                            <script type="text/javascript">
                                AC_FL_RunContent('codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0', 'width', '472', 'height', '86', 'src', 'swf/menu', 'quality', 'high', 'pluginspage', 'http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash', 'movie', 'swf/menu'); //end AC code
                            </script>
                            <noscript>
                                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
                                    height="86" width="472">
                                    <param name="movie" value="swf/menu.swf" />
                                    <param name="quality" value="high" />
                                    <embed height="86" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash"
                                        quality="high" src="swf/menu.swf" type="application/x-shockwave-flash" width="472"></embed>
                                </object>
                            </noscript>
                        </div>
                    </div>
                </noscript>
            </div>
        </div>
    </div>
    <div id="principal" runat="server" class="contenido">
        <div class="tit_secc">
            <img alt="" src="img/tit_control_proyectos.gif" /></div>
        <asp:Panel ID="pnlPrincipal2" runat="server">
            <div class="filto_proyectos">
                <div class="filtro_t1">
                    <table border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="130" align="center">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left">
                                            <label id="labelcd" runat="server" visible="false">
                                                <strong>CD:</strong></label>
                                        </td>
                                        <td align="left" width="5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlCDS" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlCDS_SelectedIndexChanged" TabIndex="1" Visible="false"
                                                Width="220px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label id="labelrik" runat="server" visible="false">
                                                <strong>Representante:</strong></label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlRik" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlRik_SelectedIndexChanged" TabIndex="2" Visible="false"
                                                Width="220px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>UEN:</strong>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlUENS" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlUENS_SelectedIndexChanged" TabIndex="3" Width="220px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Segmento:</strong>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlSegmento" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlSegmento_SelectedIndexChanged" TabIndex="4" Width="220px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Territorio:</strong>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlTerritorio" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                TabIndex="4" Width="220px" OnSelectedIndexChanged="ddlTerritorio_SelectedIndexChanged"
                                                HighlightTemplatedItems="True">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 25px; text-align: center; vertical-align: top">
                                                                <asp:Label ID="LabelID" runat="server" Width="25px" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
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
                                </table>
                                <asp:Label ID="lblTer" runat="server"></asp:Label>
                                <asp:Label ID="lblRep" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="filtro_busqueda">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="40">
                                <table border="0" cellpadding="0" cellspacing="0" width="332px">
                                    <tr>
                                        <td width="73" height="40px" align="left" background="img/bg_lupa.jpg" class="blanco">
                                            <strong style="padding-left: 10px;">Cliente:</strong>
                                        </td>
                                        <td width="224" height="40px" background="img/bg_lupa.jpg">
                                            <%-- <telerik:RadNumericTextBox ID="txtNoCliente" runat="server"   MinValue="0"
                                                MaxLength="9" Type="Number" Enabled="true" CssClass="sel2" AutoPostBack="true"
                                                OnTextChanged="txtNoCliente_TextChanged" TabIndex="6">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>--%>
                                            <telerik:RadTextBox ID="txtCliente" runat="server" Width="210px" CssClass="sel2">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="35px" height="40px">
                                            <asp:ImageButton ID="ibtnBuscarCliente" ImageUrl="img/lupa.JPG" runat="server" Width="35"
                                                Height="40" OnClick="ibtnBuscarCliente_Click" ToolTip="Buscar cliente" TabIndex="7"
                                                BorderStyle="None" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="filtro_t2">
                    <table border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="130" align="center">
                                <table width="300px" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="65" align="left">
                                            <strong>&Aacute;rea:</strong>
                                        </td>
                                        <td align="left" width="5">
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <telerik:RadComboBox ID="ddlArea" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" TabIndex="8" Width="220px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Soluci&oacute;n:</strong>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <telerik:RadComboBox ID="ddlSolucion" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlSolucion_SelectedIndexChanged" TabIndex="9" Width="220px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Aplicaci&oacute;n:</strong>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <telerik:RadComboBox ID="ddlAplicacion" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlAplicacion_SelectedIndexChanged" TabIndex="10" Width="220px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Estatus:</strong>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <telerik:RadComboBox ID="ddlEstatus" runat="server" AutoPostBack="true" MaxHeight="250px"
                                                OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged" TabIndex="11" Width="220px">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="-1" Text="-- Todos --" />
                                                    <telerik:RadComboBoxItem Value="-2" Text="Activos(A/P/N)" />
                                                    <telerik:RadComboBoxItem Value="1" Text="Análisis" />
                                                    <telerik:RadComboBoxItem Value="2" Text="Promoci&#243;n" />
                                                    <telerik:RadComboBoxItem Value="3" Text="Negociación" />
                                                    <telerik:RadComboBoxItem Value="4" Text="Cerrados" />
                                                    <telerik:RadComboBoxItem Value="5" Text="Cancelados" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="lblMensajeFiltro" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="filtro_boton">
                    <asp:LinkButton ID="ibtnFiltro" class="btn_apl_filt" runat="server" OnClick="ibtnFiltro_Click"
                        TabIndex="12">APLICAR FILTRO</asp:LinkButton>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlPrincipal" runat="server">
            <div class="tabla_control_promocion" style="width: 920px">
                <div>
                    <asp:Label ID="lblCriterios" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblEtapa" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </div>
                <div class="botones_tabla">
                    <asp:LinkButton class="btn_add" ID="ibtnNuevaOportunidad" PostBackUrl="wfrmRegistrarOportunidad.aspx"
                        runat="server" TabIndex="14">REGISTRAR NUEVO PROYECTO</asp:LinkButton>
                    <asp:LinkButton class="btn_exp" ID="ibtnExplicar" runat="server" TabIndex="13" Visible="False">EXPLICACIÓN DE TABLA</asp:LinkButton>
                </div>
                <div class="tit_rojo">
                    <img alt="" src="img/tit_control.jpg" />
                </div>
                <div id="divGrid" runat="server">
                    <telerik:RadGrid ID="dgControlPromocion" runat="server" OnNeedDataSource="rg1_NeedDataSource"
                        AutoGenerateColumns="False" AllowSorting="True" datakeyfield="Id" CellPadding="0"
                        Width="862px" CssClass="tr_1" TabIndex="14" PageSize="15" OnPageIndexChanged="rg1_PageIndexChanged"
                        OnItemDataBound="rg1_ItemDataBound" OnItemCommand="rg1_ItemCommand" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnSortCommand="dgControlPromocion_SortCommand" SortingSettings-SortedAscToolTip="Ordenado ascendente"
                        SortingSettings-SortedDescToolTip="Ordenado descedente" GridLines="None" EnableViewState="False">
                        <MasterTableView ClientDataKeyNames="Id" EnableViewState="False">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Clave" DataField="Id" UniqueName="Clave"
                                    SortExpression="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOp" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtOp" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle Width="40px" CssClass="tr_tit" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="No. cliente" DataField="Id_Cte" UniqueName="Id_Cte"
                                    SortExpression="Id_Cte">
                                    <HeaderStyle Width="70px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Cliente" DataField="NombreCte" UniqueName="Cliente"
                                    SortExpression="NombreCte">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkNombre" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.NombreCte") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" CssClass="tr_tit" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Oportunidad" DataField="Descripcion" UniqueName="Descripcion"
                                    SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkID" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.Descripcion") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" CssClass="tr_tit" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Potencial" DataField="Cli_VPObservado" UniqueName="MontoProyecto"
                                    SortExpression="Cli_VPObservado" DataFormatString="{0:N2}">
                                    <HeaderStyle Width="80px" CssClass="tr_tit" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="A" DataField="Analisis" UniqueName="Analisis">
                                    <HeaderStyle Width="100px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="P" DataField="Presentacion" UniqueName="Presentacion"
                                    SortExpression="Presentacion">
                                    <HeaderStyle Width="100px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="N" DataField="Negociacion" UniqueName="Negociacion"
                                    SortExpression="Negociacion">
                                    <HeaderStyle Width="100px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="C" DataField="Cierre" UniqueName="Cierre" SortExpression="Cierre">
                                    <HeaderStyle Width="100px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="x" DataField="FechaCancelacion" UniqueName="FechaCancelacion"
                                    SortExpression="FechaCancelacion" Visible="false">
                                    <HeaderStyle Width="80px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="X" DataField="Cancelacion" UniqueName="Cancelacion">
                                    <ItemTemplate>
                                        <asp:Image ID="btnCancelacion" runat="server" ToolTip='<%# "Motivo de cancelación: " + DataBinder.Eval(Container, "DataItem.Cancelacion") %>'
                                            ImageUrl="img\x.gif" Visible='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Avances"))==5 ? true : false%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" CssClass="tr_tit" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Avance" DataField="Avances" UniqueName="Avances"
                                    SortExpression="Avances" Display="false">
                                    <HeaderStyle Width="56px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Avance" DataField="MesModificacion" UniqueName="Avances"
                                    SortExpression="MesModificacion">
                                    <HeaderStyle Width="56px" CssClass="tr_tit" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Segmento" DataField="Segmento" UniqueName="Segmento"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Id_Ter" DataField="Id_Ter" UniqueName="Id_Ter"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cds" DataField="Cds" UniqueName="Cds" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Ids" DataField="Ids" UniqueName="Ids" Visible="False">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle Mode="NumericPages" />
                        <FooterStyle BackColor="PowderBlue" CssClass="DataGridFixedfooter" BorderColor="PowderBlue" />
                        <SortingSettings SortToolTip="Presione para ordenar" />
                        <AlternatingItemStyle CssClass="tr_2" />
                        <HeaderStyle ForeColor="White" />
                        <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            NextPagesToolTip="Páginas siguientes" PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente"
                            PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>
                </div>
                <div class="tabla_totales">
                    <table width="885" border="0" cellpadding="0" cellspacing="1">
                        <tr class="tr_tot1">
                            <td width="260px" bgcolor="#FFFFFF">
                            </td>
                            <td width="100px">
                                <strong>TOTAL</strong>
                            </td>
                            <td colspan="4">
                                <strong>MONTO POR ETAPA</strong>
                            </td>
                            <td width="100px">
                                <strong>X</strong>
                            </td>
                            <td width="56px">
                                <p>
                                    <strong>AVANCE</strong></p>
                            </td>
                        </tr>
                        <tr class="tr_tot2">
                            <td bgcolor="#FFFFFF" style="height: 16px">
                                &nbsp;
                                <asp:Label ID="lblA" runat="server"></asp:Label>
                                <asp:Label ID="lblP" runat="server"></asp:Label>
                                <asp:Label ID="lblN" runat="server"></asp:Label>
                                <asp:Label ID="lblC" runat="server"></asp:Label>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                <asp:Label ID="lblTot" runat="server"></asp:Label>
                                <asp:Label ID="lblActivos" runat="server"></asp:Label>
                                <asp:Label ID="lblInactivos" runat="server"></asp:Label>
                                <asp:Label ID="lblCancelados" runat="server"></asp:Label>
                                <asp:Label ID="lblMontoActivos" runat="server"></asp:Label>
                                <asp:Label ID="lblMontoInactivos" runat="server"></asp:Label>
                            </td>
                            <td width="100px" style="height: 16px">
                                <asp:Label ID="txtTot" runat="server"></asp:Label>
                            </td>
                            <td width="100px" style="height: 16px">
                                <asp:Label ID="txtA" runat="server"></asp:Label>
                            </td>
                            <td width="100px" style="height: 16px">
                                <asp:Label ID="txtP" runat="server"></asp:Label>
                            </td>
                            <td width="100px" style="height: 16px">
                                <asp:Label ID="txtN" runat="server"></asp:Label>
                            </td>
                            <td width="100px" style="height: 16px">
                                <asp:Label ID="txtC" runat="server"></asp:Label>
                            </td>
                            <td width="100px" style="height: 16px">
                                <asp:Label ID="txtX" runat="server"></asp:Label>
                            </td>
                            <td style="height: 16px; width: 56px">
                                <asp:Label ID="txtAvance" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </div>
        </asp:Panel>
    </div>
    <!--Termina contenido-->
    <!--Inicia footer-->
    <div style="padding-bottom: 30px;">
        <div class="footer">
            <table width="910" border="0" cellpadding="0" cellspacing="0">
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
    <telerik:RadWindowManager ID="RadWindowManagerMaster" runat="server" Style="z-index: 7001">
        <Windows>
            <%-- Crm promocion --%>
            <telerik:RadWindow ID="AbrirVentana_promocion" runat="server" Behaviors="Move, Close"
                Opacity="100" VisibleStatusbar="false" Width="840px" Height="500px" Animation="Fade"
                KeepInScreenBounds="true" Overlay="true" Title="" Modal="true" OnClientClose="OnClientClose"
                ShowContentDuringLoad="false" ReloadOnShow="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //--------------------------------------------------------------------------------------------------          
            //--------------------------------------------------------------------------------------------------
            //Abre la ventana de edición, cuando se edita seleccionandola del grid
            //--------------------------------------------------------------------------------------------------
            function AbrirVentana(cte, ter, uen, rik, seg) {
                AbrirVentana(cte, ter, uen, rik, seg);
            }
            function AbrirVentana(cte, ter, uen, rik, seg) {
                var oWnd = radopen("Crmpromocion_ventana.aspx?cte=" + cte + "&ter=" + ter + "&uen=" + uen + "&rik=" + rik + "&seg=" + seg, "AbrirVentana_promocion");
                oWnd.center();
            }
            //----------------------------------------------------------------------------------------------------
            function SoloNumerico(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (c && c == 13)
                    eventArgs.set_cancel(true);
                if (c < 48 || c > 57) //si no es numero
                    eventArgs.set_cancel(true);
            }
            function Update(elemValue) {
                parent.document.getElementBy("txtNoCliente").value = elemValue;
                document.getElementById("HiddenField1").value = elemValue;
            }
            //--------------------------------------------------------------------------------------------------
            // Actualiza el Grid cuando se cierra la ventana de detalle
            //--------------------------------------------------------------------------------------------------           
            function refreshGrid() {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }



            function popup(Cds, Cte) {
                var oWnd = radopen("Crmpromocion_ventana.aspx?cd=" + Cds + "&cte=" + Cte, "AbrirVentana_promocion");
                oWnd.center();
            }
            function OnClientClose(oWnd, args) {
                //get the transferred arguments
                var arg = args.get_argument();
                if (arg) {
                    var noCliente = arg.noCliente;
                    //document.getElementById("txtNoCliente").value = noCliente;
                    document.getElementById("HiddenField1").value = noCliente;
                }
                __doPostBack('opcionA');
            }
            function select(hd1) {
                //document.getElementById("txtNoCliente").value = hd1;
                document.getElementById("HiddenField1").value = hd1;
            }
        </script>
    </telerik:RadCodeBlock>
    <!--Termina footer-->
    </form>
</body>
</html>
