using Microsoft.AspNetCore.Identity;
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

        if (!context.CourseLookups.Any())
        {
            context.CourseLookups.AddRange(
                new CourseLookup { LookupType = "Level", Value = "Undergraduate", SortOrder = 1 },
                new CourseLookup { LookupType = "Level", Value = "Postgraduate", SortOrder = 2 },
                new CourseLookup { LookupType = "Mode", Value = "Full-time", SortOrder = 1 },
                new CourseLookup { LookupType = "Mode", Value = "Part-time", SortOrder = 2 },
                new CourseLookup { LookupType = "Mode", Value = "Sandwich", SortOrder = 3 },
                new CourseLookup { LookupType = "SubjectArea", Value = "Business and Management", SortOrder = 1 },
                new CourseLookup { LookupType = "SubjectArea", Value = "Computing and Digital Technologies", SortOrder = 2 },
                new CourseLookup { LookupType = "SubjectArea", Value = "Engineering and Technology", SortOrder = 3 },
                new CourseLookup { LookupType = "SubjectArea", Value = "Creative and Digital Industries", SortOrder = 4 },
                new CourseLookup { LookupType = "SubjectArea", Value = "Health and Social Care", SortOrder = 5 }
            );
        }

        if (!context.Courses.Any())
        {
            context.Courses.AddRange(
                new Course
                {
                    Title = "Accounting and Finance BSc (Hons)",
                    Description = "Develop professional accounting, auditing and financial planning skills through live briefs, industry software and real-world business projects.",
                    Category = "Business",
                    Level = "Undergraduate",
                    Mode = "Full-time",
                    SubjectArea = "Business and Management",
                    ImageUrl = "/images/course-accounting-finance.jpg",
                    Link = "https://www.bcu.ac.uk/courses/accounting-and-finance-bsc-hons-2026-27",
                    IsPromoted = true,
                    DisplayOrder = 1,
                    CreatedAt = DateTime.UtcNow.AddDays(-21)
                },
                new Course
                {
                    Title = "Computer Science BSc (Hons)",
                    Description = "Learn software engineering, AI and data systems with industry-standard facilities and project work that prepares you for graduate roles.",
                    Category = "Computing",
                    Level = "Undergraduate",
                    Mode = "Full-time",
                    SubjectArea = "Computing and Digital Technologies",
                    ImageUrl = "/images/course-computer-science.jpg",
                    Link = "https://www.bcu.ac.uk/courses/computer-science-bsc-hons-2026-27",
                    IsPromoted = true,
                    DisplayOrder = 2,
                    CreatedAt = DateTime.UtcNow.AddDays(-18)
                },
                new Course
                {
                    Title = "Cyber Security BSc (Hons)",
                    Description = "Build cyber defence expertise with hands-on labs, ethical hacking techniques and security management skills.",
                    Category = "Computing",
                    Level = "Undergraduate",
                    Mode = "Sandwich",
                    SubjectArea = "Computing and Digital Technologies",
                    ImageUrl = "/images/course-cyber-security.jpg",
                    Link = "https://www.bcu.ac.uk/courses/cyber-security-bsc-hons-2026-27",
                    IsPromoted = false,
                    DisplayOrder = 3,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new Course
                {
                    Title = "Architecture BA (Hons)",
                    Description = "Study creative design, urban planning and digital modelling to shape sustainable cities and the built environment.",
                    Category = "Architecture",
                    Level = "Undergraduate",
                    Mode = "Full-time",
                    SubjectArea = "Engineering and Technology",
                    ImageUrl = "/images/course-architecture.jpg",
                    Link = "https://www.bcu.ac.uk/courses/architecture-ba-hons-2026-27",
                    IsPromoted = false,
                    DisplayOrder = 4,
                    CreatedAt = DateTime.UtcNow.AddDays(-12)
                },
                new Course
                {
                    Title = "Creative Writing BA (Hons)",
                    Description = "Develop your voice through fiction, scriptwriting and digital storytelling with industry mentors and live publishing opportunities.",
                    Category = "Creative",
                    Level = "Undergraduate",
                    Mode = "Part-time",
                    SubjectArea = "Creative and Digital Industries",
                    ImageUrl = "/images/course-creative-writing.jpg",
                    Link = "https://www.bcu.ac.uk/courses/creative-writing-ba-hons-2026-27",
                    IsPromoted = false,
                    DisplayOrder = 5,
                    CreatedAt = DateTime.UtcNow.AddDays(-9)
                },
                new Course
                {
                    Title = "Public Health MSc",
                    Description = "Advance your career in health policy, population research and community wellbeing with a strong emphasis on applied practice.",
                    Category = "Health",
                    Level = "Postgraduate",
                    Mode = "Part-time",
                    SubjectArea = "Health and Social Care",
                    ImageUrl = "/images/course-public-health.jpg",
                    Link = "https://www.bcu.ac.uk/courses/public-health-msc-2026-27",
                    IsPromoted = false,
                    DisplayOrder = 6,
                    CreatedAt = DateTime.UtcNow.AddDays(-6)
                }
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
