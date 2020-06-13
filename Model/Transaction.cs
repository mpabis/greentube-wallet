using System;

namespace Greentube.Wallet.Model
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public bool Accepted { get; set; }
    }
}