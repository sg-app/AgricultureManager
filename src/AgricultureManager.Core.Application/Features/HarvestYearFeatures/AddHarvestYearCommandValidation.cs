using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.HarvestYearFeatures
{
    public class AddHarvestYearCommandValidator : AbstractValidator<AddHarvestYearCommand>
    {
        private readonly IAppDbContextFactory _dbContextFactory;

        public AddHarvestYearCommandValidator(IAppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;

            RuleFor(r => r.Year).MustAsync(CheckUnique).WithMessage("Das Jahr ist bereits vorhanden.");
        }

        private async Task<bool> CheckUnique(string year, CancellationToken token)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var existYear = await dbContext.HarvestYear
            .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Year == year, token);

            return existYear is null;
        }
    }
}
