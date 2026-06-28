namespace BCUWebsite.Models.ViewModels;

public class CourseLookupViewModel
{
    public int Id { get; set; }
    public string LookupType { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
