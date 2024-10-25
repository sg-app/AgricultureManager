using AgricultureManager.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FertilizerDetailVm
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }

        public virtual ICollection<FertilizerToDetail> FertilizerToDetails { get; set; } = [];
        public virtual ICollection<Fertilizer> Fertilizers { get; set; } = [];
    }
}
