<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="Assignment5_CSE445F25.Member" %>
<%@ Register Src="~/PlayerStatsControl.ascx" TagName="PlayerStats" TagPrefix="uc" %>


<!-- Once logged in you get access to meber page--> 
<!DOCTYPE html>
<html>
<body>
<form id="form1" runat="server">
<h2>Member Page (Placeholder)</h2>

<asp:Label ID="lblWelcome" runat="server"></asp:Label>
<br />
    <!-- Include the player lookup service for testing but you can find this in default intergartion--> 
<asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    <uc:PlayerStats ID="PlayerStatsControl" runat="server" />

</form>
</body>
</html>
