using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetTransactionsAsync(int cardId);

    Task<IEnumerable<Transaction>> GetCurrentMonthTransactionsAsync(int cardId);

    Task AddPurchaseAsync(
    int cardId,
    DateTime transactionDate,
    string description,
    decimal amount);

    Task AddPaymentAsync(
        int cardId,
        DateTime transactionDate,
        decimal amount);
}