using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class HarvestYear
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(4), Required]
        public string Year { get; set; } = string.Empty;

        public virtual ICollection<HarvestUnit> HarvestUnits { get; set; } = default!;
    }
}
