using ExpressTrackerLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Contracts
{
    public interface ITransactionService
    {
        Task<BLTransaction> Add(BLTransaction transaction);
        Task<BLTransaction> Update(BLTransaction transaction);
        Task<bool> Delete(string id);
        Task<BLTransaction> Get(BLTransaction transaction);
        Task<List<BLTransaction>> GetAll(string UserId);
        Task<List<BLTransaction>> AddMany(List<BLTransaction> transactions);
        Task<List<BLTransaction>> GetFiltered(string UserId, string Name, string Category, string Description, DateTime StartDate, DateTime EndDate);
        public Task<List<BLTransaction>> GetFilteredByDateRange(string UserId, DateTime StartDate, DateTime EndDate);


    }
}
