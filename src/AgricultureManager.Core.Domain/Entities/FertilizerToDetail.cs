using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureManager.Core.Domain.Entities
{
    public class FertilizerToDetail
    {
        public Guid FertilizerId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }

        public virtual Fertilizer Fertilizer { get; set; } = default!;
        public virtual FertilizerDetail FertilizerDetail { get; set; } = default!;
    }
}
