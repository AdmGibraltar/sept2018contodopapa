/// <reference path="~/js/ListControl/ListControlDetailView.js" />

crm.ui.ListControlTabularDetailViewHeaderDefinition = function () {
    this._node = null;
};

crm.ui.ListControlTabularDetailViewHeaderDefinition.prototype.create = function (headerRow) {
    var node = this._create(headerRow);
    this._node = node;
    return node;
};

crm.ui.ListControlTabularDetailViewHeaderDefinition.prototype._create = function (headerRow) {
    throw 'Not implemented or called from super';
};

crm.ui.ListControlTabularDetailViewHeaderDefinition.prototype.get_Node = function () {
    return this._node;
};