using BCUWebsite.Data;
using BCUWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IActionResult> Index()
    {
        var contentBlocks = await _context.ContentBlocks
            .OrderBy(c => c.Category)
            .ThenBy(c => c.Key)
            .ToListAsync();
        return View(contentBlocks);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var contentBlock = await _context.ContentBlocks.FindAsync(id);
        if (contentBlock == null)
        {
            return NotFound();
        }
        return View(contentBlock);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContentBlock contentBlock)
    {
        if (id != contentBlock.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                contentBlock.UpdatedAt = DateTime.Now;
                _context.Update(contentBlock);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Content block updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentBlockExists(contentBlock.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(contentBlock);
    }

    private bool ContentBlockExists(int id)
    {
        return _context.ContentBlocks.Any(e => e.Id == id);
    }
}
