using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class Fertilizer
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Detail { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public virtual ICollection<FertilizerToDetail> FertilizerToDetails { get; set; } = [];
        public virtual ICollection<FertilizerDetail> FertilizerDetails { get; set; } = [];
    }
}
