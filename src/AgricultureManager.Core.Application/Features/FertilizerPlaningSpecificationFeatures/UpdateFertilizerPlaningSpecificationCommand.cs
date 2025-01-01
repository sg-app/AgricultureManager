using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningSpecificationFeatures
{
    public class UpdateFertilizerPlaningSpecificationCommand : IReq<FertilizerPlaningSpecificationVm>
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateFertilizerPlaningSpecificationCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateFertilizerPlaningSpecificationCommand, FertilizerPlaningSpecificationVm>
    {
        public async Task<Response<FertilizerPlaningSpecificationVm>> Handle(UpdateFertilizerPlaningSpecificationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var field = await dbContext.FertilizerPlaningSpecification.FindAsync([request.Id], cancellationToken);
            if (field is null)
            {
                return Response.Fail<FertilizerPlaningSpecificationVm>("Eintrag nicht gefunden.");
            }

            mapper.Map(request, field);

            dbContext.FertilizerPlaningSpecification.Update(field);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fieldVm = mapper.Map<FertilizerPlaningSpecificationVm>(field);
            return Response.Success(fieldVm);
        }
    }
}
