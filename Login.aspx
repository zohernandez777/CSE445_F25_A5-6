<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignment5_CSE445F25.Login" %>

<!DOCTYPE html>
<html>
<body>
<form id="form1" runat="server">

<h2>Temporary Login (Assignment 5 Part 1)</h2>

    <!-- JUst the placeholder login page for now, ask fort a username and button to log you in, grabnts access to different pages-->
<asp:TextBox ID="txtUser" runat="server" placeholder="Enter username"></asp:TextBox>
<asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
<asp:Label ID="lblMsg" runat="server"></asp:Label>

</form>
</body>
</html>

