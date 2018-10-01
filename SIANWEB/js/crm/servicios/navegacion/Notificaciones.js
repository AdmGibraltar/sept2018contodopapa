///<reference path="~/js/crm-namespaces/crm.servicios.navegacion.js" />

crm.servicios.navegacion.Notificaciones = function () {
    this._entradas = [];
    this._$elemento = null;
    this._$contenedor = null;
    this._esNotificacionFuenteDeComando = false;
    this._$elementoTotalNotificaciones = null;
};

crm.servicios.navegacion.Notificaciones._eliminarUrl = '';
crm.servicios.navegacion.Notificaciones._eliminarNotificacionRIKUrl = '';

//TODO: esto va en UI
crm.servicios.navegacion.Notificaciones.prototype._inicializar = function ($elemento) {
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

crm.servicios.navegacion.Notificaciones.prototype._marcarNotificacionLeida = function (id, onSuccess, onFail, always, onStatusCode) {
    $.ajax({
        url: crm.servicios.navegacion.Notificaciones._marcarNotificacionLeidaUrl + '/?id=' + id,
        type: 'PUT',
        cache: false,
        statusCode: onStatusCode
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response, textStatus, jqXHR);
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (typeof (onFail) != undefined && typeof (onFail) != 'undefined') {
            onFail(jqXHR, textStatus, error);
        }
    }).always(function (jqHXR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always(jqHXR, textStatus, errorThrown);
        }
    });
};

crm.servicios.navegacion.Notificaciones.prototype.crearNotificacionProyecto = function (mensaje, onSuccess, onFailure, always, status) {
    $.ajax({
        url: crm.servicios.navegacion.Notificaciones._crearNotificacionRIKProyectoUrl + '/api/CapRIKNotificacionProyecto/',
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: mensaje,
        statusCode: status
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
};

crm.servicios.navegacion.Notificaciones.prototype.eliminarNotificacionRIK = function (id, onSuccess, onFail, always, onStatusCode) {
    this._esNotificacionFuenteDeComando = true;
    var _this = this;
    $.ajax({
        url: crm.servicios.navegacion.Notificaciones._eliminarNotificacionRIKUrl + '/?id=' + id,
        type: 'DELETE',
        cache: false,
        statusCode: onStatusCode
    }).done(function (response, textStatus, jqXHR) {
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
            onSuccess(response, textStatus, jqXHR);
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (typeof (onFail) != undefined && typeof (onFail) != 'undefined') {
            onFail(jqXHR, textStatus, error);
        }
    }).always(function (jqHXR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always(jqHXR, textStatus, errorThrown);
        }
    });
};

crm.servicios.navegacion.Notificaciones.prototype._retirarElemento = function (menuItemId) {
    this._esNotificacionFuenteDeComando = true;
    var $menuItem = $('#' + menuItemId);
    $menuItem.addClass('animated fadeOutLeft').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
        $menuItem.removeClass('animated fadeOutLeft');
        $menuItem.remove();
    });
};

crm.servicios.navegacion.Notificaciones.prototype._$retirarElemento = function ($menuItem) {
    this._esNotificacionFuenteDeComando = true;
    $menuItem.addClass('animated fadeOutLeft').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
        $menuItem.removeClass('animated fadeOutLeft');
        $menuItem.remove();
    });
};

crm.servicios.navegacion.Notificacion = new crm.servicios.navegacion.Notificaciones();