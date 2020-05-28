using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HL7Messages
{
    public class DBFunctions
    {
        public Boolean InsertADTMessage(string connectionString, ADTData ADTMessage)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm = new SqlCommand("uspInsertADTData", cn);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add(new SqlParameter("@HL7Message", SqlDbType.NVarChar,-1)).Value = ADTMessage.HL7Message;
            cm.Parameters.Add(new SqlParameter("@ControlId", SqlDbType.NVarChar, 20)).Value = ADTMessage.ControlId;
            cm.Parameters.Add(new SqlParameter("@SendingApplication", SqlDbType.NVarChar, 10)).Value = ADTMessage.SendingApplication;
            cm.Parameters.Add(new SqlParameter("@MessageDate", SqlDbType.DateTime, 8)).Value = ADTMessage.MessageDate;
            cm.Parameters.Add(new SqlParameter("@MRN", SqlDbType.NVarChar, 15)).Value = ADTMessage.MRN;
            cm.Parameters.Add(new SqlParameter("@Event", SqlDbType.NVarChar, 3)).Value = ADTMessage.HL7Event;
            cm.Parameters.Add(new SqlParameter("@SendingFacility", SqlDbType.NVarChar, 50)).Value = ADTMessage.SendingFacility;
            cm.Parameters.Add(new SqlParameter("@PatientClass", SqlDbType.NVarChar, 10)).Value = ADTMessage.PatientClass;
            cm.Parameters.Add(new SqlParameter("@PV13", SqlDbType.NVarChar, -1)).Value = ADTMessage.PV13;
            cm.Parameters.Add(new SqlParameter("@Encounter", SqlDbType.NVarChar, 15)).Value = ADTMessage.Encounter;
            cm.Parameters.Add(new SqlParameter("@StreetAddress", SqlDbType.NVarChar, 150)).Value = ADTMessage.GeoCodedData.StreetAddress;
            cm.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 35)).Value = ADTMessage.GeoCodedData.City;
            cm.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 35)).Value = ADTMessage.GeoCodedData.State;
            cm.Parameters.Add(new SqlParameter("@ZIPCode", SqlDbType.NVarChar, 10)).Value = ADTMessage.GeoCodedData.ZIPCode;
            cm.Parameters.Add(new SqlParameter("@Easting", SqlDbType.NVarChar, 20)).Value = ADTMessage.GeoCodedData.Easting;
            cm.Parameters.Add(new SqlParameter("@Northing", SqlDbType.NVarChar, 20)).Value = ADTMessage.GeoCodedData.Northing;
            
            
            
            return true;
        }
    }
}