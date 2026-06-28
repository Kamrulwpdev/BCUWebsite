using BCUWebsite.Models;

namespace BCUWebsite.Models.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalCourses { get; set; }
    public int TotalNewsArticles { get; set; }
    public int TotalContentBlocks { get; set; }
    public List<Course> RecentCourses { get; set; } = new();
    public List<NewsArticle> RecentNews { get; set; } = new();
}
