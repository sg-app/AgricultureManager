
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models.Identity
{
    public class UserVm
    {
        [Required]
        [MinLength(4)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string LogonMessage { get; set; } = string.Empty;
    }
}
