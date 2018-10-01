<%@ Page  Language="C#" AutoEventWireup="true"
    CodeBehind="wfrmRegistrarOportunidad.aspx.cs" Inherits="SIANWEB.wfrmRegistrarOportunidad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <meta http-equiv="Pragma" content="no-cache"> 
<meta http-equiv="no-cache"> 
<meta http-equiv="Expires" content="-1"> 
<meta http-equiv="Cache-Control" content="no-cache"> 
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="HiddenField1" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="divCliente" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlUEN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSegmento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSolucion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlAplicacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnBuscarCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCliente" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkPresentacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChk" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNegociacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChk" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkCierre">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChk" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ibtnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="principal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ibtnRegresar" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="ibtnGuardar" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
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
    <div id="principal" runat="server" class="contenido">
        <div class="tit_secc">
            <img alt="" src="img/tit_alta_proy.gif" /></div>
        <div id="divGeneral" runat="server">
            <div id="filtros" class="alta_proyectos">
                <asp:Panel ID="pnlFiltro" runat="server">
                    <div class="filtro_t1">
                        <table border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="130" align="center">
                                    <div>
                                        <table border="0" cellspacing="0" cellpadding="0" width="250px">
                                            <tr>
                                                <td align="left">
                                                    <strong>UEN:</strong>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlUEN" runat="server" CssClass="sel1" TabIndex="1" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlUEN_SelectedIndexChanged" controltovalidate="ddlUEN"
                                                        Width="180px" MaxHeight="150px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td style="width: 200px">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Requerido"
                                                        ControlToValidate="ddlUEN" Display="Dynamic" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <strong>Segmento:</strong>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlSegmento" runat="server" CssClass="sel1" TabIndex="2"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSegmento_SelectedIndexChanged"
                                                        Width="180px" MaxHeight="150px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Requerido"
                                                        ControlToValidate="ddlSegmento" Display="Dynamic" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <strong>Territorio:</strong>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlTerritorio" runat="server" CssClass="sel1" TabIndex="3"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlTerritorio_SelectedIndexChanged"
                                                        Width="180px" MaxHeight="150px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Requerido"
                                                        ControlToValidate="ddlTerritorio" Display="Dynamic" ForeColor="Red" InitialValue="-- Seleccionar --"
                                                        SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="alta">
                        <table border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="130" align="center">
                                    <div>
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="blanco" align="left">
                                                    <strong>&Aacute;rea:</strong>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlArea" runat="server" CssClass="sel1" TabIndex="4" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" Width="180px" MaxHeight="150px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td width="60">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="blanco" align="left">
                                                    <strong>Soluci&oacute;n:</strong>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlSolucion" runat="server" CssClass="sel1" TabIndex="5"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSolucion_SelectedIndexChanged"
                                                        Width="180px" MaxHeight="150px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSolucion"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="White" SetFocusOnError="True"
                                                        ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="blanco" align="left">
                                                    <strong>Aplicaci&oacute;n:</strong>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlAplicacion" runat="server" CssClass="sel1" TabIndex="6"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlAplicacion_SelectedIndexChanged"
                                                        Width="180px" MaxHeight="150px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSolucion"
                                                        Display="Dynamic" ErrorMessage="*Requerido" ForeColor="White" SetFocusOnError="True"
                                                        ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <div class="alta_cientes" id="divCliente" runat="server">
                <div style="margin: 0px auto 0px auto; width: 600px;" align="center">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="60" height="40" class="blanco">
                                <strong>Cliente:</strong>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtCliente" runat="server" Width="250px" ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCliente"
                                    ErrorMessage="*" ForeColor="White" SetFocusOnError="True" Display="Dynamic" ValidationGroup="Guardar"
                                    Font-Bold="False">*Campo obligatorio</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:ImageButton ID="ibtnBuscarCliente" ImageUrl="img/lupa2.jpg" alt="Buscar" Width="38"
                                    Height="40" runat="server" ToolTip="Buscar" TabIndex="8" OnClick="ibtnBuscarCliente_Click"
                                    BorderStyle="None" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="divCampos" runat="server" align="center">
                <table width="540" border="0" align="center" cellpadding="0" cellspacing="0" style="text-align: left;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        
                        <td>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>                                   
                                    <td width="30">
                                        <asp:CheckBox ID="chkNoRepetitiva" runat="server" TabIndex="9" />
                                    </td>
                                    <td>
                                        Venta no repetitiva
                                    </td>
                                    <td>           
                                          <asp:HiddenField ID="HiddenCampaniaId" runat="server" Value="0" />
                                          <asp:HiddenField ID="HiddenCampania" runat="server" Value="" />                       
                                        <asp:CheckBox ID="CheckPerteneceCampania" runat="server" Text="Pertenece a Campaña" Checked="false" TabIndex="10" ReadOnly="True" onclick=" return false; "/>
                                         &nbsp;
                                    </td>
                                    <td>
                                         <telerik:RadTextBox ID="txtCampania" runat="server" Width="200" TabIndex="11" Value="" ReadOnly="True"></telerik:RadTextBox>                                       
                                          
                                        &nbsp;
                                    </td>
                                     </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table id="tableGridDatos" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblEstruct" runat="server"></asp:Label>
                            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellSpacing="1" DataKeyField="AplicacionID" ShowFooter="true" Width="900" OnItemDataBound="DataGrid1_ItemDataBound"
                                ViewStateMode="Enabled">
                                <Columns>
                                    <asp:BoundColumn DataField="UEN" HeaderText="UEN" SortExpression="UEN"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Segmento" HeaderText="Segmento" SortExpression="Segmento">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Area" HeaderText="Área"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Solucion" HeaderText="Solución"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Aplicacion" HeaderText="Aplicación"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Porcentaje2" HeaderText="VPTeórico" DataFormatString="{0:N2}">
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="VPObservado">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txt1" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Porcentaje") %>'
                                                Width="80px" MinValue="0" MaxLength="9">
                                            </telerik:RadNumericTextBox>
                                            <asp:CheckBox ID="chk1" runat="server" Checked="true" />
                                            <asp:HiddenField ID="lblIdAplicacion" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.AplicacionID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="tr_tit" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
                <table id="tableDatos" runat="server" width="540" border="0" align="center" cellpadding="0"
                    cellspacing="0" style="text-align: left;">
                    <tr runat="server" id="vpt">
                        <td style="width: 350px">
                            Valor potencial te&oacute;rico:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtVPTeorico" runat="server" CssClass="inp1" ReadOnly="True"
                                TabIndex="10">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr runat="server" id="vpo">
                        <td style="height: 52px; width: 350px;">
                            Valor potencial observado:
                        </td>
                        <td style="height: 52px">
                            <telerik:RadNumericTextBox ID="txtVPObservado" runat="server" CssClass="inp1" TabIndex="11"
                                MaxLength="9" MinValue="0.0000001">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtVPObservado"
                                Display="Dynamic" ErrorMessage="*Valor incorrecto" MaximumValue="999999999" MinimumValue="0.0000001"
                                Type="Double" SetFocusOnError="True" ValidationGroup="Guardar" ForeColor="Red"></asp:RangeValidator>
                        </td>
                        <td style="height: 52px">
                            <asp:RequiredFieldValidator ID="rfvVPO" runat="server" ControlToValidate="txtVPObservado"
                                ErrorMessage="*Requerido" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="padding-top: 15px; width: 350px;">
                            Comentarios:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtComentarios" runat="server" CssClass="inp1" TabIndex="12"
                                MaxLength="300" TextMode="MultiLine" Width="368px">
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
                        <td style="padding-top: 15px; width: 350px;" valign="top">
                            Productos:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtProductos" runat="server" CssClass="inp1" TabIndex="13"
                                MaxLength="1000" TextMode="MultiLine" Width="368px">
                                <ClientEvents OnKeyPress="SinComilla" />
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*Requerido"
                                ControlToValidate="txtProductos" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 350px">
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div class="tabla_control_promocion">
                <asp:Panel ID="pnlChk" runat="server">
                    <div class="tit_rojo2">
                        <img alt="" height="20" src="img/seguimiento.jpg" width="137" /></div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr class="tr_tit">
                            <td height="32" width="220">
                                Análisis
                            </td>
                            <td class="td_bg1" width="1">
                            </td>
                            <td width="220">
                                Promoción
                            </td>
                            <td class="td_bg1" width="1">
                            </td>
                            <td width="220">
                                Negociación
                            </td>
                            <td class="td_bg1" width="1">
                            </td>
                            <td width="221">
                                Cierre
                            </td>
                        </tr>
                        <tr class="tr_1">
                            <td height="30">
                                <asp:CheckBox ID="chkAnalisis" runat="server" Checked="true" TabIndex="14" Enabled="false" />
                            </td>
                            <td class="td_bg1">
                            </td>
                            <td>
                                <asp:CheckBox ID="chkPresentacion" runat="server" TabIndex="15" AutoPostBack="true"
                                    OnCheckedChanged="chkPresentacion_CheckedChanged" />
                            </td>
                            <td class="td_bg1">
                            </td>
                            <td>
                                <asp:CheckBox ID="chkNegociacion" runat="server" AutoPostBack="true" OnCheckedChanged="chkNegociacion_CheckedChanged" />
                            </td>
                            <td class="td_bg1">
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCierre" runat="server" TabIndex="16" AutoPostBack="true" OnCheckedChanged="chkCierre_CheckedChanged" />
                            </td>
                        </tr>
                        <tr class="tr_2">
                            <td height="30">
                                <asp:Label ID="lblAnalisis" runat="server"></asp:Label>
                                <asp:HiddenField ID="HiddenField3" runat="server" Value="1" />
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
                </asp:Panel>
            </div>
            <div class="tabla_control_promocion">
                <table align="center" border="0" cellpadding="0" cellspacing="0" style="text-align: left"
                    width="900">
                    <tr>
                        <td>
                            <table align="right" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Fecha de cotización:
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtCotizacion" runat="server" CssClass="inp1" TabIndex="17">
                                            <Calendar ID="Calendar1" runat="server">
                                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                    TodayButtonCaption="Hoy" />
                                            </Calendar>
                                            <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                runat="server" MaxLength="10" TabIndex="18">
                                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                            </DateInput>
                                            <DatePopupButton HoverImageUrl="img/cal.jpg" ImageUrl="img/cal.jpg" ToolTip="Abrir el calendario"
                                                TabIndex="19" Enabled="false" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                            ErrorMessage="*Requerido" ForeColor="Red" ControlToValidate="txtCotizacion" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblVenta" runat="server" Text="Venta promedio mensual esperada:"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtVentaMensual" runat="server" MaxLength="8" CssClass="inp1"
                                            TabIndex="20">
                                        </telerik:RadNumericTextBox>
                                        &nbsp;<asp:RangeValidator ID="rvVenta" runat="server" ControlToValidate="txtVentaMensual"
                                            Display="Dynamic" ErrorMessage="*Valor incorrecto" MaximumValue="10000000" MinimumValue="1"
                                            Type="Double" SetFocusOnError="True" ValidationGroup="Guardar" ForeColor="Red"></asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="rfVenta" runat="server" ForeColor="Red" Display="Dynamic"
                                            ErrorMessage="*Requerido" ValidationGroup="Guardar" ControlToValidate="txtVentaMensual">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="divBotones">
                        <td align="right" height="50">
                            <asp:LinkButton ID="ibtnRegresar" runat="server" CausesValidation="False" class="btn_regresar"
                                OnClick="ibtnRegresar_Click" TabIndex="22">REGRESAR</asp:LinkButton>
                            <asp:LinkButton ID="ibtnGuardar" runat="server" class="btn_guardar" OnClientClick="validar()"
                                OnClick="ibtnGuardar_Click" ValidationGroup="Guardar" TabIndex="21">GUARDAR</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <telerik:RadWindowManager ID="RadWindowManagerMaster" runat="server" Style="z-index: 7001">
            <Windows>
                <telerik:RadWindow ID="AbrirVentana_promocion" runat="server" Behaviors="Move, Close"
                    Opacity="100" VisibleStatusbar="false" Width="840px" Height="500px" Animation="Fade"
                    KeepInScreenBounds="true" Overlay="true" Title="" Modal="true" OnClientClose="OnClientClose"
                    ShowContentDuringLoad="false" ReloadOnShow="true">
                </telerik:RadWindow>
                <telerik:RadWindow ID="AbrirVentana_Autorizar" runat="server" Behaviors="Move" Opacity="100"
                    VisibleStatusbar="false" Width="450px" Height="220px" Animation="Fade" KeepInScreenBounds="true"
                    Overlay="true" Title="" Modal="true" OnClientClose="OnClientCloseAutorizar" ShowContentDuringLoad="false"
                    ReloadOnShow="true">
                </telerik:RadWindow>
                <telerik:RadWindow ID="AbrirVentana_Validar" runat="server" Behaviors="Move" Opacity="100"
                    VisibleStatusbar="false" Width="450px" Height="200px" Animation="Fade" KeepInScreenBounds="true"
                    Overlay="true" Title="Avance a proyectos de promoción" Modal="true" OnClientClose="OnClientCloseValidar"
                    ShowContentDuringLoad="false" ReloadOnShow="true">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </div>
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
                        if (arg) {
                            callerObj["onclick"] = "";
                            if (callerObj.click) callerObj.click(); //Works fine every time in IE, but does not work for links in Moz      
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
            function validar() {

                if (Page_ClientValidate("Guardar")) {
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    var cliente = $find("<%= txtCliente.ClientID  %>");
                    var aplicacion = $find("<%= ddlAplicacion.ClientID %>")._value;

                    
                    var totalRegistros = '<%= totAplicaciones %>'

                    if (deshabilitados > 0 && aplicacion == -1) {
                        if (totalRegistros == deshabilitados) {
                            radalert("No ha seleccionado ninguna aplicación", 330, 150);
                            return false;
                        }
                        else {
                            return radconfirm("En el proyecto del cliente " + cliente.get_value() + ".<br><br>Se ha detectado que una o algunas aplicaciones NO están SELECCIONADAS.<br><br>Es importante considerar que este proyecto se va a dividir en varios proyectos dependiendo las aplicaciones de la solución.<br><br>¿Está seguro de continuar?", event);
                        }
                    }
                    else {
                        return true;
                    }
                }
             
            }
            //-------------------------------------------------------
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
            //------------------- ventana de clientes
            function SinComilla(sender, eventArgs) {
                var c = eventArgs.get_keyCode();
                if (c == 39) //el guion bajo  y '|' tambien son permitidos
                    eventArgs.set_cancel(true);
            }
            function popup() {
                var cte = document.getElementById("txtNoCliente").value;
                var cd = document.getElementById("HiddenField1").value;
                var oWnd = radopen("Crmpromocion_ventana.aspx?cd=" + cd + "&cte=" + cte, "AbrirVentana_promocion");
                oWnd.center();
            }

            function AbrirVentana(cte, ter, uen, seg) {
                AbrirVentana(cte, ter, uen, seg);
            }
            function AbrirVentana(cte, ter, uen, seg) {
                var oWnd = radopen("Crmpromocion_ventana.aspx?cte=" + cte + "&ter=" + ter + "&uen=" + uen + "&seg=" + seg, "AbrirVentana_promocion");
                oWnd.center();
            }
            function cambio() {
                var valor = document.getElementById('txtNCliente').value;
                document.getElementById('HiddenField2').value = valor;
            }
            function refreshGrid() {
            }
            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg) {
                    var noCliente = arg.noCliente;
                    document.getElementById("HiddenField1").value = noCliente;
                }
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest('opcionA');
            }
            function select(hd1) {
                document.getElementById("HiddenField1").value = hd1;
            }
            //----------------ventana de autorizar
            function AbrirVentanaAutorizar(id) {
                var oWnd = radopen("CrmAceptarAvance.aspx?id=" + id, "AbrirVentana_Autorizar");
                oWnd.center();
            }
            function OnClientCloseAutorizar(oWnd, args) {
                var arg = args.get_argument();
                if (arg) {
                    var IdChk = arg.Id;
                    var fec = arg.fecha;
                    var valido = arg.Valido;
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    ajaxManager.ajaxRequest('opcionB');
                }
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

            //----------------ventana de validar
            function AbrirVentanaValidar(id, cliente) {
                var oWnd = radopen("CrmAceptarCambios.aspx?id=" + id + "&Cliente=" + cliente, "AbrirVentana_Validar");
                oWnd.center();
            }
            function OnClientCloseValidar(oWnd, args) {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                var arg = args.get_argument();
                if (arg) {
                    var valido = arg.Valido;
                    var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                    ajaxManager.ajaxRequest('opcionC');
                }
            }  
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
