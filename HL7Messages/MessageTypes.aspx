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


        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowCancelingEdit="GridView1_RowCancelingEdit"    

OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" ShowFooter="true" OnRowCommand="GridView1_RowCommand"   
      BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="3">
                <%-- Theme Properties --%>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <Columns>   
 
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
                    <FooterTemplate>
                        <asp:TextBox ID="txt_MessageTypeFooter" runat="server"></asp:TextBox>   
                    </FooterTemplate>
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="SecurityValue">   
                        <ItemTemplate>   
                            <asp:Label ID="lbl_SecurityValue" runat="server" Text='<%#Eval("SecurityValue") %>'></asp:Label>   
                        </ItemTemplate>   
                        <EditItemTemplate>   
                            <asp:TextBox ID="txt_SecurityValue" runat="server" Text='<%#Eval("SecurityValue") %>'></asp:TextBox>   
                        </EditItemTemplate>   
                    <FooterTemplate>
                            <asp:TextBox ID="txt_SecurityValueFooter" runat="server"></asp:TextBox>   
                    </FooterTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>   
        </asp:GridView> 
            <br />
            <asp:Label ID="lblStatusMessage" Text="" runat="server"/>
            <br />
            <asp:Label ID="lblErrorMessage" Text="" ForeColor="Red" runat="server"/>

        </div>
    </form>
</body>
</html>
