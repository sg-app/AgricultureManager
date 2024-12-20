using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class FertilizerDetail
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Comment { get; set; }
        public bool SystemEntry { get; set; }

        public virtual ICollection<FertilizerToDetail> FertilizerToDetails { get; set; } = [];
        public virtual ICollection<Fertilizer> Fertilizers { get; set; } = [];
    }
}
