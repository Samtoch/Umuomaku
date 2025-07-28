using System.ComponentModel.DataAnnotations;

namespace Umuomaku.Data.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        [Required]
        public string LocationOrEvent { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime? DateOfEvent { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UserUpdated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
