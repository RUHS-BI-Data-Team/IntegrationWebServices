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
            //Store the DataTable in ViewState
            DataTable tbl = GetMessageTypes().Copy();
            GridView1.DataSource = tbl;
            GridView1.DataBind();
        }
            private DataTable GetMessageTypes()
        {
            dt = new DataTable("MessageTypes");
            if (File.Exists(Server.MapPath("MessageTypes.xml")) == true){
                {
                    dt.ReadXml(Server.MapPath("MessageTypes.xml"));
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        dt.Rows.Add(dt.NewRow());
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView1.Rows[0].Cells.Clear();
                        GridView1.Rows[0].Cells.Add(new TableCell());
                        GridView1.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                        GridView1.Rows[0].Cells[0].Text = "No Data Found ..!";
                        GridView1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    }

                }

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

     
        //protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DataTable dt = (DataTable)ViewState["CurrentTable"];
        //        LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
        //        if (lb != null)
        //        {
        //            if (dt.Rows.Count > 1)
        //            {
        //                if (e.Row.RowIndex == dt.Rows.Count - 1)
        //                {
        //                    lb.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                lb.Visible = false;
        //            }
        //        }
        //        ClearMsgs();
        //    }
        //}
        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.   
            GridView1.EditIndex = e.NewEditIndex;
            ClearMsgs();
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

            if ((messagetype.Text.Length == 0) || (processtorun.Text.Length == 0) || (securityvalue.Text.Length == 0) || (enginetypename.Text.Length == 0))
            {
                lblErrorMessage.Text = "Please enter value in all the columns";
            }
            else
            {
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
                ClearMsgs();
                ShowData();
            }
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview   
            GridView1.EditIndex = -1;
            ClearMsgs();
            ShowData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lbldeleteid = GridView1.Rows[e.RowIndex].FindControl("lbl_Id") as Label;
            DataTable datatable = GetMessageTypes().Copy();
            if (datatable.Rows.Count > 1)
            {
                DataRow dr = datatable.Select("Id=" + lbldeleteid.Text).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (dr != null)
                {
                    dr.Delete();
                }
                datatable.AcceptChanges();
                datatable.WriteXml(Server.MapPath("MessageTypes.xml"), XmlWriteMode.WriteSchema);
                ClearMsgs();
                ShowData();
            }
            else
            {
                    lblStatusMessage.Text = "There needs to be at least one row in the table";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                DataTable datatable = GetMessageTypes().Copy();

                DataRow dr = null;
                dr = datatable.NewRow();
                if (dr != null)
                {

                    TextBox messagetypeFooter = (GridView1.FooterRow.FindControl("txt_MessageTypeFooter") as TextBox);
                    TextBox processtorunFooter = (GridView1.FooterRow.FindControl("txt_ProcessToRunFooter") as TextBox);
                    TextBox securityvalueFooter = (GridView1.FooterRow.FindControl("txt_SecurityValueFooter") as TextBox);
                    TextBox enginetypenameFooter = (GridView1.FooterRow.FindControl("txt_EngineTypeNameFooter") as TextBox);
                    if ((messagetypeFooter.Text.Length == 0) || (processtorunFooter.Text.Length == 0) || (securityvalueFooter.Text.Length == 0) || (enginetypenameFooter.Text.Length == 0))
                    {
                        lblErrorMessage.Text = "Please enter value in all the columns";
                    }
                    else
                    {
                        dr["Id"] = datatable.Rows.Count + 1;
                        dr["MessageType"] = messagetypeFooter.Text;
                        dr["ProcessToRun"] = processtorunFooter.Text;
                        dr["SecurityValue"] = securityvalueFooter.Text;
                        dr["EngineTypeName"] = enginetypenameFooter.Text;

                        datatable.Rows.Add(dr);
                        datatable.AcceptChanges();
                        datatable.WriteXml(Server.MapPath("MessageTypes.xml"), XmlWriteMode.WriteSchema);
                        ClearMsgs();
                        ShowData();
                    }
                }
            }
        }
        protected void ClearMsgs()
        {
            lblStatusMessage.Text = "";
            lblErrorMessage.Text = "";
         }
    }
}