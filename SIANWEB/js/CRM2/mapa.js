var _mapa = null;
var _modalGoogleMapsCallback = null;
var _ultimoMarcadorMapa = null;

var _posicionCentroMonterrey = {
    lat: 25.712978,
    lng: -100.305573
};



function elegirUbicacion() {
    if (_modalGoogleMapsCallback != null) {
        _modalGoogleMapsCallback(_ultimoMarcadorMapa.getPosition().lat(), _ultimoMarcadorMapa.getPosition().lng());
    }
    $('#dvModalMapaGoogle').modal('hide');
}

//MAPA
function inicializarMapa(posicion) {
    _mapa = new google.maps.Map(document.getElementById('dvModalMap'), {
        center: posicion, //{ lat: -34.397, lng: 150.644 },
        zoom: 2
    });

    _mapa.addListener('click', function (event) {
        if (_ultimoMarcadorMapa != null) {
            _ultimoMarcadorMapa.setMap(null);
        }

        _ultimoMarcadorMapa = new google.maps.Marker({
            position: event.latLng,
            map: _mapa
        });
    });
}

//MAPA
function initMap() {

    var mapPosition = null;
    //Se obtiene la posicion del usuario
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            mapPosition = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            inicializarMapa(mapPosition);
        }, function () {
            mapPosition = _posicionCentroMonterrey;
            inicializarMapa(mapPosition);
        });
    } else {
        mapPosition = _posicionCentroMonterrey;
        inicializarMapa(mapPosition);
    }

    //            _mapa = new google.maps.Map(document.getElementById('dvModalMap'), {
    //                center: mapPosition,//{ lat: -34.397, lng: 150.644 },
    //                zoom: -2
    //            });
    //            
    //            _mapa.addListener('click', function (event) {
    //                if (_ultimoMarcadorMapa != null) {
    //                    _ultimoMarcadorMapa.setMap(null);
    //                }

    //                _ultimoMarcadorMapa = new google.maps.Marker({
    //                    position: event.latLng,
    //                    map: _mapa
    //                });
    //            });
}


// Mapa
function mapaNegocioUbicacionSeleccionada(lat, lng) {
    $('#hdnUbicacionNegocioLat').val(lat);
    $('#hdnUbicacionNegocioLng').val(lng);
}

// Mapa
function mapaHogarUbicacionSeleccionada(lat, lng) {
    $('#hdnUbicacionHogarLat').val(lat);
    $('#hdnUbicacionHogarLng').val(lng);
}

// Mapa
function mapaCalleProspectoUbicacionSeleccionada(lat, lng) {
    $('#hdnUbicacionCalleProspectoLat').val(lat);
    $('#hdnUbicacionCalleProspectoLng').val(lng);
}

// Mapa
function mapaCalleProspectoUbicacionSeleccionada_(lat, lng) {
    //request street name
    //
    $('#dvModalNuevoProspecto #txtCalle').val('Obteniendo la dirección...');
    $.ajax({
        url: 'https://maps.googleapis.com/maps/api/geocode/json?latlng=' + lat + ',' + lng + '&key=AIzaSyCAH5iBFTeLd9n1MvKL0z5RiDJcQVSFNnM',
        type: 'GET',
        cache: false
    }).done(function (response, textStatus, jqXHR) {
        $('#dvModalNuevoProspecto #txtCalle').val(response.results[0].formatted_address);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        $('#dvModalNuevoProspecto #txtCalle').val('Se presentó una complicación al obtener la dirección.');
    });
}
