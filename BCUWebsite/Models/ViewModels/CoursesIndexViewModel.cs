using System.Collections.Generic;
using BCUWebsite.Models;

namespace BCUWebsite.Models.ViewModels;

public class CoursesIndexViewModel
{
    public IEnumerable<Course> Courses { get; set; } = Enumerable.Empty<Course>();
    public int TotalCount { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public int TotalPages { get; set; }

    public string? Query { get; set; }
    public string? Level { get; set; }
    public string? SubjectArea { get; set; }
    public string? Mode { get; set; }

    public IEnumerable<string> Levels { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<string> SubjectAreas { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<string> Modes { get; set; } = Enumerable.Empty<string>();
}
