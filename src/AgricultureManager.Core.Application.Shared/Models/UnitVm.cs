using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class UnitVm
    {
        public Guid Id { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
