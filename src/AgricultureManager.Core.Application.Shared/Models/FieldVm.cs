using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FieldVm : IDeleteEntity
    {
        public Guid Id { get; set; }
        [MaxLength(45, ErrorMessage ="Maximal {1} Zeichern erlaubt"), Required(ErrorMessage ="Nummer muss ausgefüllt sein.")]
        public string Number { get; set; } = string.Empty;
        [MaxLength(150, ErrorMessage = "Maximal {1} Zeichern erlaubt"), Required(ErrorMessage = "Name muss ausgefüllt sein.")]
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        [MaxLength(500, ErrorMessage = "Maximal {1} Zeichern erlaubt")]
        public string? Comment { get; set; }

        public ICollection<HarvestUnitVm> HarvestUnits { get; set; } = default!;

        public string AreaDisplay => $"{Area:N2} ha";
    }
}
