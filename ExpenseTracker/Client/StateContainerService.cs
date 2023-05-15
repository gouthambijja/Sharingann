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
        public List<BLCategory> Categories= new List<BLCategory>();
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
        public void SetCategory(List<BLCategory> _Categories)
        {
            if(_Categories!=null)
            Categories = _Categories;
            NotifyStateChanged();
        }
        public void DeleteCategory(BLCategory _category)
        {
            Categories.Remove(_category); NotifyStateChanged();
        }
        public void AddCategory(BLCategory category)
        {
            Categories.Add(category);
            NotifyStateChanged();
        }
        public void SetTransaction(List<BLTransaction> _transactions)
        {
            if(_transactions!=null)
            Transactions = _transactions;
            NotifyStateChanged();
        }

        /// <summary>
        /// The state change event notification
        /// </summary>
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
