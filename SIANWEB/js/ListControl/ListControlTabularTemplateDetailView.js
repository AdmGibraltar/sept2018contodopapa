/// <reference path="~/js/ListControl/ListControlTabularDetailView.js" />

crm.ui.ListControlTabularTemplateDetailView = function ($tpl) {
    crm.ui.ListControlDetailView.call(this);
    this._$tpl = $tpl;
};

crm.ui.ListControlTabularTemplateDetailView.prototype = new crm.ui.ListControlTabularDetailView();

crm.ui.ListControlTabularTemplateDetailView.prototype._createItem = function ($rowElement, dataElement) {
    return $($rowElement).loadTemplate(this._$tpl, dataElement);
};