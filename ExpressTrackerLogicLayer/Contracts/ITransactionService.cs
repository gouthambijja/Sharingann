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
        Task<BLTransaction> Delete(BLTransaction transaction);
        Task<BLTransaction> Get(BLTransaction transaction);
        Task<List<BLTransaction>> GetAll(string UserId);
    }
}
