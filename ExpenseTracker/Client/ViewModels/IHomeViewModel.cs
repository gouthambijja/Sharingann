using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client.ViewModels
{
    public interface IHomeViewModel
    {
        public DateTime FilterStartDate { get; set; }
        public DateTime FilterEndDate { get; set; }
        public string FilterCategory { get; set; }
        public string FilterName { get; set; }
        public string FilterDescription { get; set; }
        public  string TransactionSearchString { get; set; }
        public static string UserId { get; set; }
        Task<List<BLTransaction>> GetTransactions(string UserId);
        Task<List<BLTransaction>> GetBinTransactions(string UserId);
        Task<BLTransaction> RestoreTransaction(BLTransaction transaction);
        public Task<BLTransaction> AddTransaction(BLTransaction transaction);
        Task<List<BLCategory>> GetCategories(string UserId);
        public Task<BLUser> GetUserByJWT(string token);
        Task<List<BLTransaction>> GetFilteredTransactions(string UserId);
        Task<List<BLTransaction>> GetFilteredTransactionsByDate(string UserId, DateTime StartDate, DateTime EndDate);

    }
}
