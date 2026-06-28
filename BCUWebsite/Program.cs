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

// Ensure database schema is up to date and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();

    using var connection = dbContext.Database.GetDbConnection();
    connection.Open();
    using var command = connection.CreateCommand();

    command.CommandText = @"CREATE TABLE IF NOT EXISTS CourseLookups (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        LookupType TEXT NOT NULL,
        Value TEXT NOT NULL,
        SortOrder INTEGER NOT NULL
    );";
    command.ExecuteNonQuery();

    command.CommandText = "PRAGMA table_info('Courses');";
    using var reader = command.ExecuteReader();
    var existingColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    while (reader.Read())
    {
        existingColumns.Add(reader.GetString(reader.GetOrdinal("name")));
    }
    reader.Close();

    if (!existingColumns.Contains("Level"))
    {
        command.CommandText = "ALTER TABLE Courses ADD COLUMN Level TEXT;";
        command.ExecuteNonQuery();
    }
    if (!existingColumns.Contains("Mode"))
    {
        command.CommandText = "ALTER TABLE Courses ADD COLUMN Mode TEXT;";
        command.ExecuteNonQuery();
    }
    if (!existingColumns.Contains("SubjectArea"))
    {
        command.CommandText = "ALTER TABLE Courses ADD COLUMN SubjectArea TEXT;";
        command.ExecuteNonQuery();
    }
}

await DbInitializer.InitializeAsync(app.Services);

app.Run();
