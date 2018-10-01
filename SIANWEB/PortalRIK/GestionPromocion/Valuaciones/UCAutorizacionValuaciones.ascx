<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAutorizacionValuaciones.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Valuaciones.UCAutorizacionValuaciones" %>
<div class="table-responsive" style="display: inline !important;">
    <% if (ConteoValuacionesAAutorizar > 0)
       {%>
    <table id="tblValuacionesAutorizacion_<%=this.ClientID %>" style="margin-bottom: 0px" class="table">
        <thead>
            <tr>
                <th style="text-align: center">Proyecto </br>Valuación</th>
                <th style="text-align: center">Rik</th>
                <th style="text-align: center">Cliente</th>
                <th style="text-align: center">Área de</br>Aplicación</th>
                <th style="text-align: center">Utilidad</br>Remanente</th>
                <th style="text-align: center">Valor</br>Presente Neto</th>
                <th style="text-align: center">Motivo</th>
                <th></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rptrValuacionesAAprobar">
                <ItemTemplate>
                    <tr id="trValuacionesAAprobar_<%#Eval("Id_Vap") %>">
                        <td style="text-align: center">
                            <%#Eval("Id_Op") %>-<%#Eval("Id_Vap") %>
                        </td>
                        <td>
                            <%#Eval("Id_Rik") %>&nbsp;<%#Eval("RikNombre") %>
                        </td>
                        <td>
                            <%#Eval("Id_Cte") %>&nbsp;<%#Eval("CteNombre") %>
                        </td>
                        <td style="text-align: center">
                            <%--<%#Eval("AreaAplicacion") %>--%>
                            <%#Eval("AplNombre") %>
                        </td>
                        <td>
                            <span style='<%# (decimal)Eval("Vap_UtilidadRemanente") < 0 ? "background-color:red;" : "background-color:green;" %>; color: White; font-weight: bold;'>
                                <%#Eval("Vap_UtilidadRemanente","{0:c}")%>
                            </span>
                        </td>
                        <td>
                            <span style='<%# (decimal)Eval("Vap_ValorPresenteNeto") < 0 ? "background-color:red;" : "background-color:green;" %>; color: White; font-weight: bold;'>
                                <%#Eval("Vap_ValorPresenteNeto","{0:c}")%>
                            </span>
                        </td>
                        <td style="text-align: center">
                            <%--<a href="#!" data-toggle="popover" style="<%#!(bool)Eval("EsPositiva") ? "" : "display: none" %>" data-html="true" title="" data-content="<%#Eval("MotivoParaAutorizacion") %>" data-placement="top">
                                <i class="fa fa-exclamation-circle fa-2x" aria-hidden="true"></i>
                            </a>--%>
                            <lable>
                            <%#Eval("MotivoParaAutorizacion") %>
                            </lable>    

                        </td>
                        <td style="text-align: center; vertical-align: middle;">
                            <img id="imgAutorizacionValuacionenProgreso_<%#Eval("Id_Vap") %>" style="display: none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                        </td>
                        <td>                                                   
                            <button class="btn btn-primary" onclick="AutorizarPregunta(<%#Eval("Id_Vap") %>);return false;">Autorizar</button>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <% }           
       else
       {%>
       <div class="blank-slate-pf">
        <div class="blank-slate-pf-icon">
            <i class="fa fa-thumbs-o-up" aria-hidden="true"></i>
        </div>
        <h1>Valuaciones a Autorizar</h1>
        <p>
            ¡Felicidades! Te encuentras al corriente en el proceso de autorización de valuaciones. Te invitamos a que continúes con el seguimiento de las valuaciones para el beneficio de la organización.
        </p>
       </div>
    <% }%>
</div>

<!--Modal ver valuacion-->
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

<script type="text/javascript">

    
    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function editarValuacion(idEmp, idCd, idVal, idCte){
        $('#iframeVentanaValuacion').attr('src', '../../../CapValProyectosCRMII.aspx?Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
        $('#dvCuerpoVentanaValuacion').block({message: 'Cargando...'});
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function AutorizarPregunta(idVal) {
        alertify.confirm("Antenci&oacute;n', 'Est&aacute; a punto de autorizar la valuaci&oacute;n:" + idVal + " ¿Est&aacute; seguro de que desea continuar?", function (ev) {
            ev.preventDefault();
            //$('#dvModalCancelarProyecto').modal('show');                

            autorizarValuacion(idVal);

        }, function (ev) {
            ev.preventDefault();
        });
    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    function autorizarValuacion(idVal) {
        //TODO: bloquear el kebab y mostrar la señal de progreso
        
        $('#imgAutorizacionValuacionenProgreso_' + idVal).show();
        $('#kebabActions_' + idVal).attr('disabled', true);

        $.ajax({
            url: '<%=ApplicationUrl %>' + '/api/AutorizarValuacion/?idVal=' + idVal,
            type: 'GET',
            cache: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy(autorizarValuacion, null, idVal);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            //retirar nodo tr de la tabla
            var $row = $('#tblValuacionesAutorizacion_<%=this.ClientID %> #trValuacionesAAprobar_' + idVal);
            $row.addClass('animated fadeOutLeft').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $row.removeClass('animated fadeOutLeft');
                $row.remove();
            });
            PatternflyToast.showSuccess('La valuación ' + idVal + ' ha sido autorizada', 6000);
            $('#trValuacionesAAprobar_' + idVal).remove();
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
            $('#imgAutorizacionValuacionenProgreso_' + idVal).hide();
            $('#kebabActions_' + idVal).attr('disabled', false);
            PatternflyToast.showError('Se presentó una complicación al autorizar la valuación ' + idVal, 6000);
        }).always(function (jqXHR, textStatus, errorThrown) {
            //TODO: desbloquear la tabla
        });

    }

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $(document).ready(function () {
        /*
        $('#dvModalValuacion').on('show.bs.modal', function(event){
            var idCte=$(event.relatedTarget).data('idcte');
            var modo=$(event.relatedTarget).data('modo');
            if(modo==0){
                //bloquearProyecto(idCte/*_clienteDeOportunidad**);
            }else{
                var idVal=$(event.relatedTarget).data('idval');
                editarValuacion(<%=EntidadSesion.Id_Emp %>, <%=EntidadSesion.Id_Cd %>, idVal, idCte);
            }
        });

        $('#iframeVentanaValuacion').on('load', function(){
            $('#dvCuerpoVentanaValuacion').unblock();
        });
        */
    });

</script>