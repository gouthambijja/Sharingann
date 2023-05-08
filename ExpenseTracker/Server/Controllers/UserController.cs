using ExpenseTracker.Server.Models;
using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("RegisterUser")]
        public async Task<bool> RegisterUser(Models.User user)
        {
            //in this method you should only create a user record and not authenticate the user
            var UsernameExists = await IsUsernameExists(user.Username);
            if (UsernameExists == false)
            {
                user.Password = Utility.Encrypt(user.Password);
                await _userService.Add(new BLUser()
                {
                    Username = user.Username,
                    UserId = "",
                    Password = user.Password,
                });
            }
            else{
                return false;
            }
            return true;
        }
        [HttpPost("LoginUser")]
        public async Task<Models.User> LoginUser(string Username, string password)
        {
            password = Utility.Encrypt(password);
            Console.WriteLine(Username + " " + password);
            var UsernameExists = await IsUsernameExists(Username);
            if (UsernameExists == false) return null;
            BLUser _user = await _userService.Get(Username, password);
            if (_user == null) return null;
            Models.User user = new Models.User();
            user.Username = _user.Username;
            user.Password = _user.Password;
            user.UserId = _user.UserId;
            return user;
        }

        [HttpGet("IsUsernameExists")]
        public async Task<bool> IsUsernameExists(string Username)
        {
            return await _userService.GetById(Username);
        }

    }
}
