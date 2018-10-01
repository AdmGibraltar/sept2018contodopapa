
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Encabezado
function Cargar_PropuestaTecnoEconomicaEnc(CRM_Usuario_Rik, Id_Op, Id_Cte, Id_Val, CALLBACK) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomicaEnc/?CRM_Usuario_Rik='+CRM_Usuario_Rik+'&Enc=0&Id_Op=' + Id_Op + '&Id_Cte=' + Id_Cte + '&Id_Val=' + Id_Val,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        //Export_Excel_Informe1(response);
        //console.log(response);
        lst = response;
        var Vap_Estatus = lst.Vap_Estatus;
        var Vap_Estatus2 = lst.Vap_Estatus2;

        if (CALLBACK) {
            CALLBACK(Vap_Estatus, Vap_Estatus2);
        }

    }).fail(function (jqXHR, textStatus, error) {              
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomicaEnc.');       
        }                
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Detalle
function Cargar_PropuestaTecnoEconomica(CRM_Usuario_Rik, Id_Op, Id_Cte, Id_Val, CALLBACK) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomica/?CRM_Usuario_Rik='+CRM_Usuario_Rik+'&Id_Op=' + Id_Op + '&Id_Cte=' + Id_Cte + '&Id_Val=' + Id_Val,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        //Export_Excel_Informe1(response);
        //console.log(response);
        lst = response;

        if (CALLBACK) {
            CALLBACK();
        }

    }).fail(function (jqXHR, textStatus, error) {        
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomica.');
        }                
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Update_OportunidadesProductos(
    Id_Op, Id_Val, Id_Cte, Id_Prd, Cantidad, 
    AplDilucion, DilucionA, DilucionC, CPT_ProductoActual, CPT_SituacionActual, 
    CPT_VentajasKey, CPT_RecursoImagenProductoActual, CPT_RecursoImagensolucionKey, 
    COP_CostoEnUso, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomica/?'+
                    'Id_Op=' + Id_Op + 
                    '&Id_Val=' + Id_Val + 
                    '&Id_Cte=' + Id_Cte +
                    '&Id_Prd=' + Id_Prd + 
                    '&Cantidad=' + Cantidad +
                    '&AplDilucion=' + AplDilucion + 
                    '&DilucionA=' + DilucionA + 
                    '&DilucionC=' + DilucionC +
                    '&CPT_ProductoActual=' + CPT_ProductoActual + 
                    '&CPT_SituacionActual=' + CPT_SituacionActual +
                    '&CPT_VentajasKey=' + CPT_VentajasKey + 
                    '&CPT_RecursoImagenProductoActual=' + CPT_RecursoImagenProductoActual + 
                    '&CPT_RecursoImagensolucionKey=' + CPT_RecursoImagensolucionKey +
                    '&COP_CostoEnUso=' + COP_CostoEnUso,
    cache: false,
    type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        if (CALLBACK) {
            CALLBACK();
        }

    }).fail(function (jqXHR, textStatus, error) {
         if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {            
            alertify.error('Ocurrió una error: funcion Update_OportunidadesProductos.');
        } 
    });
}
       
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\       
function crearRecursoArchivoUsandoFormData (Op, Archivo, idRep, status) {    
    var file_data = $('#file').prop('files')[0];   
    var form_data = new FormData();                  
    form_data.append('file', file_data);

    
    $.ajax({        
        url: _ApplicationUrl +'/api/RepositorioCrearRecursoArchivo?IdDocento=99&IdTipoDoc=88',          
        type: 'POST',
        cache: false,
        contentType: false, //'multipart/form-data',
        data: form_data,
        processData: false,
        statusCode: status,
        beforeSend: function (jqXHR, settings) {
            /*if(jqXHR.upload){
                jqXHR.upload.addEventListener('progress', $.proxy(_this._actualizarBarraProgresoTransferencia, _this), false);
            }*/
            //alert(url);
        },
    }).done(function (response, textStatus, jqXHR) {

        var Estatus = response.Estatus;
        var Hash = response.Hash;

        if (Estatus==1) {
            var Contenedor = $('#modalCargaImagen_Contenedor').val();
            $(Contenedor).attr("src", _ApplicationUrl +'/imgupload/'+ Hash);        
            $('#modalCargaRecurso').modal('hide');
        } else {
            alertify.error('Error al cargar la imagen.');
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
       if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');            
            $('#modalCargaRecurso').modal('hide');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {            
            alertify.error('Ocurrió una error: funcion crearRecursoArchivoUsandoFormData.');
        } 
    }).always(function (jqXHR, textStatus, errorThrown) {
        console.log(3);
    }).error(function(data, status){				    
        alertify.error('Ocurrió una error: funcion crearRecursoArchivoUsandoFormData.');
    });

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function aceptarPropuestaTecnoEconomica(idVal) {

    PatternflyToast.showProgress('Generando ACYS...');
    $.ajax({
        url: _ApplicationUrl  + '/api/AceptarPropuestaTecnoEconomica/?idVal=' + idVal,
        type: 'GET',
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(aceptarPropuestaTecnoEconomica, null, idVal);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        
        //PatternflyToast.hideProgress();
        /*
        if (_acysGeneradoExitosamenteCallback != null) {
            _acysGeneradoExitosamenteCallback(response);
        } else {
            PatternflyToast.showSuccess('El ACYS con folio ' + response + ' de la propuesta ha sido generado satisfactoriamente', 8000);
        }
        */

        $('#dvModalPropuestaTE_ver2').modal('hide');
        PatternflyToast.showSuccess('El ACYS con folio ' + response + ' de la propuesta ha sido generado satisfactoriamente', 8000);

        $(_tablaProyectos.table().container()).block({ message: '<img src=\'<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>\' />Actualizando' });
        _tablaProyectos.ajax.reload(function () {
            $(_tablaProyectos.table().container()).unblock();
        });

        
    }).fail(function (jqXHR, textStatus, errorThrown) {
       /* switch (jqXHR.status) {
            case 401:
                alert('La sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                PatternflyToast.hideProgress();
                PatternflyToast.showError(jqXHR.responseJSON.Message, 8000);
                break;
        }*/
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {            
            alertify.error('Ocurrió una error: funcion aceptarPropuestaTecnoEconomica.');
        } 

    }).always(function (jqXHR, textStatus, errorThrown) {        
        console.log(3);
    }).error(function(data, status){				    
        alertify.error('Ocurrió una error: funcion aceptarPropuestaTecnoEconomica.');
    });

}


                  
  
  