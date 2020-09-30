using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using GeoCodeAddressUsingRCITWS;
using HL7Messages;
using System.Threading;

namespace WebServiceTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        GeoCodeResult gcResult = new GeoCodeResult();
        GeoCodeAddress gcAddress = new GeoCodeAddress();
        HL7Functions frnHL7 = new HL7Functions();
        //HL7WebServices.InterfaceWebServicesSoapClient ws = new HL7WebServices.InterfaceWebServicesSoapClient();
        HL7WebServicesTest.InterfaceWebServicesSoapClient ws = new HL7WebServicesTest.InterfaceWebServicesSoapClient();
        private void button1_Click(object sender, EventArgs e)

        {
            //ws.AddHL7MessageToWarehouse()
            ////CalRedieServices.CalREDIEServicesSoapClient ws = new CalRedieServices.CalREDIEServicesSoapClient();
            //textBox2.Text=ws.AddHL7MessageToWarehouse("ADT Message","Security",textBox1.Text);
            
        }
        private void ProcessDay()
        {
            SqlConnection GIScn = new SqlConnection("Data Source=cpc-intsqlprd01;Initial Catalog=GIS;Integrated Security=True; timeout= 120");
            SqlCommand GIScm = new SqlCommand("uspCheckForNewerAddress", GIScn);
            GIScm.CommandType = CommandType.StoredProcedure;
            GIScm.Parameters.Add(new SqlParameter("@MRN", SqlDbType.NVarChar, 15));
            GIScm.Parameters.Add(new SqlParameter("@HL7MessageDate", SqlDbType.DateTime));
            GIScm.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int, 4));
            GIScm.Parameters["@Count"].Direction = ParameterDirection.ReturnValue;

            SqlConnection cn = new SqlConnection("Data Source=cpc-intsqlprd01;Initial Catalog=GIS;Integrated Security=True; timeout= 120");
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

                int count;
                count = GIScm.ExecuteNonQuery();
                count = Convert.ToInt16(GIScm.Parameters["@Count"].Value);

                if (count == 0)
                {
                    try
                    {
                        gcResult = gcAddress.GeoCode(frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.1", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.3", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.4", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.5", 0));
                        gcAddress.AddPatientAddressToDB(gcResult, dr["MRN"].ToString(), dr["HL7ControlId"].ToString(), Convert.ToDateTime(dr["HL7MessageDate"].ToString()));
                        endId = dr["Id"].ToString();
                    }
                    catch
                    {
                        Thread.Sleep(6000);
                        gcResult = gcAddress.GeoCode(frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.1", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.3", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.4", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.5", 0));
                        gcAddress.AddPatientAddressToDB(gcResult, dr["MRN"].ToString(), dr["HL7ControlId"].ToString(), Convert.ToDateTime(dr["HL7MessageDate"].ToString()));
                        endId = dr["Id"].ToString();
                    }
                }
                //int milliseconds = 200;
                //Thread.Sleep(milliseconds);
            }
            GIScn.Close();
            DateTime Finish = DateTime.Now;
            txtTime.Text = Finish.Subtract(Start).TotalHours.ToString();
            dr.Close();
            cn.Close();
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {

            DateTime NewStartTime;
            //NewStartTime = GetMaxDateFromPatientAddress();
            //txtStartDate.Text = NewStartTime.ToString();
            do
            { 
                NewStartTime = GetMaxDateFromPatientAddress();
                txtStartDate.Text = NewStartTime.ToString();
                ProcessDay();
            }
            while (NewStartTime.AddDays(1) < DateTime.Now);
            //if(NewStartTime.AddDays(1) > DateTime.Now)
            //{ 
            //    ProcessDay();
            //}
        }

        private DateTime GetMaxDateFromPatientAddress()
        {
            DateTime ReturnValue;
            SqlConnection GIScn = new SqlConnection("Data Source=cpc-intsqlprd01;Initial Catalog=GIS;Integrated Security=True; timeout= 120");
            SqlCommand GIScm = new SqlCommand("uspSelectNewestHL7MessageDate", GIScn);
            GIScm.CommandType = CommandType.StoredProcedure;
            GIScm.Parameters.Add(new SqlParameter("@HL7MessageDate", SqlDbType.DateTime));
            GIScm.Parameters["@HL7MessageDate"].Direction = ParameterDirection.Output;
            GIScn.Open();
            GIScm.ExecuteNonQuery();
            ReturnValue = Convert.ToDateTime(GIScm.Parameters["@HL7MessageDate"].Value);
            GIScn.Close();
            return ReturnValue;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtStartDate.Text = GetMaxDateFromPatientAddress().ToString();

        }
    }
}
