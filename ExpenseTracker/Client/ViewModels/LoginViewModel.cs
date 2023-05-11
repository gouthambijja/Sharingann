using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ExpenseTracker.Client.ViewModels
{
    public class LoginViewModel:ILoginViewModel
    {
        [Required]
        public string? Username { get ; set; }
        [Required]
        public string? Password { get ; set; }
        public string? UserId { get ; set; }
        private HttpResponseMessage response = new HttpResponseMessage();
        private readonly HttpClient _httpClient;
        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<BLUser> Login()
        {
            
            var _user = await _httpClient.PostAsJsonAsync("/user/login", this);
            var user = await _user.Content.ReadFromJsonAsync<BLUser>();
            return user;
            //string response = await GetAuthenticateJwt(user);
            //return response;
        }

        public async Task<string> GetAuthenticateJwt(BLUser user)
        {
            var response = await _httpClient.PostAsJsonAsync($"/user/authenticatejwt", user);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<BLUser> GetUserByJWT(string token)
        {
            if (token == null) return null;
            token = token.Substring(0, token.Length);
            return await _httpClient.GetFromJsonAsync<BLUser>($"user/getuserbyjwt?jwtToken={token}");
        }
    }

}
