using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class PlantProtectantVm
    {
        public Guid Id { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        public PlantProtectantTypeVm PlantProtectantType { get; set; }
        [MaxLength(500)]
        public string? ActiveSubstance { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
    public enum PlantProtectantTypeVm
    {
        Herbicide,
        Insecticide,
        Fungicide,
        Molluscicide,
        Acaricide,
        Rodenticide
    }
}
