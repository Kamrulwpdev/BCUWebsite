using BCUWebsite.Data;
using BCUWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BCUWebsite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalCourses = await _context.Courses.CountAsync(),
            TotalNewsArticles = await _context.NewsArticles.CountAsync(),
            TotalContentBlocks = await _context.ContentBlocks.CountAsync(),
            RecentCourses = await _context.Courses
                .OrderByDescending(c => c.CreatedAt)
                .Take(5)
                .ToListAsync(),
            RecentNews = await _context.NewsArticles
                .OrderByDescending(n => n.PublishedDate)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }
}
