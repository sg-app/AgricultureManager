using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Identity
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; } = [];

        [Required]
        public byte[] PasswordSalt { get; set; } = [];
    }
}
