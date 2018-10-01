<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListControlTabularDetailEditView_js.ascx.cs" Inherits="SIANWEB.js.ListControl.ListControlTabularDetailEditView_js" %>
<script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/js/inputmask.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/js/inputmask.numeric.extensions.js") %>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/js/jquery.inputmask-3.x/js/jquery.inputmask.js") %>" type="text/javascript"></script>
<script type="text/html" id="tplListControlTabularDetailEditViewContainer">
    <table class="table">
        
    </table>
</script>
<script type="text/html" id="tplListControlTabularDetailEditViewElement">
    <td data-content="Id_Prd" style="text-align: right">
    </td>
    <td data-content="Prd_Descripcion">
    </td>
    <td data-content="Prd_Precio" data-numeraljs='"format" : "$0,0.00"' style="text-align: center">
    </td>
    <td data-content="Prd_Presentacion" style="text-align: center">
    </td>
    <td>
        <input type="text" id="COP_ConsumoMensual" data-value="COP_ConsumoMensual" data-inputmask="'alias' : 'numeric'" />
    </td>
    <td data-content="ConsumoMensualLitros" id="consumoMensualLts" style="text-align: center">
    </td>
    <td data-content="GastoMensual" id="gastoMensual" data-numeraljs='"format" : "$0,0.00"' style="text-align: center">
    </td>
    <td style="text-align: center">
        <input type="text" style="width: 30%;" id="COP_DilucionAntecedente" data-value="COP_DilucionAntecedente" data-inputmask="'alias' : 'integer'" />
        :
        <input type="text" style="width: 30%;" id="COP_DilucionConsecuente" data-value="COP_DilucionConsecuente" data-inputmask="'alias' : 'integer'" />
    </td>
    <td data-content="ConsumoMensualLtsDiluidos" id="consumoMensualLtsDiluidos" style="text-align: center">
    </td>
    <td>
        <input type="text" id="COP_CostoEnUso" data-value="COP_CostoEnUso" data-inputmask="'alias' : 'currency', 'autoUnmask' : 'true'" disabled />
    </td>
</script>
<script type="text/javascript">
    if (typeof (crm) == undefined || typeof (crm) == 'undefined') {
        function crm() {
        }
    }

    if (typeof (crm.ui) == undefined || typeof (crm.ui) == 'undefined') {
        crm.ui = function () {
        };
    }

    crm.ui.ListControlTabularDetailEditView = function (options) {
        this._$tableNode = $('<table class="table">');
        this._$tableBody = $('<tbody>');
        this._$tableNode.append(this._$tableBody);
        if (typeof (options) != undefined && typeof (options) != 'undefined') {
            if (typeof (options.container) != undefined && typeof (options.container) != 'undefined') {
                this._$container = options.container;
                this._$container.append(this._$tableNode);
            } else {
                this._$container = null;
            }

            if (typeof (options.template) != undefined && typeof (options.template) != 'undefined') {
                this._$template = options.template;
            } else {
                //default
                this._$template = $('#tplListControlTabularDetailEditViewElement');
            }

            //options.headers: mejor establécelos cuando definas una instancia.
            if (typeof (options.headers) != undefined && typeof (options.headers) != 'undefined') {
                this._tableHeaders = options.headers;
                this.buildTableHeader();
            }

            if (typeof (options.dataSource) != undefined && typeof (options.dataSource) != 'undefined') {
                this._ds = options.dataSource;
                this.dataBind();
            } else {
                this._ds = null;
            }
        }
    };

    crm.ui.ListControlTabularDetailEditView.prototype.set_TableHeaders = function (tableHeaders) {
        this._tableHeaders = tableHeaders;
    };

    //somekind of setup
    crm.ui.ListControlTabularDetailEditView.prototype.buildTableHeader = function () {
        var $tableHeader = $('<thead><tr></tr></thead>');
        var $tableHeaderRow = $tableHeader.find('tr');
        $.each(this._tableHeaders, function (index, element) {
            var $headerDefinition = $('<th style="' + (element.style != null ? element.style : '') + '">' + element.title + '</th>');
            $tableHeaderRow.append($headerDefinition);
        });
        this._$tableNode.append($tableHeader);
    };

    crm.ui.ListControlTabularDetailEditView.prototype.set_Container = function (container) {
        this._$container = container;
        this._$container.append(this._$tableNode);
    };

    crm.ui.ListControlTabularDetailEditView.prototype.set_DataSource = function (ds) {
        this._ds = ds;
    };

    crm.ui.ListControlTabularDetailEditView.prototype.dataBind = function () {
        var _this = this;
        this._$tableBody.empty();
        $.each(this._ds, function (index, element) {
            _this._$tableBody.append(_this._createItem(element));
        });
    };

    crm.ui.ListControlTabularDetailEditView.prototype._calcularCostoEnUso = function (dataItem, consumoMensual, dilucionConsecuente) {
        var costoEnUso = 0.0;
        if (consumoMensual != '') {
            if (dataItem.COP_EsQuimico == true) {
                if (dilucionConsecuente != '') {
                    var precio = dataItem.CapValProyectoDet.Vap_Precio; //ProductoActual.Prd_Pesos;
                    var unidadesPresentacion = dataItem.ProductoSerializable.Prd_UniEmp;
                    var consumoMensualEnLitrosDiluidos = ((unidadesPresentacion * consumoMensual) * (parseInt(dilucionConsecuente) + 1));
                    if (consumoMensualEnLitrosDiluidos != 0.0) {
                        costoEnUso = (consumoMensual * precio) / consumoMensualEnLitrosDiluidos;
                    }
                }
            }
        }
        return costoEnUso;
    };

    crm.ui.ListControlTabularDetailEditView.prototype._createItem = function (dataItem) {
        var row = $('<tr>');
        $(row).loadTemplate(this._$template, dataItem);
        var $consumoMensual = $(row).find('#COP_ConsumoMensual');
        var $dc = $(row).find('#COP_DilucionConsecuente');
        var _this = this;
        $dc.blur(function () {
            var consumoMensual = $consumoMensual.val();
            var costoEnUso = _this._calcularCostoEnUso(dataItem, consumoMensual, $dc.val());
            var $costoEnUso = $(row).find('#COP_CostoEnUso');
            $costoEnUso.val(costoEnUso);

            var $consumoMensualLtsDiluidos = $(row).find('#consumoMensualLtsDiluidos');
            var consumoMensualLtsDiluidos = ((dataItem.ProductoSerializable.Prd_UniEmp * consumoMensual) * (parseInt(dataItem.COP_DilucionConsecuente) + 1));
            $consumoMensualLtsDiluidos.text(consumoMensualLtsDiluidos);
            $consumoMensualLtsDiluidos.data('value', consumoMensualLtsDiluidos);
        });
        $consumoMensual.blur(function () {
            //actualizar el campo de costo en uso
            var consumoMensual = $(this).val();
            var $dilucionConsecuente = $(row).find('#COP_DilucionConsecuente');
            var costoEnUso = _this._calcularCostoEnUso(dataItem, consumoMensual, $dilucionConsecuente.val());
            var $costoEnUso = $(row).find('#COP_CostoEnUso');

            var $consumoMensualLts = $(row).find('#consumoMensualLts');

            var consumoMensualLts = dataItem.ProductoSerializable.Prd_UniEmp * consumoMensual;
            $consumoMensualLts.text(consumoMensualLts);
            $consumoMensualLts.data('value', consumoMensualLts);

            var $gastoMensual = $(row).find('#gastoMensual');
            var gastoMensual = numeral(dataItem.CapValProyectoDet.Vap_Precio/*ProductoActual.Prd_Pesos*/ * consumoMensual).format('$0,0.00');
            $gastoMensual.text(gastoMensual);
            $gastoMensual.data('value', gastoMensual);

            var $consumoMensualLtsDiluidos = $(row).find('#consumoMensualLtsDiluidos');
            var consumoMensualLtsDiluidos = ((dataItem.ProductoSerializable.Prd_UniEmp * consumoMensual) * (parseInt(dataItem.COP_DilucionConsecuente) + 1));
            $consumoMensualLtsDiluidos.text(consumoMensualLtsDiluidos);
            $consumoMensualLtsDiluidos.data('value', consumoMensualLtsDiluidos);

            $costoEnUso.val(costoEnUso);
        });
        var costoEnUso = this._calcularCostoEnUso(dataItem, $consumoMensual.val(), $dc.val());
        var $costoEnUso = $(row).find('#COP_CostoEnUso');
        $costoEnUso.val(costoEnUso);

        var $consumoMensualLts = $(row).find('#consumoMensualLts');
        $consumoMensualLts.data('value', dataItem.ConsumoMensualLitros);

        var $gastoMensual = $(row).find('#gastoMensual');
        $gastoMensual.data('value', dataItem.GastoMensual);

        var $consumoMensualLtsDiluidos = $(row).find('#consumoMensualLtsDiluidos');
        $consumoMensualLtsDiluidos.data('value', dataItem.ConsumoMensualLtsDiluidos);

        $(row).data('_obj_', dataItem);
        $(row).numeraljs();
        return row;
    };

    crm.ui.ListControlTabularDetailEditView.prototype.actualizarModelo = function () {
        var $rows = $(this._$tableBody).find('tr');
        $.each($rows, function (index, element) {
            var dataObject = $(element).data('_obj_');

            dataObject.COP_ConsumoMensual = $(element).find('#COP_ConsumoMensual').val();
            dataObject.COP_DilucionAntecedente = $(element).find('#COP_DilucionAntecedente').val();
            dataObject.COP_DilucionConsecuente = $(element).find('#COP_DilucionConsecuente').val();

            dataObject.COP_CostoEnUso = $(element).find('#COP_CostoEnUso').val();
            var dilucionAntecedente = $(element).find('#COP_DilucionAntecedente').val();
            var dilucionConsecuente = $(element).find('#COP_DilucionConsecuente').val();
            dataObject.DilucionCompuesta = dilucionAntecedente + ':' + dilucionConsecuente;

            var consumoMensualLitros = $(element).find('#consumoMensualLts').data('value');
            dataObject.ConsumoMensualLitros = consumoMensualLitros;

            var gastoMensual = $(element).find('#gastoMensual').data('value');
            dataObject.GastoMensual = gastoMensual;

            var consumoMensualLtsDiluidos = $(element).find('#consumoMensualLtsDiluidos').data('value');
            dataObject.ConsumoMensualLtsDiluidos = consumoMensualLtsDiluidos;

            //la fuente ha sido actualizada, puesto que el acceso a los objetos en js son por referencia.
        });
    };
</script>