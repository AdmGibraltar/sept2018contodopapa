<%@ Page Title="CONTROL DE PROYECTOS DE PROMOCIÓN" Language="C#" AutoEventWireup="true"
    CodeBehind="CrmProyectos.aspx.cs" Inherits="SIANWEB.CrmProyectos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Key Química CRM</title>
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <script src="js/swf.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>          
            <telerik:AjaxSetting AjaxControlID="ddlUEN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlFiltro" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSegmento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlFiltro" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlFiltro" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlFiltro" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlSolucion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlFiltro" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlAplicacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlFiltro" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>   

            
             <telerik:AjaxSetting AjaxControlID="chkPresentacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChk" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="100%" />
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
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <!--Inicia header-->
    <div id="centrador">
        <div class="header">
            <div class="menu_sesion">
                  <asp:HyperLink ID="hpInicio" runat="server" ImageUrl="img/btn_inicio.jpg" NavigateUrl="wfrmInicioGeneral.aspx"></asp:HyperLink>
                <a href="Login.aspx?Id=1">
                    <asp:Image ID="ibtnCerrarSesion" runat="server" ImageUrl="img/btn_cerrar_sesion.jpg" /></a>
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
    <div class="tit_secc"><img alt="" src="img/tit_alta_proy.gif" /></div>
    <div id="filtros" class="alta_proyectos">
     <asp:Panel ID="pnlFiltro" runat="server">
    <div class="filtro_t1">
      <table border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td height="130" align="center"><table border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td>
                    U<strong>EN:</strong></td>
                <td>
                   <telerik:RadComboBox ID="ddlUEN" runat="server" CssClass="sel1" 
                        AutoPostBack="True" onselectedindexchanged="ddlUEN_SelectedIndexChanged">
                  </telerik:RadComboBox></td>
              </tr>
              <tr>
                <td><strong>Segmento:</strong></td>
                <td><telerik:RadComboBox ID="ddlSegmento" runat="server" CssClass="sel1" 
                        AutoPostBack="True" onselectedindexchanged="ddlSegmento_SelectedIndexChanged">
                  </telerik:RadComboBox></td>
              </tr>
              <tr>
                <td><strong>Territorio</strong></td>
                <td><telerik:RadComboBox ID="ddlTerritorio" runat="server" CssClass="sel1" 
                        AutoPostBack="True" onselectedindexchanged="ddlTerritorio_SelectedIndexChanged">
                  </telerik:RadComboBox></td>
              </tr>
              <tr>
                <td>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
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
          <td height="130" align="center"><table border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td class="blanco"><strong>&Aacute;rea:</strong></td>
                <td><telerik:RadComboBox ID="ddlArea" runat="server" CssClass="sel1" 
                        AutoPostBack="True" onselectedindexchanged="ddlArea_SelectedIndexChanged">
                  </telerik:RadComboBox>
                </td>
              </tr>
              <tr>
                <td class="blanco"><strong>Soluci&oacute;n:</strong></td>
                <td><telerik:RadComboBox ID="ddlSolucion" runat="server" CssClass="sel1" 
                        AutoPostBack="True" onselectedindexchanged="ddlSolucion_SelectedIndexChanged">
                  </telerik:RadComboBox>
                </td>
              </tr>
              <tr>
                <td class="blanco"><strong>Aplicaci&oacute;n:</strong></td>
                <td><telerik:RadComboBox ID="ddlAplicacion" runat="server" CssClass="sel1" 
                        AutoPostBack="True" onselectedindexchanged="ddlAplicacion_SelectedIndexChanged">
                  </telerik:RadComboBox>
                </td>
              </tr>
          </table></td>
        </tr>
      </table>
    </div>
    </asp:Panel>
  </div>        
   <div class="alta_cientes">
      <div style=" margin:0px auto 0px auto; width:300px;">
        <table border="0" cellpadding="0" cellspacing="0">
          <tr><td width="60" height="40" class="blanco"><strong>Cliente</strong></td>
            <td><asp:TextBox ID="txtNoCliente" class="sel2" runat="server" ReadOnly="True" Width="384px"></asp:TextBox></td>
            <td><asp:ImageButton ID="ibtnBuscarCliente" imageurl="img/lupa2.jpg" alt="Buscar" width="38" height="40" runat="server" CausesValidation="False" OnClientClick="javascript:popup();" /></td>
          </tr>
        </table>
      </div>
    </div> 
          <table width="540" border="0" align="center" cellpadding="0" cellspacing="0" style="text-align:left;">
              <tr>
                  <td colspan="2">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label></td>
              </tr>
              <tr>
                  <td colspan="2">
                      &nbsp;</td>
              </tr>
      <tr>
        <td>&nbsp;</td>
        <td><table border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="9"></td>
              <td width="30">
                <asp:CheckBox ID="chkNoRepetitiva" runat="server" /></td>
              <td> Venta No Repetitiva</td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td>Valor Potencial Te&oacute;rico:</td>
        <td>
            <telerik:RadTextBox  ID="txtVPTeorico" runat="server" CssClass="inp1" ReadOnly="True"></telerik:RadTextBox>
            <asp:Label ID="lblVPTeorico" runat="server" Visible="False"></asp:Label>
        </td>
      </tr>
      <tr>
        <td style="height: 52px">Valor Potencial Observado:</td>
        <td style="height: 52px"> &nbsp;
        <telerik:RadNumericTextBox ID="txtVPObservado" runat="server" CssClass="inp1"></telerik:RadNumericTextBox>&nbsp;  
            <asp:RequiredFieldValidator ID="rfvVPO" runat="server" ControlToValidate="txtVPObservado"
                ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" 
                ValidationGroup="Guardar">*Campo Obligatorio</asp:RequiredFieldValidator></td>
      </tr>
      <tr>
        <td valign="top" style="padding-top:15px;">Comentarios:</td>
        <td> <telerik:RadTextBox  ID="txtComentarios" runat="server" CssClass="inp1" TextMode="MultiLine" Width="368px"></telerik:RadTextBox></td>
      </tr>
        <tr>
            <td style="padding-top:15px" valign="top">
                Productos:</td>
            <td>
                <telerik:RadTextBox ID="txtProductos" runat="server" CssClass="inp1" TextMode="MultiLine" Width="368px"></telerik:RadTextBox></td>
        </tr>
    </table>
    <br />
           <div class="tabla_control_promocion">
            <asp:Panel ID="pnlChk" runat="server">
              <div class="tit_rojo2">
                  <img alt="" height="20" src="img/seguimiento.jpg" width="137" /></div>
              <table border="0" cellpadding="0" cellspacing="0" width="884">
                  <tr class="tr_tit">
                      <td height="32" width="220">
                          Análisis</td>
                      <td class="td_bg1" width="1">
                      </td>
                      <td width="220">
                          Promoción</td>
                      <td class="td_bg1" width="1">
                      </td>
                      <td width="220">
                          Negociación</td>
                      <td class="td_bg1" width="1">
                      </td>
                      <td width="221">
                          Cierre</td>
                  </tr>
                  <tr class="tr_1">
                      <td height="30">
                          <asp:CheckBox ID="chkAnalisis" runat="server" Checked="true" Enabled="false" /></td>
                      <td class="td_bg1">
                      </td>
                      <td>
                          <asp:CheckBox ID="chkPresentacion" runat="server" AutoPostBack="True" 
                              oncheckedchanged="chkPresentacion_CheckedChanged" /></td>
                      <td class="td_bg1">
                      </td>
                      <td>
                          <asp:CheckBox ID="chkNegociacion" runat="server" AutoPostBack="True" 
                              oncheckedchanged="chkNegociacion_CheckedChanged" /></td>
                      <td class="td_bg1">
                      </td>
                      <td>
                          <asp:CheckBox ID="chkCierre" runat="server" AutoPostBack="True" 
                              oncheckedchanged="chkCierre_CheckedChanged" /></td>
                  </tr>
                  <tr class="tr_2">
                      <td height="30">
                          <asp:Label ID="lblAnalisis" runat="server"></asp:Label></td>
                      <td>
                      </td>
                      <td>
                          <asp:Label ID="lblPresentacion" runat="server"></asp:Label></td>
                      <td>
                      </td>
                      <td>
                          <asp:Label ID="lblNegociacion" runat="server"></asp:Label></td>
                      <td>
                      </td>
                      <td>
                          <asp:Label ID="lblCierre" runat="server"></asp:Label></td>
                  </tr>
              </table>
              </asp:Panel>
          </div>
          <table align="center" border="0" cellpadding="0" cellspacing="0" style="text-align: left"
              width="900">
              <tr>
                  <td>
                      <table align="right" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                              <td>
                                  Fecha de cotización:</td>
                              <td>
                                  &nbsp;
                                   <telerik:RadDatePicker ID="txtCotizacion" runat="server" CssClass="inp1">                                
                                 <Calendar ID="Calendar1" runat="server">                                       
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar" TodayButtonCaption="Hoy" />
                                    </Calendar>
                                    <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server" MaxLength="10">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="img/cal.jpg" ImageUrl="img/cal.jpg" ToolTip="Abrir el calendario" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" 
                                    ControlToValidate="txtCotizacion" ValidationGroup="Guardar"></asp:RequiredFieldValidator>                         
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <asp:Label ID="lblVenta" runat="server" Text="Venta Promedio Mensual Esperada:"></asp:Label></td>
                              <td>
                                  <telerik:RadTextBox ID="txtVentaMensual" runat="server" MaxLength="10" CssClass="inp1">
                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                </telerik:RadTextBox>
                                  &nbsp;<asp:RangeValidator ID="rvVenta" runat="server" ControlToValidate="txtVentaMensual"
                                      Display="Dynamic" ErrorMessage="*Valor Incorrecto" MaximumValue="10000000" MinimumValue="1"
                                      Type="Currency" SetFocusOnError="True" ValidationGroup="Guardar" 
                                      ForeColor="Red"></asp:RangeValidator></td>
                          </tr>
                      </table>
                  </td>
              </tr>
              <tr>
                  <td align="right" height="50">
                      <asp:LinkButton ID="ibtnRegresar" runat="server" CausesValidation="False" 
                          class="btn_regresar" onclick="ibtnRegresar_Click">REGRESAR</asp:LinkButton>
                      <asp:LinkButton ID="ibtnGuardar" runat="server" class="btn_guardar" 
                          onclick="ibtnGuardar_Click" ValidationGroup="Guardar">GUARDAR</asp:LinkButton>
                  </td>
              </tr>
          </table>


<telerik:radcodeblock id="RadCodeBlock1" runat="server">
 <script type="text/javascript">
    function popup() {
                var Cte = document.getElementById("txtNoCliente").value;
                var Cds = document.getElementById("HiddenField1").value;
                //var oWnd = radopen("Crmpromocion_ventana.aspx?cd=" + Cds + "&cte=" + Cte, "AbrirVentana_promocion");
                //oWnd.center();
                window.open("Crmpromocion_ventana.aspx?cd=" + Cds + "&cte=" + Cte, '');
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
   </script>
</telerik:radcodeblock>
</form>
</body>
</html>

  
