namespace ESTADOTC.MVC.Controllers

open Microsoft.AspNetCore.Mvc
open ESTADOTC.MVC.Services
open ESTADOTC.MVC.Models

type CardController(apiService: ICardApiService) =
    inherit Controller()

    member this.Index() =
        task {
            let cardId = 1

            let! statement =
                apiService.GetCardStatementAsync(cardId)

            let! financialSummary =
                apiService.GetFinancialSummaryAsync(cardId)

            let! monthlyTotals =
                apiService.GetMonthlyPurchaseTotalsAsync(cardId)

            let! currentMonthTransactions =
                apiService.GetCurrentMonthTransactionsAsync(cardId)

            let model =
                {
                    Statement = statement
                    FinancialSummary = financialSummary
                    MonthlyTotals = monthlyTotals
                    CurrentMonthTransactions = currentMonthTransactions
                }

            return this.View(model)
        }