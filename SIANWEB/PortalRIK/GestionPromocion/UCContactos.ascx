<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCContactos.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.UCContactos" %>

<script type="text/html" id="tplPresentacionDialogoFormaContacto">
    
</script>

<script src="<%=Page.ResolveUrl("~/js/horizon-swiper/horizon-swiper.min.js") %>"></script>
<script type="text/javascript">

    crm.contactos= function(){
    };

    crm.contactos.HorizonSwiperListadoContactos = function ($element) {
        this._$elementoContenedor=$element;
        var $listadoContactos=this._$elementoContenedor.find('#dvListadoContactos');
        if (!$($listadoContactos).hasClass('horizon-swiper')) {
            $($listadoContactos).addClass('horizon-swiper');
        }
        this._$element = $listadoContactos;// $($element).horizonSwiper();
        this._$vistaContactosVacios = this._$elementoContenedor.find('#bsSinContactos');
        this._contactos = [];
    };

    crm.contactos.HorizonSwiperListadoContactos.prototype.reestablecer = function () {
        this._contactos = [];
        this._construir();
    };

    crm.contactos.HorizonSwiperListadoContactos.prototype._construir = function () {
        var $newContainer = $('<div class="horizon-swiper" id="dvListadoContactos">');
        var $this = this;
        $.each(this._contactos, function (index, element) {
            var $newElement = $('<div class="horizon-item">');
            $newElement.loadTemplate($('#tplElementoListadoContactos'));
            var $botonRetirar = $newElement.find('#btnRetirarContacto');
            $botonRetirar.data('_ui_element', $newElement);
            $botonRetirar.click($.proxy(function (e) {
                var $thisElement = element;
                this._contactos = $.grep(this._contactos, function (element2, index2) {
                    return (element2 != $thisElement);
                });
                this._construir();
                //                var $uiElement = $botonRetirar.data('_ui_element');
                //                $uiElement.remove();
            }, $this));
            $newContainer.append($newElement);
        });
        $(this._$element).replaceWith($newContainer);
        if (this._contactos.length > 0) {
            this._$vistaContactosVacios.hide();
            $($newContainer).horizonSwiper();
        } else {
            this._$vistaContactosVacios.show();
        }
        this._$element = $newContainer;
    };

    crm.contactos.HorizonSwiperListadoContactos.prototype.agregar = function (objContacto) {
        //actualizar el modelo de contactos
        this._contactos.push(objContacto);
        //actualizar la interface de usuario
        /*var $nuevoElemento = $('<div class="horizon-item">');
        $nuevoElemento.loadTemplate($('#tplElementoListadoContactos'));
        $(this._$element).append($nuevoElemento);*/

        this._construir();
    };

    crm.contactos.HorizonSwiperListadoContactos.prototype.cargar = function (idCte, idTer) {
        $.ajax({
            url: '<%=ApplicationUrl %>' + '/api/CatClienteDetContactos/?idCte=' + idCte,
            type: 'GET',
            cache: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(AsociacionProspectoTerritorio.prototype._cargarTerritoriosAsociados, null, idCte, onSuccess, onFailure, always);
                }
            }
        });
    };

    crm.contactos.FormaContacto=function($element, idCte, idTer){
      this._contacto=null;
      this._$element = $element;
      $(this._$element).loadTemplate('#tplFormaContacto');
      this._idCte=idCte;
      this._idTer=idTer;
    };

    crm.contactos.FormaContacto.prototype.limpiar = function () {
        this._$element.find('#txtNombreContacto').val('');
        this._$element.find('#txtPuesto').val('');
        this._$element.find('#txtCumpleanos').val('');
        this._$element.find('#txtCorreoElectronico').val('');
        this._$element.find('#txtDireccionNegocio').val('');
        this._$element.find('#txtDireccionHogar').val('');
        this._$element.find('#txtTelefonoNegocio').val('');
        this._$element.find('#txtTelefonoHogar').val('');
        this._$element.find('#hdnId_Contacto').val('');
    };

    crm.contactos.FormaContacto.prototype._updateModel=function(){
        var nombreContacto=this._$element.find('#txtNombreContacto').val();
        var puesto=this._$element.find('#txtPuesto').val();
        var cumpleanos=this._$element.find('#txtCumpleanos').val();
        var correoElectronico=this._$element.find('#txtCorreoElectronico').val();
        var direccionNegocio=this._$element.find('#txtDireccionNegocio').val();
        var direccionHogar=this._$element.find('#txtDireccionHogar').val();
        var telefonoNegocio=this._$element.find('#txtTelefonoNegocio').val();
        var telefonoHogar=this._$element.find('#txtTelefonoHogar').val();
        var idContacto=this._$element.find('#hdnId_Contacto').val();
        var infoContacto={
            Id_Emp: '<%=EntidadSesion.Id_Emp %>',
            Id_Cd: '<%=EntidadSesion.Id_Cd %>',
            Id_Cte: this._idCte,
            Id_Ter: this._idTer,
            Id_Contacto: idContacto,
            Cont_Nombre: nombreContacto,
            Cont_Domicilio: direccionNegocio,
            Cont_Telefono: telefonoNegocio,
            Cont_Email: correoElectronico
        };
        return infoContacto;
    };

    crm.contactos.FormaContacto.prototype._updateForm=function(contacto){
        this._$element.find('#txtNombreContacto').val(contacto.Cont_Nombre);
        this._$element.find('#txtPuesto').val();
        this._$element.find('#txtCumpleanos').val();
        this._$element.find('#txtCorreoElectronico').val(contacto.Cont_Email);
        this._$element.find('#txtDireccionNegocio').val(contacto.Cont_Domicilio);
        this._$element.find('#txtDireccionHogar').val();
        this._$element.find('#txtTelefonoNegocio').val(contacto.Cont_Telefono);
        this._$element.find('#txtTelefonoHogar').val();
        this._$element.find('#hdnId_Contacto').val(contacto.Id_Contacto);
    };

    crm.contactos.FormaContacto.prototype.get_contacto=function(){
        this._contacto=this._updateModel();
        return this._contacto;
    };

    crm.contactos.FormaContacto.prototype.set_contacto=function(contacto){
        this._updateForm(contacto);
        this._contacto=contacto;
    };

    crm.contactos.VistaDialogoFormaContacto = function ($element, modo, onAceptar, onHidden) {
        this._$element = $element;
        $(this._$element).loadTemplate('#tplVistaDialogoFormaContacto');
        this._modo = modo;
        this._onAceptar = onAceptar;
        this._formaContacto = null;
        this._onHidden = onHidden;
        var $btnAceptar = this._$element.find('#btnCrearNuevoContacto');
        $btnAceptar.click($.proxy(crm.contactos.VistaDialogoFormaContacto.prototype._aceptar, this, $btnAceptar[0]));
        $(this._$element).find('#dvModalNuevoContacto').on('hidden.bs.modal', $.proxy(function () {
            this._onHidden();
            if ($('#dvModalEditarProyecto').hasClass('in')) {
                $('body').addClass('modal-open');
            }
        }, this));
    };

    //
    crm.contactos.VistaDialogoFormaContacto.prototype.set_FormaContacto = function (frmContacto) {
        this._formaContacto = frmContacto;
    };

    // Contactos Modal
    crm.contactos.VistaDialogoFormaContacto.prototype.mostrar = function () {        
        $(this._$element).find('#dvModalNuevoContacto').modal('show');
    };

    //
    // Guardar Contacto 
    //
    crm.contactos.VistaDialogoFormaContacto.prototype._aceptar = function (sender) {
        if (this._onAceptar != null) {
            this._onAceptar(this._formaContacto.get_contacto());
        }
        this._$element.find('#dvModalNuevoContacto').modal('hide');
    };

    crm.contactos.Modo = {
        NUEVO: 1,
        EDICION: 2
    };

    (function ($) {
        $.widget('crm.contactos', {
            options: {
                objTer: null,
                modo: crm.contactos.Modo.NUEVO,
                onNuevoContactoHidden: function(){}
            },
            territorio: function (objTer) {
                if (objTer == undefined) {
                    return this.options.objTer;
                }

                this.options.objTer = objTer;
                //actualizar UI
                //Titulo de territorio
                $(this.element).find('#ddTerritorio').text(this.options.objTer.Ter_Nombre);
                //Listado de contactos
                this._listadoContactos.reestablecer();
            },
            _agregarNuevo: function(){
                //agregar nueva instancia de contacto al conjunto de contactos del componente
                var nombreContacto=this._formaContacto.get_nombre();
                //agregar un nuevo elemento al listado de contactos del componente
                this._listadoContactos.agregar({
                    Contacto_Nombre: nombreContacto 
                });
            },
            _create: function () {
                $(this.element).loadTemplate($('#tplContactos'));

                this._agregarfn=null;
                
                var $dialogoFormaContactoContenido=$(this.element).find('#dvModalContacto');
                this._vistaFormaContacto=new crm.contactos.VistaDialogoFormaContacto($dialogoFormaContactoContenido, this.options.modo, $.proxy(this._onAceptar, this), this.options.onNuevoContactoHidden);

                var $formaContactoContainer = $($dialogoFormaContactoContenido).find('#frmModalNuevoContacto');
                this._formaContacto = new crm.contactos.FormaContacto($formaContactoContainer, 0, 0);

                this._vistaFormaContacto.set_FormaContacto(this._formaContacto);

                this._listadoContactos=new crm.contactos.HorizonSwiperListadoContactos($(this.element).find('#dvListadoContactosContenedor'));
                this._contactos=[];
//                if(this.options.modo==crm.contactos.Modo.NUEVO){
//                    this._agregarfn=$.proxy(,this);
//                }
                var $botonAgregar=$(this.element).find('#btnAgregarContacto');
                $botonAgregar.click($.proxy(function(e){
                    $(this.element).contactos('mostrarVistaFormaContacto');
                }, this));
            },
            mostrarVistaFormaContacto: function(idCte, idTer){
                this._formaContacto.limpiar();
                this._vistaFormaContacto.mostrar();
            },
            _onAceptar: function(contacto){
                this._listadoContactos.agregar({});
            },
            reestablecer: function(){
                this._listadoContactos.reestablecer();
            }
        });
    })(jQuery);
</script>

<script type="text/html" id="tplVistaDialogoFormaContacto">
    <div id="dvModalNuevoContacto" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaNuevoContacto">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="imgProcesandoNuevoContacto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="h5">
                        Nuevo Contacto
                    </h4>
                </div>
                <div class="modal-body" id="dvContenidoModalNuevoContacto">
                    <div id="frmModalNuevoContacto">
                        
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary"
                        id="btnCrearNuevoContacto">
                        Guardar
                    </button>
                </div>
            </div>
        </div>
    </div>
</script>

<script type="text/html" id="tplElementoListadoContactos">
    <div class="panel panel-default">
        <div class="panel-heading">
            <button type="button" class="close" style="line-height: 0 !important;" aria-label="Close" id="btnRetirarContacto">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="panel-body">
            <img class="img-thumbnail" src='<%=Page.ResolveUrl("~/Img/user-placeholder-140x140.png") %>' />
        </div>
    </div>
</script>

<script type="text/html" id='tplContactos'>
    <div class="panel panel-default" data-id="id">
        <div class="panel-heading">
            <h4>
                Contactos
            </h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <dl class="dl-horizontal">
                        <dt>
                            Territorio
                        </dt>
                        <dd id="ddTerritorio">
                            
                        </dd>
                    </dl>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <button class="btn btn-primary" id="btnAgregarContacto">
                        Agregar
                    </button>

                    <!--Dialogo forma contacto-->
                    <div id="dvModalContacto">
                    </div>
                    <!--//Dialogo forma contacto-->

                </div>
            </div>
            <!--Listado-->
            <div class="row">
                <div class="col-md-12">

                    <div id="dvListadoContactosContenedor" class="horizon-swiper">
                        <!--Blank slate-->
                        <div class="blank-slate-pf" id="bsSinContactos">
                            <h1>
                                Contactos
                            </h1>
                            <p>
                                Presiona "Agregar contacto" para asociar un nuevo contacto al territorio 
                            </p>
                        </div>
                        <!--//Blank slate-->
                        <div id="dvListadoContactos" class="horizon-swiper">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</script>

<script type="text/html" id="tplFormaContacto">
<div class="row">
    <div class="col-md-8">
        <input type="hidden" id="hdnId_Contacto" name="Id_Contacto" />
        <div class="form-group">
            <label for="txtNombreContacto">
                Nombre
            </label>
            <input class="form-control" type="text" id="txtNombreContacto" />
        </div>

        <div class="form-group">
            <label for="txtPuesto">
                Puesto
            </label>
            <input class="form-control" type="text" id="txtPuesto" />
        </div>

        <div class="form-group">
            <label for="txtCumpleanos">
                Cumpleaños
            </label>
            <input class="form-control" type="date" id="txtCumpleanos" />
        </div>

        <div class="form-group">
            <label for="txtCorreoElectronico">
                Correo Electrónico
            </label>
            <input class="form-control" type="text" id="txtCorreoElectronico" />
        </div>
        <img id="direccionMap" />
        <hr />
        <h2>Direcciones</h2>

        <div class="form-group">
            <label for="txtDireccionNegocio">
                Negocio
            </label>
                                    
            <div class="input-group">
                <input class="form-control" type="text" id="txtDireccionNegocio" />
                <span class="input-group-addon"><a href="#" data-toggle="modal" data-target="#dvModalMapaGoogle" id="btnMapaUbicacionNegocio" ><i class="fa fa-map-marker" aria-hidden="true"></i></a></span>
                <input type="hidden" id="hdnUbicacionNegocioLat" />
                <input type="hidden" id="hdnUbicacionNegocioLng" />
            </div>
        </div>

        <div class="form-group">
            <label for="txtDireccionHogar">
                Hogar
            </label>
                                    
            <div class="input-group">
                <input class="form-control" type="text" id="txtDireccionHogar" />
                <span class="input-group-addon"><a href="#" data-toggle="modal" data-target="#dvModalMapaGoogle" id="btnMapaUbicacionHogar"><i class="fa fa-map-marker" aria-hidden="true"></i></a></span>
                <input type="hidden" id="hdnUbicacionHogarLat" />
                <input type="hidden" id="hdnUbicacionHogarLng" />
            </div>
        </div>

        <hr />
        <h2>Teléfonos</h2>

        <div class="form-group">
            <label for="txtTelefonoNegocio">
                Negocio
            </label>
            <input class="form-control" type="text" id="txtTelefonoNegocio" />
        </div>

        <div class="form-group">
            <label for="txtTelefonoHogar">
                Hogar
            </label>
            <input class="form-control" type="text" id="txtTelefonoHogar" />
        </div>

    </div>
    <div class="col-md-4">
        <img class="img-thumbnail" src="<%=Page.ResolveUrl("~/Img/user-placeholder-140x140.png") %>" />
    </div>
</div>
</script>