using AgricultureManager.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class FertilizerToDetailVm
    {
        public Guid FertilizerId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }

        public virtual FertilizerVm Fertilizer { get; set; } = default!;
        public virtual FertilizerDetailVm FertilizerDetail { get; set; } = default!;
    }
}
