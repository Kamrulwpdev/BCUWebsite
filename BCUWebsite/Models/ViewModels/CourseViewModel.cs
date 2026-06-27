using Microsoft.AspNetCore.Http;

namespace BCUWebsite.Models.ViewModels;

public class CourseViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string? Link { get; set; }
    public bool IsFeatured { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? ExistingImageUrl { get; set; }
}
