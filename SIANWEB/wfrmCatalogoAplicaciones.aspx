<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmCatalogoAplicaciones.aspx.cs"
    Inherits="SIANWEB.CrmCatAplicaciones" %>

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
<body class="twoColHybLtHdr">
    <form id="form1" runat="server">
    <telerik:radscriptmanager id="RadScriptManager1" runat="server">
    </telerik:radscriptmanager>
    <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlUENs">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSegmentos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlAreas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSol">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDeshacer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent"  LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnCrear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAplicaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
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
    <div id="container" class="centrador">
        <div id="mainContent" runat="server" align="center" class="contenido">
            <table align="center" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="CATÁLOGO DE APLICACIONES"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" id="tbCombos" runat="server">
                            <tr>
                                <td align="right" width="140">
                                    <asp:Label ID="Label11" runat="server" Text="UEN:"></asp:Label>
                                </td>
                                <td style="width: 5px; height: 22px;">
                                </td>
                                <td style="height: 22px" align="left" colspan="4">
                                    <telerik:radcombobox id="ddlUENs" runat="server" autopostback="True" changetextonkeyboardnavigation="true"
                                        datatextfield="Descripcion" datavaluefield="Id" enableloadondemand="true" filter="Contains"
                                        highlighttemplateditems="true" loadingmessage="Cargando..." markfirstmatch="true"
                                        onselectedindexchanged="cmbUEN_SelectedIndexChanged" width="250px" maxheight="250px"
                                        onclientload="cmb1_Load" onclientblur="txt1_OnBlur">
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
                                    </telerik:radcombobox>
                                </td>
                                <td width="80">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Segmento:"></asp:Label>
                                </td>
                                <td style="width: 5px">
                                </td>
                                <td align="left" colspan="4">
                                    <telerik:radcombobox id="ddlSegmentos" runat="server" datatextfield="Descripcion"
                                        datavaluefield="Id" enableloadondemand="True" filter="Contains" highlighttemplateditems="True"
                                        loadingmessage="Cargando..." markfirstmatch="True" width="250px" autopostback="True"
                                        onselectedindexchanged="cmbSegmento_SelectedIndexChanged" maxheight="250px" onclientload="cmb2_Load"
                                        onclientblur="txt2_OnBlur">
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
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblArea" runat="server" Text="Área:"></asp:Label>
                                </td>
                                <td style="width: 5px">
                                </td>
                                <td align="left" colspan="4">
                                    <telerik:radcombobox id="ddlAreas" runat="server" changetextonkeyboardnavigation="true"
                                        datatextfield="Descripcion" datavaluefield="Id" enableloadondemand="true" filter="Contains"
                                        highlighttemplateditems="true" loadingmessage="Cargando..." markfirstmatch="true"
                                        width="250px" autopostback="True" onselectedindexchanged="ddlAreas_SelectedIndexChanged"
                                        maxheight="250px" onclientload="cmb3_Load" onclientblur="txt3_OnBlur">
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
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server" Text="Solución:"></asp:Label>
                                </td>
                                <td style="width: 5px">
                                </td>
                                <td align="left" colspan="4">
                                    <telerik:radcombobox id="ddlSol" runat="server" changetextonkeyboardnavigation="true"
                                        datatextfield="Descripcion" datavaluefield="Id" enableloadondemand="true" filter="Contains"
                                        highlighttemplateditems="true" loadingmessage="Cargando..." markfirstmatch="true"
                                        width="250px" autopostback="True" onselectedindexchanged="ddlSol_SelectedIndexChanged"
                                        maxheight="250px" onclientload="cmb4_Load" onclientblur="txt4_OnBlur">
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
                                    </telerik:radcombobox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr runat="server" id="trAreas" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblAreas" runat="server" Text="Aplicación:"></asp:Label>
                                </td>
                                <td style="width: 5px">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <telerik:radtextbox id="txtPosicion" runat="server" width="184px" tabindex="3">
                                    </telerik:radtextbox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPosicion"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr runat="server" id="trPotencial" visible="false">
                                <td align="right">
                                    <asp:Label ID="lblPPotencial" runat="server" Text="Porcentaje potencial:"></asp:Label>
                                </td>
                                <td style="width: 5px">
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:radnumerictextbox id="txtPotencial" runat="server" width="104px" tabindex="4"
                                        maxlength="9" maxvalue="100" minvalue="0">
                                    </telerik:radnumerictextbox>
                                </td>
                                <td>
                                    <asp:Label ID="lblPorcentaje" runat="server" Text="%"></asp:Label>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPotencial"
                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td rowspan="2" width="80">
                                    <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="Imagenes/disk_blue.png"
                                        ToolTip="Agregar aplicación" TabIndex="5" OnClick="btnAgregar_Click" ValidationGroup="guardar" />
                                    <asp:ImageButton ID="btnDeshacer" runat="server" CausesValidation="False" ImageUrl="Imagenes/undo.png"
                                        ToolTip="Deshacer cambios " TabIndex="6" OnClick="btnDeshacer_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td style="width: 5px">
                                    &nbsp;
                                </td>
                                <td width="107">
                                    &nbsp;
                                </td>
                                <td width="10">
                                    &nbsp;
                                </td>
                                <td width="70">
                                    &nbsp;
                                </td>
                                <td width="70">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:HiddenField ID="HF_Modificar" runat="server" />
                    </td>
                    <td align="right">
                        &nbsp;<asp:Panel ID="pnlAgrega" runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Registrar nueva aplicación"></asp:Label>&nbsp;
                            <asp:ImageButton ID="ibtnCrear" runat="server" AlternateText="Registrar Oportunidad"
                                ImageUrl="Imagenes/document_new.png" OnClick="ibtnCrear_Click" /></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:radgrid id="rgAplicaciones" runat="server" autogeneratecolumns="False" gridlines="None"
                            width="600px" onneeddatasource="rgAplicaciones_NeedDataSource" onitemcommand="rgAplicaciones_ItemCommand"
                            onpageindexchanged="rgAplicaciones_PageIndexChanged" onitemdatabound="rgAplicaciones_ItemDataBound"
                            pagesize="15" allowpaging="True" mastertableview-nomasterrecordstext="No se encontraron registros."
                            cssclass="tr_1">
                            <MasterTableView ClientDataKeyNames="Id_Apl">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Id_Apl" HeaderText="Clave" UniqueName="Clave"
                                        Visible="false">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id_Apl" HeaderText="Clave" UniqueName="Clave">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" Width="70px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Apl_Descripcion" HeaderText="Aplicación" UniqueName="Solucion">
                                        <HeaderStyle CssClass="tr_tit" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Apl_Potencial" HeaderText="Porcentaje(%)" DataFormatString="{0:N2}"
                                        UniqueName="Porcentaje">
                                        <ItemStyle HorizontalAlign="center" />
                                        <HeaderStyle Width="100px" CssClass="tr_tit" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="img\ic_edit.jpg"
                                                ToolTip="Editar" CommandName="Modificar" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Borrar"
                                        ImageUrl="img\ic_trash.jpg" UniqueName="DeleteColumn">
                                        <HeaderStyle Width="70px" CssClass="tr_tit" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                NextPagesToolTip="Páginas siguientes" PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente"
                                PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                        </telerik:radgrid>
                        <asp:Label ID="lblMensajes" runat="server" Font-Italic="True"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
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
    <!--Termina footer-->
    <telerik:radwindowmanager id="RadWindowManager1" runat="server">
    </telerik:radwindowmanager>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
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

            var cmb1;
            var cmb2;
            var cmb3;
            var cmb4;

            function cmb1_Load(sender) {
                cmb1 = sender;
            }
            function cmb2_Load(sender) {
                cmb2 = sender;
            }
            function cmb3_Load(sender) {
                cmb3 = sender;
            }
            function cmb4_Load(sender) {
                cmb4 = sender;
            }
            function txt1_OnBlur(sender, args) {
                OnBlur(sender, cmb1);
            }
            function txt2_OnBlur(sender, args) {
                OnBlur(sender, cmb2);
            }
            function txt3_OnBlur(sender, args) {
                OnBlur(sender, cmb3);
            }
            function txt4_OnBlur(sender, args) {
                OnBlur(sender, cmb4);
            }
            function OnBlur(textbox, combo) {
                //debugger;

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
        </script>
    </telerik:radcodeblock>
    </form>
</body>
</html>
