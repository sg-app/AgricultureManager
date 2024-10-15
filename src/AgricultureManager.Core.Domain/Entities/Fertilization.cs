﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Core.Domain.Entities
{
    public class Fertilization
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid HarvestUnitId { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public Guid FertilizerId { get; set; }
        public double Dosage { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(2)]
        public string? BBCH { get; set; }
        [MaxLength(250)]
        public string? Setting { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        public virtual HarvestUnit HarvestUnit { get; set; } = default!;
        public virtual Person? Person { get; set; }
        public virtual Fertilizer Fertilizer { get; set; } = default!;
        public virtual Unit? Unit { get; set; }
    }
}
