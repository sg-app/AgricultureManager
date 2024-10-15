using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Core.Domain.Entities
{
    public class Seed
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestUnitId { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public Guid CultureId { get; set; }
        public bool IsMainCulture { get; set; }
        [MaxLength(150), Required]
        public string VarietyName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? ApprovalNumber { get; set; }
        public double Quantity { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SeedCategoryId { get; set; }
        public Guid? SeedTechnologyId { get; set; }
        [MaxLength(250)]
        public string? Setting { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }


        public virtual HarvestUnit HarvestUnit { get; set; } = default!;
        public virtual Person? Person { get; set; }
        public virtual Culture Culture { get; set; } = default!;
        public virtual Unit? Unit { get; set; }
        public virtual SeedCategory? SeedCategory { get; set; }
        public virtual SeedTechnology? SeedTechnology { get; set; }
    }
}
