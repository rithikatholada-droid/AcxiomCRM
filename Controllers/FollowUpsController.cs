using AcxiomCRM.Data;
using AcxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Controllers;

public class FollowUpsController : Controller
{
    private readonly AppDbContext _db;
    public FollowUpsController(AppDbContext db) => _db = db;

    // List + search by subject/customer/lead + filter by status
    public async Task<IActionResult> Index(string? q, string? status)
    {
        var query = _db.FollowUps.Include(f => f.Customer).Include(f => f.Lead).AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(f => f.Subject.Contains(q)
                || (f.Customer != null && f.Customer.Name.Contains(q))
                || (f.Lead != null && f.Lead.Name.Contains(q)));
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(f => f.Status == status);
        ViewData["q"] = q;
        ViewData["status"] = status;
        return View(await query.OrderBy(f => f.FollowUpDate).ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        await LoadLists();
        return View(new FollowUp());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FollowUp model)
    {
        if (model.CustomerId == null && model.LeadId == null)
            ModelState.AddModelError("", "Select a customer or a lead for this follow-up.");
        if (!ModelState.IsValid) { await LoadLists(); return View(model); }
        _db.FollowUps.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Follow-up scheduled.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var f = await _db.FollowUps.FindAsync(id);
        if (f == null) return NotFound();
        await LoadLists();
        return View(f);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, FollowUp model)
    {
        if (id != model.Id) return NotFound();
        if (model.CustomerId == null && model.LeadId == null)
            ModelState.AddModelError("", "Select a customer or a lead for this follow-up.");
        if (!ModelState.IsValid) { await LoadLists(); return View(model); }
        _db.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Follow-up updated.";
        return RedirectToAction(nameof(Index));
    }

    // One-click "Mark as completed"
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(int id)
    {
        var f = await _db.FollowUps.FindAsync(id);
        if (f != null)
        {
            f.Status = "Completed";
            await _db.SaveChangesAsync();
            TempData["Success"] = "Follow-up marked as completed.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var f = await _db.FollowUps.FindAsync(id);
        if (f != null)
        {
            _db.FollowUps.Remove(f);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Follow-up deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadLists()
    {
        ViewBag.Customers = new SelectList(await _db.Customers.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");
        ViewBag.Leads = new SelectList(await _db.Leads.OrderBy(l => l.Name).ToListAsync(), "Id", "Name");
    }
}
