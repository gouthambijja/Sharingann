using ExpressTrackerDBAccessLayer.Contracts;
using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<BLTransaction> Add(BLTransaction transaction)
        {
            try
            {
                Console.WriteLine("tranasactions!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                transaction.TransactionId = Guid.NewGuid().ToString();
                transaction.CreatedAt = DateTime.Now;
                transaction.UpdatedAt = DateTime.Now;
                transaction.IsActive = true;
                transaction.IsPermanentDelete = false;
                var mapper = AutoMappers.InitializeTransactionAutoMapper();
                Transaction _transaction = mapper.Map<Transaction>(transaction);
                _transaction = await _transactionRepository.Add(_transaction);
                if (_transaction == null) return null;
                return transaction;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLTransaction>> AddMany(List<BLTransaction> transactions)
        {
            try
            {
                List<Transaction> _transactions = new List<Transaction>();
                var mapper = AutoMappers.InitializeTransactionAutoMapper();
                foreach(var transaction in transactions)
                {
                    transaction.TransactionId = Guid.NewGuid().ToString();
                    transaction.CreatedAt = DateTime.Now;
                    transaction.UpdatedAt = DateTime.Now;
                    transaction.IsActive = true;
                    transaction.IsPermanentDelete = false;
                    _transactions.Add( mapper.Map<Transaction>(transaction));
                }
                _transactions = await _transactionRepository.AddMany(_transactions);
                if (_transactions == null) return null;
                return transactions;
            }
            catch
            {
                return null;
            }
        }
        

        public async Task<bool> Delete(string id)
        {
            try
            {
                var _transaction = await _transactionRepository.Delete(id);
                if (_transaction == false) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletePermanently(string id)
        {
            try
            {
                Console.WriteLine("hey SErvice");
                var _transaction = await _transactionRepository.DeletePermanently(id);
                if (_transaction == false) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        

        public async Task<BLTransaction> Get(BLTransaction transaction)
        {
            throw new NotImplementedException();
        }

        

        public async Task<List<BLTransaction>> GetAll(string UserId)
        {
            try
            {
               var transactions =  await _transactionRepository.GetAll(UserId);
                if (transactions == null) return null;
               var _BLTransactions = new List<BLTransaction>();
                foreach (var transaction in transactions)
                {
                    _BLTransactions.Add(new BLTransaction()
                    {
                        Name = transaction.Name,
                        Amount = transaction.Amount,
                        Category = transaction.Category,
                        Description = transaction.Description,
                        Date = transaction.Date,
                        UserId = transaction.UserId,
                        TransactionId = transaction.TransactionId
                    });
                }
                return _BLTransactions;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLTransaction>> GetBinAll(string UserId)
        {
            try
            {
               var transactions =  await _transactionRepository.GetBinAll(UserId);
                if (transactions == null) return null;
               var _BLTransactions = new List<BLTransaction>();
                foreach (var transaction in transactions)
                {
                    _BLTransactions.Add(new BLTransaction()
                    {
                        Name = transaction.Name,
                        Amount = transaction.Amount,
                        Category = transaction.Category,
                        Description = transaction.Description,
                        Date = transaction.Date,
                        UserId = transaction.UserId,
                        TransactionId = transaction.TransactionId
                    });
                }
                return _BLTransactions;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BLTransaction>> GetFiltered(string UserId, string Name, string Category, string Description, DateTime StartDate, DateTime EndDate)
        {
            try
            {
               var transactions =  await _transactionRepository.GetFiltered(UserId, Name, Category, Description, StartDate, EndDate);

                Console.WriteLine(transactions.Count());
                if (transactions == null) return null;
               var _BLTransactions = new List<BLTransaction>();
                foreach (var transaction in transactions)
                {
                    _BLTransactions.Add(new BLTransaction()
                    {
                        Name = transaction.Name,
                        Amount = transaction.Amount,
                        Category = transaction.Category,
                        Description = transaction.Description,
                        Date = transaction.Date,
                        UserId = transaction.UserId,
                        TransactionId = transaction.TransactionId
                    });
                }
                return _BLTransactions;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BLTransaction>> GetFilteredByDateRange(string UserId, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var transactions = await _transactionRepository.GetFilteredByDateRange(UserId, StartDate,EndDate);
                if (transactions == null) return null;
                var _BLTransactions = new List<BLTransaction>();
                foreach (var transaction in transactions)
                {
                    _BLTransactions.Add(new BLTransaction()
                    {
                        Name = transaction.Name,
                        Amount = transaction.Amount,
                        Category = transaction.Category,
                        Description = transaction.Description,
                        Date = transaction.Date,
                        UserId = transaction.UserId,
                        TransactionId = transaction.TransactionId
                    });
                }
                return _BLTransactions;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BLTransaction> Restore(BLTransaction transaction)
        {
            try
            {
                Console.WriteLine("restore-");
                var mapper = AutoMappers.InitializeTransactionAutoMapper();
                Transaction _transaction = mapper.Map<Transaction>(transaction);
                _transaction = await _transactionRepository.Restore(_transaction);
                if (_transaction == null) return null;
                return transaction;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BLTransaction> Update(BLTransaction transaction)
        {
            try
            {
                var mapper = AutoMappers.InitializeTransactionAutoMapper();
                Transaction _transaction = mapper.Map<Transaction>(transaction);
                _transaction = await _transactionRepository.Update(_transaction);
                if (_transaction == null) return null;
                return transaction;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteMultiple(List<string> TransactionIds)
        {
            try
            {
                return await _transactionRepository.DeleteMultiple(TransactionIds);
            }
            catch
            {
                return false;
            }
        }
    }
}
