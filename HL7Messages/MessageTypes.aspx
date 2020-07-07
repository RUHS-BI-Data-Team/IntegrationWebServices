<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageTypes.aspx.cs" Inherits="HL7Messages.MessageTypes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
<%--            <asp:GridView ID="grdMessageTypes" runat="server" Caption="MessageTypes" OnRowCommand="grdMessageTypes_RowCommand" OnRowDeleting="grdMessageTypes_RowDeleting" OnRowEditing="grdMessageTypes_RowEditing" OnRowCancelingEdit="grdMessageTypes_RowCancelingEdit" OnRowUpdating="grdMessageTypes_RowUpdating">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="Id" />
                    <asp:BoundField DataField="MessageType" />
                 </Columns>
            </asp:GridView>--%>


        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" OnRowCancelingEdit="GridView1_RowCancelingEdit"    

OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">   
            <Columns>   
                <asp:TemplateField>   
                    <ItemTemplate>   
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>   
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete"/>   
                    </EditItemTemplate>   
                </asp:TemplateField>   
<%--                <asp:CommandField ShowEditButton="true" />  
                <asp:CommandField ShowDeleteButton="true" />--%>
                <asp:TemplateField HeaderText="ID">   
                    <ItemTemplate>   
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>   
                    </ItemTemplate>   
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="MessageType">   
                    <ItemTemplate>   
                        <asp:Label ID="lbl_MessageType" runat="server" Text='<%#Eval("MessageType") %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:TextBox ID="txt_MessageType" runat="server" Text='<%#Eval("MessageType") %>'></asp:TextBox>   
                    </EditItemTemplate>   
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="ProcessToRun">   
                    <ItemTemplate>   
                        <asp:Label ID="lbl_ProcessToRun" runat="server" Text='<%#Eval("ProcessToRun") %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:TextBox ID="txt_ProcessToRun" runat="server" Text='<%#Eval("ProcessToRun") %>'></asp:TextBox>   
                    </EditItemTemplate>   
                </asp:TemplateField>   
             <asp:TemplateField HeaderText="SecurityValue">   
                    <ItemTemplate>   
                        <asp:Label ID="lbl_SecurityValue" runat="server" Text='<%#Eval("SecurityValue") %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:TextBox ID="txt_SecurityValue" runat="server" Text='<%#Eval("SecurityValue") %>'></asp:TextBox>   
                    </EditItemTemplate>   
                </asp:TemplateField>  
             <asp:TemplateField HeaderText="EngineTypeName">   
                    <ItemTemplate>   
                        <asp:Label ID="lbl_EngineTypeName" runat="server" Text='<%#Eval("EngineTypeName") %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:TextBox ID="txt_EngineTypeName" runat="server" Text='<%#Eval("EngineTypeName") %>'></asp:TextBox>   
                    </EditItemTemplate>   
                </asp:TemplateField>                   
            </Columns>   
        </asp:GridView> 
        </div>
    </form>
</body>
</html>
