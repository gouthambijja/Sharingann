using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client.ViewModels
{
    public interface IHomeViewModel
    {
        public static string UserId { get; set; }
        Task<List<BLTransaction>> GetTransactions(string UserId);
        public Task<BLUser> GetUserByJWT(string token);
    }
}
