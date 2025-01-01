using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.TaxRateFeatures
{
    public class AddTaxRateCommand : IReq<TaxRateVm>
    {
        public string TaxRateName { get; set; } = string.Empty;
        public decimal TaxRateValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

    }
    public class AddTaxRateCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddTaxRateCommand, TaxRateVm>
    {
        public async Task<Response<TaxRateVm>> Handle(AddTaxRateCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<TaxRate>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.TaxRate.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<TaxRateVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
