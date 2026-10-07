namespace AcxiomCRM.Models;

// Dropdown values used across the app (single place to change them)
public static class Options
{
    public static readonly string[] CustomerStatuses = { "Active", "Inactive" };
    public static readonly string[] LeadStatuses = { "New", "Contacted", "Qualified", "Unqualified", "Converted", "Lost" };
    public static readonly string[] LeadSources = { "Website", "Referral", "Cold Call", "Social Media", "Event" };
    public static readonly string[] Stages = { "Qualification", "Proposal", "Negotiation", "Won", "Lost" };
    public static readonly string[] FollowUpTypes = { "Call", "Meeting", "Email", "Task" };
    public static readonly string[] FollowUpStatuses = { "Planned", "Completed", "Missed", "Cancelled" };

    // Maps a status text to a Bootstrap badge colour
    public static string Badge(string s) => s switch
    {
        "Active" or "Won" or "Completed" or "Converted" => "success",
        "Qualified" or "Negotiation" => "primary",
        "Contacted" or "Proposal" or "Planned" => "info",
        "Lost" or "Missed" or "Inactive" or "Unqualified" => "danger",
        "Cancelled" => "secondary",
        _ => "warning"
    };
}
