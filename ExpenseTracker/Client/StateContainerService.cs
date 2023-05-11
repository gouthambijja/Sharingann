using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client
{
    public class StateContainerService
    {
        /// <summary>
        /// The State property with initial value
        /// </summary>
        public BLUser User { get; set; }
        public List<BLTransaction> Transactions { get; set; }
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
            Transactions.Add(_transaction);
            NotifyStateChanged();
        }
        public void SetTrasaction(List<BLTransaction> _transactions)
        {
            Transactions = _transactions;
            NotifyStateChanged();
        }

        /// <summary>
        /// The state change event notification
        /// </summary>
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
    