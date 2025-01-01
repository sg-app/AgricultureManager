using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models.EditorModels
{
    public class EditSeedDetailVm
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public HarvestUnitVm HarvestUnit { get; set; } = default!;
        [MinLength(3, ErrorMessage = "Sortenname muss mindestens {1} Zeichen enthalten.")]
        [MaxLength(150, ErrorMessage = "Sortenname darf nicht mehr als {1} Zeichen enthalten.")]
        public string VarietyName { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Zulassungsnummer darf nicht mehr als {1} Zeichen enthalten.")]
        public string? ApprovalNumber { get; set; }
        [Range(0.01, 1000000, ErrorMessage = "Menge muss zwischen {1} und {2} liegen.")]
        public double Quantity { get; set; }
        public Guid? SeedCategoryId { get; set; }
        [MaxLength(250, ErrorMessage = "Einstellung darf nicht mehr als {1} Zeichen enthalten.")]
        public string? Setting { get; set; }
        [MaxLength(500, ErrorMessage = "Kommentar darf nicht mehr als {1} Zeichen enthalten.")]
        public string? Comment { get; set; }
    }
}
