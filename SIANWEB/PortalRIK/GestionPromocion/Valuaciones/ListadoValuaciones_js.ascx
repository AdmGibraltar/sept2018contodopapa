<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListadoValuaciones_js.ascx.cs" Inherits="SIANWEB.PortalRIK.GestionPromocion.Valuaciones.ListadoValuaciones_js" %>
<script type="text/html" id="tplListControlValuacionesTabularDetailView">
    <td data-content="">
            
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

    if (typeof (crm.ui.listados) == undefined || typeof (crm.ui.listados) == 'undefined') {
        crm.ui.listados = function () {
        };
    }

    if (typeof (crm.ui.listados.valuaciones) == undefined || typeof (crm.ui.listados.valuaciones) == 'undefined') {
        crm.ui.listados.valuaciones = function () {
        };
    }

    if (typeof (crm.ui.listados.valuaciones.ListControlValuacionesTabularDetailView) == undefined || typeof (crm.ui.listados.valuaciones.ListControlValuacionesTabularDetailView) == 'undefined') {
        crm.ui.listados.valuaciones.ListControlValuacionesTabularDetailView = function (options) {
            if (typeof (options) != undefined && typeof (options) != 'undefined') {
                if (typeof (options.container) != undefined && typeof (options.container) != 'undefined') {
                    this._$container = options._$container;
                } else {
                    this._$container = null;
                }

                if (typeof (options.dataSource) != undefined && typeof (options.dataSource) != 'undefined') {
                    this._ds = options.dataSource;
                } else {
                    this._ds = null;
                }
            }

            this._template = $('$tplListControlValuacionesTabularDetailView');
        };

        crm.ui.ListControlValuacionesTabularDetailView.prototype.set_DataSource = function (ds) {
            this._ds = ds;
        };

        crm.ui.ListControlValuacionesTabularDetailView.prototype.dataBind = function () {
            var _this = this;
            $.each(this._ds, function (index, element) {
                _this._$container.append(_this._createItem(element));
            });
        };

        crm.ui.ListControlValuacionesTabularDetailView.prototype._createItem = function (element) {
            var $tr = $('<tr>');
            $tr.loadTemplate(this._template, element);
            return $tr;
        };
    }
</script>