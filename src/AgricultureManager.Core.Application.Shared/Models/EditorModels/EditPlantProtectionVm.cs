using AgricultureManager.Core.Application.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models.EditorModels
{
    public class EditPlantProtectionVm
    {
        [Required(ErrorMessage = "Datum muss ausgewählt sein.")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        [NotEmptyGuid(ErrorMessage = "Pflanzenschutzmittel muss ausgewählt sein.")]
        public Guid PlantProtectantId { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(2, ErrorMessage = "BBCH kann nicht mehr als {1} Zeichen enthalten.")]
        public string? BBCH { get; set; }

        public IList<EditPlantProtectionDetailVm> Details { get; set; } = [];
    }
}
