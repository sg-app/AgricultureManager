using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.CompanyStore
{
    public record LoadCompanyDataAction();
    public record LoadCompanyDataResultAction(CompanyVm Company);
    public record LoadCompanyDataResultFailAction();
    public record UpdateCompanyAction(CompanyVm Company);
}
