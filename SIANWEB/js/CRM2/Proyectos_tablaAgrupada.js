
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarUENs($, jqElement, onSuccess, onFailure) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatUEN/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var $selUEN = jqElement;
        $selUEN.find('option').remove();
        $selUEN.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            $selUEN.append('<option value="' + element.Id_Uen + '">' + element.Uen_Descripcion + '</option>');
        });

        $selUEN.selectpicker('val', 0);
        $selUEN.selectpicker('refresh');

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess($);
        }
    }).fail(function (jqXHR, textStatus, error) {
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function servicioCancelarProyecto(idCte, idOp, idCausa, onSuccess, onFailure, always) {
    $.ajax({
        url: _ApplicationUrl + '/api/CancelarProyecto',
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarTerritoriosDeProspecto($, jqElement, idRik, idProspecto, onSuccess, onFailure) {
    $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/ProspectoTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {                
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarTerritoriosDeProspecto, null, $, jqElement, idRik, idProspecto, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $selTerritorio = jqElement;
        $selTerritorio.find('option').remove();
        $selTerritorio.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            var node = $('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
            node.data('objterritorio', element);
            $selTerritorio.append(node);
        });
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(4438). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        alertify.error('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos');
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function retirarProductoSvc(idCte, idOp, idPrd, onSuccess, onFailed, always, statusCodeHandlers) {
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        type: 'DELETE',
        cache: false,
        statusCode: statusCodeHandlers
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response, textStatus, jqXHR);
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (typeof (onFailed) != undefined && typeof (onFailed) != 'undefined') {
            onFailed(jqXHR, textStatus, error);
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always(jqXHROrData, textStatus, errorOrJQXHR);
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function retirarProducto(idCte, idOp, idPrd) {
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        type: 'DELETE',
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(retirarProducto, this, idCte, idOp, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstProductos = $('#lstProductos');
        var elem = $lstProductos.find('#lstElem_' + idPrd);
        elem.remove();

        _totalProductos.i = _totalProductos.i - 1;
        if (_totalProductos.i == 0) {
            $('#contenidoSeccionProductos').fadeOut();
            $('#productosBlankSlate').fadeIn();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3973). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function $aceptarEditarCantidad($C, idPrd) {
    var dvCantidadDisplay = $C.find('#dvCantidadDisplay_' + idPrd);
    var dvCantidadEdit = $C.find('#dvCantidadEdit_' + idPrd);
    var dvDilucionDisplay = $C.find('#dvDilucionDisplay_' + idPrd);
    var dvDilucionEdit = $C.find('#dvDilucionEdit_' + idPrd);

    var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
    var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
    var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
    var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');

    //var $lstElem = $C.find('#lstProductos #lstElem_' + idPrd);
    var dataObject = $C.data('objetodatos');
    var objectCopy = jQuery.extend(true, {}, dataObject);
    objectCopy.COP_Cantidad = txtCantidadEdit.val();
    objectCopy.COP_Dilucion = txtDilucionEdit.val();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos', //?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        type: 'PUT',
        cache: false,
        data: JSON.stringify(objectCopy),
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($aceptarEditarCantidad, this, $C, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        dvCantidadDisplay.show();
        dvCantidadEdit.hide();
        dvCantidadDisplayValue.text(txtCantidadEdit.val());

        dvDilucionDisplay.show();
        dvDilucionEdit.hide();
        dvDilucionDisplayValue.text(txtDilucionEdit.val());
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3910). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    });

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function aceptarEditarCantidad(idPrd) {
    var dvCantidadDisplay = $('#dvCantidadDisplay_' + idPrd);
    var dvCantidadEdit = $('#dvCantidadEdit_' + idPrd);
    var dvDilucionDisplay = $('#dvDilucionDisplay_' + idPrd);
    var dvDilucionEdit = $('#dvDilucionEdit_' + idPrd);

    var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
    var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
    var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
    var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');

    var $lstElem = $('#lstProductos #lstElem_' + idPrd);
    var dataObject = $lstElem.data('objetodatos');
    var objectCopy = jQuery.extend(true, {}, dataObject);
    objectCopy.COP_Cantidad = txtCantidadEdit.val();
    objectCopy.COP_Dilucion = txtDilucionEdit.val();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos', //?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        type: 'PUT',
        cache: false,
        data: JSON.stringify(objectCopy),
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(aceptarEditarCantidad, this, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        dvCantidadDisplay.show();
        dvCantidadEdit.hide();
        dvCantidadDisplayValue.text(txtCantidadEdit.val());

        dvDilucionDisplay.show();
        dvDilucionEdit.hide();
        dvDilucionDisplayValue.text(txtDilucionEdit.val());
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3857). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\    
// PRODUCTO AGREGAR OTROS        
function $agregarProductoOtro($C, $tipo, _this) {
    var ProductoClave = $C.find('#txtProductoBusqueda').val();
    var ProductoCantidad = $C.find('#txtProductoCantidad').val();
    var ProductoDescripcion = $C.find('#txtProductoDescripcion').val();
    var Id_Prd = $C.find('#hdnProductoBusqueda').val();

    var fCantidad = parseFloat(ProductoCantidad);
    if (isNaN(fCantidad)) {
        fCantidad = 0;
    }

    if (fCantidad.toString() != ProductoCantidad) {
        fCantidad = 0;
    }

    if (ProductoClave == '' || ProductoCantidad == '' || ProductoDescripcion == '' || Id_Prd == '' || fCantidad == 0) {
        //PatternflyToast.showError('La cantidad o clave de producto no es valido.', 10000);   
        alertify.error('La cantidad o clave de producto no es valido.');
    } else {
        $(_this).prop('disabled', true);
        $C.find('#imgAgregandoProducto').show();
        $.ajax({
            url: _ApplicationUrl + '/api/CrmOportunidadesProductos',
            type: 'POST',
            data: $C.find('#frmAgregarProducto').serialize(),
            cache: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $C, _this);
                }
            }
        }).done(function (response, textStatus, jqXHR) {

            var $lstProductos = $C.find('#lstProductos');
            var n = $crearElementoDeListadoDeProductos($C[0].id, response);
            $lstProductos.append(n);
            var lstElem = $lstProductos.find('#lstElem_' + response.Id_Prd);
            lstElem.data('objetodatos', response);
            $C.find('#txtProductoBusqueda').val('');
            $C.find('#txtProductoCantidad').val('');
            $C.find('#txtProductoDescripcion').val('');
            $C.find('#tdProductoDescripcion').removeClass('has-error');
            $C.find('#spanProductoDescripcionHlp').hide();
            $C.find('#btnAgregarProducto').attr('disabled', true);
            if (_totalOtrosProductos.i == 0) {
                $C.find('#productosBlankSlate').fadeOut();
                $C.find('#contenidoSeccionProductos').fadeIn();
            }
            _totalOtrosProductos.i = _totalOtrosProductos.i + 1;
        }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado(3756). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                case 409:
                    //alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    alertify.error('ERROR: Intenta duplicar el producto.');
                    break;
                default:
                    PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                    break;
            }
            $(_this).prop('disabled', false);
        }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            $C.find('#imgAgregandoProducto').hide();
        });
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// PRODUCTO - AGREGAR de APLICACION         
function $agregarProductoAplicacion($C, _this) {
    var ProductoClave = $C.find('#txtProductoBusqueda').val();
    var ProductoCantidad = $C.find('#txtProductoCantidad').val();
    var ProductoDescripcion = $C.find('#txtProductoDescripcion').val();
    var Id_Prd = $C.find('#hdnProductoBusqueda').val();

    var fCantidad = parseFloat(ProductoCantidad);
    if (isNaN(fCantidad)) {
        fCantidad = 0;
    }
    if (fCantidad.toString() != ProductoCantidad) {
        fCantidad = 0;
    }

    if (ProductoClave == '' || ProductoCantidad == '' || ProductoDescripcion == '' || Id_Prd == '' || fCantidad == 0) {
        //PatternflyToast.showError('La cantidad o clave de producto no es valido.', 10000);   
        alertify.error('La cantidad o clave de producto no es valido.');
    } else {
        $(_this).prop('disabled', true);
        $C.find('#imgAgregandoProducto').show();
        $.ajax({
            url: _ApplicationUrl + '/api/CrmOportunidadesProductos',
            type: 'POST',
            data: $C.find('#frmAgregarProducto').serialize(),
            cache: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $C, _this);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            var $lstProductos = $C.find('#lstProductos');
            var n = $crearElementoDeListadoDeProductos($C[0].id, response);
            $lstProductos.append(n);
            var lstElem = $lstProductos.find('#lstElem_' + response.Id_Prd);
            lstElem.data('objetodatos', response);
            $C.find('#txtProductoBusqueda').val('');
            $C.find('#txtProductoCantidad').val('');
            $C.find('#txtProductoDescripcion').val('');
            $C.find('#tdProductoDescripcion').removeClass('has-error');
            $C.find('#spanProductoDescripcionHlp').hide();
            $C.find('#btnAgregarProducto').attr('disabled', true);
            if (_totalProductos.i == 0) {
                $C.find('#productosBlankSlate').fadeOut();
                $C.find('#contenidoSeccionProductos').fadeIn();
            }
            _totalProductos.i = _totalProductos.i + 1;
        }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado(3684). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                case 409:
                    //alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    alertify.error('ERROR: Intenta duplicar el producto.');
                    break;
                default:
                    //PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                    alertify.error(jqXHR.responseJSON.Message);
                    break;
            }
            $(_this).prop('disabled', false);
        }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            $C.find('#imgAgregandoProducto').hide();
        });
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function agregarProducto(_this) {
    $(_this).prop('disabled', true);
    $('#imgAgregandoProducto').show();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos',
        type: 'POST',
        data: $('#frmAgregarProducto').serialize(),
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(agregarProducto, this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstProductos = $('#lstProductos');
        var n = crearElementoDeListadoDeProductos(response);
        $lstProductos.append(n);
        var lstElem = $lstProductos.find('#lstElem_' + response.Id_Prd);
        lstElem.data('objetodatos', response);
        $('#txtProductoBusqueda').val('');
        $('#txtProductoCantidad').val('');
        $('#txtProductoDescripcion').val('');
        $('#tdProductoDescripcion').removeClass('has-error');
        $('#spanProductoDescripcionHlp').hide();
        $('#btnAgregarProducto').attr('disabled', true);
        /*$('#hdnAgregarProducto_Id_Uen').val('');
        $('#hdnAgregarProducto_Id_Seg').val('');
        $('#hdnAgregarProducto_Id_Area').val('');
        $('#hdnAgregarProducto_Id_Sol').val('');
        $('#hdnAgregarProducto_Id_Apl').val('');
        $('#hdnAgregarProducto_Id_SubFam').val('');*/
        if (_totalProductos.i == 0) {
            $('#productosBlankSlate').fadeOut();
            $('#contenidoSeccionProductos').fadeIn();
        }
        _totalProductos.i = _totalProductos.i + 1;
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3616). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
        $(_this).prop('disabled', false);
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $('#imgAgregandoProducto').hide();
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function _precargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad) {
    _oportunidadSeleccionada = oportunidadSeleccionada;
    _clienteDeOportunidad = clienteDeOportunidad;
    limpiarListadoDeProductos();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, onSuccess);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstProductos = $('#lstProductos');
        $.each(response, function (index, element) {
            var n = crearElementoDeListadoDeProductos(element);
            $lstProductos.append(n);
            var lstElem = $lstProductos.find('#lstElem_' + element.Id_Prd);
            lstElem.data('objetodatos', element);
        });

        //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
        $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
        $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);

        precargarProyectoSeleccionado(clienteDeOportunidad, oportunidadSeleccionada);
        //                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
        //                    onSuccess();
        //                }

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3558). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {

    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Productos
function _cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, onSuccess) {
    //actualizarComandosValuacion(_proyectoSeleccionado);
    cargarInfoSeccionGeneral(_proyectoSeleccionado);
    _oportunidadSeleccionada = oportunidadSeleccionada;
    _clienteDeOportunidad = clienteDeOportunidad;
    limpiarListadoDeProductos();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, onSuccess);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstProductos = $('#lstProductos');
        $.each(response, function (index, element) {
            var n = crearElementoDeListadoDeProductos(element);
            $lstProductos.append(n);
            var lstElem = $lstProductos.find('#lstElem_' + element.Id_Prd);
            lstElem.data('objetodatos', element);
        });

        //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
        $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
        $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3507). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {

    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
// Productos de proyecto
function _cargarProductos(idOp, idCte, onSuccess, onFail, always, statusCodeHandlers) {

    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + idOp + '&Id_Cte=' + idCte,
        cache: false,
        type: 'GET',
       /* statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, onSuccess);
            }
        } */
        //statusCodeHandlers RFH 30 05 2018 
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response, textStatus, jqXHR);
        }
    }).fail(function (jqXHR, textStatus, error) {
        /*if (typeof (onFail) != undefined && typeof (onFail) != 'undefined') {
        onFail(jqXHR, textStatus, error);
        }*/
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3414). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                $('#dvDialogoInicioSesion').modal();
                break;
            default:
                var Messge = '';
                if (typeof (jqXHR.responseJSON.Message) == 'undefined') {
                    Messge = 'No se puede mostrar el error.';
                } else {
                    Messge = jqXHR.responseJSON.Message;
                }

                $('#toastDanger #toastDangerMessage').html(Messge);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }

    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always();
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Carga de 
// Productos de Proyecto
//
function cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, datos) {
    _proyectoSeleccionado = datos;
    //Se evalua si se debe de ocultar el área de productos de aplicación

    // No debe ocultar nada al usuario - RFH 24 Ene 2018
    /*if(_proyectoSeleccionado.Area==-1){
    $('#contenedorProductosDeAplicacion').hide();
    }else{
    $('#contenedorProductosDeAplicacion').show();
    }*/

    cargarInfoSeccionGeneral(datos);
    _oportunidadSeleccionada = oportunidadSeleccionada;
    _clienteDeOportunidad = clienteDeOportunidad;
    //            obtenerRutaDeOferta(clienteDeOportunidad, oportunidadSeleccionada);
    $('#dvDetalles:hidden').slideDown();
    $('#imgCargandoProductos').fadeIn();
    cargarListadoDeProductos(oportunidadSeleccionada, clienteDeOportunidad, _proyectoSeleccionado);

    $('#DetalleProyecto').css('display', 'block');


    //            $.ajax({
    //                url: _ApplicationUrl + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
    //                cache: false,
    //                type: 'GET',
    //                statusCode: {
    //                    401: function (jqXHR, textStatus, errorThrown) {
    //                        $('#dvDialogoInicioSesion').modal();
    //                        _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, datos);
    //                    }
    //                }
    //            }).done(function (response, textStatus, jqXHR) {
    //                _totalProductos=response.length;
    //                $('#txtProductoCantidad').attr('disabled', true);
    //                $('#btnAgregarProducto').attr('disabled', true);
    //                if(response.length>0){
    //                    $('#productosBlankSlate').hide();
    //                    $('#contenidoSeccionProductos').show();
    //                    var $lstProductos = $('#lstProductos');
    //                    $.each(response, function (index, element) {
    //                        var n = crearElementoDeListadoDeProductos(element);
    //                        $lstProductos.append(n);
    //                        var lstElem=$lstProductos.find('#lstElem_' + element.Id_Prd);
    //                        lstElem.data('objetodatos', element);
    //                    });
    //                }else{
    //                    $('#contenidoSeccionProductos').hide();
    //                    $('#productosBlankSlate').show();
    //                }
    //                //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
    //                $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
    //                $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);
    //                
    //            }).fail(function (jqXHR, textStatus, error) {
    //                switch (jqXHR.status) {
    //                    case 401:
    //                        alert('LA sesion ha expirado(3321). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
    //                        break;
    //                    default:
    //                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
    //                        $('#toastDanger').fadeIn();
    //                        setTimeout(function () {
    //                            $('#toastDanger').fadeOut();
    //                        }, 3000);
    //                        break;
    //                }
    //            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    //                $('#imgCargandoProductos').fadeOut();

    //            });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function $obtenerRutaDeOferta($C, idCte, idOp) {
    //deshabilitar y abilitar el comando para agregar productos al proyecto
    $('#btnAgregarProducto').attr('disabled', true);
    $.ajax({
        url: _ApplicationUrl + '/api/OfertaPromocion/?idCte=' + idCte + '&idOp=' + idOp,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {                
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($obtenerRutaDeOferta, null, $C, idCte, idOp);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $C.find('#btnAgregarProducto').attr('disabled', false);
        $asignarValoresACamposDeFormaParaAgregarProducto($C, response);
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3245). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }        
        alertify.error('Ocurrió un error al obtener la información del producto');
        //establecer un tooltip en el comando para señalar el error asociado.
    }).always(function (jqXHR, textStatus, errorThrown) {

    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function obtenerRutaDeOferta(idCte, idOp) {
    //deshabilitar y abilitar el comando para agregar productos al proyecto
    $('#btnAgregarProducto').attr('disabled', true);
    $.ajax({
        url: _ApplicationUrl + '/api/OfertaPromocion/?idCte=' + idCte + '&idOp=' + idOp,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {                
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(obtenerRutaDeOferta, null, idCte, idOp);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#btnAgregarProducto').attr('disabled', false);
        asignarValoresACamposDeFormaParaAgregarProducto(response);
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3214). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }        
        alertify.error('Ocurrió un error al obtener la información del producto');
        //establecer un tooltip en el comando para señalar el error asociado.
    }).always(function (jqXHR, textStatus, errorThrown) {

    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Producto busqueda
function $cargarInfoProductoAplicacion($C, idCte, idOp, idPrd) {
    $C.find('#imgBuscandoProducto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/BusquedaProductoCatalogoUnico/?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($cargarInfoProductoAplicacion, null, $C, idCte, idOp, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response != null) {
            $C.find('#spanProductoDescripcionHlp').hide();
            $C.find('#tdProductoDescripcion').removeClass('has-error');
            $C.find('#txtProductoCantidad').attr('disabled', false);
            $C.find('#txtProductoDescripcion').val(response.CatProductoSerializable.Prd_Descripcion);
            $C.find('#hdnProductoBusqueda').val(response.CatProductoSerializable.Id_Prd);
            $C.find('#btnAgregarProducto').attr('disabled', false);

        } else {
            
            /*$C.find('#tdProductoDescripcion').addClass('has-error');
            $C.find('#spanProductoDescripcionHlp').show();
            $C.find('#txtProductoCantidad').attr('disabled', true);
            $C.find('#tdProductoDescripcion').val('');
            $C.find('#btnAgregarProducto').attr('disabled', true);*/
            alertify.error('El producto ' + idPrd + ' no se encuentra o no existe.');
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3030). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        alertify.error('Ocurrió un error al obtener la información del producto');
    }).always(function (jqXHR, textStatus, errorThrown) {
        $C.find('#imgBuscandoProducto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Producto de producto general
// 
function $cargarInfoProducto($C, id) {
    $C.find('#imgBuscandoProducto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatProducto/?id=' + id,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($cargarInfoProducto, null, $C, id);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response != null) {
            $C.find('#spanProductoDescripcionHlp').hide();
            $C.find('#tdProductoDescripcion').removeClass('has-error');
            $C.find('#txtProductoCantidad').attr('disabled', false);
            $C.find('#txtProductoDescripcion').val(response.Prd_Descripcion);
            $C.find('#hdnProductoBusqueda').val(response.Id_Prd);
            $C.find('#btnAgregarProducto').attr('disabled', false);
        } else {
            $C.find('#tdProductoDescripcion').val('');
            $C.find('#txtProductoCantidad').val('');
            
            /* $C.find('#tdProductoDescripcion').addClass('has-error');
            $C.find('#spanProductoDescripcionHlp').show();
            $C.find('#txtProductoCantidad').attr('disabled', true);
            $C.find('#tdProductoDescripcion').val('');
            $C.find('#btnAgregarProducto').attr('disabled', true);*/
            alertify.error('El producto ' + id + ' no se encuentra o no existe.');
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(2986). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }        
        alertify.error('Ocurrió un error al obtener la información del producto');
    }).always(function (jqXHR, textStatus, errorThrown) {
        $C.find('#imgBuscandoProducto').fadeOut();
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Producto
function cargarInfoProducto(id) {
    $('#imgBuscandoProducto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatProducto/?id=' + id,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarInfoProducto, null, id);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response != null) {
            $('#spanProductoDescripcionHlp').hide();
            $('#tdProductoDescripcion').removeClass('has-error');
            $('#txtProductoCantidad').attr('disabled', false);
            $('#txtProductoDescripcion').val(response.Prd_Descripcion);
            $('#hdnProductoBusqueda').val(response.Id_Prd);
            $('#btnAgregarProducto').attr('disabled', false);
        } else {
            /*
            $('#tdProductoDescripcion').addClass('has-error');
            $('#spanProductoDescripcionHlp').show();
            $('#txtProductoCantidad').attr('disabled', true);
            $('#tdProductoDescripcion').val('');
            $('#btnAgregarProducto').attr('disabled', true);
            */
            alertify.error('El producto ' + id + ' no se encuentra o no existe.');
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(2939). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }        
        alertify.error('El producto ' + idPrd + ' no se encuentra o no existe.');
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgBuscandoProducto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// PRODUCTOS Otros 
function cargarInfoProductoCatalogoUnico($C, id) {
    $C.find('#imgBuscandoProducto').fadeIn();
    $C.find('#txtProductoDescripcion').val('');
    $C.find('#hdnProductoBusqueda').val('');
    $.ajax({
        url: _ApplicationUrl + '/api/BusquedaOtrosProductosCatalogoUnico/?idPrd=' + id,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarInfoProductoCatalogoUnico, null, $C, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response != null) {
            if (response.length > 0) {
                var entradas = $.map(response, function (element, idx) {
                    return { label: element.NombreProducto + ' - ' + element.Ruta, value: element.Id_Prd, data: element };
                });
                $C.find('#txtProductoDescripcion').autocomplete('option', 'source', entradas);
                $C.find('#spanProductoDescripcionHlp').hide();
                $C.find('#tdProductoDescripcion').removeClass('has-error');
                $C.find('#txtProductoCantidad').attr('disabled', false);

                $C.find('#btnAgregarProducto').attr('disabled', false);
                $C.find('#txtProductoDescripcion').autocomplete('search', '');
            } else {
                /*
                $C.find('#tdProductoDescripcion').addClass('has-error');
                $C.find('#spanProductoDescripcionHlp').show();
                $C.find('#txtProductoCantidad').attr('disabled', true);
                $C.find('#tdProductoDescripcion').val('');
                $C.find('#btnAgregarProducto').attr('disabled', true);
                alert('L:2894');*/
                alertify.error('Producto ' + id + ' no se encuentra o no existe.');
            }
        } else {
            //TODO: mostrar una señal para indicar que el producto no fué encontrado
            $C.find('#tdProductoDescripcion').addClass('has-error');
            $C.find('#spanProductoDescripcionHlp').show();
            $C.find('#txtProductoCantidad').attr('disabled', true);
            $C.find('#tdProductoDescripcion').val('');
            $C.find('#btnAgregarProducto').attr('disabled', true);
            alert('L:2904');
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(2894). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }        
        alertify.error('Ocurrió un error al obtener la información del producto');
    }).always(function (jqXHR, textStatus, errorThrown) {
        $C.find('#imgBuscandoProducto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function _buscarProducto(request, response) {
    var terminoDeBusqueda = $('#txtProductoBusqueda').val();
    var data = null;
    $('#imgBuscandoProducto').show();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmCatalogoUnico?idCte=' + _clienteDeOportunidad + '&idOp=' + _oportunidadSeleccionada + '&terminoBusqueda=' + terminoDeBusqueda,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(_buscarProducto, this, request, response);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        data = $.map(response, function (p) {
            return { value: p.Id_Prd, label: p.Id_Prd + ' - ' + p.DescripcionProducto, data: p };
        });
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(2828). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $('#imgBuscandoProducto').hide();
        response(data);
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function _buscarCliente(request, response) {
    var terminoDeBusqueda = $('#selCliente').val();
    var idTer = $('#dvModalEditarProyecto #selTerritorio').selectpicker('val');
    var data = null;
    $.ajax({
        url: _ApplicationUrl + '/api/CatCliente?terminoDeBusqueda=' + terminoDeBusqueda + '&idTer=' + idTer,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(_buscarCliente, this, request, response);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        data = $.map(response, function (p) {
            return { value: p.Id_Cte, label: p.Id_Cte + ' - ' + p.Cte_NomComercial };
        });
    }).fail(function (jqXHR, textStatus, error) {
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        response(data);
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function _buscarProspecto(request, response) {
    var terminoDeBusqueda = $('#txtProspecto').val();
    var $imgProspectoEnOperacion = $('#dvModalEditarProyecto #imgProspectoEnOperacion');
    $imgProspectoEnOperacion.show();
    var data = null;
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProspecto?terminoDeBusqueda=' + terminoDeBusqueda,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(_buscarProspecto, this, request, response);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        data = $.map(response, function (p) {
            return { value: p.Id_CrmProspecto, label: p.Id_CrmProspecto + ' - ' + p.Cte_NomComercial, data: p };
        });
    }).fail(function (jqXHR, textStatus, error) {
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        response(data);
        $imgProspectoEnOperacion.hide();
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function actualizarProyecto() {
    $(this).prop('disabled', true);
    $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
    $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
    $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();

    $('#dvModalEditarProyecto #selTerritorio').prop('disabled', false);
    $('#dvModalEditarProyecto #selCliente').prop('disabled', false);
    $('#dvModalEditarProyecto #txtProspecto').prop('disabled', false);
    $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
    $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
    $('#dvModalEditarProyecto #txtPrecioUnidad').prop('disabled', false);
    $('#dvModalEditarProyecto #selArea').prop('disabled', false);
    $('#dvModalEditarProyecto #selSolucion').prop('disabled', false);

    $.ajax({
        url: _ApplicationUrl + '/api/CrmProyecto',
        type: 'PUT',
        cache: false,
        data: $('#frmDvModalNuevoProyecto').serialize(),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(actualizarProyecto, this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        _proyectoSeleccionado.Dim_Id_Uen = $('#dvModalEditarProyecto #hdnDim_Id_Uen').val();
        _proyectoSeleccionado.Dim_Id_Seg = $('#dvModalEditarProyecto #hdnDim_Id_Seg').val();
        _proyectoSeleccionado.Dim_Cantidad = $('#dvModalEditarProyecto #txtCantidad').val();
        _proyectoSeleccionado.Dim_Descripcion = $('#dvModalEditarProyecto #txtDimension').val();
        _proyectoSeleccionado.VentaPromedioMensualEsperada = $('#dvModalEditarProyecto #txtVPM').val();
        actualizarAplicacionesVPO(_proyectoSeleccionado.Id, function () {
            $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido actualizado con éxito');
            $('#toastSuccess').fadeIn();
            setTimeout(function () {
                $('#toastSuccess').fadeOut();
            }, 3000);
            $('#dvModalEditarProyecto').modal('hide');
        }, function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado(2668). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                default:
                    $('#toastDanger #toastDangerMessage').html('Se presentó una complicación al guardar la información de las aplicaciones. Por favor, revise de nuevo la información y trate de guardarlas nuevamente.');
                    $('#toastDanger').fadeIn();
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 3000);
                    break;
            }
        });

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(2683). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            case 521:
                $('#toastWarning #toastWarningMessage').html('El proyecto ha sido actualizado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage);
                $('#toastWarning').fadeIn();
                setTimeout(function () {
                    $('#toastWarning').fadeOut();
                }, 10000);
                $('#dvModalNuevoProyecto').modal('hide');
                break;
            default:
                //$(this).modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $(this).prop('disabled', false);
        $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
        $('#dvModalEditarProyecto #selUEN').prop('disabled', true);
        $('#dvModalEditarProyecto #selSegmento').prop('disabled', true);

        $('#dvModalEditarProyecto #selTerritorio').prop('disabled', true);
        $('#dvModalEditarProyecto #selCliente').prop('disabled', true);
        $('#dvModalEditarProyecto #txtProspecto').prop('disabled', true);
        $('#dvModalEditarProyecto #selUEN').prop('disabled', true);
        $('#dvModalEditarProyecto #selSegmento').prop('disabled', true);
        $('#dvModalEditarProyecto #txtPrecioUnidad').prop('disabled', true);
        $('#dvModalEditarProyecto #selArea').prop('disabled', true);
        $('#dvModalEditarProyecto #selSolucion').prop('disabled', true);

    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function crearProyectoYContinuar() {
    $(this).prop('disabled', true);
    $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProyecto',
        type: 'POST',
        cache: false,
        data: $('#frmDvModalNuevoProyecto').serialize(),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(crearProyecto, this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito');
        $('#toastSuccess').fadeIn();
        setTimeout(function () {
            $('#toastSuccess').fadeOut();
        }, 3000);
        $('#dvModalEditarProyecto').modal('hide');
        _cargarProductosDeProyecto(response.Id_Op, response.cliente);
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(2593). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            case 521:
                $('#toastWarning #toastWarningMessage').html('El proyecto ha sido creado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage);
                $('#toastWarning').fadeIn();
                setTimeout(function () {
                    $('#toastWarning').fadeOut();
                }, 10000);
                $('#dvModalEditarProyecto').modal('hide');
                break;
            default:
                //$(this).modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $(this).prop('disabled', false);
        $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function crearProyecto() {

    var txtProspecto = $('#txtProspecto').val();
    txtProspecto = txtProspecto.trim();
    if (txtProspecto.length <= 0) {
        alertify.error('ERROR : Faltan los datos del prospecto.');
        return;
    }

    $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
    $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
    $(this).prop('disabled', true);
    $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProyectoV2',
        type: 'POST',
        cache: false,
        data: $('#frmDvModalNuevoProyecto').serialize(),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(crearProyecto, this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito');
        $('#toastSuccess').fadeIn();
        setTimeout(function () {
            $('#toastSuccess').fadeOut();
        }, 5000);
        $('#dvModalEditarProyecto').modal('hide');
        $(_tablaProyectos.table().container()).block({ message: '<img src=\'<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>\' />Actualizando' });
        _tablaProyectos.ajax.reload(function () {
            $(_tablaProyectos.table().container()).unblock();
        });

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado (2538). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            case 521:
                $('#toastWarning #toastWarningMessage').html('El proyecto ha sido creado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage);
                $('#toastWarning').fadeIn();
                setTimeout(function () {
                    $('#toastWarning').fadeOut();
                }, 10000);
                $('#dvModalEditarProyecto').modal('hide');
                break;
            default:
                //$(this).modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);

                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $(this).prop('disabled', false);
        $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
        $('#dvModalEditarProyecto #selUEN').prop('disabled', true);
        $('#dvModalEditarProyecto #selSegmento').prop('disabled', true);
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function actualizarAplicacionesVPO(idOp, onSuccess, onFailure) {
    $.each(_aplicacionesSeleccionadas, function (index, item) {
        item.Id_Op = idOp;
    });
    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesAplicacion',
        type: 'PUT',
        cache: false,
        data: JSON.stringify({
            IdOp: idOp,
            OportunidadesAplicacion: _aplicacionesSeleccionadas
        }),
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(actualizarAplicacionesVPO, this, idOp, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        _aplicacionesSeleccionadas = [];
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
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarAplicaciones($, jqElement, idSol, onSuccess, onFailure) {
    $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeIn();
    var idUen = $('#dvModalEditarProyecto #selUEN').selectpicker('val');
    var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
    var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
    var idCte = $('#dvModalEditarProyecto #hdnId_Cliente').val();
    var idOp = $('#dvModalEditarProyecto #hdnId_Op').val();
    var idOpVar = idOp != null ? idOp : '0';
    $.ajax({
        url: _ApplicationUrl + '/api/CatAplicacion/?idUen=' + idUen + '&idSeg=' + idSeg + '&idArea=' + idArea + '&idSol=' + idSol + '&idOp=' + idOpVar + '&idCte=' + idCte,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                //self.location=_ApplicationUrl + '/login.aspx';
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarAplicaciones, null, $, jqElement, idSol, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstAplicacion = $('#lstAplicacion');
        $lstAplicacion.find('div').remove();

        jqElement.find('option').remove();
        //jqElement.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            jqElement.append('<option value="' + element.Id_Apl + '">' + element.Apl_Descripcion + '</option>');
            var node = $(contenidoPersonalizadoAplicacion(element, index));
            node.data('obj', element);
            node.find('[chkAplicacion]').data('obj', element);
            node.find('#txtAplVPO_' + element.Id_Apl).inputmask();
            //node.find('#txtAplVPO_' + element.Id_Apl).data('obj', element);
            $lstAplicacion.append(node);
        });
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');
        $($lstAplicacion).iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue'
        });
        $('input[chkAplicacion]').on('ifChecked', function (event) {
            var valoresAps = $('#selAplicacion').selectpicker('val');
            var apId = $(event.target).data('idapl');
            if (valoresAps == null) {
                valoresAps = [apId];
            } else {
                valoresAps.push(apId);
            }

            var apOpObj = $('#txtAplVPO_' + apId).data('obj');
            if (apOpObj == null) {
                var apObj = $(event.target).data('obj');
                apOpObj = {
                    Id_Emp: apObj.Id_Emp,
                    Id_Cd: '<%=EntidadSesion.Id_Cd %>',
                    Id_Op: _proyectoSeleccionado != null ? _proyectoSeleccionado.Id : 0,
                    Id_Apl: apObj.Id_Apl,
                    CrmOpAp_VPO: 0
                };
                $('#txtAplVPO_' + apId).data('obj', apOpObj);
            }
            _aplicacionesSeleccionadas.push(apOpObj);
            $('#txtAplVPO_' + apId).show();
            $('#selAplicacion').selectpicker('val', valoresAps);
            $('#selAplicacion').selectpicker('refresh');
        });
        $('input[chkAplicacion]').on('ifUnchecked', function (event) {
            var valoresAps = $('#selAplicacion').selectpicker('val');
            var apId = $(event.target).data('idapl');
            valoresAps = $.grep(valoresAps, function (value) {
                return value != apId;
            });
            _aplicacionesSeleccionadas = $.grep(_aplicacionesSeleccionadas, function (value) {
                return value.Id_Apl != apId;
            });
            $('#txtAplVPO_' + apId).hide();
            $('#selAplicacion').selectpicker('val', valoresAps);
            $('#selAplicacion').selectpicker('refresh');
        });
        habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto();
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess($);
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 407:
                alert('LA sesion ha expirado (2151). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Aplicaciones para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeOut();
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarSoluciones($, jqElement, idArea, onSuccess, onFailure) {
    $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatSolucion/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idArea=' + idArea,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                //self.location=_ApplicationUrl + '/login.aspx';
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarSoluciones, null, $, jqElement, idArea, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        jqElement.find('option').remove();
        jqElement.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            jqElement.append('<option value="' + element.Id_Sol + '">' + element.Sol_Descripcion + '</option>');
        });
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');

        habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto();
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess($);
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(2042). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Soluciones para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarAreas($, jqElement, idSeg, onSuccess, onFailure) {
    $('#imgProcesandoAreaDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatArea/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idSeg=' + idSeg,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {                
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarAreas, null, $, jqElement, idSeg, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        jqElement.find('option').remove();
        jqElement.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            jqElement.append('<option value="' + element.Id_Area + '">' + element.Area_Descripcion + '</option>');
        });
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');

        habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto();

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(1997). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Áreas para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoAreaDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarTerritorios($, jqElement, idSeg, onSuccess, onFailure) {
    $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>' + '&idSeg=' + idSeg,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {                
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarTerritorios, null, $, jqElement, idSeg, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $selTerritorio = jqElement;
        $selTerritorio.find('option').remove();
        $selTerritorio.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            $selTerritorio.append('<option value="' + element.Id_Ter + '">' + element.Ter_Nombre + '</option>');
        });
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(1950). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }

        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarTerritoriosPorRIK($, jqElement, idRik, onSuccess, onFailure) {
    $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + idRik,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {                
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarTerritoriosPorRIK, null, $, jqElement, idRik, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $selTerritorio = jqElement;
        $selTerritorio.find('option').remove();
        $selTerritorio.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            var node = $('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
            node.data('objterritorio', element);
            $selTerritorio.append(node);
        });
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(1905). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }

        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarSegmentos($, jqElement, idUen, onSuccess, onFailure) {
    //mostrar el indicador de operación en proceso
    $('#imgProcesandoSegmentoDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatSegmento/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idUen=' + idUen,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {                
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarSegmentos, null, $, jqElement, idUen, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $selSegmento = jqElement;
        $selSegmento.find('option').remove();
        $selSegmento.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            $selSegmento.append('<option value="' + element.Id_Seg + '">' + element.Seg_Descripcion + '</option>');
        });
        $selSegmento.selectpicker('val', 0);
        $selSegmento.selectpicker('refresh');

        habilitarSelectorDependienteDelSelectorUENDialogoNuevoProyecto();

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess($);
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(1848). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                break;
        }
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar los segmentos para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoSegmentoDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function editarProyecto(row) {
    $('#btnDvModalEditarProyectoGuardar').hide();
    $('#btnDvModalEditarProyectoActualizar').show();
    $('#dvModalEditarProyecto').find('#myModalLabel').text('Editar Proyecto');
    var datos = row;
    _proyectoSeleccionado = datos;
    limpiarFormaNuevoProyecto();
    $('#hdnId_Op').val(datos.Id);
    $('#dvModalEditarProyecto #selTipoCliente').selectpicker('val', datos.CrmTipoCliente);
    $('#dvModalEditarProyecto #selTipoCliente').selectpicker('refresh');
    selTipoCliente_onchange($);
    switch (datos.CrmTipoCliente) {
        case 1:
            $('#dvModalEditarProyecto #txtProspecto').val(datos.NombreCte);
            break;
        case 2:
            $('#dvModalEditarProyecto #selCliente').val(datos.NombreCte);
            break;
    }
    $('#hdnId_Cliente').val(datos.Id_Cte);
    $('#hdnId_CrmProspecto').val(datos.Id_CrmProspecto);
    $('#txtCantidad').val(datos.Dim_Cantidad);
    if (datos.Dim_Id_Uen != null) {
        $('#hdnDim_Id_Uen').val(datos.Dim_Id_Uen);
    } else {
        $('#hdnDim_Id_Uen').val(null);
    }
    if (datos.Dim_Id_Seg != null) {
        $('#hdnDim_Id_Seg').val(datos.Dim_Id_Seg);
    } else {
        $('#hdnDim_Id_Seg').val(null);
    }
    if (datos.Dim_Cantidad != null) {
        $('#txtCantidad').val(datos.Dim_Cantidad);
    } else {
        $('#txtCantidad').val('');
    }

    $('#txtDimension').val(datos.Dim_Descripcion);
    $('#txtVPM').val(datos.VentaPromedioMensualEsperada);

    $('#rbVtaInstalada').iCheck('uncheck');
    $('#rbVtaEsporadica').iCheck('uncheck');
    if (datos.Crm_TipoVenta == 1) {
        $('#rbVtaInstalada').iCheck('check');
    } else {
        $('#rbVtaEsporadica').iCheck('check');
    }

    cargarProductosDeProyecto(datos.Id, datos.Id_Cte, datos);

    inhabilitarCamposDeEdicion();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProyecto?opId=' + datos.Id + '&idCte=' + datos.Id_Cte,
        method: 'GET',
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(editarProyecto, this, row);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#hdnId_Op').val(datos.Id);
        $('#dvModalEditarProyecto #txtProspecto').val(datos.NombreCte);
        //Se establece el UEN del proyecto
        var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
        cargarTerritoriosPorRIK($, $selTerritorio, '<%=EntidadSesion.Id_Rik %>', jQuery.proxy(territoriosCargadosParaEdicion, null, $, $selTerritorio, response));
        //TODO: cargar los territorios asociados al cliente
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(1692). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                if (jqXHR.responseJSON == typeof (undefined)) {
                    $('#toastDanger #toastDangerMessage').html('Ocurrio un error y no es posible desplegar la descripción.');
                } else {
                    //$(this).modal('hide');
                    $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                    $('#toastDanger').fadeIn();
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 3000);
                }
                break;
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function bloquearProyecto(idCte) {
    //            _proyectoSeleccionado.EnValuacion=true;
    //            $.ajax({
    //                url: _ApplicationUrl + '/api/CrmProyecto',
    //                type: 'PUT',
    //                cache: false,
    //                data: JSON.stringify(_proyectoSeleccionado),
    //                contentType: 'application/json',
    //                statusCode: {
    //                    401: function (jqXHR, textStatus, errorThrown) {
    //                        $('#dvDialogoInicioSesion').modal();
    //                        _onLoginSuccessful = $.proxy(bloquearProyecto, this, idCte);
    //                    }
    //                }
    //            }).done(function (response, textStatus, jqXHR) {
    //                $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?Id_Vap=0&Id_Emp=0&Id_Cd=0&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    //                _cargarProductosDeProyecto(_proyectoSeleccionado.Id, _proyectoSeleccionado.Id_Cte);
    //                $('#dvCuerpoVentanaValuacion').block({message: 'Cargando...'});
    //            }).fail(function (jqXHR, textStatus, error) {
    //                switch (jqXHR.status) {
    //                    case 401:
    //                        alert('LA sesion ha expirado(1567). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
    //                        break;
    //                    default:
    //                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
    //                        $('#toastDanger').fadeIn();
    //                        setTimeout(function () {
    //                            $('#toastDanger').fadeOut();
    //                        }, 3006);
    //                        break;
    //                }
    //            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    //            });
    //Se utiliza para determinar la rutina a llamar después de que la operación de la valuación (nueva o edición) termine con éxito.
    _modoValuacion = 0;
    $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?Id_Vap=0&Id_Emp=0&Id_Cd=0&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacion').block({ message: 'Cargando...' });
}


