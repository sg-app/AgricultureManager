using AgricultureManager.Core.Application.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models.EditorModels
{
    public class EditFertilizationVm
    {
        [Required(ErrorMessage = "Datum muss ausgewählt sein.")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        [NotEmptyGuid(ErrorMessage = "Dünger muss ausgewählt sein.")]
        public Guid FertilizerId { get; set; }
        public Guid? UnitId { get; set; }
        [MaxLength(2, ErrorMessage = "BBCH kann nicht mehr als {1} Zeichen enthalten.")]
        public string? BBCH { get; set; }

        public IList<EditFertilizationDetailVm> Details { get; set; } = [];
    }
}
