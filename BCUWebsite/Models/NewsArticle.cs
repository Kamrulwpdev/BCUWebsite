using System.ComponentModel.DataAnnotations;

namespace BCUWebsite.Models;

public class NewsArticle
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Content")]
    public string Content { get; set; } = string.Empty;

    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Published Date")]
    public DateTime PublishedDate { get; set; } = DateTime.Now;

    [Display(Name = "Is Published")]
    public bool IsPublished { get; set; } = true;

    [Display(Name = "Is Featured")]
    public bool IsFeatured { get; set; } = false;
}
