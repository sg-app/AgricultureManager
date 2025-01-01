using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningSpecificationFeatures
{
    public record BulkUpdateFertilizerPlaningSpecificationCommand(Guid HarvestUnitId, ICollection<FertilizerPlaningSpecificationVm> Items) : IReq
    {
    }
    public class BulkUpdateFertilizerPlaningSpecificationCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<BulkUpdateFertilizerPlaningSpecificationCommand>
    {
        public async Task<ResponseLess> Handle(BulkUpdateFertilizerPlaningSpecificationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var dbItems = await dbContext.FertilizerPlaningSpecification
                .Where(x => x.HarvestUnitId == request.HarvestUnitId)
                .ToListAsync(cancellationToken);

            // Ermitteln der neuen Einträge
            var newItems = request.Items
                .Where(x => x.Id == Guid.Empty)
                .Select(x => new FertilizerPlaningSpecification
                {
                    Id = Guid.NewGuid(),
                    HarvestUnitId = request.HarvestUnitId,
                    FertilizerDetailId = x.FertilizerDetailId,
                    Quantity = x.Quantity
                })
                .ToList();
            // Hinzufügen der neuen Einträge
            await dbContext.FertilizerPlaningSpecification.AddRangeAsync(newItems, cancellationToken);

            // Aktualisieren der vorhandenen Einträge
            var updatedItems = dbItems
                .Where(db => request.Items.Any(x => x.Id == db.Id))
                .ToList();

            foreach (var dbItem in updatedItems)
            {
                var requestItem = request.Items.First(x => x.Id == dbItem.Id);
                mapper.Map(requestItem, dbItem);
                dbContext.Entry(dbItem).State = EntityState.Modified;
            }

            // Löschen der nicht mehr vorhandenen Einträge
            var deletedItems = dbItems
                .Where(db => !request.Items.Any(x => x.Id == db.Id))
                .ToList();

            dbContext.FertilizerPlaningSpecification.RemoveRange(deletedItems);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Success();
        }
    }
}
