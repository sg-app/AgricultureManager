using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.UnitFeatures
{
    public class UpdateUnitCommand : IReq<UnitVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }

    public class UpdateUnitCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateUnitCommand, UnitVm>
    {
        public async Task<Response<UnitVm>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var unit = await dbContext.Unit.FindAsync([request.Id], cancellationToken);
            if (unit is null)
            {
                return Response.Fail<UnitVm>("Einheit nicht gefunden.");
            }

            mapper.Map(request, unit);

            dbContext.Unit.Update(unit);
            await dbContext.SaveChangesAsync(cancellationToken);

            var unitVm = mapper.Map<UnitVm>(unit);
            return Response.Success(unitVm);
        }
    }
}
