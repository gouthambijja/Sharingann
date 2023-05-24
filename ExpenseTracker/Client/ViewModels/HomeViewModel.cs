using ExpressTrackerLogicLayer.Models;
using System.Net.Http.Json;
using System.Web.WebPages;

namespace ExpenseTracker.Client.ViewModels
{
    public class HomeViewModel : IHomeViewModel
    {

        private readonly HttpClient _httpClient;

        public string TransactionSearchType { get; set; }
        public string TransactionSearchString { get; set; }
        public string FilterCategory { get ; set ; }
        public string FilterName { get ; set ; }
        public string FilterDescription { get ; set ; }
        public DateTime FilterStartDate { get ; set ; }
        public DateTime FilterEndDate { get ; set ; }

        public HomeViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BLTransaction>> GetTransactions(string UserId)
        {
            try
            {

            return await _httpClient.GetFromJsonAsync<List<BLTransaction>>($"Transaction?UserId={UserId}");
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLTransaction> AddTransaction(BLTransaction transaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Transaction/AddTransaction", transaction);
                return await response.Content.ReadFromJsonAsync<BLTransaction>();

            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLCategory>> GetCategories(string UserId)
        {
            try
            {

            return await _httpClient.GetFromJsonAsync<List<BLCategory>>($"Category?UserId={UserId}");
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLTransaction>> GetFilteredTransactions(string UserId)
        {
            try
            {
                if (this.FilterCategory == "") this.FilterCategory = "none";
                if (this.FilterName == "") this.FilterName = "none";
                if (this.FilterDescription == "") this.FilterDescription = "none";
                var transactions =  await _httpClient.GetFromJsonAsync<List<BLTransaction>>($"Transaction/GetFiltered?UserId={UserId}&Name={this.FilterName}&Category={this.FilterCategory}&Description={this.FilterDescription}&StartDate={this.FilterStartDate}&EndDate={this.FilterEndDate}");
                this.FilterCategory = "";
                this.FilterDescription = "";
                this.FilterName = "";
                return transactions;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLTransaction>> GetFilteredTransactionsByDate(string UserId,DateTime StartDate,DateTime EndDate)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BLTransaction>>($"Transaction/GetFilteredByDate?UserId={UserId}&StartDate={StartDate}&EndDate={EndDate}");
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLUser> GetUserByJWT(string token)
        {
            if (token == null) return null;
            token = token.Substring(0, token.Length);
            Console.WriteLine(token  + " -- home" );
            try
            {
                return await _httpClient.GetFromJsonAsync<BLUser>($"/user/getuserbyjwt?jwtToken={token}");
            }
            catch
            {
                return null;
            }
        }
    }
}
