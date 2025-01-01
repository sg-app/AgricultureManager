namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FertilizerPlaningSpecificationVm
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }

        public FertilizerDetailVm FertilizerDetail { get; set; } = default!;
    }
}
