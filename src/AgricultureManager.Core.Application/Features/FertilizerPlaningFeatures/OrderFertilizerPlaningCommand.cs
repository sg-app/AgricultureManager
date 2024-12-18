using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningFeatures
{
    public enum OrderDirection
    {
        Upwards,
        Downwards
    }
    public record OrderFertilizerPlaningCommand(OrderDirection OrderDirection, FertilizerPlaningVm Item) : IReq
    {
    }
    public class OrderFertilizerPlaningCommandHandler(IAppDbContextFactory contextFactory) : IReqHandler<OrderFertilizerPlaningCommand>
    {
        public async Task<ResponseLess> Handle(OrderFertilizerPlaningCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();

            // Laden der Entitäten der HarvestUnit
            var items = await context.FertilizerPlaning
                .Where(x => x.HarvestUnitId == request.Item.HarvestUnitId)
                .OrderBy(x => x.Order)
                .ToListAsync(cancellationToken);

            // Finden des aktuellen Items
            var currentItem = items.FirstOrDefault(x => x.Id == request.Item.Id);
            if (currentItem == null)
            {
                return Response.Fail("Item not found.");
            }

            // Anpassen der Reihenfolge basierend auf der OrderDirection
            if (request.OrderDirection == OrderDirection.Upwards)
            {
                var previousItem = items.LastOrDefault(x => x.Order < currentItem.Order);
                if (previousItem != null)
                {
                    // Tauschen der Order-Werte
                    var tempOrder = currentItem.Order;
                    currentItem.Order = previousItem.Order;
                    previousItem.Order = tempOrder;
                }
            }
            else if (request.OrderDirection == OrderDirection.Downwards)
            {
                var nextItem = items.FirstOrDefault(x => x.Order > currentItem.Order);
                if (nextItem != null)
                {
                    // Tauschen der Order-Werte
                    var tempOrder = currentItem.Order;
                    currentItem.Order = nextItem.Order;
                    nextItem.Order = tempOrder;
                }
            }
            // Speichern der Änderungen
            context.FertilizerPlaning.UpdateRange(items);
            await context.SaveChangesAsync(cancellationToken);

            return Response.Success();
        }
    }
}
