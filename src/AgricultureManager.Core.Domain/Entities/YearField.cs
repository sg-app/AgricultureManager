namespace AgricultureManager.Core.Domain.Entities
{
    public class YearField
    {
        public Guid Id { get; set; }
        public Guid HarvestYearId { get; set; }
        public Guid FieldId { get; set; }

        public virtual HarvestYear HarvestYear { get; set; } = default!;
        public virtual Field Field { get; set; } = default!;
    }
}
