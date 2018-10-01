/// <reference path="~/js/ListControl/ListControlDetailView.js" />

crm.ui.ListControlTabularDetailViewColumnsHeaderDefinition = function () {
    this._node = null;
};

crm.ui.ListControlTabularDetailViewColumnsHeaderDefinition.prototype.build = function () {
    var node = this._buildNode();
    this._node = node;
};

crm.ui.ListControlTabularDetailViewColumnsHeaderDefinition.prototype._buildNode = function () {
    throw 'Not implemented or called from super';
};

crm.ui.ListControlTabularDetailViewColumnsHeaderDefinition.prototype.get_Node = function () {
    return this._node;
};