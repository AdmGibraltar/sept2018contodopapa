<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="ProspectosV2.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.ProspectosV2" %>
<%@ Register Src="~/js/UCAsociacionProspectoTerritorio_js.ascx" TagPrefix="uc" TagName="UCAsociacionProspectoTerritorio_js" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/SelectorDimension.ascx" TagPrefix="uc" TagName="SelectorDimension" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/horizontal_selector.css")%>" rel="stylesheet">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/css/radios-to-slider.min.css")%>">
    <link rel="shortcut icon" href="<%=Page.ResolveUrl("~/Img/favicon.ico")%>">
                   
    <link href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/bootstrap-treeview.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/gridstack/gridstack.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/gridstack/gridstack-extra.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.css")%>" rel="stylesheet">
    <%--<link href="<%=Page.ResolveUrl("~/css/horizon-swiper/horizon-swiper.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/horizon-swiper/horizon-theme.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/horizon-swiper/horizon-theme.min.css")%>" rel="stylesheet">--%>
    <link href="<%=Page.ResolveUrl("~/css/key_soluciones.css")%>" rel="stylesheet">
        
    
    <style>
        .myCenteredCellTable
        {
            text-align: center;
        }
        
        .noteTitleStyle
        {
            border-radius: 100px 100px 0px 0px;
            background: #00C957;
            
            text-align: center;
        }        
        label.error
        {
            color: Red;
        }
        
        .tooltip-inner {
            white-space:pre-wrap;
        }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyContent" runat="server">
   
    <div class="row" style="display: block;">

        <input type="hidden" id="hfdocumento_Info1" value="" />
        <input type="hidden" id="hfdocumento_Info2" value="" />

        <div class="col-sm-12 col-md-12">

            <div class="row ROWPAD MARGIN_BT5">
                <div class="col-sm-12 col-md-12 ROWPAD">

                <table style="width:100%;">
                    <tr>
                        <td>
                            <h3 style="display: inline-block;"><strong>Prospectos</strong></h3> 
                        </td>
                        <td style="width:20px;" valign="middle">                            
                            <div style="display:none;" id="Gerente_Icono">
                            <!--i class="fas fa-user-tie"></i-->                            
                            </div>
                        </td>
                        <td style="width:30px;" valign="middle">                            
                            <label style="margin-top:5px; display:none;" id="Gerente_lbRik">Rik&nbsp;</label>
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
                            <button id="btnNuevoProspecto" type="button" class="btn btn-primary pull-right" onclick="btnNuevoProspecto();" data-action="1" style="width:100%;">
                                <i class="fa fa-plus"></i>&nbsp;Nuevo
                            </button>
                        </td>
                    </tr>
                </table>

                    <!--div class="form-group">                    
                    </div-->

                </div>
                <%--<button class="btn btn-primary pull-right" data-toggle="modal" data-target="#dvModalNuevoProspecto" data-action="1" >--%>                
            </div>

            <div class="row ROWPAD">
                <div class="col-sm-12 col-md-12 ROWPAD">                

                    <div id="dvLoading"></div>

                    <table id="tblProspectos" class="datatable table table-striped table-bordered table-hover display">
                        <thead>
                            <tr>
                                <%--<th>#</th>--%>
                                <%-- th>Clave</th--%>
                                <th>Nombre</th>
                                <th>Editar<!--Columna de comando de edición--></th>
                                <th>Nuevo Proyecto</th>
                                <th>Eliminar<!--Columna de comando de eliminación-->
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>
    
    <div class="row" style="display: none;" id="dvSeguimiento">
        <div class="col-sm-12 col-md-12">

            <div class="row ROWPAD">
                <div class="col-sm-12 col-md-12 ROWPAD">
                    <h3 id="ProspectoNombre"><strong>Seguimiento</strong></h3>
                </div>
            </div>

            <div class="row ROWPAD">
                <div class="col-sm-12 col-md-12 ROWPAD">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" id="tabSeguimiento">
                        <li class="active"><a href="#dvGeneral" data-toggle="tab">Datos Generales</a></li>
                        <li><a href="#dvNotas" data-toggle="tab">Notas</a></li>
                        <li><a href="#dvSeccionTerritorios" data-toggle="tab">Territorio</a></li>
                        <!--<li>
                            <a href="#dvAnalisisYDiagnostico" data-toggle="tab">Análisis y Diagnóstico</a>
                        </li>-->
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">

                        <div role="tabpanel" class="tab-pane active" id="dvGeneral">
                            <div class="row" style="margin-top:10px">                                       
                                <input type="hidden" id="hfDatosGenerales_Id_Cd" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_CrmProspecto" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_CrmTipoCliente" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_Cte" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_Emp" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_Rik" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_CteDet" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_Ter" value="" />
                                <input type="hidden" id="hfDatosGenerales_Id_Seg" value="" />
                                            
                                <div class="col-md-12 col-sd-12 col-xd-10" >                                       
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
                            </div> 
                        </div>

                        <div class="tab-pane" role="tabpanel" id="dvNotas">
                            <%--NOTAS --%>
                            <%--NOTAS --%>
                            <%--NOTAS --%>
                            <div class="row ROWPAD" style="margin-top:4px; margin-bottom:4px;">
                                <div class="col-md-12">
                                    <button id="btnCrearNota" class="btn btn-primary pull-right" onclick="crearPrimeraNota(this)">
                                        <i class="fa fa-plus"></i>&nbsp;Agregar nota
                                    </button>
                                </div>
                            </div>

                            <div class="row ROWPAD">
                                <div class="col-md-12">
                                                                                            
                                    <div class="blank-slate-pf" id="bsNotasVacias">
                                        <h1>Notas</h1>
                                        <p>En esta sección podrás agregar notas de utilidad</p>
                                        <div class="blank-slate-pf-main-action">
                                               
                                        </div>
                                    </div>
                                    <!--button type="button" class="btn btn-default form-control" onclick="agregarNota(this)" id="btnAgregarNota" style="display:none;">Agregar Nota</button-->
                                    <div id="grdstkNotas" class="grid-stack" data-gs-width="12" data-gs-animate="yes" style="display:none;">                                            
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="dvSeccionTerritorios">
                            <%--TERRITORIOs --%>
                            <%--TERRITORIOs --%>
                            <%--TERRITORIOs --%>
                            <div class="row MARGIN_BOTTOM5">
                                <div class="col-md-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h5><strong>Asociar Territorio</strong></h5>
                                        </div>
                                        <div class="panel-body;">

                                        <div style="width:100%;">

                                        <table class="table_ter">
                                            <tr>
                                                <td style="text-align:left; width:200px;">
                                                    <label>Territorio</label>
                                                </td>
                                                <td style="text-align:left; width:200px;">
                                                    <select class="selectpicker form-control" style="width:195px;" id="selDetallesAsociarTerritorio_Id_Ter">
                                                    </select>    
                                                </td>                                                
                                            </tr><tr>
                                                <td style="text-align:left">
                                                    <label>Valor Potencial Observado (VPO)</label>
                                                </td>
                                                <td style="text-align:left">
                                                    <input type="text" id="txtDetallesAsociarTerritorio_Potencial" class="form-control" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />
                                                </td>                                                
                                            </tr><tr>
                                                <td colspan="2" style="text-align:right;">
                                                    <button class="btn btn-primary pull-right" type="button" id="btnDetallesAsociarTerritorio_Asociar">
                                                        <i class="fa fa-plus"></i>&nbsp;Asociar
                                                    </button>
                                                </td>
                                            </tr>
                                        </table>


                                        </div>

                                        
                                        <!--div class="row MARGIN_BOTTOM5">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-2">
                                                <label>Territorio</label>
                                            </div>
                                            <div class="col-md-4">
                                                <select class="selectpicker form-control" id="selDetallesAsociarTerritorio_Id_Ter">                                                                                                            
                                                </select>    
                                            </div>
                                        </div>

                                        <div class="row MARGIN_BOTTOM5">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-2">
                                                <label>Valor Potencial Observado (VPO)</label>
                                            </div>
                                            <div class="col-md-4">
                                                <input type="text" id="txtDetallesAsociarTerritorio_Potencial" class="form-control" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />
                                            </div>
                                        </div>

                                        <div class="row MARGIN_BOTTOM5">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-6">
                                                <button class="btn btn-primary pull-right" type="button" id="btnDetallesAsociarTerritorio_Asociar">
                                                    <i class="fa fa-plus"></i>&nbsp;Asociar
                                                </button>
                                            </div>
                                            <div class="col-md-5"></div>
                                        </div-->
                                                        
                                        </div>
                                    </div>                                                                                                
                                </div>
                            </div>
                                        
                            <div class="row MARGIN_BOTTOM5">
                                <div class="col-md-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h5><strong>Territorios Asociados</strong></h5>
                                    </div>
                                    <div class="panel-body">

                                        <table id="tblDetallesTerritoriosAsociados" class="table table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th>Clave</th>
                                                <th>Nombre de Territorio</th>
                                                <th style="text-align: center;">VPO</th>
                                                <th style="text-align: center;">Contactos</th>
                                                <%--<th style="text-align: center;">Editar</th>--%>
                                                <th style="text-align: center;">Retirar</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                        </table>

                                    </div>
                                </div>                                            
                                </div>
                            </div>
                                        
                            <div class="panel panel-default">
                            </div>

                        </div>
                    </div>            
                </div>        
            </div>

        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="modalContacto" class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaNuevoContacto">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="h9">
                        Datos de contacto
                    </h4>
                </div>
                <%--<div class="modal-body" id="dvContenidoModalContactos">--%>
                <div class="modal-body">

                    <input type="hidden" id="hdContacto_Id_Emp" />
                    <input type="hidden" id="hdContacto_Id_Cd"/>
                    <input type="hidden" id="hdContacto_Id_Cte"/>
                    <input type="hidden" id="hdContacto_Id_Ter"/>
                    <input type="hidden" id="hdContacto_Id_Consecutivo"/>

                    <div class="row">                                                
                        <div class="col-md-12">
                            
                            <div class="row">
                                <div class="col-md-8">
                                                                    
                                     <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"><label>Nombre</label></div>
                                        <div class="col-md-8">
                                            <input class="form-control" type="text" id="txtContactoNombre" />
                                        </div>                                
                                    </div>
                                        
                                    <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"><label>Puesto</label></div>
                                        <div class="col-md-8">
                                            <input class="form-control" type="text" id="txtContactoPuesto" />
                                        </div>
                                    </div>

                                    <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"><label >Cumpleaños</label></div>
                                        <div class="col-md-6">
                                            <input class="form-control" type="date" id="txtContactoCumple" />
                                        </div>
                                    </div>

                                    <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"><label>Correo electrónico</label></div>
                                        <div class="col-md-8">
                                            <input class="form-control" type="text" id="txtContactoCorreo" />
                                        </div>
                                    </div>

                                    <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"><label>Dirección</label></div>
                                        <div class="col-md-8">
                                            <input class="form-control" type="text" id="txtContactoDireccion1" />
                                        </div>
                                    </div>
                            
                                    <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"></div>
                                        <div class="col-md-8">
                                            <input class="form-control" type="text" id="txtContactoDireccion2" />
                                        </div>
                                    </div>

                                    <div class="row MARGIN_BOTTOM5">                                                
                                            <div class="col-md-12"><label>Teléfonos</label></div>                                        
                                    </div>

                                    <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"><label>Negocio</label></div>
                                        <div class="col-md-8">
                                            <input class="form-control" type="text" id="txtContactoTelNegocio" />
                                        </div>
                                    </div>

                                    <div class="row MARGIN_BOTTOM5">                                                
                                        <div class="col-md-4"><label>Casa</label></div>
                                        <div class="col-md-8">
                                            <input class="form-control" type="text" id="txtContactoTelCasa" />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-4">
                                        <img class="img-thumbnail" src="<%=Page.ResolveUrl("~/Img/user-placeholder-140x140.png") %>" />
                                </div>

                            </div>

                            </div>

                        </div>
                    </div>

                  
<%--                    <div class="row">                                                
                        <div class="col-md-12">

                            <img id="Img2" />
                                        
                            <div class="form-group">                                                                   
                                <div class="input-group">
                                    <input class="form-control" type="text" id="Text4" />
                                    <span class="input-group-addon"><a href="#" data-toggle="modal" data-target="#dvModalMapaGoogle" id="A1" ><i class="fa fa-map-marker" aria-hidden="true"></i></a></span>
                                    <input type="hidden" id="Hidden1" />
                                    <input type="hidden" id="Hidden2" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="txtDireccionHogar">Hogar</label>
                                    
                                <div class="input-group">
                                    <input class="form-control" type="text" id="Text5" />
                                    <span class="input-group-addon"><a href="#" data-toggle="modal" data-target="#dvModalMapaGoogle" id="A2"><i class="fa fa-map-marker" aria-hidden="true"></i></a></span>
                                    <input type="hidden" id="Hidden3" />
                                    <input type="hidden" id="Hidden4" />
                                </div>
                            </div>

                            <hr />
                            <h4>Teléfonos</h4>

                            <div class="form-group">
                                <label for="txtTelefonoNegocio">Negocio</label>
                                <input class="form-control" type="text" id="Text6" />
                            </div>

                            <div class="form-group">
                                <label for="txtTelefonoHogar">Hogar</label>
                                <input class="form-control" type="text" id="Text7" />
                            </div>

                        
                        <%--
                        <div class="col-md-4">
                            <img class="img-thumbnail" src="<%=Page.ResolveUrl("~/Img/user-placeholder-140x140.png") %>" />
                        </div>
                        --%
                            
                        </div>
                    </div>--%>
             
                                 
                
                <div class="modal-footer">
                    <button class="btn btn-primary" id="Button2" onclick="btnContactoGuardar()" >
                        <i class="fa fa-plus"></i>&nbsp;Guardar
                    </button>&nbsp;
                    <button type="button" class="btn btn-default pull-right" onclick="btnContactoCancelar()">Cerrar</button>
                </div>

                </div>

            </div>
        </div>
        
    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="ModalContactos" data-backdrop="static" data-keyboard="false" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaNuevoContacto">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="h8">
                        Contactos 
                    </h4>
                </div>
                <%--<div class="modal-body" id="dvContenidoModalContactos">--%>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">                                                  

                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-12">
                         <table class="table table-striped table-hover" id='tblListadoContatos'>
                            <thead>
                                <th>Nombre</th>
                                <th colspan="2">Teléfonos</th>                                
                            </thead>
                            <tbody>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                    </div>
                                       
                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary" id="btnContactoNuevo"><i class="fa fa-plus"></i>&nbsp;Agregar contacto</button>&nbsp;
                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div class="modal fade" id="ModalNuevoProspecto" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="imgDvModalNuevoProspectoEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Nuevo Prospecto</h4>
                </div>
                <div class="modal-body">
                    <form id="frmDvModalNuevoProspecto">
                                                                                    
                        <input type="hidden" id="hdnId_Cte" name="hdnId_Cte" value="" /> 
                        <input type="hidden" id="hdnCrearNuevo" name="hdnCrearNuevo" value="0" /> 

                        <%--<ul class="nav nav-tabs" role="tablist" id="navMenu">--%>
                        <ul class="nav nav-tabs" role="tablist" id="tabProspectoNuevoTerritorio">
                            <li role="presentation" class="active">
                                <a href="#dvMenu_General" aria-controls="dvMenu_General" role="tab" data-toggle="tab">
                                    General
                                </a>
                            </li>
                            <!--<li role="presentation">
                                <a href="#dvMenu_Contactos" aria-controls="dvMenu_Contactos" role="tab" data-toggle="tab">
                                    Contactos
                                </a>
                            </li>-->
                            <%--<li role="presentation">
                                <a href="#Menu_Territorios" aria-controls="Menu_Territorios" role="tab" data-toggle="tab">
                                    Territorios
                                </a>
                            </li>--%>
                        </ul>

                        <div class="tab-content" id="tabMenu">
                            <div id="dvMenu_General" class="tab-pane active" role="tabpanel">
                                <div class="form-group" style="display: none;">
                                    <label for="selTerritorioProspecto">Territorio&nbsp
                                        <i class="fa fa-flag-checkered" aria-hidden="true"></i>
                                    </label>
                                    <select id="selTerritorioProspecto" name="TerritorioTemporal" class="selectpicker form-control" required>
                                        <asp:Repeater runat="server" ID="rptTerritoriosProspecto">
                                            <ItemTemplate>
                                                <option value='<%#Eval("Id_Ter") %>'><%#Eval("Id_Ter") %> - <%#Eval("Ter_Nombre")%></option>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </select>
                                </div>
                                <div class="form-group" id="dvAlertaProspectoExistente" style="display: none;">
                                    <div class="alert alert-info">
                                        <span class="pficon pficon-info"></span>
                                        <strong>
                                            Haz elegido un prospecto existente. Puedes optar por <a class="alert-link" href="javascript:crearProyectoDeProspectoExistente('')">crear un proyecto</a> asociado a dicho prospecto o continuar con la captura de un nuevo prospecto
                                        </strong>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                        <table id="tbRFC" style="display:block; width:100%;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <label for="txtRFC">No. Cliente</label>
                                                    </td>
                                                    <td>
                                                        <label for="txtRFC">R.F.C.&nbsp</label>
                                                        <!--i class="fa fa-university" aria-hidden="true"></i-->
                                                        <img id="imgRFCEnOperacion" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" id="tbNoCte" name="tbNoCte" class="form-control col-md-2" placeholder="" readonly/>
                                                    </td>                                                    
                                                    <td>
                                                        <input type="text" id="txtRFC" name="RFC" 
                                                        class="form-control col-md-2" placeholder="RFC" 
                                                        onblur="comprobarRFC('ModalNuevoProspecto', $(this).val())" />
                                                    </td>                                                    
                                                    <td style="display:none;" id="icnRFCComprobado">
                                                        <i class="fa fa-check fa-2x" aria-hidden="true" style="color: Green;" ></i>
                                                    </td>
                                                    <td style="display:none;" id="icnRFCExistente">
                                                        <i class="fa fa-times fa-2x" aria-hidden="true" style="color: Red;" ></i>
                                                    </td>
                                                    <td>
                                                        <button type="button" id="btnRFCCrearNuevo" class="btn btn-primary" style="display:none;" >Crear nuevo con el mismo RFC</button>
                                                    </td>
                                                    <td>
                                                        <button type="button" id="btnBusquedaDeCatalogo" class="btn btn-primary" >Buscar Cliente en el Catálogo</button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                            <table id="tbRFCMultiples" style="display:none; width:100%; border: 1px solid #C0C0C0; padding:5px; margin-top:3px;">
                                                <tr>
                                                    <td><label>Registros encontrados</label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <select id="cmbRFCs" name="cmbRFCs" style="width: 100%; overflow:hidden!important; text-overflow:ellipsis;!important" >
                                                            <option></option>
                                                        </select>
                                                        <p><i>(Seleccionar uno existente no permite modificaciones)</i></p>                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>                                                        
                                                        <button type="button" id="btnRFCCancelar" class="btn btn-default btn-xs">Cancelar selección</button>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--<input type="text" id="txtRFC" name="RFC" class="form-control col-md-2" placeholder="RFC" data-inputmask="'mask' : 'aaa[a]999999aa9', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " required/>-->
                                        </div>
                                    </div>
                                    <div class="row" id="lblMensajeRFC">
                                        <div class="col-md-12" style="display:none;" >
                                            <label style="color:#ec7a08;">
                                                <!--i class="fa fa-exclamation-triangle fa-1"></i-->
                                                Advertencia: este RFC se encuentra registrado actualmente en el sistema
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row" id="lblRFCVacio">
                                        <div class="col-md-12" style="display:none;" >
                                            <label style="color:#ec7a08;">
                                                <!--i class="fa fa-exclamation-triangle fa-1"></i-->
                                                Advertencia: RFC vacío. Aunque no es requerido este dato, se aconseja actualizarlo con posterioridad.
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group ui-front">
                                    <label for="txtNombre">Nombre de la Empresa</label>&nbsp
                                        <i class="fa fa-industry" aria-hidden="true"></i>
                                        <img id="imgNombreEmpresaEnOperacion" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                    <input type="text" id="txtNombre" name="txtNombre" class="form-control" placeholder="Nombre de la empresa" />
                                    <label id="lblMensajeNombreEmpresa" style="display:none; color:#ec7a08;" >
                                    <!--i class="fa fa-exclamation-triangle fa-1"></i-->
                                    Advertencia: este nombre de empresa se encuentra registrado actualmente en el sistema
                                    </label>
                                </div>
                                <div class="form-group" id="dvContacto">
                                    <label for="txtContacto">Nombre del Contacto&nbsp</label>
                                    <!--i class="fa fa-book" aria-hidden="true"></i-->
                                    <input type="text" id="txtContacto" name="txtContacto" class="form-control" placeholder="Nombre del Contacto" />
                                </div>
                                <div class="form-group" id="dvEmail">
                                    <label for="txtEmail">Correo electrónico&nbsp</label>
                                    <i class="fa fa-envelope" aria-hidden="true"></i>
                                    <input type="text" id="txtEmail" name="txtEmail" class="form-control" placeholder="Email" 
                                    data-inputmask="'alias': 'email', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " />
                                </div>
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs" role="tablist" id="navDireccion">
                                    <li role="presentation" class="active"><a href="#dvDireccionModalNuevoProspecto" aria-controls="dvDireccionModalNuevoProspecto"
                                        role="tab" data-toggle="tab">Dirección Física</a></li>
                                    <%--<li role="presentation"><a href="#dvTerritorios" aria-controls="dvTerritorios"
                                        role="tab" data-toggle="tab">Territorios</a></li>--%>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content" id="tabDireccion">
                                    <div role="tabpanel" class="tab-pane active" id="dvDireccionModalNuevoProspecto">
                                        <div class="form-group">
                                            <label for="txtCalle">Calle</label>&nbsp<i class="fa fa-road" aria-hidden="true"></i>
                                            <div class="input-group">
                                                <input type="text" id="txtCalle" name="txtCalle" class="form-control" placeholder="Nombre de la calle" />
                                                <span class="input-group-addon">
                                                    <a href="#" data-toggle="modal" data-target="#dvModalMapaGoogle" id="btnMapaUbicacionCalleProspecto" >
                                                    <i class="fa fa-map-marker" aria-hidden="true"></i></a>
                                                </span>
                                                <input type="hidden" id="hdnUbicacionCalleProspectoLat" />
                                                <input type="hidden" id="hdnUbicacionCalleProspectoLng" />
                                            </div>
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
                                            <label for="selTerritorios"> Territorios
                                            <img id="imgProgreso" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                            </label>
                                            <select id="selTerritorios" onchange="selTerritorio$on_change(this)" class="selectpicker form-control" name="Territorios" multiple>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div role="tabpanel" class="tab-pane" id="dvMenu_Contactos">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <a href="#" data-toggle="modal" data-target="#ModalNuevoContacto">
                                                                    <i class="fa fa-user-plus fa-2x" aria-hidden="true"></i>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Acciones
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
<%--
                            <div role="tabpanel" class="tab-pane" id="Menu_Territorios">

                                <div class="row MARGIN_BOTTOM5">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading"><h5><strong>Asociar Territorio</strong></h5></div>
                                            <div class="panel-body">

                                                <div class="row MARGIN_BOTTOM5">                                                        
                                                <div class="col-md-2">
                                                    <label>Territorio</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <select class="selectpicker form-control" id="ModalNuevoProspecto_selTerritorios">                                                                                                            
                                                    </select>    
                                                </div>
                                            </div>

                                            <div class="row MARGIN_BOTTOM5">
                                                <div class="col-md-2">
                                                    <label>VPO</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <input type="text" id="Text1" class="form-control" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />
                                                </div>
                                            </div>

                                            <div class="row MARGIN_BOTTOM5">                                                        
                                                <div class="col-md-12">
                                                    <button class="btn btn-primary pull-right" type="button" onclick="btnNuevoProspecto_AsociarTerritorio(this)" id="btnNuevoProspecto_Asociar">
                                                        <i class="fa fa-plus"></i>&nbsp;Asociar
                                                    </button>
                                                </div>
                                                <div class="col-md-5"></div>
                                            </div>
                                                        
                                            </div>
                                        </div>                                                                                                
                                    </div>
                                </div>
                                        
                                <div class="row MARGIN_BOTTOM5">
                                    <div class="col-md-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                        <h4><strong>Territorios Asociados</strong></h4>
                                        </div>
                                        <div class="panel-body">

                                            <table id="Table1" class="table table-striped table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Clave</th>
                                                    <th>Nombre de Territorio</th>
                                                    <th style="text-align: center;">VPO</th>
                                                    <th style="text-align: center;">Contactos</th>
                                                    <th style="text-align: center;">Editar</th>
                                                    <th style="text-align: center;">Retirar</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                            </table>

                                        </div>
                                    </div>                                            
                                    </div>
                                </div>
                                
                            </div>
--%>
                        </div>
                        <hr />
                    
                    </form>
                </div>
                <div class="modal-footer">
                 
                    <div>
                        <img id="imgSpinnerGuardar" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></a>
                    </div>

                    <button type="button" 
                        id="btnProspectoEditar_Resetera" 
                        class="btn btn-default"                         
                        onclick="Resetear_Busqueda()">Resetear búsqueda
                    </button>
                    
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCerrar">
                    Cerrar
                    </button>                    
                    
                    <button type="button" class="btn btn-primary" id="btnDvModalNuevoProspectoGuardar" 
                    onclick="crearProspecto(this)">
                    Guardar
                    </button>                    
                    
                    <button type="button" class="btn btn-primary" id="btnCancelarCrearProyectoDeProspectoExistente" 
                    onclick="cancelarCrearProyectoDeProspectoExistente('dvModalNuevoProspecto')" style="display: none;">
                    Cancelar
                    </button>
                    
                    <button type="button" class="btn btn-primary" id="btnCrearProyectoDeProspectoExistente" 
                    onclick="crearProyectoDeProspectoExistente('dvModalNuevoProspecto')" style="display: none;">
                    Crear Proyecto
                    </button>
                            
                </div>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div class="modal fade" id="modalBuscarCliente" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img id="img3" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H11">Búsqueda de Cliente</h4>
                </div>
                <div class="modal-body" style="height:500px;">
                    
                    <table>
                        <tr>    
                            <td>
                                <table>
                                <tr>
                                    <td colspan="4">
                                        <label>Nombre o RFC (Texto aproximado minimo 3 caracteres)</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="text" class="form-control valid" id="tbBuscarCliente_Texto" value="" placeholder="Escribe aquí.." style="width:100%"/>
                                    </td>                                        
                                    <td>
                                        <button type="button" id="btnBuscarCliente_Buscar"class="btn btn-primary">Buscar</button>
                                    </td>
                                    <td id="spinnerBuscar" style="display:none;">
                                        <img src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                    </td>
                                    <td id="trBuscarCliente_TextoError" style="display:none;">
                                        <label style="color:Red;">*Se requiere el texto a buscar</label>
                                    </td>
                                </tr>
                                </table>
                            </td>                            
                        </tr>                        
                    </table>

                    
                    <div style="width:100%; height:400px; overflow-y:auto; margin:5px;">

                    <table id="tbBuscarCliente_Listado" class="table-bordered table-hover" style="width:100%;">
                        <thead>
                        <tr>                        
                            <th style="width:50px;">#</th>
                            <th style="width:130px;">RFC</th>
                            <th>Nombre</th>
                            <th></th>
                        </tr>
                        </thead>
                        <tbody>
                            <tr></tr>
                        </tbody>
                    </table>
                    </div>

                    <table>
                    <tr>
                        <td>
                            <label id="lbBuscarCliente_RegEncontrados" > </label>
                        </td>
                    </tr>
                    </table>

                </div>
                <div class="modal-footer">                    
                    <button type="button" class="btn btn-default" id="btnBuscarCliente_Cancelar">Cancelar y cerrar</button>     
                <%--                                       
                    <button type="button" class="btn btn-primary" id="btnBuscarCliente_Aplicar" onclick="crearProspecto(this)">Aplica</button>                                                                    
                --%>
                </div>
            </div>
        </div>
    </div>
    
    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div class="modal fade" id="dvModalEditarProspecto" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
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
                    <div class="form-group" style="display: none;">
                        <label for="selTerritorioProspecto">
                            Territorio&nbsp<i class="fa fa-flag-checkered" aria-hidden="true"></i></label>
                        <select id="selTerritorioProspecto" name="TerritorioTemporal" class="selectpicker form-control" required>
                            <asp:Repeater runat="server" ID="rptTerritoriosProspectoEditar">
                                <ItemTemplate>
                                    <option value='<%#Eval("Id_Ter") %>'><%#Eval("Ter_Nombre")%></option>
                                </ItemTemplate>
                            </asp:Repeater>
                        </select>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="txtRFC">
                                R.F.C.</label>&nbsp<i class="fa fa-university" aria-hidden="true"></i>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <input type="text" id="txtRFC" name="RFC" class="form-control col-md-2" placeholder="RFC" />
                                <!--<input type="text" id="txtRFC" name="RFC" class="form-control col-md-2" placeholder="RFC" data-inputmask="'mask' : 'aaa[a]999999aa9', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " required />-->
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtNombre">Nombre de la Empresa&nbsp</label>
                        <i class="fa fa-industry" aria-hidden="true"></i>
                        <input type="text" id="txtNombre" name="txtNombre" class="form-control" placeholder="Nombre de la Empresa" required />
                    </div>
                    <div class="form-group">
                        <label for="txtContacto">Nombre del Contacto&nbsp</label>
                        <i class="fa fa-book" aria-hidden="true"></i>
                        <input type="text" id="txtContacto" name="txtContacto" class="form-control" placeholder="Nombre del Contacto" required />
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Correo electrónico&nbsp</label>
                        <i class="fa fa-envelope" aria-hidden="true"></i>
                        <input type="text" id="txtEmail" name="txtEmail" class="form-control" placeholder="Email" data-inputmask="'alias': 'email', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true' " required />
                    </div>
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active">
                            <a href="#dvDireccion" aria-controls="dvDireccion" role="tab" data-toggle="tab">Dirección Física</a>
                        </li>
                        <%--<li role="presentation"><a href="#dvEdicionProspectosTerritorios" aria-controls="dvEdicionProspectosTerritorios"
                            role="tab" data-toggle="tab">Territorios</a></li>--%>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="dvDireccion">
                            <div class="form-group">
                                <label for="txtCalle">Calle&nbsp</label>
                                <i class="fa fa-road" aria-hidden="true"></i>
                                <input type="text" id="txtCalle" name="txtCalle" class="form-control" placeholder="Nombre de la calle" required />
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="txtTelefono">Teléfono&nbsp</label>
                                        <i class="fa fa-phone-square" aria-hidden="true"></i>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <input type="text" id="txtTelefono" name="txtTelefono" class="form-control" placeholder="Teléfono" required />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="dvEdicionProspectosTerritorios">
                            <div class="form-group">
                                <label for="selTerritorios">
                                    Territorios<img id="imgProgreso" style="display: none;"
                                src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                                </label>
                                <select id="selTerritorios" class="selectpicker form-control" name="Territorios" multiple></select>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
                <div class="modal-footer">                    
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" id="btnProspectoEditarGuardar" class="btn btn-primary" onclick="actualizarProspecto()">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div class="modal fade" id="dvModalNuevoProyecto" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
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

                    <div class="form-inline">
                        <div class="form-group">
                            <label>Tipo de venta&nbsp;</label>                            
                            <br/>
                            <div class="radio">
                                <label class="radio-inline">Instalada
                                    <input type="radio" id="rbVtaInstalada" name="tipoVenta" class="form-control" value="1" checked />
                                </label>
                            </div>&nbsp;
                            <div class="radio">
                                <label class="radio-inline">Espor&aacute;dica
                                    <input type="radio" id="rbVtaEsporadica" name="tipoVenta" class="form-control" value="2"/>
                                </label>
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="selTerritorio">Prospecto</label>
                        <%--<input type="text" id="txtNombreProspecto" class="form-control" disabled />--%>
                        <label type="text" id="txtNombreProspecto" class="form-control" />
                    </div>

                    <div class="form-group">
                        <label for="selTerritorio">Territorio&nbsp
                        <i class="fa fa-flag-checkered" aria-hidden="true"></i>
                            <img id="imgProcesandoTerritorioDvModalNuevoProyecto" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        </label>
                        <select id="selTerritorio" onchange="selTerritorio$on_change(this)" name="Territorio" class="selectpicker form-control">
                        </select>
                    </div>

                    <div class="form-group">
                        <label for="selUEN">UEN</label>&nbsp
                        <i class="fa fa-industry" aria-hidden="true"></i>
                        <select id="selUEN" name="Uen" disabled class="selectpicker form-control">
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="selSegmento">Segmento&nbsp
                            <i class="fa fa-tasks" aria-hidden="true"></i>
                            <img id="imgProcesandoSegmentoDvModalNuevoProyecto" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /> 
                         </label>
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
                                <input id="txtPrecioUnidad" type="text" class="form-control" data-inputmask="'alias': 'currency', 'autoUnmask':'true'" placeholder="$0.0" title="Precio por unidad de dimensión" data-toggle="tooltip" disabled/>
                            </div>
                        <div class="col-md-2 tooltip-demo">
                            <small>Cantidad:</small>
                            <input id="txtCantidad" name="Dim_Cantidad" type="text" class="form-control" placeholder="0" title="Cantidad de la unidad elegida" data-toggle="tooltip" data-inputmask="'alias': 'numeric', 'showMaskOnFocus':'false', 'showMaskOnHover':'false', 'autoUnmask':'true', 'allowMinus':'false' " onchange="txtCantidad$onchange(this)"/>
                        </div>
                        <div class="col-md-2 tooltip-demo">
                            <small>VPT:</small>
                            <input id="txtVPM" name="CrmOp_VPM" type="text" class="form-control" disabled placeholder="$0.0" data-inputmask="'alias': 'currency', 'autoUnmask':'true'" title="Venta Promedio Mensual Esperada" data-toggle="tooltip"/>
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
                                        <a data-title="Oferta" href="#!" id="aMapaOferta" data-toggle="modal" data-target="#dvModalMapaOferta"><h6>&nbsp;&nbsp;Mapa de aplicaciones</h6></a>
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
                            <div class="blank-slate-pf" id="dvLstAplicacionesOtrosSlate" style="display:none;">
                                <div class="blank-slate-pf-icon">
                                    <a href="#!" data-toggle="modal" data-target="#dvModalMapaOferta">
                                        <i class="fa fa-map-o" aria-hidden="true"></i>
                                    </a>
                                </div>
                                <h1>
                                    Otras aplicaciones
                                </h1>
                                <p>
                                    Ha optado por asociar aplicaciones que no corresponden al segmento. Presione el &iacute;cono del mapa de aplicaciones o <a href="#!" data-toggle="modal" data-target="#dvModalMapaOferta">aqu&iacute;</a> para elegir las aplicaciones que desea ofertar.
                                </p>
                                
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
                <div id="divErrorEncontado" style="display:block;">                    
                    <div class="alert alert-danger" role="alert">No ha realizado la asociacion de territorios.&nbsp;<button type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button></div>
                    
                </div>
                <div id="divBotonesAccion" style="display:block;">
                    
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnDvModalNuevoProyectoGuardar" onclick="crearProyecto()">Guardar</button>
                    <%--<button type="button" class="btn btn-primary"id="btnGuadarContinuar" onclick="crearProyectoYContinuar()">Guardar y continuar</button>--%>
                </div>                    
                </div>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
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

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="dvModalListaRepresentantes" class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">                    
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <img id="img2" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
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

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div id="dvModalNuevoContacto" style="width: 100%; height: 100%" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaNuevoContacto">
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
                    <form id="frmModalNuevoContacto">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label for="txtNombreContacto">Nombre</label>
                                    <input class="form-control" type="text" id="txtNombreContacto" />
                                </div>

                                <div class="form-group">
                                    <label for="txtPuesto">Puesto</label>
                                    <input class="form-control" type="text" id="txtPuesto" />
                                </div>

                                <div class="form-group">
                                    <label for="txtCumpleanos">Cumpleaños</label>
                                    <input class="form-control" type="date" id="txtCumpleanos" />
                                </div>

                                <div class="form-group">
                                    <label for="txtCorreoElectronico">Correo Electrónico</label>
                                    <input class="form-control" type="text" id="txtCorreoElectronico" />
                                </div>
                                <img id="direccionMap" />
                                <hr />

                                <h2>Direcciones</h2>

                                <div class="form-group">
                                    <label for="txtDireccionNegocio">Negocio</label>
                                    
                                    <div class="input-group">
                                        <input class="form-control" type="text" id="txtDireccionNegocio" />
                                        <span class="input-group-addon"><a href="#" data-toggle="modal" data-target="#dvModalMapaGoogle" id="btnMapaUbicacionNegocio" ><i class="fa fa-map-marker" aria-hidden="true"></i></a></span>
                                        <input type="hidden" id="hdnUbicacionNegocioLat" />
                                        <input type="hidden" id="hdnUbicacionNegocioLng" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtDireccionHogar">Hogar</label>
                                    
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
                                    <label for="txtTelefonoNegocio">Negocio</label>
                                    <input class="form-control" type="text" id="txtTelefonoNegocio" />
                                </div>

                                <div class="form-group">
                                    <label for="txtTelefonoHogar">Hogar</label>
                                    <input class="form-control" type="text" id="txtTelefonoHogar" />
                                </div>

                            </div>
                            <div class="col-md-4">
                                <img class="img-thumbnail" src="<%=Page.ResolveUrl("~/Img/user-placeholder-140x140.png") %>" />
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnCrearNuevoContacto" onclick="crearContacto()">
                        Guardar
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div class="modal fade" id="dvModalMapaGoogle" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H6">
                        Elige una ubicación
                    </h4>
                </div>
                <div class="modal-body">
                    <div id="dvModalMap" style="height: 200px;">
                        
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary"
                        id="btnAceptar" onclick="elegirUbicacion()">
                        Aceptar
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
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

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
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

    <!--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ -->
    <div class="modal fade" id="dvModalMapaOferta" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="H7">
                        Otras aplicaciones
                    </h4>
                </div>
                <div class="modal-body">
                    <div id="dvMapaDeOferta"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Cerrar</button>
                    <button type="button" class="btn btn-primary"
                        id="btnAceptar" onclick="elegirOtrasAplicaciones()">
                        Aceptar
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<%--Modal /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ --%>

<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" runat="server">
    
    <script src="<%=Page.ResolveUrl("~/js/jquery-ui-1.11.4.custom/jquery-ui.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/lodash.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/gridstack/gridstack.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/gridstack/gridstack.jQueryUI.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/dist/min/jquery.inputmask.bundle.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-validation/jquery.validate.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery-validation/localization/messages_es.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>    
    
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/ekko-lightbox.min.js") %>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/jquery.blockUI.min.js") %>"></script>    
    
    <%--<script src="//cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js"></script>--%>
    <%--<script src="//cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js"></script>--%>
    <script src="<%=Page.ResolveUrl("~/Librerias/jquery.blockUI.js") %>"></script>    

    <script src="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/js/alertify.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/dist/css/alertify.css")%>" rel="stylesheet">
        
    <uc:UCAsociacionProspectoTerritorio_js runat="server" ID="ucUCAsociacionProspectoTerritorio_js" />
  
    <script src="<%=Page.ResolveUrl("~/js/CRM2/Gerente.js")%>"></script>

    <script type="text/javascript">
        var _tablaProspectos = null;
        var _territoriosDeRik = [<%=TerritoriosDeRIKComoJson %>];
        var _territoriosDeRik_ParaNuevoProspecto = [<%=TerritoriosDeRIKComoJson %>];
        var _territorioAsociadosAProspecto=[];        
        var _territorioNuevoProspecto=[]; // Territorios del Nuevo Prospecto
        var _EntidadSesion_Id_Rik = <%=EntidadSesion.Id_Rik %>;        
        var _ApplicationUrl = '<%=ApplicationUrl %>';    
        var _EntidadSesion_Id_Emp = <%=EntidadSesion.Id_Emp %>;
        var _EntidadSesion_Id_Cd = <%=EntidadSesion.Id_Cd %>;    
        var _Page_ResolveUrl = "<%=Page.ResolveUrl("/") %>";        

        /*VARIABLE GERENTE */
        var _Parametro_ControlesSoloLectura= 0;    

        _Parametro_IdTU= "<%=Parametro_IdTU %>";    
        _Parametro_IdRik = "<%=Parametro_IdRik %>";    

        _CRM_Usuario_Id= "<%=CRM_Usuario_Id %>";  
        _CRM_Usuario_Rik = "<%=CRM_Usuario_Rik %>";  
        _CRM_Usuario_Nombre = "<%=CRM_Usuario_Nombre %>";  

        _CRM_Gerente_Id = "<%=CRM_Gerente_Id %>";  
        _CRM_Gerente_Rik = "<%=CRM_Gerente_Rik %>";  
        _CRM_Gerente_Nombre = "<%=CRM_Gerente_Nombre %>";          
        
    </script>
        
    <script src="<%=Page.ResolveUrl("~/js/CRM2/Config.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/mapa.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/contacto.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/nota.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/territorio.js")%>"></script>    
    <script src="<%=Page.ResolveUrl("~/js/CRM2/Prospectos2.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/CRM2/Prospectos2_ajax.js")%>"></script>        

    <script type="text/html" id='tplAsociacionTerritorios_x'>

    <div class="panel panel-default" data-id="id">
        <div class="panel-heading">
            <h4>
                Asociar Territorio
            </h4>
        </div>
        <div class="panel-body">
            
                <div class="form-group">
                    <label for="selDetallesAsociarTerritorio_Id_Ter">
                        Territorio
                    </label>
                    <select class="selectpicker form-control" id="selDetallesAsociarTerritorio_Id_Ter">
                                                                
                    </select>
                </div>
                <div class="form-group">
                    <label>
                        VPO
                    </label>
                    <input type="text" id="txtDetallesAsociarTerritorio_Potencial" class="form-control" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" />
                </div>
                <button class="btn btn-primary" type="button" id="btnDetallesAsociarTerritorio_Asociar">Asociar Territorio</button>
            
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>
                Territorios Asociados
            </h4>
        </div>
        <div class="panel-body">
            <table id="tblDetallesTerritoriosAsociados" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>Clave</th>
                        <th>Nombre de Territorio</th>
                        <th style="text-align: center;">VPO</th>
                        <th style="text-align: center;">Contactos</th>
////                      <th style="text-align: center;">Editar</th>
                        <th style="text-align: center;">Retirar</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <!--Modal contenedor de contactos-->
            
            <div id="dvModalContactos" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="hTituloVentanaNuevoContacto">
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

            <!--//Modal contenedor de contactos-->
        </div>
    </div>

</script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCAH5iBFTeLd9n1MvKL0z5RiDJcQVSFNnM&callback=initMap" async defer></script>
    <script src="<%=Page.ResolveUrl("~/js/horizontal_selector.js") %>"></script>
    <%--<script src="<%=Page.ResolveUrl("~/js/bootstrap-treeview.min.js") %>"></script>--%>
    
    <!--script src="//rawgit.com/patternfly/patternfly-bootstrap-treeview/v2.1.0/dist/bootstrap-treeview.min.js"></script-->
    <script src="<%=Page.ResolveUrl("~/Librerias/bootstrap-treeview.min.js") %>"></script>
    
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/dist/min/jquery.inputmask.bundle.min.js") %>"></script>
    <!--<script src="//rawgit.com/jonmiles/bootstrap-treeview/v1.2.0/dist/bootstrap-treeview.min.js"></script>-->
        
    <script type="text/javascript">
        $(document).ready(function () {

            _tablaProspectos.on('draw', function () {
                contador = contador + 1;
                if (contador == 2) {
                    $('#dvLoading').fadeOut(1000);
                    contador = 0;
                }
            });

            Incializa_Gerente(1);
            
        });
    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphToolbar" runat="server">
    
</asp:Content>
