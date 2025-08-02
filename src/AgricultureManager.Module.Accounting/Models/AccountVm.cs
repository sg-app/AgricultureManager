namespace AgricultureManager.Module.Accounting.Models
{
    public class AccountVm
    {
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
        public int TanProcess { get; set; }
        public DateTime? LatestSynchronisation { get; set; }

    }
}
