using AcxiomCRM.Data;
using AcxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Controllers;

public class LeadsController : Controller
{
    private readonly AppDbContext _db;
    public LeadsController(AppDbContext db) => _db = db;

    // List + search by name/company + filter by status
    public async Task<IActionResult> Index(string? q, string? status)
    {
        var query = _db.Leads.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(l => l.Name.Contains(q) || (l.Company != null && l.Company.Contains(q)));
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(l => l.Status == status);
        ViewData["q"] = q;
        ViewData["status"] = status;
        return View(await query.OrderByDescending(l => l.CreatedDate).ToListAsync());
    }

    public IActionResult Create() => View(new Lead());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Lead model)
    {
        if (!Options.LeadStatuses.Contains(model.Status))
            ModelState.AddModelError(nameof(model.Status), "Invalid lead status.");
        if (!ModelState.IsValid) return View(model);
        _db.Leads.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Lead created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var l = await _db.Leads.FindAsync(id);
        return l == null ? NotFound() : View(l);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Lead model)
    {
        if (id != model.Id) return NotFound();
        if (!Options.LeadStatuses.Contains(model.Status))
            ModelState.AddModelError(nameof(model.Status), "Invalid lead status.");
        if (!ModelState.IsValid) return View(model);
        _db.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Lead updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var l = await _db.Leads.FindAsync(id);
        if (l != null)
        {
            _db.Leads.Remove(l);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Lead deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    // Lead -> Customer conversion (only for Qualified leads)
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Convert(int id)
    {
        var l = await _db.Leads.FindAsync(id);
        if (l == null) return NotFound();
        if (l.Status != "Qualified")
        {
            TempData["Error"] = "Only Qualified leads can be converted.";
            return RedirectToAction(nameof(Index));
        }
        if (await _db.Customers.AnyAsync(c => c.Email == l.Email || c.Phone == l.Phone))
        {
            TempData["Error"] = "A customer with this email or phone already exists.";
            return RedirectToAction(nameof(Index));
        }
        _db.Customers.Add(new Customer { Name = l.Name, Email = l.Email, Phone = l.Phone, Company = l.Company });
        l.Status = "Converted";
        await _db.SaveChangesAsync();
        TempData["Success"] = "Lead converted to customer.";
        return RedirectToAction(nameof(Index));
    }
}
