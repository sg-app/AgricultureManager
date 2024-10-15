using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class PersonVm
    {
        public Guid Id { get; set; }
        [MaxLength(100), Required]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(100), Required]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? JobTitle { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
