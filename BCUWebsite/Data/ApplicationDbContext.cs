using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCUWebsite.Models;

namespace BCUWebsite.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();
    public DbSet<ContentBlock> ContentBlocks => Set<ContentBlock>();
}
