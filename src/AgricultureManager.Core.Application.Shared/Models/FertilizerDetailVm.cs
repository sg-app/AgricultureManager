using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FertilizerDetailVm : ISystemEntry
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }

        public bool SystemEntry { get; set; }
        public virtual ICollection<FertilizerToDetailVm> FertilizerToDetails { get; set; } = [];
        public virtual ICollection<FertilizerVm> Fertilizers { get; set; } = [];
    }
}
