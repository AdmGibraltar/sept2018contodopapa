<%@ Page Language="C#" CodeBehind="wfrmSeguimientoOportunidad.aspx.cs" Inherits="SIANWEB.wfrmSeguimientoOportunidad"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>Key Química CRM</title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divSeguimiento" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlAplicacion">
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCancelacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divSeguimiento" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
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
    <!--Inicia el contenido-->
    <div class="contenido" runat="server">
        <div class="tit_secc">
            <img alt="" src="img/tit_seguimiento.gif" /></div>
        <div>
            <div class="alta_proyectos" runat="server">
                <div class="filtro_t1">
                    <table align="center" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center" height="130">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>UEN:</strong>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlUEN" runat="server" CssClass="sel1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlUEN_SelectedIndexChanged" Width="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Segmento:</strong>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlSegmento" runat="server" CssClass="sel1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSegmento_SelectedIndexChanged" Width="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>Territorio:</strong>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlTerritorio" runat="server" CssClass="sel1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlTerritorio_SelectedIndexChanged" Width="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                            <asp:HiddenField ID="HiddenField6" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="alta">
                    <table border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="130" align="center">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="blanco" align="left">
                                            <strong>&Aacute;rea:</strong>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlArea" runat="server" CssClass="sel1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" Width="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="blanco" align="left">
                                            <strong>Soluci&oacute;n:</strong>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlSolucion" runat="server" CssClass="sel1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSolucion_SelectedIndexChanged" Width="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="blanco" align="left">
                                            <strong>Aplicaci&oacute;n:</strong>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="ddlAplicacion" runat="server" CssClass="sel1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlAplicacion_SelectedIndexChanged" Width="200px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="alta_cientes">
                <div style="margin: 0px auto; width: 300px">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="blanco" height="40" width="60" align="left">
                                <strong>Cliente:</strong>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNoCliente" runat="server" class="sel2" ReadOnly="true"
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <a href="#"></a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div align="center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" style="text-align: left"
                    width="540">
                    <tr>
                        <td colspan="2" style="height: 15px">
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px">
                        </td>
                        <td style="height: 15px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:CheckBox ID="chkNoRepetitiva" runat="server" Text="Venta no repetitiva" />
                        </td>
                        <td>
                            &nbsp;
                            <asp:CheckBox ID="CheckPerteneceCampania" runat="server" Text="Pertenece a Campaña" ReadOnly="True" onclick=" return false; " />
                             <asp:HiddenField ID="HiddenCampaniaId" runat="server" Value="0" />
                             <asp:HiddenField ID="HiddenCampania" runat="server" Value="" /> 
                        </td>
                        <td>
                             <telerik:RadTextBox ID="txtCampania" runat="server" Width="200" TabIndex="11" Value="" ReadOnly="True"  CssClass="sel1"></telerik:RadTextBox>                                       
                                          
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tableGridDatos">
                    <tr>
                        <td>
                            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellSpacing="1" DataKeyField="AplicacionID" ShowFooter="true" Width="900" OnItemDataBound="DataGrid1_ItemDataBound">
                                <Columns>
                                    <asp:BoundColumn DataField="UEN" HeaderText="UEN" SortExpression="UEN"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Segmento" HeaderText="Segmento" SortExpression="Segmento">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Area" HeaderText="Área"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Solucion" HeaderText="Solución"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Aplicacion" HeaderText="Aplicación"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="VPTeórico">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTeorico" runat="server" Text='<%# Bind("Porcentaje2","{0:N2}") %>'></asp:Label>
                                            <telerik:RadNumericTextBox ID="txt2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Porcentaje2") %>'
                                                Width="80px" Visible="false">
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="VPObservado">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txt1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Porcentaje") %>'
                                                Enabled='<%# DataBinder.Eval(Container.DataItem, "Activo") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Activo")) %>'
                                                Width="80px" MinValue="0" MaxLength="9">
                                            </telerik:RadNumericTextBox>
                                            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txt1" Display="Dynamic">Capture el valor</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt1"
                                                Display="Dynamic" ErrorMessage="Valor incorrecto" ValidationExpression="[$]?\s*[-+]?([0-9]{0,3}(,?[0-9]{3})*\.?[0-9]+)"></asp:RegularExpressionValidator>
                                            <asp:CheckBox ID="chk1" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Activo") is DBNull ? false : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Activo")) %>' />
                                            <div style="display: none">
                                                <telerik:RadNumericTextBox ID="lblIdAplicacion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AplicacionID") %>'
                                                    Width="80px">
                                                </telerik:RadNumericTextBox>
                                                <telerik:RadNumericTextBox ID="lblIdEstructura" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id_Estruc") %>'
                                                    Width="80px">
                                                </telerik:RadNumericTextBox>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VPObservado") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="tr_tit" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
                <table align="center" border="0" cellpadding="0" cellspacing="0" style="text-align: left"
                    width="540">
                    <tr runat="server" id="vpt">
                        <td>
                            Valor potencial teórico:
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtVPTeorico" runat="server" CssClass="inp1" ReadOnly="True"
                                TabIndex="1">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr runat="server" id="vpo">
                        <td>
                            Valor potencial observado:
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtVPObservado" runat="server" CssClass="inp1" TabIndex="2"
                                MaxLength="9">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtVPObservado"
                                Display="Dynamic" ErrorMessage="*Valor incorrecto" MaximumValue="999999999" MinimumValue="0.0000001"
                                Type="Double" SetFocusOnError="True" ValidationGroup="Guardar" ForeColor="Red"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rfvVPO" runat="server" ControlToValidate="txtVPObservado"
                                ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Guardar">*Campo obligatorio</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 15px" valign="top">
                            Comentarios:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtComentarios" runat="server" CssClass="inp1" TextMode="MultiLine"
                                TabIndex="3" Width="368px" MaxLength="300">
                                <ClientEvents OnKeyPress="SinComilla" />
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*Requerido"
                                ControlToValidate="txtComentarios" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 15px" valign="top">
                            Productos:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtProductos" runat="server" CssClass="inp1" TextMode="MultiLine"
                                TabIndex="4" Width="368px" MaxLength="16">
                                <ClientEvents OnKeyPress="SinComilla" />
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*Requerido"
                                ControlToValidate="txtProductos" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="divSeguimiento" runat="server" style="padding-bottom: 30px; height: 290px">
                <div class="tabla_control_promocion">
                    <div class="tit_rojo2">
                        <img alt="" height="20" src="img/seguimiento.jpg" width="137" /></div>
                    <div style="border-right: #dfe5f1 1px solid; border-top: #dfe5f1 1px solid; float: left;
                        border-left: #dfe5f1 1px solid; width: 720px; margin-right: 5px; border-bottom: #dfe5f1 1px solid">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="tr_tit">
                                <td height="32">
                                    Análisis
                                </td>
                                <td class="td_bg1" width="1">
                                </td>
                                <td>
                                    Promoción
                                </td>
                                <td class="td_bg1" width="1">
                                </td>
                                <td>
                                    Negociación
                                </td>
                                <td class="td_bg1" width="1">
                                </td>
                                <td>
                                    Cierre
                                </td>
                            </tr>
                            <tr class="tr_1">
                                <td height="30">
                                    <asp:CheckBox ID="chkAnalisis" runat="server" AutoPostBack="True" />
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                </td>
                                <td class="td_bg1">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkPresentacion" runat="server" TabIndex="5" onclick="validar(1)" />
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                </td>
                                <td class="td_bg1">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkNegociacion" runat="server" TabIndex="6" onclick="validar(2);" />
                                </td>
                                <td class="td_bg1">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCierre" runat="server" TabIndex="7" onclick="validar(3);" ValidationGroup="Guardar" />
                                </td>
                            </tr>
                            <tr class="tr_2">
                                <td height="30">
                                    <asp:Label ID="lblAnalisis" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblPresentacion" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblNegociacion" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblCierre" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table align="center" border="0" cellpadding="0" cellspacing="10" style="text-align: left"
                            width="100%">
                            <tr>
                                <td>
                                    <table align="right" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                Fecha de cotización:
                                            </td>
                                            <td align="left">
                                                <telerik:RadDatePicker ID="txtCotizacion" runat="server" CssClass="inp1" TabIndex="9">
                                                    <Calendar ID="Calendar1" runat="server" >
                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                            TodayButtonCaption="Hoy" />
                                                    </Calendar>
                                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                        runat="server" MaxLength="10" TabIndex="9">
                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                    </DateInput>
                                                    <DatePopupButton HoverImageUrl="img/cal.jpg" ImageUrl="img/cal.jpg" ToolTip="Abrir el calendario"
                                                        TabIndex="9" />
                                                </telerik:RadDatePicker>
                                              
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                    ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtCotizacion" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblVenta" runat="server" Text="Venta promedio mensual esperada:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <telerik:RadNumericTextBox ID="txtVentaMensual" runat="server" MaxLength="8" CssClass="inp1"
                                                    TabIndex="10">
                                                    <NumberFormat DecimalDigits="2" />
                                                </telerik:RadNumericTextBox>
                                                &nbsp;
                                                <asp:RangeValidator ID="rvVenta" runat="server" ControlToValidate="txtVentaMensual"
                                                    Display="Dynamic" ErrorMessage="*Valor Incorrecto" ForeColor="Red" MaximumValue="10000000"
                                                    MinimumValue="1" SetFocusOnError="True" Type="Currency" ValidationGroup="Guardar"></asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="rfVenta" runat="server" ControlToValidate="txtVentaMensual"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="Guardar">
                                                </asp:RequiredFieldValidator>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblAvances" runat="server"></asp:Label>
                                    <asp:Label ID="lblMes" runat="server"></asp:Label>
                                    <asp:Label ID="lblContadorAvances" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" height="50">
                                    <asp:LinkButton ID="ibtnRegresar" runat="server" CausesValidation="False" class="btn_regresar"
                                        OnClick="ibtnRegresar_Click" TabIndex="12" Text="REGRESAR"></asp:LinkButton>
                                    <asp:LinkButton ID="ibtnGuardar" runat="server" class="btn_guardar" ValidationGroup="Guardar"
                                        OnClientClick="validar(0)" OnClick="ibtnGuardar_Click" TabIndex="11" Text="GUARDAR"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divCancelar" runat="server" style="border-right: #dfe5f1 1px solid; border-top: #dfe5f1 1px solid;
                        float: left; border-left: #dfe5f1 1px solid; width: 166px; margin-right: 5px;
                        border-bottom: #dfe5f1 1px solid">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="tr_tit">
                                <td height="32">
                                    Cancelación
                                </td>
                            </tr>
                            <tr class="tr_1">
                                <td height="30">
                                    <asp:CheckBox ID="chkCancelacion" runat="server" TabIndex="8" onclick="validar(4);" />
                                </td>
                            </tr>
                            <tr class="tr_2">
                                <td height="30">
                                    <asp:Label ID="lblCancelacion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlCancela" runat="server" Visible="False">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCausa2" runat="server" ForeColor="Red" Text="Causa:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <telerik:RadComboBox ID="ddlCausa2" runat="server" Enabled="False" Width="140px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCompetidor" runat="server" ForeColor="Red" Text="Competidor:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <telerik:RadTextBox ID="txtCompetencia" runat="server" Enabled="False" MaxLength="100"
                                                        ReadOnly="True" Width="120px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblComenta2" runat="server" ForeColor="Red" Text="Comentarios:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;<telerik:RadTextBox ID="txtCancela" runat="server" Enabled="False" TextMode="MultiLine"
                                                        MaxLength="16">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <br />
            &nbsp;
        </div>
    </div>
    <!--Termina contenido-->
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
    <telerik:RadWindowManager ID="RadWindowManagerMaster" runat="server" Style="z-index: 7001">
        <Windows>
            <telerik:RadWindow ID="AbrirVentana_Cancelar" runat="server" Behaviors="Move" Opacity="100"
                VisibleStatusbar="false" Width="450px" Height="350px" Animation="Fade" KeepInScreenBounds="true"
                Overlay="true" Title="" Modal="true" OnClientClose="OnClientClose" ShowContentDuringLoad="false"
                ReloadOnShow="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="AbrirVentana_Autorizar" runat="server" Behaviors="Move" Opacity="100"
                VisibleStatusbar="false" Width="450px" Height="220px" Animation="Fade" KeepInScreenBounds="true"
                Overlay="true" Title="" Modal="true" OnClientClose="OnClientCloseAutorizar" ShowContentDuringLoad="false">
            </telerik:RadWindow>
        </Windows>
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
            var fuente;
            var oldConfirm = radconfirm;

            window.radconfirm = function (text, mozEvent) {

                var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually      
                //Cancel the event      
                ev.cancelBubble = true;
                ev.returnValue = false;
                if (ev.stopPropagation) ev.stopPropagation();
                if (ev.preventDefault) ev.preventDefault();

                //Determine who is the caller      
                var callerObj = ev.srcElement ? ev.srcElement : ev.target;

                //Call the original radconfirm and pass it all necessary parameters      
                if (callerObj) {
                    //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again.      
                    var callBackFn = function (arg) {
                        //debugger;
                        if (arg) {
                            callerObj["onclick"] = "";
                            if (callerObj.type == "checkbox") {// ESPECIFICO PARA ESTA PANTALLA
                                //debugger;
                                if (fuente == 4)
                                    AbrirVentana_Cancelar(fuente);
                                else
                                    AbrirVentanaAutorizar(fuente);
                            }
                            else if (callerObj.click) {
                                callerObj.click(); //Works fine every time in IE, but does not work for links in Moz      
                            }
                            else if (callerObj.tagName == "A") //We assume it is a link button!      
                            {
                                try {
                                    eval(callerObj.href)
                                }
                                catch (e) { }
                            }
                        }
                    }
                    oldConfirm(text, callBackFn, 550, 200, null, 'Advertencia');
                }
                return false;
            }

            var deshabilitados = 0;
            function validar(arg) {
               // debugger;
                if (Page_ClientValidate("Guardar")) {
                    fuente = arg;
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    var cliente = $find("<%= txtNoCliente.ClientID  %>");
                    var aplicacion = $find("<%= ddlAplicacion.ClientID %>")._value;

                    //debugger;
                    var totalRegistros = '<%= totAplicaciones %>'
                    if (deshabilitados > 0 && aplicacion == -1) {
                        if (totalRegistros == deshabilitados) {
                            radalert("No ha seleccionado ninguna aplicación", 330, 150);
                            return false;
                        }
                        else {
                            var ret = radconfirm("En el proyecto del cliente " + cliente.get_value() + ".<br><br>Se ha detectado que una o algunas aplicaciones NO están SELECCIONADAS.<br><br>Es importante considerar que este proyecto se va a dividir en varios proyectos dependiendo las aplicaciones de la solución.<br><br>¿Está seguro de continuar?", event);
                            return ret;
                        }
                    }
                    else {
                        if (arg == 4) {
                            var btnguardar = document.getElementById("<%= ibtnGuardar.ClientID %>");
                            if (btnguardar != null) {
                                btnguardar.style.visibility = "hidden";
                            }
                            var btnRegresar = document.getElementById("<%= ibtnRegresar.ClientID %>");
                            if (btnRegresar != null) {
                                btnRegresar.style.visibility = "hidden";
                            }

                            AbrirVentana_Cancelar(arg);
                        }
                        else if (arg > 0 && arg < 4) {

                            AbrirVentanaAutorizar(arg);
                        }
                        return true;
                    }
                }
                else {
                    var check;
                    if (arg == 1) {
                        check = document.getElementById("<%= chkPresentacion.ClientID %>");
                    }
                    else if (arg == 2) {
                        check = document.getElementById("<%= chkNegociacion.ClientID  %>");
                    }
                    else if (arg == 3) {
                        check = document.getElementById("<%= chkCierre.ClientID  %>");
                    }
                    else if (arg == 4) {
                        check = document.getElementById("<%= chkCancelacion.ClientID  %>");

                    }
                    check.checked = false;
                }
            }

            function confirmCallBackFnCa(arg) {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                if (arg) {
                    ajaxManager.ajaxRequest('Ca');
                }
            }
            function SinComilla(sender, eventArgs) {
                //debugger;
                var c = eventArgs.get_keyCode();
                //debugger;
                if (c == 39) //el guion bajo  y '|' tambien son permitidos
                    eventArgs.set_cancel(true);

            }
            function checkedchanged(txt, chk) {
                //debugger;
                var c = $find(txt);

                if (chk.checked == 0) {
                    c.set_value(0);
                    c.disable();
                    deshabilitados = deshabilitados + 1;
                }
                else {
                    c.enable();
                    deshabilitados = deshabilitados - 1;
                }
            }
            //-------------------------------------------------------
            function SoloNumericoYDiagonal(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (c && c == 13)
                    eventArgs.set_cancel(true);
                if ((c < 48 || c > 57))//si no es numero
                    if (c != 47) //si no es punto
                        eventArgs.set_cancel(true);
        }
        function SoloNumerico(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c && c == 13)
                eventArgs.set_cancel(true);
            if (c < 48 || c > 57) //si no es numero
                eventArgs.set_cancel(true);
        }
        //---------------------------------------------------------
        function OnClientClose(oWnd, args) {
            //get the transferred arguments
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            var oArg = args.get_argument();
            if (oArg) {
                var IdChk = oArg.Tipo;
                Cancelar(IdChk);
                if (IdChk > 0) {
                    ajaxManager.ajaxRequest('opcionA');
                }
                else {
                    ajaxManager.ajaxRequest('opcionC');
                }
            }
        }
        function AbrirVentanaAutorizar(id) {
            //debugger;
            var oWnd = radopen("CrmAceptarAvance.aspx?id=" + id, "AbrirVentana_Autorizar");
            oWnd.center();
        }
        function OnClientCloseAutorizar(oWnd, args) {
            //get the transferred arguments
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            var arg = args.get_argument();
            if (arg) {
                var IdChk = arg.Id;
                var fec = arg.fecha;
                var valido = arg.Valido;

                ajaxManager.ajaxRequest('opcionB');
            }
            else {
                ajaxManager.ajaxRequest('opcionC');
            }
        }
       
       

        function Cancelar(valido) {
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            if (valido == 1) {
                ajaxManager.ajaxRequest('opcionA');
            }
            else {
                ajaxManager.ajaxRequest('opcionC');
            }
        }
        function AbrirVentana_Cancelar() {
            var oWnd = radopen("CrmPromocion_Cancelar.aspx", "AbrirVentana_Cancelar");
            oWnd.center();
        }


        function confirmCallBackFn(arg) {
            document.getElementById("CheckPerteneceCampania").checked = false;
            var textbox = $find('<%= txtCampania.ClientID %>');
            textbox.set_value("")
            //var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            if (arg) {
                document.getElementById("CheckPerteneceCampania").checked = true;
                textbox.set_value(document.getElementById("HiddenCampania").value);
            }


        }

        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
