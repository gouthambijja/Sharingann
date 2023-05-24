using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client
{
    public class StateContainerService
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
        public void SetUser(BLUser _user)
        {
            User = _user;
            NotifyStateChanged();
        }
        public void AddTrasaction(BLTransaction _transaction)
        {
            Transactions.Insert(0, _transaction);
            NotifyStateChanged();
        }
        public void AddTrasactions(List<BLTransaction> _transactions)
        {
            Console.WriteLine(_transactions.Count());
            _transactions.AddRange(Transactions);
            Console.WriteLine(_transactions.Count());
            Transactions = _transactions;
            NotifyStateChanged();
        }
        public void UpdateTransaction(BLTransaction _transaction)
        {
            Console.WriteLine(Transactions.Count());
            var transaction = Transactions.Where(e => e.TransactionId == _transaction.TransactionId).First();
            Transactions.Remove(transaction);
            Console.WriteLine(Transactions.Count());
            Transactions.Insert(0, _transaction);
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
        public void SortByDateAscending()
        {
            Transactions.Sort((t1, t2) => t1.Date.CompareTo(t2.Date));
            NotifyStateChanged();
        }
        public void SortByDateDescending()
        {
            Transactions.Sort((t1, t2) => t2.Date.CompareTo(t1.Date));
            NotifyStateChanged();
        }
        public void SortByNameAscending()
        {
            Transactions.Sort((t1, t2) => t1.Name.CompareTo(t2.Name));
            NotifyStateChanged();
        }
        public void SortByNameDescending()
        {
            Transactions.Sort((t1, t2) => t2.Name.CompareTo(t1.Name));
            NotifyStateChanged();
        }
        public void SortByCategoryAscending()
        {
            Transactions.Sort((t1, t2) => t1.Category.CompareTo(t2.Category));
            NotifyStateChanged();
        }
        public void SortByCategoryDescending()
        {
            Transactions.Sort((t1, t2) => t2.Category.CompareTo(t1.Category));
            NotifyStateChanged();
        }
        public void SortByDescriptionAscending()
        {
            Transactions.Sort((t1, t2) => t1.Description.CompareTo(t2.Description));
            NotifyStateChanged();
        }
        public void SortByDescriptionDescending()
        {
            Transactions.Sort((t1, t2) => t2.Description.CompareTo(t1.Description));
            NotifyStateChanged();
        }

        /// <summary>
        /// The state change event notification
        /// </summary>
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
