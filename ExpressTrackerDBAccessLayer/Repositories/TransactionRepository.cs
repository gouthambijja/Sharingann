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

        public async Task<Transaction> Delete(Transaction transaction)
        {
            var _Transactions = await _TransactionDBContext.Transactions.
                    Where(e => e.TransactionId == transaction.TransactionId).ToListAsync();
            if (_Transactions.Count == 0) return null;
            var _Transaction = _Transactions[0];
            _TransactionDBContext.Transactions.Remove(_Transaction);
            await _TransactionDBContext.SaveChangesAsync();
            return transaction;
            
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
