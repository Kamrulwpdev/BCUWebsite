namespace BCUWebsite.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Category { get; set; }
    public string? Link { get; set; }
    public bool IsFeatured { get; set; }
}
