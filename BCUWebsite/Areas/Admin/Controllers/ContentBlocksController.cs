using BCUWebsite.Data;
using BCUWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCUWebsite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ContentBlocksController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContentBlocksController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.ContentBlocks.OrderBy(cb => cb.Key).ToList());
    }

    public IActionResult Edit(int id)
    {
        var block = _context.ContentBlocks.Find(id);
        if (block == null) return NotFound();
        return View(block);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContentBlock model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var block = _context.ContentBlocks.Find(id);
        if (block == null) return NotFound();

        block.Content = model.Content;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
