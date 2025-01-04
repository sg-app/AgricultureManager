using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Common;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;

namespace AgricultureManager.Module.Accounting.Features.StatementOfAccountFeatures
{
    public record UploadStatementOfAccountCommand(Guid AccountId, int Month, int Year, IBrowserFile File, bool Overwrite) : IReq<StatementOfAccountDocumentVm>
    {
    }
    public class UploadStatementOfAccountCommandHandler(IAccountingDbContextFactory contextFactory, IAppDbContextFactory appContextFactory, IMapper mapper) : IReqHandler<UploadStatementOfAccountCommand, StatementOfAccountDocumentVm>
    {
        public async Task<Response<StatementOfAccountDocumentVm>> Handle(UploadStatementOfAccountCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            using var appContext = appContextFactory.CreateDbContext();
            var basePathKeyValue = await appContext.Parameter.FindAsync([AccountingParameterKeys.AccountingBaseFilePath], cancellationToken);
            var documentBasePathKeyValue = await appContext.Parameter.FindAsync([AccountingParameterKeys.StatementOfAccountBaseFilePath], cancellationToken);
            var documentSaveToDbKeyValue = await appContext.Parameter.FindAsync([AccountingParameterKeys.StatementOfAccountDocumentSaveToDatabase], cancellationToken);
            var basePath = basePathKeyValue?.Value ?? "/share";
            var documentBasePath = documentBasePathKeyValue?.Value ?? "StateOfAccount";

            var fileDir = Path.Combine(documentBasePath, "StateOfAccount", GetFiscalYear(new DateTime(request.Year, request.Month, 1)));
            if (!Directory.Exists(fileDir))
                Directory.CreateDirectory(fileDir);

            var filePath = Path.Combine(fileDir, request.File.Name);
            if (File.Exists(filePath) && request.Overwrite)
                File.Delete(filePath);

            if (File.Exists(filePath))
                return Response.Fail<StatementOfAccountDocumentVm>("Datei existiert bereits.");

            using var fStream = File.Create(filePath);
            if (fStream.Length < 4294967295)
            {
                var doc = new StatementOfAccountDocument
                {
                    Id = Guid.NewGuid(),
                    AccountId = request.AccountId,
                    Month = request.Month,
                    Year = request.Year,
                    Documentname = request.File.Name,
                    Documentpath = filePath
                };

                if (bool.TryParse(documentSaveToDbKeyValue?.Value, out var saveToDb))
                {
                    if (saveToDb)
                    {
                        using var mStream = new MemoryStream();
                        await request.File.OpenReadStream().CopyToAsync(mStream, cancellationToken);
                        doc.Content = mStream.ToArray();
                    }
                }

                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var entityEntry = await context.StatementOfAccountDocument.AddAsync(doc, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    await request.File.OpenReadStream(4294967295, cancellationToken).CopyToAsync(fStream, cancellationToken);
                    return Response.Success(mapper.Map<StatementOfAccountDocumentVm>(doc));
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Response.Fail<StatementOfAccountDocumentVm>("Fehler beim Speichern in der Datenbank.");
                }
            }
            else
            {
                return Response.Fail<StatementOfAccountDocumentVm>("Datei ist zu groß. Maximal 4GB erlaubt.");
            }
        }
        private static string GetFiscalYear(DateTime? dateTime)
        {
            var currentDate = dateTime ?? DateTime.Now.Date;
            if (currentDate.Month >= 7)
                return $"{currentDate.Year}_{currentDate.AddYears(1).Year}";
            else
                return $"{currentDate.AddYears(-1).Year}_{currentDate.Year}";
        }
    }
}
