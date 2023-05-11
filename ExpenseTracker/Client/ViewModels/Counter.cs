namespace ExpenseTracker.Client.ViewModels
{
    public class Counter : ICounter
    {
        private readonly HttpClient _httpClient;
        public Counter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async  Task<string> GetData()
        {
            try
            {
                return await _httpClient.GetStringAsync("/counter/sharingan");
            }
            catch
            {
                return null;
            }
            
        }
    }
}
