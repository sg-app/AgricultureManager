using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AgricultureManager.Module.Pdf.Documents.Planing
{
    public class FertlizerPlaningDocument(IAppDbContextFactory contextFactory) : IDocument
    {

        private IEnumerable<HarvestUnit> _harvestUnits = [];
        public HarvestYearVm? HarvestYear { get; set; }
        public void Compose(IDocumentContainer container)
        {
            ArgumentNullException.ThrowIfNull(HarvestYear, nameof(HarvestYear));

            Task.Run(LoadData).GetAwaiter().GetResult();

            container.Page(page =>
            {

                page.Size(PageSizes.A4.Landscape());
                page.DefaultTextStyle(t => t.FontSize(10));
                page.Margin(25);

                page.Header().CustomHeader($"Düngeplanung - {HarvestYear.Year}");

                page.Content().Element(ComposeContent);

                page.Footer().CustomFooter();
            });
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        private async Task LoadData()
        {
            using var context = contextFactory.CreateDbContext();

            _harvestUnits = await context.HarvestUnit
                .Include(f => f.Culture)
                .Include(f => f.Field)
                .Include(f => f.FertilizerPlanings)
                .ThenInclude(f => f.Fertilizer)
                .ThenInclude(f => f.FertilizerDetails)
                .Include(f => f.FertilizerPlaningSpecifications)
                .ThenInclude(f => f.FertilizerDetail)
                .Where(f => f.HarvestYearId == HarvestYear!.Id && f.FertilizerPlanings.Any())
                .ToListAsync();
        }
        private void ComposeContent(IContainer container)
        {
            container.Column(c =>
            {

                foreach (var harvestUnit in _harvestUnits)
                {
                    c.Item().ShowEntire().Column(c =>
                    {
                        c.Item().Element(Headline2).AlignCenter().Text($"{harvestUnit.Name} - {harvestUnit.Field.Name} - {harvestUnit.Culture.Name} - {harvestUnit.Area:N2}");

                        c.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.ConstantColumn(150); //Dünger
                                c.ConstantColumn(70); //Dosierung
                                for (int i = 0; i < harvestUnit.FertilizerPlaningSpecifications.Count; i++)
                                {
                                    c.ConstantColumn(40); //Für jedes Detail
                                }
                                c.ConstantColumn(70); //Gesamt
                                c.ConstantColumn(70); //Datum
                                c.ConstantColumn(70); //Gesamt
                            });

                            //Header
                            t.Header(h =>
                            {
                                h.Cell().Element(CellStyle).Text("Dünger");
                                h.Cell().Element(CellStyle).AlignRight().Text("Dosierung");
                                foreach (var spec in harvestUnit.FertilizerPlaningSpecifications.OrderBy(f => f.FertilizerDetail.Name))
                                {
                                    h.Cell().Element(CellStyle).AlignRight().Text(spec.FertilizerDetail.Name);
                                }
                                h.Cell().Element(CellStyle).AlignRight().Text("Gesamt");
                                h.Cell().Element(CellStyle).Text("Datum");
                                h.Cell().Element(CellStyle).Text("Gesamt");
                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .DefaultTextStyle(x => x.Bold())
                                        .Background(Colors.Indigo.Lighten4)
                                        .PaddingHorizontal(3)
                                        .PaddingVertical(1);
                                }
                            });

                            //Eintrag von jedem Dünger
                            var planingItems = harvestUnit.FertilizerPlanings ?? [];
                            foreach (var planingItem in planingItems.OrderBy(f => f.Order))
                            {
                                t.Cell().Element(CellStyle).Text(planingItem.Fertilizer.Name);
                                t.Cell().Element(CellStyle).AlignRight().Text($"{planingItem.Dosage:N2} dt/ha");
                                foreach (var spec in harvestUnit.FertilizerPlaningSpecifications.OrderBy(f => f.FertilizerDetail.Name))
                                {
                                    var detailAmount = planingItem.Dosage * planingItem.Fertilizer.FertilizerToDetails.First(f => f.FertilizerDetailId == spec.FertilizerDetailId).Quantity;
                                    t.Cell().Element(CellStyle).AlignRight().Text(Math.Round(detailAmount, 0).ToString("N0"));
                                }
                                var amount = harvestUnit.Area * planingItem.Dosage * 100;
                                t.Cell().Element(CellStyle).AlignRight().Text($"{amount:N0} kg");
                                t.Cell().Element(CellStyle);
                                t.Cell().Element(CellStyle);
                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten3)
                                        .PaddingHorizontal(3);
                                }

                            }
                            //Fußzeile
                            t.Footer(f =>
                            {

                                t.Cell().ColumnSpan(2).Element(CellStyle).Text("Summe");
                                foreach (var spec in harvestUnit.FertilizerPlaningSpecifications.OrderBy(f => f.FertilizerDetail.Name))
                                {
                                    var detailAmount = 0.0;
                                    foreach (var planingItem in planingItems)
                                    {
                                        detailAmount += Math.Round(planingItem.Dosage * planingItem.Fertilizer.FertilizerToDetails.First(f => f.FertilizerDetailId == spec.FertilizerDetailId).Quantity, 0);
                                    }
                                    t.Cell().Element(CellStyle).AlignRight().Text(detailAmount.ToString("N0"));
                                }
                                t.Cell().ColumnSpan(3).Element(CellStyle);


                                t.Cell().ColumnSpan(2).Element(CellStyle).Text("Anforderung");
                                foreach (var spec in harvestUnit.FertilizerPlaningSpecifications.OrderBy(f => f.FertilizerDetail.Name))
                                {
                                    t.Cell().Element(CellStyle).AlignRight().Text(spec.Quantity.ToString("N0"));
                                }
                                t.Cell().ColumnSpan(3).Element(CellStyle);

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten3)
                                        .Background(Colors.Blue.Lighten5)
                                        .PaddingHorizontal(3);
                                }

                                t.Cell().ColumnSpan(2).Element(CellStyle2).Text("Differenz");

                                foreach (var spec in harvestUnit.FertilizerPlaningSpecifications.OrderBy(f => f.FertilizerDetail.Name))
                                {
                                    var detailAmount = 0.0;
                                    foreach (var planingItem in planingItems)
                                    {
                                        detailAmount += Math.Round(planingItem.Dosage * planingItem.Fertilizer.FertilizerToDetails.First(f => f.FertilizerDetailId == spec.FertilizerDetailId).Quantity, 0);
                                    }
                                    var difference = spec.Quantity - detailAmount;
                                    var color = difference < 0 ? Colors.Red.Darken2 : Colors.Green.Darken2;
                                    t.Cell().Element(CellStyle2).AlignRight().Text(difference.ToString("N0")).FontColor(color);
                                }
                                t.Cell().ColumnSpan(3).Element(CellStyle2);

                                static IContainer CellStyle2(IContainer container)
                                {
                                    return container
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten3)
                                        .Background(Colors.Grey.Lighten2)
                                        .PaddingHorizontal(3);
                                }
                            });
                        });
                    });
                }

                c.Item().PageBreak();
                c.Item().Column(c =>
                {
                    c.Item().Element(Headline2).AlignCenter().Text($"Gesamtübersicht {HarvestYear!.Year}");
                    c.Item().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(200);
                            c.ConstantColumn(50);
                            c.ConstantColumn(250);
                            c.ConstantColumn(80);
                        });

                        t.Header(h =>
                        {
                            h.Cell().Element(CellStyle).Text("Kultur");
                            h.Cell().Element(CellStyle).AlignRight().Text("Gabe");
                            h.Cell().Element(CellStyle).Text("Dünger");
                            h.Cell().Element(CellStyle).AlignRight().Text("Gesamt");
                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .DefaultTextStyle(x => x.Bold())
                                    .Background(Colors.Indigo.Lighten4)
                                    .Padding(3);
                            }
                        });
                        var summary = _harvestUnits
                            .SelectMany(h => h.FertilizerPlanings, (h, fp) => new { h.Culture, fp.Fertilizer, fp.Order, Planing = fp, HarvestUnit = h })
                            .GroupBy(x => new { x.Culture, x.Fertilizer, x.Order })
                            .Select(g => new
                            {
                                CultureName = g.Key.Culture.Name,
                                FertilizerName = g.Key.Fertilizer.Name,
                                g.Key.Order,
                                Amount = Math.Round(g.Sum(f => f.Planing.Dosage * f.HarvestUnit.Area) * 100, 0)
                            })
                            .OrderBy(o => o.CultureName)
                            .ThenBy(o => o.Order);

                        foreach (var item in summary)
                        {
                            t.Cell().Element(CellStyle).Text(item.CultureName);
                            t.Cell().Element(CellStyle).AlignRight().Text(item.Order.ToString());
                            t.Cell().Element(CellStyle).Text(item.FertilizerName);
                            t.Cell().Element(CellStyle).AlignRight().Text($"{item.Amount} kg");
                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .Border(1)
                                    .BorderColor(Colors.Grey.Lighten3)
                                    .Padding(3);
                            }
                        }
                        t.Footer(f =>
                        {
                            var grouped = summary.GroupBy(g => g.FertilizerName);
                            foreach (var item in grouped)
                            {
                                t.Cell().ColumnSpan(2).Element(CellStyle);
                                t.Cell().Element(CellStyle).Text(item.Key);
                                t.Cell().Element(CellStyle).AlignRight().Text($"{item.Sum(f => f.Amount)} kg");
                            }

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .DefaultTextStyle(x => x.Bold())
                                    .Border(1)
                                    .BorderColor(Colors.Grey.Lighten3)
                                    .Background(Colors.Blue.Lighten5)
                                    .PaddingHorizontal(3);
                            }
                        });
                    });
                });
            });
        }

        private static IContainer Headline2(IContainer container)
        {
            return container
                .DefaultTextStyle(x => x.Bold().FontSize(12))
                .PaddingTop(10);
        }


    }
}
