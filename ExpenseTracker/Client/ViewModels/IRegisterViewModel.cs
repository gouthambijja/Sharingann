using ExpressTrackerLogicLayer.Models;

namespace ExpenseTracker.Client.ViewModels
{
    public interface IRegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }        
        public string ConfirmPassword { get;set; }
        public bool ComparePassword();
        public Task<bool> RegisterUser();
        public Task<bool> IsValidUsername(string Username);
    }
}
