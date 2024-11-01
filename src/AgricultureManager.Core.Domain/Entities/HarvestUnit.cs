﻿using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Domain.Entities
{
    public class HarvestUnit
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestYearId { get; set; }
        [Required]
        public Guid FieldId { get; set; }

        [MaxLength(150), Required]
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        [Required]
        public Guid CultureId { get; set; }

        public virtual HarvestYear HarvestYear { get; set; } = default!;
        public virtual Field Field { get; set; } = default!;
        public virtual Culture Culture { get; set; } = default!;
    }
}
