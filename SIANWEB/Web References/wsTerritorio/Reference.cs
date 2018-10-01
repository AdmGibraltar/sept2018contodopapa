﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace SIANWEB.wsTerritorio {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Service1Soap", Namespace="http://tempuri.org/")]
    public partial class Service1 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GuardaAutClienteTerritorioOperationCompleted;
        
        private System.Threading.SendOrPostCallback ActualizaAutClienteTerritorioOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service1() {
            this.Url = global::SIANWEB.Properties.Settings.Default.SIANWEB_wsTerritorio_Service1;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GuardaAutClienteTerritorioCompletedEventHandler GuardaAutClienteTerritorioCompleted;
        
        /// <remarks/>
        public event ActualizaAutClienteTerritorioCompletedEventHandler ActualizaAutClienteTerritorioCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GuardaAutClienteTerritorio", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GuardaAutClienteTerritorio(string xmlClienteTerritorio, string xmlClienteTerritorioAnt) {
            object[] results = this.Invoke("GuardaAutClienteTerritorio", new object[] {
                        xmlClienteTerritorio,
                        xmlClienteTerritorioAnt});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GuardaAutClienteTerritorioAsync(string xmlClienteTerritorio, string xmlClienteTerritorioAnt) {
            this.GuardaAutClienteTerritorioAsync(xmlClienteTerritorio, xmlClienteTerritorioAnt, null);
        }
        
        /// <remarks/>
        public void GuardaAutClienteTerritorioAsync(string xmlClienteTerritorio, string xmlClienteTerritorioAnt, object userState) {
            if ((this.GuardaAutClienteTerritorioOperationCompleted == null)) {
                this.GuardaAutClienteTerritorioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGuardaAutClienteTerritorioOperationCompleted);
            }
            this.InvokeAsync("GuardaAutClienteTerritorio", new object[] {
                        xmlClienteTerritorio,
                        xmlClienteTerritorioAnt}, this.GuardaAutClienteTerritorioOperationCompleted, userState);
        }
        
        private void OnGuardaAutClienteTerritorioOperationCompleted(object arg) {
            if ((this.GuardaAutClienteTerritorioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GuardaAutClienteTerritorioCompleted(this, new GuardaAutClienteTerritorioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ActualizaAutClienteTerritorio", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ActualizaAutClienteTerritorio(string xmlClienteTerritorio) {
            this.Invoke("ActualizaAutClienteTerritorio", new object[] {
                        xmlClienteTerritorio});
        }
        
        /// <remarks/>
        public void ActualizaAutClienteTerritorioAsync(string xmlClienteTerritorio) {
            this.ActualizaAutClienteTerritorioAsync(xmlClienteTerritorio, null);
        }
        
        /// <remarks/>
        public void ActualizaAutClienteTerritorioAsync(string xmlClienteTerritorio, object userState) {
            if ((this.ActualizaAutClienteTerritorioOperationCompleted == null)) {
                this.ActualizaAutClienteTerritorioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnActualizaAutClienteTerritorioOperationCompleted);
            }
            this.InvokeAsync("ActualizaAutClienteTerritorio", new object[] {
                        xmlClienteTerritorio}, this.ActualizaAutClienteTerritorioOperationCompleted, userState);
        }
        
        private void OnActualizaAutClienteTerritorioOperationCompleted(object arg) {
            if ((this.ActualizaAutClienteTerritorioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ActualizaAutClienteTerritorioCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void GuardaAutClienteTerritorioCompletedEventHandler(object sender, GuardaAutClienteTerritorioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GuardaAutClienteTerritorioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GuardaAutClienteTerritorioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void ActualizaAutClienteTerritorioCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591