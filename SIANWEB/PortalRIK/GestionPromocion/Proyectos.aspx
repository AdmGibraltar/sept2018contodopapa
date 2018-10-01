<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true"
    CodeBehind="Proyectos.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Proyectos" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/SelectorDimension.ascx" TagPrefix="uc" TagName="SelectorDimension" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/radios-to-slider.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/bootstrap-treeview.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/ekko-lightbox.min.css")%>" rel="stylesheet">
    <!--link href="//cdn.datatables.net/buttons/1.1.2/css/buttons.dataTables.min.css" rel="stylesheet"-->

    <style>
        .modal-dialog-fs {
          width: 100%;
          height: 100%;
          margin: 0;
          padding: 0;
        }

        .modal-content-fs {
          height: auto;
          min-height: 100%;
          border-radius: 0;
        }
        
        a.disabled-link,
        a.disabled-link:visited ,
        a.disabled-link:active,
        a.disabled-link:hover {
            background-color:#d9d9d9 !important;
            color:#aaa !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphToolbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <button class="btn btn-primary" data-toggle="modal" data-target="#dvModalEditarProyecto" data-action="1" ><i class="fa fa-plus"></i>Nuevo Proyecto</button>
                <table class="datatable table table-striped table-bordered display" id="tblProyectos" cellspacing=0>
                    <thead>
                        <tr>
                            <th>
                                Clave
                                <div class="dropdown pull-right dropdown-kebab-pf" id="ddFiltroClave" style="float: right">
                                    <button class="btn btn-link dropdown-toggle" id="dropdownKebabRight1" aria-expanded="true"
                                        aria-haspopup="true" type="button" data-toggle="dropdown">
                                        <span class="fa fa-filter"></span>
                                    </button>
                                    <ul class="dropdown-menu dropdown-menu-left" aria-labelledby="dropdownKebabRight1">
                                        <li><a href="#" id="ddFiltroClaveOrdenarAsc"><i class="fa fa-sort-alpha-asc"></i></a>
                                        </li>
                                        <li><a href="#" id="ddFiltroClaveOrdenarDesc"><i class="fa fa-sort-alpha-desc"></i></a>
                                        </li>
                                        <li class="divider" role="separator"></li>
                                        <li>
                                            <form>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <input type="text" id="txtColClienteDesde" class="form-control" placeholder="Desde" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <input type="text" id="Text1" class="form-control" placeholder="Hasta" />
                                                </div>
                                            </div>
                                            </form>
                                        </li>
                                    </ul>
                                </div>
                            </th>
                            <th>
                                No. Cliente
                            </th>
                            <th>
                                Cliente
                            </th>
                            <th>
                                Oportunidad
                            </th>
                            <th>
                                Potencial
                            </th>
                            <th>
                                A
                            </th>
                            <th>
                                P
                            </th>
                            <th>
                                N
                            </th>
                            <th>
                                C
                            </th>
                            <!--<th>
                                X
                            </th>-->
                            <th>
                                Avance
                            </th>
                            <th>
                                Editar
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="row" id="dvDetalles" style="display: none;">
        <div class="col-md-12">
            <div class="card-pf">
                <div class="card-pf-heading">
                    <h2 class="card-pf-title">
                        Detalles
                    </h2>
                </div>
                <div class="card-pf-body">
                    <div class="row">
                        <div class="col-md-12">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#dvGeneral" data-toggle="tab">General</a> </li>
                                <li><a href="#dvProductos" data-toggle="tab">Productos<img id="imgCargandoProductos" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></a></li>
                                <li><a href="#dvHerramientas" data-toggle="tab">Herramientas</a> </li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane active" id="dvGeneral">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <br />
                                            <dl class="dl-horizontal">
                                                <dt style="text-align: left">Segmento </dt>
                                                <dd id="ddSegmento">
                                                    
                                                </dd>
                                                <dt style="text-align: left">Área </dt>
                                                <dd id="ddArea">
                                                    
                                                </dd>
                                                <dt style="text-align: left">Solución </dt>
                                                <dd id="ddSolucion">
                                                    
                                                </dd>
                                                <dt style="text-align: left">Valor potencial teórico </dt>
                                                <dd id="ddVPT">
                                                    
                                                </dd>
                                                <%--<dt style="text-align: left">Valor potencial estimado </dt>
                                                <dd>
                                                    $5,000.00
                                                </dd>--%>
                                                <dt style="text-align: left; white-space: normal">Venta promedio mensual esperada
                                                </dt>
                                                <dd id="ddVPME">
                                                    
                                                </dd>
                                            </dl>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div id="dvProgreso" style="width: 100%">
                                                <input id="Radio4" name="options" type="radio" checked />
                                                <label for="Radio4">
                                                    Análisis</label>
                                                <input id="Radio5" name="options" type="radio" />
                                                <label for="Radio5">
                                                    Promoción</label>
                                                <input id="Radio6" name="options" type="radio" />
                                                <label for="Radio6">
                                                    Negociación</label>
                                                <input id="Radio7" name="options" type="radio" />
                                                <label for="Radio7">
                                                    Cierre</label>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="dvProductos">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <form id="frmAgregarProducto">
                                                <input type="hidden" id="hdnAgregarProducto_Id_Op" name="Id_Op" />
                                                <input type="hidden" id="hdnAgregarProducto_Id_Cte" name="Id_Cte" />
                                                <div class="row ui-front" id="dvBusquedaProducto">
                                                    <%--<label for="txtProductoBusqueda">
                                                        Producto</label>--%>
                                                        <div class="col-md-5 tooltip-demo">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <input type="text" id="txtProductoBusqueda" class="form-control" placeholder="Producto" title="Ingrese parte de la descripción o del código para buscar el producto" data-toggle="tooltip" />
                                                                        </td>
                                                                        <td>
                                                                            <img id="imgBuscandoProducto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    <input type="hidden" id="hdnProductoBusqueda" name="Id_Prd" />

                                                    <input type="hidden" id="hdnAgregarProducto_Id_Uen" name="Id_Uen" />
                                                    <input type="hidden" id="hdnAgregarProducto_Id_Seg" name="Id_Seg" />
                                                    <input type="hidden" id="hdnAgregarProducto_Id_Area" name="Id_Area" />
                                                    <input type="hidden" id="hdnAgregarProducto_Id_Sol" name="Id_Sol" />
                                                    <input type="hidden" id="hdnAgregarProducto_Id_Apl" name="Id_Apl" />
                                                    <input type="hidden" id="hdnAgregarProducto_Id_SubFam" name="Id_SubFam" />

                                                </div>
                                                <div class="row">
                                                    <%--<label for="txtProductoCantidad">Cantidad</label>--%>
                                                    <div class="col-md-2 tooltip-demo">
                                                        <input type="text" id="txtProductoCantidad" name="COP_Cantidad" class="form-control" placeholder="Cantidad" title="Ingrese la cantidad del producto" />
                                                    </div>
                                                    <%--<input type="text" id="txtProductoCantidad" name="COP_Cantidad" class="form-control" style="width: 10%" />--%>
                                                </div>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <button type="button" class="btn btn-default" id="btnAgregarProducto" onclick="agregarProducto(this)">Agregar</button>
                                                            </td>
                                                            <td>
                                                                <img id="imgAgregandoProducto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </form>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="list-group list-view-pf" id="lstProductos">
                                        
                                            </div>
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#dvModalValuacion" style="display: none;" id="btnGenerarValuacion">
                                                <i class="fa fa-tasks"></i>Generar Valuación
                                            </button>
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#dvModalValuacion">
                                                <i class="fa fa-calculator"></i>Imprimir Propuesta Económica
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="dvHerramientas">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="tvHerramientas">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="dvModalEditarProyecto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="imgDvModalNuevoProyectoEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">
                        Nuevo Proyecto
                    </h4>
                </div>
                <div class="modal-body">
                    <form id="frmDvModalNuevoProyecto">
                        <input type="hidden" id="hdnId_CrmProspecto" name="Id_CrmProspecto" />
                        <input type="hidden" id="hdnId_Cliente" name="Cliente" />
                        <input type="hidden" id="hdnId_Op" name="Id_Op" />
                        <div class="form-group">
                            <label for="selCliente">
                                Tipo de Cliente</label>
                            <select id="selTipoCliente" class="selectpicker form-control" onchange="selTipoCliente_onchange(jQuery)">
                                <option value="1">Prospecto</option>
                                <option value="2">Cliente</option>
                            </select>
                        </div>
                        <div class="form-group ui-front" id="dvFgCliente" style="display: none;">
                            <label for="selCliente">
                                Cliente</label>
                            <input type="text" id="selCliente" class="form-control" placeholder="Autocompletable" />
                        </div>
                        <div class="form-group ui-front" id="dvFgProspecto">
                            <label for="txtProspecto">
                                Prospecto <img id="imgProspectoEnOperacion" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <input type="text" id="txtProspecto" class="form-control" placeholder="Autocompletable" />
                        </div>
                        <div class="form-group">
                            <label for="selTerritorio">
                                Territorio<img id="imgProcesandoTerritorioDvModalNuevoProyecto" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <select id="selTerritorio" onchange="selTerritorio$on_change(this)" name="Territorio" class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="selUEN">
                                UEN</label>
                            <select id="selUEN" name="Uen" onchange="selUEN$on_change()" disabled class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="selSegmento">
                                Segmento<img id="imgProcesandoSegmentoDvModalNuevoProyecto" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            </label>
                            <select id="selSegmento" name="Segmento" onchange="selSegmento$on_change()" disabled class="selectpicker form-control">
                            </select>
                        </div>

                        <%--<div class="form-inline">
                            <div class="form-group">
                                <label for="txtDimension">
                                    Dimensión
                                </label>
                                <div class="input-group">
                                    <input id="txtDimension" type="text" class="form-control" disabled />
                                    <button class="input-group-addon" data-toggle="modal" data-target="#dvModalDimension" type="button"><i class="fa fa-search fa-fw"></i></button>
                                    <input type="hidden" id="hdnDim_Id_Uen" name="Dim_Id_Uen" />
                                    <input type="hidden" id="hdnDim_Id_Seg" name="Dim_Id_Seg" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtCantidad">
                                    Cantidad
                                </label>
                                <input id="txtCantidad" name="Dim_Cantidad" type="text" class="form-control"/>
                            </div>
                        </div>--%>

                        <div class="row">
                            <div class="col-md-5">
                                <div class="input-group tooltip-demo">
                                    <small>Dimensión:</small>
                                    <input id="txtDimension" type="text" class="form-control" disabled placeholder="Dimension" title="Unidad de la dimensión del Segmento" data-toggle="tooltip" />
                                    <!--<button class="input-group-addon" data-toggle="modal" data-target="#dvModalDimension" type="button"><i class="fa fa-search fa-fw"></i></button>-->
                                    <input type="hidden" id="hdnDim_Id_Uen" name="Dim_Id_Uen" />
                                    <input type="hidden" id="hdnDim_Id_Seg" name="Dim_Id_Seg" />
                                </div>
                            </div>
                            <div class="col-md-2 tooltip-demo">
                                <small>Precio:</small>
                                <input id="txtPrecioUnidad" type="text" class="form-control" placeholder="$0.0" title="Precio por unidad de dimensión" data-toggle="tooltip" disabled/>
                            </div>
                            <div class="col-md-2 tooltip-demo">
                                <small>Cantidad:</small>
                                <input id="txtCantidad" name="Dim_Cantidad" type="text" class="form-control" placeholder="0" title="Cantidad de la unidad elegida" data-toggle="tooltip" onchange="txtCantidad$onchange(this)"/>
                            </div>
                            <div class="col-md-2 tooltip-demo">
                                <small>VPME:</small>
                                <input id="txtVPM" name="CrmOp_VPM" type="text" class="form-control" placeholder="$0.0" title="Venta Promedio Mensual Esperada" data-toggle="tooltip"/>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="selArea">
                                Área<img id="imgProcesandoAreaDvModalNuevoProyecto" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <select id="selArea" name="Area" onchange="selArea$on_change()" class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="selSolucion">
                                Solución<img id="imgProcesandoSolucionDvModalNuevoProyecto" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <select id="selSolucion" name="Solucion" onchange="selSolucion$on_change()" class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="selAplicacion">
                                Aplicación &nbsp;<a data-title="Oferta" href="http://www.todoenunclick.com/Notas/Imagenes/arbol_jerarquia.jpg" id="aMapaOferta"><i class="fa fa-sitemap" aria-hidden="true"></i></a><img id="imgProcesandoAplicacionDvModalNuevoProyecto" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                                    <div style="display:none;">
                                        <select style="display: none;" id="selAplicacion" name="Aplicaciones" class="selectpicker form-control"
                                            multiple>
                                        </select>
                                    </div>

                            <div class="list-group" id="lstAplicacion">
                            
                            </div>

                        </div>
                        <div class="checkbox">
                            <%--<label class="checkbox-inline">
                                <input type="checkbox" name="VentaNoRepetitiva" id="chkVentaRepetitiva" icheck />Venta no repetitiva
                            </label>--%>
                            <!--<label class="checkbox-inline">
                                <input type="checkbox" id="chkPerteneceACampana" icheck />Pertenece a Campaña
                            </label>-->
                        </div>
                        <!--
                        <div class="form-group">
                            <label for="txtVPT">
                                Valor Potencial Teórico</label>
                            <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <input type="text" id="txtVPT" name="ValorPotencialT" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtVPE">
                                Valor Potencial Estimado</label>
                            <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <input type="text" id="txtVPE" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtVPME">
                                Venta Promedio Mensual Esperada</label>
                            <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <input type="text" id="txtVPME" class="form-control" />
                            </div>
                        </div>

                        -->
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnDvModalEditarProyectoGuardar" onclick="crearProyecto()">
                        Guardar
                    </button>
                    <button type="button" class="btn btn-primary" id="btnDvModalEditarProyectoActualizar" onclick="actualizarProyecto()" style="display: none;">
                        Guardar
                    </button>
                    <%--<button type="button" class="btn btn-primary" id="btnDvModalEditarProyectoGuadarContinuar" onclick="crearProyectoYContinuar()">
                        Guardar y continuar
                    </button>--%>
                </div>
            </div>
        </div>
    </div>

    <div id="dvModalValuacion" style="width: 100%; height: 100%" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaValuacion">
        <div class="modal-dialog-fs" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="imgCargandoVentanaValuacion" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="hTituloVentanaValuacion">
                        Valuacion
                    </h4>
                </div>
                <div class="modal-body" id="dvCuerpoVentanaValuacion">
                    <iframe id="iframeVentanaValuacion" style="width: auto; min-width: 100%; height: 550px; min-height: 100%;">
                    
                    </iframe>
                </div>
                <div class="modal-footer">
                
                </div>
            </div>
        </div>
    </div>

    <div id="dvModalDimension" style="width: 100%; height: 100%" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaDimension">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="img1" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="h1">
                        Selección de Dimensión
                    </h4>
                </div>
                <div class="modal-body" id="dvCuerpoVentanaDimension">
                    <uc:SelectorDimension runat="server" ID="ucSelectorDimension" AlSeleccionar="dimensionElegida" />
                </div>
                <div class="modal-footer">
                
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>                
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ekko-lightbox.min.js") %>"></script>
    <!--<script src="//cdn.datatables.net/buttons/1.1.2/js/dataTables.buttons.min.js"></script>-->
    <%--<script src="//cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js"></script>--%>
    <script src="<%=Page.ResolveUrl("~/js/jquery.blockUI.min.js") %>"></script>    
    <script type="text/javascript">
        var _tablaProyectos = null;
        (function ($) {
            $(document).ready(function () {
                _tablaProyectos = $('#tblProyectos').DataTable({
                    "sDom": "<'dataTables_header' <'row' <'col-md-10' f i r> B <'col-md-1' <'#tblProyectosToolbar'> > > >" +
                                                        "<'table-responsive'  t >" +
                                                        "<'dataTables_footer' p >",
                    'pageLength': 7,
                    "deferRender": true,
                    'ordering': true,
                    'scrollY': '200px',
                    'scrollCollapse': true,
//                    drawCallback: function () { // this gets rid of duplicate headers
//                          $('.dataTables_scrollBody thead tr').css({ display: 'collapse' }); 
//                      },
                    'ajax': {
                        'url': '<%=Page.ResolveUrl("") %>' + '/api/CrmProyecto/', //'<%=Page.ResolveUrl("../../WebService/PortalRIK/GestionPromocion/Proyectos.svc") %>' + '/ObtenerTodos',
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
                            {   'data': 'Id', 
                                'render': function(data, type, full, meta){
                                    return '<a href="javascript:cargarProductosDeProyecto(' + full.Id + ', ' + full.Id_Cte + ', ' + meta.row + ')">' + full.Id + '</a>';
                                },
                                'width': '100px' 
                            },
                            { 'data': 'Id_Cte' },
                            { 'data': 'NombreCte' },
                            { 'data': 'Descripcion' },
                            { 'data': 'Cli_VPObservado' },
                            { 'data': 'Analisis' },
                            { 'data': 'Presentacion' },
                            { 'data': 'Negociacion' },
                            { 'data': 'Cierre' },
                    //{ 'data': 'Cancelacion' },
                            {'data': 'Avances' },
                            {
                                'data': null,
                                'render': function (data, type, full, meta) {
                                    return '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalEditarProyecto" data-action="2" data-rowindex="'+ meta.row +'" ><i class="fa fa-tasks"></i></button>';
                                },
                                'defaultContent': '<button type="button" class="btn btn-primary" disabled ><i class="fa fa-tasks"></i></button>'
                            }
                        ]
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

                $('#ddFiltroClave').on('show.bs.dropdown', function (e) {
                    e.stopPropagation();
                });

                $('#ddFiltroClaveOrdenarAsc').click(function () {
                    $('#tblProyectos').DataTable().order([0, 'asc']);
                    $('#tblProyectos').DataTable().draw(false);
                });

                $('#ddFiltroClaveOrdenarDesc').click(function () {
                    $('#tblProyectos').DataTable().order([0, 'desc']);
                    $('#tblProyectos').DataTable().draw(false);
                });



                $('input[iCheck]').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue'
                });

                //var $selUEN = $('#dvModalEditarProyecto #selUEN');
                //cargarUENs($, $selUEN);

                var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
                cargarTerritoriosPorRIK($, $selTerritorio, '<%=EntidadSesion.Id_Rik %>');

                inicializarModalNuevoProyecto();

                inicializarCampoProspecto();
                inicializarCampoCliente();
                inicializarCampoProductoBusqueda();

                $('#dvModalValuacion').on('show.bs.modal', function(event){
                    bloquearProyecto(_clienteDeOportunidad);
                });

                $('#iframeVentanaValuacion').load(function(){
                    //autoResize('iframeVentanaValuacion');
                    $('#dvCuerpoVentanaValuacion').unblock();
                });

                $('#aMapaOferta').click(function(e){
                    e.preventDefault();
                    $(this).ekkoLightbox();
                });

                determinarProyectoACargar();

            });

            

        })(jQuery);

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function bloquearProyecto(idCte) {
            _proyectoSeleccionado.EnValuacion=true;
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProyecto',
                type: 'PUT',
                cache: false,
                data: JSON.stringify(_proyectoSeleccionado),
                contentType: 'application/json',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(bloquearProyecto, this, idCte);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?Id_Vap=0&Id_Emp=0&Id_Cd=0&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
                _cargarProductosDeProyecto(_proyectoSeleccionado.Id, _proyectoSeleccionado.Id_Cte);
                $('#dvCuerpoVentanaValuacion').block();
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
                        }, 3006);
                        break;
                }
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function nuevoProyecto() {
            $('#btnDvModalEditarProyectoGuardar').show();
            $('#btnDvModalEditarProyectoActualizar').hide();
            limpiarFormaNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function editarProyecto(indice) {
            $('#btnDvModalEditarProyectoGuardar').hide();
            $('#btnDvModalEditarProyectoActualizar').show();
            $('#dvModalEditarProyecto').find('#myModalLabel').text('Editar Proyecto');
            var datos = _tablaProyectos.row(indice).data();
            _proyectoSeleccionado=datos;
            limpiarFormaNuevoProyecto();
            $('#hdnId_Op').val(datos.Id);
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
            
            cargarProductosDeProyecto(datos.Id, datos.Id_Cte, indice);


            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProyecto?opId=' + datos.Id + '&idCte=' + datos.Id_Cte,
                method: 'GET',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(editarProyecto, this, indice);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#hdnId_Op').val(datos.Id);
                $('#dvModalEditarProyecto #txtProspecto').val(datos.NombreCte);
                //Se establece el UEN del proyecto
                var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
                cargarTerritoriosPorRIK($, $selTerritorio, '<%=EntidadSesion.Id_Rik %>', jQuery.proxy(territoriosCargadosParaEdicion, null, $, $selTerritorio, response));
//                var $selUEN = $('#dvModalEditarProyecto #selUEN');
//                $selUEN.selectpicker('val', response.Id_Uen);
//                $selUEN.selectpicker('refresh');

                //Se cargan los segmentos de la UEN elegida y se establece el valor del segmento del proyecto
//                var $selSegmento = $('#dvModalEditarProyecto #selSegmento');
//                var idUen = $('#dvModalEditarProyecto #selUEN').selectpicker('val');
//                despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
//                cargarSegmentos(jQuery, $selSegmento, idUen, jQuery.proxy(segmentosCargadosParaEdicion, null, $, $selSegmento, response)); //Se instruye a cargar el territorio y el área después de la carga de los segmentos para la operación de EDICION.

                
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

        function cargarSegmento(jqelement, objSeg) {
            jqelement.append('<option value="' + objSeg.Id_Seg + '">' + objSeg.Seg_Descripcion + '</option>');
            jqelement.selectpicker('val', 0);
            jqelement.selectpicker('refresh');

            $('#dvModalEditarProyecto #hdnDim_Id_Uen').val(objSeg.Id_Uen);
            $('#dvModalEditarProyecto #hdnDim_Id_Seg').val(objSeg.Id_Seg);
            $('#dvModalEditarProyecto #txtDimension').val(objSeg.Seg_Unidades);
            $('#dvModalEditarProyecto #txtPrecioUnidad').val(objSeg.Seg_ValUniDim);
        }

        function cargarSelUEN(jqelement, objUen) {
            jqelement.append('<option value="' + objUen.Id_Uen + '">' + objUen.Uen_Descripcion + '</option>');
            jqelement.selectpicker('val', 0);
            jqelement.selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorTerritorio() {
            $('#selUEN').find('option').remove();
            $('#selUEN').selectpicker('refresh');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

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

        function cargarUENs($, jqElement, onSuccess, onFailure) {
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatUEN/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>',
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

        function selUEN$on_change() {
            var $selSegmento = $('#dvModalEditarProyecto #selSegmento');
            var idUen = $('#dvModalEditarProyecto #selUEN').selectpicker('val');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
            cargarSegmentos(jQuery, $selSegmento, idUen);
        }

        function cargarSegmentos($, jqElement, idUen, onSuccess, onFailure) {
            //mostrar el indicador de operación en proceso
            $('#imgProcesandoSegmentoDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatSegmento/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idUen=' + idUen,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
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

        function selSegmento$on_change() {
            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var $selArea = $('#dvModalEditarProyecto #selArea');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
            //cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(cargarAreas, null, jQuery, $selArea, idSeg));
            
            cargarAreas(jQuery, $selArea, idSeg);
        }

        function cargarTerritoriosPorRIK($, jqElement, idRik, onSuccess, onFailure) {
            $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + idRik,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
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

        function cargarTerritorios($, jqElement, idSeg, onSuccess, onFailure) {
            $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>' + '&idSeg=' + idSeg,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
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

        function cargarAreas($, jqElement, idSeg, onSuccess, onFailure) {
            $('#imgProcesandoAreaDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatArea/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idSeg=' + idSeg,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
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

        function cargarSoluciones($, jqElement, idArea, onSuccess, onFailure) {
            $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatSolucion/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idArea=' + idArea,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
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

        function selArea$on_change() {
            var $selSolucion = $('#dvModalEditarProyecto #selSolucion');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
            cargarSoluciones(jQuery, $selSolucion, idArea);
        }

        function cargarAplicaciones($, jqElement, idSol, onSuccess, onFailure) {
            $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeIn();
            var idUen=$('#dvModalEditarProyecto #selUEN').selectpicker('val');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            var idCte = $('#dvModalEditarProyecto #hdnId_Cliente').val();
            var idOp = $('#dvModalEditarProyecto #hdnId_Op').val();
            var idOpVar = idOp != null ? idOp : '0';
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatAplicacion/?idUen=' + idUen + '&idSeg=' + idSeg + '&idArea=' + idArea + '&idSol=' + idSol + '&idOp=' + idOpVar + '&idCte=' + idCte,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
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
                    var node=$(contenidoPersonalizadoAplicacion(element));
                    node.data('obj', element);
                    node.find('[chkAplicacion]').data('obj', element);
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

        function txtCantidad$onchange(sender){
            var cantidad=$('#txtCantidad').val();
            if(cantidad==''){
                cantidad=0;
            }
            var elementos=$('#lstAplicacion [item]');
            $.each(elementos, function(index, item){
                var objetoDatos=$(item).data('obj');
                $(item).find('#tdVPT').text(objetoDatos.Apl_Potencial/100.0 * cantidad);
            });
        }
        
        var _aplicacionesSeleccionadas=[];
        function actualizarAplicacionesVPO(idOp, onSuccess, onFailure){
            $.each(_aplicacionesSeleccionadas, function(index, item){
                item.Id_Op=idOp;
            });
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmOportunidadesAplicacion',
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

        function contenidoPersonalizadoAplicacion(aplicacion) {
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
                                    aplicacion.Apl_Potencial/100.0 * cantidad +
                                '</td>' +
                                '<td style="width: 33%;">' +
                                    '<div class="row">' +
                                        '<div class="col-md-1">' +
                                           'VPO:' +
                                        '</div>' +
                                        '<div class="col-md-6">' +
                                            '<input type="text" id="txtAplVPO_' + aplicacion.Id_Apl + '" style="display: none;" class="form-control" onchange="txtAplVPO$onchange(this)">' +//aplicacion.Porcentaje/100.0 +
                                        '</div>' + 
                                    '</div>' + 
                                '</td>' + 
                                '<td style="text-align: right;">' +
                                    '<input type="checkbox" id="chkApl_' + aplicacion.Id_Apl + '" data-idapl="' + aplicacion.Id_Apl + '" onchange="chkApl_onchange(this)" chkAplicacion/>' +
                                '</td>' +
                            '</tr>' +
                        '</table>' +
                    '</div>'
            ;
        }

        function txtAplVPO$onchange(sender){
            var objetoDatos=$(sender).data('obj');
            objetoDatos.CrmOpAp_VPO=$(sender).val();
        }

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

        function deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').find('option').remove();
            $('#selSegmento').selectpicker('refresh');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        }

        function habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto() {
            $('#selArea').prop('disabled', false);
            $('#selArea').selectpicker('refresh');

            //$('#selTerritorio').prop('disabled', false);
            //$('#selTerritorio').selectpicker('refresh');
        }

        function deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
            $('#selArea').selectpicker('refresh');
            $('#selTerritorio').selectpicker('refresh');

            deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
            $('#selArea').find('option').remove();
            //$('#selTerritorio').find('option').remove();
            $('#selArea').selectpicker('refresh');
            //$('#selTerritorio').selectpicker('refresh');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        function habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto() {
            $('#selSolucion').prop('disabled', false);
            $('#selSolucion').selectpicker('refresh');
        }

        function deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
            $('#selSolucion').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
            $('#selSolucion').find('option').remove();
            $('#selSolucion').selectpicker('refresh');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        function habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').prop('disabled', false);
            $('#selAplicacion').selectpicker('refresh');
        }

        function deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').find('option').remove();
            $('#selAplicacion').selectpicker('refresh');

            var $lstAplicacion = $('#lstAplicacion');
            $lstAplicacion.find('div').remove();

        }

        function inhabilitarSelectoresDialogoNuevoProyecto() {
            deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

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
                        nuevoProyecto();
                        break;
                    case 2:
                        $('#dvModalEditarProyecto').find('#myModalLabel').text('Editar Proyecto');
                        var row = $(trigger).data('rowindex');
                        editarProyecto(row);
                        $('#dvDetalles:hidden').slideDown();
                        break;
                }
            });

            $('#dvModalEditarProyecto').on('shown.bs.modal', function (event) {
                //$('#txtProspecto').autocomplete("option", "appendTo", ".eventInsForm");
            });
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function limpiarFormaNuevoProyecto() {
            $('#txtProspecto').val('');
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
            $('#selTerritorio').selectpicker('val', 0);
            $('#selTerritorio').selectpicker('refresh');
            despopularCascadaDependientesSelectorTerritorio();
            $('#txtCantidad').val('');
        }

        function segmentosCargadosParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Seg);
            $(jqElement).selectpicker('refresh');

            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
            cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(territoriosCargadosParaEdicion, null, jQuery, $selTerritorio, data));
        }

        function territoriosCargadosParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Ter);
            $(jqElement).selectpicker('refresh');

            selTerritorio$on_change(jqElement);

            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var $selArea = $('#dvModalEditarProyecto #selArea');

            cargarAreas($, $selArea, idSeg, $.proxy(areasCargadasParaEdicion, null, $, $selArea, data));
        }

        function areasCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.ID_Area);
            $(jqElement).selectpicker('refresh');

            var $selSolucion = $('#dvModalEditarProyecto #selSolucion');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
            cargarSoluciones($, $selSolucion, idArea, $.proxy(solucionesCargadasParaEdicion, null, $, $selSolucion, data));
        }

        function solucionesCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Sol);
            $(jqElement).selectpicker('refresh');

            var $selAplicacion = $('#dvModalEditarProyecto #selAplicacion');
            var idSol = $('#dvModalEditarProyecto #selSolucion').selectpicker('val');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
            cargarAplicaciones($, $selAplicacion, idSol, $.proxy(aplicacionesCargadasParaEdicion, null, $, $selAplicacion, data));
        }

        function aplicacionesCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Aplicaciones);
            $(jqElement).selectpicker('refresh');

            $.each(data.CrmOportunidadesAplicaciones/*Aplicaciones*/, function (index, element) {
                $('#chkApl_' + element.Id_Apl).iCheck('check');
                $('#txtAplVPO_' + element.Id_Apl).val(element.CrmOpAp_VPO);
                $('#txtAplVPO_' + element.Id_Apl).data('obj', element);
                _aplicacionesSeleccionadas.push(element);
            });

            $('#dvModalEditarProyecto #txtVPT').val(data.ValorPotencialT);
        }

        function crearProyecto() {
            $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
            $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
            $(this).prop('disabled', true);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProyecto',
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
                actualizarAplicacionesVPO(response.Id_Op, function(){
                    $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito');
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

        function crearProyectoYContinuar() {
            $(this).prop('disabled', true);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProyecto',
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

        function determinarProyectoACargar(){
            <% if (CargarDatosProyecto()) {%>
            $('#dvDetalles:hidden').slideDown();
            //precargarProyectoSeleccionado('<%= Id_Cliente.ToString() %>', '<%= Id_Op.ToString() %>');
            _precargarProductosDeProyecto('<%= Id_Op.ToString() %>', '<%= Id_Cliente.ToString() %>');
            <%} %>
        }

        function actualizarProyecto() {
            $(this).prop('disabled', true);
            $('#dvModalEditarProyecto #selUEN').prop('disabled', false);
            $('#dvModalEditarProyecto #selSegmento').prop('disabled', false);
            $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
            
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProyecto',
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
            });
        }

        function _buscarProspecto(request, response) {
            var terminoDeBusqueda = $('#txtProspecto').val();
            var $imgProspectoEnOperacion = $('#dvModalEditarProyecto #imgProspectoEnOperacion');
            $imgProspectoEnOperacion.show();
            var data = null;
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProspecto?terminoDeBusqueda=' + terminoDeBusqueda,
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

        function _buscarCliente(request, response) {
            var terminoDeBusqueda = $('#selCliente').val();
            var data = null;
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatCliente?terminoDeBusqueda=' + terminoDeBusqueda,
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
                }
            });
            $("#txtProspecto").attr('autocomplete', 'on');
        }

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

        function _buscarProducto(request, response) {
            var terminoDeBusqueda = $('#txtProductoBusqueda').val();
            var data = null;
            $('#imgBuscandoProducto').show();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmCatalogoUnico?idCte=' + _clienteDeOportunidad + '&idOp=' + _oportunidadSeleccionada + '&terminoBusqueda=' + terminoDeBusqueda,
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

        var _productoElegido = null;
        function inicializarCampoProductoBusqueda() {
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

        function asignarValoresACamposDeFormaParaAgregarProducto(data){
            $('#hdnAgregarProducto_Id_Uen').val(data.Id_Uen);
            $('#hdnAgregarProducto_Id_Seg').val(data.Id_Seg);
            $('#hdnAgregarProducto_Id_Area').val(data.Id_Area);
            $('#hdnAgregarProducto_Id_Sol').val(data.Id_Sol);
            $('#hdnAgregarProducto_Id_Apl').val(data.Id_Apl);
            $('#hdnAgregarProducto_Id_SubFam').val(data.Id_SubFam);
        }

        var _oportunidadSeleccionada=null;
        var _clienteDeOportunidad = null;

        function limpiarListadoDeProductos() {
            var $lstProductos = $('#lstProductos');
            $lstProductos.find('[elementoDeLista]').remove();
        }

        function cargarInfoSeccionGeneral(datos){
            $('#ddSegmento').text(datos.Seg_Descripcion);
            $('#ddArea').text(datos.Area_Descripcion);
            $('#ddSolucion').text(datos.Sol_Descripcion);
            $('#ddVPT').text('$' + datos.ValorPotencialTeorico);
            $('#ddVPME').text('$' + datos.VentaPromedioMensualEsperada);

            switch(datos.Estatus){
                case 1:
                    
                break;
                case 2:
                break;
                case 3:
                break;
                case 4:
                break;
            }
        }

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

        function actualizarComandosValuacion(datosProyecto){
            if(datosProyecto.CrmOp_PuedeGenerarValuacion==true){
                $('#btnGenerarValuacion').show();
            }else{
                $('#btnGenerarValuacion').hide();
            }

            if(datosProyecto.CrmOp_PuedeGenerarPE==true){
                $('#btnGenerarValuacion').show();
            }else{
                $('#btnGenerarValuacion').hide();
            }
        }

        var _lastSelectedNode=null;
        var _proyectoSeleccionado=null;
        function cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, indice) {
            var row=_tablaProyectos.row(indice);
            var node=row.node();
            var datos = row.data();
            _proyectoSeleccionado=datos;
            actualizarComandosValuacion(_proyectoSeleccionado);
            if(_lastSelectedNode==null){
                $(node).addClass('active');
            }else{
                if(_lastSelectedNode!=node){
                    $(_lastSelectedNode).removeClass('active');
                    $(node).addClass('active');
                }
            }
            _lastSelectedNode=node;
            
            cargarInfoSeccionGeneral(datos);
            _oportunidadSeleccionada = oportunidadSeleccionada;
            _clienteDeOportunidad = clienteDeOportunidad;
            limpiarCamposBusquedaProducto();
            limpiarListadoDeProductos();

            $('#dvDetalles:hidden').slideDown();
            $('#imgCargandoProductos').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
                cache: false,
                type: 'GET',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad);
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
                $('#imgCargandoProductos').fadeOut();
            });
        }

        function _cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, onSuccess) {
            actualizarComandosValuacion(_proyectoSeleccionado);
            cargarInfoSeccionGeneral(_proyectoSeleccionado);
            _oportunidadSeleccionada = oportunidadSeleccionada;
            _clienteDeOportunidad = clienteDeOportunidad;
            limpiarListadoDeProductos();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
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

        function _precargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad) {
            _oportunidadSeleccionada = oportunidadSeleccionada;
            _clienteDeOportunidad = clienteDeOportunidad;
            limpiarListadoDeProductos();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
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
//                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
//                    onSuccess();
//                }

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

        function agregarProducto(_this){
            $(_this).prop('disabled', true);
            $('#imgAgregandoProducto').show();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmOportunidadesProductos',
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
                $('#hdnAgregarProducto_Id_Uen').val('');
                $('#hdnAgregarProducto_Id_Seg').val('');
                $('#hdnAgregarProducto_Id_Area').val('');
                $('#hdnAgregarProducto_Id_Sol').val('');
                $('#hdnAgregarProducto_Id_Apl').val('');
                $('#hdnAgregarProducto_Id_SubFam').val('');
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
                $(_this).prop('disabled', false);
                $('#imgAgregandoProducto').hide();
            });
        }

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
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmOportunidadesProductos', //?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
                type: 'PUT',
                cache: false,
                data: JSON.stringify(objectCopy),
                contentType: 'application/json',
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(retirarProducto, this, idCte, idOp, idPrd);
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

        function retirarProducto(idCte, idOp, idPrd){
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmOportunidadesProductos?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
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

        // Crea Renglon de Producto
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
                    '<div class="list-view-pf-main-info">' +
                    '<div class="list-view-pf-body">' +
                        '<div class="list-view-pf-description">' +
                            '<div class="list-group-item-heading">' +  element.Nombre + '</div>' +
                            //'<div class="list-group-item-text">' + element.Ruta + '</div>' +
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
                                                '<td> &nbsp;' + element.ProductoSerializable.Prd_UniNe + '</td>' +
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

        function limpiarCamposBusquedaProducto() {
            $('#txtProductoBusqueda').val('');
            $('#txtProductoCantidad').val('');
        }

        function dimensionElegida(idUen, idSeg, unidades){
            $('#dvModalEditarProyecto #hdnDim_Id_Uen').val(idUen);
            $('#dvModalEditarProyecto #hdnDim_Id_Seg').val(idSeg);
            $('#dvModalEditarProyecto #txtDimension').val(unidades);
            $('#dvModalDimension').modal('hide');
        }

    </script>
    <script src="<%=Page.ResolveUrl("~/js/jquery.radios-to-slider.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-treeview.min.js") %>"></script>
    <!--<script src="//rawgit.com/jonmiles/bootstrap-treeview/v1.2.0/dist/bootstrap-treeview.min.js"></script>-->
</asp:Content>
