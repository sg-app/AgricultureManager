using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class FertilizerPlaningSpecification
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }

        public virtual HarvestUnit HarvestUnit { get; set; } = default!;
        public virtual FertilizerDetail FertilizerDetail { get; set; } = default!;
    }
}
