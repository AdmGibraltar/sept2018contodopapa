<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wizard_js.ascx.cs" Inherits="SIANWEB.js.sianweb.ui.wizard.wizard_js" %>
<script type="text/html" id="tplStep">
    <li class="wizard-pf-step">
        <a><span class="wizard-pf-step-number" id="spanStepIndex">1</span><span class="wizard-pf-step-title" id="spanStepTitle"></span></a>
    </li>
</script>

<script type="text/html" id="tplListGroupStep">
    <a href="#!">
        <span class="wizard-pf-substep-number" id="spanStepIndex"></span>
        <span class="wizard-pf-substep-title" id="spanStepTitle"></span>
    </a>
</script>