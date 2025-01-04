using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Module.Accounting.Domain
{
    [Table("AccountingBookingType")]
    public class BookingType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Short { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public CostType? CostType { get; set; }
    }
}
