<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignment5_CSE445F25._Default" %>
<%@ Register Src="~/PlayerStatsControl.ascx" TagPrefix="uc" TagName="PlayerStats" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assignment 5 - Default</title>
    <style>
        .section { margin-top: 40px; }
        table { border-collapse: collapse; width: 100%; margin-top: 20px; }
        th, td { border: 1px solid #333; padding: 6px; }
        th { background: #f0f0f0; }
    </style>
</head>

<body>
<form id="form1" runat="server">

    <!-- PAGE TITLE -->
    <h1>Assignment 5 – Web Application Overview/Default Page</h1>

    <!-- From here, this paargraph is where I describe functionailty, purpose, how a grader should test the app, etc. -->
    <p>
        This web application introduces a football analysis application for the NFL where users can look up player and team statistics. It uses several layers of software intergation. On my end for the two local competents USer controls and global.asax and the intergation of a remote service was done through WCF for team stats.
        Users can look up NFL player statistics, retrieve team performance data from a remote service, and view application-level analytics such as total football sessions  started. When going to look at the application do View in Browser with the Default Page!
    </p>


    <!-- Just describve how the sign in or login function works-->
    <h2>How End Users Sign Up or Log In</h2>
    <p>
    Full user registration will be implemented in Assignment 6. For Assignment 5, a temporary 
    <strong>Login.aspx</strong> page is provided so users can simulate authentication.  
    When a user clicks the “Member” or “Staff” buttons below:
    </p>
<ul>
    <li>If the user is <strong>not logged in</strong>, the application redirects to <strong>Login.aspx</strong>.</li>
    <li>If the user <strong>is already logged in</strong>, the login page is skipped and 
        the user is taken directly to the Member or Staff page.</li>
</ul>
    <!-- Explanmtaion on how TA's and graders can test my code -->
<h2>How the TA/Grader Can Test This Application</h2>
<p>
    All components used in the application include visible <strong>TryIt</strong> interfaces 
    on this Default page. Each TryIt element tests a specific component:
</p>
<ul>
    <li><strong>Global.asax Application Event Component:</strong>  
        Click the "Show Football Sessions Count" button to test the 
        <em>TotalFootballSessions</em> application-level counter.</li>

    <li><strong>User Control (PlayerStatsControl.ascx):</strong>  
        Enter any NFL player name to test the local lookup logic.</li>

    <li><strong>Remote WCF Service (TeamStatsService):</strong>  
        Enter a team name to verify that the application correctly calls the deployed 
        remote WSDL service.</li>

    <li><strong>Login Session Component:</strong>  
        Click “Go to Member Page” or “Go to Staff Page” to test session-based access control.</li>
</ul>


    <!-- Test cases graders can run in order to see if things are working correctly-->
<h2>Test Cases and Example Inputs</h2>

<h3>1. Global.asax – Application Event Counter</h3>
<p>This tests the application-level counter <strong>TotalFootballSessions</strong>.</p>
<ul>
    <li><strong>Test Case 1</strong>: Click “Show Football Sessions Count” immediately after starting the site.<br />
        <strong>Expected Output:</strong> “Total Football Tracking Sessions Started: 1”</li>

    <li><strong>Test Case 2</strong>: Refresh the page and click the button again.<br />
        <strong>Expected Output:</strong> Count increases (e.g., “2”).</li>

    <li><strong>Test Case 3</strong>: Open the site in a second browser or incognito window, then click the button.<br />
        <strong>Expected Output:</strong> Count increases again (e.g., “3”). This proves it is application-level, not user-level.</li>
</ul>


<h3>2. Player Stats Lookup – User Control</h3>
<p>This tests the PlayerStatsControl for local lookup (with optional API fallback).</p>
<ul>
    <li><strong>Test Case 1</strong>: Input “Kyler Murray”.<br />
        <strong>Expected Output:</strong> Kyler Murray: QB — 228 passing yards, 2 TD, 1 INT, 36 rushing yards.</li>

    <li><strong>Test Case 2</strong>: Input “Justin Jefferson”.<br />
        <strong>Expected Output:</strong> Justin Jefferson: WR — 143 receiving yards, 2 TD</li>

    <li><strong>Test Case 3</strong>: Input “Unknown Player”.<br />
        <strong>Expected Output:</strong> “Player not found in football stats database.” .</li>
</ul>


<h3>3. Team Stats Lookup – Remote WCF Service</h3>
<p>This tests the deployed <strong>TeamStatsService.svc</strong> remote web service.</p>
<ul>
    <li><strong>Test Case 1</strong>: Input “Cardinals”.<br />
        <strong>Expected Output:</strong> “Arizona Cardinals — 24 points, 310 total yards, 2 turnovers.”</li>

    <li><strong>Test Case 2</strong>: Input “Chiefs”.<br />
        <strong>Expected Output:</strong> “Kansas City Chiefs — 27 points, 402 total yards, 0 turnovers.”</li>

    <li><strong>Test Case 3</strong>: Input “InvalidTeam123”.<br />
        <strong>Expected Output:</strong> “Team not found in stats database.”</li>
</ul>

    <!-- NAVIGATION -->
    <!-- The Memeber and Staff Pages arent due until Assignment 6, but the pdf says to include the buttons, so for now I have a temporay login in page, member page, and staff page. Along with the buttons to get in -->
    <!-- To access the login page you have to hit member page or staff page buttons. If your not logged in it willl ask you to, if you are it will take you to that page  -->
    <div class="section">
        <h2>Navigation</h2>
        <!-- Just the GUI buttons for accessing Member and Staff Pages -->
        <asp:Button ID="btnMember" runat="server" Text="Go to Member Page" OnClick="btnMember_Click" />
        <asp:Button ID="btnStaff" runat="server" Text="Go to Staff Page" OnClick="btnStaff_Click" />
    </div>

    <!-- GLOBAL.ASAX TRYIT: LOCAL COMPTENT  -->
    <div class="section">
        <h2>TryIt: Global.asax (Application Event Component)</h2>
        <p>This local component tracks total football tracking sessions started.</p>

        <asp:Button ID="btnShowSessions" runat="server" Text="Show Football Sessions Count" OnClick="btnShowSessions_Click" />
        <br /><br />
        <asp:Label ID="lblSessions" runat="server" ForeColor="DarkGreen" Font-Size="Large"></asp:Label>
    </div>

    <!-- PLAYER STATS USER CONTROL: TryIT function LOCAL COMPTENT -->
   <!-- Pdf says we can put the try it functions on the GUI if we want to thats what I did  -->
    <!-- Lookup a nfl players stats, refer to the cs file for the database of avaiable players you can search for  -->
    <div class="section">
        <h2>TryIt: Player Stats Lookup (User Control)</h2>
        <uc:PlayerStats ID="PlayerStats1" runat="server" />
    </div>

    <!-- TEAM STATS REMOTE SERVICE -->
    <!-- Looks up A TEAMS stats, exclusive onlu to nfl teams   -->
    <div class="section">
        <h2>TryIt: Team Stats Remote Service (WCF)</h2>

        Team Name:
        <asp:TextBox ID="txtTeam" runat="server"></asp:TextBox> <!-- Type the name of the team you want to search -->
        <asp:Button ID="btnTeamStats" runat="server" Text="Get Team Stats" OnClick="btnTeamStats_Click" /> <!-- GUI button to call on method which pulls from my dictionary the stats of the team were searching  -->

        <br /><br />

        <asp:Label ID="lblTeamStats" runat="server" ForeColor="Green"></asp:Label> <!-- Displays the stats of the team you searched  -->
    </div>

    <!-- SERVICE DIRECTORY TABLE -->
<div class="section">
    <h2>Service Directory (Components Summary)</h2>
    <asp:GridView ID="DirectoryTable" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Provider" DataField="Provider" />
            <asp:BoundField HeaderText="Page/Component" DataField="Component" />
            <asp:BoundField HeaderText="Component Type" DataField="Type" />
            <asp:BoundField HeaderText="Description" DataField="Description" />
            <asp:BoundField HeaderText="Implementation (Resources & Methods)" DataField="Implementation" />
            <asp:BoundField HeaderText="Used In" DataField="UsedIn" />
        </Columns>
    </asp:GridView>
</div>

</form>
</body>
</html>
