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

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("Data Source=IntegrationDB;Initial Catalog=HL7Data;Integrated Security=True; timeout= 120");
            SqlCommand cm = new SqlCommand("Select Id, HL7Data, MRN, HL7ControlId, HL7MessageDate from tblADT Where Id > " + txtStart.Text + " and Id <= (" + txtStart.Text + "+" + txtAmount.Text + ") Order By Id", cn);
            SqlDataReader dr;
            String endId = "";
            cn.Open();
            dr = cm.ExecuteReader();
            //dr.Read();
            DateTime Start = DateTime.Now;
            while(dr.Read()){
                gcResult = gcAddress.GeoCode(frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.1", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.3", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.4", 0), frnHL7.HL7Parser(dr["HL7Data"].ToString(), "PID11.5", 0));
                gcAddress.AddPatientAddressToDB(gcResult, dr["MRN"].ToString(),dr["HL7ControlId"].ToString(),Convert.ToDateTime(dr["HL7MessageDate"].ToString()));
                endId = dr["Id"].ToString();
                int milliseconds = 500;
                Thread.Sleep(milliseconds);
            }
            DateTime Finish = DateTime.Now;
            if (endId == "")
            {
                txtStart.Text = txtAmount.Text;
            }
            else
            {
                txtStart.Text = endId;
            }
           
            txtTime.Text = Finish.Subtract(Start).TotalMinutes.ToString();
            dr.Close();
            cn.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
