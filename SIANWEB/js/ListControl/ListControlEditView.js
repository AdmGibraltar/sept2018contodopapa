/* File Created: December 8, 2016 */

if (typeof (crm) == undefined || typeof (crm) == 'undefined') {
    function crm() {
    }
}

if (typeof (crm.ui) == undefined || typeof (crm.ui) == 'undefined') {
    crm.ui = function () {
    };
}

crm.ui.ListControlEditView = function (options) {
    if (typeof (options) != undefined && typeof (options) != 'undefined') {
        if (typeof (options.detailView) != undefined && typeof (options.detailView) != 'undefined') {
            this._detailView = options.detailView;
        } else {
            this._detailView = null;
        }
    }
};

crm.ui.ListControlEditView.prototype.set_DetailView = function (dv) {
    this._detailView = dv;
};

crm.ui.ListControlEditView.prototype.set_DataSource = function (ds) {
    this._detailView.set_DataSource(ds);
};

crm.ui.ListControlEditView.prototype.dataBind = function () {
    this._detailView.dataBind();
};

crm.ui.ListControlEditView.prototype.bind = function (ds) {
    this._detailView.set_DataSource(ds);
    this._detailView.dataBind();
};