/// <reference path="~/js/ListControl/ListControlDetailView.js" />

crm.ui.ListControlTabularDetailView = function (options) {
    crm.ui.ListControlDetailView.call(this, options);

};

crm.ui.ListControlTabularDetailView.prototype = new crm.ui.ListControlDetailView();

crm.ui.ListControlTabularDetailView.prototype.onItemCreated = function ($createdNode, dataElement) {
    $createdNode.data('_dataObj_', dataElement);
};