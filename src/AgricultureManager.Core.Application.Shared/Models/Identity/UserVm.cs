
using FluentValidation;
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
        public string Password2 { get; set; } = string.Empty;

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string LogonMessage { get; set; } = string.Empty;
    }
    public class UserVmValidator : AbstractValidator<UserVm>
    {
        public UserVmValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Benutzername darf nicht leer sein..")
                .MinimumLength(4)
                .WithMessage("Benutzername muss aus mindestens {MinLength} Zeichen bestehen.");

            RuleFor(x=>x)
                .Must(x => x.Password == x.Password2)
                .WithMessage("Die Passwörter stimmen nicht überein.");
        }
    }
}
