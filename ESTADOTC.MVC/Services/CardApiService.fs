namespace ESTADOTC.MVC.Services

open System
open System.Net.Http
open System.Net.Http.Json
open System.Text.Json
open System.Threading.Tasks
open ESTADOTC.MVC.Models

type CardApiService(httpClient: HttpClient) =

    let getErrorMessageAsync (response: HttpResponseMessage) =
        task {
            let! content =
                response.Content.ReadAsStringAsync()

            try
                use document =
                    JsonDocument.Parse(content)

                let mutable title =
                    Unchecked.defaultof<JsonElement>

                if document.RootElement.TryGetProperty("title", &title) then
                    return title.GetString()
                           |> Option.ofObj
                           |> Option.defaultValue "No fue posible realizar la operación."
                else
                    return "No fue posible realizar la operación."
            with
            | _ ->
                return "No fue posible realizar la operación."
        }

    member _.GetCardStatementAsync(cardId: int) =
        httpClient.GetFromJsonAsync<CardStatementViewModel>(
            $"api/cards/{cardId}/statement"
        )

    member _.GetTransactionsAsync(cardId: int) =
        task {
            let! result =
                httpClient.GetFromJsonAsync<TransactionViewModel list>(
                    $"api/cards/{cardId}/transactions"
                )

            return result
        }

    member _.GetCurrentMonthTransactionsAsync(cardId: int) =
        task {
            let! result =
                httpClient.GetFromJsonAsync<TransactionViewModel list>(
                    $"api/cards/{cardId}/transactions/current-month"
                )

            return result
        }

    member _.GetMonthlyPurchaseTotalsAsync(cardId: int) =
        httpClient.GetFromJsonAsync<MonthlyPurchaseTotalsViewModel>(
            $"api/cards/{cardId}/monthly-purchase-totals"
        )

    member _.GetFinancialSummaryAsync(cardId: int) =
        httpClient.GetFromJsonAsync<CardFinancialSummaryViewModel>(
            $"api/cards/{cardId}/financial-summary"
        )

    member _.AddPurchaseAsync(
        cardId: int,
        transactionDate: DateTime,
        description: string,
        amount: decimal
    ) =
        task {
            let request: AddPurchaseViewModel =
                {
                    TransactionDate = transactionDate
                    Description = description
                    Amount = amount
                }

            let! response =
                httpClient.PostAsJsonAsync(
                    $"api/cards/{cardId}/purchases",
                    request
                )

            if not response.IsSuccessStatusCode then
                let! message =
                    getErrorMessageAsync response

                raise (InvalidOperationException(message))
        }

    member _.AddPaymentAsync(
        cardId: int,
        transactionDate: DateTime,
        amount: decimal
    ) =
        task {
            let request: AddPaymentViewModel =
                {
                    TransactionDate = transactionDate
                    Amount = amount
                }

            let! response =
                httpClient.PostAsJsonAsync(
                    $"api/cards/{cardId}/payments",
                    request
                )

            if not response.IsSuccessStatusCode then
                let! message =
                    getErrorMessageAsync response

                raise (InvalidOperationException(message))
        }

    member _.DownloadStatementPdfAsync(cardId: int) =
        task {
            let! response =
                httpClient.GetAsync(
                    $"api/cards/{cardId}/statement/pdf"
                )

            response.EnsureSuccessStatusCode() |> ignore

            let! bytes =
                response.Content.ReadAsByteArrayAsync()

            return bytes
        }

    interface ICardApiService with

        member this.GetCardStatementAsync(cardId) =
            this.GetCardStatementAsync(cardId)

        member this.GetTransactionsAsync(cardId) =
            this.GetTransactionsAsync(cardId)

        member this.GetCurrentMonthTransactionsAsync(cardId) =
            this.GetCurrentMonthTransactionsAsync(cardId)

        member this.GetMonthlyPurchaseTotalsAsync(cardId) =
            this.GetMonthlyPurchaseTotalsAsync(cardId)

        member this.GetFinancialSummaryAsync(cardId) =
            this.GetFinancialSummaryAsync(cardId)

        member this.AddPurchaseAsync(
            cardId,
            transactionDate,
            description,
            amount
        ) =
            this.AddPurchaseAsync(
                cardId,
                transactionDate,
                description,
                amount
            )

        member this.AddPaymentAsync(
            cardId,
            transactionDate,
            amount
        ) =
            this.AddPaymentAsync(
                cardId,
                transactionDate,
                amount
            )

        member this.DownloadStatementPdfAsync(cardId) =
            this.DownloadStatementPdfAsync(cardId)