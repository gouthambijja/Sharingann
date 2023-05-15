using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoryController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<List<BLCategory>> Get(string UserId)
        {
            return await _categoryService.Get(UserId);
        }
        [HttpPost]
        public async Task<BLCategory> Add(BLCategory category)
        {
            return await _categoryService.Add(category);
        }
        [HttpPost("Delete")]
        public async Task<BLCategory> Delete(BLCategory category)
        {
            return await _categoryService.Delete(category);
        }
    }
}
