using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.CultureFeatures
{
    public class UpdateCultureCommand : IReq<CultureVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public string? ShortName { get; set; }
        public string? Comment { get; set; }

    }

    public class UpdateCultureCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateCultureCommand, CultureVm>
    {
        public async Task<Response<CultureVm>> Handle(UpdateCultureCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.Culture.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<CultureVm>("Kultur nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.Culture.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<CultureVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
