<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCListadoValuaciones.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Valuaciones.UCListadoValuaciones" %>
<asp:ScriptManagerProxy runat="server" ID="smpCrmListadoValuaciones">
    <Scripts>
        <asp:ScriptReference Path="~/js/ListControl/crm-ns.js" />
        <asp:ScriptReference Path="~/js/ListControl/crm.ui-ns.js" />
        <asp:ScriptReference Path="~/js/ComponentesBSCRM/crm.ui.bs-ns.js" />
        <asp:ScriptReference Path="~/js/ComponentesBSCRM/BSCRMDropDownMenuCommand.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlView.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlTabularView.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlDetailView.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlTabularDetailView.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlTabularTemplateDetailView.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlTabularDetailViewHeaderDefinition.js" />
        <asp:ScriptReference Path="~/js/ListControl/ListControlTabularDetailViewTemplateHeaderDefinition.js" />
    </Scripts>
</asp:ScriptManagerProxy>

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

//            this._tabularListView=new crm.ui.ListControlTabularView({detailView: new crm.ui.ListControlTabularTemplateDetailView($('#tplListadoValuacionesRow')) , dataSource: [], container:$(this.element).find('#tbContent'), headerDefinition: new crm.ui.ListControlTabularDetailViewTemplateHeaderDefinition($('#tplListadoValuacionesTabularHeader'))});
            var filterDefinitions=[
                new crm.ui.FilterDefinition('Clave', function(element, value){
                    return element.Id_Vap==value;
                }),
                new crm.ui.FilterDefinition('Cliente', function(element, value){
                    return element.Vap_NombreCte==value;
                })
            ];
            this._toolbar=new crm.ui.ListControlToolbar({container: this.element, listControlView: /*this.options.listControlView*/null, views: this.options.views, defaultView: this.options.defaultView, filterDefinitions: filterDefinitions});
            this._obtenerValuacionesDeRIK();
        },
        _obtenerValuacionesDeRIK: function(){
            var $this=this;
            $.ajax({
                url: '<%=ApplicationUrl %>' + '/api/ValuacionesRIK',
                type: 'GET',
                cache: false,
                statusCode: {
                    401: function (jqXHR, textStatus, errorThrown) {
                        $('#dvDialogoInicioSesion').modal();
                        _onLoginSuccessful = $.proxy(this._obtenerValuacionesDeRIK, this);
                    }
                }
            }).done(function (response, textStatus, jqXHR) {
                $.each(response, function(index, element){
                    if(element.CatClienteSerializable!=null){
                        element.Vap_NombreCte=element.CatClienteSerializable.Cte_NomComercial;
                    }else{
                        element.Vap_NombreCte='Sin cliente';
                    }

                    element.Vap_FechaCorta=new Date(element.Vap_Fecha).toLocaleDateString();
                });
                $this._toolbar.get_CurrentView().bind(response);
                $this._toolbar.set_DataSource(response);
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
                
            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
                
            });
        }
    });
</script>