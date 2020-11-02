using System;
using System.Collections.Generic;
using System.Text;

namespace GeoCodeADTMessagesCL
{
    public class GeoCodeResult
    {
        string streetAddress = "";
        string city = "";
        string state = "";
        string zIPCode = "";
        double easting = 0;
        double northing = 0;
        double score = 0;

        public string StreetAddress { get { return streetAddress; } set { streetAddress = value; } }
        public string City { get { return city; } set { city = value; } }
        public string State { get { return state; } set { state = value; } }
        public string ZIPCode { get { return zIPCode; } set { zIPCode = value; } }
        public double Easting { get { return easting; } set { easting = value; } }
        public double Northing { get { return northing; } set { northing = value; } }
        public double Score { get { return score; } set { score = value; } }
    }
}
