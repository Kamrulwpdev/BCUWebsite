using BCUWebsite.Data;
using BCUWebsite.Models;
using BCUWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCUWebsite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class NewsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public NewsController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public IActionResult Index()
    {
        return View(_context.NewsArticles.OrderByDescending(a => a.PublishedDate).ToList());
    }

    public IActionResult Details(int id)
    {
        var article = _context.NewsArticles.Find(id);
        if (article == null) return NotFound();
        return View(article);
    }

    public IActionResult Create()
    {
        return View(new NewsArticleViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NewsArticleViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var article = new NewsArticle
        {
            Title = model.Title,
            Content = model.Content,
            PublishedDate = model.PublishedDate,
            IsPublished = model.IsPublished
        };

        if (model.ImageFile != null)
        {
            var fileName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile.FileName);
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);
            await using var stream = System.IO.File.Create(filePath);
            await model.ImageFile.CopyToAsync(stream);
            article.ImageUrl = $"/images/{fileName}";
        }

        _context.NewsArticles.Add(article);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var article = _context.NewsArticles.Find(id);
        if (article == null) return NotFound();

        var model = new NewsArticleViewModel
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            PublishedDate = article.PublishedDate,
            IsPublished = article.IsPublished,
            ExistingImageUrl = article.ImageUrl
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NewsArticleViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var article = _context.NewsArticles.Find(id);
        if (article == null) return NotFound();

        article.Title = model.Title;
        article.Content = model.Content;
        article.PublishedDate = model.PublishedDate;
        article.IsPublished = model.IsPublished;

        if (model.ImageFile != null)
        {
            var fileName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile.FileName);
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);
            await using var stream = System.IO.File.Create(filePath);
            await model.ImageFile.CopyToAsync(stream);
            article.ImageUrl = $"/images/{fileName}";
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var article = _context.NewsArticles.Find(id);
        if (article == null) return NotFound();
        return View(article);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var article = _context.NewsArticles.Find(id);
        if (article == null) return NotFound();

        _context.NewsArticles.Remove(article);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
