using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FertilizerVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Detail { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public ICollection<FertilizerToDetailVm> FertilizerToDetails { get; set; } = [];


        public string Details => string.Join(", ", FertilizerToDetails.Select(x => x.ToString()));
    }
}

