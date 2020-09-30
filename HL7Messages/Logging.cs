using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HL7Messages
{
    public class Logging
    {
        public void SecurityValuesDonotMatch(String FileLocation, string ProcessName, string MessageType, String Passphrase)
        {
            if (!File.Exists(FileLocation + "SecurityValuesMisMatch.txt"))
            {
               FileStream fs = File.Create(FileLocation + "SecurityValuesMisMatch.txt");
                fs.Close();

            }
            using (StreamWriter sw = new StreamWriter(FileLocation + "SecurityValuesMisMatch.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ", ProcessId=" + ProcessName + ", MessageType=" + MessageType + ", Passphrase=" + Passphrase);
                sw.Close();
            }
        }
        public void LogADTError(String FileLocation, string ProcessName, string ErrorMessage)
        {
            if (!File.Exists(FileLocation + "ADTErrorLog.txt"))
            {
                FileStream fs = File.Create(FileLocation + "ADTErrorLog.txt");
                fs.Close();

            }
            using (StreamWriter sw = new StreamWriter(FileLocation + "ADTErrorLog.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ", ProcessId=" + ProcessName + ", ErrorMessage=" + ErrorMessage);
                sw.Close();
            }
        }
        public void LogVXUError(String FileLocation, string ProcessName, string ErrorMessage)
        {
            if (!File.Exists(FileLocation + "VXUErrorLog.txt"))
            {
                FileStream fs = File.Create(FileLocation + "VXUErrorLog.txt");
                fs.Close();

            }
            using (StreamWriter sw = new StreamWriter(FileLocation + "VXUErrorLog.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ", ProcessId=" + ProcessName + ", ErrorMessage=" + ErrorMessage);
                sw.Close();
            }
        }
    }
}