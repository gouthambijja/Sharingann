
using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client.ViewModels
{
    public interface ITransactionViewModel
    {
        public string? TransactionId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string? UserId { get; set; }

        Task<BLTransaction> AddTransaction();
        Task<List<BLTransaction>> AddTransactions(List<BLTransaction> _transactions);
        Task<BLTransaction> EditTransaction();
        Task<bool> DeleteTransaction(string transactionId);
        Task<bool> DeleteTransactionPermanently(string transactionId);
        public Task<bool> DeleteMultiple(List<string> TransactionIds);

    }
}
