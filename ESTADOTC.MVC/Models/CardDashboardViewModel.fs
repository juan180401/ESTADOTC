namespace ESTADOTC.MVC.Models

type CardDashboardViewModel =
    {
        Statement: CardStatementViewModel
        FinancialSummary: CardFinancialSummaryViewModel
        MonthlyTotals: MonthlyPurchaseTotalsViewModel
        CurrentMonthTransactions: TransactionViewModel list
    }