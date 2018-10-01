<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCBootstrapConfirm.ascx.cs" Inherits="SIANWEB.Controles.Cliente.UCBootstrapConfirm" %>
<div class="modal fade" id="<%= this.ClientID%>_dvModalBootstrapConfirm" tabindex="-1" role="dialog" >

    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <img id="imgDvModalConfirmEnProgreso" style="display:none;" src="<%=Page.ResolveUrl("~/Img/patternfly/spinner-xs.gif") %>" />
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="h4Title">
                    [title]
                </h4>
            </div>
            <div class="modal-body">
                <div class="alert alert-warning" id="dvMessageContainer">
                    <span class="pficon pficon-warning-triangle-o" id="spanIcon">
                    </span>
                    <span id="spanMessage">[body]</span>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">
                    Cerrar</button>
                <button type="button" class="btn btn-primary"
                        id="btnOk">
                        Confirmar
                </button>
            </div>
        </div>
    </div>

</div>

<script type="text/javascript">
    crm.ui.BootstrapConfirm = function ($dialogNode, $dialogTitleNode, $dialogMessageNode, $dialogIconNode, $dialogBtnOkNode, $dialogMessageContainerNode, $dialogProgressIconNode) {
        this._dialogTitleNode = $dialogTitleNode;
        this._dialogMessageNode = $dialogMessageNode;
        this._dialogIconNode = $dialogIconNode;
        this._dialogNode = $dialogNode;
        this._dialogBtnOkNode = $dialogBtnOkNode;
        this._dialogMessageContainerNode = $dialogMessageContainerNode;
        this._dialogProgressIconNode = $dialogProgressIconNode;
        this._dismissOnOk = true;
        this._onHidden = null;

        var _this = this;
        $(this._dialogBtnOkNode).click(function () {
            if (_this._dialogBtnOkCallback != null) {
                _this._dialogBtnOkCallback();
            }
            if (this._dismissOnOk == true) {
                $(_this._dialogNode).modal('hide');
            }

        });

        $(this._dialogNode).on('hidden.bs.modal', function (event) {
            if (typeof (_this._onHidden) != undefined && typeof (_this._onHidden) != 'undefined') {
                if (_this._onHidden != null) {
                    _this._onHidden();
                }
            }
        });
    };

    crm.ui.BootstrapConfirm.prototype.showWarning = function (title, message, onOk, onCancel, dismissOnOK) {
        $(this._dialogTitleNode).text(title);
        $(this._dialogMessageNode).text(message);
        this._dialogBtnOkCallback = onOk;
        this._dismissOnOk = true;
        if (typeof (dismissOnOK) != undefined && typeof (dismissOnOK) != 'undefined') {
            this._dismissOnOk = dismissOnOK;
        }
        $(this._dialogIconNode).removeClass('');
        $(this._dialogIconNode).addClass('pficon pficon-warning-triangle-o');
        $(this._dialogMessageContainerNode).removeClass('');
        $(this._dialogMessageContainerNode).addClass('alert alert-warning');
        $(this._dialogNode).modal('show');
    };

    crm.ui.BootstrapConfirm.prototype.showProgress = function () {
        this._dialogProgressIconNode.show();
    };

    crm.ui.BootstrapConfirm.prototype.hideProgress = function () {
        this._dialogProgressIconNode.hide();
    };

    crm.ui.BootstrapConfirm.prototype.hide = function (onHidden) {
        this._onHidden = onHidden;
        $(this._dialogNode).modal('hide');
    }

    var BootstrapConfirm = new crm.ui.BootstrapConfirm($('#<% =this.ClientID%>_dvModalBootstrapConfirm'), $('#<% =this.ClientID%>_dvModalBootstrapConfirm #h4Title'), $('#<% =this.ClientID%>_dvModalBootstrapConfirm #spanMessage'), $('#<% =this.ClientID%>_dvModalBootstrapConfirm #spanIcon'), $('#<% =this.ClientID%>_dvModalBootstrapConfirm #btnOk'), $('#<% =this.ClientID%>_dvModalBootstrapConfirm #dvMessageContainer'), $('#<% =this.ClientID%>_dvModalBootstrapConfirm #imgDvModalConfirmEnProgreso'));

</script>