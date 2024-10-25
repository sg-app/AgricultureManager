using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Features.PersonFeatures
{
    public class AddPersonCommand : IReq<PersonVm>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Comment { get; set; }
    }
    public class AddPersonCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddPersonCommand, PersonVm>
    {
        public async Task<Response<PersonVm>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var person = mapper.Map<Person>(request);
            person.Id = Guid.NewGuid();

            await dbContext.Person.AddAsync(person, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var personVm = mapper.Map<PersonVm>(person);
            return Response.Success(personVm);
        }
    }
}
