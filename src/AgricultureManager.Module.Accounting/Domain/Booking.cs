using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Module.Accounting.Domain
{
    [Table("AccountingBooking")]
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountMouvementId { get; set; }
        public Guid BookingTypeId { get; set; }
        [Column(TypeName = "decimal(15, 2)")]
        public decimal Amount { get; set; }
        public Guid TaxRateId { get; set; }


        public AccountMouvement? AccountMouvement { get; set; }
        public BookingType BookingType { get; set; } = default!;
        public TaxRate? TaxRate { get; set; }
    }
}
