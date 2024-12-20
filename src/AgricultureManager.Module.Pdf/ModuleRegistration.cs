using AgricultureManager.Module.Api.Interfaces;
using AgricultureManager.Module.Pdf.Documents.Planing;
using AgricultureManager.Module.Pdf.Documents.Statistics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quest = QuestPDF.Infrastructure;

namespace AgricultureManager.Module.Pdf
{
    public class ModuleRegistration : IServerStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            QuestPDF.Settings.License = Quest.LicenseType.Community;


            services.AddKeyedTransient<Quest.IDocument, FertlizerPlaningDocument>(nameof(FertlizerPlaningDocument));
            services.AddKeyedTransient<Quest.IDocument, CultivatedAreasDocument>(nameof(CultivatedAreasDocument));

            services.AddSingleton<IMenuItem, DocumentMenuItem>();
        }
    }

    public class DocumentMenuItem : IMenuItem
    {
        public string Name => "Dokumente";
        public string Icon => "description";
        public string Url => string.Empty;
        public IEnumerable<IMenuItem> Children =>
        [
            new FertilizerPlaningMenuItem(),
            new CultivatedAreasMenuItem()
        ];
    }

    public class FertilizerPlaningMenuItem : IMenuItem
    {
        public string Name => "Düngeplanung";
        public string Icon => "description";
        public string Url => $"/documents/pdfviewer/{nameof(FertlizerPlaningDocument)}";
        public IEnumerable<IMenuItem> Children => [];
    }

    public class CultivatedAreasMenuItem : IMenuItem
    {
        public string Name => "Anbauflächen";
        public string Icon => "description";
        public string Url => $"/documents/pdfviewer/{nameof(CultivatedAreasDocument)}";
        public IEnumerable<IMenuItem> Children => [];
    }
}
