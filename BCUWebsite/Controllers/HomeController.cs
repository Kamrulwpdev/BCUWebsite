using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCUWebsite.Data;
using BCUWebsite.Models;
using BCUWebsite.Models.ViewModels;

namespace BCUWebsite.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            HeroStats = await _context.ContentBlocks
                .Where(c => c.Category == "HeroStats")
                .ToListAsync(),
            PromotedCourses = await _context.Courses
                .Where(c => c.IsPromoted)
                .OrderBy(c => c.DisplayOrder)
                .Take(6)
                .ToListAsync(),
            LatestNews = await _context.NewsArticles
                .Where(n => n.IsPublished)
                .OrderByDescending(n => n.PublishedDate)
                .Take(3)
                .ToListAsync(),
            FeaturedNews = await _context.NewsArticles
                .Where(n => n.IsPublished && n.IsFeatured)
                .FirstOrDefaultAsync()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
