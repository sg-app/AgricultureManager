using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Module.Accounting.Models
{
    public class TaxRateVm
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string TaxRateName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TaxRateValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
