using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AgricultureManager.Module.Pdf.Documents.Statistics
{
    public class CropRotationDocument(IAppDbContextFactory contextFactory) : IDocument
    {
        private IEnumerable<string> _years = [];
        private List<CropRotation> _cropRotations = [];
        public void Compose(IDocumentContainer container)
        {
            Task.Run(LoadData).GetAwaiter().GetResult();
            container.Page(page =>
            {

                page.Size(PageSizes.A4.Landscape());
                page.DefaultTextStyle(t => t.FontSize(10));
                page.Margin(25);

                page.Header().CustomHeader($"Fruchtfolgen");

                page.Content().Element(ComposeContent);

                page.Footer().CustomFooter();
            });
        }

        private async Task LoadData()
        {
            var currentYear = DateTime.Now.Year;
            _years = Enumerable.Range(currentYear - 7, 12).Select(y => y.ToString());

            using var context = contextFactory.CreateDbContext();
            _cropRotations = await context.HarvestUnit
                .Include(f => f.Culture)
                .Include(f => f.HarvestYear)
                .Include(f => f.Field)
                .Where(f => _years.Contains(f.HarvestYear.Year))
                .Select(s => new CropRotation
                {
                    Id = s.Id,
                    HarvestYear = s.HarvestYear,
                    Number = s.Name,
                    Name = s.Field.Name,
                    Area = s.Area,
                    Culture = s.Culture.Name
                }).ToListAsync();

        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        private void ComposeContent(IContainer container)
        {
            container.Column(c =>
            {
                c.Item().PaddingTop(20).Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn();
                        c.RelativeColumn();
                        c.RelativeColumn();
                    });

                    var groupedUnits = _cropRotations
                        .GroupBy(g => g.Number)
                        .Select(s => new
                        {
                            s.Key,
                            Units = s.OrderBy(x => x.HarvestYear.Year)
                        });

                    foreach (var item in groupedUnits)
                    {
                        t.Cell().ShowEntire().PaddingBottom(5).Element(c => UnitTable(c, item.Key, item.Units));
                    }
                });
            });
        }
        private void UnitTable(IContainer container, string number, IEnumerable<CropRotation> units)
        {
            container.Table(t =>
            {
                t.ColumnsDefinition(c =>
                {
                    c.ConstantColumn(40);
                    c.ConstantColumn(40);
                    c.ConstantColumn(140);
                });
                t.Header(h =>
                {
                    h.Cell().ColumnSpan(3).Element(CellStyle).Text($"{number} - {units.FirstOrDefault()?.Name}");
                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .DefaultTextStyle(x => x.Bold().FontSize(12))
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Background(Colors.Indigo.Lighten4)
                            .AlignCenter()
                            .AlignMiddle()
                            .Padding(2);
                    }
                });

                foreach (var year in _years.OrderBy(o => o))
                {
                    t.Cell().Element(RowHeaderCellStyle).Text(year);
                    t.Cell().Element(CellStyle).AlignRight().Text(units.FirstOrDefault(f => f.HarvestYear.Year == year)?.Area.ToString("N2"));
                    t.Cell().Element(CellStyle).Text(units.FirstOrDefault(f => f.HarvestYear.Year == year)?.Culture);
                }
                static IContainer RowHeaderCellStyle(IContainer container)
                {
                    return container
                        .Border(1)
                        .BorderColor(Colors.Grey.Lighten1)
                        .Background(Colors.Blue.Lighten5)
                        .PaddingHorizontal(3);
                }
                static IContainer CellStyle(IContainer container)
                {
                    return container
                        .Border(1)
                        .BorderColor(Colors.Grey.Lighten1)
                        .PaddingHorizontal(3);
                }

            });
        }
        internal class CropRotation
        {
            public Guid Id { get; set; }
            public HarvestYear HarvestYear { get; set; } = default!;
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public float Area { get; set; }
            public string Culture { get; set; } = string.Empty;
        }

    }

}
