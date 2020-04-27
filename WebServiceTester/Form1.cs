using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string FileName;
            DialogResult rst;
            rst = OpenXMLFile.ShowDialog();
            if (rst.ToString() == "OK")
            {
                FileName = OpenXMLFile.FileName;
                textBox1.Text = System.IO.File.ReadAllText(FileName);
                //textBox2.Text = ws.CalREDIEHL7MessageReceipt(System.IO.File.ReadAllText(FileName));
            }

        }
    }
}
