<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="Valuaciones.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Valuaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphToolbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyContent" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <table class="datatable table table-striped table-bordered" id="tblValuaciones">
                <thead>
                    <tr>
                        <th>
                            Clave
                        </th>
                        <th>
                            Fecha
                        </th>
                        <th>
                            Cliente
                        </th>
                        <th>
                            Estatus
                        </th>
                        <th>
                            Editar
                        </th>
                        <th>
                            Imprimir
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            99999
                        </td>
                        <td>
                            2016/03/01
                        </td>
                        <td>
                            Bennetts
                        </td>
                        <td>
                            <span class="label label-default">Análisis</span>
                        </td>
                        <td>
                            <button class="btn btn-primary" data-toggle="modal" data-target="#dvModalEditarEvaluacion" onclick="editarValuacion()"><i class="fa fa-tasks"></i></button>
                        </td>
                        <td>
                            <button class="btn btn-primary"><i class="fa fa-print"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="modal fade" id="dvModalNuevaValuacion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">
                        Nueva Valuación
                    </h4>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="form-group">
                            <label for="selCliente">
                                Tipo de Cliente</label>
                            <select id="selTipoCliente" class="selectpicker form-control" onchange="selTipoCliente_onchange(jQuery)">
                                <option value="1">Prospecto</option>
                                <option value="2">Cliente</option>
                            </select>
                        </div>

                        <div class="form-group" id="dvFgCliente" style="display: none;">
                            <label for="selCliente">
                                Cliente</label>
                            <input type="text" id="selCliente" class="form-control" placeholder="Autocompletable" />
                        </div>

                        <div class="form-group" id="dvFgProspecto">
                            <label for="selTerritorio">
                                Prospecto</label>
                            <input type="text" id="txtProspecto" class="form-control" placeholder="Autocompletable" />
                        </div>
                        <h2>Proyectos</h2>
                        <div class="list-group list-view-pf" style="overflow-y: auto; overflow-x: hidden; height: 200px;" id="lvProyectos">
                            <div class="list-group-item list-view-pf-stacked">
                                <div class="list-view-pf-actions">
                                    <input type="checkbox" />
                                </div>
                                <div class="list-view-pf-main-info">
                                    <div class="list-view-pf-left">
                                        <span class="fa fa-wifi list-view-pf-icon-lg"></span>
                                    </div>
                                    <div class="list-view-pf-body">
                                        <div class="list-view-pf-description">
                                            <div class="list-group-item-heading">
                                                Proyecto #1
                                                <small>Fecha creación</small>
                                            </div>
                                            <div class="list-group-item-text">
                                                Proyecto en promoción
                                            </div>
                                        </div>
                                        <div class="list-view-pf-additional-info">
                                            <span class="label label-info">Promoción</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="list-group-item list-view-pf-stacked">
                                <div class="list-view-pf-actions">
                                    <input type="checkbox" />
                                </div>
                                <div class="list-view-pf-main-info">
                                    <div class="list-view-pf-left">
                                        <span class="fa fa-thumbs-up list-view-pf-icon-lg"></span>
                                    </div>
                                    <div class="list-view-pf-body">
                                        <div class="list-view-pf-description">
                                            <div class="list-group-item-heading">
                                                Proyecto #2
                                                <small>Fecha creación</small>
                                            </div>
                                            <div class="list-group-item-text">
                                                Proyecto cerrado
                                            </div>
                                        </div>
                                        <div class="list-view-pf-additional-info">
                                            <span class="label label-success">Cerrado</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="list-group-item list-view-pf-stacked">
                                <div class="list-view-pf-actions">
                                    <input type="checkbox" />
                                </div>
                                <div class="list-view-pf-main-info">
                                    <div class="list-view-pf-left">
                                        <span class="fa fa-exchange list-view-pf-icon-lg"></span>
                                    </div>
                                    <div class="list-view-pf-body">
                                        <div class="list-view-pf-description">
                                            <div class="list-group-item-heading">
                                                Proyecto #3
                                                <small>Fecha creación</small>
                                            </div>
                                            <div class="list-group-item-text">
                                                Proyecto en negociación
                                            </div>
                                        </div>
                                        <div class="list-view-pf-additional-info">
                                            <span class="label label-warning">Negociación</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="list-group-item list-view-pf-stacked">
                                <div class="list-view-pf-actions">
                                    <input type="checkbox" />
                                </div>
                                <div class="list-view-pf-main-info">
                                    <div class="list-view-pf-left">
                                        <span class="fa fa-line-chart list-view-pf-icon-lg"></span>
                                    </div>
                                    <div class="list-view-pf-body">
                                        <div class="list-view-pf-description">
                                            <div class="list-group-item-heading">
                                                Proyecto #4
                                                <small>Fecha creación</small>
                                            </div>
                                            <div class="list-group-item-text">
                                                Proyecto en análisis
                                            </div>
                                        </div>
                                        <div class="list-view-pf-additional-info">
                                            <span class="label label-default">Análisis</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary"
                            id="btnGuardar">
                            Guardar
                        </button>
                        <button type="button" class="btn btn-primary"
                            id="btnGuadarContinuar">
                            Guardar y continuar
                        </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.7.3/js/bootstrap-select.min.js"></script>
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                $('#tblValuaciones').DataTable({
                    "sDom": "<'dataTables_header' <'row' <'col-md-10' f i r> <'col-md-1' <'#tblValuacionesToolbar'> > > >" +
                                                        "<'table-responsive'  t >" +
                                                        "<'dataTables_footer' p >",
                    'pageLength': 7,
                    'ordering': true
                });

                $('#tblValuacionesToolbar').html('<button class="btn btn-default" data-toggle="modal" data-target="#dvModalNuevaValuacion"><i class="fa fa-plus"></i>Nueva Valuación</button>');
                $('#tblValuacionesToolbar').css('padding', '2px 0');
            });
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });
        })(jQuery);

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
    </script>
</asp:Content>
