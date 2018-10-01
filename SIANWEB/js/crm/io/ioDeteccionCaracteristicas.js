crm.io.ioDeteccionCaracteristicas = function (window) {
    this._window = window;
    this._bSoportaFileReader = false;
};

crm.io.ioDeteccionCaracteristicas.prototype.detectarFileReader = function () {
    this._bSoportaFileReader = 'FileReader' in this._window;
    return this._bSoportaFileReader;
};

crm.io.ioDeteccionCaracteristicas.prototype.get_SoportaFileReader = function () {
    return this._bSoportaFileReader;
};

var ioDeteccionCaracteristicas = new crm.io.ioDeteccionCaracteristicas(window);