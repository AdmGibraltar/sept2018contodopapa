///<reference path="~/js/ListControl/ListControlView.js"/>

crm.ui.ListControlTabularView = function (options) {
    crm.ui.ListControlView.call(this, options);

    this._$tableNode = $('<table class="table table-striped table-bordered table-hover">');
    this._$tableBody = $('<tbody>');
    this._$tableNode.append(this._$tableBody);
    this._$container.append(this._$tableNode);

    if (typeof (options) != undefined && typeof (options) != 'undefined') {
        if (typeof (options.headerDefinition) != undefined && typeof (options.headerDefinition) != 'undefined') {
            this._headerDefinition = options.headerDefinition;
            this._createHeader();
        } else {
            this._headerDefinition = null;
        }
    }
};

crm.ui.ListControlTabularView.prototype = new crm.ui.ListControlView();

crm.ui.ListControlTabularView.prototype.set_HeaderDefinition = function (headerDefinition) {
    this._headerDefinition = headerDefinition;
};

crm.ui.ListControlTabularView.prototype.get_HeaderDefinition = function () {
    return headerDefinition;
};

crm.ui.ListControlTabularView.prototype.dataBind = function () {
    this._createBody();
};

crm.ui.ListControlTabularView.prototype._createHeader = function () {
    var $tableHeader = $('<thead><tr></tr></thead>');
    var $tableHeaderRow = $tableHeader.find('tr');
    var $node = this._headerDefinition.create($tableHeaderRow);
    this._$tableNode.append($tableHeader);
};

crm.ui.ListControlTabularView.prototype._createBody = function () {
    var _this = this;
    this._$tableBody.empty();
    $.each(this._dataSource, function (index, element) {
        var $tr = $('<tr>');
        var $createdItem = _this._detailView._createItem($tr, element);
        _this._detailView.onItemCreated($tr, element);
        _this._$tableBody.append($tr);
    });
};