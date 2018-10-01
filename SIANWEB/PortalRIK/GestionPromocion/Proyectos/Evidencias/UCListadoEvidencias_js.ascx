<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCListadoEvidencias_js.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Proyectos.Evidencias.UCListadoEvidencias_js" %>
<%@ Register Src="~/Controles/Cliente/DialogoRecurso/UCDialogoRecurso.ascx" TagPrefix="uc" TagName="UCDialogoRecurso" %>

<uc:UCDialogoRecurso runat="server" ID="UCDialogoRecurso1"></uc:UCDialogoRecurso>
<script type="text/html" id="tplListControlProjectPhaseFileCardDetailView">
    <div class="card-pf-body">
		<div class="card-pf-top-element" style="text-align: center;">
			<a href="#!">
				<img class="img-thumbnail" data-src="url" style="width: 64px; height: 64px;">
				</img>
			</a>
		</div>
		<div class="card-pf-heading-kebab">
			<div class="dropdown pull-right dropdown-kebab-pf">
				<button class="btn btn-link dropdown-toggle" id="ddkButton1" type="button" data-toggle="dropdown">
					<span class="fa fa-ellipsis-v">
					</span>
				</button>
				<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="ddkButton1">
					<li>
						<a href="#!">
							Eliminar
						</a>
					</li>
					<li>
						<a href="#!">
							Descargar
						</a>
					</li>
				</ul>
			</div>
		</div>
		<h4 class="card-pf-title text-center" data-content="nombreArchivo">
			
		</h4>
	</div>
</script>
<script type="text/javascript">

    crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardDetailResultsView = function () {
        crm.ui.ListControlDetailView.call(this, options);
        this._$template = $('#tplListControlProjectPhaseFileCardDetailView');
    };

    crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardDetailView.prototype = new crm.ui.ListControlDetailResultsView();

    crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardDetailView.prototype._createItem = function (rowElement, dataElement) {
        var $uiEntry = null;
        var $uiEntryContainer = $('<div class="card-pf card-pf-view">');
        $uiEntryContainer.loadTemplate(this._$template, dataElement);
        return $uiEntryContainer;
    };

    /*****************************************************************************************************************************************/
    crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardView = function (options) {
        crm.ui.ListControlResultsView.call(this, options);
        this._$element = options.element;
        this._$element.addClass('cards-pf');
        this._$rowContainer = $('<div class="row row-cards-pf">');
        this._$element.append(this._$rowContainer);
        this.set_DetailView(new crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardDetailResultsView({ container: this }));
        
    };

    crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardView.prototype = new crm.ui.ListControlResultsView();

    crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardView.prototype.append = function ($element) {
        var $newcol = $('<div class="col-xs-12 col-sm-4 col-md-3 col-lg-2">');
        this._$rowContainer.append($newcol);
    };

    crm.gestionpromocion.proyectos.evidencias.ListadoEvidencias = function (options) {
        this._listado = new crm.ui.gestionpromocion.proyectos.ListControlProjectPhaseFileCardView(options);
        this._entradas = [];
        this._currentIdOp = 0;
        var _this = this;
        this._dialogoRecurso = new crm.ui.DialogoRecurso({
            elemento: $('#<%=UCDialogoRecurso1.ClientID %>'),
            areaArrastre: $('#<%=UCDialogoRecurso1.AreaArrastreClientID %>'),
            alAceptar: function (archivosCargados) {
                //crear una nueva entrada: revisar si loadTemplate permite especificar una función como la fuente de datos para un campo
                _this._entradas.push({ url: archivosCargados[0].url, nombreArchivo: archivosCargados[0].nombreArchivo, idRecurso: archivosCargados[0].idRecurso });
                _this._listado.bind(_this._entradas);
                //asociar el recurso con la fase: servicio de asociación
                svcEvidencia.crearEvidencia(_this._currentIdOp, archivosCargados[0].idRecurso,
                function () {
                    //mostrar una señal de progreso
                    alert('Evidencia asociada con éxito');
                },
                function () {
                    //mostrar una señal de error y volver a intentar
                    alert('Se presentó una complicación al tratar de asociar la evidencia');
                },
                function () {
                },
                {
                });

            },
            comandoAceptar: $('#<%=UCDialogoRecurso1.ComandoAceptarId %>'),
            comandoArchivos: $('#<%=UCDialogoRecurso1.ComandoArchivosId %>'),
            elementoCampoUrl: $('#<%=UCDialogoRecurso1.CampoURLId %>'),
            idRep: '<%=IdRepositorioPropuesta %>',
            elementoBarraProgresoTransferenciaArchivo: $('#<%=UCDialogoRecurso1.BarraProgresoTransferenciaArchivoId %>'),
            elementoMenuNavegacionNuevoRecurso: $('#<%=UCDialogoRecurso1.MenuNavNuevoRecursoId %>'),
            elementoMenuNavegacionRecursoExistente: $('#<%=UCDialogoRecurso1.MenuNavRepositorioId %>'),
            elementoAcordeonURL: $('#<%=UCDialogoRecurso1.AcordeonElementoURL %>'),
            elementoAcordeonArchivo: $('#<%=UCDialogoRecurso1.AcordeonElementoArchivo %>')
        });
    };

    crm.gestionpromocion.proyectos.evidencias.ListadoEvidencias.prototype.recargarProyecto = function (datosProyecto) {
        this._currentIdOp = datosProyecto.Id;
        var _this = this;
        svcEvidencia.obtenerEvidenciasDeProyecto(this._currentIdOp,
        function (response) {
            //mostrar señal de progreso
            _this._dialogoRecurso.cambiarRepositorio(response.idRepositorioFase);
            _this._entradas = response.evidencias;
            _this._listado.bind(_this._entradas);
        },
        function () {
        },
        function () {
        },
        {
        });

    };

    crm.gestionpromocion.proyectos.evidencias.ListadoEvidencias.prototype.agregarEvidencia = function () {
        
    };
</script>
