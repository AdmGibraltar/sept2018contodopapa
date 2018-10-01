<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPropuestaEconomica.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.UCPropuestaEconomica" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaEconomicaResultados_js.ascx" TagPrefix="uc" TagName="UCPropuestaEconomicaResultados_js" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaEconomicaEdicion_js.ascx" TagPrefix="uc" TagName="UCPropuestaEconomicaEdicion_js" %>
<%@ Register Src="~/js/ListControl/ListControlTabularDetailResultsView_js.ascx" TagPrefix="uc" TagName="ListControlTabularDetailResultsView_js" %>
<%@ Register Src="~/js/CargadorImagenesjQPlugin.ascx" TagPrefix="uc" TagName="CargadorImagenesjQPlugin" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaTecnicaResultadosComparativo.ascx" TagPrefix="uc" TagName="UCPropuestaTecnicaResultadosComparativo" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaTecnicaResultados.ascx" TagPrefix="uc" TagName="UCPropuestaTecnicaResultados" %>
<asp:ScriptManagerProxy runat="server" ID="smpScriptManagerProxy">
    <Scripts>
        <asp:ScriptReference Path="~/js/ListControl/ListControlResultsView.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlEditView.js" />
        <asp:ScriptReference Path="~/js/numeraljs/min/numeral.min.js" />
        <asp:ScriptReference Path="~/js/numeraljs/jquery-numeraljs.js" />
    </Scripts>
</asp:ScriptManagerProxy>

<script type="text/html" id="tplEntradaPropuestaTecnicaEdicion">
        <div class="col-xs-6 col-sm-6 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <input type="text" class="panel-title form-control" data-value="CPT_ProductoActual" id="txtCPT_ProductoActual" />
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12" style="text-align: center;">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <img data-src="CPT_RecursoImagenProductoActual" class="img-rounded" style="width: 140px; height: 140px;" id="imgRIPA" />
                                    <input type="hidden" id="hdnCPT_RecursoImagenProductoActual" data-value="CPT_RecursoImagenProductoActual" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <button type="button" class="btn btn-default" onclick="elegirImagen();" id="btnElegirImagenRIPA">Elegir imagen</button>
                                    <input type="file" class="form-control" style="display: none;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr/>
                            <h4>Situaci&oacute;n Actual</h4>
                            <textarea data-content="CPT_SituacionActual" style="width: 100%;" id="txtCPT_SituacionActual"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-6 col-sm-6 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title" data-content="Prd_Descripcion">
                                        
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12" style="text-align: center;">
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <img data-src="CPT_RecursoImagenSolucionKey" class="img-rounded" style="width: 140px; height: 140px;" id="imgRISK" />
                                    <input type="hidden" id="hdnCPT_RecursoImagenSolucionKey" data-value="CPT_RecursoImagenSolucionKey" id="hdnRISK" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <button type="button" class="btn btn-default" onclick="elegirImagen();" id="btnElegirImagenSolucionActual">Elegir imagen</button>
                                    <input type="file" class="form-control" style="display: none;" id="fileSolucionActual" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <hr/>
                            <h4>Ventajas KEY</h4>
                            <textarea data-content="CPT_VentajasKey" style="width: 100%;" id="txtCPT_VentajasKey"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </script>
    <nav class="navbar navbar-default">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#nvcPrincipal" aria-expanded="false">
                </button>
                <a class="navbar-brand" href="#">Propuestas</a>
            </div>
            <div class="collapse navbar-collapse" id="nvcPrincipal">
                <ul class="nav navbar-nav">
                    <li>
                        <a href="javascript:window.print();" data-toggle="tooltip" data-placement="bottom" title="Imprimir Propuesta">
                            <i class="fa fa-print fa-2x" aria-hidden="true"></i>&nbsp;Imprimir
                        </a>
                    </li>
                    <li id="navCmdEditar">
                        <a href="javascript:editarPropuestas();" data-toggle="tooltip" data-placement="bottom" title="Editar Propuesta">
                            <i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i>&nbsp;Editar
                        </a>
                    </li>
                    <li id="navCmdGuardar" style="display: none;">
                        <a href="javascript:guardarCambiosPropuestas();" data-toggle="tooltip" data-placement="bottom" title="Guardar cambios">
                            <i class="fa fa-floppy-o fa-2x" aria-hidden="true"></i>&nbsp;Guardar
                        </a>
                    </li>
                    <li id="navCmdCancelar" style="display: none;">
                        <a href="javascript:cancelarEdicion();" data-toggle="tooltip" data-placement="bottom" title="Cancelar Edición">
                            <i class="fa fa-times fa-2x" aria-hidden="true"></i>&nbsp;Cancelar
                        </a>
                    </li>
                    <li id="navCmdAceptar" style="display: block;">
                        <a href="#" data-toggle="tooltip" data-placement="bottom" title="Aceptar Propuesta">
                            <i class="fa fa-thumbs-o-up fa-2x" aria-hidden="true"></i>&nbsp;Aceptar
                        </a>
                    </li>
                    <li>
                        <a href="#" data-toggle="tooltip" data-placement="bottom" title="Rechazar Propuesta">
                            <i class="fa fa-thumbs-o-down fa-2x" aria-hidden="true"></i>&nbsp;Rechazar
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div>
        <ul class="nav nav-tabs">
            <li>
                <a href="#dvPropuestaEconomica" data-toggle="tab">Propuesta Econ&oacute;mica</a>
            </li>
            <li class="active">
                <a href="#dvPropuestaTecnica" data-toggle="tab">Propuesta T&eacute;cnica</a>
            </li>
        </ul>
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane" id="dvPropuestaEconomica">
                <uc:UCPropuestaEconomicaResultados_js runat="server" ID="ucUCPropuestaEconomicaResultados_js" />
                <div id="dvPropuestaEconomicaResultadosContenedor" class="section-to-print">
                    <div class="row">
                        <div class="col-md-12" style="background-color: #00adef !important; -webkit-print-color-adjust:exact;">
                            <img src="../../../Img/fondo_blanco.png" height="100px" vspace="10" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h1>Propuesta Económica</h1>
                        </div>
                    </div>
                    <div id="dvPropuestaEconomicaResultados">
                    
                    </div>
                </div>
                <uc:UCPropuestaEconomicaEdicion_js runat="server" ID="ucUCPropuestaEconomicaEdicion_js" />
                <div id="dvPropuestaEconomicaEdicion" style="display: none;">
                </div>
                <footer></footer>
            </div>
            <div role="tabpanel" class="tab-pane active" id="dvPropuestaTecnica">
                <div id="dvPropuestaTecnicaResultados" class="section-to-print">
                    <uc:UCPropuestaTecnicaResultadosComparativo runat="server" ID="ucUCPropuestaTecnicaResultadosComparativo" />    
                </div>
                <div id="dvPropuestaTecnicaEdicion" style="display: none;">
                    
                </div>
            </div>
        </div>
    </div>
    <div id="dvCargadorImagenesContenedor">
    </div>
    <script type="text/javascript">
        $(document).ready(function(){
            $('#dvCargadorImagenesContenedor').cargadorimagenes();
            $('[data-toggle="tooltip"]').tooltip();
        });

        function elegirImagen(alAceptar){
            $('#dvCargadorImagenesContenedor').cargadorimagenes('mostrar', alAceptar);
        }


        //Actualiza el resultado de la vista de la propuesta económica basado en las actualizaciones hechas a la fuente
        function PropuestaEconomica_ActualizarVistaResultados(){
        //function actualizarVistaResultadosPropuestaEconomica(){
            $propuestaEconomicaResultados.resultadospropuestaeconomica('actualizar', fuentePropuestaEconomica);
        }
        function PropuestaEconomica_MostrarVistaResultados(){
        //function mostrarVistaResultadosPropuestaEconomica(){
            $propuestaEconomicaResultados.fadeIn();
            $propuestaEconomicaEdicion.fadeOut();
        }

        function PropuestaTecnica_MostrarVistaResultados(){
        //function mostrarVistaResultadosPropuestaTecnica(){
            $propuestaTecnicaEdicion.fadeOut();
            $propuestaTecnicaResultados.fadeIn();
            $('#navCmdGuardar').hide();
            $('#navCmdCancelar').hide();
            $('#navCmdEditar').show();
        }

        //
        // Para modificar CRM2 utilizar los eventos en la forma 
        // VisualizarPropuesta.aspx
        //

        function cancelarEdicion(){
            $propuestaTecnicaEdicion.fadeOut();
            $propuestaTecnicaResultados.fadeIn();
            $propuestaEconomicaResultados.fadeIn();
            $propuestaEconomicaEdicion.fadeOut();

            $('#navCmdGuardar').hide();
            $('#navCmdCancelar').hide();
            $('#navCmdAceptar').show(); // Aceptar Show
            $('#navCmdEditar').show();
        }
        
        function editarPropuestas(){
            $propuestaTecnicaResultados.fadeOut();
            $propuestaTecnicaEdicion.fadeIn();
            $propuestaEconomicaResultados.fadeOut();
            $propuestaEconomicaEdicion.fadeIn();

            $('#navCmdGuardar').show();
            $('#navCmdEditar').hide();
            $('#navCmdAceptar').hide();
            $('#navCmdAceptar').css("display","none"); //Aceptar Hide
            $('#navCmdCancelar').show();
        }

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
                PropuestaTecnica_MostrarVistaResultados();
                //mostrarVistaResultadosPropuestaTecnica();
                PropuestaEconomica_ActualizarVistaResultados();
                //actualizarVistaResultadosPropuestaEconomica();
                //mostrarVistaResultadosPropuestaEconomica();
                PropuestaEconomica_MostrarVistaResultados();
            }).fail(function (jqXHR, textStatus, errorThrown) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        $(this).modal('hide');
                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
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

            crm.PropuestaTecnica.ModeloComparacionEntradaDetalle.prototype.crear = function (fuente) {
                var row = $('<div class="row">');
                $(row).loadTemplate(this._$element, fuente);
                $(row).data('_obj_', fuente);
                var $btnElegirImagenRIPA=$(row).find('#btnElegirImagenRIPA');
                var $imgRIPA=$(row).find('#imgRIPA');
                var $hdnRIPA=$(row).find('#hdnCPT_RecursoImagenProductoActual');
                var _this=this;
                $btnElegirImagenRIPA.click(function(){
                    elegirImagen($btnElegirImagenRIPA, row, function(url){
                        $imgRIPA.attr('src', url);
                        $hdnRIPA.val(url);
                    });
                });
                var $btnElegirImagenSolucionActual=$(row).find('#btnElegirImagenSolucionActual');
                var $imgRISK=$(row).find('#imgRISK');
                var $hdnRISK=$(row).find('#hdnRISK');
                $btnElegirImagenSolucionActual.click(function(url){
                    elegirImagen($btnElegirImagenSolucionActual, row, function(url){
                        $imgRISK.attr('src', url);
                        $hdnRISK.val(url);
                    });
                });
                return row;
            };

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
                        });
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

        //Modelo de datos de las vistas de resultados y edición
        var fuente = <%=DetallePropuestaSerializado %>;
        var fuentePropuestaEconomica=<%=DetallePropuestaEconomicaSerializado %>;
        $(document).ready(function () {
            
            $propuestaTecnicaEdicion=$('#dvPropuestaTecnicaEdicion').editorpropuestatecnica({fuente: fuente});
            $propuestaTecnicaResultados=$('#dvPropuestaTecnicaResultados');

            $.each(fuentePropuestaEconomica, function(index, element){
                element.Prd_Descripcion=element.ProductoSerializable.Prd_Descripcion;
                element.Prd_Presentacion=element.ProductoSerializable.Prd_Presentacion;
                element.Prd_Precio=element.ProductoActual.Prd_Pesos;
            });
            $propuestaEconomicaResultados=$('#dvPropuestaEconomicaResultados').resultadospropuestaeconomica({initialized: true, dataSource: fuentePropuestaEconomica, idCte: <%=IdCte %>, idVal: <%=IdVal %>});
            $propuestaEconomicaEdicion=$('#dvPropuestaEconomicaEdicion').edicionpropuestaeconomica({initialized: true, dataSource: fuentePropuestaEconomica, idCte: <%=IdCte %>, idVal: <%=IdVal %>});

            $('[data-inputmask]').inputmask();
        });
    </script>
    <uc:CargadorImagenesjQPlugin runat="server" ID="ucCargadorImagenesjQPlugin" />