using AcxiomCRM.Models;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<Opportunity> Opportunities => Set<Opportunity>();
    public DbSet<FollowUp> FollowUps => Set<FollowUp>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Unique email/phone = duplicate customer prevention at database level too
        b.Entity<Customer>().HasIndex(c => c.Email).IsUnique();
        b.Entity<Customer>().HasIndex(c => c.Phone).IsUnique();
    }
}
