using System.ComponentModel.DataAnnotations;

namespace BCUWebsite.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Course Title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }

    [Display(Name = "Category")]
    public string? Category { get; set; }

    [Display(Name = "Link")]
    public string? Link { get; set; }

    [Display(Name = "Is Promoted")]
    public bool IsPromoted { get; set; }

    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
