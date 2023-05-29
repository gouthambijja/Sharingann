using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExpressTrackerLogicLayer.Models;
using ExpressTrackerDBAccessLayer.Contracts;

namespace ExpressTrackerLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }




        public async Task<BLCategory> Add(BLCategory category)
        {
            try
            {
                category.CategoryId = Guid.NewGuid().ToString();
                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;
                category.IsActive = true;
                category.IsPermanentDelete = false;
                var mapper = AutoMappers.InitializeCategoryAutoMapper();
                Category _category = mapper.Map<Category>(category);
                _category = await _categoryRepository.Add(_category);
                return category;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BLCategory> Delete(BLCategory category)
        {
            try
            {
                var mapper = AutoMappers.InitializeCategoryAutoMapper();
                Category _category = mapper.Map<Category>(category);
                _category = await _categoryRepository.Delete(_category);
                return category;
            }
            catch { return null; }
        }

        public async Task<List<BLCategory>> Get(string UserId)
        {
            try
            {
                var categories = await _categoryRepository.Get(UserId);
                List<BLCategory> _BLCategories = new List<BLCategory>();
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        _BLCategories.Add(new BLCategory()
                        {
                            CategoryId = category.CategoryId,
                            Name = category.Name,
                            UserId = category.UserId
                        });
                    }
                    return _BLCategories;
                }
                return null;

            }
            catch { return null; }
        }
    }
}
