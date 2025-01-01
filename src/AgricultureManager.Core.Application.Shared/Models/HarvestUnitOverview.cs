namespace AgricultureManager.Core.Application.Shared.Models
{
    public class HarvestUnitOverview
    {
        public Guid Id { get; set; }
        public string HarvestUnitName { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public float Area { get; set; }
        public string CultureShortName { get; set; } = string.Empty;
        public HarvestUnitVm HarvestUnit { get; set; } = default!;

        public string ListValue => $"{HarvestUnitName} ({FieldName})";
    }
}
