using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Keys;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.Json;

namespace AgricultureManager.Module.Pdf.Documents.Documentation
{
    public class HarvestYearDocument(IAppDbContextFactory contextFactory) : IDocument
    {
        private IEnumerable<HarvestUnit> _harvestUnits = [];
        private IEnumerable<Core.Domain.Entities.Unit> _units = [];
        private IEnumerable<Person> _persons = [];
        private IEnumerable<SeedTechnology> _seedTechnologies = [];
        private IEnumerable<SeedCategory> _seedCategories = [];
        private CompanyVm? _company;
        public HarvestYearVm? HarvestYear { get; set; }
        private Guid _currentHarvestUnitId;
        private static readonly string HeadlineColor = Colors.Blue.Lighten5;
        private static readonly string LineColor = Colors.Grey.Darken2;
        private static readonly float HeadlineSize = 10;

        public void Compose(IDocumentContainer container)
        {
            ArgumentNullException.ThrowIfNull(HarvestYear, nameof(HarvestYear));

            Task.Run(LoadData).GetAwaiter().GetResult();

            foreach (var harvestUnit in _harvestUnits)
            {
                _currentHarvestUnitId = harvestUnit.Id;
                container.Page(page =>
                {

                    page.Size(PageSizes.A4.Landscape());
                    page.DefaultTextStyle(t => t.FontSize(8));
                    page.Margin(25);

                    page.Header().CustomHeader($"Schlagdokumentation - Ernte {HarvestYear.Year}");

                    page.Content().Element(ComposeContent);

                    page.Footer().CustomFooter();
                });
            }
        }

        private async Task LoadData()
        {
            using var context = contextFactory.CreateDbContext();
            var cancellationToken = CancellationToken.None;

            var companyKeyValue = await context.Parameter.FindAsync([ParameterKeys.Company], cancellationToken);
            ArgumentNullException.ThrowIfNull(companyKeyValue, nameof(companyKeyValue));
            _company = JsonSerializer.Deserialize<CompanyVm>(companyKeyValue.Value);

            _harvestUnits = await context.HarvestUnit
                .AsNoTracking()
                .Where(f => f.HarvestYearId == HarvestYear!.Id)
                .Include(f => f.Culture)
                .Include(f => f.Field)
                .Include(f => f.HarvestYear)
                .Include(f => f.Seeds)
                .ThenInclude(f => f.Culture)
                .Include(f => f.Fertilizations)
                .ThenInclude(f => f.Fertilizer)
                .ThenInclude(f => f.FertilizerToDetails)
                .Include(f => f.PlantProtections)
                .ThenInclude(f => f.PlantProtectant)
                .Include(f => f.Harvests)
                .Include(f => f.FertilizerPlaningSpecifications)
                .ToListAsync(cancellationToken);

            _units = await context.Unit.AsNoTracking().ToListAsync(cancellationToken);
            _persons = await context.Person.AsNoTracking().ToListAsync(cancellationToken);
            _seedCategories = await context.SeedCategory.AsNoTracking().ToListAsync(cancellationToken);
            _seedTechnologies = await context.SeedTechnology.AsNoTracking().ToListAsync(cancellationToken);
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        private void ComposeContent(IContainer container)
        {
            var harvestUnit = _harvestUnits.FirstOrDefault(f => f.Id == _currentHarvestUnitId);
            if (harvestUnit != null)
            {
                // Ernteeinheit Beschreibung.
                container.Column(c =>
                {
                    // Überschrift
                    c.Item()
                        .AlignCenter()
                        .AlignMiddle()
                        .Text($"{harvestUnit.Name} - {harvestUnit.Field.Name} - {harvestUnit.Culture.Name}")
                        .FontSize(14)
                        .Bold();

                    #region Betrieb Schlag

                    c.Item().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        // Neue Tabelle für Betrieb
                        t.Cell().Row(1).Column(1).Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                            });
                            t.Header(h =>
                            {
                                h.Cell().Element(CellStyle).Text("Betrieb");
                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .DefaultTextStyle(x => x.Bold().FontSize(HeadlineSize))
                                        .PaddingRight(3)
                                        .Background(HeadlineColor)
                                        .AlignCenter();
                                }
                            });
                            var companyName = string.IsNullOrEmpty(_company?.CompanyName) ? $"{_company?.FirstName} {_company?.LastName}" : _company?.CompanyName;
                            t.Cell().Row(1).Column(1).PaddingRight(3).Text(_company?.CompanyNumber);
                            t.Cell().Row(2).Column(1).PaddingRight(3).Text(companyName);
                            t.Cell().Row(3).Column(1).PaddingRight(3).Text($"{_company?.Street} {_company?.Housenumber}, {_company?.Plz} {_company?.City}");
                        });

                        // Neue Tabelle für Schlag
                        t.Cell().Row(1).Column(2).Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.RelativeColumn();
                            });
                            t.Header(h =>
                            {
                                h.Cell().ColumnSpan(2).Element(CellStyle).Text("Schlag");
                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .DefaultTextStyle(x => x.Bold().FontSize(HeadlineSize))
                                        .PaddingLeft(3)
                                        .Background(HeadlineColor)
                                        .AlignCenter();
                                }
                            });

                            t.Cell().Row(1).Column(1).PaddingLeft(3).Text(t =>
                            {
                                t.Span("Flurnummer: ").Bold();
                                t.Span(harvestUnit.Field.Number);
                            });
                            t.Cell().Row(2).Column(1).PaddingLeft(3).Text(t =>
                            {
                                t.Span("Größe: ").Bold();
                                t.Span(harvestUnit.Area.ToString("N2"));
                                t.Span(" ha");
                            });
                            t.Cell().Row(1).Column(2).Text(t =>
                            {
                                t.Span("Schlagname: ").Bold();
                                t.Span(harvestUnit.Field.Name);
                            });
                            t.Cell().Row(2).Column(2).Text(t =>
                            {
                                t.Span("Hauptfrucht: ").Bold();
                                t.Span(harvestUnit.Culture.Name);
                            });
                        });

                    });
                    #endregion

                    #region Aussaat
                    c.Item().LineHorizontal(1).LineColor(LineColor);
                    c.Spacing(5);
                    c.Item().Element(HeadlineStyle).Text(t =>
                    {
                        t.Span("Aussaat");
                        t.AlignCenter();
                    });

                    var seeds = _harvestUnits.First(f => f.Id == _currentHarvestUnitId).Seeds.OrderBy(o => o.Date).ToList();
                    for (int i = 0; i < seeds.Count; i++)
                    {
                        var seed = seeds[i];
                        c.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.RelativeColumn();
                                c.RelativeColumn();
                            });

                            t.Cell().Row(1).Column(1).Text(t =>
                            {
                                t.Span("Datum: ").Bold();
                                t.Span(seed.Date.ToString("d"));
                            });
                            t.Cell().Row(2).Column(1).Text(t =>
                            {
                                t.Span("Aussaattechnik: ").Bold();
                                t.Span(_seedTechnologies.FirstOrDefault(f => f.Id == seed.SeedTechnologyId)?.Name);
                            });
                            t.Cell().Row(3).Column(1).Text(t =>
                            {
                                t.Span("Einstellung: ").Bold();
                                t.Span(seed.Setting);
                            });

                            t.Cell().Row(1).Column(2).Text(t =>
                            {
                                t.Span("Sorte: ").Bold();
                                t.Span(seed.VarietyName);
                            });
                            t.Cell().Row(2).Column(2).Text(t =>
                            {
                                t.Span("Saatgutkategorie: ").Bold();
                                t.Span(_seedCategories.FirstOrDefault(f => f.Id == seed.SeedCategoryId)?.Name);
                            });
                            t.Cell().Row(3).Column(2).Text(t =>
                            {
                                t.Span("Bemerkung: ").Bold();
                                t.Span(seed.Comment);
                            });

                            t.Cell().Row(1).Column(3).Text(t =>
                            {
                                t.Span("Saatmenge: ").Bold();
                                t.Span($"{seed.Quantity} {GetUnitName(seed.UnitId)}");
                            });
                            t.Cell().Row(2).Column(3).Text(t =>
                            {
                                t.Span("Anerkennungsnummer: ").Bold();
                                t.Span(seed.ApprovalNumber);
                            });
                            t.Cell().Row(3).Column(3).Text(t =>
                            {
                                t.Span("Frucht: ").Bold();
                                t.Span(seed.Culture.Name);
                            });
                        });
                        if (i < seeds.Count - 1) { c.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten3); }
                    }
                    #endregion

                    #region Düngung
                    c.Item().LineHorizontal(1).LineColor(LineColor);
                    c.Spacing(5);
                    c.Item().Element(HeadlineStyle).Text(t =>
                    {
                        t.Span("Düngung");
                        t.AlignCenter();
                    });

                    c.Item().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(60);
                            c.RelativeColumn();
                            c.ConstantColumn(40);
                            c.ConstantColumn(40);
                            c.ConstantColumn(20);
                            c.ConstantColumn(20);
                            c.ConstantColumn(20);
                            c.ConstantColumn(20);
                            c.RelativeColumn();
                            c.ConstantColumn(30);
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        t.Header(h =>
                        {
                            h.Cell().Element(CellStyle).Text("Datum");
                            h.Cell().Element(CellStyle).Text("Bezeichnung");
                            h.Cell().Element(CellStyle).AlignRight().Text("Menge");
                            h.Cell().Element(CellStyle).Text("Einheit");
                            h.Cell().Element(CellStyle).AlignRight().Text("N");
                            h.Cell().Element(CellStyle).AlignRight().Text("P");
                            h.Cell().Element(CellStyle).AlignRight().Text("K");
                            h.Cell().Element(CellStyle).AlignRight().Text("S");
                            h.Cell().Element(CellStyle).Text("Anwender");
                            h.Cell().Element(CellStyle).AlignRight().Text("BBCH");
                            h.Cell().Element(CellStyle).Text("Einstellung");
                            h.Cell().Element(CellStyle).Text("Bemerkung");
                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold()).BorderBottom(1).BorderColor(Colors.Black).Padding(3);
                            }
                        });
                        var fertilization = _harvestUnits.First(f => f.Id == _currentHarvestUnitId).Fertilizations;
                        foreach (var item in fertilization.OrderBy(o => o.Date))
                        {
                            t.Cell().Element(CellStyle).Text(item.Date.ToString("d"));
                            t.Cell().Element(CellStyle).Text(item.Fertilizer.Name);
                            t.Cell().Element(CellStyle).AlignRight().Text(item.Dosage.ToString("N2"));
                            t.Cell().Element(CellStyle).Text(GetUnitName(item.UnitId));
                            foreach (var detailId in SystemEntryKeys.FertilizerDetailKeys)
                            {
                                var quantity = item.Fertilizer.FertilizerToDetails.FirstOrDefault(f => f.FertilizerDetailId == detailId)?.Quantity ?? 0;
                                var detailAmount = item.Dosage * quantity;
                                t.Cell().Element(CellStyle).AlignRight().Text(detailAmount.ToString("N0"));
                            }
                            t.Cell().Element(CellStyle).Text(GetPersonName(item.PersonId));
                            t.Cell().Element(CellStyle).AlignRight().Text(item.BBCH);
                            t.Cell().Element(CellStyle).Text(item.Setting);
                            t.Cell().Element(CellStyle).Text(item.Comment);
                            static IContainer CellStyle(IContainer container)
                            {
                                return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingHorizontal(3);
                            }
                        }

                        t.Footer(f =>
                        {
                            f.Cell().ColumnSpan(4).Element(CellStyle).Text("Anforderung:");
                            foreach (var detailId in SystemEntryKeys.FertilizerDetailKeys)
                            {
                                var spec = harvestUnit.FertilizerPlaningSpecifications.FirstOrDefault(f => f.FertilizerDetailId == detailId)?.Quantity ?? 0;
                                f.Cell().Element(CellStyle).AlignRight().Text(spec.ToString("N0"));
                            }
                            f.Cell().ColumnSpan(4).Element(CellStyle);

                            f.Cell().ColumnSpan(4).Element(CellStyle).Text("Summen:");
                            foreach (var detailId in SystemEntryKeys.FertilizerDetailKeys)
                            {
                                var detailAmount = 0.0;
                                foreach (var planingItem in harvestUnit.Fertilizations)
                                {
                                    var quantity = planingItem.Fertilizer.FertilizerToDetails.FirstOrDefault(f => f.FertilizerDetailId == detailId)?.Quantity ?? 0;
                                    detailAmount += Math.Round(planingItem.Dosage * quantity, 0);
                                }
                                f.Cell().Element(CellStyle).AlignRight().Text(detailAmount.ToString("N0"));
                            }
                            f.Cell().ColumnSpan(4).Element(CellStyle);

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Orange.Medium)
                                    .Background(Colors.Orange.Lighten5)
                                    .PaddingHorizontal(3);
                            }
                        });

                    });
                    #endregion

                    #region Pflanzenschutz
                    c.Item().LineHorizontal(1).LineColor(LineColor);
                    c.Spacing(5);
                    c.Item().Element(HeadlineStyle).Text(t =>
                    {
                        t.Span("Pflanzenschutz");
                        t.AlignCenter();
                    });

                    c.Item().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.ConstantColumn(60);
                            c.RelativeColumn();
                            c.ConstantColumn(50);
                            c.ConstantColumn(40);
                            c.RelativeColumn();
                            c.ConstantColumn(30);
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        t.Header(h =>
                        {
                            h.Cell().Element(CellStyle).Text("Datum");
                            h.Cell().Element(CellStyle).Text("Bezeichnung");
                            h.Cell().Element(CellStyle).AlignRight().Text("Menge");
                            h.Cell().Element(CellStyle).Text("Einheit");
                            h.Cell().Element(CellStyle).Text("Anwender");
                            h.Cell().Element(CellStyle).AlignRight().Text("BBCH");
                            h.Cell().Element(CellStyle).Text("Einstellung");
                            h.Cell().Element(CellStyle).Text("Bemerkung");
                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold()).BorderBottom(1).BorderColor(Colors.Black).Padding(3);
                            }
                        });
                        var plantProtections = _harvestUnits.First(f => f.Id == _currentHarvestUnitId).PlantProtections;
                        foreach (var item in plantProtections.OrderBy(o => o.Date))
                        {
                            t.Cell().Element(CellStyle).Text(item.Date.ToString("d"));
                            t.Cell().Element(CellStyle).Text(item.PlantProtectant.Name);
                            t.Cell().Element(CellStyle).AlignRight().Text(item.Dosage.ToString("N2"));
                            t.Cell().Element(CellStyle).Text(GetUnitName(item.UnitId));
                            t.Cell().Element(CellStyle).Text(GetPersonName(item.PersonId));
                            t.Cell().Element(CellStyle).AlignRight().Text(item.BBCH);
                            t.Cell().Element(CellStyle).Text(item.Setting);
                            t.Cell().Element(CellStyle).Text(item.Comment);
                            static IContainer CellStyle(IContainer container)
                            {
                                return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(3);
                            }
                        }

                    });
                    #endregion

                    #region Ernte
                    c.Item().LineHorizontal(1).LineColor(LineColor);
                    c.Spacing(5);
                    c.Item().Element(HeadlineStyle).Text(t =>
                    {
                        t.Span("Ernte");
                        t.AlignCenter();
                    });
                    var harvest = _harvestUnits.First(f => f.Id == _currentHarvestUnitId).Harvests.FirstOrDefault();
                    if (harvest != null)
                    {
                        c.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.RelativeColumn();
                            });

                            t.Cell().Row(1).Column(1).Text(t =>
                            {
                                t.Span("Datum: ").Bold();
                                t.Span(harvest.Date.ToString("d"));
                            });
                            t.Cell().Row(2).Column(1).Text(t =>
                            {
                                t.Span("Einstellung: ").Bold();
                                t.Span(harvest.Setting);
                            });

                            t.Cell().Row(1).Column(2).Text(t =>
                            {
                                t.Span("Menge: ").Bold();
                                t.Span($"{harvest.Quantity} {GetUnitName(harvest.UnitId)}");
                            });
                            t.Cell().Row(2).Column(2).Text(t =>
                            {
                                t.Span("Bemerkung: ").Bold();
                                t.Span(harvest.Comment);
                            });
                        });
                    }
                    #endregion

                });


            }
        }
        private static IContainer HeadlineStyle(IContainer container)
        {
            return container
                .Background(HeadlineColor)
                .DefaultTextStyle(s => s.Bold().FontSize(HeadlineSize));
        }

        private string GetPersonName(Guid? personId)
        {
            if (personId.HasValue)
            {
                var person = _persons.FirstOrDefault(f => f.Id == personId);
                return $"{person?.FirstName} {person?.LastName}";
            }
            return string.Empty;
        }
        private string GetUnitName(Guid? unitId)
        {
            if (unitId.HasValue)
            {
                var unit = _units.FirstOrDefault(f => f.Id == unitId);
                return unit?.Name ?? string.Empty;
            }
            return string.Empty;
        }
    }
}
