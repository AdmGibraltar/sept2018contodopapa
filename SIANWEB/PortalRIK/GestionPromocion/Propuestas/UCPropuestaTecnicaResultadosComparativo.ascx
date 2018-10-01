<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPropuestaTecnicaResultadosComparativo.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.UCPropuestaTecnicaResultadosComparativo" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaTecnicaResultados_js.ascx" TagPrefix="uc" TagName="UCPropuestaTecnicaResultados_js" %>
<uc:UCPropuestaTecnicaResultados_js runat="server" ID="ucUCPropuestaTecnicaResultados_js" />

<%--<div class="row">
    <div class="col-md-12" style="background-color: #00adef !important; -webkit-print-color-adjust:exact;">
        <img src="../../../Img/fondo_blanco.png" height="100px" vspace="10" />
    </div>
</div>--%>
<%--
<div class="row">
    <div class="col-md-12">
        <h1>Propuesta Técnica</h1>
    </div>
</div>--%>
<div id="dvPropuestaTecnicaResultadosVista">
    <asp:Repeater ID="rptCmpPropuestaTecnica" runat="server" 
        DataSourceID="ObjectDataSource1">
        <ItemTemplate>
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <%#Eval("CPT_ProductoActual")%>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" style="text-align: center;">
                                    <img src='<%# Eval("CPT_RecursoImagenProductoActual")%>' class="img-rounded" style="width: 140px; height: 140px;" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <hr/>
                                    <h4>Situaci&oacute;n Actual</h4>
                                    <p><%#Eval("CPT_SituacionActual")%></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <%# DataBinder.Eval(Container.DataItem, "CatProductoSerializable.Prd_Descripcion") %>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" style="text-align: center;">
                                    <img src='<%# Eval("CPT_RecursoImagenSolucionKey")%>' class="img-rounded" style="width: 140px; height: 140px;" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <hr/>
                                    <h4>Ventajas KEY</h4>
                                    <p><%# Eval("CPT_VentajasKey")%></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<script type="text/javascript">
    function actualizarReporte(fuente){
        //mostrar un efecto para evitar que el usuario perciva el redibujado
        $('#dvPropuestaTecnicaResultadosVista').propuestatecnicaresultado('actualizar', fuente);
    }

    (function ($) {
        $(document).ready(function () {
            //propuestatecnicaresultado
            $('#dvPropuestaTecnicaResultadosVista').propuestatecnicaresultado({initialized: true, idCte: <%= IdCte %>, idVal: <%= IdVal %>, modeloEntrada: crm.PropuestaTecnicaResultados.ModeloComparacionEntradaDetalle._instancia});
        });
    })(jQuery);
</script>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="ObtenerReportePropuestaTecnica" 
    TypeName="CapaNegocios.CN_CrmPropuestaTecnica">
        <SelectParameters>
            <asp:SessionParameter Name="s" SessionField="Custom" Type="Object" />
            <asp:QueryStringParameter Name="idCte" QueryStringField="idCte" Type="Int32" />
            <asp:QueryStringParameter Name="idVal" QueryStringField="idVal" Type="Int32" />
        </SelectParameters>
</asp:ObjectDataSource>
    