using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Newtonsoft.Json;

namespace GeoCodeAddressUsingRCITWS
{
    public class GeoCodeAddress
    {
       public void AddPatientAddressToDB(GeoCodeResult PatientAddrerss, string MRN, string ControlId, DateTime HL7MessageDateTime)
        {
            if (PatientAddrerss.Easting > 100000 && PatientAddrerss.Northing > 100000 && MRN != "" && ControlId != "")
            {
                SqlConnection cn = new SqlConnection("Data Source=IntegrationDB;Initial Catalog=GIS;Integrated Security=True; timeout= 120");
                SqlCommand cm = new SqlCommand("uspInsertPatientAddressGeom", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add(new SqlParameter("@MRN", SqlDbType.NVarChar, 15));
                cm.Parameters.Add(new SqlParameter("@HL7ControlId", SqlDbType.NVarChar, 20));
                cm.Parameters.Add(new SqlParameter("@HL7MessageDate", SqlDbType.DateTime));
                cm.Parameters.Add(new SqlParameter("@StreetAddress", SqlDbType.NVarChar, 35));
                cm.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 25));
                cm.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 25));
                cm.Parameters.Add(new SqlParameter("@ZIPCode", SqlDbType.NVarChar, 10));
                cm.Parameters.Add(new SqlParameter("@Easting", SqlDbType.NVarChar, 20));
                cm.Parameters.Add(new SqlParameter("@Northing", SqlDbType.NVarChar, 20));

                cm.Parameters["@MRN"].Value = MRN;
                cm.Parameters["@HL7ControlId"].Value = ControlId;
                cm.Parameters["@HL7MessageDate"].Value = HL7MessageDateTime;
                cm.Parameters["@StreetAddress"].Value = PatientAddrerss.StreetAddress;
                cm.Parameters["@City"].Value = PatientAddrerss.City;
                cm.Parameters["@State"].Value = PatientAddrerss.State;
                cm.Parameters["@ZIPCode"].Value = PatientAddrerss.ZIPCode;
                cm.Parameters["@Easting"].Value = PatientAddrerss.Easting;
                cm.Parameters["@Northing"].Value = PatientAddrerss.Northing;
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();
            }
        }
        public GeoCodeResult GeoCode(string StreetAddress, string City, string State, string ZIPCode) {
            GeoCodeResult GR = new GeoCodeResult();
            string URL = "https://gis.countyofriverside.us/arcgis_public/rest/services/GeocodingService/RiversideGeocoder/GeocodeServer/findAddressCandidates?";
            Uri GeoCodeRequest = new Uri(string.Format(URL + "outFields=*&f=pjson&SingleLine={0}&City={1}&State={2}&Zip={3}&outSR={4}",
            Uri.EscapeDataString(StreetAddress), Uri.EscapeDataString(City), Uri.EscapeDataString(State), Uri.EscapeDataString(ZIPCode), 2230));
            WebRequest w = WebRequest.Create(GeoCodeRequest);
            WebResponse r = w.GetResponse();
            StreamReader sr = new StreamReader(r.GetResponseStream());
            String ResponseData = sr.ReadToEnd();
            ArcGISReultsFormat.RootObject jnRoot = JsonConvert.DeserializeObject<ArcGISReultsFormat.RootObject>(ResponseData);
            List<ArcGISReultsFormat.Candidate> Addresses = jnRoot.candidates;
            GR.StreetAddress = GetAddress(Addresses);
            GR.City = GetCity(Addresses);
            GR.State = GetState(Addresses);
            GR.ZIPCode = GetZip(Addresses);
            GR.Easting = GetX(Addresses);
            GR.Northing = GetY(Addresses);
            GR.Score = GetScore(Addresses);
            return GR;
        }

        static double GetX(List<ArcGISReultsFormat.Candidate> Addresses)
        {
            double x = 0;
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].location.x != 0)
                {
                    x = Addresses[i].location.x;
                    break;
                }
            }
            return x;
        }

        static double GetY(List<ArcGISReultsFormat.Candidate> Addresses)
        {
            double y = 0;
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].location.y != 0)
                {
                    y = Addresses[i].location.y;
                    break;
                }
            }
            return y;
        }

        static String GetAddress(List<ArcGISReultsFormat.Candidate> Addresses)
        {
            String a = "";
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].attributes.Match_addr != "")
                {
                    a = Addresses[i].attributes.Match_addr;
                    break;
                }
            }
            if (a.Contains(","))
            {
                return a.Substring(0, a.IndexOf(",") + 1);
            }
            else
            {
                return a;
            }
        }

        static String GetCity(List<ArcGISReultsFormat.Candidate> Addresses)
        {
            String c = "";
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].attributes.City != "")
                {
                    c = Addresses[i].attributes.City;
                    break;
                }
            }
            return c;
        }

        static String GetState(List<ArcGISReultsFormat.Candidate> Addresses)
        {
            String s = "";
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].attributes.State != "")
                {
                    s = Addresses[i].attributes.State;
                    break;
                }
            }
            return s;
        }

        static String GetZip(List<ArcGISReultsFormat.Candidate> Addresses)
        {
            String z = "";
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].attributes.ZIP != "")
                {
                    z = Addresses[i].attributes.ZIP;
                    break;
                }
            }
            return z;
        }
        static double GetScore(List<ArcGISReultsFormat.Candidate> Addresses)
        {
            double z = 0;
            for (int i = 0; i < Addresses.Count; i++)
            {
                if (Addresses[i].attributes.Score > z)
                {
                    z = Addresses[i].attributes.Score;
                }
            }
            return z;
        }
    }
}
