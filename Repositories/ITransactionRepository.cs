using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;

namespace Greentube.Wallet.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransaction(
            Guid transactionId,
            Guid playerId,
            TransactionType transactionType,
            decimal amount);
    }
}