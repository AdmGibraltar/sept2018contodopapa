/*
Key Quimica 
14 Jun 2018 RFH
*/

var Id_Rik = 0;
var i_tmp = 0;

var CRM_Modulo = 0;
var _Gerente_IdTU = 0; // Tipo Usuario

var _CRM_Usuario_Id = 0;
var _CRM_Usuario_Rik = 0;
var _CRM_Usuario_Nombre = 0;

var _CRM_Gerente_Id = 0;
var _CRM_Gerente_Rik = 0;
var _CRM_Gerente_Nombre = 0;

var _Parametro_IdTU= 0;    
var _Parametro_IdRik = 0;

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
String.prototype.capitalize = function() {
  return this.replace(/(^|\s)([a-z])/g, function(m, p1, p2) {
    return p1 + p2.toUpperCase();
  });
};

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Recagar_tblProyectos(Rik) {

    $('#dvSeguimiento').css('display', 'none');
    $('#dvLoading').show();
    $('#DetalleProyecto').css('display', 'none');

    _tablaProyectos = $('#tblProyectos').DataTable({
        /*"sDom": "<'dataTables_header' <'row' <'col-md-10' f i r> B <'col-md-1' <'#tblProyectosToolbar'> > > >" +
        "<'table-responsive'  t >" +
        "<'dataTables_footer' p >",*/
        'pageLength': 7,
        "deferRender": true,
        'ordering': true,
        'scrollY': '200px',
        'scrollCollapse': true,
        'language': {
            "processing": "<img src='../../Img/ajax-loader.gif'> Cargando...",
            'url': 'http://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json'
        },

        //                    drawCallback: function () { // this gets rid of duplicate headers
        //                          $('.dataTables_scrollBody thead tr').css({ display: 'collapse' }); 
        //                      },
        'ajax': {
            'url': _ApplicationUrl + '/api/ObtenerProyectosPorRik/?IdRik=' + Rik, //'/api/CrmProyecto_TablaAgrupada/', //'<%=Page.ResolveUrl("../../WebService/PortalRIK/GestionPromocion/Proyectos.svc") %>' + '/ObtenerTodos',
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
                            'defaultContent': '<button ' +
                                'type="button" ' +
                                'class="btn btn-primary" ' +
                                'data-toggle="modal" ' +
                                'data-target="#dvModalValuacion" ' +
                                'data-modo="0" ' +
                                'id="btnGenerarValuacion">' +
                                    '<i class="fa fa-tasks"></i>Generar Valuación' +
                                '</button>',
                            'render': function (data, type, full, meta) {
                                return '<button ' +
                                'type="button" class="btn btn-primary" ' +
                                'data-toggle="modal" ' +
                                'data-target="#dvModalValuacion" data-modo="0" ' +
                                'id="btnGenerarValuacion" ' +
                                'data-idcte="' + full.Id_Cte + '">' +
                                    '<i class="fa fa-tasks"></i>Generar Valuación' +
                                '</button>';
                            }
                        }
                    ]
    });
}

/*
Key Soluciones 
29 Jun 2018 RFH Creacion 
Prospectos2.js 

*/

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Recargar_TblProspecto(Rik) {

    var _Estado;

    switch (_Parametro_ControlesSoloLectura) {
        case 0:
            //Edicion       
            _Estado = '';
            break;
        case 1:
            // Solo lectura 
            _Estado = 'disabled';
            break;
        default:
            _Estado = 'disabled';
            break;
    }

    $('#dvLoading').show();
    
    _tablaProspectos = $('#tblProspectos').DataTable({ /*"sDom": "<'dataTables_header' <'row' <'col-md-9' f i r> <'col-md-2' <'#tblProspectosToolbar'> > > >" +
                                                    "<'table-responsive'  t >" +
                                                    "<'dataTables_footer' p >",*/
        'pageLength': 7,
        'ordering': true,
        'language': {
            'url': 'http://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json'
        },
        'ajax': {
            'url': _ApplicationUrl + '/api/CrmProspecto/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + Rik,
            'dataSrc': ''
        },
        "columns": [
        /* {
        'data': 'Id_CrmProspecto',
        'render': function (data, type, full, meta) {
        return '<a>' + full.Id_CrmProspecto + '</a>';
        }
        },*/
        {
            'data': 'Cte_NomComercial',
            'render': function (data, type, full, meta) {
                return '<a href="javascript:cargarDescripcion(' + meta.row + ')">' + full.Cte_NomComercial + '</a>';
            }
        },
        {
                'data': null,
                'className': "myCenteredCellTable",
                'defaultContent': '<button ' +
                    'class="btn btn-primary" ' +
                    //'data-toggle="modal" '+
                    //'data-target="#dvModalEditarProspecto" '+
                    'onclick="EditarProspecto(this);" ' +
                    '>' +
                    '<i class="fa fa-pencil-square-o"></i>' +
                    '</button>',
                'render': function (data, type, full, meta) {
                    return '<button ' +
                    'class="btn btn-primary" ' +
                    //'data-toggle="modal" '+
                    //'data-target="#dvModalEditarProspecto" '+
                    'data-idcrmprospecto="' + data.Id_CrmProspecto + '" ' +
                    'data-rowidx="' + meta.row + '" ' +
                    'onclick="EditarProspecto(this);" ' +
                    '>' +
                        '<i class="fa fa-pencil-square-o"></i>' +
                    '</button>';
                }
            },
            /*{
            'data': null,
            'className': "myCenteredCellTable",
            'defaultContent': '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" ><i class="fa fa-tasks"></i></button>',
            'render': function (data, type, full, meta) {
            return '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" data-idcrmprospecto="' + data.Id_CrmProspecto + '" data-rowidx="' + meta.row + '" >'+
            '<i class="fa fa-tasks"></i></button>';
            }
            },*/
                {
                'data': null,
                'className': "myCenteredCellTable",
                'defaultContent': '<button class="btn btn-primary">' +
                                        '<i class="' + ICON_NUEVO + '"></i>' +
                                        '</button>',
                'render': function (data, type, full, meta) {
                    return '<button ' +
                                        'class="btn btn-primary" ' +
                                        'id="btnNuevoProyecto_' + data.Id_CrmProspecto + '" ' +
                                        'onclick="NuevoProyecto(this);" ' +
                                        'data-idcrmprospecto="' + data.Id_CrmProspecto + '" ' +
                                        'data-rowidx="' + meta.row + '" ' +
                                        _Estado + '>' +
                                        '<i class="' + ICON_NUEVO + '"></i></button>';
                }
            },
            {
                'data': null,
                'className': "myCenteredCellTable",
                'render': function (data, type, full, meta) {
                    return '<button class="btn btn-primary" ' +
                                        'data-rowid="' + meta.row +
                    //'data-toggle="modal" '+
                    //'data-target="#dvModalEliminarProspecto" '+
                                        'onclick="EditarProspecto(this);" ' +
                                        _Estado + '>' +
                                        '<i class="' + ICON_ELIMINAR + '"></i>' +
                                        '</button>';
                },
                'defaultContent': '<button class="btn btn-primary" ' +
                //'data-toggle="modal" '+
                //'data-target="#dvModalEliminarProspecto" '+
                                    'onclick="EditarProspecto(this);" ' +
                                    _Estado + '>' +
                                    '<i class="' + ICON_ELIMINAR + '"></i>' +
                                    '</button>'
            }
            ]
    });

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Encabezado
function Cargar_TabaUsuariosRik(CALLBACK) {
    $('#spinner_listarep').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/CatUsuarioRik/?IdGerente=0&IdRik=0',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        lst = response;

        if (CALLBACK) {
            CALLBACK(lst);
            $('#spinner_listarep').css('display', 'none');
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#spinner_listarep').css('display', 'none');
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_TabaUsuariosRik.');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function UsuarioRik_Sel(obj) {
    var i_control = $(obj).data('i');
    i_control = parseInt(i_control);
    if (i_tmp > 0) {
        if (i_control == i_tmp) {
            // no hace nada
        } else {
            // remover el anterior
            //$('#rbUsuarioRik_' + i_tmp).prop('checked', false);
            $('#rbUsuarioRik_' + i_tmp).iCheck('uncheck'); 
            i_tmp = i_control;
        }
    } else {        
        i_tmp = i_control;
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_Riks() {
    Cargar_TabaUsuariosRik(function (lst) {
        $('#tblUsuariosRik > tbody').empty();
        for (var i = 0; i < lst.length; i++) {
            var row = $('<tr>');
            row.append($('<td>').append(
                '<p>' +
                    lst[i].Id_Rik +
                '</p>'
            ));
            row.append($('<td>').append(
                '<label id="lbNombreRik_' + i + '">' +
                    lst[i].U_Nombre.capitalize() +
                '</label>'
            ));
            row.append($('<td>').append(
            //'<input type="radio" value="' + lst[i].Id_Rik + '" id="rbUsuarioRik_' + i + '" name="rbUsuarioRik">'                

            '<input type="checkbox" ' +
                'data-i="' + i + '" ' +
                'data-nombre="' + lst[i].U_Nombre + '" ' +
            //'class="ckeckbox_big" ' +
            //'class="iradio_square-blue" ' +
                'value="' + lst[i].Id_Rik + '" ' +
                'id="rbUsuarioRik_' + i + '" ' +
                'onclick="UsuarioRik_Sel(this)">'
            ));

            $('#tblUsuariosRik tbody').append(row);

            $('#rbUsuarioRik_' + i).iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            $('#rbUsuarioRik_' + i).on('ifChecked', function (event) {
                UsuarioRik_Sel(this);
            });
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Iniciar_Interface() {
    switch (CRM_Modulo) {
        case 1:
            //Prospectos         
            switch (_Parametro_ControlesSoloLectura) {
                case 0:
                    //Edicion                        
                    $('#btnNuevoProspecto').prop('disabled', false);                    
                    break;
                case 1:
                    // Solo lectura 
                    $('#btnNuevoProspecto').prop('disabled', true);
                    break;
            }

            // Funciones comunes.
            $('#dvSeguimiento').css('display', 'none');

            break;
        case 2:
            //Proyectos            
            switch (_Parametro_ControlesSoloLectura) {
                case 0:
                    //Edicion                        
                    $('#btnNuevoProyecto').prop('disabled', false);
                    //$('#btnAgregarProducto').prop('disabled', false);

                    $('#contenedorOtrosProductos').find('#btnAgregarProducto').prop('disabled', false);
                    $('#contendorProductos').find('#btnAgregarProducto').prop('disabled', false);
                    
                    break;
                case 1:
                    // Solo lectura 
                    $('#btnNuevoProyecto').prop('disabled', true);
                    //$('#btnAgregarProducto').prop('disabled', true);

                    $('#contenedorOtrosProductos').find('#btnAgregarProducto').prop('disabled', true);
                    $('#contendorProductos').find('#btnAgregarProducto').prop('disabled', true);

                    break;
            }
            break;
            break;
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnListaRep_Seleccion() {
    // Selecion de RIK
    var Rik = $('#rbUsuarioRik_' + i_tmp).val();
    var Nombre = $('#lbNombreRik_' + i_tmp).text();

    // Pasa las variable a vars principales
    _CRM_Usuario_Rik = Rik;
    _CRM_Usuario_Nombre = Nombre;

    if (_CRM_Usuario_Rik == 0) {
        _CRM_Usuario_Rik= -1;
    }

    if (_CRM_Gerente_Rik != _CRM_Usuario_Rik) {
        _Parametro_ControlesSoloLectura = 1;
        alertify.success('Solo lectura');
    } else {
        _Parametro_ControlesSoloLectura = 0;
        alertify.success('Edición');
    }
    
    $('#Gerente_btnCambiarUsuario').text(Nombre);
    $('#dvModalListaRepresentantes').modal('hide');    

    switch (CRM_Modulo) {
        case 1:
            Recargar_TblProspecto(Rik);
            Iniciar_Interface();
            break;
        case 2:
            Recagar_tblProyectos(Rik);
            Iniciar_Interface();
            break;
        case 3:
            // DashBoard / Inicio 
            document.location.href = "FullDashboard.aspx?Id_Rik=" + Rik+'&Rik_Nombre=' + Nombre;
            break;
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnGerente_SelecconUsuario(obj) {
    var i = $(obj).data('i');
    var Rik = $('rbUsuarioRik_' + i).val();    
    $('#dvModalListaRepresentantes').modal('hide');        
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnGerente_CambiarUsuario() {
    $('#dvModalListaRepresentantes').modal('show');
    Cargar_Riks();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Incializa_Gerente(GModulo) {

    CRM_Modulo = GModulo;

    // Si es Gerente 
    if (_Parametro_IdTU == 3) {
        // Visualiza controles de gerente.
        $('#Gerente_btnCambiarUsuario').text(_CRM_Usuario_Nombre);        
        $('#Gerente_btnCambiarUsuario').css('display', 'block');
        $('#Gerente_Icono').css('display', 'block');
        $('#Gerente_lbRik').css('display', 'block');

        if (_CRM_Usuario_Rik == 0) {
            _CRM_Usuario_Rik =-1;
        }

        if (_CRM_Gerente_Rik != _CRM_Usuario_Rik) {
            _Parametro_ControlesSoloLectura = 1;
            alertify.success('Solo lectura');
        } else {
            _Parametro_ControlesSoloLectura = 0;
            alertify.success('Modo Edición');
        }

    } else {
        $('#Gerente_btnCambiarUsuario').text('');        
        $('#Gerente_btnCambiarUsuario').css('display', 'none');
        $('#Gerente_Icono').css('display', 'none');
        $('#Gerente_lbRik').css('display', 'none');
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    //alert(_Gerente_Nombre);

});

