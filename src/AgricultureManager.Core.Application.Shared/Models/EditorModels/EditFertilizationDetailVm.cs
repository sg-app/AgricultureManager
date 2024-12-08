using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models.EditorModels
{
    public class EditFertilizationDetailVm
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public HarvestUnitVm HarvestUnit { get; set; } = default!;
        [Range(0.01, 1000000, ErrorMessage = "Dosierung muss zwischen {1} und {2} liegen.")]
        public double Dosage { get; set; }
        [MaxLength(250, ErrorMessage = "Einstellung darf nicht mehr als {1} Zeichen enthalten.")]
        public string? Setting { get; set; }
        [MaxLength(500, ErrorMessage = "Kommentar darf nicht mehr als {1} Zeichen enthalten.")]
        public string? Comment { get; set; }
    }
}
