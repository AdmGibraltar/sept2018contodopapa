<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true"
    CodeBehind="Prospectos.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Prospectos" %>

<%@ Register Src="~/PortalRIK/GestionPromocion/SelectorDimension.ascx" TagPrefix="uc" TagName="SelectorDimension" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/css/horizontal_selector.css")%>" rel="stylesheet">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/radios-to-slider.min.css")%>">
    <link href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/bootstrap-treeview.min.css")%>" rel="stylesheet">
    <style>
        .myCenteredCellTable
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">
    <div class="row">
        <div class="col-sm-6 col-md-7">
            <div class="row">
                <div class="col-sm-12 col-md-12">
                    <button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProspecto" data-action="1" ><i class="fa fa-plus"></i>Nuevo Prospecto</button>
                    <table class="datatable table table-striped table-bordered" id="tblProspectos">
                        <thead>
                            <tr>
                                <th>
                                    Nombre
                                </th>
                                <th>
                                    Editar
                                    <!--Columna de comando de edición-->
                                </th>
                                <th>
                                    Nuevo Proyecto
                                </th>
                                <th>
                                    Eliminar
                                    <!--Columna de comando de eliminación-->
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <!--
                            <tr>
                                <td>
                                    <a href="#">Colegio San Roberto</a>
                                </td>
                                <td>
                                    <div><button class="btn btn-primary"><i class="fa fa-pencil-square-o"></i></button></div>
                                </td>
                                <td>
                                    <button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" ><i class="fa fa-tasks"></i></button>
                                </td>
                                <td>
                                    <button class="btn btn-default" data-toggle="modal" data-target="#dvModalEliminarProspecto"><i class="fa fa-times"></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="#">Doctors Hospital</a>
                                </td>
                                <td>
                                    <button class="btn btn-primary"><i class="fa fa-pencil-square-o"></i></button>
                                </td>
                                <td>
                                    <button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" ><i class="fa fa-tasks"></i></button>
                                </td>
                                <td>
                                    <button class="btn btn-default" data-toggle="modal" data-target="#dvModalEliminarProspecto"><i class="fa fa-times"></i></button>
                                </td>
                            </tr>
                            -->
                        </tbody>
                    </table>
                </div>
            </div>
            
        </div>
        
        <div class="col-md-6 col-md-5">
            <br />
            <!--<div class="card-pf">
                <div class="card-pf-heading">
                    <h2 class="card-pf-title">
                        Datos Generales <i class="indicator glyphicon glyphicon-chevron-up pull-right"></i>
                    </h2>
                </div>
                <div class="card-pf-body">
                    <dl class="dl-horizontal">
                        <dt>
                            Contacto
                        </dt>
                        <dd>
                            Angel Gerardo Segura Orozco
                        </dd>
                        <dt>
                            Correo electrónico
                        </dt>
                        <dd>
                            angel.segura@bsdenterprise.com
                        </dd>
                        <dt>
                            Teléfono
                        </dt>
                        <dd>
                            555-5678
                        </dd>
                    </dl>
                </div>
            </div>-->
            <div class="card-pf">
                <div class="card-pf-heading">
                    <h2 class="card-pf-title">
                        Herramientas <i class="indicator glyphicon glyphicon-chevron-up pull-right">
                        </i>
                    </h2>
                </div>
                <div class="card-pf-body">
                    <div id="dvHerramientas">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="display: none;" id="dvSeguimiento">
        <div class="col-sm-12 col-md-12">
            <div class="row">
                <div class="col-sm-12 col-md-12">
                    <div class="card-pf">
                        <div class="card-pf-heading">
                            <h2 class="card-pf-title">
                                Seguimiento
                            </h2>
                        </div>
                        <div class="card-pf-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- Nav tabs -->
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <a href="#dvGeneral" data-toggle="tab">Datos Generales</a>
                                        </li>
                                        <!--<li>
                                            <a href="#dvAnalisisYDiagnostico" data-toggle="tab">Análisis y Diagnóstico</a>
                                        </li>-->
                                    </ul>
                                    <!-- Tab panes -->
                                    <div class="tab-content">
                                        <div role="tabpanel" class="tab-pane active" id="dvGeneral">
                                            <dl class="dl-horizontal">
                                                <dt>Nombre Comercial</dt>
                                                    <dd id="ddDatosGeneralesNombreComercial"></dd>
                                                <dt>Calle</dt>
                                                    <dd id="ddDatosGeneralesCalle"></dd>
                                                <dt>Contacto</dt>
                                                    <dd id="ddDatosGeneralesContacto"></dd>
                                                <dt>Correo electrónico</dt>
                                                    <dd id="ddDatosGeneralesCorreoElectronico"></dd>
                                                <dt>Teléfono</dt>
                                                    <dd id="ddDatosGeneralesTelefono"></dd>
                                            </dl>
                                        </div>
                                        <!--
                                        <div role="tabpanel" class="tab-pane" id="dvAnalisisYDiagnostico">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <br />
                                                    <div class="list-group list-view-pf" style="overflow-y: auto; overflow-x: hidden; height: 200px;" id="lvProyectos">
                                                        <div class="list-group-item list-view-pf-stacked">
                                                            <div class="list-view-pf-actions">
                                                                <button class="btn btn-default">Agregar Productos</button>
                                                            </div>
                                                            <div class="list-view-pf-main-info">
                                                                <div class="list-view-pf-left">
                                                                    
                                                                </div>
                                                                <div class="list-view-pf-body">
                                                                    <div class="list-view-pf-description">
                                                                        <div class="list-group-item-heading">
                                                                            Propuesta Proyecto #1
                                                                            <small>Fecha creación</small>
                                                                        </div>
                                                                        <div class="list-group-item-text">
                                                                            Proyecto en promoción
                                                                        </div>
                                                                    </div>
                                                                    <div class="list-view-pf-additional-info">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        Clave
                                                                                    </th>
                                                                                    <th>
                                                                                        Producto
                                                                                    </th>
                                                                                    <th>
                                                                                        Presen.
                                                                                    </th>
                                                                                    <th>
                                                                                        Unidad
                                                                                    </th>
                                                                                    <th>
                                                                                        Cantidad
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="list-group-item list-view-pf-stacked">
                                                            <div class="list-view-pf-actions">
                                                                <button class="btn btn-default">Agregar Productos</button>
                                                            </div>
                                                            <div class="list-view-pf-main-info">
                                                                <div class="list-view-pf-left">
                                                                    
                                                                </div>
                                                                <div class="list-view-pf-body">
                                                                    <div class="list-view-pf-description">
                                                                        <div class="list-group-item-heading">
                                                                            Propuesta Proyecto #2
                                                                            <small>Fecha creación</small>
                                                                        </div>
                                                                        <div class="list-group-item-text">
                                                                            Proyecto cerrado
                                                                        </div>
                                                                    </div>
                                                                    <div class="list-view-pf-additional-info">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        Clave
                                                                                    </th>
                                                                                    <th>
                                                                                        Producto
                                                                                    </th>
                                                                                    <th>
                                                                                        Presen.
                                                                                    </th>
                                                                                    <th>
                                                                                        Unidad
                                                                                    </th>
                                                                                    <th>
                                                                                        Cantidad
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="list-group-item list-view-pf-stacked">
                                                            <div class="list-view-pf-actions">
                                                                <button class="btn btn-default">Agregar Productos</button>
                                                            </div>
                                                            <div class="list-view-pf-main-info">
                                                                <div class="list-view-pf-left">
                                                                    
                                                                </div>
                                                                <div class="list-view-pf-body">
                                                                    <div class="list-view-pf-description">
                                                                        <div class="list-group-item-heading">
                                                                            Propuesta Proyecto #3
                                                                            <small>Fecha creación</small>
                                                                        </div>
                                                                        <div class="list-group-item-text">
                                                                            Proyecto en negociación
                                                                        </div>
                                                                    </div>
                                                                    <div class="list-view-pf-additional-info">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        Clave
                                                                                    </th>
                                                                                    <th>
                                                                                        Producto
                                                                                    </th>
                                                                                    <th>
                                                                                        Presen.
                                                                                    </th>
                                                                                    <th>
                                                                                        Unidad
                                                                                    </th>
                                                                                    <th>
                                                                                        Cantidad
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="list-group-item list-view-pf-stacked">
                                                            <div class="list-view-pf-actions">
                                                                <button class="btn btn-default">Agregar Productos</button>
                                                            </div>
                                                            <div class="list-view-pf-main-info">
                                                                <div class="list-view-pf-left">
                                                                    
                                                                </div>
                                                                <div class="list-view-pf-body">
                                                                    <div class="list-view-pf-description">
                                                                        <div class="list-group-item-heading">
                                                                            Propuesta Proyecto #4
                                                                            <small>Fecha creación</small>
                                                                        </div>
                                                                        <div class="list-group-item-text">
                                                                            Proyecto en análisis
                                                                        </div>
                                                                    </div>
                                                                    <div class="list-view-pf-additional-info">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        Clave
                                                                                    </th>
                                                                                    <th>
                                                                                        Producto
                                                                                    </th>
                                                                                    <th>
                                                                                        Presen.
                                                                                    </th>
                                                                                    <th>
                                                                                        Unidad
                                                                                    </th>
                                                                                    <th>
                                                                                        Cantidad
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Nuevo Prospecto -->
    <div class="modal fade" id="dvModalNuevoProspecto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="imgDvModalNuevoProspectoEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">
                        Nuevo Prospecto</h4>
                </div>
                <div class="modal-body">
                    <form id="frmDvModalNuevoProspecto">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="txtRFC">
                                R.F.C.</label>&nbsp<i class="fa fa-university" aria-hidden="true"></i>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <input type="text" id="txtRFC" name="RFC" class="form-control col-md-2" placeholder="RFC" data-inputmask="'mask' : 'aaa[a]999999aa9', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtNombre">
                            Nombre de la Empresa</label>&nbsp<i class="fa fa-industry" aria-hidden="true"></i>
                        <input type="text" id="txtNombre" name="txtNombre" class="form-control" placeholder="Nombre de la empresa" />
                    </div>
                    <div class="form-group">
                        <label for="txtContacto">
                            Nombre del Contacto</label>&nbsp<i class="fa fa-book" aria-hidden="true"></i>
                        <input type="text" id="txtContacto" name="txtContacto" class="form-control" placeholder="Nombre del Contacto" />
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">
                            Correo electrónico</label>&nbsp<i class="fa fa-envelope" aria-hidden="true"></i>
                        <input type="email" id="txtEmail" name="txtEmail" class="form-control" placeholder="Email" 
                        data-inputmask="'alias': 'email', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " />
                    </div>
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#dvDireccionModalNuevoProspecto" aria-controls="dvDireccionModalNuevoProspecto"
                            role="tab" data-toggle="tab">Dirección Física</a></li>
                        <%--<li role="presentation"><a href="#dvTerritorios" aria-controls="dvTerritorios"
                            role="tab" data-toggle="tab">Territorios</a></li>--%>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="dvDireccionModalNuevoProspecto">
                            <div class="form-group">
                                <label for="txtCalle">
                                    Calle</label>&nbsp<i class="fa fa-road" aria-hidden="true"></i>
                                <input type="text" id="txtCalle" name="txtCalle" class="form-control" placeholder="Nombre de la calle" />
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="txtTelefono">
                                            Teléfono
                                        </label>&nbsp<i class="fa fa-phone-square" aria-hidden="true"></i>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <input type="text" id="txtTelefono" name="txtTelefono" class="form-control" placeholder="Teléfono" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="dvTerritorios">
                            <div class="form-group">
                                <label for="selTerritorios"> Territorios<img id="imgProgreso" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                </label>
                                <select id="selTerritorios" onchange="selTerritorio$on_change(this)" class="selectpicker form-control" name="Territorios" multiple></select>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnDvModalNuevoProspectoGuardar" onclick="crearProspecto()">
                        Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Editar Prospecto -->
    <div class="modal fade" id="dvModalEditarProspecto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="imgDvModalEditarProspectoEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H4">
                        Editar Prospecto</h4>
                </div>
                <div class="modal-body">
                    <form id="frmDvModalEditarProspecto">
                    <input type="hidden" id="hdnIdCrmProspectoEditarProyecto" name="idCrmProspecto" />
                    <input type="hidden" id="hdnId_Cte" name="hdnId_Cte" />
                    <input type="hidden" id="hdnId_Rik" name="hdnId_Rik" />
                    <input type="hidden" id="hdnId_CrmTipoCliente" name="hdnId_CrmTipoCliente" />
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="txtRFC">
                                R.F.C.</label>&nbsp<i class="fa fa-university" aria-hidden="true"></i>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <input type="text" id="txtRFC" name="RFC" class="form-control col-md-2" placeholder="RFC" data-inputmask="'mask' : 'aaa[a]999999aa9', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtNombre">
                            Nombre de la Empresa</label>&nbsp<i class="fa fa-industry" aria-hidden="true"></i>
                        <input type="text" id="txtNombre" name="txtNombre" class="form-control" placeholder="Nombre de la Empresa" />
                    </div>
                    <div class="form-group">
                        <label for="txtContacto">
                            Nombre del Contacto</label>&nbsp<i class="fa fa-book" aria-hidden="true"></i>
                        <input type="text" id="txtContacto" name="txtContacto" class="form-control" placeholder="Nombre del Contacto" />
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">
                            Correo electrónico</label>&nbsp<i class="fa fa-envelope" aria-hidden="true"></i>
                        <input type="email" id="txtEmail" name="txtEmail" class="form-control" placeholder="Email" data-inputmask="'alias': 'email', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " />
                    </div>
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#dvDireccion" aria-controls="dvDireccion"
                            role="tab" data-toggle="tab">Dirección</a></li>
                        <%--<li role="presentation"><a href="#dvEdicionProspectosTerritorios" aria-controls="dvEdicionProspectosTerritorios"
                            role="tab" data-toggle="tab">Territorios</a></li>--%>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="dvDireccion">
                            <div class="form-group">
                                <label for="txtCalle">
                                    Calle</label>&nbsp<i class="fa fa-road" aria-hidden="true"></i>
                                <input type="text" id="txtCalle" name="txtCalle" class="form-control" placeholder="Nombre de la calle" />
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="txtTelefono">
                                            Teléfono
                                        </label>&nbsp<i class="fa fa-phone-square" aria-hidden="true"></i>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <input type="text" id="txtTelefono" name="txtTelefono" class="form-control" placeholder="Teléfono" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="dvEdicionProspectosTerritorios">
                            <div class="form-group">
                                <label for="selTerritorios">
                                    Territorios<img id="imgProgreso" style="display: none;"
                                src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                                <select id="selTerritorios" class="selectpicker form-control" name="Territorios" multiple></select>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary" onclick="actualizarProspecto()">
                        Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Nuevo Proyecto -->
    <div class="modal fade" id="dvModalNuevoProyecto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="imgDvModalNuevoProyectoEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H1">
                        Nuevo Proyecto
                    </h4>
                </div>
                <div class="modal-body">
                    <form id="frmDvModalNuevoProyecto">
                    <input type="hidden" id="hdnId_CrmProspecto" name="Id_CrmProspecto" />
                    <input type="hidden" id="hdnCliente" name="Cliente" />

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
                        <select id="selUEN" name="Uen" disabled class="selectpicker form-control">
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="selSegmento">
                            Segmento&nbsp<i class="fa fa-tasks" aria-hidden="true"></i><img id="imgProcesandoSegmentoDvModalNuevoProyecto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /> </label>
                        <select id="selSegmento" name="Segmento" disabled class="selectpicker form-control">
                        </select>
                    </div>

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
                            <input id="txtCantidad" name="Dim_Cantidad" type="text" class="form-control" placeholder="0" title="Cantidad de la unidad elegida" data-toggle="tooltip" data-inputmask="'alias': 'numeric', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true', 'allowMinus':'false' "/>
                        </div>
                        <div class="col-md-2 tooltip-demo">
                            <small>VPME:</small>
                            <input id="txtVPM" name="CrmOp_VPM" type="text" class="form-control" placeholder="$0.0" title="Venta Promedio Mensual Esperada" data-toggle="tooltip"/>
                        </div>
                    </div>

                    <div class="form-inline">
                        <%--<div class="form-group">
                            <label for="txtDimension">
                                Dimensión
                            </label>
                            <div class="input-group">
                                <input id="txtDimension" type="text" class="form-control" disabled />
                                <button class="input-group-addon" data-toggle="modal" data-target="#dvModalDimension" type="button"><i class="fa fa-search fa-fw"></i></button>
                                <input type="hidden" id="hdnDim_Id_Uen" name="Dim_Id_Uen" />
                                <input type="hidden" id="hdnDim_Id_Seg" name="Dim_Id_Seg" />
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <%--<label for="txtCantidad">
                                Cantidad
                            </label>--%>
                            <%--<input id="txtCantidad" name="Dim_Cantidad" type="text" class="form-control" placeholder="Cantidad"/>--%>
                        </div>
                    </div>

                    <div class="form-horizontal">
                        <div class="form-group">
                            <%--<label for="txtVPM" class="col-sm-2">
                                VPM
                            </label>--%>
                            <div class="col-sm-6">
                                <%--<input id="txtVPM" name="CrmOp_VPM" type="text" class="form-control" placeholder="Venta Promedio Mensual Esperada"/>--%>
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="selArea">
                            Área&nbsp<i class="fa fa-share-alt" aria-hidden="true"></i><img id="imgProcesandoAreaDvModalNuevoProyecto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
                        <select id="selArea" name="Area" onchange="selArea$on_change()" class="selectpicker form-control">
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="selSolucion">
                            Solución&nbsp<i class="fa fa-recycle" aria-hidden="true"></i><img id="imgProcesandoSolucionDvModalNuevoProyecto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></label>
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
                                <select style="display: none;" id="selAplicacion" name="Aplicaciones" class="selectpicker form-control" multiple>
                                </select>
                            </div>

                            <div class="list-group" id="lstAplicacion">
                            
                            </div>
                        
                    </div>
                    <div class="checkbox">
                        <%--<label class="checkbox-inline">
                            <input type="checkbox" name="VentaNoRepetitiva" iCheck />Venta no repetitiva
                        </label>--%>
                        <%--<label class="checkbox-inline">
                            <input type="checkbox" iCheck />Pertenece a Campaña
                        </label>--%>
                    </div>
                    <%--<div class="form-group">
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
                    </div>--%>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary"
                            id="btnDvModalNuevoProyectoGuardar" onclick="crearProyecto()">
                            Guardar
                        </button>
                        <button type="button" class="btn btn-primary"
                            id="btnGuadarContinuar" onclick="crearProyectoYContinuar()">
                            Guardar y continuar
                        </button>
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
                    <h4 class="modal-title" id="h3">
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

    <!-- Modal Eliminar Prospecto -->
    <div class="modal fade" id="dvModalEliminarProspecto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H2">
                        Eliminar Prospecto
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="alert alert-warning">
                        <span class="pficon pficon-warning-triangle-o">
                        </span>
                        ¿Está seguro de eliminar el prospecto?
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary"
                            id="Button1" onclick="eliminarProspecto(jQuery)">
                            Confirmar
                        </button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Dialogo Error -->
    <div class="modal fade" id="dvDialogoError" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="btnCerrarDialogo" type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="dvDialogoErrorTituloEncabezado">
                        [Titulo encabezado]
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="alert alert-warning">
                        <span class="pficon pficon-warning-triangle-o">
                        </span>
                        <div id="dvDialogoErrorMensaje">
                            [Mensaje]
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnDvDialogoErrorCerrar" type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary"
                            id="btnDvDialogoErrorConfirmar" onclick="eliminarProspecto(jQuery)">
                            Confirmar
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
    <script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/dist/min/jquery.inputmask.bundle.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>            
    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ekko-lightbox.min.js") %>"></script>
    <%--<script src="//cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js"></script>--%>
    <script src="<%=Page.ResolveUrl("~/js/jquery.blockUI.min.js") %>"></script>    
    <script type="text/javascript">
        function cargarDescripcion(rowIdx){
            var data = $('#tblProspectos').DataTable().row(rowIdx).data();
            $('#ddDatosGeneralesContacto').text(data.Cte_Contacto);
            $('#ddDatosGeneralesCorreoElectronico').text(data.Cte_Email);
            $('#ddDatosGeneralesTelefono').text(data.Cte_Telefono);
            $('#ddDatosGeneralesNombreComercial').text(data.Cte_NomComercial);
            $('#ddDatosGeneralesCalle').text(data.Cte_Calle);

            $('#dvSeguimiento:hidden').slideDown();
        }

        function crmOnReady($) {
            $('#tblProspectos').DataTable({ /*"sDom": "<'dataTables_header' <'row' <'col-md-9' f i r> <'col-md-2' <'#tblProspectosToolbar'> > > >" +
                                                        "<'table-responsive'  t >" +
                                                        "<'dataTables_footer' p >",*/
                    'pageLength': 7,
                    'ordering': true,
                    'ajax': {
                        'url': '<%=Page.ResolveUrl("") %>' + '/api/CrmProspecto/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>',
                        'dataSrc': ''
                    },
                    "columns": [
                                { 
                                    'data': 'Cte_NomComercial',
                                    'render' : function (data, type, full, meta) {
                                        return '<a href="javascript:cargarDescripcion(' + meta.row + ')">' + full.Cte_NomComercial + '</a>';
                                    }
                                },
                                {
                                    'data': null,
                                    'className': "myCenteredCellTable",
                                    'defaultContent': '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalEditarProspecto" ><i class="fa fa-tasks"></i></button>',
                                    'render': function (data, type, full, meta) {
                                        return '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalEditarProspecto" data-idcrmprospecto="' + data.Id_CrmProspecto + '" data-rowidx="' + meta.row + '" ><i class="fa fa-tasks"></i></button>';
                                    }
                                },
                                {
                                    'data': null,
                                    'className': "myCenteredCellTable",
                                    'defaultContent': '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" ><i class="fa fa-tasks"></i></button>',
                                    'render': function (data, type, full, meta) {
                                        return '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalNuevoProyecto" data-idcrmprospecto="' + data.Id_CrmProspecto + '" data-rowidx="' + meta.row + '" ><i class="fa fa-tasks"></i></button>';
                                    }
                                },
                                {
                                    'data': null,
                                    'className': "myCenteredCellTable",
                                    'render': function (data, type, full, meta) {
                                        return '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalEliminarProspecto" data-rowid="' + meta.row + '" ><i class="fa fa-tasks"></i></button>';
                                    },
                                    'defaultContent': '<button class="btn btn-primary" data-toggle="modal" data-target="#dvModalEliminarProspecto" ><i class="fa fa-tasks"></i></button>'
                                }
                                ]
                });

                //$('#tblProspectosToolbar').html('<button class="btn btn-default" data-toggle="modal" data-target="#dvModalNuevoProspecto" ><i class="fa fa-plus"></i> Nuevo Prospecto</button>');
                $('#tblProspectosToolbar').css('padding', '2px 0');
                //$('#tblProyectos').DataTable({searching: false});

                var estadoPairs = [
                    { value: 0, text: 'Análisis' },
                    { value: 1, text: 'Promoción' },
                    { value: 2, text: 'Negociación' },
                    { value: 3, text: 'Cierre' }
                    ];
                //createHorizontalSelectors(estadoPairs, '.hSelectorEstado');

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
                $('#dvHerramientas').treeview({
                    collapseIcon: "fa fa-angle-down",
                    data: defaultData,
                    expandIcon: "fa fa-angle-right",
                    nodeIcon: "fa fa-folder",
                    showBorder: false,
                    enableLinks: true
                });
                $('#dvHerramientas').treeview('collapseAll', { silent: true });

                var $selTerritorio = $('#dvModalNuevoProyecto #selTerritorio');
                cargarTerritoriosPorRIK($, $selTerritorio, '<%=EntidadSesion.Id_Rik %>');


                $('#dvModalDimension').on('hidden.bs.modal', function(){
                    if($('#dvModalNuevoProyecto').hasClass('in')){
                        $('body').addClass('modal-open');
                    }
                });

                    $('input[iCheck]').iCheck({
                        checkboxClass: 'icheckbox_square-blue',
                        radioClass: 'iradio_square-blue'
                    });

                    $('#aMapaOferta').click(function(e){
                        e.preventDefault();
                        $(this).ekkoLightbox();
                    });

                    var $selUEN = $('#dvModalNuevoProyecto #selUEN');
                    cargarUENs($, $selUEN);

                    inhabilitarSelectoresDialogoNuevoProyecto();

                    inicializarModalEditarProspecto();
                    inicializarModalNuevoProspecto();
                    inicializarModalEliminarProspecto();
                    inicializarModalNuevoProyecto();

                    cargarListadosDeTerritorios();

                    $('input').inputmask();
                }


                function createHorizontalSelectors(pairs, jqSelector) {
                    $.each(pairs, function (index, element) {
                        $(jqSelector).append($('<option>', {
                            value: element.value,
                            text: element.text
                        }));
                    });
                    $(jqSelector).each(function (index) {
                        $(this).horizontalSelector();
                    });
                }

                function limpiarFormaEditarProspecto() {
                    $('#dvModalEditarProspecto #txtRFC').val('');
                    $('#dvModalEditarProspecto #txtNombre').val('');
                    $('#dvModalEditarProspecto #txtContacto').val('');
                    $('#dvModalEditarProspecto #txtEmail').val('');
                    $('#dvModalEditarProspecto #txtCalle').val('');
                    $('#dvModalEditarProspecto #txtTelefono').val('');

                    var dvTerritoriosElement = $('#dvModalEditarProspecto #dvTerritorios');
                    var jqElement = $(dvTerritoriosElement).find('#selTerritorios');
                    jqElement.selectpicker('val', 0);
                    jqElement.selectpicker('refresh');
                }

                function seleccionarTerritorios(jqTerritoriosEdicion, territorios) {
                    $(jqTerritoriosEdicion).selectpicker('val', territorios);
                    $(jqTerritoriosEdicion).selectpicker('refresh');
                }

                function inicializarModalEditarProspecto() {
                    $('#dvModalEditarProspecto').on('show.bs.modal', function (event) {
                        var trigger = $(event.relatedTarget);
                        var idCrmProspecto = trigger.data('idcrmprospecto');
                        _indiceProspectoAActualizar = trigger.data('rowidx');
                        _datosProspectoAActualizar = $('#tblProspectos').DataTable().row(_indiceProspectoAActualizar).data();
                        limpiarFormaEditarProspecto();
                        cargarCamposDialogoEditarProspecto(idCrmProspecto);
                        cargarDescripcion(_indiceProspectoAActualizar);
                    });
                }

                function cargarCamposDialogoEditarProspecto(idCrmProspecto) {
                    $.ajax({
                        url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProspecto/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>' + '&idCrmProspecto=' + idCrmProspecto,
                        type: 'GET',
                        cache: false,
                        statusCode: {
                            401: function (jqXHR, textStatus, errorThrown) {
                                $('#dvDialogoInicioSesion').modal();
                                _onLoginSuccessful = $.proxy(cargarCamposDialogoEditarProspecto, null, idCrmProspecto);
                            }
                        }
                    }).done(function (response, textStatus, jqXHR) {
                        $('#dvModalEditarProspecto #hdnIdCrmProspectoEditarProyecto').val(idCrmProspecto);
                        $('#dvModalEditarProspecto #hdnId_Cte').val(response.Id_Cte);
                        $('#dvModalEditarProspecto #hdnId_Rik').val(response.Id_Rik);
                        $('#dvModalEditarProspecto #hdnId_CrmTipoCliente').val(response.Id_CrmTipoCliente);
                        $('#dvModalEditarProspecto #txtNombre').val(response.Cte_NomComercial);
                        $('#dvModalEditarProspecto #txtContacto').val(response.Cte_Contacto);
                        $('#dvModalEditarProspecto #txtEmail').val(response.Cte_Email);
                        $('#dvModalEditarProspecto #txtCalle').val(response.Cte_Calle);
                        $('#dvModalEditarProspecto #txtTelefono').val(response.Cte_Telefono);
                        $('#dvModalEditarProspecto #txtRFC').val(response.Cte_Rfc);
                        var jqTerritoriosEdicion = $('#dvModalEditarProspecto #selTerritorios');
                        seleccionarTerritorios(jqTerritoriosEdicion, response.Territorios);
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
                    });
                }


        (function ($) {
            $(document).ready(function () {
                
            });

            
        })(jQuery);

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
                setTimeout(function(){
                    $('#toastDanger').fadeOut();
                }, 3000);
                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    onFailure($);
                }
            });
        }

        function selUEN$on_change() {
            var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
            var idUen = $('#dvModalNuevoProyecto #selUEN').selectpicker('val');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
            cargarSegmentos(jQuery, $selSegmento, idUen);
        }

        var _onLoginSuccessful=null;

        function cargarSegmentos($, jqElement, idUen, onSuccess, onFailure) {
            //mostrar el indicador de operación en proceso
            $('#imgProcesandoSegmentoDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatSegmento/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idUen=' + idUen,
                cache: false,
                type: 'GET',
                statusCode : {
                    401 : function(jqXHR, textStatus, errorThrown){
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful=$.proxy(cargarSegmentos, null, $, jqElement, idUen, onSuccess, onFailure);
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
                switch(jqXHR.status){
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                    break;
                }
                $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar los segmentos para el registro de Proyectos');
                $('#toastDanger').fadeIn();
                setTimeout(function(){
                    $('#toastDanger').fadeOut();
                }, 3000);
                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    onFailure($);
                }
            }).always(function(jqXHR, textStatus, errorThrown){
                $('#imgProcesandoSegmentoDvModalNuevoProyecto').fadeOut();
            });
        }

        function selSegmento$on_change(){
            var $selTerritorio = $('#dvModalEditarProyecto #selTerritorio');
            var idSeg = $('#dvModalNuevoProyecto #selSegmento').selectpicker('val');
            var $selArea = $('#dvModalNuevoProyecto #selArea');
            var $selUEN = $('#dvModalEditarProyecto #selUEN');
            var $selSegmento = $('#dvModalEditarProyecto #selSegmento');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
            //cargarTerritorios(jQuery, $selTerritorio, idSeg, $.proxy(cargarAreas, null, jQuery, $selArea, idSeg));
            
            cargarAreas(jQuery, $selArea, idSeg);
        }

        function cargarTerritorios($, jqElement, idSeg, onSuccess, onFailure)
        {
            $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + '<%=EntidadSesion.Id_Rik %>' + '&idSeg=' + idSeg,
                cache: false,
                type: 'GET',
                statusCode : {
                    401 : function(jqXHR, textStatus, errorThrown){
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful=$.proxy(cargarTerritorios, null, $, jqElement, idSeg, onSuccess, onFailure);
                    }
                }
            }).done(function(response, textStatus, jqXHR){
                var $selTerritorio=jqElement;
                $selTerritorio.find('option').remove();
                $selTerritorio.append('<option value="0">--Seleccione--</option>');
                $.each(response, function(index, element){
                    $selTerritorio.append('<option value="' + element.Id_Ter + '">' + element.Ter_Nombre + '</option>');
                });
                $selTerritorio.selectpicker('val', 0);
                $selTerritorio.selectpicker('refresh');
                if(typeof(onSuccess)!=undefined && typeof(onSuccess)!='undefined'){
                    onSuccess();
                }
            }).fail(function(jqXHR, textStatus, error){
                switch(jqXHR.status){
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                }

                $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos');
                $('#toastDanger').fadeIn();
                setTimeout(function(){
                    $('#toastDanger').fadeOut();
                }, 3000);
                if(typeof(onFailure)!=undefined && typeof(onFailure)!='undefined'){
                    onFailure($);
                }
            }).always(function(jqXHR, textStatus, errorThrown){
                $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeOut();
            });
        }

        function cargarAreas($, jqElement, idSeg, onSuccess, onFailure){
            $('#imgProcesandoAreaDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatArea/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idSeg=' + idSeg,
                cache: false,
                type: 'GET',
                statusCode : {
                    401 : function(jqXHR, textStatus, errorThrown){
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful=$.proxy(cargarAreas, null, $, jqElement, idSeg, onSuccess, onFailure);
                    }
                }
            }).done(function(response, textStatus, jqXHR){
                jqElement.find('option').remove();
                jqElement.append('<option value="0">--Seleccione--</option>');
                $.each(response, function(index, element){
                    jqElement.append('<option value="' + element.Id_Area + '">' + element.Area_Descripcion + '</option>');
                });
                jqElement.selectpicker('val', 0);
                jqElement.selectpicker('refresh');

                habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto();

                if(typeof(onSuccess)!=undefined && typeof(onSuccess)!='undefined'){
                    onSuccess();
                }
            }).fail(function(jqXHR, textStatus, error){
                switch(jqXHR.status){
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                }
                $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Áreas para el registro de Proyectos');
                $('#toastDanger').fadeIn();
                setTimeout(function(){
                    $('#toastDanger').fadeOut();
                }, 3000);
                if(typeof(onFailure)!=undefined && typeof(onFailure)!='undefined'){
                    onFailure($);
                }
            }).always(function(jqXHR, textStatus, errorThrown){
                $('#imgProcesandoAreaDvModalNuevoProyecto').fadeOut();
            });
        }

        function cargarSoluciones($, jqElement, idArea, onSuccess, onFailure){
            $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatSolucion/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idArea=' + idArea,
                cache: false,
                type: 'GET',
                statusCode : {
                    401 : function(jqXHR, textStatus, errorThrown){
                        //self.location='<%=Page.ResolveUrl("") %>' + '/login.aspx';
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful=$.proxy(cargarSoluciones, null, $, jqElement, idArea, onSuccess, onFailure);
                    }
                }
            }).done(function(response, textStatus, jqXHR){
                jqElement.find('option').remove();
                jqElement.append('<option value="0">--Seleccione--</option>');
                $.each(response, function(index, element){
                    jqElement.append('<option value="' + element.Id_Sol + '">' + element.Sol_Descripcion + '</option>');
                });
                jqElement.selectpicker('val', 0);
                jqElement.selectpicker('refresh');

                habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto();
                if(typeof(onSuccess)!=undefined && typeof(onSuccess)!='undefined'){
                    onSuccess($);
                }
            }).fail(function(jqXHR, textStatus, error){
                switch(jqXHR.status){
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                }
                $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Soluciones para el registro de Proyectos');
                $('#toastDanger').fadeIn();
                setTimeout(function(){
                    $('#toastDanger').fadeOut();
                }, 3000);
                if(typeof(onFailure)!=undefined && typeof(onFailure)!='undefined'){
                    onFailure($);
                }
            }).always(function(jqXHR, textStatus, errorThrown){
                $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeOut();
            });
        }

        function selArea$on_change(){
            var $selSolucion = $('#dvModalNuevoProyecto #selSolucion');
            var idArea = $('#dvModalNuevoProyecto #selArea').selectpicker('val');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
            cargarSoluciones(jQuery, $selSolucion, idArea);
        }

        var _aplicacionesSeleccionadas = [];

        function cargarAplicaciones($, jqElement, idSol, onSuccess, onFailure){
            $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeIn();
            var idUen = $('#dvModalNuevoProyecto #selUEN').selectpicker('val');
            var idSeg = $('#dvModalNuevoProyecto #selSegmento').selectpicker('val');
            var idArea = $('#dvModalNuevoProyecto #selArea').selectpicker('val');
            var idCte = $('#dvModalNuevoProyecto #hdnCliente').val();
            var idOp = $('#dvModalNuevoProyecto #hdnId_Op').val();
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
                    var node = $(contenidoPersonalizadoAplicacion(element));
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

                    var apOpObj = $('#txtAplVPO_' + apId).data('obj');
                    if (apOpObj == null) {
                        var apObj = $(event.target).data('obj');
                        apOpObj = {
                            Id_Emp: apObj.Id_Emp,
                            Id_Cd: '<%=EntidadSesion.Id_Cd %>',
                            Id_Op: idOpVar != null ? idOpVar : 0,
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
                    _aplicacionesSeleccionadas = $.grep(_aplicacionesSeleccionadas, function (value) {
                        return value.Id_Apl != apId;
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

        function selSolucion$on_change(){
            var $selAplicacion = $('#dvModalNuevoProyecto #selAplicacion');
            var idSol = $('#dvModalNuevoProyecto #selSolucion').selectpicker('val');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
            cargarAplicaciones(jQuery, $selAplicacion, idSol);
        }

        function cerrarToastDanger($){
            $('#toastDanger').fadeOut();
        }

        function cerrarToastSuccess($){
            $('#toastSuccess').fadeOut();
        }

        function cerrarToastWarning($) {
            $('#toastWarning').fadeOut();
        }

        function eliminarProspecto($){
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProspecto',
                type: 'DELETE',
                cache: false,
                data: prospectoActualAEliminar,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(eliminarProspecto, null, $);
                    }
                }
            }).done(function(response, textStatus, jqXHR){
                _renglonDelProspectoAEliminar.remove();
                _renglonDelProspectoAEliminar=null;
                prospectoActualAEliminar=null;
                limpiarSeccionDatosGenerales();
                $('#tblProspectos').DataTable().draw();
                $('#dvModalEliminarProspecto').modal('hide');
                $('#toastSuccess #toastSuccessMessage').html('El prospecto se eliminó satisfactoriamente');
                $('#toastSuccess').fadeIn();
                setTimeout(function(){
                    $('#toastSuccess').fadeOut();
                }, 3000); 
            }).fail(function(jqXHR, textStatus, error){
                switch (jqXHR.status) {
                    case 401:
                        alert('La sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        $('#dvModalEliminarProspecto').modal('hide');
                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                        $('#toastDanger').fadeIn();
                        setTimeout(function () {
                            $('#toastDanger').fadeOut();
                        }, 3000);
                        break;
                }
            });
        }

        function habilitarSelectorDependienteDelSelectorUENDialogoNuevoProyecto(){
            $('#selSegmento').prop('disabled', false);
            $('#selSegmento').selectpicker('refresh');
        }

        function deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto(){
            $('#selSegmento').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        }

        function despopularCascadaDependientesSelectorUENDialogoNuevoProyecto(){
            $('#selSegmento').find('option').remove();
            $('#selSegmento').selectpicker('refresh');
            despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
        }

        function habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto(){
            $('#selArea').prop('disabled', false);
            $('#selArea').selectpicker('refresh');

            //$('#selTerritorio').prop('disabled', false);
            //$('#selTerritorio').selectpicker('refresh');
        }

        function deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto(){
            $('#selArea').selectpicker('refresh');
            $('#selTerritorio').selectpicker('refresh');

            deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        function despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto(){
            $('#selArea').find('option').remove();
            //$('#selTerritorio').find('option').remove();
            $('#selArea').selectpicker('refresh');
            //$('#selTerritorio').selectpicker('refresh');
            despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
        }

        function habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto(){
            $('#selSolucion').prop('disabled', false);
            $('#selSolucion').selectpicker('refresh');
        }

        function deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto(){
            $('#selSolucion').selectpicker('refresh');
            deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        function despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto(){
            $('#selSolucion').find('option').remove();
            $('#selSolucion').selectpicker('refresh');
            despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
        }

        function habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto(){
            $('#selAplicacion').prop('disabled', false);
            $('#selAplicacion').selectpicker('refresh');
        }

        function deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto(){
            $('#selAplicacion').selectpicker('refresh');
        }

        function despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto(){
            $('#selAplicacion').find('option').remove();
            $('#selAplicacion').selectpicker('refresh');

            var $lstAplicacion = $('#lstAplicacion');
            $lstAplicacion.find('div').remove();
        }

        function inhabilitarSelectoresDialogoNuevoProyecto(){
            deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

        function actualizarProspecto(){
            $(this).prop('disabled', true);
            $('#imgDvModalEditarProspectoEnProgreso').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProspecto',
                type: 'PUT',
                cache: false,
                data: $('#frmDvModalEditarProspecto').serialize(),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(actualizarProspecto, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                _datosProspectoAActualizar.Cte_NomComercial=$('#dvModalEditarProspecto #txtNombre').val();
                _datosProspectoAActualizar.Cte_Contacto=$('#dvModalEditarProspecto #txtContacto').val();
                _datosProspectoAActualizar.Cte_Email=$('#dvModalEditarProspecto #txtEmail').val();
                _datosProspectoAActualizar.Cte_Calle=$('#dvModalEditarProspecto #txtCalle').val();
                _datosProspectoAActualizar.Cte_Telefono=$('#dvModalEditarProspecto #txtTelefono').val();
                $('#toastSuccess #toastSuccessMessage').html('El prospecto ha sido actualizado con éxito');
                $('#toastSuccess').fadeIn();
                //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
                setTimeout(function () {
                    $('#toastSuccess').fadeOut();
                }, 3000);
                $('#dvModalEditarProspecto').modal('hide');
                $('#tblProspectos').DataTable().row(_indiceProspectoAActualizar).data(_datosProspectoAActualizar);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        $(this).modal('hide');
                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.ExceptionMessage);
                        $('#toastDanger').fadeIn();
                        setTimeout(function () {
                            $('#toastDanger').fadeOut();
                        }, 3000);
                        break;
                }
            }).complete(function(){
                $(this).prop('disabled', false);
                $('#imgDvModalEditarProspectoEnProgreso').fadeOut();
            });
        }

        function limpiarFormaNuevoProspecto() {
            $('#dvModalNuevoProspecto #txtRFC').val('');
            $('#dvModalNuevoProspecto #txtNombre').val('');
            $('#dvModalNuevoProspecto #txtContacto').val('');
            $('#dvModalNuevoProspecto #txtEmail').val('');
            $('#dvModalNuevoProspecto #txtCalle').val('');
            $('#dvModalNuevoProspecto #txtTelefono').val('');

            var dvTerritoriosElement = $('#dvModalNuevoProspecto #dvTerritorios');
            var jqElement = $(dvTerritoriosElement).find('#selTerritorios');
            jqElement.selectpicker('val', 0);
            jqElement.selectpicker('refresh');
        }

        function inicializarModalNuevoProspecto(){
            $('#dvModalNuevoProspecto').on('show.bs.modal', function(event){
                var trigger=$(event.relatedTarget);
                $('#btnDvModalNuevoProspectoGuardar').prop('disabled', false);
                limpiarFormaNuevoProspecto();
            });
        }

        function crearProspecto(){
            $(this).prop('disabled', true);
            $('#imgDvModalNuevoProspectoEnProgreso').fadeIn();
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CrmProspecto',
                type: 'POST',
                cache: false,
                data: $('#frmDvModalNuevoProspecto').serialize(),
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(actualizarProspecto, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $('#toastSuccess #toastSuccessMessage').html('El prospecto ha sido creado con éxito');
                $('#toastSuccess').fadeIn();
                //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
                setTimeout(function () {
                    $('#toastSuccess').fadeOut();
                }, 3000);
                $('#dvModalNuevoProspecto').modal('hide');
                $('#tblProspectos').DataTable().row.add(response).draw();
            }).fail(function (jqXHR, textStatus, errorThrown) {
                switch (jqXHR.status) {
                    case 401:
                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                        break;
                    default:
                        //$(this).modal('hide');
                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.ExceptionMessage);
                        mostrarToast($('#toastDanger'), $('#dvModalNuevoProspecto'));
                        setTimeout(function () {
                            $('#toastDanger').fadeOut();
                        }, 3000);
                        break;
                }
            }).always(function () {
                $(this).prop('disabled', false);
                $('#imgDvModalNuevoProspectoEnProgreso').fadeOut();
            });
        }

        var prospectoActualAEliminar=null;
        var _renglonDelProspectoAEliminar=null;
        function inicializarModalEliminarProspecto(){
            $('#dvModalEliminarProspecto').on('show.bs.modal', function(event){
                var trigger=$(event.relatedTarget);
                var rowId=trigger.data('rowid');
                _renglonDelProspectoAEliminar=$('#tblProspectos').DataTable().row(rowId);
                var datosProspecto=_renglonDelProspectoAEliminar.data();
                prospectoActualAEliminar=datosProspecto;
                cargarDescripcion(rowId);
            });
        }

        function limpiarSeccionDatosGenerales(){
            $('#ddDatosGeneralesContacto').text('');
            $('#ddDatosGeneralesCorreoElectronico').text('');
            $('#ddDatosGeneralesTelefono').text('');
            $('#ddDatosGeneralesNombreComercial').text('');
            $('#ddDatosGeneralesCalle').text('');
        }

        function crearProyecto() {
            $('#dvModalNuevoProyecto #selUEN').prop('disabled', false);
            $('#dvModalNuevoProyecto #selSegmento').prop('disabled', false);
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
                actualizarAplicacionesVPO(response.Id_Op, function () {
                    $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito');
                    $('#toastSuccess').fadeIn();
                    setTimeout(function () {
                        $('#toastSuccess').fadeOut();
                    }, 3000);
                    $('#dvModalNuevoProyecto').modal('hide');
                }, function (jqXHR, textStatus, error) {
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
                $('#dvModalNuevoProyecto #selUEN').prop('disabled', true);
                $('#dvModalNuevoProyecto #selSegmento').prop('disabled', true);
                $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
                $('#dvModalNuevoProyecto #selSegmento').selectpicker('refresh');
                $(this).prop('disabled', false);
                $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
            });
        }

        function actualizarAplicacionesVPO(idOp, onSuccess, onFailure) {
            $.each(_aplicacionesSeleccionadas, function (index, item) {
                item.Id_Op = idOp;
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
            }).done(function (response, textStatus, jqXHR) {
                _aplicacionesSeleccionadas = [];
                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    if (onSuccess != null) {
                        onSuccess(response, textStatus, jqXHR);
                    }
                }
            }).fail(function (jqXHR, textStatus, error) {
                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    if (onFailure != null) {
                        onFailure(jqXHR, textStatus, error);
                    }
                }
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            });
        }

        function inicializarModalNuevoProyecto() {
            $('#dvModalNuevoProyecto').on('show.bs.modal', function (event) {
                var trigger = $(event.relatedTarget);
                $('#btnDvModalNuevoProyectoGuardar').prop('disabled', false);
                var rowId=$(trigger).data('rowidx');
                var datosProspecto=$('#tblProspectos').DataTable().row(rowId).data();
                $('#dvModalNuevoProyecto #hdnId_CrmProspecto').val(datosProspecto.Id_CrmProspecto);
                $('#dvModalNuevoProyecto #hdnCliente').val(datosProspecto.Id_Cte);
                
                limpiarFormaNuevoProyecto();
                cargarDescripcion(rowId);
            });
        }

        function limpiarFormaNuevoProyecto() {
            $('#hdnDim_Id_Uen').val(null);
            $('#hdnDim_Id_Seg').val(null);
            $('#txtDimension').val('');
            $('#txtCantidad').val('');
            $('#txtVPM').val('');

            var $selTerritorio = $('#dvModalNuevoProyecto #selTerritorio');
            $selTerritorio.selectpicker('val', 0);
            $selTerritorio.selectpicker('refresh');

            $('#dvModalNuevoProyecto #selUEN').selectpicker('val', 0);
            $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');

            despopularCascadaDependientesSelectorTerritorio();
            //despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

        function listadoTerritoriosListo(territorios) {
            cargarTerritoriosParaDialogoNuevoProspecto(territorios);
            cargarTerritoriosParaDialogoEditarProspecto(territorios);
        }

        function listadoTerritoriosFallido() {
        }

        function listadoTerritoriosSiempre() {
            $('#dvModalNuevoProspecto').find('#imgProgreso').fadeOut();
            $('#dvModalEditarProspecto').find('#imgProgreso').fadeOut();
            $('#dvModalNuevoProspecto #selTerritorio').attr('disabled', false);
            $('#dvModalEditarProspecto #selTerritorio').attr('disabled', false);
        }

        function cargarListadosDeTerritorios() {
            $('#dvModalNuevoProspecto').find('#imgProgreso').fadeIn();
            $('#dvModalEditarProspecto').find('#imgProgreso').fadeIn();
            $('#dvModalNuevoProspecto #selTerritorio').attr('disabled', true);
            $('#dvModalEditarProspecto #selTerritorio').attr('disabled', true);
            cargarDatosTerritorios($, $.proxy(listadoTerritoriosListo), $.proxy(listadoTerritoriosFallido), $.proxy(listadoTerritoriosSiempre));
        }

        function cargarTerritoriosParaDialogoNuevoProspecto(datosTerritorios) {
            var dvTerritoriosElement=$('#dvModalNuevoProspecto #dvTerritorios');
            var jqElement=$(dvTerritoriosElement).find('#selTerritorios');

            var $selTerritorio = jqElement;
            $selTerritorio.find('option').remove();
            $.each(datosTerritorios, function (index, element) {
                $selTerritorio.append('<option value="' + element.Id_Ter + '">' + element.Ter_Nombre + '</option>');
            });
            $selTerritorio.selectpicker('val', 0);
            $selTerritorio.selectpicker('refresh');
        }

        function cargarTerritoriosParaDialogoEditarProspecto(datosTerritorios) {
            var dvTerritoriosElement=$('#dvModalEditarProspecto #dvEdicionProspectosTerritorios');
            var jqElement=$(dvTerritoriosElement).find('#selTerritorios');
            var $selTerritorio = jqElement;
            $selTerritorio.find('option').remove();
            $.each(datosTerritorios, function (index, element) {
                $selTerritorio.append('<option value="' + element.Id_Ter + '">' + element.Ter_Nombre + '</option>');
            });
            $selTerritorio.selectpicker('val', 0);
            $selTerritorio.selectpicker('refresh');
        }

        //Consulta los territorios asociados a un RIK
        function cargarDatosTerritorios($, onSuccess, onFailure, always) {
            $.ajax({
                url: '<%=Page.ResolveUrl("") %>' + '/api/CatTerritorio',
                type: 'GET',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(cargarTerritorios, this, $, dvTerritoriosElement, jqElement, onSuccess, onFailure);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                    if(onSuccess!=null)
                        onSuccess(response);
                }
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
                if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
                    if (onFailure != null)
                        onFailure($);
                }
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                if (typeof (always) != undefined && typeof (always) != 'undefined') {
                    if (always != null)
                        always(jqXHROrData);
                }
            });
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

        function selTerritorio$on_change(_this) {
            var value=$(_this).selectpicker('val');
            var objTerritorio=$(_this).find('option[value="' + value + '"]').data('objterritorio');
            despopularCascadaDependientesSelectorTerritorio();
            var $selUEN = $('#dvModalNuevoProyecto #selUEN');
            var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
            $('#txtDimension').val('');
            $('#txtPrecioUnidad').val('');
            if(objTerritorio!=typeof(undefined) && objTerritorio!='undefined' && objTerritorio!=undefined){
                cargarSelUEN($selUEN, objTerritorio.CatUENSerializable);
                cargarSegmento($selSegmento, objTerritorio.CatSegmentoSerializable);
                selSegmento$on_change();
            }
        }

        function despopularCascadaDependientesSelectorTerritorio() {
            $('#selUEN').find('option').remove();
            $('#selUEN').selectpicker('refresh');
            despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
        }

        function cargarSegmento(jqelement, objSeg) {
            jqelement.append('<option value="' + objSeg.Id_Seg + '">' + objSeg.Seg_Descripcion + '</option>');
            jqelement.selectpicker('val', 0);
            jqelement.selectpicker('refresh');

            $('#dvModalNuevoProyecto #hdnDim_Id_Uen').val(objSeg.Id_Uen);
            $('#dvModalNuevoProyecto #hdnDim_Id_Seg').val(objSeg.Id_Seg);
            $('#dvModalNuevoProyecto #txtDimension').val(objSeg.Seg_Unidades);
            $('#dvModalNuevoProyecto #txtPrecioUnidad').val(objSeg.Seg_ValUniDim);
        }

        function cargarSelUEN(jqelement, objUen) {
            jqelement.append('<option value="' + objUen.Id_Uen + '">' + objUen.Uen_Descripcion + '</option>');
            jqelement.selectpicker('val', 0);
            jqelement.selectpicker('refresh');
        }

        function contenidoPersonalizadoAplicacion(aplicacion) {
            var cantidad = $('#txtCantidad').val();
            if (cantidad == '') {
                cantidad = 0;
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
                                    aplicacion.Apl_Potencial / 100.0 * cantidad +
                                '</td>' +
                                '<td style="width: 33%;">' +
                                    '<div class="row">' +
                                        '<div class="col-md-1">' +
                                           'VPO:' +
                                        '</div>' +
                                        '<div class="col-md-6">' +
                                            '<input type="text" id="txtAplVPO_' + aplicacion.Id_Apl + '" style="display: none;" class="form-control" onchange="txtAplVPO$onchange(this)">' + //aplicacion.Porcentaje/100.0 +
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

        function chkApl_onchange(sender) {
            var chk = $(sender);
            var valoresAps = $('#selAplicacion').selectpicker('val');
            var apId = chk.data('idapl');
            if (chk.is(':checked') == true) {
                valoresAps.push(apId);
            }
            else {
                valoresAps = $.grep(valoresAps, function (value) {
                    return value != apId;
                });
            }
            $('#selAplicacion').selectpicker('val', valoresAps);
            $('#selAplicacion').selectpicker('refresh');
        }

        function crearProyectoYContinuar() {
            $('#dvModalNuevoProyecto #selUEN').prop('disabled', false);
            $('#dvModalNuevoProyecto #selSegmento').prop('disabled', false);
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
                        _onLoginSuccessful = $.proxy(crearProyectoYContinuar, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                actualizarAplicacionesVPO(response.Id_Op, function () {
                    $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito. Redirigiendo...');
                    $('#toastSuccess').fadeIn();
                    setTimeout(function () {
                        $('#toastSuccess').fadeOut();
                    }, 3000);
                    $('#dvModalNuevoProyecto').modal('hide');
                    window.location.href = 'Proyectos.aspx?Id_Cliente=' + response.Cliente + '&Id_Op=' + response.Id_Op;
                }, function (jqXHR, textStatus, error) {
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
                $('#dvModalNuevoProyecto #selUEN').prop('disabled', true);
                $('#dvModalNuevoProyecto #selSegmento').prop('disabled', true);
                $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
                $('#dvModalNuevoProyecto #selSegmento').selectpicker('refresh');
                $(this).prop('disabled', false);
                $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
            });
        }

        function dimensionElegida(idUen, idSeg, unidades) {
            $('#dvModalNuevoProyecto #hdnDim_Id_Uen').val(idUen);
            $('#dvModalNuevoProyecto #hdnDim_Id_Seg').val(idSeg);
            $('#dvModalNuevoProyecto #txtDimension').val(unidades);
            $('#dvModalDimension').modal('hide');
        }

        function txtAplVPO$onchange(sender) {
            var objetoDatos = $(sender).data('obj');
            objetoDatos.CrmOpAp_VPO = $(sender).val();
        }

        var _indiceProspectoAActualizar=-1;
        var _datosProspectoAActualizar = null;

        function txtRFCPH_onincomplete() {
            $(this).val('');
        }
    </script>
    
    <script src="<%=Page.ResolveUrl("~/js/horizontal_selector.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-treeview.min.js") %>"></script>
    <!--<script src="//rawgit.com/jonmiles/bootstrap-treeview/v1.2.0/dist/bootstrap-treeview.min.js"></script>-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphToolbar" runat="server">
    
</asp:Content>
