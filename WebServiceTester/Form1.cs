using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace WebServiceTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        HL7WebServices.HL7MessageReceiverSoapClient ws = new HL7WebServices.HL7MessageReceiverSoapClient();
        private void button1_Click(object sender, EventArgs e)

        {
            //CalRedieServices.CalREDIEServicesSoapClient ws = new CalRedieServices.CalREDIEServicesSoapClient();
            textBox2.Text=ws.AddHL7MessageToWarehouse("ADT Message","Security",textBox1.Text);

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("Data Source=IntegrationDB;Initial Catalog=HL7Data;Integrated Security=True; timeout= 120");
            SqlCommand cm = new SqlCommand("Select Top 1 * from tblADT", cn);
            SqlDataReader dr;
            cn.Open();
            dr = cm.ExecuteReader();
            dr.Read();
            textBox2.Text = ws.AddHL7MessageToWarehouse("ADT Message", "Security", dr["HL7Data"].ToString());
            dr.Close();
            cn.Close();
        }
    }
}
