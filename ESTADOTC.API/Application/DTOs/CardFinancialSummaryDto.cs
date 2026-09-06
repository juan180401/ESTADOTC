namespace ESTADOTC.API.Application.DTOs;

public class CardFinancialSummaryDto
{
    public decimal CurrentBalance { get; set; }
    public decimal BonifiableInterest { get; set; }
    public decimal MinimumPayment { get; set; }
    public decimal TotalToPay { get; set; }
    public decimal CashPaymentWithInterest { get; set; }
}