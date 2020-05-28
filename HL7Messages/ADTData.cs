using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoCodeAddressUsingRCITWS;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace HL7Messages
{
   
   

    public class ADTData
    {
        GeoCodeResult gcResult = new GeoCodeResult();
        GeoCodeAddress gcAddress = new GeoCodeAddress();
        HL7Functions frnHL7 = new HL7Functions();
        string hL7Message;
        string controlId; //MSH10
        string sendingApplication; //MSH3
        DateTime? messageDate; //MSH7
        string mRN; //= dbo.ufnSelectMRNFromHL7Message(@Message)
        string hL7Event; //= dbo.ufnParseHL7Value(@Message, 'MSH9.2', 1)
        string sendingFacility; // = dbo.ufnParseHL7Value(@Message, 'MSH4', 1)
        string patientClass; // = dbo.ufnParseHL7Value(@Message, 'PV12', 1)
        string pV13; //= dbo.ufnParseHL7Value(@Message, 'PV13', 0)
        string encounter;// = dbo.ufnParseHL7Value(@Message, 'PID18', 1)
       
        public ADTData() { }
        public ADTData(string HL7Message)
        {
            ClearValues();
            hL7Message = HL7Message;
            controlId = frnHL7.HL7Parser(HL7Message, "MSH10", 0);
            sendingApplication = frnHL7.HL7Parser(HL7Message, "MSH3", 0);
            messageDate = frnHL7.ConvertHL7Date2SystemDate(frnHL7.HL7Parser(HL7Message, "MSH7", 0));
            mRN = frnHL7.FindPaserLocation(HL7Message, "PID3.1","MRN", "PID3.5");
            hL7Event = frnHL7.HL7Parser(HL7Message, "MSH9", 0);
            sendingFacility = frnHL7.HL7Parser(HL7Message, "MSH4", 0);
            patientClass = frnHL7.HL7Parser(HL7Message, "PV12", 0);
            pV13 = frnHL7.HL7Parser(HL7Message, "PV13", 0);
            encounter = frnHL7.HL7Parser(HL7Message, "PID18", 0);

            gcResult = gcAddress.GeoCode(frnHL7.HL7Parser(HL7Message, "PID11.1", 0), frnHL7.HL7Parser(HL7Message, "PID11.3", 0), frnHL7.HL7Parser(HL7Message, "PID11.4", 0), frnHL7.HL7Parser(HL7Message, "PID11.5", 0));
            

        }
        public string HL7Message { get { return hL7Message; } }
        public string ControlId { get { return controlId; } set { controlId = value; } }
        public string SendingApplication { get { return sendingApplication; } set { sendingApplication = value; } }
        public DateTime? MessageDate { get { return messageDate; } set { messageDate = value; } }
        public string MRN { get { return mRN; } set { mRN = value; } }
        public string HL7Event { get { return hL7Event; } set { hL7Event = value; } }
        public string SendingFacility { get { return sendingFacility; } set { sendingFacility = value; } }
        public string PatientClass { get { return patientClass; } set { patientClass = value; } }
        public string PV13 { get { return pV13; } set { pV13 = value; } }
        public string Encounter { get { return encounter; } set { encounter = value; } }
        public GeoCodeResult GeoCodedData { get { return gcResult; } }

        private void ClearValues()
        {
            hL7Message = "";
            controlId = "";
            sendingApplication = "";
            messageDate = null;
            mRN = "";
            hL7Event = "";
            sendingFacility = "";
            patientClass = "";
            pV13 = "";
            encounter = "";
        }

    }
}