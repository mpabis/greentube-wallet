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

        public async Task<bool> Add(Guid transactionId, Guid playerId, TransactionType transactionType, decimal amount)
        {
            var transaction = await _transactionRepository.CreateTransaction(transactionId, playerId, transactionType, amount);
            if (transaction == null)
                return false;

            var amountDelta = CalculateAmountDelta(transactionType, amount);
            return await _playerRepository.ChangeBalance(playerId, amountDelta);
        }

        private static decimal CalculateAmountDelta(TransactionType transactionType, decimal amount)
        {
            decimal amountDelta;
            switch (transactionType)
            {
                case TransactionType.Deposit:
                case TransactionType.Win:
                    amountDelta = amount;
                    break;
                case TransactionType.Stake:
                    amountDelta = -amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
            }

            return amountDelta;
        }
    }
}