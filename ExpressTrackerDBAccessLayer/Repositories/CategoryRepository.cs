using ExpressTrackerDBAccessLayer.Contracts;
using ExpressTrackerDBAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerDBAccessLayer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ExpenseTrackerDBContext _CategoryDBContext;
        public CategoryRepository(ExpenseTrackerDBContext categoryDBContext)
        {
            _CategoryDBContext = categoryDBContext;
        }
        public async Task<Category> Add(Category category)
        {
            try
            {
                _CategoryDBContext.Add(category);
                await _CategoryDBContext.SaveChangesAsync();
                return category;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Category> Delete(Category category)
        {
            try
            {
                Console.WriteLine(category.Name);
                var _Categories = await _CategoryDBContext.Categories.
                     Where(e => e.CategoryId == category.CategoryId).ToListAsync();
                if (_Categories.Count == 0) return null;
                var _category = _Categories[0];
                _category.IsActive = false;
                _category.UpdatedAt = DateTime.Now; 
                await _CategoryDBContext.SaveChangesAsync();
                return category;
            }
            catch
            {
                return null;
            }
        }

            public async Task<List<Category>> Get(string UserId)
            {
                try
                {
                    var List = await _CategoryDBContext.Categories.
                   Where(e => e.UserId == UserId && e.IsActive == true).ToListAsync();
                    if (List.Count == 0) return null;
                    else return List;
                }
                catch
                {
                    return null;
                }
            }
    }
}
