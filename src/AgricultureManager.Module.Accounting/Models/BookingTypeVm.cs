using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Module.Accounting.Models
{
    public class BookingTypeVm
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Short { get; set; }
        [Required(ErrorMessage ="Kostentyp muss ausgewählt sein.")]
        public CostTypeVm? CostType { get; set; }
    }
}
