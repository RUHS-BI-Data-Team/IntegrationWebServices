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

namespace WebServiceTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
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
            SqlCommand cm = new SqlCommand("Select Top 1000 * from tblADT", cn);
            SqlDataReader dr;
            cn.Open();
            dr = cm.ExecuteReader();
            //dr.Read();
            DateTime Start = DateTime.Now;
            while(dr.Read()){
                textBox1.Text = dr["HL7Data"].ToString();
                HL7WebServicesTest.ValidateReturn WSresponse;

                WSresponse = ws.AddHL7MessageToWarehouse("ADT Message", "Security", dr["HL7Data"].ToString());
                textBox2.Text = WSresponse.Validate;
            }
            DateTime Finish = DateTime.Now;

            txtTime.Text = Finish.Subtract(Start).TotalMinutes.ToString();
            dr.Close();
            cn.Close();
        }
    }
}
