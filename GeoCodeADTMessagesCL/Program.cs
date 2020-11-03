using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace GeoCodeADTMessagesCL
{
    class Program
    {
        static GeoCodeResult gcResult = new GeoCodeResult();
        static GeoCodeAddress gcAddress = new GeoCodeAddress();
        static HL7Functions frnHL7 = new HL7Functions();
        
        static void Main(string[] args)
        {
            DateTime? NewStartTime;
            string ConnectionString = ConfigurationManager.ConnectionStrings["HL7Warehouse"].ToString();
            NewStartTime = GetMaxDateFromPatientAddress(ConnectionString);
            Console.WriteLine("Starting Date: " + NewStartTime.ToString());
            ProcessDay(ConnectionString);
            //Console.ReadKey(false); //used to stop the command window closing
        }
        static private void ProcessDay(string cns)
        {
            SqlConnection GIScn = new SqlConnection(cns);
            SqlCommand GIScm = new SqlCommand("uspCheckForNewerAddress", GIScn);
            GIScm.CommandType = CommandType.StoredProcedure;
            GIScm.Parameters.Add(new SqlParameter("@MRN", SqlDbType.NVarChar, 15));
            GIScm.Parameters.Add(new SqlParameter("@HL7MessageDate", SqlDbType.DateTime));
            GIScm.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 4));
            GIScm.Parameters["@Count"].Direction = ParameterDirection.ReturnValue;

            SqlConnection cn = new SqlConnection(cns);
            //SqlCommand cm = new SqlCommand("Select Id, HL7Data, MRN, HL7ControlId, HL7MessageDate from tblADT Where HL7MessageDate < '" + txtStartDate.Text + " 00:00' and HL7MessageDate >= DateAdd(day,-" + txtAmountDays.Value.ToString() + ", '" + txtStartDate.Text + " 00:00') Order By HL7MessageDate DESC", cn);
            SqlCommand cm = new SqlCommand("uspGeoCodePatientAddresList", cn);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr;
            String endId = "";
            cn.Open();
            dr = cm.ExecuteReader();
            //dr.Read();
            DateTime Start = DateTime.Now;
            GIScn.Open();
            while (dr.Read())
            {
                GIScm.Parameters["@MRN"].Value = dr["MRN"].ToString();
                GIScm.Parameters["@HL7MessageDate"].Value = Convert.ToDateTime(dr["HL7MessageDate"].ToString());
                Console.Clear();
                Console.WriteLine("Checking for a message newer then: " + Convert.ToDateTime(dr["HL7MessageDate"].ToString()) + " for MRN: " + dr["MRN"].ToString());
                int count;
                count = GIScm.ExecuteNonQuery();
                count = Convert.ToInt16(GIScm.Parameters["@Count"].Value);

                if (count == 0)
                {
                    try
                    {
                        gcResult = gcAddress.GeoCode(frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.1", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.3", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.4", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.5", 0));
                        gcAddress.AddPatientAddressToDB(gcResult, dr["MRN"].ToString(), dr["HL7ControlId"].ToString(), Convert.ToDateTime(dr["HL7MessageDate"].ToString()), cns);
                        endId = dr["Id"].ToString();
                    }
                    catch
                    {
                        Thread.Sleep(6000);
                        gcResult = gcAddress.GeoCode(frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.1", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.3", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.4", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.5", 0));
                        gcAddress.AddPatientAddressToDB(gcResult, dr["MRN"].ToString(), dr["HL7ControlId"].ToString(), Convert.ToDateTime(dr["HL7MessageDate"].ToString()), cns);
                        endId = dr["Id"].ToString();
                    }

                }
                else
                {
                    Console.WriteLine("Newer message found for MRN: " + dr["MRN"].ToString());
                }
                //int milliseconds = 200;
                //Thread.Sleep(milliseconds);
            }
            GIScn.Close();
            DateTime Finish = DateTime.Now;
            Console.WriteLine(Finish.Subtract(Start).TotalHours.ToString());
            dr.Close();
            cn.Close();
        }
        static DateTime GetMaxDateFromPatientAddress(string cns)
        {
            DateTime? ReturnValue = null;
            try { 
            SqlConnection GIScn = new SqlConnection(cns);
            SqlCommand GIScm = new SqlCommand("uspSelectNewestHL7MessageDate", GIScn);
            GIScm.CommandType = CommandType.StoredProcedure;
            GIScm.Parameters.Add(new SqlParameter("@HL7MessageDate", SqlDbType.DateTime));
            GIScm.Parameters["@HL7MessageDate"].Direction = ParameterDirection.Output;
            GIScn.Open();
            GIScm.ExecuteNonQuery();
            ReturnValue = Convert.ToDateTime(GIScm.Parameters["@HL7MessageDate"].Value);
            GIScn.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return (DateTime)ReturnValue;
        }
    }
}
