using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Module.Accounting.Models
{
    public class ConfigurationVm
    {
        [Key]
        public string IBAN { get; set; }
        public string AccountHolder { get; set; }
        public string Account { get; set; }
        public int BLZ { get; set; }
        public string BankName { get; set; }
        public string BIC { get; set; }
        public string Url { get; set; }
        public int HbciVersion { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public DateTime? BankStatementEndDate { get; set; }
    }
}
