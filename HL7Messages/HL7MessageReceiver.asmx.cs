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
    /// <summary>
    /// Summary description for HL7MessageReceiver
    /// </summary>
    [WebService(Namespace = "http://www.RUHealth.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HL7MessageReceiver : System.Web.Services.WebService
    {
        //ReturnDataForInterface rdfi;
        //[return: XmlElement(Namespace = "http://www.RUHealth.org",
        //ElementName = "InterfaceResponse2")]
        //[WebMethod][SoapRpcMethod]
        //public ReturnDataForInterface AddHL7MessageToWarehouse2([XmlElement("MyResponse2", Namespace="http://www.microsoft.com")] string MessageType, String Passphrase, String HL7Message)
        //{
        //    ReturnDataForInterface response = new ReturnDataForInterface();
        //    response.Result = "F";
        //    response.FailureReason = "Reason will be entered here";
        //    return response;
        //}

        [return: XmlElement(Namespace = "http://www.RUHealth.org",
        ElementName = "InterfaceResponse")]
        [WebMethod]
        [SoapDocumentMethod]
        public ReturnDataForInterface AddHL7MessageToWarehouse([XmlElement("Result", Namespace = "http://www.RUHealth.org")]
        //[XmlElement("Success", Namespace = "http://www.RUHealth.org")]
        //[XmlElement("Failure", Namespace = "http://www.RUHealth.org")]
        //[XmlElement("FailureReason", Namespace = "http://www.RUHealth.org")]
        string MessageType, String Passphrase, String HL7Message)
        {
            ReturnDataForInterface response = new ReturnDataForInterface();
            response.Result = "F";
            response.FailureReason = "Reason will be entered here";
            return response;
        }

        [WebMethod]
        public XmlDocument AddHL7MessageToWarehouse_old (String MessageType, String Passphrase, String HL7Message)
        {
            //rdfi = new ReturnDataForInterface();
            //rdfi.Result = rdfi.Failure;
            //XElement ReturnDoc = new XElement();
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //XmlElement elmBody = doc.CreateElement( string.Empty, "body", string.Empty );
            //doc.AppendChild(elmBody);
            //XmlElement elmSuccess = doc.CreateElement(string.Empty, "Success", string.Empty);
            //elmBody.AppendChild(elmSuccess);
            //XmlElement elmFaliure = doc.CreateElement(string.Empty, "Failure", string.Empty);
            //elmBody.AppendChild(elmFaliure);
            

            XmlNode node = doc.AppendChild(doc.CreateElement("Response"));

            node.AppendChild(doc.CreateElement("Success"));
            node.AppendChild(doc.CreateElement("Failure"));
            node.AppendChild(doc.CreateElement("Result"));
            node.AppendChild(doc.CreateElement("FailureReason"));
            node.ChildNodes[0].InnerText = "S";
            node.ChildNodes[1].InnerText = "F";
            node.ChildNodes[2].InnerText = "F"; //0 = faliure 1 = success
            node.ChildNodes[3].InnerText = MessageType; // "Reason will be entered here";
            //node.AppendChild(doc.CreateElement("Failure")).InnerText = "0";
            //node.AppendChild(doc.CreateElement("Result")).InnerText = "0";
            //node.AppendChild(doc.CreateElement("FailureReason")).InnerText = "Reason will be entered here";
            ////ReturnDoc.Elements(new XElement("Response",new XElement("Success","1"), new XElement("Failure","0"),new XElement("Result","1"),new XElement("FailureReason",""));

            return doc;
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

