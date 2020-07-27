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
            Intgn.Libraries.Security.UserAccount usr = (Intgn.Libraries.Security.UserAccount)(User.Identity);

            //if (!((usr.Roles.Contains(System.Web.Configuration.WebConfigurationManager.AppSettings["grpIntegrationAdmin"].ToString()))))
            String grp = System.Web.Configuration.WebConfigurationManager.AppSettings["grpIntegrationAdmin"].ToString();
            Boolean flag = false;
            string[] role = usr.Roles.Split(',');
            for (int i = 0; i < role.Length; i++)
            {
               string[] words = role[i].Split('=');
                if (words[1] == grp)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                if (!(Page.IsPostBack))
                {
                    ShowData();
                }
            }
            else
            {
                Response.Redirect("DoNotHaveAccess.aspx");
            }
        }

        private void ShowData()
        {
            //Store the DataTable in ViewState
            DataTable tbl = GetMessageTypes().Copy();
            GridView1.DataSource = tbl;
            GridView1.DataBind();
            ClearMsgs();
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
            dt.Columns.Add(new DataColumn("SecurityValue", typeof(String)));
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementStep = 1;
            dt.Columns["Id"].AutoIncrementSeed = 1;

            DataRow dr = dt.NewRow();
            dr["MessageType"] = "Sample Type";
            dr["SecurityValue"] = "Sample Security Value";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["MessageType"] = "Sample Type1";
            dr1["SecurityValue"] = "Sample Security Value1";
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
            TextBox securityvalue = GridView1.Rows[e.RowIndex].FindControl("txt_SecurityValue") as TextBox;

            if ((messagetype.Text.Length == 0) || (securityvalue.Text.Length == 0))
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
                    dr["SecurityValue"] = securityvalue.Text;
                }
                datatable.AcceptChanges();
                datatable.WriteXml(Server.MapPath("MessageTypes.xml"), XmlWriteMode.WriteSchema);
                ShowData();
            }
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
            if (datatable.Rows.Count > 1)
            {
                DataRow dr = datatable.Select("Id=" + lbldeleteid.Text).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (dr != null)
                {
                    dr.Delete();
                }
                datatable.AcceptChanges();
                datatable.WriteXml(Server.MapPath("MessageTypes.xml"), XmlWriteMode.WriteSchema);
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
                TextBox messagetypeFooter = (TextBox)(GridView1.FooterRow.FindControl("txt_MessageTypeFooter"));
                TextBox securityvalueFooter = (GridView1.FooterRow.FindControl("txt_SecurityValueFooter") as TextBox);

                DataTable datatable = GetMessageTypes().Copy();
                DataRow dr = null;
                dr = datatable.NewRow();
                if (dr != null)
                {
                    if ((messagetypeFooter.Text.Length == 0) || (securityvalueFooter.Text.Length == 0))
                    {
                        lblErrorMessage.Text = "Please enter value in all the columns";
                    }
                    else
                    {
                        dr["Id"] = datatable.Rows.Count + 1;
                        dr["MessageType"] = messagetypeFooter.Text;
                        dr["SecurityValue"] = securityvalueFooter.Text;

                        datatable.Rows.Add(dr);
                        datatable.AcceptChanges();
                        datatable.WriteXml(Server.MapPath("MessageTypes.xml"), XmlWriteMode.WriteSchema);
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