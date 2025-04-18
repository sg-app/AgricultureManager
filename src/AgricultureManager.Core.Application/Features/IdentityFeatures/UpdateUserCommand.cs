using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.Models.Identity;
using System.Security.Cryptography;
using System.Text;

namespace AgricultureManager.Core.Application.Features.IdentityFeatures
{
    public record UpdateUserCommand(string UserName, string? Firstname, string? Lastname, string Password) : IReq<UserVm> { }

    public class UpdateUserCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<UpdateUserCommand, UserVm>
    {
        public async Task<Response<UserVm>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entity = await dbContext.User.FindAsync([request.UserName], cancellationToken);
            if (entity is null)
            {
                return Response.Fail<UserVm>("Benutzer nicht gefunden.");
            }
            
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                using var hmac = new HMACSHA512();
                entity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
                entity.PasswordSalt = hmac.Key;
            }
            entity.Firstname = request.Firstname;
            entity.Lastname = request.Lastname;

            dbContext.User.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Success(new UserVm { UserName = entity.UserName });
        }
    }
}
