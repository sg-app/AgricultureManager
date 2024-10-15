using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class SeedVm
    {
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestUnitId { get; set; }
        [Required]
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


        public virtual HarvestUnitVm HarvestUnit { get; set; } = default!;
        public virtual PersonVm? Person { get; set; }
        public virtual CultureVm Culture { get; set; } = default!;
        public virtual UnitVm? Unit { get; set; }
        public virtual SeedCategoryVm? SeedCategory { get; set; }
        public virtual SeedTechnologyVm? SeedTechnology { get; set; }
    }
}
