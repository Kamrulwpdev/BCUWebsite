using BCUWebsite.Data;
using BCUWebsite.Models;
using BCUWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCUWebsite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public CoursesController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public IActionResult Index()
    {
        return View(_context.Courses.ToList());
    }

    public IActionResult Details(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();
        return View(course);
    }

    public IActionResult Create()
    {
        return View(new CourseViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var course = new Course
        {
            Title = model.Title,
            Description = model.Description,
            Category = model.Category,
            Level = model.Level,
            Mode = model.Mode,
            SubjectArea = model.SubjectArea,
            Link = model.Link,
            IsPromoted = model.IsPromoted,
            DisplayOrder = model.DisplayOrder,
            CreatedAt = DateTime.UtcNow
        };

        if (model.ImageFile != null)
        {
            var fileName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile.FileName);
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);
            await using var stream = System.IO.File.Create(filePath);
            await model.ImageFile.CopyToAsync(stream);
            course.ImageUrl = $"/images/{fileName}";
        }

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();

        var model = new CourseViewModel
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Category = course.Category,
            Level = course.Level,
            Mode = course.Mode,
            SubjectArea = course.SubjectArea,
            Link = course.Link,
            IsPromoted = course.IsPromoted,
            DisplayOrder = course.DisplayOrder,
            CreatedAt = course.CreatedAt,
            UpdatedAt = course.UpdatedAt,
            ExistingImageUrl = course.ImageUrl
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();

        course.Title = model.Title;
        course.Description = model.Description;
        course.Category = model.Category;
        course.Level = model.Level;
        course.Mode = model.Mode;
        course.SubjectArea = model.SubjectArea;
        course.Link = model.Link;
        course.IsPromoted = model.IsPromoted;
        course.DisplayOrder = model.DisplayOrder;
        course.UpdatedAt = DateTime.UtcNow;

        if (model.ImageFile != null)
        {
            var fileName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile.FileName);
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);
            await using var stream = System.IO.File.Create(filePath);
            await model.ImageFile.CopyToAsync(stream);
            course.ImageUrl = $"/images/{fileName}";
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();
        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return NotFound();

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
