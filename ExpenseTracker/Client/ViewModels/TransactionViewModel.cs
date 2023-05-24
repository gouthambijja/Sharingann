using ExpressTrackerDBAccessLayer.Models;
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
                   
                var response = await _httpClient.PostAsJsonAsync("/Transaction/AddTransaction", this);
                this.Amount = 0;
                this.Category = "Investment";
                this.Description = "";
                this.Name = "";
                return await response.Content.ReadFromJsonAsync<BLTransaction>();

            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLTransaction>> AddTransactions(List<BLTransaction> _transactions)
        {
            try
            {
                 foreach(var _transaction in _transactions)
                {
                    _transaction.Date = DateTime.Now;
                }  
                var response = await _httpClient.PostAsJsonAsync("/Transaction/AddTransactions", _transactions);
                return await response.Content.ReadFromJsonAsync<List<BLTransaction>>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<BLTransaction> EditTransaction()
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/Transaction/EditTransaction", this);
                return await response.Content.ReadFromJsonAsync<BLTransaction>();
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteTransaction(string transactionId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/Transaction/DeleteTransaction/{transactionId}");
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                return false;
            }
        }
    }
}
