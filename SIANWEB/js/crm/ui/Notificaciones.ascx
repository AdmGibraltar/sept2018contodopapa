<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notificaciones.ascx.cs" Inherits="SIANWEB.js.crm.ui.Notificaciones" %>
<script type="text/html" id="tplNotificacion">
    <span id="spanIcono">
    </span>
    <div id="dvContenido">
    </div>
    <a id='hlMarcarMensaje' data-toggle='tooltip' data-placement='bottom' title data-original-title='Marcar como leído'><i id="iIconoLeido" class="fa fa-eye-slash"></i></a>
    <button id="btnEliminar" type="button" class="close">
        <span class="pficon pficon-close">
        </span>
    </button>
</script>
<script type="text/javascript">
    crm.ui.Notificaciones.notificaciones = new crm.ui.Notificaciones({ elemento: $('#ulNotificaciones'), nodoPlantilla: $('#tplNotificacion') });
</script>