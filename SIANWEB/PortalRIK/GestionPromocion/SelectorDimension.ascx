<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectorDimension.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.SelectorDimension" %>

<asp:Repeater runat="server" ID="rptSelectorDimension" >
    <ItemTemplate>
        <div class="table-responsive">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th class="col-md-4">
                            UEN
                        </th>
                        <th class="col-md-6">
                            <%#Eval("Uen_Descripcion") %>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%--<tr class="active">
                        <td class="col-md-4">
                            UEN
                        </td>
                        <td class="col-md-4">
                            <%#Eval("Uen_Descripcion") %>
                        </td>
                    </tr>--%>
                    <tr class="success">
                        <td class="col-md-4 text-center">
                            SEGMENTOS
                        </td>
                        <td class="col-md-6 text-center">
                            Ponderación de atractividad
                        </td>
                        <td class="col-md-2 text-center">
                            Dimensión
                        </td>
                        <td class="col-md-6 text-center">
                            Valor Std. Mensual
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptSelectorDimension_CuerpoTabla" DataSource='<%# Eval("CatSegmentos") %>'>
                        <ItemTemplate>
                            <tr>
                                <td class="col-md-4">
                                    <%#Eval("Seg_Descripcion") %>
                                </td>
                                <td class="col-md-6">
                                </td>
                                <td class="col-md-2">
                                    <%#Eval("Seg_Unidades") %>
                                </td>
                                <td class="col-md-6">
                                    <%#Eval("Seg_ValUniDim") %>
                                </td>
                                <td class="col-md-1">
                                    <button selectorDimension data-iduen='<%# Eval("Id_Uen") %>' data-idseg='<%# Eval("Id_Seg") %>' data-unidades='<%# Eval("Seg_Unidades") %>' onclick="_selectorDimension$AlSeleccionar(this)">Seleccionar</button>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </ItemTemplate>
    <SeparatorTemplate>
        <br />
    </SeparatorTemplate>
</asp:Repeater>

<script type="text/javascript">
    function _selectorDimension$AlSeleccionar(sender){
        var idUen=$(sender).data('iduen');
        var idSeg=$(sender).data('idseg');
        var unidades=$(sender).data('unidades');
        <%=AlSeleccionar %>(idUen, idSeg, unidades);
    }
</script>