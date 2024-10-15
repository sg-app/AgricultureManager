using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class HarvestYearVm
    {
        public Guid Id { get; set; }
        [MaxLength(4), Required]
        public string Year { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }

        public virtual CompanyVm Company { get; set; } = default!;
    }
}
