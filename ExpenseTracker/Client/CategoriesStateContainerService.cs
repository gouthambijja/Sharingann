using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client
{
    public class CategoiresStateContainerService
    {
        /// <summary>
        /// The State property with initial value
        /// </summary>
        public List<BLCategory> Categories = new List<BLCategory>();
        /// <summary>
        /// The event that will be raised for state changed
        /// </summary>
        public event Action OnStateChange;

        /// <summary>
        /// The method that will be accessed by the sender component 
        /// to update the state
        /// </summary>


        public void SetCategory(List<BLCategory> _Categories)
        {
            if (_Categories != null)
                Categories = _Categories;
            NotifyStateChanged();
        }
        public void DeleteCategory(BLCategory _category)
        {
            Categories.Remove(_category); NotifyStateChanged();
        }
        public void AddCategory(BLCategory category)
        {
            Categories.Insert(0, category);
            NotifyStateChanged();
        }

        /// <summary>
        /// The state change event notification
        /// </summary>
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
