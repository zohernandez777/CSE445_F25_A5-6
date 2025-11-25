using System;
using System.Collections.Generic;   
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// README: this is the default page that displays the component directory and exposes TryIt features.

namespace Assignment5_CSE445F25
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only build and bind the directory on the initial page load.
            if (!IsPostBack)
            {
                LoadDirectory();
            }
        }

        // Loads the simplified service directory with required fields.
        private void LoadDirectory()
        {
            var dt = new DataTable();

            dt.Columns.Add("Provider");
            dt.Columns.Add("Component");     // Page/Component
            dt.Columns.Add("Type");          // Component Type
            dt.Columns.Add("Description");   // Component Description
            dt.Columns.Add("Implementation"); // Actual resources and methods
            dt.Columns.Add("UsedIn");        // Where the component is used

            // 1) Player Stats User Control
            dt.Rows.Add(
                "Alonzo Hernandez",
                "PlayerStatsControl.ascx",
                "User Control (ASCX)",
                "Local player stats lookup with in-memory data and fuzzy matching.",
                "PlayerStatsControl.ascx.cs: btnLookup_Click, FindBestKey, Normalize, Levenshtein",
                "Default.aspx › TryIt: Player Stats Lookup"
            );

            // 2) WCF Team Stats Service
            dt.Rows.Add(
                "Alonzo Hernandez",
                "TeamStatsService.svc / ITeamStatsService",
                "WSDL/WCF Service",
                "Returns a formatted NFL team stats summary by name or alias.",
                "ITeamStatsService.GetTeamStats(string); TeamStatsService.GetTeamStats(string); client: TeamStatsProxy.TeamStatsServiceClient",
                "Default.aspx › TryIt: Team Stats (btnTeamStats_Click)"
            );

            // 3) Global Application Events (Session counter)
            dt.Rows.Add(
                "Alonzo Hernandez",
                "Global.asax",
                "Application Event (Session_Start)",
                "Tracks total sessions started across the application.",
                "Global.asax.cs: Session_Start updates Application[\"TotalFootballSessions\"]; Default.aspx.cs: btnShowSessions_Click reads the value",
                "Default.aspx › TryIt: Global.asax counter"
            );

            // 4) Login Session Component
            dt.Rows.Add(
                "Alonzo Hernandez",
                "Login.aspx",
                "Web Form (Session Auth)",
                "Simulates authentication by storing a username in Session and redirecting.",
                "Login.aspx.cs: btnLogin_Click sets Session[\"user\"] and Response.Redirect(\"Default.aspx\")",
                "Accessed when clicking Member/Staff if not logged in"
            );

            // 5) Member Page (protected)
            dt.Rows.Add(
                "Alonzo Hernandez",
                "Member.aspx",
                "Protected Page",
                "Displays a welcome message for authenticated users; allows logout.",
                "Member.aspx.cs: Page_Load checks Session[\"user\"]; btnLogout_Click calls Session.Abandon()",
                "Navigated via Default.aspx › Go to Member Page"
            );

            // 6) Staff Page (protected)
            dt.Rows.Add(
                "Alonzo Hernandez",
                "Staff.aspx",
                "Protected Page",
                "Displays staff section banner for authenticated users; allows logout.",
                "Staff.aspx.cs: Page_Load checks Session[\"user\"]; btnLogout_Click abandons session",
                "Navigated via Default.aspx › Go to Staff Page"
            );

            // 7) Navigation logic (routing to Member/Staff/Login)
            dt.Rows.Add(
                "Alonzo Hernandez",
                "Default.aspx (Navigation)",
                "Web Form UI logic",
                "Routes user to Member/Staff or prompts login based on Session.",
                "Default.aspx.cs: btnMember_Click, btnStaff_Click",
                "Default.aspx › Navigation section"
            );

            DirectoryTable.DataSource = dt;
            DirectoryTable.DataBind();
        }

        // Navigates to Member page if a user session exists; otherwise forces login.
        protected void btnMember_Click(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("Login.aspx");
            else
                Response.Redirect("Member.aspx");
        }

        // Navigates to Staff page if a user session exists; otherwise forces login.
        protected void btnStaff_Click(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("Login.aspx");
            else
                Response.Redirect("Staff.aspx");
        }

        // TryIt handler: displays the application-level TotalFootballSessions count.
        protected void btnShowSessions_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(Application["TotalFootballSessions"] ?? 0);
            lblSessions.Text = $"Total Football Tracking Sessions Started: {count}";
        }

        // Invokes the WCF TeamStats service via the generated proxy to retrieve team stats.
        protected void btnTeamStats_Click(object sender, EventArgs e)
        {
            string team = txtTeam.Text.Trim();

            var client = new TeamStatsProxy.TeamStatsServiceClient();
            lblTeamStats.Text = client.GetTeamStats(team);
            client.Close();
        }
    }
}