namespace ESTADOTC.MVC.Controllers

open System
open System.Threading.Tasks
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

    [<HttpGet>]
    member this.Purchase() =
        let model =
            {
                TransactionDate = DateTime.Today
                Description = ""
                Amount = 0M
            }

        this.View(model)

    [<HttpPost>]
    [<ValidateAntiForgeryToken>]
    member this.Purchase(
        transactionDate: DateTime,
        description: string,
        amount: decimal
    ) : Task<IActionResult> =
        task {
            if String.IsNullOrWhiteSpace(description) then
                this.ModelState.AddModelError(
                    "Description",
                    "La descripción es obligatoria."
                )

            if amount <= 0M then
                this.ModelState.AddModelError(
                    "Amount",
                    "El monto debe ser mayor que cero."
                )

            if not this.ModelState.IsValid then
                let model =
                    {
                        TransactionDate = transactionDate
                        Description = description
                        Amount = amount
                    }

                return this.View(model) :> IActionResult
            else
                do!
                    apiService.AddPurchaseAsync(
                        1,
                        transactionDate,
                        description,
                        amount
                    )

                return this.RedirectToAction("Index") :> IActionResult
        }

    [<HttpGet>]
    member this.Payment() =
        let model =
            {
                TransactionDate = DateTime.Today
                Amount = 0M
            }

        this.View(model)

    [<HttpPost>]
    [<ValidateAntiForgeryToken>]
    member this.Payment(
        transactionDate: DateTime,
        amount: decimal
    ) : Task<IActionResult> =
        task {
            if amount <= 0M then
                this.ModelState.AddModelError(
                    "Amount",
                    "El monto debe ser mayor que cero."
                )

            if not this.ModelState.IsValid then
                let model =
                    {
                        TransactionDate = transactionDate
                        Amount = amount
                    }

                return this.View(model) :> IActionResult
            else
                do!
                    apiService.AddPaymentAsync(
                        1,
                        transactionDate,
                        amount
                    )

                return this.RedirectToAction("Index") :> IActionResult
        }