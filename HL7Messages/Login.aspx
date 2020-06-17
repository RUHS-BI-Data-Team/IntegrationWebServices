<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="InterfaceDataFiltersWeb.Login" %> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblUserName" runat="server" Text="Employee ID :" AssociatedControlID="UsernameTextBox" CssClass="span-3"></asp:Label><asp:TextBox ID="UsernameTextBox" runat="server" CssClass="span-4 bypass-state-tracking" />
    </div>
    <div>    
        <asp:Label ID="lblPassword" runat="server" Text="Password :" AssociatedControlID="PasswordTextBox" CssClass="span-3"></asp:Label><asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="span-4 bypass-state-tracking" />
    </div>
    <div>
        <asp:Button ID="SignInButton" runat="server" Text="Sign In" OnClick="SignInButton_Click"/>
    </div>
    </form>
</body>
</html>
