///<reference path="~/js/crm/ui/uiDeteccionCaracteristicas.js" />
///<reference path="~/js/crm/com/comDeteccionCaracteristicas.js" />
///<reference path="~/js/crm/io/ioDeteccionCaracteristicas.js" />

crm.ui.DialogoRecurso = function (opciones) {
    this._$elemento = opciones.elemento;
    this._$areaArrastre = opciones.areaArrastre;
    this._archivosLocalesCargados = [];
    this._alAceptar = opciones.alAceptar;
    this._$comandoAceptar = opciones.comandoAceptar;
    this._recursosElegidos = [];
    this._currentIdRep = opciones.idRep;
    this._$comandoArchivos = opciones.comandoArchivos;
    this._$elementoBarraProgresoTransferenciaArchivo=opciones.elementoBarraProgresoTransferenciaArchivo;
    this._$elementoMenuNavegacionNuevoRecurso=opciones.elementoMenuNavegacionNuevoRecurso;
    this._$elementoMenuNavegacionRecursoExistente=opciones.elementoMenuNavegacionRecursoExistente;
    this._$elementoAcordeonURL=opciones.elementoAcordeonURL;
    this._$elementoAcordeonArchivo=opciones.elementoAcordeonArchivo;

    this._$elementoCampoURL = opciones.elementoCampoUrl;
};

crm.ui.DialogoRecurso.prototype._crearRecursoURLOnSuccess = function (response) {
    this._recursosElegidos=[];
    this._recursosElegidos.push({
        url: response.url,
        idRecurso: response.idRecurso
    });
    $(this._$elemento).modal('hide');
    this._alAceptar(this._recursosElegidos);
};

crm.ui.DialogoRecurso.prototype._crearRecursoURLOnFailure = function () {
    //Diseñar la notificación de falla
    //Por lo pronto no se cierra el diálogo
};

crm.ui.DialogoRecurso.prototype._crearRecursoURL = function (url, idRep, onSuccess, onFailure, always, status) {
    var data = {
        Url: url,
        IdBibliotecaNodoPadre: idRep
    };
    $.ajax({
        url: sysAplicacion.get_urlAplicacion() + crm.ui.DialogoRecurso._crearRecursoURLControladorURL,
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        statusCode: status
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
};

crm.ui.DialogoRecurso.prototype._crearRecursoArchivoUsandoIFrame = function (archivo, idRep, onSuccess, onFailure, always, status) {
    
};

crm.ui.DialogoRecurso.prototype._inicializarBarraProgresoTransferencia=function(){
    this._$elementoBarraProgresoTransferenciaArchivo.css('width', '0%');
    this._$elementoBarraProgresoTransferenciaArchivo.find('span').text('0% completado');
    this._$elementoBarraProgresoTransferenciaArchivo.show();
};

crm.ui.DialogoRecurso.prototype._actualizarBarraProgresoTransferencia=function(e){
    var progreso=Math.ceil(e.loaded/e.total*100);
    this._$elementoBarraProgresoTransferenciaArchivo.css('width', progreso + '%');
    this._$elementoBarraProgresoTransferenciaArchivo.find('span').text( progreso + '% completado');
};

crm.ui.DialogoRecurso.prototype._crearRecursoArchivoUsandoFormData = function (archivo, idRep, onSuccess, onFailure, always, status) {
    var data = new FormData();
    data.append('archivo', archivo);
    var _this=this;
    this._inicializarBarraProgresoTransferencia();
    $.ajax({
        url: sysAplicacion.get_urlAplicacion() + crm.ui.DialogoRecurso._crearRecursoArchivoControladorURL + '?idBiblioNodoPadre=' + this._currentIdRep,
        type: 'POST',
        cache: false,
        contentType: false,//'multipart/form-data',
        data: data,
        processData: false,
        statusCode: status,
        beforeSend: function (jqXHR, settings) {
            if(jqXHR.upload){
                jqXHR.upload.addEventListener('progress', $.proxy(_this._actualizarBarraProgresoTransferencia, _this), false);
            }
        },
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        _this._$elementoBarraProgresoTransferenciaArchivo.hide();
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
};

crm.ui.DialogoRecurso.prototype._crearRecursoArchivoOnSuccess=function(response){
    this._recursosElegidos=[];
    this._recursosElegidos.push({
        url: response.url,
        idRecurso: response.idRecurso
    });
    $(this._$elemento).modal('hide');
    this._alAceptar(this._recursosElegidos);
};

crm.ui.DialogoRecurso.prototype._crearRecursoArchivoOnFailure=function(){
    
};

crm.ui.DialogoRecurso.prototype._crearRecursoArchivo = function (archivo, idRep, onSuccess, onFailure, always, status) {
    //Revisar por el soporte de FormData para ejecutar la operación mediante jqXHR;
    //En caso de no soportar la versión de FormData, utilizar la operación de transmisión usando iframe
    var archivo = this._archivosLocalesCargados[0];
    //Inicializar y mostrar la notificación de progreso
    if (comDeteccionCaracteristicas.get_SoportaFormData() == true) {
        //Soporta FormData
        this._crearRecursoArchivoUsandoFormData(archivo, idRep, onSuccess, onFailure, always, status);
    } else {
        //usar iframe
        this._crearRecursoArchivoUsandoIFrame(archivo, idRep, onSuccess, onFailure, always, status);
    }
};

crm.ui.DialogoRecurso.prototype.inicializar = function () {
    var _this = this;

    this._$comandoArchivos.on('change', function () {
        _this._archivosLocalesCargados = [];
        if (this.files[0].size < 1024000) {
            _this._archivosLocalesCargados.push(this.files[0]);
            
        }else{
            //muestra una notificación avisando que el peso máximo del archivo no debe exceder 1 mb.
        }
    });

    this._$comandoAceptar.click(function () {
        //evaluar que apartado de encuentra disponible: en caso de ser el apartado de nuevo recurso, evaluar si se encuentra desplegado el sub apartado de URL;
        //1. Apartado de URL desplegado: llamar al servicio de creación de recursos de URL, pasando como parámetro el nivel del nodo en el que se encuentra la URL y el valor del campo de enlace.
        //2. Apartado de nueva imagen: llamar al servicio de creación de recursos de imagen, transmitiendo el valor del nivel del nodo de la librería en donde se desea crear la nueva imagen como parámetro de la cadena de petición, y transmitiendo la imagen como parte de una instancia FormData.

        var bEsURL = false;
        var bEsRecursoNuevo = false;

        bEsRecursoNuevo = _this._$elementoMenuNavegacionNuevoRecurso.hasClass('active');

        if (bEsRecursoNuevo == true) {
            bEsURL=!_this._$elementoAcordeonURL.hasClass('collapsed');
            if (bEsURL == true) {
                var url = _this._$elementoCampoURL.val();
                if (url.length == 0) {
                    //Diseñar la notificación de validación de campo vacío para el campo URL
                    return;
                }
                _this._crearRecursoURL(url, _this._currentIdRep, $.proxy(_this._crearRecursoURLOnSuccess, _this), $.proxy(_this._crearRecursoURLOnFailure, _this), function () { },
                {
                    401: function () {
                        //Diseñar el componente de inicio de sesión y llamarle aquí......o crear un componente en versión estandard para el manejo de 
                    }
                });
            } else {
                //Es un archivo

                _this._crearRecursoArchivo(_this._archivosLocalesCargados[0], _this._currentIdRep, $.proxy(_this._crearRecursoArchivoOnSuccess, _this), function () { }, function(){},
                {
                    401: function () {
                    }
                });
            }
        } else {
            //recurso existente
        }

        
    });
    if (uiDeteccionCaracteristicas.get_soportaArrastrarYColocar() == true) {
        //soporta arrastrar y colocar: configurar la zona de arrastre.
        if (ioDeteccionCaracteristicas.get_SoportaFileReader() == true) {
            this._$areaArrastre.addClass('has-advanced-upload');

            this._$areaArrastre.on('drag dragstart dragend dragover dragenter dragleave drop', function (e) {
                e.preventDefault();
                e.stopPropagation();
            }).on('dragover dragenter', function () {
                this._$areaArrastre.addClass('is-dragover');
            }).on('dragleave dragend drop', function () {
                this._$areaArrastre.removeClass('is-dragover');
            }).on('drop', function (e) {
                this._archivosLocalesCargados = e.originalEvent.dataTransfer.files;
            });
        }
    }
};

crm.ui.DialogoRecurso.prototype.cambiarRepositorio=function(idRep){
    this._currentIdRep=idRep;
};

crm.ui.DialogoRecurso.prototype.mostrar = function () {
    $(this._$elemento).modal('show');
};