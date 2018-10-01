crm.com.comDeteccionCaracteristicas = function (window) {
    this._window = window;
    this._bSoportaFormData = false;
};

crm.com.comDeteccionCaracteristicas.prototype.detectarFormData = function () {
    this._bSoportaFormData = 'FormData' in this._window;
    return this._bSoportaFormData;
};

crm.com.comDeteccionCaracteristicas.prototype.get_SoportaFormData = function () {
    return this._bSoportaFormData;
};

var comDeteccionCaracteristicas = new crm.com.comDeteccionCaracteristicas(window);
 