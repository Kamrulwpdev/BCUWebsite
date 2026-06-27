using BCUWebsite.Data;
using BCUWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalCourses = _context.Courses.Count(),
            TotalNewsArticles = _context.NewsArticles.Count(),
            TotalContentBlocks = _context.ContentBlocks.Count()
        };

        return View(model);
    }
}
