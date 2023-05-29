using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client
{
    public class BinTransactionStateContainerService
    {
        /// <summary>
        /// The State property with initial value
        /// </summary>
        public BLUser User { get; set; }
        public List<BLTransaction> Transactions = new List<BLTransaction>();
        /// <summary>
        /// The event that will be raised for state changed
        /// </summary>
        public event Action OnStateChange;

        /// <summary>
        /// The method that will be accessed by the sender component 
        /// to update the state
        /// </summary>

        public void AddTrasaction(BLTransaction _transaction)
        {
            Transactions.Insert(0, _transaction);
            NotifyStateChanged();
        }
        public void AddTransactions(List<BLTransaction> _transactions)
        {
            Console.WriteLine(_transactions.Count());
            _transactions.AddRange(Transactions);
            Console.WriteLine(_transactions.Count());
            Transactions = _transactions;
            NotifyStateChanged();
        }
        public void DeleteTransaction(BLTransaction _transaction)
        {
            Transactions.Remove(_transaction);
            NotifyStateChanged();
        }
        public void SetTransaction(List<BLTransaction> _transactions)
        {
            if (_transactions != null)
                Transactions = _transactions;
            NotifyStateChanged();
        }

        /// <summary>
        /// The state change event notification
        /// </summary>
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
