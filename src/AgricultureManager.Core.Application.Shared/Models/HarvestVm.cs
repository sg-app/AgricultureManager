using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class HarvestVm
    {
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestUnitId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public double Quantity { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(250)]
        public string? Setting { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public virtual HarvestUnitVm HarvestUnit { get; set; } = default!;
        public virtual PersonVm? Person { get; set; }
        public virtual UnitVm? Unit { get; set; }
    }
}
