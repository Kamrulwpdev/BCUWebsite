using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCUWebsite.Data;
using BCUWebsite.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Use SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.OpenConnection();
    using var command = dbContext.Database.GetDbConnection().CreateCommand();
    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='AspNetUsers';";
    var hasAspNetUsers = command.ExecuteScalar() != null;
    dbContext.Database.CloseConnection();

    if (!hasAspNetUsers)
    {
        dbContext.Database.EnsureDeleted();
    }

    dbContext.Database.EnsureCreated();
    
    // Seed data if empty
    if (!dbContext.ContentBlocks.Any())
    {
        dbContext.ContentBlocks.AddRange(
            new ContentBlock { 
                Key = "HeroStat1", 
                Title = "No. 1", 
                Content = "in the West Midlands for work experience (RateMyPlacement Awards, 2025)", 
                Category = "HeroStats" 
            },
            new ContentBlock { 
                Key = "HeroStat2", 
                Title = "Gold", 
                Content = "for student experience, overall rating silver (TEF, 2023)", 
                Category = "HeroStats" 
            },
            new ContentBlock { 
                Key = "HeroStat3", 
                Title = "£500m", 
                Content = "invested in industry-standard facilities", 
                Category = "HeroStats" 
            },
            new ContentBlock { 
                Key = "HeroStat4", 
                Title = "7th", 
                Content = "in England for social mobility (HEPI, 2025)", 
                Category = "HeroStats" 
            }
        );
        
        dbContext.Courses.AddRange(
            new Course { 
                Title = "Computer Science BSc", 
                Description = "Learn to develop cutting-edge software and systems.", 
                Category = "Undergraduate", 
                IsPromoted = true, 
                DisplayOrder = 1 
            },
            new Course { 
                Title = "Business Management BA", 
                Description = "Develop strategic leadership and management skills.", 
                Category = "Undergraduate", 
                IsPromoted = true, 
                DisplayOrder = 2 
            }
        );
        
        dbContext.NewsArticles.AddRange(
            new NewsArticle { 
                Title = "Sir Lenny Henry urges media industry to stand together against racism", 
                Content = "Sir Lenny Henry has called on the UK media industry to confront racism, sexism, ableism and inequality head-on.", 
                IsPublished = true, 
                IsFeatured = true 
            }
        );
        
        dbContext.SaveChanges();
    }
}

await DbInitializer.InitializeAsync(app.Services);

app.Run();
