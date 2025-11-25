using System;
using System.Web.UI;

namespace Assignment5_CSE445F25
{
    // Code-behind for Login.aspx: handles simple session-based authentication.
    // This page sets a username into Session state; no persistence or password logic.
    public partial class Login : Page
    {
        // Click handler for the Login button.
        // Flow:
        // 1. Validate that the user entered non-whitespace text.
        // 2. Store trimmed username in Session (per-user, lasts for the session lifetime).
        // 3. Redirect to Default.aspx to continue inside the site.
        // 4. If input invalid, display a message instead of redirecting.
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Guard against empty / whitespace-only input.
            if (!string.IsNullOrWhiteSpace(txtUser.Text))
            {
                // Store the username in Session so other pages can check authentication.
                Session["user"] = txtUser.Text.Trim();

                // Issue an HTTP redirect to the landing page; ends current request.
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMsg.Text = "Please enter a username.";
            }
        }
    }
}