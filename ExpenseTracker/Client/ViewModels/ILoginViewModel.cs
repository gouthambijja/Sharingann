namespace ExpenseTracker.Client.ViewModels
{
    public interface ILoginViewModel
    {
       public string UserName { get; set; }
       public string Password { get; set; }
        public Task Login();
        public Task<AuthenticationResponse> GetAuthenticateJwt();
        
    }
}
