using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Infrastructure.Repositories.Interfaces;

public interface ICardRepository
{
    Task<Card?> GetCardStatementAsync(int cardId);
}