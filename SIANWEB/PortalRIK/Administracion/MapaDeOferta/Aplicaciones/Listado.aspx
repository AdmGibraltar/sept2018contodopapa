<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="Listado.aspx.cs" Inherits="SIANWEB.PortalRIK.Administracion.MapaDeOferta.Aplicaciones.Listado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphToolbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyContent" runat="server">
    <table id="tblAplicaciones" class="datatable table table-stripped table-bordered">
        <thead>
            <tr>
                <th>
                    Nombre
                </th>
                <th>
                    Ruta
                </th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <div id="dvModalEditarAplicacion" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="imgDvModalEditarAplicacionEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">
                            &times;
                        </span>
                    </button>
                    <h4 class="modal-title">Editar Aplicaci&oacute;n</h4>
                </div>
                <div class="modal-body">
                    <form id="theForm">
                        <div class="form-group">
                            <label for="txtDescripcion">
                                Descripci&oacute;n
                            </label>
                            <input type="text" name="Descripcion" id="txtDescripcion" class="form-control" placeholder="Descripci&oacute;n" />
                        </div>
                        <div class="form-group">
                            <label for="selUen">
                                Uen
                            </label>
                            <select class="selectpicker form-control" id="selUen" name="IdUen" onchange="selUen_onChange(this);" data-topcontainer="dvModalEditarAplicacion">
                                <asp:Repeater runat="server" ID="rptUen">
                                    <ItemTemplate>
                                        <option value='<%# Eval("Id_Uen") %>'>
                                            <%# Eval("Uen_Descripcion")%>
                                        </option>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </select>
                        </div>

                        <div class="form-group">
                            <label for="selSegmento">
                                Segmento
                            </label>
                            <select class="selectpicker form-control" id="selSegmento" name="IdSegmento" data-topcontainer="dvModalEditarAplicacion" onchange="selSegmento_onChange(this);">
                            </select>
                        </div>

                        <div class="form-group">
                            <label for="selSegmento">
                                &Aacute;rea
                            </label>
                            <select class="selectpicker form-control" id="selArea" name="IdArea">
                            </select>
                        </div>

                        <div class="form-group">
                            <label for="selSegmento">
                                Soluci&oacute;n
                            </label>
                            <select class="selectpicker form-control" id="selSolucion" name="IdSolucion">
                            </select>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" onclick="dvModalEditarUsuarioSaveChanges(this)" id="btnSaveChanges">Aplicar</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>   

    <script type="text/javascript">
        var _tblAplicaciones = null;
        $(document).ready(function () {
            inicializarModalEditarAplicacion();
            inicializarListado();
        });

        function inicializarListado() {
            _tblAplicaciones=$('#tblAplicaciones').DataTable({
                "deferRender": true,
                'language': {
                    'url': 'http://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json'
                },
                'ajax': {
                    'url': '<%= ApplicationUrl %>/api/Aplicaciones',
                    'dataSrc': ''
                },
                'columns': [
                    {
                        'data': 'Nombre',
                        'render': function (data, type, full, meta) {
                            return '<table width="100%"><tr><td>' + full.Nombre + '</td><td style="white-space: nowrap; width: 10%; text-align: center;">' + generarHTMLKebab(meta.row) + '</td></tr></table>';
                        }
                    },
                    {
                        'data' : 'Ruta'
                    }
                ]
            });
        }

        function generarHTMLKebab(rowId) {
            return '<div class="dropdown  dropdown-kebab-pf">' +
                    '<button class="btn btn-link dropdown-toggle" type="button" id="dropdownKebab" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">' +
                      '<span class="fa fa-ellipsis-v"></span>' +
                    '</button>' +
                    '<ul class="dropdown-menu " aria-labelledby="dropdownKebab">' +
                    '  <li><a href="#!" data-rowid="' + rowId + '" onclick="lnkEditar_Click(this)">Editar</a></li>' +
                    '  <li><a href="#!" data-rowid="' + rowId + '" onclick="lnkEliminar_Click(this)">Eliminar</a></li>' +
                    '</ul>' +
                    '</div>';
        }

        function inicializarModalEditarAplicacion() {
            $('#dvModalEditarAplicacion').on('show.bs.modal', function (event) {
                var $btnSaveChanges = $(this).find('#btnSaveChanges');
                $btnSaveChanges.data('topContainer', $(this));

                cargarCamposModalEditarAplicacion(_detalleAplicacionEdicionActual);
            });
        }

        var _mapaOferta = new Object();
        function cargarCamposModalEditarAplicacion(data) {
            $('#dvModalEditarAplicacion #txtDescripcion').val(data.Nombre);
            $('#dvModalEditarAplicacion #selUen').selectpicker('val', data.IdUen);
            var $selSegmentos=$('#dvModalEditarAplicacion #selSegmento');
            //actualizar el contenido de selSegmento
            //Se revisa primero si se encuentra el cache de segmentos asociados a una uen; de ser así, se carga inmediatamente el selector de uen sin tener que pasar por el servidor
            //Se valida que el valor de la llave exista; las condicion de inicialización es indefinido
            if (typeof (_mapaOferta[data.IdUen]) != 'undefined' && typeof (_mapaOferta[data.IdUen]) != undefined) {
                //se valida que el mapa de segmento para esta uen no sea nulo
                if (_mapaOferta[data.IdUen] != null) {
                    cargarListadoSegmentos($selSegmentos, _mapaOferta[data.IdUen].data);
                    //si existe, es un arreglo
                    $selSegmentos.selectpicker('val', data.IdSegmento);
                    $selSegmentos.selectpicker('refresh');

                    diferirCargaDeAreas($('#dvModalEditarAplicacion'), data.IdUen, data.IdSegmento, data.IdArea, data);
                }
            } else {
                //el mapa no existe; se procede a cargar desde el servidor
                leerListadoSegmentos(data, $.proxy(leerListadoSegmentosExito, { data: data, $selSegmentos: $selSegmentos, $container: $('#dvModalEditarAplicacion') }), $.proxy(leerListadoSegmentosFallo, null), $.proxy(leerListadoSegmentosSiempre, null));
            }
        }

        function selUen_onChange(sender){
            var container=$(sender).data('topcontainer');
            var $container=$('#' + container);
            var idUen=$(sender).selectpicker('val');
            //Descargar los elementos de las vistas de los controles de listado
            diferirCargaDeSegmentosAlSeleccionarUen($container, idUen);
        }

        function selSegmento_onChange(sender){
            var container=$(sender).data('topcontainer');
            var $container=$('#' + container);
            var idUen=$container.find('#selUen').selectpicker('val');
            var idSegmento=$container.find('#selSegmento').selectpicker('val');
            diferirCargaDeAreasAlSeleccionarSegmento($container, idUen, idSegmento);
        }

        function diferirCargaDeSegmentosAlSeleccionarUen($container, idUen){
            var $selSegmentos=$container.find('#selSegmento');
            if (typeof (_mapaOferta[idUen]) != 'undefined' && typeof (_mapaOferta[idUen]) != undefined) {
                //se valida que el mapa de segmento para esta uen no sea nulo
                if (_mapaOferta[idUen] != null) {
                    cargarListadoSegmentos($selSegmentos, _mapaOferta[idUen].data);
                    //si existe, es un arreglo
                    $selSegmentos.selectpicker('val', 0);
                    $selSegmentos.selectpicker('refresh');
                }
            } else {
                //el mapa no existe; se procede a cargar desde el servidor
                leerListadoSegmentos({IdUen: idUen}, $.proxy(leerListadoSegmentosAlSeleccionarUenExito, { data: {IdUen: idUen}, $selSegmentos: $selSegmentos, $container: $('#dvModalEditarAplicacion') }), $.proxy(leerListadoSegmentosFallo, null), $.proxy(leerListadoSegmentosSiempre, null));
            }
        }

        function diferirCargaDeAreasAlSeleccionarSegmento($container, idUen, idSegmento){
            if (typeof (_mapaOferta[idUen]) != 'undefined' && typeof (_mapaOferta[idUen]) != undefined) {
                var $selArea = $container.find('#selArea');
                if (typeof (_mapaOferta[idUen].data[idSegmento]) != 'undefined' && typeof (_mapaOferta[idUen].data[idSegmento]) != undefined) {
                    //se valida que el mapa de segmento para esta uen no sea nulo
                    if (_mapaOferta[idUen].data[idSegmento].data != null) {
                        //si existe, es un arreglo
                        cargarListadoAreas($selArea, _mapaOferta[idUen].data[idSegmento].data);
                        $selArea.selectpicker('val', 0);
                        $selArea.selectpicker('refresh');
                    }else {
                        //el mapa no existe; se procede a cargar desde el servidor
                        leerListadoAreas({IdUen: idUen, IdSegmento: idSegmento}, $.proxy(leerListadoAreasAlSeleccionarSegmentoExito, { data: {IdUen: idUen, IdSegmento: idSegmento}, $selArea: $selArea, $container: $container }), $.proxy(leerListadoAreasFallo, null), $.proxy(leerListadoAreasSiempre, null));
                    }
                }else{
                    throw 'El mapa para el segmento seleccionado no ha sido cargado';
                }
            } else {
                throw 'El mapa para la unidad de negocios seleccionada no ha sido cargado';
            }
        }

        function diferirCargaDeSolucionesAlSeleccionarArea($container, idUen, idSegmento, idArea, data) {
            if (typeof (_mapaOferta[idUen]) != 'undefined' && typeof (_mapaOferta[idUen]) != undefined) {
                var $selSolucion = $container.find('#selSolucion');
                if (typeof (_mapaOferta[idUen].data[idSegmento]) != 'undefined' && typeof (_mapaOferta[idUen].data[idSegmento]) != undefined) {
                    if (typeof (_mapaOferta[idUen].data[idSegmento].data[idArea]) != 'undefined' && typeof (_mapaOferta[idUen].data[idSegmento].data[idArea]) != undefined) {
                        //se valida que el mapa de segmento para esta uen no sea nulo
                        if (_mapaOferta[idUen].data[idSegmento].data[idArea].data != null) {
                            //si existe, es un arreglo
                            cargarListadoSoluciones($selSolucion, _mapaOferta[idUen].data[idSegmento].data[idArea].data);
                            $selSolucion.selectpicker('val', 0);
                            $selSolucion.selectpicker('refresh');
                        } else {
                            leerListadoSoluciones(data, $.proxy(leerListadoSolucionesAlSeleccionarAreaExito, { data: {IdUen: idUen, IdSegmento: idSegmento, IdArea: idArea}, $selSolucion: $selSolucion, $container: $container }), $.proxy(leerListadoSolucionesFallo, null), $.proxy(leerListadoSolucionesSiempre, null));
                        }
                    }else{
                        throw 'El mapa para el área seleccionada no ha sido cargado';
                    }
                } else {
                    throw 'El mapa para el segmento seleccionado no ha sido cargado';
                }
            } else {
                throw 'El mapa para la unidad de negocios seleccionada no ha sido cargado';
            }
        }

        function leerListadoSolucionesAlSeleccionarAreaExito(response, responseStatus, jqXHR) {
            if (typeof (response.succeeded) != 'undefined' && typeof (response.succeeded) != undefined) {
                if (response.succeeded == true) {
                    var _this=this;
                    _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data[_this.data.IdArea].data=new Object();
                    $.each(response.data, function(index, element){
                        _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data[_this.data.IdArea].data[element.IdSolucion] = { element: element, data: null };
                    });
                    cargarListadoSoluciones(this.$selSolucion, _mapaOferta[this.data.IdUen].data[this.data.IdSegmento].data[this.data.IdArea].data);
                } else {
                    showDangerToast(response.mensaje, 10000);
                }
            } else {
                showDangerToast('Se ha presentado una anomalía al proceder. Se ha avisado de esta condición al equipo de soporte.', 10000);
            }
        }

        function diferirCargaDeAreas($container, idUen, idSegmento, idArea, data) {
            if (typeof (_mapaOferta[idUen]) != 'undefined' && typeof (_mapaOferta[idUen]) != undefined) {
                var $selArea = $container.find('#selArea');
                if (typeof (_mapaOferta[idUen].data[idSegmento]) != 'undefined' && typeof (_mapaOferta[idUen].data[idSegmento]) != undefined) {
                    //se valida que el mapa de segmento para esta uen no sea nulo
                    if (_mapaOferta[idUen].data[idSegmento].data != null) {
                        //si existe, es un arreglo
                        cargarListadoAreas($selArea, _mapaOferta[idUen].data[idSegmento].data);
                        $selArea.selectpicker('val', idArea);
                        $selArea.selectpicker('refresh');

                        diferirCargaDeSoluciones($container, idUen, idSegmento, idArea, data.IdSolucion, data);
                    }else {
                        //el mapa no existe; se procede a cargar desde el servidor
                        leerListadoAreas(data, $.proxy(leerListadoAreasExito, { data: data, $selArea: $selArea, $container: $container }), $.proxy(leerListadoAreasFallo, null), $.proxy(leerListadoAreasSiempre, null));
                    }
                }else{
                    throw 'El mapa para el segmento seleccionado no ha sido cargado';
                }
            } else {
            throw 'El mapa para la unidad de negocios seleccionada no ha sido cargado';
            }
        }

        function leerListadoAreasExito(response, responseStatus, jqXHR) {
            if (typeof (response.succeeded) != 'undefined' && typeof (response.succeeded) != undefined) {
                if (response.succeeded == true) {
                    var _this=this;
                    _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data=new Object();
                    $.each(response.data, function(index, element){
                        _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data[element.IdArea] = { element: element, data: null };
                    });
                    cargarListadoAreas(this.$selArea, _mapaOferta[this.data.IdUen].data[this.data.IdSegmento].data);
                    //si existe, es un arreglo
                    this.$selArea.selectpicker('val', this.data.IdArea);
                    this.$selArea.selectpicker('refresh');

                    diferirCargaDeSoluciones(this.$container, this.data.IdUen, this.data.IdSegmento, this.data.IdArea, this.data.IdSolucion, this.data);
                } else {
                    showDangerToast(response.mensaje, 10000);
                }
            }
        }

        function leerListadoAreasAlSeleccionarSegmentoExito(response, responseStatus, jqXHR) {
            if (typeof (response.succeeded) != 'undefined' && typeof (response.succeeded) != undefined) {
                if (response.succeeded == true) {
                    var _this=this;
                    _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data=new Object();
                    $.each(response.data, function(index, element){
                        _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data[element.IdArea] = { element: element, data: null };
                    });
                    cargarListadoAreas(this.$selArea, _mapaOferta[this.data.IdUen].data[this.data.IdSegmento].data);
                } else {
                    showDangerToast(response.mensaje, 10000);
                }
            }
        }

        function leerListadoAreasFallo(jqXHR, testStatus, error) {
            showDangerToast(jqXHR.responseJSON.ExceptionMessage, 10000);
        }

        function leerListadoAreasSiempre(jqXHROrData, textStatus, errorOrjqXHR) {
        }

        function leerListadoAreas(datos, exito, falla, siempre) {
            $.ajax({
                url: '<%=ApplicationUrl %>/api/ObtenerAreasPorSegmento?idUen=' + datos.IdUen + '&idSegmento=' + datos.IdSegmento,
                cache: false,
                type: 'GET'
            }).done(function (response, responseStatus, jqXHR) {
                if (typeof (exito) != 'undefined' && typeof (exito) != undefined) {
                    exito(response, responseStatus, jqXHR);
                }
            }).fail(function (jqXHR, testStatus, error) {
                if (typeof (falla) != 'undefined' && typeof (falla) != undefined) {
                    falla(jqXHR, testStatus, error);
                }
            }).always(function (jqXHROrData, textStatus, errorOrjqXHR) {
                if (typeof (siempre) != 'undefined' && typeof (siempre) != undefined) {
                    siempre(jqXHROrData, textStatus, errorOrjqXHR);
                }
            });
        }

        function cargarListadoAreas($elemento, items) {
            $elemento.find('option').remove();
            for (var element in items) {
                var opt = $('<option value="' + items[element].element.IdArea + '">' + items[element].element.Descripcion + '</option>');
                $elemento.append(opt);
            }
            $elemento.selectpicker('refresh');
        }



        function diferirCargaDeSoluciones($container, idUen, idSegmento, idArea, idSolucion, data) {
            if (typeof (_mapaOferta[idUen]) != 'undefined' && typeof (_mapaOferta[idUen]) != undefined) {
                var $selSolucion = $container.find('#selSolucion');
                if (typeof (_mapaOferta[idUen].data[idSegmento]) != 'undefined' && typeof (_mapaOferta[idUen].data[idSegmento]) != undefined) {
                    if (typeof (_mapaOferta[idUen].data[idSegmento].data[idArea]) != 'undefined' && typeof (_mapaOferta[idUen].data[idSegmento].data[idArea]) != undefined) {
                        //se valida que el mapa de segmento para esta uen no sea nulo
                        if (_mapaOferta[idUen].data[idSegmento].data[idArea].data != null) {
                            //si existe, es un arreglo
                            cargarListadoSoluciones($selSolucion, _mapaOferta[idUen].data[idSegmento].data[idArea].data);
                            $selSolucion.selectpicker('val', idSolucion);
                            $selSolucion.selectpicker('refresh');
                        } else {
                            leerListadoSoluciones(data, $.proxy(leerListadoSolucionesExito, { data: data, $selSolucion: $selSolucion, $container: $container }), $.proxy(leerListadoSolucionesFallo, null), $.proxy(leerListadoSolucionesSiempre, null));
                        }
                    }else{
                        throw 'El mapa para el área seleccionada no ha sido cargado';
                    }
                } else {
                    throw 'El mapa para el segmento seleccionado no ha sido cargado';
                }
            } else {
                throw 'El mapa para la unidad de negocios seleccionada no ha sido cargado';
            }
        }

        function leerListadoSolucionesExito(response, responseStatus, jqXHR) {
            if (typeof (response.succeeded) != 'undefined' && typeof (response.succeeded) != undefined) {
                if (response.succeeded == true) {
                    var _this=this;
                    _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data[_this.data.IdArea].data=new Object();
                    $.each(response.data, function(index, element){
                        _mapaOferta[_this.data.IdUen].data[_this.data.IdSegmento].data[_this.data.IdArea].data[element.IdSolucion] = { element: element, data: null };
                    });
                    cargarListadoSoluciones(this.$selSolucion, _mapaOferta[this.data.IdUen].data[this.data.IdSegmento].data[this.data.IdArea].data);
                    //si existe, es un arreglo
                    this.$selSolucion.selectpicker('val', this.data.IdSolucion);
                    this.$selSolucion.selectpicker('refresh');

                } else {
                    showDangerToast(response.mensaje, 10000);
                }
            } else {
                showDangerToast('Se ha presentado una anomalía al proceder. Se ha avisado de esta condición al equipo de soporte.', 10000);
            }
        }

        function leerListadoSolucionesFallo(jqXHR, testStatus, error) {
            showDangerToast(jqXHR.responseJSON.ExceptionMessage, 10000);
        }

        function leerListadoSolucionesSiempre(jqXHROrData, textStatus, errorOrjqXHR) {
        }

        function leerListadoSoluciones(datos, exito, falla, siempre) {
            $.ajax({
                url: '<%=ApplicationUrl %>/api/ObtenerSolucionesPorArea?idUen=' + datos.IdUen + '&idSegmento=' + datos.IdSegmento + '&idArea=' + datos.IdArea,
                cache: false,
                type: 'GET'
            }).done(function (response, responseStatus, jqXHR) {
                if (typeof (exito) != 'undefined' && typeof (exito) != undefined) {
                    exito(response, responseStatus, jqXHR);
                }
            }).fail(function (jqXHR, testStatus, error) {
                if (typeof (falla) != 'undefined' && typeof (falla) != undefined) {
                    falla(jqXHR, testStatus, error);
                }
            }).always(function (jqXHROrData, textStatus, errorOrjqXHR) {
                if (typeof (siempre) != 'undefined' && typeof (siempre) != undefined) {
                    siempre(jqXHROrData, textStatus, errorOrjqXHR);
                }
            });
        }

        function cargarListadoSoluciones($elemento, items) {
            $elemento.find('option').remove();
            for(var element in items){
                var opt = $('<option value="' + items[element].element.IdSolucion + '">' + items[element].element.Descripcion + '</option>');
                $elemento.append(opt);
            }
            $elemento.selectpicker('refresh');
        }



        function leerListadoSegmentosExito(response, responseStatus, jqXHR) {
            if(typeof(response.succeeded)!='undefined' && typeof(response.succeeded)!=undefined){
                if (response.succeeded == true) {
                    _mapaOferta[this.data.IdUen] = new Object();
                    _mapaOferta[this.data.IdUen].data = new Object();
                    var _this=this;
                    $.each(response.data, function (index, element) {
                        _mapaOferta[_this.data.IdUen].data[element.IdSegmento] = { element: element, data: null };
                    });
                    cargarListadoSegmentos(this.$selSegmentos, _mapaOferta[this.data.IdUen].data);
                    //si existe, es un arreglo
                    this.$selSegmentos.selectpicker('val', this.data.IdSegmento);
                    this.$selSegmentos.selectpicker('refresh');

                    diferirCargaDeAreas(this.$container, this.data.IdUen, this.data.IdSegmento, this.data.IdArea, this.data);
                } else {
                    showDangerToast(response.mensaje, 10000);
                }
            }
        }

        function leerListadoSegmentosAlSeleccionarUenExito(response, responseStatus, jqXHR) {
            if(typeof(response.succeeded)!='undefined' && typeof(response.succeeded)!=undefined){
                if (response.succeeded == true) {
                    _mapaOferta[this.data.IdUen] = new Object();
                    _mapaOferta[this.data.IdUen].data = new Object();
                    var _this=this;
                    $.each(response.data, function (index, element) {
                        _mapaOferta[_this.data.IdUen].data[element.IdSegmento] = { element: element, data: null };
                    });
                    cargarListadoSegmentos(this.$selSegmentos, _mapaOferta[this.data.IdUen].data);
                    //si existe, es un arreglo
                    this.$selSegmentos.selectpicker('val', 0);
                    this.$selSegmentos.selectpicker('refresh');
                } else {
                    showDangerToast(response.mensaje, 10000);
                }
            }
        }

        function leerListadoSegmentosFallo(jqXHR, testStatus, error) {
            showDangerToast(jqXHR.responseJSON.ExceptionMessage, 10000);
        }

        function leerListadoSegmentosSiempre(jqXHROrData, textStatus, errorOrjqXHR){
        }

        function leerListadoSegmentos(datos, exito, falla, siempre) {
            $.ajax({
                url: '<%=ApplicationUrl %>/api/ObtenerSegmentosPorUen?idUen=' + datos.IdUen,
                cache: false,
                type: 'GET'
            }).done(function (response, responseStatus, jqXHR) {
                if (typeof (exito) != 'undefined' && typeof (exito) != undefined) {
                    exito(response, responseStatus, jqXHR);
                }
            }).fail(function (jqXHR, testStatus, error) {
                if (typeof (falla) != 'undefined' && typeof (falla) != undefined) {
                    falla(jqXHR, testStatus, error);
                }
            }).always(function (jqXHROrData, textStatus, errorOrjqXHR) {
                if (typeof (siempre) != 'undefined' && typeof (siempre) != undefined) {
                    siempre(jqXHROrData, textStatus, errorOrjqXHR);
                }
            });
        }

        function cargarListadoSegmentos($elemento, segmentos) {
            $elemento.find('option').remove();
            for (var element in segmentos) {
                var opt = $('<option value="' + segmentos[element].element.IdSegmento + '">' + segmentos[element].element.Descripcion + '</option>');
                $elemento.append(opt);
            }
            $elemento.selectpicker('refresh');
        }

        function lnkEditar_Click(sender) {
            var rowId = $(sender).data('rowid');
            var detalle = _tblAplicaciones.row(rowId).data();
            _detalleAplicacionEdicionActual = detalle;
            $('#dvModalEditarAplicacion').modal('show');
        }

        function lnkEliminar_Click(sender) {
            var rowId = $(sender).data('rowid');
            var detalle = _tblAplicaciones.row(rowId).data();
            _detalleAplicacionEdicionActual = detalle;
            showAlertDialog('Eliminar Aplicaci&oacute;n', 'Est&aacute; a punto de eliminar esta aplicaci&oacute;n. ¿Desea continuar?', confirmarEliminarAplicacion);
        }
    </script>
</asp:Content>
