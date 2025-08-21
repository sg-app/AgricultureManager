using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Module.Accounting.Domain
{
    [Table("AccountingAccount")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AccountHolder { get; set; } = string.Empty;
        public string? AccountNumber { get; set; }
        public string? Blz { get; set; }
        public string? Bic { get; set; }
        public string? Iban { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? HbciVersion { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; }
        public int TanProcess { get; set; } = 946; // Default value for TAN process
        public DateTime? LatestSynchronisation { get; set; }

        public virtual ICollection<AccountMouvement> AccountMouvements { get; set; } = [];
    }
}
