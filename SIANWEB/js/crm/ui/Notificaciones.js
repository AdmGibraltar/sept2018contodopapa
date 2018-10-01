/// <reference path="~/js/ListControl/crm-ns.js" />
/// <reference path="~/js/ListControl/crm.ui-ns.js" />
/// <reference path="~/js/jquery-template/jquery.loadTemplate.js" />

crm.ui.Notificacion = function (opts) {
    this._$elemento = opts.elemento;
    this._$elementoIcono = opts.elementoIcono;
    this._$elementoContenido = opts.elementoContenido;
    this._$elementoRetirar = opts.elementoRetirar;
    this._$elementoLeido = opts.elementoLeido;
};

crm.ui.Notificacion.IconoLeido = {
    LEIDO: 0,
    NOLEIDO: 1
};

crm.ui.Notificacion._clasesIconoLeido = [
    'fa fa-eye',
    'fa fa-eye-slash'
];

crm.ui.Notificacion.prototype.set_contenido = function ($contenido) {
    this._$elementoContenido.html($contenido);
};

crm.ui.Notificacion.prototype.get_elementoRetirar = function () {
    return this._$elementoRetirar;
};

crm.ui.Notificacion.prototype.get_ElementoIconoLeido = function () {
    return this._$elementoLeido;
};

crm.ui.Notificacion.prototype.get_ElementoMarcarLeido = function () {
    var $comandoEnlace = (this._$elementoLeido).find('#hlMarcarMensaje');

};

crm.ui.Notificacion.prototype.set_iconoLeido = function (eIconoLeido) {
    $(this._$elementoLeido).removeClass();
    $(this._$elementoLeido).addClass(crm.ui.Notificacion._clasesIconoLeido[crm.ui.Notificacion.IconoLeido]);
};

crm.ui.Notificacion.prototype.marcarComoLeido = function () {
    this.set_iconoLeido(crm.ui.Notificacion.IconoLeido.LEIDO);
    
};

crm.ui.Notificacion.prototype.get_elemento = function () {
    return this._$elemento;
};

crm.ui.IconosNotificacion = {
    OK: 0,
    ERROR: 1,
    INFO: 2,
    MAIL: 3,
    USER: 4, //prospecto
    VALUACION: 5,
    PROYECTO: 6,
    PROPUESTA: 7,
    ACYS: 8
};

crm.ui._ClasesIconosNofificacion = [
    'i pficon pficon-ok',
    'i pficon pficon-error-circle-o',
    'i pficon pficon-info',
    'fa-envelope',
    'i pficon pficon-user',
    'i pficon pficon-rebalance',
    'i pficon pficon-messages',
    'i pficon pficon-blueprint',
    'i pficon pficon-bundle'
];

crm.ui.Notificacion.prototype.set_Icono = function (/*crm.ui.IconosNotificacion*/iconoNotificacion) {
    this._$elementoIcono.removeClass();
    this._$elementoIcono.addClass(crm.ui._ClasesIconosNofificacion[iconoNotificacion]);
};

crm.ui.Notificaciones = function (opts) {
    this._$elemento = opts.elemento;
    this._$nodoPlantilla = opts.nodoPlantilla;
};

crm.ui.Notificaciones.prototype.crearElemento = function () {

    var $elemento = $('<li class="list-group-item" id="idElemento">').loadTemplate(this._$nodoPlantilla);
    var $elementoIcono = $($elemento).find('#spanIcono');
    var $elementoContenido = $($elemento).find('#dvContenido');
    var $elementoRetirar = $($elemento).find('#btnEliminar');
    var $elementoLeido = $($elemento).find('#iIconoLeido');
    var notificacion = new crm.ui.Notificacion({
        elemento: $elemento,
        elementoIcono: $elementoIcono,
        elementoContenido: $elementoContenido,
        elementoRetirar: $elementoRetirar,
        elementoLeido: $elementoLeido 
    });
    $(this._$elemento).append($elemento);
    return notificacion;
};

crm.ui.Notificaciones.prototype.removerElemento = function ($elementoNotificacion) {
    $elementoNotificacion.remove();
};