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
        Logging log = new Logging();
        string conn = ConfigurationManager.ConnectionStrings["HL7Warehouse"].ConnectionString;
        ValidateReturn r = new ValidateReturn();
        ADTData d = new ADTData();
        string CheckErrorMessage = "";
        string LoadErrorMessage = "";
        string TypesErrorMessage = "";
        DataTable dtTypes = new DataTable("MessageTypes");
        [WebMethod]
        [SoapDocumentMethod]
        public ValidateReturn AddHL7MessageToWarehouse(string MessageType, String Passphrase, String HL7Message)
        {
            
            if (Application["MessageTypes"] is object)
            {
                dtTypes.ReadXml(new StringReader(Application["MessageTypes"].ToString()));
            }
            else
            {
                StringWriter mt = new StringWriter();
                if (File.Exists(Server.MapPath("MessageTypes.xml")) == true)
                {
                    dtTypes.ReadXml(Server.MapPath("MessageTypes.xml"));
                    dtTypes.WriteXml(mt, XmlWriteMode.WriteSchema);
                    Application["MessageTypes"] = mt.ToString();
                }

            }

            switch (GetMessageTypeProcess(MessageType, Passphrase))
            {
                case 0:
                    //log requests that does not match security
                    log.SecurityValuesDonotMatch(Server.MapPath("~/"), "0", MessageType, Passphrase);
                    r.Validate = Passphrase;
                    break;
                case 1: //ADT
                    ADTData d = new ADTData();
                   d.HL7Message = HL7Message.Replace("\n", "\r");


                    if (dbf.InsertADTMessage(conn, d, ref LoadErrorMessage) == true)
                    {
                        r.Validate = Passphrase;
                    }
                    else
                    {
                        r.Validate = LoadErrorMessage;
                        log.LogADTError(Server.MapPath("~/"), "1", LoadErrorMessage);
                    }
                   
                    break;
                case 2: //VXU
                    VXUData v = new VXUData();
                    v.HL7Message = HL7Message.Replace("\n", "\r");
                    if (dbf.InsertVXUMessage(conn, v, ref LoadErrorMessage) == true)
                    {
                        r.Validate = Passphrase;
                    }
                    else
                    {
                        r.Validate = LoadErrorMessage;
                        log.LogVXUError(Server.MapPath("~/"), "2", LoadErrorMessage);
                    }
                    break;
            }
            return r;
        }
        private int GetMessageTypeProcess(string MessageType, String Passphrase) {
            int returnValue = 0;
            foreach(DataRow r in dtTypes.Rows)
            {
                if(r["MessageType"].ToString() == MessageType && r["SecurityValue"].ToString() == Passphrase)
                {
                    returnValue = Convert.ToInt32(r["ProcessToRun"].ToString());
                }
            }
            return returnValue;
        }
    }
           
}

