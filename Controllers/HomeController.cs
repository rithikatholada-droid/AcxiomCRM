using AcxiomCRM.Data;
using AcxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    public HomeController(AppDbContext db) => _db = db;

    // Dashboard
    public async Task<IActionResult> Index()
    {
        var vm = new DashboardViewModel
        {
            TotalCustomers = await _db.Customers.CountAsync(),
            TotalLeads = await _db.Leads.CountAsync(),
            OpenOpportunities = await _db.Opportunities.CountAsync(o => o.Stage != "Won" && o.Stage != "Lost"),
            WonOpportunities = await _db.Opportunities.CountAsync(o => o.Stage == "Won"),
            LostOpportunities = await _db.Opportunities.CountAsync(o => o.Stage == "Lost"),
            PendingFollowUps = await _db.FollowUps.CountAsync(f => f.Status == "Planned"),
            PipelineValue = await _db.Opportunities
                .Where(o => o.Stage != "Won" && o.Stage != "Lost").SumAsync(o => (double?)o.Amount) ?? 0,
            Upcoming = await _db.FollowUps.Include(f => f.Customer).Include(f => f.Lead)
                .Where(f => f.Status == "Planned").OrderBy(f => f.FollowUpDate).Take(5).ToListAsync()
        };

        foreach (var s in Options.LeadStatuses)
        {
            vm.LeadLabels.Add(s);
            vm.LeadCounts.Add(await _db.Leads.CountAsync(l => l.Status == s));
        }
        foreach (var s in Options.Stages)
        {
            vm.StageLabels.Add(s);
            vm.StageCounts.Add(await _db.Opportunities.CountAsync(o => o.Stage == s));
        }
        return View(vm);
    }
}
