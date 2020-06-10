<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageTypes.aspx.cs" Inherits="HL7Messages.MessageTypes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="grdMessageTypes" runat="server" Caption="MessageTypes">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="Id" />
                    <asp:BoundField DataField="MessageType" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
