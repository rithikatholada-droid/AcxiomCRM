using AcxiomCRM.Data;
using AcxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcxiomCRM.Controllers;

public class CustomersController : Controller
{
    private readonly AppDbContext _db;
    public CustomersController(AppDbContext db) => _db = db;

    // List + search by name, email, phone or company
    public async Task<IActionResult> Index(string? q)
    {
        var query = _db.Customers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(c => c.Name.Contains(q) || c.Email.Contains(q) || c.Phone.Contains(q)
                                  || (c.Company != null && c.Company.Contains(q)));
        ViewData["q"] = q;
        return View(await query.OrderByDescending(c => c.CreatedDate).ToListAsync());
    }

    public IActionResult Create() => View(new Customer());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer model)
    {
        await CheckDuplicates(model);
        if (!ModelState.IsValid) return View(model);
        _db.Customers.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Customer created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var c = await _db.Customers.FindAsync(id);
        return c == null ? NotFound() : View(c);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Customer model)
    {
        if (id != model.Id) return NotFound();
        await CheckDuplicates(model);
        if (!ModelState.IsValid) return View(model);
        _db.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Customer updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var c = await _db.Customers.FindAsync(id);
        if (c != null)
        {
            if (await _db.Opportunities.AnyAsync(o => o.CustomerId == id))
            {
                TempData["Error"] = "Cannot delete: this customer has opportunities.";
                return RedirectToAction(nameof(Index));
            }
            _db.Customers.Remove(c);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Customer deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    // Server-side duplicate check (email and phone must be unique)
    private async Task CheckDuplicates(Customer m)
    {
        if (await _db.Customers.AnyAsync(c => c.Email == m.Email && c.Id != m.Id))
            ModelState.AddModelError(nameof(m.Email), "A customer with this email already exists.");
        if (await _db.Customers.AnyAsync(c => c.Phone == m.Phone && c.Id != m.Id))
            ModelState.AddModelError(nameof(m.Phone), "A customer with this phone already exists.");
    }
}
