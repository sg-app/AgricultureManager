using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.PersonFeatures
{
    public class UpdatePersonCommand : IReq<PersonVm>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Comment { get; set; }

    }

    public class UpdatePersonCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdatePersonCommand, PersonVm>
    {
        public async Task<Response<PersonVm>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var person = await dbContext.Person.FindAsync([request.Id], cancellationToken);
            if (person is null)
            {
                return Response.Fail<PersonVm>("Schlag nicht gefunden.");
            }

            mapper.Map(request, person);

            dbContext.Person.Update(person);
            await dbContext.SaveChangesAsync(cancellationToken);

            var personVm = mapper.Map<PersonVm>(person);
            return Response.Success(personVm);
        }
    }
}
