using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Core.Domain.Entities
{
    public class Harvest
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestUnitId { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public double Quantity { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(250)]
        public string? Setting { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public virtual HarvestUnit HarvestUnit { get; set; } = default!;
        public virtual Person? Person { get; set; }
        public virtual Unit? Unit { get; set; }
    }
}
