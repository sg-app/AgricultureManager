using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class HarvestUnit
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid YearFieldId { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        [Required]
        public Guid CultureId { get; set; }

        public virtual YearField YearField { get; set; } = default!;
        public virtual Culture Culture { get; set; } = default!;
    }
}
