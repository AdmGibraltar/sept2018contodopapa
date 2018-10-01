<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisualizarPropuestas.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.VisualizarPropuestas" %>
<%--
 
 / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 
 
PROPUESTA TECNO - ECONOMICA 

/ / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 

--%>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaEconomicaResultados_js.ascx" TagPrefix="uc" TagName="UCPropuestaEconomicaResultados_js" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaEconomicaEdicion_js.ascx" TagPrefix="uc" TagName="UCPropuestaEconomicaEdicion_js" %>
<%@ Register Src="~/js/ListControl/ListControlTabularDetailResultsView_js.ascx" TagPrefix="uc" TagName="ListControlTabularDetailResultsView_js" %>
<%@ Register Src="~/js/CargadorImagenesjQPlugin.ascx" TagPrefix="uc" TagName="CargadorImagenesjQPlugin" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaTecnicaResultadosComparativo.ascx" TagPrefix="uc" TagName="UCPropuestaTecnicaResultadosComparativo" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaTecnicaResultados.ascx" TagPrefix="uc" TagName="UCPropuestaTecnicaResultados" %>
<%@ Register Src="~/Controles/Cliente/UCBootstrapConfirm.ascx" TagPrefix="uc" TagName="UCBootstrapConfirm" %>
<%@ Register Src="~/Controles/Cliente/UCPatternflyToast.ascx" TagPrefix="uc" TagName="UCPatternflyToast" %>
<%@ Register Src="~/Controles/Cliente/DialogoRecurso/UCDialogoRecurso.ascx" TagPrefix="uc" TagName="UCDialogoRecurso" %>
<%@ Register Src="~/js/crm/ui/DialogoRecurso/DialogoRecurso_js.ascx" TagPrefix="uc" TagName="DialogoRecurso_js" %>
<%@ Register Src="~/js/crm/sys/sysAplicacion.ascx" TagPrefix="uc" TagName="sysAplicacion" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" class="no-js">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/patternfly/patternfly-additions.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/animate.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/dragndrop.css")%>">
    <style>
        .toast-pf-top-right-rel {
          left: 20px;
          position: relative;
          right: 20px;
          top: 12px;
          z-index: 1035;
          /* Medium devices (desktops, 992px and up) */
        }
        
        @media print {
          body * {
            visibility: hidden;
          }
          .section-to-print, .section-to-print * {
            visibility: visible;
          }
          
          footer {page-break-after: always;}
          
          /*.section-to-print {
            position: absolute;
            left: 0;
            top: 0;
          }*/
          
          .tab-content > .tab-pane {
                display: block !important;
                opacity: 1 !important;
                visibility: visible !important;
            }
        }
        
        .float-inside
        {
            float: right;
            margin-right: 6px;
            margin-top: -20px;
            position: relative;
            z-index: 2;
            
        }
    </style>
</head>
<body>
    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm-ns.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm.ui-ns.js") %>"></script>
    
    <form id="form1" runat="server">
    <script src="//code.jquery.com/jquery-2.1.4.min.js"></script>
    <uc:UCBootstrapConfirm runat="server" id="ucBootstrapConfirm"></uc:UCBootstrapConfirm>
    <uc:UCPatternflyToast runat="server" id="ucPatternflyToast"></uc:UCPatternflyToast>
    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js")%>"></script>
        
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    
    <script src="<%=Page.ResolveUrl("~/js/patternfly/patternfly.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ListControl/ListControlResultsView.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ListControl/ListControlEditView.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/jquery-numeraljs.js") %>"></script>

    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>">

    <script type="text/html" id="tplEntradaPropuestaTecnicaEdicion">
        <div class="col-xs-6 col-sm-6 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <input type="text" class="panel-title form-control" data-value="CPT_ProductoActual" id="txtCPT_ProductoActual" placeholder="Descripción del producto actual..." maxlength="250"/>
                    <a href="#!" class="float-inside" data-toggle="popover" data-html="true" title=""
                        data-popovercontent="Este apartado sirve para ingresar el nombre o la descripción del producto que actualmente se utiliza con el cliente" data-close="true"
                        data-placement="bottom">
                            <span class="fa fa-info-circle"></span>
                    </a>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12" style="text-align: center;">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <img data-src="CPT_RecursoImagenProductoActual" class="img-rounded" style="width: 140px; height: 140px;" id="imgRIPA" />
                                    <input type="hidden" id="hdnCPT_RecursoImagenProductoActual" data-value="CPT_RecursoImagenProductoActual" />
                                    <input type="hidden" id="hdnId_RecursoImagenProductoActual" data-value="Id_RecursoImagenProductoActual" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <button type="button" class="btn btn-default" id="btnElegirImagenRIPA" data-relimg="imgRIPA" data-relhdncpt="hdnCPT_RecursoImagenProductoActual" data-relhdnid="hdnId_RecursoImagenProductoActual">Elegir imagen</button>
                                    <input type="file" class="form-control" style="display: none;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr/>
                            <table>
                                <tr>
                                    <td>
                                        <h4>Situaci&oacute;n Actual</h4>
                                    </td>
                                    <td style="text-align: center; vertical-align: middle;">
                                        <a href="#!" data-toggle="popover" data-html="true" title=""
                                            data-popovercontent="Este apartado sirve para ingresar la descripción de la situación que atravieza el cliente usando el producto actual" data-close="true"
                                            data-placement="bottom">
                                                <span class="fa fa-info-circle"></span>
                                        </a>
                                    </td>
                                </tr>
                            </table>
                            
                            <textarea data-content="CPT_SituacionActual" style="width: 100%;" id="txtCPT_SituacionActual" maxlength="250"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-6 col-sm-6 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title" data-content="Prd_Descripcion"></h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12" style="text-align: center;">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <img data-src="CPT_RecursoImagenSolucionKey" class="img-rounded" style="width: 140px; height: 140px;" id="imgRISK" />
                                    <input type="hidden" id="hdnCPT_RecursoImagenSolucionKey" data-value="CPT_RecursoImagenSolucionKey" />
                                    <input type="hidden" id="hdnId_RecursoImagenSolucionKey" data-value="Id_RecursoImagenSolucionKey" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <button type="button" class="btn btn-default" id="btnElegirImagenSolucionActual" data-relimg="imgRISK" data-relhdncpt="hdnCPT_RecursoImagenSolucionKey" data-relhdnid="hdnId_RecursoImagenSolucionKey">Elegir imagen</button>
                                    <input type="file" class="form-control" style="display: none;" id="fileSolucionActual" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr/>
                            <table>
                                <tr>
                                    <td>
                                        <h4>Ventajas KEY</h4>
                                    </td>
                                    <td>
                                        <td style="text-align: center; vertical-align: middle;">
                                        <a href="#!" data-toggle="popover" data-html="true" title=""
                                            data-popovercontent="Este apartado sirve para ingresar la descripción de las ventajas que presentará nuestro producto ante la situación actual" data-close="true"
                                            data-placement="bottom">
                                                <span class="fa fa-info-circle"></span>
                                        </a>
                                    </td>
                                    </td>
                                </tr>
                            </table>                            
                            <textarea data-content="CPT_VentajasKey" style="width: 100%;" id="txtCPT_VentajasKey" maxlength="250"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
                <input type="hidden" name ="hfValuacionEstatus" id="hfValuacionEstatus" value = <%  =Valuacion.Vap_Estatus2 %>  />
                <input type="hidden" name ="Imprimiendo" id="Imprimiendo" value="0" />

                <ul class="nav navbar-nav">
                    <li id="navCmdImpresionEncabezado" style="display: block;">
                        <a id="btnPrevisualizar" onclick="PrevisualizarPropuesta('1');" title="Impresión de Encabezado.">
                            <i class="fa fa-print fa-2x" aria-hidden="true"></i>&nbsp;Encabezado
                        </a>
                    </li>
                    <li id="navCmdImpresionPConomica" style="display: block;">
                        <a id="btnPrevisualuzar2" onclick="PrevisualizarPropuesta('2');" title="Impresión de Propuesta Economica.">
                            <i class="fa fa-print fa-2x" aria-hidden="true"></i>&nbsp;P. Economica
                        </a>
                    </li>
                    <li id="navCmdImpresionPTecnica" style="display: block;">
                        <a id="btnPrevisualuzar3" onclick="PrevisualizarPropuesta('3');" title="Impresión de Propuesta Técnica.">
                            <i class="fa fa-print fa-2x" aria-hidden="true"></i>&nbsp;P. Técnica
                        </a>
                    </li>
                    <% if (Valuacion.Vap_Estatus2 == 3)
                    { %>
                        <li id="navCmdEditar">
                        <a href="javascript:editarPropuestas();" data-toggle="tooltip" data-placement="bottom" title="Editar">
                            <i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i>&nbsp;Editar
                        </a>
                        </li>
                    <%} %>
                    <li id="navCmdGuardar" style="display: none;">
                        <a href="javascript:guardarCambiosPropuestas();" data-toggle="tooltip" data-placement="bottom" title="Guardar">
                            <i class="fa fa-floppy-o fa-2x" aria-hidden="true"></i>&nbsp;Guardar
                        </a>
                    </li>
                    <li id="navCmdCancelar" style="display: none;">
                        <a href="javascript:cancelarEdicion();" data-toggle="tooltip" data-placement="bottom" title="Cancelar Edición">
                            <i class="fa fa-times fa-2x" aria-hidden="true"></i>&nbsp;Cancelar
                        </a>
                    </li>
                    <% if (Valuacion.Vap_Estatus2 == 3)
                       { %>
                       <li id="navCmdAceptar" style="display: block;">
                            <a href="#" data-toggle="tooltip" data-placement="bottom" id="aAceptarPropuesta">
                            <i class="fa fa-check fa-2x" aria-hidden="true"></i>&nbsp;Aceptar Propuesta
                            </a>
                       </li>
                    <!--<li><a href="#" data-toggle="tooltip" data-placement="bottom" title="Rechazar Propuesta"><i class="fa fa-thumbs-o-down fa-2x" aria-hidden="true"></i></a></li>--><%} %>
                    <li>
                        <a href="javascript:regresarAProyectos()" data-toggle="tooltip" data-placement="bottom" title="Regresar al listado">
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

    </form>
    <div id="dvCargadorImagenesContenedor">
    </div>
    
    <script src="<%=Page.ResolveUrl("~/js/patternfly/patternfly.min.js")%>"></script>       
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.io-ns.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.com-ns.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm-namespaces/crm.sys-ns.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm/io/ioDeteccionCaracteristicas.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm/com/comDeteccionCaracteristicas.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm/ui/uiDeteccionCaracteristicas.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/crm/ui/DialogoRecurso/DialogoRecurso.js")%>"></script>
    <uc:sysAplicacion runat="server" ID="sysAplicacion1"></uc:sysAplicacion>
    <script type="text/javascript">

        var _acysGeneradoExitosamenteCallback=null;
        var _dialogoRecurso=null;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

        $(document).ready(function(){            

            $(':root').addClass('js').removeClass('no-js');
            $('#dvCargadorImagenesContenedor').cargadorimagenes();
            $('[data-toggle="tooltip"]').tooltip();

            $('#aAceptarPropuesta').click(function(){
                BootstrapConfirm.showWarning('Aceptar Propuesta', 'Está a punto de aceptar esta propuesta y generar el ACYS correspondiente. ¿Desea continuar?', function(){
                    aceptarPropuestaTecnoEconomica('<%=IdVal %>');
                });
            });

            uiDeteccionCaracteristicas.revisarSoporteArrastrarYColocar();
            ioDeteccionCaracteristicas.detectarFileReader();
            comDeteccionCaracteristicas.detectarFormData();

            _dialogoRecurso = new crm.ui.DialogoRecurso({
                elemento: $('#<%=UCDialogoRecurso1.ClientID %>'),
                areaArrastre: $('#<%=UCDialogoRecurso1.AreaArrastreClientID %>'),
                alAceptar: function(archivosCargados){
                    var $renglonElementoPropuestaTecnica=_$renglonElementoPropuestaTecnicaSeleccionado;
                    var $btnImagen=_$btnElegirImagenSeleccionado;
                    var imgId=$btnImagen.data('relimg');
                    var $img=$renglonElementoPropuestaTecnica.find('#' + imgId);
                    $img.attr('src', archivosCargados[0].url);
                    var hdnCPTId=$btnImagen.data('relhdncpt');
                    var $hdnCPT=$renglonElementoPropuestaTecnica.find('#' + hdnCPTId);
                    $hdnCPT.val(archivosCargados[0].url);
                    var hdnId=$btnImagen.data('relhdnid');
                    var $hdnId=$renglonElementoPropuestaTecnica.find('#' + hdnId);
                    $hdnId.val(archivosCargados[0].idRecurso);
                },
                comandoAceptar: $('#<%=UCDialogoRecurso1.ComandoAceptarId %>'),
                comandoArchivos: $('#<%=UCDialogoRecurso1.ComandoArchivosId %>'),
                elementoCampoUrl: $('#<%=UCDialogoRecurso1.CampoURLId %>'),
                idRep: '<%=IdRepositorioPropuesta %>',
                elementoBarraProgresoTransferenciaArchivo: $('#<%=UCDialogoRecurso1.BarraProgresoTransferenciaArchivoId %>'),
                elementoMenuNavegacionNuevoRecurso: $('#<%=UCDialogoRecurso1.MenuNavNuevoRecursoId %>'),
                elementoMenuNavegacionRecursoExistente: $('#<%=UCDialogoRecurso1.MenuNavRepositorioId %>'),
                elementoAcordeonURL: $('#<%=UCDialogoRecurso1.AcordeonElementoURL %>'),
                elementoAcordeonArchivo: $('#<%=UCDialogoRecurso1.AcordeonElementoArchivo %>')
            });

            _dialogoRecurso.inicializar();

            $('[data-toggle="popover"]').popover();
        });
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function aceptarPropuestaTecnoEconomica(idVal){
            
            PatternflyToast.showProgress('Generando ACYS...');
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/AceptarPropuestaTecnoEconomica/?idVal=' + idVal,
                type: 'GET',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(aceptarPropuestaTecnoEconomica, null, idVal);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                PatternflyToast.hideProgress();
                if(_acysGeneradoExitosamenteCallback!=null){
                    _acysGeneradoExitosamenteCallback(response);
                }else{
                    PatternflyToast.showSuccess('El ACYS con folio ' + response + ' de la propuesta ha sido generado satisfactoriamente', 8000);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        PatternflyToast.hideProgress();
                        PatternflyToast.showError(jqXHR.responseJSON.Message, 8000);
                        break;
                }
            }).always(function (jqXHR, textStatus, errorThrown) {
                BootstrapConfirm.hideProgress();
            });
        }

        var _$renglonElementoPropuestaTecnicaSeleccionado=null;
        var _$btnElegirImagenSeleccionado=null;
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function elegirImagen($btnImg, $renglonEntradaSeleccionada, alAceptar){
            console.log("function elegirImagen");
            //establecer los parámetros que representan el contexto de la entrada de la propuesta técnica: url del controlador (para imagen de producto 
            //actual y solución key), id de la entrada de la propuesta técnica
            //la idea es que el diálogo se encargue de llamar al controlador indicado, encargado de realizar la asociación registrando un nuevo recurso o usando uno existente, y devolver
            _$renglonElementoPropuestaTecnicaSeleccionado=$renglonEntradaSeleccionada;
            _$btnElegirImagenSeleccionado=$btnImg;
            //.........o, el diálogo se puede encargar de subir la imagen y devolver el recurso; el proceso de guardado de la propuesta decidirá si usar o no el recurso.
            //Este último método necesitará que se especifique el nivel de la biblioteca en el cual se guardará el recurso.
            _dialogoRecurso.mostrar();
            //$('#dvCargadorImagenesContenedor').cargadorimagenes('mostrar', alAceptar);
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                                          
        function PrevisualizarPropuesta(parameter){
            //Response.Redirect("VisualizarPropuestas.aspx");
            //Response.Redirect("'Propuestas/VisualizarPropuestas.aspx?idCte='");
            $('#hdImprimiendo').val(1);                        
            __doPostBack('btnPrevisualizar', parameter);
            //window.close ();
            //alert('Reporte');        
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function editarPropuestas(){
            
            $propuestaTecnicaResultados.fadeOut();
            $propuestaTecnicaEdicion.fadeIn();
            $propuestaEconomicaResultados.fadeOut();
            $propuestaEconomicaEdicion.fadeIn();

            $('#navCmdImpresionEncabezado').hide();
            $('#navCmdImpresionPTecnica').hide();
            $('#navCmdImpresionPConomica').hide();

            $('#navCmdGuardar').show();
            $('#navCmdEditar').hide();
            $('#navCmdAceptar').hide();
            $('#navCmdCancelar').show();
        }

        function cancelarEdicion(){            
            $propuestaTecnicaEdicion.fadeOut();
            $propuestaTecnicaResultados.fadeIn();
            $propuestaEconomicaResultados.fadeIn();
            $propuestaEconomicaEdicion.fadeOut();

            $('#navCmdImpresionEncabezado').show();
            $('#navCmdImpresionPTecnica').show();
            $('#navCmdImpresionPConomica').show();

            $('#navCmdGuardar').hide();
            $('#navCmdEditar').show();
            $('#navCmdCancelar').hide();
            $('#navCmdAceptar').show();
            //$('#navCmdAceptar').css("display","block"); //Aceptar Hide            
            
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        //Actualiza el resultado de la vista de la propuesta económica basado en las actualizaciones hechas a la fuente
        function PropuestaEconomica_ActualizarVistaResultados(){            
        //function actualizarVistaResultadosPropuestaEconomica(){            
            $propuestaEconomicaResultados.resultadospropuestaeconomica('actualizar', fuentePropuestaEconomica);
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function mostrarVistaResultadosPropuestaEconomica(){            
            $propuestaEconomicaResultados.fadeIn();
            $propuestaEconomicaEdicion.fadeOut();
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function mostrarVistaResultadosPropuestaTecnica(){            
            $propuestaTecnicaEdicion.fadeOut();
            $propuestaTecnicaResultados.fadeIn();
            $('#navCmdGuardar').hide();
            $('#navCmdCancelar').hide();
            $('#navCmdEditar').show();
        }
        
        var _regresarAProyectosFn=null;

        function regresarAProyectos(){
            if(_regresarAProyectosFn!=null){
                _regresarAProyectosFn();
            }
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function guardarCambiosPropuestas(){            
            //persistir los cambios en la fuente de datos
            //impactar el detalle de la vista de reporte
            //mostrar la vista de resultados del reporte
            $('#dvPropuestaTecnicaEdicion').editorpropuestatecnica('actualizarModelo');
            $($propuestaEconomicaEdicion).edicionpropuestaeconomica('actualizarModelo');

            //Mostrar señal de operación en progreso: blockui
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmPropuestas/',
                type: 'PUT',
                cache: false,
                contentType: 'application/json',
                data: JSON.stringify({DatosPropuestaTecnica: {Detalle: fuente}, DatosPropuestaEconomica: {Detalle: fuentePropuestaEconomica}}),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(guardarCambiosPropuestas, null);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                actualizarReporte(fuente);
                mostrarVistaResultadosPropuestaTecnica();
                PropuestaEconomica_ActualizarVistaResultados();
                //actualizarVistaResultadosPropuestaEconomica();
                mostrarVistaResultadosPropuestaEconomica();
                
                $('#navCmdImpresionEncabezado').show();
                $('#navCmdImpresionPTecnica').show();
                $('#navCmdImpresionPConomica').show();
                $('#navCmdAceptar').show();

            }).fail(function (jqXHR, textStatus, errorThrown) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        $(this).modal('hide');
                        //$('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.InnerException.InnerException.ExceptionMessage);
                        $('#toastDanger').fadeIn();
                        setTimeout(function () {
                            $('#toastDanger').fadeOut();
                        }, 3000);
                        break;
                }

                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    onFailure($);
                }
            }).always(function (jqXHR, textStatus, errorThrown) {
                if (typeof (always) != undefined && typeof (always) != 'undefined') {
                    always($);
                }
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        (function ($) {
            if (typeof (crm) == undefined || typeof (crm) == 'undefined') {
                function crm() {
                }
            }

            crm.PropuestaTecnica = function () {
            };

            crm.PropuestaTecnica.ModeloComparacionEntradaDetalle = function ($element) {
                this._$element = $element;
            };

            crm.PropuestaTecnica.ModeloComparacionEntradaDetalle.prototype.actualizarModelo= function(contenedor){
                //el contenedor en este modelo consta de filas, cada una de ellas contiene dos columnas.
                //Los datos a extraer son: CPT_ProductoActual, CPT_SituacionActual, CPT_RecursoImagenProductoActual y CPT_RecursoImagenSolucionKey

                //Obtenemos todos los nodos de primer nivel del contenedor
                var $rows=$(contenedor).find('>.row');
                $.each($rows, function(index, element){
                    var dataObject=$(element).data('_obj_');
                    var $hdnCPT_RecursoImagenProductoActual=$(element).find('#hdnCPT_RecursoImagenProductoActual');
                    dataObject.CPT_ProductoActual=$(element).find('#txtCPT_ProductoActual').val();
                    dataObject.CPT_RecursoImagenProductoActual=$hdnCPT_RecursoImagenProductoActual.val();
                    dataObject.CPT_SituacionActual=$(element).find('#txtCPT_SituacionActual').val();
                    dataObject.CPT_RecursoImagenSolucionKey=$(element).find('#hdnCPT_RecursoImagenSolucionKey').val();
                    dataObject.CPT_VentajasKey=$(element).find('#txtCPT_VentajasKey').val();
                    //la fuente ha sido actualizada, puesto que el acceso a los objetos en js son por referencia.
                });
            };
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            crm.PropuestaTecnica.ModeloComparacionEntradaDetalle.prototype.crear = function (fuente) {
                var row = $('<div class="row">');
                $(row).loadTemplate(this._$element, fuente);
                $(row).data('_obj_', fuente);
                var $btnElegirImagenRIPA=$(row).find('#btnElegirImagenRIPA');
                var $imgRIPA=$(row).find('#imgRIPA');
                var $hdnRIPA=$(row).find('#hdnCPT_RecursoImagenProductoActual');
                $btnElegirImagenRIPA.click(function(){
                    elegirImagen($btnElegirImagenRIPA, row, function(url){
                        $imgRIPA.attr('src', url);
                        $hdnRIPA.val(url);
                    });
                });
                var $btnElegirImagenSolucionActual=$(row).find('#btnElegirImagenSolucionActual');
                var $imgRISK=$(row).find('#imgRISK');
                var $hdnRISK=$(row).find('#hdnCPT_RecursoImagenSolucionKey');
                $btnElegirImagenSolucionActual.click(function(url){
                    elegirImagen($btnElegirImagenSolucionActual, row, function(url){
                        $imgRISK.attr('src', url);
                        $hdnRISK.val(url);
                    });
                });
                return row;
            };
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            crm.PropuestaTecnica.ModeloComparacionEntradaDetalle._instancia = new crm.PropuestaTecnica.ModeloComparacionEntradaDetalle($('#tplEntradaPropuestaTecnicaEdicion'));

            $.widget('crm.editorpropuestatecnica', {
                options: {
                    fuente: null,
                    modeloEntrada: crm.PropuestaTecnica.ModeloComparacionEntradaDetalle._instancia
                },
                _create: function () {
                    if (this.options.fuente != null) {
                        var _this=this;
                        $.each(this.options.fuente, function (index, element) {
                            element.Prd_Descripcion=element.CatProductoSerializable.Prd_Descripcion;
                            var nuevaEntrada = _this.options.modeloEntrada.crear(element);
                            $(_this.element).append(nuevaEntrada);
                            nuevaEntrada.find('[data-toggle="popover"]').popover({
                                content: function(){
                                    return $(this).data('popovercontent');
                                }
                            });
                        });
                        //$('[data-toggle="popover"]').popover();
                    }
                },
                actualizarModelo: function(){
                    this.options.modeloEntrada.actualizarModelo(this.element);
                }
            });
        })(jQuery);

        var $propuestaTecnicaResultados=null;
        var $propuestaTecnicaEdicion = null;
        var $propuestaEconomicaResultados=null;
        var $propuestaEconomicaEdicion=null;

        //
        //Modelo de datos de las vistas de resultados y edición
        // PROPUESTA ECONOMICA
        //
        var fuente = <%=DetallePropuestaSerializado %>;
        var fuentePropuestaEconomica=<%=DetallePropuestaEconomicaSerializado %>;
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        $(document).ready(function () {
            
            $propuestaTecnicaEdicion=$('#dvPropuestaTecnicaEdicion').editorpropuestatecnica({fuente: fuente});
            $propuestaTecnicaResultados=$('#dvPropuestaTecnicaResultados');

            $.each(fuentePropuestaEconomica, function(index, element){
                element.Prd_Descripcion=element.ProductoSerializable.Prd_Descripcion;
                element.Prd_Presentacion=element.ProductoSerializable.Prd_Presentacion;

                if (element.CapValProyectoDet == null) {
                    element.Prd_Precio=0;
                } else {
                    if (element.CapValProyectoDet.Vap_Precio==null) {                    
                        element.Prd_Precio=0;
                    } else {
                        element.Prd_Precio=element.CapValProyectoDet.Vap_Precio;
                    }
                }
                                
                element.COP_CostoEnUso=calcularCostoEnUso(element, element.COP_ConsumoMensual, element.COP_DilucionConsecuente);
                element.ConsumoMensualLitros=element.ProductoSerializable.Prd_UniEmp*element.COP_ConsumoMensual;
                
                if (element.CapValProyectoDet == null) {
                    element.GastoMensual=0;
                } else {
                    if (element.CapValProyectoDet.Vap_Precio==null) {
                        element.GastoMensual=0;
                    } else {
                        element.GastoMensual=element.COP_ConsumoMensual*element.CapValProyectoDet.Vap_Precio;//element.ProductoActual.Prd_Pesos;
                    }
                }
                
                element.ConsumoMensualLtsDiluidos=element.ConsumoMensualLitros*(parseInt(element.COP_DilucionConsecuente)+1);
            });
            $propuestaEconomicaResultados=$('#dvPropuestaEconomicaResultados').resultadospropuestaeconomica({
                initialized: true, 
                dataSource: fuentePropuestaEconomica, 
                idCte: <%=IdCte %>, 
                idVal: <%=IdVal %>
            });

            $propuestaEconomicaEdicion=$('#dvPropuestaEconomicaEdicion').edicionpropuestaeconomica({initialized: true, dataSource: fuentePropuestaEconomica, idCte: <%=IdCte %>, idVal: <%=IdVal %>});

            $('[data-inputmask]').inputmask();
        });
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function calcularCostoEnUso(dataItem, consumoMensual, dilucionConsecuente) {            
            var costoEnUso = 0.0;
            if (consumoMensual != '') {
                if (dataItem.COP_EsQuimico == true) {
                    if (dilucionConsecuente != '') {
                        var precio = dataItem.CapValProyectoDet.Vap_Precio;//ProductoActual.Prd_Pesos;
                        var unidadesPresentacion = dataItem.ProductoSerializable.Prd_UniEmp;
                        var consumoMensualEnLitrosDiluidos = ((unidadesPresentacion * consumoMensual) * (parseInt(dilucionConsecuente) + 1));
                        if (consumoMensualEnLitrosDiluidos != 0.0) {
                            costoEnUso = (consumoMensual * precio) / consumoMensualEnLitrosDiluidos;
                        }
                    }
                }
            }
            return costoEnUso;
        };

    </script>
<uc:CargadorImagenesjQPlugin runat="server" ID="ucCargadorImagenesjQPlugin" />
<uc:UCDialogoRecurso runat="server" ID="UCDialogoRecurso1"></uc:UCDialogoRecurso>
<uc:DialogoRecurso_js runat="server" ID="DialogoRecurso_js1"></uc:DialogoRecurso_js>
</body>

</html>
