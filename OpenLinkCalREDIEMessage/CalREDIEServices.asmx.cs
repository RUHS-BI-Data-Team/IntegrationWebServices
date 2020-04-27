using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace HL7MessageWebServices
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://RUHealth.org/")]
    //[WebService(Namespace = "ns1:urn:cdc:iisb:2011")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    
    public class CalREDIEServices : System.Web.Services.WebService
    {
       
        [WebMethod]
        public string CalREDIEHL7Message(string MessageXML)
        {
            return "Message Saved";
        }

        
        private void GetHL7Information(string HL7, ref string HL7MessageId, ref string HL7AcknowledgementCode,ref string HL7AcknowledgementMessage)
        {
            //HL7 = HL7.Replace("&amp", "&");
            string[] segments;
            //Char[] delimiters = new char[] { Convert.ToChar("\r") };
            
            segments = HL7.Split(new char[] { Convert.ToChar("\r") }, StringSplitOptions.RemoveEmptyEntries); 

            //segments = HL7.Substring(HL7.IndexOf("MSA|"));
            string[] fields;
            fields = segments[1].Split(new char[] { Convert.ToChar("|") }); //MSA segement

            HL7MessageId = fields[2];
            HL7AcknowledgementCode = fields[1];
            HL7AcknowledgementMessage = fields[3];
            //return "";
            
        }
        private string InsdertMissingTag(string XMLData, string XMLTag,string StartTagtoAdd, String EndTagToAdd)
        {
            int EndTagLocation = XMLData.IndexOf(XMLTag) + XMLTag.Length;
            if(XMLData.Substring(EndTagLocation, StartTagtoAdd.Length) != StartTagtoAdd)
            {
                XMLData=XMLData.Insert(EndTagLocation, StartTagtoAdd);
            }
            XMLTag = XMLTag.Insert(1, "/");
            EndTagLocation = XMLData.IndexOf(XMLTag);
            if (XMLData.Substring(EndTagLocation- EndTagToAdd.Length, EndTagToAdd.Length) != EndTagToAdd)
            {
                XMLData = XMLData.Insert(EndTagLocation, EndTagToAdd);
            }
            return XMLData;
        }
        private Boolean SaveResponseToDB(string Message, String HL7Data, string Status,string HL7MessageId, string HL7AcknowledgementCode, string HL7AcknowledgementMessage)
        {
            SqlConnection cn = new SqlConnection("Data Source=IntegrationDB;Initial Catalog=HL7Data;Persist Security Info=True;User ID=IntegrationEngineUser;Password=Ieut#2017");
            SqlCommand cm = new SqlCommand("uspInsertCalREDIEResponceMessage", cn);
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.Parameters.Add(new SqlParameter("@Message", SqlDbType.Text, 2147483647));
            cm.Parameters.Add(new SqlParameter("@HL7Data", SqlDbType.Text, 2147483647));
            cm.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50));
            cm.Parameters.Add(new SqlParameter("@HL7MessageId", SqlDbType.NVarChar, 15));
            cm.Parameters.Add(new SqlParameter("@HL7AcknowledgementCode", SqlDbType.NVarChar, 2));
            cm.Parameters.Add(new SqlParameter("@HL7AcknowledgementMessage", SqlDbType.NVarChar, 80));
            cm.Parameters["@Message"].Value = Message;
            cm.Parameters["@HL7Data"].Value = HL7Data;
            cm.Parameters["@Status"].Value = Status;
            cm.Parameters["@HL7MessageId"].Value = HL7MessageId;
            cm.Parameters["@HL7AcknowledgementCode"].Value = HL7AcknowledgementCode;
            cm.Parameters["@HL7AcknowledgementMessage"].Value = HL7AcknowledgementMessage;
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
            return true;
        }
    }


}
