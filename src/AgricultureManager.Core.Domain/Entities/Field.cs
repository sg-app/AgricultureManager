﻿using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class Field
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(45), Required]
        public string Number { get; set; } = string.Empty;
        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public virtual ICollection<HarvestUnit> HarvestUnits { get; set; } = default!;
    }
}
