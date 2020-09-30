using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.IO;

namespace HL7Messages
{
    public class DBFunctions
    {
        public String LoadMessageTypesFromDB(string connectionString, ref String ErrorMessage)
        {
            DataTable dt = new DataTable("Types");
            StringWriter xmlTypes = new StringWriter();
            SqlConnection cn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cm = new SqlCommand("uspSelectMessageTypes", cn);
                cm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cm);
                cn.Open();
                da.Fill(dt);
                cn.Close();
                dt.WriteXml(xmlTypes,XmlWriteMode.WriteSchema);
                return xmlTypes.ToString();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return "";
            }
            finally
            {
                cn.Close();
            }
        }

        //Not used with the removal of the Patient Address geocode removed
        public int CheckForExistingADTControlID(string connectionString, string ControlId, ref String ErrorMessage) //Not Use
        {
            SqlConnection cn = new SqlConnection(connectionString);
            try
            {
                ErrorMessage = "";
                SqlCommand cm = new SqlCommand("uspADTControlIdExist", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add(new SqlParameter("@ControlId", SqlDbType.NVarChar, 20)).Value = ControlId;
                cm.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 4));
                cm.Parameters["@Count"].Direction = ParameterDirection.ReturnValue;
                cn.Open();
                int count;
                count = cm.ExecuteNonQuery();
                count = Convert.ToInt16(cm.Parameters["@Count"].Value);
                cn.Close();
                if (count == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }catch(Exception e)
            {
                ErrorMessage = e.Message;
                return 2;
            }
            finally
            {
                cn.Close();
            }
        }
        public Boolean InsertADTMessage(string connectionString, ADTData ADTMessage, ref string ErrorMessage)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cm = new SqlCommand("uspInsertADTData", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add(new SqlParameter("@HL7Message", SqlDbType.NVarChar, -1)).Value = ADTMessage.HL7Message;
                cm.Parameters.Add(new SqlParameter("@ControlId", SqlDbType.NVarChar, 20)).Value = ADTMessage.ControlId;
                cm.Parameters.Add(new SqlParameter("@SendingApplication", SqlDbType.NVarChar, 10)).Value = ADTMessage.SendingApplication;
                cm.Parameters.Add(new SqlParameter("@MessageDate", SqlDbType.DateTime, 8)).Value = ADTMessage.MessageDate;
                cm.Parameters.Add(new SqlParameter("@MRN", SqlDbType.NVarChar, 15)).Value = ADTMessage.MRN;
                cm.Parameters.Add(new SqlParameter("@Event", SqlDbType.NVarChar, 3)).Value = ADTMessage.HL7Event;
                cm.Parameters.Add(new SqlParameter("@SendingFacility", SqlDbType.NVarChar, 50)).Value = ADTMessage.SendingFacility;
                cm.Parameters.Add(new SqlParameter("@PatientClass", SqlDbType.NVarChar, 10)).Value = ADTMessage.PatientClass;
                cm.Parameters.Add(new SqlParameter("@PV13", SqlDbType.NVarChar, -1)).Value = ADTMessage.PV13;
                cm.Parameters.Add(new SqlParameter("@Encounter", SqlDbType.NVarChar, 15)).Value = ADTMessage.Encounter;
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();
                return true;
            }
            catch(Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                cn.Close();
            }
        }
        public Boolean InsertVXUMessage(string connectionString, VXUData VXUMessage, ref string ErrorMessage)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cm = new SqlCommand("uspInsertVXUData", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add(new SqlParameter("@HL7Message", SqlDbType.NVarChar, -1)).Value = VXUMessage.HL7Message;
                cm.Parameters.Add(new SqlParameter("@ControlId", SqlDbType.NVarChar, 20)).Value = VXUMessage.ControlId;
                cm.Parameters.Add(new SqlParameter("@SendingApplication", SqlDbType.NVarChar, 10)).Value = VXUMessage.SendingApplication;
                cm.Parameters.Add(new SqlParameter("@MessageDate", SqlDbType.DateTime, 8)).Value = VXUMessage.MessageDate;
                cm.Parameters.Add(new SqlParameter("@MRN", SqlDbType.NVarChar, 15)).Value = VXUMessage.MRN;
                cm.Parameters.Add(new SqlParameter("@Event", SqlDbType.NVarChar, 3)).Value = VXUMessage.HL7Event;
                cm.Parameters.Add(new SqlParameter("@SendingFacility", SqlDbType.NVarChar, 50)).Value = VXUMessage.SendingFacility;
                cm.Parameters.Add(new SqlParameter("@HL7AdministeredCodeId", SqlDbType.NVarChar, 10)).Value = VXUMessage.AdministeredCodeId;
                cm.Parameters.Add(new SqlParameter("@HL7AdministeredCodeText", SqlDbType.NVarChar,255)).Value = VXUMessage.AdministeredCodeText;
                cm.Parameters.Add(new SqlParameter("@HL7AdministrationDateTime", SqlDbType.DateTime, 8)).Value = VXUMessage.AdministrationDateTime; 
                cm.Parameters.Add(new SqlParameter("@HL7OrderNumber", SqlDbType.NVarChar, 15)).Value = VXUMessage.OrderNumber;
                cm.Parameters.Add(new SqlParameter("@HL7OrderingProviderId", SqlDbType.NVarChar, 15)).Value = VXUMessage.OrderingProviderId;
                cm.Parameters.Add(new SqlParameter("@HL7Site", SqlDbType.NVarChar, 10)).Value = VXUMessage.Site;
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();
                return true;

            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}