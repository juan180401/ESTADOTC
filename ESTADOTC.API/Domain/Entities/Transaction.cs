namespace ESTADOTC.API.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public DateTime TransactionDate { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; } = string.Empty;
    }
}
