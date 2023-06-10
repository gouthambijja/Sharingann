using ExpenseTracker.Client.ViewModels;
using ExpenseTracker.Server.Models;
using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ExpressTrackerDBAccessLayer.Models;
using PasswordGenerator;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
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
        public async Task<bool> RegisterUser(BLUser user)
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
                    SecurityQuestion = user.SecurityQuestion,
                    SecurityAnswer = user.SecurityAnswer,
                });
            }
            else
            {
                return false;
            }
            return true;
        }
        [HttpPost("login")]
        public async Task<BLUser> LoginUser(BLUser LoginUser)
        {
            LoginUser.Password = Utility.Encrypt(LoginUser.Password);
            var UsernameExists = await IsUsernameExists(LoginUser.Username);
            if (UsernameExists == false) return null;
            BLUser _user = await _userService.Get(LoginUser.Username, LoginUser.Password);
            if (_user == null) return null;
            return _user;
        }

        [HttpGet("IsUsernameExists")]
        public async Task<bool> IsUsernameExists(string Username)
        {
            return await _userService.GetById(Username);
        }
        [HttpPut("UpdatePassword")]
        public async Task<bool> UpdatePassword(ChangePassword cp)
        {
            return await _userService.ChangePassword(cp.Username, Utility.Encrypt(cp.OldPassword), Utility.Encrypt(cp.NewPassword));
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
                Expires = DateTime.UtcNow.AddDays(1),
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
                    var user = await _userService.GetUserById(userId);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return null;
        }
        [HttpPost("getuserbyjwt")]
        public async Task<BLUser> GetUserByJwtPost(string jwtToken)
        {
            Console.Write("hello");

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
        [HttpGet("GoogleSignIn")]
        public async Task GoogleSignIn()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = "/user/google-login-callback" });

            
        }
        [HttpGet]
        [Route("google-login-callback")]
        public async Task<IActionResult> GoogleLoginCallBack()
        {
            var authenticationResult = await HttpContext
            .AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (authenticationResult.Succeeded)
            {
                var authClaims = authenticationResult.Principal.Claims.ToList();
                var identites = authenticationResult.Ticket.Principal.Identities.ToList();
                Console.WriteLine(HttpContext.User.ToString() + " " + HttpContext.User.Identity.Name);
                var claims = HttpContext.User.Claims.ToList();

                string email = authenticationResult.Principal.Claims.Where(e => e.Type == ClaimTypes.Email)
                .Select(_ => _.Value)
                .FirstOrDefault();
                string nameIdentifier = authenticationResult.Principal.Claims.Where(e => e.Type == ClaimTypes.NameIdentifier).Select(e => e.Value).FirstOrDefault();
                Console.WriteLine(email);
                int AtIdx = email.IndexOf("@");
                string Username = email.Substring(0, AtIdx);
                Console.WriteLine(Username);
                string UserPassword = Utility.Encrypt(nameIdentifier);

                //--------------------------------------------------------
                //If Username doesn't exist it will register automatically
                //---------------------------------------------------------
                var UsernameExists = await IsUsernameExists(Username);
                if (UsernameExists == false)
                {
                    await _userService.Add(new BLUser()
                    {
                        Username = Username,
                        UserId = "",
                        Password = UserPassword,
                    });
                }
                var response = await LoginUser(new BLUser()
                {
                    Username = Username,
                    Password = nameIdentifier,
                });
                var jwtToken = await  Authenticatejwt(response);
                return Redirect($"/login?Token={jwtToken}");
                //return Redirect($"/login?UserToken={jwtToken}");

            }
            return Redirect($"login");

        }
    }

}
