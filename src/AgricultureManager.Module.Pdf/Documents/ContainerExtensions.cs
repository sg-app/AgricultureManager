using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace AgricultureManager.Module.Pdf.Documents
{
    public static class ContainerExtensions
    {
        public static void CustomFooter(this IContainer container)
        {
            container
               .Height(15)
               .DefaultTextStyle(x => x.FontSize(10))
               .AlignMiddle()
               .Table(t =>
               {
                   t.ColumnsDefinition(c =>
                   {
                       c.RelativeColumn();
                       c.RelativeColumn();
                       c.RelativeColumn();
                   });
                   t.Cell().Column(1).Text(DateTime.Now.ToString());
                   t.Cell().Column(2).AlignCenter().Text(t =>
                   {
                       t.CurrentPageNumber();
                       t.Span(" / ");
                       t.TotalPages();
                   });
               });
        }

        public static TextSpanDescriptor CustomHeader(this IContainer container, string text)
        {
            return container
                .Height(30)
                .DefaultTextStyle(x => x.FontSize(18).Bold())
                .AlignCenter()
                .AlignMiddle()
                .Text(text);
        }
    }
}

