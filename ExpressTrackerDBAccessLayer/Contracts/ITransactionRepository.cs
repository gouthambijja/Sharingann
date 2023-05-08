using ExpressTrackerDBAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpressTrackerDBAccessLayer.Contracts
{
    public interface ITransactionRepository
    {
        Task<Transaction> Add(Transaction transaction);
        Task<Transaction> Update(Transaction transaction);
        Task<Transaction> Delete(Transaction transaction);
        Task<Transaction> Get(Transaction transaction);
        Task<List<Transaction>> GetAll(string UserId);
    }
}
