

function Territorios(idRik, idProspecto, CallBack) {
    var res = [];
    $.ajax({
        //url: '<%=ApplicationUrl %>' + '/api/ProspectoTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
        url: _ApplicationUrl + '/api/ProspectoTerritorio/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
        cache: false,
        type: 'GET',        
    }).done(function (response, textStatus, jqXHR) {        
        //res = response;
          $.each(response, function (index, element) {            
            obj = {};
            obj.IdTerritorio = element.Id_Ter;
            obj.TerritorioNombre = element.Ter_Nombre;
            res.push(obj);
        });

        if (res.length>0) {
            //ejecuta callback
            CallBack();
        }
        if (res.length<=0) {
            alertify.error('No ha realizado la asociación de territorios.');
        }

    }).fail(function (jqXHR, textStatus, error) {        
    }).always(function (jqXHR, textStatus, errorThrown) {        
    });    
}
         
         
         
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// territorios por RIK
function ObtenerTerritorios_PorRik() {

    var TerrLst = [];

    $.ajax({            
            url: _ApplicationUrl + '/api/CatTerritorio?X=1&Y=1',
            type: 'GET',
            cache: false,
            async:false,
          /*  statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(cargarTerritorios, this, $, dvTerritoriosElement, jqElement, onSuccess, onFailure);
                }
            }*/
    }).done(function (response, textStatus, jqXHR) {
            //if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            if (response != null) {
                TerrLst  = response ;
            }
            /*
                if (response != null) {
                    //onSuccess(response);
                    if (CALLBACK) {
                        CALLBACK(response);
                    }                
                }*/

    }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
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
            if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                if (onFailure != null)
                    onFailure($);
            }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            /*
            if (typeof (always) != undefined && typeof (always) != 'undefined') {
                if (always != null)
                    always(jqXHROrData);
            */
    });  
    
    return TerrLst;  
}     
