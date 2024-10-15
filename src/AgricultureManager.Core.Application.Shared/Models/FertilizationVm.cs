using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FertilizationVm
    {
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestUnitId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public Guid FertilizerId { get; set; }
        public double Dosage { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(2)]
        public string? BBCH { get; set; }
        [MaxLength(250)]
        public string? Setting { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public virtual HarvestUnitVm HarvestUnit { get; set; } = default!;
        public virtual PersonVm? Person { get; set; }
        public virtual FertilizerVm Fertilizer { get; set; } = default!;
        public virtual UnitVm? Unit { get; set; }
    }
}
