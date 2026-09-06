namespace ESTADOTC.MVC.Controllers

open Microsoft.AspNetCore.Mvc
open ESTADOTC.MVC.Services

type CardController(apiService: ICardApiService) =
    inherit Controller()

    member this.Index() =
        task {
            let! card = apiService.GetCardStatementAsync(1)
            return this.View(card)
        }