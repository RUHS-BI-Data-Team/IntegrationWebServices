using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HL7Messages
{
    public class ReturnDataForInterface
    {
        string success = "S";
        string failure = "F";
        string result = "";
        string failureReason = "";

        public string Success { get { return success; } set { success = value; } }
    
        public string Failure { get { return failure; } set { failure = value; } }
        public string Result { get { return result; } set { result = value; } }
        public string FailureReason { get { return failureReason; } set { failureReason = value; } }

    }
}