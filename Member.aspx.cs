using System;
using System.Web.UI;

namespace Assignment5_CSE445F25
{
    // Code-behind for Member.aspx.
    // Displays a welcome message for the authenticated user and allows logout.
    public partial class Member : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Basic auth check: if there is no user in Session, force navigation to Login.
            // This prevents direct URL access to Member.aspx without logging in.
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
                return; // Ensure no further processing after redirect.
            }

            // Safe to display the welcome message (Session["user"] was set in Login.aspx).
            lblWelcome.Text = "Welcome, " + Session["user"];
        }

        // Handles logout button click.
        // Abandons the current session (clears all session data) and returns to the public page.
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();          // Clear all session variables and end the session.
            Response.Redirect("Default.aspx"); // Navigate back to landing page.
        }
    }
}