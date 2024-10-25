using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.PersonFeatures
{
    public class RemovePersonCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemovePersonCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemovePersonCommand>
    {
        public async Task<ResponseLess> Handle(RemovePersonCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Person.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Person nicht gefunden.");

            dbContext.Person.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
