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
            return HL7Message;
        }
    }
}
