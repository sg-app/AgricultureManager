namespace AgricultureManager.Core.Application.Shared.Models
{
    public class YearFieldVm
    {
        public Guid Id { get; set; }
        public Guid HarvestYearId { get; set; }
        public Guid FieldId { get; set; }

        public virtual HarvestYearVm HarvestYear { get; set; } = default!;
        public virtual FieldVm Field { get; set; } = default!;
    }
}
