namespace ESTADOTC.API.Application.DTOs;

public class AddPaymentDto
{
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
}