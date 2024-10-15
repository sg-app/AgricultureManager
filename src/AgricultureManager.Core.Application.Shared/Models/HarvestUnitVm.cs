using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class HarvestUnitVm
    {
        public Guid Id { get; set; }
        [Required]
        public Guid YearFieldId { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        [Required]
        public Guid CultureId { get; set; }

        public virtual YearFieldVm YearField { get; set; } = default!;
        public virtual CultureVm Culture { get; set; } = default!;
    }
}
