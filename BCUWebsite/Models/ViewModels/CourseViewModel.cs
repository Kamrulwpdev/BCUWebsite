using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BCUWebsite.Models.ViewModels;

public class CourseViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Course Title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Category")]
    public string? Category { get; set; }

    [Display(Name = "Link")]
    public string? Link { get; set; }

    [Display(Name = "Is Promoted")]
    public bool IsPromoted { get; set; }

    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; }

    public IFormFile? ImageFile { get; set; }
    public string? ExistingImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
