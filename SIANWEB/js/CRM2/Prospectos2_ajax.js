function Consultar_PorId_Cte(Id_Cte, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatCliente/Buscar_CteById?Id_Cte=' + Id_Cte,
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
        var Datos = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {
            CALLBACK(Datos);
        }

        /*
        if (Estado == 1) {
        if (Datos.length > 0) {
        var RegistrosEncontrados = Datos[0].RegistrosEcontrados;
        $('#lbBuscarCliente_RegEncontrados').text(RegistrosEncontrados + ' Registros encontrados.');
        $('#tbBuscarCliente_Listado > tbody').empty();

        for (var i = 0; i < Datos.length; i++) {

        var row = $('<tr>');
        row.append($('<td>').append(
        Datos[i].Cte
        ));
        row.append($('<td>').append(
        Datos[i].RFC
        ));
        row.append($('<td>').append(
        Datos[i].NomComercial
        ));
        row.append($('<td>').append(
        '<button ' +
        ' data-id_cte="' + Datos[i].Cte + '" ' +
        ' onclick="btnBuscarCliente_Aplicar(this);" ' +
        'class="button">' +
        '<span>Abrir</span>' +
        '</button>'
        ));
        $('#tbBuscarCliente_Listado tbody').append(row);
        $('#chbCte_' + i).iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
        });
        }
        } else {
        // no se encontro informacion 
        $('#tbBuscarCliente_Listado > tbody').empty();
        $('#lbBuscarCliente_RegEncontrados').text('No se encontraron registro.');
        }
        }
        */

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
    }).always(function (jqXHR, textStatus, errorThrown) {
        //$('#' + parent + ' #imgRFCEnOperacion').hide();
    });
    //$('#spinnerBuscar').css('display', 'none');
    //  
}  

function BuscarCliente_wcb(TextoBuscar) {
    $('#spinnerBuscar').css('display','block');
    $.ajax({
        url: _ApplicationUrl + '/api/CatCliente/Buscar?TextoBuscar=' + TextoBuscar,
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
        var Datos = response.Datos;
        var Estado = response.Estado;
        $('#spinnerBuscar').css('display', 'none');
        if (Estado == 1) {
            if (Datos.length > 0) {
                var RegistrosEncontrados = Datos[0].RegistrosEcontrados;
                $('#lbBuscarCliente_RegEncontrados').text(RegistrosEncontrados + ' Registros encontrados.');
                $('#tbBuscarCliente_Listado > tbody').empty();                

                for (var i = 0; i < Datos.length; i++) {

                    var row = $('<tr>');
                    row.append($('<td>').append(
						Datos[i].Cte
        			));
                    row.append($('<td>').append(
						Datos[i].RFC
        			));
                    row.append($('<td>').append(
						Datos[i].NomComercial
        			));
                    row.append($('<td>').append(
                         '<button ' +
                         ' data-id_cte="' + Datos[i].Cte + '" '+
                         ' onclick="btnBuscarCliente_Aplicar(this);" ' +
                         'class="button">'+
                         '<span>Abrir</span>'+
                         '</button>'
        			));
                    $('#tbBuscarCliente_Listado tbody').append(row);
                    $('#chbCte_' + i).iCheck({
                        checkboxClass: 'icheckbox_square-blue',
                        radioClass: 'iradio_square-blue'
                    });
                }
            } else {
                // no se encontro informacion 
                $('#tbBuscarCliente_Listado > tbody').empty();                
                $('#lbBuscarCliente_RegEncontrados').text('No se encontraron registro.');
            }
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        $('#spinnerBuscar').css('display', 'none');
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
        //$('#' + parent + ' #imgRFCEnOperacion').hide();
    });
    //$('#spinnerBuscar').css('display', 'none');
    //  
               
}
