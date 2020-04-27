using GeoCodeAddressUsingRCITWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HL7Messages
{
    /// <summary>
    /// Summary description for HL7MessageReceiver
    /// </summary>
    [WebService(Namespace = "http://RUHealth.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HL7MessageReceiver : System.Web.Services.WebService
    {
        
        [WebMethod]
        public string AddHL7MessageToWarehouse (String MessageType, String Passphrase, String HL7Message)
        {

            GeoCodeResult r = new GeoCodeResult();
            GeoCodeAddress a = new GeoCodeAddress();
            r = a.GeoCode(HL7Parser(HL7Message, "PID11.1", 0), HL7Parser(HL7Message, "PID11.3", 0), HL7Parser(HL7Message, "PID11.4", 0), HL7Parser(HL7Message, "PID11.5", 0));
            return r.Easting.ToString();
        }

        public static String HL7Parser(string HL7Message, string HL7Element, Int16 RepeatLocation = 0)
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

