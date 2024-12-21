using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;

namespace AgricultureManager.Module.Accounting.Features.TaxRateFeatures
{
    public class UpdateTaxRateCommand : IReq<TaxRateVm>
    {
        public Guid Id { get; set; }
        public string TaxRateName { get; set; } = string.Empty;
        public decimal TaxRateValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }

    public class UpdateTaxRateCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateTaxRateCommand, TaxRateVm>
    {
        public async Task<Response<TaxRateVm>> Handle(UpdateTaxRateCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.TaxRate.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<TaxRateVm>("Steuerrate nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.TaxRate.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<TaxRateVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
