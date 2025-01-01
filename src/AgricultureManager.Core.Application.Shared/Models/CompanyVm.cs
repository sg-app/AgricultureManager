using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class CompanyVm
    {
        public Guid Id { get; set; }
        [MaxLength(50, ErrorMessage = "Betriebsname maximal {1} Zeichen erlaubt.")]
        public string? CompanyName { get; set; }
        [MaxLength(50, ErrorMessage = "Vorname maximal {1} Zeichen erlaubt.")]
        public string? FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Nachname maximal {1} Zeichen erlaubt.")]
        public string? LastName { get; set; }
        [MaxLength(100, ErrorMessage = "Straße maximal {1} Zeichen erlaubt.")]
        public string? Street { get; set; }
        [MaxLength(10, ErrorMessage = "Hausnummer maximal {1} Zeichen erlaubt.")]
        public string? Housenumber { get; set; }
        [MaxLength(10, ErrorMessage = "PLZ maximal {1} Zeichen erlaubt.")]
        public string? Plz { get; set; }
        [MaxLength(50, ErrorMessage = "Stadt maximal {1} Zeichen erlaubt.")]
        public string? City { get; set; }
        [MinLength(10, ErrorMessage = "Mindestens {1} Zeichen benötigt.")]
        [MaxLength(20, ErrorMessage = "Maximal {1} Zeichen erlaubt.")]
        public string CompanyNumber { get; set; } = string.Empty;
    }
}
