﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infocorp.TITA.SilverlightUI.WSTitaReference {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Runtime.Serialization.DataContractAttribute(Name="DTIssue", Namespace="http://tempuri.org/")]
    public partial class DTIssue : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTAttachment> AttachmentsField;
        
        private System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTField> FieldsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTAttachment> Attachments {
            get {
                return this.AttachmentsField;
            }
            set {
                if ((object.ReferenceEquals(this.AttachmentsField, value) != true)) {
                    this.AttachmentsField = value;
                    this.RaisePropertyChanged("Attachments");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTField> Fields {
            get {
                return this.FieldsField;
            }
            set {
                if ((object.ReferenceEquals(this.FieldsField, value) != true)) {
                    this.FieldsField = value;
                    this.RaisePropertyChanged("Fields");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Runtime.Serialization.DataContractAttribute(Name="DTAttachment", Namespace="http://tempuri.org/")]
    public partial class DTAttachment : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string NameField;
        
        private byte[] DataField;
        
        private string UrlField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public byte[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Url {
            get {
                return this.UrlField;
            }
            set {
                if ((object.ReferenceEquals(this.UrlField, value) != true)) {
                    this.UrlField = value;
                    this.RaisePropertyChanged("Url");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Runtime.Serialization.DataContractAttribute(Name="DTField", Namespace="http://tempuri.org/")]
    public partial class DTField : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string NameField;
        
        private Infocorp.TITA.SilverlightUI.WSTitaReference.Types TypeField;
        
        private bool RequiredField;
        
        private Infocorp.TITA.SilverlightUI.WSTitaReference.ArrayOfString ChoicesField;
        
        private string ValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public Infocorp.TITA.SilverlightUI.WSTitaReference.Types Type {
            get {
                return this.TypeField;
            }
            set {
                if ((this.TypeField.Equals(value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public bool Required {
            get {
                return this.RequiredField;
            }
            set {
                if ((this.RequiredField.Equals(value) != true)) {
                    this.RequiredField = value;
                    this.RaisePropertyChanged("Required");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public Infocorp.TITA.SilverlightUI.WSTitaReference.ArrayOfString Choices {
            get {
                return this.ChoicesField;
            }
            set {
                if ((object.ReferenceEquals(this.ChoicesField, value) != true)) {
                    this.ChoicesField = value;
                    this.RaisePropertyChanged("Choices");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Runtime.Serialization.DataContractAttribute(Name="Types", Namespace="http://tempuri.org/")]
    public enum Types : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Integer = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        String = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Choice = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Boolean = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DateTime = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Note = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        User = 6,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://tempuri.org/", ItemName="string")]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.ServiceModel.ServiceContractAttribute()]
    public interface WSTitaSoap {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/GetIssues", ReplyAction="*")]
        System.IAsyncResult BeginGetIssues(Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequest request, System.AsyncCallback callback, object asyncState);
        
        Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponse EndGetIssues(System.IAsyncResult result);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetIssuesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIssues", Namespace="http://tempuri.org/", Order=0)]
        public Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequestBody Body;
        
        public GetIssuesRequest() {
        }
        
        public GetIssuesRequest(Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetIssuesRequestBody {
        
        public GetIssuesRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetIssuesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIssuesResponse", Namespace="http://tempuri.org/", Order=0)]
        public Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponseBody Body;
        
        public GetIssuesResponse() {
        }
        
        public GetIssuesResponse(Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetIssuesResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue> GetIssuesResult;
        
        public GetIssuesResponseBody() {
        }
        
        public GetIssuesResponseBody(System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue> GetIssuesResult) {
            this.GetIssuesResult = GetIssuesResult;
        }
    }
    
    public interface WSTitaSoapChannel : Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    public partial class GetIssuesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetIssuesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    public partial class WSTitaSoapClient : System.ServiceModel.ClientBase<Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap>, Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap {
        
        private BeginOperationDelegate onBeginGetIssuesDelegate;
        
        private EndOperationDelegate onEndGetIssuesDelegate;
        
        private System.Threading.SendOrPostCallback onGetIssuesCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public WSTitaSoapClient() {
        }
        
        public WSTitaSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSTitaSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSTitaSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSTitaSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<GetIssuesCompletedEventArgs> GetIssuesCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        System.IAsyncResult Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap.BeginGetIssues(Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetIssues(request, callback, asyncState);
        }
        
        private System.IAsyncResult BeginGetIssues(System.AsyncCallback callback, object asyncState) {
            Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequest inValue = new Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequest();
            inValue.Body = new Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequestBody();
            return ((Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap)(this)).BeginGetIssues(inValue, callback, asyncState);
        }
        
        Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponse Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap.EndGetIssues(System.IAsyncResult result) {
            return base.Channel.EndGetIssues(result);
        }
        
        private System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue> EndGetIssues(System.IAsyncResult result) {
            Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponse retVal = ((Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap)(this)).EndGetIssues(result);
            return retVal.Body.GetIssuesResult;
        }
        
        private System.IAsyncResult OnBeginGetIssues(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginGetIssues(callback, asyncState);
        }
        
        private object[] OnEndGetIssues(System.IAsyncResult result) {
            System.Collections.Generic.List<Infocorp.TITA.SilverlightUI.WSTitaReference.DTIssue> retVal = this.EndGetIssues(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetIssuesCompleted(object state) {
            if ((this.GetIssuesCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetIssuesCompleted(this, new GetIssuesCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetIssuesAsync() {
            this.GetIssuesAsync(null);
        }
        
        public void GetIssuesAsync(object userState) {
            if ((this.onBeginGetIssuesDelegate == null)) {
                this.onBeginGetIssuesDelegate = new BeginOperationDelegate(this.OnBeginGetIssues);
            }
            if ((this.onEndGetIssuesDelegate == null)) {
                this.onEndGetIssuesDelegate = new EndOperationDelegate(this.OnEndGetIssues);
            }
            if ((this.onGetIssuesCompletedDelegate == null)) {
                this.onGetIssuesCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetIssuesCompleted);
            }
            base.InvokeAsync(this.onBeginGetIssuesDelegate, null, this.onEndGetIssuesDelegate, this.onGetIssuesCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap CreateChannel() {
            return new WSTitaSoapClientChannel(this);
        }
        
        private class WSTitaSoapClientChannel : ChannelBase<Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap>, Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap {
            
            public WSTitaSoapClientChannel(System.ServiceModel.ClientBase<Infocorp.TITA.SilverlightUI.WSTitaReference.WSTitaSoap> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginGetIssues(Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesRequest request, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = request;
                System.IAsyncResult _result = base.BeginInvoke("GetIssues", _args, callback, asyncState);
                return _result;
            }
            
            public Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponse EndGetIssues(System.IAsyncResult result) {
                object[] _args = new object[0];
                Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponse _result = ((Infocorp.TITA.SilverlightUI.WSTitaReference.GetIssuesResponse)(base.EndInvoke("GetIssues", _args, result)));
                return _result;
            }
        }
    }
}