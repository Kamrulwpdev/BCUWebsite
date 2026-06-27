namespace BCUWebsite.Models;

public class NewsArticle
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime PublishedDate { get; set; } = DateTime.Now;
    public bool IsPublished { get; set; }
}
