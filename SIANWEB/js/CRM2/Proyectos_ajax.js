
// Proyesctos Ajax.js
// Esta libreria es parte de
// Proyectos_TablaAgrupada y esta pendiente de integrar
// 11 Sep 2018 RFH

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function servicioCancelarProyecto(idCte, idOp, idCausa, onSuccess, onFailure, always) {
    $.ajax({
        url: '<%=ApplicationUrl %>' + '/api/CancelarProyecto',
        type: 'PUT',
        cache: false,
        data: JSON.stringify({
            IdOp: idOp,
            IdCte: idCte,
            IdCausa: idCausa
        }),
        contentType: 'application/json'
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            if (onSuccess != null) {
                onSuccess(response, textStatus, jqXHR);
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            if (onFailure != null) {
                onFailure(jqXHR, textStatus, error);
            }
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            if (always != null) {
                always(jqXHROrData, textStatus, errorOrJQXHR);
            }
        }
    });
}
