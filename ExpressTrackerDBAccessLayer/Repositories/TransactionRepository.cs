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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ExpenseTrackerDBContext _TransactionDBContext;
        public TransactionRepository(ExpenseTrackerDBContext userDbContext)
        {
            _TransactionDBContext = userDbContext;
        }

        public async Task<Transaction> Add(Transaction transaction)
        {
            try
            {
                _TransactionDBContext.Add(transaction);
                await _TransactionDBContext.SaveChangesAsync();
                return transaction;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Transaction>> AddMany(List<Transaction> transactions)
        {
            try
            {
                Console.WriteLine(transactions[0].UserId + " alksdjfklasjflkjsaklfjslakfjslkj;");
               await _TransactionDBContext.Transactions.AddRangeAsync(transactions);
                await _TransactionDBContext.SaveChangesAsync();

                return transactions;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Delete(string transactionId)
        {
            var _Transactions = await _TransactionDBContext.Transactions.
                    Where(e => e.TransactionId == transactionId).ToListAsync();
            if (_Transactions.Count == 0) return false;
            var _Transaction = _Transactions[0];
            _TransactionDBContext.Transactions.Remove(_Transaction);
            await _TransactionDBContext.SaveChangesAsync();
            return true;
            
        }

        public async Task<Transaction> Get(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Transaction>> GetAll(string UserId)
        {
            try
            {
                var _Transactions = await _TransactionDBContext.Transactions
                    .Where(e => e.UserId == UserId).OrderByDescending(e => e.Date).ToListAsync();
                Console.WriteLine(_Transactions.Count());
                return _Transactions;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Transaction>> GetFiltered(string UserId, string Name, string Category, string Description, DateTime StartDate, DateTime EndDate)
        {
            try
            {

                var _Transactions = await _TransactionDBContext.Transactions
                    .Where(e => e.UserId == UserId &&  ((Name != "none") ? e.Name == Name : true) && ((Category != "none") ? e.Category == Category : true) && ((Description != "none") ? e.Description == Description : true) && e.Date >= StartDate && e.Date <= EndDate).ToListAsync();
                return _Transactions;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Transaction>> GetFilteredByDateRange(string UserId,DateTime StartDate,DateTime EndDate)
        {
            Console.WriteLine(EndDate);
            try
            {
                var _Transactions = await _TransactionDBContext.Transactions
                    .Where(e => e.UserId == UserId && e.Date >= StartDate && e.Date <= EndDate).OrderByDescending(e => e.Date).ToListAsync();
                return _Transactions;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Transaction> Update(Transaction transaction)
        {
            var _Transactions = await _TransactionDBContext.Transactions.
                Where(e => e.TransactionId == transaction.TransactionId).ToListAsync();
            var _Transaction    = _Transactions[0];
            _Transaction.Name = transaction.Name;
            _Transaction.Description = transaction.Description;
            _Transaction.Amount = transaction.Amount;
            _Transaction.Category = transaction.Category;
            await _TransactionDBContext.SaveChangesAsync();
            return _Transaction;
        }
    }
}
