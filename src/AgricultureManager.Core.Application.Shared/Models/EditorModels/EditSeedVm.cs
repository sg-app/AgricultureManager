using AgricultureManager.Core.Application.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Core.Application.Shared.Models.EditorModels
{
    public class EditSeedVm
    {
        [Required(ErrorMessage = "Datum muss ausgewählt sein.")]
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        [NotEmptyGuid(ErrorMessage = "Kultur muss ausgewählt sein.")]
        public Guid CultureId { get; set; }
        public bool IsMainCulture { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SeedTechnologyId { get; set; }

        public IList<EditSeedDetailVm> Details { get; set; } = [];
    }
}
