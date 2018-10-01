<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPropuestaEconomicaEdicion_js.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.UCPropuestaEconomicaEdicion_js" %>
<%@ Register Src="~/js/ListControl/ListControlTabularDetailEditView_js.ascx" TagPrefix="uc" TagName="ListControlTabularDetailEditView_js" %>
<uc:ListControlTabularDetailEditView_js runat="server" ID="ucListControlTabularDetaileditView_js" />

<script type="text/javascript">
    (function ($) {
        $.widget('crm.edicionpropuestaeconomica', {
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
                    this._detailView=new crm.ui.ListControlTabularDetailEditView({container: this.element, dataSource: this.options.dataSource, headers: [
                        { title: 'Código', style: 'text-align: center' }, 
                        { title: 'Producto', style: 'text-align: center' }, 
                        { title: 'Precio', style: 'text-align: center' }, 
                        { title: 'Presentación', style: 'text-align: center' }, 
                        { title: 'Consumo Mensual(unidades)', style: 'text-align: center' }, 
                        { title: 'Consumo Mensual(L)', style: 'text-align: center'}, 
                        { title: 'Gasto Mensual', style: 'text-align: center'}, 
                        //{ title: '', style: 'text-align: center; width: 30px' },  // TITULO PARA CHECKBOX DE DILUCION SOLUCION
                        { title: 'Dilución', style: 'text-align: center; width: 10%' }, 
                        { title: 'Consumo Mensual(L Diluidos)', style: 'text-align: center' }, 
                        { title: 'Costo en uso', style: 'text-align: center' }
                        ]
                    });
                }
                this._listControlEditView=new crm.ui.ListControlEditView({detailView: this._detailView});
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
            actualizarModelo: function(){
                this._detailView.actualizarModelo(this.element);
            }
        });
    })(jQuery);
</script>