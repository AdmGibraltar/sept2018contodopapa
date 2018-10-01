var _conteoNotas = 0;
var _notasaCargar = [];

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function crearPrimeraNota(sender) {
    //Falta establecer el manejador para cuando la petición termina (always)
    crearNota($.proxy(contenidoDePrimeraNota));
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function agregarNota(sender) {
    crearNota($.proxy(contenidoNota));
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function contenidoNota(nota) {
    var _gridStack = $('#grdstkNotas').data('gridstack');

    var widget = $('<div><div class="grid-stack-item-content">' +
            '<div class="panel panel-default">' +
                '<div class="panel-heading" style="padding: 10px 15px 20px 10px !important;">' +
                    '<button type="button" id="btnRemoverNota" class="close" style="line-height:0 !important;" aria-label="Close">' +
                        '<span aria-hidden="true" class="panel-title">&times;</span>' +
                    '</button>' +
    //'<img id="imgDvActualizandoNotaEnProgreso" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" style="display:none; float: right;" /></div>'+
                        '<img id="imgDvActualizandoNotaEnProgreso" src="' + _Page_ResolveUrl + '/Img/patternfly/spinner-xs.gif" style="display:none; float: right;" />' +
                    '</div>' +
                    '<div class="panel-body">' +
                        '<textarea id="txtNota" class="form-control" rows="5" cols="20" placeholder="Escribe aquí tu nota..."></textarea>' +
                    '</div>' +
                '</div>' +
            '</div>' +
            '</div>');

    var btnRemoverNota = $(widget).find('#btnRemoverNota');
    $(btnRemoverNota).click(_removerNota);
    $(btnRemoverNota).data('_grdstkWidget', widget);
    $(btnRemoverNota).data('idNota', nota.Id_Nota);
    var txtNota = $(widget).find('#txtNota');
    $(txtNota).data('_grdstkWidget', widget);
    $(txtNota).data('idNota', nota.Id_Nota);
    $(txtNota).focus(txtNota$alEnfocar);
    $(txtNota).blur(txtNota$alDesenfocar);

    _gridStack.addWidget(widget, _renglonActual, _columnaActual, 4, 2);
    _conteoNotas = _conteoNotas + 1;
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// NOTAS primer nota
function contenidoDePrimeraNota(nota) {
    var _gridStack = $('#grdstkNotas').data('gridstack');

    var widget = $('<div>' +
            '<div class="grid-stack-item-content">' +
                '<div class="panel panel-default">' +
                    '<div class="panel-heading" style="padding: 10px 15px 20px 10px !important;">' +
                        '<button type="button" id="btnRemoverNota" class="close" style="line-height:0 !important;" aria-label="Close">' +
                            '<span aria-hidden="true" class="panel-title">&times;</span>' +
                         '</button>' +
    //'<img id="imgDvActualizandoNotaEnProgreso" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" style="display:none; float: right;" />'+
                            '<img id="imgDvActualizandoNotaEnProgreso" src="' + _Page_ResolveUrl + '/Img/patternfly/spinner-xs.gif" style="display:none; float: right;" />' +
                    '</div>' +
                    '<div class="panel-body">' +
                        '<textarea id="txtNota" class="form-control" rows="5" cols="20" placeholder="Escribe aquí tu nota..."></textarea>' +
                    '</div>' +
                '</div></div>' +
             '</div>');

    var btnRemoverNota = $(widget).find('#btnRemoverNota');
    $(btnRemoverNota).click(_removerNota);
    $(btnRemoverNota).data('_grdstkWidget', widget);
    $(btnRemoverNota).data('idNota', nota.Id_Nota);
    var txtNota = $(widget).find('#txtNota');
    $(txtNota).data('_grdstkWidget', widget);
    $(txtNota).focus(txtNota$alEnfocar);
    $(txtNota).blur(txtNota$alDesenfocar);
    $(txtNota).data('idNota', nota.Id_Nota);

    _gridStack.addWidget(widget, _renglonActual, _columnaActual, 4, 2);
    $('#bsNotasVacias').hide();
    $('#grdstkNotas').show();
    $('#btnAgregarNota').show();
    _conteoNotas = _conteoNotas + 1;
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function reestablecerNotas() {
    _renglonActual = 0;
    _columnaActual = 0;
    _conteoNotas = 0;
    var _gridStack = $('#grdstkNotas').data('gridstack');
    _gridStack.removeAll();
    $.each(_notasaCargar, function (index, element) {
        var widget = $('<div><div class="grid-stack-item-content">' +
                '<div class="panel panel-default"><div class="panel-heading" style="padding: 10px 15px 20px 10px !important;">' +
                '<button type="button" id="btnRemoverNota" class="close" style="line-height:0 !important;" aria-label="Close">' +
                '<span aria-hidden="true" class="panel-title">&times;</span></button>' +
        //'<img id="imgDvActualizandoNotaEnProgreso" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" style="display:none; float: right;" />' +
                '<img id="imgDvActualizandoNotaEnProgreso" src="' + _Page_ResolveUrl + '/Img/patternfly/spinner-xs.gif" style="display:none; float: right;" />' +
                '</div><div class="panel-body">' +
                '<textarea id="txtNota" class="form-control" rows="5" cols="20" placeholder="Escribe aquí tu nota..." id="Nota_' + element.Id_Nota + '">' + element.CatNotaSerializable.Texto +
                '</textarea></div></div></div></div>');

        var btnRemoverNota = $(widget).find('#btnRemoverNota');
        $(btnRemoverNota).click(_removerNota);
        $(btnRemoverNota).data('_grdstkWidget', widget);
        $(btnRemoverNota).data('idNota', element.Id_Nota);
        var txtNota = $(widget).find('#txtNota');
        $(txtNota).data('_grdstkWidget', widget);
        $(txtNota).data('idNota', element.Id_Nota);
        $(txtNota).focus(txtNota$alEnfocar);
        $(txtNota).blur(txtNota$alDesenfocar);

        _gridStack.addWidget(widget, _renglonActual, _columnaActual, 4, 2);
        _conteoNotas = _conteoNotas + 1;
    });
    if (_conteoNotas > 0) {
        $('#bsNotasVacias').hide();
        $('#grdstkNotas').show();
        $('#btnAgregarNota').show();
    } else {
        $('#bsNotasVacias').show();
        $('#grdstkNotas').hide();
        $('#btnAgregarNota').hide();
    }

    _renglonActual = Math.floor(_conteoNotas / 4);
    _columnaActual = _conteoNotas - _renglonActual * 4 - 1;
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function txtNota$alEnfocar() {
    var grdstkWidget = $(this).data('_grdstkWidget');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function txtNota$alDesenfocar() {
    var grdstkWidget = $(this).data('_grdstkWidget');
    $(grdstkWidget).find('#btnRemoverNota').hide();
    $(grdstkWidget).find('#imgDvActualizandoNotaEnProgreso').show();
    var texto = $(this).val();
    var idNota = $(this).data('idNota');
    actualizarNota(idNota, texto, undefined, undefined, function () { $(grdstkWidget).find('#imgDvActualizandoNotaEnProgreso').hide(); $(grdstkWidget).find('#btnRemoverNota').show(); });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// NOTAS Actualizar
function actualizarNota(idNota, texto, onSuccess, onFailure, always) {
    var data = {        
        Id_Emp: _EntidadSesion_Id_Emp,
        Id_Cd: _EntidadSesion_Id_Cd,
        Id_Rik: _EntidadSesion_Id_Rik,
        Id_Cliente: _clienteSeleccionado.Id_Cte,
        Id_Nota: idNota,
        CatNota: {
            Id_Nota: idNota,
            Texto: texto
        },
        CatNotaSerializable: {
            Id_Nota: idNota,
            Texto: texto
        }
    };
    $.ajax({        
        url: _ApplicationUrl + '/api/CapNotasProspecto/',
        type: 'PUT',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(actualizarNota, null, idNota, texto, onSuccess, onFailure, always);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }
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

        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// NOTAS Eliminar
function eliminarNota(idNota, onSuccess, onFailure, always) {
    var data = {        
        Id_Emp: _EntidadSesion_Id_Emp,
        Id_Cd: _EntidadSesion_Id_Cd,
        Id_Rik: _EntidadSesion_Id_Rik,
        Id_Cliente: _clienteSeleccionado.Id_Cte,
        Id_Nota: idNota,
        CatNota: {
            Id_Nota: idNota,
            Texto: ''
        },
        CatNotaSerializable: {
            Id_Nota: idNota,
            Texto: ''
        }
    };
    $.ajax({        
        url: _ApplicationUrl + '/api/CapNotasProspecto/',
        type: 'DELETE',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(actualizarNota, null, idNota, texto, onSuccess, onFailure, always);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }
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

        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function _removerNota() {
    console.log('Remover Nota');
    var grdstkWidget = $(this).data('_grdstkWidget');
    $(grdstkWidget).find('#btnRemoverNota').hide();
    $(grdstkWidget).find('#imgDvActualizandoNotaEnProgreso').show();
    var idNota = $(this).data('idNota');
    eliminarNota(idNota, $.proxy(removerWidget, this, grdstkWidget), undefined, function () {
        $(grdstkWidget).find('#imgDvActualizandoNotaEnProgreso').hide();
        $(grdstkWidget).find('#btnRemoverNota').show();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function removerWidget(grdstkWidget) {
    var _gridStack = $('#grdstkNotas').data('gridstack');
    _gridStack.removeWidget(grdstkWidget);
    _conteoNotas = _conteoNotas - 1;
    if (_conteoNotas == 0) {
        $('#bsNotasVacias').show();
        $('#grdstkNotas').hide();
        $('#btnAgregarNota').hide();
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function createHorizontalSelectors(pairs, jqSelector) {
    $.each(pairs, function (index, element) {
        $(jqSelector).append($('<option>', {
            value: element.value,
            text: element.text
        }));
    });
    $(jqSelector).each(function (index) {
        $(this).horizontalSelector();
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarNotas(data) {
    var idCte = data.Id_Cte;
    $.ajax({        
        url: _ApplicationUrl + '/api/CapNotasProspecto/?idCte=' + idCte,
        type: 'GET',
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarNotas, null, data);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        _notasaCargar = response;
        reestablecerNotas();
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
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function crearNota(onSuccess, onFailure, always) {
    var data = {        
        Id_Emp: _EntidadSesion_Id_Emp,
        Id_Cd: _EntidadSesion_Id_Cd,
        Id_Rik: _EntidadSesion_Id_Rik,
        Id_Cliente: _clienteSeleccionado.Id_Cte,
        Id_Nota: 0,
        CatNotaSerializable: {
            Id_Nota: 0,
            Texto: ''
        }
    };
    $.ajax({        
        url: _ApplicationUrl + '/api/CapNotasProspecto/',
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(crearNota, null, onSuccess, onFailure, always);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }
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

        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
}
