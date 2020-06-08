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
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace HL7Messages
{
    [WebService(Namespace = "http://www.RUHealth.org", Name = "InterfaceWebServices", Description = "Web Services for OpenLink to send HL7 messages")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class HL7MessageReceiver : System.Web.Services.WebService
    {
        DBFunctions dbf = new DBFunctions();
        string conn = ConfigurationManager.ConnectionStrings["HL7Warehouse"].ConnectionString;
        ValidateReturn r = new ValidateReturn();
        ADTData d = new ADTData();
        string CheckErrorMessage = "";
        string LoadErrorMessage = "";
        string TypesErrorMessage = "";
        DataTable dtTypes = new DataTable("Types");
        [WebMethod]
        [SoapDocumentMethod]
        public ValidateReturn AddHL7MessageToWarehouse(//[XmlElement(ElementName  = "AckNack", Namespace = "http://www.RUHealth.org")]
        string MessageType, String Passphrase, String HL7Message)
        {
            
            if (Application["MessageTypes"] is object)
            {
                dtTypes.ReadXml(new StringReader(Application["MessageTypes"].ToString()));
            }
            else
            {
                string xmlTypes = dbf.LoadMessageTypesFromDB(conn, ref TypesErrorMessage);
                if(xmlTypes == "")
                {
                    r.Validate = TypesErrorMessage;
                    return r;
                }
                dtTypes.ReadXml(new StringReader(xmlTypes));
            }

            switch (GetMessageTypeProcess(MessageType, Passphrase))
            {
                case 1: //ADT
                    ADTData d = new ADTData();

                    switch (dbf.CheckForExistingADTContreolID(conn, d.GetControlId(HL7Message), ref CheckErrorMessage))
                    {
                        case 0:
                            d.HL7Message = HL7Message;

                            if (dbf.InsertADTMessage(conn, d, ref LoadErrorMessage) == true)
                            {
                                r.Validate = Passphrase;
                            }
                            else
                            {
                                r.Validate = LoadErrorMessage;
                                //r.Validate = "Error Loading Data into Databse";
                            }
                            break;

                        case 1:
                            r.Validate = Passphrase;
                            break;
                        case 2:
                            r.Validate = CheckErrorMessage;
                            //r.Validate = "error Checking for existing HL7 message";
                            break;
                    }
                    break;
            }
            return r;
        }
        private int GetMessageTypeProcess(string MessageType, String Passphrase) {
            return 0;
        }
    }
           
}

