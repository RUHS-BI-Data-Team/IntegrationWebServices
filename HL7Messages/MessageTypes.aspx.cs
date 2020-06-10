using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace HL7Messages
{
    public partial class MessageTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdMessageTypes.DataSource = GetMessageTypes().Copy();
            grdMessageTypes.DataBind();

        }
        private DataTable GetMessageTypes()
        {
            DataTable dt = new DataTable("MessageTypes");
            if(File.Exists("MessageTypes.xml") == true){
                dt.ReadXml("MessageTypes.xml");
            }
            else
            {
                dt = CreateDataTable();
            }
            return dt;
        }
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable("MessageType");
            dt.Columns.Add(new DataColumn("Id", typeof(Int16)));
            dt.Columns.Add(new DataColumn("MessageType", typeof(String)));
            dt.Columns.Add(new DataColumn("ProcessToRun", typeof(String)));
            dt.Columns.Add(new DataColumn("SecurityValue", typeof(String)));
            dt.Columns.Add(new DataColumn("EngineTypeName", typeof(String)));
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementStep = 1;
            dt.Columns["Id"].AutoIncrementSeed = 1;

            DataRow dr = dt.NewRow();
            dr["MessageType"] = "Sample Type";
            dr["ProcessToRun"] = "Sample Process";
            dr["SecurityValue"] = "Sample Security Value";
            dr["EngineTypeName"] = "Sample Engine Type";
            dt.Rows.Add(dr);
            return dt;
        }
        
    }
}