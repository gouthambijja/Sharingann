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
            return await _httpClient.GetFromJsonAsync<List<BLTransaction>>($"Transaction?UserId={UserId}");
        }
        
        public async Task<BLUser> GetUserByJWT(string token)
        {
            if (token == null) return null;
            token = token.Substring(0, token.Length);
            return await _httpClient.GetFromJsonAsync<BLUser>($"user/getuserbyjwt?jwtToken={token}");
        }
    }
}
