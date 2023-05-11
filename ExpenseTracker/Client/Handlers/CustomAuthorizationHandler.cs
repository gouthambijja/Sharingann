using Blazored.LocalStorage;

namespace BlazorApp2.Client.handlers
{
    public class CustomAuthorizationHandler:DelegatingHandler
    {
        public ILocalStorageService LocalStorageService { get; set; }
        public CustomAuthorizationHandler(ILocalStorageService localStorageService)
        {
            this.LocalStorageService = localStorageService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwtToken = await LocalStorageService.GetItemAsync<string>("jwt_token");

            if (jwtToken != null)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
            }
            return await base.SendAsync(request, cancellationToken); 
        }
    }
}
