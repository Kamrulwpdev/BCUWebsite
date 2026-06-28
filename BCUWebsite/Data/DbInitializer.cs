using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCUWebsite.Models;

namespace BCUWebsite.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.EnsureCreatedAsync();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var adminEmail = "admin@bcuwebsite.local";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        if (!context.Courses.Any())
        {
            context.Courses.AddRange(
                new Course { Title = "ASP.NET Core Bootcamp", Description = "Build modern web apps with ASP.NET Core.", Category = "Web Development", ImageUrl = "/images/sample-course-1.jpg", Link = "https://example.com/course/aspnet-core", IsPromoted = true, DisplayOrder = 1 },
                new Course { Title = "C# Fundamentals", Description = "Learn C# from the ground up.", Category = "Programming", ImageUrl = "/images/sample-course-2.jpg", Link = "https://example.com/course/csharp", DisplayOrder = 2 }
            );
        }

        if (!context.NewsArticles.Any())
        {
            context.NewsArticles.AddRange(
                new NewsArticle { Title = "BCU Website Launch", Content = "We have launched an admin dashboard for managing courses and news articles.", PublishedDate = DateTime.UtcNow.AddDays(-3), IsPublished = true, ImageUrl = "/images/sample-news-1.jpg" },
                new NewsArticle { Title = "New Content Blocks", Content = "Content blocks now keep your homepage copy editable without redeploying.", PublishedDate = DateTime.UtcNow, IsPublished = true }
            );
        }

        if (!context.ContentBlocks.Any())
        {
            context.ContentBlocks.AddRange(
                new ContentBlock { Key = "HeroStat1", Title = "Courses", Content = "100+ Courses", Category = "Hero" },
                new ContentBlock { Key = "HeroStat2", Title = "Students", Content = "Thousands of Students", Category = "Hero" },
                new ContentBlock { Key = "HomepageWelcome", Title = "Welcome", Content = "Welcome to the new BCU admin dashboard!", Category = "Home" }
            );
        }

        await context.SaveChangesAsync();
    }
}
