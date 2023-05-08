using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Contracts
{
    public interface ICategoryService
    {
        Task<BLCategory> Add(BLCategory category);
        Task<BLCategory> Delete(BLCategory category);
        Task<List<BLCategory>> Get(string UserId);
    }
}
