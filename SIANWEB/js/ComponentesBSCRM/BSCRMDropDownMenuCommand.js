///<reference path="~/js/ListControl/crm-ns.js"/>
///<reference path="~/js/ListControl/crm.ui-ns.js"/>
///<reference path="~/js/ComponentesBSCRM/crm.ui.bs-ns.js"/>

crm.ui.bs.DropDownMenuCommandItem = function (title, dataObject, dropDownMenuCommand, fnOnItemSelected) {
    var $li = $('<li>');
    $li.data('_dataObj_', this);
    var $a = $('<a href="#">' + title + '</a>');
    var _this = this;
    $a.click(function () {
        fnOnItemSelected(dropDownMenuCommand, { item: _this });
    });
    $li.append($a);
    this._viewNode = $li;
    this._dataObject = dataObject;
    this._title = title;
};

crm.ui.bs.DropDownMenuCommandItem.prototype.get_DataObject=function(){
    return this._dataObject;
};

crm.ui.bs.DropDownMenuCommandItem.prototype.get_ViewNode=function(){
    return this._viewNode;
};

crm.ui.bs.DropDownMenuCommandItem.prototype.get_Title = function () {
    return this._title;
};

crm.ui.bs.DropDownMenuCommand = function ($element, aItems, fnOnItemSelected) {
    this._$ul = $($element).find('ul.dropdown-menu');
    this._$button = $($element).find('button[data-toggle="dropdown"]');
    this._fnOnItemSelected = fnOnItemSelected;
    this._selectedItem = null;
    this._items = [];
    if (typeof (aItems) != undefined && typeof (aItems) != 'undefined') {
        var _this = this;
        $.each(aItems, function (index, element) {
            var item = new crm.ui.bs.DropDownMenuCommandItem(element.title, element.dataObject, _this, function (sender, e) {
                _this._selectedItem = e.item;
                _this._$button.find('#dvTitle').text(e.item.get_Title());
                fnOnItemSelected(sender, e);
            });
            _this._items.push(item);
            _this._$ul.append(item.get_ViewNode());
        });
        if (this._items.length > 0) {
            this._$button.find('#dvTitle').text(aItems[0].title);
            this._selectedItem = this._items[0];
        }
    }
};

crm.ui.bs.DropDownMenuCommand.prototype.get_SelectedItem = function () {
    return this._selectedItem;
};

crm.ui.bs.DropDownMenuCommand.prototype.addElement = function (title, dataObject) {
    var item = new crm.ui.bs.DropDownMenuCommandItem(element.title, element.dataObject);
    this._$ul.append(item.get_ViewNode());
};