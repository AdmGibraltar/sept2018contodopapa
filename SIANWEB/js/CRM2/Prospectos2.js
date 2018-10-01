/*
Key Soluciones 
01 Ene 2017 Angel Segura
01 Jun 2018 RFH Correcciones 
Prospectos2.js 
*/

var _renglonActual = 0;
var _columnaActual = 0;

var contador = 0;
var prospectoActualAEliminar = null;
var _renglonDelProspectoAEliminar = null;
var _ROWIDX=0

var _valorUnidadDimension=0.0;

var _onLoginSuccessful = null;
var _indiceProspectoAActualizar = -1;
var _datosProspectoAActualizar = null;

var _clienteSeleccionado = null;

var _prospectoElegido = null;
var _bProspectoSeleccionadoDeLista = false;
var _peticionDeBusquedaNombreEmpresa = null;
var _peticionDeBusquedaExactaNombreEmpresa = null;
var _responseObjectBusquedaNombreEmpresa = null;

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnRetirarTerritorio$click(sender){
    var idTer=$(sender).data('terid');
    retirarAsignacionDeTerritorio(_clienteSeleccionado.Id_Cte, idTer, $.proxy(btnRetirarTerritorioExitosa, null, idTer));
}
        
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Territorios Asociacion       
// 
function btnRetirarTerritorioExitosa(idTer){
    
    $('#tblDetallesTerritoriosAsociados tbody tr[id="' + idTer + '"]').remove();
    //_territorioAsociadosAProspecto
    var territorioRetirado=$.grep(_territoriosDeRik, function(element, index){
        return element.Id_Ter==idTer;
    });
    var $select=$('#selDetallesAsociarTerritorio_Id_Ter');
    $($select).append('<option value="' + territorioRetirado[0].Id_Ter + '">' + territorioRetirado[0].Id_Ter + ' - ' + territorioRetirado[0].Ter_Nombre + '</option>');
    $($select).selectpicker('refresh');

    _territorioAsociadosAProspecto=$.grep(_territorioAsociadosAProspecto, function(element, index){
        return element.Id_Ter!=idTer;
    });

    var elementosPresentersEnSelectorTerritorio=$('#selDetallesAsociarTerritorio_Id_Ter option').length;
    if(elementosPresentersEnSelectorTerritorio<1){
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true);
    }else{
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false);
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Territorios Asociacion       
// 
function retirarAsignacionDeTerritorio(idCliente, idTerritorio, onSuccess, onFailure, always){
    $.ajax({        
        url: _ApplicationUrl+ '/api/CatClienteDet/?idCte=' + idCliente + '&idTer=' + idTerritorio,
        type: 'DELETE',
        cache: false,
        //contentType: 'application/json',
        //data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(retirarAsignacionDeTerritorio, null, idCliente, idTerritorio, onSuccess, onFailure, always);
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarTerritoriosAsociadosAProspecto(idCliente, onSuccess, onFailure, always) {
    $.ajax({        
        url: _ApplicationUrl + '/api/CatTerritorio/?idCliente=' + idCliente,
        type: 'GET',
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarTerritoriosAsociadosAProspecto, null, idCliente, onSuccess, onFailure, always);
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//Territorios Asociacion
//
function cargarTerritoriosNoAsociadosAProspecto() {
    var territoriosNoAsociados=$.grep(_territoriosDeRik, function(element, index){
        return $.grep(_territorioAsociadosAProspecto, function(element2, index2){
            return element2.Id_Ter==element.Id_Ter;
        }).length<1;
    });
    var $select=$('#selDetallesAsociarTerritorio_Id_Ter');
    $($select).find('option').remove();
    $.each(territoriosNoAsociados, function(index, element){
        $($select).append('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
    });
    $($select).selectpicker('val', 0);
    $($select).selectpicker('refresh');

    var elementosPresentersEnSelectorTerritorio=$('#selDetallesAsociarTerritorio_Id_Ter option').length;
    if(elementosPresentersEnSelectorTerritorio<1){
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true);
    }else{
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false);
    }
}
    
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//PROSPECTO / Grid - Cargar en seccion de SEGUIMIENTO la Información del prospecto.
//
function cargarDescripcion(rowIdx) {

    switch (_Parametro_ControlesSoloLectura) {
    case 0:
        //Edicion                        
        $('#btnCrearNota').prop('disabled', false);                    
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false);                                
    break;
    case 1:
        // Solo lectura 
        $('#btnCrearNota').prop('disabled', true);
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true);
    break;
    }
    
    $('#tabSeguimiento a[href="#dvGeneral"]').tab('show');  
              
    var data = $('#tblProspectos').DataTable().row(rowIdx).data();
    $('#ddDatosGeneralesContacto').text(data.Cte_Contacto);
    $('#ddDatosGeneralesCorreoElectronico').text(data.Cte_Email);
    $('#ddDatosGeneralesTelefono').text(data.Cte_Telefono);
    $('#ddDatosGeneralesNombreComercial').text(data.Cte_NomComercial);
    $('#ddDatosGeneralesCalle').text(data.Cte_Calle);
    $('#dvSeguimiento:hidden').slideDown();

    //$('#ProspectoNombre').html('<strong>Seguimiento : ' + data.Cte_NomComercial + '</strong>');
    $('#hfDatosGenerales_Id_Cd').val(data.Id_Cd);
    $('#hfDatosGenerales_Id_CrmProspecto').val(data.Id_CrmProspecto);
    $('#hfDatosGenerales_Id_CrmTipoCliente').val(data.Id_CrmTipoCliente);
    $('#hfDatosGenerales_Id_Cte').val(data.Id_Cte);
    $('#hfDatosGenerales_Id_Emp').val(data.Id_Emp);
    $('#hfDatosGenerales_Id_Rik').val(data.Id_Rik);
    $('#hfDatosGenerales_Id_Ter').val(data.Id_Ter_Temporal);
            
    _clienteSeleccionado = data;
    cargarNotas(data);

    //cargarTerritoriosAsociadosAProspecto(data.Id_Cte, $.proxy(cargarTerritoriosAsociadosAProspectoSucceeded, null), $.proxy(cargarTerritoriosNoAsociadosAProspectoFailed, null));            
    //$('#dvSeccionTerritorios').asociacionprospectoterritorio({modo: $.fn.asociacionprospectoterritorio.MODOS.PERSISTENTE, idCte: data.Id_Cte, idRik: <%=EntidadSesion.Id_Rik %>});

    $('#dvSeccionTerritorios').asociacionprospectoterritorio({
        modo: $.fn.asociacionprospectoterritorio.MODOS.PERSISTENTE,
        idCte: data.Id_Cte,
        idRik: _EntidadSesion_Id_Rik
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Terrotorios Asociacion
//
function btnDetallesAsociarTerritorio_Asociar$click(sender){    
    var idTer=$('#selDetallesAsociarTerritorio_Id_Ter').selectpicker('val');
    var territorioSeleccionado=$.grep(_territoriosDeRik, function(element, index){
        return element.Id_Ter==idTer;
    });
    var vpo=$('#txtDetallesAsociarTerritorio_Potencial').val();    
    asociarTerritorioACliente(_clienteSeleccionado.Id_Cte, idTer, _EntidadSesion_Id_Rik, territorioSeleccionado.Id_Seg, vpo, $.proxy(territorioAsociadoConExito, null));
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// PROSPECTO NUEVO - Asociar Territorio
//
function btnNuevoProspecto_AsociarTerritorio(sender) {            
    var idTer = $('#ModalNuevoProspecto_selTerritorios').selectpicker('val');
            
    var territorioSeleccionado = $.grep(_territoriosDeRik, function (element, index) {
        return element.Id_Ter == idTer;
    });

    var vpo = $('#txtDetallesAsociarTerritorio_Potencial').val();    

    var data = {
        IdCte: 0,
        IdRik: _EntidadSesion_Id_Rik,
        IdTer: idTer,
        IdSeg: territorioSeleccionado.Id_Seg,
        VPO: vpo
    };

    _territorioNuevoProspecto.push(data); 

}       

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Territorios Asociacion
//
function territorioAsociadoConExito(response){
    
    //agregar el elemento al conjunto de _territorioAsociadosAProspecto
    _territorioAsociadosAProspecto.push(response);

    //remover el elemento elegido del selector de territorio
    $('#selDetallesAsociarTerritorio_Id_Ter option[value="' + response.Id_Ter + '"]').remove();
    $('#selDetallesAsociarTerritorio_Id_Ter').selectpicker('refresh');
    //agregar una nueva fila al listado de territorios asociados
    var tabletbody=$('#tblDetallesTerritoriosAsociados tbody');
    var newRow = $('<tr id="' + response.Id_Ter + '">' +
    '<td>' + response.Id_Ter + '</td>' +
    '<td> ' + response.CatTerritorioSerializable.Ter_Nombre + ' </td>' +
    '<td>' + response.Cte_Potencial + '</td>' +
    '<td style="text-align: center">' +
        '<button class="btn btn-primary"><i class="'+ICON_NUEVO+'"></i></button>' +
    '</td>' +
    '<td style="text-align: center">' +
        '<button type="button" onclick="btnRetirarTerritorio$click(this)" data-terid="' + response.Id_Ter + '" class="btn btn-primary">' +
        '<i class="fa fa-times"></i></button>' +
    '</td></tr>');

    $(tabletbody).append(newRow);
    $('#txtDetallesAsociarTerritorio_Potencial').val('');

    var elementosPresentersEnSelectorTerritorio=$('#selDetallesAsociarTerritorio_Id_Ter option').length;
    if(elementosPresentersEnSelectorTerritorio<1){
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true);
    }else{
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false);
    }

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Asociar Territorio
function asociarTerritorioACliente(idCliente, idTer, idRik, idSeg, vpo, onSuccess, onFailure, always) {
    var data={
        IdCte: idCliente, 
        IdRik: idRik, 
        IdTer: idTer, 
        IdSeg: idSeg, 
        VPO: vpo
    };
    $.ajax({        
        url: _ApplicationUrl + '/api/CatClienteDet/',
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(asociarTerritorioACliente, null, idCliente, idTer, idRik, idSeg, vpo, onSuccess, onFailure, always);
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Territorios Asociacion
function cargarTerritoriosAsociadosAProspectoSucceeded(response){
    
    if(response!=null){
        var tabletbody=$('#tblDetallesTerritoriosAsociados tbody');
        $(tabletbody).find('tr').remove();
        _territorioAsociadosAProspecto=[];
        $.each(response, function(index, element){
            _territorioAsociadosAProspecto.push(element);
            var newRow = $('<tr id="' + element.Id_Ter + '">' +
            '<td>' + element.Id_Ter + '</td>' +
            '<td>' + element.CatTerritorioSerializable.Ter_Nombre + '</td>' +
            '<td>' + element.Cte_Potencial + '</td>' +
            '<td style="text-align: center">' +
                '<button class="btn btn-primary"><i class="'+ICON_NUEVO+'"></i></button>' +
            '</td>' +
            '<td style="text-align: center">' +
                '<button type="button" onclick="btnRetirarTerritorio$click(this)" data-terid="' + element.Id_Ter + '" class="btn btn-primary">' +
                '<i class="fa fa-times"></i></button>' +
            '</td>' +
            '</tr>');

            $(tabletbody).append(newRow);
        });
    }
    cargarTerritoriosNoAsociadosAProspecto();

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Territorios
function cargarTerritoriosNoAsociadosAProspectoFailed(){
}

function cargarDescripcionProspecto(objProspecto) {
    var data = objProspecto;
    $('#ddDatosGeneralesContacto').text(data.Cte_Contacto);
    $('#ddDatosGeneralesCorreoElectronico').text(data.Cte_Email);
    $('#ddDatosGeneralesTelefono').text(data.Cte_Telefono);
    $('#ddDatosGeneralesNombreComercial').text(data.Cte_NomComercial);
    $('#ddDatosGeneralesCalle').text(data.Cte_Calle);
    $('#dvSeguimiento:hidden').slideDown();
    _clienteSeleccionado = data;
    cargarNotas(data);
}
        
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\       
// Prospecto Nuevo 
function btnNuevoProspecto() {
    
    $('#tbRFCMultiples').css('display','none');
    $('#btnRFCCrearNuevo').css('display','none');

    _territorioNuevoProspecto = [];

    //limpiarFormaNuevoProspecto();
    $('#ModalNuevoProspecto #tbNoCte').val('');
    $('#ModalNuevoProspecto #txtRFC').val('');
    $('#ModalNuevoProspecto #txtNombre').val('');
    $('#ModalNuevoProspecto #txtContacto').val('');
    $('#ModalNuevoProspecto #txtEmail').val('');
    $('#ModalNuevoProspecto #txtCalle').val('');
    $('#ModalNuevoProspecto #txtTelefono').val('');

    $('#dvMenu_General #txtNombre').attr('readonly',false);
    $('#dvMenu_General #txtContacto').attr('readonly',false);
    $('#dvMenu_General #txtEmail').attr('readonly',false);
    $('#dvMenu_General #txtCalle').attr('readonly',false);
    $('#dvMenu_General #txtTelefono').attr('readonly',false);                                       

    $('#ModalNuevoProspecto #hdnId_Cte').val('');
    $('#ModalNuevoProspecto #hdnCrearNuevo').val('');
            

    var dvTerritoriosElement = $('#ModalNuevoProspecto #dvTerritorios');
    var jqElement = $(dvTerritoriosElement).find('#selTerritorios');
    jqElement.selectpicker('val', 0);
    jqElement.selectpicker('refresh');

    //inicializarModalNuevoProspecto();
    $('#ModalNuevoProspecto').on('show.bs.modal', function (event) {
        //console.log('424');
        var trigger = $(event.relatedTarget);
        $('#btnDvModalNuevoProspectoGuardar').prop('disabled', false);
        limpiarFormaNuevoProspecto();
        cancelarCrearProyectoDeProspectoExistente('ModalNuevoProspecto');
        $('#ModalNuevoProspecto #lblMensajeRFC').hide();
        $('#ModalNuevoProspecto #lblRFCVacio').hide();
        $('#ModalNuevoProspecto #lblMensajeNombreEmpresa').hide();
        $('#ModalNuevoProspecto #icnRFCComprobado').hide();
        $('#ModalNuevoProspecto #icnRFCExistente').hide();

        $('#dvMenu_Territorios').asociacionprospectoterritorio('limpiar');
    });
    $('#ModalNuevoProspecto').on('show.bs.modal', function (event) {
        //console.log('438');
        //$('#frmDvModalNuevoProspecto').validate().resetForm();
    });
    $('#tabProspectoNuevoTerritorio a[href="#dvMenu_General"]').tab('show');            
    $('#ModalNuevoProspecto').modal('show');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Prospecto
function limpiarFormaEditarProspecto() {
        $('#dvModalEditarProspecto #txtRFC').val('');
        $('#dvModalEditarProspecto #txtNombre').val('');
        $('#dvModalEditarProspecto #txtContacto').val('');
        $('#dvModalEditarProspecto #txtEmail').val('');
        $('#dvModalEditarProspecto #txtCalle').val('');
        $('#dvModalEditarProspecto #txtTelefono').val('');

        var dvTerritoriosElement = $('#dvModalEditarProspecto #dvTerritorios');
        var jqElement = $(dvTerritoriosElement).find('#selTerritorios');
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');
}
        
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function seleccionarTerritorios(jqTerritoriosEdicion, territorios) {
        $(jqTerritoriosEdicion).selectpicker('val', territorios);
        $(jqTerritoriosEdicion).selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    /*
    function inicializarModalEditarProspecto() {
    console.log('inicializarModalEditarProspecto')
        $('#dvModalEditarProspecto').on('show.bs.modal', function (event) {
            console.log('471');
            var trigger = $(event.relatedTarget);
            var idCrmProspecto = trigger.data('idcrmprospecto');
            _indiceProspectoAActualizar = trigger.data('rowidx');
            _datosProspectoAActualizar = $('#tblProspectos').DataTable().row(_indiceProspectoAActualizar).data();
            limpiarFormaEditarProspecto();
            cargarCamposDialogoEditarProspecto(idCrmProspecto);
            cargarDescripcion(_indiceProspectoAActualizar);
        });

        $('#dvModalEditarProspecto').on('hide.bs.modal', function (event) {
            $('#frmDvModalEditarProspecto').validate().resetForm();
        });
    }
    */

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\    
    // Carga informacion del prospecto a editar.     
    function cargarCamposDialogoEditarProspecto(idCrmProspecto) {
        $.ajax({            
            url: _ApplicationUrl + '/api/CrmProspecto/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + _EntidadSesion_Id_Rik + '&idCrmProspecto=' + idCrmProspecto,
            type: 'GET',
            cache: false,
//            statusCode: {
//                401: function (jqXHR, textStatus, errorThrown) {
//                    $('#dvDialogoInicioSesion').modal();
//                    _onLoginSuccessful = $.proxy(cargarCamposDialogoEditarProspecto, null, idCrmProspecto);
//                }
//            }
            success:function (data) {
                console.log('1');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('2');
            }
        });

        /*

        }).done(function (response, textStatus, jqXHR) {
            var Ctdr = '#dvModalEditarProspecto ';
            $(Ctdr + '#hdnIdCrmProspectoEditarProyecto').val(idCrmProspecto);
            $(Ctdr + '#hdnId_Cte').val(response.Id_Cte);
            $(Ctdr + '#hdnId_Rik').val(response.Id_Rik);
            $(Ctdr + '#hdnId_CrmTipoCliente').val(response.Id_CrmTipoCliente);
            $(Ctdr + '#txtNombre').val(response.Cte_NomComercial);
            $(Ctdr + '#txtContacto').val(response.Cte_Contacto);
            $(Ctdr + '#txtEmail').val(response.Cte_Email);
            $(Ctdr + '#txtCalle').val(response.Cte_Calle);
            $(Ctdr + '#txtTelefono').val(response.Cte_Telefono);
            $(Ctdr + '#txtRFC').val(response.Cte_Rfc);
            if (response.Id_Ter_Temporal != null) {
                $(Ctdr + '#selTerritorioProspecto').selectpicker('val', response.Id_Ter_Temporal);
                $(Ctdr + '#selTerritorioProspecto').selectpicker('refresh');
            } else {
                $(Ctdr + '#selTerritorioProspecto').selectpicker('val', 0);
                $(Ctdr + '#selTerritorioProspecto').selectpicker('refresh');
            }

            if (response.Id_CrmTipoCliente==2) {
                //si es de tipo 2 es un cliente del catalogo                     
                $(Ctdr+'#txtNombre').attr('readonly',true);
                $(Ctdr+'#txtContacto').attr('readonly',true);
                $(Ctdr+'#txtEmail').attr('readonly',true);
                $(Ctdr+'#txtCalle').attr('readonly',true);
                $(Ctdr+'#txtTelefono').attr('readonly',true);
                $(Ctdr+'#txtRFC').attr('readonly',true);
                $('#btnProspectoEditarGuardar').prop('disabled', true);                
            } else {                    
                $(Ctdr+'#txtNombre').attr('readonly',false);
                $(Ctdr+'#txtContacto').attr('readonly',false);
                $(Ctdr+'#txtEmail').attr('readonly',false);
                $(Ctdr+'#txtCalle').attr('readonly',false);
                $(Ctdr+'#txtTelefono').attr('readonly',false);
                $(Ctdr+'#txtRFC').attr('readonly',false);
                $('#btnProspectoEditarGuardar').prop('disabled', false);                
            }
            var jqTerritoriosEdicion = $('#dvModalEditarProspecto #selTerritorios');
            //seleccionarTerritorios(jqTerritoriosEdicion, response.Territorios);
            //function seleccionarTerritorios(jqTerritoriosEdicion, territorios) {
            //$(jqTerritoriosEdicion).selectpicker('val', response.Territorios);
            //$(jqTerritoriosEdicion).selectpicker('refresh');
            //}

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

        */





    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarUENs($, jqElement, onSuccess, onFailure) {
        $.ajax({            
            url: _ApplicationUrl + '/api/CatUEN/?idEmp=' + _EntidadSesion_Id_Emp  + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + _EntidadSesion_Id_Rik,
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function selUEN$on_change() {
        var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
        var idUen = $('#dvModalNuevoProyecto #selUEN').selectpicker('val');
        despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
        cargarSegmentos(jQuery, $selSegmento, idUen);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarSegmentos($, jqElement, idUen, onSuccess, onFailure) {
        //mostrar el indicador de operación en proceso
        $('#imgProcesandoSegmentoDvModalNuevoProyecto').fadeIn();
        $.ajax({            
            url: _ApplicationUrl + '/api/CatSegmento/?idEmp=' + _EntidadSesion_Id_Emp + '&idUen=' + idUen,
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
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function selSegmento$on_change() {
        var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
        var idSeg = $('#dvModalNuevoProyecto #selSegmento').selectpicker('val');
        var $selArea = $('#dvModalNuevoProyecto #selArea');
        var $selUEN = $('#dvModalEditarProyecto #selUEN');
        var $selSegmento = $('#dvModalEditarProyecto #selSegmento');
        despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        //cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(cargarAreas, null, jQuery, $selArea, idSeg));
        cargarAreas(jQuery, $selArea, idSeg);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarTerritorios($, jqElement, idSeg, onSuccess, onFailure) {
        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
        $.ajax({            
            url: _ApplicationUrl  + '/api/CatTerritorio/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + _EntidadSesion_Id_Rik + '&idSeg=' + idSeg,
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
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarAreas($, jqElement, idSeg, onSuccess, onFailure) {
        $('#imgProcesandoAreaDvModalNuevoProyecto').fadeIn();
        $.ajax({            
            url: _ApplicationUrl + '/api/CatArea/?idEmp=' + _EntidadSesion_Id_Emp + '&idSeg=' + idSeg,
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
            jqElement.append('<option value="-1">Otros</option>');
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
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarSoluciones($, jqElement, idArea, onSuccess, onFailure) {
        $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeIn();
        $.ajax({            
            url: _ApplicationUrl + '/api/CatSolucion/?idEmp=' + _EntidadSesion_Id_Emp + '&idArea=' + idArea,
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {                    
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
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function selArea$on_change() {
        var $selSolucion = $('#dvModalNuevoProyecto #selSolucion');
        var idArea = $('#dvModalNuevoProyecto #selArea').selectpicker('val');

        despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        if(idArea==-1){
            _otrosSeleccionado=true;
            //mostrarMapaDeOferta();
            //poblar los listados de solución y aplicación con el elemento "Otros"
            //cargar los listados de solución y aplicación con los elementos "Otros".
            $selSolucion.find('option').remove();
            $selSolucion.append('<option value="-1">Otros</option>');
            //Se establece el valor "Otros" automáticamente en el selector de solución y aplicación
            $selSolucion.selectpicker('val', 0);
            $selSolucion.selectpicker('refresh');

            var $lstAplicacion = $('#dvModalNuevoProyecto #lstAplicacion');
            $lstAplicacion.find('div').remove();

            $('#dvModalNuevoProyecto #dvLstAplicacionesOtrosSlate').show();

        }else{
            _otrosSeleccionado=false;
            //ocultarMapaDeOferta();
            $('#dvModalNuevoProyecto #dvLstAplicacionesOtrosSlate').hide();
            cargarSoluciones(jQuery, $selSolucion, idArea);
        }
    }
        
    var _aplicacionesSeleccionadas = [];

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarAplicaciones($, jqElement, idSol, onSuccess, onFailure) {
        $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeIn();
        var idUen = $('#dvModalNuevoProyecto #selUEN').selectpicker('val');
        var idSeg = $('#dvModalNuevoProyecto #selSegmento').selectpicker('val');
        var idArea = $('#dvModalNuevoProyecto #selArea').selectpicker('val');
        var idCte = $('#dvModalNuevoProyecto #hdnCliente').val();
        var idOp = $('#dvModalNuevoProyecto #hdnId_Op').val();
        var idOpVar = idOp != null ? idOp : '0';
        $.ajax({            
            url: _ApplicationUrl + '/api/CatAplicacion/?idUen=' + idUen + '&idSeg=' + idSeg + '&idArea=' + idArea + '&idSol=' + idSol + '&idOp=' + idOpVar + '&idCte=' + idCte,
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    //self.location='<%=ApplicationUrl %>' + '/login.aspx';
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

                if (element.Apl_Activo == false) {
                    $('#txtAplVPO_'+element.Id_Apl).prop('disabled',true);                    
                }  
                                
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
                        //Id_Cd: '<%=EntidadSesion.Id_Cd %>',
                        Id_Cd: _EntidadSesion_Id_Cd ,
                        Id_Op: idOpVar != null ? idOpVar : 0,
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
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function selSolucion$on_change() {
        var $selAplicacion = $('#dvModalNuevoProyecto #selAplicacion');
        var idSol = $('#dvModalNuevoProyecto #selSolucion').selectpicker('val');
        despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        cargarAplicaciones(jQuery, $selAplicacion, idSol);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cerrarToastDanger($) {
        $('#toastDanger').fadeOut();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cerrarToastSuccess($) {
        $('#toastSuccess').fadeOut();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cerrarToastWarning($) {
        $('#toastWarning').fadeOut();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function habilitarSelectorDependienteDelSelectorUENDialogoNuevoProyecto() {
        $('#selSegmento').prop('disabled', false);
        $('#selSegmento').selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto() {
        $('#selSegmento').selectpicker('refresh');
        deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function despopularCascadaDependientesSelectorUENDialogoNuevoProyecto() {
        $('#selSegmento').find('option').remove();
        $('#selSegmento').selectpicker('refresh');
        despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto() {
        $('#selArea').prop('disabled', false);
        $('#selArea').selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
        $('#selArea').selectpicker('refresh');
        $('#selTerritorio').selectpicker('refresh');

        deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
        $('#selArea').find('option').remove();        
        $('#selArea').selectpicker('refresh');        
        despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto() {
        $('#selSolucion').prop('disabled', false);
        $('#selSolucion').selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
        $('#selSolucion').selectpicker('refresh');
        deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
        $('#selSolucion').find('option').remove();
        $('#selSolucion').selectpicker('refresh');
        despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto() {
        $('#selAplicacion').prop('disabled', false);
        $('#selAplicacion').selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
        $('#selAplicacion').selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
        $('#selAplicacion').find('option').remove();
        $('#selAplicacion').selectpicker('refresh');

        var $lstAplicacion = $('#lstAplicacion');
        $lstAplicacion.find('div').remove();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function inhabilitarSelectoresDialogoNuevoProyecto() {
        deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
    }


    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function Resetear_Busqueda () {

        _territorioNuevoProspecto = [];

        //limpiarFormaNuevoProspecto();
        $('#ModalNuevoProspecto #txtRFC').val('');
        $('#ModalNuevoProspecto #txtNombre').val('');
        $('#ModalNuevoProspecto #txtContacto').val('');
        $('#ModalNuevoProspecto #txtEmail').val('');
        $('#ModalNuevoProspecto #txtCalle').val('');
        $('#ModalNuevoProspecto #txtTelefono').val('');
        $('#txtRFC').val('');

        $('#txtRFC').prop('disabled', false);
        $('#txtRFC').attr('readonly',false);

        $('#tbNoCte').val('');

        $('#dvMenu_General #txtNombre').attr('readonly',false);
        $('#dvMenu_General #txtContacto').attr('readonly',false);
        $('#dvMenu_General #txtEmail').attr('readonly',false);
        $('#dvMenu_General #txtCalle').attr('readonly',false);
        $('#dvMenu_General #txtTelefono').attr('readonly',false);                                       

        $('#ModalNuevoProspecto #hdnId_Cte').val('');
        $('#ModalNuevoProspecto #hdnCrearNuevo').val('');
            

            $('#btnDvModalNuevoProspectoGuardar').prop('disabled', false);
            //limpiarFormaNuevoProspecto();
            //cancelarCrearProyectoDeProspectoExistente('ModalNuevoProspecto');

            $('#ModalNuevoProspecto #lblMensajeRFC').hide();
            $('#ModalNuevoProspecto #lblRFCVacio').hide();
            $('#ModalNuevoProspecto #lblMensajeNombreEmpresa').hide();
            $('#ModalNuevoProspecto #icnRFCComprobado').hide();
            $('#ModalNuevoProspecto #icnRFCExistente').hide();

            //$('#dvMenu_Territorios').asociacionprospectoterritorio('limpiar');
        
        
        //$('#tabProspectoNuevoTerritorio a[href="#dvMenu_General"]').tab('show');            
        //$('#ModalNuevoProspecto').modal('show');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function actualizarProspecto() {
        if ($('#frmDvModalEditarProspecto').valid() == false) {
            return;
        }
        $(this).prop('disabled', true);
        $('#imgDvModalEditarProspectoEnProgreso').fadeIn();
        $.ajax({            
            url: _ApplicationUrl + '/api/CrmProspecto',
            type: 'PUT',
            cache: false,
            data: $('#frmDvModalEditarProspecto').serialize(),
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(actualizarProspecto, this);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            _datosProspectoAActualizar.Cte_NomComercial = $('#dvModalEditarProspecto #txtNombre').val();
            _datosProspectoAActualizar.Cte_Contacto = $('#dvModalEditarProspecto #txtContacto').val();
            _datosProspectoAActualizar.Cte_Email = $('#dvModalEditarProspecto #txtEmail').val();
            _datosProspectoAActualizar.Cte_Calle = $('#dvModalEditarProspecto #txtCalle').val();
            _datosProspectoAActualizar.Cte_Telefono = $('#dvModalEditarProspecto #txtTelefono').val();
            $('#toastSuccess #toastSuccessMessage').html('El prospecto ha sido actualizado con éxito');
            $('#toastSuccess').fadeIn();
            //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
            setTimeout(function () {
                $('#toastSuccess').fadeOut();
            }, 3000);
            $('#dvModalEditarProspecto').modal('hide');
            $('#tblProspectos').DataTable().row(_indiceProspectoAActualizar).data(_datosProspectoAActualizar);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                default:
                    $(this).modal('hide');
                    $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.ExceptionMessage);
                    $('#toastDanger').fadeIn();
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 3000);
                    break;
            }
        }).complete(function () {
            $(this).prop('disabled', false);
            $('#imgDvModalEditarProspectoEnProgreso').fadeOut();
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    //
    // Nuevo Prospecto 
    //
    function limpiarFormaNuevoProspecto() {
        $('#dvModalNuevoProspecto #txtRFC').val('');
        $('#dvModalNuevoProspecto #txtNombre').val('');
        $('#dvModalNuevoProspecto #txtContacto').val('');
        $('#dvModalNuevoProspecto #txtEmail').val('');
        $('#dvModalNuevoProspecto #txtCalle').val('');
        $('#dvModalNuevoProspecto #txtTelefono').val('');

        var dvTerritoriosElement = $('#dvModalNuevoProspecto #dvTerritorios');
        var jqElement = $(dvTerritoriosElement).find('#selTerritorios');
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');
    }
                
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    //
    // Nuevo Prospecto 
    //
    function inicializarModalNuevoProspecto() {
        $('#dvModalNuevoProspecto').on('show.bs.modal', function (event) {
            //console.log('1122');
            var trigger = $(event.relatedTarget);
            $('#btnDvModalNuevoProspectoGuardar').prop('disabled', false);
            limpiarFormaNuevoProspecto();
            cancelarCrearProyectoDeProspectoExistente('dvModalNuevoProspecto');
            $('#dvModalNuevoProspecto #lblMensajeRFC').hide();
            $('#dvModalNuevoProspecto #lblRFCVacio').hide();
            $('#dvModalNuevoProspecto #lblMensajeNombreEmpresa').hide();
            $('#dvModalNuevoProspecto #icnRFCComprobado').hide();
            $('#dvModalNuevoProspecto #icnRFCExistente').hide();
            //$('#dvMenu_Territorios').asociacionprospectoterritorio('limpiar');
        });
        //'hidden.bs.modal'
        $('#dvModalNuevoProspecto').on('show.bs.modal', function (event) {
            //console.log('1136');
            $('#frmDvModalNuevoProspecto').validate().resetForm();
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function Verificar_CrmProspecto (IdCte) {           
        IdCte = IdCte.trim();
        result = -1;
        if (IdCte!="") {        
            $.ajax({                    
                    url: _ApplicationUrl + '/api/CatCliente/?IdCte=' + IdCte,
                    type: 'GET',
                    cache: false,
                    async: false,
                    contentType: 'application/json',
                    statusCode: { }
                }).done(function (response, textStatus, jqXHR) {
            
                    result  = response;

                }).fail(function (jqXHR, textStatus, errorThrown){
                    alertify.error('Error : Verificar_CrmProspecto ('+IdCte+')');
                }).always(function (jqXHR, textStatus, errorThrown) {
                
                });
        }
               
        return result;
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function DatosCliente_SoloLectura() {
        $('#txtRFC').attr('readonly',true);
        $('#txtRFC').attr('disabled', 'disabled');

        $('#txtNombre').attr('readonly',true);
        $('#txtContacto').attr('readonly',true);
        $('#txtEmail').attr('readonly',true);
        $('#txtCalle').attr('readonly',true);
        $('#txtTelefono').attr('readonly',true);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function comprobarRFC(parent, rfc) {        
        rfc = rfc.trim();        
        var new_rfc = rfc.replace(/[-!"#$%&/()=?¡¿+{}]/g, "");

        if (new_rfc == "") {
            // No ay RFC y se continua con vacio.
            return;
        }

        $('#txtRFC').val(new_rfc); 
        
        if (rfc.length>0) {
            $('#' + parent + ' #imgRFCEnOperacion').show();
            $('#' + parent + ' #icnRFCComprobado').hide();
            $('#' + parent + ' #icnRFCExistente').hide();
            $('#' + parent + ' #lblRFCVacio').hide();

            $.ajax({                
                url: _ApplicationUrl + '/api/CatCliente/?rfc=' + rfc,
                type: 'GET',
                cache: false,
                contentType: 'application/json',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(comprobarRFC, null, parent, rfc);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                var $txtRFC=$('#' + parent + ' #txtRFC');
                if(response==null){
                    $('#' + parent + ' #lblMensajeRFC').hide();
                    $('#' + parent + ' #icnRFCExistente').hide();
                    if($txtRFC.val()!=''){
                        $('#' + parent + ' #icnRFCComprobado').show();
                    }else{
                        //TODO: mostrar una notificación que advierta sobre dejar el campo RFC vacío. 
                        $('#' + parent + ' #lblRFCVacio').show();
                    }
                }else{
                    
                    if($.isArray(response)){
                        if($txtRFC.val()!=''){
                            if(response.length>0){
                                //
                                // El cliente existe se despliegan los datos 
                                // solo lectura.
                                //

                                if (response.length==1) {
                                    // Si es un solo registro
                                    $('#' + parent + ' #icnRFCComprobado').hide();
                                    //$('#' + parent + ' #icnRFCExistente').show();
                                    $('#' + parent + ' #lblMensajeRFC').show();
                                    // Carga los datos del cliente                                    

                                    $('#' + parent + ' #tbNoCte').val(response[0].Id_Cte);

                                    $('#' + parent + ' #txtNombre').val(response[0].Cte_NomComercial);
                                    $('#' + parent + ' #txtContacto').val(response[0].Cte_Contacto);
                                    $('#' + parent + ' #txtEmail').val(response[0].Cte_Email);
                                    $('#' + parent + ' #txtCalle').val(response[0].Cte_Calle);
                                    $('#' + parent + ' #txtTelefono').val(response[0].Cte_Telefono);

                                    $('#' + parent + ' #txtNombre').attr('readonly',true);
                                    $('#' + parent + ' #txtContacto').attr('readonly',true);
                                    $('#' + parent + ' #txtEmail').attr('readonly',true);
                                    $('#' + parent + ' #txtCalle').attr('readonly',true);
                                    $('#' + parent + ' #txtTelefono').attr('readonly',true);
                                    
                                    alertify.success('El RFC existe y se cargaron los datos del cliente.');
                                    $('#btnRFCCrearNuevo').css('display','block');

                                }

                                if (response.length>1) {

                                    //$('#tbRFCMultiples').css('display','block');
                                    //$('#btnRFCCrearNuevo').css('display','block');
                                    
                                    // Son varios registros carga el combo
                                    var cmbRFCs = $('#cmbRFCs').empty();

                                    cmbRFCs.append('<option data-id_cte="0">-- Seleccione --</option>');

                                    for (var c=0; c<response.length; c++) {
                                        cmbRFCs.append(
                                            $('<option '+
                                            'data-id_cte="'+response[c].Id_Cte+'" '+
                                            'data-cte_nomcomercial="'+response[c].Cte_NomComercial+'" '+
                                            'data-cte_contacto="'+response[c].Cte_Contacto+'" '+
                                            'data-cte_email="'+response[c].Cte_Email+'" '+
                                            'data-cte_calle="'+response[c].Cte_FacCalle+'" '+
                                            'data-Cte_Telefono="'+response[c].Cte_Telefono+'" '+                                                
                                            'data-html="true" '+ 
                                            '>').val(response[c].Id_Cte).text(response[c].Cte_NomComercial+' '+response[c].Cte_FacCalle).attr('title',response[c].Cte_Contacto+'/ '+response[c].Cte_Email)
                                        );
                                    }   
                                    // En el listado no permitira modificacion  
                                    $('#' + parent + ' #txtNombre').attr('readonly',true);
                                    $('#' + parent + ' #txtContacto').attr('readonly',true);
                                    $('#' + parent + ' #txtEmail').attr('readonly',true);
                                    $('#' + parent + ' #txtCalle').attr('readonly',true);
                                    $('#' + parent + ' #txtTelefono').attr('readonly',true);
                                                                                                                   
                                    alertify.success('Se encontraron duplicados.');
                                }                                   

                            }else{
                                $('#' + parent + ' #lblMensajeRFC').hide();
                                $('#' + parent + ' #icnRFCExistente').hide();
                                $('#' + parent + ' #icnRFCComprobado').show();
                                $('#' + parent + ' #hdnId_Cte').val('');

                                $('#' + parent + ' #txtNombre').val('');
                                $('#' + parent + ' #txtContacto').val('');
                                $('#' + parent + ' #txtEmail').val('');
                                $('#' + parent + ' #txtCalle').val('');
                                $('#' + parent + ' #txtTelefono').val('');

                                $('#' + parent + ' #txtNombre').attr('readonly',false);
                                $('#' + parent + ' #txtContacto').attr('readonly',false);
                                $('#' + parent + ' #txtEmail').attr('readonly',false);
                                $('#' + parent + ' #txtCalle').attr('readonly',false);
                                $('#' + parent + ' #txtTelefono').attr('readonly',false);                                    
                            }
                        }else{
                            $('#' + parent + ' #lblMensajeRFC').hide();
                            $('#' + parent + ' #icnRFCExistente').hide();
                            $('#' + parent + ' #icnRFCComprobado').hide();
                            //TODO: mostrar notificación con advertencia sobre dejar el campo RFC vacío
                            $('#' + parent + ' #lblRFCVacio').show();
                        }
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown){
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
            }).always(function (jqXHR, textStatus, errorThrown) {
                $('#' + parent + ' #imgRFCEnOperacion').hide();
            });
        } else {
            alertify.error('Es dato RFC es necesario.');
        }         
      
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    //
    //PROSPECTO 
    //
    function crearProspecto(sender) {            
        var RFC = $('#frmDvModalNuevoProspecto #txtRFC').val();
        RFC = RFC.trim();
        var Nombre = $('#frmDvModalNuevoProspecto #txtNombre').val();
        Nombre = Nombre.trim();
        var Contacto = $('#frmDvModalNuevoProspecto #txtContacto').val();
        Contacto = Contacto.trim();
        var Email = $('#frmDvModalNuevoProspecto #txtEmail').val();
        Email = Email.trim();
        var Calle = $('#frmDvModalNuevoProspecto #txtCalle').val();
        Calle = Calle.trim();
        var Telefono = $('#frmDvModalNuevoProspecto #txtTelefono').val();
        Telefono = Telefono.trim();

        var IdCte = $('#hdnId_Cte').val();

        Verifica = Verificar_CrmProspecto (IdCte);
        if (Verifica > 1) {
            alertify.error('Esta intentando duplicar este prospecto, debe realizar una busqueda y agregar el proyecto.');
            return;
        }

        /*
        if (RFC=='')
        {
            alertify.error('El RFC es requerido, si no lo tiene ahora puede capturar un ficticio y modificarlo mas tarde.');
            return;
        }
        */
        
        if (RFC=='' && Nombre=='' && Contacto=='' && Calle =='' && Telefono =='') {
            alertify.error('Debe establecer al menos el nombre.');
            return;
        }

        /*
        // Si hay solo el RFC
        if (RFC!='' && Nombre=='' && Contacto=='' && Calle =='' && Telefono =='') {
            alertify.error('No es posible guardar sin información.');
            return;
        }
        */
            
        if ($('#frmDvModalNuevoProspecto').valid() == false) {
            return;
        }
        $('#imgDvModalNuevoProspectoEnProgreso').fadeIn();
            
        var ser = $('#frmDvModalNuevoProspecto').serialize();

        $('#imgSpinnerGuardar').css('display','block');
            
        $(sender).prop('disabled', true);
        $.ajax({                
            url: _ApplicationUrl + '/api/CrmProspecto',
            type: 'POST',
            cache: false,
            data: $('#frmDvModalNuevoProspecto').serialize(),
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(crearProspecto, sender);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            $('#imgSpinnerGuardar').css('display','none');
            //Crear territorios
            //Por ahora la llamada a la creación de territorios de manera independiente se anula debido 
            //a la coincidencia de que el modelo de prospecto espera un arreglo de territorios que 
            //coincide con la declaración de los elementos creados por el plugin
            /*
            asociarTerritoriosAProspecto(response.Id_Cte, function(){
                $('#toastSuccess #toastSuccessMessage').html('El prospecto ha sido creado con éxito');
                $('#toastSuccess').fadeIn();
                //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
                setTimeout(function () {
                    $('#toastSuccess').fadeOut();
                }, 3000);
                $('#dvModalNuevoProspecto').modal('hide');
                $('#tblProspectos').DataTable().row.add(response).draw();
            });
            */
            //PatternflyToast.showSuccess('El prospecto ha sido creado con éxito', 6000);
            alertify.success('El prospecto ha sido creado con éxito');
            //Notificaciones.agregarNotificacionRIK(new crm.navegacion.Notificacion({contenido: response.Notificacion.Notif_Contenido, tipo: response.Notificacion.Id_TipoNotificacion, id: response.Notificacion.Id_Notificacion}));            
            $('#ModalNuevoProspecto').modal('hide');                            
            $('#dvSeguimiento').css('display','none');
            $('#tblProspectos').DataTable().row.add(response.Prospecto).draw();
                
        }).fail(function (jqXHR, textStatus, errorThrown) {
            $('#imgSpinnerGuardar').css('display','none');
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');                    
                    break;
                default:
                    //$(this).modal('hide');
                    //jqXHR.responseJSON.ExceptionMessage
                     try {
                        console.log(jqXHR.responseJSON.InnerException.InnerException.ExceptionMessage);
                    } catch (e) {
                        console.log('jqXHR.responseJSON.InnerException.InnerException.ExceptionMessage No esta definido');                        
                    }
                    
                    alertify.error('Debe establer el nombre del prospecto o no es posible guardar el prospecto, verifique los requerimientos del CRM</br>o verifique el log del navegador.');
                    //mostrarToast($('#toastDanger'), $('#dvModalNuevoProspecto'));
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 3000);
                    break;
            }
        }).always(function () {
            $('#imgSpinnerGuardar').css('display','none');
            $(sender).prop('disabled', false);
            $('#imgDvModalNuevoProspectoEnProgreso').fadeOut();
        });            
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function asociarTerritoriosAProspecto(idCte, alFinalizarExitosamente){
        //indicador de progreso
        $('#dvMenu_Territorios').asociacionprospectoterritorio('guardar', idCte, alFinalizarExitosamente);
    }
                
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function eliminarProspecto($) {
        $.ajax({            
            url: _ApplicationUrl + '/api/CrmProspecto/?IdCrmProspecto='+prospectoActualAEliminar.Id_CrmProspecto+'&IdCte='+prospectoActualAEliminar.Id_Cte,
            type: 'DELETE',
            cache: false,
            //data: prospectoActualAEliminar,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(eliminarProspecto, null, $);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            _renglonDelProspectoAEliminar.remove();
            _renglonDelProspectoAEliminar = null;
            prospectoActualAEliminar = null;
            limpiarSeccionDatosGenerales();
            $('#tblProspectos').DataTable().draw();
            $('#dvModalEliminarProspecto').modal('hide');
            $('#toastSuccess #toastSuccessMessage').html('El prospecto se eliminó satisfactoriamente');
            $('#toastSuccess').fadeIn();
            setTimeout(function () {
                $('#toastSuccess').fadeOut();
            }, 3000);
        }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('La sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                default:
                    $('#dvModalEliminarProspecto').modal('hide');
                    $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                    $('#toastDanger').fadeIn();
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 3000);
                    break;
            }
        });
    }
        
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function inicializarModalEliminarProspecto() {
        $('#dvModalEliminarProspecto').on('show.bs.modal', function (event) {
            //console.log('1499');
            var trigger = $(event.relatedTarget);
            var rowId = trigger.data('rowid');
            _renglonDelProspectoAEliminar = $('#tblProspectos').DataTable().row(rowId);
            var datosProspecto = _renglonDelProspectoAEliminar.data();
            prospectoActualAEliminar = datosProspecto;
            cargarDescripcion(rowId);
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function limpiarSeccionDatosGenerales() {
        $('#ddDatosGeneralesContacto').text('');
        $('#ddDatosGeneralesCorreoElectronico').text('');
        $('#ddDatosGeneralesTelefono').text('');
        $('#ddDatosGeneralesNombreComercial').text('');
        $('#ddDatosGeneralesCalle').text('');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    //
    // Sucede hace hacer click en el boton "Guardar" del modal de "Nuevo Proyecto"
    //
    function crearProyecto() {

        $('#btnDvModalNuevoProyectoGuardar').attr('disabled','disabled');

        var bHayValorVacio = false;
        var bHayValorInvalido = false;
        var CuentaAplicacionesSeleccionadas = 0;

        // Verifica el valor VPO en las aplicaciones seleccionadas , loop para recorrer 
        for (var i=0 ; i<50; i++) {            
            var VPO = $('input[name="FormaAplicaciones['+i+'].VPO"]').length;            
            if (VPO == 1) {                                
                var val = $('input[name="FormaAplicaciones['+i+'].VPO"]').val();                
                val = parseFloat(val);
                if (isNaN(val)) {
                    val = 0;
                }
                var display = $('input[name="FormaAplicaciones['+i+'].VPO"]').css('display');                
                if (display=='block') {
                    CuentaAplicacionesSeleccionadas = CuentaAplicacionesSeleccionadas+1;
                }
                val = parseFloat(val); 
                if (display=='block' && val <=0) {
                    bHayValorVacio = true;
                }   
                
                if (val>9999999) {
                    bHayValorInvalido = true;
                } 
                             
            }
        }

        if (bHayValorInvalido == true) {
            alertify.error('El VPO excede el maximo permitido por esta aplicación.');
            $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
            return; 
        }

        // Si hay alguna con el valor sin establecer 
        if (bHayValorVacio== true) {
            alertify.error('Debe establecer el Valor VPO en las aplicaciones seleccionadas.');
            $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
            return; 
        } else if (CuentaAplicacionesSeleccionadas <=0) {
            alertify.error('Debe seleccionar las aplicaciones.');
            $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
            return; 
        } else {
            //alertify.success('Se Guarda.');
        }        
        
        var txtNombreProspecto = $('#txtNombreProspecto').text();
        txtNombreProspecto = txtNombreProspecto.trim();
        if (txtNombreProspecto.length<=0) {                                
            alertify.error('ERROR : Faltan los datos del prospecto.');                
            $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
            return;
        }

        $('#dvModalNuevoProyecto #selUEN').prop('disabled', false);
        $('#dvModalNuevoProyecto #selSegmento').prop('disabled', false);
        $(this).prop('disabled', true);
        $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
        $.ajax({            
            url: _ApplicationUrl + '/api/CrmProyectoV2',
            type: 'POST',
            cache: false,
            data: $('#frmDvModalNuevoProyecto').serialize(),
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(crearProyecto, this);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            
            var $rbVtaEsporadica=$('#dvModalNuevoProyecto #rbVtaEsporadica');
            var bTipoVentaEsporadicaElegida=$rbVtaEsporadica.is(':checked');
            if(bTipoVentaEsporadicaElegida==true){
                navegarAPedidoEsporadico();
            }else{                
                alertify.success('El proyecto ha sido creado con éxito');
                $('#dvModalEditarProyecto').modal('hide');
                $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');                
            }
            /*    
            $.each(response.Notificaciones, function(index, element){
                Notificaciones.agregarNotificacionRIK(new crm.navegacion.Notificacion({contenido: element.Notif_Contenido, tipo: element.Id_TipoNotificacion, id: element.Id_Notificacion}));
            });
            */                

            $('#dvModalNuevoProyecto').modal('hide');
        }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                case 521:
                    alertify.errorsuccess('El proyecto ha sido creado con éxito');
                    PatternflyToast.showWarning('El proyecto ha sido creado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage, 10000);
                    $('#dvModalNuevoProyecto').modal('hide');
                    $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
                    break;
                default:                    
                    $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
                    PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                    break;
            }
        }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
            $('#dvModalNuevoProyecto #selUEN').prop('disabled', true);
            $('#dvModalNuevoProyecto #selSegmento').prop('disabled', true);
            $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
            $('#dvModalNuevoProyecto #selSegmento').selectpicker('refresh');
            $(this).prop('disabled', false);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
        });
    }

    var _otrosSeleccionado=false;

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function mostrarMapaDeOferta(){
        $('#dvModalNuevoProyecto #dvMapaDeOferta').fadeIn();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function ocultarMapaDeOferta(){
        $('#dvModalNuevoProyecto #dvMapaDeOferta').fadeOut();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function retirarElementosDeAplicacionEnForma($forma){
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function limpiarFormaNuevoProyecto() {
        $('#hdnDim_Id_Uen').val(null);
        $('#hdnDim_Id_Seg').val(null);
        $('#txtDimension').val('');
        $('#txtCantidad').val('');
        $('#txtVPM').val('');
        var $selTerritorio = $('#dvModalNuevoProyecto #selTerritorio');
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');
        $('#dvModalNuevoProyecto #selUEN').selectpicker('val', 0);
        $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
        despopularCascadaDependientesSelectorTerritorio();
        //despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarTerritoriosDeProspecto($, jqElement, idRik, idProspecto, onSuccess, onFailure) {

        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
        $.ajax({            
            url: _ApplicationUrl + '/api/ProspectoTerritorio/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
            cache: false,
            type: 'GET',
            async: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {                    
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(cargarTerritoriosDeProspecto, null, $, jqElement, idRik, idProspecto, onSuccess, onFailure);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            var $selTerritorio = jqElement;
            $selTerritorio.find('option').remove();        
            var ContTerritorios = 0;

            $.each(response, function (index, element) {
                var node = $('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
                node.data('objterritorio', element);
                $selTerritorio.append(node);
                ContTerritorios++;
            });
            $selTerritorio.selectpicker('val', 0);
            $selTerritorio.selectpicker('refresh');

            if (ContTerritorios <= 0) {
                $('#divErrorEncontado').css('display','block');
                $('#divBotonesAccion').css('display', 'none');
            } else {
                $('#divErrorEncontado').css('display', 'none');
                $('#divBotonesAccion').css('display', 'block');
            }

            if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                onSuccess();
            }
        }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    //
    // Nuevo Proyecto
    //
    function inicializarModalNuevoProyecto() {        
        $('#dvModalNuevoProyecto').on('show.bs.modal', function (event) {                
        //console.log('1777');
            var trigger = $(event.relatedTarget);
            $('#btnDvModalNuevoProyectoGuardar').prop('disabled', false);
            var rowId = $(trigger).data('rowidx');            
            rowId = _ROWIDX;            
            var datosProspecto = $('#tblProspectos').DataTable().row(rowId).data();
            if (_prospectoElegido != null) {
                datosProspecto = _prospectoElegido;
            }

            var $selTerritorio = $('#dvModalNuevoProyecto #selTerritorio');

            limpiarFormaNuevoProyecto();
            cargarTerritoriosDeProspecto($, $selTerritorio, datosProspecto.Id_Rik, datosProspecto.Id_CrmProspecto);

            $('#dvModalNuevoProyecto #hdnId_CrmProspecto').val(datosProspecto.Id_CrmProspecto);
            $('#dvModalNuevoProyecto #hdnCliente').val(datosProspecto.Id_Cte);
            //$('#dvModalNuevoProyecto #txtNombreProspecto').val(datosProspecto.Cte_NomComercial);
            $('#dvModalNuevoProyecto #txtNombreProspecto').text(datosProspecto.Cte_NomComercial);

            if (rowId != undefined) {
                cargarDescripcion(rowId);
            } else {
                cargarDescripcionProspecto(datosProspecto);
            }
            selTerritorio_on_change();
                
        });
        $('#dvModalNuevoProyecto').on('hide.bs.modal', function (event) {
            _prospectoElegido = null;
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function IniciaProyecto() {            
        $('#dvModalNuevoProyecto').modal('show');
        //inicializarModalNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function NuevoProyecto(obj) {            
        var IdCrmProspecto = $(obj).data('idcrmprospecto');                                   
        var RowIdx = $(obj).data('rowidx');                                   
        _ROWIDX = RowIdx;        
        cargarDescripcion(RowIdx);
        //Territorios(_EntidadSesion_Id_Rik, IdCrmProspecto, IniciaProyecto);
        IniciaProyecto();

        /* if (territorios.legth) {
            alerify.error('No ha realizado la asociacion de territorios.');
        } else {
            inicializarModalNuevoProyecto();
        }*/
            
        /*
        var IdCrmProspecto = $(obj).attr('data-idcrmprospecto');           
        var rowId = $(trigger).data('rowidx');
        var datosProspecto = $('#tblProspectos').DataTable().row(rowId).data();
        console.log(datosProspecto);
        cargarTerritoriosDeProspecto($, $selTerritorio, datosProspecto.Id_Rik, IdCrmProspecto);*/
        //alert("Nuevo Proyecto");
        //$('#dvModalNuevoProyecto').modal('show');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function listadoTerritoriosListo(territorios) {
        cargarTerritoriosParaDialogoNuevoProspecto(territorios);
        cargarTerritoriosParaDialogoEditarProspecto(territorios);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function listadoTerritoriosFallido() {
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function listadoTerritoriosSiempre() {
        $('#dvModalNuevoProspecto').find('#imgProgreso').fadeOut();
        $('#dvModalEditarProspecto').find('#imgProgreso').fadeOut();
        $('#dvModalNuevoProspecto #selTerritorio').attr('disabled', false);
        $('#dvModalEditarProspecto #selTerritorio').attr('disabled', false);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarListadosDeTerritorios() {
        $('#dvModalNuevoProspecto').find('#imgProgreso').fadeIn();
        $('#dvModalEditarProspecto').find('#imgProgreso').fadeIn();
        $('#dvModalNuevoProspecto #selTerritorio').attr('disabled', true);
        $('#dvModalEditarProspecto #selTerritorio').attr('disabled', true);
                
        //cargarDatosTerritorios($, $.proxy(listadoTerritoriosListo), $.proxy(listadoTerritoriosFallido), $.proxy(listadoTerritoriosSiempre));
        var TerrLst = ObtenerTerritorios_PorRik();

        listadoTerritoriosListo(TerrLst);

        /*
        ObtenerTerritorios_PorRik(function(){
            //listadoTerritoriosListo(territorios);
            cargarTerritoriosParaDialogoNuevoProspecto(territorios);
            cargarTerritoriosParaDialogoEditarProspecto(territorios);

        });
        */
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarTerritoriosParaDialogoNuevoProspecto(datosTerritorios) {
        var dvTerritoriosElement = $('#dvModalNuevoProspecto #dvTerritorios');
        var jqElement = $(dvTerritoriosElement).find('#selTerritorios');

        var $selTerritorio = jqElement;
        $selTerritorio.find('option').remove();
        $.each(datosTerritorios, function (index, element) {
            $selTerritorio.append('<option value="' + element.Id_Ter + '">' + element.Ter_Nombre + '</option>');
        });
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarTerritoriosParaDialogoEditarProspecto(datosTerritorios) {
        var dvTerritoriosElement = $('#dvModalEditarProspecto #dvEdicionProspectosTerritorios');
        var jqElement = $(dvTerritoriosElement).find('#selTerritorios');
        var $selTerritorio = jqElement;
        $selTerritorio.find('option').remove();
        $.each(datosTerritorios, function (index, element) {
            $selTerritorio.append('<option value="' + element.Id_Ter + '">' + element.Ter_Nombre + '</option>');
        });
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    //Consulta los territorios asociados a un RIK
    function cargarDatosTerritorios($, onSuccess, onFailure, always) {
        $.ajax({            
            url: _ApplicationUrl + '/api/CatTerritorio',
            type: 'GET',
            cache: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(cargarTerritorios, this, $, dvTerritoriosElement, jqElement, onSuccess, onFailure);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                if (onSuccess != null)
                    onSuccess(response);
            }
        }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                default:                    
                    $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                    $('#toastDanger').fadeIn();
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 3000);
                    break;
            }
            if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                if (onFailure != null)
                    onFailure($);
            }
        }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            if (typeof (always) != undefined && typeof (always) != 'undefined') {
                if (always != null)
                    always(jqXHROrData);
            }
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarTerritoriosPorRIK($, jqElement, idRik, onSuccess, onFailure) {
        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
        $.ajax({            
            url: _ApplicationUrl + '/api/CatTerritorio/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + idRik,
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
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function selTerritorio$on_change(_this) {
        var value = $(_this).selectpicker('val');
        var objTerritorio = $(_this).find('option[value="' + value + '"]').data('objterritorio');
        despopularCascadaDependientesSelectorTerritorio();
        var $selUEN = $('#dvModalNuevoProyecto #selUEN');
        var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
        $('#txtDimension').val('');
        $('#txtPrecioUnidad').val('');
        if (objTerritorio != typeof (undefined) && objTerritorio != 'undefined' && objTerritorio != undefined) {
            cargarSelUEN($selUEN, objTerritorio.CatUENSerializable);
            cargarSegmento($selSegmento, objTerritorio.CatSegmentoSerializable);
            selSegmento$on_change();
        }
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function selTerritorio_on_change() {
        var value = $('#selTerritorio').selectpicker('val');        
        var objTerritorio = $('#selTerritorio').find('option[value="' + value + '"]').data('objterritorio');
        //console.log('objTerritorio:'+objTerritorio);
        despopularCascadaDependientesSelectorTerritorio();
        var $selUEN = $('#dvModalNuevoProyecto #selUEN');
        //console.log('selUEN:'+selUEN);
        var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
        //console.log('selSegmento:'+selSegmento);
        $('#txtDimension').val('');
        $('#txtPrecioUnidad').val('');
        if (objTerritorio != typeof (undefined) && objTerritorio != 'undefined' && objTerritorio != undefined) {
            cargarSelUEN($selUEN, objTerritorio.CatUENSerializable);
            cargarSegmento($selSegmento, objTerritorio.CatSegmentoSerializable);
            selSegmento$on_change();
        }
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function despopularCascadaDependientesSelectorTerritorio() {
        $('#selUEN').find('option').remove();
        $('#selUEN').selectpicker('refresh');
        despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarSegmento(jqelement, objSeg) {
        jqelement.append('<option value="' + objSeg.Id_Seg + '">' + objSeg.Seg_Descripcion + '</option>');
        jqelement.selectpicker('val', 0);
        jqelement.selectpicker('refresh');

        $('#dvModalNuevoProyecto #hdnDim_Id_Uen').val(objSeg.Id_Uen);
        $('#dvModalNuevoProyecto #hdnDim_Id_Seg').val(objSeg.Id_Seg);
        $('#dvModalNuevoProyecto #txtDimension').val(objSeg.Seg_Unidades);
        $('#dvModalNuevoProyecto #txtPrecioUnidad').val(objSeg.Seg_ValUniDim);
        _valorUnidadDimension = objSeg.Seg_ValUniDim;
        var cantidad = $('#dvModalEditarProyecto #txtCantidad').val();
        cantidad = isNaN(cantidad) ? 0 : cantidad;
        $('#dvModalNuevoProyecto #txtVPM').val(cantidad * objSeg.Seg_ValUniDim);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarSelUEN(jqelement, objUen) {
        jqelement.append('<option value="' + objUen.Id_Uen + '">' + objUen.Uen_Descripcion + '</option>');
        jqelement.selectpicker('val', 0);
        jqelement.selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function round(value, decimals) {
        return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function contenidoPersonalizadoAplicacion(aplicacion, indice) {
        var cantidad = $('#txtCantidad').val();
        if (cantidad == '') {
            cantidad = 0;
        }
        return '<div class="list-group-item" item> ' +
                    '<table>' +
                        '<tr>' +
                            '<td style="width: 33%;">' +
                                '<h6 class="list-group-item-heading">' +
                                    aplicacion.Apl_Descripcion +
                                '</h6>' +
                            '</td>' +
                            '<td style="width: 33%;" id="tdVPT"> VPT: ' +
                                numeral(round(aplicacion.Apl_Potencial / 100.0 * cantidad * _valorUnidadDimension, 2)).format('$0,0.00') +
                            '</td>' +
                            '<td style="width: 33%;">' +
                                '<div style="display: none;">' +
                                    '<input type="text" name="FormaAplicaciones[' + indice + '].Id_Aplicacion" value="' + aplicacion.Id_Apl + '">' +
                                '</div>' +
                                '<div class="row">' +
                                    '<div class="col-md-1">' +
                                        'VPO:' +
                                    '</div>' +
                                    '<div class="col-md-6">' +
                                        '<input type="text" id="txtAplVPO_' + aplicacion.Id_Apl + '" style="display: none;" class="form-control" onchange="txtAplVPO$onchange(this)" name="FormaAplicaciones[' + indice + '].VPO" data-inputmask="\'alias\' : \'currency\', \'autoUnmask\' : \'true\'">' + //aplicacion.Porcentaje/100.0 +
                                    '</div>' +
                                '</div>' +
                            '</td>' +
                            '<td style="text-align: right;">' +
                                '<input type="checkbox" id="chkApl_' + aplicacion.Id_Apl + '" data-idapl="' + aplicacion.Id_Apl + '" onchange="chkApl_onchange(this)" chkAplicacion name="FormaAplicaciones[' + indice + '].Seleccionado"/>' +
                            '</td>' +
                        '</tr>' +
            '</table>' +
        '</div>';
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function chkApl_onchange(sender) {
        var chk = $(sender);
        var valoresAps = $('#selAplicacion').selectpicker('val');
        var apId = chk.data('idapl');
        if (chk.is(':checked') == true) {
            valoresAps.push(apId);
        }
        else {
            valoresAps = $.grep(valoresAps, function (value) {
                return value != apId;
            });
        }
        $('#selAplicacion').selectpicker('val', valoresAps);
        $('#selAplicacion').selectpicker('refresh');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function crearProyectoYContinuar() {
        $('#dvModalNuevoProyecto #selUEN').prop('disabled', false);
        $('#dvModalNuevoProyecto #selSegmento').prop('disabled', false);
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
                    _onLoginSuccessful = $.proxy(crearProyectoYContinuar, this);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            actualizarAplicacionesVPO(response.Id_Op, function () {
                $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito. Redirigiendo...');
                $('#toastSuccess').fadeIn();
                setTimeout(function () {
                    $('#toastSuccess').fadeOut();
                }, 3000);
                $('#dvModalNuevoProyecto').modal('hide');
                window.location.href = 'Proyectos.aspx?Id_Cliente=' + response.Cliente + '&Id_Op=' + response.Id_Op;
            }, function (jqXHR, textStatus, error) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                case 521:
                    $('#toastWarning #toastWarningMessage').html('El proyecto ha sido creado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage);
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
            $('#dvModalNuevoProyecto #selUEN').prop('disabled', true);
            $('#dvModalNuevoProyecto #selSegmento').prop('disabled', true);
            $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
            $('#dvModalNuevoProyecto #selSegmento').selectpicker('refresh');
            $(this).prop('disabled', false);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function dimensionElegida(idUen, idSeg, unidades) {
        $('#dvModalNuevoProyecto #hdnDim_Id_Uen').val(idUen);
        $('#dvModalNuevoProyecto #hdnDim_Id_Seg').val(idSeg);
        $('#dvModalNuevoProyecto #txtDimension').val(unidades);
        $('#dvModalDimension').modal('hide');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function txtAplVPO$onchange(sender) {
        var objetoDatos = $(sender).data('obj');
        objetoDatos.CrmOpAp_VPO = $(sender).val();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function txtRFCPH_onincomplete() {
        $(this).val('');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function txtCantidad$onchange(sender) {
        var cantidad = $('#txtCantidad').val();
        if (isNaN(cantidad)) {
            cantidad = 0;
        }
        var precio = $('#dvModalNuevoProyecto #txtPrecioUnidad').val();
        if (precio == '')
            precio = 0;
        $('#dvModalNuevoProyecto #txtVPM').val(precio * cantidad);
        var elementos = $('#lstAplicacion [item]');
        $.each(elementos, function (index, item) {
            var objetoDatos = $(item).data('obj');
            $(item).find('#tdVPT').text('VPT: ' + numeral(round(objetoDatos.Apl_Potencial / 100.0 * cantidad * _valorUnidadDimension, 2)).format('$0,0.00'));
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    //
    // Nombre de Empresa
    //
    function inicializarCampoNombreDeEmpresa(parent) {
        $('#' + parent + ' #txtNombre').autocomplete({
            source: function (request, response) {
            _buscarProspecto(parent, request, response);
        },
        open: function (event, ui) {
            _bProspectoSeleccionadoDeLista = false;
        },
        change: function (event, ui) {
            if (!_bProspectoSeleccionadoDeLista) {
                        if (_peticionDeBusquedaNombreEmpresa != null) {
                            if (_peticionDeBusquedaNombreEmpresa.readystate != 4) {
                                try {
                                    _peticionDeBusquedaNombreEmpresa.abort();
                                } catch (e) {
                                    $('#toastDanger #toastDangerMessage').html(e.ToString());
                                    $('#toastDanger').fadeIn();
                                    setTimeout(function () {
                                        $('#toastDanger').fadeOut();
                                    }, 5000);
                                }
                                _responseObjectBusquedaNombreEmpresa = null;
                                if (_responseObjectBusquedaNombreEmpresa != null) {
                                    try {
                                        _responseObjectBusquedaNombreEmpresa([]);
                                    } catch (e) {
                                        $('#toastDanger #toastDangerMessage').html(e.ToString());
                                        $('#toastDanger').fadeIn();
                                        setTimeout(function () {
                                            $('#toastDanger').fadeOut();
                                        }, 5000);
                                    }
                                    _responseObjectBusquedaNombreEmpresa = null;
                                }
                            }
                        }
                        //validarNombreEmpresaProspecto(parent, event.currentTarget);
                    }
                },
                select: function (event, ui) {
                    //hubo selección. Se procede a condicionar los casos para cliente o prospecto elegido
                    //Se deben de preparar las condiciones necesarias para las acciones subsecuentes, pero definitivamente no serán peticiones que se generen de la forma contenida
                    //en el diálogo "Nuevo Prospecto"
                    //Al seleccionar un elemento, establecer el valor del RFC(si está disponible) en el campo RFC.
                    _bProspectoSeleccionadoDeLista = true;
                    event.preventDefault();
                    $('#' + parent + ' #lblMensajeNombreEmpresa').show();
                    $('#' + parent + ' #txtNombre').val(ui.item.label);
                    _prospectoElegido = ui.item.data;
                    $('#' + parent + ' #txtRFC').val(_prospectoElegido.Cte_Rfc);
                    prospectoElegido(parent);
                }
            });
            $('#' + parent + ' #txtNombre').attr('autocomplete', 'on');
        }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
    //dvModalNuevoProyecto
    function crearProyectoDeProspectoExistente(parent) {
        //TODO: Establecer los valores de los campos de la forma de proyecto para asociar al prospecto elegido.
        //hdnId_CrmProspecto
        $('#hdnId_CrmProspecto').val(_prospectoElegido.Id_CrmProspecto);
        $('#hdnCliente').val(_prospectoElegido.Id_Cte);
        $('#dvModalNuevoProyecto').modal('show');
        $('#dvModalNuevoProspecto').modal('hide');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cancelarCrearProyectoDeProspectoExistente(parent) {
        $('#' + parent + ' #dvContacto').show();
        $('#' + parent + ' #dvEmail').show();
        $('#' + parent + ' #navDireccion').show();
        $('#' + parent + ' #tabDireccion').show();
        $('#' + parent + ' #btnCrearProyectoDeProspectoExistente').hide();
        $('#' + parent + ' #btnDvModalNuevoProspectoGuardar').show();
        $('#' + parent + ' #btnCerrar').show();
        $('#' + parent + ' #btnCancelarCrearProyectoDeProspectoExistente').hide();
        $('#' + parent + ' #dvAlertaProspectoExistente').hide();

        //Mostrar los mensajes que indican la existencia de un cliente con los valores para los campos RFC y nombre de empresa.
        $('#' + parent + ' #lblMensajeRFC').show();
        $('#' + parent + ' #lblMensajeNombreEmpresa').show();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function prospectoElegido(parent) {
        $('#' + parent + ' #dvContacto').hide();
        $('#' + parent + ' #dvEmail').hide();
        $('#' + parent + ' #navDireccion').hide();
        $('#' + parent + ' #tabDireccion').hide();
        $('#' + parent + ' #btnCrearProyectoDeProspectoExistente').show();
        $('#' + parent + ' #btnDvModalNuevoProspectoGuardar').hide();
        $('#' + parent + ' #btnCerrar').hide();
        $('#' + parent + ' #btnCancelarCrearProyectoDeProspectoExistente').show();
        $('#' + parent + ' #dvAlertaProspectoExistente').show();
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function _buscarProspecto(parent, request, response) {
        _responseObjectBusquedaNombreEmpresa=response;
        if(_peticionDeBusquedaExactaNombreEmpresa!=null){
            if(_peticionDeBusquedaExactaNombreEmpresa.readystate!=4){
                try{
                    _peticionDeBusquedaExactaNombreEmpresa.abort();
                }catch(e){
                    $('#toastDanger #toastDangerMessage').html(e.ToString());
                    $('#toastDanger').fadeIn();
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 5000);
                }
                _peticionDeBusquedaExactaNombreEmpresa=null;
            }
        }
        var terminoDeBusqueda = $('#' + parent + ' #txtNombre').val();
        var $imgProspectoEnOperacion = $('#' + parent + ' #imgNombreEmpresaEnOperacion');
        $imgProspectoEnOperacion.show();
        var data = null;
        _peticionDeBusquedaNombreEmpresa = $.ajax({                
            url: _ApplicationUrl  + '/api/CrmProspecto?terminoDeBusqueda=' + terminoDeBusqueda + '&incluirClientes=true',
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(_buscarProspecto, this, parent, request, response);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            data = $.map(response, function (p) {
                return { value: p.Id_Cte, label: p.Cte_NomComercial, data: p };
            });
        }).fail(function (jqXHR, textStatus, error) {
        }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            $imgProspectoEnOperacion.hide();
            response(data);
            _responseObjectBusquedaNombreEmpresa=null;
            _peticionDeBusquedaNombreEmpresa=null;
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function validarNombreEmpresaProspecto(parent, target){
        $('#' + parent + ' #imgNombreEmpresaEnOperacion').show();
        $('#' + parent + ' #lblMensajeNombreEmpresa').hide();
        _peticionDeBusquedaExactaNombreEmpresa = $.ajax({                
            url: _ApplicationUrl + '/api/CatCliente?nombreEmpresa=' + $(target).val() + '&sinUsar=',
            type: 'GET',
            cache: false,
            contentType: 'application/json',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(validarNombreEmpresaProspecto, null, parent, target);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            if(response==null){
                $('#' + parent + ' #lblMensajeNombreEmpresa').hide();
            }else{
                if($.isArray(response)){
                    if(response.length>0){
                        $('#' + parent + ' #lblMensajeNombreEmpresa').show();
                    }else{
                        $('#' + parent + ' #lblMensajeNombreEmpresa').hide();
                    }
                }
            }
        }).fail(function (jqXHR, textStatus, errorThrown){
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
        }).always(function (jqXHR, textStatus, errorThrown) {
            $('#' + parent + ' #imgNombreEmpresaEnOperacion').hide();
            _peticionDeBusquedaExactaNombreEmpresa=null;
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function inicializarMapaDeOferta(){
        //#dvMapaDeOferta
        var $proxyExito=$.proxy(cargarMapaDeOfertaExito, {$mapaOferta: $('#dvMapaDeOferta')});
        var $proxyFalla=$.proxy(cargarMapaDeOfertaFalla, null);
        var $proxySiempre=$.proxy(cargarMapaDeOfertaSiempre, null);
        cargarMapaDeOferta($proxyExito, $proxyFalla, $proxySiempre);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarMapaDeOfertaExito(response, textStatus, jqXHR){
        if(response.succeeded==true){
            $(this.$mapaOferta).treeview({
                collapseIcon: collapseIcon,
                expandIcon: expandIcon,
                nodeIcon: nodeIcon,
                checkedIcon: checkedIcon,
                uncheckedIcon: uncheckedIcon,
                partiallyCheckedIcon: partiallyCheckedIcon,
                showBorder: false,
                showCheckbox: true,
                hierarchicalCheck: true,
                checkboxFirst: true,
                wrapNodeText: true,
                levels: 6,
                data: response.data,
                onNodeChecked: function(event, node){
                    if($(node).data('_initialized_')!=undefined && $(node).data('_initialized_')!='undefined'){
                        if($(node).data('_initialized_')!=null){
                            var bInitialized=$(node).data('_initialized_');
                            if(!bInitialized==true){
                                //Inicializar la estructura de elementos para acomodar el valor potencial observado
                                //Actualización: obtener el detalle (u objeto) de la aplicación elegida y pasarla al listado de aplicaciones asociadas al proyecto
                            }
                        }else{
                            //Inicializar la estructura de elementos para acomodar el valor potencial observado
                            //Actualización: obtener el detalle (u objeto) de la aplicación elegida y pasarla al listado de aplicaciones asociadas al proyecto
                        }
                    }else{
                        //Inicializar la estructura de elementos para acomodar el valor potencial observado
                        //Actualización: obtener el detalle (u objeto) de la aplicación elegida y pasarla al listado de aplicaciones asociadas al proyecto
                    }
                }
            });
            $(this.$mapaOferta).treeview('collapseAll', { silent: true });
        }else{
            //mostrar un mensaje en el cuerpo del elemento slate para avisar que el mapa de oferta no se encuentra disponible
            showDangerToast(response.mensaje, 10000);
        }
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarMapaDeOfertaFalla(jqXHR, textStatus, errorThrown){
        showDangerToast(jqXHR.responseJSON.message, 10000);
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarMapaDeOfertaSiempre(jqXHROrData, textStatus, errorOrjqXHR){
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function cargarMapaDeOferta(onSuccess, onFailure, always){
        $.ajax({                
            url: _ApplicationUrl + '/api/CargarMapaOferta',
            type: 'GET',
            cache: false
        }).done(function (response, textStatus, jqXHR) {
            if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                onSuccess(response, textStatus, jqXHR);
            }
                
        }).fail(function (jqXHR, textStatus, errorThrown) {
            if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                onFailure(jqXHR, textStatus, errorThrown);
            }
        }).always(function (jqXHROrData, textStatus, errorOrjqXHR) {
            if (typeof (always) != undefined && typeof (always) != 'undefined') {
                always(jqXHROrData, textStatus, errorOrjqXHR);
            }
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function empujarAplicacionesEnForma($forma, aplicaciones){
        $.each(aplicaciones, function(index, element){
            var $chk=$('<input type="checkbox" name="FormaAplicaciones[' + index + '].Seleccionado" checked />');
            var $vpo=$('<input type="text" name="FormaAplicaciones[' + index + '].VPO" value="0" />');
            var $apl=$('<input type="text" name="FormaAplicaciones[' + index + '].Id_Apl" value="' + element + '" />');
            $forma.append($chk);
            $forma.append($vpo);
            $forma.append($apl);
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function determinarAplicacionesElegidasOtros($mapaDeOferta){
        var nodosElegidos=$($mapaDeOferta).treeview('getChecked');
        var nodosDeAplicacion=$.grep(nodosElegidos, function(element, index){
            return element.level==5;
        });
        var aplicaciones=$.map(nodosDeAplicacion, function(element, index){
            return element.aplicacion;
        });
        return aplicaciones;
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function elegirOtrasAplicaciones(){
        //poblar el listado de aplicaciones: ver ejemplo en el manejador de evento para solución
        var $selAplicacion = $('#dvModalNuevoProyecto #selAplicacion');
        var $mapaDeOferta=$('#dvMapaDeOferta');
        var aplicaciones=determinarAplicacionesElegidasOtros($mapaDeOferta);
        poblarListadoAplicaciones($selAplicacion, aplicaciones);
        $('#dvModalNuevoProyecto #dvLstAplicacionesOtrosSlate').hide();
        $('#dvModalMapaOferta').modal('hide');
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function poblarListadoAplicaciones(jqElement, source){
        var idOp = $('#dvModalNuevoProyecto #hdnId_Op').val();
        var idOpVar = idOp != null ? idOp : '0';
        var $lstAplicacion = $('#lstAplicacion');
        $lstAplicacion.find('div').remove();

        jqElement.find('option').remove();
        //jqElement.append('<option value="0">--Seleccione--</option>');
        $.each(source, function (index, element) {
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
                    //Id_Cd: '<%=EntidadSesion.Id_Cd %>',
                    Id_Cd: _EntidadSesion_Id_Cd,
                    Id_Op: idOpVar != null ? idOpVar : 0,
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
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function navegarAPedidoEsporadico(){
        PatternflyToast.showSuccess('El proyecto ha sido creado con éxito. Se le redireccionar&aacute; a la captaci&oacute;n de los productos', 6000);            
        setTimeout(function(){
            //window.location = '<%=ApplicationUrl %>/ProPedidoVI_Admin.aspx?nuevaVentana=s';
            window.location = _ApplicationUrl+'/ProPedidoVI_Admin.aspx?nuevaVentana=s';
        }, 8000);
    }

    function EditarProspecto(obj) {
        //console.log('2631');
         /*   
        alert(idcrmprospecto);                
        var trigger = $(event.relatedTarget);
        var idCrmProspecto = trigger.data('idcrmprospecto');
        _indiceProspectoAActualizar = trigger.data('rowidx');
        _datosProspectoAActualizar = $('#tblProspectos').DataTable().row(_indiceProspectoAActualizar).data();
        limpiarFormaEditarProspecto();
        cargarCamposDialogoEditarProspecto(idCrmProspecto);
        cargarDescripcion(_indiceProspectoAActualizar);
        */

        var idcrmprospecto = $(obj).data('idcrmprospecto');
        var rowidx = $(obj).data('rowidx');
        limpiarFormaEditarProspecto();
        _datosProspectoAActualizar = $('#tblProspectos').DataTable().row(rowidx).data();
        cargarCamposDialogoEditarProspecto(idcrmprospecto);
        cargarDescripcion(rowidx);

        $('#dvModalEditarProspecto').modal('show');
        // 
        switch (_Parametro_ControlesSoloLectura) {
        case 0:
            //Edicion       
            $('#btnProspectoEditarGuardar').prop('disabled',false);            
            break;
        case 1:
            // Solo lectura 
            $('#btnProspectoEditarGuardar').prop('disabled',true);            
            break;
        default:            
            $('#btnProspectoEditarGuardar').prop('disabled',true);
            break;
        }        
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function chbBuscarCliente_Sel(obj) {
        
        var IdCte_Actual = $(obj).data(cte);

        for (var i = 0; i < 250; i++) {            
            var RowCte_Checked = $('#chbCte_'+i).is(":checked");            

            if (RowCte_Checked) {
                var IdCte = $('#chbCte_'+i).data('id_cte');            
                if (IdCte_Actual == IdCte) {

                } else {                    
                    $('#chbCte_'+i).prop('checked', false);
                }
            }
        }
        //
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function btnBuscarCliente_Aplicar(obj) {
        var IdCte = $(obj).data('id_cte');  
        Consultar_PorId_Cte (IdCte, function(REG) {

            $('#modalBuscarCliente').modal('hide');  

            $('#txtRFC').val(REG.Cte_FacRfc);
            $('#txtRFC').val(REG.Cte_FacRfc);
            $('#txtNombre').val(REG.Cte_NomComercial);
            $('#txtContacto').val(REG.Cte_Contacto);
            
            $('#txtEmail').val(REG.Cte_Email);
            $('#txtCalle').val(REG.Cte_FacCalle);
            $('#txtTelefono').val(REG.Cte_Telefono);
            $('#tbNoCte').val(REG.Id_Cte);
            $('#hdnId_Cte').val(REG.Id_Cte);
            $('#hdnCrearNuevo').val(0);

            DatosCliente_SoloLectura();
                      
        });
    }
    
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    // ready
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $(document).ready(function () {

        $('#btnBuscarCliente_Cancelar').click(function (e) {              
            $('#modalBuscarCliente').modal('hide');            
        });

        // boton Buscar en Catalogo
        $('#btnBuscarCliente_Buscar').click(function (e) {                          
            var tb = $('#tbBuscarCliente_Texto').val();
            tb = tb.trim(); 

            if (tb.length < 3) {                    
                $('#trBuscarCliente_TextoError').css('display','block');
            } else {
                $('#trBuscarCliente_TextoError').css('display','none');
                // Ejecutar la busqueda
                BuscarCliente_wcb(tb, function (){
                    alert('Exito');
                });
            }
        });

        // enter en texto buscar 
        $('#tbBuscarCliente_Texto').keypress(function(e) {
            if(e.which == 13) {
                var tb = $('#tbBuscarCliente_Texto').val();
                tb = tb.trim(); 
                if (tb.length < 3) {                    
                    $('#trBuscarCliente_TextoError').css('display','block');
                } else {
                    $('#trBuscarCliente_TextoError').css('display','none');
                    // Ejecutar la busqueda
                    BuscarCliente_wcb(tb, function (){
                        alert('Exito');
                    });
                }
            }
        });
    
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
        $('#dvModalNuevoProyecto [type="radio"]').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue'
        });
            
            $('#dvModalNuevoProspecto #btnMapaUbicacionCalleProspecto').data('selectionlistener', mapaCalleProspectoUbicacionSeleccionada);
            $('#dvMenu_Territorios').asociacionprospectoterritorio();
            $('#Menu_Territorios').asociacionprospectoterritorio();
                       
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $.validator.addMethod('territorioProspectoValido', function (value, element, arg) {
                return arg != value;
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#frmDvModalNuevoProspecto').validate({
                submitHandler: function () {
                    crearProspecto(); 
                },
                rules: {
                    'selTerritorioProspecto': { territorioProspectoValido: '0' }
                },
                messages: {
                    'selTerritorioProspecto': { territorioProspectoValido: 'Por favor, elija un territorio' }
                }
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#frmDvModalEditarProspecto').validate({
                submitHandler: function () {
                    crearProspecto();
                },
                rules: {
                    'selTerritorioProspecto': { territorioProspectoValido: '0' }
                },
                messages: {
                    'selTerritorioProspecto': { territorioProspectoValido: 'Por favor, elija un territorio' }
                }
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#dvModalMapaGoogle').on('hidden.bs.modal', function(){
                if($('#dvModalNuevoProspecto').hasClass('in')){
                    $('body').addClass('modal-open');
                }
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#dvModalMapaOferta').on('show.bs.modal', function(){
                //console.log('2664');
                if(_otrosSeleccionado==false){
                    //$('#dvMapaDeOferta').treeview('disableAll', {silent: true, keepState: true});
                    $('#dvModalMapaOferta #btnAceptar').hide();
                }else{
                    //$('#dvMapaDeOferta').treeview('disableAll', {silent: true});
                    $('#dvModalMapaOferta #btnAceptar').show();
                }
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#dvModalMapaOferta').on('hidden.bs.modal', function(){
                if($('#dvModalNuevoProyecto').hasClass('in')){
                    $('body').addClass('modal-open');
                }
                $('#dvMapaDeOferta').treeview('uncheckAll');
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            //
            // Modal Contacto
            //
            $('#dvModalNuevoContacto').on('hidden.bs.modal', function () {
                //alert('xx');
                /*  if ($('#dvModalNuevoProspecto').hasClass('in')) {
                    $('body').addClass('modal-open');
                }*/
                // 
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $.fn.dataTable.ext.errMode=function(settings, helpPage, message){
                $($('#tblProspectos').DataTable().table().container()).popover({
                    content: 'Ha ocurrido un error al cargar los prospectos. Haga click <a>aquí</a> para intentar nuevamente. Gracias.',
                    html: true
                });
            };

            //Recargar_TblProspecto(_EntidadSesion_Id_Rik);
            
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            _tablaProspectos = $('#tblProspectos').DataTable({ /*"sDom": "<'dataTables_header' <'row' <'col-md-9' f i r> <'col-md-2' <'#tblProspectosToolbar'> > > >" +
                                                        "<'table-responsive'  t >" +
                                                        "<'dataTables_footer' p >",*/
                'pageLength': 7,
                'ordering': true,
                'language': {
                    'url': 'http://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json'
                },
                'ajax': {                
                    'url': _ApplicationUrl + '/api/CrmProspecto/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + _EntidadSesion_Id_Rik ,
                    'dataSrc': ''
                },
                "columns": [
                                /*{
                                    'data': 'Id_CrmProspecto',
                                    'render': function (data, type, full, meta) {
                                        return '<a>' + full.Id_CrmProspecto + '</a>';
                                    }
                                },*/
                                {
                                    'data': 'Cte_NomComercial',
                                    'render': function (data, type, full, meta) {
                                        return '<a id="hrefCargarDescripcion_'+meta.row+'" href="javascript:cargarDescripcion(' + meta.row + ')">' + full.Cte_NomComercial + '</a>';
                                    }
                                },
                                {
                                    'data': null,
                                    'className': "myCenteredCellTable",
                                    'defaultContent': '<button '+
                                        'class="btn btn-primary" '+
                                        //'data-toggle="modal" '+
                                        //'data-target="#dvModalEditarProspecto" '+
                                        'onclick="EditarProspecto(this);" ' +
                                        '>'+
                                        '<i class="'+ICON_EDITAR+'"></i>'+
                                    '</button>',
                                    'render': function (data, type, full, meta) {
                                        return '<button '+
                                        'class="btn btn-primary" '+
                                        //'data-toggle="modal" '+
                                        //'data-target="#dvModalEditarProspecto" '+
                                        'data-idcrmprospecto="' + data.Id_CrmProspecto + '" '+
                                        'data-rowidx="' + meta.row + '" '+
                                        'onclick="EditarProspecto(this);" ' +                                        
                                        '>'+
                                            '<i class="'+ICON_EDITAR+'"></i>'+
                                        '</button>';
                                    }
                                },
                                /*{
                                    'data': null,
                                    'className': "myCenteredCellTable",
                                    'defaultContent': '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" ><i class="fa fa-tasks"></i></button>',
                                    'render': function (data, type, full, meta) {
                                        return '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" data-idcrmprospecto="' + data.Id_CrmProspecto + '" data-rowidx="' + meta.row + '" >'+
                                        '<i class="fa fa-tasks"></i></button>';
                                    }
                                },*/
                                    {
                                    'data': null,
                                    'className': "myCenteredCellTable",
                                    'defaultContent': '<button class="btn btn-primary">'+
                                        '<i class="'+ICON_NUEVO+'"></i>'+
                                        '</button>',
                                    'render': function (data, type, full, meta) {
                                        return '<button '+
                                        'class="btn btn-primary" '+
                                        'id="btnNuevoProyecto_'+data.Id_CrmProspecto+'" '+
                                        'onclick="NuevoProyecto(this);" '+
                                        'data-idcrmprospecto="'+data.Id_CrmProspecto+'" '+
                                        'data-rowidx="'+meta.row+'" '+
                                        '>'+
                                        '<i class="'+ICON_NUEVO+'"></i></button>';
                                    }
                                },
                                {
                                    'data': null,
                                    'className': "myCenteredCellTable",
                                    'render': function (data, type, full, meta) {
                                        return '<button class="btn btn-primary" '+
                                        'data-rowid="' + meta.row + 
                                        //'data-toggle="modal" '+
                                        //'data-target="#dvModalEliminarProspecto" '+
                                        'onclick="EditarProspecto(this);" ' +
                                        '>'+
                                        '<i class="'+ICON_ELIMINAR+'"></i></button>';
                                },
                                    'defaultContent': '<button class="btn btn-primary" '+
                                    //'data-toggle="modal" '+
                                    //'data-target="#dvModalEliminarProspecto" '+
                                    'onclick="EditarProspecto(this);" ' +
                                    '>'+
                                    '<i class="'+ICON_ELIMINAR+'"></i>'+
                                    '</button>'
                                }
                                ]
            });

            //$('#tblProspectosToolbar').html('<button class="btn btn-default" data-toggle="modal" data-target="#dvModalNuevoProspecto" ><i class="'+ICON_MAS+'"></i> Nuevo Prospecto</button>');

            $('#tblProspectosToolbar').css('padding', '2px 0');
            //$('#tblProyectos').DataTable({searching: false});
            var estadoPairs = [
                    { value: 0, text: 'Análisis' },
                    { value: 1, text: 'Promoción' },
                    { value: 2, text: 'Negociación' },
                    { value: 3, text: 'Cierre' }
                    ];
            //createHorizontalSelectors(estadoPairs, '.hSelectorEstado');
            var defaultData = [
                    {
                        text: 'INSTITUCIONAL BASICA',
                        icon: 'fa fa-industry',
                        nodes: [
                            {
                                text: 'MANUFACTURA',
                                icon: 'fa fa-road',
                                nodes: [
                                    {
                                        text: 'Presentación Key.doc',
                                        icon: 'fa fa-file-word-o'
                                    },
                                    {
                                        text: 'Página del portal de Key',
                                        href: 'http://www.key.com.mx',
                                        icon: 'fa fa-external-link'
                                    }
                                ]
                            },
                            {
                                text: 'EDIFICIOS / TIENDAS DEPARTAMENTALES',
                                icon: 'fa fa-road',
                                nodes: [
                                    {
                                        text: 'Catálogo de productos.xlsx',
                                        icon: 'fa fa-file-excel-o'
                                    }
                                ]
                            },
                            {
                                text: 'COMPAÑIAS DE LIMPIEZA',
                                icon: 'fa fa-road'
                            }
                        ]
                    },
                    {
                        text: 'INSTITUCIONAL ESPECIALIZADA',
                        icon: 'fa fa-industry',
                        nodes: [
                            {
                                text: 'HOTELES',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'HOSPITALES',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'RESTAURANTES / COMEDORES INDUSTRIALES / COMISARIATOS / CINES',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'SUPERMERCADOS / AUTOSERVICIOS / FARMACIAS / TIENDAS DE CONVENIENCIA',
                                icon: 'fa fa-road'
                            }
                        ]
                    },
                    {
                        text: 'INDUSTRIAL',
                        icon: 'fa fa-industry',
                        nodes: [
                            {
                                text: 'INDUSTRIA EN GENERAL',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'INDUSTRIA DE TRANSPORTE',
                                icon: 'fa fa-road'
                            }
                        ]
                    },
                    {
                        text: 'ALIMENTARIA',
                        icon: 'fa fa-industry',
                        nodes: [
                            {
                                text: 'CARNICOS',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'POLLOS',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'LACTEOS',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'PANIFICACION',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'EMBOTELLADORAS',
                                icon: 'fa fa-road'
                            },
                            {
                                text: 'DIVERSOS',
                                icon: 'fa fa-road'
                            }
                        ]
                    }
                ];
            
            $('#dvHerramientas').treeview({
                collapseIcon: "fa fa-angle-down",
                data: defaultData,
                expandIcon: "fa fa-angle-right",
                nodeIcon: "fa fa-folder",
                showBorder: false,
                enableLinks: true
            });

            $('#dvHerramientas').treeview('collapseAll', { silent: true });

            /*
            var $selTerritorio = $('#dvModalNuevoProyecto #selTerritorio');
            cargarTerritoriosPorRIK($, $selTerritorio, '<%=EntidadSesion.Id_Rik %>');
            */
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#dvModalDimension').on('hidden.bs.modal', function () {
                if ($('#dvModalNuevoProyecto').hasClass('in')) {
                    $('body').addClass('modal-open');
                }
            });

            $('#dvModalMapaGoogle #btnMapaUbicacionNegocio').data('selectionlistener', mapaNegocioUbicacionSeleccionada);
            $('#dvModalMapaGoogle #btnMapaUbicacionHogar').data('selectionlistener', mapaHogarUbicacionSeleccionada);
            $('#dvModalNuevoProspecto #btnMapaUbicacionCalleProspecto').data('selectionlistener', mapaCalleProspectoUbicacionSeleccionada);

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#dvModalMapaGoogle').on('show.bs.modal', function (e) {
                //console.log('2909');
                var trigger = $(e.relatedTarget);
                _modalGoogleMapsCallback = $(trigger).data('selectionlistener');
                google.maps.event.trigger(_mapa, "resize");

                if (_ultimoMarcadorMapa != null) {
                    _ultimoMarcadorMapa.setMap(null);
                }
                _ultimoMarcadorMapa = null;
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#dvModalMapaGoogle').on('shown.bs.modal', function (e) {
                google.maps.event.trigger(_mapa, "resize");
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('input[iCheck]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            /*$('#aMapaOferta').click(function (e) {
                e.preventDefault();
                $(this).ekkoLightbox();
                $('#dvModalMapaDeOferta').modal('show');
            });*/

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#btnContactoNuevo').click(function (e) {              
                // Inicializa la forma contacto             
                $('#hdContacto_Id_Emp').val(Id_Emp);
                $('#hdContacto_Id_Cd').val(Id_Cd);
                $('#hdContacto_Id_Cte').val(Id_Cte);
                $('#hdContacto_Id_Ter').val(Id_Ter);
                $('#hdContacto_Id_Consecutivo').val(0)

                $('#txtContactoNombre').val('');
                $('#txtContactoPuesto').val('');
                $('#txtContactoCumple').val('');
                $('#txtContactoCorreo').val('');
                $('#txtContactoDireccion1').val('');
                $('#txtContactoDireccion2').val('');
                $('#txtContactoTelNegocio').val('');
                $('#txtContactoTelCasa').val('');                

                $('#ModalContactos').modal('hide');
                $('#modalContacto').modal('show');                
            });

            var $selUEN = $('#dvModalNuevoProyecto #selUEN');
            cargarUENs($, $selUEN);

            inhabilitarSelectoresDialogoNuevoProyecto();
            //inicializarModalEditarProspecto();
            inicializarModalNuevoProspecto();
            inicializarModalEliminarProspecto();
            inicializarModalNuevoProyecto();
            cargarListadosDeTerritorios();

            $('input').inputmask();

            var options = {
                cellHeight: 80,
                verticalMargin: 10
            };
            $('.grid-stack').gridstack(options);

            inicializarCampoNombreDeEmpresa('dvModalNuevoProspecto');

            inicializarMapaDeOferta();

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#btnRFCCrearNuevo').click( function () {
                $('#hdnId_Cte').val(0);
                $('#hdnCrearNuevo').val(1);
            
                $('#tbRFCMultiples').css('display','none');
                $('#btnRFCCrearNuevo').css('display','none');

                $('#dvMenu_General #txtNombre').attr('readonly',false);
                $('#dvMenu_General #txtContacto').attr('readonly',false);
                $('#dvMenu_General #txtEmail').attr('readonly',false);
                $('#dvMenu_General #txtCalle').attr('readonly',false);
                $('#dvMenu_General #txtTelefono').attr('readonly',false);                                       
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#btnRFCCancelar').click( function () {            
                $('#hdnId_Cte').val(0);                                
                $('#dvMenu_General #txtRFC').val('');
                $('#dvMenu_General #txtNombre').val('');
                $('#dvMenu_General #txtContacto').val('');
                $('#dvMenu_General #txtEmail').val('');
                $('#dvMenu_General #txtCalle').val('');
                $('#dvMenu_General #txtTelefono').val('');
            
                $('#tbRFCMultiples').css('display','none');
                $('#btnRFCCrearNuevo').css('display','none');

                $('#dvMenu_General #txtNombre').attr('readonly',false);
                $('#dvMenu_General #txtContacto').attr('readonly',false);
                $('#dvMenu_General #txtEmail').attr('readonly',false);
                $('#dvMenu_General #txtCalle').attr('readonly',false);
                $('#dvMenu_General #txtTelefono').attr('readonly',false);                                       
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#cmbRFCs').on('change',function(){
		        objSelected = $('#cmbRFCs').find("option:selected");
                IdE =  $('#cmbRFCs').find("option:selected").val();						
                Cte =  objSelected.data('id_cte');
                Nomcomercial = objSelected.data('cte_nomcomercial');                
                Contacto = objSelected.data('cte_contacto');
                Email = objSelected.data('cte_email');
                Calle = objSelected.data('cte_calle');
                Telefono =  objSelected.data('cte_telefono');  
                
                //$('#dvMenu_General #icnRFCComprobado').hide();
                //$('#dvMenu_General #lblMensajeRFC').show();
                $('#hdnId_Cte').val(Cte);
                $('#dvMenu_General #hdnId_Cte').val(Cte);
                $('#dvMenu_General #txtNombre').val(Nomcomercial);
                $('#dvMenu_General #txtContacto').val(Contacto);
                $('#dvMenu_General #txtEmail').val(Email);
                $('#dvMenu_General #txtCalle').val(Calle);
                $('#dvMenu_General #txtTelefono').val(Telefono);
	        });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
            $('#btnBusquedaDeCatalogo').click( function () {  
                btnNuevoProspecto();
                $('#lbBuscarCliente_RegEncontrados').text('En consulta de catálogo los datos son de solo lectura.'); 
                $('#tbBuscarCliente_Texto').val('');      
                $('#tbBuscarCliente_Listado > tbody').empty();                
                $('#modalBuscarCliente').modal('show');
            });
            
         /*/
         $('#dvModalEditarProspecto').on('show.bs.modal', function (event) {
            console.log('471');
            var trigger = $(event.relatedTarget);
            var idCrmProspecto = trigger.data('idcrmprospecto');
            _indiceProspectoAActualizar = trigger.data('rowidx');
            _datosProspectoAActualizar = $('#tblProspectos').DataTable().row(_indiceProspectoAActualizar).data();

            limpiarFormaEditarProspecto();
            console.log('3130');
            cargarCamposDialogoEditarProspecto(idCrmProspecto);

            cargarDescripcion(_indiceProspectoAActualizar);

        });
        */

        $('#dvModalEditarProspecto').on('hide.bs.modal', function (event) {
            $('#frmDvModalEditarProspecto').validate().resetForm();
        });


    });
