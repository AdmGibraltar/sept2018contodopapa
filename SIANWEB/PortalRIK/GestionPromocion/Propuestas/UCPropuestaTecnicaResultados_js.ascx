<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPropuestaTecnicaResultados_js.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.UCPropuestaTecnicaResultados_js" %>

<script type="text/html" id="tplPropuestaTecnicaResultadoDefault">
    <td data-content="CPT_ProductoActual">
    </td>
    <td data-content="CPT_SituacionActual">
    </td>
    <td data-content="Prd_Descripcion">
    </td>
    <td data-content="CPT_VentajasKey">
    </td>
</script>

<script type="text/html" id="tplEntradaPropuestaTecnicaResultados">
    <div class="col-xs-6 col-sm-6 col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title" data-content="CPT_ProductoActual">
                                        
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12" style="text-align: center;">
                        <img data-src="CPT_RecursoImagenProductoActual" class="img-rounded" style="width: 140px; height: 140px;" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr/>
                        <h4>Situaci&oacute;n Actual</h4>
                        <p data-content="CPT_SituacionActual"></p>
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
                        <img data-src="CPT_RecursoImagenSolucionKey" class="img-rounded" style="width: 140px; height: 140px;" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr/>
                        <h4>Ventajas KEY</h4>
                        <p data-content="CPT_VentajasKey"></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</script>

<script type="text/javascript">
    if (typeof (crm) == undefined || typeof (crm) == 'undefined') {
        function crm() {
        }
    }

    crm.PropuestaTecnicaResultados = function () {
    };

    crm.PropuestaTecnicaResultados.ModeloComparacionEntradaDetalle = function ($element) {
        this._$element = $element;
    };

    crm.PropuestaTecnicaResultados.ModeloComparacionEntradaDetalle.prototype.crear = function (fuente) {
        var row = $('<div class="row">');
        $(row).loadTemplate(this._$element, fuente);
        return row;
    };

    crm.PropuestaTecnicaResultados.ModeloComparacionEntradaDetalle._instancia = new crm.PropuestaTecnicaResultados.ModeloComparacionEntradaDetalle($('#tplEntradaPropuestaTecnicaResultados'));

    /**********************************************************************************/

    crm.PropuestaTecnicaResultados.ModeloRenglonTablaEntradaDetalle = function ($element) {
        this._$element = $element;
    };

    crm.PropuestaTecnicaResultados.ModeloRenglonTablaEntradaDetalle.prototype.crear = function (fuente) {
        var row = $('<tr>');
        $(row).loadTemplate(this._$element, fuente);
        return row;
    };

    crm.PropuestaTecnicaResultados.ModeloRenglonTablaEntradaDetalle._instancia = new crm.PropuestaTecnicaResultados.ModeloRenglonTablaEntradaDetalle($('#tplPropuestaTecnicaResultadoDefault'));

    (function ($) {

        $.widget('crm.propuestatecnicaresultado', {
            options: {
                initialized: false,
                modeloEntrada: crm.PropuestaTecnicaResultados.ModeloRenglonTablaEntradaDetalle._instancia,
                idEmp: <%=EntidadSesion.Id_Emp %>,
                idCd: <%=EntidadSesion.Id_Cd %>,
                idRik: <%=EntidadSesion.Id_Rik %>,
                idCte: 0,
                idVal: 0
            },
            _create: function () {
                if (this.options.initialized != true) {
                    this._actualizar();
                }
            },
            _construirCuerpo: function(detalle){
                $(this.element).empty();
                var _this=this;
                $.each(detalle, function(index, element){
                    element.Prd_Descripcion=element.CatProductoSerializable.Prd_Descripcion;
                    var newElement=_this.options.modeloEntrada.crear(element);
                    $(_this.element).append(newElement/*.find(':first-child')*/);
                });
            },
            _actualizar: function () {
                $.ajax({
                    url: '<%=ApplicationUrl %>' + '/api/CrmPropuestaTecnica/?idCliente=' + this.options.idCte + '&idVal=' + this.options.idVal,
                    type: 'GET',
                    cache: false,
                    statusCode: {
                        401: function (jqXHR, textStatus, errorThrown) {
                            $('#dvDialogoInicioSesion').modal();
                            _onLoginSuccessful = $.proxy(this._actualizar, this);
                        }
                    }
                }).done(function (response, textStatus, jqXHR) {
                    _construirCuerpo(response);
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
                }).always(function (jqXHR, textStatus, errorThrown) {
                    
                });
            },
            actualizar: function(detalle){
                this._construirCuerpo(detalle);
            }
        });
    })(jQuery);
</script>