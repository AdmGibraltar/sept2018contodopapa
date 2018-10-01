<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNotificaciones.ascx.cs" Inherits="SIANWEB.PortalRIK.Navegacion.Notificaciones.UCNotificaciones" %>
<%@ Register Src="~/js/crm/servicios/navegacion/UCNotificaciones_js.ascx" TagPrefix="uc" TagName="UCNotificaciones_js" %>
<li class="dropdown" id="<% = ClientID %>_menuItemNotificaciones">
    <a href="#" data-toggle="dropdown">
        <i class="fa fa-sticky-note" aria-hidden="true">
        </i>
        Notificaciones <span id="totalNotificaciones" class="<% = TotalNotificacionesNuevas > 0 ? "badge" : string.Empty %> total-notificaciones"><% = TotalNotificacionesNuevas > 0 ? TotalNotificacionesNuevas.ToString() : string.Empty  %></span>
    </a>
    <div class="dropdown-menu infotip bottom-right">
        <div class="arrow">
        </div>
        <ul class="list-group tooltip-demo" id="ulNotificaciones">
            <asp:Repeater runat="server" ID="rptrNotificaciones">
                <ItemTemplate>
                    <li class="list-group-item" id="<%#Eval("IdElemento") %>" data-idnotificacion="<%#Eval("IdNotificacion") %>">
                        <span class="<%#Eval("ClaseIcono") %>">
                        </span>
                        <%#Eval("Contenido")%>
                        <%# (bool)Eval("NotificacionLeida") == true ? "<i class='fa fa-eye' aria-hidden='true'></i>" : "<a id='hlMarcarMensaje' data-toggle='tooltip' data-placement='bottom' title data-original-title='Marcar como leído'><i class='fa fa-eye-slash' aria-hidden='true'></i></a>"%>
                        <button type="button" class="close" onclick="<%#Eval("OperacionEliminar") %>">
                            <span class="pficon pficon-close">
                            </span>
                        </button>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</li>
<script type="text/javascript">
    $(document).ready(function () {
        Notificaciones.inicializar($('#<% = ClientID %>_menuItemNotificaciones'));
//        $('#<% = ClientID %>_menuItemNotificaciones').on('hide.bs.dropdown', function () {
//            var ret = !_esNotificacionFuenteDeComando;
//            _esNotificacionFuenteDeComando = false;
//            return ret;
//        });
    });

    var _esNotificacionFuenteDeComando = false;

    function retirarElemento(sender, menuItemId) {
        _esNotificacionFuenteDeComando = true;
        var $menuItem = $('#' + menuItemId);
        $menuItem.addClass('animated fadeOutLeft').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $menuItem.removeClass('animated fadeOutLeft');
            $menuItem.remove();
        });
    }

    function eliminarNotificacionRIK(id, menuItemId) {
        Notificaciones.eliminarNotificacionRIK(id, function () { }, function () { }, function () { },
        {
            401: function (jqXHR, textStatus, errorThrown) {
                //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(eliminarNotificacionRIK, null, id, menuItemId);
            }
        });
//        _esNotificacionFuenteDeComando = true;
//        crm.servicios.navegacion.Notificacion.eliminarNotificacionRIK(id, function () {
//            retirarElemento(null, menuItemId);
//        },
//            function () {
//                PatternflyToast.showError('Se ha presentado una complicación al eliminar la notificación', 6000);
//            },
//            function () {
//                //always
//            },
//            {
//                401: function (jqXHR, textStatus, errorThrown) {
//                    //self.location='<%=ApplicationUrl %>' + '/login.aspx';
//                    $('#dvDialogoInicioSesion').modal();
//                    _onLoginSuccessful = $.proxy(eliminarNotificacionRIK, null, id, menuItemId);
//                }
//            });
    }
</script>