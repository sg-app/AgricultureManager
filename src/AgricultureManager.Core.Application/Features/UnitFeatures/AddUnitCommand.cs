using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.UnitFeatures
{
    public class AddUnitCommand : IReq<UnitVm>
    {
        public string Number { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
    public class AddUnitCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddUnitCommand, UnitVm>
    {
        public async Task<Response<UnitVm>> Handle(AddUnitCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var unit = mapper.Map<Unit>(request);
            unit.Id = Guid.NewGuid();

            await dbContext.Unit.AddAsync(unit, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var unitVm = mapper.Map<UnitVm>(unit);
            return Response.Success(unitVm);
        }
    }
}
