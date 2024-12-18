using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class FertilizerPlaning
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public int Order { get; set; }
        public Guid FertilizerId { get; set; }
        public float Dosage { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public virtual HarvestUnit HarvestUnit { get; set; } = default!;
        public virtual Fertilizer Fertilizer { get; set; } = default!;
        public virtual Unit? Unit { get; set; }
    }
}
