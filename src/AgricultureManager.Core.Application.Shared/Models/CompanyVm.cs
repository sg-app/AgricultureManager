using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class CompanyVm
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string? CompanyName { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(100)]
        public string? Street { get; set; }

        [MaxLength(10)]
        public string? Housenumber { get; set; }

        [MaxLength(10)]
        public string? Plz { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(20), MinLength(10), Required]
        public string CompanyNumber { get; set; } = string.Empty;
    }
}
