<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCListControlToolbar_js.ascx.cs" Inherits="SIANWEB.js.ListControl.UCListControlToolbar_js" %>
<script type="text/javascript">
    $.widget('crm.crmlistadovaluaciones', {
        options: {
            idEmp: <%=EntidadSesion.Id_Emp %>,
            idCd: <%=EntidadSesion.Id_Cd %>,
            idRik: <%=EntidadSesion.Id_Rik %>,
            listControlView: null,
            views: null,
            defaultView: null
        },
        _create: function () {
            //Debería ser condicionado y configurable
            this._toolbar=new crm.ui.ListControlToolbar({container: this.element, listControlView: this.options.listControlView, views: this.options.views, defaultView: this.options.defaultView});
        }
    });
</script>