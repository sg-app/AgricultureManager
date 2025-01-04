using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Module.Accounting.Models
{
    public enum CostTypeVm
    {
        [Display(Description = "Einnahmen")]
        Income,
        [Display(Description = "Variable Kosten")]
        Variable,
        [Display(Description = "Fixkosten")]
        Fix,
        [Display(Description = "Sonstige Ausgaben")]
        Expenditure
    }
}
