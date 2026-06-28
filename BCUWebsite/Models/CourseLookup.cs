using System.ComponentModel.DataAnnotations;

namespace BCUWebsite.Models;

public class CourseLookup
{
    public int Id { get; set; }

    [Required]
    public string LookupType { get; set; } = string.Empty;

    [Required]
    public string Value { get; set; } = string.Empty;

    public int SortOrder { get; set; }
}
