using ExpressTrackerDBAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerDBAccessLayer.Contracts
{
    internal interface ICategoryRepository
    {
        Task<Category> Add(Category category);
        Task<Category> Delete(Category category);
        Task<List<Category>> Get(string UserId);
    }
}
