
crm.servicios.gestionpromocion.proyectos.evidencias.SvcEvidencia = function () {
};

crm.servicios.gestionpromocion.proyectos.evidencias.SvcEvidencia.prototype.crearEvidencia = function (idOp, idRecursoArchivo, onSuccess, onFailure, always, status) {
    var data = {
        idOp: idOp,
        idRecursoArchivo: idRecursoArchivo
    };
    $.ajax({
        url: sysAplicacion.get_urlAplicacion() + '/api/CrearEvidencia/',
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
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

crm.servicios.gestionpromocion.proyectos.evidencias.SvcEvidencia.prototype.obtenerEvidenciasDeProyecto = function (idOp, onSuccess, onFailure, always, status) {
    $.ajax({
        url: sysAplicacion.get_urlAplicacion() + '/api/ObtenerEvidencias/?idOp=' + idOp,
        type: 'GET',
        cache: false,
        contentType: 'application/json',
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

var svcEvidencia = new crm.servicios.gestionpromocion.proyectos.evidencias.SvcEvidencia();