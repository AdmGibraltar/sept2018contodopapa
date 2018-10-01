<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListControlComparableDetailResultsView_js.ascx.cs" Inherits="SIANWEB.js.ListControl.ListControlComparableDetailResultsView_js" %>
<script type="text/html" id="tplComparableDetailResultsView">
    <div class="col-xs-6 col-sm-6 col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title" data-content="CPT_ProductoActual">
                                        
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12" style="text-align: center;">
                        <img data-src="CPT_RecursoImagenProductoActual" class="img-rounded" style="width: 140px; height: 140px;" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr/>
                        <h4>Situaci&oacute;n Actual</h4>
                        <p data-content="CPT_SituacionActual"></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-xs-6 col-sm-6 col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title" data-content="Prd_Descripcion">
                                        
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12" style="text-align: center;">
                        <img data-src="CPT_RecursoImagenSolucionKey" class="img-rounded" style="width: 140px; height: 140px;" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr/>
                        <h4>Ventajas KEY</h4>
                        <p data-content="CPT_VentajasKey"></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
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

    crm.ui.ListControlComparableDetailResultsView = function (options) {
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

            if (typeof (options.element) != undefined && typeof (options.element) != 'undefined') {
                this._$element = options.element;
            } else {
                //default
                this._$element = $('#tplComparableDetailResultsView');
            }
        }
    };

    crm.ui.ListControlComparableDetailResultsView.prototype.set_Container = function (container) {
        this._$container = container;
    };

    crm.ui.ListControlComparableDetailResultsView.prototype.set_DataSource = function (ds) {
        this._ds = ds;
    };

    crm.ui.ListControlComparableDetailResultsView.prototype.dataBind = function () {
        var _this = this;
        $.each(this._ds, function (index, element) {
            _this._$container.append(_this._createItem(element));
        });
    };

    crm.ui.ListControlComparableDetailResultsView.prototype._createItem = function (dataItem) {
        var row = $('<div class="row">');
        $(row).loadTemplate(this._$element, fuente);
        return row;
    };
</script>