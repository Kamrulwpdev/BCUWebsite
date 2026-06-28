using System.ComponentModel.DataAnnotations;

namespace BCUWebsite.Models
{
    public class ContentBlock
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Key")]
        public string Key { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public string? Category { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
