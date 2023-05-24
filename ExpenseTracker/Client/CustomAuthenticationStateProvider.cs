using ExpenseTracker.Client.Pages;
using Blazored.LocalStorage;
using System.Security.Claims;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using ExpressTrackerLogicLayer.Models;

namespace BlazorApp2.Client
{
    public class CustomAuthenticationStateProvider:AuthenticationStateProvider
    {
        protected readonly HttpClient _httpClient;
        protected readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(HttpClient httpClient,ILocalStorageService LocalStorage)
        {
            _httpClient = httpClient;
            _localStorage = LocalStorage;

        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            BLUser user = await GetUserByJWTAsync();
            if(user!=null && user.Username!=null)
            {

                var claimUsername = new Claim(ClaimTypes.Name, user.Username);
                var claimIdentifier = new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());

                var claimIdentity = new ClaimsIdentity(new[] { claimUsername, claimIdentifier }, "serverAuth");
                
                var claimsPricipal = new ClaimsPrincipal(claimIdentity);
                return new AuthenticationState(claimsPricipal);

            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
        public async Task<BLUser> GetUserByJWTAsync()
        {
            var jwtToken = await _localStorage.GetItemAsStringAsync("jwt_token");
            if (jwtToken == null) return null;

            //preparing http request
            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, "user/getuserbyjwt");
            //requestMessage.Content = new StringContent(jwtToken);

            //requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //var response = await _httpClient.SendAsync(requestMessage);

            //var responseStatusCode = response.StatusCode;
            jwtToken = jwtToken.Substring(1, jwtToken.Length-2);

            Console.WriteLine("user fetched -----" + jwtToken);
            BLUser returnedUser;
            try
            {
                 returnedUser = await _httpClient.GetFromJsonAsync<BLUser>($"/user/getuserbyjwt?jwtToken={jwtToken}");
            }
            catch
            {
                return null;
            }
            //returning user if found
            if (returnedUser != null) return await Task.FromResult(returnedUser);
            else return null;
        }

    }
}
 