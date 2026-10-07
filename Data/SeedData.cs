using AcxiomCRM.Models;

namespace AcxiomCRM.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Customers.Any()) return;

        db.Customers.AddRange(
            new Customer { Name = "Ravi Kumar", Email = "ravi@techsoft.com", Phone = "9876543210", Company = "TechSoft", City = "Hyderabad" },
            new Customer { Name = "Priya Sharma", Email = "priya@greenleaf.in", Phone = "9123456780", Company = "GreenLeaf", City = "Bengaluru" },
            new Customer { Name = "Anil Reddy", Email = "anil@buildwell.com", Phone = "9988776655", Company = "BuildWell", City = "Vijayawada" });

        db.Leads.AddRange(
            new Lead { Name = "Sneha Rao", Email = "sneha@startup.io", Phone = "9000011111", Company = "StartupIO", Source = "Website", Status = "New", ExpectedValue = 50000 },
            new Lead { Name = "Kiran Patel", Email = "kiran@retailhub.in", Phone = "9000022222", Company = "RetailHub", Source = "Referral", Status = "Qualified", ExpectedValue = 120000 },
            new Lead { Name = "Meena Iyer", Email = "meena@edusmart.in", Phone = "9000033333", Company = "EduSmart", Source = "Event", Status = "Contacted", ExpectedValue = 80000 });
        db.SaveChanges();

        var c = db.Customers.ToList();
        db.Opportunities.AddRange(
            new Opportunity { Name = "ERP Implementation", CustomerId = c[0].Id, Amount = 500000, Stage = "Proposal", Probability = 60 },
            new Opportunity { Name = "Website Redesign", CustomerId = c[1].Id, Amount = 150000, Stage = "Qualification", Probability = 30 },
            new Opportunity { Name = "Annual Support Contract", CustomerId = c[2].Id, Amount = 250000, Stage = "Won", Probability = 100, ExpectedCloseDate = DateTime.Today.AddDays(-5) },
            new Opportunity { Name = "Mobile App", CustomerId = c[0].Id, Amount = 300000, Stage = "Negotiation", Probability = 80 });

        db.FollowUps.AddRange(
            new FollowUp { Subject = "Demo call", FollowUpDate = DateTime.Today, Type = "Call", CustomerId = c[0].Id },
            new FollowUp { Subject = "Send proposal", FollowUpDate = DateTime.Today.AddDays(2), Type = "Email", CustomerId = c[1].Id },
            new FollowUp { Subject = "Site meeting", FollowUpDate = DateTime.Today.AddDays(4), Type = "Meeting", CustomerId = c[2].Id });
        db.SaveChanges();
    }
}
