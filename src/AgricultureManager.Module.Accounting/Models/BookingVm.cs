using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Module.Accounting.Models
{
    public class BookingVm
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountMouvementId { get; set; }
        public Guid BookingTypeId { get; set; }
        [Column(TypeName = "decimal(15, 2)")]
        public decimal Amount { get; set; }
        public Guid TaxRateId { get; set; }


        public AccountMouvementVm? AccountMouvement { get; set; }
        public BookingTypeVm? BookingType { get; set; }
        public TaxRateVm? TaxRate { get; set; }
    }
}
