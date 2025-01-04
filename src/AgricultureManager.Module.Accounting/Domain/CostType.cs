using System.ComponentModel.DataAnnotations;

namespace AgricultureManager.Module.Accounting.Domain
{
    public enum CostType
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
