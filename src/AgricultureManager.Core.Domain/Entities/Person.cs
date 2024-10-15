using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100), Required]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(100), Required]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? JobTitle { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
