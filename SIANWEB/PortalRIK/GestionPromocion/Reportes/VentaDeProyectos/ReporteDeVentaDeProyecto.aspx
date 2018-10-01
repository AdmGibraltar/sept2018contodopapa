<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteDeVentaDeProyecto.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Reportes.VentaDeProyectos.ReporteDeVentaDeProyecto" %>
<%@ Register Src="~/Controles/Cliente/UCPatternflyToast.ascx" TagPrefix="uc" TagName="UCPatternflyToast" %>
<%@ Register Src="~/js/crm/servicios/navegacion/UCNotificaciones_js.ascx" TagPrefix="uc" TagName="UCNotificaciones_js" %>
<%@ Register Src="~/PortalRIK/Navegacion/Notificaciones/UCNotificaciones.ascx" TagPrefix="uc" TagName="UCNotificaciones" %>
<%@ Register Src="~/js/crm/ui/Notificaciones.ascx" TagPrefix="uc" TagName="UINotificaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <telerik:radcodeblock runat="server" id="rcb2">
    <title>Portal RIK</title>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--link rel="shortcut icon" href="<%=Page.ResolveUrl("~/Img/patternfly/favicon.ico")%>"-->
    <link rel="shortcut icon" href="<%=Page.ResolveUrl("~/Img/favicon.ico")%>">    
    
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly-additions.min.css")%>">
    
    <%--<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">

    <script src="//code.jquery.com/jquery-2.1.4.min.js"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    </telerik:radcodeblock>
    <style>
        .toast-pf-top-right-rel {
          left: 20px;
          position: relative;
          right: 20px;
          top: 12px;
          z-index: 1035;
          /* Medium devices (desktops, 992px and up) */
        }
    </style>
    <link href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">

</head>
<body>
    <telerik:radcodeblock runat="server" id="rcb3">
    <%--<telerik:radscriptmanager id="RadScriptManager1" runat="server">
    </telerik:radscriptmanager>--%>
    <nav class="navbar navbar-default navbar-pf" role="navigation">
      <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse-1">
          <span class="sr-only">Toggle navigation</span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="/">
          <img src="<%=Page.ResolveUrl("~/Img/key_logo.jpg")%>" alt="PatternFly Enterprise Application" />
        </a>
      </div>
      <div class="collapse navbar-collapse navbar-collapse-1">
        <ul class="nav navbar-nav navbar-utility">
            <uc:UCNotificaciones runat="server" ID="UCNotificaciones1" />
          <li>
            <a href="#">Status</a>
          </li>
          <li class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <span class="pficon pficon-user"></span>
              <%=EntidadSesion.U_Nombre%> <b class="caret"></b>
            </a>
            <ul class="dropdown-menu">
              <%--<li>
                <a href="#">Link</a>
              </li>
              <li>
                <a href="#">Another link</a>
              </li>
              <li>
                <a href="#">Something else here</a>
              </li>
              <li class="divider"></li>
              <li class="dropdown-submenu">
                <a tabindex="-1" href="#">More options</a>
                <ul class="dropdown-menu">
                  <li>
                    <a href="#">Link</a>
                  </li>
                  <li>
                    <a href="#">Another link</a>
                  </li>
                  <li>
                    <a href="#">Something else here</a>
                  </li>
                  <li class="divider"></li>
                  <li class="dropdown-header">Nav header</li>
                  <li>
                    <a href="#">Separated link</a>
                  </li>
                  <li class="divider"></li>
                  <li>
                    <a href="#">One more separated link</a>
                  </li>
                </ul>
              </li>
              <li class="divider"></li>
              <li>
                <a href="#">One more separated link</a>
              </li>--%>
              <%--<li class="divider"></li>--%>
                <li>
                    <a href="<%=ApplicationUrl %>/Inicio.aspx"><i class="fa fa-reply" aria-hidden="true"></i>Ir a SIANWEB</a>
                </li>
                <li>
                    <a onclick="javascript:salirDelSistema(this)" ><i class="fa fa-sign-out" aria-hidden="true"></i>Salir</a>
                </li>
            </ul>
          </li>
        </ul>
        <!--Here lies the toolbar-->
      </div>
    </nav>

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-9 col-md-10 col-sm-push-3 col-md-push-2 PADDING10">
               
                <div class="row">
                    <div class="col-sm-12 col-md-12">
                        <h3><strong>Reporte Ventas de Proyectos</strong></h3>
                    </div>
                </div>

                <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>

                <script src="//cdn.datatables.net/1.10.7/js/jquery.dataTables.min.js"></script>
                <%--<script src="<%=Page.ResolveUrl("~/Librerias/DataTables/datatables.min.js")%>"></script>--%>

                <form runat="server" id="frmScriptManagerContainer">
                    <asp:ScriptManager runat="server" ID="smMainScriptManager">
                    </asp:ScriptManager>


               <div class="row">

                    <div class="col-sm-1 col-md-1 col-xs-12 PARAM" style=" min-width:140px;">
                        <label>Tipo de Centro</label>
                     <table>
                        <tr>
                            <td>
                                <label for="<%=rbCDI.ClientID %>">
                                    CDI
                                </label>
                                <asp:RadioButton runat="server" ID="rbCDI" Checked="true" GroupName="TipoDeCentro" />
                                <%--<input type="radio" name="tipoCentro" id="tipoCentro[0]" checked />--%>
                            </td>
                            <td>
                                <label for="<%=rbCDC.ClientID %>">
                                    CDC
                                </label>
                                <asp:RadioButton runat="server" ID="rbCDC" GroupName="TipoDeCentro" />
                                <%--<input type="radio" name="tipoCentro" id="tipoCentro[1]" />--%>
                            </td>
                        </tr>
                    </table>

                    </div>
            
                    <div class="col-sm-2 col-md-2 col-xs-12 PARAM">

                    <table width="100%">
                        <tr>
                            <td style="text-align:center;">
                                <label>Año</label>
                            </td>
                             <td style="text-align: center;">
                                <label>Mes</label>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlAnyo" DataTextField="Text" DataValueField="Value" CssClass="selectpicker" OnSelectedIndexChanged="ddlAnyo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<select class="selectpicker" data-width="fit">
                                    <asp:Repeater runat="server" ID="rptFiltroAnyos">
                                        <ItemTemplate>
                                            <option value='<%#Eval("Value") %>'>
                                                <%#Eval("Text") %>
                                            </option>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </select>--%>
                            </td>
                            <td>
                                                    <asp:DropDownList runat="server" ID="ddlMes" CssClass="selectpicker" DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<select class="selectpicker" data-width="fit">
                                                        <asp:Repeater runat="server" ID="rptFiltroMesesSP">
                                                            <ItemTemplate>
                                                                <option value='<%#Eval("Value") %>'>
                                                                    <%#Eval("Text") %>
                                                                </option>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </select>--%>
                                                </td>

                        </tr>
                    </table>

                  
                    </div>        
                    
                    <div class="col-sm-4 col-md-4 col-xs-12 PARAM">

                         <table>
                                <tr>
                                    <td style="text-align: center;" colspan="3">
                                        <label>
                                            Antigüedad de RIK
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="<%=rbAntiguedadRIKTodos.ClientID %>">
                                            Todos
                                        </label>
                                        <asp:RadioButton runat="server" ID="rbAntiguedadRIKTodos" GroupName="AntiguedadRIK" Checked="true" />
                                        <%--<input type="radio" name="antiguedadRik" id="antiguedadRik[0]" checked />--%>
                                    </td>
                                    <td>
                                        <label for="<%=rbAntiguedadMenor8Meses.ClientID %>">
                                            Menor a 8 meses
                                        </label>
                                        <asp:RadioButton runat="server" ID="rbAntiguedadMenor8Meses" GroupName="AntiguedadRIK" />
                                        <%--<input type="radio" name="antiguedadRik" id="antiguedadRik[1]" />--%>
                                    </td>
                                    <td>
                                        <label for="<%=rbAntiguedadRIKMayorIgual8Meses.ClientID %>">
                                            Mayor o igual a 8 meses
                                        </label>
                                        <asp:RadioButton runat="server" ID="rbAntiguedadRIKMayorIgual8Meses" GroupName="AntiguedadRIK" />
                                        <%--<input type="radio" name="antiguedadRik" id="antiguedadRik[2]" />--%>
                                    </td>
                                </tr>
                            </table>

                    </div>

                    <div class="col-sm-3 col-md-3 col-xs-12 PARAM">

                     <table>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <label>
                                    Otros
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label for="<%=chkPromedioTresMeses.ClientID %>">
                                    Promedio 3 meses
                                </label>
                                <asp:CheckBox runat="server" ID="chkPromedioTresMeses" />
                                <%--<input type="checkbox" name="promedioTresMeses" id="promedioTresMeses" />--%>
                            </td>
                            <td>
                                <label for="<%=chkListadoRIKS.ClientID %>">
                                    Listado RIK's
                                </label>
                                <asp:CheckBox runat="server" ID="chkListadoRIKS" />
                                <%--<input type="checkbox" name="listadoRiks" id="listadoRiks" />--%>
                            </td>
                        </tr>
                    </table>

                    </div>

                    <div class="col-sm-1 col-md-1 VAL_BOTTOM">
                        <div class="row">
                            <div class="col-sm-6">
                                <button class="btn btn-link active">
                                    <i class="fa fa-file-excel-o fa-2x" aria-hidden="true"></i>
                                </button>
                            </div>
                            <div class="col-sm-6">
                                  <button runat="server" ID="btnActualizar" type="button" class="btn btn-link" onserverclick="btnActualizar_Click">
                                <i class="fa fa-refresh fa-2x" aria-hidden="true"></i>
                            </button>                       
                            </div>
                        </div>
                    </div>

               </div>

               <div class="row">

                <div class="col-sm-12 col-md-12 col-xs-12" style=" min-width:140px;">
               
                    <div runat="server" style="width: 100%;" id="reporteBlankSlate" visible="true">
                        <div class="blank-slate-pf">
                            <div class="blank-slate-pf-icon">
                                <i class="fa fa-book" aria-hidden="true"></i>
                            </div>
                            <h1>
                                Reporte De Venta
                            </h1>
                            <p>
                                Selecciona las condiciones que deseas considerar y presiona <a href="#!">aquí</a> para actualizar el reporte
                            </p>
                        </div>
                    </div>

                    <div runat="server" id="dvContenedorDeReporte" visible="false">
                        <table style="width: 100%;" id="tblContenido">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;">
                                                <h3><strong>Key Química, S.A. de C.V.</strong></h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <h3><strong>SIANWEB Central</strong></h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <h4><strong>Reporte de Ventas De Proyectos | <%=ViewModel.NombreRepresentanteElegido %> <%=ViewModel.MesElegido %> <%=ViewModel.AnyoElegido %></strong></h4>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div runat="server" id="tblReportePorCd" visible="false">
                                        <table class="table table-striped table-bordered">
                                            <col />
                                                <colgroup span="2"></colgroup>

                                                <colgroup span="2"></colgroup>
                                                <colgroup span="2"></colgroup>
                                                <colgroup span="2"></colgroup>
                                                <colgroup span="2"></colgroup>
                                                <thead>
                                                    <tr>
                                                        <th rowspan="3" style="text-align: center;">
                                                            CDI
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Venta Espor&aacute;dica
                                                        </th>
                                                        <th colspan="8" scope="colgroup" style="text-align: center;">
                                                            Venta Instalada
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Proyectos Ingresados en el mes
                                                        </th>

                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Proyectos Ingresados en el mes
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Proyectos Promoción
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Cierre
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Cancelados
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>

                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                    </tr>
                                                </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rptReporte">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style='<%# Eval("StyleCdNombre") %>'>
                                                                <asp:LinkButton runat="server" ID="lnkbtnCDNombre" Text='<%#Eval("Cd_Nombre") %>' Enabled='<%# !(bool)Eval("EsZona") %>' OnClick="lnkbtnCDNombre_Click" CommandArgument='<%#Eval("Id_Cd") %>' >
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style='<%# Eval("VE_StyleProyectosIngresadosNumProy") %>'>
                                                                <%#Eval("VE_ProyectosIngresadosNumeroProyectos")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VE_StyleProyectosIngresadosImporteProy") %>'>
                                                                <%#Eval("VE_ProyectosIngresadosImporteProyecto")%>
                                                            </td>

                                                            <td style='<%# Eval("VI_StyleProyectosIngresadosNumProy") %>'>
                                                                <%#Eval("VI_ProyectosIngresadosNumeroProyectos")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleProyectosIngresadosImporteProy") %>'>
                                                                <%#Eval("VI_ProyectosIngresadosImporteProyecto")%>
                                                            </td>

                                                            <td style='<%# Eval("VI_StyleProyectosPromocionNumeroProyecto") %>'>
                                                                <%#Eval("VI_ProyectosPromocionNumeroProyecto")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleProyectosPromocionImporteProyecto") %>'>
                                                                <%#Eval("VI_ProyectosPromocionImporteProyecto")%>
                                                            </td>
                                                            
                                                            <td style='<%# Eval("VI_StyleCierreNumeroProyectos") %>'>
                                                                <%#Eval("VI_CierreNumeroProyectos")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleCierreImporteProyectos") %>'>
                                                                <%#Eval("VI_CierreImporteProyectos")%>
                                                            </td>
                                                            
                                                            <td style='<%# Eval("VI_StyleCanceladoNumProy") %>'>
                                                                <%#Eval("VI_CanceladoNumProy")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleCanceladoImporteProy") %>'>
                                                                <%#Eval("VI_CanceladoImporteProy")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div runat="server" id="tblReportePorRik" visible="false">
                                        <table class="table table-striped table-bordered">
                                            <col />
                                                <colgroup span="2"></colgroup>
                                                <colgroup span="2"></colgroup>
                                                <colgroup span="2"></colgroup>
                                                <colgroup span="2"></colgroup>
                                                <colgroup span="2"></colgroup>
                                                <thead>
                                                    <tr>
                                                        <th rowspan="3" style="text-align: center;">
                                                            CDI
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Venta Espor&aacute;dica
                                                        </th>
                                                        <th colspan="8" scope="colgroup" style="text-align: center;">
                                                            Venta Instalada
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Proyectos Ingresados en el mes
                                                        </th>

                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Proyectos Ingresados en el mes
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Proyectos Promoción
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Cierre
                                                        </th>
                                                        <th colspan="2" scope="colgroup" style="text-align: center;">
                                                            Cancelados
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>


                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Núm. proy.
                                                        </th>
                                                        <th scope="col" style="text-align: center;">
                                                            Importe proy.
                                                        </th>
                                                    </tr>
                                                </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rptReportePorRIK">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style='<%# Eval("StyleCdNombre") %>'>
                                                                <asp:LinkButton runat="server" ID="lnkbtnRikNombre" Enabled="false" Text='<%#Eval("Rik_Nombre") %>' CommandArgument='<%#Eval("Id_Rik") %>' OnClick="lnkbtnRikNombre_Click" >
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style='<%# Eval("VE_StyleProyectosIngresadosNumProy") %>'>
                                                                <%#Eval("VE_ProyectosIngresadosNumeroProyectos")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VE_StyleProyectosIngresadosImporteProy") %>'>
                                                                <%#Eval("VE_ProyectosIngresadosImporteProyecto")%>
                                                            </td>

                                                            <td style='<%# Eval("VI_StyleProyectosIngresadosNumProy") %>'>
                                                                <%#Eval("VI_ProyectosIngresadosNumeroProyectos")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleProyectosIngresadosImporteProy") %>'>
                                                                <%#Eval("VI_ProyectosIngresadosImporteProyecto")%>
                                                            </td>

                                                            <td style='<%# Eval("VI_StyleProyectosPromocionNumeroProyecto") %>'>
                                                                <%#Eval("VI_ProyectosPromocionNumeroProyecto")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleProyectosPromocionImporteProyecto") %>'>
                                                                <%#Eval("VI_ProyectosPromocionImporteProyecto")%>
                                                            </td>
                                                            
                                                            <td style='<%# Eval("VI_StyleCierreNumeroProyectos") %>'>
                                                                <%#Eval("VI_CierreNumeroProyectos")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleCierreImporteProyectos") %>'>
                                                                <%#Eval("VI_CierreImporteProyectos")%>
                                                            </td>
                                                            
                                                            <td style='<%# Eval("VI_StyleCanceladoNumProy") %>'>
                                                                <%#Eval("VI_CanceladoNumProy")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("VI_StyleCanceladoImporteProy") %>'>
                                                                <%#Eval("VI_CanceladoImporteProy")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div runat="server" id="tblReportePorProyecto" visible="false">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table class="table table-striped table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th style="text-align: center;">
                                                                        Proyecto
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Cliente
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Área
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Solución
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Aplicación
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Productos
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        VTeórico
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Análisis
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Presentación
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Negociación
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Cierre
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Cancelación
                                                                    </th>
                                                                    <th style="text-align: center; white-space:nowrap;">
                                                                        Monto Proyecto
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Fecha Mod.
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Estatus
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Causa
                                                                    </th>
                                                                    <th style="text-align: center;">
                                                                        Comentarios
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater runat="server" ID="rptReporteProyecto">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("IdProyecto")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("NombreCliente")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Area")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Solucion")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Aplicacion")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Productos")%>
                                                                            </td>
                                                                            <td data-numeraljs='"format" : "$0,0.00"' style="text-align: right;">
                                                                                <%# Eval("VTeorico")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Analisis")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Presentacion")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Negociacion")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Cierre")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("Cancelacion")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap; text-align: right;" data-numeraljs='"format" : "$0,0.00"' >
                                                                                <%# Eval("MontoProyecto")%>
                                                                            </td>
                                                                            <td style="white-space:nowrap;">
                                                                                <%# Eval("FechaModificacion")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Estatus")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Causa")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Comentarios")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                        
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                
               </div>
           </div>

               </form>
                
            </div>
            <div class="col-sm-3 col-md-2 col-sm-pull-9 col-md-pull-10 sidebar-pf sidebar-pf-left">
                <div class="panel-group" id="accordion">

                    <!--div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#mnuContentAgenda"><i class="fa fa-calendar">
                                </i>&nbsp; Agenda </a>
                            </h4>
                        </div>
                        <div id="mnuContentAgenda" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <ul class="nav nav-pills nav-stacked">
                                    <li><a href="#"><i class="fa fa-rocket"></i>Inicio</a></li>
                                </ul>
                            </div>
                        </div>
                    </div-->

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><i class="fa fa-eye">
                                </i>&nbsp; Gestión de la Promoción </a>
                            </h4>
                        </div>
                        <div id="collapseOne" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <ul class="nav nav-pills nav-stacked">
                                    <li <%=(int)Session["activeMenu"]==2?"class=\"active\"":"" %>><a href="../../FullDashboard.aspx">
                                        <i class="fa fa-tachometer"></i>Inicio</a></li>
                                    <li <%=(int)Session["activeMenu"]==1?"class=\"active\"":"" %>><a href="../../ProspectosV2.aspx">
                                        <i class="fa fa-street-view"></i>Prospectos</a></li>
                                    <li <%=(int)Session["activeMenu"]==4?"class=\"active\"":"" %>><a href="../../Proyectos_TablaAgrupada.aspx">
                                        <i class="fa fa-cog"></i>Proyectos</a></li>
                                    <!--li <%=(int)Session["activeMenu"]==5?"class=\"active\"":"" %>>
                                    <a href="../../Valuaciones/ListadoValuaciones.aspx">
                                    <i class="fa fa-flask"></i>Valuaciones</a>
                                    </li-->
                                    <!--li>
                                    <a href="#"><i class="fa fa-paper-plane"></i>Propuestas</a>
                                    </li-->
                                    <li <%=(int)Session["activeMenu"]==7?"class=\"active\"":"" %>><a href="../../Reportes/Dinamo/ReporteDinamoFull.aspx">
                                        <i class="fa fa-book"></i>Reporte DINAMO</a></li>
                                    <li <%=(int)Session["activeMenu"]==8?"class=\"active\"":"" %>><a href="ReporteDeVentaDeProyecto.aspx">
                                        <i class="fa fa-book"></i>Reporte Venta de Proyectos</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /col -->
        </div>
    </div>
    <!--Toast messages-->
    <div class="toast-pf toast-pf-max-width toast-pf-top-right alert alert-danger alert-dismissable"
        style="display: none" id="toastDanger">
        <button type="button" class="close" aria-hidden="true" onclick="cerrarToastDanger(jQuery)">
            <span class="pficon pficon-close"></span>
        </button>
        <span class="pficon pficon-error-circle-o"></span>
        <div id="toastDangerMessage">
            Message
        </div>
    </div>
    <div class="toast-pf toast-pf-max-width toast-pf-top-right alert alert-success alert-dismissable"
        style="display: none" id="toastSuccess">
        <button type="button" class="close" aria-hidden="true" onclick="cerrarToastSuccess(jQuery)">
            <span class="pficon pficon-close"></span>
        </button>
        <span class="pficon pficon-ok"></span>
        <div id="toastSuccessMessage">
            Message
        </div>
    </div>
    <div class="toast-pf toast-pf-max-width toast-pf-top-right alert alert-warning alert-dismissable"
        style="display: none" id="toastWarning">
        <button type="button" class="close" aria-hidden="true" onclick="cerrarToastWarning(jQuery)">
            <span class="pficon pficon-close"></span>
        </button>
        <span class="pficon pficon-warning-triangle-o"></span>
        <div id="toastWarningMessage">
            Message
        </div>
    </div>
    <!--Toast messages-->
    <!--Login dialog-->
    <div class="modal fade" id="dvDialogoInicioSesion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="btndvDialogoInicioSesionCerrar" type="button" class="close" data-dismiss="modal"
                        aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H3">
                        Iniciar sesion
                    </h4>
                </div>
                <div class="modal-body">
                    <form id="frmDvDialogoInicioSesion">
                    <div class="form-group">
                        <label for="Cu_User">
                            Usuario
                        </label>
                        <input type="text" id="Username" name="Username" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="Cu_pass">
                            Contraseña
                        </label>
                        <input type="password" id="Password" name="Password" class="form-control" />
                    </div>
                    </form>
                    <div id="wrnDvDialogoInicioSesion" class="alert alert-warning" style="display: none;">
                        <span class="pficon pficon-warning-triangle-o"></span>
                        <div id="msgWrnDvDialogoInicioSesion">
                            Mensaje
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnDvDialogoInicioSesionCerrar" type="button" class="btn btn-default"
                        onclick="redireccionarALogin()" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnDvDialogoInicioSesionLogin"
                        onclick="login(jQuery)">
                        Confirmar
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!--Login dialog-->

    <telerik:radcodeblock runat="server" id="rcb1">

    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm-ns.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm.ui-ns.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.navegacion.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.servicios.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.servicios.navegacion.js")%>"></script>
    <uc:UCPatternflyToast runat="server" ID="ucPatternflyToast1" />

    <script src="<%=Page.ResolveUrl("~/js/patternfly/patternfly.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Modernizr-input.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery.placeholder.min.js")%>"></script>
    
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/servicios/navegacion/Notificaciones.js") %>"></script>
    <uc:UCNotificaciones_js runat="server" ID="UCNotificaciones_js" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/ui/Notificaciones.js") %>"></script>
    <uc:UINotificaciones runat="server" ID="UINotificaciones1" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/crm/navegacion/Notificaciones.js") %>"></script>

    <script>
        if ((typeof (console) == undefined) || (typeof (console) == 'undefined')) {
            window.console = new Object();
            window.console.log = function () {
            };
        }

        $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
            console.log(message);
        };
        // Initialize Datatables
        $(document).ready(function () {
            $('.datatable').dataTable({
                "fnDrawCallback": function (oSettings) {
                    // if .sidebar-pf exists, call sidebar() after the data table is drawn
                    if ($('.sidebar-pf').length > 0) {
                        $(document).sidebar();
                    }
                }
            });

            $('.tooltip-demo').tooltip({
                selector: '[data-toggle=tooltip]',
                container: 'body'
            });

            if (typeof (crmOnReady) != undefined && typeof (crmOnReady) != 'undefined') {
                crmOnReady($);
            }

            if (!Modernizr.input.placeholder) {
                createPlaceholders();
            }
        });

        function login($) {
            $('#wrnDvDialogoInicioSesion').fadeOut();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/Login/',
                data: $('#frmDvDialogoInicioSesion').serialize(),
                cache: false,
                type: 'POST',
                statusCode: {
                    506: function (jqXHR, textStatus, errorThrown) {
                        //Manejar el caso apropiado
                    },
                    507: function (jqXHR, textStatus, errorThrown) {
                        //Manejar el caso apropiado
                    },
                    508: function (jqXHR, textStatus, errorThrown) {
                        //Manejar el caso apropiado
                    },
                    509: function (jqXHR, textStatus, errorThrown) {
                        //Manejar el caso apropiado
                    },
                    510: function (jqXHR, textStatus, errorThrown) {
                        //Manejar el caso apropiado
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#dvDialogoInicioSesion').modal('hide');
                if (_onLoginSuccessful != null) {
                    _onLoginSuccessful();
                }
            }).fail(function (jqXHR, textStatus, error) {
                //Mostrar el toast con el mensaje de error; retirar las llamadas para mostrar el toast en cada uno de los casos de código de respuesta, y solo manejar las acciones de los casos en particular por código.
                $('#wrnDvDialogoInicioSesion #msgWrnDvDialogoInicioSesion').html(jqXHR.responseJSON.Message);
                $('#wrnDvDialogoInicioSesion').fadeIn()
            });
        }

        function redireccionarALogin() {
            self.location = '<%=ApplicationUrl %>' + '/login.aspx';
        }

        function mostrarToast(jqToastElement, jqParent) {
            $(jqToastElement).appendTo($(jqParent));
            $(jqToastElement).fadeIn();
        }

        var _onLoginSuccessful = null;

        function salirDelSistema() {
            window.location = '<%=ApplicationUrl %>' + '/Login.aspx?Id=1';
        }

    </script>
    <script src="<%=Page.ResolveUrl("~/js/placeholder-setup.js")%>"></script>
    </telerik:radcodeblock>
    
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>            
    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/jquery-numeraljs.js")%>"></script>
    <script type="text/javascript">
        function crmOnReady() {
            $('input[type="radio"]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            $('input[type="checkbox"]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            $('#tblContenido').numeraljs();
        }

        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            };
        })();
    </script>

    </telerik:radcodeblock>
</body>
</html>
