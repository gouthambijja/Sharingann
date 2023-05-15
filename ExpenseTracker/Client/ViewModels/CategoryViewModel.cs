using ExpressTrackerLogicLayer.Models;
using System.Net.Http.Json;

namespace ExpenseTracker.Client.ViewModels
{
    
    public class CategoryViewModel : ICategoryViewModel
    {
        private readonly HttpClient _httpClient;
        public CategoryViewModel(HttpClient httpClient)
        {
              _httpClient = httpClient;
        }
        public string Name { get ; set ; }
        public string UserId { get ; set ; }
        public string CategoryId { get ; set ; }

        public async Task<BLCategory> AddCategory()
        {
            try
            {
                var category = await _httpClient.PostAsJsonAsync("/Category", this);
                return await category.Content.ReadFromJsonAsync<BLCategory>();
            }
            catch
            {
                return null;
            }
        }
        public async Task<BLCategory> DeleteCategory(BLCategory category)
        {
            try
            {
                var _category = await _httpClient.PostAsJsonAsync("/Category/Delete", category);
                return await _category.Content.ReadFromJsonAsync<BLCategory>();
            }
            catch
            {
                return null;
            }
        }
    }
}
