using ExpenseTracker.Client.ViewModels;
using ExpenseTracker.Server.Models;
using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserService _userService;
        private static IConfiguration _config;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public static void configure(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("Register")]
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
            else
            {
                return false;
            }
            return true;
        }
        [HttpPost("login")]
        public async Task<Models.User> LoginUser(User LoginUser)
        {
            LoginUser.Password = Utility.Encrypt(LoginUser.Password);
            var UsernameExists = await IsUsernameExists(LoginUser.Username);
            if (UsernameExists == false) return null;
            BLUser _user = await _userService.Get(LoginUser.Username, LoginUser.Password);
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


        //jwt methods//---------------------------------
        private string GenerateJwtToken(BLUser user)
        {
            string secretKey = _config["Authentication:JWTSettings:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claimUsername = new Claim(ClaimTypes.Name, user.Username);
            var claimIdentifier = new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());

            var claimIdentity = new ClaimsIdentity(new[] { claimUsername, claimIdentifier }, "serverAuth");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //
                Subject = claimIdentity,
                //expire in next x days
                Expires = DateTime.UtcNow.AddMinutes(10),
                //which 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        [HttpPost("authenticatejwt")]
        public async Task<string> Authenticatejwt(BLUser user)
        {
            string token = string.Empty;
            Console.WriteLine(user.Username + " " + user.Password);
            token = GenerateJwtToken(user);
            Console.WriteLine(token);
            return token;
        }
        //public async Task<ActionResult<LoginCredintials>> GetUserByJwt([FromBody] string jwtToken)
        [HttpGet("getuserbyjwt")]
        public async Task<BLUser> GetUserByJwt(string jwtToken)
        {
            try
            {
                //getting the secret key
                string secretKey = _config["Authentication:JWTSettings:SecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);

                //preparing the validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                
                     Console.WriteLine(jwtToken);
                //validationg token
                var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
                    return await _userService.GetUserById(userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return null;
        }
    }
}
