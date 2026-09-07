namespace ESTADOTC.MVC.Models

open System

type TransactionViewModel =
    {
        Id: int
        CardId: int
        TransactionDate: DateTime
        Description: string
        Amount: decimal
        TransactionType: string
    }