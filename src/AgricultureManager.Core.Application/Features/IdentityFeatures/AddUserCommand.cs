using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.Models.Identity;
using AgricultureManager.Core.Domain.Identity;
using System.Security.Cryptography;
using System.Text;

namespace AgricultureManager.Core.Application.Features.IdentityFeatures
{
    public record AddUserCommand(string UserName, string? Firstname, string? Lastname, string Password) : IReq<UserVm> { }

    public class AddUserCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<AddUserCommand, UserVm>
    {
        public async Task<Response<UserVm>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var user = new User
            {
                UserName = request.UserName,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
            };

            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            user.PasswordSalt = hmac.Key;

            dbContext.User.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Success(new UserVm { UserName = user.UserName });
        }
    }
}
