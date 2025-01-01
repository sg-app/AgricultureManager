using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.PeopleStore
{
    public record LoadPeoplesDataAction();
    public record LoadPeoplesDataResultAction(IEnumerable<PersonVm> Peoples);
    public record LoadPeopleDataResultFailAction();
    public record AddPeopleAction(PersonVm People);
    public record UpdatePeopleAction(PersonVm People);
    public record RemovePeopleAction(Guid PersonId);
}
