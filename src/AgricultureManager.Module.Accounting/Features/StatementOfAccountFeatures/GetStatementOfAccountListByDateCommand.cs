using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace AgricultureManager.Module.Accounting.Features.StatementOfAccountFeatures
{
    public record GetStatementOfAccountListByDateCommand(DateTime? StartDate, DateTime? EndDate) : IReq<ICollection<StatementOfAccountDocumentVm>>
    {
    }

    public class GetStatementOfAccountListByDateCommandHandler(IAccountingDbContextFactory contextFactory, IMapper mapper) : IReqHandler<GetStatementOfAccountListByDateCommand, ICollection<StatementOfAccountDocumentVm>>
    {
        public async Task<Response<ICollection<StatementOfAccountDocumentVm>>> Handle(GetStatementOfAccountListByDateCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            var query = context.StatementOfAccountDocument.AsQueryable();

            if (request.StartDate.HasValue)
            {
                var startYear = request.StartDate.Value.Year;
                var startMonth = request.StartDate.Value.Month;
                query = query.Where(f => (f.Year > startYear) || (f.Year == startYear && f.Month >= startMonth));
            }

            if (request.EndDate.HasValue)
            {
                var endYear = request.EndDate.Value.Year;
                var endMonth = request.EndDate.Value.Month;
                query = query.Where(f => (f.Year < endYear) || (f.Year == endYear && f.Month <= endMonth));
            }

            var entities = await query.OrderBy(f => f.Year).ThenBy(f => f.Month).ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<ICollection<StatementOfAccountDocumentVm>>(entities));
        }
    }
}
