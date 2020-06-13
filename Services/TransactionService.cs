using System;
using System.Threading.Tasks;
using Greentube.Wallet.Model;
using Greentube.Wallet.Repositories;

namespace Greentube.Wallet.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPlayerRepository _playerRepository;

        public TransactionService(ITransactionRepository transactionRepository, IPlayerRepository playerRepository)
        {
            _transactionRepository = transactionRepository;
            _playerRepository = playerRepository;
        }

        public async Task<bool> Add(
            Guid transactionId,
            Guid playerId,
            TransactionType transactionType,
            decimal amount,
            decimal balance)
        {
            var existingTransaction = await _transactionRepository.GetTransaction(transactionId);
            if (existingTransaction != null)
            {
                return existingTransaction.Accepted;
            }

            var newBalance = CalculateNewBalance(transactionType, amount, balance);
            
            var transaction = await _transactionRepository.CreateTransaction(
                transactionId,
                playerId,
                transactionType,
                amount,
                Accepted(newBalance));

            if (transaction == null || !transaction.Accepted)
            {
                return false;
            }

            return await _playerRepository.ChangeBalance(playerId, newBalance);
        }

        private bool Accepted(in decimal newBalance) => newBalance >= 0;

        private static decimal CalculateNewBalance(TransactionType transactionType, decimal amount, decimal oldBalance)
        {
            decimal newBalance;
            switch (transactionType)
            {
                case TransactionType.Deposit:
                case TransactionType.Win:
                    newBalance = oldBalance + amount;
                    break;
                case TransactionType.Stake:
                    newBalance = oldBalance - amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
            }

            return newBalance;
        }
    }
}