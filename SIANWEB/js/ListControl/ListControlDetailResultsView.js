/* File Created: December 8, 2016 */

if (typeof (crm) == undefined || typeof (crm) == 'undefined') {
    function crm() {
    }
}

if (typeof (crm.ui) == undefined || typeof (crm.ui) == 'undefined') {
    crm.ui = function () {
    };
}

crm.ui.ListControlDetailResultsView = function (options) {
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
};

crm.ui.ListControlDetailView.prototype.set_DataSource = function (ds) {
    this._ds = ds;
};

crm.ui.ListControlDetailView.prototype.dataBind = function () {
    var _this = this;
    $.each(this._ds, function (index, element) {
        _this._$container.append(_this._createItem(element));
    });
};