namespace AgricultureManager.Core.Application.Shared.Models.Identity
{
    public class UserToken
    {
        public string Username { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
