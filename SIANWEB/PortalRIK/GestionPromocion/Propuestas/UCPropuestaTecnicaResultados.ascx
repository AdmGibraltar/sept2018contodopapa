<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPropuestaTecnicaResultados.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Propuestas.UCPropuestaTecnicaResultados" %>
<%@ Register Src="~/PortalRIK/GestionPromocion/Propuestas/UCPropuestaTecnicaResultados_js.ascx" TagPrefix="uc" TagName="UCPropuestaTecnicaResultados_js" %>
<uc:UCPropuestaTecnicaResultados_js runat="server" ID="ucUCPropuestaTecnicaResultados_js" />
<div class="jumbotron">
    <h1>
        KEY
    </h1>
    <p>
        Propuesta Técnica
    </p>
</div>
<table class="table">
    <thead>
        <tr>
            <th></th>
            <th>Producto Actual</th>
            <th>Situación Actual</th>
            <th></th>
            <th>Solución KEY</th>
            <th>Ventajas KEY</th>
        </tr>
    </thead>
    <tbody id="tbPropuestaTecnicaResultados">
        <asp:Repeater runat="server" ID="rptTblPropuestaTecnica" 
            DataSourceID="ObjectDataSource1">
            <ItemTemplate>
                <tr>
                    <td>
                        <img src='<%#Eval("CPT_RecursoImagenProductoActual")%>' class="img-rounded" style="width: 64px; height: 64px;" />
                    </td>
                    <td style="vertical-align:middle;">
                        <%#Eval("CPT_ProductoActual")%>
                    </td>
                    <td style="vertical-align:middle;">
                        <%#Eval("CPT_SituacionActual")%>
                    </td>
                    <td>
                        <img src='<%#Eval("CPT_RecursoImagenSolucionKey")%>' class="img-rounded" style="width: 64px; height: 64px;" />
                    </td>
                    <td style="vertical-align:middle;">
                        <%# DataBinder.Eval(Container.DataItem, "CatProductoSerializable.Prd_Descripcion") %>
                    </td>
                    <td style="vertical-align:middle;">
                        <%#Eval("CPT_VentajasKey")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
<script type="text/javascript">
    function actualizarReporte(fuente){
        //mostrar un efecto para evitar que el usuario perciva el redibujado
        $('#tbPropuestaTecnicaResultados').propuestatecnicaresultado('actualizar', fuente);
    }

    (function ($) {
        $(document).ready(function () {
            //propuestatecnicaresultado
            $('#tbPropuestaTecnicaResultados').propuestatecnicaresultado({initialized: true, idCte: <%= IdCte %>, idVal: <%= IdVal %>, modeloEntrada: crm.PropuestaTecnicaResultados.ModeloComparacionEntradaDetalle._instancia});
        });
    })(jQuery);
</script>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="ObtenerReportePropuestaTecnica" 
    TypeName="CapaNegocios.CN_CrmPropuestaTecnica">
    <SelectParameters>
        <asp:SessionParameter Name="s" SessionField="CustomSession" Type="Object" />
        <asp:QueryStringParameter Name="idCte" QueryStringField="idCte" Type="Int32" />
        <asp:QueryStringParameter Name="idVal" QueryStringField="idVal" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
