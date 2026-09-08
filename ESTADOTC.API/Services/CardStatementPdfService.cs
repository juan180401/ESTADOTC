using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ESTADOTC.API.Services;

public sealed class PdfTransaction
{
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class CardStatementPdfService
{
    public byte[] Generate(
        string holderName,
        string cardNumber,
        decimal currentBalance,
        decimal creditLimit,
        decimal availableBalance,
        decimal bonifiableInterest,
        decimal minimumPayment,
        decimal totalToPay,
        decimal cashPaymentWithInterest,
        decimal currentMonthPurchases,
        decimal previousMonthPurchases,
        IEnumerable<PdfTransaction> transactions)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);

                // HEADER
                page.Header()
                    .Text("Estado de cuenta")
                    .FontSize(24)
                    .Bold();

                // CONTENT
                page.Content().Column(column =>
                {
                    column.Spacing(15);

                    // TARJETAHABIENTE
                    column.Item()
                        .Text($"Tarjetahabiente: {holderName}")
                        .FontSize(14)
                        .Bold();

                    column.Item()
                        .Text($"Tarjeta: {cardNumber}");

                    column.Item()
                        .LineHorizontal(1);

                    // SALDOS
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item()
                                .Text("Saldo actual")
                                .FontSize(10);

                            c.Item()
                                .Text(currentBalance.ToString("C"))
                                .FontSize(18)
                                .Bold();
                        });

                        row.RelativeItem().Column(c =>
                        {
                            c.Item()
                                .Text("Límite de crédito")
                                .FontSize(10);

                            c.Item()
                                .Text(creditLimit.ToString("C"))
                                .FontSize(18)
                                .Bold();
                        });

                        row.RelativeItem().Column(c =>
                        {
                            c.Item()
                                .Text("Crédito disponible")
                                .FontSize(10);

                            c.Item()
                                .Text(availableBalance.ToString("C"))
                                .FontSize(18)
                                .Bold();
                        });
                    });

                    // RESUMEN FINANCIERO
                    column.Item()
                        .PaddingTop(10)
                        .Text("Resumen financiero")
                        .FontSize(16)
                        .Bold();

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell()
                            .Text("Interés bonificable");

                        table.Cell()
                            .AlignRight()
                            .Text(bonifiableInterest.ToString("C"));

                        table.Cell()
                            .Text("Pago mínimo");

                        table.Cell()
                            .AlignRight()
                            .Text(minimumPayment.ToString("C"));

                        table.Cell()
                            .Text("Total a pagar");

                        table.Cell()
                            .AlignRight()
                            .Text(totalToPay.ToString("C"));

                        table.Cell()
                            .Text("Pago con interés");

                        table.Cell()
                            .AlignRight()
                            .Text(cashPaymentWithInterest.ToString("C"));
                    });

                    // COMPRAS
                    column.Item()
                        .PaddingTop(10)
                        .Text("Compras")
                        .FontSize(16)
                        .Bold();

                    column.Item()
                        .Text($"Mes actual: {currentMonthPurchases:C}");

                    column.Item()
                        .Text($"Mes anterior: {previousMonthPurchases:C}");

                    // MOVIMIENTOS
                    column.Item()
                        .PaddingTop(10)
                        .Text("Movimientos del mes")
                        .FontSize(16)
                        .Bold();

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn(2);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell()
                                .Text("Fecha")
                                .Bold();

                            header.Cell()
                                .Text("Descripción")
                                .Bold();

                            header.Cell()
                                .Text("Tipo")
                                .Bold();

                            header.Cell()
                                .AlignRight()
                                .Text("Monto")
                                .Bold();
                        });

                        foreach (var transaction in transactions)
                        {
                            table.Cell()
                                .Text(
                                    transaction.TransactionDate
                                        .ToString("dd/MM/yyyy")
                                );

                            table.Cell()
                                .Text(transaction.Description);

                            table.Cell()
                                .Text(transaction.TransactionType);

                            table.Cell()
                                .AlignRight()
                                .Text(transaction.Amount.ToString("C"));
                        }
                    });
                });

                // FOOTER
                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Página ");
                        text.CurrentPageNumber();
                    });
            });
        });

        return document.GeneratePdf();
    }
}