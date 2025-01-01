using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.SKCharts;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace AgricultureManager.Module.Pdf.Documents.Statistics
{
    public class CultivatedAreasDocument(IAppDbContextFactory contextFactory) : IDocument
    {
        public HarvestYearVm? HarvestYear { get; set; }
        private List<CultivatedArea> _cultivatedAreas = new();
        public void Compose(IDocumentContainer container)
        {
            ArgumentNullException.ThrowIfNull(HarvestYear, nameof(HarvestYear));
            Task.Run(LoadData).GetAwaiter().GetResult();

            container.Page(page =>
            {

                page.Size(PageSizes.A4.Landscape());
                page.DefaultTextStyle(t => t.FontSize(10));
                page.Margin(25);

                page.Header().CustomHeader($"Anbauflächen - {HarvestYear.Year}");

                page.Content().Element(ComposeContent);

                page.Footer().CustomFooter();
            });
        }
        private async Task LoadData()
        {
            using var context = contextFactory.CreateDbContext();
            var areaSumOfYear = context.HarvestUnit.Where(f => f.HarvestYearId == HarvestYear!.Id).Sum(f => f.Area);

            _cultivatedAreas = await context.HarvestUnit
                .Include(f => f.Culture)
                .Where(f => f.HarvestYearId == HarvestYear!.Id)
                .GroupBy(g => g.Culture)
                .Select(s => new CultivatedArea
                {
                    Culture = s.Key.Name,
                    CultureShort = s.Key.ShortName,
                    Areas = (decimal)s.Sum(f => f.Area),
                    Percentage = (decimal)(s.Sum(f => f.Area) / areaSumOfYear * 100)
                })
                .OrderByDescending(o => o.Areas)
                .ToListAsync();
        }
        private class CultivatedArea
        {
            public string Culture { get; set; } = string.Empty;
            public string? CultureShort { get; set; }
            public decimal Areas { get; set; }
            public decimal Percentage { get; set; }
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        private void ComposeContent(IContainer container)
        {
            container.PaddingTop(20).Column(c =>
            {
                c.Item().Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.ConstantColumn(250);
                        c.ConstantColumn(70);
                        c.ConstantColumn(70);
                    });
                    t.Header(h =>
                    {
                        h.Cell().Element(CellStyle).Text("Kultur");
                        h.Cell().Element(CellStyle).AlignRight().Text("Fläche");
                        h.Cell().Element(CellStyle).AlignRight().Text("Anteil");
                        static IContainer CellStyle(IContainer container)
                        {
                            return container
                                .DefaultTextStyle(x => x.Bold().FontSize(12))
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten1)
                                .Background(Colors.Indigo.Lighten4)
                                .Padding(3);
                        }
                    });

                    foreach (var item in _cultivatedAreas)
                    {
                        t.Cell().Element(CellStyle).Text(item.Culture);
                        t.Cell().Element(CellStyle).AlignRight().Text(item.Areas.ToString("N2") + " ha");
                        t.Cell().Element(CellStyle).AlignRight().Text(item.Percentage.ToString("N1") + " %");
                    }
                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Padding(3);
                    }

                });
                c.Item().Element(PieChartView);
            });
        }

        private void PieChartView(IContainer container)
        {
            var colors = new string[] { "#fd7f6f", "#7eb0d5", "#b2e061", "#bd7ebe", "#ffb55a", "#ffee65", "#beb9db", "#fdcce5", "#8bd3c7" };

            container.Padding(5).ExtendHorizontal().Height(300).SkiaSharpCanvas((c, s) =>
            {

                var series = new List<ISeries>();
                var colorIdx = 0;
                foreach (var item in _cultivatedAreas)
                {
                    series.Add(
                        new PieSeries<decimal>
                        {
                            Values = [item.Areas],
                            DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                            DataLabelsSize = 10,
                            DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer,
                            DataLabelsFormatter = point => $"{item.CultureShort}: {item.Areas:N2} ha",
                            Fill = new SolidColorPaint(SKColor.Parse(colors[colorIdx]))
                        });
                    colorIdx++;
                }

                var chart = new SKPieChart()
                {
                    Height = (int)s.Height,
                    Width = (int)s.Width,
                    Series = series,
                };
                chart.SaveImage(c);
            });
        }
    }
}
