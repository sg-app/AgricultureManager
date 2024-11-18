using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Common
{
    public static class CustomItems
    {
        public static HarvestYearVm CreateNewYearItem => new() { Id = Guid.Parse("F6974092-D05E-4FF1-BC83-995CEEFA0A09"), Year = "Erstellen..." };

    }
}
