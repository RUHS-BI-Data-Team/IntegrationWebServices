using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HL7Messages
{
    public class RDEData
    {
        Logging log = new Logging();
        HL7Functions frnHL7;// = HL7Functions;
        string logFileLocation;
        string hL7Message;
        string controlId; //MSH10
        string sendingApplication; //MSH3
        DateTime? messageDate; //MSH7
        string mRN; //= dbo.ufnSelectMRNFromHL7Message(@Message)
        string hL7Event; //= dbo.ufnParseHL7Value(@Message, 'MSH9.2', 1)
        string sendingFacility; // = dbo.ufnParseHL7Value(@Message, 'MSH4', 1)
        string patientClass; // = dbo.ufnParseHL7Value(@Message, 'PV12', 1)
        string encounter;// = dbo.ufnParseHL7Value(@Message, 'PID18', 1)
        string hL7PatientFamilyName; //PID-5.1
        //DateTime? hL7PatientDateOfBirth; //PID-7.1
        //string hL7OrderControl; //ORC-1
        //string hL7FillerOrderNumber; //ORC-3
        //DateTime? hL7QantityStartDateTime; //ORC-7.4
        //string hL7OrderringProviderNPI; //ORC-12
        //string hL7NDCorRxNormCode; //RXE-21
        string hL7GiveDosageForm; //RXE-6
        string hL7GiveAmount; //RXE-3
        string hL7GiveUnits; //RXE-5
        //string hL7Sig; //RXE-21.2
        //string hL7Route; //RXR-1


        public RDEData(string LogFileLocation)
        {
            logFileLocation = LogFileLocation;
            frnHL7 = new HL7Functions(logFileLocation, "RDE");
        }

        public string GetControlId(string HL7Message)
        {
            return controlId = frnHL7.HL7Parser(HL7Message, "MSH10", 0);
        }
        public RDEData(string LogFileLocation, string HL7Message)
        {
            ClearValues();
            hL7Message = HL7Message;
            LoadValues();

        }
        public string HL7Message { get { return hL7Message; } set { hL7Message = value; LoadValues(); } }
        public string ControlId { get { return controlId; } set { controlId = value; } }
        public string SendingApplication { get { return sendingApplication; } set { sendingApplication = value; } }
        public DateTime? MessageDate { get { return messageDate; } set { messageDate = value; } }
        public string MRN { get { return mRN; } set { mRN = value; } }
        public string HL7Event { get { return hL7Event; } set { hL7Event = value; } }
        public string SendingFacility { get { return sendingFacility; } set { sendingFacility = value; } }
        public string PatientClass { get { return patientClass; } set { patientClass = value; } }
         public string Encounter { get { return encounter; } set { encounter = value; } }
        public string HL7PatientFamilyName { get { return hL7PatientFamilyName; } set { hL7PatientFamilyName = value; } }
        //public DateTime? HL7PatientDateOfBirth { get { return hL7PatientDateOfBirth; } set { hL7PatientDateOfBirth = value; } }
        //public string HL7OrderControl { get { return hL7OrderControl; } set { hL7OrderControl = value; } }
        //public string HL7FillerOrderNumber { get { return hL7FillerOrderNumber; } set { hL7FillerOrderNumber = value; } }
        //public DateTime? HL7QantityStartDateTime { get { return hL7QantityStartDateTime; } set { hL7QantityStartDateTime = value; } }
        //public string HL7OrderringProviderNPI { get { return hL7OrderringProviderNPI; } set { hL7OrderringProviderNPI = value; } }
        //public string HL7NDCorRxNormCode { get { return hL7NDCorRxNormCode; } set { hL7NDCorRxNormCode = value; } }
        public string HL7GiveDosageForm { get { return hL7GiveDosageForm; } set { hL7GiveDosageForm = value; } }
        public string HL7GiveAmount { get { return hL7GiveAmount; } set { hL7GiveAmount = value; } }
        public string HL7GiveUnits { get { return hL7GiveUnits; } set { hL7GiveUnits = value; } }
        //public string HL7Sig { get { return hL7Sig; } set { hL7Sig = value; } }
        //public string HL7Route { get { return hL7Route; } set { hL7Route = value; } }


        //public GeoCodeResult GeoCodedData { get { return gcResult; } }

        private void LoadValues()
        {
            controlId = frnHL7.HL7Parser(hL7Message, "MSH10", 0);
            sendingApplication = frnHL7.HL7Parser(hL7Message, "MSH3", 0);
            messageDate = frnHL7.ConvertHL7Date2SystemDate(frnHL7.HL7Parser(hL7Message, "MSH7", 0));
            mRN = frnHL7.FindPaserLocation(hL7Message, "PID3.1", "MRN", "PID3.4");
            hL7Event = frnHL7.HL7Parser(hL7Message, "MSH9.2", 0);
            sendingFacility = frnHL7.HL7Parser(hL7Message, "MSH4", 0);
            patientClass = frnHL7.HL7Parser(hL7Message, "PV12", 0);
            encounter = frnHL7.HL7Parser(hL7Message, "PID18", 0);
            hL7PatientFamilyName = frnHL7.HL7Parser(hL7Message, "PID5.1", 0);
            //hL7PatientDateOfBirth = frnHL7.ConvertHL7Date2SystemDate(frnHL7.HL7Parser(hL7Message, "PID7.1", 0));
            //hL7OrderControl = frnHL7.HL7Parser(hL7Message, "ORC1", 0);
            //hL7FillerOrderNumber = frnHL7.HL7Parser(hL7Message, "ORC3", 0);
            //hL7QantityStartDateTime = frnHL7.ConvertHL7Date2SystemDate(frnHL7.HL7Parser(hL7Message, "ORC7.4", 0));
            //hL7OrderringProviderNPI = frnHL7.HL7Parser(hL7Message, "ORC12", 0);
            //hL7NDCorRxNormCode = frnHL7.HL7Parser(hL7Message, "RXE21", 0);
            hL7GiveDosageForm = frnHL7.HL7Parser(hL7Message, "RXE6", 0);
            hL7GiveAmount = frnHL7.HL7Parser(hL7Message, "RXE3", 0);
            hL7GiveUnits = frnHL7.HL7Parser(hL7Message, "RXE5", 0);
            //hL7Sig = frnHL7.HL7Parser(hL7Message, "RXE21.2", 0);
            //hL7Route = frnHL7.HL7Parser(hL7Message, "RXR1", 0);
            //gcResult = gcAddress.GeoCode(frnHL7.HL7Parser(HL7Message, "PID11.1", 0), frnHL7.HL7Parser(HL7Message, "PID11.3", 0), frnHL7.HL7Parser(HL7Message, "PID11.4", 0), frnHL7.HL7Parser(HL7Message, "PID11.5", 0));


        }
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
            encounter = "";
            hL7PatientFamilyName = "";
            //hL7PatientDateOfBirth = null;
            //hL7OrderControl = "";
            //hL7FillerOrderNumber = "";
            //hL7PatientDateOfBirth = null;
            //hL7OrderringProviderNPI = "";
            //hL7NDCorRxNormCode = "";
            hL7GiveDosageForm = "";
            hL7GiveAmount = "";
            hL7GiveUnits = "";
            //hL7Sig = "";
            //hL7Route = "";
        }

    }
}