using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.SeedFeatures
{
    public class UpdateSeedCommand : IReq<SeedVm>
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public Guid CultureId { get; set; }
        public bool IsMainCulture { get; set; }
        public string VarietyName { get; set; } = string.Empty;
        public string? ApprovalNumber { get; set; }
        public double Quantity { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SeedCategoryId { get; set; }
        public Guid? SeedTechnologyId { get; set; }
        public string? Setting { get; set; }
        public string? Comment { get; set; }

    }

    public class UpdateSeedCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateSeedCommand, SeedVm>
    {
        public async Task<Response<SeedVm>> Handle(UpdateSeedCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.Seed.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<SeedVm>("Aussaat nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.Seed.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<SeedVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
