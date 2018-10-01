/* File Created: December 8, 2016 */

crm.ui.ListControlView = function (options) {
    if (typeof (options) != undefined && typeof (options) != 'undefined') {
        if (typeof (options.detailView) != undefined && typeof (options.detailView) != 'undefined') {
            this._detailView = options.detailView;
        } else {
            this._detailView = null;
        }

        if (typeof (options.container) != undefined && typeof (options.container) != 'undefined') {
            this._$container = options.container;
        } else {
            this._$container = null;
        }

        if (typeof (options.dataSource) != undefined && typeof (options.dataSource) != 'undefined') {
            this._dataSource = options.dataSource;
        } else {
            this._dataSource = null;
        }
    }
};

crm.ui.ListControlView.prototype.set_DetailView = function (dv) {
    this._detailView = dv;
};

crm.ui.ListControlView.prototype.set_DataSource = function (ds) {
    this._dataSource = ds;
};

crm.ui.ListControlView.prototype.dataBind = function () {
    throw 'Not implemented or called from super';
};

crm.ui.ListControlView.prototype.bind = function (ds) {
    this.set_DataSource(ds);
    this.dataBind();
};