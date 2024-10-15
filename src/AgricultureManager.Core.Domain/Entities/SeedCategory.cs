using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class SeedCategory
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
