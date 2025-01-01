using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.SeedFeatures
{
    public class AddSeedCommand : IReq<SeedVm>
    {
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
    public class AddSeedCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddSeedCommand, SeedVm>
    {
        public async Task<Response<SeedVm>> Handle(AddSeedCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<Seed>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.Seed.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<SeedVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
