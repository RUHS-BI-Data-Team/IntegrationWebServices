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

    

}
