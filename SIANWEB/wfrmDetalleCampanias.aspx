<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmDetalleCampanias.aspx.cs" Inherits="SIANWEB.wfrmDetalleCampanias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=8" /> 
      <meta http-equiv="Pragma" content="no-cache"> 
<meta http-equiv="no-cache"> 
<meta http-equiv="Expires" content="-1"> 
<meta http-equiv="Cache-Control" content="no-cache"> <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Key Química CRM</title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
    <script type="text/javascript">
        //--------------------------------------------------------------------------------------------------
        //Cuando el combo pierde el foco
        //Si el usuario escribió y no eligió un item correcto limpia el combo
        //--------------------------------------------------------------------------------------------------
        function Combo_ClientBlur(sender, args) {
            //debugger;
            var itemSelected = sender.findItemByText(sender.get_text())
            if (itemSelected == null) {
                LimpiarComboSelectIndex0(sender);
                //args.set_cancel(true);
            }
        }

        //Limpiar radComboBox
        //param sender --> objeto a limpiar
        //NOTA: selecciona el primer item del combo que debe contener el texto '-- seleccionar --'
        function LimpiarComboSelectIndex0(sender) {
            //debugger;
            if (sender.get_items().get_count() > 0) {
                sender.get_items().getItem(0).select();
                sender.set_value('-- Seleccionar --');
            }
            else {
                sender.set_value('');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:radwindowmanager id="RadWindowManager1" runat="server">
    </telerik:radwindowmanager>
    <telerik:radscriptmanager id="RadScriptManager1" runat="server">
        </telerik:radscriptmanager>
    <telerik:radajaxmanager id="RadAjaxManager1" runat="server" enablepageheadupdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgCampanaAplicaciones">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgCampanaAplicaciones" LoadingPanelID="RadAjaxLoadingPanel1" />
                        
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlUENs">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlSegmentos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="lnkGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ibtnAgregar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ibtnAplicar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlRepresentantes" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="lnkGuardarReps">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgCampanaRikMetas" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ibtnAsignarReps">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="contenido" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlCDS">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlRepresentantes" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ibtnAplicar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgCampanaRikMetas" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                
            </AjaxSettings>

        </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
        </telerik:radajaxloadingpanel>
    <div class="centrador">
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
            <!--Inicia contenido-->
            <div id="contenido" class="contenido" runat="server">
                <br />

                <table align="center" border="0">
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;<asp:Label ID="Label1" runat="server" Text="Nombre de la campaña:" Font-Bold="True"></asp:Label>&nbsp;&nbsp;
                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="344px" MaxLength="150">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="val_txtNombre" runat="server" 
                                ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="*Requerido" 
                                ForeColor="Red" ValidationGroup="GuardarCampana">
                            </asp:RequiredFieldValidator>
                         </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tr_1">
                            <asp:Label ID="Label7" runat="server" Text="UEN:" Font-Bold="True"></asp:Label>
                            <telerik:RadComboBox ID="ddlUENs" runat="server" Width="200px" Filter="Contains"
                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" 
                                DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." 
                                AutoPostBack="True" 
                                OnClientBlur="Combo_ClientBlur"                                 
                                OnSelectedIndexChanged="ddlUENs_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td class="tr_1">
                            <asp:Label ID="Label8" runat="server" Text="Segmento:" Font-Bold="True"></asp:Label>
                            <telerik:RadComboBox ID="ddlSegmentos" runat="server" Width="200px" Filter="Contains"
                                ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" 
                                DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." 
                                AutoPostBack="True" 
                                OnClientBlur="Combo_ClientBlur"                                 
                                OnSelectedIndexChanged="ddlSegmentos_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center">
                            <asp:Label ID="Label9" runat="server" Text="Aplicaciones" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBoxList ID="chkAplicaciones" runat="server" Width="800px" RepeatColumns="4">
                            </asp:CheckBoxList>                            
                        </td>
                    </tr>
                    </table>
                <table align="center" border="0" width="892">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            </td>
                        <td align="right" >
                            <asp:ImageButton ID="ibtnAgregar" runat="server" AlternateText="Agregar" ToolTip="Agregar" ImageUrl="img\add2.png"
                            onclick="ibtnAgregar_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="3"><div class="tit_azul">APLICACIONES DE CAMPAÑA</div>
                                
                            <telerik:radgrid id="rgCampanaAplicaciones" runat="server" 
                                autogeneratecolumns="False" gridlines="None"
                                pagesize="15" mastertableview-nomasterrecordstext="No se encontraron registros."
                                allowpaging="True" allowsorting="False" headerstyle-horizontalalign="Center" 
                                onneeddatasource="rgCampanaAplicaciones_NeedDataSource"
                                onpageindexchanged="rgCampanaAplicaciones_PageIndexChanged" 
                                onitemcommand="rgCampanaAplicaciones_ItemCommand" Width="892px">

                                <SortingSettings SortedAscToolTip="Orden acendente" 
                                    SortedDescToolTip="Orden decendente" SortToolTip="Click para reordenar" />

                                <ExportSettings IgnorePaging="true" OpenInNewWindow="true" FileName="Lista" HideStructureColumns="true" ExportOnlyData="true">
                                    <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="Lista" Title="Lista" />
                                </ExportSettings>

                                <MasterTableView DataKeyNames="Id_Emp,Id_Cam,Id_Apl" CommandItemDisplay="none" PageSize="15">
                                
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </ExpandCollapseColumn>

                                    <CommandItemSettings 
                                        ExportToPdfText="Exportar a Pdf" AddNewRecordText="Agregar">
                                    </CommandItemSettings>

                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false"
                                            UniqueName="Id_Emp">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Id_Uen" HeaderText="UENID" 
                                            UniqueName="Id_Uen">
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Uen_Descripcion" HeaderText="UEN" 
                                            UniqueName="Uen_Descripcion">
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Id_Seg" HeaderText="SegmentoID" 
                                            UniqueName="Id_Seg">
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Seg_Descripcion" HeaderText="Segmento" 
                                            UniqueName="Seg_Descripcion">
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Id_Apl" HeaderText="AplicaciónID" 
                                            UniqueName="Id_Apl">
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Apl_Descripcion" HeaderText="Aplicación" 
                                            UniqueName="Apl_Descripcion">
                                        </telerik:GridBoundColumn>                                    

                                        <telerik:GridTemplateColumn  HeaderStyle-HorizontalAlign="Center" UniqueName="DeleteColumn"
                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:LinkButton id="lnkQuitarAplicacion" runat="server" CausesValidation="false" CommandName="Eliminar" Text="Quitar"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>

                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores" FirstPageToolTip="Primera página" LastPageToolTip="Última página" 
                                    NextPageToolTip="Siguiente página" 
                                    PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                    PrevPageToolTip="Página anterior" 
                                    PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                    ShowPagerText="True" PageButtonCount="3" />

                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:radgrid>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;

                            <asp:HiddenField ID="HD_CampanaActual" runat="server" />

                            <asp:ImageButton ID="ibtnAsignarReps" runat="server" AlternateText="Asignar representantes" 
                                ImageUrl="img\add2.png" ToolTip="Asignar representantes"
                                onclick="ibtnAsignarReps_Click" Visible="False" />
                            <asp:LinkButton ID="lnkGuardar" runat="server" class="btn_guardar" ValidationGroup="GuardarCampana"
                                onclick="lnkGuardar_Click">GUARDAR</asp:LinkButton></td>
                    </tr>
                    </table>
                <table align="center" border="0" width="892">
                    <tr>
                        <td colspan="3">
                            &nbsp;<asp:Panel ID="pnlRepresentantes" runat="server" Visible="False" Width="100%">
                                &nbsp;<table 
                                    style="border-right: #33cc00 thin solid; border-top: #33cc00 thin solid; border-left: #33cc00 thin solid; border-bottom: #33cc00 thin solid" 
                                    cellpadding="0" cellspacing="0" class="tr_1" width="892">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="CDS:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Cantidad:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Monto:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="ddlCDS" runat="server" Width="200px" Filter="Contains"
                                            ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" 
                                            DataTextField="Descripcion" DataValueField="Id" LoadingMessage="Cargando..." 
                                            AutoPostBack="True" 
                                            OnClientBlur="Combo_ClientBlur"                                 
                                            OnSelectedIndexChanged="ddlCDS_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtACantidad" runat="server" Width="70px" MaxLength="3"
                                            MinValue="1">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            <ClientEvents />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="val_txtACantidad" runat="server" 
                                            ControlToValidate="txtACantidad" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="MetasRik">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        $<telerik:RadNumericTextBox ID="txtAMonto" runat="server" Width="70px" MaxLength="9"
                                            MinValue="1">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            <ClientEvents />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="val_txtAMonto" runat="server" 
                                            ControlToValidate="txtAMonto" Display="Dynamic" ErrorMessage="*Requerido" 
                                            ForeColor="Red" ValidationGroup="MetasRik">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td align="right">
                                        &nbsp;</td>
                                    <td align="right">
                                        <asp:ImageButton ID="ibtnAplicar" runat="server" 
                                            AlternateText="Aplicar a todos" ImageUrl="~/img/check2.png" 
                                            onclick="ibtnAplicar_Click" ToolTip="Aplicar a todos" 
                                            ValidationGroup="MetasRik" />
                                    </td>
                                </tr> 
                                    <tr>
                                        <td colspan="5">
                                            <div class="tit_azul">
                                                METAS Y CUOTAS DE REPRESENTANTES</div>
                                            <telerik:RadGrid ID="rgCampanaRikMetas" runat="server" allowpaging="True" 
                                                allowsorting="False" autogeneratecolumns="False" gridlines="None" 
                                                headerstyle-horizontalalign="Center" 
                                                mastertableview-nomasterrecordstext="No se encontraron registros." 
                                                onneeddatasource="rgCampanaRikMetas_NeedDataSource" 
                                                onpageindexchanged="rgCampanaRikMetas_PageIndexChanged" pagesize="15" 
                                                Width="892px">
                                                <SortingSettings SortedAscToolTip="Orden acendente" 
                                                    SortedDescToolTip="Orden decendente" SortToolTip="Click para reordenar" />
                                                <ExportSettings ExportOnlyData="true" FileName="Lista" 
                                                    HideStructureColumns="true" IgnorePaging="true" OpenInNewWindow="true">
                                                    <Pdf PageHeight="210mm" PageTitle="Lista" PageWidth="297mm" Title="Lista" />
                                                </ExportSettings>
                                                <MasterTableView CommandItemDisplay="none" DataKeyNames="Id_Emp,Id_Cam" 
                                                    PageSize="15">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <CommandItemSettings AddNewRecordText="Agregar" 
                                                        ExportToPdfText="Exportar a Pdf" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Emp" Display="false" HeaderText="Id_Emp" 
                                                            UniqueName="Id_Emp">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Id_Rik" HeaderText="No. Rik" 
                                                            UniqueName="Id_Rik">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Rik_Nombre" HeaderText="Representante" 
                                                            UniqueName="Rik_Nombre">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="MetCam_Cantidad" 
                                                            HeaderText="Núm. proyectos" UniqueName="MetCam_Cantidad">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="MetCam_Monto" HeaderText="Monto proyectos" 
                                                            UniqueName="MetCam_Monto">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <PagerStyle FirstPageToolTip="Primera página" LastPageToolTip="Última página" 
                                                    NextPagesToolTip="Páginas siguientes" NextPageToolTip="Siguiente página" 
                                                    PageButtonCount="3" 
                                                    PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:" 
                                                    PrevPagesToolTip="Páginas anteriores" PrevPageToolTip="Página anterior" 
                                                    ShowPagerText="True" />
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                            </table> 
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 29px;">
                        </td>
                        <td style="width: 100px; height: 29px;">
                            </td>
                        <td style="width: 100px; height: 29px;">
                            <asp:LinkButton ID="lnkGuardarReps" runat="server" class="btn_guardar" 
                                Visible="False"
                                onclick="lnkGuardarReps_Click">GUARDAR</asp:LinkButton></td>
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


        </div>
    </div>
    </form>
</body>
</html>
