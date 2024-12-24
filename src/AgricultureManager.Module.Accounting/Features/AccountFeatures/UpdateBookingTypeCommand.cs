using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.AccountFeatures
{
    public class UpdateAccountCommand : IReq<AccountVm>
    {
        public Guid Id { get; set; }
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

    public class UpdateAccountCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateAccountCommand, AccountVm>
    {
        public async Task<Response<AccountVm>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.Account.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<AccountVm>("Konto nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.Account.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<AccountVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
