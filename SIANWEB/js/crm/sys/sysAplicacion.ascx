<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sysAplicacion.ascx.cs" Inherits="SIANWEB.js.crm.sys.sysAplicacion" %>
<script type="text/javascript">
    crm.sys.sysAplicacion = function () {
        this._urlAplicacion = '<%=ApplicationUrl %>';
    };

    crm.sys.sysAplicacion.prototype.get_urlAplicacion = function () {
        return this._urlAplicacion;
    };

    var sysAplicacion = new crm.sys.sysAplicacion();
</script>