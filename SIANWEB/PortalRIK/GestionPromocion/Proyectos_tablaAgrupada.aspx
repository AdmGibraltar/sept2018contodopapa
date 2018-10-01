<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="Proyectos_tablaAgrupada.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Proyectos_tablaAgrupada" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/SelectorDimension.ascx" TagPrefix="uc" TagName="SelectorDimension" %>
<%--<%@ Register Src="~/Controles/Cliente/UCBootstrapConfirm.ascx" TagPrefix="uc" TagName="BootstrapConfirm" %>--%>
<%--<%@ Register Src="~/Controles/Cliente/UCPatternflyToast.ascx" TagPrefix="uc" TagName="UCPatternflyToast" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/radios-to-slider.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/bootstrap-treeview.min.css")%>" rel="stylesheet">
    
    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/ekko-lightbox.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/form-wizard.css")%>" rel="stylesheet">
    <link href="//cdn.datatables.net/buttons/1.1.2/css/buttons.dataTables.min.css" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">

    <style>
        
        #loader {
          border: 16px solid #f3f3f3;
          border-radius: 50%;
          border-top: 16px solid #3498db;
          width: 120px;
          height: 120px;
          -webkit-animation: spin 2s linear infinite;
          animation: spin 2s linear infinite;
          margin-left:250px;
          margin-top:250px;
        }

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
        
        td.details-control
        {
            cursor: pointer;
        }
        
        td.details-control:before
        {
            display: block;
            cursor: pointer;
            content: '\f055';
            font-family: FontAwesome;
            text-align: center;
        }
        
        tr.shown td.details-control:before
        {
            display: block;
            cursor: pointer;
            content: '\f056';
            font-family: FontAwesome;
            text-align: center;
        }
        
        div.slider
        {
            display: none;
        }
        
        /*div.dataTables_processing { z-index: 1; }*/
        
        .dataTables_wrapper
        {
            /*height: 300px;*/
        }
        
        .dataTables_scrollBody
        {
            min-height: 200px;
        }

        .inactiveLink {
            pointer-events: none;
            cursor: default;
        }
        
        .dataTables_processing {
            left: 50%;
            position: absolute;
            top: 50%;
            z-index: 100;
        } 	
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">
    <div class="row" style="display: block;">
        <div class="col-sm-12 col-md-12">        
            <div class="row ROWPAD MARGIN_BT5">
                <div class="col-sm-12 col-md-12 ROWPAD">
                    
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <h3 style="display: inline-block;"><strong>Proyectos</strong></h3> 
                            </td>
                            <td style="width:20px;" valign="middle">                            
                                <div style="display:none;" id="Gerente_Icono">
                                <!--i class="fas fa-user-tie"></i-->                            
                                </div>
                            </td>
                            <td style="width:30px;" valign="middle">                            
                                <label style="margin-top:5px;" id="Gerente_lbRik">Rik&nbsp;</label>
                            </td>
                            <td style="width:180px;">
                                <button 
                                    id="Gerente_btnCambiarUsuario"
                                    class="btn pull-right btnGerente" 
                                    onclick="btnGerente_CambiarUsuario();" 
                                    data-action="1" 
                                    data-toggle="tooltip" 
                                    title="Haga clic aqu&iacute; para seleccionar el Rik."
                                    style="width:100%; display:none;">
                                    &nbsp;
                                </button>                            
                            </td>
                            <td style="width:5px;">                            
                            </td>
                            <td style="width:100px;">                            
                                <button id="btnNuevoProyecto" class="btn btn-primary pull-right" data-toggle="modal" data-target="#dvModalEditarProyecto" data-action="1" style="width:100%;">
                                    <i class="fa fa-plus"></i>&nbsp;Nuevo
                                </button>
                            </td>
                        </tr>
                    </table>

                </div>       

            </div>
            <div class="row ROWPAD">
                <div class="col-sm-12 col-md-12 ROWPAD">

                <div id="dvLoading"></div>

                <table class="datatable table table-bordered display" id="tblProyectos" cellspacing="0">
                    <thead>
                        <tr>
                            <th>Proyectos</th>
                            <th>No. Cliente</th>
                            <th>Cliente</th>
                            <th>Valuación</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                </div>
            </div>

            <div style="display: none;" id="DetalleProyecto">

            <div class="row ROWPAD">
                <div class="col-sm-12 col-md-12 ROWPAD">
                <h3><strong>Detalles del proyecto</strong></h3>
                </div>
            </div>

            <div class="row ROWPAD">
                <div class="col-md-12 ROWPAD">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a href="#dvGeneral" data-toggle="tab">General</a> 
                        </li>
                        <li><a href="#dvProductos" data-toggle="tab">Productos
                            <img id="imgCargandoProductos" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></a>
                        </li>
                        <li><a href="#dvHerramientas" data-toggle="tab">Herramientas</a> 
                        </li>
                        <!--<li><a href="#dvProductos" data-toggle="tab">Propuesta Tecno-Econ&oacute;mica</a></li>-->
                    </ul>
                    <!-- Tab panes -->
                            <div class="tab-content">
                               
                                <div role="tabpanel" class="tab-pane active" id="dvGeneral">

                                    <input type="hidden" id="hfDatosGenerales_Id_Apl" value="">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <br />
                                            <dl class="dl-horizontal">
                                                <dt style="text-align: left">Segmento </dt>
                                                <dd id="ddSegmento"></dd>
                                                <dt style="text-align: left">Área </dt>
                                                <dd id="ddArea"></dd>
                                                <dt style="text-align: left">Solución </dt>
                                                <dd id="ddSolucion"></dd>
                                                <dt style="text-align: left">Valor potencial teórico </dt>
                                                <dd id="ddVPT"></dd>
                                                <%--<dt style="text-align: left">Valor potencial estimado </dt>
                                                <dd>$5,000.00</dd>--%>
                                                <dt style="text-align: left; white-space: normal">Venta promedio mensual esperada</dt>
                                                <dd id="ddVPME"></dd>
                                            </dl>
                                            <button class="btn btn-default" type="button" onclick="cancelarProyecto(this)" id="btnCancelarProyecto">
                                                <i class="fa fa-times" aria-hidden="true"></i>
                                                Cancelar Proyecto
                                            </button>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">                                            
                                            <div class="wizard">
                                                <div class="wizard-inner">
                                                    <div class="connecting-line"></div>
                                                    <ul class="nav nav-tabs" role="tablist">

                                                        <li role="presentation" class="active">
                                                            <a href="#step1" data-toggle="tab" aria-controls="step1" role="tab" title="Análisis">
                                                                <span class="round-tab">
                                                                    <i class="glyphicon glyphicon-folder-open"></i>
                                                                </span>
                                                            </a>
                                                        </li>

                                                        <li role="presentation">
                                                            <a href="#step2" data-toggle="tab" aria-controls="step2" role="tab" title="Presentación">
                                                                <span class="round-tab">
                                                                    <i class="glyphicon glyphicon-pencil"></i>
                                                                </span>
                                                            </a>
                                                        </li>
                                                        <li role="presentation" class="disabled">
                                                            <a href="#step3" data-toggle="tab" aria-controls="step3" role="tab" title="Negociación">
                                                                <span class="round-tab">
                                                                    <i class="glyphicon glyphicon-picture"></i>
                                                                </span>
                                                            </a>
                                                        </li>

                                                        <li role="presentation" class="disabled">
                                                            <a href="#complete" data-toggle="tab" aria-controls="complete" role="tab" title="Cierre">
                                                                <span class="round-tab">
                                                                    <i class="glyphicon glyphicon-ok"></i>
                                                                </span>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <!--
                                                <form role="form">
                                                    <div class="tab-content">
                                                        <div class="tab-pane active" role="tabpanel" id="step1">
                                                            <h3>Step 1</h3>
                                                            <p>This is step 1</p>
                                                            <ul class="list-inline pull-right">
                                                                <li><button type="button" class="btn btn-primary next-step">Save and continue</button></li>
                                                            </ul>
                                                        </div>
                                                        <div class="tab-pane" role="tabpanel" id="step2">
                                                            <h3>Step 2</h3>
                                                            <p>This is step 2</p>
                                                            <ul class="list-inline pull-right">
                                                                <li><button type="button" class="btn btn-default prev-step">Previous</button></li>
                                                                <li><button type="button" class="btn btn-primary next-step">Save and continue</button></li>
                                                            </ul>
                                                        </div>
                                                        <div class="tab-pane" role="tabpanel" id="step3">
                                                            <h3>Step 3</h3>
                                                            <p>This is step 3</p>
                                                            <ul class="list-inline pull-right">
                                                                <li><button type="button" class="btn btn-default prev-step">Previous</button></li>
                                                                <li><button type="button" class="btn btn-default next-step">Skip</button></li>
                                                                <li><button type="button" class="btn btn-primary btn-info-full next-step">Save and continue</button></li>
                                                            </ul>
                                                        </div>
                                                        <div class="tab-pane" role="tabpanel" id="complete">
                                                            <h3>Complete</h3>
                                                            <p>You have successfully completed all steps.</p>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                </form>
                                                -->
                                            </div>

                                            <!--<div id="dvProgreso" style="width: 100%">
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
                                            </div>-->

                                        </div>
                                    </div>
                                </div>
                               
                                <div role="tabpanel" class="tab-pane" id="dvProductos" style="margin-bottom:50px;">

                                    <!-- contenedorProductosDeAplicacion -->
                                    <!-- contenedorProductosDeAplicacion -->
                                    <!-- contenedorProductosDeAplicacion -->
                                    <!-- contenedorProductosDeAplicacion -->
                                    
                                    <h4>&nbsp;<strong>Productos de aplicaci&oacute;n</strong></h4>

                                    <div class="row" id="contenedorProductosDeAplicacion">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <div class="panel panel-default">
                                                <!--div class="panel-heading">
                                                    <h4>Productos de Aplicaci&oacute;n</h4>
                                                </div-->
                                                <div class="panel-body" id="contendorProductos" style="padding:0px;">
                                                    <!--div class="row">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                        <h4>Productos de Aplicaci&oacute;n</h4>
                                                        </div>
                                                    </div-->
                                                    <div class="row">
                                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                            <form id="frmAgregarProducto">
                                                                <input type="hidden" id="hdnAgregarProducto_Id_Op" name="Id_Op" />
                                                                <input type="hidden" id="hdnAgregarProducto_Id_Cte" name="Id_Cte" />
                                                                <input type="hidden" id="hdnAgregarProducto_COP_EsQuimico" name="COP_EsQuimico" value="true" />
                                                                <input type="hidden" id="hdnAgregarProducto_COP_DilucionAntecedente" name="COP_DilucionAntecedente" value="1" />
                                                                <input type="hidden" id="hdnAgregarProducto_COP_DilucionConsecuente" name="COP_DilucionConsecuente" value="1" />
                                                                <input type="hidden" id="hdnAgregarProducto_COP_ConsumoMensual" name="COP_ConsumoMensual" value="0" />
                                                                <input type="hidden" id="hdnAgregarProducto_COP_CostoEnUso" name="COP_CostoEnUso" value="0" />

                                                                <div class="row ui-front" id="dvBusquedaProducto">
                                                                        <div class="col-md-12 tooltip-demo">
                                                                            <table class="table MARGIN_BOTTOM5" width="100%">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th></th>
                                                                                    <th style="text-align: center;">Clave</th>
                                                                                    <th style="text-align: center;">Descripci&oacute;n</th>
                                                                                    <th style="text-align: center;">Cantidad</th>
                                                                                    <th></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="text-align: center; width: 5%;">
                                                                                        <img id="imgBuscandoProducto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                                                                        <img id="imgAgregandoProducto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                                                                    </td>
                                                                                    <td style="text-align: center; width: 12%;">                                                                                            
                                                                                        <input type="text" id="txtProductoBusqueda" name="txtProductoBusqueda" class="form-control" placeholder="Clave" title="Ingrese parte de la descripción o del código para buscar el producto" data-toggle="tooltip" />
                                                                                    </td>
                                                                                    <td style="text-align: center;" id="tdProductoDescripcion">
                                                                                        <input type="text" id="txtProductoDescripcion" class="form-control" readonly="readonly" disabled placeholder="Descripción" />
                                                                                        <span class="help-block" id="spanProductoDescripcionHlp" style="display: none;">Producto no encontrado</span>
                                                                                    </td>
                                                                                    <td style="text-align: center; width: 12%;">
                                                                                        <input type="text" id="txtProductoCantidad" name="COP_Cantidad" class="form-control" placeholder="Cantidad" title="Ingrese la cantidad del producto" />
                                                                                    </td>
                                                                                    <td style="text-align: center; width: 12%;">
                                                                                        <button 
                                                                                            id="btnAgregarProducto" 
                                                                                            type="button" 
                                                                                            class="btn btn-default" 
                                                                                            onclick="$agregarProductoAplicacion($('#contendorProductos'),this)">Agregar
                                                                                        </button>
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
                                                                    <input type="hidden" id="hdnTipoProducto" name="hdnTipoProducto" value="1"/>
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                                                                        
                                                    <div class="row" id="contenidoSeccionProductos">
                                                        <div class="col-md-12">
                                                            <div class="list-group list-view-pf" id="lstProductos"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <!--hr class="PADDING_TB_10" /-->

                                    <!--Otros productos-->
                                    <!--Otros productos-->
                                    <!--Otros productos-->
                                    <!--Otros productos-->

                                    <h4>&nbsp;<strong>Otros productos</strong></h4>

                                    <div class="row" id="contenedorOtrosProductos" style="display: block;">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <div>
                                                <div class="panel panel-default">
                                                    <!--div class="panel-heading">
                                                        <h4>Otros Productos</h4>
                                                    </div-->
                                                    <div class="panel-body" style="padding:0px;">
                                                        <div class="row">
                                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                <form id="frmAgregarProducto">
                                                                    <input type="hidden" id="hdnAgregarProducto_Id_Op" name="Id_Op" />
                                                                    <input type="hidden" id="hdnAgregarProducto_Id_Cte" name="Id_Cte" />
                                                                    <input type="hidden" id="hdnAgregarProducto_COP_EsQuimico" name="COP_EsQuimico" value="true" />
                                                                    <input type="hidden" id="hdnAgregarProducto_COP_DilucionAntecedente" name="COP_DilucionAntecedente" value="1" />
                                                                    <input type="hidden" id="hdnAgregarProducto_COP_DilucionConsecuente" name="COP_DilucionConsecuente" value="1" />
                                                                    <input type="hidden" id="hdnAgregarProducto_COP_ConsumoMensual" name="COP_ConsumoMensual" value="0" />
                                                                    <input type="hidden" id="hdnAgregarProducto_COP_CostoEnUso" name="COP_CostoEnUso" value="0" />
                                                                    <div class="row ui-front" id="dvBusquedaProducto">
                                                                            <div class="col-md-12 tooltip-demo">
                                                                                <table class="table MARGIN_BOTTOM5" width="100%">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th></th>
                                                                                            <th style="text-align: center;">Clave</th>
                                                                                            <th style="text-align: center;">Descripci&oacute;n</th>
                                                                                            <th style="text-align: center;">Cantidad</th>
                                                                                            <th>
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="text-align: center; width: 5%;">
                                                                                                <img id="imgBuscandoProducto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                                                                                <img id="imgAgregandoProducto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                                                                            </td>
                                                                                            <td style="text-align: center; width: 12%;">

                                                                                                <input type="text" id="txtProductoBusquedaOtros" class="form-control" placeholder="Clave" title="Ingrese parte de la descripción o del código para buscar el producto" data-toggle="tooltip" />

                                                                                            </td>
                                                                                            <td style="text-align: center;" id="tdProductoDescripcion">
                                                                                                <input type="text" id="txtProductoDescripcion" class="form-control" readonly="readonly" disabled placeholder="Descripción" />
                                                                                                <span class="help-block" id="spanProductoDescripcionHlp" style="display: none;">Producto no encontrado</span>
                                                                                            </td>
                                                                                            <td style="text-align: center; width: 12%;">
                                                                                                <input type="text" id="txtProductoCantidad" name="COP_Cantidad" class="form-control" placeholder="Cantidad" title="Ingrese la cantidad del producto" />
                                                                                            </td>
                                                                                            <td style="text-align: center; width: 12%;">
                                                                                                <button 
                                                                                                    id="btnAgregarProducto" 
                                                                                                    type="button" 
                                                                                                    class="btn btn-default" 
                                                                                                    onclick="$agregarProductoOtro($('#contenedorOtrosProductos'),this)">Agregar
                                                                                                </button>
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
                                                                </form>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="contenidoSeccionProductos">
                                                            <div class="col-md-12">
                                                                <div class="list-group list-view-pf" id="lstProductos"></div>                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div role="tabpanel" class="tab-pane" id="dvHerramientas">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="tvHerramientas"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
            </div>

            </div>
        </div>
    </div>

    
    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="dvModalListaRepresentantes" class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">                    
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="img3" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="h10">
                        <table>
                            <tr>
                                <td>Representantes disponibles&nbsp;</td>
                                <td>
                                    <img id="spinner_listarep" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></a>
                                </td>
                            </tr>
                        </table>                        
                    </h4>
                </div>
                <div class="modal-body">
                    
                    <table id="tblUsuariosRik" class="table table-hover" style="width:100%;">
                        <thead>
                            <tr>
                                <th>Rik</th>
                                <th>Nombre</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    
                </div>
                <div class="modal-footer">
                    
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnListadoRik_Aplicar" onclick="btnListaRep_Seleccion();">Aplicar</button>                
                </div>
            </div>
        </div>
    </div>

    <%--MODAL --%>
    <div class="modal fade" id="dvModalEditarProyecto" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
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
                        
                        <div class="form-inline">
                            <div class="form-group">
                                <label>
                                    Tipo de Venta
                                </label>
                                <br />

                                <div class="radio">
                                    <label class="radio-inline">
                                        Instalada<input type="radio" id="rbVtaInstalada" name="tipoVenta" class="form-control" value="1" />
                                    </label>
                                </div>
                                &nbsp;
                                <div class="radio">
                                    <label class="radio-inline">
                                        Espor&aacute;dica<input type="radio" id="rbVtaEsporadica" name="tipoVenta" class="form-control" value="2"/>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <br />

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
                            <input type="text" id="selCliente" class="form-control" placeholder="Nombre del Cliente" />
                        </div>
                        <div class="form-group ui-front" id="dvFgProspecto">
                            <label for="txtProspecto">
                                Prospecto <img id="imgProspectoEnOperacion" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <input type="text" id="txtProspecto" class="form-control" placeholder="Prospecto" />
                        </div>
                        <div class="form-group">
                            <label for="selTerritorio">
                                Territorio&nbsp<i class="fa fa-flag-checkered" aria-hidden="true"></i><img id="imgProcesandoTerritorioDvModalNuevoProyecto" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <select id="selTerritorio" onchange="selTerritorio$on_change(this)" name="Territorio" class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="selUEN">
                                UEN</label>&nbsp<i class="fa fa-industry" aria-hidden="true"></i>
                            <select id="selUEN" name="Uen" onchange="selUEN$on_change()" disabled class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="selSegmento">
                                Segmento&nbsp<i class="fa fa-tasks" aria-hidden="true"></i><img id="imgProcesandoSegmentoDvModalNuevoProyecto" style="display: none;"
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

                        <div class="row" style="margin-bottom:5px;">
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
                                <input id="txtPrecioUnidad" type="text" class="form-control" data-inputmask="'alias': 'currency', 'autoUnmask':'true'" placeholder="$0.0" title="Precio por unidad de dimensión" data-toggle="tooltip" disabled/>
                            </div>
                            <div class="col-md-2 tooltip-demo">
                                <small>Cantidad:</small>
                                <input id="txtCantidad" name="Dim_Cantidad" type="text" class="form-control" placeholder="0" title="Cantidad de la unidad elegida" data-toggle="tooltip" onchange="txtCantidad$onchange(this)"/>
                            </div>
                            <div class="col-md-2 tooltip-demo">
                                <small>VPO:</small>
                                <input id="txtVPM" name="CrmOp_VPM" type="text" class="form-control" data-inputmask="'alias': 'currency', 'autoUnmask':'true'" placeholder="$0.00" title="Venta Promedio Mensual Esperada" disabled data-toggle="tooltip"/>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="selArea">
                                Área&nbsp<i class="fa fa-share-alt" aria-hidden="true"></i><img id="imgProcesandoAreaDvModalNuevoProyecto" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <select id="selArea" name="Area" onchange="selArea$on_change()" class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="selSolucion">
                                Solución&nbsp<i class="fa fa-recycle" aria-hidden="true"></i><img id="imgProcesandoSolucionDvModalNuevoProyecto" style="display: none;"
                                    src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                            <select id="selSolucion" name="Solucion" onchange="selSolucion$on_change()" class="selectpicker form-control">
                            </select>
                        </div>
                        <div class="form-group">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <label for="selAplicacion">
                                                Aplicación&nbsp<i class="fa fa-sitemap" aria-hidden="true"></i><img id="imgProcesandoAplicacionDvModalNuevoProyecto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                            </label>
                                        </td>
                                        <td>
                                            <a data-title="Oferta" href="http://www.todoenunclick.com/Notas/Imagenes/arbol_jerarquia.jpg" id="aMapaOferta"><h6>&nbsp;&nbsp;Mapa de aplicaciones</h6></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
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

    <%--MODAL /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
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

    <%--MODAL 
    <div id="dvModalPropuestaTE" style="width: 100%; height: 100%" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog-fs" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="imgCargandoVentanaPropuestaTE" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        Propuesta Tecno-Econ&oacute;mica
                    </h4>
                </div>
                <div class="modal-body" id="dvCuerpoVentanaPropuesta">
                    <iframe id="iframeVentanaPropuesta" style="width: auto; min-width: 100%; height: 550px; min-height: 100%;">
                    
                    </iframe>
                </div>
                <div class="modal-footer">
                
                </div>
            </div>
        </div>
    </div>
    --%>             

    <%--MODAL /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
    <div id="dvModalPropuestaTE_ver2" style="width: 100%; height: 100%" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog-fs" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="img2" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        Propuesta Tecno-Econ&oacute;mica
                    </h4>
                </div>
                <div class="modal-body" id="Div2">
                       
                    <div class="col-md-12">

                    <input type="hidden" id="hf_Id_Cte" value="0">
                    <input type="hidden" id="hf_Id_Val" value="0">
                    <input type="hidden" id="hf_Id_Op" value="0">
                    <input type="hidden" id="hf_Vap_Estatus" value="0">
                    <input type="hidden" id="hf_Vap_Estatus2" value="0">


                        <div class="row form-inline" id="rowError" style="display:none;">                                                
                            <label>Ha ocurrido un error inesperado y NO es posible continuar con ese documento.</label>
                            <button type="button" class="btn btn-default btn-sm" id="Button1">
                                <i class="fa fa-times fa-2x">
                            </i>&nbsp;Cerrar</button>&nbsp;&nbsp;
                        </div>

                        <div class="row form-inline" id="rowLoagin" style="display:block;">                                                
                            <label>Cargando...</label>
                            <div id="loading_ModalPropuestaTE" class="Loading"></div>
                        </div>

                        <div class="row form-inline" id="rowAceptarPropuesta" style="display:none;">                                                

                            <div class="panel panel-default">
                            <div class="panel-body">
                                <h4><strong>Aceptar Propuesta</strong><h4>
                                <h4>Está a punto de aceptar esta propuesta y generar el ACYS correspondiente. ¿Desea continuar?"</h4>

                                <button type="button" class="btn btn-primary btn-sm" id="btnAceptarPro_Aceptar">
                                    <i class="fa fa-check fa-2x">
                                    </i>&nbsp;Aceptar Propuesta 
                                </button>   
                            
                                <button type="button" class="btn btn-default btn-sm" id="btnAceptarPro_Cancelar">
                                <i class="fa fa-times fa-2x">
                                </i>&nbsp;Cancelar</button>&nbsp;&nbsp;                            
                            
                            </div>
                            </div>                            
                        </div>

                        <div class="row form-inline" id="rowPropuestaAcciones" style="display:none;">                                                
                            <button type="button" class="btn btn-primary btn-sm" id="btnPropuestaAceptar">
                            <i class="fa fa-check-square fa-2x">
                            </i>&nbsp;Aceptar Propuesta 
                            </button>                               
                            <button type="button" onclick="PrevisualizarPropuesta('1');" class="btn btn-default btn-sm" id="btnPropuestaImprimir1">
                            <i class="fa fa-print fa-2x">
                            </i>&nbsp;Encabezado</button>                               
                            <button type="button" onclick="PrevisualizarPropuesta('2');" class="btn btn-default btn-sm" id="btnPropuestaImprimir2">
                            <i class="fa fa-print fa-2x">
                            </i>&nbsp;P. Economica</button>                               
                            <button type="button" onclick="PrevisualizarPropuesta('3');" class="btn btn-default btn-sm" id="btnPropuestaImprimir3">
                            <i class="fa fa-print fa-2x">
                            </i>&nbsp;P. Técnica</button>                               
                            <button type="button" class="btn btn-primary btn-sm" id="btnPropuestaEditar">
                            <i class="fa fa-pencil-square-o fa-2x">
                            </i>&nbsp;Editar</button>&nbsp;&nbsp;
                            <button type="button" class="btn btn-default btn-sm" id="btnPropuestaCerrar">
                            <i class="fa fa-times fa-2x">
                            </i>&nbsp;Cerrar</button>&nbsp;&nbsp;
                            <!--button type="button" class="btn btn-danger pull-rigth text-rigth" id="btnPropuestaRechazar"-->
                            <%--<i class="fa fa-check fa-2x">--%>
                            <!--/i>&nbsp;Rechazar-->
                            <!--/button-->
                        </div>

                        <div class="row form-inline" id="rowPropuestaEdicion"  style="display:none;">                                            
                            <button type="button" class="btn btn-primary btn-sm" id="btnPropuesta_Guardar">
                                <i class="fa fa-floppy-o fa-2x" ></i>&nbsp;Guardar</button>                        
                            <button type="button" class="btn btn-default btn-sm" id="btnPropuestaCancelarEdicion">
                                <i class="fa fa-times fa-2x" ></i>&nbsp;Cancelar</button>                        
                        </div>

                        <div class="row mt5" id="divPropuestaDetalle">
                                <ul class="nav nav-tabs role="tablist">
                                    <li class="active"><a href="#PropEconomica" data-toggle="tab">Propuesta Econ&oacute;mica</a></li>
                                    <li><a href="#PropTecnica" data-toggle="tab">Propuesta T&eacute;cnica</a></li>                                                
                                </ul>                                                           

                                <div class="tab-content">
                                    <div class="tab-pane active" id="PropEconomica">                    

                                        <table id="tblPropuestaEconomica" class="table table-hover table-bordered" style="width: 100%;">
                                            <thead>
                                                <tr>
                                                    <td valign="bottom" style="text-align:center; vertical-align: bottom;">C&oacute;digo</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Producto</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Precio</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Presentaci&oacute;n</td>
                                                    <td style="text-align:center; vertical-align: bottom;" >Consumo Mensual</br>(Unidades)</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Consumo Mensual</br>(L)</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Gasto Mensual</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Diluci&oacute;n</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Consumo Mensual</br>(L Diluidos)</td>
                                                    <td style="text-align:center; vertical-align: bottom;">Costo en uso</td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>

                                    </div>
                        
                                    <div class="tab-pane" id="PropTecnica">                    

                                        <div id="divPropuestaTecnica" style="width: 100%; height:200px; overflow:scroll">
                                            <table id="tblPropuestaTecnica" class="" style="width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <td>Situacion Actual</td>
                                                        <td>Ventajas Key</td>                                                
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>    
                                        </div>
                                    </div>
                                </div>                       

                        </div>

                        <div class="row mt5" id="pnlVisorDeReporte" style="display:none;">
                            <label id="lbPreparandoReporte">Preparando el reporte...</label>
                            <iframe id="iframeVisorReporte" src="" width="100%" height="500"></iframe>
                        </div>

                    </div>  
                                       

            </div>
            <div class="modal-footer">                
            </div>
        </div>
    </div>
    </div>
    
    <%--MODAL /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
    <script type="text/html" id="tplValuacionGlobal">
        <td style="background-color:yellow;">
        </td>
        <td style="background-color:yellow;">
            --
        </td>
        <td style="font-weight: bold; background-color:yellow; text-align: center;">
            Valuaci&oacute;n Global
        </td>
        <td style="background-color:yellow;">
            --
        </td>
        <td id="tdUtilidadRemanente" data-content="Vap_UtilidadRemanente" style="font-weight: bold; background-color:yellow;">
            <!--Utilidad remanente-->
        </td>
        <td id="tdVPN" data-content="Vap_ValorPresenteNeto" style="font-weight: bold; background-color:yellow;">
            <!--VPN-->
        </td>
        <td style="background-color:yellow;">
            --
        </td>
        <td>
            <button data-template-bind='[
                {"attribute": "data-idcte", "value": "Id_Cte"},
                {"attribute": "data-idval", "value": "Id_Vap"}
            ]' 
            type="button" class="btn btn-primary" data-toggle="modal" data-target="#dvModalValuacionGlobal" id="btnEditarValuacion" data-modo="1">
            <i class="fa fa-tasks"></i>Ver Valuación
            </button>
        </td>
        <td style="background-color:yellow;">
            --
        </td>
    </script>

    <%--MODAL /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
    <div id="dvModalValuacionGlobal" style="width: 100%; height: 100%" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog-fs" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="imgCargandoVentanaValuacionGlobal" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        Valuaci&oacute;n Global
                    </h4>
                </div>
                <div class="modal-body" id="dvCuerpoVentanaValuacionGlobal">
                    <iframe id="iframeVentanaValuacionGlobal" style="width: auto; min-width: 100%; height: 550px; min-height: 100%;">
                    
                    </iframe>
                </div>
                <div class="modal-footer">
                
                </div>
            </div>
        </div>
    </div>

    <%--MODAL /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
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

    <%--MODAL /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
    <div id="dvModalCancelarProyecto" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="h4ModalCancelarProyectoTitulo">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="imgModalCancelarProyectoEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                            <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="h4ModalCancelarProyectoTitulo">
                        Cancelar Proyecto
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="blank-slate-pf" style="padding:40px 110px;" >
                        <div class="blank-slate-pf-icon">
                            <i class="fa fa-thumbs-down" aria-hidden="true"></i>
                        </div>
                        <h1>
                            Causa de Cancelaci&oacute;n
                        </h1>
                        <p>
                            Ah decidido cancelar este proyecto. Por favor, indique la causa de cancelación:
                        </p>
                        <div>
                            <table id="tblCausasCancelacion">
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptCausasCancelacion">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <input type="radio" name="CausaCancelacion" value='<%# Eval("Id_Causa") %>'/>
                                                    <label>
                                                        <%#Eval("Descripcion")%>
                                                    </label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" onclick="confirmarCancelarProyecto(this)">Aceptar</button>
                </div>
            </div>
        </div>
    </div>

    <%--<uc:BootstrapConfirm runat="server" ID="btpConfirm1" />--%>
        <%--MODAL /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>
    <div class="modal fade" id="modalCargaRecurso" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="imgDvModalRecursoEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H2">
                        Recurso</h4>
                </div>
                <div class="modal-body">
                    <ul class="nav nav-tabs" role="tablist" id="navMenu">
                        <li id="MenuNavNuevoRecursoId" role="presentation" class="active">
                            <a href="#dvMenu_NuevoRecurso" aria-controls="dvMenu_NuevoRecurso" role="tab" data-toggle="tab">
                                Nuevo
                            </a>
                        </li>
                        <li  id="MenuNavRepositorioId" role="presentation">
                            <a href="#dvMenu_RecursoExistente" aria-controls="dvMenu_RecursoExistente" role="tab" data-toggle="tab">
                                Repositorio
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content" id="tabMenu">
                        <div id="dvMenu_NuevoRecurso" class="tab-pane active" role="tabpanel">

                            <div class="panel-group" id="accordion">

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a id="AcordeonElementoURL" data-toggle="collapse" data-parent="#accordion" href="#nuevoRURL">
                                                URL
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="nuevoRURL" class="panel-collapse collapse in">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    URL: <input type="text" class="form-control" id="CampoURLId" placeholder="http://" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a id="AcordeonElementoArchivo" data-toggle="collapse" data-parent="#accordion" href="#nuevoRIL" class="collapsed">
                                                Archivo local
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="nuevoRIL" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <div class="row" style="display: none;">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="progress progress-xs progress-stripped active">
                                                        <div id="BarraProgresoTransferenciaArchivoId" class="progress-bar" role="progressbar" aria-valuenow="0" 
                                                        aria-valuemin="0" aria-valuemax="100" style="width: 0%; display: none;">
                                                            <span class="sr-only">0% completado</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="height:180px;">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    
                                                    <input type="hidden" id="modalCargaImagen_Contenedor" value=""/>                                                        
                                                    </br>                                                        
                                                    </br>                                                        

                                                        <div class="box box__input" id="AreaArrastreClientID">   
                                                        <i class="fa fa-upload fa-5x"></i><br/>

                                                        <input class="box__file" type="file" name="file" id="file" style="display:none;" />                                                       
                                                        
                                                        <label type="label" id="lbmodalRecursoNombreArchivo"></label></br>
                                                        <button type="button" class="btn btn-primary btn-sm" id="btnBuscarArchivo">Buscar archivo</button>

                                                       	<input type="submit" id="btnSubitImagen" name="uploadButton" value="Subir" style="display:none;"/>                                                        
                                                        <asp:Label runat="server" ID="Span1" ></asp:Label> 
                                                        </br>                                                        
                                                        </br>                                                        
                                                   
                                                    </div>
                                                    <div class="box__uploading" style="display:none;">Subiendo&hellip;</div>
                                                    <div class="box__success" style="display:none;">Completado</div>
                                                    <div class="box__error" style="display:none;">Error</div>
                                                    
                                                </div>
                                            </div>
                                        
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="dvMenu_RecursoExistente">
                                
                        </div>
                    </div>
                    <hr />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="modalCagaRecurso_Aceptar">Aceptar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCerrar">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">

    <script type="text/javascript">

        var _ApplicationUrl = '<%=ApplicationUrl %>';    
        var _EntidadSesion_Id_Emp = <%=EntidadSesion.Id_Emp %>;
        var _EntidadSesion_Id_Cd = <%=EntidadSesion.Id_Cd %>;    

    </script>

    <script type="text/javascript" src="../../js/crm2/Proyectos_tablaAgrupada_PropuestaTE.js"></script>
    <script type="text/javascript" src="../../js/crm2/Proyectos_tablaAgrupada_PropuestaTE_ajax.js"></script>
    <script type="text/javascript" src="../../js/crm2/Proyectos_tablaAgrupada.js"></script>
    <script type="text/javascript" src="../../js/crm-namespaces/crm.js"></script>
    <script type="text/javascript" src="../../js/crm-namespaces/crm.ui-ns.js"></script>

    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm-ns.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ListControl/crm.ui-ns.js") %>"></script>
    
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>            
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <%--<uc:UCPatternflyToast runat="server" id="ucPatternflyToast"></uc:UCPatternflyToast>--%>
    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ekko-lightbox.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/form-wizard.js") %>"></script>
    <!--<script src="//cdn.datatables.net/buttons/1.1.2/js/dataTables.buttons.min.js"></script>-->
    <%--<script src="//cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js"></script>--%>
    <script src="<%=Page.ResolveUrl("~/js/jquery.blockUI.min.js") %>"></script>    

    <script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/dist/min/jquery.inputmask.bundle.min.js")%>"></script>

    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet">
       
    <script src="<%=Page.ResolveUrl("~/js/CRM2/Gerente.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/valuacion.js")%>"></script>

    <script type="text/javascript">

        var _ApplicationUrl = '<%=ApplicationUrl %>';

        /*VARIABLE GERENTE */
        var _Parametro_ControlesSoloLectura = 0;    

        _Parametro_IdTU = "<%=Parametro_IdTU %>";
        _Parametro_IdRik = "<%=Parametro_IdRik %>";

        _CRM_Gerente_Id = "<%=CRM_Gerente_Id %>";
        _CRM_Gerente_Rik = "<%=CRM_Gerente_Rik %>";
        _CRM_Gerente_Nombre = "<%=CRM_Gerente_Nombre %>";

        _CRM_Usuario_Id = "<%=CRM_Usuario_Id %>";
        _CRM_Usuario_Rik = "<%=CRM_Usuario_Rik %>";
        _CRM_Usuario_Nombre = "<%=CRM_Usuario_Nombre %>";          
         
    </script>

    <script type="text/javascript">
        var _contador_intentos = 0;
        var _tablaProyectos = null;
        var contador = 0;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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
                    "processing": "<img src='../../Img/ajax-loader.gif'> Cargando...",
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
                            'defaultContent': '<button '+
                                'type="button" '+
                                'class="btn btn-primary" '+
                                //'data-toggle="modal" '+
                                //'data-target="#dvModalValuacion" '+
                                'onclick="VerValuacion(this)" '+
                                'data-modo="0" '+
                                'id="btnGenerarValuacion">'+
                                    '<i class="fa fa-tasks"></i>Generar Valuación'+
                                '</button>',
                            'render': function (data, type, full, meta) {
                                
                                return '<button '+
                                'type="button" class="btn btn-primary" '+
                                //'data-toggle="modal" '+
                                //'data-target="#dvModalValuacion" '+
                                'onclick="VerValuacion(this)" '+
                                'data-modo="0" '+
                                'id="btnGenerarValuacion_' + meta.row + '" '+
                                'data-idcte="' + full.Id_Cte + '">'+
                                    '<i class="fa fa-tasks"></i>Generar Valuación'+
                                '</button>';
                            }
                        }
                    ]
            });
                               


            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaValuacionGlobal_Generada(id){
                $('#dvModalValuacionGlobal').modal('hide');
                recargarListadoProyectos();                
                alertify.success('La valuación global ' + id + ' ha sido creada con éxito');
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaValuacionGlobal_Actualizada(id){
                $('#dvModalValuacionGlobal').modal('hide');
                recargarListadoProyectos();                
                alertify.success('La valuación global ' + id + ' ha sido actualizada con éxito.');
            }

            $('#iframeVentanaValuacionGlobal').on('load', function(){
                if(_modoValuacionGlobal==0){
                    $('#iframeVentanaValuacionGlobal')[0].contentWindow._externalCustomFn=cerrarVentanaValuacionGlobal_Generada;
                }else{
                    $('#iframeVentanaValuacionGlobal')[0].contentWindow._externalCustomFn=cerrarVentanaValuacionGlobal_Actualizada;
                }
                
                $('#dvCuerpoVentanaValuacionGlobal').unblock();
            });

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function toggled($e){
                $e.preventDefault();
                $($e.currentTarget).unbind('ifToggled', toggled);
                var state=$($e.currentTarget).is(':checked') ? 'uncheck' : 'check';
                $($e.currentTarget).iCheck(state);
                $($e.currentTarget).iCheck('update');//.on('ifToggled', toggled);
            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function crearTablaHija(datos){                                
                var sliderDiv=$('<div class="slider">');
                var elemento=$('<table class="table table-bordered table-hover">'+
                    '<thead>'+
                        '<tr>'+
                            '<th></th>'+
                            '<th>Clave</th>'+
                            '<th style="text-align: center;">Área de Aplicación</th>'+
                            //'<th>EditarX</th>'+ RFH TODO: Verificar funcionamiento de Edit.
                            '<th></th>'+ 
                            '<th style="text-align: center;">Utilidad Remanente</th>'+
                            '<th style="text-align: center;">Valor Presente Neto</th>'+
                            '<th style="text-align: center;">Estado de Valuación</th>'+
                            '<th>Editar Valuación</th>'+
                            '<th style="text-align: center;">Propuestas</th>'+
                        '</tr>'+
                    '</thead><tbody>');

                crearFilaValuacionGlobal(elemento, datos.ValuacionGlobal);                

                sliderDiv.append(elemento);

                $.each(datos.Proyectos, function(index, element){
                    var tr=$('<tr>');
                    var $tdForCheck=$('<td>'+
                        '<div class="inactiveLink">'+
                            '<input type="checkbox" id="chk_' + element.Id + '" name="chk_' + element.Id + '"/>'+
                        '</div>'+
                    '</td>');
                    validarEstadoDeCheck($tdForCheck.find('input'), element, datos.ProyectosEnValuacion);
                    // $tdForCheck.find('div').on('click', function(e){
                    //     e.preventDefault();
                    //     return false;
                    // });
                    $tdForCheck.find('input').iCheck({checkboxClass: 'icheckbox_square-blue'});
                    
                    $tdForCheck.find('.iCheck-helper').css('position', 'relative');
                    tr.append($tdForCheck);
                    
                    //Se crea ahora el campo para la descripción del proyecto, pero se agrega a la estructura hasta después de agregar el campo de detalle.
                    var $tdDescripcion=$('<td>' + element.Descripcion + '</td>'); // Columna Area de Aplicacion
                    if(element.Cancelado==true){
                        $tdDescripcion.css('text-decoration', 'line-through');
                    }

                    var a=$('<a>');
                    $(a).attr('href', '#dvDetalles');
                    $(a).click(function(){
                        // Funcion para desplegar el detalle del proyecto 
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

                        // Gerente
                        switch (_Parametro_ControlesSoloLectura) {
                        case 0:
                            //Edicion
                            // 11 Sep 2018 RFH       
                            $('#btnCancelarProyecto').prop('disabled',false);                                        
                            $("#btnCancelarProyecto").css("pointer-events", "auto");

                            $('#txtProductoBusqueda').prop('disabled',false);            
                            $('#txtProductoBusquedaOtros').prop('disabled',false);            
                            $('#btnAgregarProducto').prop('disabled',false);            
                            break;
                        case 1:
                            // Solo lectura 
                            // 11 Sep 2018 RFH       
                            $('#btnCancelarProyecto').prop('disabled',true);            
                            $("#btnCancelarProyecto").css("pointer-events", "none");

                            $('#txtProductoBusqueda').prop('disabled',true);            
                            $('#txtProductoBusquedaOtros').prop('disabled',true);       
                            $('#btnAgregarProducto').prop('disabled',true);                 
                            break;
                        default:            
                            // 11 Sep 2018 RFH       
                            $('#btnCancelarProyecto').prop('disabled',true);
                            $("#btnCancelarProyecto").css("pointer-events", "none");

                            $('#txtProductoBusqueda').prop('disabled',true);            
                            $('#txtProductoBusquedaOtros').prop('disabled',true);            
                            $('#btnAgregarProducto').prop('disabled',true);                 
                            break;
                        }        

                    });
                    $(a).text(element.Id); // Columna : HRef par el ID de proyecto
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

                    // TODO: Verificar el funcionamiento de este boton ya que es confuso para el usaurio y siempre hace click aqui pensando 
                    // que va poder agregar los productos
                    // RFH

                    /*
                    var editCommand=$('<button type="button" class="btn btn-primary" '+
                    'data-toggle="modal" data-target="#dvModalEditarProyecto" data-action="2" >'+
                    '<i class="fa fa-pencil-square-o"></i>'+
                    '</button>');
                    */

                    var editCommand=$('<button type="button" class="btn btn-default" '+
                    'data-toggle="modal" data-target="" data-action="2" >'+
                    //'<i class="fa fa-pencil-square-o"></i>'+
                    '</button>');
                    
                    editCommand.data('obj', element);
                   /* if(element.Cancelado==true){
                        $(tr).append($('<td>--</td>'));
                    }else{
                        $(tr).append($('<td>').append(editCommand));
                    }*/

                    $(tr).append($('<td></td>'));

                    
                    if(element.CrmValuacionOportunidades!=null){

                        $('#hf_Vap_Estatus').val(element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus);                        
                        $('#hf_Vap_Estatus2').val(element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2);                        

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
                                '<button type="button" '+
                                    'class="btn btn-primary" '+                                                                        
                                    'id="btnEditarValuacion" '+                                    
                                    'data-id="btnVerValuacion_'+element.Id+'" '+
                                    'onclick="VerValuacion(this)" '+
                                    //'data-toggle="modal" '+
                                    //'data-target="#dvModalValuacion" '+                                    
                                    'data-idcte="' + element.Id_Cte + '" '+
                                    'data-idval="' + element.CrmValuacionOportunidades.CapValProyectoSerializable.Id_Vap + '" data-modo="1" '+
                                    '>'+
                                        '<i class="fa fa-tasks"></i>Ver Valuación'+
                                '</button>'+
                                '</td>')
                                );
                            }
                        }

                    }else{
                        $(tr).append($('<td>').text('--'));
                        $(tr).append($('<td>').text('--'));
                        $(tr).append($('<td>').text('Sin Valuación'));
                        $(tr).append($('<td>').text(''));
                    }

                    //
                    //
                    //
                    //Condiciones para la visualización del comando de propuestas
                    // PROPUESTA TECNOECONOMICA
                    //
                    //

                    if(element.CrmValuacionOportunidades!=null){
                        if(element.CrmValuacionOportunidades.CapValProyectoSerializable!=null){
                            if(element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus2==3 && 
                            element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_Estatus=='A'){

                                $(tr).append($('<td>'+
                                '<button type="button" class="btn btn-primary" '+
                                    'id="btnVisualizarPropuestaTE" '+
                                    'onclick="Valuacion_Cargar(this)"' +
                                    //'data-toggle="modal" '+
                                    //'data-target="#dvModalPropuestaTE_ver2" '+                                
                                    //'data-target="#dvModalPropuestaTE" '+                                
                                    'data-idop="' + element.Id + '" '+
                                    'data-idcte="' + element.Id_Cte + '" '+
                                    'data-idval="' + element.CrmValuacionOportunidades.CapValProyectoSerializable.Id_Vap + '" >'+                                
                                    '<i class="fa fa-tasks"></i>Ver Propuesta'+
                                '</button>'+
                                '</td>'));

                            }else if( 
                                /*Cascada para manejar el caso cuando la valuación es positiva, pero no ha sido autorizada: solo para casos antiguos*/
                                    element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente!=null
                                    && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto!=null
                                    ){
                                        if(
                                            element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_UtilidadRemanente>0
                                            && element.CrmValuacionOportunidades.CapValProyectoSerializable.Vap_ValorPresenteNeto>0
                                        ){
                                            $(tr).append($('<td>'+
                                            '<button type="button" class="btn btn-primary" '+
                                            'onclick="Valuacion_Cargar(this)"' +
                                            //'data-toggle="modal" '+
                                            //'data-target="#dvModalPropuestaTE_ver2" '+
                                            'name="btnVisualizarPropuestaTE" '+
                                            'id="btnVisualizarPropuestaTE_'+element.CrmValuacionOportunidades.CapValProyectoSerializable.Id_Vap+'" '+
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

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function establecerFormatoResultadosValuacion($nodo, valor){
                if(valor<0){
                    $nodo.css('background-color', 'red');
                }else{
                    $nodo.css('background-color', 'green');
                }

                $nodo.css('color', 'white');
                $nodo.css('font-weight', 'bold');
            }

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

            inicializarModalNuevoProyecto();

            inicializarCampoProspecto();
            inicializarCampoCliente();

            var $contenedorListadoProductosAplicacion=$('#contendorProductos');
            var $contenedorListadoOtrosProductos=$('#contenedorOtrosProductos');
            //$inicializarCampoProductoAplicacionBusqueda($contenedorListadoProductosAplicacion);
            //$inicializarCampoProductoBusquedaOtrosProductos($contenedorListadoOtrosProductos);
                     
            // Proyecto - Cancelacion
            $('#dvModalCancelarProyecto').on('shown.bs.modal', function(event){
                if($('#dvModalNuevoProspecto').hasClass('in')){
                    $('body').addClass('modal-open');
                }
            });

            $('#dvModalValuacionGlobal').on('show.bs.modal', function(event){
                var idCte=$(event.relatedTarget).data('idcte');
                var modo=$(event.relatedTarget).data('modo');
                if(modo==0){
                    bloquearProyecto(idCte/*_clienteDeOportunidad*/);
                }else{
                    var idVal=$(event.relatedTarget).data('idval');
                    editarValuacionGlobal(<%=EntidadSesion.Id_Emp %>, <%=EntidadSesion.Id_Cd %>, idVal, idCte);
                }
            });

            //
            //
            // Visualizacion de Propuesta 
            //
            //

            /*
            $('#dvModalPropuestaTE').on('show.bs.modal', function(event){
                //var idCte=$(event.relatedTarget).data('idcte');
                //var idVal=$(event.relatedTarget).data('idval');                

                var idCte = 0;
                var idVal = 1;

                $('#iframeVentanaPropuesta').attr('src', 'Propuestas/VisualizarPropuestas_Ver2.aspx?idCte=' + idCte + '&idVal=' + idVal);
                //$('#iframeVentanaPropuesta').attr('src', 'Propuestas/VisualizarPropuestas.aspx?idCte=' + idCte + '&idVal=' + idVal);                
                //$('#dvCuerpoVentanaPropuesta').block({message: 'Cargando...'});
            });
            */
            
            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaPropuestaAcysGeneradoExitosamente(response){
                $('#dvModalPropuestaTE').modal('hide');                
                alertify.success('El ACYS con folio ' + response + ' de la propuesta ha sido generado satisfactoriamente.');

            }

            // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
            function cerrarVentanaPropuesta(){
                $('#dvModalPropuestaTE').modal('hide');
            }

           /* $('#iframeVentanaPropuesta').load(function(){
                //autoResize('iframeVentanaValuacion');
                /* DESCOMENTAR SI SE UTILIZA LA VERSION ORIGINAL
                $('#iframeVentanaPropuesta')[0].contentWindow._acysGeneradoExitosamenteCallback=cerrarVentanaPropuestaAcysGeneradoExitosamente;
                $('#iframeVentanaPropuesta')[0].contentWindow._regresarAProyectosFn=cerrarVentanaPropuesta;
                $('#dvCuerpoVentanaPropuesta').unblock();
                *
            });*/

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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Valuacion 
        var _modoValuacion=0;
        
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        var _modoValuacionGlobal=0;
        
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function nuevoProyecto() {
            $('#btnDvModalEditarProyectoGuardar').show();
            $('#btnDvModalEditarProyectoActualizar').hide();
            limpiarFormaNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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
              
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cargarSelUEN(jqelement, objUen) {
            jqelement.append('<option value="' + objUen.Id_Uen + '">' + objUen.Uen_Descripcion + '</option>');
            jqelement.selectpicker('val', 0);
            jqelement.selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorTerritorio() {
            $('#selUEN').find('option').remove();
            $('#selUEN').selectpicker('refresh');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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
        
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selUEN$on_change() {
            var $selSegmento = $('#dvModalEditarProyecto #selSegmento');
            var idUen = $('#dvModalEditarProyecto #selUEN').selectpicker('val');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
            cargarSegmentos(jQuery, $selSegmento, idUen);
        }
        
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selSegmento$on_change() {
            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var $selArea = $('#dvModalEditarProyecto #selArea');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
            //cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(cargarAreas, null, jQuery, $selArea, idSeg));
            
            cargarAreas(jQuery, $selArea, idSeg);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selArea$on_change() {
            var $selSolucion = $('#dvModalEditarProyecto #selSolucion');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
            cargarSoluciones(jQuery, $selSolucion, idArea);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function round(value, decimals){
            return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals );
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function txtAplVPO$onchange(sender){
            var objetoDatos=$(sender).data('obj');
            objetoDatos.CrmOpAp_VPO=$(sender).val();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function selSolucion$on_change() {
            var $selAplicacion = $('#dvModalEditarProyecto #selAplicacion');
            var idSol = $('#dvModalEditarProyecto #selSolucion').selectpicker('val');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
            cargarAplicaciones(jQuery, $selAplicacion, idSol);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habilitarSelectorDependienteDelSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').prop('disabled', false);
            $('#selSegmento').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorUENDialogoNuevoProyecto() {
            $('#selSegmento').find('option').remove();
            $('#selSegmento').selectpicker('refresh');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto() {
            //$('#selArea').prop('disabled', false);
            //$('#selArea').selectpicker('refresh');
            //$('#selTerritorio').prop('disabled', false);
            //$('#selTerritorio').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
            $('#selArea').selectpicker('refresh');
            $('#selTerritorio').selectpicker('refresh');

            deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
            $('#selArea').find('option').remove();
            //$('#selTerritorio').find('option').remove();
            $('#selArea').selectpicker('refresh');
            //$('#selTerritorio').selectpicker('refresh');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto() {
            //$('#selSolucion').prop('disabled', false);
            //$('#selSolucion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
            $('#selSolucion').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
            $('#selSolucion').find('option').remove();
            $('#selSolucion').selectpicker('refresh');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').prop('disabled', false);
            $('#selAplicacion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').selectpicker('refresh');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
            $('#selAplicacion').find('option').remove();
            $('#selAplicacion').selectpicker('refresh');

            var $lstAplicacion = $('#lstAplicacion');
            $lstAplicacion.find('div').remove();

        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function inhabilitarSelectoresDialogoNuevoProyecto() {
            deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        /// <summary>
        /// Esta función inicializa el manejador de evento del diálogo del Proyecto. Su principal función es discernir 
        /// entre la naturaleza del activador del diálogo para 
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function despopularListadoTerritorio($elemento){
            $elemento.find('option').remove();
            $elemento.append('<option value="0">--Seleccione--</option>');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function segmentosCargadosParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Seg);
            $(jqElement).selectpicker('refresh');

            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
            cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(territoriosCargadosParaEdicion, null, jQuery, $selTerritorio, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function territoriosCargadosParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Ter);
            $(jqElement).selectpicker('refresh');

            selTerritorio$on_change(jqElement);

            var idSeg = $('#dvModalEditarProyecto #selSegmento').selectpicker('val');
            var $selArea = $('#dvModalEditarProyecto #selArea');

            cargarAreas($, $selArea, idSeg, $.proxy(areasCargadasParaEdicion, null, $, $selArea, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function areasCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.ID_Area);
            $(jqElement).selectpicker('refresh');

            var $selSolucion = $('#dvModalEditarProyecto #selSolucion');
            var idArea = $('#dvModalEditarProyecto #selArea').selectpicker('val');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
            cargarSoluciones($, $selSolucion, idArea, $.proxy(solucionesCargadasParaEdicion, null, $, $selSolucion, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function solucionesCargadasParaEdicion($, jqElement, data) {
            $(jqElement).selectpicker('val', data.Id_Sol);
            $(jqElement).selectpicker('refresh');

            var $selAplicacion = $('#dvModalEditarProyecto #selAplicacion');
            var idSol = $('#dvModalEditarProyecto #selSolucion').selectpicker('val');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
            cargarAplicaciones($, $selAplicacion, idSol, $.proxy(aplicacionesCargadasParaEdicion, null, $, $selAplicacion, data));
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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



        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function determinarProyectoACargar(){
            <% if (CargarDatosProyecto()) {%>
            $('#dvDetalles:hidden').slideDown();
            //precargarProyectoSeleccionado('<%= Id_Cliente.ToString() %>', '<%= Id_Op.ToString() %>');
            _precargarProductosDeProyecto('<%= Id_Op.ToString() %>', '<%= Id_Cliente.ToString() %>');
            <%} %>
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // Producto Busqueda 
//        function inicializarCampoProductoBusqueda() {            //            
//            $('#txtProductoBusqueda').blur(function(eventObject){
//                var id=$(this).val();
//                if(id!=''){
//                    cargarInfoProducto(id);
//                }
//            });
//        }

        // PRODUCTOS OTROS
//        function $inicializarCampoProductoBusquedaOtrosProductos($C){            //            
//            $C.find('#txtProductoDescripcion').autocomplete({
//                minLength: 0,
//                select: function(event, ui){
//                    event.preventDefault();
//                    $C.find('#txtProductoDescripcion').val(ui.item.data.NombreProducto + ' (' + ui.item.label + ')');
//                    $C.find('#hdnProductoBusqueda').val(ui.item.value);
//                    _productoElegido=ui.item.data;
//                    $asignarValoresACamposDeFormaParaAgregarProducto($C, _productoElegido);
//                }
//            });
//            $C.find('#txtProductoBusqueda').blur(function(eventObject){
//                var id=$(this).val();
//                if(id!=''){                    
//                    cargarInfoProductoCatalogoUnico($C, id);
//                }
//            });
//        }

//        function $inicializarCampoProductoBusqueda($C) {            
//            
//            ///<summary>Inicializa el comportamiento del campo de búsqueda de producto en la sección que lo contiene especificada por $C</summary>
//            ///<param name="$C" type="jqNode">Nodo contenedor del campo</param>
//            $C.find('#txtProductoBusqueda').blur(function(eventObject){
//                var id=$(this).val();
//                if(id!=''){
//                    $cargarInfoProducto($C, id);
//                }
//            });
//        }

/*
//        function $inicializarCampoProductoAplicacionBusqueda($C) {            
//            
//            ///<summary>Inicializa el comportamiento del campo de búsqueda de producto en la sección que lo contiene especificada por $C</summary>
//            ///<param name="$C" type="jqNode">Nodo contenedor del campo</param>
//            $C.find('#txtProductoBusqueda').blur(function(eventObject){
//                var id=$(this).val();
//                if(id!=''){
//                    $cargarInfoProductoAplicacion($C, _proyectoSeleccionado.Id_Cte, _proyectoSeleccionado.Id, id);
//                }
//         
   });
//        }
*/

        var _productoElegido = null;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function asignarValoresACamposDeFormaParaAgregarProducto(data){
            $('#hdnAgregarProducto_Id_Uen').val(data.Id_Uen);
            $('#hdnAgregarProducto_Id_Seg').val(data.Id_Seg);
            $('#hdnAgregarProducto_Id_Area').val(data.Id_Area);
            $('#hdnAgregarProducto_Id_Sol').val(data.Id_Sol);
            $('#hdnAgregarProducto_Id_Apl').val(data.Id_Apl);
            $('#hdnAgregarProducto_Id_SubFam').val(data.Id_SubFam);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $asignarValoresACamposDeFormaParaAgregarProducto($C, data){
            ///<summary>Asigna el valor de los campos de la forma de envío en el contenedor de búsqueda de productos</summary>
            ///<param name="$C" type="jqNode">Contenedor de la sección de la forma de búsqueda de productos</param>
            ///<param name="data" type="object">Estructura de datos con información de la ruta de la aplicación</param>
            $C.find('#hdnAgregarProducto_Id_Uen').val(data.Id_Uen);
            $C.find('#hdnAgregarProducto_Id_Seg').val(data.Id_Seg);
            $C.find('#hdnAgregarProducto_Id_Area').val(data.Id_Area);
            $C.find('#hdnAgregarProducto_Id_Sol').val(data.Id_Sol);
            $C.find('#hdnAgregarProducto_Id_Apl').val(data.Id_Apl);
            $C.find('#hdnAgregarProducto_Id_SubFam').val(data.Id_SubFam);
        }

        var _oportunidadSeleccionada=null;
        var _clienteDeOportunidad = null;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function limpiarListadoDeProductos() {
            var $lstProductos = $('#lstProductos');
            $lstProductos.find('[elementoDeLista]').remove();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $limpiarListadoDeProductos($C) {
            var $lstProductos = $C.find('#lstProductos');
            $lstProductos.find('[elementoDeLista]').remove();
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Proyecto - Carga Ingo General
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        var _lastSelectedNode=null;
        var _proyectoSeleccionado=null;
        var _$campoDescripcionActual=null;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
        // Productos de proyecto
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

                //Productos de Aplicación
                var otrosProductos=$.grep(response, function(element, index){
                    var elementosEncontrados=$.grep(productosDeAplicacion, function(elementProductosDeAplicacion, index){
                        return element==elementProductosDeAplicacion;
                    });
                    return elementosEncontrados.length==0;
                });

                //Se inicializan los listados de productos de la aplicación y otros productos.
                var $c_PA=$('#contendorProductos');
                $c_PA.data('_modeloListado_', _totalProductos);

                var $c_LO=$('#contenedorOtrosProductos');
                $c_LO.data('_modeloListado_', _totalOtrosProductos);

                _totalProductos.i=productosDeAplicacion.length;
                _totalOtrosProductos.i=otrosProductos.length;

                inicializarApartadoProductos($c_PA, productosDeAplicacion);
                inicializarApartadoProductos($c_LO, otrosProductos);

                $obtenerRutaDeOferta($c_PA, _clienteDeOportunidad, _oportunidadSeleccionada);
                $obtenerRutaDeOferta($c_LO, _clienteDeOportunidad, _oportunidadSeleccionada);

            },function(jqXHR, status, error){
            },function(jqXHR, status, error){
                $('#imgCargandoProductos').fadeOut();
            },
            {}
            );
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Productos
        function inicializarApartadoProductos($C, dataItems){            
            ///<summary>Inicializa el listado de productos asociados a un proyecto</summary>
            ///<param name="$contendor" type="jqNode">Componente contenedor de la sección a inicializar</param>
            ///<param name="dataItems" type="CrmOportunidadesProductos[]">Conjunto de productos</param>
            //Se limpia el listado
            $limpiarCamposBusquedaProducto($C);
            $limpiarListadoDeProductos($C);

            //$C.find('#txtProductoCantidad').attr('disabled', true);
            //$C.find('#btnAgregarProducto').attr('disabled', true);

            if(dataItems.length>0){            
                //$C.find('#productosBlankSlate').hide(); RFH               
                $C.find('#contenidoSeccionProductos').show();
                var $lstProductos = $C.find('#lstProductos');
                $.each(dataItems, function (index, element) {
                    var n = $crearElementoDeListadoDeProductos($C[0].id, element);
                    $lstProductos.append(n);
                    var lstElem=$lstProductos.find('#lstElem_' + element.Id_Prd);
                    lstElem.data('objetodatos', element);
                });
            }else{
                $C.find('#contenidoSeccionProductos').hide();
                $C.find('#productosBlankSlate').show();

            }
            //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
            $C.find('#hdnAgregarProducto_Id_Op').val(_oportunidadSeleccionada);
            $C.find('#hdnAgregarProducto_Id_Cte').val(_clienteDeOportunidad);
        }

        var _totalProductos={i: 0};
        var _totalOtrosProductos={i: 0};
                
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $editarCantidad($C, idPrd) {
            var dvCantidadDisplay = $C.find('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $C.find('#dvCantidadEdit_' + idPrd);

            var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
            var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
            txtCantidadEdit.val(dvCantidadDisplayValue.text());

            dvCantidadDisplay.hide();
            dvCantidadEdit.show();

            var dvDilucionDisplay = $C.find('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $C.find('#dvDilucionEdit_' + idPrd);

            var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
            var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');
            txtDilucionEdit.val(dvDilucionDisplayValue.text());

            dvDilucionDisplay.hide();
            dvDilucionEdit.show();

        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $cancelarEditarCantidad($C, idPrd){
            var dvCantidadDisplay = $C.find('#dvCantidadDisplay_' + idPrd);
            var dvCantidadEdit = $C.find('#dvCantidadEdit_' + idPrd);
            dvCantidadEdit.hide();
            dvCantidadDisplay.show();

            var dvDilucionDisplay = $C.find('#dvDilucionDisplay_' + idPrd);
            var dvDilucionEdit = $C.find('#dvDilucionEdit_' + idPrd);
            dvDilucionEdit.hide();
            dvDilucionDisplay.show();
        }


        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Productos - Retirar
        function $retirarProducto($C, idCte, idOp, idPrd){
            
            retirarProductoSvc(idCte, idOp, idPrd, 
            function(response, textStatus, jqXHR){
                var $lstProductos = $C.find('#lstProductos');
                var elem=$lstProductos.find('#lstElem_' + idPrd);
                elem.remove();
                var totalProductos=$C.data('_modeloListado_');
                totalProductos.i=totalProductos.i-1;
                if(totalProductos.i==0){
                    $C.find('#contenidoSeccionProductos').fadeOut();
                    $C.find('#productosBlankSlate').fadeIn();
                }
            },
            function(jqXHR, textStatus, error){
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado(4036). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                        break;
                }
            },
            function(jqXHROrData, textStatus, errorOrJQXHR){
            },
            {
                401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy($retirarProducto, null, $C, idCte, idOp, idPrd);
                    }
            }
            );
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Crear Renglon de Producto
        function crearElementoDeListadoDeProductos(E) {            
            var editCommandClass = '';
            var editCommandEditAction = 'javascript:editarCantidad(' + E.Id_Prd + ')';
            var editCommandRemoveAction = 'javascript:retirarProducto(' + E.Id_Cte + ',' + E.Id_Op + ',' + E.Id_Prd + ')';
            if (_proyectoSeleccionado.EnValuacion != null) {
                if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
                    editCommandClass = 'disabled-link';
                    editCommandEditAction = '#';
                    editCommandRemoveAction='#';

                    //Es editable 
                    DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                        '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                            '<span class="fa fa-ellipsis-v"></span>' +
                        '</button>' +
                        '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                            '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
                            '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'">Retirar</a></li>' +
                        '</ul>' +
                    '</div>' ;

                } else {

                    //No es editable
                    DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf"></div>' ;
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
                                                    '<a href="javascript:aceptarEditarCantidad(' + E.Id_Prd + ')"><i class="fa fa-check" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a href="javascript:cancelarEditarCantidad(' + E.Id_Prd + ')"><i class="fa fa-times" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                            '</tr>' +
                                        '</table>' +
                                    '</div>' +
                                    '<td>';

            var n = $('<div class="list-group-item list-view-pf-stacked list-view-pf-top-align" id="lstElem_' + E.Id_Prd + '" elementoDeLista>' +
                    '<div class="list-view-pf-checkbox"><input type="checkbox"></div>' +
                    '<div class="list-view-pf-actions">' +
                        DropDown_Ellipsis +
                        /*'<div class="dropdown pull-right dropdown-kebab-pf">' +
                            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                                '<span class="fa fa-ellipsis-v"></span>' +
                            '</button>' +
                            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                                '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
                                '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'">Retirar</a></li>' +
                            '</ul>' +
                        '</div>' +*/
                    '</div>' +
                    '<div class="list-view-pf-main-info PADDING_TB_10">' +
                    '<div class="list-view-pf-body">' +
                        '<div class="list-view-pf-description">' +
                            '<div class="list-group-item-heading">' + E.Id_Prd + '&nbsp;' + E.Nombre + '</div>' +
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
                                                '<td> &nbsp;' + 'piezas' /*E.ProductoSerializable.Prd_UniNe*/ + '</td>' +
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
                                                    '<a href="javascript:aceptarEditarCantidad(' + E.Id_Prd + ')"><i class="fa fa-check" aria-hidden="true"></i></a>' +
                                                '</td>' +
                                                '<td>' +
                                                    '<a href="javascript:cancelarEditarCantidad(' + E.Id_Prd + ')"><i class="fa fa-times" aria-hidden="true"></i></a>' +
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
                return n;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Crea Renglon de Producto
        function $crearElementoDeListadoDeProductos(idContenedor, E) {                        
            ///<summary>General la cadena de texto que representa el enmarcado del elemento a ingresar en el listado de productos asociados al proyecto</summary>
            ///<param name="idContenedor" type="String">Identificador del elemento que actúa como contenedor de la forma y el listado de productos asociados</param>
            ///<param name="E" type="object">Instancia de la estructura de datos</param>
            var editCommandClass = '';
            var editCommandEditAction = "javascript:$editarCantidad($('#" + idContenedor + "\')," + E.Id_Prd + ")";
            var editCommandRemoveAction = "javascript:$retirarProducto($('#" + idContenedor + "\')," + E.Id_Cte + "," + E.Id_Op + "," + E.Id_Prd + ")";
            if (_proyectoSeleccionado.EnValuacion != null) {
                if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
                  //  editCommandClass = 'disabled-link';
                //} else {                
                    //Es editable 
                    DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                        '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                            '<span class="fa fa-ellipsis-v"></span>' +
                        '</button>' +
                        '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                            '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
                            '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'">Retirar</a></li>' +
                        '</ul>' +
                    '</div>' ;
                } else {
                    //No es editable
                    //DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf"></div>' ;
                    DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                        '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                            '<span class="fa fa-ellipsis-v"></span>' +
                        '</button>' +
                        '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                            '<li>Edición no permitida.</li>' +                            
                        '</ul>' +
                    '</div>' ;


                }
            }

            if (_proyectoSeleccionado.Estatus===1 || _proyectoSeleccionado.Estatus===2) {
                DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                        '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                            '<span class="fa fa-ellipsis-v"></span>' +
                        '</button>' +
                        '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                            '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
                            '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'">Retirar</a></li>' +
                        '</ul>' +
                '</div>' ;
            } else {
                //DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf"></div>' ;
                 DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                        '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                            '<span class="fa fa-ellipsis-v"></span>' +
                        '</button>' +
                        '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                            '<li>Edición no permitida.</li>' +                            
                        '</ul>' +
                '</div>' ;
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

                        DropDown_Ellipsis+

                        /*'<div class="dropdown pull-right dropdown-kebab-pf">' +
                            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                                '<span class="fa fa-ellipsis-v"></span>' +
                            '</button>' +
                            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                                '<li><a class="' + editCommandClass + '" id="aEditCommandAction">Editar</a></li>' +
                                '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'" id="aRemoveCommandAction">Retirar</a></li>' +
                            '</ul>' +
                        '</div>' +*/
                    '</div>' +
                    '<div class="list-view-pf-main-info PADDING_TB_10">' +
                    '<div class="list-view-pf-body">' +
                        '<div class="list-view-pf-description">' +
                            '<div class="list-group-item-heading">' + E.Id_Prd + '&nbsp;' + E.Nombre + '</div>' +
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
                                                '<td> &nbsp;' + 'piezas' /*E.ProductoSerializable.Prd_UniNe*/ + '</td>' +
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function limpiarCamposBusquedaProducto() {            
            $('#txtProductoBusqueda').val('');
            $('#txtProductoCantidad').val('');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function $limpiarCamposBusquedaProducto($C) {                        
            $C.find('#txtProductoBusqueda').val('');
            $C.find('#txtProductoCantidad').val('');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function dimensionElegida(idUen, idSeg, unidades){
            $('#dvModalEditarProyecto #hdnDim_Id_Uen').val(idUen);
            $('#dvModalEditarProyecto #hdnDim_Id_Seg').val(idSeg);
            $('#dvModalEditarProyecto #txtDimension').val(unidades);
            $('#dvModalDimension').modal('hide');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function recargarListadoProyectos(){
            $(_tablaProyectos.table().container()).block({message: '<img src=\'<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>\' />Actualizando'});
            _tablaProyectos.ajax.reload(function() { 
                $(_tablaProyectos.table().container()).unblock(); 
                
            });
        }

        var _thisWindow=null;
        var _valorUnidadDimension=0.0;

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function cancelarProyecto(sender){
            /*BootstrapConfirm.showWarning('Cancelar Proyecto', 'Est&aacute; a punto de cancelar el proyecto. ¿Est&aacute; seguro de que desea continuar?', function(){
                BootstrapConfirm.hide(function(){
                    $('#dvModalCancelarProyecto').modal('show');
                });
            }, function(){
            }, false);*/           

            var btnDiabled = $(sender).hasClass('disabled');            

            if (!btnDiabled) {
                // Solo si esta habilitado
                alertify
                .okBtn("Si, Cancelar el proyecto")
                .cancelBtn("No")
                .confirm("<b>Cancelar Proyecto</b><br/>Est&aacute; a punto de cancelar el proyecto. ¿Est&aacute; seguro de que desea continuar?", function (ev) {                
                    ev.preventDefault();                
                    $('#dvModalCancelarProyecto input[name="CausaCancelacion"]').attr('checked', false); 
                    $('#dvModalCancelarProyecto').modal('show');
                }, function(ev) {                
                    ev.preventDefault();                
                });
            }

        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Proyecto - Cancelar
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

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Proyecto - Cancelar
        function servicioCancelarProyectoExito(response, textStatus, jqXHR){
            //Tachar proyecto en listado, o al menos tachar su descripción y cancelar la creación de los comandos
            //Asociar el renglón correspondiente de la tabla al proyecto elegido actualmente, o crear un identificador que indique el renglón elegido. 
            _$campoDescripcionActual.css('text-decoration', 'line-through');            
            alertify.success('El proyecto ha sido cancelado con &eacute;xito');
            $('#dvModalCancelarProyecto').modal('hide');
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Proyecto - Cancelar
        function servicioCancelarProyectoFallo(jqXHR, textStatus, error){
            //PatternflyToast.showError(jqXHR.responseJSON.ExceptionMessage, 10000);
            alertify.error(jqXHR.responseJSON.ExceptionMessage);
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        function servicioCancelarProyectoSiempre(jqXHROrData, textStatus, errorOrJQXHR){
            //Esconder el indicador de la operación en progreso del modal actual
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

    $(document).ready(function () {        

            // Oculta el gif de loading
            _tablaProyectos.on( 'draw', function () {
                contador=contador + 1; 
                if (contador==2) {
                    $('#dvLoading').fadeOut(1000);
                    contador = 0;
                }                
            });

            $('#txtProductoBusqueda').blur(function(eventObject){
                var id=$(this).val();
                if(id!=''){
                    //$cargarInfoProducto($('#contendorProductos'), id);
                    $cargarInfoProductoAplicacion($('#contendorProductos'), _proyectoSeleccionado.Id_Cte, _proyectoSeleccionado.Id, id);                    
                }
            });

            $('#txtProductoBusquedaOtros').blur(function(eventObject){
                var id=$(this).val();
                if(id!=''){
                    $cargarInfoProducto($('#contenedorOtrosProductos'), id);
                }
            });

            Incializa_Gerente(2);            
    });
    
    </script>

    <script src="<%=Page.ResolveUrl("~/js/jquery.radios-to-slider.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-treeview.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js") %>"></script>
    <!--<script src="//rawgit.com/jonmiles/bootstrap-treeview/v1.2.0/dist/bootstrap-treeview.min.js"></script>-->
    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphToolbar" runat="server">
   
</asp:Content>
