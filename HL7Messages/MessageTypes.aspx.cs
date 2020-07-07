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
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack))
            {
                ShowData();
            }
        }

        private void ShowData()
        {
            GridView1.DataSource = GetMessageTypes().Copy();
            GridView1.DataBind();
        }
            private DataTable GetMessageTypes()
        {
            dt = new DataTable("MessageTypes");
            if (File.Exists(Server.MapPath("MessageTypes.xml")) == true){
                dt.ReadXml(Server.MapPath("MessageTypes.xml"));
            }
            else
            {
            
                dt = CreateDataTable();
            }
            return dt;
        }
        private DataTable CreateDataTable()
        {
            dt = new DataTable("MessageTypes");
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

            DataRow dr1 = dt.NewRow();
            dr1["MessageType"] = "Sample Type1";
            dr1["ProcessToRun"] = "Sample Process1";
            dr1["SecurityValue"] = "Sample Security Value1";
            dr1["EngineTypeName"] = "Sample Engine Type1";
            dt.Rows.Add(dr1);
            return dt;
        }

       protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.   
            GridView1.EditIndex = e.NewEditIndex;
            ShowData();
        }
        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            //Finding the controls from Gridview for the row which is going to update   
            Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_Id") as Label;
            TextBox messagetype = GridView1.Rows[e.RowIndex].FindControl("txt_MessageType") as TextBox;
            TextBox processtorun = GridView1.Rows[e.RowIndex].FindControl("txt_ProcessToRun") as TextBox;
            TextBox securityvalue = GridView1.Rows[e.RowIndex].FindControl("txt_SecurityValue") as TextBox;
            TextBox enginetypename = GridView1.Rows[e.RowIndex].FindControl("txt_EngineTypeName") as TextBox;
            
            GridView1.EditIndex = -1;

            DataTable datatable = GetMessageTypes().Copy();
            DataRow dr = datatable.Select("Id=" + id.Text).FirstOrDefault(); 
            if (dr != null)
            {
                dr["MessageType"] = messagetype.Text;
                dr["ProcessToRun"] = processtorun.Text;
                dr["SecurityValue"] = securityvalue.Text;
                dr["EngineTypeName"] = enginetypename.Text;
            }
            datatable.AcceptChanges();
            datatable.WriteXml(Server.MapPath("MessageTypes.xml"), XmlWriteMode.WriteSchema);
            ShowData();
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview   
            GridView1.EditIndex = -1;
            ShowData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lbldeleteid = GridView1.Rows[e.RowIndex].FindControl("lbl_Id") as Label;
            DataTable datatable = GetMessageTypes().Copy();
            DataRow dr = datatable.Select("Id=" + lbldeleteid.Text).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
            if (dr != null)
            {
                dr.Delete();
            }
            datatable.AcceptChanges();
            datatable.WriteXml(Server.MapPath("MessageTypes.xml"), XmlWriteMode.WriteSchema);
            ShowData();
        }
    }
}