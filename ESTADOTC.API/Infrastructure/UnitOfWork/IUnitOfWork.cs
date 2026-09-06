using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    ICardRepository Cards { get; }
    ITransactionRepository Transactions { get; }
}