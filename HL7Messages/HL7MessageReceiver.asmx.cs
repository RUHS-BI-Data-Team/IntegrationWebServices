using GeoCodeAddressUsingRCITWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;

namespace HL7Messages
{
    [WebService(Namespace = "http://www.RUHealth.org", Name = "InterfaceWebServices", Description = "Web Services for OpenLink to send HL7 messages")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class HL7MessageReceiver : System.Web.Services.WebService
    {
        //[WebMethod]
        //[SoapDocumentMethod]
        //public AckNakReturn AddHL7MessageToWarehouseAckNak(//[XmlElement(ElementName  = "AckNack", Namespace = "http://www.RUHealth.org")]
        //string MessageType, String Passphrase, String HL7Message)
        //{
        //    AckNakReturn r = new AckNakReturn();
        //    r.AckNak = r.Nak;
        //    r.NakReason = "Reason will be entered here";
        //    return r;
        //}

        [WebMethod]
        [SoapDocumentMethod]
        public ValidateReturn AddHL7MessageToWarehouse(//[XmlElement(ElementName  = "AckNack", Namespace = "http://www.RUHealth.org")]
        string MessageType, String Passphrase, String HL7Message)
        {

            ADTData d = new ADTData(HL7Message);
            //ProcessADTWarehouseMessage(HL7Message);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(d.GetType());
            System.IO.StringWriter writer = new System.IO.StringWriter();
            x.Serialize(writer, d);

            ValidateReturn r = new ValidateReturn();
            r.Validate = Passphrase;
            return r;
        }

    }
           
}

