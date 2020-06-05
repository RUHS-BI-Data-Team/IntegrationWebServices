﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebServiceTester.HL7WebServicesTest {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ValidateReturn", Namespace="http://www.RUHealth.org")]
    [System.SerializableAttribute()]
    public partial class ValidateReturn : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValidateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Validate {
            get {
                return this.ValidateField;
            }
            set {
                if ((object.ReferenceEquals(this.ValidateField, value) != true)) {
                    this.ValidateField = value;
                    this.RaisePropertyChanged("Validate");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.RUHealth.org", ConfigurationName="HL7WebServicesTest.InterfaceWebServicesSoap")]
    public interface InterfaceWebServicesSoap {
        
        // CODEGEN: Generating message contract since element name MessageType from namespace http://www.RUHealth.org is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.RUHealth.org/AddHL7MessageToWarehouse", ReplyAction="*")]
        WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponse AddHL7MessageToWarehouse(WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.RUHealth.org/AddHL7MessageToWarehouse", ReplyAction="*")]
        System.Threading.Tasks.Task<WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponse> AddHL7MessageToWarehouseAsync(WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddHL7MessageToWarehouseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddHL7MessageToWarehouse", Namespace="http://www.RUHealth.org", Order=0)]
        public WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequestBody Body;
        
        public AddHL7MessageToWarehouseRequest() {
        }
        
        public AddHL7MessageToWarehouseRequest(WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.RUHealth.org")]
    public partial class AddHL7MessageToWarehouseRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string MessageType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Passphrase;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string HL7Message;
        
        public AddHL7MessageToWarehouseRequestBody() {
        }
        
        public AddHL7MessageToWarehouseRequestBody(string MessageType, string Passphrase, string HL7Message) {
            this.MessageType = MessageType;
            this.Passphrase = Passphrase;
            this.HL7Message = HL7Message;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddHL7MessageToWarehouseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddHL7MessageToWarehouseResponse", Namespace="http://www.RUHealth.org", Order=0)]
        public WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponseBody Body;
        
        public AddHL7MessageToWarehouseResponse() {
        }
        
        public AddHL7MessageToWarehouseResponse(WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.RUHealth.org")]
    public partial class AddHL7MessageToWarehouseResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public WebServiceTester.HL7WebServicesTest.ValidateReturn AddHL7MessageToWarehouseResult;
        
        public AddHL7MessageToWarehouseResponseBody() {
        }
        
        public AddHL7MessageToWarehouseResponseBody(WebServiceTester.HL7WebServicesTest.ValidateReturn AddHL7MessageToWarehouseResult) {
            this.AddHL7MessageToWarehouseResult = AddHL7MessageToWarehouseResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface InterfaceWebServicesSoapChannel : WebServiceTester.HL7WebServicesTest.InterfaceWebServicesSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InterfaceWebServicesSoapClient : System.ServiceModel.ClientBase<WebServiceTester.HL7WebServicesTest.InterfaceWebServicesSoap>, WebServiceTester.HL7WebServicesTest.InterfaceWebServicesSoap {
        
        public InterfaceWebServicesSoapClient() {
        }
        
        public InterfaceWebServicesSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InterfaceWebServicesSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InterfaceWebServicesSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InterfaceWebServicesSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponse WebServiceTester.HL7WebServicesTest.InterfaceWebServicesSoap.AddHL7MessageToWarehouse(WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest request) {
            return base.Channel.AddHL7MessageToWarehouse(request);
        }
        
        public WebServiceTester.HL7WebServicesTest.ValidateReturn AddHL7MessageToWarehouse(string MessageType, string Passphrase, string HL7Message) {
            WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest inValue = new WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest();
            inValue.Body = new WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequestBody();
            inValue.Body.MessageType = MessageType;
            inValue.Body.Passphrase = Passphrase;
            inValue.Body.HL7Message = HL7Message;
            WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponse retVal = ((WebServiceTester.HL7WebServicesTest.InterfaceWebServicesSoap)(this)).AddHL7MessageToWarehouse(inValue);
            return retVal.Body.AddHL7MessageToWarehouseResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponse> WebServiceTester.HL7WebServicesTest.InterfaceWebServicesSoap.AddHL7MessageToWarehouseAsync(WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest request) {
            return base.Channel.AddHL7MessageToWarehouseAsync(request);
        }
        
        public System.Threading.Tasks.Task<WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseResponse> AddHL7MessageToWarehouseAsync(string MessageType, string Passphrase, string HL7Message) {
            WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest inValue = new WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequest();
            inValue.Body = new WebServiceTester.HL7WebServicesTest.AddHL7MessageToWarehouseRequestBody();
            inValue.Body.MessageType = MessageType;
            inValue.Body.Passphrase = Passphrase;
            inValue.Body.HL7Message = HL7Message;
            return ((WebServiceTester.HL7WebServicesTest.InterfaceWebServicesSoap)(this)).AddHL7MessageToWarehouseAsync(inValue);
        }
    }
}