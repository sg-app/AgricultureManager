using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerFeatures
{
    public class UpdateFertilizerCommand : IReq<FertilizerVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Detail { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateFertilizerCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateFertilizerCommand, FertilizerVm>
    {
        public async Task<Response<FertilizerVm>> Handle(UpdateFertilizerCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var fertilizer = await dbContext.Fertilizer.FindAsync([request.Id], cancellationToken);
            if (fertilizer is null)
            {
                return Response.Fail<FertilizerVm>("Dünger nicht gefunden.");
            }

            mapper.Map(request, fertilizer);

            dbContext.Fertilizer.Update(fertilizer);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fertilizerVm = mapper.Map<FertilizerVm>(fertilizer);
            return Response.Success(fertilizerVm);
        }
    }
}
