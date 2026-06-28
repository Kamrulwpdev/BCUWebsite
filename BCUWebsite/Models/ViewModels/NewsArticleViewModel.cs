using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BCUWebsite.Models.ViewModels;

public class NewsArticleViewModel
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
    public bool IsPublished { get; set; }
    public bool IsFeatured { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? ExistingImageUrl { get; set; }
}
