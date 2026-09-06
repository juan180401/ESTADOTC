using System.Data;
using Dapper;
using ESTADOTC.API.Domain.Entities;
using ESTADOTC.API.Infrastructure.Data;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TransactionRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int cardId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
        {
            CardId = cardId
        };

        return await connection.QueryAsync<Transaction>(
            "dbo.sp_GetTransactions",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Transaction>> GetCurrentMonthTransactionsAsync(int cardId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
        {
            CardId = cardId
        };

        return await connection.QueryAsync<Transaction>(
            "dbo.sp_GetCurrentMonthTransactions",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task AddPurchaseAsync(
    int cardId,
    DateTime transactionDate,
    string description,
    decimal amount)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
        {
            CardId = cardId,
            TransactionDate = transactionDate,
            Description = description,
            Amount = amount
        };

        await connection.ExecuteAsync(
            "dbo.sp_AddPurchase",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task AddPaymentAsync(
        int cardId,
        DateTime transactionDate,
        decimal amount)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
        {
            CardId = cardId,
            TransactionDate = transactionDate,
            Amount = amount
        };

        await connection.ExecuteAsync(
            "dbo.sp_AddPayment",
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}