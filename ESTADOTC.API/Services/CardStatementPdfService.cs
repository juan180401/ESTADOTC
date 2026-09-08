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
    private const string Primary = "#A71930";
    private const string PrimaryDark = "#781020";
    private const string PrimarySoft = "#F8ECEF";

    private const string TextPrimary = "#1F2937";
    private const string TextSecondary = "#6B7280";

    private const string Border = "#E5E7EB";
    private const string Background = "#F7F8FA";

    private const string Success = "#198754";
    private const string Danger = "#B42318";

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

        var transactionList = transactions.ToList();

        var formattedCardNumber =
            FormatCardNumber(cardNumber);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(35);

                page.DefaultTextStyle(
                    style => style
                        .FontSize(10)
                        .FontColor(TextPrimary)
                );


                // =====================================================
                // HEADER
                // =====================================================

                page.Header()
                    .PaddingBottom(20)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Column(column =>
                            {
                                column.Spacing(3);

                                column.Item()
                                    .Text("ESTADOTC")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Primary);

                                column.Item()
                                    .Text("Gestión de tarjeta")
                                    .FontSize(9)
                                    .FontColor(TextSecondary);
                            });

                        row.ConstantItem(190)
                            .AlignRight()
                            .Column(column =>
                            {
                                column.Spacing(3);

                                column.Item()
                                    .AlignRight()
                                    .Text("ESTADO DE CUENTA")
                                    .FontSize(14)
                                    .Bold()
                                    .FontColor(TextPrimary);

                                column.Item()
                                    .AlignRight()
                                    .Text(
                                        $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}"
                                    )
                                    .FontSize(8)
                                    .FontColor(TextSecondary);
                            });
                    });


                // =====================================================
                // CONTENT
                // =====================================================

                page.Content()
                    .Column(column =>
                    {
                        column.Spacing(16);


                        // =================================================
                        // TARJETA
                        // =================================================

                        column.Item()
                            .Background(PrimaryDark)
                            .Padding(22)
                            .Column(card =>
                            {
                                card.Spacing(18);

                                card.Item()
                                    .Row(row =>
                                    {
                                        row.RelativeItem()
                                            .Column(c =>
                                            {
                                                c.Item()
                                                    .Text("ESTADOTC")
                                                    .FontSize(13)
                                                    .Bold()
                                                    .FontColor("#FFFFFF");

                                                c.Item()
                                                    .Text("Crédito")
                                                    .FontSize(8)
                                                    .FontColor("#E7C8CE");
                                            });

                                        row.RelativeItem()
                                            .AlignRight()
                                            .Text("ACTIVA")
                                            .FontSize(8)
                                            .Bold()
                                            .FontColor("#FFFFFF");
                                    });

                                card.Item()
                                    .Text(formattedCardNumber)
                                    .FontSize(17)
                                    .Bold()
                                    .FontColor("#FFFFFF");

                                card.Item()
                                    .Row(row =>
                                    {
                                        row.RelativeItem()
                                            .Column(c =>
                                            {
                                                c.Item()
                                                    .Text("TARJETAHABIENTE")
                                                    .FontSize(7)
                                                    .FontColor("#E7C8CE");

                                                c.Item()
                                                    .PaddingTop(3)
                                                    .Text(holderName.ToUpperInvariant())
                                                    .FontSize(10)
                                                    .Bold()
                                                    .FontColor("#FFFFFF");
                                            });

                                        row.RelativeItem()
                                            .AlignRight()
                                            .Column(c =>
                                            {
                                                c.Item()
                                                    .AlignRight()
                                                    .Text("LÍMITE DE CRÉDITO")
                                                    .FontSize(7)
                                                    .FontColor("#E7C8CE");

                                                c.Item()
                                                    .AlignRight()
                                                    .PaddingTop(3)
                                                    .Text(creditLimit.ToString("C"))
                                                    .FontSize(10)
                                                    .Bold()
                                                    .FontColor("#FFFFFF");
                                            });
                                    });
                            });


                        // =================================================
                        // SALDOS PRINCIPALES
                        // =================================================

                        column.Item()
                            .Row(row =>
                            {
                                row.Spacing(10);

                                row.RelativeItem()
                                    .Border(1)
                                    .BorderColor(Border)
                                    .Background("#FFFFFF")
                                    .Padding(14)
                                    .Column(c =>
                                    {
                                        c.Spacing(5);

                                        c.Item()
                                            .Text("Saldo actual")
                                            .FontSize(8)
                                            .FontColor(TextSecondary);

                                        c.Item()
                                            .Text(currentBalance.ToString("C"))
                                            .FontSize(16)
                                            .Bold()
                                            .FontColor(TextPrimary);
                                    });

                                row.RelativeItem()
                                    .Border(1)
                                    .BorderColor(Border)
                                    .Background("#FFFFFF")
                                    .Padding(14)
                                    .Column(c =>
                                    {
                                        c.Spacing(5);

                                        c.Item()
                                            .Text("Límite de crédito")
                                            .FontSize(8)
                                            .FontColor(TextSecondary);

                                        c.Item()
                                            .Text(creditLimit.ToString("C"))
                                            .FontSize(16)
                                            .Bold()
                                            .FontColor(TextPrimary);
                                    });

                                row.RelativeItem()
                                    .Border(1)
                                    .BorderColor(Primary)
                                    .Background(PrimarySoft)
                                    .Padding(14)
                                    .Column(c =>
                                    {
                                        c.Spacing(5);

                                        c.Item()
                                            .Text("Crédito disponible")
                                            .FontSize(8)
                                            .FontColor(TextSecondary);

                                        c.Item()
                                            .Text(availableBalance.ToString("C"))
                                            .FontSize(16)
                                            .Bold()
                                            .FontColor(Primary);
                                    });
                            });


                        // =================================================
                        // RESUMEN FINANCIERO
                        // =================================================

                        column.Item()
                            .PaddingTop(4)
                            .Column(section =>
                            {
                                section.Spacing(3);

                                section.Item()
                                    .Text("INFORMACIÓN DE PAGO")
                                    .FontSize(7)
                                    .Bold()
                                    .FontColor(Primary);

                                section.Item()
                                    .Text("Resumen financiero")
                                    .FontSize(15)
                                    .Bold();
                            });

                        column.Item()
                            .Border(1)
                            .BorderColor(Border)
                            .Background("#FFFFFF")
                            .Padding(16)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });


                                // Interés bonificable

                                table.Cell()
                                    .PaddingVertical(7)
                                    .Text("Interés bonificable")
                                    .FontColor(TextSecondary);

                                table.Cell()
                                    .PaddingVertical(7)
                                    .AlignRight()
                                    .Text(
                                        bonifiableInterest.ToString("C")
                                    )
                                    .Bold();


                                // Pago mínimo

                                table.Cell()
                                    .BorderTop(1)
                                    .BorderColor(Border)
                                    .PaddingVertical(7)
                                    .Text("Pago mínimo")
                                    .FontColor(TextSecondary);

                                table.Cell()
                                    .BorderTop(1)
                                    .BorderColor(Border)
                                    .PaddingVertical(7)
                                    .AlignRight()
                                    .Text(
                                        minimumPayment.ToString("C")
                                    )
                                    .Bold();


                                // Total a pagar

                                table.Cell()
                                    .BorderTop(1)
                                    .BorderColor(Border)
                                    .PaddingVertical(7)
                                    .Text("Total a pagar")
                                    .FontColor(TextSecondary);

                                table.Cell()
                                    .BorderTop(1)
                                    .BorderColor(Border)
                                    .PaddingVertical(7)
                                    .AlignRight()
                                    .Text(
                                        totalToPay.ToString("C")
                                    )
                                    .Bold();


                                // Pago con interés

                                table.Cell()
                                    .BorderTop(1)
                                    .BorderColor(Border)
                                    .PaddingVertical(8)
                                    .Text("Pago con interés")
                                    .Bold()
                                    .FontColor(TextPrimary);

                                table.Cell()
                                    .BorderTop(1)
                                    .BorderColor(Border)
                                    .PaddingVertical(8)
                                    .AlignRight()
                                    .Text(
                                        cashPaymentWithInterest.ToString("C")
                                    )
                                    .FontSize(12)
                                    .Bold()
                                    .FontColor(Primary);
                            });


                        // =================================================
                        // COMPRAS POR MES
                        // =================================================

                        column.Item()
                            .PaddingTop(4)
                            .Column(section =>
                            {
                                section.Spacing(3);

                                section.Item()
                                    .Text("CONSUMO")
                                    .FontSize(7)
                                    .Bold()
                                    .FontColor(Primary);

                                section.Item()
                                    .Text("Compras")
                                    .FontSize(15)
                                    .Bold();
                            });

                        column.Item()
                            .Row(row =>
                            {
                                row.Spacing(10);

                                row.RelativeItem()
                                    .Background(Background)
                                    .Padding(14)
                                    .Column(c =>
                                    {
                                        c.Spacing(4);

                                        c.Item()
                                            .Text("Mes actual")
                                            .FontSize(8)
                                            .FontColor(TextSecondary);

                                        c.Item()
                                            .Text(
                                                currentMonthPurchases.ToString("C")
                                            )
                                            .FontSize(14)
                                            .Bold();
                                    });

                                row.RelativeItem()
                                    .Background(Background)
                                    .Padding(14)
                                    .Column(c =>
                                    {
                                        c.Spacing(4);

                                        c.Item()
                                            .Text("Mes anterior")
                                            .FontSize(8)
                                            .FontColor(TextSecondary);

                                        c.Item()
                                            .Text(
                                                previousMonthPurchases.ToString("C")
                                            )
                                            .FontSize(14)
                                            .Bold();
                                    });
                            });


                        // =================================================
                        // MOVIMIENTOS
                        // =================================================

                        column.Item()
                            .PaddingTop(4)
                            .Column(section =>
                            {
                                section.Spacing(3);

                                section.Item()
                                    .Text("ACTIVIDAD RECIENTE")
                                    .FontSize(7)
                                    .Bold()
                                    .FontColor(Primary);

                                section.Item()
                                    .Text("Movimientos del mes")
                                    .FontSize(15)
                                    .Bold();
                            });


                        if (transactionList.Count == 0)
                        {
                            column.Item()
                                .Border(1)
                                .BorderColor(Border)
                                .Background(Background)
                                .Padding(18)
                                .AlignCenter()
                                .Text(
                                    "No existen movimientos durante el mes actual."
                                )
                                .FontColor(TextSecondary);
                        }
                        else
                        {
                            column.Item()
                                .Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(75);
                                        columns.RelativeColumn(2.2f);
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });


                                    // =========================================
                                    // HEADER TABLA
                                    // =========================================

                                    table.Header(header =>
                                    {
                                        header.Cell()
                                            .Background(PrimaryDark)
                                            .Padding(8)
                                            .Text("Fecha")
                                            .FontSize(8)
                                            .Bold()
                                            .FontColor("#FFFFFF");

                                        header.Cell()
                                            .Background(PrimaryDark)
                                            .Padding(8)
                                            .Text("Descripción")
                                            .FontSize(8)
                                            .Bold()
                                            .FontColor("#FFFFFF");

                                        header.Cell()
                                            .Background(PrimaryDark)
                                            .Padding(8)
                                            .Text("Tipo")
                                            .FontSize(8)
                                            .Bold()
                                            .FontColor("#FFFFFF");

                                        header.Cell()
                                            .Background(PrimaryDark)
                                            .Padding(8)
                                            .AlignRight()
                                            .Text("Monto")
                                            .FontSize(8)
                                            .Bold()
                                            .FontColor("#FFFFFF");
                                    });


                                    // =========================================
                                    // ROWS
                                    // =========================================

                                    foreach (var transaction in transactionList)
                                    {
                                        var isPurchase =
                                            transaction.TransactionType
                                                .Equals(
                                                    "PURCHASE",
                                                    StringComparison.OrdinalIgnoreCase
                                                );

                                        var transactionLabel =
                                            isPurchase
                                                ? "Compra"
                                                : "Pago";

                                        var amountColor =
                                            isPurchase
                                                ? Danger
                                                : Success;

                                        var amountPrefix =
                                            isPurchase
                                                ? "- "
                                                : "+ ";


                                        table.Cell()
                                            .BorderBottom(1)
                                            .BorderColor(Border)
                                            .PaddingVertical(9)
                                            .PaddingHorizontal(8)
                                            .Text(
                                                transaction.TransactionDate
                                                    .ToString("dd/MM/yyyy")
                                            )
                                            .FontSize(8);

                                        table.Cell()
                                            .BorderBottom(1)
                                            .BorderColor(Border)
                                            .PaddingVertical(9)
                                            .PaddingHorizontal(8)
                                            .Text(
                                                transaction.Description
                                            )
                                            .FontSize(8);

                                        table.Cell()
                                            .BorderBottom(1)
                                            .BorderColor(Border)
                                            .PaddingVertical(9)
                                            .PaddingHorizontal(8)
                                            .Text(
                                                transactionLabel
                                            )
                                            .FontSize(8)
                                            .Bold()
                                            .FontColor(amountColor);

                                        table.Cell()
                                            .BorderBottom(1)
                                            .BorderColor(Border)
                                            .PaddingVertical(9)
                                            .PaddingHorizontal(8)
                                            .AlignRight()
                                            .Text(
                                                amountPrefix +
                                                transaction.Amount.ToString("C")
                                            )
                                            .FontSize(8)
                                            .Bold()
                                            .FontColor(amountColor);
                                    }
                                });
                        }
                    });


                // =====================================================
                // FOOTER
                // =====================================================

                page.Footer()
                    .PaddingTop(15)
                    .BorderTop(1)
                    .BorderColor(Border)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Text(
                                "ESTADOTC · Estado de cuenta generado electrónicamente"
                            )
                            .FontSize(7)
                            .FontColor(TextSecondary);

                        row.RelativeItem()
                            .AlignRight()
                            .Text(text =>
                            {
                                text.DefaultTextStyle(
                                    style =>
                                        style
                                            .FontSize(7)
                                            .FontColor(TextSecondary)
                                );

                                text.Span("Página ");
                                text.CurrentPageNumber();
                                text.Span(" de ");
                                text.TotalPages();
                            });
                    });
            });
        });

        return document.GeneratePdf();
    }


    private static string FormatCardNumber(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            return string.Empty;

        var digits =
            new string(
                cardNumber
                    .Where(char.IsDigit)
                    .ToArray()
            );

        if (digits.Length == 0)
            return cardNumber;

        return string.Join(
            " ",
            Enumerable
                .Range(0, (digits.Length + 3) / 4)
                .Select(index =>
                {
                    var start = index * 4;

                    var length =
                        Math.Min(
                            4,
                            digits.Length - start
                        );

                    return digits.Substring(
                        start,
                        length
                    );
                })
        );
    }
}