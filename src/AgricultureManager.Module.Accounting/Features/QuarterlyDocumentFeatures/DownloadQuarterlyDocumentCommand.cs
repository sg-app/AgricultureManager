using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace AgricultureManager.Module.Accounting.Features.QuarterlyDocumentFeatures
{
    public record DownloadQuarterlyDocumentCommand(Guid AccountId, int Quarter, int Year) : IReq<byte[]>
    {
    }

    public class DownloadQuarterlyDocumentCommandHandler(IAccountingDbContextFactory contextFactory) : IReqHandler<DownloadQuarterlyDocumentCommand, byte[]>
    {
        public async Task<Response<byte[]>> Handle(DownloadQuarterlyDocumentCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();
            var quaraterMont = GetMonthForQuarter(request.Quarter);
            var statementFiles = await context.StatementOfAccountDocument
                .Where(
                    f => f.AccountId == request.AccountId && 
                    f.Year == request.Year &&
                    quaraterMont.Contains(f.Month)
                )
                .Select(f => f.Documentpath).
                ToListAsync(cancellationToken);

            // Get all documents
            var documents = await context.Document
                .Include(f => f.AccountMouvement)
                .Where(f =>
                    f.AccountMouvement!.AccountId == request.AccountId &&
                    f.AccountMouvement!.ValueDate.Year == request.Year &&
                    quaraterMont.Contains(f.AccountMouvement!.ValueDate.Month))
                .Select(f => f.Documentpath)
                .ToListAsync(cancellationToken);

            documents.AddRange(statementFiles);


            //using var zipFile = new ZipFile();
            //zipFile.AlternateEncodingUsage = ZipOption.Always;
            //zipFile.AlternateEncoding = Encoding.UTF8;
            //// Check files exists.
            //foreach (var item in documents)
            //{
            //    if (File.Exists(item))
            //        zipFile.AddFile(item, "");
            //}

            //using var ms = new MemoryStream();
            //zipFile.Save(ms);

            using var memoryStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var filePath in documents)
                {
                    if (File.Exists(filePath))
                    {
                        var fileName = Path.GetFileName(filePath);
                        var zipEntry = zipArchive.CreateEntry(fileName, CompressionLevel.Fastest);
                        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        using var entryStream = zipEntry.Open();
                        await fileStream.CopyToAsync(entryStream, cancellationToken);
                    }
                }
            }

            memoryStream.Position = 0;
            return Response.Success(memoryStream.ToArray());

            //return Response.Success($"{request.Year}_Q{request.Quarter}_quarterlyDocuments", ResponseType.Ok, ms.ToArray());
        }


        private static IEnumerable<int> GetMonthForQuarter(int quarter)
            => quarter switch
            {
                1 => [1, 2, 3],
                2 => [4, 5, 6],
                3 => [7, 8, 9],
                4 => [10, 11, 12],
                _ => throw new ArgumentException("Ungültiges Quartal", nameof(quarter)),
            };

    }
}
