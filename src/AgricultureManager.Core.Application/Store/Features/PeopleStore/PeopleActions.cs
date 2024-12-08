using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.PeopleStore
{
    public record LoadPeoplesDataAction();
    public record LoadPeoplesDataResultAction(IEnumerable<PersonVm> Peoples);
}
