using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class PlantProtectant
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        public PlantProtectantType PlantProtectantType { get; set; }
        [MaxLength(500)]
        public string? ActiveSubstance { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
    public enum PlantProtectantType
    {
        Herbicide,
        Insecticide,
        Fungicide,
        Molluscicide,
        Acaricide,
        Rodenticide
    }
}
