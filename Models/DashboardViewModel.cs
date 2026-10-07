namespace AcxiomCRM.Models;

public class DashboardViewModel
{
    public int TotalCustomers { get; set; }
    public int TotalLeads { get; set; }
    public int OpenOpportunities { get; set; }
    public int WonOpportunities { get; set; }
    public int LostOpportunities { get; set; }
    public int PendingFollowUps { get; set; }
    public double PipelineValue { get; set; }

    public List<string> LeadLabels { get; set; } = new();
    public List<int> LeadCounts { get; set; } = new();
    public List<string> StageLabels { get; set; } = new();
    public List<int> StageCounts { get; set; } = new();

    public List<FollowUp> Upcoming { get; set; } = new();
}
