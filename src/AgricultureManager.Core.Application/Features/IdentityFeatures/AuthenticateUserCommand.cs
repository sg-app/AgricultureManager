using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.Models.Identity;
using System.Security.Cryptography;
using System.Text;

namespace AgricultureManager.Core.Application.Features.IdentityFeatures
{
    public record AuthenticateUserCommand(string UserName, string Password) :IReq<UserVm> { }
    public class AuthenticateUserCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<AuthenticateUserCommand, UserVm>
    {
        public async Task<Response<UserVm>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.User.FindAsync([ request.UserName ], cancellationToken);
            if (entity is null)
            {
                return Response.Fail<UserVm>("Benutzer nicht gefunden.");
            }
            using var hmac = new HMACSHA512(entity.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            if (!computedHash.SequenceEqual(entity.PasswordHash))
            {
                return Response.Fail<UserVm>("Passwort ist falsch.");
            }
            var user = new UserVm
            {
                UserName = entity.UserName,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
            };
            return Response.Success(user);
        }
    }
}
