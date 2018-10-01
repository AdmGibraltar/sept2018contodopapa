
// Proyesctos.js
// Esta libreria es parte de 
// Proyectos_TablaAgrupada y esta pendiente de integrar
// 11 Sep 2018 RFH

   
        var _tablaProyectos = null;

        function crmOnReady($){
            $('input').inputmask();
            _thisWindow=window;
            _tablaProyectos = $('#tblProyectos').DataTable({
                /*"sDom": "<'dataTables_header' <'row' <'col-md-10' f i r> B <'col-md-1' <'#tblProyectosToolbar'> > > >" +
                                                    "<'table-responsive'  t >" +
                                                    "<'dataTables_footer' p >",*/
                'pageLength': 7,
                "deferRender": true,
                'ordering': true,
                'scrollY': '200px',
                'scrollCollapse': true,
                'language':{
                    'url' : 'http://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json'
                },
//                    drawCallback: function () { // this gets rid of duplicate headers
//                          $('.dataTables_scrollBody thead tr').css({ display: 'collapse' }); 
//                      },
                'ajax': {
                    'url': '<%=ApplicationUrl %>' + '/api/ObtenerProyectosPorRik', //'/api/CrmProyecto_TablaAgrupada/', //'<%=Page.ResolveUrl("../../WebService/PortalRIK/GestionPromocion/Proyectos.svc") %>' + '/ObtenerTodos',
                    'dataSrc': ''
                },
                /*buttons: [
                    {
                        text: 'Nuevo Proyecto',
                        action: function(e, dt, node, config){
                        },
                        className: 'btn btn-default',
                        tag: 'input'
                    }
                ],*/
                'columns': [
                        {
                            'className': 'details-control',
                            'orderable': false, 
                            'data': null,
                            'defaultContent': ''
                        },
                        { 'data': 'Id_Cte' },
                        { 'data': 'NombreCliente' },
                        { 
                            'data': null,
                            'defaultContent': '<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#dvModalValuacion" data-modo="0" id="btnGenerarValuacion"><i class="fa fa-tasks"></i>Generar Valuación</button>',
                            'render': function (data, type, full, meta) {
                                
                                return '<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#dvModalValuacion" data-modo="0" id="btnGenerarValuacion" data-idcte="' + full.Id_Cte + '"><i class="fa fa-tasks"></i>Generar Valuación</button>';
                            }
                        }
                    ]
            });
            
            function cerrarVentanaValuacion_Generada(id){
                $('#dvModalValuacion').modal('hide');
                recargarListadoProyectos();
                PatternflyToast.showSuccess('La valuación ' + id + ' ha sido creada con éxito', 10000);
//                if(_thisWindow!=null){
//                    _thisWindow.location.assign(_thisWindow.location.origin + _thisWindow.location.pathname);
//                }
            }

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaValuacion_Actualizada(id){            
                $('#dvModalValuacion').modal('hide');
                recargarListadoProyectos();
                PatternflyToast.showSuccess('La valuación ' + id + ' ha sido actualizada con éxito', 10000);
//                if(_thisWindow!=null){
//                    _thisWindow.location.assign(_thisWindow.location.origin + _thisWindow.location.pathname);
//                }
            }

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#iframeVentanaValuacion').on('load', function(){
                if(_modoValuacion==0){
                    $('#iframeVentanaValuacion')[0].contentWindow._externalCustomFn=cerrarVentanaValuacion_Generada;
                }else{
                    $('#iframeVentanaValuacion')[0].contentWindow._externalCustomFn=cerrarVentanaValuacion_Actualizada;
                }
                
                $('#dvCuerpoVentanaValuacion').unblock();
            });

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaValuacionGlobal_Generada(id){                
                $('#dvModalValuacionGlobal').modal('hide');
                recargarListadoProyectos();
                PatternflyToast.showSuccess('La valuación global ' + id + ' ha sido creada con éxito', 10000);
            }

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaValuacionGlobal_Actualizada(id){            
                $('#dvModalValuacionGlobal').modal('hide');
                recargarListadoProyectos();
                PatternflyToast.showSuccess('La valuación global ' + id + ' ha sido actualizada con éxito', 10000);
            }

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#iframeVentanaValuacionGlobal').on('load', function(){
                if(_modoValuacionGlobal==0){
                    $('#iframeVentanaValuacionGlobal')[0].contentWindow._externalCustomFn=cerrarVentanaValuacionGlobal_Generada;
                }else{
                    $('#iframeVentanaValuacionGlobal')[0].contentWindow._externalCustomFn=cerrarVentanaValuacionGlobal_Actualizada;
                }
                
                $('#dvCuerpoVentanaValuacionGlobal').unblock();
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function crearFilaValuacionGlobal($tbody, datosValuacionGlobal){
                ///<summary>Crea la fila de la valuación global por agrupación de cliente en el listado de proyectos</summary>
                ///<param name="datosValuacionGlobal" type="CapValuacionGlobalCliente"></param>
                var $tr=$('<tr></tr>');
                $tr.loadTemplate($('#tplValuacionGlobal'), datosValuacionGlobal);
                var $tdUtilidadRemanente=$tr.find('#tdUtilidadRemanente');
                var $tdVPN=$tr.find('#tdVPN');
                $tdUtilidadRemanente.text(numeral(datosValuacionGlobal.Vap_UtilidadRemanente).format('$0,0.00'));
                $tdVPN.text(numeral(datosValuacionGlobal.Vap_ValorPresenteNeto).format('$0,0.00'));
                $tbody.append($tr);
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function validarEstadoDeCheck($check, proyecto, proyectosEnValuacionGlobal){
                var coincidencia=$.grep(proyectosEnValuacionGlobal, function(element, index){
                    return proyecto.Id==element.Id_Op;
                });
                if(coincidencia.length>0){
                    //$check.iCheck('check');
                    $check.prop('checked', true);
                }else{
                    //$check.iCheck('uncheck');
                    $check.prop('checked', false);
                }
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function toggled($e){
                $e.preventDefault();
                $($e.currentTarget).unbind('ifToggled', toggled);
                var state=$($e.currentTarget).is(':checked') ? 'uncheck' : 'check';
                $($e.currentTarget).iCheck(state);
                $($e.currentTarget).iCheck('update');//.on('ifToggled', toggled);
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function crearTablaHija(datos){                                
                var sliderDiv=$('<div class="slider">');
                var elemento=$('<table class="table table-striped table-bordered"><thead><tr><th></th><th>Clave</th><th style="text-align: center;">Área de Aplicación</th><th>Editar</th><th style="text-align: center;">Utilidad Remanente</th><th style="text-align: center;">Valor Presente Neto</th><th style="text-align: center;">Estado de Valuación</th><th>Editar Valuación</th><th style="text-align: center;">Propuestas</th></tr></thead><tbody>');
                crearFilaValuacionGlobal(elemento, datos.ValuacionGlobal);                
                sliderDiv.append(elemento);               

                $.each(datos.Proyectos, function(index, element){
                    var tr=$('<tr>');
                    var $tdForCheck=$('<td><div class="inactiveLink"><input type="checkbox" id="chk_' + element.Id + '" name="chk_' + element.Id + '"/></div></td>');
                    validarEstadoDeCheck($tdForCheck.find('input'), element, datos.ProyectosEnValuacion);
                    // $tdForCheck.find('div').on('click', function(e){
                    //     e.preventDefault();
                    //     return false;
                    // });
                    $tdForCheck.find('input').iCheck({checkboxClass: 'icheckbox_square-blue'});
                    
                    $tdForCheck.find('.iCheck-helper').css('position', 'relative');
                    tr.append($tdForCheck);
                    
                    //Se crea ahora el campo para la descripción del proyecto, pero se agrega a la estructura hasta después de agregar el campo de detalle.
                    var $tdDescripcion=$('<td>' + element.Descripcion + '</td>');
                    if(element.Cancelado==true){
                        $tdDescripcion.css('text-decoration', 'line-through');
                    }

                    var a=$('<a>');
                    $(a).attr('href', '#dvDetalles');
                    $(a).click(function(){
                        var renglonActual=elemento.data('_renglonActual');
                        if(renglonActual!=null){
                            if(renglonActual!=tr){
                                //retirar el estilo del renglón actual.
                                renglonActual.children('td,th').css({'background-color': 'transparent', 'border-bottom-color': ''});
                            }
                        }
                        renglonActual=tr;
                        renglonActual.children('td,th').css({'background-color': '#d5ecf9', 'border-bottom-color': '#a7cadf'});
                        elemento.data('_renglonActual', renglonActual);
                        _$campoDescripcionActual=$tdDescripcion;
                        cargarProductosDeProyecto(element.Id, element.Id_Cte, element);

                        //Si el proyecto está cancelado, se desactiva el comando de cancelación
                        if(element.Cancelado==true){
                            $('#btnCancelarProyecto').attr('disabled', true);
                            if($('#btnCancelarProyecto').hasClass('disabled')==false){
                                $('#btnCancelarProyecto').addClass('disabled');
                            }
                        }else{
                            $('#btnCancelarProyecto').attr('disabled', false);
                            if($('#btnCancelarProyecto').hasClass('disabled')==true){
                                $('#btnCancelarProyecto').removeClass('disabled');
                            }
                        }
                    });
                    $(a).text(element.Id);
                    $(tr).append($('<td>').append(a)); //+ element.Id + '</td>'));
                    
                    $(tr).append($tdDescripcion);

                    /*
                    $(tr).append($('<td>' + element.Cli_VPObservado + '</td>'));

                    $(tr).append($('<td>' + element.Analisis + '</td>'));

                    $(tr).append($('<td>' + element.Presentacion + '</td>'));

                    $(tr).append($('<td>' + element.Negociacion + '</td>'));

                    $(tr).append($('<td>' + element.Cierre + '</td>'));

                    $(tr).append($('<td>' + element.Avances + '</td>'));
                    */

                    var editCommand=$('<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#dvModalEditarProyecto" data-action="2" ><i class="fa fa-pencil-square-o"></i></button>');
                    
                    editCommand.data('obj', element);
                    if(element.Cancelado==true){
                        $(tr).append($('<td>--</td>'));
                    }else{
                        $(tr).append($('<td>').append(editCommand));
                    }
                    
                    if(element.CrmValuacionOportunidades!=null){
                        var alMenosUnoNegativo=false;
                        if(element.UtilidadRemanente!=null){//.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente!=null){
                            var $nodoUR=$('<td>');
                            establecerFormatoResultadosValuacion($nodoUR, element.UtilidadRemanente);//CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente);
                            if(element.UtilidadRemanente<0){//.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente<0){
                                alMenosUnoNegativo=true;
                            }
                            var utilidadRemanenteConFormato=numeral(element.UtilidadRemanente).format('$0,0.00');//.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente).format('$0,0.00');
                            $(tr).append($nodoUR.text(utilidadRemanenteConFormato));
                        }else{
                            $(tr).append($('<td>').text('--'));
                        }

                        if(element.ValorPresenteNeto!=null){//.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto!=null){
                            var $nodoVPN=$('<td>');
                            if(element.ValorPresenteNeto<0){//.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto<0){
                                alMenosUnoNegativo=true;
                            }
                            establecerFormatoResultadosValuacion($nodoVPN, element.ValorPresenteNeto);//.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto);
                            var valorPresenteNetoConFormato=numeral(element.ValorPresenteNeto).format('$0,0.00');//.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto).format('$0,0.00');
                            $(tr).append($nodoVPN.text(valorPresenteNetoConFormato));
                        }else{
                            $(tr).append($('<td>').text('--'));
                        }

                        //En proceso de autorización
                        if( (element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus=='C' && 
                            (element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2==2 || element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2==3)) || 
                            (element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus=='A' && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2==2) ){
                            $(tr).append($('<td>Autorizando</td>'));
                        }else if(
                                (element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus=='A' && 
                                (element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2==3 || element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2==4))
                                ){
                            $(tr).append($('<td>Autorizado</td>'));
                        }else if( /*Cascada para manejar el caso cuando la valuación es positiva, pero no ha sido autorizada: solo para casos antiguos*/
                                element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente!=null
                                && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto!=null
                                ){

                                if(
                                    element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente>0
                                    && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto>0
                                ){
                                    $(tr).append($('<td>Autorizado</td>'));
                                }else{
                                    $(tr).append($('<td>Autorizando</td>'));
                                }
                        }
//                        if(alMenosUnoNegativo==true){
//                            $(tr).append($('<td><button class="btn btn-primary"><i class="fa fa-bell" aria-hidden="true"></i>Requerir Autorización</button></td>'));
//                        }else{
//                            $(tr).append($('<td>Autorizado</td>'));
//                        }

                        if(element.CrmValuacionOportunidades!=null){
                            if(element.CrmValuacionOportunidades.CapValProyectoSerializable!=null){
                                $(tr).append($('<td>'+
                                        '<button '+
                                            'type="button" '+
                                            'class="btn btn-primary" '+
                                            'data-toggle="modal" '+
                                            'data-target="#dvModalValuacion" '+
                                            'id="btnEditarValuacion" '+
                                            'data-idcte="' + element.Id_Cte + '" '+
                                            'data-idval="' + element.CrmValuacionOportunidades.CapValProyectoSerializable.Id_Vap + '" data-modo="1">'+
                                                '<i class="fa fa-tasks"></i>Ver Valuación'+
                                        '</button>'+
                                    '</td>'));
                            }
                        }

                    }else{
                        $(tr).append($('<td>').text('--'));
                        $(tr).append($('<td>').text('--'));
                        $(tr).append($('<td>').text('Sin Valuación'));
                        $(tr).append($('<td>').text(''));
                    }

                    //Condiciones para la visualización del comando de propuestas
                    if(element.CrmValuacionOportunidades!=null){
                        if(element.CrmValuacionOportunidades.CapValProyectoSerializable!=null){
                            if(element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2==3 && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus=='A'){
                                $(tr).append($('<td><button type="button" class="btn btn-primary" '+
                                'data-toggle="modal" '+
                                'data-target="#dvModalPropuestaTE" '+
                                'id="btnVisualizarPropuestaTE" '+
                                'data-idop="' + element.Id + '" '+
                                'data-idcte="' + element.Id_Cte + '" '+
                                'data-idval="' + element.CrmValuacionOportunidades.CapValProyectoSerializable.Id_Vap + '" >'+
                                    '<i class="fa fa-tasks"></i>Ver Propuesta</button>'+
                                '</td>'));

                            }else if( /*Cascada para manejar el caso cuando la valuación es positiva, pero no ha sido autorizada: solo para casos antiguos*/
                                    element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente!=null
                                    && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto!=null
                                    ){
                                        if(
                                            element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente>0
                                            && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto>0
                                        ){
                                            $(tr).append($('<td><button type="button" class="btn btn-primary" '+
                                            'data-toggle="modal" '+
                                            'data-target="#dvModalPropuestaTE" '+
                                            'id="btnVisualizarPropuestaTE" '+
                                            'data-idop="' + element.Id + '" '+
                                            'data-idcte="' + element.Id_Cte + '" '+
                                            'data-idval="' + element.CrmValuacionOportunidades.CapValProyectoSerializable.Id_Vap + '" >'+
                                                '<i class="fa fa-tasks"></i>Ver Propuesta</button>'+
                                            '</td>'));

                                        }else{
                                            $(tr).append($('<td>').text(''));
                                        }
                            }
                        }else{
                            $(tr).append($('<td>').text(''));
                        }
                    }else{
                        $(tr).append($('<td>').text(''));
                    }
                    

                    $(elemento).find('tbody').append(tr);
                });
                    
                return sliderDiv;
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function establecerFormatoResultadosValuacion($nodo, valor){
                if(valor<0){
                    $nodo.css('background-color', 'red');
                }else{
                    $nodo.css('background-color', 'green');
                }

                $nodo.css('color', 'white');
                $nodo.css('font-weight', 'bold');
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#tblProyectos tbody').on('click', 'td.details-control', function(){
                var tr=$(this).closest('tr');
                var row=_tablaProyectos.row(tr);
                if(row.child.isShown()){
                    $('div.slider', row.child()).slideUp(function(){
                        row.child.hide();
                        tr.removeClass('shown');
                    });
                }else{
                    row.child(crearTablaHija(row.data()), 'no-padding').show();
                    tr.addClass('shown');
                    $('div.slider', row.child()).slideDown();
                }
            });

            //$('#tblProyectosToolbar').html('<button class="btn btn-default" data-toggle="modal" data-target="#dvModalEditarProyecto" data-action="1" ><i class="fa fa-plus"></i>Nuevo Proyecto</button>');
            //$('#tblProyectosToolbar').css('padding', '2px 0');

            $('#dvProgreso').radiosToSlider({ animation: true, fitContainer: true });

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

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#dvModalDimension').on('hidden.bs.modal', function(){
                if($('#dvModalEditarProyecto').hasClass('in')){
                    $('body').addClass('modal-open');
                }
            });

            $('#tvHerramientas').treeview({
                collapseIcon: "fa fa-angle-down",
                data: defaultData,
                expandIcon: "fa fa-angle-right",
                nodeIcon: "fa fa-folder",
                showBorder: false,
                enableLinks: true
            });

            $('#tvHerramientas').treeview('collapseAll', { silent: true });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#ddFiltroClave').on('show.bs.dropdown', function (e) {
                e.stopPropagation();
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#ddFiltroClaveOrdenarAsc').click(function () {
                $('#tblProyectos').DataTable().order([0, 'asc']);
                $('#tblProyectos').DataTable().draw(false);
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#ddFiltroClaveOrdenarDesc').click(function () {
                $('#tblProyectos').DataTable().order([0, 'desc']);
                $('#tblProyectos').DataTable().draw(false);
            });
            
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('input[iCheck]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            //var $selUEN = $('#dvModalEditarProyecto #selUEN');
            //cargarUENs($, $selUEN);

//            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
//            cargarTerritoriosPorRIK($, $selTerritorio, '<%=EntidadSesion.Id_Rik %>');

            inicializarModalNuevoProyecto();

            inicializarCampoProspecto();
            inicializarCampoCliente();

            var $contenedorListadoProductosAplicacion=$('#contendorProductos');
            var $contenedorListadoOtrosProductos=$('#contenedorOtrosProductos');
            //$inicializarCampoProductoBusqueda($contenedorListadoProductosAplicacion);
            $inicializarCampoProductoAplicacionBusqueda($contenedorListadoProductosAplicacion);
            //$inicializarCampoProductoBusqueda($contenedorListadoOtrosProductos);
            $inicializarCampoProductoBusquedaOtrosProductos($contenedorListadoOtrosProductos);
            //inicializarCampoProductoBusqueda();
                        
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#dvModalCancelarProyecto').on('shown.bs.modal', function(event){
                if($('#dvModalNuevoProspecto').hasClass('in')){
                    $('body').addClass('modal-open');
                }
            });
                        
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#dvModalValuacionGlobal').on('show.bs.modal', function(event){
                var idCte=$(event.relatedTarget).data('idcte');
                var modo=$(event.relatedTarget).data('modo');
                if(modo==0){
                    bloquearProyecto(idCte/*_clienteDeOportunidad*/);
                }else{
                    var idVal=$(event.relatedTarget).data('idval');
                    //editarValuacionGlobal(<%=EntidadSesion.Id_Emp %>, <%=EntidadSesion.Id_Cd %>, idVal, idCte);
                    editarValuacionGlobal(_EntidadSesion_Id_Emp, _EntidadSesion_Id_Cd, idVal, idCte);
                }
            });

            //
            //
            // Visualizacion de Propuesta 
            //
            //
            $('#dvModalPropuestaTE').on('show.bs.modal', function(event){
                var idCte=$(event.relatedTarget).data('idcte');
                var idVal=$(event.relatedTarget).data('idval');

                $('#iframeVentanaPropuesta').attr('src', 'Propuestas/VisualizarPropuestas.aspx?idCte=' + idCte + '&idVal=' + idVal);
                $('#dvCuerpoVentanaPropuesta').block({message: 'Cargando...'});
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaPropuestaAcysGeneradoExitosamente(response){
                $('#dvModalPropuestaTE').modal('hide');
                PatternflyToast.showSuccess('El ACYS con folio ' + response + ' de la propuesta ha sido generado satisfactoriamente', 8000);
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaPropuesta(){
                $('#dvModalPropuestaTE').modal('hide');
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#iframeVentanaPropuesta').load(function(){
                //autoResize('iframeVentanaValuacion');
                $('#iframeVentanaPropuesta')[0].contentWindow._acysGeneradoExitosamenteCallback=cerrarVentanaPropuestaAcysGeneradoExitosamente;
                $('#iframeVentanaPropuesta')[0].contentWindow._regresarAProyectosFn=cerrarVentanaPropuesta;
                $('#dvCuerpoVentanaPropuesta').unblock();
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            $('#aMapaOferta').click(function(e){
                e.preventDefault();
                $(this).ekkoLightbox();
            });

            determinarProyectoACargar();

            $('#dvModalEditarProyecto [type="radio"]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            $('#tblCausasCancelacion [type="radio"]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });
        }

        var _modoValuacion=0;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function editarValuacion(idEmp, idCd, idVal, idCte){
            //Se utiliza para determinar la rutina a llamar después de que la operación de la valuación (nueva o edición) termine con éxito.
            _modoValuacion=1;
            $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
            $('#dvCuerpoVentanaValuacion').block({message: 'Cargando...'});
        }

        var _modoValuacionGlobal=0;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function nuevoProyecto() {
            $('#btnDvModalEditarProyectoGuardar').show();
            $('#btnDvModalEditarProyectoActualizar').hide();
            limpiarFormaNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inhabilitarCamposDeEdicion(){
            $('#dvModalEditarProyecto #selTipoCliente').prop('disabled', true);
            $('#dvModalEditarProyecto #selTipoCliente').selectpicker('refresh');

            $('#dvModalEditarProyecto #selCliente').prop('disabled', true);
            $('#dvModalEditarProyecto #txtProspecto').prop('disabled', true);
            $('#dvModalEditarProyecto #selTerritorio').prop('disabled', true);
            $('#dvModalEditarProyecto #selTerritorio').selectpicker('refresh');
            $('#dvModalEditarProyecto #selArea').prop('disabled', true);
            $('#dvModalEditarProyecto #selArea').selectpicker('refresh');
            $('#dvModalEditarProyecto #selSolucion').prop('disabled', true);
            $('#dvModalEditarProyecto #selSolucion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habilitarCamposDeEdicion(){
            $('#dvModalEditarProyecto #selTipoCliente').prop('disabled', false);
            $('#dvModalEditarProyecto #selTipoCliente').selectpicker('refresh');

            $('#dvModalEditarProyecto #selCliente').prop('disabled', false);
            $('#dvModalEditarProyecto #txtProspecto').prop('disabled', false);
            $('#dvModalEditarProyecto #selTerritorio').prop('disabled', false);
            $('#dvModalEditarProyecto #selTerritorio').selectpicker('refresh');
            $('#dvModalEditarProyecto #selArea').prop('disabled', false);
            $('#dvModalEditarProyecto #selArea').selectpicker('refresh');
            $('#dvModalEditarProyecto #selSolucion').prop('disabled', false);
            $('#dvModalEditarProyecto #selSolucion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function editarProyecto(row) {
            $('#btnDvModalEditarProyectoGuardar').hide();
            $('#btnDvModalEditarProyectoActualizar').show();
            $('#dvModalEditarProyecto').find('#myModalLabel').text('Editar Proyecto');
            var datos = row;
            _proyectoSeleccionado=datos;
            limpiarFormaNuevoProyecto();
            $('#hdnId_Op').val(datos.Id);
            $('#dvModalEditarProyecto #selTipoCliente').selectpicker('val', datos.CrmTipoCliente);
            $('#dvModalEditarProyecto #selTipoCliente').selectpicker('refresh');
            selTipoCliente_onchange($);
            switch(datos.CrmTipoCliente){
                case 1:
                    $('#dvModalEditarProyecto #txtProspecto').val(datos.NombreCte);
                    break;
                case 2:
                    $('#dvModalEditarProyecto #selCliente').val(datos.NombreCte);
                    break;
            }
            $('#hdnId_Cliente').val(datos.Id_Cte);
            $('#hdnId_CrmProspecto').val(datos.Id_CrmProspecto);
            $('#txtCantidad').val(datos.Dim_Cantidad);
            if(datos.Dim_Id_Uen!=null){
                $('#hdnDim_Id_Uen').val(datos.Dim_Id_Uen);
            }else{
                $('#hdnDim_Id_Uen').val(null);
            }
            if(datos.Dim_Id_Seg!=null){
                $('#hdnDim_Id_Seg').val(datos.Dim_Id_Seg);
            }else{
                $('#hdnDim_Id_Seg').val(null);
            }
            if(datos.Dim_Cantidad!=null){
                $('#txtCantidad').val(datos.Dim_Cantidad);
            }else{
                $('#txtCantidad').val('');
            }

            $('#txtDimension').val(datos.Dim_Descripcion);
            $('#txtVPM').val(datos.VentaPromedioMensualEsperada);

            $('#rbVtaInstalada').iCheck('uncheck');
            $('#rbVtaEsporadica').iCheck('uncheck');
            if(datos.Crm_TipoVenta==1){
                $('#rbVtaInstalada').iCheck('check');
            }else{
                $('#rbVtaEsporadica').iCheck('check');
            }
            
            
            cargarProductosDeProyecto(datos.Id, datos.Id_Cte, datos);

            inhabilitarCamposDeEdicion();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmProyecto?opId=' + datos.Id + '&idCte=' + datos.Id_Cte,
                method: 'GET',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(editarProyecto, this, row);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#hdnId_Op').val(datos.Id);
                $('#dvModalEditarProyecto #txtProspecto').val(datos.NombreCte);
                //Se establece el UEN del proyecto
                var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
                cargarTerritoriosPorRIK($, $selTerritorio, '<%=EntidadSesion.Id_Rik %>', jQuery.proxy(territoriosCargadosParaEdicion, null, $, $selTerritorio, response));
                //TODO: cargar los territorios asociados al cliente
                
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
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function limpiarFormaProyecto() {
            $('#txtProspecto').val('');
            $('#selSegmento').selectpicker('val', 0);
            $('#selArea').val(0);
            $('#selSolucion').val(0);
            $('#selAplicacion').val(0);
            $('#chkVentaNoRepetitiva').attr('checked', false);
            $('#chkPerteneceCampana').attr('checked', false);
            $('#txtVPT').val('');
            $('#txtVPE').val('');
            $('#txtVPME').val('');
        }

//        function cargarSegmentos($) {
//            $.ajax({
//                url: '<%=Page.ResolveUrl("../../WebService/PortalRIK/GestionPromocion/Proyectos.svc") %>' + '/ObtenerSegmentos',
//                method: 'GET',
//                cache: false
//            }).done(function (response, textStatus, jqXHR) {
//                var $selSegmento = $('#selSegmento');
//                $.each(response.d, function (index, element) {
//                    $selSegmento.append('<option value=' + element.Data.Id + '>' + element.Data.Descripcion + '</option>');
//                });
//                $('#selSegmento').selectpicker('refresh');
//                $('#selSegmento').selectpicker('val', -1);
//            }).fail(function (jqXHR, textStatus, error) {
//                alert(textStatus);
//            });
//        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selTipoCliente_onchange($) {
            var $selTipoCliente = $('#selTipoCliente');
            if ($($selTipoCliente).val() == 1) {
                $('#dvFgProspecto').show();
                $('#dvFgCliente').hide();
            }
            else {
                $('#dvFgProspecto').hide();
                $('#dvFgCliente').show();
            }
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarSegmento(jqelement, objSeg) {
            jqelement.append('<option value="' + objSeg.Id_Seg + '">' + objSeg.Seg_Descripcion + '</option>');
            jqelement.selectpicker('val', 0);
            jqelement.selectpicker('refresh');

            $('#dvModalEditarProyecto #hdnDim_Id_Uen').val(objSeg.Id_Uen);
            $('#dvModalEditarProyecto #hdnDim_Id_Seg').val(objSeg.Id_Seg);
            $('#dvModalEditarProyecto #txtDimension').val(objSeg.Seg_Unidades);
            $('#dvModalEditarProyecto #txtPrecioUnidad').val(objSeg.Seg_ValUniDim);
            _valorUnidadDimension = objSeg.Seg_ValUniDim;
            var cantidad=$('#dvModalEditarProyecto #txtCantidad').val();
            cantidad=isNaN(cantidad) ? 0 : cantidad;
            $('#dvModalEditarProyecto #txtVPM').val(cantidad*objSeg.Seg_ValUniDim);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarSelUEN(jqelement, objUen) {
            jqelement.append('<option value="' + objUen.Id_Uen + '">' + objUen.Uen_Descripcion + '</option>');
            jqelement.selectpicker('val', 0);
            jqelement.selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorTerritorio() {
            $('#selUEN').find('option').remove();
            $('#selUEN').selectpicker('refresh');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selTerritorio$on_change(_this) {
            var value=$(_this).selectpicker('val');
            var objTerritorio=$(_this).find('option[value="' + value + '"]').data('objterritorio');
            despopularCascadaDependientesSelectorTerritorio();
            var $selUEN = $('#dvModalEditarProyecto #selUEN');
            var $selSegmento = $('#dvModalEditarProyecto #selSegmento');
            $('#txtDimension').val('');
            $('#txtPrecioUnidad').val('');
            if(objTerritorio!=typeof(undefined) && objTerritorio!='undefined' && objTerritorio!=undefined){
                cargarSelUEN($selUEN, objTerritorio.CatUENSerializable);
                cargarSegmento($selSegmento, objTerritorio.CatSegmentoSerializable);
                selSegmento$on_change();
            }
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarUENs($, jqElement, onSuccess, onFailure) {
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatUEN/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>',
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selUEN$on_change() {
            var $selSegmento = $('#dvModalEditarProyecto #selSegmento');
            var idUen = $('#dvModalEditarProyecto #selUEN').selectpicker('val');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
            cargarSegmentos(jQuery, $selSegmento, idUen);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarSegmentos($, jqElement, idUen, onSuccess, onFailure) {
            //mostrar el indicador de operación en proceso
            $('#imgProcesandoSegmentoDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatSegmento/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idUen=' + idUen,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selSegmento$on_change() {
            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var $selArea = $('#dvModalEditarProyecto #selArea');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
            //cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(cargarAreas, null, jQuery, $selArea, idSeg));            
            cargarAreas(jQuery, $selArea, idSeg);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarTerritoriosPorRIK($, jqElement, idRik, onSuccess, onFailure) {
            $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + idRik,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarTerritoriosPorRIK, null, $, jqElement, idRik, onSuccess, onFailure);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                var $selTerritorio = jqElement;
                $selTerritorio.find('option').remove();
                $selTerritorio.append('<option value="0">--Seleccione--</option>');
                $.each(response, function (index, element) {
                    var node=$('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarTerritorios($, jqElement, idSeg, onSuccess, onFailure) {
            $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>' + '&idSeg=' + idSeg,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarAreas($, jqElement, idSeg, onSuccess, onFailure) {
            $('#imgProcesandoAreaDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatArea/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idSeg=' + idSeg,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarAreas, null, $, jqElement, idSeg, onSuccess, onFailure);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                jqElement.find('option').remove();
                jqElement.append('<option value="0">--Seleccione--</option>');
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarSoluciones($, jqElement, idArea, onSuccess, onFailure) {
            $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatSolucion/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idArea=' + idArea,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selArea$on_change() {
            var $selSolucion = $('#dvModalEditarProyecto #selSolucion');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
            cargarSoluciones(jQuery, $selSolucion, idArea);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarAplicaciones($, jqElement, idSol, onSuccess, onFailure) {
            $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeIn();
            var idUen=$('#dvModalEditarProyecto #selUEN').selectpicker('val');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            var idCte = $('#dvModalEditarProyecto #hdnId_Cliente').val();
            var idOp = $('#dvModalEditarProyecto #hdnId_Op').val();
            var idOpVar = idOp != null ? idOp : '0';
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatAplicacion/?idUen=' + idUen + '&idSeg=' + idSeg + '&idArea=' + idArea + '&idSol=' + idSol + '&idOp=' + idOpVar + '&idCte=' + idCte,
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
                    var node=$(contenidoPersonalizadoAplicacion(element, index));
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
                    
                    var apOpObj=$('#txtAplVPO_' + apId).data('obj');
                    if(apOpObj==null){
                        var apObj=$(event.target).data('obj');
                        apOpObj={
                            Id_Emp: apObj.Id_Emp, 
                            Id_Cd: '<%=EntidadSesion.Id_Cd %>', 
                            Id_Op: _proyectoSeleccionado!=null ? _proyectoSeleccionado.Id : 0, 
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
                    _aplicacionesSeleccionadas=$.grep(_aplicacionesSeleccionadas, function(value){
                        return value.Id_Apl!=apId;
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function txtCantidad$onchange(sender){
            var cantidad=$('#txtCantidad').val();
            if(isNaN(cantidad)){
                cantidad=0;
            }
            var precio=$('#dvModalEditarProyecto #txtPrecioUnidad').val();
            if(precio=='')
                precio=0;
            $('#dvModalEditarProyecto #txtVPM').val(precio*cantidad);
            var elementos=$('#lstAplicacion [item]');
            $.each(elementos, function(index, item){
                var objetoDatos=$(item).data('obj');
                $(item).find('#tdVPT').text('VPT: ' + numeral(round(objetoDatos.Apl_Potencial / 100.0 * cantidad * _valorUnidadDimension, 2)).format('$0,0.00'));
            });
        }
        
        var _aplicacionesSeleccionadas=[];

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function actualizarAplicacionesVPO(idOp, onSuccess, onFailure){
            $.each(_aplicacionesSeleccionadas, function(index, item){
                item.Id_Op=idOp;
            });
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesAplicacion',
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
            }).done(function(response, textStatus, jqXHR){
                _aplicacionesSeleccionadas = [];
                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    if(onSuccess!=null){
                        onSuccess(response, textStatus, jqXHR);
                    }
                }
            }).fail(function(jqXHR, textStatus, error){
                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    if(onFailure!=null){
                        onFailure(jqXHR, textStatus, error);
                    }
                }
            }).always(function(jqXHROrData, textStatus, errorOrJQXHR){
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function round(value, decimals){
            return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals );
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function contenidoPersonalizadoAplicacion(aplicacion, indice) {
            var cantidad=$('#txtCantidad').val();
            if(cantidad==''){
                cantidad=0;
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
                                            '<input type="text" id="txtAplVPO_' + aplicacion.Id_Apl + '" style="display: none;" class="form-control" onchange="txtAplVPO$onchange(this)" name="FormaAplicaciones[' + indice + '].VPO" data-inputmask="\'alias\' : \'currency\', \'autoUnmask\' : \'true\'">' +//aplicacion.Porcentaje/100.0 +
                                        '</div>' + 
                                    '</div>' + 
                                '</td>' + 
                                '<td style="text-align: right;">' +
                                    '<input type="checkbox" id="chkApl_' + aplicacion.Id_Apl + '" data-idapl="' + aplicacion.Id_Apl + '" onchange="chkApl_onchange(this)" chkAplicacion name="FormaAplicaciones[' + indice + '].Seleccionado"/>' +
                                '</td>' +
                            '</tr>' +
                        '</table>' +
                    '</div>'
            ;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function txtAplVPO$onchange(sender){
            var objetoDatos=$(sender).data('obj');
            objetoDatos.CrmOpAp_VPO=$(sender).val();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function chkApl_onchange(sender) {
            var chk = $(sender);
            var valoresAps = $('#selAplicacion').selectpicker('val');
            var apId = chk.data('idapl');
            if (chk.is(':checked') == true) {
                valoresAps.push(apId);
                $('#txtAplVPO_' + apId).show();
            }
            else {
                valoresAps = $.grep(valoresAps, function (value) {
                    return value != apId;
                });
                $('#txtAplVPO_' + apId).hide();
            }
            $('#selAplicacion').selectpicker('val', valoresAps);
            $('#selAplicacion').selectpicker('refresh');
        }

        function selSolucion$on_change() {
            var $selAplicacion = $('#dvModalEditarProyecto #selAplicacion');
            var idSol = $('#dvModalEditarProyecto #selSolucion').selectpicker('val');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
            cargarAplicaciones(jQuery, $selAplicacion, idSol);
        }

        function habilitarSelectorDependienteDelSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').prop('disabled', false);
            $('#selSegmento').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').find('option').remove();
            $('#selSegmento').selectpicker('refresh');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();        
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto() {
            //$('#selArea').prop('disabled', false);
            //$('#selArea').selectpicker('refresh');

            //$('#selTerritorio').prop('disabled', false);
            //$('#selTerritorio').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
            $('#selArea').selectpicker('refresh');
            $('#selTerritorio').selectpicker('refresh');

            deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
            $('#selArea').find('option').remove();
            //$('#selTerritorio').find('option').remove();
            $('#selArea').selectpicker('refresh');
            //$('#selTerritorio').selectpicker('refresh');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto() {
            //$('#selSolucion').prop('disabled', false);
            //$('#selSolucion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
            $('#selSolucion').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
            $('#selSolucion').find('option').remove();
            $('#selSolucion').selectpicker('refresh');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').prop('disabled', false);
            $('#selAplicacion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').find('option').remove();
            $('#selAplicacion').selectpicker('refresh');
            var $lstAplicacion = $('#lstAplicacion');
            $lstAplicacion.find('div').remove();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inhabilitarSelectoresDialogoNuevoProyecto() {
            deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        /// <summary>
        /// Esta función inicializa el manejador de evento del diálogo del Proyecto. Su principal función es discernir entre la naturaleza del activador del diálogo para 
        /// determinar el estado de la modalidad (edición o nuevo)        
        /// </summary>
        function inicializarModalNuevoProyecto() {
            $('#dvModalEditarProyecto').on('show.bs.modal', function (event) {
                var trigger = $(event.relatedTarget);
                var action = $(trigger).data('action');
                $('#btnDvModalEditarProyectoGuardar').prop('disabled', false);

                limpiarFormaNuevoProyecto();

                switch (action) {
                    case 1: //nuevo
                        $('#dvModalEditarProyecto').find('#myModalLabel').text('Nuevo Proyecto');
                        habilitarCamposDeEdicion();
                        nuevoProyecto();
                        break;
                    case 2:
                        $('#dvModalEditarProyecto').find('#myModalLabel').text('Editar Proyecto');
                        var row = $(trigger).data('obj');
                        editarProyecto(row);
                        $('#dvDetalles:hidden').slideDown();
                        break;
                }
            });

            $('#dvModalEditarProyecto').on('shown.bs.modal', function (event) {
                //$('#txtProspecto').autocomplete("option", "appendTo", ".eventInsForm");
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function limpiarFormaNuevoProyecto() {
            $('#dvModalEditarProyecto #txtProspecto').val('');
            $('#dvModalEditarProyecto #selCliente').val('');

            $('#dvModalEditarProyecto #selTipoCliente').selectpicker('val', 1);
            $('#dvModalEditarProyecto #selTipoCliente').selectpicker('refresh');
            selTipoCliente_onchange($);

            $('#txtVPT').val('');
            $('#txtVPE').val('');
            $('#txtVPME').val('');
            $('#hdnId_Op').val('0');
            $('#dvModalEditarProyecto #selUEN').selectpicker('val', 0);
            $('#dvModalEditarProyecto #selUEN').selectpicker('refresh');
            $('#hdnDim_Id_Uen').val(null);
            $('#hdnDim_Id_Seg').val(null);
            $('#txtCantidad').val('');
            $('#dvModalEditarProyecto #txtDimension').val('');
            $('#txtVPM').val('');

//            $('#selTerritorio').find('option').remove();
//            $('#selTerritorio').selectpicker('refresh');
            var $selTerritorio=$('#dvModalEditarProyecto #selTerritorio');
            despopularListadoTerritorio($selTerritorio);
            $selTerritorio.selectpicker('val', 0);
            $selTerritorio.selectpicker('refresh');
            despopularCascadaDependientesSelectorTerritorio();
            $('#txtCantidad').val('');

            $('#rbVtaInstalada').iCheck('check');
            $('#rbVtaEsporadica').iCheck('uncheck');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularListadoTerritorio($elemento){
            $elemento.find('option').remove();
            $elemento.append('<option value="0">--Seleccione--</option>');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function segmentosCargadosParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Seg);
            $(jqElement).selectpicker('refresh');

            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
            cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(territoriosCargadosParaEdicion, null, jQuery, $selTerritorio, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function territoriosCargadosParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Ter);
            $(jqElement).selectpicker('refresh');

            selTerritorio$on_change(jqElement);

            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var $selArea = $('#dvModalEditarProyecto #selArea');

            cargarAreas($, $selArea, idSeg, $.proxy(areasCargadasParaEdicion, null, $, $selArea, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function areasCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.ID_Area);
            $(jqElement).selectpicker('refresh');

            var $selSolucion = $('#dvModalEditarProyecto #selSolucion');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
            cargarSoluciones($, $selSolucion, idArea, $.proxy(solucionesCargadasParaEdicion, null, $, $selSolucion, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function solucionesCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Sol);
            $(jqElement).selectpicker('refresh');

            var $selAplicacion = $('#dvModalEditarProyecto #selAplicacion');
            var idSol = $('#dvModalEditarProyecto #selSolucion').selectpicker('val');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
            cargarAplicaciones($, $selAplicacion, idSol, $.proxy(aplicacionesCargadasParaEdicion, null, $, $selAplicacion, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function aplicacionesCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Aplicaciones);
            $(jqElement).selectpicker('refresh');

            $('input[chkAplicacion]').iCheck('disable');

            $.each(data.CrmOportunidadesAplicaciones/*Aplicaciones*/, function (index, element) {
                $('#chkApl_' + element.Id_Apl).iCheck('check');
                //$('#chkApl_' + element.Id_Apl).iCheck('disable');
                $('#txtAplVPO_' + element.Id_Apl).val(element.CrmOpAp_VPO);
                $('#txtAplVPO_' + element.Id_Apl).data('obj', element);
                _aplicacionesSeleccionadas.push(element);
            });

            $('#dvModalEditarProyecto #txtVPT').val(data.ValorPotencialT);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function crearProyecto() {
            $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
            $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
            $(this).prop('disabled', true);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmProyectoV2',
                type: 'POST',
                cache: false,
                data: $('#frmDvModalNuevoProyecto').serialize(),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(crearProyecto, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito');
                $('#toastSuccess').fadeIn();
                setTimeout(function () {
                    $('#toastSuccess').fadeOut();
                }, 5000);
                $('#dvModalEditarProyecto').modal('hide');
                $(_tablaProyectos.table().container()).block({message: '<img src=\'<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>\' />Actualizando'});
                _tablaProyectos.ajax.reload(function(){ $(_tablaProyectos.table().container()).unblock(); });
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
                        $('#dvModalEditarProyecto').modal('hide');
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
                $(this).prop('disabled', false);
                $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();

                $('#dvModalEditarProyecto #selUEN').prop('disabled', true);
                $('#dvModalEditarProyecto #selSegmento').prop('disabled', true);
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function crearProyectoYContinuar() {
            $(this).prop('disabled', true);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmProyecto',
                type: 'POST',
                cache: false,
                data: $('#frmDvModalNuevoProyecto').serialize(),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(crearProyecto, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito');
                $('#toastSuccess').fadeIn();
                setTimeout(function () {
                    $('#toastSuccess').fadeOut();
                }, 3000);
                $('#dvModalEditarProyecto').modal('hide');
                _cargarProductosDeProyecto(response.Id_Op, response.cliente);
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
                        $('#dvModalEditarProyecto').modal('hide');
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
                $(this).prop('disabled', false);
                $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function determinarProyectoACargar(){
            <% if (CargarDatosProyecto()) { %>
            $('#dvDetalles:hidden').slideDown();            
            //_precargarProductosDeProyecto('<%= Id_Op.ToString() %>', '<%= Id_Cliente.ToString() %>');
            _precargarProductosDeProyecto('<%= Id_Op.ToString() %>', '<%= Id_Cliente.ToString() %>');
            <% } %>
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function actualizarProyecto() {
            $(this).prop('disabled', true);
            $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
            $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
            
            $('#dvModalEditarProyecto #selTerritorio').prop('disabled', false);
            $('#dvModalEditarProyecto #selCliente').prop('disabled', false);
            $('#dvModalEditarProyecto #txtProspecto').prop('disabled', false);
            $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
            $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
            $('#dvModalEditarProyecto #txtPrecioUnidad').prop('disabled', false);
            $('#dvModalEditarProyecto #selArea').prop('disabled', false);
            $('#dvModalEditarProyecto #selSolucion').prop('disabled', false);

            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmProyecto',
                type: 'PUT',
                cache: false,
                data: $('#frmDvModalNuevoProyecto').serialize(),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(actualizarProyecto, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                _proyectoSeleccionado.Dim_Id_Uen=$('#dvModalEditarProyecto #hdnDim_Id_Uen').val();
                _proyectoSeleccionado.Dim_Id_Seg=$('#dvModalEditarProyecto #hdnDim_Id_Seg').val();
                _proyectoSeleccionado.Dim_Cantidad=$('#dvModalEditarProyecto #txtCantidad').val();
                _proyectoSeleccionado.Dim_Descripcion=$('#dvModalEditarProyecto #txtDimension').val();
                _proyectoSeleccionado.VentaPromedioMensualEsperada=$('#dvModalEditarProyecto #txtVPM').val();
                actualizarAplicacionesVPO(_proyectoSeleccionado.Id, function(){
                    $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido actualizado con éxito');
                    $('#toastSuccess').fadeIn();
                    setTimeout(function () {
                        $('#toastSuccess').fadeOut();
                    }, 3000);
                    $('#dvModalEditarProyecto').modal('hide');
                }, function(jqXHR, textStatus, error){
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
                        $('#toastWarning #toastWarningMessage').html('El proyecto ha sido actualizado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage);
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
                $(this).prop('disabled', false);
                $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
                $('#dvModalEditarProyecto #selUEN').prop('disabled', true);
                $('#dvModalEditarProyecto #selSegmento').prop('disabled', true);

                $('#dvModalEditarProyecto #selTerritorio').prop('disabled', true);
                $('#dvModalEditarProyecto #selCliente').prop('disabled', true);
                $('#dvModalEditarProyecto #txtProspecto').prop('disabled', true);
                $('#dvModalEditarProyecto #selUEN').prop('disabled', true);
                $('#dvModalEditarProyecto #selSegmento').prop('disabled', true);
                $('#dvModalEditarProyecto #txtPrecioUnidad').prop('disabled', true);
                $('#dvModalEditarProyecto #selArea').prop('disabled', true);
                $('#dvModalEditarProyecto #selSolucion').prop('disabled', true);

            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function _buscarProspecto(request, response) {
            var terminoDeBusqueda = $('#txtProspecto').val();
            var $imgProspectoEnOperacion = $('#dvModalEditarProyecto #imgProspectoEnOperacion');
            $imgProspectoEnOperacion.show();
            var data = null;
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmProspecto?terminoDeBusqueda=' + terminoDeBusqueda,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(_buscarProspecto, this, request, response);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                data = $.map(response, function (p) {
                    return { value: p.Id_CrmProspecto, label: p.Id_CrmProspecto + ' - ' + p.Cte_NomComercial, data: p };
                });
            }).fail(function (jqXHR, textStatus, error) {
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                response(data);
                $imgProspectoEnOperacion.hide();
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function _buscarCliente(request, response) {
            var terminoDeBusqueda = $('#selCliente').val();
            var idTer=$('#dvModalEditarProyecto #selTerritorio').selectpicker('val');
            var data = null;
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatCliente?terminoDeBusqueda=' + terminoDeBusqueda + '&idTer=' + idTer,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(_buscarCliente, this, request, response);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                data = $.map(response, function (p) {
                    return { value: p.Id_Cte, label: p.Id_Cte + ' - ' + p.Cte_NomComercial };
                });
            }).fail(function (jqXHR, textStatus, error) {
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                response(data);
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarCampoProspecto() {
            $('#txtProspecto').autocomplete({
                source: function (request, response) {
                    _buscarProspecto(request, response);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $('#txtProspecto').val(ui.item.label);
                    $('#hdnId_CrmProspecto').val(ui.item.value);
                    $('#hdnId_Cliente').val(ui.item.data.Id_Cte);
                    var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
                    cargarTerritoriosDeProspecto($, $selTerritorio, ui.item.data.Id_Rik, ui.item.value,
                        function(){
                            selTerritorio$on_change($selTerritorio);
                        },
                        function(){
                        }
                    );
                }
            });
            $("#txtProspecto").attr('autocomplete', 'on');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarCampoCliente() {
            $('#selCliente').autocomplete({
                source: function (request, response) {
                    _buscarCliente(request, response);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $('#selCliente').val(ui.item.label);
                    $('#hdnId_Cliente').val(ui.item.value);
                }
            });
            $("#selCliente").attr('autocomplete', 'on');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function _buscarProducto(request, response) {            
            var terminoDeBusqueda = $('#txtProductoBusqueda').val();
            var data = null;
            $('#imgBuscandoProducto').show();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmCatalogoUnico?idCte=' + _clienteDeOportunidad + '&idOp=' + _oportunidadSeleccionada + '&terminoBusqueda=' + terminoDeBusqueda,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(_buscarProducto, this, request, response);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                data = $.map(response, function (p) {
                    return { value: p.Id_Prd, label: p.Id_Prd + ' - ' + p.DescripcionProducto, data: p };
                });
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
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                $('#imgBuscandoProducto').hide();
                response(data);
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarInfoProductoCatalogoUnico($container, id){            
            $container.find('#imgBuscandoProducto').fadeIn();
            $container.find('#txtProductoDescripcion').val('');
            $container.find('#hdnProductoBusqueda').val('');
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/BusquedaOtrosProductosCatalogoUnico/?idPrd=' + id,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarInfoProductoCatalogoUnico, null, $container, idPrd);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                if(response!=null){
                    if(response.length>0){
                        var entradas=$.map(response, function(element, idx){
                            return {label: element.NombreProducto + ' - ' + element.Ruta, value: element.Id_Prd, data: element};
                        });
                        $container.find('#txtProductoDescripcion').autocomplete('option', 'source', entradas);
                        $container.find('#spanProductoDescripcionHlp').hide();
                        $container.find('#tdProductoDescripcion').removeClass('has-error');
                        $container.find('#txtProductoCantidad').attr('disabled', false);
                        
                        $container.find('#btnAgregarProducto').attr('disabled', false);
                        $container.find('#txtProductoDescripcion').autocomplete('search', '');
                    }else{
                        $container.find('#tdProductoDescripcion').addClass('has-error');
                        $container.find('#spanProductoDescripcionHlp').show();
                        $container.find('#txtProductoCantidad').attr('disabled', true);
                        $container.find('#tdProductoDescripcion').val('');
                        $container.find('#btnAgregarProducto').attr('disabled', true);
                    }
                    
                }else{
                    //TODO: mostrar una señal para indicar que el producto no fué encontrado
                    $container.find('#tdProductoDescripcion').addClass('has-error');
                    $container.find('#spanProductoDescripcionHlp').show();
                    $container.find('#txtProductoCantidad').attr('disabled', true);
                    $container.find('#tdProductoDescripcion').val('');
                    $container.find('#btnAgregarProducto').attr('disabled', true);
                }
            }).fail(function (jqXHR, textStatus, error) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                        break;
                }
                //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);
                alertify.error('Ocurrió un error al obtener la información del producto');
            }).always(function (jqXHR, textStatus, errorThrown) {
                $container.find('#imgBuscandoProducto').fadeOut();
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarInfoProducto(id){            
            $('#imgBuscandoProducto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatProducto/?id=' + id,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarInfoProducto, null, id);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                if(response!=null){
                    $('#spanProductoDescripcionHlp').hide();
                    $('#tdProductoDescripcion').removeClass('has-error');
                    $('#txtProductoCantidad').attr('disabled', false);
                    $('#txtProductoDescripcion').val(response.Prd_Descripcion);
                    $('#hdnProductoBusqueda').val(response.Id_Prd);
                    $('#btnAgregarProducto').attr('disabled', false);

                }else{
                    //TODO: mostrar una señal para indicar que el producto no fué encontrado
                    $('#tdProductoDescripcion').addClass('has-error');
                    $('#spanProductoDescripcionHlp').show();
                    $('#txtProductoCantidad').attr('disabled', true);
                    $('#tdProductoDescripcion').val('');
                    $('#btnAgregarProducto').attr('disabled', true);
                }
            }).fail(function (jqXHR, textStatus, error) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                        break;
                }
                //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);
                alertify.error('Ocurrió un error al obtener la información del producto');
            }).always(function (jqXHR, textStatus, errorThrown) {
                $('#imgBuscandoProducto').fadeOut();
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $cargarInfoProducto($contenedor, id){            
            $contenedor.find('#imgBuscandoProducto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CatProducto/?id=' + id,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy($cargarInfoProducto, null, $contenedor, id);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                if(response!=null){
                    $contenedor.find('#spanProductoDescripcionHlp').hide();
                    $contenedor.find('#tdProductoDescripcion').removeClass('has-error');
                    $contenedor.find('#txtProductoCantidad').attr('disabled', false);
                    $contenedor.find('#txtProductoDescripcion').val(response.Prd_Descripcion);
                    $contenedor.find('#hdnProductoBusqueda').val(response.Id_Prd);
                    $contenedor.find('#btnAgregarProducto').attr('disabled', false);

                }else{
                    //TODO: mostrar una señal para indicar que el producto no fué encontrado
                    $contenedor.find('#tdProductoDescripcion').addClass('has-error');
                    $contenedor.find('#spanProductoDescripcionHlp').show();
                    $contenedor.find('#txtProductoCantidad').attr('disabled', true);
                    $contenedor.find('#tdProductoDescripcion').val('');
                    $contenedor.find('#btnAgregarProducto').attr('disabled', true);
                }
            }).fail(function (jqXHR, textStatus, error) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                        break;
                }
                //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);                
                alertify.error('Ocurrió un error al obtener la información del producto');
            }).always(function (jqXHR, textStatus, errorThrown) {
                $contenedor.find('#imgBuscandoProducto').fadeOut();
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Producto busqueda
        function $cargarInfoProductoAplicacion($contenedor, idCte, idOp, idPrd){            
            $contenedor.find('#imgBuscandoProducto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/BusquedaProductoCatalogoUnico/?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy($cargarInfoProductoAplicacion, null, $contenedor, idCte, idOp, idPrd);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                if(response!=null){
                    $contenedor.find('#spanProductoDescripcionHlp').hide();
                    $contenedor.find('#tdProductoDescripcion').removeClass('has-error');
                    $contenedor.find('#txtProductoCantidad').attr('disabled', false);
                    $contenedor.find('#txtProductoDescripcion').val(response.CatProductoSerializable.Prd_Descripcion);
                    $contenedor.find('#hdnProductoBusqueda').val(response.CatProductoSerializable.Id_Prd);
                    $contenedor.find('#btnAgregarProducto').attr('disabled', false);

                }else{
                    //TODO: mostrar una señal para indicar que el producto no fué encontrado
                    /*$contenedor.find('#tdProductoDescripcion').addClass('has-error');
                    $contenedor.find('#spanProductoDescripcionHlp').show();
                    $contenedor.find('#txtProductoCantidad').attr('disabled', true);
                    $contenedor.find('#tdProductoDescripcion').val('');
                    $contenedor.find('#btnAgregarProducto').attr('disabled', true);*/
                    alertify.error('El producto '+idPrd+' no se encuentra o no existe.');

                }
            }).fail(function (jqXHR, textStatus, error) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                        break;
                }
                //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);
                alertify.error('Ocurrió un error al obtener la información del producto');
            }).always(function (jqXHR, textStatus, errorThrown) {
                $contenedor.find('#imgBuscandoProducto').fadeOut();
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inicializarCampoProductoBusqueda() {
            
            $('#txtProductoBusqueda').blur(function(eventObject){
                var id=$(this).val();
                if(id!=''){
                    cargarInfoProducto(id);
                }
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $inicializarCampoProductoBusquedaOtrosProductos($container){
            
            $container.find('#txtProductoDescripcion').autocomplete({
                minLength: 0,
                select: function(event, ui){
                    event.preventDefault();
                    $container.find('#txtProductoDescripcion').val(ui.item.data.NombreProducto + ' (' + ui.item.label + ')');
                    $container.find('#hdnProductoBusqueda').val(ui.item.value);
                    _productoElegido=ui.item.data;
                    $asignarValoresACamposDeFormaParaAgregarProducto($container, _productoElegido);
                }
            });
            $container.find('#txtProductoBusqueda').blur(function(eventObject){
                var id=$(this).val();
                if(id!=''){                    
                    cargarInfoProductoCatalogoUnico($container, id);
                }
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $inicializarCampoProductoBusqueda($container) {            
            ///<summary>Inicializa el comportamiento del campo de búsqueda de producto en la sección que lo contiene especificada por $container</summary>
            ///<param name="$container" type="jqNode">Nodo contenedor del campo</param>
            $container.find('#txtProductoBusqueda').blur(function(eventObject){
                var id=$(this).val();
                if(id!=''){
                    $cargarInfoProducto($container, id);
                }
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $inicializarCampoProductoAplicacionBusqueda($container) {            
            ///<summary>Inicializa el comportamiento del campo de búsqueda de producto en la sección que lo contiene especificada por $container</summary>
            ///<param name="$container" type="jqNode">Nodo contenedor del campo</param>
            $container.find('#txtProductoBusqueda').blur(function(eventObject){
                var id=$(this).val();
                if(id!=''){
                    $cargarInfoProductoAplicacion($container, _proyectoSeleccionado.Id_Cte, _proyectoSeleccionado.Id, id);
                }
            });
        }

        var _productoElegido = null;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function _inicializarCampoProductoBusqueda() {
            
            $('#txtProductoBusqueda').autocomplete({
                source: function (request, response) {
                    _buscarProducto(request, response);
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $('#txtProductoBusqueda').val(ui.item.label);
                    $('#hdnProductoBusqueda').val(ui.item.value);
                    _productoElegido=ui.item.data;
                    asignarValoresACamposDeFormaParaAgregarProducto(_productoElegido);
                }
            });
            $("#selCliente").attr('autocomplete', 'on');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function asignarValoresACamposDeFormaParaAgregarProducto(data){
            $('#hdnAgregarProducto_Id_Uen').val(data.Id_Uen);
            $('#hdnAgregarProducto_Id_Seg').val(data.Id_Seg);
            $('#hdnAgregarProducto_Id_Area').val(data.Id_Area);
            $('#hdnAgregarProducto_Id_Sol').val(data.Id_Sol);
            $('#hdnAgregarProducto_Id_Apl').val(data.Id_Apl);
            $('#hdnAgregarProducto_Id_SubFam').val(data.Id_SubFam);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $asignarValoresACamposDeFormaParaAgregarProducto($container, data){            
            $container.find('#hdnAgregarProducto_Id_Uen').val(data.Id_Uen);
            $container.find('#hdnAgregarProducto_Id_Seg').val(data.Id_Seg);
            $container.find('#hdnAgregarProducto_Id_Area').val(data.Id_Area);
            $container.find('#hdnAgregarProducto_Id_Sol').val(data.Id_Sol);
            $container.find('#hdnAgregarProducto_Id_Apl').val(data.Id_Apl);
            $container.find('#hdnAgregarProducto_Id_SubFam').val(data.Id_SubFam);
        }

        var _oportunidadSeleccionada=null;
        var _clienteDeOportunidad = null;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function limpiarListadoDeProductos() {
            
            var $lstProductos = $('#lstProductos');
            $lstProductos.find('[elementoDeLista]').remove();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $limpiarListadoDeProductos($container) {
            
            var $lstProductos = $container.find('#lstProductos');
            $lstProductos.find('[elementoDeLista]').remove();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarInfoSeccionGeneral(datos){
            
            $('#ddSegmento').text(datos.Seg_Descripcion);
            $('#ddArea').text(datos.Area_Descripcion);
            $('#ddSolucion').text(datos.Sol_Descripcion);
            $('#ddVPT').text('$' + datos.ValorPotencialTeorico);
            $('#ddVPME').text('$' + datos.VentaPromedioMensualEsperada);

            //deshabilitar todos los comandos de cambio de estado
            $('.wizard .nav-tabs li').addClass('disabled');
            //simular el evento click del comando asociado al estado en el que se encuentra el proyecto
            var tabSelector='.wizard .nav-tabs li:eq(_index_)';
            tabSelector=tabSelector.replace('_index_', datos.Estatus-1);
            var $element=$(tabSelector);
            $($element).removeClass('disabled');
            $($element).find('a[data-toggle="tab"]').click();
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function precargarProyectoSeleccionado(idCte, idOp){
            
            //_proyectoSeleccionado=
            $('#tblProyectos').DataTable().rows().every(function(rowIdx, tableLoop, rowLoop){
                var data=this.data();
                if(data.Cliente==idCte && data.Id==idOp){
                    var node=this.node();
                    _proyectoSeleccionado=data;
                    $(node).addClass('active');
                    _lastSelectedNode=node;
                    cargarInfoSeccionGeneral(_proyectoSeleccionado);
                }
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function actualizarComandosValuacion(datosProyecto){
            
            if(datosProyecto.CrmOp_PuedeGenerarValuacion==true){
                $('#btnGenerarValuacion').show();
            }else{
                $('#btnGenerarValuacion').hide();
            }

            if(datosProyecto.CrmOp_PuedeGenerarPE==true){
                $('#btnGenerarPE').show();
            }else{
                $('#btnGenerarPE').hide();
            }
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function obtenerRutaDeOferta(idCte, idOp){
            
            //deshabilitar y abilitar el comando para agregar productos al proyecto
            $('#btnAgregarProducto').attr('disabled', true);
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/OfertaPromocion/?idCte=' + idCte + '&idOp=' + idOp,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(obtenerRutaDeOferta, null, idCte, idOp);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#btnAgregarProducto').attr('disabled', false);
                asignarValoresACamposDeFormaParaAgregarProducto(response);
            }).fail(function (jqXHR, textStatus, error) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                }                
                alertify.error('Ocurrió un error al obtener la información del producto');
                //establecer un tooltip en el comando para señalar el error asociado.
            }).always(function (jqXHR, textStatus, errorThrown) {
                
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $obtenerRutaDeOferta($container, idCte, idOp){            
            //deshabilitar y abilitar el comando para agregar productos al proyecto
            $('#btnAgregarProducto').attr('disabled', true);
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/OfertaPromocion/?idCte=' + idCte + '&idOp=' + idOp,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy($obtenerRutaDeOferta, null, $container, idCte, idOp);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $container.find('#btnAgregarProducto').attr('disabled', false);
                $asignarValoresACamposDeFormaParaAgregarProducto($container, response);
            }).fail(function (jqXHR, textStatus, error) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                        break;
                }                
                alertify.error('Ocurrió un error al obtener la información del producto');                
            }).always(function (jqXHR, textStatus, errorThrown) {                
            });
        }

        var _lastSelectedNode=null;
        var _proyectoSeleccionado=null;
        var _$campoDescripcionActual=null;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, datos) {
            
            _proyectoSeleccionado=datos;
            
            //Se evalua si se debe de ocultar el área de productos de aplicación
            if(_proyectoSeleccionado.Area==-1){
                $('#contenedorProductosDeAplicacion').hide();
            }else{
                $('#contenedorProductosDeAplicacion').show();
            }

            cargarInfoSeccionGeneral(datos);
            _oportunidadSeleccionada = oportunidadSeleccionada;
            _clienteDeOportunidad = clienteDeOportunidad;

//            obtenerRutaDeOferta(clienteDeOportunidad, oportunidadSeleccionada);
            $('#dvDetalles:hidden').slideDown();
            $('#imgCargandoProductos').fadeIn();
            cargarListadoDeProductos(oportunidadSeleccionada, clienteDeOportunidad, _proyectoSeleccionado);
//            $.ajax({
//                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
//                cache: false,
//                type: 'GET',
//                statusCode: {
//                    401: function (jqXHR, textStatus, errorThrown) {
//                        $('#dvDialogoInicioSesion').modal();
//                        _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, datos);
//                    }
//                }
//            }).done(function (response, textStatus, jqXHR) {
//                _totalProductos=response.length;
//                $('#txtProductoCantidad').attr('disabled', true);
//                $('#btnAgregarProducto').attr('disabled', true);
//                if(response.length>0){
//                    $('#productosBlankSlate').hide();
//                    $('#contenidoSeccionProductos').show();
//                    var $lstProductos = $('#lstProductos');
//                    $.each(response, function (index, element) {
//                        var n = crearElementoDeListadoDeProductos(element);
//                        $lstProductos.append(n);
//                        var lstElem=$lstProductos.find('#lstElem_' + element.Id_Prd);
//                        lstElem.data('objetodatos', element);
//                    });
//                }else{
//                    $('#contenidoSeccionProductos').hide();
//                    $('#productosBlankSlate').show();
//                }
//                //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
//                $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
//                $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);
//                
//            }).fail(function (jqXHR, textStatus, error) {
//                switch (jqXHR.status) {
//                    case 401:
//                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
//                        break;
//                    default:
//                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
//                        $('#toastDanger').fadeIn();
//                        setTimeout(function () {
//                            $('#toastDanger').fadeOut();
//                        }, 3000);
//                        break;
//                }
//            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
//                $('#imgCargandoProductos').fadeOut();

//            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
        function cargarListadoDeProductos(idOp, idCte, datosProyecto){            
            
            _cargarProductos(idOp, idCte, function(response, status, jqXHR){

                //Productos de la aplicación
                var productosDeAplicacion=$.grep(response, function(element, index){
                    return  element.Id_Apl==datosProyecto.Id_Apl 
                            && element.Id_Area==datosProyecto.Id_Area 
                            && element.Id_Sol==datosProyecto.Id_Sol 
                            && element.Id_Seg==datosProyecto.Id_Seg 
                            && element.Id_Uen==datosProyecto.IdUen;
                });
                
                // Productos que no pertenecen a la aplicación: 
                var otrosProductos=$.grep(response, function(element, index){
                    var elementosEncontrados=$.grep(productosDeAplicacion, function(elementProductosDeAplicacion, index){
                        return element==elementProductosDeAplicacion;
                    });
                    return elementosEncontrados.length==0;
                });

                //Se inicializan los listados de productos de la aplicación y otros productos.
                var $contenedorListadoProductosAplicacion=$('#contendorProductos');
                $contenedorListadoProductosAplicacion.data('_modeloListado_', _totalProductos);
                var $contenedorListadoOtrosProductos=$('#contenedorOtrosProductos');
                $contenedorListadoOtrosProductos.data('_modeloListado_', _totalOtrosProductos);

                _totalProductos.i=productosDeAplicacion.length;
                _totalOtrosProductos.i=otrosProductos.length;

                inicializarApartadoProductos($contenedorListadoProductosAplicacion, productosDeAplicacion);
                inicializarApartadoProductos($contenedorListadoOtrosProductos, otrosProductos);

                $obtenerRutaDeOferta($contenedorListadoProductosAplicacion, _clienteDeOportunidad, _oportunidadSeleccionada);
                $obtenerRutaDeOferta($contenedorListadoOtrosProductos, _clienteDeOportunidad, _oportunidadSeleccionada);

            },function(jqXHR, status, error){
            },function(jqXHR, status, error){
                $('#imgCargandoProductos').fadeOut();
            },
            {}
            );
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function _cargarProductos(idOp, idCte, onSuccess, onFail, always, statusCodeHandlers){                        
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + idOp + '&Id_Cte=' + idCte,
                cache: false,
                type: 'GET',
                statusCode: statusCodeHandlers
            }).done(function (response, textStatus, jqXHR) {
                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    onSuccess(response, textStatus, jqXHR);
                }
            }).fail(function (jqXHR, textStatus, error) {
                if (typeof (onFail) != undefined && typeof (onFail) != 'undefined') {
                    onFail(jqXHR, textStatus, error);
                }
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                if (typeof (always) != undefined && typeof (always) != 'undefined') {
                    always();
                }
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Productos
        function inicializarApartadoProductos($contenedor, dataItems){            
            //Se limpia el listado
            $limpiarCamposBusquedaProducto($contenedor);
            $limpiarListadoDeProductos($contenedor);
            $contenedor.find('#txtProductoCantidad').attr('disabled', true);
            $contenedor.find('#btnAgregarProducto').attr('disabled', true);
            if(dataItems.length>0){            
                //$contenedor.find('#productosBlankSlate').hide(); RFH               
                $contenedor.find('#contenidoSeccionProductos').show();
                var $lstProductos = $contenedor.find('#lstProductos');
                $.each(dataItems, function (index, element) {
                    var n = $crearElementoDeListadoDeProductos($contenedor[0].id, element);
                    $lstProductos.append(n);
                    var lstElem=$lstProductos.find('#lstElem_' + element.Id_Prd);
                    lstElem.data('objetodatos', element);
                });
            }else{
                $contenedor.find('#contenidoSeccionProductos').hide();
                $contenedor.find('#productosBlankSlate').show();
            }
            //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
            $contenedor.find('#hdnAgregarProducto_Id_Op').val(_oportunidadSeleccionada);
            $contenedor.find('#hdnAgregarProducto_Id_Cte').val(_clienteDeOportunidad);
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function _cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, onSuccess) {
            //actualizarComandosValuacion(_proyectoSeleccionado);
            cargarInfoSeccionGeneral(_proyectoSeleccionado);
            _oportunidadSeleccionada = oportunidadSeleccionada;
            _clienteDeOportunidad = clienteDeOportunidad;
            limpiarListadoDeProductos();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, onSuccess);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                var $lstProductos = $('#lstProductos');
                $.each(response, function (index, element) {
                    var n = crearElementoDeListadoDeProductos(element);
                    $lstProductos.append(n);
                    var lstElem=$lstProductos.find('#lstElem_' + element.Id_Prd);
                    lstElem.data('objetodatos', element);
                });

                //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
                $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
                $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);

                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    onSuccess();
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
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {

            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function _precargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad) {
            _oportunidadSeleccionada = oportunidadSeleccionada;
            _clienteDeOportunidad = clienteDeOportunidad;
            limpiarListadoDeProductos();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, onSuccess);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                var $lstProductos = $('#lstProductos');
                $.each(response, function (index, element) {
                    var n = crearElementoDeListadoDeProductos(element);
                    $lstProductos.append(n);
                    var lstElem=$lstProductos.find('#lstElem_' + element.Id_Prd);
                    lstElem.data('objetodatos', element);
                });

                //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
                $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
                $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);

                precargarProyectoSeleccionado(clienteDeOportunidad, oportunidadSeleccionada);

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
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {

            });
        }

        var _totalProductos={i: 0};
        var _totalOtrosProductos={i: 0};
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function agregarProducto(_this){
            
            $(_this).prop('disabled', true);
            $('#imgAgregandoProducto').show();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos',
                type: 'POST',
                data: $('#frmAgregarProducto').serialize(),
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(agregarProducto, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                var $lstProductos = $('#lstProductos');
                var n = crearElementoDeListadoDeProductos(response);
                $lstProductos.append(n);
                var lstElem=$lstProductos.find('#lstElem_' + response.Id_Prd);
                lstElem.data('objetodatos', response);
                $('#txtProductoBusqueda').val('');
                $('#txtProductoCantidad').val('');
                $('#txtProductoDescripcion').val('');
                $('#tdProductoDescripcion').removeClass('has-error');
                $('#spanProductoDescripcionHlp').hide();
                $('#btnAgregarProducto').attr('disabled', true);                
                if(_totalProductos.i==0){
                    $('#productosBlankSlate').fadeOut();
                    $('#contenidoSeccionProductos').fadeIn();
                }
                _totalProductos.i=_totalProductos.i+1;
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
                $(_this).prop('disabled', false);
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                $('#imgAgregandoProducto').hide();
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // PRODUCTO - AGREGAR de APLICACION         
        function $agregarProductoAplicacion($container, _this){            

            var ProductoClave = $container.find('#txtProductoBusqueda').val();
            var ProductoCantidad = $container.find('#txtProductoCantidad').val();
            
            if (ProductoClave=='' || ProductoCantidad=='') {
                //PatternflyToast.showError('La cantidad o clave de producto no es valido.', 10000);   
                alertify.error('La cantidad o clave de producto no es valido.');
            } else {
                $(_this).prop('disabled', true);
                $container.find('#imgAgregandoProducto').show();
                $.ajax({
                    url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos',
                    type: 'POST',
                    data: $container.find('#frmAgregarProducto').serialize(),
                    cache: false,
                    statusCode: {
                        401: function (jqXHR, textStatus, errorThrown) {
                            $('#dvDialogoInicioSesion').modal();
                            _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $container, _this);
                        }
                    }
                }).done(function (response, textStatus, jqXHR) {
                    var $lstProductos = $container.find('#lstProductos');
                    var n = $crearElementoDeListadoDeProductos($container[0].id, response);
                    $lstProductos.append(n);
                    var lstElem=$lstProductos.find('#lstElem_' + response.Id_Prd);
                    lstElem.data('objetodatos', response);
                    $container.find('#txtProductoBusqueda').val('');
                    $container.find('#txtProductoCantidad').val('');
                    $container.find('#txtProductoDescripcion').val('');
                    $container.find('#tdProductoDescripcion').removeClass('has-error');
                    $container.find('#spanProductoDescripcionHlp').hide();
                    $container.find('#btnAgregarProducto').attr('disabled', true);
                    if(_totalProductos.i==0){
                        $container.find('#productosBlankSlate').fadeOut();
                        $container.find('#contenidoSeccionProductos').fadeIn();
                    }
                    _totalProductos.i=_totalProductos.i+1;
                }).fail(function (jqXHR, textStatus, error) {
                    switch (jqXHR.status) {
                        case 401:
                            alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                            break;
                        default:
                            //PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                            alertify.error(jqXHR.responseJSON.Message);
                            break;
                    }
                    $(_this).prop('disabled', false);
                }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                    $container.find('#imgAgregandoProducto').hide();
                });
            }
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
        // PRODUCTO AGREGAR OTROS        
        function $agregarProductoOtro($container, $tipo,  _this){            
            var ProductoClave = $container.find('#txtProductoBusqueda').val();
            var ProductoCantidad = $container.find('#txtProductoCantidad').val();
            var ProductoDescripcion= $container.find('#txtProductoDescripcion').val();
            var Id_Prd = $container.find('#hdnProductoBusqueda').val();             
            
            if (ProductoClave=='' || ProductoCantidad=='' || ProductoDescripcion=='') {
                //PatternflyToast.showError('La cantidad o clave de producto no es valido.', 10000);   
                alertify.error('La cantidad o clave de producto no es valido.');   
            } else {                      
                //alert(ProductoClave);               

                $(_this).prop('disabled', true);
                $container.find('#imgAgregandoProducto').show();
                $.ajax({
                    url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos',
                    type: 'POST',
                    data: $container.find('#frmAgregarProducto').serialize(),
                    cache: false,
                    statusCode: {
                        401: function (jqXHR, textStatus, errorThrown) {
                            $('#dvDialogoInicioSesion').modal();
                            _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $container, _this);
                        }
                    }
                }).done(function (response, textStatus, jqXHR) {

                    var $lstProductos = $container.find('#lstProductos');
                    var n = $crearElementoDeListadoDeProductos($container[0].id, response);
                    $lstProductos.append(n);
                    var lstElem=$lstProductos.find('#lstElem_' + response.Id_Prd);
                    lstElem.data('objetodatos', response);
                    $container.find('#txtProductoBusqueda').val('');
                    $container.find('#txtProductoCantidad').val('');
                    $container.find('#txtProductoDescripcion').val('');
                    $container.find('#tdProductoDescripcion').removeClass('has-error');
                    $container.find('#spanProductoDescripcionHlp').hide();
                    $container.find('#btnAgregarProducto').attr('disabled', true);
                    if(_totalOtrosProductos.i==0){
                        $container.find('#productosBlankSlate').fadeOut();
                        $container.find('#contenidoSeccionProductos').fadeIn();
                    }
                    _totalOtrosProductos.i=_totalOtrosProductos.i+1;
                }).fail(function (jqXHR, textStatus, error) {
                    switch (jqXHR.status) {
                        case 401:
                            alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                            break;
                        default:
                            //PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                            alertify.error(jqXHR.responseJSON.Message);
                            break;
                    }
                    $(_this).prop('disabled', false);
                }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                    $container.find('#imgAgregandoProducto').hide();
                });
            }
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function editarCantidad(idPrd) {
            var dvCantidadDisplay = $('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $('#dvCantidadEdit_' + idPrd);

            var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
            var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
            txtCantidadEdit.val(dvCantidadDisplayValue.text());

            dvCantidadDisplay.hide();
            dvCantidadEdit.show();

            var dvDilucionDisplay = $('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $('#dvDilucionEdit_' + idPrd);

            var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
            var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');
            txtDilucionEdit.val(dvDilucionDisplayValue.text());

            dvDilucionDisplay.hide();
            dvDilucionEdit.show();
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $editarCantidad($contenedor, idPrd) {
            var dvCantidadDisplay = $contenedor.find('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $contenedor.find('#dvCantidadEdit_' + idPrd);

            var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
            var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
            txtCantidadEdit.val(dvCantidadDisplayValue.text());

            dvCantidadDisplay.hide();
            dvCantidadEdit.show();

            var dvDilucionDisplay = $contenedor.find('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $contenedor.find('#dvDilucionEdit_' + idPrd);

            var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
            var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');
            txtDilucionEdit.val(dvDilucionDisplayValue.text());

            dvDilucionDisplay.hide();
            dvDilucionEdit.show();

        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function aceptarEditarCantidad(idPrd) {
            var dvCantidadDisplay = $('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $('#dvCantidadEdit_' + idPrd);
            var dvDilucionDisplay = $('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $('#dvDilucionEdit_' + idPrd);

            var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
            var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
            var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
            var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');
            
            var $lstElem = $('#lstProductos #lstElem_' + idPrd);
            var dataObject=$lstElem.data('objetodatos');
            var objectCopy=jQuery.extend(true, {}, dataObject);
            objectCopy.COP_Cantidad = txtCantidadEdit.val();
            objectCopy.COP_Dilucion = txtDilucionEdit.val();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos', //?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
                type: 'PUT',
                cache: false,
                data: JSON.stringify(objectCopy),
                contentType: 'application/json',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(aceptarEditarCantidad, this, idPrd);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                dvCantidadDisplay.show();
                dvCantidadEdit.hide();
                dvCantidadDisplayValue.text(txtCantidadEdit.val());

                dvDilucionDisplay.show();
                dvDilucionEdit.hide();
                dvDilucionDisplayValue.text(txtDilucionEdit.val());
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
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $aceptarEditarCantidad($contenedor, idPrd) {
            var dvCantidadDisplay = $contenedor.find('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $contenedor.find('#dvCantidadEdit_' + idPrd);
            var dvDilucionDisplay = $contenedor.find('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $contenedor.find('#dvDilucionEdit_' + idPrd);

            var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
            var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
            var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
            var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');
            
            //var $lstElem = $contenedor.find('#lstProductos #lstElem_' + idPrd);
            var dataObject=$contenedor.data('objetodatos');
            var objectCopy=jQuery.extend(true, {}, dataObject);
            objectCopy.COP_Cantidad = txtCantidadEdit.val();
            objectCopy.COP_Dilucion = txtDilucionEdit.val();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos', //?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
                type: 'PUT',
                cache: false,
                data: JSON.stringify(objectCopy),
                contentType: 'application/json',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy($aceptarEditarCantidad, this, $contenedor, idPrd);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                dvCantidadDisplay.show();
                dvCantidadEdit.hide();
                dvCantidadDisplayValue.text(txtCantidadEdit.val());

                dvDilucionDisplay.show();
                dvDilucionEdit.hide();
                dvDilucionDisplayValue.text(txtDilucionEdit.val());
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
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cancelarEditarCantidad(idPrd){
            var dvCantidadDisplay = $('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $('#dvCantidadEdit_' + idPrd);
            dvCantidadEdit.hide();
            dvCantidadDisplay.show();

            var dvDilucionDisplay = $('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $('#dvDilucionEdit_' + idPrd);
            dvDilucionEdit.hide();
            dvDilucionDisplay.show();
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $cancelarEditarCantidad($contenedor, idPrd){
            var dvCantidadDisplay = $contenedor.find('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $contenedor.find('#dvCantidadEdit_' + idPrd);
            dvCantidadEdit.hide();
            dvCantidadDisplay.show();

            var dvDilucionDisplay = $contenedor.find('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $contenedor.find('#dvDilucionEdit_' + idPrd);
            dvDilucionEdit.hide();
            dvDilucionDisplay.show();
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function retirarProducto(idCte, idOp, idPrd){
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
                type: 'DELETE',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(retirarProducto, this, idCte, idOp, idPrd);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                var $lstProductos = $('#lstProductos');
                var elem=$lstProductos.find('#lstElem_' + idPrd);
                elem.remove();

                _totalProductos.i=_totalProductos.i-1;
                if(_totalProductos.i==0){
                    $('#contenidoSeccionProductos').fadeOut();
                    $('#productosBlankSlate').fadeIn();
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
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function retirarProductoSvc(idCte, idOp, idPrd, onSuccess, onFailed, always, statusCodeHandlers){            
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
                type: 'DELETE',
                cache: false,
                statusCode: statusCodeHandlers
            }).done(function (response, textStatus, jqXHR) {
                if(typeof(onSuccess)!=undefined && typeof(onSuccess)!='undefined'){
                    onSuccess(response, textStatus, jqXHR);
                }
            }).fail(function (jqXHR, textStatus, error) {
                if(typeof(onFailed)!=undefined && typeof(onFailed)!='undefined'){
                    onFailed(jqXHR, textStatus, error);
                }
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                if(typeof(always)!=undefined && typeof(always)!='undefined'){
                    always(jqXHROrData, textStatus, errorOrJQXHR);
                }
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Productos - Retirar
        function $retirarProducto($container, idCte, idOp, idPrd){            
            retirarProductoSvc(idCte, idOp, idPrd, 
            function(response, textStatus, jqXHR){
                var $lstProductos = $container.find('#lstProductos');
                var elem=$lstProductos.find('#lstElem_' + idPrd);
                elem.remove();
                var totalProductos=$container.data('_modeloListado_');
                totalProductos.i=totalProductos.i-1;
                if(totalProductos.i==0){
                    $container.find('#contenidoSeccionProductos').fadeOut();
                    $container.find('#productosBlankSlate').fadeIn();
                }
            },
            function(jqXHR, textStatus, error){
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        //PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                        alertify.error(jqXHR.responseJSON.Message);
                        break;
                }
            },
            function(jqXHROrData, textStatus, errorOrJQXHR){
            },
            {
                401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy($retirarProducto, null, $container, idCte, idOp, idPrd);
                    }
            }
            );
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Crea Renglon en Listado de Productos
        function crearElementoDeListadoDeProductos(element) {
            var editCommandClass = '';
            var editCommandEditAction = 'javascript:editarCantidad(' + element.Id_Prd + ')';
            var editCommandRemoveAction = 'javascript:retirarProducto(' + element.Id_Cte + ',' + element.Id_Op + ',' + element.Id_Prd + ')';
            if (_proyectoSeleccionado.EnValuacion != null) {
                if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
                    editCommandClass = 'disabled-link';
                    editCommandEditAction = '#';
                    editCommandRemoveAction='#';
                }
            }

            var textoNodoDilucion = '<td>' +
                                        '<div id="dvDilucionDisplay_' + element.Id_Prd + '" >' +
                                            '<table>' +
                                                '<tr>' +
                                                    '<td>Dilución</td>' +
                                                    '<td style="text-align: right; width: 60px">' +
                                                        '<div id="dvDilucionDisplayValue">' +
                                                            (element.COP_Dilucion!=null ? element.COP_Dilucion : '') +
                                                        '</div>' +
                                                    '</td>' +
                                                '</tr>' +
                                            '</table>' +
                                        '</div>' +
                                        '<div id="dvDilucionEdit_' + element.Id_Prd + '" style="display:none;">' +
                                        '<table>' +
                                            '<tr>' +
                                                '<td>Dilución 1:</td>' +
                                                '<td>' +
                                                    '<input type="text" id="txtDilucion" value="' + (element.COP_Dilucion!=null ? element.COP_Dilucion : '') + '" style="text-align: right; width: 60px;">' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a href="javascript:aceptarEditarCantidad(' + element.Id_Prd + ')"><i class="fa fa-check" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a href="javascript:cancelarEditarCantidad(' + element.Id_Prd + ')"><i class="fa fa-times" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                            '</tr>' +
                                        '</table>' +
                                    '</div>' +
                                    '<td>';

            var n = $('<div class="list-group-item list-view-pf-stacked list-view-pf-top-align" id="lstElem_' + element.Id_Prd + '" elementoDeLista>' +
                    '<div class="list-view-pf-checkbox"><input type="checkbox"></div>' +
                    '<div class="list-view-pf-actions">' +
                        '<div class="dropdown pull-right dropdown-kebab-pf">' +
                            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                                '<span class="fa fa-ellipsis-v"></span>' +
                            '</button>' +
                            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                                '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
                                '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'">Retirar</a></li>' +
                            '</ul>' +
                        '</div>' +
                    '</div>' +
                    '<div class="list-view-pf-main-info PADDING_TB_10">' +
                    '<div class="list-view-pf-body">' +
                        '<div class="list-view-pf-description">' +
                            '<div class="list-group-item-heading">' + element.Id_Prd + '&nbsp;' + element.Nombre +'</div>' +
                             //'<div class="list-group-item-text">' + element.Ruta + '</div>' +
                             '<div class="list-group-item-text"></div>' +
                        '</div>' +
                        '<div class="list-view-pf-additional-info">' +
                            '<table>' +
                                '<tr>' +
                                    '<td>' +
                                    '<div id="dvCantidadDisplay_' + element.Id_Prd + '">' +
                    //cantidad
                                        '<table>' +
                                            '<tr>' +
                                                '<td>Cantidad de Producto</td>' +
                                                '<td style="text-align: right; width: 60px">' +
                                                    '<div id="dvCantidadDisplayValue">' +
                                                        element.COP_Cantidad +
                                                    '</div>' +
                                                '</td>' +
                                                '<td> &nbsp;' + 'piezas' /*element.ProductoSerializable.Prd_UniNe*/ + '</td>' +
                                            '</tr>' +
                                        '</table>' +
                                    '</div>' +
                                    '<div id="dvCantidadEdit_' + element.Id_Prd + '" style="display:none;">' +
                                        '<table>' +
                                            '<tr>' +
                                                '<td>Cantidad de Producto</td>' +
                                                '<td>' +
                                                    '<input type="text" id="txtCantidad" value="' + element.COP_Cantidad + '" style="text-align: right; width: 60px;">' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a href="javascript:aceptarEditarCantidad(' + element.Id_Prd + ')"><i class="fa fa-check" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a href="javascript:cancelarEditarCantidad(' + element.Id_Prd + ')"><i class="fa fa-times" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                            '</tr>' +
                                        '</table>' +
                                    '</div>' +
                                    '</td>' +
                                '</tr>' +
                                '<tr>' +
                                   (element.COP_EsQuimico==true ? textoNodoDilucion : '' )  +
                                '</tr>' +
                            '</table>' +
                        '</div>' +
                    '</div>' +
                    '</div>' +
                '</div>');
                return n;
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $crearElementoDeListadoDeProductos(idContenedor, E) {            
            var editCommandClass = '';
            var editCommandEditAction = "javascript:$editarCantidad($('#" + idContenedor + "\')," + E.Id_Prd + ")";
            var editCommandRemoveAction = "javascript:$retirarProducto($('#" + idContenedor + "\')," + E.Id_Cte + "," + E.Id_Op + "," + E.Id_Prd + ")";
            if (_proyectoSeleccionado.EnValuacion != null) {
                if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
                    editCommandClass = 'disabled-link';
                }
            }

            var textoNodoDilucion = '<td>' +
                                        '<div id="dvDilucionDisplay_' + E.Id_Prd + '" >' +
                                            '<table>' +
                                                '<tr>' +
                                                    '<td>Dilución</td>' +
                                                    '<td style="text-align: right; width: 60px">' +
                                                        '<div id="dvDilucionDisplayValue">' +
                                                            (E.COP_Dilucion!=null ? E.COP_Dilucion : '') +
                                                        '</div>' +
                                                    '</td>' +
                                                '</tr>' +
                                            '</table>' +
                                        '</div>' +
                                        '<div id="dvDilucionEdit_' + E.Id_Prd + '" style="display:none;">' +
                                        '<table>' +
                                            '<tr>' +
                                                '<td>Dilución 1:</td>' +
                                                '<td>' +
                                                    '<input type="text" id="txtDilucion" value="' + (E.COP_Dilucion!=null ? E.COP_Dilucion : '') + '" style="text-align: right; width: 60px;">' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a id="aAceptarDilucion"><i class="fa fa-check" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a id="aCancelarDilucion"><i class="fa fa-times" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                            '</tr>' +
                                        '</table>' +
                                    '</div>' +
                                    '<td>';

            var n = $('<div class="list-group-item list-view-pf-stacked list-view-pf-top-align" id="lstElem_' + E.Id_Prd + '" elementoDeLista>' +
                    '<div class="list-view-pf-checkbox"><input type="checkbox"></div>' +
                    '<div class="list-view-pf-actions">' +
                        '<div class="dropdown pull-right dropdown-kebab-pf">' +
                            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                                '<span class="fa fa-ellipsis-v"></span>' +
                            '</button>' +
                            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                                '<li><a class="' + editCommandClass + '" id="aEditCommandAction">Editar</a></li>' +
                                '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'" id="aRemoveCommandAction">Retirar</a></li>' +
                            '</ul>' +
                        '</div>' +
                    '</div>' +
                    '<div class="list-view-pf-main-info PADDING_TB_10">' +
                    '<div class="list-view-pf-body">' +
                        '<div class="list-view-pf-description">' +
                            '<div class="list-group-item-heading"> '+ E.Id_Prd + '&nbsp;' + E.Nombre + '</div>' +
                            //'<div class="list-group-item-text">' + E.Ruta + '</div>' +
                            '<div class="list-group-item-text"></div>' +
                        '</div>' +
                        '<div class="list-view-pf-additional-info">' +
                            '<table>' +
                                '<tr>' +
                                    '<td>' +
                                    '<div id="dvCantidadDisplay_' + E.Id_Prd + '">' +
                    //cantidad
                                        '<table>' +
                                            '<tr>' +
                                                '<td>Cantidad de Producto</td>' +
                                                '<td style="text-align: right; width: 60px">' +
                                                    '<div id="dvCantidadDisplayValue">' +
                                                        E.COP_Cantidad +
                                                    '</div>' +
                                                '</td>' +
                                                '<td> &nbsp;' + 'piezas' /*element.ProductoSerializable.Prd_UniNe*/ + '</td>' +
                                            '</tr>' +
                                        '</table>' +
                                    '</div>' +
                                    '<div id="dvCantidadEdit_' + E.Id_Prd + '" style="display:none;">' +
                                        '<table>' +
                                            '<tr>' +
                                                '<td>Cantidad de Producto</td>' +
                                                '<td>' +
                                                    '<input type="text" id="txtCantidad" value="' + E.COP_Cantidad + '" style="text-align: right; width: 60px;">' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a id="aAceptarEditarCantidad"><i class="fa fa-check" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a id="aCancelarEditarCantidad"><i class="fa fa-times" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                            '</tr>' +
                                        '</table>' +
                                    '</div>' +
                                    '</td>' +
                                '</tr>' +
                                '<tr>' +
                                   (E.COP_EsQuimico==true ? textoNodoDilucion : '' )  +
                                '</tr>' +
                            '</table>' +
                        '</div>' +
                    '</div>' +
                    '</div>' +
                '</div>');
                if (_proyectoSeleccionado.EnValuacion != null) {
                    if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
                        n.find('#aEditCommandAction').attr('href', '#');
                        //n.find('#aRemoveCommandAction').attr('href', '#');
                    }else{
                        n.find('#aEditCommandAction').click(function(){
                            $editarCantidad(n, E.Id_Prd);
                        });
                        //n.find('#aRemoveCommandAction');
                    }
                }

                n.find('#aAceptarEditarCantidad').click(function(){
                    $aceptarEditarCantidad(n, E.Id_Prd);
                });

                n.find('#aCancelarEditarCantidad').click(function(){
                    $cancelarEditarCantidad(n, E.Id_Prd);
                });

                n.find('#aAceptarDilucion').click(function(){
                    $aceptarEditarCantidad(n, E.Id_Prd);
                });

                n.find('#aCancelarDilucion').click(function(){
                    $cancelarEditarCantidad(n, E.Id_Prd);
                });

                return n;
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function autoResize(id) {
            var newheight;
            var newwidth;

            if (document.getElementById) {
                newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
                newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
            }

            document.getElementById(id).height = (newheight) + "px";
            document.getElementById(id).width = (newwidth) + "px";
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function limpiarCamposBusquedaProducto() {
            console.lo('function $agregarProductoOtro');
            $('#txtProductoBusqueda').val('');
            $('#txtProductoCantidad').val('');
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $limpiarCamposBusquedaProducto($container) {
            
            $container.find('#txtProductoBusqueda').val('');
            $container.find('#txtProductoCantidad').val('');
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function dimensionElegida(idUen, idSeg, unidades){
            $('#dvModalEditarProyecto #hdnDim_Id_Uen').val(idUen);
            $('#dvModalEditarProyecto #hdnDim_Id_Seg').val(idSeg);
            $('#dvModalEditarProyecto #txtDimension').val(unidades);
            $('#dvModalDimension').modal('hide');
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function recargarListadoProyectos(){
            $(_tablaProyectos.table().container()).block({message: '<img src=\'<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>\' />Actualizando'});
            _tablaProyectos.ajax.reload(function(){ $(_tablaProyectos.table().container()).unblock(); });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarTerritoriosDeProspecto($, jqElement, idRik, idProspecto, onSuccess, onFailure) {
            $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/ProspectoTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarTerritoriosDeProspecto, null, $, jqElement, idRik, idProspecto, onSuccess, onFailure);
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
                //PatternflyToast.showError('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos', 10000);
                alertify.error('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos');
                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    onFailure($);
                }
            }).always(function (jqXHR, textStatus, errorThrown) {
                $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeOut();
            });
        }

        var _thisWindow=null;
        var _valorUnidadDimension=0.0;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cancelarProyecto(sender){
        /*
            BootstrapConfirm.showWarning('Cancelar Proyecto', 'Est&aacute; a punto de cancelar el proyecto. ¿Est&aacute; seguro de que desea continuar?', function(){
                BootstrapConfirm.hide(function(){
                    $('#dvModalCancelarProyecto').modal('show');
                });
            }, function(){

            }, false);
            */

            alertify.confirm("Cancelar Proyecto', 'Est&aacute; a punto de cancelar el proyecto. ¿Est&aacute; seguro de que desea continuar?", function (ev) {                
                ev.preventDefault();
                $('#dvModalCancelarProyecto').modal('show');                
            }, function(ev) {                
                ev.preventDefault();                                
            });
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function confirmarCancelarProyecto(sender){
            var idCausa=$('#dvModalCancelarProyecto input[name="CausaCancelacion"]:checked').val();
            servicioCancelarProyecto(
				_proyectoSeleccionado.Cliente, 
				_proyectoSeleccionado.Id, 
				idCausa, 
				servicioCancelarProyectoExito, 
				servicioCancelarProyectoFallo, 
				servicioCancelarProyectoSiempre
			);
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function servicioCancelarProyectoExito(response, textStatus, jqXHR){
            //Tachar proyecto en listado, o al menos tachar su descripción y cancelar la creación de los comandos
            //Asociar el renglón correspondiente de la tabla al proyecto elegido actualmente, o crear un identificador que indique el renglón elegido. 
            _$campoDescripcionActual.css('text-decoration', 'line-through');
            PatternflyToast.showSuccess('El proyecto ha sido cancelado con &eacute;xito', 10000);
            $('#dvModalCancelarProyecto').modal('hide');
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function servicioCancelarProyectoFallo(jqXHR, textStatus, error){
            //PatternflyToast.showError(jqXHR.responseJSON.ExceptionMessage, 10000);
            alertify.error(jqXHR.responseJSON.ExceptionMessage);
        }
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function servicioCancelarProyectoSiempre(jqXHROrData, textStatus, errorOrJQXHR){
            //Esconder el indicador de la operación en progreso del modal actual
        }
                
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function servicioCancelarProyecto(idCte, idOp, idCausa, onSuccess, onFailure, always){
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/CancelarProyecto',
                type: 'PUT',
                cache: false,
                data: JSON.stringify({
                    IdOp: idOp,
                    IdCte: idCte,
                    IdCausa: idCausa
                }),
                contentType: 'application/json'
            }).done(function(response, textStatus, jqXHR){
                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    if(onSuccess!=null){
                        onSuccess(response, textStatus, jqXHR);
                    }
                }
            }).fail(function(jqXHR, textStatus, error){
                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    if(onFailure!=null){
                        onFailure(jqXHR, textStatus, error);
                    }
                }
            }).always(function(jqXHROrData, textStatus, errorOrJQXHR){
                if (typeof (always) != undefined && typeof (always) != 'undefined') {
                    if(always!=null){
                        always(jqXHROrData, textStatus, errorOrJQXHR);
                    }
                }
            });
        }
        

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        $(document).ready(function () {
            //alert('Ok');
        });

