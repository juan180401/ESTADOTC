namespace ESTADOTC.MVC.Models

open System

type AddPaymentViewModel =
    {
        TransactionDate: DateTime
        Amount: decimal
    }