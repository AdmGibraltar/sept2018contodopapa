<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPropuestaEconomicaResultados_js.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.UCPropuestaEconomicaResultados_js" %>
<%@ Register Src="~/js/ListControl/ListControlTabularDetailResultsView_js.ascx" TagPrefix="uc" TagName="ListControlTabularDetailResultsView_js" %>
<uc:ListControlTabularDetailResultsView_js runat="server" ID="ucListControlTabularDetailResultsView_js" />
<script type="text/javascript">
    (function ($) {
        $.widget('crm.resultadospropuestaeconomica', {
            options: {
                initialized: false,
                detailView: null,
                dataSource: null,
                idEmp: <%=EntidadSesion.Id_Emp %>,
                idCd: <%=EntidadSesion.Id_Cd %>,
                idRik: <%=EntidadSesion.Id_Rik %>,
                idCte: 0,
                idVal: 0
            },
            _create: function () {
                
                if(this.options.detailView==null){
                    this._detailView=new crm.ui.ListControlTabularDetailResultsView({container: this.element, dataSource: this.options.dataSource, headers: [
                        {title: 'Código'}, 
                        {title: 'Producto'}, 
                        {title: 'Precio'}, 
                        {title: 'Presentación'}, 
                        {title: 'Consumo Mensual(unidades)'}, 
                        {title: 'Consumo Mensual(L)'}, 
                        {title: 'Gasto Mensual'}, 
                        {title: 'Dilución'}, 
                        {title: 'Consumo Mensual(L Diluidos)'}, 
                        {title: 'Costo en uso'}
                        ]
                    });
                }
                this._listControlResultsView=new crm.ui.ListControlResultsView({detailView: this._detailView});
                if (this.options.initialized != true) {
                    this._actualizar();
                }else{
                    this._construirCuerpo();
                }
            },
            _actualizar: function () {
                $.ajax({
                    url: '<%=ApplicationUrl %>' + '/api/CrmPropuestaEconomica/?idCliente=' + this.options.idCte + '&idVal=' + this.options.idVal,
                    type: 'GET',
                    cache: false,
                    statusCode: {
                        401: function (jqXHR, textStatus, errorThrown) {
                            $('#dvDialogoInicioSesion').modal();
                            _onLoginSuccessful = $.proxy(this._actualizar, this);
                        }
                    }
                }).done(function (response, textStatus, jqXHR) {
                    this._detailView.set_DataSource(response);
                    _construirCuerpo();
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
                }).always(function (jqXHR, textStatus, errorThrown) {

                });
            },
            _construirCuerpo: function(){
                //$(this.element).empty();
                this._detailView.dataBind();
            },
            actualizar: function(detalle){
                if(typeof(detalle)==undefined || typeof(detalle)=='undefied'){
                    this._construirCuerpo();
                }else{
                    this._detailView.set_DataSource(detalle);
                    this._construirCuerpo();
                }
                
            }
        });
    })(jQuery);
</script>