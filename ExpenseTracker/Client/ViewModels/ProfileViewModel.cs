using ExpenseTracker.Client.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;
using System.Net.Http.Json;
using static ExpenseTracker.Client.ViewModels.ProfileViewModel;

namespace ExpenseTracker.Client.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        private string HashSalt = "$2a$10$xnQs0sKkOlMyMhgeSiCuuO";
        private readonly HttpClient _httpClient;
        private readonly StateContainerService stateContainerService;
        public ProfileViewModel(HttpClient httpClient, StateContainerService _stateContainerServices)
        {
            _httpClient = httpClient;
            stateContainerService = _stateContainerServices;
        }
        public async Task<bool> ChangePassword(string newPassword, string oldPassword)
        {
            try
            {
                oldPassword = BCrypt.Net.BCrypt.HashPassword(oldPassword, HashSalt);
                newPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, HashSalt);
                ChangePassword cp = new ChangePassword()
                {
                    Username = stateContainerService.User.Username,
                    OldPassword = oldPassword,
                    NewPassword = newPassword,
                };
        
        var response = await _httpClient.PutAsJsonAsync("/User/UpdatePassword",cp);
                return true;
            }
            catch
            {
    return false;
}
        }

        public async Task<bool> ConfirmPassword(string newPassword)
{
    throw new NotImplementedException();
}

    }
}
