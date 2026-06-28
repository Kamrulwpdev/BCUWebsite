using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCUWebsite.Data;
using BCUWebsite.Models.ViewModels;

namespace BCUWebsite.Controllers;

public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string q, string? level, string? subjectArea, string? mode, int page = 1)
    {
        const int PageSize = 20;

        var query = _context.Courses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(c => EF.Functions.Like(c.Title, $"%{q}%") || EF.Functions.Like(c.Description, $"%{q}%"));
        }

        if (!string.IsNullOrWhiteSpace(subjectArea))
        {
            query = query.Where(c => c.SubjectArea == subjectArea);
        }

        if (!string.IsNullOrWhiteSpace(level))
        {
            query = query.Where(c => c.Level == level);
        }

        if (!string.IsNullOrWhiteSpace(mode))
        {
            query = query.Where(c => c.Mode == mode);
        }

        var total = await query.CountAsync();

        var courses = await query
            .OrderByDescending(c => c.IsPromoted)
            .ThenBy(c => c.DisplayOrder)
            .ThenBy(c => c.Title)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var levels = await _context.CourseLookups
            .Where(l => l.LookupType == "Level")
            .OrderBy(l => l.SortOrder)
            .Select(l => l.Value)
            .ToListAsync();

        var subjectAreas = await _context.CourseLookups
            .Where(l => l.LookupType == "SubjectArea")
            .OrderBy(l => l.SortOrder)
            .Select(l => l.Value)
            .ToListAsync();

        var modesList = await _context.CourseLookups
            .Where(l => l.LookupType == "Mode")
            .OrderBy(l => l.SortOrder)
            .Select(l => l.Value)
            .ToListAsync();

        var vm = new CoursesIndexViewModel
        {
            Courses = courses,
            TotalCount = total,
            Page = page,
            PageSize = PageSize,
            TotalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize)),
            Query = q,
            Level = level,
            SubjectArea = subjectArea,
            Mode = mode,
            Levels = levels,
            SubjectAreas = subjectAreas,
            Modes = modesList
        };

        return View(vm);
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return NotFound();
        return View(course);
    }
}
