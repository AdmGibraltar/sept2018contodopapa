
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function ModalListadoContactosShow(e) {
    var Id_CteDet = $(e).data('id_ctedet');
    var Id_Ter = $(e).data('id_ter');
    var Id_Seg = $(e).data('id_seg');

    $('#hfDatosGenerales_Id_CteDet').val(Id_CteDet);
    $('#hfDatosGenerales_Id_Ter').val(Id_Ter);
    $('#hfDatosGenerales_Id_Seg').val(Id_Seg);

    CargaListadoContactos();

    $('#ModalContactos').modal('show');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnContactoModificar(sender) {
    var Id_Emp = $(sender).data('id_emp');
    var Id_Cd = $(sender).data('id_cd');
    var Id_Cte = $(sender).data('id_cte');
    var Id_Ter = $(sender).data('id_ter');
    var Id_Consecutivo = $(sender).data('id_consecutivo');
    $('#hdContacto_Id_Emp').val(Id_Emp);
    $('#hdContacto_Id_Cd').val(Id_Cd);
    $('#hdContacto_Id_Cte').val(Id_Cte);
    $('#hdContacto_Id_Ter').val(Id_Ter);
    $('#hdContacto_Id_Consecutivo').val(Id_Consecutivo);

    $.ajax({
        url: _ApplicationUrl + '/api/CatClienteDetContacto/?IdEmp=' + Id_Emp + '&IdCd=' + Id_Cd + '&IdCte=' + Id_Cte + '&IdTer=' + Id_Ter + '&IdConsecutivo=' + Id_Consecutivo,
        type: 'GET',
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                //_onLoginSuccessful = $.proxy(cargarTerritoriosAsociadosAProspecto, null, idCliente, onSuccess, onFailure, always);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
        }

        $('#hdContacto_Id_Emp').val(response.Id_Emp);
        $('#hdContacto_Id_Cd').val(response.Id_Cd);
        $('#hdContacto_Id_Cte').val(response.Id_Cte);
        $('#hdContacto_Id_Ter').val(response.Id_Ter);
        $('#hdContacto_Id_Consecutivo').val(response.Id_Consecutivo);
        $('#txtContactoNombre').val(response.Nombre);
        $('#txtContactoPuesto').val(response.Puesto);
        if (response.Cumpleanios == '1900-01-01') {
            $('#txtContactoCumple').val('');
        } else {
            $('#txtContactoCumple').val(response.Cumpleanios);
        }
        $('#txtContactoCorreo').val(response.Correo);
        $('#txtContactoDireccion1').val(response.Direccion1);
        $('#txtContactoDireccion2').val(response.Direccion2);
        $('#txtContactoTelNegocio').val(response.TelNegocio);
        $('#txtContactoTelCasa').val(response.TelCasa);
        $('#ModalContactos').modal('hide');
        $('#modalContacto').modal('show');

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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnContactoGuardar() {
    var data = {
        Id_Emp: $('#hfDatosGenerales_Id_Emp').val(),
        Id_Cd: $('#hfDatosGenerales_Id_Cd').val(),
        Id_Cte: $('#hfDatosGenerales_Id_Cte').val(),
        Id_CteDet: '',
        Id_Ter: $('#hfDatosGenerales_Id_Ter').val(),
        Id_Seg: '',
        Nombre: $('#txtContactoNombre').val(),
        Puesto: $('#txtContactoPuesto').val(),
        Cumpleanios : $('#txtContactoCumple').val(),
        Correo: $('#txtContactoCorreo').val(),
        Direccion1: $('#txtContactoDireccion1').val(),
        Direccion2: $('#txtContactoDireccion2').val(),
        TelNegocio: $('#txtContactoTelNegocio').val(),
        TelCasa: $('#txtContactoTelCasa').val(),                
        Id_Consecutivo: $('#hdContacto_Id_Consecutivo').val(),                
    }
    $.ajax({        
        url: _ApplicationUrl + '/api/CatClienteDetContacto/',
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                //CargaListadoContactos();
                //_onLoginSuccessful = $.proxy(asociarTerritorioACliente, null, idCliente, idTer, idRik, idSeg, vpo, onSuccess, onFailure, always);
            }
        }
    }).success(function (response) {

        $('#modalContacto').modal('hide');
        CargaListadoContactos('');
        $('#ModalContactos').modal('show');               
                            
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }
        //ModalListadoContactosShow();
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnContactoCancelar() {
    $('#modalContacto').modal('hide');
    ModalListadoContactosShow();            
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnContactoEliminar(sender) {
} 

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Contactos - Despliega el modal de listado de contactos
function CargaListadoContactos() {
    Id_Emp = $('#hfDatosGenerales_Id_Emp').val();
    Id_Cd = $('#hfDatosGenerales_Id_Cd').val();
    Id_Cte = $('#hfDatosGenerales_Id_Cte').val();
    Id_Ter = $('#hfDatosGenerales_Id_Ter').val();
    Id_Ter = Id_Ter.trim();

    if (Id_Ter !="") {        
        $.ajax({
                url: _ApplicationUrl + '/api/CatClienteDetContacto/?IdEmp=' + Id_Emp + '&IdCd=' + Id_Cd + '&IdCte=' + Id_Cte+'&IdTer='+Id_Ter ,
                type: 'GET',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(CargaListadoContactos);
                    }
                }
            }).success (function (response){            
                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    onSuccess(response);
                }
            }).done(function (response, textStatus, jqXHR) {
                /*if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    onSuccess(response);
                }*/
                var tabletbody = $('#tblListadoContatos tbody');
                $(tabletbody).find('tr').remove();
                $.each(response, function (index, element) {
                    //_territorioAsociadosAProspecto.push(element);
                    var newRow = $('<tr id="' + element.Id_Ter + '">' +
                    '<td>' + element.Nombre + '</td>' +
                    '<td>' + element.TelNegocio+', '+element.TelCasa +'</td>' +
                    '<td style="text-align: center">' +
                        '<button onclick="btnContactoModificar(this)" class="btn btn-primary"' +
                            ' data-id_emp ='+element.Id_Emp+' '+
                            ' data-id_cd =' + element.Id_Cd + ' ' +
                            ' data-id_cte =' + element.Id_Cte + ' ' +
                            ' data-id_ter =' + element.Id_Ter + ' ' +
                            ' data-id_consecutivo =' + element.Id_Consecutivo + ' >' +
                            '<i class="fa fa-pencil-square-o"></i>' +
                        '</button>&nbsp;' +
                        '<button type="button" onclick="btnContactoEliminar(this)" class="btn btn-primary"' +
                            ' data-id_emp ='+element.Id_Emp+' '+
                            ' data-id_cd =' + element.Id_Cd + ' ' +
                            ' data-id_cte =' + element.Id_Cte + ' ' +
                            ' data-id_ter =' + element.Id_Ter + ' ' +
                            ' data-id_consecutivo =' + element.Id_Consecutivo + ' >' +                        
                            '<i class="fa fa-times"></i>'+
                        '</button>' +
                    '</td>' +                    
                    '</tr>');
                    $(tabletbody).append(newRow);
                });
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
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
function CargaListadoContactos_Succeeded (response) {          
}
        