using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client.ViewModels
{
    public interface ICategoryViewModel
    {
        string Name { get; set; }
        string UserId { get; set; }
        string CategoryId { get; set; }
        public Task<BLCategory> AddCategory();
        public Task<BLCategory> DeleteCategory(BLCategory category);
    }
}
