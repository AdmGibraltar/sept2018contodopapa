<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisualizarPropuestas_Ver2.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.VisualizarPropuesta_Ver2" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<html xmlns="http://www.w3.org/1999/xhtml">

<body>

    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>                                                 
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>">

    <script src="<%=Page.ResolveUrl("~/js/crm2/Proyectos_tablaAgrupada_PropuestaTE.js")%>"></script>                                                 

    <script src="js/jquery-2.1.4.js" type="text/javascript"></script>    
    
    <asp:Panel ID="pnlAciones" runat="server">    
        <nav class="navbar navbar-default">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#nvcPrincipal" aria-expanded="false">
                    </button>
                    <!--a class="navbar-brand" href="#">Propuestas</a-->
                </div>
                <div class="collapse navbar-collapse" id="nvcPrincipal">
                            
                    <asp:HiddenField id="hfImprimiendo" runat="server" />                
                    <input type="hidden" name ="hfValuacionEstatus" id="hfValuacionEstatus" value =""/>
                    <input type="hidden" name ="Imprimiendo" id="Imprimiendo" value="" />

                    <ul class="nav navbar-nav">
                        <li id="navCmdImpresionEncabezado" style="display: block;">
                            <a id="btnPrevisualizar" onclick="PrevisualizarPropuesta('1');" title="Impresión de Encabezado.">
                                <i class="fa fa-print fa-2x" aria-hidden="true"></i>&nbsp;Encabezado
                            </a>
                        </li>
                        <li id="navCmdImpresionPConomica" style="display: block;">
                            <a id="btnPrevisualuzar2" onclick="PrevisualizarPropuesta('2');"  title="Impresión de Propuesta Economica.">
                                <i class="fa fa-print fa-2x" aria-hidden="true"></i>&nbsp;P. Economica
                            </a>
                        </li>
                        <li id="navCmdImpresionPTecnica" style="display: block;">
                            <a id="btnPrevisualuzar3" onclick="PrevisualizarPropuesta('3');" title="Impresión de Propuesta Técnica.">
                                <i class="fa fa-print fa-2x" aria-hidden="true"></i>&nbsp;P. Técnica
                            </a>
                        </li>
                    
                        <li id="navCmdEditar">
                            <a data-toggle="tooltip" data-placement="bottom" title="Editar">
                                <i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i>&nbsp;Editar
                            </a>
                            </li>                    
                        <li id="navCmdGuardar" style="display: none;">
                            <a data-toggle="tooltip" data-placement="bottom" title="Guardar">
                                <i class="fa fa-floppy-o fa-2x" aria-hidden="true"></i>&nbsp;Guardar
                            </a>
                        </li>
                        <li id="navCmdCancelar" style="display: none;">
                            <a data-toggle="tooltip" data-placement="bottom" title="Cancelar Edición">
                                <i class="fa fa-times fa-2x" aria-hidden="true"></i>&nbsp;Cancelar
                            </a>
                        </li>
                    
                           <li id="navCmdAceptar" style="display: block;">
                                <a href="#" data-toggle="tooltip" data-placement="bottom" id="aAceptarPropuesta">
                                <i class="fa fa-check fa-2x" aria-hidden="true"></i>&nbsp;Aceptar Propuesta
                                </a>
                           </li>
                        <!--<li><a href="#" data-toggle="tooltip" data-placement="bottom" title="Rechazar Propuesta"><i class="fa fa-thumbs-o-down fa-2x" aria-hidden="true"></i></a></li>--><%} %>
                        <li>
                            <a  data-toggle="tooltip" data-placement="bottom" title="Regresar al listado">
                                <i class="fa fa-times fa-2x" aria-hidden="true"></i>&nbsp;Cerrar 
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
        </asp:Panel>
    
    <asp:Panel ID="pnlVisorDeReporte" runat="server">    
        <div id="divReporte" style="display:block;" style="width:100%;" >            
            <%--<telerik:reportviewer id="tReportViewer" runat="server"></telerik:reportviewer>           --%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" style="width:100%;">
            </rsweb:ReportViewer>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlPropuesta" runat="server">    
        <div id="divPropuesta">        
        <ul class="nav nav-tabs">                        
            <li class="active">
                <a href="#dvPropuestaEconomica" data-toggle="tab">Propuesta Econ&oacute;mica</a>
            </li>
            <li>
                <a href="#dvPropuestaTecnica" data-toggle="tab">Propuesta T&eacute;cnica</a>
            </li>
        </ul>
        <div class="tab-content">
            <%--PROPUESTA ECONOMICA 0--%>
            <div role="tabpanel" class="tab-pane active" id="dvPropuestaEconomica">
                <uc:UCPropuestaEconomicaResultados_js runat="server" ID="ucUCPropuestaEconomicaResultados_js" />
                <div id="dvPropuestaEconomicaResultadosContenedor" class="section-to-print">                    
                    <div id="dvPropuestaEconomicaResultados"></div>
                </div>
                <uc:UCPropuestaEconomicaEdicion_js runat="server" ID="ucUCPropuestaEconomicaEdicion_js" />
                <%--PROPUESTA ECONOMICA EDICION 1--%>
                <%--PROPUESTA ECONOMICA EDICION 1--%>                
                <div id="dvPropuestaEconomicaEdicion" style="display: none;"></div>
                <footer>
                <%--PROPUESTA ECONOMICA EDICION FOOTER --%>                
                </footer>
            </div>
            <%--PROPUESTA TECNICA 0--%>
            <div role="tabpanel" class="tab-pane" id="dvPropuestaTecnica">
                <%--PROPUESTA TECNICA --%>
                <%--PROPUESTA TECNICA --%>
                <div id="dvPropuestaTecnicaResultados" class="section-to-print">
                    <uc:UCPropuestaTecnicaResultadosComparativo runat="server" ID="ucUCPropuestaTecnicaResultadosComparativo" />    
                </div>
                <%--PROPUESTA TECNICA --%>
                <%--PROPUESTA TECNICA --%>
                <div id="dvPropuestaTecnicaEdicion" style="display: none;">                    
                </div>
            </div>
        </div>

    </div>
    </asp:Panel>

    </body>
</html>
