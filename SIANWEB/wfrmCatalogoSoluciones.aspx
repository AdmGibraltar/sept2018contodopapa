<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmCatalogoSoluciones.aspx.cs"
    Inherits="SIANWEB.CrmCatSoluciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta http-equiv="Cache-Control" content="no-cache">
    <title>Solución</title>
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlUENs">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSegmentos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlAreas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDeshacer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnCrear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgAreas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="mainContent" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
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
        <div id="mainContent" align="center" class="contenido" runat="server">
            <table align="center" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblMensaje" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="CATÁLOGO DE SOLUCIONES"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="pnl1">
                            <table>
                                <tr>
                                    <td colspan="4" width="180px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="height: 22px">
                                        <asp:Label ID="Label11" runat="server" Text="UEN:"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left" style="height: 22px">
                                        <telerik:RadComboBox ID="ddlUENs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUENs_SelectedIndexChanged"
                                            Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            LoadingMessage="Cargando..." Width="224px" TabIndex="10" OnClientBlur="txt1_OnBlur"
                                            OnClientLoad="cmb1_Load">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="height: 22px">
                                        <asp:Label ID="Label2" runat="server" Text="Segmento:"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left" style="height: 22px">
                                        <telerik:RadComboBox ID="ddlSegmentos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSegmentos_SelectedIndexChanged"
                                            Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            LoadingMessage="Cargando..." Width="224px" TabIndex="11" OnClientBlur="txt2_OnBlur"
                                            OnClientLoad="cmb2_Load">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="height: 22px">
                                        <asp:Label ID="lblArea" runat="server" Text="Área:"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="left" style="height: 22px">
                                        <telerik:RadComboBox ID="ddlAreas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAreas_SelectedIndexChanged"
                                            Filter="Contains" ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true"
                                            DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                            LoadingMessage="Cargando..." Width="224px" TabIndex="12" OnClientBlur="txt3_OnBlur"
                                            OnClientLoad="cmb3_Load">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 25px; text-align: center; vertical-align: top">
                                                            <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblAreas" runat="server" Text="Solución:" Visible="False"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 5px">
                                    </td>
                                    <td align="left">
                                        <telerik:RadTextBox ID="txtPosicion" runat="server" MaxLength="50" TabIndex="13"
                                            Visible="False" Width="184px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfPosicion" runat="server" ControlToValidate="txtPosicion"
                                            ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Guardar"
                                            Display="Dynamic">*Requerido</asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" rowspan="2">
                                        <asp:ImageButton ID="btnAgregar" runat="server" CausesValidation="true" ImageUrl="Imagenes/disk_blue.png"
                                            OnClick="btnAgregar_Click" TabIndex="15" ToolTip="Agregar solución" Visible="False"
                                            ValidationGroup="Guardar" />
                                        <asp:ImageButton ID="btnDeshacer" runat="server" CausesValidation="false" ImageUrl="Imagenes/undo.png"
                                            OnClick="btnDeshacer_Click" TabIndex="16" ToolTip="Deshacer cambios " Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="200px">
                                        <asp:Label ID="lblPPotencial" runat="server" Text="Porcentaje potencial:" Visible="False"
                                            Width="130px"></asp:Label>
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td align="left" width="300px">
                                        <telerik:RadNumericTextBox ID="txtPotencial" runat="server" MaxLength="9" TabIndex="14"
                                            Visible="False" Width="104px" Culture="es-MX" Type="Percent" MaxValue="100" MinValue="0">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfPotencial" runat="server" ControlToValidate="txtPotencial"
                                            ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Guardar"
                                            Display="Dynamic">*Requerido</asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr id="pnlAgrega" runat="server">
                    <td style="width: 100px">
                        &nbsp;
                        <asp:HiddenField ID="HF_Modificar" runat="server" />
                    </td>
                    <td align="right">
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="Registrar nueva solución"></asp:Label>
                        &nbsp;
                        <asp:ImageButton ID="ibtnCrear" runat="server" AlternateText="Registrar Oportunidad"
                            ImageUrl="Imagenes/document_new.png" OnClick="ibtnCrear_Click" ValidationGroup="new"
                            TabIndex="17" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadGrid ID="rgAreas" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            Width="600px" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="rgAreas_ItemCommand"
                                            OnPageIndexChanged="rgAreas_PageIndexChanged" OnItemDataBound="rgAreas_ItemDataBound"
                                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                            CssClass="tr_1" TabIndex="4">
                                            <MasterTableView ClientDataKeyNames="Clave">
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Clave" HeaderText="Clave" UniqueName="Clave"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Clave" HeaderText="Clave" UniqueName="Clave">
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Solucion" HeaderText="Solución" UniqueName="Solucion">
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit"></HeaderStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Porcentaje" HeaderText="Porcentaje(%)" UniqueName="Porcentaje"
                                                        DataFormatString="{0:N2}">
                                                        <HeaderStyle Width="200" HorizontalAlign="Center" CssClass="tr_tit"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        AllowFiltering="false" ItemStyle-Width="35px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="img\ic_edit.jpg" ToolTip="Editar"
                                                                CommandName="Modificar" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar solución" ConfirmDialogHeight="150px"
                                                        ConfirmDialogWidth="350px" UniqueName="Delete" Visible="true" ButtonType="ImageButton"
                                                        ImageUrl="img\ic_trash.jpg" ButtonCssClass="delete">
                                                        <HeaderStyle HorizontalAlign="Center" CssClass="tr_tit" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                                NextPagesToolTip="Páginas siguientes" PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente"
                                                PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                                ShowPagerText="True" PageButtonCount="3" />
                                        </telerik:RadGrid>
                                        <asp:Label ID="lblMensajes" runat="server" Font-Italic="True"></asp:Label>
                                        <br />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
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
                        <%--  <table border="0" align="right" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <a href="#" tabindex="7">&gt; URL M&oacute;dulo CRM</a>
                                </td>
                                <td>
                                    <a href="#" tabindex="8">&gt; URL M&oacute;dulo RP</a>
                                </td>
                                <td>
                                    <a href="#" tabindex="9">&gt; URL keyquimica.com</a>
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
            var cmb1
            var cmb2
            var cmb3
            function cmb1_Load(sender) {
                cmb1 = sender;
            }
            function cmb2_Load(sender) {
                cmb2 = sender;
            }
            function cmb3_Load(sender) {
                cmb3 = sender;
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
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
