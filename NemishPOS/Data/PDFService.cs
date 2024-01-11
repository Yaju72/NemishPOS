using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.Metadata;
using static NemishPOS.Pages.GenerateReport;

public class PdfGenerationService
{
    public void GenerateDailyReport(string filePath, DateTime day, int totalSales, List<CoffeeStatistics> topFiveCoffees, List<AddInStatistics> topFiveAddIns)
    {
        // Quest PDF generation logic for daily report
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);

                page.DefaultTextStyle(x => x.FontSize(13));

                page.Header()
                    .Text("Daily/Monthly Report")
                    .SemiBold().FontSize(25);

                page.Content().Column(x =>
                {
                    x.Item().Text($"Total Sales: {day}");
                    x.Spacing(20);
                    x.Item().Text("Top 5 Coffees");

                    x.Item().Table(table =>
                    {
                        table.ColumnsDefinition(column =>
                        {
                            column.RelativeColumn();
                            column.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Coffee Name");
                            header.Cell().Text("Total Quantity Sold");
                        });

                        foreach (var item in topFiveCoffees)
                        {
                            table.Cell().Text(item.CoffeeName);
                            table.Cell().Text(item.TotalQuantitySold);
                        }
                    });

                    x.Item().Text("Top 5 AddIns");

                    x.Item().Table(table =>
                    {
                        table.ColumnsDefinition(column =>
                        {
                            column.RelativeColumn();
                            column.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("AddIn Name");
                            header.Cell().Text("Total Quantity Sold");
                        });

                        foreach (var item in topFiveAddIns)
                        {
                            table.Cell().Text(item.AddInName);
                            table.Cell().Text(item.TotalQuantitySold);
                        }
                    });
                });

            });
        })
        .GeneratePdf(filePath);
    }
}
