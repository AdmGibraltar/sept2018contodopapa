<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListControlToolbar_js.ascx.cs" Inherits="SIANWEB.js.ListControl.ListControlToolbar_js" %>
<script type="text/html" id="tplListControlToolbar">
    <div class="row toolbar-pf">
        <div class="col-sm-12 col-md-12 col-lg-12">
            <div class="toolbar-pf-actions">
                <div class="form-group toolbar-pf-filter">
                    <div class="input-group" id="toolbarFilter">
                        <div class="input-group-btn" id="ddAttributeBox">
                            <button class="btn btn-default dropdown-toggle" aria-expanded="false" aria-haspopup="true" type="button" data-toggle="dropdown">
                                <span id="dvTitle">[1er. elemento]</span>
                                <span class="caret">
                                </span>
                            </button>
                            <ul class="dropdown-menu">
                                
                            </ul>
                        </div>
                        <input class="form-control" type="text" placeholder="Filter by [elemento seleccionado]..." id="txtFilter" />
                    </div>
                </div>
                <div class="toolbar-pf-action-right">
                    <div class="form-group toolbar-pf-find">
                        <button class="btn btn-link btn-find" type="button">
                            <span class="fa fa-search">
                            </span>
                        </button>
                        <div class="find-pf-dropdown-container">
                            <input class="form-control" type="text" placeholder="Buscar por clave..." />
                            <div class="find-pf-buttons">
                                <span class="find-pf-nums">
                                    1 de 3
                                </span>
                                <button class="btn btn-link" type="button">
                                    <span class="fa fa-angle-up">
                                    </span>
                                </button>
                                <button class="btn btn-link" type="button">
                                    <span class="fa fa-angle-down">
                                    </span>
                                </button>
                                <button class="btn btn-link btn-find-close" type="button">
                                    <span class="pficon pficon-close">
                                    </span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group toolbar-pf-view-selector" id="dvViewSelector">
                            
                    </div>
                </div>
            </div>
            <div class="row toolbar-pf-results">
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <h5 id="hNumberOfResults">[Number of results]</h5>
                    <p>Filtros activos:</p>
                    <ul class="list-inline" id="ulToolbarActiveFilterList">
                    </ul>
                    <p>
                        <a href="#">Limpiar todos los filtros</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</script>
<script type="text/html" id="tplToolbarActiveFilterCommand">
    <span class="label label-info">
        <div id="dvTitleAndValue">
            
        </div>
        <a href="#">
            <span class="pficon pficon-close">
            </span>
        </a>
    </span>
</script>
<script type="text/javascript">
    if (typeof (crm) == undefined || typeof (crm) == 'undefined') {
        function crm() {
        }
    }

    if (typeof (crm.ui) == undefined || typeof (crm.ui) == 'undefined') {
        crm.ui = function () {
        };
    }

    crm.ui.FilterDefinition = function (attributeTitle, expression) {
        this._attributeTitle = attributeTitle;
        this._expression = expression;
    };

    crm.ui.FilterDefinition.prototype.get_AttributeTitle = function () {
        return this._attributeTitle;
    };

    crm.ui.FilterDefinition.prototype.get_Expression = function () {
        return this._expression;
    };

    crm.ui.ToolbarActiveFilter = function (filterDefinition, value) {
        this._filterDefinition = filterDefinition;
        this._value = value;
    };

    crm.ui.ToolbarActiveFilter.prototype.get_FilterDefinition = function () {
        return this._filterDefinition;
    };

    crm.ui.ToolbarActiveFilter.prototype.get_Value = function () {
        return this._value;
    };

    crm.ui.ToolbarActiveFilterCommandView = function (toolbarActiveFilter, toolbar) {
        this._toolbarActiveFilter = toolbarActiveFilter;
        this._toolbar = toolbar;
        this._node = null;
    };

    ///No existe necesidad de regresar el nodo debido a que se crea la vista a partir del contenedor que nos pasan
    crm.ui.ToolbarActiveFilterCommandView.prototype.createView = function (container) {
        $(container).loadTemplate($('#tplToolbarActiveFilterCommand'));
        var $dvtitleAndValue = $(container).find('#dvTitleAndValue');
        $dvtitleAndValue.text(this._toolbarActiveFilter.get_FilterDefinition().get_AttributeTitle() + ':' + this._toolbarActiveFilter.get_Value());
    };

    crm.ui.ToolbarFilterAttributeBox = function ($element, filterDefinitions, toolbarFilter) {
        this._toolbarFilter = toolbarFilter;
        var aItems = $.map(filterDefinitions, function (element, index) {
            return { title: element.get_AttributeTitle(), dataObject: element };
        });
        var $this = this;
        this._dropDownMenuCommand = new crm.ui.bs.DropDownMenuCommand($element, aItems, function (sender, e) {
            $this._toolbarFilter.set_FilterBoxPlaceHolderColumn(e.item.get_Title());
        });

        if (aItems.length > 0) {
            $this._toolbarFilter.set_FilterBoxPlaceHolderColumn(aItems[0].title);
        }
    };

    crm.ui.ToolbarFilterAttributeBox.prototype.get_SelectedAttribute = function () {
        return this._dropDownMenuCommand.get_SelectedItem();
    };

    crm.ui.ToolbarActiveFilterItem = function (filterDefinition, value, toolbarActiveFilterList) {
        this._filterDefinition = filterDefinition;
        this._value = value;
        this._toolbarActiveFilterList = toolbarActiveFilterList;
        this._node = null;
        this._$parent=null;
    };

    crm.ui.ToolbarActiveFilterItem.prototype.get_LiContainer = function () {
        return this._$parent;
    };

    crm.ui.ToolbarActiveFilterItem.prototype.createViewNode = function ($parentNode) {
        var $outerSpan = $('<span class="label label-info">');
        $outerSpan.text(this._filterDefinition.get_AttributeTitle() + ': ' + this._value);
        var $a = $('<a href="#">');
        var $innerSpan = $('<span class="pficon pficon-close">');
        $outerSpan.append($a);
        $a.append($innerSpan);
        $parentNode.append($outerSpan);
        this._$parent=$parentNode;
        var _this = this;
        $innerSpan.click(function () {
            _this._toolbarActiveFilterList.remove(_this);
        });
    };

    crm.ui.ToolbarActiveFilterList = function ($element, toolbarFilter) {
        this._$element = $element;
        this._toolbarFilter = toolbarFilter;
    };

    crm.ui.ToolbarActiveFilterList.prototype.push = function (filterDefinition, value) {
        var $li = $('<li>');
        var item = new crm.ui.ToolbarActiveFilterItem(filterDefinition, value, this);
        item.createViewNode($li);
        this._$element.append($li);
    };

    crm.ui.ToolbarActiveFilterList.prototype.remove = function (toolbarActiveFilterItem) {
        toolbarActiveFilterItem.get_LiContainer().remove();
        //trigger filter conditions on the original data source
        //this._toolbarFilter.filterDataSource();
    };

    crm.ui.ToolbarFilter = function ($element, filterDefinitions, $activefilterListElement) {
        this._filterDefinitions = filterDefinitions;
        this._$element = $element;

        //falta filterbox: txtFilter
        this._$filterTextBox = $($element).find('#txtFilter');

        var $attributeBoxElement = $($element).find('#ddAttributeBox');
        this._attributeBox = new crm.ui.ToolbarFilterAttributeBox($attributeBoxElement, filterDefinitions, this);

        //var $ulToolbarActiveFilterList = $($element).find('#ulToolbarActiveFilterList');
        this._toolbarActiveFilterList = new crm.ui.ToolbarActiveFilterList($activefilterListElement, this);

        this._setupFilterBox();
    };

    crm.ui.ToolbarFilter.prototype._setupFilterBox = function () {
        var _this = this;
        this._$filterTextBox.keyup(function (e) {
            if (e.keyCode == 13) {
                //add a new active filter and then apply its condition to the current result
                var value=_this._$filterTextBox.val();
                var filterDefinition=_this._attributeBox.get_SelectedAttribute().get_DataObject();
                _this._toolbarActiveFilterList.push(filterDefinition, value);
            }
        });
    };

    crm.ui.ToolbarFilter.prototype.set_FilterBoxPlaceHolderColumn = function (columnTitle) {
        this._$filterTextBox.attr('placeholder', 'Filtrar por ' + columnTitle);
    };

    crm.ui.ListControlToolbarDetailView = function (listControlDetailView, strClass, onActivation, listControlView) {
        this._listControlDetailView = listControlDetailView;
        this._strClass = strClass;
        this._onActivation = onActivation;
        this._listControlView = listControlView;
    };

    crm.ui.ListControlToolbarDetailView.prototype.get_ClassIcon = function () {
        return this._strClass;
    };

    crm.ui.ListControlToolbarDetailView.prototype.set_ClassIcon = function (strClass) {
        this._strClass = strClass;
    };

    crm.ui.ListControlToolbarDetailView.prototype.activate=function(){
        this._onActivation();
    };

    crm.ui.ListControlToolbarDetailView.prototype.get_ListControlView = function () {
        return this._listControlView;
    };

    crm.ui.ListControlToolbarDetailView.prototype.set_ListControlView = function (listControlView) {
        this._listControlView = listControlView;
    };

    crm.ui.ListControlToolbarViewCommand = function (toolbarViewSelector, listControlToolbarDetailView) {
        this._toolbarViewSelector = toolbarViewSelector;
        this._$commandNode = $('<buttom class="btn btn-link" id="btn"><i id="iIcon"></i></buttom>');
        var $btn = $(this._$commandNode).find('#btn');
        var $iIcon = $(this._$commandNode).find('#iIcon');
        $iIcon.addClass(listControlToolbarDetailView.get_ClassIcon());
        this._listControlToolbarDetailView = listControlToolbarDetailView;
        var _this = this;
        $btn.click(function () {
            if (_this._toolbarViewSelector._currentActiveCommand != null) {
                _this._toolbarViewSelector._currentActiveCommand.set_Inactive();
            }
            _this._toolbarViewSelector._currentActiveCommand = _this;
            _this.set_Active();
            _this._listControlToolbarDetailView.activate();
            var ds = _this._toolbarViewSelector.get_Toolbar().get_DataSource();
            _this._listControlToolbarDetailView.get_ListControlView().bind(ds);
        });
    };

    crm.ui.ListControlToolbarViewCommand.prototype.get_Node = function () {
        return this._$commandNode;
    };

    //Este método debe llamarse cuando el elemento asociado al comando reciba la notificación de activación.
    crm.ui.ListControlToolbarViewCommand.prototype.set_Active = function () {
        this._$commandNode.addClass('active');
    };

    //Este método debe llamarse cuando el elemento asociado al comando reciba la notificación de desactivación.
    crm.ui.ListControlToolbarViewCommand.prototype.set_Inactive = function () {
        this._$commandNode.removeClass('active');
    };

    crm.ui.ListControlToolbarViewCommand.prototype.get_ToolbarDetailView = function () {
        return this._listControlToolbarDetailView;
    };

    //Debe llevar el control del comando actual activado.
    crm.ui.ToolbarViewSelector = function ($container, listControlToolbarViews, defaultView, toolbar) {
        this._listControlToolbarViews = listControlToolbarViews;
        this._currentActiveCommand = null;
        this._$container = $container;
        this._toolbarViewCommands = [];
        this._toolbar = toolbar;
        var $this = this;

        $.each(listControlToolbarViews, function (index, element) {
            var toolbarViewCommand = $this._addViewNode(element);
            if (element === defaultView) {
                toolbarViewCommand.set_Active();
                $this._currentActiveCommand = toolbarViewCommand;
            }
        });

        if (defaultView == null) {
            this._currentActiveCommand = this._toolbarViewCommands[0];
        }
    };

    crm.ui.ToolbarViewSelector.prototype._addViewNode = function (listControlToolbarDetailView) {
        var toolbarViewCommand = new crm.ui.ListControlToolbarViewCommand(this, listControlToolbarDetailView);
        this._$container.append(toolbarViewCommand.get_Node());
        this._toolbarViewCommands.push(toolbarViewCommand);
        return toolbarViewCommand;
    };

    crm.ui.ToolbarViewSelector.prototype.get_SelectedView = function () {
        return this._currentActiveCommand.get_ToolbarDetailView();
    };

    crm.ui.ToolbarViewSelector.prototype.get_Toolbar = function () {
        return this._toolbar;
    };

    crm.ui.ListControlToolbar = function (options) {
        if (typeof (options) != undefined && typeof (options) != 'undefined') {
            this.options = options;
            if (typeof (options.container) != undefined && typeof (options.container) != 'undefined') {
                var toolbarNode = $('#tplListControlToolbar');
                this._$container = options.container;
                $(this._$container).addClass('container-fluid');
                var $barContainer = this._$container.find('.tb-head');
                this._$barContainer = $barContainer;
                $barContainer.addClass('row toolbar-pf');

                $barContainer.loadTemplate(toolbarNode);
            } else {
                this._$container = null;
            }

            if (typeof (options.listControlView) != undefined && typeof (options.listControlView) != 'undefined') {
                this._listControlView = options.listControlView;
            } else {
                this._listControlView = null;
            }


            this._toolbarViewSelector = null;
            if (typeof (options.views) != undefined && typeof (options.views) != 'undefined') {
                this._views = options.views;
                if (options.views != null) {
                    if (this._$container != null) {
                        var $dvViewSelector = $(this._$container).find('#dvViewSelector');
                        this._toolbarViewSelector = new crm.ui.ToolbarViewSelector($dvViewSelector, options.views, options.defaultView, this);
                    }
                }
            }

            if (typeof (options.defaultView) != undefined && typeof (options.defaultView) != 'undefined') {
                this._defaultView = options.defaultView;
                if (this._listControlView != null) {
                    if (options.defaultView != null) {
                        this._listControlView.set_DetailView(this._defaultView);
                    }
                }
            }

            if (typeof (options.dataSource) != undefined && typeof (options.dataSource) != 'undefined') {
                //Maybe a set_DataSource and then an update()....lets see.
                this.set_DataSource(options.dataSource);
            }

            if (typeof (options.filterDefinitions) != undefined && typeof (options.filterDefinitions) != 'undefined') {
                this._toolbarFilter = new crm.ui.ToolbarFilter(this._$barContainer.find('#toolbarFilter'), options.filterDefinitions, this._$barContainer.find('#ulToolbarActiveFilterList'));
            }
        }
    };

    crm.ui.ListControlToolbar.prototype._addActiveFilter = function (value) {
        //retrieve the filter definition from the associated filter on the selected filter box

    };

    crm.ui.ListControlToolbar.prototype.get_CurrentView = function () {
        return this._toolbarViewSelector.get_SelectedView().get_ListControlView();
    };

    ///Its more like a filter toolbar for a data source
    crm.ui.ListControlToolbar.prototype.set_DataSource = function (ds) {
        this._ds = ds;
        this.set_NumberOfResults(this._ds.length);
    };

    crm.ui.ListControlToolbar.prototype.set_NumberOfResults = function (nResults) {
        this._$container.find('#hNumberOfResults').text(nResults + ' Results');
    };
</script>