using ExpressTrackerLogicLayer.Models;
using System.Net.Http.Json;

namespace ExpenseTracker.Client.ViewModels
{
    public class HomeViewModel : IHomeViewModel
    {

        private readonly HttpClient _httpClient;
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
        
        public async Task<BLUser> GetUserByJWT(string token)
        {
            if (token == null) return null;
            token = token.Substring(0, token.Length);
            try
            {
                return await _httpClient.GetFromJsonAsync<BLUser>($"user/getuserbyjwt?jwtToken={token}");
            }
            catch
            {
                return null;
            }
        }
    }
}
