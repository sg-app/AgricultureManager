﻿using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models
{
    public class CultureVm
    {
        public Guid Id { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4)]
        public string? Code { get; set; }
        [MaxLength(10)]
        public string? ShortName { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
