using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greentube.Wallet.Model;

namespace Greentube.Wallet.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private List<Transaction> _transactions;

        public TransactionRepository()
        {
            _transactions = new List<Transaction>();
        }


        public Task<Transaction> CreateTransaction(Guid transactionId, Guid playerId, TransactionType transactionType, decimal amount)
        {
            if (_transactions.Any(trx => trx.Id == transactionId))
                return Task.FromResult((Transaction)null);

            var transaction = new Transaction
            {
                Id = transactionId,
                PlayerId = playerId,
                Type = transactionType,
                Amount = amount
            };

            _transactions.Add(transaction);
            return Task.FromResult(transaction);
        }
    }
}