using AcxiomCRM.Data;
using AcxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Controllers;

public class OpportunitiesController : Controller
{
    private readonly AppDbContext _db;
    public OpportunitiesController(AppDbContext db) => _db = db;

    // List + search by name/customer + filter by stage
    public async Task<IActionResult> Index(string? q, string? stage)
    {
        var query = _db.Opportunities.Include(o => o.Customer).AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(o => o.Name.Contains(q) || o.Customer!.Name.Contains(q));
        if (!string.IsNullOrWhiteSpace(stage))
            query = query.Where(o => o.Stage == stage);
        ViewData["q"] = q;
        ViewData["stage"] = stage;
        return View(await query.OrderByDescending(o => o.CreatedDate).ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        await LoadCustomers();
        return View(new Opportunity());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Opportunity model)
    {
        if (!ModelState.IsValid) { await LoadCustomers(); return View(model); }
        _db.Opportunities.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Opportunity created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var o = await _db.Opportunities.FindAsync(id);
        if (o == null) return NotFound();
        await LoadCustomers();
        return View(o);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Opportunity model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid) { await LoadCustomers(); return View(model); }
        _db.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Opportunity updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var o = await _db.Opportunities.FindAsync(id);
        if (o != null)
        {
            _db.Opportunities.Remove(o);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Opportunity deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCustomers() =>
        ViewBag.Customers = new SelectList(await _db.Customers.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");
}
