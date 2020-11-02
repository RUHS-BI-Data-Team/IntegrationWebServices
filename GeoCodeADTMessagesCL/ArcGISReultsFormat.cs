using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodeADTMessagesCL
{
    class ArcGISReultsFormat
    {
        public class SpatialReference
        {
            public int wkid { get; set; }
            public int latestWkid { get; set; }
        }

        public class Location
        {
            public double x { get; set; }
            public double y { get; set; }
        }

        public class Attributes
        {
            public string Loc_name { get; set; }
            public double Score { get; set; }
            public string Match_addr { get; set; }
            public string Addr_type { get; set; }
            public string House { get; set; }
            public string PreDir { get; set; }
            public string PreType { get; set; }
            public string StreetName { get; set; }
            public string SufType { get; set; }
            public string SufDir { get; set; }
            public string UnitNumber { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZIP { get; set; }
            public string User_fld { get; set; }
            public string Side { get; set; }
            public string FromAddr { get; set; }
            public string ToAddr { get; set; }
        }

        public class Candidate
        {
            public string address { get; set; }
            public Location location { get; set; }
            public double score { get; set; }
            public Attributes attributes { get; set; }
        }

        public class RootObject
        {
            public SpatialReference spatialReference { get; set; }
            public List<Candidate> candidates { get; set; }
        }
    }
}
