﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCWizard.ascx.cs" Inherits="SIANWEB.js.sianweb.ui.wizard.UCWizard" %>
<div class="modal" tabindex="-1" role="dialog" id="<% = ClientID %>">
  <div class="modal-dialog modal-lg wizard-pf">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close wizard-pf-dismiss" aria-label="Close"><span
          aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="h4WizardTitle">
            <% = Title %>
        </h4>
      </div>
      <div class="modal-body wizard-pf-body clearfix">
        <div class="wizard-pf-steps">
          <ul class="wizard-pf-steps-indicator" id="ulSteps">
            <asp:PlaceHolder runat="server" ID="phSteps">
            </asp:PlaceHolder>
            <li class="wizard-pf-step active" data-tabgroup="1">
              <a><span class="wizard-pf-step-number">1</span><span class="wizard-pf-step-title">First Step</span></a>
            </li>
            <li class="wizard-pf-step" data-tabgroup="2">
              <a><span class="wizard-pf-step-number">2</span><span class="wizard-pf-step-title">Second Step</span></a>
            </li>
            <li class="wizard-pf-step" data-tabgroup="3">
              <a><span class="wizard-pf-step-number">3</span><span class="wizard-pf-step-title">Review</span></a>
            </li>
          </ul>
        </div>

        <div class="wizard-pf-row">
          <div class="wizard-pf-sidebar hidden">
            <ul class="list-group" id="ulListGroup">
              <li class="list-group-item active">
                <a href="#">
                  <span class="wizard-pf-substep-number">1A.</span>
                  <span class="wizard-pf-substep-title">Details</span>
                </a>
              </li>
              <li class="list-group-item">
                <a href="#">
                  <span class="wizard-pf-substep-number">1B.</span>
                  <span class="wizard-pf-substep-title">Settings</span>
                </a>
              </li>
            </ul>
            <ul class="list-group hidden">
              <li class="list-group-item">
                <a href="#">
                  <span class="wizard-pf-substep-number">2A.</span>
                  <span class="wizard-pf-substep-title">Details</span>
                </a>
              </li>
              <li class="list-group-item">
                <a href="#">
                  <span class="wizard-pf-substep-number">2B.</span>
                  <span class="wizard-pf-substep-title">Settings</span>
                </a>
              </li>
            </ul>
            <ul class="list-group hidden">
              <li class="list-group-item">
                <a>
                  <span class="wizard-pf-substep-number">3A.</span>
                  <span class="wizard-pf-substep-title">Summary</span>
                </a>
              </li>
              <li class="list-group-item">
                <a>
                  <span class="wizard-pf-substep-number">3B.</span>
                  <span class="wizard-pf-substep-title">Progress</span>
                </a>
              </li>
            </ul>
          </div> <!-- /.wizard-pf-sidebar -->
          <div class="wizard-pf-main">
            <div class="wizard-pf-loading blank-slate-pf">
              <div class="spinner spinner-lg blank-slate-pf-icon"></div>
              <h3 class="blank-slate-pf-main-action">Loading Wizard</h3>
              <p class="blank-slate-pf-secondary-action">Lorem ipsum dolor sit amet, porta at suspendisse ac, ut wisi
                vivamus, lorem sociosqu eget nunc amet. </p>
            </div>
            <div class="wizard-pf-contents hidden">
              <form class="form-horizontal">
                <!-- replacing id with data-id to pass build errors -->
                <div class="form-group required">
                  <label class="col-sm-2 control-label" for="textInput-markup">Name</label>
                  <div class="col-sm-10">
                    <input type="text" data-id="textInput-markup" class="form-control">
                  </div>
                </div>
                <div class="form-group">
                  <label class="col-sm-2 control-label" for="descriptionInput-markup">Description (Optional)</label>
                  <div class="col-sm-10">
                    <textarea data-id="descriptionInput-markup" class="form-control" rows="2"></textarea>
                  </div>
                </div>
              </form>
            </div>
            <div class="wizard-pf-contents hidden">
              <form class="form-horizontal">
                <div class="form-group required">
                  <label class="col-sm-2 control-label" for="lorem">Lorem ipsum</label>
                  <div class="col-sm-10">
                    <input type="text" id="lorem" class="form-control">
                  </div>
                </div>
                <div class="form-group">
                  <label class="col-sm-2 control-label" for="dolor">Dolor (Optional)</label>
                  <div class="col-sm-10">
                    <textarea id="dolor" class="form-control" rows="2"></textarea>
                  </div>
                </div>
              </form>
            </div>
            <div class="wizard-pf-contents hidden">
              <form class="form-horizontal">
                <div class="form-group required">
                  <label class="col-sm-2 control-label" for="aliquam">Aliquam</label>
                  <div class="col-sm-10">
                    <input type="text" id="aliquam" class="form-control">
                  </div>
                </div>
                <div class="form-group">
                  <label class="col-sm-2 control-label" for="fermentum">Fermentum</label>
                  <div class="col-sm-10">
                    <textarea id="fermentum" class="form-control" rows="2"></textarea>
                  </div>
                </div>
              </form>
            </div>
            <div class="wizard-pf-contents hidden">
              <form class="form-horizontal">
                <div class="form-group required">
                  <label class="col-sm-2 control-label" for="consectetur">Consectetur</label>
                  <div class="col-sm-10">
                    <input type="text" id="consectetur" class="form-control">
                  </div>
                </div>
                <div class="form-group">
                  <label class="col-sm-2 control-label" for="adipiscing">Adipiscing</label>
                  <div class="col-sm-10">
                    <textarea id="adipiscing" class="form-control" rows="2"></textarea>
                  </div>
                </div>
              </form>
            </div>
            <div class="wizard-pf-contents hidden">
              <div class="wizard-pf-review-steps">
                <ul class="list-group">
                  <li class="list-group-item">
                    <a onclick="$(this).toggleClass('collapsed'); $('#reviewStep1').toggleClass('collapse');">First Step</a>
                    <div id="reviewStep1" class="wizard-pf-review-substeps">
                      <ul class="list-group">
                        <li class="list-group-item">
                          <a onclick="$(this).toggleClass('collapsed'); $('#reviewStep1Substep1').toggleClass('collapse');">
                            <span class="wizard-pf-substep-number">1A.</span>
                            <span class="wizard-pf-substep-title">Details</span>
                          </a>
                          <div id="reviewStep1Substep1" class="wizard-pf-review-content">
                            <form class="form">
                              <div class="wizard-pf-review-item">
                                <span class="wizard-pf-review-item-label">Name:</span>
                                <span class="wizard-pf-review-item-value">First Last</span>
                              </div>
                              <div class="wizard-pf-review-item">
                                <span class="wizard-pf-review-item-label">Description:</span>
                                <span class="wizard-pf-review-item-value">This is the description</span>
                              </div>
                            </form>
                          </div>
                        </li>
                        <li class="list-group-item">
                          <a onclick="$(this).toggleClass('collapsed'); $('#reviewStep1Substep2').toggleClass('collapse');">
                            <span class="wizard-pf-substep-number">1B.</span>
                            <span class="wizard-pf-substep-title">Settings</span>
                          </a>
                          <div id="reviewStep1Substep2" class="wizard-pf-review-content">
                            <form class="form">
                              <div class="wizard-pf-review-item">
                                <div class="wizard-pf-review-item-field">Setting A</div>
                                <div class="wizard-pf-review-item-field">Setting B</div>
                              </div>
                            </form>
                          </div>
                        </li>
                      </ul>
                    </div>
                  </li>
                  <li class="list-group-item">
                    <a onclick="$(this).toggleClass('collapsed'); $('#reviewStep2').toggleClass('collapse');">Second Step</a>
                    <div id="reviewStep2" class="wizard-pf-review-substeps">
                      <ul class="list-group">
                        <li class="list-group-item">
                          <a onclick="$(this).toggleClass('collapsed'); $('#reviewStep2Substep1').toggleClass('collapse');">
                            <span class="wizard-pf-substep-number">2A.</span>
                            <span class="wizard-pf-substep-title">Details</span>
                          </a>
                          <div id="reviewStep2Substep1" class="wizard-pf-review-content">
                            <form class="form">
                              <div class="wizard-pf-review-item">
                                <span class="wizard-pf-review-item-label">Name:</span>
                                <span class="wizard-pf-review-item-value">First Last</span>
                              </div>
                              <div class="wizard-pf-review-item">
                                <span class="wizard-pf-review-item-label">Description:</span>
                                <span class="wizard-pf-review-item-value">This is the description</span>
                              </div>
                            </form>
                          </div>
                        </li>
                        <li class="list-group-item">
                          <a onclick="$(this).toggleClass('collapsed'); $('#reviewStep2Substep2').toggleClass('collapse');">
                            <span class="wizard-pf-substep-number">2B.</span>
                            <span class="wizard-pf-substep-title">Settings</span>
                          </a>
                          <div id="reviewStep2Substep2" class="wizard-pf-review-content">
                            <form class="form">
                              <div class="wizard-pf-review-item">
                                <div class="wizard-pf-review-item-field">Setting A</div>
                                <div class="wizard-pf-review-item-field">Setting B</div>
                              </div>
                            </form>
                          </div>
                        </li>
                      </ul>
                    </div>
                  </li>
                </ul>
              </div>
            </div>
            <div class="wizard-pf-contents hidden">
              <div class="wizard-pf-process blank-slate-pf">
                <div class="spinner spinner-lg blank-slate-pf-icon"></div>
                <h3 class="blank-slate-pf-main-action">Deployment in progress</h3>
                <p class="blank-slate-pf-secondary-action">Lorem ipsum dolor sit amet, porta at suspendisse ac, ut wisi
                  vivamus, lorem sociosqu eget nunc amet. </p>
              </div>
              <div class="wizard-pf-complete blank-slate-pf hidden">
                <div class="wizard-pf-success-icon"><span class="glyphicon glyphicon-ok-circle"></span></div>
                <h3 class="blank-slate-pf-main-action">Deployment was successful</h3>
                <p class="blank-slate-pf-secondary-action">Lorem ipsum dolor sit amet, porta at suspendisse ac, ut wisi
                  vivamus, lorem sociosqu eget nunc amet. </p>
                <button type="button" class="btn btn-lg btn-primary">
                  View Deployment
                </button>

              </div>
            </div>
          </div><!-- /.wizard-pf-main -->
        </div>

      </div><!-- /.wizard-pf-body -->

      <div class="modal-footer wizard-pf-footer">
        <button type="button" class="btn btn-default btn-cancel wizard-pf-cancel wizard-pf-dismiss">Cancel</button>
        <button type="button" class="btn btn-default wizard-pf-back disabled">
          <span class="i fa fa-angle-left"></span>
          Back
        </button>
        <button type="button" class="btn btn-primary wizard-pf-next disabled">
          Next
          <span class="i fa fa-angle-right"></span>
        </button>
        <button type="button" class="btn btn-primary hidden wizard-pf-finish">
          Deploy
          <span class="i fa fa-angle-right"></span>
        </button>
        <button type="button" class="btn btn-primary hidden wizard-pf-close wizard-pf-dismiss">Close</button>

      </div><!-- /.wizard-pf-footer -->

    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
