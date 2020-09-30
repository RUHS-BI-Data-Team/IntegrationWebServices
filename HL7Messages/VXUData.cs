using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HL7Messages
{
    public class VXUData
    {
        HL7Functions frnHL7 = new HL7Functions();
        string hL7Message;
        string controlId; //MSH10
        string sendingApplication; //MSH3
        DateTime? messageDate; //MSH7
        string mRN; //= dbo.ufnSelectMRNFromHL7Message(@Message)
        string hL7Event; //= dbo.ufnParseHL7Value(@Message, 'MSH9.2', 1)
        string sendingFacility; // = dbo.ufnParseHL7Value(@Message, 'MSH4', 1)
        string administeredCodeId; //  = dbo.ufnParseHL7Value(@Message, 'RXA5.1', 1)
        string administeredCodeText; // = dbo.ufnParseHL7Value(@Message, 'RXA5.2', 1)
        DateTime? administrationDateTime; // = dbo.ufnParseHL7Value(@Message, 'RXA3', 1)
        string orderNumber; // = dbo.ufnParseHL7Value(@Message, 'ORC2.1', 1)
        string orderingProviderId; // = dbo.ufnParseHL7Value(@Message, 'ORC12.1', 1)
        string site; // = dbo.ufnParseHL7Value(@Message, 'RXR2', 1)

        public VXUData() { }

        public VXUData(string HL7Message)
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
        public string AdministeredCodeId { get { return administeredCodeId; } set { administeredCodeId = value; } }
        public string AdministeredCodeText { get { return administeredCodeText; } set { administeredCodeText = value; } }
        public DateTime? AdministrationDateTime { get { return administrationDateTime; } set { administrationDateTime = value; } }
        public string OrderNumber { get { return orderNumber; } set { orderNumber = value; } }
        public string OrderingProviderId { get { return orderingProviderId; } set { orderingProviderId = value; } }
        public string Site { get { return site; } set { site = value; } }



        private void LoadValues()
        {
            controlId = frnHL7.HL7Parser(hL7Message, "MSH10", 0);
            sendingApplication = frnHL7.HL7Parser(hL7Message, "MSH3", 0);
            messageDate = frnHL7.ConvertHL7Date2SystemDate(frnHL7.HL7Parser(hL7Message, "MSH7", 0));
            mRN = frnHL7.FindPaserLocation(hL7Message, "PID3.1", "MRN", "PID3.4");
            hL7Event = frnHL7.HL7Parser(hL7Message, "MSH9.2", 0);
            sendingFacility = frnHL7.HL7Parser(hL7Message, "MSH4", 0);
            administeredCodeId = frnHL7.HL7Parser(hL7Message, "RXA5.1", 1);
            administeredCodeText = frnHL7.HL7Parser(hL7Message, "RXA5.2", 1);
            administrationDateTime = frnHL7.ConvertHL7Date2SystemDate(frnHL7.HL7Parser(hL7Message, "RXA3", 1));
            orderNumber = frnHL7.HL7Parser(hL7Message, "ORC2.1", 1);
            orderingProviderId = frnHL7.HL7Parser(hL7Message, "ORC12.1", 1);
            site = frnHL7.HL7Parser(hL7Message, "RXR2", 1);

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
            AdministeredCodeId = "";
            AdministeredCodeText = "";
            AdministrationDateTime = null;
            OrderNumber = "";
            OrderingProviderId = "";
            Site = "";
        }
    }
}