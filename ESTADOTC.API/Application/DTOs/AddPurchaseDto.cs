namespace ESTADOTC.API.Application.DTOs;

public class AddPurchaseDto
{
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}