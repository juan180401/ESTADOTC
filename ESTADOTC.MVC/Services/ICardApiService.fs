namespace ESTADOTC.MVC.Services

open System
open System.Threading.Tasks
open ESTADOTC.MVC.Models

type ICardApiService =
    abstract member GetCardStatementAsync:
        cardId: int -> Task<CardStatementViewModel>

    abstract member GetTransactionsAsync:
        cardId: int -> Task<TransactionViewModel list>

    abstract member GetCurrentMonthTransactionsAsync:
        cardId: int -> Task<TransactionViewModel list>

    abstract member GetMonthlyPurchaseTotalsAsync:
        cardId: int -> Task<MonthlyPurchaseTotalsViewModel>

    abstract member GetFinancialSummaryAsync:
        cardId: int -> Task<CardFinancialSummaryViewModel>

    abstract member AddPurchaseAsync:
        cardId: int *
        transactionDate: DateTime *
        description: string *
        amount: decimal -> Task

    abstract member AddPaymentAsync:
        cardId: int *
        transactionDate: DateTime *
        amount: decimal -> Task

    abstract member DownloadStatementPdfAsync:
        cardId: int -> Task<byte array>