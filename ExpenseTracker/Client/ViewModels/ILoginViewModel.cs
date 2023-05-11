using ExpressTrackerLogicLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Client.ViewModels
{
    public interface ILoginViewModel
    {
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserId { get; set; }
        public Task<BLUser> Login();
        public Task<string> GetAuthenticateJwt(BLUser user);
        public Task<BLUser> GetUserByJWT(string token);


    }
}
