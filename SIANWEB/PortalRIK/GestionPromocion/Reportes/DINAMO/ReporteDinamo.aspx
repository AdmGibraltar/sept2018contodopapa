<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PortalRIK.Master" AutoEventWireup="true" CodeBehind="ReporteDinamo.aspx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Reportes.DINAMO.ReporteDinamo" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/css/icheck/skins/square/blue.css")%>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphToolbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyContent" runat="server">
    <div class="container-fluid">
        <div class="row toolbar-pf">
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="toolbar-pf-actions">
                    <div class="form-group">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <label>Tipo de Centro</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="<%=rbCDI.ClientID %>">
                                        CDI
                                    </label>
                                    <asp:RadioButton runat="server" ID="rbCDI" Checked="true" GroupName="TipoDeCentro" />
                                    <%--<input type="radio" name="tipoCentro" id="tipoCentro[0]" checked />--%>
                                </td>
                                <td>
                                    <label for="<%=rbCDC.ClientID %>">
                                        CDC
                                    </label>
                                    <asp:RadioButton runat="server" ID="rbCDC" GroupName="TipoDeCentro" />
                                    <%--<input type="radio" name="tipoCentro" id="tipoCentro[1]" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="form-group">
                        <table>
                            <tr>
                                <td style="text-align:center;">
                                    <label>Año</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlAnyo" DataTextField="Text" DataValueField="Value" CssClass="selectpicker" OnSelectedIndexChanged="ddlAnyo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%--<select class="selectpicker" data-width="fit">
                                        <asp:Repeater runat="server" ID="rptFiltroAnyos">
                                            <ItemTemplate>
                                                <option value='<%#Eval("Value") %>'>
                                                    <%#Eval("Text") %>
                                                </option>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </select>--%>
                                </td>
                            </tr>
                        </table>
                        
                    </div>

                    <div class="form-group">
                        <table>
                            <tr>
                                <td style="text-align: center;">
                                    <label>
                                        Mes
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlMes" CssClass="selectpicker" DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%--<select class="selectpicker" data-width="fit">
                                        <asp:Repeater runat="server" ID="rptFiltroMesesSP">
                                            <ItemTemplate>
                                                <option value='<%#Eval("Value") %>'>
                                                    <%#Eval("Text") %>
                                                </option>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </select>--%>
                                </td>
                            </tr>
                        </table>
                        
                        
                    </div>

                    <div class="form-group">
                        <table>
                            <tr>
                                <td style="text-align: center;" colspan="3">
                                    <label>
                                        Antigüedad de RIK
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="<%=rbAntiguedadRIKTodos.ClientID %>">
                                        Todos
                                    </label>
                                    <asp:RadioButton runat="server" ID="rbAntiguedadRIKTodos" GroupName="AntiguedadRIK" Checked="true" />
                                    <%--<input type="radio" name="antiguedadRik" id="antiguedadRik[0]" checked />--%>
                                </td>
                                <td>
                                    <label for="<%=rbAntiguedadMenor8Meses.ClientID %>">
                                        Menor a 8 meses
                                    </label>
                                    <asp:RadioButton runat="server" ID="rbAntiguedadMenor8Meses" GroupName="AntiguedadRIK" />
                                    <%--<input type="radio" name="antiguedadRik" id="antiguedadRik[1]" />--%>
                                </td>
                                <td>
                                    <label for="<%=rbAntiguedadRIKMayorIgual8Meses.ClientID %>">
                                        Mayor o igual a 8 meses
                                    </label>
                                    <asp:RadioButton runat="server" ID="rbAntiguedadRIKMayorIgual8Meses" GroupName="AntiguedadRIK" />
                                    <%--<input type="radio" name="antiguedadRik" id="antiguedadRik[2]" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="form-group">
                        <table>
                            <tr>
                                <td style="text-align:center;" colspan="2">
                                    <label>
                                        Otros
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="<%=chkPromedioTresMeses.ClientID %>">
                                        Promedio 3 meses
                                    </label>
                                    <asp:CheckBox runat="server" ID="chkPromedioTresMeses" />
                                    <%--<input type="checkbox" name="promedioTresMeses" id="promedioTresMeses" />--%>
                                </td>
                                <td>
                                    <label for="<%=chkListadoRIKS.ClientID %>">
                                        Listado RIK's
                                    </label>
                                    <asp:CheckBox runat="server" ID="chkListadoRIKS" />
                                    <%--<input type="checkbox" name="listadoRiks" id="listadoRiks" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="toolbar-pf-action-right">
                        <div class="toolbar-pf-view-selector">
                            <button runat="server" ID="btnActualizar" type="button" class="btn btn-link" onserverclick="btnActualizar_Click">
                                <i class="fa fa-refresh fa-2x" aria-hidden="true"></i>
                            </button>
                            <button class="btn btn-link active">
                                <i class="fa fa-file-excel-o fa-2x" aria-hidden="true"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div runat="server" class="row" id="reporteBlankSlate" visible="true">
        <div class="blank-slate-pf">
            <div class="blank-slate-pf-icon">
                <i class="fa fa-book" aria-hidden="true"></i>
            </div>
            <h1>
                Reporte DINAMO
            </h1>
            <p>
                Selecciona las condiciones que deseas considerar y presiona <a href="#!">aquí</a> para actualizar el reporte
            </p>
        </div>
    </div>

    <div runat="server" id="dvContenedorDeReporte" visible="false">
        <table style="width: 100%;" id="tblContenido">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center;">
                                <h3><strong>Key Química, S.A. de C.V.</strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <h3><strong>SIANWEB Central</strong></h3>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <h4><strong>INDICADORES DINAMO | <%=ViewModel.NombreRepresentanteElegido %> <%=ViewModel.MesElegido %> <%=ViewModel.AnyoElegido %></strong></h4>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div runat="server" id="tblReportePorCd" visible="false">
                        <table class="table table-striped table-bordered">
                            <col />
                            <colgroup span="2"></colgroup>
                            <colgroup span="3"></colgroup>
                            <colgroup span="3"></colgroup>
                            <colgroup span="2"></colgroup>
                            <colgroup span="1"></colgroup>
                            <thead>
                                <tr>
                                    <th rowspan="2" style="text-align: center;">
                                        CDI
                                    </th>
                                    <th colspan="2" scope="colgroup" style="text-align: center;">
                                        Proyectos Ingresados en el mes
                                    </th>
                                    <th colspan="3" scope="colgroup" style="text-align: center;">
                                        Proyectos Promocion
                                    </th>
                                    <th colspan="3" scope="colgroup" style="text-align: center;">
                                        Cierre
                                    </th>
                                    <th colspan="2" scope="colgroup" style="text-align: center;">
                                        Cancelados
                                    </th>
                                    <th colspan="1" scope="colgroup" style="text-align: center;">
                                        Entradas
                                    </th>
                                </tr>
                                <tr>
                                    <th scope="col" style="text-align: center;">
                                        Núm. proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Importe proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Monto proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Cumplimiento
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Plantilla
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Monto Cierre
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Cumplimiento
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Plantilla
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Núm. proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Importe proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Frecuencia
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rptReporte">
                                    <ItemTemplate>
                                        <tr>
                                            <td style='<%# Eval("StyleCdNombre") %>'>
                                                <asp:LinkButton runat="server" ID="lnkbtnCDNombre" Text='<%#Eval("Cd_Nombre") %>' Enabled='<%# !(bool)Eval("EsZona") %>' OnClick="lnkbtnCDNombre_Click" CommandArgument='<%#Eval("Id_Cd") %>' >
                                                </asp:LinkButton>
                                            </td>
                                            <td style='<%# Eval("StyleProyectosIngresadosNumProy") %>'>
                                                <%#Eval("ProyectosIngresadosNumeroProyectos")%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleProyectosIngresadosImporteProy") %>'>
                                                <%#Eval("ProyectosIngresadosImporteProyecto")%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleProyectosPromocionMontoProy") %>'>
                                                <%#Eval("ProyectosPromocionMontoProyecto")%>
                                            </td>
                                            <td data-numeraljs='"format" : "0.0%"' style='<%# Eval("StyleProyectosPromocionCumplimientoProy") %>'>
                                                <%#Eval("ProyectosPromocionCumplimiento")%>
                                            </td>
                                            <td style='<%# Eval("StyleProyectosPromocionPlantilla") %>'>
                                                <%--<%#Eval("PlantillaPromocion") %>--%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleCierreMontoCierre") %>'>
                                                <%#Eval("CierreMonto")%>
                                            </td>
                                            <td data-numeraljs='"format" : "0.0%"' style='<%# Eval("StyleCierreCumplimiento") %>'>
                                                <%#Eval("CierreCumplimiento")%>
                                            </td>
                                            <td style='<%# Eval("StyleCierrePlantilla") %>'>
                                                <%--<%#Eval("PlantillaCierre") %>--%>
                                            </td>
                                            <td style='<%# Eval("StyleCanceladosNumProy") %>'>
                                                <%#Eval("CanceladoNumProy")%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleCanceladosImporteProy") %>'>
                                                <%#Eval("CanceladoImporteProy")%>
                                            </td>
                                            <td data-numeraljs='"format" : "0.0%"' style='<%# Eval("StyleEntradasFrecuencia") %>'>
                                                <%#Eval("EntradasFrecuencia")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div runat="server" id="tblReportePorRik" visible="false">
                        <table class="table table-striped table-bordered">
                            <col />
                            <colgroup span="2"></colgroup>
                            <colgroup span="2"></colgroup>
                            <colgroup span="2"></colgroup>
                            <colgroup span="2"></colgroup>
                            <colgroup span="1"></colgroup>
                            <thead>
                                <tr>
                                    <th rowspan="2" style="text-align: center;">
                                        Representante
                                    </th>
                                    <th colspan="2" scope="colgroup" style="text-align: center;">
                                        Proy. Ingresados en el mes
                                    </th>
                                    <th colspan="2" scope="colgroup" style="text-align: center;">
                                        Proyectos promoción
                                    </th>
                                    <th colspan="2" scope="colgroup" style="text-align: center;">
                                        Cierre
                                    </th>
                                    <th colspan="2" scope="colgroup" style="text-align: center;">
                                        Cancelados
                                    </th>
                                    <th colspan="1" scope="colgroup" style="text-align: center;">
                                        Entradas
                                    </th>
                                </tr>
                                <tr>
                                    <th scope="col" style="text-align: center;">
                                        Núm. proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Importe proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Monto proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Cumplimiento
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Monto Cierre
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Cumplimiento
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Núm. proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Importe proy.
                                    </th>
                                    <th scope="col" style="text-align: center;">
                                        Frecuencia
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rptReportePorRIK">
                                    <ItemTemplate>
                                        <tr>
                                            <td style='<%# Eval("StyleCdNombre") %>'>
                                                <asp:LinkButton runat="server" ID="lnkbtnRikNombre" Text='<%#Eval("Rik_Nombre") %>' CommandArgument='<%#Eval("Id_Rik") %>' OnClick="lnkbtnRikNombre_Click" >
                                                </asp:LinkButton>
                                            </td>
                                            <td style='<%# Eval("StyleProyectosIngresadosNumProy") %>'>
                                                <%#Eval("ProyectosIngresadosNumeroProyectos")%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleProyectosIngresadosImporteProy") %>'>
                                                <%#Eval("ProyectosIngresadosImporteProyecto")%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleProyectosPromocionMontoProy") %>'>
                                                <%#Eval("ProyectosPromocionMontoProyecto")%>
                                            </td>
                                            <td data-numeraljs='"format" : "0.0%"' style='<%# Eval("StyleProyectosPromocionCumplimientoProy") %>'>
                                                <%#Eval("ProyectosPromocionCumplimiento")%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleCierreMontoCierre") %>'>
                                                <%#Eval("CierreMonto")%>
                                            </td>
                                            <td data-numeraljs='"format" : "0.0%"' style='<%# Eval("StyleCierreCumplimiento") %>'>
                                                <%#Eval("CierreCumplimiento")%>
                                            </td>
                                            <td style='<%# Eval("StyleCanceladosNumProy") %>'>
                                                <%#Eval("CanceladoNumProy")%>
                                            </td>
                                            <td data-numeraljs='"format" : "$0,0.00"' style='<%# Eval("StyleCanceladosImporteProy") %>'>
                                                <%#Eval("CanceladoImporteProy")%>
                                            </td>
                                            <td data-numeraljs='"format" : "0.0%"' style='<%# Eval("StyleEntradasFrecuencia") %>'>
                                                <%#Eval("EntradasFrecuencia")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div runat="server" id="tblReportePorProyecto" visible="false">
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <table class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center;">
                                                        Proyecto
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Cliente
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Área
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Solución
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Aplicación
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Productos
                                                    </th>
                                                    <th style="text-align: center;">
                                                        VTeórico
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Análisis
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Presentación
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Negociación
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Cierre
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Cancelación
                                                    </th>
                                                    <th style="text-align: center; white-space:nowrap;">
                                                        Monto Proyecto
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Fecha Mod.
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Estatus
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Causa
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Comentarios
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rptReporteProyecto">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("IdProyecto")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("NombreCliente")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Area")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Solucion")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Aplicacion")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Productos")%>
                                                            </td>
                                                            <td data-numeraljs='"format" : "$0,0.00"' style="text-align: right;">
                                                                <%# Eval("VTeorico")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Analisis")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Presentacion")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Negociacion")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Cierre")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("Cancelacion")%>
                                                            </td>
                                                            <td style="white-space:nowrap; text-align: right;" data-numeraljs='"format" : "$0,0.00"' >
                                                                <%# Eval("MontoProyecto")%>
                                                            </td>
                                                            <td style="white-space:nowrap;">
                                                                <%# Eval("FechaModificacion")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Estatus")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Causa")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Comentarios")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                    </div>
                </td>
            </tr>
        </table>
    </div>
    

    <%--<input type="button" onclick="tableToExcel('tblContenido', 'W3C Example Table')" value="Export to Excel" />--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScripts" runat="server">
    
    <script src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>            
    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/min/numeral.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/numeraljs/jquery-numeraljs.js")%>"></script>
    <script type="text/javascript">
        function crmOnReady() {
            $('input[type="radio"]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            $('input[type="checkbox"]').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });

            $('#tblContenido').numeraljs();
        }

        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            };
        })();
    </script>
</asp:Content>
