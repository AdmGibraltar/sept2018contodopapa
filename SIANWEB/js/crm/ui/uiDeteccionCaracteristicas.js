crm.ui.uiDeteccionCaracteristicas = function ($, window) {
    this._$ = $;
    this._window = window;
    this._bSoportaArrastrarYColocar = false;
};

crm.ui.uiDeteccionCaracteristicas.prototype.revisarSoporteArrastrarYColocar = function () {
    var $div = this._$('<div>');
    this._bSoportaArrastrarYColocar = ('draggable' in $div[0]) || ('ondragstart' in $div[0] && 'ondrop' in $div[0]);
    return this._bSoportaArrastrarYColocar;
};

crm.ui.uiDeteccionCaracteristicas.prototype.get_soportaArrastrarYColocar = function () {
    return this._bSoportaArrastrarYColocar;
};

var uiDeteccionCaracteristicas = new crm.ui.uiDeteccionCaracteristicas($, window);