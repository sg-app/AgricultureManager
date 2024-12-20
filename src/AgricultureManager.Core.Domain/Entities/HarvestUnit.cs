using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class HarvestUnit
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestYearId { get; set; }
        [Required]
        public Guid FieldId { get; set; }

        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        [Required]
        public Guid CultureId { get; set; }

        public virtual HarvestYear HarvestYear { get; set; } = default!;
        public virtual Field Field { get; set; } = default!;
        public virtual Culture Culture { get; set; } = default!;

        public virtual ICollection<Seed> Seeds { get; set; } = [];
        public virtual ICollection<Fertilization> Fertilizations { get; set; } = [];
        public virtual ICollection<PlantProtection> PlantProtections { get; set; } = [];
        public virtual ICollection<Harvest> Harvests { get; set; } = [];

        public virtual ICollection<FertilizerPlaning> FertilizerPlanings { get; set; } = [];
        public virtual ICollection<FertilizerPlaningSpecification> FertilizerPlaningSpecifications { get; set; } = [];
    }
}
