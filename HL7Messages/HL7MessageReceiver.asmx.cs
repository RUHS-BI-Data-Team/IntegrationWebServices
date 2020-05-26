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


            ValidateReturn r = new ValidateReturn();
            r.Validate = Passphrase;
            return r;
        }

         String ProcessADTWarehouseMessage(String HL7Message)
        {
            GeoCodeResult r = new GeoCodeResult();
            GeoCodeAddress a = new GeoCodeAddress();
            r = a.GeoCode(HL7Parser(HL7Message, "PID11.1", 0), HL7Parser(HL7Message, "PID11.3", 0), HL7Parser(HL7Message, "PID11.4", 0), HL7Parser(HL7Message, "PID11.5", 0));
            return HL7Parser(HL7Message, "PID11.1", 0);
        }

        private static String HL7Parser(string HL7Message, string HL7Element, Int16 RepeatLocation = 0)
        {
            HL7ParseAndScub.LightWeightParser parser = new HL7ParseAndScub.LightWeightParser();
            string returnValue = "";
            parser.Message = HL7Message;
            //returnValue = myParser.Message;
            if (parser.FindValue(HL7Element) == true)
            {
                List<string> Values = parser.ParsedValue;
                if (RepeatLocation == 0)
                {
                    foreach (string v in Values)
                    {
                        if (returnValue == "")
                        {
                            returnValue = v;
                        }
                        else
                        {
                            returnValue = returnValue + ", " + v;
                        }
                    }
                }
                else
                {
                    if (RepeatLocation <= Values.Count)
                    {
                        returnValue = Values[RepeatLocation - 1];
                    }
                    else
                    {
                        returnValue = "";
                    }
                }
            }
            else
            {
                returnValue = "";
            }
            return returnValue;
        }
    }
}

