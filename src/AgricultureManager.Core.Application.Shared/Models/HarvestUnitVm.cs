using AgricultureManager.Core.Application.Shared.Attributes;
using AgricultureManager.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class HarvestUnitVm
    {
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestYearId { get; set; }
        [Required]
        public Guid FieldId { get; set; }
        [MaxLength(150, ErrorMessage ="Maximal {1} Zeichen erlaubt."), Required(ErrorMessage = "Name muss ausgefüllt sein.")]
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        [NotEmptyGuid(ErrorMessage = "Kultur muss auswählt sein.")]
        public Guid CultureId { get; set; }

        public virtual CultureVm Culture { get; set; } = default!;
        public virtual Field Field { get; set; } = default!;

        public string AreaDisplay => $"{Area:N2} ha";
    }
}
