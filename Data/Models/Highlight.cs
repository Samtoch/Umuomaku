using System.ComponentModel.DataAnnotations;

namespace Umuomaku.Data.Models
{
    public class Highlight
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; } 
        public string? UserUpdated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
