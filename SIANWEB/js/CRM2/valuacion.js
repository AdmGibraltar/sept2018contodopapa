// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarVentanaValuacion_Generada(id) {
    $('#dvModalValuacion').modal('hide');
    recargarListadoProyectos();
    alertify.success('La valuación ' + id + ' ha sido creada con éxito.');
    //                if(_thisWindow!=null){
    //                    _thisWindow.location.assign(_thisWindow.location.origin + _thisWindow.location.pathname);
    //                }
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarVentanaValuacion_Actualizada(id) {
    $('#dvModalValuacion').modal('hide');
    recargarListadoProyectos();
    alertify.success('La valuación ' + id + ' ha sido actualizada con éxito');
    //                if(_thisWindow!=null){
    //                    _thisWindow.location.assign(_thisWindow.location.origin + _thisWindow.location.pathname);
    //                }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$('#iframeVentanaValuacion').on('load', function () {
    if (_modoValuacion == 0) {
        $('#iframeVentanaValuacion')[0].contentWindow._externalCustomFn = cerrarVentanaValuacion_Generada;
    } else {
        $('#iframeVentanaValuacion')[0].contentWindow._externalCustomFn = cerrarVentanaValuacion_Actualizada;
    }

    $('#dvCuerpoVentanaValuacion').unblock();
});

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function editarValuacion(idEmp, idCd, idVal, idCte, SoloLectura) {

    var params =
    'Parametro_IdTU=' + _Parametro_IdTU +
    '&Parametro_IdRik=' + _Parametro_IdRik +
    '&CRM_Gerente_Id=' + _CRM_Gerente_Id +
    '&CRM_Gerente_Rik=' + _CRM_Gerente_Rik +
    '&CRM_Usuario_Id=' + _CRM_Usuario_Id +
    '&CRM_Usuario_Rik=' + _CRM_Usuario_Rik;

    _modoValuacion = 1;
    $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?' + params + '&SoloLectura=' + SoloLectura + '&Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacion').block({ message: 'Cargando...' });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function editarValuacionGlobal(idEmp, idCd, idVal, idCte) {
    //Se utiliza para determinar la rutina a llamar después de que la operación de la valuación (nueva o edición) termine con éxito.
    _modoValuacionGlobal = 1;
    $('#iframeVentanaValuacionGlobal').attr('src', 'Valuaciones/CapValGlobalProyectos.aspx?Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacionGlobal').block({ message: 'Cargando...' });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function bloquearProyecto(idCte) {

    var params =
    'Parametro_IdTU=' + _Parametro_IdTU +
    '&Parametro_IdRik=' + _Parametro_IdRik +
    '&CRM_Gerente_Id=' + _CRM_Gerente_Id +
    '&CRM_Gerente_Rik=' + _CRM_Gerente_Rik +
    '&CRM_Usuario_Id=' + _CRM_Usuario_Id +
    '&CRM_Usuario_Rik=' + _CRM_Usuario_Rik;

    alertify.success('Valuación: ' + _Parametro_ControlesSoloLectura);
    var SoloLectura = _Parametro_ControlesSoloLectura;

    _modoValuacion = 0;
    $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?'+ params +'&SoloLectura=' + SoloLectura + '&Id_Vap=0&Id_Emp=0&Id_Cd=0&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacion').block({ message: 'Cargando...' });

    //            _proyectoSeleccionado.EnValuacion=true;
    //            $.ajax({
    //                url: '<%=ApplicationUrl %>' + '/api/CrmProyecto',
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
    //                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function VerValuacion(obj) {
    var idCte = $(obj).data('idcte');
    var modo = $(obj).data('modo');

    alertify.success('Se visualiza Valuacion en modo: ' + _Parametro_ControlesSoloLectura);
    
    $('#dvModalValuacion').on('show.bs.modal', function (event) {        
        if (modo == 0) {
            bloquearProyecto(idCte);
        } else {
            var idVal = $(obj).data('idval');
            // * _Parametro_ControlesSoloLectura del gerente
            editarValuacion(_EntidadSesion_Id_Emp, _EntidadSesion_Id_Cd, idVal, idCte, _Parametro_ControlesSoloLectura);
        }
    }).modal('show');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function editarValuacionGlobal(idEmp, idCd, idVal, idCte) {    
    _modoValuacionGlobal = 1;
    $('#iframeVentanaValuacionGlobal').attr('src', 'Valuaciones/CapValGlobalProyectos.aspx?Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacionGlobal').block({ message: 'Cargando...' });
}


/*
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$('#dvModalValuacion').on('show.bs.modal', function (event) {
    var idCte = $(event.relatedTarget).data('idcte');
    var modo = $(event.relatedTarget).data('modo');
    if (modo == 0) {
        bloquearProyecto(idCte/*_clienteDeOportunidad*);
    } else {
        var idVal = $(event.relatedTarget).data('idval');
        //editarValuacion(<%=EntidadSesion.Id_Emp %>, <%=EntidadSesion.Id_Cd %>, idVal, idCte);
        editarValuacion(EntidadSesion_Id_Emp, EntidadSesion_Id_Cd, idVal, idCte);

    }
});
*/

   // Valuacion 
/*
$('#dvModalValuacion').on('show.bs.modal', function(event){
    var idCte=$(event.relatedTarget).data('idcte');
    var modo=$(event.relatedTarget).data('modo');
    if(modo==0){
        bloquearProyecto(idCte/*_clienteDeOportunidad*);
    }else{
        var idVal=$(event.relatedTarget).data('idval');
    editarValuacion(<%=EntidadSesion.Id_Emp %>, <%=EntidadSesion.Id_Cd %>, idVal, idCte);
    }
});
*/

