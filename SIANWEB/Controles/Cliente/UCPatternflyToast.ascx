<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPatternflyToast.ascx.cs" Inherits="SIANWEB.Controles.Cliente.UCPatternflyToast" %>
<div class="toast-pf toast-pf-max-width toast-pf-top-right alert alert-danger alert-dismissable"
        style="display: none; position: fixed !important;" id="<%= this.ClientID%>_toast">
        <button type="button" class="close" aria-hidden="true" id="btnClose">
            <span class="pficon pficon-close"></span>
        </button>
        <span class="pficon" id="spanMessageType"><img id="imgDvModalConfirmEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" /></span>
        <div id="toastMessage">
            Message
        </div>
    </div>
<script type="text/javascript">
    crm.ui.PatternflyToast = function ($toastNode, $toastMessageNode, $toastCloseCommandNode, $toastSpanMessageType, $toastProgressAnimationNode) {
        this._$toastNode = $toastNode;
        this._$toastMessageNode = $toastMessageNode;
        this._$toastCloseCommandNode = $toastCloseCommandNode;
        this._toastCloseCommandOnClickCallback = null;
        this._$toastSpanMessageType = $toastSpanMessageType;
        this._$toastProgressAnimationNode = $toastProgressAnimationNode;
        var _this = this;
        this._$toastCloseCommandNode.click(function () {
            if (_this._toastCloseCommandOnClickCallback != null) {
                _this._toastCloseCommandOnClickCallback();
            }
            _this._$toastNode.fadeOut();
            if (_this._currentTimeoutId != null) {
                clearTimeout(_this._currentTimeoutId);
            }
        });
        this._currentTimeoutId = null;
    };

    crm.ui.PatternflyToast.prototype.showSuccess = function (message, time) {
        this._$toastSpanMessageType.removeClass('');
        this._$toastSpanMessageType.addClass('pficon pficon-ok');
        this._$toastNode.removeClass('');
        this._$toastNode.addClass('toast-pf toast-pf-max-width toast-pf-top-right alert alert-success alert-dismissable');
        this._$toastMessageNode.text(message);
        this._$toastNode.fadeIn();
        var _this = this;
        this._currentTimeoutId = setTimeout(function () {
            _this._currentTimeoutId = null;
            _this._$toastNode.fadeOut();
        }, time);
    };

    crm.ui.PatternflyToast.prototype.showError = function (message, time) {
        this._$toastSpanMessageType.removeClass('');
        this._$toastSpanMessageType.addClass('pficon pficon-error-circle-o');
        this._$toastNode.removeClass('');
        this._$toastNode.addClass('toast-pf toast-pf-max-width toast-pf-top-right alert alert-danger alert-dismissable');
        this._$toastMessageNode.text(message);
        this._$toastNode.fadeIn();
        var _this = this;
        this._currentTimeoutId = setTimeout(function () {
            _this._currentTimeoutId = null;
            _this._$toastNode.fadeOut();
        }, time);
    };

    crm.ui.PatternflyToast.prototype.showProgress = function (message, time) {
        this._$toastSpanMessageType.removeClass('');
        this._$toastSpanMessageType.addClass('pficon');
        this._$toastNode.removeClass('');
        this._$toastNode.addClass('toast-pf toast-pf-max-width toast-pf-top-right alert alert-info alert-dismissable');
        this._$toastMessageNode.text(message);
        this._$toastProgressAnimationNode.show();
        this._$toastNode.fadeIn();
    };

    crm.ui.PatternflyToast.prototype.showWarning = function (message, time) {
        this._$toastSpanMessageType.removeClass('');
        this._$toastSpanMessageType.addClass('pficon pficon-warning-triangle-o');
        this._$toastNode.removeClass('');
        this._$toastNode.addClass('toast-pf toast-pf-max-width toast-pf-top-right alert alert-warning alert-dismissable');
        this._$toastMessageNode.text(message);
        this._$toastProgressAnimationNode.show();
        this._$toastNode.fadeIn();
    };

    crm.ui.PatternflyToast.prototype.hideProgress = function () {
        this._$toastProgressAnimationNode.hide();
        this._$toastNode.fadeOut();
    };

    var PatternflyToast = new crm.ui.PatternflyToast($('#<%= this.ClientID%>_toast'), $('#<%= this.ClientID%>_toast #toastMessage'), $('#<%= this.ClientID%>_toast #btnClose'), $('#<%= this.ClientID%>_toast #spanMessageType'), $('#<%= this.ClientID%>_toast #imgDvModalConfirmEnProgreso'));
</script>