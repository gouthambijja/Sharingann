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
        Task<bool> Delete(string transactionId);
        Task<Transaction> Get(Transaction transaction);
        Task<List<Transaction>> GetAll(string UserId);
        public Task<List<Transaction>> AddMany(List<Transaction> transactions);
        public Task<List<Transaction>> GetFiltered(string UserId, string Name, string Category, string Description, DateTime StartDate, DateTime EndDate);
        public Task<List<Transaction>> GetFilteredByDateRange(string UserId, DateTime StartDate, DateTime EndDate);

    }
}
