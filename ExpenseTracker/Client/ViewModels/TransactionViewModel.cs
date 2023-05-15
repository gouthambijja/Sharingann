using ExpressTrackerLogicLayer.Models;
using System.Net.Http.Json;

namespace ExpenseTracker.Client.ViewModels
{
    public class TransactionViewModel : ITransactionViewModel
    {
        private readonly HttpClient _httpClient;
        public TransactionViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string? TransactionId { get ; set ; }
        public string? Name { get ; set ; }
        public string? Description { get ; set ; }
        public string? Category { get ; set ; }
        public DateTime Date { get ; set ; }
        public double Amount { get ; set ; }
        public string? UserId { get ; set ; }

        public async Task<BLTransaction> AddTransaction()
        {
            try
            {
                   
                Date = DateTime.Now;
                var response = await _httpClient.PostAsJsonAsync("/Transaction/AddTransaction", this);
                return await response.Content.ReadFromJsonAsync<BLTransaction>();

            }
            catch
            {
                return null;
            }
        }
    }
}
