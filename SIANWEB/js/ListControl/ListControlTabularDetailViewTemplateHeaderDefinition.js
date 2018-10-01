/// <reference path="~/js/ListControl/ListControlTabularDetailViewHeaderDefinition.js" />

crm.ui.ListControlTabularDetailViewTemplateHeaderDefinition = function ($tpl) {
    crm.ui.ListControlTabularDetailViewHeaderDefinition.call(this);
    this._$tpl = $tpl;
};

crm.ui.ListControlTabularDetailViewTemplateHeaderDefinition.prototype = new crm.ui.ListControlTabularDetailViewHeaderDefinition();

crm.ui.ListControlTabularDetailViewTemplateHeaderDefinition.prototype._create = function ($headerRow) {
    return $($headerRow).loadTemplate(this._$tpl);
};