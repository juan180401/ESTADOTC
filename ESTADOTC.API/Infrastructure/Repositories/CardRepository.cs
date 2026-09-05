using System.Data;
using Dapper;
using ESTADOTC.API.Domain.Entities;
using ESTADOTC.API.Infrastructure.Data;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;
using System.Data;

namespace ESTADOTC.API.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CardRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Card?> GetCardStatementAsync(int cardId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
        {
            CardId = cardId
        };

        return await connection.QueryFirstOrDefaultAsync<Card>(
            "dbo.sp_GetCardStatement",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<CardFinancialSummary?> GetFinancialSummaryAsync(int cardId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
        {
            CardId = cardId
        };

        return await connection.QueryFirstOrDefaultAsync<CardFinancialSummary>(
            "dbo.sp_GetCardFinancialSummary",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<MonthlyPurchaseTotals?> GetMonthlyPurchaseTotalsAsync(int cardId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new
        {
            CardId = cardId
        };

        return await connection.QueryFirstOrDefaultAsync<MonthlyPurchaseTotals>(
            "dbo.sp_GetMonthlyPurchaseTotals",
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}