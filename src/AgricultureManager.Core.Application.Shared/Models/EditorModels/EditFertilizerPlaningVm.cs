namespace AgricultureManager.Core.Application.Shared.Models.EditorModels
{
    public class EditFertilizerPlaningVm : FertilizerPlaningVm
    {
        public Dictionary<FertilizerDetailVm, int> Details { get; set; } = [];
    }
}
