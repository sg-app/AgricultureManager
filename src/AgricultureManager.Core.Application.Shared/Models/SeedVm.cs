using AgricultureManager.Core.Application.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class SeedVm
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        [Required(ErrorMessage = "Datum muss ausgewählt sein.")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        [NotEmptyGuid(ErrorMessage = "Kultur muss ausgewählt sein.")]
        public Guid CultureId { get; set; }
        public bool IsMainCulture { get; set; }
        [MinLength(3, ErrorMessage = "Sortenname muss mindestens {1} Zeichen enthalten.")]
        [MaxLength(150, ErrorMessage = "Sortenname darf nicht mehr als {1} Zeichen enthalten.")]
        public string VarietyName { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Zulassungsnummer darf nicht mehr als {1} Zeichen enthalten.")]
        public string? ApprovalNumber { get; set; }
        [Range(0.01, 1000000, ErrorMessage = "Menge muss zwischen {1} und {2} liegen.")]
        public double Quantity { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SeedCategoryId { get; set; }
        public Guid? SeedTechnologyId { get; set; }
        [MaxLength(250, ErrorMessage = "Einstellung darf nicht mehr als {1} Zeichen enthalten.")]
        public string? Setting { get; set; }
        [MaxLength(500, ErrorMessage = "Kommentar darf nicht mehr als {1} Zeichen enthalten.")]
        public string? Comment { get; set; }


        public virtual HarvestUnitVm HarvestUnit { get; set; } = default!;
        public virtual PersonVm? Person { get; set; }
        public virtual CultureVm Culture { get; set; } = default!;
        public virtual UnitVm? Unit { get; set; }
        public virtual SeedCategoryVm? SeedCategory { get; set; }
        public virtual SeedTechnologyVm? SeedTechnology { get; set; }
    }
}
