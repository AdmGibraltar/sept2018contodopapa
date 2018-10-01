///<reference path="~/js/sianweb/ui/wizard/sianweb.ui.wizard-ns.js" />

sianweb.ui.wizard.WizardStep = function (options) {
    this._options = $.extend({}, options, sianweb.ui.wizard.WizardStep.DEFAULTOPTIONS);
    this._previousStep = this._options.previousStep;
    this._nextStep = this._options.nextStep;
    this._$stepElement = this._options.element;
    this._$stepListGroupElement = this._options.stepListGroupElement;

    this._$stepElement.data('stepInstance', this);
    this._$stepListGroupElement.data('stepInstance', this);

    this._$anchorStepElement = this._$stepElement.find('a');

    this._$spanStepIndex = this._$stepElement.find('#spanStepIndex');
    this._$spanStepTitle = this._$stepElement.find('#spanStepTitle');

    this._$spanStepIndex.text(this._options.index);
    this._$spanStepTitle.texxt(this._options.title);
};

sianweb.ui.wizard.WizardStep.DEFAULTOPTIONS = {
    element: $('<div>'),
    stepListGroupElement: null,
    previousStep: null,
    nextStep: null,
    index: 0,
    title: '',
    onValidate: function () {
        return true;
    }
};

sianweb.ui.wizard.WizardStep.prototype.activate = function () {
    if (this._$stepElement.hasClass('active') == false) {
        this._$stepElement.addClass('active');
    }
};

sianweb.ui.wizard.WizardStep.prototype.deactivate = function () {
    if (this._$stepElement.hasClass('active')==true) {
        this._$stepElement.removeClass('active');
    }
};

//Escribir los manejadores para los elementos de paso y de grupo de listado
sianweb.ui.wizard.WizardStep.prototype.get_stepElement = function () {
    return this._$stepElement;
};

sianweb.ui.wizard.WizardStep.prototype.get_stepListGroupElement = function () {
    return this._$stepListGroupElement;
};

sianweb.ui.wizard.WizardStep.prototype.show = function () {
    this._$stepElement.addClass('active');
    this._$stepListGroupElement.show();
};

sianweb.ui.wizard.WizardStep.prototype.hide = function () {
    this._$stepElement.removeClass('active');
    this._$stepListGroupElement.hide();
};

sianweb.ui.wizard.WizardStep.prototype.get_nextStep = function () {
    return this._nextStep;
};

sianweb.ui.wizard.WizardStep.prototype.set_nextStep = function (step) {
    this._nextStep=step;
};

sianweb.ui.wizard.WizardStep.prototype.get_previousStep = function () {
    return this._previousStep;
};

sianweb.ui.wizard.WizardStep.prototype.set_previousStep = function (step) {
    this._previousStep=step;
};

sianweb.ui.wizard.WizardStep.prototype.is_initialStep = function () {
    return this._previousStep == null;
};

sianweb.ui.wizard.WizardStep.prototype.validate = function () {
    return this._options.onValidate();
};

sianweb.ui.wizard.WizardStep.prototype.is_finalStep = function () {
    return this._nextStep == null;
};

sianweb.ui.wizard.WizardStepGroup = function (options) {
    sianweb.ui.wizard.WizardStep.call(this, options);
};

sianweb.ui.wizard.WizardStepGroup.prototype = new sianweb.ui.wizard.WizardStep();

sianweb.ui.wizard.Wizard = function ($element, options) {
    this._$element = $element;
    this._options = $element.extend({}, options, sianweb.ui.wizard.Wizard.DEFAULTOPTIONS);
    this._currentStep = null;
    this._initialStep = null;
    this._btnNext = $element.find('#btnNext');
    this._btnBack = $element.find('#btnBack');
    this._btnFinalize = $element.find('#btnFinalize');
    this._steps = [];

    this._$stepsIndicatorElement = this._$element.find('#ulSteps');
    this._$listGroupElement = this._$element.find('#ulListGroup');
};

sianweb.ui.wizard.Wizard.prototype.initialize = function () {
    //La implementación asume que el contendor del wizard es un modal de bootstrap
    this._updateLayout();

    $(window).resize($.proxy(sianweb.ui.wizard.Wizard.prototype._updateLayout, this));
};

sianweb.ui.wizard.Wizard.prototype.set_initialStep = function (initialStep) {
    this._initialStep = initialStep;
};

sianweb.ui.wizard.Wizard.prototype.addStep = function (step) {
    this._steps.push(step);
    step.get_stepElement().click($.proxy(sianweb.ui.wizard.Wizard.prototype._stepElementClick, this, step));
    step.get_stepListGroupElement().click($.proxy(sianweb.ui.wizard.Wizard.prototype._stepListGroupElementClick, this, step));
};

sianweb.ui.wizard.Wizard.prototype._stepListGroupElementClick = function (step) {
    if (this._currentStep != step) {
        //validar que se pueda navegar a step en caso de que step se encuentre como un paso posterior a this._currentStep
        //si el paso es un paso antecesor, activar el comando "Next" en caso de que el paso asociado al evento tenga un paso posterior asociado
        //Misma instrucción anterior pero para las condiciones del comando "Atrás".
        this._currentStep.hide();
        this._currentStep.deactivate();
        this._currentStep = step;
        this._currentStep.show();
        this._currentStep.activate();
    }
};

sianweb.ui.wizard.Wizard.prototype._stepElementClick = function (step) {
    if (this._currentStep != step) {
        //validar que se pueda navegar a step en caso de que step se encuentre como un paso posterior a this._currentStep
        //si el paso es un paso antecesor, activar el comando "Next" en caso de que el paso asociado al evento tenga un paso posterior asociado
        //Misma instrucción anterior pero para las condiciones del comando "Atrás".
        this._currentStep.hide();
        this._currentStep.deactivate();
        this._currentStep = step;
        this._currentStep.show();
        this._currentStep.activate();
    }
};

sianweb.ui.wizard.Wizard.prototype.linkSteps = function (origin, destination) {
    origin.set_nextStep(destination);
    destination.set_previousStep(origin);
};

sianweb.ui.wizard.Wizard.prototype.reset = function () {
    if (this._currentStep != null) {
        this._currentStep.hide();
    }
    this._currentStep = this._initialStep;
    this._currentStep.show();
};

sianweb.ui.wizard.Wizard.prototype._btnNextClick = function (sender, e) {
    if (this._currentStep.validate() == true) {
        this._currentStep.hide();

        this._currentStep = this._currentStep.get_nextStep();
        this._currentStep.show();

        this._btnBack.prop('disabled', 'false');
        if (this._currentStep.is_finalStep() == true) {
            this._btnNext.prop('disabled', 'true');
        }
    }
};

sianweb.ui.wizard.Wizard.prototype._btnBackClick = function (sender, e) {
    this._btnNext.prop('disabled', 'false');
    
    this._currentStep.hide();

    this._currentStep = this._currentStep.get_previousStep();
    this._currentStep.show();

    if (this._currentStep.is_initialStep() == true) {
        this._btnBack.prop('disabled', 'true');
    }
};

sianweb.ui.wizard.Wizard.prototype._btnFinalizeClick = function () {
    this._options.onFinalize();
};

sianweb.ui.wizard.Wizard.prototype._updateLayout = function () {
    var top = (this._$element.find('.modal-header').outerHeight() + this._$element.find('.wizard-pf-steps').outerHeight()) + "px",
          bottom = this._$element.find(".modal-footer").outerHeight() + "px",
          sidebarwidth = this._$element.find(' .wizard-pf-sidebar').outerWidth() + "px";
    this._$element.find(".wizard-pf-row").css("top", top);
    this._$element.find(" .wizard-pf-row").css("bottom", bottom);
    this._$element.find(".wizard-pf-main").css("margin-left", sidebarwidth);
};

sianweb.ui.wizard.Wizard.prototype.createStepWithElement = function (options) {
    var $stepElement = $('<li class="wizard-pf-step">');
    $stepElement.loadTemplate('#tplStep', {
        spanStepIndex: options.index,
        spanStepTitle: options.title
    });
    this._$stepsIndicatorElement.append($stepElement);
    options.element = $stepElement;
    var $listGroupStepElement = $('<li class="list-group-item active">');
    $listGroupStepElement.loadTemplate('#tplListGroupStep', {
        spanStepIndex: options.index,
        spanStepTitle: options.title
    });
    var stepObj = new sianweb.ui.wizard.WizardStep(options);
    this.addStep(stepObj);
    return stepObj;
};

sianweb.ui.wizard.Wizard.DEFAULTOPTIONS = {
    onFinalize: function () {
    }
};

