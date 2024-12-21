using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Module.Accounting.Domain
{
    [Table("AccountingAccountMouvements")]
    public class AccountMouvement
    {
        public Guid Id { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime ValueDate { get; set; }
        [MaxLength(10)]
        public string? TransactionTypeId { get; set; }
        [MaxLength(10)]
        public string? TypeCode { get; set; }
        [MaxLength(200)]
        public string? PartnerName { get; set; }
        [MaxLength(700)]
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(15, 2)")]
        public decimal Amount { get; set; }



        /// <summary>
        /// Pending (Vorgemerkt)
        /// </summary>
        public bool? Pending { get; set; }
        [MaxLength(50)]
        public string? Text { get; set; }
        [MaxLength(10)]
        public string? Primanota { get; set; }

        public string? TextKeyAddition { get; set; }


        /// <summary>
        /// BIC of the counterpart
        /// </summary>
        [MaxLength(50)]
        public string? BankCode { get; set; }

        /// <summary>
        /// IBAN of the counterpart
        /// </summary>
        [MaxLength(25)]
        public string? AccountCode { get; set; }

        /// <summary>
        /// Name of the counterpart
        /// </summary>

        public string? EndToEndId { get; set; }

        public string? MandateId { get; set; }

        /// <summary>
        /// Unique Identifier (if available)
        /// </summary>
        public string? ProprietaryRef { get; set; }
        [MaxLength(25)]
        public string? CustomerRef { get; set; }

        public string? PaymentInformationId { get; set; }

        public string? MessageId { get; set; }

        public bool? Storno { get; set; }



        public virtual ICollection<Document>? Documents { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }

    }
}
