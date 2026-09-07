namespace ESTADOTC.MVC.Models

open System

type AddPurchaseViewModel =
    {
        TransactionDate: DateTime
        Description: string
        Amount: decimal
    }