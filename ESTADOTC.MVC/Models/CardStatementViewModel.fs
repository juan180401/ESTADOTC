namespace ESTADOTC.MVC.Models

type CardStatementViewModel =
    {
        Id: int
        CardNumber: string
        HolderName: string
        CreditLimit: decimal
        CurrentBalance: decimal
        AvailableBalance: decimal
        InterestRate: decimal
        MinimumPaymentRate: decimal
    }