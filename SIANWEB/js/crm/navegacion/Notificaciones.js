/// <reference path="~/js/ListControl/crm-ns.js" />
/// <reference path="~/js/ListControl/crm.ui-ns.js" />
/// <reference path="~/js/crm/ui/Notificaciones.js" />
/// <reference path="~/js/crm-namespaces/crm.navegacion.js" />

crm.navegacion.Notificacion = function (opts) {
    this.$_contenido = opts.contenido;
    this._tipo = opts.tipo;
    this._id = opts.id;
};

crm.navegacion.Notificacion.prototype.get_contenido = function () {
    return this.$_contenido;
};

crm.navegacion.Notificacion.prototype.get_Tipo = function () {
    return this._tipo;
};

crm.navegacion.Notificacion.prototype.get_Id = function () {
    return this._id;
};

crm.navegacion.Notificaciones = function () {
    this._entradas = [];
    this._$elemento = null;
    this._$contenedor = null;
    this._esNotificacionFuenteDeComando = false;
    this._$elementoTotalNotificaciones = null;
};

crm.servicios.navegacion.Notificaciones._eliminarUrl = '';
crm.servicios.navegacion.Notificaciones._eliminarNotificacionRIKUrl = '';

crm.navegacion.Notificaciones.prototype.inicializar = function ($elemento) {
    this._$elemento = $elemento;
    this._$contenedor = $elemento.find('.dropdown-menu.infotip ul.list-group');
    this._$elementoTotalNotificaciones = this._$elemento.find('#totalNotificaciones');
    var $entradas = this._$contenedor.find('.list-group-item');
    var _this = this;
    $.each($entradas, function (index, element) {
        var id = $(element).data('idnotificacion');
        var $comandoMarcador = $(element).find('#hlMarcarMensaje');
        $comandoMarcador.click(function () {
            _this._esNotificacionFuenteDeComando = true;
            var $icono = $comandoMarcador.find('i');
            $icono.removeClass();
            $icono.addClass('fa fa-eye');
            $comandoMarcador.attr('data-original-title', 'Notificación leida');
            $comandoMarcador.tooltip('hide');
            $comandoMarcador.tooltip('show');
            //            _this._marcarNotificacionLeida(id, function () {
            //                
            //            },
            //            function () { },
            //            function () { },
            //            {
            //                401: function (jqXHR, textStatus, errorThrown) {
            //                    //self.location='<%=ApplicationUrl %>' + '/login.aspx';
            //                    $('#dvDialogoInicioSesion').modal();
            //                    _onLoginSuccessful = $.proxy(eliminarNotificacionRIK, null, id, menuItemId);
            //                }
            //            });
        });
        _this._entradas.push({ id: id, $elemento: $(element) });
    });

    $(this._$elemento).on('hide.bs.dropdown', function () {
        var ret = !_this._esNotificacionFuenteDeComando;
        _this._esNotificacionFuenteDeComando = false;
        return ret;
    });
};

crm.navegacion.Notificaciones.prototype.crearNotificacionRIK = function (mensaje) {
    crm.servicios.navegacion.Notificacion.crearNotificacionProyecto(mensaje,
    function (response) {
        crm.navegacion.Notificaciones.prototype.agregarNotificacionRIK(new crm.navegacion.Notificacion({
            contenido: mensaje,
            tipo: response.Id_TipoNotificacion,
            id: response.Id_Notificacion 
        }));
    },
    function () {
    },
    function () {
    },
    {

    });
};

crm.navegacion.Notificaciones.prototype.agregarNotificacionRIK = function (notificacion) {
    var notificacionUI = crm.ui.Notificaciones.notificaciones.crearElemento();
    var $elemento = notificacionUI.get_elemento();
    notificacionUI.set_contenido(notificacion.get_contenido());
    notificacionUI.set_Icono(notificacion.get_Tipo());
    $elemento.data('idnotificacion', notificacion.get_Id());
    $elemento.data('_obj_', $elemento);
    var _this = this;
    this._entradas.push({ id: notificacion.get_Id(), $elemento: $elemento });
    _this._$elementoTotalNotificaciones.text(this._entradas.length);
    $(notificacionUI.get_elementoRetirar()).click(function () {
        var id = notificacionUI.get_elemento().data('idnotificacion');
        _this.retirarNotificacionRIK(id, function () { }, function () { }, function () { }, {});
    });
};

crm.navegacion.Notificaciones.prototype.retirarNotificacionRIK = function (id, onSuccess, onFailure, always, status) {
    var _this = this;
    crm.servicios.navegacion.Notificacion.eliminarNotificacionRIK(id,
        function (response) {
            _this._esNotificacionFuenteDeComando = true;
            var entradaElegida = $.grep(_this._entradas, function (element, index) {
                return element.id == id;
            });
            _this._entradas = $.grep(_this._entradas, function (element, index) {
                return element.id != id;
            });
            _this._$retirarElemento(entradaElegida[0].$elemento);
            if (_this._entradas.length > 0) {
                _this._$elementoTotalNotificaciones.addClass('badge');
                _this._$elementoTotalNotificaciones.text(_this._entradas.length);
            } else {
                _this._$elementoTotalNotificaciones.removeClass();
                _this._$elementoTotalNotificaciones.text('');
            }
            if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                onSuccess(response);
            }
        },
        function () {
            if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                onFailure();
            }
        },
        function () {
            if (typeof (always) != undefined && typeof (always) != 'undefined') {
                always();
            }
        }, status
    );
};

crm.navegacion.Notificaciones.prototype._$retirarElemento = function ($menuItem) {
    this._esNotificacionFuenteDeComando = true;
    $menuItem.addClass('animated fadeOutLeft').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
        $menuItem.removeClass('animated fadeOutLeft');
        $menuItem.remove();
    });
};

var Notificaciones = new crm.navegacion.Notificaciones();