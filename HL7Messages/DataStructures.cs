using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections;

namespace HL7Messages
{
    [XmlRoot("ackNakReturn", Namespace = "http://RUHealth.org")]
    public class AckNakReturn
    {
        string ack = "S";
        string nak = "F";
        string ackNak = "";
        string nakReason = "";

        [XmlElement("Ack")]
        public string Ack { get { return ack; } set { ack = value; } }

        [XmlElement("Nak")]
        public string Nak { get { return nak; } set { nak = value; } }

        [XmlElement("AckNak")]
        public string AckNak { get { return ackNak; } set { ackNak = value; } }

        [XmlElement("NakReason")]
        public string NakReason { get { return nakReason; } set { nakReason = value; } }
    }

    public class ValidateReturn
    {
        string validate;
        [XmlElement("Validate")]
        public string Validate { get { return validate; } set { validate = value; } }

    }

    public class ADTData 
    {
        string hL7Message;
        string controlId; //dbo.ufnParseHL7Value(@Message,'MSH10',0)
        string sendingApplication; //= dbo.ufnParseHL7Value(@Message, 'MSH3', 0)
        string messageDate; // = dbo.ufnHL7DateToSQLDate(dbo.ufnParseHL7Value(@Message, 'MSH7', 0))
        string mRN; //= dbo.ufnSelectMRNFromHL7Message(@Message)
        string hL7Event; //= dbo.ufnParseHL7Value(@Message, 'MSH9.2', 1)
        string sendingFacility; // = dbo.ufnParseHL7Value(@Message, 'MSH4', 1)
        string patientClass; // = dbo.ufnParseHL7Value(@Message, 'PV12', 1)
        string pV13; //= dbo.ufnParseHL7Value(@Message, 'PV13', 0)
        string encounter;// = dbo.ufnParseHL7Value(@Message, 'PID18', 1)

        public ADTData(string HL7Message) {
            hL7Message = HL7Message;
            controlId = HL7Parser(HL7Message, "MSH10", 0);
            sendingApplication = HL7Parser(HL7Message, "MSH3", 0);
            messageDate = HL7Parser(HL7Message, "MSH7", 0);
            mRN = HL7Parser(HL7Message, "MSH10", 0);
            hL7Event = HL7Parser(HL7Message, "MSH9", 0);
            sendingFacility = HL7Parser(HL7Message, "MSH4", 0);
            patientClass = HL7Parser(HL7Message, "PV12", 0);
            pV13 = HL7Parser(HL7Message, "PV13", 0);
            encounter = HL7Parser(HL7Message, "PID18", 0);

        }
        public string HL7Message { get { return hL7Message; } } 
        public string ControlId { get { return controlId; } set { controlId = value; } }
        public string SendingApplication { get { return sendingApplication; } set { sendingApplication = value; } }
        public string MessageDate { get { return messageDate; } set { messageDate = value; } }
        public string MRN { get { return mRN; } set { mRN = value; } }
        public string HL7Event { get { return hL7Event; } set { hL7Event = value; } }
        public string SendingFacility { get { return sendingFacility; } set { sendingFacility = value; } }
        public string PatientClass { get { return patientClass; } set { patientClass = value; } }
        public string PV13 { get { return pV13; } set { pV13 = value; } }
        public string Encounter { get { return encounter; } set { encounter = value; } }


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
