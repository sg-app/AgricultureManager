using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FertilizerPlaningVm
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public int Order { get; set; }
        public Guid FertilizerId { get; set; }
        public float Dosage { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(500, ErrorMessage = "Maximal {1} Zeichen erlaubt.")]
        public string? Comment { get; set; }
    }
}
