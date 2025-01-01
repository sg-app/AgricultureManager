using AgricultureManager.Core.Application.Shared.Extensions;
using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.AccountFeatures
{
    public class AddAccountCommand : IReq<AccountVm>
    {
        public string BankName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AccountHolder { get; set; } = string.Empty;
        public string? AccountNumber { get; set; }
        public string? Blz { get; set; }
        public string? Bic { get; set; }
        public string? Iban { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? HbciVersion { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; }
        public DateTime? LatestSynchronisation { get; set; }

    }
    public class AddAccountCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddAccountCommand, AccountVm>
    {
        public async Task<Response<AccountVm>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<Account>(request);
            culture.Id = Guid.NewGuid();
            await dbContext.Account.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<AccountVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
