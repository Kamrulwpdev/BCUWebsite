using BCUWebsite.Models;

namespace BCUWebsite.Models.ViewModels;

public class HomeViewModel
{
    public List<ContentBlock> HeroStats { get; set; } = new();
    public List<Course> PromotedCourses { get; set; } = new();
    public List<NewsArticle> LatestNews { get; set; } = new();
    public NewsArticle? FeaturedNews { get; set; }
}
