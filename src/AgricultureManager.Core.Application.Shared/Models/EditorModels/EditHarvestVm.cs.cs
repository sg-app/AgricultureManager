using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models.EditorModels
{
    public class EditHarvestVm
    {
        [Required(ErrorMessage = "Datum muss ausgewählt sein.")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? UnitId { get; set; }

        public IList<EditHarvestDetailVm> Details { get; set; } = [];
    }
}
