namespace ESTADOTC.MVC.Models

type CardFinancialSummaryViewModel =
    {
        CurrentBalance: decimal
        BonifiableInterest: decimal
        MinimumPayment: decimal
        TotalToPay: decimal
        CashPaymentWithInterest: decimal
    }