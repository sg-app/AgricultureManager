using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.CultureFeatures
{
    public class AddCultureCommand : IReq<CultureVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public string? ShortName { get; set; }
        public string? Comment { get; set; }
    }
    public class AddCultureCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddCultureCommand, CultureVm>
    {
        public async Task<Response<CultureVm>> Handle(AddCultureCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<Culture>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.Culture.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<CultureVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
