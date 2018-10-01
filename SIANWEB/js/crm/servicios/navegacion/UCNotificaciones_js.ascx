<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNotificaciones_js.ascx.cs" Inherits="SIANWEB.js.crm.servicios.navegacion.UCNavegacion_js" %>
<script type="text/javascript">

    crm.servicios.navegacion.Notificaciones._eliminarNotificacionRIKUrl = '<%=ApplicationUrl %>' + '/api/CapRIKNotificacion';
    crm.servicios.navegacion.Notificaciones._crearNotificacionRIKProyectoUrl = '<%=ApplicationUrl %>' + '/api/CapRIKNotificacionProyecto';
</script>