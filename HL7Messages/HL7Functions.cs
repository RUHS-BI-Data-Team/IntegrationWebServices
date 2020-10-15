using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HL7Messages
{
    public class HL7Functions
    {
        public HL7Functions(string LogFileLocation, string MessageType)
        {
            logFileLocation = LogFileLocation;
            messageType = MessageType;
        }
        Logging log = new Logging();
        string logFileLocation;
        string messageType;
        HL7ParseAndScub.LightWeightParser parser = new HL7ParseAndScub.LightWeightParser();
        public DateTime? ConvertHL7Date2SystemDate(string HL7Date)
        {
            DateTime? returnValue = null;
            try
            {
                if (HL7Date.Length >= 8)
                {
                    String Year, Month, Day, Hour, Minute, Second;
                    Year = HL7Date.Substring(0, 4);
                    Month = HL7Date.Substring(4, 2);
                    Day = HL7Date.Substring(6, 2);
                    if (HL7Date.Length > 12)
                    {
                        Hour = HL7Date.Substring(8, 2);
                        Minute = HL7Date.Substring(10, 2);
                        Second = HL7Date.Substring(12);
                        returnValue = DateTime.Parse(Month + "/" + Day + "/" + Year + " " + Hour + ":" + Minute + ":" + Second);
                    }
                    else
                    {
                        returnValue = DateTime.Parse(Month + "/" + Day + "/" + Year + " 00:00");
                    }
                }
            }
            catch (Exception e)
            {
                if (messageType == "ADT")
                {
                    log.LogADTError(logFileLocation, messageType + "ConvertHL7Date2SystemDate:" + HL7Date, e.Message);
                }
                else if (messageType == "VXU")
                {
                    log.LogVXUError(logFileLocation, messageType + "ConvertHL7Date2SystemDate:" + HL7Date, e.Message);
                }
                else if (messageType == "RDE")
                {
                    log.LogRDEError(logFileLocation, messageType + "ConvertHL7Date2SystemDate:" + HL7Date, e.Message);
                }
            }
            return returnValue;
        }

           public String FindPaserLocation(string HL7Message, string HL7ElementLocation, string MatchValue, string HL7ElementMatchLocation)
        {
            //HL7ParseAndScub.LightWeightParser parser = new HL7ParseAndScub.LightWeightParser();
            string returnValue = "";
            string[] MatchValues;
            string[] LocationValues;
            MatchValues = HL7Parser(HL7Message, HL7ElementMatchLocation, 0).Split(',');
            LocationValues = HL7Parser(HL7Message, HL7ElementLocation, 0).Split(',');
            for (int i = 0; i < MatchValues.Length; i++)
            {
                if(MatchValues[i] == MatchValue)
                {
                    returnValue = LocationValues[i];
                }
            }
            return returnValue;
        }
        public String HL7Parser(string HL7Message, string HL7Element, Int16 RepeatLocation = 0)
        {
            //HL7ParseAndScub.LightWeightParser parser = new HL7ParseAndScub.LightWeightParser();
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
