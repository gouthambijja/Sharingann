using ExpressTrackerLogicLayer.Models;
using System.Net.Http.Json;

namespace ExpenseTracker.Client.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        private readonly HttpClient _httpClient;
        public RegisterViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        private string HashSalt = "$2a$10$xnQs0sKkOlMyMhgeSiCuuO";



        public bool ComparePassword()
        {
            return Password == ConfirmPassword;
        }

        public async Task<bool> IsValidUsername(string Username)
        {
            var response = await _httpClient.GetFromJsonAsync<bool>($"/User/IsUsernameExists?Username={Username}");
            return response;
        }

        public async Task<bool> RegisterUser()
        {
            this.Password = BCrypt.Net.BCrypt.HashPassword(this.Password,HashSalt);
            var response = await _httpClient.PostAsJsonAsync("/User/Register", this);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
