<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargadorImagenesjQPlugin.ascx.cs" Inherits="SIANWEB.js.CargadorImagenesjQPlugin" %>

<script type="text/html" id="tplCargadorImagenes">
    <div class="panel panel-default" id="dvPanelContenedor" style="display: none; position: absolute; left: 30%; top: 30%; width: 50%;">
        <div class="panel-heading">
            <h3 class="panel-title">
                Cargar im&aacute;gen
            </h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="form-inline">
                        <div class="form-group">
                            <label>
                                Direcci&oacute;n &nbsp; <i class="fa fa-globe" aria-hidden="true"></i>
                            </label>
                            <input type="text" id="txtDireccion" class="form-control" style="width: 100%;" />
                        </div>
                    </div>
                    <br/>
                    &oacute;
                    <br/>
                    <button class="btn btn-primary"><i class="fa fa-cloud-upload" aria-hidden="true"></i>&nbsp;Cargar im&aacute;gen</button>
                </div>
            </div>
            <br/>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <table style="width: 100%;">
                        <tbody>
                            <tr>
                                <td style="width: 100%; text-align: right;">
                                    <button class="btn btn-primary" id="btnAceptar">Aceptar</button>
                                </td>
                                <td style="text-align: right;">
                                    <button class="btn btn-default" id="btnCancelar">Cancelar</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            
        </div>
    </div>
</script>

<script type="text/javascript">
    (function ($) {
        $.widget('sianweb.cargadorimagenes', {
            options: {

            },
            _create: function () {
                $(this.element).loadTemplate($('#tplCargadorImagenes'));
                this.$_panelContenedor = $(this.element).find('#dvPanelContenedor');
                this._alAceptar = null;
                //asignar la animación zoomOut cuando se active el comando Aceptar o Cancelar
                var _this = this;
                this.$_panelContenedor.find('#btnAceptar').click(function (e) {
                    _this.$_panelContenedor.addClass('animated zoomOut').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                        _this.$_panelContenedor.removeClass('animated zoomOut');
                        _this.$_panelContenedor.hide();
                    });
                    if (_this._alAceptar != null) {
                        var url = _this.$_panelContenedor.find('#txtDireccion').val();
                        _this._alAceptar(url);
                    }
                });

                this.$_panelContenedor.find('#btnCancelar').click(function (e) {
                    _this.$_panelContenedor.addClass('animated zoomOut').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                        _this.$_panelContenedor.removeClass('animated zoomOut');
                        _this.$_panelContenedor.hide();
                    });
                });
            },
            mostrar: function (alAceptar) {
                var _this = this;
                _this.$_panelContenedor.show();
                this.$_panelContenedor.addClass('animated zoomIn').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                    _this.$_panelContenedor.removeClass('animated zoomIn');
                });
                this._alAceptar = alAceptar;
            }
        });
    })(jQuery);
</script>