<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAsociacionProspectoTerritorio_js.ascx.cs" Inherits="SIANWEB.js.UCAsociacionProspectoTerritorio_js" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/UCContactos.ascx" TagPrefix="uc" TagName="UCContactos" %>
<script src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.min.js") %>"></script>
<uc:UCContactos runat="server" ID="ucUCContactos" />
<script type="text/javascript">
    ///<reference path="http://code.jquery.com/jquery-2.1.4.min.js"/>
    (function ($) {

        function ModeloSelectPickerSelectorTerritorio(element, opts){
            this._selector=element;

            if(opts.tipoVista==$.fn.asociacionprospectoterritorio.TIPOSVISTA.EXTENDIDO){
                $(this._selector).data('width', 'auto');
            }

            $(this._selector).selectpicker();
        }

        ModeloSelectPickerSelectorTerritorio.prototype.reestablecer=function(){
            $(this._selector).find('option').remove();
            var _this=this;
            $.each($.fn.asociacionprospectoterritorio._territoriosRIK, function(index, element){
                _this.agregarTerritorio(element);
            });

            $(this._selector).selectpicker('val', 0);
            $(this._selector).selectpicker('refresh');
        };

        ModeloSelectPickerSelectorTerritorio.prototype.get_TerritorioSeleccionado=function(){
            return $(this._selector).selectpicker('val');
        };

        ModeloSelectPickerSelectorTerritorio.prototype.set_selectedValue=function(val){
            $(this._selector).selectpicker('val', val);
        };

        ModeloSelectPickerSelectorTerritorio.prototype.refresh=function(){
            $(this._selector).selectpicker('refresh');
        };

        ModeloSelectPickerSelectorTerritorio.prototype.removeElementByValue=function(val){
            $(this._selector).find('option[value="' + val + '"]').remove();
        };

        ModeloSelectPickerSelectorTerritorio.prototype.agregarTerritorio=function(objTer){
            $(this._selector).append('<option value="' + objTer.Id_Ter + '">' + objTer.Id_Ter + ' - ' + objTer.Ter_Nombre + '</option>');
        };

        function ModeloTablaListadoAsociaciones(element, opts, componenteContactos, componenteAsociaciones){
            this._element=element;
            this._insertfn=$.proxy(ModeloTablaListadoAsociaciones.prototype._insertarModoTemporal, this);
            this._removefn=$.proxy(ModeloTablaListadoAsociaciones.prototype._removerTerritorioTemporal, this);
            this._opciones = $.extend({}, ModeloTablaListadoAsociaciones.defaults, opts);
            this._componenteContactos=componenteContactos;
            this._componenteAsociaciones=componenteAsociaciones;
        }

        ModeloTablaListadoAsociaciones.prototype.limpiar=function(){
            $(this._element).find('tbody tr').remove();
        };

        ModeloTablaListadoAsociaciones.prototype._removerTerritorioPersistente=function(idTer){
            var $this=this;
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatClienteDet/?idCte=' + this._opciones.idCte + '&idTer=' + idTer,
                type: 'DELETE',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(ModeloTablaListadoAsociaciones.prototype._removerTerritorioPersistente, $this, idTer);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    onSuccess(response);
                }
                $this._removerTerritorioTemporal(idTer);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        $(this).modal('hide');
                        /*
                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.ExceptionMessage);
                        $('#toastDanger').fadeIn();
                        setTimeout(function () {
                            $('#toastDanger').fadeOut();
                        }, 10000);
                        */
                        alertify.error(jqXHR.responseJSON.ExceptionMessage);
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
        };

        ModeloTablaListadoAsociaciones.prototype._removerTerritorioTemporal=function(idTer){
            $(this._element).find('tbody tr[id="' + idTer + '"]').remove();
            this._opciones.alRemoverTerritorio(idTer);
        };

        ModeloTablaListadoAsociaciones.prototype.removerTerritorio=function(idTer){
            this._removefn(idTer);
        };

        //
        //Territorios
        // 1)
        //
        ModeloTablaListadoAsociaciones.prototype._insertarModoPersistente=function(objTer){
        
            var $this=this;
            var data={
                IdCte: this._opciones.idCte, 
                IdRik: this._opciones.idRik, 
                IdTer: objTer.Id_Ter, 
                IdSeg: objTer.Id_Seg, 
                VPO: objTer.Cte_Potencial
            };

            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatClienteDet/',
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
                var tabletbody=$($this._element).find('tbody');
                // Arma el renglon 
                var newRow=$('<tr id="' + response.Id_Ter + '">'+
                '<td>' + response.Id_Ter + 
                    '<input type="hidden" id="hdnId_Ter" name="TerritoriosAsociados" value="' + response.Id_Ter + '"/>'+
                '</td>'+
                '<td> ' + response.CatTerritorioSerializable.Ter_Nombre +  ' </td>'+
                '<td style="text-align: center; vertical-align:middle;">' + response.Cte_Potencial + 
                    '<div style="display: none;">'+
                        '<input type="text" id="txtCte_Potencial" name="Territorios" value="' + response.Cte_Potencial + '" />'+
                    '</div>'+
                    '</td>'+
                '<td style="text-align: center; vertical-align:middle;">'+
                    //'<a id="aContactos"><i class="fa fa-user-plus fa-2x" aria-hidden="true"></i></a>'+
                     '<a id="btnContactos" '+
                        'data-id_ter="'+objTer.Id_Ter+'" '+
                        'data-id_seg="'+objTer.Id_Seg+'" '+
                        'data-id_ctedet="0"  '+
                        'onclick="ModalListadoContactosShow(this);" >'+
                        '<i class="fa fa-user-plus fa-2x"></i>'+
                     '</a>'+
                '</td>'+
                //'<td style="text-align: center; vertical-align:middle;">'+
                    /*'<button class="btn btn-primary"><i class="fa fa-tasks"></i></button>'+*/
                //'</td>'+
                '<td style="text-align: center; vertical-align:middle;">'+
                    '<button id="btnRetirarTerritorio" type="button" data-terid="' + response.Id_Ter + '" class="btn btn-primary">'+
                        '<i class="fa fa-times"></i>'+
                    '</button>'+
                '</td></tr>');

                var $btnRetirarTerritorio=newRow.find('#btnRetirarTerritorio');
                $btnRetirarTerritorio.click($.proxy(ModeloTablaListadoAsociaciones.prototype._removerTerritorioPersistente, $this, response.Id_Ter));

                var $aContactos=newRow.find('#aContactos');
                $aContactos.click($.proxy(function(sender){
                    this._componenteAsociaciones.abrirContactos(response.Id_Cte, response);
                }, this, $aContactos));
                $(tabletbody).append(newRow);
            
                var $rows=tabletbody.find('tr');
                $.each($rows, function(index, element){
                    var $txtCte_Potencial=$(element).find('#txtCte_Potencial');
                    $($txtCte_Potencial).attr('name', 'TerritoriosAsociados[' + index + '].VPO');
                    var $hdnId_Ter=$(element).find('#hdnId_Ter');
                    $($hdnId_Ter).attr('name', 'TerritoriosAsociados[' + index + '].Id_Ter');
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

        };

        ModeloTablaListadoAsociaciones.prototype.set_modoPersistente=function(){
            this._insertfn=$.proxy(ModeloTablaListadoAsociaciones.prototype._insertarModoPersistente, this);
            this._removefn=$.proxy(ModeloTablaListadoAsociaciones.prototype._removerTerritorioPersistente, this);
        };

        // 
        // Prospectos / Seguimiento / 
        // 2) 
        //
        ModeloTablaListadoAsociaciones.prototype._insertarModoTemporal=function(objTer){
            var tabletbody=$(this._element).find('tbody');
            var vpoFormateado=numeral(objTer.Cte_Potencial).format('$0,0.00');
            var newRow=$('<tr id="' + objTer.Id_Ter + '">'+
            '<td>' + objTer.Id_Ter + '<input type="hidden" id="hdnId_Ter" name="TerritoriosAsociados" value="' + objTer.Id_Ter + '"/></td>'+
            '<td> ' + objTer.Ter_Nombre +  ' </td>'+
            '<td style="text-align: center; vertical-align:middle;">' + vpoFormateado + '<div style="display: none;">'+
                '<input type="text" id="txtCte_Potencial" name="Territorios" value="' + objTer.Cte_Potencial + '" /></div></td>'+
            '<td style="text-align: center; vertical-align:middle;">'+
                '<a id="aContactos">'+
                '<i class="fa fa-user-plus fa-2x" aria-hidden="true"></i></a>'+
            '</td>'+
            /*'<td style="text-align: center; vertical-align:middle;">'+
                '<button class="btn btn-primary">'+
                    '<i class="fa fa-tasks"></i>'+
                '</button>'+
            '</td>'+*/
            '<td style="text-align: center; vertical-align:middle;">'+
                '<button id="btnRetirarTerritorio" type="button" data-terid="' + objTer.Id_Ter + '" class="btn btn-primary">'+
                    '<i class="fa fa-times"></i>'+
                '</button>'+
            '</td>'+
            '</tr>');

            var $btnRetirarTerritorio=newRow.find('#btnRetirarTerritorio');
            $btnRetirarTerritorio.click($.proxy(ModeloTablaListadoAsociaciones.prototype.removerTerritorio, this, objTer.Id_Ter));
            var $aContactos=newRow.find('#aContactos');
            $aContactos.click($.proxy(function(sender){
                    this._componenteAsociaciones.abrirContactos(this._opciones.idCte, objTer);
                }, this, $aContactos));
            $(tabletbody).append(newRow);
            
            var $rows=tabletbody.find('tr');
            $.each($rows, function(index, element){
                var $txtCte_Potencial=$(element).find('#txtCte_Potencial');
                $($txtCte_Potencial).attr('name', 'TerritoriosAsociados[' + index + '].VPO');
                var $hdnId_Ter=$(element).find('#hdnId_Ter');
                $($hdnId_Ter).attr('name', 'TerritoriosAsociados[' + index + '].Id_Ter');
            });
        };

        ModeloTablaListadoAsociaciones.prototype.agregarTerritorio=function(objTer){
            this._insertfn(objTer);
        };


        // 
        // 
        // Prospecto / Territorios / Asociacion Territorios
        // Despliega el listado de territorios
        // 3)
        //
        ModeloTablaListadoAsociaciones.prototype.reset=function(src){
            
            var $this=this;
            var tabletbody=$(this._element).find('tbody');
            tabletbody.find('tr').remove();
            $.each(src, function(index, element){
                var vpoFormateado=numeral(element.Cte_Potencial).format('$0,0.00');
                var newRow=$('<tr id="' + element.Id_Ter + '">'+
                '<td>' + element.Id_Ter + '<input type="hidden" id="hdnId_Ter" name="TerritoriosAsociados" value="' + element.Id_Ter + '"/></td>'+
                '<td> ' + element.Ter_Nombre +  ' </td>'+
                '<td style="text-align: center; vertical-align:middle;">' + vpoFormateado + '<div style="display: none;">'+
                    '<input type="text" id="txtCte_Potencial" name="Territorios" value="' + element.Cte_Potencial + '" />'+
                '</div></td>'+
                '<td style="text-align: center; vertical-align:middle;">'+
                    //'<a id="aContactos"><i class="fa fa-user-plus fa-2x" aria-hidden="true"></i></a>'+
                    '<a id="btnContactos" '+
                        'data-id_ter="'+element.Id_Ter+'" '+
                        'data-id_seg="'+element.Id_Ter+'" '+
                        'data-id_ctedet="'+element.Id_Ter+'"  '+
                        'onclick="ModalListadoContactosShow(this);" >'+
                        '<i class="fa fa-user-plus fa-2x"></i>'+
                     '</a>'+
                 '</td>'+
                 /*'<td style="text-align: center; vertical-align:middle;">'+
                    '<button class="btn btn-primary"><i class="fa fa-tasks"></i></button>'+
                 '</td>'+*/
                 '<td style="text-align: center; vertical-align:middle;">'+
                    '<button id="btnRetirarTerritorio" type="button" data-terid="' + element.Id_Ter + '" class="btn btn-primary">'+
                        '<i class="fa fa-times"></i>'+
                    '</button>'+
                 '</td>'+
                 '</tr>');

                var $btnRetirarTerritorio=newRow.find('#btnRetirarTerritorio');
                $btnRetirarTerritorio.click($.proxy(ModeloTablaListadoAsociaciones.prototype.removerTerritorio, $this, element.Id_Ter));

                //
                // Boton Contactos 
                //
                var $aContactos=newRow.find('#aContactos');
                $aContactos.click($.proxy(function(sender){
                    this._componenteAsociaciones.abrirContactos(element.Id_Cte, element); //debe ser el dialogo: dvModalContactos
                    //$(this._componenteContactos).contactos('mostrarVistaFormaContacto');
                }, $this, $aContactos));
                $(tabletbody).append(newRow);
            });
            
            var $rows=tabletbody.find('tr');
            $.each($rows, function(index, element){
                var $txtCte_Potencial=$(element).find('#txtCte_Potencial');
                $($txtCte_Potencial).attr('name', 'TerritoriosAsociados[' + index + '].VPO');
                var $hdnId_Ter=$(element).find('#hdnId_Ter');
                $($hdnId_Ter).attr('name', 'TerritoriosAsociados[' + index + '].Id_Ter');
            });
        };

        ModeloTablaListadoAsociaciones.prototype.set_AlRemoverTerritorio=function(fn){
            this._opciones.alRemoverTerritorio=fn;
        };

        ModeloTablaListadoAsociaciones.defaults={
            alRemoverTerritorio: function(idTer){},
            idCte: 0
        };

        //
        // Prospecto / Seguimiento / Territorios
        // 
        //
        function AsociacionProspectoTerritorio($this, opciones) {
            
            var opts = $.extend({}, $.fn.asociacionprospectoterritorio._apiDefaults, opciones);
            //$($this).loadTemplate($('#tplAsociacionTerritorios'));
            $($this).inputmask();
            this._modeloSelectorTerritorio=null;
            this._modeloListadoAsociaciones=null;
            //var contenedorContactos=$($this).find('#dvContenidoModalContactos');                                       
            
            var contenedorContactos=$('#dvContenidoModalContactos');

            this._element=$this;
            var $modalContactos=$(this._element).find('#dvModalContactos');
            this._componenteContactos=$(contenedorContactos).contactos({onNuevoContactoHidden: function(){
                if($modalContactos.hasClass('in')){
                    $('body').addClass('modal-open');
                }
            }});

            //  $modalContactos.on('show.bs.modal', $.proxy(function (e) {
            //      $(this._componenteContactos).contactos('territorio', this._objTerActual);
            //  }, this));

            this._idCteActual=null;
            this._objTerActual=null;
            this._territoriosProspecto=[];
            var elementoSelectorTerritorio=$($this).find('#selDetallesAsociarTerritorio_Id_Ter');
            if(opts.modeloSelectorTerritorio==$.fn.asociacionprospectoterritorio.MODELOS_SELECTOR_TERRITORIO.SELECTPICKER)
            {
                this._modeloSelectorTerritorio=new ModeloSelectPickerSelectorTerritorio(elementoSelectorTerritorio, opts);
            }else{
                throw '¡Modelo de selector de territorio no soportado!';
            }

            if(opts.modeloListadoAsociaciones==$.fn.asociacionprospectoterritorio.MODELOS_LISTADO_ASOCIACIONES.TABLA){
                var $tabla=$($this).find('#tblDetallesTerritoriosAsociados');
                this._modeloListadoAsociaciones=new ModeloTablaListadoAsociaciones($tabla, opts, this._componenteContactos, this);
            }else{
                throw '¡Modelo de listado de territorios no soportado!';
            }
            
            if(typeof(opts.persistente)!=undefined && typeof(opts.persistente)!='undefined'){
                this._cargarListadoDeTerritoriosAsociados(opts.persistente.idCte);
            }
            this._cargarTerritoriosDisponibles();

            if(opts.tipoVista==$.fn.asociacionprospectoterritorio.TIPOSVISTA.EXTENDIDO){
                var $forma=$($this).find('#frmDetallesAsociarTerritorio');
                $forma.addClass('form-inline');
                var $ctrlPotencial=$($this).find('#txtDetallesAsociarTerritorio_Potencial');
                $ctrlPotencial.css('width', '50%');
            }else{
            }
            this._determinarYConfigurarModoDeTrabajo(opts);
        }

        AsociacionProspectoTerritorio.prototype.limpiar=function(){
            this._territoriosProspecto=[];
            this._modeloSelectorTerritorio.reestablecer();
            this._modeloListadoAsociaciones.limpiar();
            $(this._componenteContactos).contactos('reestablecer');
        };

        //Este método solo está disponible para el modo de nueva captura
        AsociacionProspectoTerritorio.prototype.guardar=function(idCte, alFinalizarConExito){
            //guardar asociaciones de territorio/cliente
            _guardarEntrada(this._territoriosProspecto, 0, idCte, alFinalizarConExito);
//            $.each(this._territoriosProspecto, function(index, element){
//                _guardarClienteTerritorio(idCte, element.Id_Rik, element.Id_Ter, element.Id_Seg, element.Cte_Potencial);
//            });
            //guardar contactos
        };

        function _guardarEntrada(coleccion, indice, idCte, alFinalizarConExito){
            var element=coleccion[indice];
            var datos=$.map(coleccion, function(element, index){
                return {
                    Id_Cte : idCte,
                    Id_Rik: element.Id_Rik,
                    Id_Ter: element.Id_Ter,
                    Id_Seg: element.Id_Seg,
                    Cte_Potencial: element.Cte_Potencial
                };
            });
            _guardarClienteTerritorios(datos, alFinalizarConExito);
            /*
            _guardarClienteTerritorio(idCte, element.Id_Rik, element.Id_Ter, element.Id_Seg, element.Cte_Potencial, function(){
                if(indice + 1<coleccion.length){
                    _guardarEntrada(coleccion, indice+1, idCte, alFinalizarConExito);
                }else{
                    alFinalizarConExito();
                }
            });
            */
        }

        //Pendiente
        function _guardarContactos(){
        }

        //
        //Envia los datos en un solo lote
        //
        function _guardarClienteTerritorios(datos, onSuccess, onFailure, always){
            var data={Data: datos};
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatClienteDetPostArray/',
                type: 'POST',
                cache: false,
                contentType: 'application/json',
                data: JSON.stringify(data),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(_guardarClienteTerritorios, null, datos, onSuccess, onFailure, always);
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

        function _guardarClienteTerritorio(idCte, idRik, idTer, idSeg, vpo, onSuccess, onFailure, always){
            var data={
                IdCte : idCte,
                IdRik: idRik,
                IdTer: idTer,
                IdSeg: idSeg,
                VPO: vpo
            };
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatClienteDet/',
                type: 'POST',
                cache: false,
                contentType: 'application/json',
                data: JSON.stringify(data),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(_guardarClienteTerritorio, null);
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

        AsociacionProspectoTerritorio.prototype.get_TerritoriosAsociados=function(){
            return this._territoriosProspecto;
        };

        //
        // Prospecto / Seguimiento / Territorios
        //  ABRE LOS CONTATOS EN LA SECCION DE TERRITORIO
        // Lanza el modal de listado de contactos
        //
        AsociacionProspectoTerritorio.prototype.abrirContactos=function(idCte, objTer){                    
            var $modalContactos=$(this._element).find('#dvModalContactos');
            this._idCteActual=idCte;
            this._objTerActual=objTer;
            $(this._componenteContactos).contactos('territorio', objTer);
            //$($modalContactos).modal('show');
            $('#dvModalContactos').modal('show');            
        };

        AsociacionProspectoTerritorio.prototype.get_TerritorioSeleccionado=function(){
            return this._modeloSelectorTerritorio.get_TerritorioSeleccionado();
        };

        AsociacionProspectoTerritorio.prototype.recargarListado=function(idCte){
            this._cargarListadoDeTerritoriosAsociados(idCte);
        };

        AsociacionProspectoTerritorio.prototype._cargarListadoDeTerritoriosAsociados=function(idCte){
            this._idCteActual=idCte;
            this._modeloListadoAsociaciones._opciones.idCte=idCte;
            this._cargarTerritoriosAsociados(idCte, 
                $.proxy(AsociacionProspectoTerritorio.prototype._cargaTerritoriosAsociadosExitosa, this), 
                $.proxy(AsociacionProspectoTerritorio.prototype._cargaTerritoriosAsociadosFallida, this)
            );
        };

        //
        // Territorios
        //
        AsociacionProspectoTerritorio.prototype._cargaTerritoriosAsociadosExitosa=function(response){
            $.each(response, function(index, element){
                element.Ter_Nombre=element.CatTerritorioSerializable.Ter_Nombre;
            });
            this._territoriosProspecto=response;
            this._cargarListadoTerritorios();
            this._cargarTerritoriosDisponibles();
        };

        //
        // Territorios
        //
        AsociacionProspectoTerritorio.prototype._cargarListadoTerritorios=function(){
            this._modeloListadoAsociaciones.reset(this._territoriosProspecto);
        };

        AsociacionProspectoTerritorio.prototype._cargaTerritoriosAsociadosFallida=function(){
            
        };

        //
        // Territorios
        //
        AsociacionProspectoTerritorio.prototype._cargarTerritoriosAsociados=function(idCte, onSuccess, onFailure, always){
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatClienteDet/?idCte=' + idCte,
                type: 'GET',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(AsociacionProspectoTerritorio.prototype._cargarTerritoriosAsociados, null, idCte, onSuccess, onFailure, always);
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
        };

        //
        // Asociacion Territorios
        //
        AsociacionProspectoTerritorio.prototype._cargarTerritoriosDisponibles=function(){
            var $this=this;
            var territoriosNoAsociados=$.grep($.fn.asociacionprospectoterritorio._territoriosRIK, function(element, index){
                return $.grep($this._territoriosProspecto, function(element2, index2){
                    return element2.Id_Ter==element.Id_Ter;
                }).length<1;
            });

            var $select=$(this._element).find('#selDetallesAsociarTerritorio_Id_Ter');
            $($select).find('option').remove();
            $.each(territoriosNoAsociados, function(index, element){
                $($select).append('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
            });

            // Modal Nuevo Prospecto Territorios
            var $MNP_select=$(this._element).find('#ModalNuevoProspecto_selTerritorios');
            $($MNP_select).find('option').remove();
            $.each(territoriosNoAsociados, function(index, element){
                $($MNP_select).append('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
            });
            
            this._modeloSelectorTerritorio.set_selectedValue(0);
            this._modeloSelectorTerritorio.refresh();
        };

        AsociacionProspectoTerritorio.prototype._determinarYConfigurarModoDeTrabajo=function(opciones){
            var $btnAsociarTerritorio=$(this._element).find('#btnDetallesAsociarTerritorio_Asociar');
            $btnAsociarTerritorio.click($.proxy(AsociacionProspectoTerritorio.prototype._btnAsociarTerritorio_Temporal$click, this, $btnAsociarTerritorio));
            this._modeloListadoAsociaciones.set_AlRemoverTerritorio($.proxy(AsociacionProspectoTerritorio.prototype._alRemoverTerritorioTermporal, this));
//            if(opciones.modo==$.fn.asociacionprospectoterritorio.MODOS.TEMPORAL) { //modo temporal
//                
//            }else{//modo persistente
//                
//            }
        };

        AsociacionProspectoTerritorio.prototype._alRemoverTerritorioTermporal=function(idTer){
            var territorioRetirado=$.grep(this._territoriosProspecto, function(element, index){
                return element.Id_Ter==idTer;
            });

            this._territoriosProspecto=$.grep(this._territoriosProspecto, function(element, index){
                return element.Id_Ter!=idTer;
            });
            this._modeloSelectorTerritorio.agregarTerritorio(territorioRetirado[0]);
            this._modeloSelectorTerritorio.refresh();
        };

        //
        // Prospectos / Seguimiento / Territorio - Asociar Territorio 
        //
        AsociacionProspectoTerritorio.prototype._btnAsociarTerritorio_Temporal$click=function(sender){
            
            var idTer=this._modeloSelectorTerritorio.get_TerritorioSeleccionado();

            if (idTer==null) 
            {
                alertify.error('No hay territorios disponibles.');
            } else {
                var territorioSeleccionado=$.grep($.fn.asociacionprospectoterritorio._territoriosRIK, function(element, index){
                    return element.Id_Ter==idTer;
                });
                //var $VPO=$(this._element).find('#txtDetallesAsociarTerritorio_Potencial');
                var $VPO=$('#txtDetallesAsociarTerritorio_Potencial').val();
                //var vpo=$VPO.val();
                territorioSeleccionado[0].Cte_Potencial = 0;
                territorioSeleccionado[0].Cte_Potencial=$VPO;
                this._territoriosProspecto.push(territorioSeleccionado[0]);
                this._modeloSelectorTerritorio.removeElementByValue(idTer);
                this._modeloSelectorTerritorio.refresh();
                this._modeloListadoAsociaciones.agregarTerritorio(territorioSeleccionado[0]);
                $('#txtDetallesAsociarTerritorio_Potencial').val('');
            }
        };

        $.fn.asociacionprospectoterritorio = function (opciones, arg1, arg2) {
            var opts = $.extend({}, $.fn.asociacionprospectoterritorio.defaults, opciones);
            var obj=$(this).data('asociacionprospectoterritorio_obj');
            if(obj==null){
                var asociacionProspectoTerritorio=null;
                if (opts.modo == $.fn.asociacionprospectoterritorio.MODOS.TEMPORAL) { //modo temporal
                    return this.each(function(){
                        asociacionProspectoTerritorio=new AsociacionProspectoTerritorio(this, opts);
                        $(this).data('asociacionprospectoterritorio_obj', asociacionProspectoTerritorio);
                    });
                } else { //modo persistente
                    return this.each(function(){
                        asociacionProspectoTerritorio=new AsociacionProspectoTerritorio(this, opts);
                        $(this).data('asociacionprospectoterritorio_obj', asociacionProspectoTerritorio);
                        asociacionProspectoTerritorio._modeloListadoAsociaciones.set_modoPersistente();
                        asociacionProspectoTerritorio.recargarListado(opts.idCte);
                    });
                }
            }else{
                if(typeof(opciones)=='string'){
                    if(opciones=='recargar'){
                        obj.recargarListado(arg1);
                    }else if(opciones=='guardar'){
                        obj.guardar(arg1, arg2);
                    }else if(opciones=='limpiar'){
                        obj.limpiar();
                    }
                }else{
                    if(typeof(opts.modo)!='undefined' && typeof(opts.modo)!=undefined){
                        if (opts.modo == $.fn.asociacionprospectoterritorio.MODOS.PERSISTENTE){
                            obj._modeloListadoAsociaciones.set_modoPersistente();
                            obj.recargarListado(opts.idCte);
                        }
                    }
                }
            }
            
        };

        $.fn.asociacionprospectoterritorio.MODOS = {
            TEMPORAL: 1,
            PERSISTENTE: 2
        };

        $.fn.asociacionprospectoterritorio.TIPOSVISTA = {
            REDUCIDO: 1,
            EXTENDIDO: 2
        };

        $.fn.asociacionprospectoterritorio.MODELOS_SELECTOR_TERRITORIO={
            SELECTPICKER: 1
        };

        $.fn.asociacionprospectoterritorio.MODELOS_LISTADO_ASOCIACIONES={
            TABLA: 1
        };

        $.fn.asociacionprospectoterritorio.defaults = {
            modo: $.fn.asociacionprospectoterritorio.MODOS.TEMPORAL
        };

        $.fn.asociacionprospectoterritorio._apiDefaults={
            modeloSelectorTerritorio: $.fn.asociacionprospectoterritorio.MODELOS_SELECTOR_TERRITORIO.SELECTPICKER,
            tipoVista: $.fn.asociacionprospectoterritorio.TIPOSVISTA.REDUCIDO,
            modeloListadoAsociaciones: $.fn.asociacionprospectoterritorio.MODELOS_LISTADO_ASOCIACIONES.TABLA,
        };

        $.fn.asociacionprospectoterritorio._territoriosRIK = [<%=TerritoriosDeRIKComoJson %>];

        function btnAgregar$click(sender, eventArgs){
            sender.get_TerritorioSeleccionado();
        }
    })(jQuery);
    
    
</script>


<script type="text/html" id="tplDialogoContenedorContactos_x">
    <div id="dvModalContactos" style="width: 100%; height: 100%" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaNuevoContacto">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="h5">
                        Contactos
                    </h4>
                </div>
                <div class="modal-body" id="dvContenidoModalContactos">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</script>