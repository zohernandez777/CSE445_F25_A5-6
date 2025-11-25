<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlayerStatsControl.ascx.cs" Inherits="Assignment5_CSE445F25.WebUserControl1" %>

<%-- 
    PlayerStatsControl.ascx
    Purpose:
      - Reusable Web Forms user control that lets a user enter an NFL player name and fetch a stats summary.
    How it works:
      - The TextBox (txtPlayer) collects the player's name.
      - The Button (btnLookup) triggers a postback and executes btnLookup_Click in PlayerStatsControl.ascx.cs.
      - The Label (lblStats) displays the result (summary or not-found/error).
--%>

<%-- Section header shown on the page --%>
<h3>Football Player Stat Lookup</h3>

<%-- Duplicate header kept intentionally to match the current visual layout --%>
<h3>Football Player Stat Lookup</h3>

<%-- Input: user enters a player name (e.g., "Patrick Mahomes"). Placeholder guides the expected input. --%>
<asp:TextBox ID="txtPlayer" runat="server" Placeholder="Enter player name"></asp:TextBox>

<%-- Action: triggers server-side lookup in btnLookup_Click (code-behind). Causes a postback. --%>
<asp:Button ID="btnLookup" runat="server" Text="Lookup Stats" OnClick="btnLookup_Click" />

<br /><br />

<%-- Output: displays the formatted stats, or a not-found/error message after lookup. --%>
<asp:Label ID="lblStats" runat="server" ForeColor="DarkGreen" Font-Size="Large"></asp:Label>
