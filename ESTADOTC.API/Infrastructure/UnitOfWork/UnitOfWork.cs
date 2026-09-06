using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public ICardRepository Cards { get; }
    public ITransactionRepository Transactions { get; }

    public UnitOfWork(
        ICardRepository cardRepository,
        ITransactionRepository transactionRepository)
    {
        Cards = cardRepository;
        Transactions = transactionRepository;
    }
}
