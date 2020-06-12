using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;

namespace Greentube.Wallet.Services
{
    public interface ITransactionService
    {
        Task<bool> Add(Guid transactionId, Guid playerId, TransactionType transactionType, decimal amount);
    }
}