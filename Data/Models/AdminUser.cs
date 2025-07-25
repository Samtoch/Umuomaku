using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Umuomaku.Data.Models
{
    public class AdminUser
    {
        [Key] // Denotes this as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies that the DB generates the ID
        public long Id { get; set; } 

        [StringLength(256)]
        public string? UserName { get; set; }

        [StringLength(256)]
        public string? NormalizedUserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string? NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; } // TINYINT(1) maps to bool

        [Column(TypeName = "longtext")] // Explicitly map to MySQL's LONGTEXT
        public string PasswordHash { get; set; }

        [Column(TypeName = "longtext")]
        public string? SecurityStamp { get; set; }

        [Column(TypeName = "longtext")]
        public string? ConcurrencyStamp { get; set; }

        [Column(TypeName = "longtext")]
        public string? PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; } // TIMESTAMP NULL can map to DateTimeOffset?

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        // Custom fields
        [StringLength(255)]
        public string? FirstName { get; set; }

        [StringLength(255)]
        public string? LastName { get; set; }

        // DATETIME DEFAULT CURRENT_TIMESTAMP will be handled by the database
        public DateTime DateCreated { get; set; } // MySQL's DATETIME maps to DateTime

        public DateTime? LastLogin { get; set; } // DATETIME NULL maps to DateTime?
    }
}
