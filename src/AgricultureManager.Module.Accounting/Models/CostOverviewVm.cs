namespace AgricultureManager.Module.Accounting.Models
{
    public class CostOverviewVm
    {
        public CostTypeVm? CostType { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<BookingVm> Bookings { get; set; } = [];
    }
}
