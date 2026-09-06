namespace ESTADOTC.API.Domain.Entities

{
    public class Card
    {
        public int Id { get; set; }

        public string CardNumber { get; set; } = string.Empty;

        public string HolderName { get; set; } = string.Empty;

        public decimal CreditLimit { get; set; }

        public decimal InterestRate { get; set; }

        public decimal MinimumPaymentRate { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal CurrentBalance { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}
